using System;
using Xunit;
using static ExpressionToString.Tests.Globals;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        [Trait("Category",Unary)]
        public void ConstructArrayLength() => PreRunTest();

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructConvert() => PreRunTest();

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructConvertChecked() => PreRunTest();

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructConvertCheckedForReferenceType() => PreRunTest();

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructNegate() => PreRunTest();

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructBitwiseNot() => PreRunTest();

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructLogicalNot() => PreRunTest();

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructTypeAs() => PreRunTest();

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructPostDecrementAssign() => PreRunTest();

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructPostIncrementAssign() => PreRunTest();

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructPreDecrementAssign() => PreRunTest();

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructPreIncrementAssign() => PreRunTest();

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructIsTrue() => PreRunTest();

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructIsFalse() => PreRunTest();

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructIncrement() => PreRunTest();

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructDecrement() => PreRunTest();

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructThrow() => PreRunTest();

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructRethrow() => PreRunTest();
    }
}
