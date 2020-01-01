using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpressionTreeVisualizer.Util;
using ExpressionTreeVisualizer.Serialization;
using static ExpressionTreeToString.Util.Functions;

namespace ExpressionTreeVisualizer.UI {
    public class ConfigViewModel : ViewModelBase<Config> {
        public ConfigViewModel(Config model) : base(model) { }

        public string Formatter {
            get => Model.Formatter;
            set {
                Language = ResolveLanguage(value);
                NotifyChanged(Model.Formatter, value, () => Model.Formatter = value);
            }
        }

        public string Language {
            get => Model.Language;
            set => NotifyChanged(Model.Language, value, () => Model.Language = value);
        }
    }
}
