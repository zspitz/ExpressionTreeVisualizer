using Xunit;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class CompilerGeneratedBase {
        [Fact]
        [Trait("Category", Defaults)]
        public void DefaultRefType() => RunTest(
            () => default(string),
            "() => null",
            "Function() Nothing", 
            @"Lambda(
    Constant(null,
        typeof(string)
    )
)"
        );

        [Fact]
        [Trait("Category", Defaults)]
        public void DefaultValueType() => RunTest(
            () => default(int),
            "() => 0",
            "Function() 0", 
            @"Lambda(
    Constant(0)
)"
        );
    }
}
