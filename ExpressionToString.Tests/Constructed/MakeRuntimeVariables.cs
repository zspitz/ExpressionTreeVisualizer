using Xunit;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Runners;
using static System.Linq.Expressions.Expression;

namespace ExpressionToString.Tests.Constructed {
    [Trait("Source", FactoryMethods)]
    public class MakeRuntimeVariables
    {
        [Fact]
        public void ConstructRuntimeVariables() => BuildAssert(
            RuntimeVariables(x, s1),
            "// variables -- double x, string s1",
            "' Variables -- x As Double, s1 As String"
        );

        [Fact]
        public void RuntimeVariablesWithinBlock() => BuildAssert(
            Block(
                new [] {s2}, //forces an explicit block
                Constant(true),
                RuntimeVariables(x, s1)
            ),
            @"{
    string s2;
    true;
    // variables -- double x, string s1
}",
            @"Block
    Dim s2 As String
    True
    ' Variables -- x As Double, s1 As String
End Block"
        );
    }
}
