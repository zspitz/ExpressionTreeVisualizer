using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ExpressionTreeVisualizer.Util {
    public static class MultiSelectTreeViewExtensions {
        public static List<T> SelectedItems<T>(this MultiSelectTreeView treeview) => treeview.SelectedItems.Cast<T>().ToList();
    }
}
