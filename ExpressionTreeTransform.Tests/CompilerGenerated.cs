using System;
using System.Linq.Expressions;
using Xunit;
using static ExpressionTreeTransform.Util.Globals;

namespace ExpressionTreeTransform.Tests {
    public class CompilerGenerated {
        private void buildAssert<T>(Expression<Func<T>>expr, string csharp, string vb) {
            var testCSharpCode = expr.ToCode(CSharp);
            var testVBCode = expr.ToCode(VisualBasic);
            Assert.True(testCSharpCode == csharp);
            Assert.True(testVBCode == vb);
        }

        [Fact]
        public void ReturnBooleanTruel() => buildAssert(() => true, "() => true", "Function() True");

        [Fact]
        public void ReturnBooleanFalse() => buildAssert(() => false, "() => false", "Function() False");

        [Fact]
        public void ReturnMemberAccess() => buildAssert(() => "abcd".Length, "() => \"abcd\".Length", "Function() \"abcd\".Length");

        [Fact]
        public void ReturnObjectCreation() => buildAssert(() => new DateTime(1980, 1, 1), "() => new DateTime(1980, 1, 1)", "Function() New Date(1980, 1, 1)");
    }
}
