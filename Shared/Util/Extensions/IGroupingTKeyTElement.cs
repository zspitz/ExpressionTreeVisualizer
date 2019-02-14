using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ExpressionToString.Util {
    public static class IGroupingTKeyTElementExtensions {
        public static Dictionary<TKey, List<TElement>> ToDictionaryList<TKey, TElement>(IEnumerable<IGrouping<TKey, TElement>> groups) => groups.ToDictionary(group => group.Key, group => group.ToList());
    }
}
