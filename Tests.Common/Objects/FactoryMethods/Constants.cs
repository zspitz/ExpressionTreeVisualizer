using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static ExpressionToString.Tests.Categories;
using static System.Linq.Expressions.Expression;
using System.Collections;

namespace ExpressionToString.Tests.Objects {
    partial class FactoryMethods {
        [Category(Constants)]
        public static readonly Expression Random = Constant(new Random());

        [Category(Constants)]
        public static readonly Expression ValueTuple = Constant(("abcd", 5));

        [Category(Constants)]
        public static readonly Expression OldTuple = Constant(Tuple.Create("abcd", 5));

        [Category(Constants)]
        public static readonly Expression Array = Constant(new object[] { "abcd", 5, new Random() });

        [Category(Constants)]
        public static readonly Expression Type = Constant(typeof(string));

        [Category(Constants)]
        public static readonly Expression DifferentTypeForNodeAndValue = Constant(new List<string>(), typeof(IEnumerable));
    }
}