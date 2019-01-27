Imports System.Linq.Expressions
Imports Xunit
Imports ExpressionTreeTransform.Util.Globals

Namespace ExpressionTreeTransform.Tests
    Public Class CompilerGenerated
        Private Sub BuildAssert(Of T)(expr As Expression(Of Func(Of T)), CSharpResult As String, VBResult As String)
            Dim testCSharpCode = expr.ToCode(CSharp)
            Dim testVBCode = expr.ToCode(VisualBasic)
            Assert.True(testCSharpCode = CSharpResult)
            Assert.True(testVBCode = VBResult)
        End Sub

        <Fact>
        Sub ReturnBooleanTrue()
            BuildAssert(Function() True, "() => true", "Function() True")
        End Sub

        <Fact>
        Sub ReturnBooleanFalse()
            BuildAssert(Function() False, "() => false", "Function() False")
        End Sub

        <Fact>
        Sub ReturnObjectCreation()
            BuildAssert(Function() New Date(1980, 1, 1), "() => new DateTime(1981, 1, 1)", "Function() New Date(1980, 1, 1)")
        End Sub

        <Fact>
        Sub ReturnDateLiteral()
            BuildAssert(Function() #1/1/1980#, "() => #DateTime", "Function() #1/1/1980")
        End Sub
    End Class
End Namespace

