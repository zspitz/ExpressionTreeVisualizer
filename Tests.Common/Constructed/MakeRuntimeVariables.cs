using Xunit;
using static ExpressionToString.Tests.Globals;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        [Trait("Category", RuntimeVars)]
        public void ConstructRuntimeVariables() => RunTest(
            RuntimeVariables(x, s1),
            "// variables -- double x, string s1",
            "' Variables -- x As Double, s1 As String", 
            "RuntimeVariables(x, s1)"
        );

        [Fact]
        [Trait("Category", RuntimeVars)]
        public void RuntimeVariablesWithinBlock() => RunTest(
            Block(
                new[] { s2 }, //forces an explicit block
                Constant(true),
                RuntimeVariables(x, s1)
            ),
            @"(
    string s2,
    true
    // variables -- double x, string s1
)",
            @"Block
    Dim s2 As String
    True
    ' Variables -- x As Double, s1 As String
End Block", 
            @"Block(new[] { s2 },
    Constant(true),
    RuntimeVariables(x, s1)
)"
        );
    }
}
