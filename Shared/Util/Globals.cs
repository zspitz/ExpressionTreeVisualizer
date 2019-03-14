using ExpressionToString.Util;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ExpressionToString {
    public static class FormatterNames {
        public const string CSharp = "C#";
        public const string VisualBasic = "Visual Basic";
    }

    public static class Globals {
        public readonly static HashSet<MethodInfo> stringConcats;
        public readonly static HashSet<MethodInfo> stringFormats;

        static Globals() {
            var methods = typeof(string)
                .GetMethods()
                .Where(x => {
                    switch (x.Name) {
                        case "Concat":
                            return x.GetParameters().All(
                                y => y.ParameterType.In(typeof(string), typeof(string[]))
                            );
                        case "Format":
                            return x.GetParameters().First().ParameterType == typeof(string);
                        default: return false;
                    }
                })
                .ToLookup(x => x.Name);

            stringConcats = methods["Concat"].ToHashSet();
            stringFormats = methods["Format"].ToHashSet();
        }
    }
}
