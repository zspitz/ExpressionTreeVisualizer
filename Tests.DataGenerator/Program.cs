using ExpressionToString;
using ExpressionTreeTestObjects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using static ExpressionToString.FormatterNames;

namespace Tests.DataGenerator {
    class Program {
        static void Main(string[] args) {
            var formatter = TextualTree;
            var language = CSharp;

            var lines = new List<string>();
            foreach (var (category, source, name, o) in Objects.Get()) {
                lines.Add($"---- ${source}.{name}");

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
            }

            lines.Add("------");

            File.WriteAllLines("generated test data.txt", lines);
        }
    }
}
