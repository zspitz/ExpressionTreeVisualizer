using System.Linq.Expressions;
using static ExpressionToString.Tests.Categories;
using static System.Linq.Expressions.Expression;

namespace ExpressionToString.Tests.Objects {
    partial class FactoryMethods {

        [Category(NewArray)]
        public static readonly Expression SingleDimensionInit = NewArrayInit(typeof(string), Constant(""));

        [Category(NewArray)]
        public static readonly Expression SingleDimensionInitExplicitType = NewArrayInit(typeof(object), Constant(""));

        [Category(NewArray)]
        public static readonly Expression SingleDimensionWithBounds = NewArrayBounds(typeof(string), Constant(5));

        [Category(NewArray)]
        public static readonly Expression MultidimensionWithBounds = NewArrayBounds(typeof(string), Constant(2), Constant(3));

        [Category(NewArray)]
        public static readonly Expression JaggedWithElementsImplicitType = NewArrayInit(typeof(string[]),
            NewArrayInit(typeof(string), Constant("ab"), Constant("cd")),
            NewArrayInit(typeof(string), Constant("ef"), Constant("gh"))
        );

        [Category(NewArray)]
        public static readonly Expression JaggedWithElementsExplicitType = NewArrayInit(typeof(object[]),
            NewArrayInit(typeof(string), Constant("ab"), Constant("cd")),
            NewArrayInit(typeof(string), Constant("ef"), Constant("gh"))
        );

        [Category(NewArray)]
        public static readonly Expression JaggedWithBounds = NewArrayBounds(typeof(string[]), Constant(5));

        [Category(NewArray)]
        public static readonly Expression ArrayOfMultidimensionalArray = NewArrayBounds(typeof(string[,]), Constant(5));

        [Category(NewArray)]
        public static readonly Expression MultidimensionalArrayOfArray = NewArrayBounds(typeof(string[]), Constant(3), Constant(2));
    }
}