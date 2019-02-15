using Xunit;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Runners;

namespace ExpressionToString.Tests {
    [Trait("Source", CSharpCompiler)]
    public class Unsorted {
        [Fact]
        public void Conditional() => BuildAssert(
            (int i) => i > 10 ? i : i + 10,
            "(int i) => i > 10 ? i : i + 10",
            "Function(i As Integer) If(i > 10, i, i + 10)"
        );

        [Fact]
        public void TypeCheck() => BuildAssert(
#pragma warning disable CS0183 // 'is' expression's given expression is always of the provided type
            () => "" is string,
#pragma warning restore CS0183 // 'is' expression's given expression is always of the provided type
            "() => \"\" is string",
            "Function() TypeOf \"\" Is String"
        );
    }
}
