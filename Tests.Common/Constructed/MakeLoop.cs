using Xunit;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        [Trait("Category", Loops)]
        public void EmptyLoop() => PreRunTest();

        [Fact]
        [Trait("Category", Loops)]
        public void EmptyLoop1() => PreRunTest();
    }
}
