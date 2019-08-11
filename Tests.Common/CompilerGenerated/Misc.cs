using Xunit;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class CompilerGeneratedBase {
        [Fact]
        [Trait("Category", Conditionals)]
        public void Conditional() => PreRunTest();

        [Fact]
        public void TypeCheck() => PreRunTest();

        [Fact]
        [Trait("Category", Invocation)]
        public void InvocationNoArguments() => PreRunTest();

        [Fact]
        [Trait("Category", Invocation)]
        public void InvocationOneArgument() => PreRunTest();
    }
}
