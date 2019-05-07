using Xunit;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Runners;
using static System.Linq.Expressions.Expression;

namespace ExpressionToString.Tests.Constructed {
    [Trait("Source", FactoryMethods)]
    public class MakeNewArray {
        [Fact]
        public void SingleDimensionInit() => RunTest(
            NewArrayInit(typeof(string), Constant("")),
            "new [] { \"\" }",
            "{ \"\" }"
        );

        [Fact]
        public void SingleDimensionInitExplicitType() => RunTest(
            NewArrayInit(typeof(object), Constant("")),
            "new object[] { \"\" }",
            "New Object() { \"\" }"
        );

        [Fact]
        public void SingleDimensionWithBounds() => RunTest(
            NewArrayBounds(typeof(string),Constant(5)),
            "new string[5]",
            "New String(4) {}"
        );

        [Fact]
        public void MultidimensionWithBounds() => RunTest(
            NewArrayBounds(typeof(string), Constant(2), Constant(3)),
            "new string[2, 3]",
            "New String(1, 2) {}"
        );

        [Fact]
        public void JaggedWithElementsImplicitType() => RunTest(
            NewArrayInit(typeof(string[]), 
                NewArrayInit(typeof(string), Constant("ab"), Constant("cd")),
                NewArrayInit(typeof(string), Constant("ef"), Constant("gh"))
            ),
            "new string[][] { new [] { \"ab\", \"cd\" }, new [] { \"ef\", \"gh\" } }",
            "{ ({ \"ab\", \"cd\" }), ({ \"ef\", \"gh\" }) }"
        );

        [Fact]
        public void JaggedWithElementsExplicitType() => RunTest(
            NewArrayInit(typeof(object[]),
                NewArrayInit(typeof(string), Constant("ab"), Constant("cd")),
                NewArrayInit(typeof(string), Constant("ef"), Constant("gh"))
            ),
            "new object[][] { new [] { \"ab\", \"cd\" }, new [] { \"ef\", \"gh\" } }",
            "New Object()() { ({ \"ab\", \"cd\" }), ({ \"ef\", \"gh\" }) }"
        );

        [Fact]
        public void JaggedWithBounds() => RunTest(
            NewArrayBounds(typeof(string[]), Constant(5)),
            "new string[5][]",
            "New String(4)() {}"
        );

        [Fact]
        public void ArrayOfMultidimensionalArray() => RunTest(
            NewArrayBounds(typeof(string[,]), Constant(5)),
            "new string[5][,]",
            "New String(4)(,) {}"
        );

        [Fact]
        public void MultidimensionalArrayOfArray() => RunTest(
            NewArrayBounds(typeof(string[]), Constant(3), Constant(2)),
            "new string[3, 2][]",
            "New String(2, 1)() {}"
        );

    }
}
