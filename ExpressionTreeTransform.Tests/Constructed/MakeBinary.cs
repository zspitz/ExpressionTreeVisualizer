using System.Linq.Expressions;
using Xunit;
using static ExpressionTreeTransform.Tests.Runners;
using static System.Linq.Expressions.Expression;
using static ExpressionTreeTransform.Util.Globals;

namespace ExpressionTreeTransform.Tests {
    [Trait("Source", FactoryMethods)]
    public class MakeBinary {
        readonly ParameterExpression i = Parameter(typeof(int), "i");
        readonly ParameterExpression j = Parameter(typeof(int), "j");
        readonly ParameterExpression x = Parameter(typeof(double), "x");
        readonly ParameterExpression y = Parameter(typeof(double), "y");
        readonly ParameterExpression b1 = Parameter(typeof(bool), "b1");
        readonly ParameterExpression b2 = Parameter(typeof(bool), "b2");
        readonly ParameterExpression s1 = Parameter(typeof(string), "s1");
        readonly ParameterExpression s2 = Parameter(typeof(string), "s2");
        readonly ParameterExpression arr = Parameter(typeof(string[]), "arr");

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
    }
}
