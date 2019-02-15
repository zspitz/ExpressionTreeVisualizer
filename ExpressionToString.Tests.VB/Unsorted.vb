Public Class Unsorted
    <Fact>
    Public Sub Conditional()
        BuildAssert(
            Function(i As Integer) If(i > 10, i, i + 10),
            "(int i) => i > 10 ? i : i + 10",
            "Function(i As Integer) If(i > 10, i, i + 10)"
        )
    End Sub

    <Fact>
    Public Sub ConstantNothingToObject()
        BuildAssert(
            Function() Nothing,
            "() => null",
            "Function() Nothing"
        )
    End Sub

    <Fact>
    Public Sub ConstantNothingToReferenceType()
        BuildAssert(Of String)(
            Function() Nothing,
            "() => null",
            "Function() Nothing"
        )
    End Sub

    <Fact>
    Public Sub ConstantNothingToValueType()
        BuildAssert(Of Integer)(
            Function() Nothing,
            "() => 0",
            "Function() 0"
        )
    End Sub
End Class

