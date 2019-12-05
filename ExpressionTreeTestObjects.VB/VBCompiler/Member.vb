Partial Module VBCompiler

    <Category(Member)>
    Friend InstanceMember As Expression = IIFE(Function()
                                                   Dim s = ""
                                                   Return Expr(Function() s.Length)
                                               End Function)

    <Category(Member)>
    Friend ClosedVariable As Expression = IIFE(Function()
                                                   Dim s = ""
                                                   Return Expr(Function() s)
                                               End Function)

    <Category(Member)>
    Friend StaticMember As Expression = Expr(Function() String.Empty)
End Module