using System;
using System.Linq.Expressions;
using Xunit;
using static ExpressionToString.Util.FormatterNames;

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
    }
}
