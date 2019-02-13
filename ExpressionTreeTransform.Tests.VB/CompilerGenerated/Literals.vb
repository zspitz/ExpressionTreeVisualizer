Public Class Literals
    <Fact>
    Sub [True]()
        BuildAssert(Function() True, "() => true", "Function() True")
    End Sub

    <Fact>
    Sub [False]()
        BuildAssert(Function() False, "() => false", "Function() False")
    End Sub

    <Fact>
    Sub [Nothing]()
        BuildAssert(Function() CType(Nothing, String), "() => null", "Function() Nothing")
    End Sub

    <Fact>
    Sub [Integer]()
        BuildAssert(Function() 5, "() => 5", "Function() 5")
    End Sub

    <Fact>
    Sub NonInteger()
        BuildAssert(Function() 7.32, "() => 7.32", "Function() 7.32")
    End Sub

    <Fact>
    Sub [String]()
        BuildAssert(Function() "abcd", "() => ""abcd""", "Function() ""abcd""")
    End Sub

    <Fact>
    Sub DateTime()
        BuildAssert(Function() #1981-1-1#, "() => #DateTime", $"Function() #{#1981-1-1#.ToString()}#")
    End Sub

    <Fact>
    Sub TimeSpan()
        Dim ts = New TimeSpan(5, 4, 3, 2, 1)
        BuildAssert(Constant(ts), "#TimeSpan", $"#{ts.ToString()}#")
    End Sub
End Class
