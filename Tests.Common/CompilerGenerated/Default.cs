using Xunit;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class CompilerGeneratedBase {
        [Fact]
        [Trait("Category", Defaults)]
        public void DefaultRefType() => PreRunTest();

        [Fact]
        [Trait("Category", Defaults)]
        public void DefaultValueType() => PreRunTest();
    }
}
