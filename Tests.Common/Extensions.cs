using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Common {
    public static class Extensions {
        public static TheoryData<T1, T2, T3> ToTheoryData<T1, T2, T3>(this IEnumerable<ValueTuple<T1, T2, T3>> src) {
            var ret = new TheoryData<T1, T2, T3>();
            foreach (var (a, b, c) in src) {
                ret.Add(a, b, c);
            }
            return ret;
        }
    }
}
