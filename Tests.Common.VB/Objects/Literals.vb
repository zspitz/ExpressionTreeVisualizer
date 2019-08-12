Imports ExpressionToString.Tests.Functions
Imports System.Linq.Expressions.Expression

Namespace Objects
    Partial Public Module VBCompiler
        <Category(Literal)>
        Public [True] As Expression = Expr(Function() True)

        <Category(Literal)>
        Public [False] As Expression = Expr(Function() False)

        <Category(Literal)>
        Public NothingString As Expression = Expr(Function() CType(Nothing, String))

        <Category(Literal)>
        Public [Nothing] As Expression = Expr(Function() Nothing)

        <Category(Literal)>
        Public [Integer] As Expression = Expr(Function() 5)

        <Category(Literal)>
        Public NonInteger As Expression = Expr(Function() 7.32)

        <Category(Literal)>
        Public [String] As Expression = Expr(Function() "abcd")

        <Category(Literal)>
        Public TimeSpan As Expression = Constant(New TimeSpan(5, 4, 3, 2, 1))

        <Category(Literal)>
        Public EscapedString As Expression = Expr(Function() """")

        <Category(Literal)>
        Public ConstantNothingToObject As Expression = Expr(Function() Nothing)

        <Category(Literal)>
        Public ConstantNothingToReferenceType As Expression = Expr(Of String)(Function() Nothing)

        <Category(Literal)>
        Public ConstantNothingToValueType As Expression = Expr(Of Integer)(Function() Nothing)
    End Module
End Namespace