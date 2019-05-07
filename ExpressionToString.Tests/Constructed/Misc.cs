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
            RunTest(
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
        public void MakeTypeCheck() => RunTest(
            TypeIs(
                Constant(""),
                typeof(string)
            ),
            "\"\" is string",
            "TypeOf \"\" Is String"
        );

        [Fact]
        public void MakeTypeEqual() => RunTest(
            TypeEqual(
                Constant(""),
                typeof(IEnumerable)
            ),
            "\"\".GetType() == typeof(IEnumerable)",
            "\"\".GetType = GetType(IEnumerable)"
        );

        [Fact]
        public void MakeInvocation() => RunTest(
            Invoke(
                Lambda(Constant(5))
            ),
            "(() => 5)()",
            "(Function() 5)()"
        );

        [Fact]
        public void MakeByRefParameter() => RunTest(
            Lambda(
                Constant(true),
                Parameter(typeof(string).MakeByRefType(), "s4")
            ),
            "(ref string s4) => true",
            "Function(ByRef s4 As String) True"
        );

        [Fact]
        public void MakeQuoted() => RunTest(
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
        public void MakeQuoted1() => RunTest(
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
        public void MakeDebugInfo() => RunTest(
            DebugInfo(document,1,2,3,4),
            "// Debug to source.txt -- L1C2 : L3C4",
            "' Debug to source.txt -- L1C2 : L3C4"
        );

        [Fact]
        public void MakeClearDebugInfo() => RunTest(
            ClearDebugInfo(document),
            "// Clear debug info from source.txt",
            "' Clear debug info from source.txt"
        );
    }
}
