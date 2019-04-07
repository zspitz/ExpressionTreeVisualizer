using System.Collections;
using Xunit;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Runners;
using static System.Linq.Expressions.Expression;

namespace ExpressionToString.Tests.Constructed {
    [Trait("Source", FactoryMethods)]
    public class Misc {
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

        [Fact]
        public void MakeTypeCheck() => BuildAssert(
            TypeIs(
                Constant(""),
                typeof(string)
            ),
            "\"\" is string",
            "TypeOf \"\" Is String"
        );

        [Fact]
        public void MakeTypeEqual() => BuildAssert(
            TypeEqual(
                Constant(""),
                typeof(IEnumerable)
            ),
            "\"\".GetType() == typeof(IEnumerable)",
            "\"\".GetType = GetType(IEnumerable)"
        );

        [Fact]
        public void MakeInvocation() => BuildAssert(
            Invoke(
                Lambda(Constant(5))
            ),
            "(() => 5)()",
            "(Function() 5)()"
        );

        [Fact]
        public void MakeByRefParameter() => BuildAssert(
            Lambda(
                Constant(true),
                Parameter(typeof(string).MakeByRefType(), "s4")
            ),
            "(ref string s4) => true",
            "Function(ByRef s4 As String) True"
        );
    }
}
