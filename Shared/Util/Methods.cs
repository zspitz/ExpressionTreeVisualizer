using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionToString.Util {
    public static class Methods {
        public static void VerifyCount<T>(IList<T> lst, int count) {
            if (lst.Count == count) { return; }
            throw new InvalidOperationException("Invalid argument count");
        }
        public static void VerifyCount<T>(IList<T> lst, int? min, int? max) {
            if (min == null && max == null) { throw new ArgumentNullException("Either 'min' or 'max' must be non-null"); }
            bool valid = min == null || lst.Count >= min;
            valid = valid && (max == null || lst.Count <= max);
            if (valid) { return; }
            throw new InvalidOperationException("Invalid argument count");
        }
    }
}
