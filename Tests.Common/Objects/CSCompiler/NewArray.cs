using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static ExpressionToString.Tests.Functions;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests.Objects {
    partial class CSCompiler {

        [Category(NewArray)]
        public static readonly Expression SingleDimensionInit = Expr(() => new string[] { "" });

        [Category(NewArray)]
        public static readonly Expression SingleDimensionInitExplicitType = Expr(() => new object[] { "" });

        [Category(NewArray)]
        public static readonly Expression SingleDimensionWithBounds = Expr(() => new string[5]);

        [Category(NewArray)]
        public static readonly Expression MultidimensionWithBounds = Expr(() => new string[2, 3]);

        [Category(NewArray)]
        public static readonly Expression JaggedWithElementsImplicitType = Expr(() => new string[][] {
            new [] {"ab","cd" },
            new [] {"ef","gh"}
        });

        [Category(NewArray)]
        public static readonly Expression JaggedWithElementsExplicitType = Expr(() => new object[][] {
            new [] {"ab","cd" },
            new [] {"ef","gh"}
        });

        [Category(NewArray)]
        public static readonly Expression JaggedWithBounds = Expr(() => new string[5][]);

        [Category(NewArray)]
        public static readonly Expression ArrayOfMultidimensionalArray = Expr(() => new string[5][,]);

        [Category(NewArray)]
        public static readonly Expression MultidimensionalArrayOfArray = Expr(() => new string[3, 2][]);
    }
}