using ExpressionTreeTransform;
using System;
using System.Linq;
using System.Linq.Expressions;
using OneOf;

namespace _tests {
    public static class _testsExtensions {
        public static void _testExtension(this OneOf<string, int, bool> arg) => throw new NotImplementedException();
    }

    class Program {
        static void Main(string[] args) {
            Expression<Func<string, string>> expr = x => x + x;
            var writer = new CSharpCodeWriter(expr, out var dict);
            Console.WriteLine(writer.ToString());
            foreach (var kvp in dict) {
                Console.WriteLine(kvp.Key.GetType());
                Console.WriteLine(kvp.Value);
                Console.WriteLine();
            }

            Console.ReadKey(true);
        }
    }
}
