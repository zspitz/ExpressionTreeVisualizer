using System;
using System.Diagnostics.CodeAnalysis;

namespace ExpressionTreeVisualizer.Serialization {
    [Serializable]
    [SuppressMessage("", "IDE0032", Justification = "https://github.com/dotnet/core/issues/2981")]
    public struct EndNodeData {
        private string? _closure;
        private string? _name;
        private string? _type;
        private string? _value;

        public string? Closure {
            get => _closure;
            set => _closure = value;
        }
        public string? Name {
            get => _name;
            set => _name = value;
        }
        public string? Type {
            get => _type;
            set => _type = value;
        }
        public string? Value {
            get => _value;
            set => _value = value;
        }
    }
}
