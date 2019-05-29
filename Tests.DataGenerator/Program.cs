using ExpressionToString.Tests;
using System;
using System.Linq;
using Xunit;
using ExpressionToString.Util;
using System.Reflection;
using System.IO;

namespace Tests.DataGenerator {
    class Program {
        static void Main(string[] args) {
            var instances = new TestsBase[] {
                new CompilerGeneratedTestData(),
                new ConstructedTestData(),
                //new VBCompilerGeneratedTestData()
            };
            var methods = instances
                .SelectMany(x =>
                    x.GetType()
                    .GetMethods()
                    .Where(m => m.HasAttribute<FactAttribute>() && m.GetCustomAttribute<FactAttribute>().Skip.IsNullOrWhitespace())
                    .Select(m => (instance: x, method: m))
                )
                .OrderBy(x => x.method.ReflectedType.Name)
                .ThenBy(x => x.method.Name)
                .ToList();
            Runner.total = methods.Count;
            foreach (var (instance, method) in methods) {
                method.Invoke(instance, new object[] { });
            }

            Runner.lines.InsertRange(0, new [] {
                $"Count {Runner.lines.Count / 3}",
                ""
            });

            File.WriteAllLines("generated test data.txt", Runner.lines);
        }
    }
}
