using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        [Trait("Category", Constants)]
        public void Random() => PreRunTest();

        [Fact]
        [Trait("Category", Constants)]
        public void ValueTuple() => PreRunTest();

        [Fact]
        [Trait("Category", Constants)]
        public void OldTuple() => PreRunTest();

        [Fact]
        [Trait("Category", Constants)]
        public void Array() => PreRunTest();

        [Fact]
        [Trait("Category", Constants)]
        public void Type() => PreRunTest();

        [Fact]
        [Trait("Category", Constants)]
        public void DifferentTypeForNodeAndValue() => PreRunTest();
    }
}
