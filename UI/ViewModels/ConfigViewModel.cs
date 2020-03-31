using ZSpitz.Util.Wpf;
using ExpressionTreeVisualizer.Serialization;

namespace ExpressionTreeVisualizer.UI {
    public class ConfigViewModel : ViewModelBase<Config> {
        private readonly Config _originalValues;
        public ConfigViewModel(Config config) : base(config.Clone()) {
            _originalValues = config;
        }

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

        public bool IsDirty {
            get {
                var o = _originalValues;
                var m = Model;
                return
                    o.Formatter != m.Formatter ||
                    o.Language != m.Language ||
                    o.Path != m.Path;
            }
        }
    }
}
