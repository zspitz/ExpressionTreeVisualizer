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
            var testCSharpCode = expr.ToString(CSharp).Replace("\r\n", "\n");
            var testVBCode = expr.ToString(VisualBasic).Replace("\r\n", "\n");
            Assert.Equal(csharp, testCSharpCode);
            Assert.Equal(vb, testVBCode);
        }

        public static void BuildAssert(MemberBinding mbind, string csharp, string vb) {
            var testCSharpCode = mbind.ToString(CSharp).Replace("\r\n", "\n");
            var testVBCode = mbind.ToString(VisualBasic).Replace("\r\n", "\n");
            Assert.Equal(csharp, testCSharpCode);
            Assert.Equal(vb, testVBCode);
        }

        public static void BuildAssert(ElementInit init, string csharp, string vb) {
            var testCSharpCode = init.ToString(CSharp).Replace("\r\n", "\n");
            var testVBCode = init.ToString(VisualBasic).Replace("\r\n", "\n");
            Assert.Equal(csharp, testCSharpCode);
            Assert.Equal(vb, testVBCode);
        }

        public static void BuildAssert(CatchBlock catchBlock, string csharp, string vb) => throw new NotImplementedException();
        public static void BuildAssert(SwitchCase switchCase, string csharp, string vb) => throw new NotImplementedException();
        public static void BuildAssert(LabelTarget labelTarget, string csharp, string vb) => throw new NotImplementedException();

        public static void BuildAssert(Type type, string csharp, string vb) => throw new NotImplementedException();
        public static void BuildAssert(SymbolDocumentInfo symbolDocumentInfo, string csharp, string vb) => throw new NotImplementedException();
        public static void BuildAssert(bool @bool, string csharp, string vb) => throw new NotImplementedException();
    }
}
