using Xunit;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        [Trait("Category", RuntimeVars)]
        public void ConstructRuntimeVariables() => PreRunTest();

        [Fact]
        [Trait("Category", RuntimeVars)]
        public void RuntimeVariablesWithinBlock() => PreRunTest();
    }
}
