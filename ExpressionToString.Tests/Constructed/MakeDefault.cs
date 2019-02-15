using Xunit;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Runners;
using static System.Linq.Expressions.Expression;

namespace ExpressionToString.Tests.Constructed {
    [Trait("Source", FactoryMethods)]
    public class MakeDefault {
        [Fact]
        public void MakeDefaultRefType() => BuildAssert(
            Default(typeof(string)),
            "default(string)",
            "CType(Nothing, String)"
        );

        [Fact]
        public void MakeDefaultValueType() => BuildAssert(
            Default(typeof(int)),
            "default(int)",
            "CType(Nothing, Integer)"
        );
    }
}
