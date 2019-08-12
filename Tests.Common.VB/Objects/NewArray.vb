Imports ExpressionToString.Tests.Functions

Namespace Objects
    Partial Public Module VBCompiler

        <Category(NewArray)>
        Public SingleDimensionInit As Expression = Expr(Function() New String() {""})

        <Category(NewArray)>
        Public SingleDimensionInitExplicitType As Expression = Expr(Function() New Object() {""})

        <Category(NewArray)>
        Public SingleDimensionWithBounds As Expression = Expr(Function() New String(4) {})

        <Category(NewArray)>
        Public MultidimensionWithBounds As Expression = Expr(Function() New String(1, 2) {})

        <Category(NewArray)>
        Public JaggedWithElementsImplicitType As Expression = Expr(Function() {
            ({"ab", "cd"}),
            ({"ef", "gh"})
        })

        ' for jagged array literals, the inner literals need to be wrapped in parentheses
        ' we're checking that this is only done for literals
        <Category(NewArray)>
        Public JaggedWithElementsImplicitTypeInnerNonLiteral As Expression = IIFE(Function()
                                                                                      Dim arr1 = New String() {"ab", "cd"}
                                                                                      Dim arr2 = New String() {"ef", "gh"}
                                                                                      Return Expr(Function() {
                                                                                                      arr1,
                                                                                                      arr2
                                                                                        })
                                                                                  End Function)

        <Category(NewArray)>
        Public JaggedWithElementsExplicitType As Expression = Expr(Function() New Object()() {
            ({"ab", "cd"}),
            ({"ef", "gh"})
        })

        <Category(NewArray)>
        Public JaggedWithBounds As Expression = Expr(Function() New String(4)() {})

        <Category(NewArray)>
        Public ArrayOfMultidimensionalArray As Expression = Expr(Function() New String(4)(,) {})

        <Category(NewArray)>
        Public MultidimensionalArrayOfArray As Expression = Expr(Function() New String(2, 1)() {})
    End Module
End Namespace