Partial Public Class VBCompilerGeneratedBase
    <Fact> <Trait("Category", Lambdas)>
    Sub NoParametersVoidReturn()
        RunTest(
            Sub() Console.WriteLine(),
            "() => Console.WriteLine()",
            "Sub() Console.WriteLine",
            "Lambda(
    Call(
        typeof(Console).GetMethod(""WriteLine"")
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", Lambdas)>
    Sub OneParameterVoidReturn()
        RunTest(Of String)(
            Sub(s) Console.WriteLine(s),
            "(string s) => Console.WriteLine(s)",
            "Sub(s As String) Console.WriteLine(s)",
            "Lambda(
    Call(
        typeof(Console).GetMethod(""WriteLine""),
        new[] { s }
    ),
    var s = Parameter(
        typeof(string),
        ""s""
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", Lambdas)>
    Sub TwoParametersVoidReturn()
        RunTest(Of String, String)(
            Sub(s1, s2) Console.WriteLine(s1 + s2),
            "(string s1, string s2) => Console.WriteLine(s1 + s2)",
            "Sub(s1 As String, s2 As String) Console.WriteLine(s1 + s2)",
            "Lambda(
    Call(
        typeof(Console).GetMethod(""WriteLine""),
        new[] {
            Call(
                typeof(string).GetMethod(""Concat""),
                new[] { s1, s2 }
            )
        }
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
        )
    End Sub

    <Fact> <Trait("Category", Lambdas)>
    Sub NoParametersNonVoidReturn()
        RunTest(
            Function() "abcd",
            "() => ""abcd""",
            "Function() ""abcd""",
            "Lambda(
    Constant(""abcd"")
)"
        )
    End Sub

    <Fact> <Trait("Category", Lambdas)>
    Sub OneParameterNonVoidReturn()
        RunTest(Of String, String)(
            Function(s) s,
            "(string s) => s",
            "Function(s As String) s",
            "Lambda(s,
    var s = Parameter(
        typeof(string),
        ""s""
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", Lambdas)>
    Sub TwoParametersNonVoidReturn()
        RunTest(Of String, String, String)(
            Function(s1, s2) s1 + s2,
            "(string s1, string s2) => s1 + s2",
            "Function(s1 As String, s2 As String) s1 + s2",
            "Lambda(
    Call(
        typeof(string).GetMethod(""Concat""),
        new[] { s1, s2 }
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
        )
    End Sub
End Class
