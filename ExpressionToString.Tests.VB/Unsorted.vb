Public Class Unsorted
    <Fact>
    Public Sub Conditional()
        BuildAssert(
            Function(i As Integer) If(i > 10, i, i + 10),
            "(int i) => i > 10 ? i : i + 10",
            "Function(i As Integer) If(i > 10, i, i + 10)"
        )
    End Sub
End Class

