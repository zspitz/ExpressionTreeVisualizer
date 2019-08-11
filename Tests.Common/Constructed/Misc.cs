using System.Collections;
using System.Linq.Expressions;
using Xunit;
using static ExpressionToString.Tests.Globals;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        [Trait("Category", TypeCheck)]
        public void MakeTypeCheck() => PreRunTest();

        [Fact]
        [Trait("Category", TypeCheck)]
        public void MakeTypeEqual() => PreRunTest();

        [Fact]
        [Trait("Category", Invocation)]
        public void MakeInvocation() => PreRunTest();

        [Fact]
        [Trait("Category", Lambdas)]
        public void MakeByRefParameter() => PreRunTest();

        [Fact]
        [Trait("Category", Quoted)]
        public void MakeQuoted() => PreRunTest();

        [Fact]
        [Trait("Category", Quoted)]
        public void MakeQuoted1() => PreRunTest();

        [Fact]
        [Trait("Category", DebugInfos)]
        public void MakeDebugInfo() => PreRunTest();

        [Fact]
        [Trait("Category", DebugInfos)]
        public void MakeClearDebugInfo() => PreRunTest();
    }
}
