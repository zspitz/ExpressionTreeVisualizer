using System;
using System.Linq.Expressions;
using Xunit;
using static ExpressionTreeTransform.Util.Globals;

namespace ExpressionTreeTransform.Tests {
    public static class Runners {
        public static void BuildAssert<T>(Expression<Func<T>> expr, string csharp, string vb) {
            BuildAssert(expr as Expression, csharp, vb);
        }

        public static void BuildAssert(Expression expr, string csharp, string vb) {
            var testCSharpCode = expr.ToCode(CSharp);
            var testVBCode = expr.ToCode(VisualBasic);
            Assert.True(testCSharpCode == csharp);
            Assert.True(testVBCode == vb);
        }
    }
}
