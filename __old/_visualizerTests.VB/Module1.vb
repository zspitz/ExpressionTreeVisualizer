Imports System.Linq.Expressions
Imports ExpressionTreeVisualizer
Imports Microsoft.VisualStudio.DebuggerVisualizers
Imports System.Linq.Expressions.Expression

Module Module1

    Sub Main()
        Dim i = 13, j = 22
        Dim expr As Expression(Of Func(Of Integer)) = Function() i >> j
        Dim visualizerHost = New VisualizerDevelopmentHost(expr, GetType(Visualizer), GetType(VisualizerDataObjectSource))
        visualizerHost.ShowVisualizer()
    End Sub

End Module
