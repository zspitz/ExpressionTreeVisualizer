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

    <Fact>
    Public Sub TypeCheck()
        BuildAssert(
            Function() TypeOf "" Is String,
            "() => """" is string",
            "Function() TypeOf """" Is String"
        )
    End Sub

    <Fact>
    Public Sub InvocationNoArguments()
        Dim del = Function() Date.Now.Day
        BuildAssert(
            Function() del(),
            "() => del()",
            "Function() del()"
        )
    End Sub

    <Fact>
    Public Sub InvocationOneArgument()
        Dim del = Function(i As Integer) Date.Now.Day
        BuildAssert(
            Function() del(5),
            "() => del(5)",
            "Function() del(5)"
        )
    End Sub
End Class

