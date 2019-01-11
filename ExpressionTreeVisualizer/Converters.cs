using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using static System.Windows.DependencyProperty;
using ExpressionTreeTransform.Util;

namespace ExpressionTreeVisualizer {
    public abstract class ReadOnlyConverterBase : IValueConverter {
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => UnsetValue;
    }
    
    public class RootConverter : ReadOnlyConverterBase {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => new[] { new KeyValuePair<string, object>("", value) };
    }

    public class ConditionalFormatConverter : ReadOnlyConverterBase {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var sValue = value as string;
            if (sValue.IsNullOrWhitespace()) { return value; }
            return value.Formatted(parameter as string);
        }
    }
}
