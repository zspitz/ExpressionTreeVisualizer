Imports System.Linq.Expressions
Imports ExpressionTreeVisualizer
Imports Microsoft.VisualStudio.DebuggerVisualizers
Imports System.Linq.Expressions.Expression
Imports ExpressionTreeToString

Module Module1

    Sub Main()
        'Dim s = "ab"
        'Dim expr As Expression(Of Func(Of Boolean)) = Function() s Like "ab*"

        'Dim queryableSource As IQueryable(Of Person) = (New List(Of Person)).AsQueryable
        'Dim qry = queryableSource.Where(Function(x) x.LastName.StartsWith("D"))

        'Dim expr = qry.GetType().GetProperty("Expression", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance).GetValue(qry)

        'Dim expr As Expression(Of Func(Of Person, Boolean)) = Function(x) x.LastName.StartsWith("D")


        Dim expr As Expression(Of Func(Of Integer, Boolean)) = Function(n As Integer) (n + 42 = 27) = ("abcd" <> String.Empty)




        Dim visualizerHost = New VisualizerDevelopmentHost(expr, GetType(Visualizer), GetType(VisualizerDataObjectSource))
        visualizerHost.ShowVisualizer()
    End Sub

End Module

Friend Class Person
    Property LastName As String
    Property FirstName As String
    Property DateOfBirth As Date
End Class
