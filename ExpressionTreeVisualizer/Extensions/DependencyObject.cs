using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace ExpressionTreeVisualizer.Util {
    public static class DependencyObjectExtensions {
        public static IEnumerable<T> FindVisualChildren<T>(this DependencyObject o) where T : DependencyObject {
            if (o == null) { yield break; }
            foreach (var child in Enumerable.Range(0, VisualTreeHelper.GetChildrenCount(o)).Select(index => VisualTreeHelper.GetChild(o, index))) {
                var childAsT = child as T;
                if (childAsT != null) { yield return childAsT; }
                foreach (var descendant in FindVisualChildren<T>(child)) {
                    yield return descendant;
                }
            }
        }
    }
}
