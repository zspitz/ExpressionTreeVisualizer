Public Module Tests
    <Fact>
    Public Sub DateTime()
        Dim expr = Functions.Expr(Function() #1981-1-1#)
        Assert.Equal($"() => #DateTime", expr.ToString("C#"))
        Assert.Equal($"Function() #{#1981-1-1#.ToString()}#", expr.ToString("Visual Basic"))
    End Sub

    <Fact>
    Sub InterpolatedString()
        Dim expr = Functions.Expr(Function() $"{#2001-1-1#}")
        Assert.Equal("() => $""{#DateTime}", expr.ToString("C#"))
        Assert.Equal("Function() $""{#" + #2001-1-1#.ToString + "#}""", expr.ToString("Visual Basic"))
    End Sub
End Module
