using System.Linq.Expressions;
using System.Reflection;
using static System.Reflection.BindingFlags;

namespace ExpressionToString.Util {
    public static class BinaryExpressionExtensions {
        private static PropertyInfo IsReferenceComparisonProperty = typeof(BinaryExpression).GetProperty("IsReferenceComparison", Instance | NonPublic);
        public static bool IsReferenceComparison(this BinaryExpression expr) =>
            (bool)IsReferenceComparisonProperty.GetValue(expr);
    }
}
