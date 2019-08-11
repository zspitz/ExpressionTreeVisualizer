using System.Linq.Expressions;
using static ExpressionToString.Tests.Categories;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Globals;

namespace ExpressionToString.Tests.Objects {
    partial class FactoryMethods {
        [Category(Indexer)]
        public static readonly Expression MakeArrayIndex = ArrayIndex(arr, Constant(0));

        [Category(Indexer)]
        public static readonly Expression MakeArrayMultipleIndex = ArrayIndex(arr2D, Constant(0), Constant(1));

        [Category(Indexer)]
        public static readonly Expression MakeArrayAccess = ArrayAccess(arr, Constant(0));

        [Category(Indexer)]
        public static readonly Expression InstanceIndexer = MakeIndex(
            lstString, listIndexer, new[] { Constant(0) as Expression }
        );

        [Category(Indexer)]
        public static readonly Expression PropertyIndexer = Property(lstString, listIndexer, Constant(0));
    }
}