using ExpressionTreeVisualizer;
using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static Microsoft.CodeAnalysis.LanguageNames;

namespace _visualizerTests {
    class Program {
        [STAThread]
        static void Main(string[] args) {
            //var i = 7;
            //var j = 8;
            //Expression<Func<int>> expr = () => i + j;

            //Expression<Func<bool>> expr = () => i * j <= 25 || new DateTime(1, 1, 1981).Year >= j && new { DateTime.Now }.Now.Day > 10;

            //Expression<Func<int, int>> expr = x => Enumerable.Range(1, x).Select(y => x * y).Count();

            //var expr1 = Lambda(Constant(new DateTime(1980, 1, 1)));

            //Expression<Func<Foo>> expr = () => new Foo("baz") { Bar = "bar" };

            // Expression<Func<List<string>>> expr = () => new List<string> { "abcd", "defg" };

            Expression<Func<Wrapper>> expr = () => new Wrapper {
                {",","2"},
                "1",
                {"3","4"}
            };

            var data = new VisualizerData(expr, VisualBasic);
            var visualizerHost = new VisualizerDevelopmentHost(data, typeof(Visualizer));
            visualizerHost.ShowVisualizer();

            Console.ReadKey(true);
        }
    }

    class Foo {
        public string Bar { get; set; }
        public Foo(string baz) {

        }
    }

    class Wrapper : List<string> {
        public void Add(string s1, string s2) => throw new NotImplementedException();
        public void Add(string s1, string s2, string s3) => throw new NotImplementedException();
    }
}
