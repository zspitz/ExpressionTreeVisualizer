// based on https://github.com/carbonrobot/wpf-autogrid/blob/master/source/WpfAutoGrid/DependencyExtensions.cs

using System.Windows;

namespace WpfAutoGrid {
    public static class DependencyExtensions {
        /// <summary>
        /// Sets the value of the <paramref name="property"/> only if it hasn't been explicitly set.
        /// </summary>
        public static bool SetIfDefault<T>(this DependencyObject o, DependencyProperty property, T value) {
            if (DependencyPropertyHelper.GetValueSource(o, property).BaseValueSource == BaseValueSource.Default) {
                o.SetValue(property, value);
                return true;
            }
            return false;
        }
    }
}
