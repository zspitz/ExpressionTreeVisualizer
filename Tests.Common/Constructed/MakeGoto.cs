using System.Linq.Expressions;
using Xunit;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        [Trait("Category", Gotos)]
        public void MakeBreak() => PreRunTest();

        [Fact]
        [Trait("Category", Gotos)]
        public void MakeBreakWithValue() => PreRunTest();

        [Fact]
        [Trait("Category", Gotos)]
        public void MakeContinue() => PreRunTest();

        [Fact]
        [Trait("Category", Gotos)]
        public void MakeGotoWithoutValue() => PreRunTest();

        [Fact]
        [Trait("Category", Gotos)]
        public void MakeGotoWithValue() => PreRunTest();

        [Fact]
        [Trait("Category", Gotos)]
        public void MakeReturn() => PreRunTest();

        [Fact]
        [Trait("Category", Gotos)]
        public void MakeReturnWithValue() => PreRunTest();
    }
}
