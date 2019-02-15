using Xunit;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Runners;
using static System.Linq.Expressions.Expression;

namespace ExpressionToString.Tests {
    public class Unsorted {
        [Trait("Source", CSharpCompiler)]
        [Fact]
        public void Conditional() => BuildAssert(
            (int i) => i > 10 ? i : i + 10,
            "(int i) => i > 10 ? i : i + 10",
            "Function(i As Integer) If(i > 10, i, i + 10)"
        );

        [Trait("Source", FactoryMethods)]
        [Fact]
        public void MakeConditional() {
            var i = Parameter(typeof(int), "i");
            BuildAssert(
                Condition(
                    GreaterThan(i, Constant(10)),
                    i,
                    Add(i, Constant(10))
                ),
                "i > 10 ? i : i + 10",
                "If(i > 10, i, i + 10)"
            );
        }
    }
}
