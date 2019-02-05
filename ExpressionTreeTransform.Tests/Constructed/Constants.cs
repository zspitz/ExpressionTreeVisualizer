using System;
using Xunit;
using static ExpressionTreeTransform.Tests.Runners;
using static System.Linq.Expressions.Expression;
using static ExpressionTreeTransform.Util.Globals;

namespace ExpressionTreeTransform.Tests {
    [Trait("Source", FactoryMethods)]
    public class Constants {
        [Fact]
        public void Random() => BuildAssert(Constant(new Random()), "#Random", "#Random");
    }
}
