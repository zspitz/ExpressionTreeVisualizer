using System.Linq.Expressions;
using static ExpressionTreeTestObjects.Categories;
using static System.Linq.Expressions.Expression;

namespace ExpressionTreeTestObjects {
    partial class FactoryMethods {

        [Category(NewArray)]
        internal static readonly Expression SingleDimensionInit = NewArrayInit(typeof(string), Constant(""));

        [Category(NewArray)]
        internal static readonly Expression SingleDimensionInitExplicitType = NewArrayInit(typeof(object), Constant(""));

        [Category(NewArray)]
        internal static readonly Expression SingleDimensionWithBounds = NewArrayBounds(typeof(string), Constant(5));

        [Category(NewArray)]
        internal static readonly Expression MultidimensionWithBounds = NewArrayBounds(typeof(string), Constant(2), Constant(3));

        [Category(NewArray)]
        internal static readonly Expression JaggedWithElementsImplicitType = NewArrayInit(typeof(string[]),
            NewArrayInit(typeof(string), Constant("ab"), Constant("cd")),
            NewArrayInit(typeof(string), Constant("ef"), Constant("gh"))
        );

        [Category(NewArray)]
        internal static readonly Expression JaggedWithElementsExplicitType = NewArrayInit(typeof(object[]),
            NewArrayInit(typeof(string), Constant("ab"), Constant("cd")),
            NewArrayInit(typeof(string), Constant("ef"), Constant("gh"))
        );

        [Category(NewArray)]
        internal static readonly Expression JaggedWithBounds = NewArrayBounds(typeof(string[]), Constant(5));

        [Category(NewArray)]
        internal static readonly Expression ArrayOfMultidimensionalArray = NewArrayBounds(typeof(string[,]), Constant(5));

        [Category(NewArray)]
        internal static readonly Expression MultidimensionalArrayOfArray = NewArrayBounds(typeof(string[]), Constant(3), Constant(2));
    }
}