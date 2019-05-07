using Xunit;

namespace ExpressionToString.Tests {
    public partial class CompilerGeneratedBase {
        [Fact]
        public void SingleDimensionInit() => RunTest(
            () => new string[] { "" },
            "() => new [] { \"\" }",
            "Function() { \"\" }"
        );

        [Fact]
        public void SingleDimensionInitExplicitType() => RunTest(
            () => new object[] { "" },
            "() => new object[] { \"\" }",
            "Function() New Object() { \"\" }"
        );

        [Fact]
        public void SingleDimensionWithBounds() => RunTest(
            () => new string[5],
            "() => new string[5]",
            "Function() New String(4) {}"
        );

        [Fact]
        public void MultidimensionWithBounds() => RunTest(
            () => new string[2, 3],
            "() => new string[2, 3]",
            "Function() New String(1, 2) {}"
        );

        [Fact]
        public void JaggedWithElementsImplicitType() => RunTest(
            () => new string[][] {
                new [] {"ab","cd" },
                new [] {"ef","gh"}
            },
            "() => new string[][] { new [] { \"ab\", \"cd\" }, new [] { \"ef\", \"gh\" } }",
            "Function() { ({ \"ab\", \"cd\" }), ({ \"ef\", \"gh\" }) }"
        );

        [Fact]
        public void JaggedWithElementsExplicitType() => RunTest(
            () => new object[][] {
                new [] {"ab","cd" },
                new [] {"ef","gh"}
            },
            "() => new object[][] { new [] { \"ab\", \"cd\" }, new [] { \"ef\", \"gh\" } }",
            "Function() New Object()() { ({ \"ab\", \"cd\" }), ({ \"ef\", \"gh\" }) }"
        );

        [Fact]
        public void JaggedWithBounds() => RunTest(
            () => new string[5][],
            "() => new string[5][]",
            "Function() New String(4)() {}"
        );

        [Fact]
        public void ArrayOfMultidimensionalArray() => RunTest(
            () => new string[5][,],
            "() => new string[5][,]",
            "Function() New String(4)(,) {}"
        );

        [Fact]
        public void MultidimensionalArrayOfArray() => RunTest(
            () => new string[3, 2][],
            "() => new string[3, 2][]",
            "Function() New String(2, 1)() {}"
        );
    }
}
