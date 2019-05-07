using System.Linq.Expressions;
using Xunit;
using static System.Linq.Expressions.Expression;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        private LabelTarget labelTarget = Label("target");

        [Fact]
        public void MakeBreak() => RunTest(
            Break(labelTarget),
            "break target",
            "Exit target"
        );

        [Fact]
        public void MakeBreakWithValue() => RunTest(
            Break(labelTarget, Constant(5)),
            "break target 5",
            "Exit target 5"
        );

        [Fact]
        public void MakeContinue() => RunTest(
            Continue(labelTarget),
            "continue target",
            "Continue target"
        );

        [Fact]
        public void MakeGotoWithoutValue() => RunTest(
            Goto(labelTarget),
            "goto target",
            "Goto target"
        );

        [Fact]
        public void MakeGotoWithValue() => RunTest(
            Goto(labelTarget, Constant(5)),
            "goto target 5",
            "Goto target 5"
        );

        [Fact]
        public void MakeReturn() => RunTest(
            Return(labelTarget),
            "return target",
            "Return target"
        );

        [Fact]
        public void MakeReturnWithValue() => RunTest(
            Return(labelTarget, Constant(5)),
            "return target 5",
            "Return target 5"
        );
    }
}
