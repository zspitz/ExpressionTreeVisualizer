using System.Linq.Expressions;
using static ExpressionTreeTestObjects.Categories;
using static System.Linq.Expressions.Expression;
using static ExpressionTreeTestObjects.Globals;

namespace ExpressionTreeTestObjects {
    internal static partial class FactoryMethods {
        [Category(Binary)]
        internal static readonly Expression ConstructAdd = Add(x, y);

        [Category(Binary)]
        internal static readonly Expression ConstructAddChecked = AddChecked(x, y);

        [Category(Binary)]
        internal static readonly Expression ConstructDivide = Divide(x, y);

        [Category(Binary)]
        internal static readonly Expression ConstructModulo = Modulo(x, y);

        [Category(Binary)]
        internal static readonly Expression ConstructMultiply = Multiply(x, y);

        [Category(Binary)]
        internal static readonly Expression ConstructMultiplyChecked = MultiplyChecked(x, y);

        [Category(Binary)]
        internal static readonly Expression ConstructSubtract = Subtract(x, y);

        [Category(Binary)]
        internal static readonly Expression ConstructSubtractChecked = SubtractChecked(x, y);

        [Category(Binary)]
        internal static readonly Expression ConstructAndBitwise = And(i, j);

        [Category(Binary)]
        internal static readonly Expression ConstructOrBitwise = Or(i, j);

        [Category(Binary)]
        internal static readonly Expression ConstructExclusiveOrBitwise = ExclusiveOr(i, j);

        [Category(Binary)]
        internal static readonly Expression ConstructAndLogical = And(b1, b2);

        [Category(Binary)]
        internal static readonly Expression ConstructOrLogical = Or(b1, b2);

        [Category(Binary)]
        internal static readonly Expression ConstructExclusiveOrLogical = ExclusiveOr(b1, b2);

        [Category(Binary)]
        internal static readonly Expression ConstructAndAlso = AndAlso(b1, b2);

        [Category(Binary)]
        internal static readonly Expression ConstructOrElse = OrElse(b1, b2);

        [Category(Binary)]
        internal static readonly Expression ConstructEqual = Equal(x, y);

        [Category(Binary)]
        internal static readonly Expression ConstructNotEqual = NotEqual(x, y);

        [Category(Binary)]
        internal static readonly Expression ConstructReferenceEqual = ReferenceEqual(lstString, lstString);

        [Category(Binary)]
        internal static readonly Expression ConstructReferenceNotEqual = ReferenceNotEqual(lstString, lstString);

        [Category(Binary)]
        internal static readonly Expression ConstructGreaterThanOrEqual = GreaterThanOrEqual(x, y);

        [Category(Binary)]
        internal static readonly Expression ConstructGreaterThan = GreaterThan(x, y);

        [Category(Binary)]
        internal static readonly Expression ConstructLessThan = LessThan(x, y);

        [Category(Binary)]
        internal static readonly Expression ConstructLessThanOrEqual = LessThanOrEqual(x, y);

        [Category(Binary)]
        internal static readonly Expression ConstructCoalesce = Coalesce(s1, s2);

        [Category(Binary)]
        internal static readonly Expression ConstructLeftShift = LeftShift(i, j);

        [Category(Binary)]
        internal static readonly Expression ConstructRightShift = RightShift(i, j);

        [Category(Binary)]
        internal static readonly Expression ConstructPower = Power(x, y);

        [Category(Binary)]
        internal static readonly Expression ConstructArrayIndex = ArrayIndex(arr, i);

        [Category(Binary)]
        internal static readonly Expression ConstructAssign = Assign(x, Constant(5.2, typeof(double)));

        [Category(Binary)]
        internal static readonly Expression ConstructAddAssign = AddAssign(i, j);

        [Category(Binary)]
        internal static readonly Expression ConstructAddAssignChecked = AddAssignChecked(i, j);

        [Category(Binary)]
        internal static readonly Expression ConstructAndAssign = AndAssign(b1, b2);

        [Category(Binary)]
        internal static readonly Expression ConstructDivideAssign = DivideAssign(i, j);

        [Category(Binary)]
        internal static readonly Expression ConstructExclusiveOrAssign = ExclusiveOrAssign(b1, b2);

        [Category(Binary)]
        internal static readonly Expression ConstructLeftShiftAssign = LeftShiftAssign(i, j);

        [Category(Binary)]
        internal static readonly Expression ConstructModuloAssign = ModuloAssign(i, j);

        [Category(Binary)]
        internal static readonly Expression ConstructMultiplyAssign = MultiplyAssign(i, j);

        [Category(Binary)]
        internal static readonly Expression ConstructMultiplyAssignChecked = MultiplyAssignChecked(i, j);

        [Category(Binary)]
        internal static readonly Expression ConstructOrAssign = OrAssign(b1, b2);

        [Category(Binary)]
        internal static readonly Expression ConstructPowerAssign = PowerAssign(x, y);

        [Category(Binary)]
        internal static readonly Expression ConstructRightShiftAssign = RightShiftAssign(i, j);

        [Category(Binary)]
        internal static readonly Expression ConstructSubtractAssign = SubtractAssign(i, j);

        [Category(Binary)]
        internal static readonly Expression ConstructSubtractAssignChecked = SubtractAssignChecked(i, j);
    }
}
