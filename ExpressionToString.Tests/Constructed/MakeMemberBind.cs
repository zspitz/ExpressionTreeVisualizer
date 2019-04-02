using System.Linq;
using Xunit;
using static ExpressionToString.Tests.Runners;
using static ExpressionToString.Tests.Globals;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Util.Functions;
using System.Collections.Generic;
using static System.Reflection.BindingFlags;
using System.Linq.Expressions;
using System;
using System.Reflection;

namespace ExpressionToString.Tests.Constructed {
    internal class DummyMember {
        internal string Foo { get; set; }
    }

    [Trait("Source", FactoryMethods)]
    public class MemberBind {
        [Fact]
        public void MakeMemberBind() => BuildAssert(
            Bind(
                typeof(DummyMember).GetMember("Foo", Instance | NonPublic).Single(), Constant("abcd")
            ),
            "Foo = \"abcd\"",
            ".Foo = \"abcd\""
        );

        [Fact]
        public void MakeElementInit() => BuildAssert(
            ElementInit(
                GetMethod(() => ((List<string>)null).Add("")),
                Constant("abcd")
            ),
            "\"abcd\"",
            "\"abcd\""
        );

        [Fact]
        public void MakeElementInit2Arguments() => BuildAssert(
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
        public void MakeMemberMemberBind() => BuildAssert(
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
            @".Data = {
    .Name = ""abcd""
}"
        );

        static readonly MethodInfo addMethod = GetMethod(() => ((IList<Node>)null).Add(new Node()));
        static readonly ConstructorInfo nodeConstructor = typeof(Node).GetConstructor(new Type[] { });

        [Fact]
        public void MakeListBinding() => BuildAssert(
            ListBind(
                GetMember(() => ((Node)null).Children),
                ElementInit(addMethod, Expression.New(nodeConstructor)),
                ElementInit(addMethod, Expression.New(nodeConstructor))
            ),
            @"Children = {
    new Node(),
    new Node()
}",
            @".Children = {
    New Node,
    New Node
}"
        );
    }
}
