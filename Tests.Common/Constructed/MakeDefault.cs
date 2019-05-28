using Xunit;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        [Trait("Category", Defaults)]
        public void MakeDefaultRefType() => RunTest(
            Default(typeof(string)),
            "default(string)",
            "CType(Nothing, String)", 
            @"Default(
    typeof(string)
)"
        );

        [Fact]
        [Trait("Category", Defaults)]
        public void MakeDefaultValueType() => RunTest(
            Default(typeof(int)),
            "default(int)",
            "CType(Nothing, Integer)", 
            @"Default(
    typeof(int)
)"
        );
    }
}
