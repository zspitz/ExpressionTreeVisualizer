using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static ExpressionToString.Tests.Functions;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests.Objects {
    class Foo {
        public string Bar { get; set; }
        public string Baz { get; set; }
        public Foo() { }
        public Foo(string baz) { }
    }

    class Wrapper : List<string> {
        public void Add(string s1, string s2) => throw new NotImplementedException();
    }

    // class used for MemberMemberBinding and ListBinding
    class Node {
        public NodeData Data { get; set; } = new NodeData();
        public IList<Node> Children { get; set; } = new List<Node>() { null };
    }

    class NodeData {
        public string Name { get; set; }
    }

    partial class CSCompiler {

        [Category(NewObject)]
        public static readonly Expression NamedType = Expr(() => new Random());

        [Category(NewObject)]
        public static readonly Expression NamedTypeWithInitializer = Expr(() => new Foo { Bar = "abcd" });

        [Category(NewObject)]
        public static readonly Expression NamedTypeWithInitializers = Expr(() => new Foo { Bar = "abcd", Baz = "efgh" });

        [Category(NewObject)]
        public static readonly Expression NamedTypeConstructorParameters = Expr(() => new Foo("ijkl"));

        [Category(NewObject)]
        public static readonly Expression NamedTypeConstructorParametersWithInitializers = Expr(() => new Foo("ijkl") { Bar = "abcd", Baz = "efgh" });

        [Category(NewObject)]
        public static readonly Expression AnonymousType = Expr(() => new { Bar = "abcd", Baz = "efgh" });

        [Category(NewObject)]
        public static readonly Expression AnonymousTypeFromVariables = IIFE(() => {
            var Bar = "abcd";
            var Baz = "efgh";
            return Expr(() => new { Bar, Baz });
        });

        [Category(NewObject)]
        public static readonly Expression CollectionTypeWithInitializer = Expr(() => new List<string> { "abcd", "efgh" });

        [Category(NewObject)]
        public static readonly Expression CollectionTypeWithMultipleElementsInitializers = Expr(() => new Wrapper { { "ab", "cd" }, { "ef", "gh" } });

        [Category(NewObject)]
        public static readonly Expression CollectionTypeWithSingleOrMultipleElementsInitializers = Expr(() => new Wrapper { { "ab", "cd" }, "ef" });

        [Category(NewObject)]
        public static readonly Expression MemberMemberBinding = Expr(() => new Node { Data = { Name = "abcd" } });

        [Category(NewObject)]
        public static readonly Expression ListBinding = Expr(() => new Node { Children = { new Node(), new Node() } });
    }
}