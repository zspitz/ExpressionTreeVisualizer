using Xunit;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        [Trait("Category", NewArray)]
        public void SingleDimensionInit() => PreRunTest();

        [Fact]
        [Trait("Category", NewArray)]
        public void SingleDimensionInitExplicitType() => PreRunTest();

        [Fact]
        [Trait("Category", NewArray)]
        public void SingleDimensionWithBounds() => PreRunTest();

        [Fact]
        [Trait("Category", NewArray)]
        public void MultidimensionWithBounds() => PreRunTest();

        [Fact]
        [Trait("Category", NewArray)]
        public void JaggedWithElementsImplicitType() => PreRunTest();

        [Fact]
        [Trait("Category", NewArray)]
        public void JaggedWithElementsExplicitType() => PreRunTest();

        [Fact]
        [Trait("Category", NewArray)]
        public void JaggedWithBounds() => PreRunTest();

        [Fact]
        [Trait("Category", NewArray)]
        public void ArrayOfMultidimensionalArray() => PreRunTest();

        [Fact]
        [Trait("Category", NewArray)]
        public void MultidimensionalArrayOfArray() => PreRunTest();
    }
}
