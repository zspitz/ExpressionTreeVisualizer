using System.Linq.Expressions;
using Xunit;
using static ExpressionToString.Tests.Runners;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Globals;

namespace ExpressionToString.Tests.Constructed {
    [Trait("Source", FactoryMethods)]
    public class MakeBinary {
        [Fact]
        public void ConstructAdd() => BuildAssert(Add(x, y), "x + y", "x + y");

        [Fact]
        public void ConstructAddChecked() => BuildAssert(AddChecked(x, y), "x + y", "x + y");

        [Fact]
        public void ConstructDivide() => BuildAssert(Divide(x, y), "x / y", "x / y");

        [Fact]
        public void ConstructModulo() => BuildAssert(Modulo(x, y), "x % y", "x Mod y");

        [Fact]
        public void ConstructMultiply() => BuildAssert(Multiply(x, y), "x * y", "x * y");

        [Fact]
        public void ConstructMultiplyChecked() => BuildAssert(MultiplyChecked(x, y), "x * y", "x * y");

        [Fact]
        public void ConstructSubtract() => BuildAssert(Subtract(x, y), "x - y", "x - y");

        [Fact]
        public void ConstructSubtractChecked() => BuildAssert(SubtractChecked(x, y), "x - y", "x - y");

        [Fact]
        public void ConstructAndBitwise() => BuildAssert(And(i, j), "i & j", "i And j");

        [Fact]
        public void ConstructOrBitwise() => BuildAssert(Or(i, j), "i | j", "i Or j");

        [Fact]
        public void ConstructExclusiveOrBitwise() => BuildAssert(ExclusiveOr(i, j), "i ^ j", "i Xor j");

        [Fact]
        public void ConstructAndLogical() => BuildAssert(And(b1, b2), "b1 & b2", "b1 And b2");

        [Fact]
        public void ConstructOrLogical() => BuildAssert(Or(b1, b2), "b1 | b2", "b1 Or b2");

        [Fact]
        public void ConstructExclusiveOrLogical() => BuildAssert(ExclusiveOr(b1, b2), "b1 ^ b2", "b1 Xor b2");

        [Fact]
        public void ConstructAndAlso() => BuildAssert(AndAlso(b1, b2), "b1 && b2", "b1 AndAlso b2");

        [Fact]
        public void ConstructOrElse() => BuildAssert(OrElse(b1, b2), "b1 || b2", "b1 OrElse b2");

        [Fact]
        public void ConstructEqual() => BuildAssert(Equal(x, y), "x == y", "x = y");

        [Fact]
        public void ConstructNotEqual() => BuildAssert(NotEqual(x, y), "x != y", "x <> y");

        [Fact]
        public void ConstructReferenceEqual() => BuildAssert(ReferenceEqual(lst, lst), "lst == lst", "lst Is lst");

        [Fact]
        public void ConstructReferenceNotEqual() => BuildAssert(ReferenceNotEqual(lst, lst), "lst != lst", "lst IsNot lst");

        [Fact]
        public void ConstructGreaterThanOrEqual() => BuildAssert(GreaterThanOrEqual(x, y), "x >= y", "x >= y");

        [Fact]
        public void ConstructGreaterThan() => BuildAssert(GreaterThan(x, y), "x > y", "x > y");

        [Fact]
        public void ConstructLessThan() => BuildAssert(LessThan(x, y), "x < y", "x < y");

        [Fact]
        public void ConstructLessThanOrEqual() => BuildAssert(LessThanOrEqual(x, y), "x <= y", "x <= y");

        [Fact]
        public void ConstructCoalesce() => BuildAssert(Coalesce(s1, s2), "s1 ?? s2", "If(s1, s2)");

        [Fact]
        public void ConstructLeftShift() => BuildAssert(LeftShift(i, j), "i << j", "i << j");

        [Fact]
        public void ConstructRightShift() => BuildAssert(RightShift(i, j), "i >> j", "i >> j");

        [Fact]
        public void ConstructPower() => BuildAssert(Power(x, y), "Math.Pow(x, y)", "x ^ y");

        [Fact]
        public void ConstructArrayIndex() => BuildAssert(ArrayIndex(arr, i), "arr[i]", "arr(i)");

        [Fact]
        public void ConstructAssign() => BuildAssert(Assign(x, Constant(5.2,typeof(double))), "x = 5.2", "x = 5.2");

        [Fact]
        public void ConstructAddAssign() => BuildAssert(AddAssign(i,j), "i += j", "i += j");

        [Fact]
        public void ConstructAddAssignChecked() => BuildAssert(AddAssignChecked(i, j), "i += j", "i += j");

        [Fact]
        public void ConstructAndAssign() => BuildAssert(AndAssign(b1, b2), "b1 &= b2", "b1 = b1 And b2");

        [Fact]
        public void ConstructDivideAssign() => BuildAssert(DivideAssign(i,j), "i /= j", "i /= j");

        [Fact]
        public void ConstructExclusiveOrAssign() => BuildAssert(ExclusiveOrAssign(b1, b2), "b1 ^= b2", "b1 = b1 Xor b2");

        [Fact]
        public void ConstructLeftShiftAssign() => BuildAssert(LeftShiftAssign(i, j), "i <<= j", "i <<= j");

        [Fact]
        public void ConstructModuloAssign() => BuildAssert(ModuloAssign(i, j), "i %= j", "i = i Mod j");

        [Fact]
        public void ConstructMultiplyAssign() => BuildAssert(MultiplyAssign(i,j), "i *= j", "i *= j");

        [Fact]
        public void ConstructMultiplyAssignChecked() => BuildAssert(MultiplyAssignChecked(i, j), "i *= j", "i *= j");

        [Fact]
        public void ConstructOrAssign() => BuildAssert(OrAssign(b1,b2), "b1 |= b2", "b1 = b1 Or b2");

        [Fact]
        public void ConstructPowerAssign() => BuildAssert(PowerAssign(x,y), "x = Math.Pow(x, y)", "x ^= y");

        [Fact]
        public void ConstructRightShiftAssign() => BuildAssert(RightShiftAssign(i,j), "i >>= j", "i >>= j");

        [Fact]
        public void ConstructSubtractAssign() => BuildAssert(SubtractAssign(i,j), "i -= j", "i -= j");

        [Fact]
        public void ConstructSubtractAssignChecked() => BuildAssert(SubtractAssignChecked(i, j), "i -= j", "i -= j");

        [Fact]
        public void ConstructPostDecrementAssign() => BuildAssert(PostDecrementAssign(i), "i--", "(i -= 1 : i + 1)");

        [Fact]
        public void ConstructPostIncrementAssign() => BuildAssert(PostIncrementAssign(i), "i++", "(i += 1 : i - 1)");

        [Fact]
        public void ConstructPreDecrementAssign() => BuildAssert(PreDecrementAssign(i), "--i", "(i -= 1 : i)");

        [Fact]
        public void ConstructPreIncrementAssign() => BuildAssert(PreIncrementAssign(i), "++i", "(i += 1 : i)");
    }
}
