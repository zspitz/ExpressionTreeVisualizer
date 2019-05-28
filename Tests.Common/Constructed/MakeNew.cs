using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;
using static ExpressionToString.Util.Functions;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        Type fooType;
        PropertyInfo barProp;
        PropertyInfo bazProp;
        ConstructorInfo fooCtor1;
        MethodInfo add1 = typeof(List<string>).GetMethod("Add");
        MethodInfo add2 = GetMethod(() => (null as Wrapper).Add("", ""));

        public ConstructedBase() {
            fooType = typeof(Foo);
            barProp = fooType.GetProperty("Bar");
            bazProp = fooType.GetProperty("Baz");
            fooCtor1 = fooType.GetConstructor(new[] { typeof(string) });
        }

        [Fact]
        [Trait("Category", NewObject)]
        public void NamedType() => RunTest(
            New(typeof(Random)),
            "new Random()",
            "New Random", 
            @"New(
    typeof(Random).GetConstructor()
)"
        );

        [Fact]
        [Trait("Category", NewObject)]
        public void NamedTypeWithInitializer() => RunTest(
            MemberInit(
                New(fooType),
                Bind(barProp, Constant("abcd"))
            ),
            @"new Foo() {
    Bar = ""abcd""
}",
            @"New Foo With {
    .Bar = ""abcd""
}",
            @"MemberInit(
    New(
        typeof(Foo).GetConstructor()
    ),
    new [] {
        Bind(
            typeof(Foo).GetProperty(""Bar""),
            Constant(""abcd"")
        )
    }
)"
        );

        [Fact]
        [Trait("Category", NewObject)]
        public void NamedTypeWithInitializers() => RunTest(
            MemberInit(
                New(fooType),
                Bind(barProp, Constant("abcd")),
                Bind(bazProp, Constant("efgh"))
            ),
            @"new Foo() {
    Bar = ""abcd"",
    Baz = ""efgh""
}",
            @"New Foo With {
    .Bar = ""abcd"",
    .Baz = ""efgh""
}",
            @"MemberInit(
    New(
        typeof(Foo).GetConstructor()
    ),
    new [] {
        Bind(
            typeof(Foo).GetProperty(""Bar""),
            Constant(""abcd"")
        ),
        Bind(
            typeof(Foo).GetProperty(""Baz""),
            Constant(""efgh"")
        )
    }
)"
        );

        [Fact]
        [Trait("Category", NewObject)]
        public void NamedTypeConstructorParameters() => RunTest(
            New(fooCtor1, Constant("ijkl")),
            @"new Foo(""ijkl"")",
            @"New Foo(""ijkl"")", 
            @"New(
    typeof(Foo).GetConstructor(),
    new [] {
        Constant(""ijkl"")
    }
)"
        );

        [Fact]
        [Trait("Category", NewObject)]
        public void NamedTypeConstructorParametersWithInitializers() => RunTest(
            MemberInit(
                New(fooCtor1, Constant("ijkl")),
                Bind(barProp, Constant("abcd")),
                Bind(bazProp, Constant("efgh"))
            ),
            @"new Foo(""ijkl"") {
    Bar = ""abcd"",
    Baz = ""efgh""
}",
            @"New Foo(""ijkl"") With {
    .Bar = ""abcd"",
    .Baz = ""efgh""
}", 
            @"MemberInit(
    New(
        typeof(Foo).GetConstructor(),
        new [] {
            Constant(""ijkl"")
        }
    ),
    new [] {
        Bind(
            typeof(Foo).GetProperty(""Bar""),
            Constant(""abcd"")
        ),
        Bind(
            typeof(Foo).GetProperty(""Baz""),
            Constant(""efgh"")
        )
    }
)"
        );

        [Fact]
        [Trait("Category", NewObject)]
        public void CollectionTypeWithInitializer() => RunTest(
            ListInit(
                New(typeof(List<string>)),
                ElementInit(add1, Constant("abcd")),
                ElementInit(add1, Constant("efgh"))
            ),
            @"new List<string>() {
    ""abcd"",
    ""efgh""
}",
            @"New List(Of String) From {
    ""abcd"",
    ""efgh""
}",
            @"ListInit(
    New(#RuntimeConstructorInfo, new [] {}),
    new [] {
        ElementInit(#RuntimeMethodInfo, new [] {
            Constant(""abcd"")
        }),
        ElementInit(#RuntimeMethodInfo, new [] {
            Constant(""efgh"")
        })
    }
)"
        );

        [Fact]
        [Trait("Category", NewObject)]
        public void CollectionTypeWithMultiElementInitializers() => RunTest(
            ListInit(
                New(typeof(Wrapper)),
                ElementInit(add2, Constant("ab"), Constant("cd")),
                ElementInit(add2, Constant("ef"), Constant("gh"))
            ),
            @"new Wrapper() {
    {
        ""ab"",
        ""cd""
    },
    {
        ""ef"",
        ""gh""
    }
}",
            @"New Wrapper From {
    {
        ""ab"",
        ""cd""
    },
    {
        ""ef"",
        ""gh""
    }
}",
            @"ListInit(
    New(#RuntimeConstructorInfo, new [] {}),
    new [] {
        ElementInit(#RuntimeMethodInfo, new [] {
            Constant(""ab""),
            Constant(""cd"")
        }),
        ElementInit(#RuntimeMethodInfo, new [] {
            Constant(""ef""),
            Constant(""gh"")
        })
    }
)"
        );

        [Fact]
        [Trait("Category", NewObject)]
        public void CollectionTypeWithSingleOrMultiElementInitializers() => RunTest(
            ListInit(
                New(typeof(Wrapper)),
                ElementInit(add2, Constant("ab"), Constant("cd")),
                ElementInit(add1, Constant("ef"))
            ),
            @"new Wrapper() {
    {
        ""ab"",
        ""cd""
    },
    ""ef""
}",
            @"New Wrapper From {
    {
        ""ab"",
        ""cd""
    },
    ""ef""
}",
            @"ListInit(
    New(#RuntimeConstructorInfo, new [] {}),
    new [] {
        ElementInit(#RuntimeMethodInfo, new [] {
            Constant(""ab""),
            Constant(""cd"")
        }),
        ElementInit(#RuntimeMethodInfo, new [] {
            Constant(""ef"")
        })
    }
)"
        );
    }
}
