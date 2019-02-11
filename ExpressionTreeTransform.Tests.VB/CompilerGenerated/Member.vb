Public Class Member
    <Fact> Public Sub InstanceMember()
        Dim s = ""
        BuildAssert(
            Function() s.Length,
            "() => s.Length",
            "Function() s.Length"
        )
    End Sub

    <Fact> Public Sub ClosedVariable()
        Dim s = ""
        BuildAssert(
            Function() s,
            "() => s",
            "Function() s"
        )
    End Sub

    <Fact> Public Sub StaticMember()
        BuildAssert(
            Function() String.Empty,
            "() => string.Empty",
            "Function() String.Empty"
        )
    End Sub
End Class
