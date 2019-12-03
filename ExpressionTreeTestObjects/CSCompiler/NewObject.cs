using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static ExpressionTreeTestObjects.Functions;
using static ExpressionTreeTestObjects.Categories;

namespace ExpressionTreeTestObjects {
    internal class Foo {
        internal string Bar { get; set; }
        internal string Baz { get; set; }
        internal Foo() { }
        internal Foo(string baz) { }
    }

    internal class Wrapper : List<string> {
        internal void Add(string s1, string s2) => throw new NotImplementedException();
    }

    // class used for MemberMemberBinding and ListBinding
    internal class Node {
        internal NodeData Data { get; set; } = new NodeData();
        internal IList<Node> Children { get; set; } = new List<Node>() { null };
    }

    internal class NodeData {
        internal string Name { get; set; }
    }

    partial class CSCompiler {

        [Category(NewObject)]
        internal static readonly Expression NamedType = Expr(() => new Random());

        [Category(NewObject)]
        internal static readonly Expression NamedTypeWithInitializer = Expr(() => new Foo { Bar = "abcd" });

        [Category(NewObject)]
        internal static readonly Expression NamedTypeWithInitializers = Expr(() => new Foo { Bar = "abcd", Baz = "efgh" });

        [Category(NewObject)]
        internal static readonly Expression NamedTypeConstructorParameters = Expr(() => new Foo("ijkl"));

        [Category(NewObject)]
        internal static readonly Expression NamedTypeConstructorParametersWithInitializers = Expr(() => new Foo("ijkl") { Bar = "abcd", Baz = "efgh" });

        [Category(NewObject)]
        internal static readonly Expression AnonymousType = Expr(() => new { Bar = "abcd", Baz = "efgh" });

        [Category(NewObject)]
        internal static readonly Expression AnonymousTypeFromVariables = IIFE(() => {
            var Bar = "abcd";
            var Baz = "efgh";
            return Expr(() => new { Bar, Baz });
        });

        [Category(NewObject)]
        internal static readonly Expression CollectionTypeWithInitializer = Expr(() => new List<string> { "abcd", "efgh" });

        [Category(NewObject)]
        internal static readonly Expression CollectionTypeWithMultipleElementsInitializers = Expr(() => new Wrapper { { "ab", "cd" }, { "ef", "gh" } });

        [Category(NewObject)]
        internal static readonly Expression CollectionTypeWithSingleOrMultipleElementsInitializers = Expr(() => new Wrapper { { "ab", "cd" }, "ef" });

        [Category(NewObject)]
        internal static readonly Expression MemberMemberBinding = Expr(() => new Node { Data = { Name = "abcd" } });

        [Category(NewObject)]
        internal static readonly Expression ListBinding = Expr(() => new Node { Children = { new Node(), new Node() } });
    }
}