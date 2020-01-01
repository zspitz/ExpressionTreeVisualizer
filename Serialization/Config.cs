using System;
using static ExpressionTreeToString.FormatterNames;
using static System.IO.Path;

namespace ExpressionTreeVisualizer.Serialization {
    [Serializable]
    public class Config {
        public string Formatter { get; set; } = CSharp;
        public string Language { get; set; } = CSharp;
        public string? Path { get; set; }

        public Config Clone() => new Config {
            Formatter = Formatter,
            Language = Language,
            Path = Path
        };
    }
}
