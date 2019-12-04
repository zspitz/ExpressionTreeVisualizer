Partial Friend Module VBCompiler
    <Category(Unary)>
    Friend ArrayLength As Expression = IIFE(Function()
                                                Dim arr = New String() {}
                                                Return Expr(Function() arr.Length)
                                            End Function)

    <Category(Unary)>
    Friend Convert As Expression = IIFE(Function()
                                            Dim o As Object = New Random
                                            Return Expr(Function() CType(o, Random))
                                        End Function)

    <Category(Unary)>
    Friend CObject As Expression = IIFE(Function()
                                            Dim lst = New List(Of String)()
                                            Return Expr(Function() CObj(lst))
                                        End Function)

    <Category(Unary)>
    Friend Negate As Expression = IIFE(Function()
                                           Dim i = 1
                                           Return Expr(Function() -i)
                                       End Function)

    <Category(Unary)>
    Friend BitwiseNot As Expression = IIFE(Function()
                                               Dim i = 1
                                               Return Expr(Function() Not i)
                                           End Function)

    <Category(Unary)>
    Friend LogicalNot As Expression = IIFE(Function()
                                               Dim b = True
                                               Return Expr(Function() Not b)
                                           End Function)

    <Category(Unary)>
    Friend TypeAs As Expression = IIFE(Function()
                                           Dim o As Object = Nothing
                                           Return Expr(Function() TryCast(o, String))
                                       End Function)
End Module