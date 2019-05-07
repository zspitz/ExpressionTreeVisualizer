Public Class Member
    <Fact>
    Sub InstanceMember()
        Dim s = ""
        RunTest(
            Function() s.Length,
            "() => s.Length",
            "Function() s.Length"
        )
    End Sub

    <Fact>
    Sub ClosedVariable()
        Dim s = ""
        RunTest(
            Function() s,
            "() => s",
            "Function() s"
        )
    End Sub

    <Fact>
    Sub StaticMember()
        RunTest(
            Function() String.Empty,
            "() => string.Empty",
            "Function() String.Empty"
        )
    End Sub

    <Fact(Skip:="Test for nested scope")>
    Sub NestedClosureScope()
        Assert.False(True)
    End Sub
End Class
