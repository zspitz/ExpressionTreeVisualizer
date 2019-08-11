using System.Linq.Expressions;
using Xunit;
using static ExpressionToString.Tests.Globals;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        [Trait("Category", SwitchCases)]
        public void SingleValueSwitchCase() => PreRunTest();

        [Fact]
        [Trait("Category", SwitchCases)]
        public void MultiValueSwitchCase() => PreRunTest();

        [Fact]
        [Trait("Category", SwitchCases)]
        public void SingleValueSwitchCase1() => PreRunTest();

        [Fact]
        [Trait("Category", SwitchCases)]
        public void MultiValueSwitchCase1() => PreRunTest();

        [Fact]
        [Trait("Category", SwitchCases)]
        public void SwitchOnExpressionWithDefaultSingleStatement() => PreRunTest();

        [Fact]
        [Trait("Category", SwitchCases)]
        public void SwitchOnExpressionWithDefaultMultiStatement() => PreRunTest();

        [Fact]
        [Trait("Category", SwitchCases)]
        public void SwitchOnMultipleStatementsWithDefault() => PreRunTest();

        [Fact]
        [Trait("Category", SwitchCases)]
        public void SwitchOnExpressionWithoutDefault() => PreRunTest();

        [Fact]
        [Trait("Category", SwitchCases)]
        public void SwitchOnMultipleStatementsWithoutDefault() => PreRunTest();
    }
}
