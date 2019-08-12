Imports ExpressionToString.Tests.Functions

Namespace Objects
    Partial Public Module VBCompiler
        <Category(Binary)>
        Public Add As Expression = IIFE(Function()
                                            Dim x As Double = 0, y As Double = 0
                                            Return Expr(Function() x + y)
                                        End Function)

        <Category(Binary)>
        Public Divide As Expression = IIFE(Function()
                                               Dim x As Double = 0, y As Double = 0
                                               Return Expr(Function() x / y)
                                           End Function)

        <Category(Binary)>
        Public Modulo As Expression = IIFE(Function()
                                               Dim x As Double = 0, y As Double = 0
                                               Return Expr(Function() x Mod y)
                                           End Function)

        <Category(Binary)>
        Public Multiply As Expression = IIFE(Function()
                                                 Dim x As Double = 0, y As Double = 0
                                                 Return Expr(Function() x * y)
                                             End Function)

        <Category(Binary)>
        Public Subtract As Expression = IIFE(Function()
                                                 Dim x As Double = 0, y As Double = 0
                                                 Return Expr(Function() x - y)
                                             End Function)

        <Category(Binary)>
        Public AndBitwise As Expression = IIFE(Function()
                                                   Dim i As Integer = 0, j As Integer = 0
                                                   Return Expr(Function() i And j)
                                               End Function)

        <Category(Binary)>
        Public OrBitwise As Expression = IIFE(Function()
                                                  Dim i As Integer = 0, j As Integer = 0
                                                  Return Expr(Function() i Or j)
                                              End Function)

        <Category(Binary)>
        Public ExclusiveOrBitwise As Expression = IIFE(Function()
                                                           Dim i As Integer = 0, j As Integer = 0
                                                           Return Expr(Function() i Xor j)
                                                       End Function)

        <Category(Binary)>
        Public AndLogical As Expression = IIFE(Function()
                                                   Dim b1 As Boolean = True, b2 As Boolean = True
                                                   Return Expr(Function() b1 And b2)
                                               End Function)

        <Category(Binary)>
        Public OrLogical As Expression = IIFE(Function()
                                                  Dim b1 As Boolean = True, b2 As Boolean = True
                                                  Return Expr(Function() b1 Or b2)
                                              End Function)

        <Category(Binary)>
        Public ExclusiveOrLogical As Expression = IIFE(Function()
                                                           Dim b1 As Boolean = True, b2 As Boolean = True
                                                           Return Expr(Function() b1 Xor b2)
                                                       End Function)

        <Category(Binary)>
        Public [AndAlso] As Expression = IIFE(Function()
                                                  Dim b1 As Boolean = True, b2 As Boolean = True
                                                  Return Expr(Function() b1 AndAlso b2)
                                              End Function)

        <Category(Binary)>
        Public [OrElse] As Expression = IIFE(Function()
                                                 Dim b1 As Boolean = True, b2 As Boolean = True
                                                 Return Expr(Function() b1 OrElse b2)
                                             End Function)

        <Category(Binary)>
        Public Equal As Expression = IIFE(Function()
                                              Dim i As Integer = 0, j As Integer = 0
                                              Return Expr(Function() i = j)
                                          End Function)

        <Category(Binary)>
        Public NotEqual As Expression = IIFE(Function()
                                                 Dim i As Integer = 0, j As Integer = 0
                                                 Return Expr(Function() i <> j)
                                             End Function)

        <Category(Binary)>
        Public GreaterThanOrEqual As Expression = IIFE(Function()
                                                           Dim i As Integer = 0, j As Integer = 0
                                                           Return Expr(Function() i >= j)
                                                       End Function)

        <Category(Binary)>
        Public GreaterThan As Expression = IIFE(Function()
                                                    Dim i As Integer = 0, j As Integer = 0
                                                    Return Expr(Function() i > j)
                                                End Function)

        <Category(Binary)>
        Public LessThan As Expression = IIFE(Function()
                                                 Dim i As Integer = 0, j As Integer = 0
                                                 Return Expr(Function() i < j)
                                             End Function)

        <Category(Binary)>
        Public LessThanOrEqual As Expression = IIFE(Function()
                                                        Dim i As Integer = 0, j As Integer = 0
                                                        Return Expr(Function() i <= j)
                                                    End Function)

        <Category(Binary)>
        Public Coalesce As Expression = IIFE(Function()
                                                 Dim s1 As String = Nothing, s2 As String = Nothing
                                                 Return Expr(Function() If(s1, s2))
                                             End Function)

        ' The VB compiler has special behavior around shift operations - https://docs.microsoft.com/dotnet/visual-basic/language-reference/operators/left-shift-operator#remarks
        <Category(Binary)>
        Public LeftShift As Expression = IIFE(Function()
                                                  Dim i As Integer = 0, j As Integer = 0
                                                  Return Expr(Function() i << j)
                                              End Function)

        <Category(Binary)>
        Public RightShift As Expression = IIFE(Function()
                                                   Dim i As Integer = 0, j As Integer = 0
                                                   Return Expr(Function() i >> j)
                                               End Function)

        <Category(Binary)>
        Public ArrayIndex As Expression = IIFE(Function()
                                                   Dim arr = New String() {}
                                                   Return Expr(Function() arr(0))
                                               End Function)

        <Category(Binary)>
        Public Power As Expression = IIFE(Function()
                                              Dim x As Double = 0, y As Double = 0
                                              Return Expr(Function() x ^ y)
                                          End Function)
    End Module
End Namespace