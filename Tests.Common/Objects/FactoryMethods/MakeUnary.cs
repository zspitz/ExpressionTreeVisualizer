using System;
using System.Linq.Expressions;
using static ExpressionToString.Tests.Categories;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Globals;

namespace ExpressionToString.Tests.Objects {
    public static partial class FactoryMethods {
        [Category(Unary)]
        public static readonly Expression ConstructArrayLength = ArrayLength(arr);

        [Category(Unary)]
        public static readonly Expression ConstructConvert = Convert(arr, typeof(object));

        [Category(Unary)]
        public static readonly Expression ConstructConvertChecked = ConvertChecked(Constant(5), typeof(float));

        [Category(Unary)]
        public static readonly Expression ConstructConvertCheckedForReferenceType = ConvertChecked(arr, typeof(object));

        [Category(Unary)]
        public static readonly Expression ConstructNegate = Negate(i);

        [Category(Unary)]
        public static readonly Expression ConstructBitwiseNot = Not(i);

        [Category(Unary)]
        public static readonly Expression ConstructLogicalNot = Not(b1);

        [Category(Unary)]
        public static readonly Expression ConstructTypeAs = TypeAs(arr, typeof(object));

        [Category(Unary)]
        public static readonly Expression ConstructPostDecrementAssign = PostDecrementAssign(i);

        [Category(Unary)]
        public static readonly Expression ConstructPostIncrementAssign = PostIncrementAssign(i);

        [Category(Unary)]
        public static readonly Expression ConstructPreDecrementAssign = PreDecrementAssign(i);

        [Category(Unary)]
        public static readonly Expression ConstructPreIncrementAssign = PreIncrementAssign(i);

        [Category(Unary)]
        public static readonly Expression ConstructIsTrue = IsTrue(b1);

        [Category(Unary)]
        public static readonly Expression ConstructIsFalse = IsFalse(b1);

        [Category(Unary)]
        public static readonly Expression ConstructIncrement = Increment(i);

        [Category(Unary)]
        public static readonly Expression ConstructDecrement = Decrement(i);

        [Category(Unary)]
        public static readonly Expression ConstructThrow = Throw(Constant(new Random()));

        [Category(Unary)]
        public static readonly Expression ConstructRethrow = Rethrow();
      }
}
