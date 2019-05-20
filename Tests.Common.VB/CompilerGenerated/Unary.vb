Partial Public Class VBCompilerGeneratedBase
    <Fact> <Trait("Category", Unary)>
    Sub ArrayLength()
        Dim arr = New String() {}
        RunTest(Function() arr.Length, "() => arr.Length", "Function() arr.Length")
    End Sub

    <Fact> <Trait("Category", Unary)>
    Sub Convert()
        Dim o As Object = New Random
        RunTest(Function() CType(o, Random), "() => (Random)o", "Function() CType(o, Random)")
    End Sub

    <Fact> <Trait("Category", Unary)>
    Sub CObject()
        Dim lst = New List(Of String)()
        RunTest(Function() CObj(lst), "() => (object)lst", "Function() CObj(lst)")
    End Sub

    <Fact> <Trait("Category", Unary)>
    Sub Negate()
        Dim i = 1
        RunTest(Function() -i, "() => -i", "Function() -i")
    End Sub

    <Fact> <Trait("Category", Unary)>
    Sub BitwiseNot()
        Dim i = 1
        RunTest(Function() Not i, "() => ~i", "Function() Not i")
    End Sub

    <Fact> <Trait("Category", Unary)>
    Sub LogicalNot()
        Dim b = True
        RunTest(Function() Not b, "() => !b", "Function() Not b")
    End Sub

    <Fact> <Trait("Category", Unary)>
    Sub TypeAs()
        Dim o As Object = Nothing
        RunTest(Function() TryCast(o, String), "() => o as string", "Function() TryCast(o, String)")
    End Sub
End Class
