using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static ExpressionTreeTestObjects.Functions;
using static ExpressionTreeTestObjects.Categories;
using static System.Linq.Expressions.Expression;
using System.Reflection;

namespace ExpressionTreeTestObjects {
    partial class FactoryMethods {
        static readonly Type fooType = typeof(Foo);
        static readonly PropertyInfo barProp = typeof(Foo).GetProperty("Bar");
        static readonly PropertyInfo bazProp = typeof(Foo).GetProperty("Baz");
        static readonly ConstructorInfo fooCtor1 = typeof(Foo).GetConstructor(new[] { typeof(string) });
        static readonly MethodInfo add1 = typeof(List<string>).GetMethod("Add");
        static readonly MethodInfo add2 = GetMethod(() => (null as Wrapper).Add("", ""));

        [Category(NewObject)]
        internal static readonly Expression NamedType = New(typeof(Random));

        [Category(NewObject)]
        internal static readonly Expression NamedTypeWithInitializer = MemberInit(
            New(fooType),
            Bind(barProp, Constant("abcd"))
        );

        [Category(NewObject)]
        internal static readonly Expression NamedTypeWithInitializers = MemberInit(
            New(fooType),
            Bind(barProp, Constant("abcd")),
            Bind(bazProp, Constant("efgh"))
        );

        [Category(NewObject)]
        internal static readonly Expression NamedTypeConstructorParameters = New(fooCtor1, Constant("ijkl"));

        [Category(NewObject)]
        internal static readonly Expression NamedTypeConstructorParametersWithInitializers = MemberInit(
            New(fooCtor1, Constant("ijkl")),
            Bind(barProp, Constant("abcd")),
            Bind(bazProp, Constant("efgh"))
        );

        [Category(NewObject)]
        internal static readonly Expression CollectionTypeWithInitializer = ListInit(
            New(typeof(List<string>)),
            ElementInit(add1, Constant("abcd")),
            ElementInit(add1, Constant("efgh"))
        );

        [Category(NewObject)]
        internal static readonly Expression CollectionTypeWithMultiElementInitializers = ListInit(
            New(typeof(Wrapper)),
            ElementInit(add2, Constant("ab"), Constant("cd")),
            ElementInit(add2, Constant("ef"), Constant("gh"))
        );

        [Category(NewObject)]
        internal static readonly Expression CollectionTypeWithSingleOrMultiElementInitializers = ListInit(
            New(typeof(Wrapper)),
            ElementInit(add2, Constant("ab"), Constant("cd")),
            ElementInit(add1, Constant("ef"))
        );
    }
}