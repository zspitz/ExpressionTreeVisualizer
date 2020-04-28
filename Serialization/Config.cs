using System;
using static ExpressionTreeToString.FormatterNames;

namespace ExpressionTreeVisualizer.Serialization {
    [Serializable]
#if VISUALIZER_DEBUGGEE
    public class Config : Periscope.Debuggee.ConfigBase<Config> {
#else
    public class Config {
#endif
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

#if VISUALIZER_DEBUGGEE
        public override bool NeedsTransferData(Config original) => 
            original.Formatter != Formatter || 
            original.Language != Language || 
            original.Path != Path;

        public override Config Clone() =>
#else
        public Config Clone() => 
#endif
            new Config {
                Formatter = Formatter,
                Language = Language,
                Path = Path
            };

        public void Deconstruct(out string formatter, out string language, out string? path) {
            formatter = Formatter;
            language = Language;
            path = Path;
        }
    }
}
