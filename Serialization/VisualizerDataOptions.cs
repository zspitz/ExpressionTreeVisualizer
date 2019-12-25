using System;
using System.ComponentModel;
using static ExpressionTreeToString.Util.Functions;
using static ExpressionTreeToString.FormatterNames;
using ExpressionTreeVisualizer.Serialization.Util;

namespace ExpressionTreeVisualizer.Serialization {
    [Serializable]
    public class VisualizerDataOptions : INotifyPropertyChanged {
        private string _formatter = CSharp;
        public string Formatter {
            get => _formatter;
            set {
                Language = ResolveLanguage(value);
                this.NotifyChanged(ref _formatter, value, args => PropertyChanged?.Invoke(this, args));
            }
        }

        private string _language = CSharp;
        public string Language {
            get => _language;
            set => this.NotifyChanged(ref _language, value, args => PropertyChanged?.Invoke(this, args));
        }

        public string? Path { get; set; }

        [field: NonSerialized]
        public event PropertyChangedEventHandler? PropertyChanged;

        public VisualizerDataOptions(VisualizerDataOptions? options = null) {
            if (options is { }) {
                _formatter = options.Formatter;
                _language = options.Language;
                Path = options.Path;
            }
        }
    }

}
