using System;
using Xunit;
using static ExpressionToString.Tests.Globals;
using static System.Linq.Expressions.Expression;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        public void ConstructArrayLength() => RunTest(ArrayLength(arr), "arr.Length", "arr.Length");

        [Fact]
        public void ConstructConvert() => RunTest(Convert(arr, typeof(object)), "(object)arr", "CObj(arr)");

        [Fact]
        public void ConstructConvertChecked() => RunTest(ConvertChecked(arr, typeof(object)), "(object)arr", "CObj(arr)");

        [Fact]
        public void ConstructNegate() => RunTest(Negate(i), "-i", "-i");

        [Fact]
        public void ConstructBitwiseNot() => RunTest(Not(i), "~i", "Not i");

        [Fact]
        public void ConstructLogicalNot() => RunTest(Not(b1), "!b1", "Not b1");

        [Fact]
        public void ConstructTypeAs() => RunTest(TypeAs(arr, typeof(object)), "arr as object", "TryCast(arr, Object)");

        [Fact]
        public void ConstructPostDecrementAssign() => RunTest(PostDecrementAssign(i), "i--", "(i -= 1 : i + 1)");

        [Fact]
        public void ConstructPostIncrementAssign() => RunTest(PostIncrementAssign(i), "i++", "(i += 1 : i - 1)");

        [Fact]
        public void ConstructPreDecrementAssign() => RunTest(PreDecrementAssign(i), "--i", "(i -= 1 : i)");

        [Fact]
        public void ConstructPreIncrementAssign() => RunTest(PreIncrementAssign(i), "++i", "(i += 1 : i)");

        [Fact]
        public void ConstructIsTrue() => RunTest(IsTrue(b1), "b1", "b1");

        [Fact]
        public void ConstructIsFalse() => RunTest(IsFalse(b1), "!b1", "Not b1");

        [Fact]
        public void ConstructIncrement() => RunTest(Increment(i), "i += 1", "i += 1");

        [Fact]
        public void ConstructDecrement() => RunTest(Decrement(i), "i -= 1", "i -= 1");

        [Fact]
        public void ConstructThrow() => RunTest(Throw(Constant(new Random())), "throw #Random", "Throw #Random");

        [Fact]
        public void ConstructRethrow() => RunTest(Rethrow(), "throw", "Throw");
    }
}
