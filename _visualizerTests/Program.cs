using ExpressionTreeVisualizer;
using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.CodeAnalysis.LanguageNames;
using static System.Linq.Expressions.Expression;

namespace _visualizerTests {
    class Program {
        [STAThread]
        static void Main(string[] args) {
            var i = 7;
            var j = 8;
            //Expression<Func<int>> expr = () => i + j;

            Expression<Func<bool>> expr = () => i * j <= 25 || new DateTime(1,1,1981).Year >= j;

            //var expr1 = Lambda(Constant(new DateTime(1980, 1, 1)));

            var data = new VisualizerData(expr, CSharp);
            var visualizerHost = new VisualizerDevelopmentHost(data, typeof(Visualizer));
            visualizerHost.ShowVisualizer();
        }
    }
}
