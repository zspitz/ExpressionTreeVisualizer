Imports ExpressionToString.Tests.Functions

Namespace Objects
    Partial Public Module VBCompiler
        <Category(Unary)>
        Public ArrayLength As Expression = IIFE(Function()
                                                    Dim arr = New String() {}
                                                    Return Expr(Function() arr.Length)
                                                End Function)

        <Category(Unary)>
        Public Convert As Expression = IIFE(Function()
                                                Dim o As Object = New Random
                                                Return Expr(Function() CType(o, Random))
                                            End Function)

        <Category(Unary)>
        Public CObject As Expression = IIFE(Function()
                                                Dim lst = New List(Of String)()
                                                Return Expr(Function() CObj(lst))
                                            End Function)

        <Category(Unary)>
        Public Negate As Expression = IIFE(Function()
                                               Dim i = 1
                                               Return Expr(Function() -i)
                                           End Function)

        <Category(Unary)>
        Public BitwiseNot As Expression = IIFE(Function()
                                                   Dim i = 1
                                                   Return Expr(Function() Not i)
                                               End Function)

        <Category(Unary)>
        Public LogicalNot As Expression = IIFE(Function()
                                                   Dim b = True
                                                   Return Expr(Function() Not b)
                                               End Function)

        <Category(Unary)>
        Public TypeAs As Expression = IIFE(Function()
                                               Dim o As Object = Nothing
                                               Return Expr(Function() TryCast(o, String))
                                           End Function)
    End Module
End Namespace
