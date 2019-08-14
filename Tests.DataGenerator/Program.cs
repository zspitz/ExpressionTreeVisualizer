using ExpressionToString.Tests;
using System;
using System.Linq;
using Xunit;
using ExpressionToString.Util;
using System.Reflection;
using System.IO;
using static ExpressionToString.Tests.Functions;
using System.Collections.Generic;
using System.Linq.Expressions;
using static ExpressionToString.FormatterNames;
using ExpressionToString;

namespace Tests.DataGenerator {
    class Program {
        static void Main(string[] args) {
            RegisterTestObjectContainer(typeof(ExpressionToString.Tests.Objects.VBCompiler));

            var formatter = TextualTree;
            var language = CSharp;
            var objects = GetObjects();

            var lines = new List<string>();
            GetObjects().ForEachT((o, objectName, category) => {
                lines.Add($"---- {objectName}");

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
            });

            File.WriteAllLines("generated test data.txt", lines);
        }
    }
}
