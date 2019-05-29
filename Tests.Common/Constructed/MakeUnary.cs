using System;
using Xunit;
using static ExpressionToString.Tests.Globals;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        [Trait("Category",Unary)]
        public void ConstructArrayLength() => RunTest(ArrayLength(arr), "arr.Length", "arr.Length", "ArrayLength(arr)");

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructConvert() => RunTest(Convert(arr, typeof(object)), "(object)arr", "CObj(arr)", @"Convert(arr,
    typeof(object)
)");

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructConvertChecked() => RunTest(
            ConvertChecked(Constant(5), typeof(float)), 
            "(float)5", "CSng(5)", @"ConvertChecked(
    Constant(5),
    typeof(float)
)");

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructConvertCheckedForReferenceType() => RunTest(ConvertChecked(arr, typeof(object)), "(object)arr", "CObj(arr)", @"Convert(arr,
    typeof(object)
)");

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructNegate() => RunTest(Negate(i), "-i", "-i", "Negate(i)");

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructBitwiseNot() => RunTest(Not(i), "~i", "Not i", "Not(i)");

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructLogicalNot() => RunTest(Not(b1), "!b1", "Not b1", @"Not(b1)");

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructTypeAs() => RunTest(TypeAs(arr, typeof(object)), "arr as object", "TryCast(arr, Object)", @"TypeAs(arr,
    typeof(object)
)");

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructPostDecrementAssign() => RunTest(PostDecrementAssign(i), "i--", "(i -= 1 : i + 1)", "PostDecrementAssign(i)");

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructPostIncrementAssign() => RunTest(PostIncrementAssign(i), "i++", "(i += 1 : i - 1)", "PostIncrementAssign(i)");

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructPreDecrementAssign() => RunTest(PreDecrementAssign(i), "--i", "(i -= 1 : i)", "PreDecrementAssign(i)");

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructPreIncrementAssign() => RunTest(PreIncrementAssign(i), "++i", "(i += 1 : i)", "PreIncrementAssign(i)");

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructIsTrue() => RunTest(
            IsTrue(b1), 
            "b1", 
            "b1", 
            "IsTrue(b1)"
        );

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructIsFalse() => RunTest(
            IsFalse(b1), 
            "!b1", 
            "Not b1", 
            "IsFalse(b1)"
        );

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructIncrement() => RunTest(Increment(i), "i += 1", "i += 1", "Increment(i)");

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructDecrement() => RunTest(Decrement(i), "i -= 1", "i -= 1", "Decrement(i)");

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructThrow() => RunTest(Throw(
            Constant(new Random())), 
            "throw #Random", 
            "Throw #Random", 
            @"Throw(
    Constant(#Random)
)");

        [Fact]
        [Trait("Category", Unary)]
        public void ConstructRethrow() => RunTest(Rethrow(), "throw", "Throw", "Throw(null)");
    }
}
