Imports ExpressionToString.Tests.Functions

Namespace Objects
    Partial Public Module VBCompiler
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

        <Category(Indexer)>
        Public ArraySingleIndex As Expression = IIFE(Function()
                                                         Dim arr = New String() {}
                                                         Return Expr(Function() arr(5))
                                                     End Function)

        <Category(Indexer)>
        Public ArrayMultipleIndex As Expression = IIFE(Function()
                                                           Dim arr = New String(,) {}
                                                           Return Expr(Function() arr(5, 6))
                                                       End Function)

        <Category(Indexer)>
        Public TypeIndexer As Expression = IIFE(Function()
                                                    Dim lst = New List(Of String)()
                                                    Return Expr(Function() lst(3))
                                                End Function)

        <Category(Indexer)>
        Public VBDeclaredTypeIndexer As Expression = IIFE(Function()
                                                              Dim x As New DummyWithDefault
                                                              Return Expr(Function() x(5))
                                                          End Function)
    End Module
End Namespace