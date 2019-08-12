Imports ExpressionToString.Tests.Functions

Namespace Objects
    Partial Public Module VBCompiler
        <Category(Lambdas)>
        Public NoParametersVoidReturn As Expression = Expr(Sub() Console.WriteLine())

        <Category(Lambdas)>
        Public OneParameterVoidReturn As Expression = Expr(Sub(s As String) Console.WriteLine(s))

        <Category(Lambdas)>
        Public TwoParametersVoidReturn As Expression = Expr(Sub(s1 As String, s2 As String) Console.WriteLine(s1 + s2))

        <Category(Lambdas)>
        Public NoParametersNonVoidReturn As Expression = Expr(Function() "abcd")

        <Category(Lambdas)>
        Public OneParameterNonVoidReturn As Expression = Expr(Function(s As String) s)

        <Category(Lambdas)>
        Public TwoParametersNonVoidReturn As Expression = Expr(Function(s1 As String, s2 As String) s1 + s2)
    End Module
End Namespace