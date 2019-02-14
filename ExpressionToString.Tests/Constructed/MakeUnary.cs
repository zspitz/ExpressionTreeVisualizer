using Xunit;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Runners;
using static System.Linq.Expressions.Expression;

namespace ExpressionToString.Tests.Constructed {
    [Trait("Source", FactoryMethods)]
    public class MakeUnary {
        [Fact]
        public void ConstructArrayLength() => BuildAssert(ArrayLength(arr), "arr.Length", "arr.Length");

        [Fact]
        public void ConstructConvert() => BuildAssert(Convert(arr, typeof(object)), "(object)arr", "CObj(arr)");

        [Fact]
        public void ConstructNegate() => BuildAssert(Negate(i), "-i", "-i");

        [Fact]
        public void ConstructBitwiseNot() => BuildAssert(Not(i), "~i", "Not i");

        [Fact]
        public void ConstructLogicalNot() => BuildAssert(Not(b1), "!b1", "Not b1");

        [Fact]
        public void ConstructTypeAs() => BuildAssert(TypeAs(arr, typeof(object)), "arr as object", "TryCast(arr, Object)");
    }
}
