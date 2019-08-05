using Xunit;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        [Trait("Category", Binary)]
        public void ConstructAdd() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructAddChecked() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructDivide() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructModulo() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructMultiply() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructMultiplyChecked() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructSubtract() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructSubtractChecked() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructAndBitwise() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructOrBitwise() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructExclusiveOrBitwise() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructAndLogical() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructOrLogical() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructExclusiveOrLogical() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructAndAlso() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructOrElse() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructEqual() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructNotEqual() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructReferenceEqual() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructReferenceNotEqual() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructGreaterThanOrEqual() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructGreaterThan() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructLessThan() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructLessThanOrEqual() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructCoalesce() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructLeftShift() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructRightShift() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructPower() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructArrayIndex() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructAssign() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructAddAssign() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructAddAssignChecked() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructAndAssign() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructDivideAssign() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructExclusiveOrAssign() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructLeftShiftAssign() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructModuloAssign() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructMultiplyAssign() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructMultiplyAssignChecked() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructOrAssign() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructPowerAssign() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructRightShiftAssign() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructSubtractAssign() => PreRunTest();

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructSubtractAssignChecked() => PreRunTest();
    }
}
