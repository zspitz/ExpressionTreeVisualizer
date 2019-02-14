Public Class NewArray
    <Fact>
    Sub SingleDimensionInit()
        BuildAssert(
            Function() New String() {""},
            "() => new [] { """" }",
            "Function() { """" }"
        )
    End Sub

    <Fact>
    Sub SingleDimensionInitExplicitType()
        BuildAssert(
            Function() New Object() {""}, ' the VB.NET compiler adds a conversion for each element to the array type
            "() => new [] { (object)"""" }",
            "Function() { CObj("""") }"
        )
    End Sub

    <Fact>
    Sub SingleDimensionWithBounds()
        BuildAssert(
            Function() New String(4) {},
            "() => new string[5]",
            "Function() New String(4) {}"
        )
    End Sub

    <Fact>
    Sub MultidimensionWithBounds()
        BuildAssert(
            Function() New String(1, 2) {},
            "() => new string[2, 3]",
            "Function() New String(1, 2) {}"
        )
    End Sub

    <Fact>
    Sub JaggedWithElementsImplicitType()
        BuildAssert(
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
        BuildAssert(
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
        BuildAssert(
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
        BuildAssert(
            Function() New String(4)() {},
            "() => new string[5][]",
            "Function() New String(4)() {}"
        )
    End Sub

    <Fact>
    Sub ArrayOfMultidimensionalArray()
        BuildAssert(
            Function() New String(4)(,) {},
            "() => new string[5][,]",
            "Function() New String(4)(,) {}"
        )
    End Sub

    <Fact>
    Sub MultidimensionalArrayOfArray()
        BuildAssert(
            Function() New String(2, 1)() {},
            "() => new string[3, 2][]",
            "Function() New String(2, 1)() {}"
        )
    End Sub
End Class
