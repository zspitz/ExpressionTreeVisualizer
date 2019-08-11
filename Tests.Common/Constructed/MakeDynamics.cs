using System;
using Xunit;
using Microsoft.CSharp.RuntimeBinder;
using static Microsoft.CSharp.RuntimeBinder.Binder;
using static System.Linq.Expressions.Expression;
using System.Linq.Expressions;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        [Trait("Category", Dynamics)]
        public void ConstructGetIndex() => PreRunTest();

        [Fact]
        [Trait("Category", Dynamics)]
        public void ConstructGetIndexMultipleKeys() => PreRunTest();

        [Fact]
        [Trait("Category", Dynamics)]
        public void ConstructGetMember() => PreRunTest();

        [Fact]
        [Trait("Category", Dynamics)]
        public void ConstructInvocationNoArguments() => PreRunTest();

        [Fact]
        [Trait("Category", Dynamics)]
        public void ConstructInvocationWithArguments() => PreRunTest();

        [Fact]
        [Trait("Category", Dynamics)]
        public void ConstructMemberInvocationNoArguments() => PreRunTest();

        [Fact]
        [Trait("Category", Dynamics)]
        public void ConstructMemberInvocationWithArguments() => PreRunTest();

        [Fact]
        [Trait("Category", Dynamics)]
        public void ConstructSetIndex() => PreRunTest();

        [Fact]
        [Trait("Category", Dynamics)]
        public void ConstructSetIndexMultipleKeys() => PreRunTest();

        [Fact]
        [Trait("Category", Dynamics)]
        public void ConstructSetMember() => PreRunTest();
    }
}
