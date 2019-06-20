Imports System.Linq.Expressions
Imports ExpressionTreeVisualizer
Imports Microsoft.VisualStudio.DebuggerVisualizers
Imports System.Linq.Expressions.Expression
Imports ExpressionToString

Module Module1

    Sub Main()
        Dim writeline = GetType(Console).GetMethods().Single(
            Function(x) x.Name = "WriteLine" And x.GetParameters().Length = 0
        )
        Dim expr = IfThen(
            Constant(True),
            Block(
                Constant(True),
                Block(
                    {Parameter(GetType(String), "s1")},
                    Constant(True),
                    Constant(True)
                ),
                Constant(True)
            )
        )

        Console.WriteLine(expr.ToString("Visual Basic"))


        Dim multiplier As Expression(Of Func(Of Integer, Single, Single)) = Function(i, f) i * f

        Dim visualizerHost = New VisualizerDevelopmentHost(multiplier, GetType(Visualizer), GetType(VisualizerDataObjectSource))
        visualizerHost.ShowVisualizer()
    End Sub

End Module
