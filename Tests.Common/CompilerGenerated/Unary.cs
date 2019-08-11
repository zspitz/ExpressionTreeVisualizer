using Xunit;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class CompilerGeneratedBase {
        [Fact]
        [Trait("Category", Unary)]
        public void ArrayLength() => PreRunTest();

        [Fact]
        [Trait("Category", Unary)]
        public void Convert() => PreRunTest();

        [Fact]
        [Trait("Category", Unary)]
        public void Negate() => PreRunTest();

        [Fact]
        [Trait("Category", Unary)]
        public void BitwiseNot() => PreRunTest();

        [Fact]
        [Trait("Category", Unary)]
        public void LogicalNot() => PreRunTest();

        [Fact]
        [Trait("Category", Unary)]
        public void TypeAs() => PreRunTest();
    }
}
