Imports System.Linq.Expressions.Expression

Partial Public Class VBCompilerGeneratedBase
    <Fact>
    Sub [True]()
        RunTest(Function() True, "() => true", "Function() True")
    End Sub

    <Fact>
    Sub [False]()
        RunTest(Function() False, "() => false", "Function() False")
    End Sub

    <Fact>
    Sub [Nothing]()
        RunTest(Function() CType(Nothing, String), "() => null", "Function() Nothing")
    End Sub

    <Fact>
    Sub [Integer]()
        RunTest(Function() 5, "() => 5", "Function() 5")
    End Sub

    <Fact>
    Sub NonInteger()
        RunTest(Function() 7.32, "() => 7.32", "Function() 7.32")
    End Sub

    <Fact>
    Sub [String]()
        RunTest(Function() "abcd", "() => ""abcd""", "Function() ""abcd""")
    End Sub

    <Fact>
    Sub DateTime()
        RunTest(Function() #1981-1-1#, "() => #DateTime", $"Function() #{#1981-1-1#.ToString()}#")
    End Sub

    <Fact>
    Sub TimeSpan()
        Dim ts = New TimeSpan(5, 4, 3, 2, 1)
        RunTest(Constant(ts), "#TimeSpan", $"#{ts.ToString()}#")
    End Sub

    <Fact>
    Sub EscapedString()
        RunTest(
            Function() """",
            "() => ""\""""",
            "Function() """""""""
        )
    End Sub

    <Fact>
    Sub InterpolatedString()
        Dim toString = #2001-1-1#.ToString
        RunTest(
            Function() $"{#2001-1-1#}",
            "() => $""{(object)#DateTime}""",
            "Function() $""{CObj(#" + toString + "#)}"""
        )
    End Sub
End Class
