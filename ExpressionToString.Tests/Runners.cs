using System;
using System.Linq.Expressions;
using Xunit;
using static ExpressionToString.FormatterNames;

namespace ExpressionToString.Tests {
    public static class Runners {
        public static void BuildAssert(Expression<Action> expr, string csharp, string vb)  => BuildAssert(expr as Expression, csharp, vb);
        public static void BuildAssert<T>(Expression<Action<T>> expr, string csharp, string vb) => BuildAssert(expr as Expression, csharp, vb);
        public static void BuildAssert<T1, T2>(Expression<Action<T1, T2>> expr, string csharp, string vb) => BuildAssert(expr as Expression, csharp, vb);

        public static void BuildAssert<T>(Expression<Func<T>> expr, string csharp, string vb) => BuildAssert(expr as Expression, csharp, vb);
        public static void BuildAssert<T1, T2>(Expression<Func<T1, T2>> expr, string csharp, string vb) => BuildAssert(expr as Expression, csharp, vb);
        public static void BuildAssert<T1, T2, T3>(Expression<Func<T1, T2, T3>> expr, string csharp, string vb) => BuildAssert(expr as Expression, csharp, vb);

        public static void BuildAssert(Expression expr, string csharp, string vb) {
            var testCSharpCode = expr.ToString(CSharp);
            var testVBCode = expr.ToString(VisualBasic);
            Assert.Equal(csharp, testCSharpCode);
            Assert.Equal(vb, testVBCode);
        }

        public static void BuildAssert(MemberBinding mbind, string csharp, string vb) {
            var testCSharpCode = mbind.ToString(CSharp);
            var testVBCode = mbind.ToString(VisualBasic);
            Assert.Equal(csharp, testCSharpCode);
            Assert.Equal(vb, testVBCode);
        }

        public static void BuildAssert(ElementInit init, string csharp, string vb) {
            var testCSharpCode = init.ToString(CSharp);
            var testVBCode = init.ToString(VisualBasic);
            Assert.Equal(csharp, testCSharpCode);
            Assert.Equal(vb, testVBCode);
        }

        public static void BuildAssert(SwitchCase switchCase, string csharp, string vb) {
            var testCSharpCode = switchCase.ToString(CSharp);
            var testVBCode = switchCase.ToString(VisualBasic);
            Assert.Equal(csharp, testCSharpCode);
            Assert.Equal(vb, testVBCode);
        }

        public static void BuildAssert(CatchBlock catchBlock, string csharp, string vb) {
            var testCSharpCode = catchBlock.ToString(CSharp);
            var testVBCode = catchBlock.ToString(VisualBasic);
            Assert.Equal(csharp, testCSharpCode);
            Assert.Equal(vb, testVBCode);
        }

        public static void BuildAssert(LabelTarget labelTarget, string csharp, string vb) {
            var testCSharpCode = labelTarget.ToString(CSharp);
            var testVBCode = labelTarget.ToString(VisualBasic);
            Assert.Equal(csharp, testCSharpCode);
            Assert.Equal(vb, testVBCode);
        }

        public static void BuildAssert(SymbolDocumentInfo symbolDocumentInfo, string csharp, string vb) {
            var testCSharpCode = symbolDocumentInfo.ToString(CSharp);
            var testVBCode = symbolDocumentInfo.ToString(VisualBasic);
            Assert.Equal(csharp, testCSharpCode);
            Assert.Equal(vb, testVBCode);
        }
    }
}
