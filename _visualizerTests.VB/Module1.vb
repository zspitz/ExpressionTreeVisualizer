Imports System.Linq.Expressions
Imports ExpressionTreeVisualizer
Imports Microsoft.VisualStudio.DebuggerVisualizers
Imports System.Linq.Expressions.Expression

Module Module1

    Sub Main()
        Dim i = 7
        Dim j = 5
        Dim expr As Expression(Of Func(Of Integer)) = Function() i * j
        Dim data = New VisualizerData(expr, "C#")
        Dim visualizerHost = New VisualizerDevelopmentHost(data, GetType(Visualizer))
        visualizerHost.ShowVisualizer()
    End Sub

End Module
