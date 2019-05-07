using Xunit;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        [Trait("Category", Lambdas)]
        public void NoParametersVoidReturn() => RunTest(
            Lambda(Call(writeline0)), 
            "() => Console.WriteLine()", 
            "Sub() Console.WriteLine"
        );

        [Fact]
        [Trait("Category", Lambdas)]
        public void OneParameterVoidReturn() => RunTest(
            Lambda(Call(writeline1, s), s), 
            "(string s) => Console.WriteLine(s)", 
            "Sub(s As String) Console.WriteLine(s)"
        );

        [Fact]
        [Trait("Category", Lambdas)]
        public void TwoParametersVoidReturn() => RunTest(
            Lambda(Call(writeline1, Add(s1, s2, concat)), s1, s2),
            "(string s1, string s2) => Console.WriteLine(s1 + s2)",
            "Sub(s1 As String, s2 As String) Console.WriteLine(s1 + s2)"
        );

        [Fact]
        [Trait("Category", Lambdas)]
        public void NoParametersNonVoidReturn() => RunTest(
            Lambda(Constant("abcd")),
            "() => \"abcd\"",
            "Function() \"abcd\""
        );

        [Fact]
        [Trait("Category", Lambdas)]
        public void OneParameterNonVoidReturn() => RunTest(
            Lambda(s, s),
            "(string s) => s",
            "Function(s As String) s"
        );

        [Fact]
        [Trait("Category", Lambdas)]
        public void TwoParametersNonVoidReturn() => RunTest(
            Lambda(Add(s1, s2, concat), s1, s2),
            "(string s1, string s2) => s1 + s2",
            "Function(s1 As String, s2 As String) s1 + s2"
        );
    }
}
