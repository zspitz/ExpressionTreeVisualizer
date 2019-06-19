using Xunit;
using static System.Linq.Expressions.Expression;
using System.Linq.Expressions;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        [Trait("Category", Blocks)]
        public void BlockNoVariables() => RunTest(
            Block(
                Constant(true),
                Constant(true)
            ),
            @"(
    true,
    true
)",
            @"Block
    True
    True
End Block",
            @"Block(
    Constant(true),
    Constant(true)
)"
        );

        [Fact]
        [Trait("Category", Blocks)]
        public void BlockSingleVariable() => RunTest(
            Block(
                new[] { i },
                Constant(true),
                Constant(true)
            ),
            @"(
    int i,
    true,
    true
)",
            @"Block
    Dim i As Integer
    True
    True
End Block",
            @"Block(new[] { i },
    Constant(true),
    Constant(true)
)"
        );

        [Fact]
        [Trait("Category", Blocks)]
        public void BlockMultipleVariable() => RunTest(
            Block(
                new[] { i, s1 },
                Constant(true),
                Constant(true)
            ),
            @"(
    int i,
    string s1,
    true,
    true
)",
    @"Block
    Dim i As Integer
    Dim s1 As String
    True
    True
End Block",
    @"Block(new[] { i, s1 },
    Constant(true),
    Constant(true)
)"
        );

        private static readonly BlockExpression nestedBlock = Block(
            Constant(true),
            Block(
                Constant(true),
                Constant(true)
            ),
            Constant(true)
        );
        private static readonly BlockExpression nestedBlockWithVariable = Block(
            Constant(true),
            Block(
                new[] { s1 },
                Constant(true),
                Constant(true)
            ),
            Constant(true)
        );

        [Fact]
        [Trait("Category", Blocks)]
        public void NestedInlineBlock() => RunTest(
            nestedBlock,
            @"(
    true,
    true,
    true,
    true
)",
            @"Block
    True
    True
    True
    True
End Block",
            @"Block(
    Constant(true),
    Block(
        Constant(true),
        Constant(true)
    ),
    Constant(true)
)"
        );

        [Fact]
        [Trait("Category", Blocks)]
        public void NestedBlockInTest() => RunTest(
            IfThen(
                nestedBlock,
                Constant(true)
            ),
            @"if (
    true,
    true,
    true,
    true
) {
    true;
}",
            @"If
    True
    True
    True
    True
Then True",
            @"IfThen(
    Block(
        Constant(true),
        Block(
            Constant(true),
            Constant(true)
        ),
        Constant(true)
    ),
    Constant(true)
)"
        );

        [Fact]
        [Trait("Category", Blocks)]
        public void NestedBlockInBlockSyntax() => RunTest(
            IfThen(
                Constant(true),
                nestedBlock
            ),
            @"if (true) {
    true;
    true;
    true;
    true;
}",
            @"If True Then
    True
    True
    True
    True
End If",
            @"IfThen(
    Constant(true),
    Block(
        Constant(true),
        Block(
            Constant(true),
            Constant(true)
        ),
        Constant(true)
    )
)"
        );

        [Fact]
        [Trait("Category", Blocks)]
        public void NestedInlineBlockWithVariable() => RunTest(
            nestedBlockWithVariable,
            @"(
    true,
    (
        string s1,
        true,
        true
    ),
    true
)",
            @"Block
    True
    Block
        Dim s1 As String
        True
        True
    End Block
    True
End Block",
            @"Block(
    Constant(true),
    Block(new[] { s1 },
        Constant(true),
        Constant(true)
    ),
    Constant(true)
)"
        );

        [Fact]
        [Trait("Category", Blocks)]
        public void NestedBlockInTestWithVariables() => RunTest(
            IfThen(
                nestedBlockWithVariable,
                Constant(true)
            ),
            @"if (
    true,
    (
        string s1,
        true,
        true
    ),
    true
) {
    true;
}",
            @"If
    True
    Block
        Dim s1 As String
        True
        True
    End Block
    True
Then True",
            @"IfThen(
    Block(
        Constant(true),
        Block(new[] { s1 },
            Constant(true),
            Constant(true)
        ),
        Constant(true)
    ),
    Constant(true)
)"
        );

        [Fact]
        [Trait("Category", Blocks)]
        public void NestedBlockInBlockSyntaxWithVariable() => RunTest(
            IfThen(
                Constant(true),
                nestedBlockWithVariable
            ),
            @"if (true) {
    true;
    {
        string s1;
        true;
        true;
    }
    true;
}",
            @"If True Then
    True
    Block
        Dim s1 As String
        True
        True
    End Block
    True
End If",
            @"IfThen(
    Constant(true),
    Block(
        Constant(true),
        Block(new[] { s1 },
            Constant(true),
            Constant(true)
        ),
        Constant(true)
    )
)"
        );

    }
}
