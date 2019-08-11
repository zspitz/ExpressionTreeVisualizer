using Xunit;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        [Trait("Category", Labels)]
        public void ConstructLabel() => PreRunTest();

        [Fact]
        [Trait("Category", Labels)]
        public void ConstructLabel1() => PreRunTest();

        [Fact]
        public void ConstructLabelTarget() => PreRunTest();

        [Fact]
        public void ConstructEmptyLabelTarget() => PreRunTest();
    }
}
