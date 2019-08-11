using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static ExpressionToString.Tests.Functions;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests.Objects {
    partial class CSCompiler {
        [Category(Literal)]
        public static readonly Expression True = Expr(() => true);

        [Category(Literal)]
        public static readonly Expression False = Expr(() => false);

        [Category(Literal)]
        public static readonly Expression Nothing = Expr(() => (string)null);

        [Category(Literal)]
        public static readonly Expression Integer = Expr(() => 5);

        [Category(Literal)]
        public static readonly Expression NonInteger = Expr(() => 7.32);

        [Category(Literal)]
        public static readonly Expression String = Expr(() => "abcd");

        [Category(Literal)]
        public static readonly Expression InterpolatedString = Expr(() => $"{new DateTime(2001, 1, 1)}");

        [Category(Literal)]
        public static readonly Expression Type = Expr(() => typeof(string));
    }
}