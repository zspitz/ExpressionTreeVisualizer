using ExpressionTreeTransform;
using System;
using System.Linq;
using System.Linq.Expressions;
using OneOf;
using System.Collections.Generic;
using static System.Linq.Expressions.Expression;

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

            var lst = new List<object>();
            Expression expr1 = MakeMemberAccess(Constant(new List<object>()), typeof(List<object>).GetMember("Count").First());
            writer = new CSharpCodeWriter(expr1);
            Console.WriteLine(writer.ToString());

            Console.ReadKey(true);
        }
    }
}
