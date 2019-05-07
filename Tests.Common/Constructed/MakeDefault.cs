using Xunit;
using static System.Linq.Expressions.Expression;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        public void MakeDefaultRefType() => RunTest(
            Default(typeof(string)),
            "default(string)",
            "CType(Nothing, String)"
        );

        [Fact]
        public void MakeDefaultValueType() => RunTest(
            Default(typeof(int)),
            "default(int)",
            "CType(Nothing, Integer)"
        );
    }
}
