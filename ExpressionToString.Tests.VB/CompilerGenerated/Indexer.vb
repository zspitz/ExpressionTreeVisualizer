Public Class Indexer
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

    <Fact>
    Public Sub VBDeclaredTypeIndexer()
        Dim x As New DummyWithDefault
        BuildAssert(
            Function() x(5),
            "() => x[5]",
            "Function() x(5)"
        )
    End Sub
End Class
