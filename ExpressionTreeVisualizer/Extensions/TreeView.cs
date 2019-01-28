using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ExpressionTreeVisualizer.Util {
    public static class TreeViewExtensions {
        public static T SelectedItem<T>(this TreeView treeview) => (T)treeview.SelectedItem;
    }
}
