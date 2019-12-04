Partial Friend Module VBCompiler
    <Category(NewObject)>
    Friend NamedType As Expression = Expr(Function() New Random())

    <Category(NewObject)>
    Friend NamedTypeWithInitializer As Expression = Expr(Function() New Foo With {.Bar = "abcd"})

    <Category(NewObject)>
    Friend NamedTypeWithInitializers As Expression = Expr(Function() New Foo With {.Bar = "abcd", .Baz = "efgh"})

    <Category(NewObject)>
    Friend NamedTypeConstructorParameters As Expression = Expr(Function() New Foo("ijkl"))

    <Category(NewObject)>
    Friend NamedTypeConstructorParametersWithInitializers As Expression = Expr(Function() New Foo("ijkl") With {.Bar = "abcd", .Baz = "efgh"})

    <Category(NewObject)>
    Friend AnonymousType As Expression = Expr(Function() New With {.Bar = "abcd", .Baz = "efgh"})

    <Category(NewObject)>
    Friend AnonymousTypeFromVariables As Expression = IIFE(Function()
                                                               Dim Bar = "abcd"
                                                               Dim Baz = "efgh"
                                                               Return Expr(Function() New With {Bar, Baz})
                                                           End Function)

    <Category(NewObject)>
    Friend CollectionTypeWithInitializer As Expression = Expr(Function() New List(Of String) From {"abcd", "efgh"})

    <Category(NewObject)>
    Friend CollectionTypeWithMultipleElementsInitializers As Expression = Expr(Function() New Wrapper From {{"ab", "cd"}, {"ef", "gh"}})

    <Category(NewObject)>
    Friend CollectionTypeWithSingleOrMultipleElementsInitializers As Expression = Expr(Function() New Wrapper From {{"ab", "cd"}, "ef"})
End Module