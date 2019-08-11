using Xunit;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class CompilerGeneratedBase {
        [Fact]
        [Trait("Category", Member)]
        public void InstanceMember() => PreRunTest();

        [Fact]
        [Trait("Category", Member)]
        public void ClosedVariable() => PreRunTest();

        [Fact]
        [Trait("Category", Member)]
        public void StaticMember() => PreRunTest();

        [Fact(Skip ="Test for nested closure scopes")]
        [Trait("Category", Member)]
        public void NestedClosedVariable() {
            Assert.False(true);
        }
    }
}
