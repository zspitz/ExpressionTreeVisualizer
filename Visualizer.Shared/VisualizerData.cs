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
                var child = current.Children.Values.FirstOrDefault(x => x.Span.start <= start && x.SpanEnd >= end);
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

            //switch (o) {
            //    case Expression expr:
            //        NodeData = new ExpressionNodeData(expr, this, (0, Source.Length));
            //        break;
            //    case MemberBinding mbind:
            //        NodeData = new ExpressionNodeData(mbind, this);
            //        break;
            //    case ElementInit init:
            //        NodeData = new ExpressionNodeData(init, this);
            //        break;
            //    case SwitchCase switchCase:
            //        NodeData = new ExpressionNodeData(switchCase, this);
            //        break;
            //    case CatchBlock catchBlock:
            //        NodeData = new ExpressionNodeData(catchBlock, this);
            //        break;
            //    default:
            //        throw new ArgumentException($"Unable to visualize type {o.GetType().Name}");
            //}
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
        public Dictionary<string, ExpressionNodeData> Children { get; set; }
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
                .Select(x => (x.Item1, new ExpressionNodeData(x.Item2, visualizerData, Span)))
                .ToDictionary();
        }

        private static List<(Type, string[])> preferredPropertyOrders = new List<(Type, string[])> {
            (typeof(LambdaExpression), new [] {"Parameters", "Body" } )
        };


        //internal ExpressionNodeData(ElementInit init, VisualizerData visualizerData) {
        //    NodeType = "ElementInit";
        //    if (visualizerData.VisitedObjects.TryGetValue(init, out var spans)) {
        //        Span = spans.Single();
        //    }
        //    Children = init.Arguments.Select((x, index) => {
        //        return ($"Argument[{index}]", new ExpressionNodeData(x, visualizerData, Span));
        //    }).ToDictionary();
        //}

        //internal ExpressionNodeData(MemberBinding binding, VisualizerData visualizerData) {
        //    Name = binding.Member.Name;
        //    NodeType = binding.BindingType.ToString();
        //    if (visualizerData.VisitedObjects.TryGetValue(binding, out var spans)) {
        //        Span = spans.Single();
        //    }
        //    switch (binding) {
        //        case MemberAssignment assignmentBinding:
        //            Children = new[] {
        //                ( "Expression", new ExpressionNodeData(assignmentBinding.Expression, visualizerData, Span) )
        //            }.ToDictionary();
        //            break;
        //        case MemberListBinding listBinding:
        //            Children = listBinding.Initializers.Select((x, index) => ($"Iinitializers[{index}]", new ExpressionNodeData(x, visualizerData))).ToDictionary();
        //            break;
        //        case MemberMemberBinding memberBinding:
        //            Children = memberBinding.Bindings.Select((x, index) => ($"Bindings[{index}]", new ExpressionNodeData(x, visualizerData))).ToDictionary();
        //            break;
        //        default:
        //            throw new NotImplementedException();
        //    }
        //}

        //internal ExpressionNodeData(CatchBlock catchBlock, VisualizerData visualizerData) {
        //    NodeType = "CatchBlock";
        //    if (visualizerData.VisitedObjects.TryGetValue(catchBlock, out var spans)) {
        //        Span = spans.Single();
        //    }
        //    throw new NotImplementedException();
        //}

        //internal ExpressionNodeData(Expression expr, VisualizerData visualizerData, (int start, int length) parentSpan, bool isParameterDeclaration = false) {
        //    var language = visualizerData.Options.Language;

        //    // populate properties
        //    NodeType = expr.NodeType.ToString();
        //    ReflectionTypeName = expr.Type.FriendlyName(language);
        //    if (visualizerData.VisitedObjects.TryGetValue(expr, out var spans)) {
        //        Span = spans.Where(x => x.start >= parentSpan.start && (x.start + x.length) <= (parentSpan.start + parentSpan.length)).OrderBy(x => x.start).SingleOrDefault();
        //        //if (expr is ParameterExpression pexpr1) {
        //        //    Span = spans.Where(x => x.start >= parentSpan.start && (x.start + x.length) <= (parentSpan.start + parentSpan.length)).OrderBy(x => x.start).FirstOrDefault();
        //        //}  else if (expr is ConstantExpression) {
        //        //    Span = spans.Single();
        //        //}
        //    }
        //    IsDeclaration = isParameterDeclaration;

        //    // fill the Name and Closure properties
        //    switch (expr) {
        //        case ParameterExpression pexpr:
        //            Name = pexpr.Name;
        //            break;
        //        case MemberExpression mexpr:
        //            Name = mexpr.Member.Name;
        //            var expressionType = mexpr.Expression.Type;
        //            if (expressionType.IsClosureClass()) {
        //                Closure = expressionType.FriendlyName(language);
        //            }
        //            break;
        //        case MethodCallExpression callexpr:
        //            Name = callexpr.Method.Name;
        //            break;
        //    }

        //    // fill StringValue and EndNodeType properties
        //    switch (expr) {
        //        case ConstantExpression cexpr when !cexpr.Type.IsClosureClass():
        //            StringValue = StringValue(cexpr.Value, language);
        //            EndNodeType = Constant;
        //            break;
        //        case ParameterExpression pexpr1:
        //            EndNodeType = Parameter;
        //            break;
        //        case Expression e1 when expr.IsClosedVariable():
        //            StringValue = StringValue(expr.ExtractValue(), language);
        //            EndNodeType = ClosedVar;
        //            break;
        //        case DefaultExpression defexpr:
        //            EndNodeType = Default;
        //            break;
        //    }
        //    if (EndNodeType != null) { visualizerData.CollectedEndNodes.Add(this); }

        //    // populate child nodes
        //    if (expr is LambdaExpression lambda) {
        //        // lambda expression needs special handling, because there is no other way to distinguish between parameter declaration and usage
        //        Children = lambda.Parameters.Select((x, index) => ($"Parameter[{index}]", new ExpressionNodeData(x, visualizerData, Span, true))).ToDictionary();
        //        Children["Body"] = new ExpressionNodeData(lambda.Body, visualizerData, Span);
        //    } else {
        //        Children = expr.GetType().GetProperties().OrderBy(x => x.Name).SelectMany(prp => {
        //            IEnumerable<(string, Expression)> ret = Enumerable.Empty<(string, Expression)>();
        //            if (prp.PropertyType.InheritsFromOrImplements<Expression>()) {
        //                ret = new[] { (prp.Name, prp.GetValue(expr) as Expression) };
        //            } else if (prp.PropertyType.InheritsFromOrImplements<IEnumerable<Expression>>()) {
        //                ret = (prp.GetValue(expr) as IEnumerable<Expression>).Select((x, index) => ($"{prp.Name}[{index}]", x));
        //            }
        //            return ret.Where(x => x.Item2 != null);
        //        }).Select(x => (x.Item1, new ExpressionNodeData(x.Item2, visualizerData, Span))).ToDictionary();

        //        switch (expr) {
        //            case MemberInitExpression initexpr:
        //                initexpr.Bindings.Select((x, index) => ($"Binding[{index}]", new ExpressionNodeData(x, visualizerData))).AddRangeTo(Children);
        //                break;
        //            case ListInitExpression listinitexpr:
        //                listinitexpr.Initializers.Select((x, index) => ($"Initializer[{index}]", new ExpressionNodeData(x, visualizerData))).AddRangeTo(Children);
        //                break;
        //        }
        //    }
        //}

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