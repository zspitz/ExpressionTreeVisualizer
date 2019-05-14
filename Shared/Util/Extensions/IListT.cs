using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionToString.Util {
    public static class ListTExtensions {
        public static void RemoveLast<T>(this List<T> lst, int count = 1) => lst.RemoveRange(lst.Count - count, count);
    }
}
