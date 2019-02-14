using Xunit;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Runners;
using static System.Linq.Expressions.Expression;

namespace ExpressionToString.Tests.Constructed {
    [Trait("Source", FactoryMethods)]
    public class MakeNewArray {
        [Fact]
        public void SingleDimensionInit() => BuildAssert(
            NewArrayInit(typeof(string), Constant("")),
            "new [] { \"\" }",
            "{ \"\" }"
        );

        [Fact]
        public void SingleDimensionInitExplicitType() => BuildAssert(
            NewArrayInit(typeof(object), Constant("")),
            "new object[] { \"\" }",
            "New Object() { \"\" }"
        );

        [Fact]
        public void SingleDimensionWithBounds() => BuildAssert(
            NewArrayBounds(typeof(string),Constant(5)),
            "new string[5]",
            "New String(4) {}"
        );

        [Fact]
        public void MultidimensionWithBounds() => BuildAssert(
            NewArrayBounds(typeof(string), Constant(2), Constant(3)),
            "new string[2, 3]",
            "New String(1, 2) {}"
        );

        [Fact]
        public void JaggedWithElementsImplicitType() => BuildAssert(
            NewArrayInit(typeof(string[]), 
                NewArrayInit(typeof(string), Constant("ab"), Constant("cd")),
                NewArrayInit(typeof(string), Constant("ef"), Constant("gh"))
            ),
            "new string[][] { new [] { \"ab\", \"cd\" }, new [] { \"ef\", \"gh\" } }",
            "{ ({ \"ab\", \"cd\" }), ({ \"ef\", \"gh\" }) }"
        );

        [Fact]
        public void JaggedWithElementsExplicitType() => BuildAssert(
            NewArrayInit(typeof(object[]),
                NewArrayInit(typeof(string), Constant("ab"), Constant("cd")),
                NewArrayInit(typeof(string), Constant("ef"), Constant("gh"))
            ),
            "new object[][] { new [] { \"ab\", \"cd\" }, new [] { \"ef\", \"gh\" } }",
            "New Object()() { ({ \"ab\", \"cd\" }), ({ \"ef\", \"gh\" }) }"
        );

        [Fact]
        public void JaggedWithBounds() => BuildAssert(
            NewArrayBounds(typeof(string[]), Constant(5)),
            "new string[5][]",
            "New String(4)() {}"
        );

        [Fact]
        public void ArrayOfMultidimensionalArray() => BuildAssert(
            NewArrayBounds(typeof(string[,]), Constant(5)),
            "new string[5][,]",
            "New String(4)(,) {}"
        );

        [Fact]
        public void MultidimensionalArrayOfArray() => BuildAssert(
            NewArrayBounds(typeof(string[]), Constant(3), Constant(2)),
            "new string[3, 2][]",
            "New String(2, 1)() {}"
        );

    }
}
