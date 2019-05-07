using System.Linq;
using Xunit;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Util.Functions;
using System.Collections.Generic;
using static System.Reflection.BindingFlags;
using System.Linq.Expressions;
using System;
using System.Reflection;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    internal class DummyMember {
        internal string Foo { get; set; }
    }

    public partial class ConstructedBase {
        [Fact]
        [Trait("Category",MemberBindings)]
        public void MakeMemberBind() => RunTest(
            Bind(
                typeof(DummyMember).GetMember("Foo", Instance | NonPublic).Single(), Constant("abcd")
            ),
            "Foo = \"abcd\"",
            ".Foo = \"abcd\""
        );

        [Fact]
        [Trait("Category", MemberBindings)]
        public void MakeElementInit() => RunTest(
            ElementInit(
                GetMethod(() => ((List<string>)null).Add("")),
                Constant("abcd")
            ),
            "\"abcd\"",
            "\"abcd\""
        );

        [Fact]
        [Trait("Category", MemberBindings)]
        public void MakeElementInit2Arguments() => RunTest(
            ElementInit(
                GetMethod(() => ((Wrapper)null).Add("", "")),
                Constant("abcd"),
                Constant("efgh")
            ),
            @"{
    ""abcd"",
    ""efgh""
}",
            @"{
    ""abcd"",
    ""efgh""
}"
        );

        [Fact]
        [Trait("Category", MemberBindings)]
        public void MakeMemberMemberBind() => RunTest(
            Expression.MemberBind(
                GetMember(() => ((Node)null).Data),
                Bind(
                    GetMember(() => ((NodeData)null).Name),
                    Constant("abcd")
                )
            ),
            @"Data = {
    Name = ""abcd""
}",
            @".Data = With {
    .Name = ""abcd""
}"
        );

        static readonly MethodInfo addMethod = GetMethod(() => ((IList<Node>)null).Add(new Node()));
        static readonly ConstructorInfo nodeConstructor = typeof(Node).GetConstructor(new Type[] { });

        [Fact]
        [Trait("Category", MemberBindings)]
        public void MakeListBinding() => RunTest(
            ListBind(
                GetMember(() => ((Node)null).Children),
                ElementInit(addMethod, New(nodeConstructor)),
                ElementInit(addMethod, New(nodeConstructor))
            ),
            @"Children = {
    new Node(),
    new Node()
}",
            @".Children = From {
    New Node,
    New Node
}"
        );
    }
}
