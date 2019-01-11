using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ExpressionTreeTransform {
    public static class ExpressionExtension {

        // TODO within the visualizer, it may be possible to get the workspace / generator for the current code
        public static SyntaxNode ToSyntaxNode(this Expression expr, string language = LanguageNames.CSharp) =>
            new Mapper().GetSyntaxNode(expr, language);

        public static SyntaxNode ToSyntaxNode(this Expression expr, string language, out Dictionary<Expression, SyntaxNode> expressionSyntaxNodes) =>
            new Mapper().GetSyntaxNode(expr, language, out expressionSyntaxNodes);

        // TODO // format code based on current project settings / editor settings ?
        public static string ToCode(this Expression expr, string language = LanguageNames.CSharp) =>
            expr.ToSyntaxNode(language).ToFullString();
    }
}
