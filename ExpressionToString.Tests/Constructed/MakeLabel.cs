using Xunit;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Runners;
using static System.Linq.Expressions.Expression;

namespace ExpressionToString.Tests.Constructed {
    [Trait("Source", FactoryMethods)]
    public class MakeLabel {
        [Fact]
        public void ConstructLabel() => BuildAssert(
            // we're using variables here to force explicit blocks, which have indentation
            // in order to verify that the label is written without indentation
            Block(
                new[] { i },
                Block(
                    new[] { j },
                    Constant(true),
                    Label(Label("target")),
                    Constant(true)
                )
            ),
            @"{
    int i;
    {
        int j;
        true;
target:
        true;
    }
}",
            @"Block
    Dim i As Integer
    Block
        Dim j As Integer
        True
target:
        True
    End Block
End Block"
        );

        [Fact]
        public void ConstructLabel1() => BuildAssert(
            Block(
                new[] { i },
                Block(
                    new[] { j },
                    Label(Label("target")),
                    Constant(true)
                )
            ),
            @"{
    int i;
    {
        int j;
target:
        true;
    }
}",
            @"Block
    Dim i As Integer
    Block
        Dim j As Integer
target:
        True
    End Block
End Block"
        );

        [Fact]
        public void ConstructLabelTarget() => BuildAssert(
            Label("target"),
            "target",
            "target"
        );

        [Fact]
        public void ConstructEmptyLabelTarget() => BuildAssert(
            Label(""),
            "",
            ""
        );
    }
}
