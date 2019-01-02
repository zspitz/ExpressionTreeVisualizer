using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using System;
using System.Linq;
using System.Linq.Expressions;
using static System.Linq.Expressions.ExpressionType;

namespace ExpressionTreeTransform {
    public static class ExpressionExtension {

        // TODO within the visualizer, it may be possible to get the workspace / generator for the current code
        public static SyntaxNode ToSyntaxNode(this Expression expr, string language = LanguageNames.CSharp)  => 
            new Mapper().GetSyntaxNode(expr, language);

        public static string ToCode(this Expression expr, string language = LanguageNames.CSharp) {
            // pass expression to ToSyntaxNode

            // generate string from Roslyn syntax tree

            // format code based on current project settings / editor settings ?

            throw new NotImplementedException();
        }
    }
}
