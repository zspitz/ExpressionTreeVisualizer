using Xunit;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {

        // note that the NodeType of the expression constructed Conditional factory method can be either typeof(void) or some other type
        // the NodeTypeof IfThen and IfThenElse is always typeof(void)

        [Fact]
        [Trait("Category", Conditionals)]
        public void VoidConditionalWithElse() => PreRunTest();

        [Fact]
        [Trait("Category", Conditionals)]
        public void VoidConditional1WithElse() => PreRunTest();

        [Fact]
        [Trait("Category", Conditionals)]
        public void VoidConditionalWithoutElse() => PreRunTest();

        [Fact]
        [Trait("Category", Conditionals)]
        public void VoidConditional1WithoutElse() => PreRunTest();

        [Fact]
        [Trait("Category", Conditionals)]
        public void NonVoidConditionalWithElse() => PreRunTest();

        [Fact]
        [Trait("Category", Conditionals)]
        public void NonVoidConditionalWithoutElse() => PreRunTest();

        [Fact]
        [Trait("Category", Conditionals)]
        public void MultilineTestPart() => PreRunTest();

        [Fact]
        [Trait("Category", Conditionals)]
        public void MultilineTestPart1() => PreRunTest();

        [Fact]
        [Trait("Category", Conditionals)]
        public void MultilineIfTrue() => PreRunTest();

        [Fact]
        [Trait("Category", Conditionals)]
        public void MultilineIfFalse() => PreRunTest();

        [Fact]
        [Trait("Category", Conditionals)]
        public void NestedIfThen() => PreRunTest();

        [Fact]
        [Trait("Category", Conditionals)]
        public void NestedElse() => PreRunTest();

        [Fact]
        [Trait("Category", Conditionals)]
        public void MakeConditional() => PreRunTest();
    }
}
