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
            "Sub() Console.WriteLine",
            @"Lambda(
    Call(
        typeof(Console).GetMethod(""WriteLine"")
    )
)"
        );

        [Fact]
        [Trait("Category", Lambdas)]
        public void OneParameterVoidReturn() => RunTest(
            Lambda(Call(writeline1, s), s),
            "(string s) => Console.WriteLine(s)",
            "Sub(s As String) Console.WriteLine(s)",
            @"Lambda(
    Call(
        typeof(Console).GetMethod(""WriteLine""),
        s
    ),
    var s = Parameter(
        typeof(string),
        ""s""
    )
)"
        );

        [Fact]
        [Trait("Category", Lambdas)]
        public void TwoParametersVoidReturn() => RunTest(
            Lambda(Call(writeline1, Add(s1, s2, concat)), s1, s2),
            "(string s1, string s2) => Console.WriteLine(s1 + s2)",
            "Sub(s1 As String, s2 As String) Console.WriteLine(s1 + s2)",
            @"Lambda(
    Call(
        typeof(Console).GetMethod(""WriteLine""),
        Add(s1, s2)
    ),
    var s1 = Parameter(
        typeof(string),
        ""s1""
    ),
    var s2 = Parameter(
        typeof(string),
        ""s2""
    )
)"
        );

        [Fact]
        [Trait("Category", Lambdas)]
        public void NoParametersNonVoidReturn() => RunTest(
            Lambda(Constant("abcd")),
            "() => \"abcd\"",
            "Function() \"abcd\"",
            @"Lambda(
    Constant(""abcd"")
)"
        );

        [Fact]
        [Trait("Category", Lambdas)]
        public void OneParameterNonVoidReturn() => RunTest(
            Lambda(s, s),
            "(string s) => s",
            "Function(s As String) s",
            @"Lambda(s,
    var s = Parameter(
        typeof(string),
        ""s""
    )
)"
        );

        [Fact]
        [Trait("Category", Lambdas)]
        public void TwoParametersNonVoidReturn() => RunTest(
            Lambda(Add(s1, s2, concat), s1, s2),
            "(string s1, string s2) => s1 + s2",
            "Function(s1 As String, s2 As String) s1 + s2",
            @"Lambda(
    Add(s1, s2),
    var s1 = Parameter(
        typeof(string),
        ""s1""
    ),
    var s2 = Parameter(
        typeof(string),
        ""s2""
    )
)"
        );

        [Fact]
        [Trait("Category", Lambdas)]
        public void NamedLambda() => RunTest(
            Lambda(
                Add(s1, s2, concat),
                "name",
                new[] { s1, s2 }
            ),
            "(string s1, string s2) => s1 + s2",
            "Function(s1 As String, s2 As String) s1 + s2",
                        @"Lambda(
    Add(s1, s2),
    ""name"", new[] {
        var s1 = Parameter(
            typeof(string),
            ""s1""
        ),
        var s2 = Parameter(
            typeof(string),
            ""s2""
        )
    }
)"
        );

        [Fact]
        [Trait("Category", Lambdas)]
        public void MultilineLambda() => RunTest(
            Lambda(
                IfThen(Constant(true), writeLineTrue)
            ),
            @"() => {
    if (true) {
        Console.WriteLine(true);
    }
}",
            @"Sub()
    If True Then Console.WriteLine(True)
End Sub",
            @"Lambda(
    IfThen(
        Constant(true),
        Call(
            typeof(Console).GetMethod(""WriteLine""),
            Constant(true)
        )
    )
)"
        );

        [Fact]
        [Trait("Category", Lambdas)]
        public void NestedLambda() => RunTest(
            Lambda(
                Lambda(Add(s1, s2, concat), s1, s2)
            ),
            @"() => {
    return (string s1, string s2) => s1 + s2;
}",
            @"Function()
    Return Function(s1 As String, s2 As String) s1 + s2
End Function",
            @"Lambda(
    Lambda(
        Add(s1, s2),
        var s1 = Parameter(
            typeof(string),
            ""s1""
        ),
        var s2 = Parameter(
            typeof(string),
            ""s2""
        )
    )
)"
        );

        [Fact]
        [Trait("Category", Lambdas)]
        public void LambdaMultilineBlockNonvoidReturn() => RunTest(
            Lambda(
                Block(
                    Constant(true),
                    Constant(true)
                )
            ),
            @"() => {
    true;
    return true;
}",
            @"Function()
    True
    Return True
End Function",
            @"Lambda(
    Block(
        Constant(true),
        Constant(true)
    )
)"
        );

        [Fact]
        [Trait("Category", Lambdas)]
        public void LambdaMultilineNestedBlockNonvoidReturn() => RunTest(
            Lambda(
                Block(
                    Constant(true),
                    Block(
                        new[] { s1, s2 },
                        Constant(true),
                        Constant(true)
                    )
                )
            ),
            @"() => {
    true;
    {
        string s1;
        string s2;
        true;
        return true;
    }
}",
            @"Function()
    True
    Block
        Dim s1 As String
        Dim s2 As String
        True
        Return True
    End Block
End Function",
            @"Lambda(
    Block(
        Constant(true),
        Block(new[] { s1, s2 },
            Constant(true),
            Constant(true)
        )
    )
)"
        );

    }
}
