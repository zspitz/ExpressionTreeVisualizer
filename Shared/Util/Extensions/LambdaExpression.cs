using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionToString.Util {
    public static class LambdaExpressionExtensions {
        public static object GetTarget(this LambdaExpression expr) => expr.Compile().Target;
    }
}
