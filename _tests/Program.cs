using ExpressionTreeTransform;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace _tests {
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
