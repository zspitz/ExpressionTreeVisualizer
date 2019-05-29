using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionToString.Util {
    public static class ParameterInfoExtensions {
        public static bool HasAttribute<TAttribute>(this ParameterInfo pi, bool inherit = false) where TAttribute : Attribute =>
            pi.GetCustomAttributes(typeof(TAttribute), inherit).Any();
    }
}
