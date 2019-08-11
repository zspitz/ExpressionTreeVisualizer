using Xunit;
using static System.Linq.Expressions.Expression;
using System.Linq.Expressions;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        [Trait("Category", Blocks)]
        public void BlockNoVariables() => PreRunTest();

        [Fact]
        [Trait("Category", Blocks)]
        public void BlockSingleVariable() => PreRunTest();

        [Fact]
        [Trait("Category", Blocks)]
        public void BlockMultipleVariable() => PreRunTest();

        [Fact]
        [Trait("Category", Blocks)]
        public void NestedInlineBlock() => PreRunTest();

        [Fact]
        [Trait("Category", Blocks)]
        public void NestedBlockInTest() => PreRunTest();

        [Fact]
        [Trait("Category", Blocks)]
        public void NestedBlockInBlockSyntax() => PreRunTest();

        [Fact]
        [Trait("Category", Blocks)]
        public void NestedInlineBlockWithVariable() => PreRunTest();

        [Fact]
        [Trait("Category", Blocks)]
        public void NestedBlockInTestWithVariables() => PreRunTest();

        [Fact]
        [Trait("Category", Blocks)]
        public void NestedBlockInBlockSyntaxWithVariable() => PreRunTest();
    }
}
