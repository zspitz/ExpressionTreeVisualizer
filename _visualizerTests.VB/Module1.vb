Imports System.Linq.Expressions
Imports ExpressionTreeVisualizer
Imports Microsoft.VisualStudio.DebuggerVisualizers

Module Module1

    Sub Main()
        Dim multiplier As Expression(Of Func(Of Integer, Single, Single)) = Function(i, f) i * f

        Dim visualizerHost = New VisualizerDevelopmentHost(multiplier, GetType(Visualizer), GetType(VisualizerDataObjectSource))
        visualizerHost.ShowVisualizer()
    End Sub

End Module
