Partial Module VBCompiler
    <Category(Lambdas)>
    Friend NoParametersVoidReturn As Expression = Expr(Sub() Console.WriteLine())

    <Category(Lambdas)>
    Friend OneParameterVoidReturn As Expression = Expr(Sub(s As String) Console.WriteLine(s))

    <Category(Lambdas)>
    Friend TwoParametersVoidReturn As Expression = Expr(Sub(s1 As String, s2 As String) Console.WriteLine(s1 + s2))

    <Category(Lambdas)>
    Friend NoParametersNonVoidReturn As Expression = Expr(Function() "abcd")

    <Category(Lambdas)>
    Friend OneParameterNonVoidReturn As Expression = Expr(Function(s As String) s)

    <Category(Lambdas)>
    Friend TwoParametersNonVoidReturn As Expression = Expr(Function(s1 As String, s2 As String) s1 + s2)
End Module