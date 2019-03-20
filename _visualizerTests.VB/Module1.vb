Imports System.Linq.Expressions
Imports ExpressionTreeVisualizer
Imports Microsoft.VisualStudio.DebuggerVisualizers

Module Module1

    Sub Main()
        Dim expr As Expression(Of Func(Of String, Object, Boolean)) = Function(s1, s2) s1 Like s2
        Dim visualizerHost = New VisualizerDevelopmentHost(expr, GetType(Visualizer), GetType(VisualizerDataObjectSource))
        visualizerHost.ShowVisualizer()
    End Sub

End Module
