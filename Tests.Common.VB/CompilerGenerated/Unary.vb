Partial Public Class VBCompilerGeneratedBase
    <Fact> <Trait("Category", Unary)>
    Sub ArrayLength()
        Dim arr = New String() {}
        RunTest(
            Function() arr.Length,
            "() => arr.Length",
            "Function() arr.Length",
            "Lambda(
    ArrayLength(arr)
)"
        )
    End Sub

    <Fact> <Trait("Category", Unary)>
    Sub Convert()
        Dim o As Object = New Random
        RunTest(
            Function() CType(o, Random),
            "() => (Random)o",
            "Function() CType(o, Random)",
            "Lambda(
    Convert(o,
        typeof(Random)
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", Unary)>
    Sub CObject()
        Dim lst = New List(Of String)()
        RunTest(
            Function() CObj(lst),
            "() => lst",
            "Function() lst",
            "Lambda(
    Convert(lst,
        typeof(object)
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", Unary)>
    Sub Negate()
        Dim i = 1
        RunTest(
            Function() -i,
            "() => -i",
            "Function() -i",
            "Lambda(
    NegateChecked(i)
)"
        )
    End Sub

    <Fact> <Trait("Category", Unary)>
    Sub BitwiseNot()
        Dim i = 1
        RunTest(
            Function() Not i,
            "() => ~i",
            "Function() Not i",
            "Lambda(
    Not(i)
)"
        )
    End Sub

    <Fact> <Trait("Category", Unary)>
    Sub LogicalNot()
        Dim b = True
        RunTest(
            Function() Not b,
            "() => !b",
            "Function() Not b",
            "Lambda(
    Not(b)
)"
        )
    End Sub

    <Fact> <Trait("Category", Unary)>
    Sub TypeAs()
        Dim o As Object = Nothing
        RunTest(
            Function() TryCast(o, String),
            "() => o as string",
            "Function() TryCast(o, String)",
            "Lambda(
    TypeAs(o,
        typeof(string)
    )
)"
        )
    End Sub
End Class
