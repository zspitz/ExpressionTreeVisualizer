using System.Collections.Generic;
using System.Linq.Expressions;
using static ExpressionTreeTestObjects.Categories;
using static ExpressionTreeTestObjects.Functions;


namespace ExpressionTreeTestObjects {
    partial class CSCompiler {
        [Category(Unary)]
        internal static readonly Expression Negate = IIFE(() => {
            var i = 1;
            return Expr(() => -i);
        });

        [Category(Unary)]
        internal static readonly Expression BitwiseNot = IIFE(() => {
            var i = 1;
            return Expr(() => ~i);
        });

        [Category(Unary)]
        internal static readonly Expression LogicalNot = IIFE(() => {
            var b = true;
            return Expr(() => !b);
        });

        [Category(Unary)]
        internal static readonly Expression TypeAs = IIFE(() => {
            object o = null;
            return Expr(() => o as string);
        })
            ;
        [Category(Unary)]
        internal static readonly Expression ArrayLength = IIFE(() => {
            var arr = new string[] { };
            return Expr(() => arr.Length);
        });

        [Category(Unary)]
        internal static readonly Expression Convert = IIFE(() => {
            var lst = new List<string>();
            return Expr(() => (object)lst);
        });
    }
}
