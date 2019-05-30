using Xunit;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        [Trait("Category",NewArray)]
        public void SingleDimensionInit() => RunTest(
            NewArrayInit(typeof(string), Constant("")),
            "new[] { \"\" }",
            "{ \"\" }",
            @"NewArrayInit(
    typeof(string),
    Constant("""")
)"
        );

        [Fact]
        [Trait("Category", NewArray)]
        public void SingleDimensionInitExplicitType() => RunTest(
            NewArrayInit(typeof(object), Constant("")),
            "new object[] { \"\" }",
            "New Object() { \"\" }", 
            @"NewArrayInit(
    typeof(object),
    Constant("""")
)"
        );

        [Fact]
        [Trait("Category", NewArray)]
        public void SingleDimensionWithBounds() => RunTest(
            NewArrayBounds(typeof(string),Constant(5)),
            "new string[5]",
            "New String(4) {}", 
            @"NewArrayBounds(
    typeof(string),
    Constant(5)
)"
        );

        [Fact]
        [Trait("Category", NewArray)]
        public void MultidimensionWithBounds() => RunTest(
            NewArrayBounds(typeof(string), Constant(2), Constant(3)),
            "new string[2, 3]",
            "New String(1, 2) {}", 
            @"NewArrayBounds(
    typeof(string),
    Constant(2),
    Constant(3)
)"
        );

        [Fact]
        [Trait("Category", NewArray)]
        public void JaggedWithElementsImplicitType() => RunTest(
            NewArrayInit(typeof(string[]), 
                NewArrayInit(typeof(string), Constant("ab"), Constant("cd")),
                NewArrayInit(typeof(string), Constant("ef"), Constant("gh"))
            ),
            "new string[][] { new[] { \"ab\", \"cd\" }, new[] { \"ef\", \"gh\" } }",
            "{ ({ \"ab\", \"cd\" }), ({ \"ef\", \"gh\" }) }", 
            @"NewArrayInit(
    typeof(string[]),
    NewArrayInit(
        typeof(string),
        Constant(""ab""),
        Constant(""cd"")
    ),
    NewArrayInit(
        typeof(string),
        Constant(""ef""),
        Constant(""gh"")
    )
)"
        );

        [Fact]
        [Trait("Category", NewArray)]
        public void JaggedWithElementsExplicitType() => RunTest(
            NewArrayInit(typeof(object[]),
                NewArrayInit(typeof(string), Constant("ab"), Constant("cd")),
                NewArrayInit(typeof(string), Constant("ef"), Constant("gh"))
            ),
            "new object[][] { new[] { \"ab\", \"cd\" }, new[] { \"ef\", \"gh\" } }",
            "New Object()() { ({ \"ab\", \"cd\" }), ({ \"ef\", \"gh\" }) }",
            @"NewArrayInit(
    typeof(object[]),
    NewArrayInit(
        typeof(string),
        Constant(""ab""),
        Constant(""cd"")
    ),
    NewArrayInit(
        typeof(string),
        Constant(""ef""),
        Constant(""gh"")
    )
)"
        );

        [Fact]
        [Trait("Category", NewArray)]
        public void JaggedWithBounds() => RunTest(
            NewArrayBounds(typeof(string[]), Constant(5)),
            "new string[5][]",
            "New String(4)() {}", 
            @"NewArrayBounds(
    typeof(string[]),
    Constant(5)
)"
        );

        [Fact]
        [Trait("Category", NewArray)]
        public void ArrayOfMultidimensionalArray() => RunTest(
            NewArrayBounds(typeof(string[,]), Constant(5)),
            "new string[5][,]",
            "New String(4)(,) {}", 
            @"NewArrayBounds(
    typeof(string[,]),
    Constant(5)
)"
        );

        [Fact]
        [Trait("Category", NewArray)]
        public void MultidimensionalArrayOfArray() => RunTest(
            NewArrayBounds(typeof(string[]), Constant(3), Constant(2)),
            "new string[3, 2][]",
            "New String(2, 1)() {}", 
            @"NewArrayBounds(
    typeof(string[]),
    Constant(3),
    Constant(2)
)"
        );
    }
}
