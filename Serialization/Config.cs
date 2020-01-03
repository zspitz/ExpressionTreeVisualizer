using System;
using static ExpressionTreeToString.FormatterNames;

namespace ExpressionTreeVisualizer.Serialization {
    [Serializable]
    public class Config {
        private string formatter = CSharp;
        public string Formatter {
            get => formatter;
            set {
                formatter = value;
                if (value == VisualBasic || value == CSharp) {
                    Language = value;
                }
            }
        }

        public string Language { get; set; } = CSharp;
        public string? Path { get; set; }

        public Config Clone() => new Config {
            Formatter = Formatter,
            Language = Language,
            Path = Path
        };
    }
}
