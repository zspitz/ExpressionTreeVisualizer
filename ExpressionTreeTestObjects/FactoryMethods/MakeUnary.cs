using System;
using System.Linq.Expressions;
using static ExpressionTreeTestObjects.Categories;
using static System.Linq.Expressions.Expression;
using static ExpressionTreeTestObjects.Globals;

namespace ExpressionTreeTestObjects {
    internal static partial class FactoryMethods {
        [Category(Unary)]
        internal static readonly Expression ConstructArrayLength = ArrayLength(arr);

        [Category(Unary)]
        internal static readonly Expression ConstructConvert = Convert(arr, typeof(object));

        [Category(Unary)]
        internal static readonly Expression ConstructConvertChecked = ConvertChecked(Constant(5), typeof(float));

        [Category(Unary)]
        internal static readonly Expression ConstructConvertCheckedForReferenceType = ConvertChecked(arr, typeof(object));

        [Category(Unary)]
        internal static readonly Expression ConstructNegate = Negate(i);

        [Category(Unary)]
        internal static readonly Expression ConstructBitwiseNot = Not(i);

        [Category(Unary)]
        internal static readonly Expression ConstructLogicalNot = Not(b1);

        [Category(Unary)]
        internal static readonly Expression ConstructTypeAs = TypeAs(arr, typeof(object));

        [Category(Unary)]
        internal static readonly Expression ConstructPostDecrementAssign = PostDecrementAssign(i);

        [Category(Unary)]
        internal static readonly Expression ConstructPostIncrementAssign = PostIncrementAssign(i);

        [Category(Unary)]
        internal static readonly Expression ConstructPreDecrementAssign = PreDecrementAssign(i);

        [Category(Unary)]
        internal static readonly Expression ConstructPreIncrementAssign = PreIncrementAssign(i);

        [Category(Unary)]
        internal static readonly Expression ConstructIsTrue = IsTrue(b1);

        [Category(Unary)]
        internal static readonly Expression ConstructIsFalse = IsFalse(b1);

        [Category(Unary)]
        internal static readonly Expression ConstructIncrement = Increment(i);

        [Category(Unary)]
        internal static readonly Expression ConstructDecrement = Decrement(i);

        [Category(Unary)]
        internal static readonly Expression ConstructThrow = Throw(Constant(new Random()));

        [Category(Unary)]
        internal static readonly Expression ConstructRethrow = Rethrow();
      }
}
