using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Text;
using CS = Microsoft.CodeAnalysis.CSharp.SyntaxKind;
using VB = Microsoft.CodeAnalysis.VisualBasic.SyntaxKind;
using static Microsoft.CodeAnalysis.LanguageNames;

namespace ExpressionTreeTransform.Util {
    public static class SyntaxNodeExtensions {
        private static CS[] CSLiteralKinds = new[] { CS.CharacterLiteralExpression, CS.FalseLiteralExpression, CS.NullLiteralExpression, CS.NullLiteralExpression, CS.NumericLiteralExpression, CS.StringLiteralExpression, CS.TrueLiteralExpression };
        private static VB[] VBLiteralKinds = new[] { VB.CharacterLiteralExpression, VB.DateLiteralExpression, VB.FalseLiteralExpression, VB.FloatingLiteralToken, VB.IntegerLiteralToken, VB.NothingLiteralExpression, VB.NumericLiteralExpression, VB.StringLiteralExpression, VB.TrueLiteralExpression };
        public static bool IsLiteral(this SyntaxNode node) {
            if (node.Language == CSharp) { return ((CS)node.RawKind).In(CSLiteralKinds); }
            if (node.Language == VisualBasic) { return ((VB)node.RawKind).In(VBLiteralKinds); }
            throw new NotImplementedException();
        }
    }
}
