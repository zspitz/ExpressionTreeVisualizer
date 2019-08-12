Partial Public Class VBCompilerGeneratedBase
    <Fact> <Trait("Category", Member)>
    Sub InstanceMember()
        PreRunTest()
    End Sub

    <Fact> <Trait("Category", Member)>
    Sub ClosedVariable()
        PreRunTest()
    End Sub

    <Fact> <Trait("Category", Member)>
    Sub StaticMember()
        PreRunTest()
    End Sub

    <Fact(Skip:="Test for nested scope")> <Trait("Category", Member)>
    Sub NestedClosureScope()
        Assert.False(True)
    End Sub
End Class
