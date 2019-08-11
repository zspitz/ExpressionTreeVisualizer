using Xunit;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        [Trait("Category", Try)]
        public void ConstructSimpleCatch() => PreRunTest();

        [Fact]
        [Trait("Category", Try)]
        public void ConstructCatchSingleStatement() => PreRunTest();

        [Fact]
        [Trait("Category", Try)]
        public void ConstructCatchMultiStatement() => PreRunTest();

        [Fact]
        [Trait("Category", Try)]
        public void ConstructCatchSingleStatementWithType() => PreRunTest();

        [Fact]
        [Trait("Category", Try)]
        public void ConstructCatchMultiStatementWithType() => PreRunTest();

        [Fact]
        [Trait("Category", Try)]
        public void ConstructCatchSingleStatementWithFilter() => PreRunTest();

        [Fact]
        [Trait("Category", Try)]
        public void ConstructCatchMultiStatementWithFilter() => PreRunTest();

        [Fact]
        [Trait("Category", Try)]
        public void ConstructCatchWithMultiStatementFilter() => PreRunTest();

        [Fact]
        [Trait("Category", Try)]
        public void ConstructTryCatch() => PreRunTest();

        [Fact]
        [Trait("Category", Try)]
        public void ConstructTryCatchFinally() => PreRunTest();

        [Fact]
        [Trait("Category", Try)]
        public void ConstructTryFault() => PreRunTest();

        [Fact]
        [Trait("Category", Try)]
        public void ConstructTryFinally() => PreRunTest();
    }
}
