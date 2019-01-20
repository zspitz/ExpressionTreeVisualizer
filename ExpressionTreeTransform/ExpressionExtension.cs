using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;

namespace ExpressionTreeTransform {
    public static class ExpressionExtension {

        // TODO within the visualizer, it may be possible to get the workspace / generator for the current code
        public static SyntaxNode ToSyntaxNode(this Expression expr, string language = LanguageNames.CSharp) =>
            new Mapper().GetSyntaxNode(expr, language);

        public static SyntaxNode ToSyntaxNode(this Expression expr, string language, out ImmutableDictionary<object, SyntaxNode> mappedSyntaxNodes) =>
            new Mapper().GetSyntaxNode(expr, language, out mappedSyntaxNodes);

        // TODO // format code based on current project settings / editor settings ?
        public static string ToCode(this Expression expr, string language = LanguageNames.CSharp) =>
            expr.ToSyntaxNode(language).ToFullString();

        internal static object ExtractValue(this Expression expr) =>
            Lambda(expr).Compile().DynamicInvoke();
    }
}
