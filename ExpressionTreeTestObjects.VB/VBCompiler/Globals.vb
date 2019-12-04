Friend Class Foo
    Friend Property Bar As String
    Friend Property Baz As String
    Friend Sub New()
    End Sub
    Friend Sub New(ByVal baz As String)
    End Sub
End Class

#Disable Warning IDE0060 ' Remove unused parameter
#Disable Warning BC40003 ' Member shadows an overloadable member declared in the base type
Friend Class Wrapper
    Inherits List(Of String)
    Friend Sub Add(ByVal s1 As String)
        Throw New NotImplementedException
    End Sub
    Friend Sub Add(ByVal s1 As String, ByVal s2 As String)
        Throw New NotImplementedException
    End Sub
    Friend Sub Add(ByVal s1 As String, ByVal s2 As String, ByVal s3 As String)
        Throw New NotImplementedException
    End Sub
End Class
#Enable Warning BC40003 ' Member shadows an overloadable member declared in the base type
#Enable Warning IDE0060 ' Remove unused parameter

Friend Class Node
    Property Data As NodeData = New NodeData
    Property Children As IList(Of Node) = New List(Of Node)
End Class
Friend Class NodeData
    Property Name As String
End Class

Friend Module Dummy
    Friend Sub DummyMethod()
    End Sub
End Module