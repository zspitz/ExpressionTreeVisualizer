using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using ExpressionToString;
using ExpressionToString.Tests;
using ExpressionToString.Util;
using static ExpressionToString.FormatterNames;

namespace Tests.DataGenerator {
    public static class Runner {
        public static int total = 0;
        private static string formatter = ObjectNotation;
        private static string language = CSharp;
        public static readonly List<string> lines = new List<string>();
        //public static NodeTypeExpressionTypeMapper visitor = new NodeTypeExpressionTypeMapper();

        private static Dictionary<string, string> typenameMapping = new[] {
                ("CompilerGeneratedBase", "CSCompiler"),
                ("ConstructedBase","FactoryMethods"),
                ("VBCompilerGeneratedBase","VBCompiler")
            }.ToDictionary();
        [Obsolete] public static void WriteData(object o, string testData) {
            lines.Add($"---- {TestMethodName()}");

            string toWrite;
            switch (o) {
                case Expression expr:
                    toWrite = expr.ToString(formatter, language);
                    break;
                case MemberBinding mbind:
                    toWrite = mbind.ToString(formatter, language);
                    break;
                case ElementInit init:
                    toWrite = init.ToString(formatter, language);
                    break;
                case SwitchCase switchCase:
                    toWrite = switchCase.ToString(formatter, language);
                    break;
                case CatchBlock catchBlock:
                    toWrite = catchBlock.ToString(formatter, language);
                    break;
                case LabelTarget labelTarget:
                    toWrite = labelTarget.ToString(formatter, language);
                    break;
                default:
                    throw new NotImplementedException();
            }
            lines.Add(toWrite);

            //lines.Add(testData);

            //visitor.VisitExt(o);

            string TestMethodName() {
                var mi = new StackTrace().GetFrames().Select(x => x.GetMethod()).FirstOrDefault(x => x.DeclaringType.BaseType == typeof(TestsBase));
                return $"{typenameMapping[mi.ReflectedType.Name]}.{mi.Name}";
            }
        }
    }
}
