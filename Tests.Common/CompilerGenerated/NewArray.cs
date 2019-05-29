using Xunit;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class CompilerGeneratedBase {
        [Fact]
        [Trait("Category", NewArray)]
        public void SingleDimensionInit() => RunTest(
            () => new string[] { "" },
            "() => new[] { \"\" }",
            "Function() { \"\" }"
        );

        [Fact]
        [Trait("Category", NewArray)]
        public void SingleDimensionInitExplicitType() => RunTest(
            () => new object[] { "" },
            "() => new object[] { \"\" }",
            "Function() New Object() { \"\" }"
        );

        [Fact]
        [Trait("Category", NewArray)]
        public void SingleDimensionWithBounds() => RunTest(
            () => new string[5],
            "() => new string[5]",
            "Function() New String(4) {}"
        );

        [Fact]
        [Trait("Category", NewArray)]
        public void MultidimensionWithBounds() => RunTest(
            () => new string[2, 3],
            "() => new string[2, 3]",
            "Function() New String(1, 2) {}"
        );

        [Fact]
        [Trait("Category", NewArray)]
        public void JaggedWithElementsImplicitType() => RunTest(
            () => new string[][] {
                new [] {"ab","cd" },
                new [] {"ef","gh"}
            },
            "() => new string[][] { new[] { \"ab\", \"cd\" }, new[] { \"ef\", \"gh\" } }",
            "Function() { ({ \"ab\", \"cd\" }), ({ \"ef\", \"gh\" }) }"
        );

        [Fact]
        [Trait("Category", NewArray)]
        public void JaggedWithElementsExplicitType() => RunTest(
            () => new object[][] {
                new [] {"ab","cd" },
                new [] {"ef","gh"}
            },
            "() => new object[][] { new[] { \"ab\", \"cd\" }, new[] { \"ef\", \"gh\" } }",
            "Function() New Object()() { ({ \"ab\", \"cd\" }), ({ \"ef\", \"gh\" }) }"
        );

        [Fact]
        [Trait("Category", NewArray)]
        public void JaggedWithBounds() => RunTest(
            () => new string[5][],
            "() => new string[5][]",
            "Function() New String(4)() {}"
        );

        [Fact]
        [Trait("Category", NewArray)]
        public void ArrayOfMultidimensionalArray() => RunTest(
            () => new string[5][,],
            "() => new string[5][,]",
            "Function() New String(4)(,) {}"
        );

        [Fact]
        [Trait("Category", NewArray)]
        public void MultidimensionalArrayOfArray() => RunTest(
            () => new string[3, 2][],
            "() => new string[3, 2][]",
            "Function() New String(2, 1)() {}"
        );
    }
}
