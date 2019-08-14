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
        Dim expr = Functions.Expr(Function() #1981-1-1#)
        Assert.Equal($"Function() #{#1981-1-1#.ToString()}#", expr.ToString("Visual Basic"))
    End Sub

    <Fact> <Trait("Category", Literal)>
    Sub EscapedString()
        PreRunTest()
    End Sub

    <Fact> <Trait("Category", Literal)>
    Sub InterpolatedString()
        Dim expr = Functions.Expr(Function() $"{#2001-1-1#}")
        Assert.Equal("Function() $""{#" + #2001-1-1#.ToString + "#}""", expr.ToString("Visual Basic"))
    End Sub
End Class
