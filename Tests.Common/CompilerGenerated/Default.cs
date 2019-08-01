using Xunit;
using static ExpressionToString.Tests.Categories;
using ExpressionToString.Tests.Objects;

namespace ExpressionToString.Tests {
    public partial class CompilerGeneratedBase {
        [Fact]
        [Trait("Category", Defaults)]
        public void DefaultRefType() => RunTest(
            CSCompiler.DefaultRefType,
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
            CSCompiler.DefaultValueType,
            "() => 0",
            "Function() 0", 
            @"Lambda(
    Constant(0)
)"
        );
    }
}
