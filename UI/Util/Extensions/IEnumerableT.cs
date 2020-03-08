using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTreeVisualizer.Util {
public static class IEnumerableTExtensions {
        public static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> src) => new ReadOnlyCollection<T>(src.ToList());

        public static IEnumerable<T> SelectMany<T>(this IEnumerable<IEnumerable<T>> src) => src.SelectMany(x => x);
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> src, Action<T> action) {
            foreach (var item in src) {
                action(item);
            }
            return src;
        }
    }
}
