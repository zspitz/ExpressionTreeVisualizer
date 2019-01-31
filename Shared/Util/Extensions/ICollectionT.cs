using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ExpressionTreeTransform.Util {
    public static class ICollectionTExtensions {
        public static void AddRange<T>(this ICollection<T> dest, IEnumerable<T> toAdd) => toAdd.ForEach(x => dest.Add(x));
    }
}
