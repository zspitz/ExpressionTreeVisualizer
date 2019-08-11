using Xunit;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class CompilerGeneratedBase {
        [Fact]
        [Trait("Category", Literal)]
        public void True() => PreRunTest();

        [Fact]
        [Trait("Category", Literal)]
        public void False() => PreRunTest();

        [Fact]
        [Trait("Category", Literal)]
        public void Nothing() => PreRunTest();

        [Fact]
        [Trait("Category", Literal)]
        public void Integer() => PreRunTest();

        [Fact]
        [Trait("Category", Literal)]
        public void NonInteger() => PreRunTest();

        [Fact]
        [Trait("Category", Literal)]
        public void String() => PreRunTest();

        [Fact]
        [Trait("Category", Literal)]
        public void InterpolatedString() => PreRunTest();

        [Fact]
        [Trait("Category", Literal)]
        public void Type() => PreRunTest();
    }
}
