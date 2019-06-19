using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionToString.Util {
    public static class BlockExpressionExtensions {
        public static bool HasMultipleLines(this BlockExpression expr) => expr.Variables.Any() || expr.Expressions.Count > 1;

        public static bool HasVariablesRecursive(this BlockExpression expr) =>
            expr.Variables.Any() || expr.Expressions.OfType<BlockExpression>().Any(x => x.HasVariablesRecursive());
        
    }
}
