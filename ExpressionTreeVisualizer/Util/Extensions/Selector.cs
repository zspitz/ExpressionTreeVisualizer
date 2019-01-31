using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace ExpressionTreeVisualizer.Util {
    public static class SelectorExtensions {
        public static T SelectedItem<T>(this Selector selector) => (T)selector.SelectedItem;
    }
}
