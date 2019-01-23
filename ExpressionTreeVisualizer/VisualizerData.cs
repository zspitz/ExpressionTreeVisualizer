using ExpressionTreeTransform;
using ExpressionTreeTransform.Util;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionTreeVisualizer {
    [Serializable]
    public class VisualizerData {
        public string Source { get; set; }
        public ExpressionNodeData NodeData { get; set; }
        public HashSet<EndNodeData> Constants { get; } = new HashSet<EndNodeData>();
        public HashSet<EndNodeData> Parameters { get; } = new HashSet<EndNodeData>();
        public HashSet<EndNodeData> ClosedVars { get; } = new HashSet<EndNodeData>();

        // for deserialization
        public VisualizerData() { }

        public VisualizerData(Expression expr, string language) {
            var node = expr.ToSyntaxNode(language, out var mappedSyntaxNodes);
            Source = node.ToFullString();
            NodeData = new ExpressionNodeData(expr, node, mappedSyntaxNodes, this);
        }
    }

    [Serializable]
    public class ExpressionNodeData {
        public Dictionary<string, ExpressionNodeData> Children { get; set; }
        public string NodeType { get; set; } // ideally this should be an intersection type of multiple enums
        public string ReflectionTypeName { get; set; }
        public (int start, int length) Span { get; set; }
        public string StringValue { get; set; }
        public string Name { get; set; }
        public string Closure { get; set; }
        public EndNodeTypes? EndNodeType { get; set; }

        public EndNodeData EndNodeData => new EndNodeData {
            Closure = Closure,
            Name=Name,
            Type=ReflectionTypeName,
            Value = StringValue
        };

        // for deserialization
        public ExpressionNodeData() { }

        public ExpressionNodeData(ElementInit init, SyntaxNode tree, ImmutableDictionary<object, SyntaxNode> mappedSyntaxNodes, VisualizerData visualizerData) {
            NodeType = "ElementInit";
            Children = init.Arguments.Select((x, index) => {
                return ($"Argument[{index}]", new ExpressionNodeData(x, tree, mappedSyntaxNodes, visualizerData));
            }).ToDictionary();
        }

        public ExpressionNodeData(MemberBinding binding, SyntaxNode tree, ImmutableDictionary<object, SyntaxNode> mappedSyntaxNodes,VisualizerData visualizerData) {
            Name = binding.Member.Name;
            NodeType = binding.BindingType.ToString();
            if (mappedSyntaxNodes.TryGetValue(binding, out var node)) {
                Span = (node.SpanStart, node.Span.Length);
            }
            switch (binding) {
                case MemberAssignment assignmentBinding:
                    Children = new[] {
                        ( "Expression", new ExpressionNodeData(assignmentBinding.Expression, tree, mappedSyntaxNodes, visualizerData) )
                    }.ToDictionary();
                    break;
                case MemberListBinding listBinding:
                    Children = listBinding.Initializers.Select((x, index) => ($"Iinitializers[{index}]", new ExpressionNodeData(x, tree, mappedSyntaxNodes, visualizerData))).ToDictionary();
                    break;
                case MemberMemberBinding memberBinding:
                    throw new NotImplementedException("MemberMemberBinding");
                default:
                    throw new NotImplementedException();
            }
        }

        public ExpressionNodeData(Expression expr, SyntaxNode tree, ImmutableDictionary<object, SyntaxNode> mappedSyntaxNodes, VisualizerData visualizerData) {
            // populate child nodes
            Children = expr.GetType().GetProperties().SelectMany(prp => {
                IEnumerable<(string, Expression)> ret = Enumerable.Empty<(string, Expression)>();
                if (prp.PropertyType.InheritsFromOrImplements<Expression>()) {
                    ret = new[] { (prp.Name, prp.GetValue(expr) as Expression) };
                } else if (prp.PropertyType.InheritsFromOrImplements<IEnumerable<Expression>>()) {
                    ret = (prp.GetValue(expr) as IEnumerable<Expression>).Select((x, index) => ($"{prp.Name}[{index}]", x));
                }
                return ret.Where(x => x.Item2 != null);
            }).Select(x => (x.Item1, new ExpressionNodeData(x.Item2, tree, mappedSyntaxNodes, visualizerData))).ToDictionary();

            switch (expr) {
                case MemberInitExpression initexpr:
                    initexpr.Bindings.Select((x, index) => ($"Binding[{index}]", new ExpressionNodeData(x, tree, mappedSyntaxNodes, visualizerData))).AddRangeTo(Children);
                    break;
                case ListInitExpression listinitexpr:
                    listinitexpr.Initializers.Select((x, index) => ($"Initializer[{index}]", new ExpressionNodeData(x, tree, mappedSyntaxNodes, visualizerData))).AddRangeTo(Children);
                    break;
            }

            // populate properties
            NodeType = expr.NodeType.ToString();
            ReflectionTypeName = expr.Type.FriendlyName(tree.Language);
            if (mappedSyntaxNodes.TryGetValue(expr, out var node)) {
                Span = (node.SpanStart, node.Span.Length);
                StringValue = node.GetAnnotations("stringValue").SingleOrDefault()?.Data;
            }

            // fill the Name and Closure properties
            switch (expr) {
                case ParameterExpression pexpr:
                    Name = pexpr.Name;
                    break;
                case MemberExpression mexpr:
                    Name = mexpr.Member.Name;
                    if (mexpr.Expression is ConstantExpression cexpr1 && cexpr1.Type.IsClosureClass()) {
                        Closure = cexpr1.Type.FriendlyName(tree.Language);
                    }
                    break;
            }

            switch (expr) {
                case ConstantExpression cexpr when !cexpr.Type.IsClosureClass():
                    EndNodeType = EndNodeTypes.Constant;
                    visualizerData.Constants.Add(EndNodeData);
                    break;
                case ParameterExpression pexpr1:
                    EndNodeType = EndNodeTypes.Parameter;
                    visualizerData.Parameters.Add(EndNodeData);
                    break;
                case MemberExpression mexpr when mexpr.Expression is ConstantExpression cexpr1 && cexpr1.Type.IsClosureClass():
                    EndNodeType = EndNodeTypes.ClosedVar;
                    visualizerData.ClosedVars.Add(EndNodeData);
                    break;
            }
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
        ClosedVar
    }
}
