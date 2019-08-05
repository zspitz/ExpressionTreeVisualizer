using Xunit;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        [Trait("Category", Lambdas)]
        public void NoParametersVoidReturn() => PreRunTest();

        [Fact]
        [Trait("Category", Lambdas)]
        public void OneParameterVoidReturn() => PreRunTest();

        [Fact]
        [Trait("Category", Lambdas)]
        public void TwoParametersVoidReturn() => PreRunTest();

        [Fact]
        [Trait("Category", Lambdas)]
        public void NoParametersNonVoidReturn() => PreRunTest();

        [Fact]
        [Trait("Category", Lambdas)]
        public void OneParameterNonVoidReturn() => PreRunTest();

        [Fact]
        [Trait("Category", Lambdas)]
        public void TwoParametersNonVoidReturn() => PreRunTest();

        [Fact]
        [Trait("Category", Lambdas)]
        public void NamedLambda() => PreRunTest();

        [Fact]
        [Trait("Category", Lambdas)]
        public void MultilineLambda() => PreRunTest();

        [Fact]
        [Trait("Category", Lambdas)]
        public void NestedLambda() => PreRunTest();

        [Fact]
        [Trait("Category", Lambdas)]
        public void LambdaMultilineBlockNonvoidReturn() => PreRunTest();

        [Fact]
        [Trait("Category", Lambdas)]
        public void LambdaMultilineNestedBlockNonvoidReturn() =>PreRunTest();
    }
}
