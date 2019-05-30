using System.Collections;
using System.Linq.Expressions;
using Xunit;
using static ExpressionToString.Tests.Globals;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
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
                "If(i > 10, i, i + 10)", 
                @"Condition(
    GreaterThan(i,
        Constant(10)
    ),
    i,
    Add(i,
        Constant(10)
    )
)"
            );
        }

        [Fact]
        public void MakeTypeCheck() => RunTest(
            TypeIs(
                Constant(""),
                typeof(string)
            ),
            "\"\" is string",
            "TypeOf \"\" Is String", 
            @"TypeIs(
    Constant(""""),
    typeof(string)
)"
        );

        [Fact]
        public void MakeTypeEqual() => RunTest(
            TypeEqual(
                Constant(""),
                typeof(IEnumerable)
            ),
            "\"\".GetType() == typeof(IEnumerable)",
            "\"\".GetType = GetType(IEnumerable)", 
            @"TypeEqual(
    Constant(""""),
    typeof(IEnumerable)
)"
        );

        [Fact]
        [Trait("Category",Invocation)]
        public void MakeInvocation() => RunTest(
            Invoke(
                Lambda(Constant(5))
            ),
            "(() => 5)()",
            "(Function() 5)()", 
            @"Invoke(
    Lambda(
        Constant(5)
    )
)"
        );

        [Fact]
        [Trait("Category", Lambdas)]
        public void MakeByRefParameter() => RunTest(
            Lambda(
                Constant(true),
                Parameter(typeof(string).MakeByRefType(), "s4")
            ),
            "(ref string s4) => true",
            "Function(ByRef s4 As String) True", @"Lambda(
    Constant(true),
    var s4 = Parameter(
        typeof(string).MakeByRef(),
        ""s4""
    )
)"
        );

        [Fact]
        [Trait("Category",Quoted)]
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
End Block",
            @"Block(new[] { x },
    Quote(
        Lambda(
            Call(
                typeof(Console).GetMethod(""WriteLine""),
                Constant(true)
            )
        )
    )
)"
        );

        [Fact]
        [Trait("Category", Quoted)]
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
' --- Quoted - end",
            @"Lambda(
    Quote(
        Lambda(
            Call(
                typeof(Console).GetMethod(""WriteLine""),
                Constant(true)
            )
        )
    )
)"
        );

        SymbolDocumentInfo document = SymbolDocument("source.txt");

        [Fact]
        [Trait("Category", DebugInfos)]
        public void MakeDebugInfo() => RunTest(
            DebugInfo(document,1,2,3,4),
            "// Debug to source.txt -- L1C2 : L3C4",
            "' Debug to source.txt -- L1C2 : L3C4",
            "DebugInfo(#SymbolDocumentInfo, 1, 2, 3, 4)"
        );

        [Fact]
        [Trait("Category", DebugInfos)]
        public void MakeClearDebugInfo() => RunTest(
            ClearDebugInfo(document),
            "// Clear debug info from source.txt",
            "' Clear debug info from source.txt", 
            "ClearDebugInfo(#SymbolDocumentInfo)"
        );
    }
}
