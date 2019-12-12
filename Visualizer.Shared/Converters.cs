using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using static System.Windows.DependencyProperty;
using ExpressionToString.Util;
using static System.Windows.Visibility;

namespace ExpressionTreeVisualizer {
    public abstract class ReadOnlyConverterBase : IValueConverter {
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => UnsetValue;
    }
    public abstract class ReadOnlyMultiConverterBase : IMultiValueConverter {
        public abstract object Convert(object[] values, Type targetType, object parameter, CultureInfo culture);
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => new[] { UnsetValue };
    }

    public class RootConverter : ReadOnlyConverterBase {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => new[] { value };
    }

    public class ConditionalFormatConverter : ReadOnlyConverterBase {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var sValue = value as string;
            if (sValue.IsNullOrWhitespace()) { return value; }
            return value.Formatted((string)parameter);
        }
    }

    public class AnyVisibilityConverter : ReadOnlyConverterBase {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if (((IEnumerable)value).Any()) { return Visible; }
            return Collapsed;
        }
    }

    public class TitleConverter : ReadOnlyMultiConverterBase {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            var formatter = values[0] as string;
            var language = values[1] as string;
            var path = values[2] as string;
            var parts = new List<(string? name, string? value)> {
                {"Formatter", formatter }
            };
            if (formatter != language) {
                parts.Add("Language", language);
            }
            if (!path.IsNullOrWhitespace()) {
                parts.Add("Path", path);
            }
            return parts.SelectT((name, val) => $"{name}: {val}").Joined(", ");
        }
    }
}
