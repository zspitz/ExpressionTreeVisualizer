using System;
using Xunit;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class CompilerGeneratedBase {
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
    }
}
