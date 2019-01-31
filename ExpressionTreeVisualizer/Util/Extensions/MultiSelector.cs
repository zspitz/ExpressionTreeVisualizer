using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls.Primitives;

namespace ExpressionTreeVisualizer.Util {
    public static class MultiSelectorExtensions {
        public static List<T> SelectedItems<T>(this MultiSelector multiSelector) => multiSelector.SelectedItems.Cast<T>().ToList();
    }
}
