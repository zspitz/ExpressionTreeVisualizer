using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;

namespace ExpressionTreeTransform.Util {
    public static class ExpressionExtensions {
        public static object ExtractValue(this Expression expr) =>
            Lambda(expr).Compile().DynamicInvoke();

        public static Expression SansConvert(this Expression expr) =>
            expr is UnaryExpression uexpr && uexpr.NodeType == ExpressionType.Convert ?
                uexpr.Operand :
                expr;
    }
}
