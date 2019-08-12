Imports ExpressionToString.Tests.Functions

Namespace Objects
    Partial Public Module VBCompiler
        <Category(Conditionals)>
        Public Conditional As Expression = Expr(Function(i As Integer) If(i > 10, i, i + 10))

        <Category(TypeChecks)>
        Public TypeCheck As Expression = Expr(Function() TypeOf "" Is String)

        <Category(Invocation)>
        Public InvocationNoArguments As Expression = IIFE(Function()
                                                              Dim del = Function() Date.Now.Day
                                                              Return Expr(Function() del())
                                                          End Function)

        <Category(Invocation)>
        Public InvocationOneArgument As Expression = IIFE(Function()
                                                              Dim del = Function(i As Integer) Date.Now.Day
                                                              Return Expr(Function() del(5))
                                                          End Function)
    End Module
End Namespace
