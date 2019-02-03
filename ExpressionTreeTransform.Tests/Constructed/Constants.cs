using System;
using Xunit;
using static ExpressionTreeTransform.Tests.Runners;
using static System.Linq.Expressions.Expression;

namespace ExpressionTreeTransform.Tests {
    public class Constants {
        [Fact]
        public void Random() => BuildAssert(Constant(new Random()), "#Random", "#Random");
    }
}
