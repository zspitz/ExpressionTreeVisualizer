using Xunit;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        [Trait("Category", Defaults)]
        public void MakeDefaultRefType() => PreRunTest();

        [Fact]
        [Trait("Category", Defaults)]
        public void MakeDefaultValueType() => PreRunTest();
    }
}
