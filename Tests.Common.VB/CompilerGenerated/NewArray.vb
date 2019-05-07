Partial Public Class VBCompilerGeneratedBase
    <Fact>
    Sub SingleDimensionInit()
        RunTest(
            Function() New String() {""},
            "() => new [] { """" }",
            "Function() { """" }"
        )
    End Sub

    <Fact>
    Sub SingleDimensionInitExplicitType()
        RunTest(
            Function() New Object() {""}, ' the VB.NET compiler adds a conversion for each element to the array type
            "() => new [] { (object)"""" }",
            "Function() { CObj("""") }"
        )
    End Sub

    <Fact>
    Sub SingleDimensionWithBounds()
        RunTest(
            Function() New String(4) {},
            "() => new string[5]",
            "Function() New String(4) {}"
        )
    End Sub

    <Fact>
    Sub MultidimensionWithBounds()
        RunTest(
            Function() New String(1, 2) {},
            "() => new string[2, 3]",
            "Function() New String(1, 2) {}"
        )
    End Sub

    <Fact>
    Sub JaggedWithElementsImplicitType()
        RunTest(
            Function() {
                ({"ab", "cd"}),
                ({"ef", "gh"})
            },
            "() => new string[][] { new [] { ""ab"", ""cd"" }, new [] { ""ef"", ""gh"" } }",
            "Function() { ({ ""ab"", ""cd"" }), ({ ""ef"", ""gh"" }) }"
        )
    End Sub

    <Fact>
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
            "Function() { arr1, arr2 }"
        )
    End Sub

    <Fact>
    Sub JaggedWithElementsExplicitType()
        RunTest(
            Function() New Object()() {
                ({"ab", "cd"}),
                ({"ef", "gh"})
            },
            "() => new object[][] { (object[])new [] { ""ab"", ""cd"" }, (object[])new [] { ""ef"", ""gh"" } }",
            "Function() { CType({ ""ab"", ""cd"" }, Object()), CType({ ""ef"", ""gh"" }, Object()) }"
        )
    End Sub

    <Fact>
    Sub JaggedWithBounds()
        RunTest(
            Function() New String(4)() {},
            "() => new string[5][]",
            "Function() New String(4)() {}"
        )
    End Sub

    <Fact>
    Sub ArrayOfMultidimensionalArray()
        RunTest(
            Function() New String(4)(,) {},
            "() => new string[5][,]",
            "Function() New String(4)(,) {}"
        )
    End Sub

    <Fact>
    Sub MultidimensionalArrayOfArray()
        RunTest(
            Function() New String(2, 1)() {},
            "() => new string[3, 2][]",
            "Function() New String(2, 1)() {}"
        )
    End Sub
End Class
