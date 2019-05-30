using Xunit;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        [Trait("Category", Loops)]
        public void EmptyLoop() => RunTest(
            Loop(Constant(true)),
            @"while (true) {
    true;
}",
            @"Do
    True
Loop",
            @"Loop(
    Constant(true)
)"
            );

        [Fact]
        [Trait("Category", Loops)]
        public void EmptyLoop1() => RunTest(
            Loop(
                Block(
                    Constant(true),
                    Constant(true)
                )
            ),
            @"while (true) {
    true;
    true;
}",
            @"Do
    True
    True
Loop", 
            @"Loop(
    Block(
        Constant(true),
        Constant(true)
    )
)"
            );

    }
}
