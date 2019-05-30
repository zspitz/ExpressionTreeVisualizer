using Xunit;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        [Trait("Category", Labels)]
        public void ConstructLabel() => RunTest(
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
End Block",
            @"Block(new[] { i },
    Block(new[] { j },
        Constant(true),
        Label(
            Label(""target""),
            null
        ),
        Constant(true)
    )
)"
        );

        [Fact]
        [Trait("Category", Labels)]
        public void ConstructLabel1() => RunTest(
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
End Block",
            @"Block(new[] { i },
    Block(new[] { j },
        Label(
            Label(""target""),
            null
        ),
        Constant(true)
    )
)"
        );

        [Fact]
        public void ConstructLabelTarget() => RunTest(
            Label("target"),
            "target",
            "target", 
            @"Label(""target"")"
        );

        [Fact]
        public void ConstructEmptyLabelTarget() => RunTest(
            Label(""),
            "",
            "",
            @"Label("""")"
        );
    }
}
