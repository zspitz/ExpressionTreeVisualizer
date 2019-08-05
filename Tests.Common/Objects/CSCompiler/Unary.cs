using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static ExpressionToString.Tests.Categories;
using static ExpressionToString.Tests.Functions;


namespace ExpressionToString.Tests.Objects {
    partial class CSCompiler {
        [Category(Unary)]
        public static readonly Expression Negate = IIFE(() => {
            var i = 1;
            return Expr(() => -i);
        });

        [Category(Unary)]
        public static readonly Expression BitwiseNot = IIFE(() => {
            var i = 1;
            return Expr(() => ~i);
        });

        [Category(Unary)]
        public static readonly Expression LogicalNot = IIFE(() => {
            var b = true;
            return Expr(() => !b);
        });

        [Category(Unary)]
        public static readonly Expression TypeAs = IIFE(() => {
            object o = null;
            return Expr(() => o as string);
        })
            ;
        [Category(Unary)]
        public static readonly Expression ArrayLength = IIFE(() => {
            var arr = new string[] { };
            return Expr(() => arr.Length);
        });

        [Category(Unary)]
        public static readonly Expression Convert = IIFE(() => {
            var lst = new List<string>();
            return Expr(() => (object)lst);
        });
    }
}
