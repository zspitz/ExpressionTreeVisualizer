Friend Class Foo
    Public Property Bar As String
    Public Property Baz As String
    Public Sub New()
    End Sub
    Public Sub New(ByVal baz As String)
    End Sub
End Class

Friend Class Wrapper
    Inherits List(Of String)
    Public Sub Add(ByVal s1 As String)
        Throw New NotImplementedException
    End Sub
    Public Sub Add(ByVal s1 As String, ByVal s2 As String)
        Throw New NotImplementedException
    End Sub
    Public Sub Add(ByVal s1 As String, ByVal s2 As String, ByVal s3 As String)
        Throw New NotImplementedException
    End Sub
End Class

Friend Class Node
    Property Data As NodeData = New NodeData
    Property Children As IList(Of Node) = New List(Of Node)
End Class
Friend Class NodeData
    Property Name As String
End Class

Partial Public Class VBCompilerGeneratedBase
    <Fact> <Trait("Category", NewObject)>
    Public Sub NamedType()
        RunTest(
            Function() New Random(),
            "() => new Random()",
            "Function() New Random",
            "Lambda(
    New(
        typeof(Random).GetConstructor()
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", NewObject)>
    Public Sub NamedTypeWithInitializer()
        RunTest(
            Function() New Foo With {.Bar = "abcd"},
            "() => new Foo() {
    Bar = ""abcd""
}",
            "Function() New Foo With {
    .Bar = ""abcd""
}",
            "Lambda(
    MemberInit(
        New(
            typeof(Foo).GetConstructor()
        ),
        Bind(
            typeof(Foo).GetProperty(""Bar""),
            Constant(""abcd"")
        )
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", NewObject)>
    Public Sub NamedTypeWithInitializers()
        RunTest(
            Function() New Foo With {.Bar = "abcd", .Baz = "efgh"},
            "() => new Foo() {
    Bar = ""abcd"",
    Baz = ""efgh""
}",
            "Function() New Foo With {
    .Bar = ""abcd"",
    .Baz = ""efgh""
}",
            "Lambda(
    MemberInit(
        New(
            typeof(Foo).GetConstructor()
        ),
        Bind(
            typeof(Foo).GetProperty(""Bar""),
            Constant(""abcd"")
        ),
        Bind(
            typeof(Foo).GetProperty(""Baz""),
            Constant(""efgh"")
        )
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", NewObject)>
    Public Sub NamedTypeConstructorParameters()
        RunTest(
            Function() New Foo("ijkl"),
            "() => new Foo(""ijkl"")",
            "Function() New Foo(""ijkl"")",
            "Lambda(
    New(
        typeof(Foo).GetConstructor(),
        Constant(""ijkl"")
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", NewObject)>
    Public Sub NamedTypeConstructorParametersWithInitializers()
        RunTest(
            Function() New Foo("ijkl") With {.Bar = "abcd", .Baz = "efgh"},
            "() => new Foo(""ijkl"") {
    Bar = ""abcd"",
    Baz = ""efgh""
}",
            "Function() New Foo(""ijkl"") With {
    .Bar = ""abcd"",
    .Baz = ""efgh""
}",
            "Lambda(
    MemberInit(
        New(
            typeof(Foo).GetConstructor(),
            Constant(""ijkl"")
        ),
        Bind(
            typeof(Foo).GetProperty(""Bar""),
            Constant(""abcd"")
        ),
        Bind(
            typeof(Foo).GetProperty(""Baz""),
            Constant(""efgh"")
        )
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", NewObject)>
    Public Sub AnonymousType()
        RunTest(
            Function() New With {.Bar = "abcd", .Baz = "efgh"},
            "() => new {
    Bar = ""abcd"",
    Baz = ""efgh""
}",
            "Function() New With {
    .Bar = ""abcd"",
    .Baz = ""efgh""
}",
            "Lambda(
    New(
        typeof({ string Bar, string Baz }).GetConstructor(),
        Constant(""abcd""),
        Constant(""efgh"")
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", NewObject)>
    Public Sub AnonymousTypeFromVariables()
        Dim Bar = "abcd"
        Dim Baz = "efgh"
        RunTest(
            Function() New With {Bar, Baz},
            "() => new {
    Bar,
    Baz
}",
            "Function() New With {
    Bar,
    Baz
}",
            "Lambda(
    New(
        typeof({ string Bar, string Baz }).GetConstructor(),
        Bar, Baz
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", NewObject)>
    Public Sub CollectionTypeWithInitializer()
        RunTest(
            Function() New List(Of String) From {"abcd", "efgh"},
            "() => new List<string>() {
    ""abcd"",
    ""efgh""
}",
            "Function() New List(Of String) From {
    ""abcd"",
    ""efgh""
}",
            "Lambda(
    ListInit(
        New(
            typeof(List<string>).GetConstructor()
        ),
        new[] {
            ElementInit(
                typeof(List<string>).GetMethod(""Add""),
                new[] {
                    Constant(""abcd"")
                }
            ),
            ElementInit(
                typeof(List<string>).GetMethod(""Add""),
                new[] {
                    Constant(""efgh"")
                }
            )
        }
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", NewObject)>
    Public Sub CollectionTypeWithMultipleElementsInitializers()
        RunTest(
            Function() New Wrapper From {{"ab", "cd"}, {"ef", "gh"}},
            "() => new Wrapper() {
    {
        ""ab"",
        ""cd""
    },
    {
        ""ef"",
        ""gh""
    }
}",
            "Function() New Wrapper From {
    {
        ""ab"",
        ""cd""
    },
    {
        ""ef"",
        ""gh""
    }
}",
            "Lambda(
    ListInit(
        New(
            typeof(Wrapper).GetConstructor()
        ),
        new[] {
            ElementInit(
                typeof(Wrapper).GetMethod(""Add""),
                new[] {
                    Constant(""ab""),
                    Constant(""cd"")
                }
            ),
            ElementInit(
                typeof(Wrapper).GetMethod(""Add""),
                new[] {
                    Constant(""ef""),
                    Constant(""gh"")
                }
            )
        }
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", NewObject)>
    Public Sub CollectionTypeWithSingleOrMultipleElementsInitializers()
        RunTest(
            Function() New Wrapper From {{"ab", "cd"}, "ef"},
            "() => new Wrapper() {
    {
        ""ab"",
        ""cd""
    },
    ""ef""
}",
            "Function() New Wrapper From {
    {
        ""ab"",
        ""cd""
    },
    ""ef""
}",
            "Lambda(
    ListInit(
        New(
            typeof(Wrapper).GetConstructor()
        ),
        new[] {
            ElementInit(
                typeof(Wrapper).GetMethod(""Add""),
                new[] {
                    Constant(""ab""),
                    Constant(""cd"")
                }
            ),
            ElementInit(
                typeof(Wrapper).GetMethod(""Add""),
                new[] {
                    Constant(""ef"")
                }
            )
        }
    )
)"
        )
    End Sub
End Class
