using System.Collections;
using System.Linq.Expressions;
using Xunit;
using static ExpressionToString.Tests.Globals;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        public void MakeConditional() {
            var i = Parameter(typeof(int), "i");
            RunTest(
                Condition(
                    GreaterThan(i, Constant(10)),
                    i,
                    Add(i, Constant(10))
                ),
                "i > 10 ? i : i + 10",
                "If(i > 10, i, i + 10)", 
                @"Condition(
    GreaterThan(i,
        Constant(10)
    ),
    i,
    Add(i,
        Constant(10)
    )
)"
            );
        }

        [Fact]
        public void MakeTypeCheck() => RunTest(
            TypeIs(
                Constant(""),
                typeof(string)
            ),
            "\"\" is string",
            "TypeOf \"\" Is String", 
            @"TypeIs(
    Constant(""""),
    typeof(string)
)"
        );

        [Fact]
        public void MakeTypeEqual() => RunTest(
            TypeEqual(
                Constant(""),
                typeof(IEnumerable)
            ),
            "\"\".GetType() == typeof(IEnumerable)",
            "\"\".GetType = GetType(IEnumerable)", 
            @"TypeEqual(
    Constant(""""),
    typeof(IEnumerable)
)"
        );

        [Fact]
        [Trait("Category",Invocation)]
        public void MakeInvocation() => RunTest(
            Invoke(
                Lambda(Constant(5))
            ),
            "(() => 5)()",
            "(Function() 5)()", 
            @"Invoke(
    Lambda(
        Constant(5)
    )
)"
        );

        [Fact]
        [Trait("Category", Lambdas)]
        public void MakeByRefParameter() => PreRunTest();

        [Fact]
        [Trait("Category", Quoted)]
        public void MakeQuoted() => PreRunTest();

        [Fact]
        [Trait("Category", Quoted)]
        public void MakeQuoted1() => PreRunTest();

        [Fact]
        [Trait("Category", DebugInfos)]
        public void MakeDebugInfo() => PreRunTest();

        [Fact]
        [Trait("Category", DebugInfos)]
        public void MakeClearDebugInfo() => PreRunTest();
    }
}
