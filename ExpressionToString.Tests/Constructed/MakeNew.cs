using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Runners;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Util.Functions;

namespace ExpressionToString.Tests.Constructed
{
    [Trait("Source", CSharpCompiler)]
    public class MakeNew
    {
        Type fooType;
        PropertyInfo barProp;
        PropertyInfo bazProp;
        ConstructorInfo fooCtor1;
        MethodInfo add1 = typeof(List<string>).GetMethod("Add");
        MethodInfo add2 = GetMethod(() => (null as Wrapper).Add("", ""));

        public MakeNew() {
            fooType = typeof(Foo);
            barProp = fooType.GetProperty("Bar");
            bazProp = fooType.GetProperty("Baz");
            fooCtor1 = fooType.GetConstructor(new[] { typeof(string) });
        }

        [Fact]
        public void NamedType() => BuildAssert(
            New(typeof(Random)),
            "new Random()",
            "New Random"
        );

        [Fact]
        public void NamedTypeWithInitializer() => BuildAssert(
            MemberInit(
                New(fooType),
                Bind(barProp, Constant("abcd"))
            ),
            "new Foo() { Bar = \"abcd\" }",
            "New Foo With {.Bar = \"abcd\"}"
        );

        [Fact]
        public void NamedTypeWithInitializers() => BuildAssert(
            MemberInit(
                New(fooType),
                Bind(barProp, Constant("abcd")),
                Bind(bazProp, Constant("efgh"))
            ),
            "new Foo() { Bar = \"abcd\", Baz = \"efgh\" }",
            "New Foo With {.Bar = \"abcd\", .Baz = \"efgh\"}"
        );

        [Fact]
        public void NamedTypeConstructorParameters() => BuildAssert(
            New(fooCtor1, Constant("ijkl")),
            "new Foo(\"ijkl\")",
            "New Foo(\"ijkl\")"
        );

        [Fact]
        public void NamedTypeConstructorParametersWithInitializers() => BuildAssert(
            MemberInit(
                New(fooCtor1, Constant("ijkl")),
                Bind(barProp, Constant("abcd")),
                Bind(bazProp, Constant("efgh"))
            ),
            "new Foo(\"ijkl\") { Bar = \"abcd\", Baz = \"efgh\" }",
            "New Foo(\"ijkl\") With {.Bar = \"abcd\", .Baz = \"efgh\"}"
        );

        [Fact]
        public void CollectionTypeWithInitializer() => BuildAssert(
            ListInit(
                New(typeof(List<string>)), 
                ElementInit(add1, Constant("abcd")), 
                ElementInit(add1, Constant("efgh"))
            ),
            "new List<string>() { \"abcd\", \"efgh\" }",
            "New List(Of String) From {\"abcd\", \"efgh\"}"
        );

        [Fact]
        public void CollectionTypeWithMultipleElementsInitializers() => BuildAssert(
            ListInit(
                New(typeof(Wrapper)),
                ElementInit(add2, Constant("ab"), Constant("cd")),
                ElementInit(add2, Constant("ef"), Constant("gh"))
            ),
            "new Wrapper() { { \"ab\", \"cd\" }, { \"ef\", \"gh\" } }",
            "New Wrapper From {{\"ab\", \"cd\"}, {\"ef\", \"gh\"}}"
        );

        [Fact]
        public void CollectionTypeWithSingleOrMultipleElementsInitializers() => BuildAssert(
            ListInit(
                New(typeof(Wrapper)),
                ElementInit(add2, Constant("ab"), Constant("cd")),
                ElementInit(add1, Constant("ef"))
            ),
            "new Wrapper() { { \"ab\", \"cd\" }, \"ef\" }",
            "New Wrapper From {{\"ab\", \"cd\"}, \"ef\"}"
        );
    }
}
