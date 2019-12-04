Partial Friend Module VBCompiler

    <Category(NewArray)>
    Friend SingleDimensionInit As Expression = Expr(Function() New String() {""})

    <Category(NewArray)>
    Friend SingleDimensionInitExplicitType As Expression = Expr(Function() New Object() {""})

    <Category(NewArray)>
    Friend SingleDimensionWithBounds As Expression = Expr(Function() New String(4) {})

    <Category(NewArray)>
    Friend MultidimensionWithBounds As Expression = Expr(Function() New String(1, 2) {})

    <Category(NewArray)>
    Friend JaggedWithElementsImplicitType As Expression = Expr(Function() {
            ({"ab", "cd"}),
            ({"ef", "gh"})
        })

    ' for jagged array literals, the inner literals need to be wrapped in parentheses
    ' we're checking that this is only done for literals
    <Category(NewArray)>
    Friend JaggedWithElementsImplicitTypeInnerNonLiteral As Expression = IIFE(Function()
                                                                                  Dim arr1 = New String() {"ab", "cd"}
                                                                                  Dim arr2 = New String() {"ef", "gh"}
                                                                                  Return Expr(Function() {
                                                                                                      arr1,
                                                                                                      arr2
                                                                                        })
                                                                              End Function)

    <Category(NewArray)>
    Friend JaggedWithElementsExplicitType As Expression = Expr(Function() New Object()() {
            ({"ab", "cd"}),
            ({"ef", "gh"})
        })

    <Category(NewArray)>
    Friend JaggedWithBounds As Expression = Expr(Function() New String(4)() {})

    <Category(NewArray)>
    Friend ArrayOfMultidimensionalArray As Expression = Expr(Function() New String(4)(,) {})

    <Category(NewArray)>
    Friend MultidimensionalArrayOfArray As Expression = Expr(Function() New String(2, 1)() {})
End Module