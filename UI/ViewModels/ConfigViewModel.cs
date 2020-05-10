using ZSpitz.Util.Wpf;
using ExpressionTreeVisualizer.Serialization;

namespace ExpressionTreeVisualizer {
    public class ConfigViewModel : ViewModelBase<Config> {
        public ConfigViewModel(Config config) : base(config) { }

        public string Formatter {
            get => Model.Formatter;
            set {
                var prevLanguage = Model.Language;
                NotifyChanged(Model.Formatter, value, () => Model.Formatter = value);
                NotifyChanged(prevLanguage, Language, null, "Language");
            }
        }

        public string Language {
            get => Model.Language;
            set => NotifyChanged(Model.Language, value, () => Model.Language = value);
        }
    }
}
