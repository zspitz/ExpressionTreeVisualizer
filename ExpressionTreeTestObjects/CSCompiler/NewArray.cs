using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static ExpressionTreeTestObjects.Functions;
using static ExpressionTreeTestObjects.Categories;

namespace ExpressionTreeTestObjects {
    partial class CSCompiler {

        [Category(NewArray)]
        internal static readonly Expression SingleDimensionInit = Expr(() => new string[] { "" });

        [Category(NewArray)]
        internal static readonly Expression SingleDimensionInitExplicitType = Expr(() => new object[] { "" });

        [Category(NewArray)]
        internal static readonly Expression SingleDimensionWithBounds = Expr(() => new string[5]);

        [Category(NewArray)]
        internal static readonly Expression MultidimensionWithBounds = Expr(() => new string[2, 3]);

        [Category(NewArray)]
        internal static readonly Expression JaggedWithElementsImplicitType = Expr(() => new string[][] {
            new [] {"ab","cd" },
            new [] {"ef","gh"}
        });

        [Category(NewArray)]
        internal static readonly Expression JaggedWithElementsExplicitType = Expr(() => new object[][] {
            new [] {"ab","cd" },
            new [] {"ef","gh"}
        });

        [Category(NewArray)]
        internal static readonly Expression JaggedWithBounds = Expr(() => new string[5][]);

        [Category(NewArray)]
        internal static readonly Expression ArrayOfMultidimensionalArray = Expr(() => new string[5][,]);

        [Category(NewArray)]
        internal static readonly Expression MultidimensionalArrayOfArray = Expr(() => new string[3, 2][]);
    }
}