Imports System.Linq.Expressions.Expression

Partial Public Class VBCompilerGeneratedBase
    <Fact> <Trait("Category", Literal)>
    Sub [True]()
        PreRunTest()
    End Sub

    <Fact> <Trait("Category", Literal)>
    Sub [False]()
        PreRunTest()
    End Sub

    <Fact> <Trait("Category", Literal)>
    Sub NothingString()
        PreRunTest()
    End Sub

    <Fact> <Trait("Category", Literal)>
    Sub [Nothing]()
        PreRunTest()
    End Sub

    <Fact> <Trait("Category", Literal)>
    Sub [Integer]()
        PreRunTest()
    End Sub

    <Fact> <Trait("Category", Literal)>
    Sub NonInteger()
        PreRunTest()
    End Sub

    <Fact> <Trait("Category", Literal)>
    Sub [String]()
        PreRunTest()
    End Sub

    <Fact> <Trait("Category", Literal)>
    Sub DateTime()
        RunTest(
            Function() #1981-1-1#,
            "() => #DateTime",
            $"Function() #{#1981-1-1#.ToString()}#",
            "Lambda(
    Constant(#DateTime)
)"
        )
    End Sub

    <Fact> <Trait("Category", Literal)>
    Sub EscapedString()
        RunTest(
            Function() """",
            "() => ""\""""",
            "Function() """"""""",
            "Lambda(
    Constant(""\"""")
)"
        )
    End Sub

    <Fact> <Trait("Category", Literal)>
    Sub InterpolatedString()
        Dim toString = #2001-1-1#.ToString
        RunTest(
            Function() $"{#2001-1-1#}",
            "() => $""{#DateTime}""",
            "Function() $""{#" + toString + "#}""",
            "Lambda(
    Call(
        typeof(string).GetMethod(""Format""),
        Constant(""{0}""),
        Convert(
            Constant(#DateTime),
            typeof(object)
        )
    )
)"
        )
    End Sub
End Class
