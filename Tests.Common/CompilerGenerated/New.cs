using Xunit;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class CompilerGeneratedBase {
        [Fact]
        [Trait("Category", NewObject)]
        public void NamedType() => PreRunTest();

        [Fact]
        [Trait("Category", NewObject)]
        public void NamedTypeWithInitializer() => PreRunTest();

        [Fact]
        [Trait("Category", NewObject)]
        public void NamedTypeWithInitializers() => PreRunTest();

        [Fact]
        [Trait("Category", NewObject)]
        public void NamedTypeConstructorParameters() => PreRunTest();

        [Fact]
        [Trait("Category", NewObject)]
        public void NamedTypeConstructorParametersWithInitializers() => PreRunTest();

        [Fact]
        [Trait("Category", NewObject)]
        public void AnonymousType() => PreRunTest();

        [Fact]
        [Trait("Category", NewObject)]
        public void AnonymousTypeFromVariables() => PreRunTest();

        [Fact]
        [Trait("Category", NewObject)]
        public void CollectionTypeWithInitializer() => PreRunTest();

        [Fact]
        [Trait("Category", NewObject)]
        public void CollectionTypeWithMultipleElementsInitializers() => PreRunTest();

        [Fact]
        [Trait("Category", NewObject)]
        public void CollectionTypeWithSingleOrMultipleElementsInitializers() => PreRunTest();

        [Fact]
        [Trait("Category", NewObject)]
        public void MemberMemberBinding() => PreRunTest();

        [Fact]
        [Trait("Category", NewObject)]
        public void ListBinding() => PreRunTest();
    }
}
