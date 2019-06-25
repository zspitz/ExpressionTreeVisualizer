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
            return value.Formatted(parameter as string);
        }
    }

    public class AnyVisibilityConverter : ReadOnlyConverterBase {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            if ((value as IEnumerable).Any()) { return Visible; }
            return Collapsed;
        }
    }

    public class TitleConverter : ReadOnlyMultiConverterBase {
        public override object Convert(object[] values, Type targetType, object parameter, CultureInfo culture) {
            var formatter = values[0] as string;
            var language = values[1] as string;
            if (formatter == language) { return formatter; }
            return $"Formatter: {formatter}, Language: {language}";
        }
    }
}
