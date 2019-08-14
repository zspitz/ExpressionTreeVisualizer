using System.Linq;
using System.Linq.Expressions;
using static System.Linq.Expressions.Expression;

namespace ExpressionToString.Util {
    public static class ExpressionExtensions {
        // TODO what about expressions which contain ParameterExpressions?
        // we'd have to pass in the values to use when calling the compiled lambda, and also which name corresponds to which value
        // parhaps an array of tuples?
        public static object ExtractValue(this Expression expr) {
            if (!(expr is LambdaExpression lambda)) {
                lambda = Lambda(expr);
            }
            return lambda.Compile().DynamicInvoke();
        }

        public static bool TryExtractValue(this Expression expr, out object value) {
            value = null;
            try {
                value = expr.ExtractValue();
                return true;
            } catch {}
            return false;
        }

        public static Expression SansConvert(this Expression expr) =>
            expr is UnaryExpression uexpr && uexpr.NodeType == ExpressionType.Convert ?
                uexpr.Operand :
                expr;

        public static bool IsEmpty(this Expression expr) =>
            expr is DefaultExpression && expr.Type == typeof(void);

        public static bool IsClosedVariable(this Expression expr) =>
            expr is MemberExpression mexpr && (mexpr.Expression?.Type.IsClosureClass() ?? false);

        public static string Name(this Expression expr, string language = "C#") {
            string ret = "";
            string staticTypename = "";
            switch (expr) {
                case ParameterExpression pexpr:
                    ret = pexpr.Name;
                    break;
                case MemberExpression mexpr:
                    if (mexpr.Expression == null) {
                        staticTypename = mexpr.Member.DeclaringType.FriendlyName(language) + ".";
                    }
                    ret = staticTypename + mexpr.Member.Name;
                    break;
                case MethodCallExpression callExpr:
                    if (callExpr.Object == null) {
                        staticTypename = callExpr.Method.DeclaringType.FriendlyName(language);
                    }
                    ret = staticTypename + callExpr.Method.Name;
                    break;
                case LambdaExpression lambdaExpr:
                    ret = lambdaExpr.Name;
                    break;
            }
            return ret;
        }
    }
}
