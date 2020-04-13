using ExpressionTreeVisualizer.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using ZSpitz.Util;
using ZSpitz.Util.Wpf;
using static System.Windows.Visibility;

namespace ExpressionTreeVisualizer {
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

    public class NotNullToVisibilityConverter : ReadOnlyConverterBase {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) => 
            value is null ? Collapsed : Visible;
    }
}
