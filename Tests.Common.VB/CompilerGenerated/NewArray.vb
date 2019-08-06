Partial Public Class VBCompilerGeneratedBase
    <Fact> <Trait("Category", NewArray)>
    Sub SingleDimensionInit()
        RunTest(
            Function() New String() {""},
            "() => new[] { """" }",
            "Function() { """" }",
            "Lambda(
    NewArrayInit(
        typeof(string),
        Constant("""")
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", NewArray)>
    Sub SingleDimensionInitExplicitType()
        RunTest(
            Function() New Object() {""},
            "() => new[] { """" }",
            "Function() { """" }",
            "Lambda(
    NewArrayInit(
        typeof(object),
        Convert(
            Constant(""""),
            typeof(object)
        )
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", NewArray)>
    Sub SingleDimensionWithBounds()
        RunTest(
            Function() New String(4) {},
            "() => new string[5]",
            "Function() New String(4) {}",
            "Lambda(
    NewArrayBounds(
        typeof(string),
        Constant(5)
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", NewArray)>
    Sub MultidimensionWithBounds()
        RunTest(
            Function() New String(1, 2) {},
            "() => new string[2, 3]",
            "Function() New String(1, 2) {}",
            "Lambda(
    NewArrayBounds(
        typeof(string),
        Constant(2),
        Constant(3)
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", NewArray)>
    Sub JaggedWithElementsImplicitType()
        RunTest(
            Function() {
                ({"ab", "cd"}),
                ({"ef", "gh"})
            },
            "() => new string[][] { new[] { ""ab"", ""cd"" }, new[] { ""ef"", ""gh"" } }",
            "Function() { ({ ""ab"", ""cd"" }), ({ ""ef"", ""gh"" }) }",
            "Lambda(
    NewArrayInit(
        typeof(string[]),
        NewArrayInit(
            typeof(string),
            Constant(""ab""),
            Constant(""cd"")
        ),
        NewArrayInit(
            typeof(string),
            Constant(""ef""),
            Constant(""gh"")
        )
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", NewArray)>
    Sub JaggedWithElementsImplicitTypeInnerNonLiteral()
        ' for jagged array literals, the inner literals need to be wrapped in parentheses
        ' we're checking that this is only done for literals
        Dim arr1 = New String() {"ab", "cd"}
        Dim arr2 = New String() {"ef", "gh"}
        RunTest(
            Function() {
                arr1,
                arr2
            },
            "() => new string[][] { arr1, arr2 }",
            "Function() { arr1, arr2 }",
            "Lambda(
    NewArrayInit(
        typeof(string[]),
        arr1, arr2
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", NewArray)>
    Sub JaggedWithElementsExplicitType()
        RunTest(
            Function() New Object()() {
                ({"ab", "cd"}),
                ({"ef", "gh"})
            },
            "() => new object[][] { new[] { ""ab"", ""cd"" }, new[] { ""ef"", ""gh"" } }",
            "Function() { { ""ab"", ""cd"" }, { ""ef"", ""gh"" } }",
            "Lambda(
    NewArrayInit(
        typeof(object[]),
        Convert(
            NewArrayInit(
                typeof(string),
                Constant(""ab""),
                Constant(""cd"")
            ),
            typeof(object[])
        ),
        Convert(
            NewArrayInit(
                typeof(string),
                Constant(""ef""),
                Constant(""gh"")
            ),
            typeof(object[])
        )
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", NewArray)>
    Sub JaggedWithBounds()
        RunTest(
            Function() New String(4)() {},
            "() => new string[5][]",
            "Function() New String(4)() {}",
            "Lambda(
    NewArrayBounds(
        typeof(string[]),
        Constant(5)
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", NewArray)>
    Sub ArrayOfMultidimensionalArray()
        RunTest(
            Function() New String(4)(,) {},
            "() => new string[5][,]",
            "Function() New String(4)(,) {}",
            "Lambda(
    NewArrayBounds(
        typeof(string[,]),
        Constant(5)
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", NewArray)>
    Sub MultidimensionalArrayOfArray()
        RunTest(
            Function() New String(2, 1)() {},
            "() => new string[3, 2][]",
            "Function() New String(2, 1)() {}",
            "Lambda(
    NewArrayBounds(
        typeof(string[]),
        Constant(3),
        Constant(2)
    )
)"
        )
    End Sub
End Class
