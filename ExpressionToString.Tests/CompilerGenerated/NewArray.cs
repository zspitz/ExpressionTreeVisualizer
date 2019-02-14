using Xunit;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Runners;

namespace ExpressionToString.Tests {
    [Trait("Source", CSharpCompiler)]
    public class NewArray {
        [Fact]
        public void SingleDimensionInit() => BuildAssert(
            () => new string[] { "" },
            "() => new [] { \"\" }",
            "Function() { \"\" }"
        );

        [Fact]
        public void SingleDimensionInitExplicitType() => BuildAssert(
            () => new object[] { "" },
            "() => new object[] { \"\" }",
            "Function() New Object() { \"\" }"
        );

        [Fact]
        public void SingleDimensionWithBounds() => BuildAssert(
            () => new string[5],
            "() => new string[5]",
            "Function() New String(4) {}"
        );

        [Fact]
        public void MultidimensionWithBounds() => BuildAssert(
            () => new string[2, 3],
            "() => new string[2, 3]",
            "Function() New String(1, 2) {}"
        );

        [Fact]
        public void JaggedWithElementsImplicitType() => BuildAssert(
            () => new string[][] {
                new [] {"ab","cd" },
                new [] {"ef","gh"}
            },
            "() => new string[][] { new [] { \"ab\", \"cd\" }, new [] { \"ef\", \"gh\" } }",
            "Function() { ({ \"ab\", \"cd\" }), ({ \"ef\", \"gh\" }) }"
        );

        [Fact]
        public void JaggedWithElementsExplicitType() => BuildAssert(
            () => new object[][] {
                new [] {"ab","cd" },
                new [] {"ef","gh"}
            },
            "() => new object[][] { new [] { \"ab\", \"cd\" }, new [] { \"ef\", \"gh\" } }",
            "Function() New Object()() { ({ \"ab\", \"cd\" }), ({ \"ef\", \"gh\" }) }"
        );

        [Fact]
        public void JaggedWithBounds() => BuildAssert(
            () => new string[5][],
            "() => new string[5][]",
            "Function() New String(4)() {}"
        );

        [Fact]
        public void ArrayOfMultidimensionalArray() => BuildAssert(
            () => new string[5][,],
            "() => new string[5][,]",
            "Function() New String(4)(,) {}"
        );

        [Fact]
        public void MultidimensionalArrayOfArray() => BuildAssert(
            () => new string[3, 2][],
            "() => new string[3, 2][]",
            "Function() New String(2, 1)() {}"
        );
    }
}
