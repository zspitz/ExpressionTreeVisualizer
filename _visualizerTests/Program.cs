using ExpressionTreeVisualizer;
using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.CodeAnalysis.LanguageNames;

namespace _visualizerTests {
    class Program {
        [STAThread]
        static void Main(string[] args) {
            var i = 7;
            var j = 8;
            Expression<Func<bool>> expr = () => i * j <= 25;

            var data = new VisualizerData(expr, CSharp);

            var visualizerHost = new VisualizerDevelopmentHost(data, typeof(Visualizer));
            visualizerHost.ShowVisualizer();
        }
    }
}
