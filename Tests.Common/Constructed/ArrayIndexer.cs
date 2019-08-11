using Xunit;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        [Trait("Category", Indexer)]
        public void MakeArrayIndex() => PreRunTest();

        [Fact]
        [Trait("Category", Indexer)]
        public void MakeArrayMultipleIndex() => PreRunTest();

        [Fact]
        [Trait("Category", Indexer)]
        public void MakeArrayAccess() => PreRunTest();

        [Fact]
        [Trait("Category", Indexer)]
        public void InstanceIndexer() => PreRunTest();

        [Fact]
        [Trait("Category", Indexer)]
        public void PropertyIndexer() => PreRunTest();
    }
}
