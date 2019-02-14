using System;
using static System.Linq.Expressions.Expression;
using Xunit;
using static ExpressionToString.Tests.Runners;
using static ExpressionToString.Tests.Globals;

namespace ExpressionToString.Tests {
    [Trait("Source", CSharpCompiler)]
    public class Literals {
        [Fact]
        public void True() => BuildAssert(() => true, "() => true", "Function() True");

        [Fact]
        public void False() => BuildAssert(() => false, "() => false", "Function() False");

        [Fact]
        public void Nothing() => BuildAssert(() => (string)null, "() => null", "Function() Nothing");

        [Fact]
        public void Integer() => BuildAssert(() => 5, "() => 5", "Function() 5");

        [Fact]
        public void NonInteger() => BuildAssert(() => 7.32, "() => 7.32", "Function() 7.32");

        [Fact]
        public void String() => BuildAssert(() => "abcd", "() => \"abcd\"", "Function() \"abcd\"");

        #region VB-only literals
        [Trait("Source", FactoryMethods)]
        [Fact]
        public void DateTime() {
            var dte = new DateTime(1981, 1, 1);
            BuildAssert(Constant(dte), "#DateTime", $"#{dte.ToString()}#");
        }

        [Trait("Source", FactoryMethods)]
        [Fact]
        public void TimeSpan() {
            var ts = new TimeSpan(5, 4, 3, 2);
            BuildAssert(Constant(ts), "#TimeSpan", $"#{ts.ToString()}#");
        }
        #endregion

    }
}
