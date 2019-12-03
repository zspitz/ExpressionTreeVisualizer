using System.Linq.Expressions;
using static ExpressionTreeTestObjects.Categories;
using static System.Linq.Expressions.Expression;
using static ExpressionTreeTestObjects.Globals;

namespace ExpressionTreeTestObjects {
    partial class FactoryMethods {
        [Category(Indexer)]
        internal static readonly Expression MakeArrayIndex = ArrayIndex(arr, Constant(0));

        [Category(Indexer)]
        internal static readonly Expression MakeArrayMultipleIndex = ArrayIndex(arr2D, Constant(0), Constant(1));

        [Category(Indexer)]
        internal static readonly Expression MakeArrayAccess = ArrayAccess(arr, Constant(0));

        [Category(Indexer)]
        internal static readonly Expression InstanceIndexer = MakeIndex(
            lstString, listIndexer, new[] { Constant(0) as Expression }
        );

        [Category(Indexer)]
        internal static readonly Expression PropertyIndexer = Property(lstString, listIndexer, Constant(0));
    }
}