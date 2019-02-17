using ExpressionTreeVisualizer;
using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace _visualizerTests {
    class Program {
        [STAThread]
        static void Main(string[] args) {
            //var i = 7;
            //var j = 8;

            //Expression<Func<int, string, bool>> expr = (i, s) => (i * i * i + 15) >= 10 && s.Length <= 25 || (Math.Pow(j, 3) > 100 && j + 15 < 100);

            //var i = 5;
            //Expression<Func<int, int>> expr = j => (i + j + 17) * (i + j + 17);

            //Expression<Func<bool>> expr = () => true;

            //Expression<Func<string, int, string>> expr = (s, i) => $"{s}, {i}";

            //Expression<Func<object[]>> expr = () => new object[] { "" };

            //Expression<Func<string[][]>> expr = () => new string[5][];

            //Expression<Func<int, int, string>> expr = (i, j) => (i + j + 5).ToString();

            //var lst = new List<string>();
            //Expression<Func<string>> expr = () => lst[5];

            //var arr = new string[,][] { };
            //Expression<Func<string>> expr = () => arr[5, 2][7];

            //Func<int> del = () => DateTime.Now.Day;
            //Expression<Func<int>> expr = () => del();

            //Expression<Func<Foo>> expr = () => new Foo("ijkl") { Bar = "abcd", Baz = "efgh" };
            //var binding = ((MemberInitExpression)expr.Body).Bindings[0];

            Expression<Func<Wrapper>> expr = () => new Wrapper { { "ab", "cd" }, "ef" };

            var visualizerHost = new VisualizerDevelopmentHost(expr, typeof(Visualizer), typeof(VisualizerDataObjectSource));
            visualizerHost.ShowVisualizer();

            //Console.ReadKey(true);
        }
    }

    class Foo {
        public string Bar { get; set; }
        public string Baz { get; set; }
        public Foo() { }
        public Foo(string baz) { }
    }

    class Wrapper : List<string> {
        public void Add(string s1, string s2) => throw new NotImplementedException();
    }
}