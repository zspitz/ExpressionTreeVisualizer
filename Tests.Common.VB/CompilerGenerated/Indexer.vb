Partial Public Class VBCompilerGeneratedBase
    Private Class DummyWithDefault
        Dim data As Integer
        Default Property Item(index As Integer) As Integer
            Get
                Return data
            End Get
            Set(value As Integer)
                data = index
            End Set
        End Property
    End Class

    <Fact> <Trait("Category", Indexer)>
    Public Sub ArraySingleIndex()
        Dim arr = New String() {}
        RunTest(
            Function() arr(5),
            "() => arr[5]",
            "Function() arr(5)",
            "Lambda(
    ArrayIndex(arr,
        Constant(5)
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", Indexer)>
    Public Sub ArrayMultipleIndex()
        Dim arr = New String(,) {}
        RunTest(
            Function() arr(5, 6),
            "() => arr[5, 6]",
            "Function() arr(5, 6)",
            "Lambda(
    ArrayIndex(arr, new[] {
        Constant(5),
        Constant(6)
    })
)"
        )
    End Sub

    <Fact> <Trait("Category", Indexer)>
    Public Sub TypeIndexer()
        Dim lst = New List(Of String)()
        RunTest(
            Function() lst(3),
            "() => lst[3]",
            "Function() lst(3)",
            "Lambda(
    Property(lst,
        typeof(List<string>).GetProperty(""Item""),
        new[] {
            Constant(3)
        }
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", Indexer)>
    Public Sub VBDeclaredTypeIndexer()
        Dim x As New DummyWithDefault
        RunTest(
            Function() x(5),
            "() => x[5]",
            "Function() x(5)",
            "Lambda(
    Property(x,
        typeof(DummyWithDefault).GetProperty(""Item""),
        new[] {
            Constant(5)
        }
    )
)"
        )
    End Sub
End Class
