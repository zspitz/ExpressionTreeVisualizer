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
            @"true;
true;",
            @"True
True",
            @"Block(new [] {
    Constant(true),
    Constant(true)
})"
        );

        [Fact]
        [Trait("Category", Blocks)]
        public void BlockSingleVariable() => RunTest(
            Block(
                new[] { i },
                Constant(true),
                Constant(true)
            ),
            @"{
    int i;
    true;
    true;
}",
            @"Block
    Dim i As Integer
    True
    True
End Block",
            @"Block(new [] { i }, new [] {
    Constant(true),
    Constant(true)
})"
        );

        [Fact]
        [Trait("Category", Blocks)]
        public void BlockMultipleVariable() => RunTest(
            Block(
                new[] { i,s1 },
                Constant(true),
                Constant(true)
            ),
            @"{
    int i;
    string s1;
    true;
    true;
}",
    @"Block
    Dim i As Integer
    Dim s1 As String
    True
    True
End Block",
    @"Block(new [] { i, s1 }, new [] {
    Constant(true),
    Constant(true)
})"
        );
    }
}
