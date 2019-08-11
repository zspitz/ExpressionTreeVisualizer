using Xunit;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class CompilerGeneratedBase {
        [Fact]
        [Trait("Category", Indexer)]
        public void ArraySingleIndex() => PreRunTest();

        [Fact]
        [Trait("Category", Indexer)]
        public void ArrayMultipleIndex() => PreRunTest();

        [Fact]
        [Trait("Category", Indexer)]
        public void TypeIndexer() => PreRunTest();
    }
}
