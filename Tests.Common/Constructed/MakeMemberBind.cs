using Xunit;
using static ExpressionToString.Util.Functions;
using System.Collections.Generic;
using System;
using System.Reflection;
using static ExpressionToString.Tests.Categories;
using ExpressionToString.Tests.Objects;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        [Trait("Category", MemberBindings)]
        public void MakeMemberBind() => PreRunTest();

        [Fact]
        [Trait("Category", MemberBindings)]
        public void MakeElementInit() => PreRunTest();

        [Fact]
        [Trait("Category", MemberBindings)]
        public void MakeElementInit2Arguments() => PreRunTest();

        [Fact]
        [Trait("Category", MemberBindings)]
        public void MakeMemberMemberBind() => PreRunTest();

        static readonly MethodInfo addMethod = GetMethod(() => ((IList<Node>)null).Add(new Node()));
        static readonly ConstructorInfo nodeConstructor = typeof(Node).GetConstructor(new Type[] { });

        [Fact]
        [Trait("Category", MemberBindings)]
        public void MakeListBinding() => PreRunTest();
    }
}
