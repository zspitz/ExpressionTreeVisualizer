using ExpressionTreeTransform;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using ExpressionTreeTransform.Util;
using CS = Microsoft.CodeAnalysis.CSharp;
using VB = Microsoft.CodeAnalysis.VisualBasic;
using System.Collections.Immutable;

namespace ExpressionTreeVisualizer {
    [Serializable]
    public class VisualizerData {
        public string Source { get; set; }
        public ExpressionNodeData NodeData { get; set; }

        // for deserialization
        public VisualizerData() { }

        public VisualizerData(Expression expr, string language) {
            var node = expr.ToSyntaxNode(language, out var expressionSyntaxNodes);
            Source = node.ToFullString();
            NodeData = new ExpressionNodeData(expr, node, expressionSyntaxNodes);
        }

        // TODO closed-over variables
    }

    [Serializable]
    public class ExpressionNodeData {
        public Dictionary<string, ExpressionNodeData> Children { get; set; }
        public ExpressionType NodeType { get; set; }
        public string ReflectionTypeName { get; set; }
        public (int start, int length) Span { get; set; }
        public string ConstantValue { get; set; }

        // for deserialization
        public ExpressionNodeData() { }

        public ExpressionNodeData(Expression expr, SyntaxNode tree, ImmutableDictionary<Expression, SyntaxNode> expressionSyntaxNode) {
            Children = expr.GetType().GetProperties().SelectMany(prp => {
                IEnumerable<(string, Expression)> ret = Enumerable.Empty<(string, Expression)>();
                if (prp.PropertyType.InheritsFromOrImplements<Expression>()) {
                    ret = new[] { (prp.Name, prp.GetValue(expr) as Expression) };
                } else if (prp.PropertyType.InheritsFromOrImplements<IEnumerable<Expression>>()) {
                    ret = (prp.GetValue(expr) as IEnumerable<Expression>).Select((x, index) => ($"{prp.Name}[{index}]", x));
                }
                return ret.Where(x => x.Item2 != null);
            }).Select(x => {
                if (expressionSyntaxNode.TryGetValue(x.Item2, out var node)) {
                    Span = (node.SpanStart, node.Span.Length);
                }
                return (x.Item1, new ExpressionNodeData(x.Item2, tree, expressionSyntaxNode));
            }).ToDictionary();

            NodeType = expr.NodeType;
            ReflectionTypeName = expr.Type.Name;

            if (expr is ConstantExpression cexpr) {
                var sn = expressionSyntaxNode[expr];
                if (sn.IsLiteral()) {
                    ConstantValue = sn.ToFullString();
                } else if (cexpr.Type.UnderlyingIfNullable().In(typeof(DateTime), typeof(TimeSpan))) {
                    ConstantValue = cexpr.Value.ToString();
                }
            }
        }
    }
}
