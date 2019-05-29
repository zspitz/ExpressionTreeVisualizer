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
        private static int counter = 0;
        public static readonly List<string> lines = new List<string>();
        public static void WriteData(object o, string testData) {
            string toWrite;
            switch (o) {
                case Expression expr:
                    toWrite = expr.ToString(FactoryMethods);
                    break;
                case MemberBinding mbind:
                    toWrite = mbind.ToString(FactoryMethods);
                    break;
                case ElementInit init:
                    toWrite = init.ToString(FactoryMethods);
                    break;
                case SwitchCase switchCase:
                    toWrite = switchCase.ToString(FactoryMethods);
                    break;
                case CatchBlock catchBlock:
                    toWrite = catchBlock.ToString(FactoryMethods);
                    break;
                case LabelTarget labelTarget:
                    toWrite = labelTarget.ToString(FactoryMethods);
                    break;
                default:
                    throw new NotImplementedException();
            }

            if (testData != toWrite) {
                counter += 1;
                lines.AddRange(new[] {
                    ",@\"" + toWrite + "\"",
                    $"{TestMethodName()}",
                    ""
                });
            }

            string TestMethodName() {
                var mi = new StackTrace().GetFrames().Select(x => x.GetMethod()).FirstOrDefault(x => x.DeclaringType.BaseType == typeof(TestsBase));
                return $"{mi.ReflectedType.Name}.{mi.Name}";
            }
        }
    }
}
