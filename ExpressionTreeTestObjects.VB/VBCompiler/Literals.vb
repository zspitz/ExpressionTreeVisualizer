Partial Friend Module VBCompiler
    <Category(Literal)>
    Friend [True] As Expression = Expr(Function() True)

    <Category(Literal)>
    Friend [False] As Expression = Expr(Function() False)

    <Category(Literal)>
    Friend NothingString As Expression = Expr(Function() CType(Nothing, String))

    <Category(Literal)>
    Friend [Nothing] As Expression = Expr(Function() Nothing)

    <Category(Literal)>
    Friend [Integer] As Expression = Expr(Function() 5)

    <Category(Literal)>
    Friend NonInteger As Expression = Expr(Function() 7.32)

    <Category(Literal)>
    Friend [String] As Expression = Expr(Function() "abcd")

    <Category(Literal)>
    <Obsolete("This isn't a compiler-generated expression")>
    Friend TimeSpan As Expression = Expression.Constant(New TimeSpan(5, 4, 3, 2, 1))

    <Category(Literal)>
    Friend EscapedString As Expression = Expr(Function() """")

    <Category(Literal)>
    Friend ConstantNothingToObject As Expression = Expr(Function() Nothing)

    <Category(Literal)>
    Friend ConstantNothingToReferenceType As Expression = Expr(Of String)(Function() Nothing)

    <Category(Literal)>
    Friend ConstantNothingToValueType As Expression = Expr(Of Integer)(Function() Nothing)
End Module