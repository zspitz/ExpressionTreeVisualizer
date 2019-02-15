using Xunit;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Runners;

namespace ExpressionToString.Tests {
    [Trait("Source", CSharpCompiler)]
    public class Default {
        [Fact]
        public void DefaultRefType() => BuildAssert(
            () => default(string),
            "() => null",
            "Function() Nothing"
        );

        [Fact]
        public void DefaultValueType() => BuildAssert(
            () => default(int),
            "() => 0",
            "Function() 0"
        );
    }
}
