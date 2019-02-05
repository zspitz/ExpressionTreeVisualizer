Imports System.Linq.Expressions

<Assembly: AssemblyTrait("Source", VBCompiler)>

Public Module Runners
    Sub BuildAssert(Of T)(expr As Expression(Of Func(Of T)), CSharpResult As String, VBResult As String)
        BuildAssert(CType(expr, Expression), CSharpResult, VBResult)
    End Sub

    Sub BuildAssert(expr As Expression, CSharpResult As String, VBResult As String)
        Dim testCSharpCode = expr.ToCode(CSharp)
        Dim testVBCode = expr.ToCode(VisualBasic)
        Assert.True(testCSharpCode = CSharpResult)
        Assert.True(testVBCode = VBResult)
    End Sub
End Module
