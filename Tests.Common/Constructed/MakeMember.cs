using Xunit;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        [Trait("Category",Member)]
        public void InstanceMember() => PreRunTest();

        [Fact]
        [Trait("Category", Member)]
        public void StaticMember() => PreRunTest();
    }
}
