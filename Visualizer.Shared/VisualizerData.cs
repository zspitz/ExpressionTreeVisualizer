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

namespace ExpressionTreeVisualizer {
    [Serializable]
    public class VisualizerDataOptions : INotifyPropertyChanged {
        private string _language = CSharp;
        public string Language {
            get => _language;
            set => this.NotifyChanged(ref _language, value, args => PropertyChanged?.Invoke(this, args));
        }

        [field:NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
    }

    [Serializable]
    public class VisualizerData {
        public string Source { get; set; }
        public VisualizerDataOptions Options { get; set; }
        public ExpressionNodeData NodeData { get; set; }

        [NonSerialized] // the items in this List are grouped and serialized into separate collections
        public List<ExpressionNodeData> CollectedEndNodes;
        [NonSerialized]
        public Dictionary<object, List<(int start, int length)>> VisitedObjects;

        public Dictionary<EndNodeData, List<ExpressionNodeData>> Constants { get; }
        public Dictionary<EndNodeData, List<ExpressionNodeData>> Parameters { get; }
        public Dictionary<EndNodeData, List<ExpressionNodeData>> ClosedVars { get; }
        public Dictionary<EndNodeData, List<ExpressionNodeData>> Defaults { get; }
        internal HashSet<(object o, int start, int length)> VisitedSpans { get; }

        public ExpressionNodeData FindNodeBySpan(int start, int length) {
            var end = start + length;
            if (start < NodeData.Span.start || end > NodeData.SpanEnd) { throw new ArgumentOutOfRangeException(); }
            var current = NodeData;
            while (true) {
                // we should really use SingleOrDefault, except that multiple instances of the same ParameterExpression might be returned, because we can't figure out the right start and end for multiple ParameterExpression
                var child = current.Children.Values().FirstOrDefault(x => x.Span.start <= start && x.SpanEnd >= end);
                if (child == null) { break; }
                current = child;
            }
            return current;
        }

        // for deserialization
        public VisualizerData() { }

