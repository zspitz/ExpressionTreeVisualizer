using Xunit;

namespace ExpressionToString.Tests {
    public partial class CompilerGeneratedBase {
        [Fact]
        public void DefaultRefType() => RunTest(
            () => default(string),
            "() => null",
            "Function() Nothing"
        );

        [Fact]
        public void DefaultValueType() => RunTest(
            () => default(int),
            "() => 0",
            "Function() 0"
        );
    }
}
