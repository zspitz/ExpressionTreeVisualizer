using ExpressionToString;
using ExpressionToString.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using static ExpressionToString.Util.Functions;
using static ExpressionTreeVisualizer.EndNodeTypes;
using static ExpressionToString.FormatterNames;
using System.Collections;
using System.Runtime.CompilerServices;
using static ExpressionToString.Globals;
using System.Reflection;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace ExpressionTreeVisualizer {
    [Serializable]
    public class VisualizerDataOptions : INotifyPropertyChanged {
        private string _formatter = CSharp;
        public string Formatter {
            get => _formatter;
            set {
                Language = ResolveLanguage(value);
                this.NotifyChanged(ref _formatter, value, args => PropertyChanged?.Invoke(this, args));
            }
        }

        private string _language = CSharp;
        public string Language {
            get => _language;
            set => this.NotifyChanged(ref _language, value, args => PropertyChanged?.Invoke(this, args));
        }

        public string Path { get; set; }

        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        public VisualizerDataOptions(VisualizerDataOptions options = null) {
            if (options != null) {
                _formatter = options.Formatter;
                _language = options.Language;
                Path = options.Path;
            }
        }
    }

    [Serializable]
    public class VisualizerData {
        public string Source { get; set; }
        public VisualizerDataOptions Options { get; set; }
        public ExpressionNodeData NodeData { get; set; }

        [NonSerialized] // the items in this List are grouped and serialized into separate collections
        public List<ExpressionNodeData> CollectedEndNodes;
        [NonSerialized]
        public Dictionary<string, (int start, int length)> PathSpans;

        public Dictionary<EndNodeData, List<ExpressionNodeData>> Constants { get; }
        public Dictionary<EndNodeData, List<ExpressionNodeData>> Parameters { get; }
        public Dictionary<EndNodeData, List<ExpressionNodeData>> ClosedVars { get; }
        public Dictionary<EndNodeData, List<ExpressionNodeData>> Defaults { get; }

        public ExpressionNodeData FindNodeBySpan(int start, int length) {
            var end = start + length;
            //if (start < NodeData.Span.start || end > NodeData.SpanEnd) { throw new ArgumentOutOfRangeException(); }
            var current = NodeData;
            while (true) {
                var child = current.Children.SingleOrDefault(x => x.Span.start <= start && x.SpanEnd >= end);
                if (child == null) { break; }
                current = child;
            }
            return current;
        }

        // for deserialization
        public VisualizerData() { }

        public VisualizerData(object o, VisualizerDataOptions options = null) {
            Options = options ?? new VisualizerDataOptions();
            if (!Options.Path.IsNullOrWhitespace()) {
                o = (ResolvePath(o, options.Path) as Expression).ExtractValue();
            }
            Source = WriterBase.Create(o, Options.Formatter, Options.Language, out var pathSpans).ToString();
            PathSpans = pathSpans;
            CollectedEndNodes = new List<ExpressionNodeData>();
            NodeData = new ExpressionNodeData(o, ("", ""), this, false);

            // TODO it should be possible to write the following using LINQ
            Constants = new Dictionary<EndNodeData, List<ExpressionNodeData>>();
            Parameters = new Dictionary<EndNodeData, List<ExpressionNodeData>>();
            ClosedVars = new Dictionary<EndNodeData, List<ExpressionNodeData>>();
            Defaults = new Dictionary<EndNodeData, List<ExpressionNodeData>>();

            foreach (var x in CollectedEndNodes) {
                Dictionary<EndNodeData, List<ExpressionNodeData>> dict;
                switch (x.EndNodeType) {
                    case Constant:
                        dict = Constants;
                        break;
                    case Parameter:
                        dict = Parameters;
                        break;
                    case ClosedVar:
                        dict = ClosedVars;
                        break;
                    case Default:
                        dict = Defaults;
                        break;
                    default:
                        throw new InvalidOperationException();
                }

                if (!dict.TryGetValue(x.EndNodeData, out var lst)) {
                    lst = new List<ExpressionNodeData>();
                    dict[x.EndNodeData] = lst;
                }
                lst.Add(x);
            }
        }
    }

    [Serializable]
    [DebuggerDisplay("{FullPath}")]
    public class ExpressionNodeData : INotifyPropertyChanged {
        public List<ExpressionNodeData> Children { get; set; }
        public string NodeType { get; set; } // ideally this should be an intersection type of multiple enums
        public string ReflectionTypeName { get; set; }
        public (int start, int length) Span { get; set; }
        public int SpanEnd => Span.start + Span.length;
        public string StringValue { get; set; }
        public string Name { get; set; }
        public string Closure { get; set; }
        public EndNodeTypes? EndNodeType { get; set; }
        public bool IsDeclaration { get; set; }
        public string PathFromParent { get; set; } = "";
        public string FullPath { get; set; } = "";
        public (string @namespace, string typename, string propertyname)? ParentProperty { get; set; }
        public List<(string @namespace, string enumTypename, string membername)> NodeTypesParts { get; set; }

        private List<(string @namespace, string typename)> _baseTypes;
        public List<(string @namespace, string typename)> BaseTypes => _baseTypes;
        public string WatchExpressionFormatString { get; set; }
        public bool EnableValueInNewWindow { get; set; }

        private string[] _factoryMethodNames;
        public string[] FactoryMethodNames => _factoryMethodNames;

        public EndNodeData EndNodeData => new EndNodeData {
            Closure = Closure,
            Name = Name,
            Type = ReflectionTypeName,
            Value = StringValue
        };

        // for deserialization
        public ExpressionNodeData() { }

        private static HashSet<Type> propertyTypes = NodeTypes.SelectMany(x => new[] { x, typeof(IEnumerable<>).MakeGenericType(x) }).ToHashSet();

        internal ExpressionNodeData(object o, (string aggregatePath, string pathFromParent) path, VisualizerData visualizerData, bool isParameterDeclaration = false, PropertyInfo pi = null, string parentWatchExpression = "") {
            var (aggregatePath, pathFromParent) = path;
            PathFromParent = pathFromParent;
            if (aggregatePath.IsNullOrWhitespace() || pathFromParent.IsNullOrWhitespace()) {
                FullPath = aggregatePath + pathFromParent;
            } else {
                FullPath = $"{aggregatePath}.{pathFromParent}";
            }

            var language = visualizerData.Options.Language;
            switch (o) {
                case Expression expr:
                    NodeType = expr.NodeType.ToString();
                    NodeTypesParts = new List<(string @namespace, string enumTypename, string membername)> {
                        (typeof(ExpressionType).Namespace, nameof(ExpressionType), NodeType)
                    };
                    if (expr is GotoExpression gexpr) {
                        NodeTypesParts.Add(
                            typeof(GotoExpressionKind).Namespace, nameof(GotoExpressionKind), gexpr.Kind.ToString()
                        );
                    }
                    ReflectionTypeName = expr.Type.FriendlyName(language);
                    IsDeclaration = isParameterDeclaration;

                    // fill the Name and Closure properties, for expressions
                    Name = expr.Name(language);

                    if (expr is MemberExpression mexpr && 
                        mexpr.Expression?.Type is Type expressionType &&
                        expressionType.IsClosureClass()) {
                            Closure = expressionType.Name;
                    }

                    object value = null;

                    // fill StringValue and EndNodeType properties, for expressions
                    switch (expr) {
                        case ConstantExpression cexpr when !cexpr.Type.IsClosureClass():
                            value = cexpr.Value;
                            EndNodeType = Constant;
                            break;
                        case ParameterExpression pexpr1:
                            EndNodeType = Parameter;
                            break;
                        case Expression e1 when expr.IsClosedVariable():
                            value = expr.ExtractValue();
                            EndNodeType = ClosedVar;
                            break;
                        case DefaultExpression defexpr:
                            value = defexpr.ExtractValue();
                            EndNodeType = Default;
                            break;
                    }
                    if (EndNodeType != null) { visualizerData.CollectedEndNodes.Add(this); }

                    if (value != null) {
                        StringValue = StringValue(value, language);
                        EnableValueInNewWindow = value.GetType().InheritsFromOrImplementsAny(NodeTypes);
                    }

                    break;
                case MemberBinding mbind:
                    NodeType = mbind.BindingType.ToString();
                    NodeTypesParts = new List<(string @namespace, string enumTypename, string membername)> {
                        (typeof(MemberBindingType).Namespace, nameof(MemberBindingType), NodeType)
                    };    
                    Name = mbind.Member.Name;
                    break;
                case CallSiteBinder callSiteBinder:
                    NodeType = callSiteBinder.BinderType();
                    break;
                default:
                    NodeType = o.GetType().FriendlyName(language);
                    break;
            }

            if (visualizerData.PathSpans.TryGetValue(FullPath, out var span)) {
                Span = span;
            }

            if (parentWatchExpression.IsNullOrWhitespace()) {
                WatchExpressionFormatString = "{0}";
            } else if (pi != null) {
                var watchPathFromParent = PathFromParent;
                if (visualizerData.Options.Language == CSharp) {
                    WatchExpressionFormatString = $"(({pi.DeclaringType.FullName}){parentWatchExpression}).{watchPathFromParent}";
                } else {  //VisualBasic
                    watchPathFromParent = watchPathFromParent.Replace("[", "(").Replace("]", ")");
                    WatchExpressionFormatString = $"CType({parentWatchExpression}, {pi.DeclaringType.FullName}).{watchPathFromParent}";
                }
            }

            // populate Children
            var type = o.GetType();
            var preferredOrder = PreferredPropertyOrders.FirstOrDefault(x => x.Item1.IsAssignableFrom(type)).Item2;
            Children = type.GetProperties()
                .Where(prp =>
                    !(prp.DeclaringType.Name == "BlockExpression" && prp.Name == "Result") &&
                    propertyTypes.Any(x => x.IsAssignableFrom(prp.PropertyType))
                )
                .OrderBy(prp => {
                    if (preferredOrder == null) { return -1; }
                    return Array.IndexOf(preferredOrder, prp.Name);
                })
                .ThenBy(prp => prp.Name)
                .SelectMany(prp => {
                    if (prp.PropertyType.InheritsFromOrImplements<IEnumerable>()) {
                        return (prp.GetValue(o) as IEnumerable).Cast<object>().Select((x, index) => ($"{prp.Name}[{index}]", x, prp));
                    } else {
                        return new[] { (prp.Name, prp.GetValue(o), prp) };
                    }
                })
                .Where(x => x.x != null)
                .SelectT((relativePath, o1, prp) => new ExpressionNodeData(o1, (FullPath ?? "", relativePath), visualizerData, false, prp, WatchExpressionFormatString))
                .ToList();

            // populate URLs
            if (pi != null) {
                ParentProperty = (pi.DeclaringType.Namespace, pi.DeclaringType.Name, pi.Name);
            }

            if (!baseTypes.TryGetValue(o.GetType(), out _baseTypes)) {
                _baseTypes = o.GetType().BaseTypes(true, true).Where(x => x != typeof(object) && x.IsPublic).Select(x => (x.Namespace, x.Name)).Distinct().ToList();
                baseTypes[o.GetType()] = _baseTypes;
            }

            string factoryMethodName = null;
            if (o is BinaryExpression || o is UnaryExpression) {
                BinaryUnaryMethods.TryGetValue(((Expression)o).NodeType, out factoryMethodName);
            }
            if (factoryMethodName.IsNullOrWhitespace()) {
                var publicType = o.GetType().BaseTypes(false, true).FirstOrDefault(x => !x.IsInterface && x.IsPublic);
                factoryMethods.TryGetValue(publicType, out _factoryMethodNames);
            } else {
                _factoryMethodNames = new[] { factoryMethodName };
            }
        }

        private static Dictionary<Type, List<(string @namespace, string typename)>> baseTypes = new Dictionary<Type, List<(string @namespace, string typename)>>();

        private static Dictionary<Type, string[]> factoryMethods = typeof(Expression).GetMethods()
            .Where(x => x.IsStatic)
            .GroupBy(x => x.ReturnType, x => x.Name)
            .ToDictionary(x => x.Key, x => x.Distinct().Ordered().ToArray());


        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isSelected;
        public bool IsSelected {
            get => _isSelected;
            set => this.NotifyChanged(ref _isSelected, value, args => PropertyChanged?.Invoke(this, args));
        }

        public void ClearSelection() {
            IsSelected = false;
            Children.ForEach(x => x.ClearSelection());
        }
    }

    [Serializable]
    [SuppressMessage("", "IDE0032", Justification = "https://github.com/dotnet/core/issues/2981")]
    public struct EndNodeData {
        private string _closure;
        private string _name;
        private string _type;
        private string _value;

        public string Closure {
            get => _closure;
            set => _closure = value;
        }
        public string Name {
            get => _name;
            set => _name = value;
        }
        public string Type {
            get => _type;
            set => _type = value;
        }
        public string Value {
            get => _value;
            set => _value = value;
        }
    }

    public enum EndNodeTypes {
        Constant,
        Parameter,
        ClosedVar,
        Default
    }
}
