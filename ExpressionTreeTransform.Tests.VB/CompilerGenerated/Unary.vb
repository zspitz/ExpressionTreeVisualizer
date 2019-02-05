Public Class Unary
    <Fact>
    Public Sub ArrayLength()
        Dim arr = New String() {}
        BuildAssert(Function() arr.Length, "() => arr.Length", "Function() arr.Length")
    End Sub

    <Fact>
    Public Sub Convert()
        Dim o As Object = New Random
        BuildAssert(Function() CType(o, Random), "() => (Random)o", "Function() CType(o, Random)")
    End Sub

    <Fact>
    Public Sub CObject()
        Dim lst = New List(Of String)()
        BuildAssert(Function() CObj(lst), "() => (object)lst", "Function() CObj(lst)")
    End Sub

    <Fact>
    Public Sub Negate()
        Dim i = 1
        BuildAssert(Function() -i, "() => -i", "Function() -i")
    End Sub

    <Fact>
    Public Sub BitwiseNot()
        Dim i = 1
        BuildAssert(Function() Not i, "() => ~i", "Function() Not i")
    End Sub

    <Fact>
    Public Sub LogicalNot()
        Dim b = True
        BuildAssert(Function() Not b, "() => !b", "Function() Not b")
    End Sub

    <Fact>
    Public Sub TypeAs()
        Dim o As Object = Nothing
        BuildAssert(Function() TryCast(o, String), "() => o as string", "Function() TryCast(o, String)")
    End Sub
End Class
