Imports System
Imports System.Linq.Expressions
Imports Xunit
Imports Microsoft.CodeAnalysis.LanguageNames
Imports Microsoft.CodeAnalysis.VisualBasic
Imports Microsoft.CodeAnalysis.VisualBasic.Syntax
Imports ExpressionTreeTransform.Util

Namespace ExpressionTreeTransform.Tests.VB
    Public Class CompilerGenerated
        ReadOnly mapper As New Mapper

        Private Sub BuildAssert(Of T)(expr As Expression(Of Func(Of T)), stringExpression As String)
            Dim mapped = mapper.GetSyntaxNode(expr, VisualBasic)
            Dim node = VisualBasicSyntaxTree.ParseText($"Dim expr = {stringExpression}").GetRoot.DescendantNodes.OfType(Of EqualsValueSyntax).First.ChildNodes.First

            Assert.True(node.GetDiagnostics.None)
            Assert.True(mapped.IsEquivalentTo(node, False))
        End Sub

        <Fact>
        Sub ReturnBooleanTrue()
            BuildAssert(Function() True, "Function() True")
        End Sub

        <Fact>
        Sub ReturnBooleanFalse()
            BuildAssert(Function() False, "Function() False")
        End Sub
    End Class
End Namespace