        public VisualizerData(object o, VisualizerDataOptions options = null) {
            Options = options ?? new VisualizerDataOptions();
            Source = CodeWriter.Create(Options.Language, o, out var visitedObjects).ToString();
            VisitedObjects = visitedObjects;
            CollectedEndNodes = new List<ExpressionNodeData>();
            NodeData = new ExpressionNodeData(o, this, (0, Source.Length), false);

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
    public class ExpressionNodeData : INotifyPropertyChanged {
        public  List<KeyValuePair<string, ExpressionNodeData>> Children { get; set; }
        public string NodeType { get; set; } // ideally this should be an intersection type of multiple enums
        public string ReflectionTypeName { get; set; }
        public (int start, int length) Span { get; set; }
        public int SpanEnd => Span.start + Span.length;
        public string StringValue { get; set; }
        public string Name { get; set; }
        public string Closure { get; set; }
        public EndNodeTypes? EndNodeType { get; set; }
        public bool IsDeclaration { get; set; }

        public EndNodeData EndNodeData => new EndNodeData {
            Closure = Closure,
            Name = Name,
            Type = ReflectionTypeName,
            Value = StringValue
        };

        // for deserialization
        public ExpressionNodeData() { }

        private static HashSet<Type> types = new HashSet<Type>() {
            typeof(Expression),
            typeof(MemberBinding),
            typeof(ElementInit),
            typeof(SwitchCase),
            typeof(CatchBlock)
        };

        private static HashSet<Type> propertyTypes = types.SelectMany(x => new[] { x, typeof(IEnumerable<>).MakeGenericType(x) }).ToHashSet();

        internal ExpressionNodeData(object o, VisualizerData visualizerData, (int start, int length) parentSpan, bool isParameterDeclaration = false) {
            switch (o) {
                case Expression expr:
                    var language = visualizerData.Options.Language;
                    NodeType = expr.NodeType.ToString();
                    ReflectionTypeName = expr.Type.FriendlyName(language);
                    IsDeclaration = isParameterDeclaration;

                    // fill the Name and Closure properties, for expressions
                    switch (expr) {
                        case ParameterExpression pexpr:
                            Name = pexpr.Name;
                            break;
                        case MemberExpression mexpr:
                            Name = mexpr.Member.Name;
                            var expressionType = mexpr.Expression?.Type;
                            if (expressionType.IsClosureClass()) {
                                Closure = expressionType.FriendlyName(language);
                            }
                            break;
                        case MethodCallExpression callexpr:
                            Name = callexpr.Method.Name;
                            break;
                    }

                    // fill StringValue and EndNodeType properties, for expressions
                    switch (expr) {
                        case ConstantExpression cexpr when !cexpr.Type.IsClosureClass():
                            StringValue = StringValue(cexpr.Value, language);
                            EndNodeType = Constant;
                            break;
                        case ParameterExpression pexpr1:
                            EndNodeType = Parameter;
                            break;
                        case Expression e1 when expr.IsClosedVariable():
                            StringValue = StringValue(expr.ExtractValue(), language);
                            EndNodeType = ClosedVar;
                            break;
                        case DefaultExpression defexpr:
                            EndNodeType = Default;
                            break;
                    }
                    if (EndNodeType != null) { visualizerData.CollectedEndNodes.Add(this); }

                    break;
                case MemberBinding mbind:
                    NodeType = mbind.BindingType.ToString();
                    Name = mbind.Member.Name;
                    break;
                default:
                    NodeType = o.GetType().ToString();
                    break;
            }

            if (visualizerData.VisitedObjects.TryGetValue(o, out var spans)) {
                var matchedSpans = spans.Where(x => parentSpan.start <= x.start && parentSpan.length >= x.length).ToList();
                if (matchedSpans.Count >= 1) {
                    Span = matchedSpans.First();
                    spans.Remove(Span);
                }
            }

            // TODO specify order for properties; sometimes alphabetical order is not preferred; e.g. Parameters then Body for LambdaExpression

            // populate Children
            var type = o.GetType();
            var preferredOrder = preferredPropertyOrders.FirstOrDefault(x => x.Item1.IsAssignableFrom(type)).Item2;
            Children = type.GetProperties()
                .Where(prp => propertyTypes.Any(x => x.IsAssignableFrom(prp.PropertyType)))
                .OrderBy(prp => {
                    if (preferredOrder == null) { return -1; }
                    return Array.IndexOf(preferredOrder, prp.Name);
                })
                .ThenBy(prp => prp.Name)
                .SelectMany(prp => {
                    if (prp.PropertyType.InheritsFromOrImplements<IEnumerable>()) {
                        var (left, right)= visualizerData.Options.Language == CSharp ? ('[',']') : ('(',')');
                        return (prp.GetValue(o) as IEnumerable).Cast<object>().Select((x, index) => ($"{prp.Name}{left}{index}{right}", x));
                    } else {
                        return new[] { (prp.Name, prp.GetValue(o)) };
                    }
                })
                .Where(x => x.Item2 != null)
                .Select(x => KVP(x.Item1, new ExpressionNodeData(x.Item2, visualizerData, Span)))
                .ToList();
        }

        private static List<(Type, string[])> preferredPropertyOrders = new List<(Type, string[])> {
            (typeof(LambdaExpression), new [] {"Parameters", "Body" } ),
            (typeof(BinaryExpression), new [] {"Left", "Right", "Conversion"}),
            (typeof(BlockExpression), new [] { "Variables", "Expressions", "Result"}),
            (typeof(CatchBlock), new [] { "Variable", "Filter", "Body"}),
            (typeof(ConditionalExpression), new [] { "Test", "IfTrue", "IfFalse"}),
            (typeof(IndexExpression), new [] { "Object", "Arguments" }),
            (typeof(InvocationExpression), new [] {"Arguments", "Expression"}),
            (typeof(ListInitExpression), new [] {"NewExpression", "Initializers"}),
            (typeof(MemberInitExpression), new [] {"NewExpression", "Bindings"}),
            (typeof(MethodCallExpression), new [] {"Object", "Arguments"}),
            (typeof(SwitchCase), new [] {"TestValues", "Body"}),
            (typeof(SwitchExpression), new [] {"SwitchValue", "Cases", "DefaultBody"}),
            (typeof(TryExpression), new [] {"Body", "Handlers", "Finally", "Fault"})
        };

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isSelected;
        public bool IsSelected {
            get => _isSelected;
            set => this.NotifyChanged(ref _isSelected, value, args => PropertyChanged?.Invoke(this, args));
        }

        public void ClearSelection() {
            IsSelected = false;
            Children.ForEach(x => x.Value.ClearSelection());
        }
    }

    [Serializable]
    public struct EndNodeData {
        public string Closure { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
    }

    public enum EndNodeTypes {
        Constant,
        Parameter,
        ClosedVar,
        Default
    }
}


// TODO write method to load span into this ExpressionNodeData