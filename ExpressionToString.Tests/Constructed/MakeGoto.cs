using System.Linq.Expressions;
using Xunit;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Runners;
using static System.Linq.Expressions.Expression;

namespace ExpressionToString.Tests.Constructed {
    [Trait("Source", FactoryMethods)]
    public class MakeGoto {
        private LabelTarget labelTarget = Label("target");

        [Fact]
        public void MakeBreak() => BuildAssert(
            Break(labelTarget),
            "break target",
            "Exit target"
        );

        [Fact]
        public void MakeBreakWithValue() => BuildAssert(
            Break(labelTarget, Constant(5)),
            "break target 5",
            "Exit target 5"
        );

        [Fact]
        public void MakeContinue() => BuildAssert(
            Continue(labelTarget),
            "continue target",
            "Continue target"
        );

        [Fact]
        public void MakeGotoWithoutValue() => BuildAssert(
            Goto(labelTarget),
            "goto target",
            "Goto target"
        );

        [Fact]
        public void MakeGotoWithValue() => BuildAssert(
            Goto(labelTarget, Constant(5)),
            "goto target 5",
            "Goto target 5"
        );

        [Fact]
        public void MakeReturn() => BuildAssert(
            Return(labelTarget),
            "return target",
            "Return target"
        );

        [Fact]
        public void MakeReturnWithValue() => BuildAssert(
            Return(labelTarget, Constant(5)),
            "return target 5",
            "Return target 5"
        );
    }
}
