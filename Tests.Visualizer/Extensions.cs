using System;
using System.Collections.Generic;
using Xunit;

namespace ExpressionToString.Tests.Visualizer {
    internal static class Extensions {
        internal static TheoryData<T1, T2> ToTheoryData<T1, T2>(this IEnumerable<ValueTuple<T1, T2>> src) {
            var ret = new TheoryData<T1, T2>();
            foreach (var (a, b) in src) {
                ret.Add(a, b);
            }
            return ret;
        }
    }
}
