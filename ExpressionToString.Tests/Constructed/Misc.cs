using System.Collections;
using System.Linq.Expressions;
using Xunit;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Runners;
using static System.Linq.Expressions.Expression;

namespace ExpressionToString.Tests.Constructed {
    [Trait("Source", FactoryMethods)]
    public class Misc {
        [Fact]
        public void MakeConditional() {
            var i = Parameter(typeof(int), "i");
            BuildAssert(
                Condition(
                    GreaterThan(i, Constant(10)),
                    i,
                    Add(i, Constant(10))
                ),
                "i > 10 ? i : i + 10",
                "If(i > 10, i, i + 10)"
            );
        }

        [Fact]
        public void MakeTypeCheck() => BuildAssert(
            TypeIs(
                Constant(""),
                typeof(string)
            ),
            "\"\" is string",
            "TypeOf \"\" Is String"
        );

        [Fact]
        public void MakeTypeEqual() => BuildAssert(
            TypeEqual(
                Constant(""),
                typeof(IEnumerable)
            ),
            "\"\".GetType() == typeof(IEnumerable)",
            "\"\".GetType = GetType(IEnumerable)"
        );

        [Fact]
        public void MakeInvocation() => BuildAssert(
            Invoke(
                Lambda(Constant(5))
            ),
            "(() => 5)()",
            "(Function() 5)()"
        );

        [Fact]
        public void MakeByRefParameter() => BuildAssert(
            Lambda(
                Constant(true),
                Parameter(typeof(string).MakeByRefType(), "s4")
            ),
            "(ref string s4) => true",
            "Function(ByRef s4 As String) True"
        );

        [Fact]
        public void MakeQuoted() => BuildAssert(
            Block(
                new[] { x },
                Quote(
                    Lambda(writeLineTrue)
                )
            ),
            @"{
    double x;
    // --- Quoted - begin
        () => Console.WriteLine(true)
    // --- Quoted - end
}",
            @"Block
    Dim x As Double
    ' --- Quoted - begin
        Sub() Console.WriteLine(True)
    ' --- Quoted - end
End Block"
        );

        [Fact]
        public void MakeQuoted1() => BuildAssert(
            Lambda(
                Quote(
                    Lambda(writeLineTrue)
                )
            ),
            @"() =>
// --- Quoted - begin
    () => Console.WriteLine(true)
// --- Quoted - end",
            @"Function()
' --- Quoted - begin
    Sub() Console.WriteLine(True)
' --- Quoted - end"
        );

        SymbolDocumentInfo document = SymbolDocument("source.txt");

        [Fact]
        public void MakeDebugInfo() => BuildAssert(
            DebugInfo(document,1,2,3,4),
            "// Debug to source.txt -- L1C2 : L3C4",
            "' Debug to source.txt -- L1C2 : L3C4"
        );

        [Fact]
        public void MakeClearDebugInfo() => BuildAssert(
            ClearDebugInfo(document),
            "// Clear debug info from source.txt",
            "' Clear debug info from source.txt"
        );
    }
}
