using System;
using Xunit;
using static ExpressionTreeTransform.Tests.Runners;
using static System.Linq.Expressions.Expression;
using static ExpressionTreeTransform.Tests.Globals;

namespace ExpressionTreeTransform.Tests.Constructed {
    [Trait("Source", FactoryMethods)]
    public class Constants {
        [Fact]
        public void Random() => BuildAssert(Constant(new Random()), "#Random", "#Random");

        [Fact]
        public void ValueTuple() => BuildAssert(
            Constant(("abcd", 5)), 
            "(\"abcd\", 5)",
            "(\"abcd\", 5)"
        );

        [Fact]
        public void OldTuple() => BuildAssert(
            Constant(Tuple.Create("abcd", 5)),
            "(\"abcd\", 5)",
            "(\"abcd\", 5)"
        );
    }
}
