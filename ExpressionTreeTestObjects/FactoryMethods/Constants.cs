using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static ExpressionTreeTestObjects.Categories;
using static System.Linq.Expressions.Expression;
using System.Collections;

namespace ExpressionTreeTestObjects {
    partial class FactoryMethods {
        [Category(Constants)]
        internal static readonly Expression Random = Constant(new Random());

        [Category(Constants)]
        internal static readonly Expression ValueTuple = Constant(("abcd", 5));

        [Category(Constants)]
        internal static readonly Expression OldTuple = Constant(Tuple.Create("abcd", 5));

        [Category(Constants)]
        internal static readonly Expression Array = Constant(new object[] { "abcd", 5, new Random() });

        [Category(Constants)]
        internal static readonly Expression Type = Constant(typeof(string));

        [Category(Constants)]
        internal static readonly Expression DifferentTypeForNodeAndValue = Constant(new List<string>(), typeof(IEnumerable));
    }
}