Imports ExpressionToString.Tests.Functions

Namespace Objects
    Partial Public Module VBCompiler
        <Category(NewObject)>
        Public NamedType As Expression = Expr(Function() New Random())

        <Category(NewObject)>
        Public NamedTypeWithInitializer As Expression = Expr(Function() New Foo With {.Bar = "abcd"})

        <Category(NewObject)>
        Public NamedTypeWithInitializers As Expression = Expr(Function() New Foo With {.Bar = "abcd", .Baz = "efgh"})

        <Category(NewObject)>
        Public NamedTypeConstructorParameters As Expression = Expr(Function() New Foo("ijkl"))

        <Category(NewObject)>
        Public NamedTypeConstructorParametersWithInitializers As Expression = Expr(Function() New Foo("ijkl") With {.Bar = "abcd", .Baz = "efgh"})

        <Category(NewObject)>
        Public AnonymousType As Expression = Expr(Function() New With {.Bar = "abcd", .Baz = "efgh"})

        <Category(NewObject)>
        Public AnonymousTypeFromVariables As Expression = IIFE(Function()
                                                                   Dim Bar = "abcd"
                                                                   Dim Baz = "efgh"
                                                                   Return Expr(Function() New With {Bar, Baz})
                                                               End Function)

        <Category(NewObject)>
        Public CollectionTypeWithInitializer As Expression = Expr(Function() New List(Of String) From {"abcd", "efgh"})

        <Category(NewObject)>
        Public CollectionTypeWithMultipleElementsInitializers As Expression = Expr(Function() New Wrapper From {{"ab", "cd"}, {"ef", "gh"}})

        <Category(NewObject)>
        Public CollectionTypeWithSingleOrMultipleElementsInitializers As Expression = Expr(Function() New Wrapper From {{"ab", "cd"}, "ef"})
    End Module
End Namespace