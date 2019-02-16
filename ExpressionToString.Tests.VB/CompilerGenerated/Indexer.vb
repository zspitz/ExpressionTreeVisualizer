Public Class Indexer
    <Fact>
    Public Sub ArraySingleIndex()
        Dim arr = New String() {}
        BuildAssert(
            Function() arr(5),
            "() => arr[5]",
            "Function() arr(5)"
        )
    End Sub

    <Fact>
    Public Sub ArrayMultipleIndex()
        Dim arr = New String(,) {}
        BuildAssert(
            Function() arr(5, 6),
            "() => arr[5, 6]",
            "Function() arr(5, 6)"
        )
    End Sub

    <Fact>
    Public Sub TypeIndexer()
        Dim lst = New List(Of String)()
        BuildAssert(
            Function() lst(3),
            "() => lst[3]",
            "Function() lst(3)"
        )
    End Sub
End Class
