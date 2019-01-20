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

        // for deserialization
        public VisualizerData() { }

        public VisualizerData(Expression expr, string language) {
            var node = expr.ToSyntaxNode(language, out var mappedSyntaxNodes);
            Source = node.ToFullString();
            NodeData = new ExpressionNodeData(expr, node, mappedSyntaxNodes);
        }

        // TODO closed-over variables
    }

    [Serializable]
    public class ExpressionNodeData {
        public Dictionary<string, ExpressionNodeData> Children { get; set; }
        public string NodeType { get; set; } // ideally this should be an intersection type of multiple enums
        public string ReflectionTypeName { get; set; }
        public (int start, int length) Span { get; set; }
        public string StringValue { get; set; }
        public string Name { get; set; }

        // for deserialization
        public ExpressionNodeData() { }

        public ExpressionNodeData(ElementInit init, SyntaxNode tree, ImmutableDictionary<object, SyntaxNode> mappedSyntaxNodes) {
            NodeType = "ElementInit";
            Children = init.Arguments.Select((x, index) => {
                return ($"Argument[{index}]", new ExpressionNodeData(x, tree, mappedSyntaxNodes));
            }).ToDictionary();
        }

        public ExpressionNodeData(MemberBinding binding, SyntaxNode tree, ImmutableDictionary<object, SyntaxNode> mappedSyntaxNodes) {
            Name = binding.Member.Name;
            NodeType = binding.BindingType.ToString();
            if (mappedSyntaxNodes.TryGetValue(binding, out var node)) {
                Span = (node.SpanStart, node.Span.Length);
            }
            switch (binding) {
                case MemberAssignment assignmentBinding:
                    Children = new[] {
                        ( "Expression", new ExpressionNodeData(assignmentBinding.Expression, tree, mappedSyntaxNodes) )
                    }.ToDictionary();
                    break;
                case MemberListBinding listBinding:
                    Children = listBinding.Initializers.Select((x, index) => ($"Iinitializers[{index}]", new ExpressionNodeData(x, tree, mappedSyntaxNodes))).ToDictionary();
                    break;
                case MemberMemberBinding memberBinding:
                    throw new NotImplementedException("MemberMemberBinding");
                default:
                    throw new NotImplementedException();
            }
        }

        public ExpressionNodeData(Expression expr, SyntaxNode tree, ImmutableDictionary<object, SyntaxNode> mappedSyntaxNodes) {
            Children = expr.GetType().GetProperties().SelectMany(prp => {
                IEnumerable<(string, Expression)> ret = Enumerable.Empty<(string, Expression)>();
                if (prp.PropertyType.InheritsFromOrImplements<Expression>()) {
                    ret = new[] { (prp.Name, prp.GetValue(expr) as Expression) };
                } else if (prp.PropertyType.InheritsFromOrImplements<IEnumerable<Expression>>()) {
                    ret = (prp.GetValue(expr) as IEnumerable<Expression>).Select((x, index) => ($"{prp.Name}[{index}]", x));
                }
                return ret.Where(x => x.Item2 != null);
            }).Select(x => (x.Item1, new ExpressionNodeData(x.Item2, tree, mappedSyntaxNodes))).ToDictionary();

            if (expr is MemberInitExpression initexpr) {
                initexpr.Bindings.Select((x, index) => ($"Binding[{index}]", new ExpressionNodeData(x, tree, mappedSyntaxNodes))).AddRangeTo(Children);
            }

            NodeType = expr.NodeType.ToString();
            ReflectionTypeName = expr.Type.FriendlyName(tree.Language);
            if (mappedSyntaxNodes.TryGetValue(expr, out var node)) {
                Span = (node.SpanStart, node.Span.Length);
                StringValue = node.GetAnnotations("stringValue").SingleOrDefault()?.Data;
            }
            if (expr is ParameterExpression pexpr) {
                Name = pexpr.Name;
            }
        }
    }
}
