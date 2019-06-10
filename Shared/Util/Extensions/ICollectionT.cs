using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ExpressionToString.Util {
    public static class ICollectionTExtensions {
        public static void AddRange<T>(this ICollection<T> dest, IEnumerable<T> toAdd) => toAdd.ForEach(x => dest.Add(x));

        public static void Add<T1, T2>(this ICollection<ValueTuple<T1, T2>> lst, T1 t1, T2 t2) => lst.Add((t1, t2));
    }
}
