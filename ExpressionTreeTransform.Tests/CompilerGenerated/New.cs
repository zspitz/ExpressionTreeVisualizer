using System;
using System.Collections.Generic;
using Xunit;
using static ExpressionTreeTransform.Tests.Globals;
using static ExpressionTreeTransform.Tests.Runners;

namespace ExpressionTreeTransform.Tests {
    class Foo {
        public string Bar { get; set; }
        public string Baz { get; set; }
        public Foo() { }
        public Foo(string baz) { }
    }

    class Wrapper : List<string> {
        public void Add(string s1, string s2) => throw new NotImplementedException();
    }

    [Trait("Source", CSharpCompiler)]
    public class New {
        [Fact]
        public void NamedType() => BuildAssert(
            () => new Random(),
            "() => new Random()",
            "Function() New Random"
        );

        [Fact]
        public void NamedTypeWithInitializer() => BuildAssert(
            () => new Foo { Bar = "abcd" },
            "() => new Foo() { Bar = \"abcd\" }",
            "Function() New Foo With {.Bar = \"abcd\"}"
        );

        [Fact]
        public void NamedTypeWithInitializers() => BuildAssert(
            () => new Foo { Bar = "abcd", Baz = "efgh" },
            "() => new Foo() { Bar = \"abcd\", Baz = \"efgh\" }",
            "Function() New Foo With {.Bar = \"abcd\", .Baz = \"efgh\"}"
        );

        [Fact]
        public void NamedTypeConstructorParameters() => BuildAssert(
            () => new Foo("ijkl"),
            "() => new Foo(\"ijkl\")",
            "Function() New Foo(\"ijkl\")"
        );

        [Fact]
        public void NamedTypeConstructorParametersWithInitializers() => BuildAssert(
            () => new Foo("ijkl") { Bar = "abcd", Baz = "efgh" },
            "() => new Foo(\"ijkl\") { Bar = \"abcd\", Baz = \"efgh\" }",
            "Function() New Foo(\"ijkl\") With {.Bar = \"abcd\", .Baz = \"efgh\"}"
        );

        [Fact]
        public void AnonymousType() => BuildAssert(
            () => new { Bar = "abcd", Baz = "efgh" },
            "() => new { Bar = \"abcd\", Baz = \"efgh\" }",
            "Function() New With {.Bar = \"abcd\", .Baz = \"efgh\"}"
        );

        [Fact]
        public void AnonymousTypeFromVariables() {
            var Bar = "abcd";
            var Baz = "efgh";
            BuildAssert(
                () => new { Bar, Baz },
                "() => new { Bar, Baz }",
                "Function() New With {Bar, Baz}"
            );
        }

        [Fact]
        public void CollectionTypeWithInitializer() => BuildAssert(
            () => new List<string> { "abcd", "efgh" },
            "() => new List<string>() { \"abcd\", \"efgh\" }",
            "Function() New List(Of String) From {\"abcd\", \"efgh\"}"
        );

        [Fact]
        public void CollectionTypeWithMultipleElementsInitializers() => BuildAssert(
            () => new Wrapper { { "ab", "cd" }, { "ef", "gh" } },
            "() => new Wrapper() { { \"ab\", \"cd\" }, { \"ef\", \"gh\" } }",
            "Function() New Wrapper From {{\"ab\", \"cd\"}, {\"ef\", \"gh\"}}"
        );

        [Fact]
        public void CollectionTypeWithSingleOrMultipleElementsInitializers() => BuildAssert(
            () => new Wrapper { { "ab", "cd" }, "ef" },
            "() => new Wrapper() { { \"ab\", \"cd\" }, \"ef\" }",
            "Function() New Wrapper From {{\"ab\", \"cd\"}, \"ef\"}"
        );
    }
}
