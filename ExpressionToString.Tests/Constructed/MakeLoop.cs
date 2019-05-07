using Xunit;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Runners;
using static System.Linq.Expressions.Expression;

namespace ExpressionToString.Tests.Constructed {
    [Trait("Source", FactoryMethods)]
    public class MakeLoop {
        [Fact]
        public void EmptyLoop() => RunTest(
            Loop(Constant(true)),
            @"while (true) {
    true;
}",
            @"Do
    True
Loop"
            );

        [Fact]
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
Loop"
            );

    }
}
