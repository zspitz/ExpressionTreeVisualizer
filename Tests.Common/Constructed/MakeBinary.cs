using Xunit;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        [Fact]
        [Trait("Category", Binary)]
        public void ConstructAdd() => RunTest(Add(x, y), "x + y", "x + y", "Add(x, y)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructAddChecked() => RunTest(AddChecked(x, y), "x + y", "x + y", "AddChecked(x, y)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructDivide() => RunTest(Divide(x, y), "x / y", "x / y", "Divide(x, y)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructModulo() => RunTest(Modulo(x, y), "x % y", "x Mod y", "Modulo(x, y)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructMultiply() => RunTest(Multiply(x, y), "x * y", "x * y", @"Multiply(x, y)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructMultiplyChecked() => RunTest(MultiplyChecked(x, y), "x * y", "x * y", "MultiplyChecked(x, y)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructSubtract() => RunTest(Subtract(x, y), "x - y", "x - y", "Subtract(x, y)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructSubtractChecked() => RunTest(SubtractChecked(x, y), "x - y", "x - y", "SubtractChecked(x, y)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructAndBitwise() => RunTest(And(i, j), "i & j", "i And j", "And(i, j)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructOrBitwise() => RunTest(Or(i, j), "i | j", "i Or j", "Or(i, j)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructExclusiveOrBitwise() => RunTest(ExclusiveOr(i, j), "i ^ j", "i Xor j", "ExclusiveOr(i, j)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructAndLogical() => RunTest(And(b1, b2), "b1 & b2", "b1 And b2", "And(b1, b2)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructOrLogical() => RunTest(Or(b1, b2), "b1 | b2", "b1 Or b2", "Or(b1, b2)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructExclusiveOrLogical() => RunTest(ExclusiveOr(b1, b2), "b1 ^ b2", "b1 Xor b2", "ExclusiveOr(b1, b2)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructAndAlso() => RunTest(AndAlso(b1, b2), "b1 && b2", "b1 AndAlso b2", "AndAlso(b1, b2)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructOrElse() => RunTest(OrElse(b1, b2), "b1 || b2", "b1 OrElse b2", "OrElse(b1, b2)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructEqual() => RunTest(Equal(x, y), "x == y", "x = y", "Equal(x, y)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructNotEqual() => RunTest(NotEqual(x, y), "x != y", "x <> y", "NotEqual(x, y)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructReferenceEqual() => RunTest(ReferenceEqual(lstString, lstString), "lstString == lstString", "lstString Is lstString", "Equal(lstString, lstString)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructReferenceNotEqual() => RunTest(ReferenceNotEqual(lstString, lstString), "lst != lst", "lst IsNot lst", "NotEqual(lstString, lstString)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructGreaterThanOrEqual() => RunTest(GreaterThanOrEqual(x, y), "x >= y", "x >= y", "GreaterThanOrEqual(x, y)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructGreaterThan() => RunTest(GreaterThan(x, y), "x > y", "x > y", "GreaterThan(x, y)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructLessThan() => RunTest(LessThan(x, y), "x < y", "x < y", "LessThan(x, y)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructLessThanOrEqual() => RunTest(LessThanOrEqual(x, y), "x <= y", "x <= y", "LessThanOrEqual(x, y)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructCoalesce() => RunTest(Coalesce(s1, s2), "s1 ?? s2", "If(s1, s2)", "Coalesce(s1, s2)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructLeftShift() => RunTest(LeftShift(i, j), "i << j", "i << j", "LeftShift(i, j)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructRightShift() => RunTest(RightShift(i, j), "i >> j", "i >> j", "RightShift(i, j)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructPower() => RunTest(Power(x, y), "Math.Pow(x, y)", "x ^ y", "Power(x, y)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructArrayIndex() => RunTest(ArrayIndex(arr, i), "arr[i]", "arr(i)", "ArrayIndex(arr, i)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructAssign() => RunTest(Assign(x, Constant(5.2,typeof(double))), "x = 5.2", "x = 5.2", "Assign(x, Constant(5.2))");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructAddAssign() => RunTest(AddAssign(i,j), "i += j", "i += j", "AddAssign(i, j)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructAddAssignChecked() => RunTest(AddAssignChecked(i, j), "i += j", "i += j", "AddAssignChecked(i, j)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructAndAssign() => RunTest(AndAssign(b1, b2), "b1 &= b2", "b1 = b1 And b2", "AndAssign(b1, b2)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructDivideAssign() => RunTest(DivideAssign(i,j), "i /= j", "i /= j", "DivideAssign(i, j)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructExclusiveOrAssign() => RunTest(ExclusiveOrAssign(b1, b2), "b1 ^= b2", "b1 = b1 Xor b2", "ExclusiveOrAssign(b1, b2)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructLeftShiftAssign() => RunTest(LeftShiftAssign(i, j), "i <<= j", "i <<= j", "LeftShiftAssign(i, j)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructModuloAssign() => RunTest(ModuloAssign(i, j), "i %= j", "i = i Mod j", "ModuloAssign(i, j)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructMultiplyAssign() => RunTest(MultiplyAssign(i,j), "i *= j", "i *= j", "MultiplyAssign(i, j)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructMultiplyAssignChecked() => RunTest(MultiplyAssignChecked(i, j), "i *= j", "i *= j", "MultiplyAssignChecked(i, j)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructOrAssign() => RunTest(OrAssign(b1,b2), "b1 |= b2", "b1 = b1 Or b2", "OrAssign(b1, b2)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructPowerAssign() => RunTest(PowerAssign(x,y), "x = Math.Pow(x, y)", "x ^= y", "PowerAssign(x, y)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructRightShiftAssign() => RunTest(RightShiftAssign(i,j), "i >>= j", "i >>= j", "RightShiftAssign(i, j)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructSubtractAssign() => RunTest(SubtractAssign(i,j), "i -= j", "i -= j", "SubtractAssign(i, j)");

        [Fact]
        [Trait("Category", Binary)]
        public void ConstructSubtractAssignChecked() => RunTest(SubtractAssignChecked(i, j), "i -= j", "i -= j", "SubtractAssignChecked(i, j)");
    }
}
