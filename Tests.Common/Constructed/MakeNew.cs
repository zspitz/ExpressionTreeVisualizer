using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit;
using static ExpressionToString.Util.Functions;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Categories;
using ExpressionToString.Tests.Objects;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
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
        public void CollectionTypeWithInitializer() => PreRunTest();

        [Fact]
        [Trait("Category", NewObject)]
        public void CollectionTypeWithMultiElementInitializers() => PreRunTest();

        [Fact]
        [Trait("Category", NewObject)]
        public void CollectionTypeWithSingleOrMultiElementInitializers() => PreRunTest();
    }
}
