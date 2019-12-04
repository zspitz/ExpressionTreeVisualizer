<ObjectContainer>
Partial Friend Module VBCompiler
    <Category(Conditionals)>
    Friend Conditional As Expression = Expr(Function(i As Integer) If(i > 10, i, i + 10))

    <Category(TypeChecks)>
    Friend TypeCheck As Expression = Expr(Function() TypeOf "" Is String)

    <Category(Invocation)>
    Friend InvocationNoArguments As Expression = IIFE(Function()
                                                          Dim del = Function() Date.Now.Day
                                                          Return Expr(Function() del())
                                                      End Function)

    <Category(Invocation)>
    Friend InvocationOneArgument As Expression = IIFE(Function()
                                                          Dim del = Function(i As Integer) Date.Now.Day
                                                          Return Expr(Function() del(5))
                                                      End Function)
End Module
