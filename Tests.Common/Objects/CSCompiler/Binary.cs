using System.Linq.Expressions;
using static ExpressionToString.Tests.Categories;
using static ExpressionToString.Tests.Functions;

namespace ExpressionToString.Tests.Objects {
    partial class CSCompiler {
        [Category(Binary)]
        public static readonly Expression Add = IIFE(() => {
            double x = 0, y = 0;
            return Expr(() => x + y);
        });

        [Category(Binary)]
        public static readonly Expression Divide = IIFE(() => {
            double x = 0, y = 0;
            return Expr(() => x / y);
        });

        [Category(Binary)]
        public static readonly Expression Modulo = IIFE(() => {
            double x = 0, y = 0;
            return Expr(() => x % y);
        });

        [Category(Binary)]
        public static readonly Expression Multiply = IIFE(() => {
            double x = 0, y = 0;
            return Expr(() => x * y);
        });

        [Category(Binary)]
        public static readonly Expression Subtract = IIFE(() => {
            double x = 0, y = 0;
            return Expr(() => x - y);
        });

        [Category(Binary)]
        public static readonly Expression AndBitwise = IIFE(() => {
            int i = 0, j = 0;
            return Expr(() => i & j);
        });

        [Category(Binary)]
        public static readonly Expression OrBitwise = IIFE(() => {
            int i = 0, j = 0;
            return Expr(() => i | j);
        });

        [Category(Binary)]
        public static readonly Expression ExclusiveOrBitwise = IIFE(() => {
            int i = 0, j = 0;
            return Expr(() => i ^ j);
        });

        [Category(Binary)]
        public static readonly Expression AndLogical = IIFE(() => {
            bool b1 = true, b2 = true;
            return Expr(() => b1 & b2);
        });

        [Category(Binary)]
        public static readonly Expression OrLogical = IIFE(() => {
            bool b1 = true, b2 = true;
            return Expr(() => b1 | b2);
        });

        [Category(Binary)]
        public static readonly Expression ExclusiveOrLogical = IIFE(() => {
            bool b1 = true, b2 = true;
            return Expr(() => b1 ^ b2);
        });

        [Category(Binary)]
        public static readonly Expression AndAlso = IIFE(() => {
            bool b1 = true, b2 = true;
            return Expr(() => b1 && b2);
        });

        [Category(Binary)]
        public static readonly Expression OrElse = IIFE(() => {
            bool b1 = true, b2 = true;
            return Expr(() => b1 || b2);
        });

        [Category(Binary)]
        public static readonly Expression Equal = IIFE(() => {
            int i = 0, j = 0;
            return Expr(() => i == j);
        });

        [Category(Binary)]
        public static readonly Expression NotEqual = IIFE(() => {
            int i = 0, j = 0;
            return Expr(() => i != j);
        });

        [Category(Binary)]
        public static readonly Expression GreaterThanOrEqual = IIFE(() => {
            int i = 0, j = 0;
            return Expr(() => i >= j);
        });

        [Category(Binary)]
        public static readonly Expression GreaterThan = IIFE(() => {
            int i = 0, j = 0;
            return Expr(() => i > j);
        });

        [Category(Binary)]
        public static readonly Expression LessThan = IIFE(() => {
            int i = 0, j = 0;
            return Expr(() => i < j);
        });

        [Category(Binary)]
        public static readonly Expression LessThanOrEqual = IIFE(() => {
            int i = 0, j = 0;
            return Expr(() => i <= j);
        });

        [Category(Binary)]
        public static readonly Expression Coalesce = IIFE(() => {
            string s1 = null, s2 = null;
            return Expr(() => s1 ?? s2);
        });

        [Category(Binary)]
        public static readonly Expression LeftShift = IIFE(() => {
            int i = 0, j = 0;
            return Expr(() => i << j);
        });

        [Category(Binary)]
        public static readonly Expression RightShift = IIFE(() => {
            int i = 0, j = 0;
            return Expr(() => i >> j);
        });

        [Category(Binary)]
        public static readonly Expression ArrayIndex = IIFE(() => {
            var arr = new string[] { };
            return Expr(() => arr[0]);
        });
    }
}
