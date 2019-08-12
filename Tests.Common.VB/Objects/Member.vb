Imports ExpressionToString.Tests.Functions

Namespace Objects
    Partial Public Module VBCompiler

        <Category(Member)>
        Public InstanceMember As Expression = IIFE(Function()
                                                       Dim s = ""
                                                       Return Expr(Function() s.Length)
                                                   End Function)

        <Category(Member)>
        Public ClosedVariable As Expression = IIFE(Function()
                                                       Dim s = ""
                                                       Return Expr(Function() s)
                                                   End Function)

        <Category(Member)>
        Public StaticMember As Expression = Expr(Function() String.Empty)
    End Module
End Namespace