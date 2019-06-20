Imports System.Linq.Expressions
Imports ExpressionTreeVisualizer
Imports Microsoft.VisualStudio.DebuggerVisualizers
Imports System.Linq.Expressions.Expression
Imports ExpressionToString

Module Module1

    Sub Main()
        Dim s = "ab"
        Dim expr As Expression(Of Func(Of Boolean)) = Function() s Like "ab*"

        Dim visualizerHost = New VisualizerDevelopmentHost(expr, GetType(Visualizer), GetType(VisualizerDataObjectSource))
        visualizerHost.ShowVisualizer()
    End Sub

End Module
