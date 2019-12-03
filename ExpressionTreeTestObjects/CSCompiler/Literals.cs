using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static ExpressionTreeTestObjects.Functions;
using static ExpressionTreeTestObjects.Categories;

namespace ExpressionTreeTestObjects {
    partial class CSCompiler {
        [Category(Literal)]
        internal static readonly Expression True = Expr(() => true);

        [Category(Literal)]
        internal static readonly Expression False = Expr(() => false);

        [Category(Literal)]
        internal static readonly Expression Nothing = Expr(() => (string)null);

        [Category(Literal)]
        internal static readonly Expression Integer = Expr(() => 5);

        [Category(Literal)]
        internal static readonly Expression NonInteger = Expr(() => 7.32);

        [Category(Literal)]
        internal static readonly Expression String = Expr(() => "abcd");

        [Category(Literal)]
        internal static readonly Expression InterpolatedString = Expr(() => $"{new DateTime(2001, 1, 1)}");

        [Category(Literal)]
        internal static readonly Expression Type = Expr(() => typeof(string));
    }
}