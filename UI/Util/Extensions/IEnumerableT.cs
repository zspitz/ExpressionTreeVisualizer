using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTreeVisualizer.Util {
public static class IEnumerableTExtensions {
        public static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> src) => new ReadOnlyCollection<T>(src.ToList());
    }
}
