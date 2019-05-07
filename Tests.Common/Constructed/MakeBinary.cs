using Xunit;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Globals;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        public void ConstructAdd() => RunTest(Add(x, y), "x + y", "x + y");

        [Fact]
        public void ConstructAddChecked() => RunTest(AddChecked(x, y), "x + y", "x + y");

        [Fact]
        public void ConstructDivide() => RunTest(Divide(x, y), "x / y", "x / y");

        [Fact]
        public void ConstructModulo() => RunTest(Modulo(x, y), "x % y", "x Mod y");

        [Fact]
        public void ConstructMultiply() => RunTest(Multiply(x, y), "x * y", "x * y");

        [Fact]
        public void ConstructMultiplyChecked() => RunTest(MultiplyChecked(x, y), "x * y", "x * y");

        [Fact]
        public void ConstructSubtract() => RunTest(Subtract(x, y), "x - y", "x - y");

        [Fact]
        public void ConstructSubtractChecked() => RunTest(SubtractChecked(x, y), "x - y", "x - y");

        [Fact]
        public void ConstructAndBitwise() => RunTest(And(i, j), "i & j", "i And j");

        [Fact]
        public void ConstructOrBitwise() => RunTest(Or(i, j), "i | j", "i Or j");

        [Fact]
        public void ConstructExclusiveOrBitwise() => RunTest(ExclusiveOr(i, j), "i ^ j", "i Xor j");

        [Fact]
        public void ConstructAndLogical() => RunTest(And(b1, b2), "b1 & b2", "b1 And b2");

        [Fact]
        public void ConstructOrLogical() => RunTest(Or(b1, b2), "b1 | b2", "b1 Or b2");

        [Fact]
        public void ConstructExclusiveOrLogical() => RunTest(ExclusiveOr(b1, b2), "b1 ^ b2", "b1 Xor b2");

        [Fact]
        public void ConstructAndAlso() => RunTest(AndAlso(b1, b2), "b1 && b2", "b1 AndAlso b2");

        [Fact]
        public void ConstructOrElse() => RunTest(OrElse(b1, b2), "b1 || b2", "b1 OrElse b2");

        [Fact]
        public void ConstructEqual() => RunTest(Equal(x, y), "x == y", "x = y");

        [Fact]
        public void ConstructNotEqual() => RunTest(NotEqual(x, y), "x != y", "x <> y");

        [Fact]
        public void ConstructReferenceEqual() => RunTest(ReferenceEqual(lstString, lstString), "lst == lst", "lst Is lst");

        [Fact]
        public void ConstructReferenceNotEqual() => RunTest(ReferenceNotEqual(lstString, lstString), "lst != lst", "lst IsNot lst");

        [Fact]
        public void ConstructGreaterThanOrEqual() => RunTest(GreaterThanOrEqual(x, y), "x >= y", "x >= y");

        [Fact]
        public void ConstructGreaterThan() => RunTest(GreaterThan(x, y), "x > y", "x > y");

        [Fact]
        public void ConstructLessThan() => RunTest(LessThan(x, y), "x < y", "x < y");

        [Fact]
        public void ConstructLessThanOrEqual() => RunTest(LessThanOrEqual(x, y), "x <= y", "x <= y");

        [Fact]
        public void ConstructCoalesce() => RunTest(Coalesce(s1, s2), "s1 ?? s2", "If(s1, s2)");

        [Fact]
        public void ConstructLeftShift() => RunTest(LeftShift(i, j), "i << j", "i << j");

        [Fact]
        public void ConstructRightShift() => RunTest(RightShift(i, j), "i >> j", "i >> j");

        [Fact]
        public void ConstructPower() => RunTest(Power(x, y), "Math.Pow(x, y)", "x ^ y");

        [Fact]
        public void ConstructArrayIndex() => RunTest(ArrayIndex(arr, i), "arr[i]", "arr(i)");

        [Fact]
        public void ConstructAssign() => RunTest(Assign(x, Constant(5.2,typeof(double))), "x = 5.2", "x = 5.2");

        [Fact]
        public void ConstructAddAssign() => RunTest(AddAssign(i,j), "i += j", "i += j");

        [Fact]
        public void ConstructAddAssignChecked() => RunTest(AddAssignChecked(i, j), "i += j", "i += j");

        [Fact]
        public void ConstructAndAssign() => RunTest(AndAssign(b1, b2), "b1 &= b2", "b1 = b1 And b2");

        [Fact]
        public void ConstructDivideAssign() => RunTest(DivideAssign(i,j), "i /= j", "i /= j");

        [Fact]
        public void ConstructExclusiveOrAssign() => RunTest(ExclusiveOrAssign(b1, b2), "b1 ^= b2", "b1 = b1 Xor b2");

        [Fact]
        public void ConstructLeftShiftAssign() => RunTest(LeftShiftAssign(i, j), "i <<= j", "i <<= j");

        [Fact]
        public void ConstructModuloAssign() => RunTest(ModuloAssign(i, j), "i %= j", "i = i Mod j");

        [Fact]
        public void ConstructMultiplyAssign() => RunTest(MultiplyAssign(i,j), "i *= j", "i *= j");

        [Fact]
        public void ConstructMultiplyAssignChecked() => RunTest(MultiplyAssignChecked(i, j), "i *= j", "i *= j");

        [Fact]
        public void ConstructOrAssign() => RunTest(OrAssign(b1,b2), "b1 |= b2", "b1 = b1 Or b2");

        [Fact]
        public void ConstructPowerAssign() => RunTest(PowerAssign(x,y), "x = Math.Pow(x, y)", "x ^= y");

        [Fact]
        public void ConstructRightShiftAssign() => RunTest(RightShiftAssign(i,j), "i >>= j", "i >>= j");

        [Fact]
        public void ConstructSubtractAssign() => RunTest(SubtractAssign(i,j), "i -= j", "i -= j");

        [Fact]
        public void ConstructSubtractAssignChecked() => RunTest(SubtractAssignChecked(i, j), "i -= j", "i -= j");
    }
}
