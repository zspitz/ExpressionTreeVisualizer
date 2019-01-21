using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq.Expressions;
using System.Windows;
using System.Windows.Controls;

[assembly: DebuggerVisualizer(typeof(_testVisualizer.TestVisualizer), typeof(_testVisualizer.TestVisualizerDataObjectSource), Target = typeof(System.Linq.Expressions.Expression), Description = "Test Visualizer")]

namespace _testVisualizer {
    public class TestVisualizer : DialogDebuggerVisualizer {
        protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider) {
            var data = (TestVisualizerData)objectProvider.GetObject();
            var txt = new TextBlock();
            txt.SetBinding(TextBlock.TextProperty, "Status");
            var window = new Window {
                DataContext = data,
                Content = txt
            };
            window.ShowDialog();
        }
    }

    [Serializable]
    public class TestVisualizerData {
        public TestVisualizerData() { }
        public TestVisualizerData(System.Linq.Expressions.Expression expr) {
            Debugger.NotifyOfCrossThreadDependency();
            var workspace = new AdhocWorkspace();
            Status = "Success";
        }
        public string Status { get; set; }
    }

    public class TestVisualizerDataObjectSource : VisualizerObjectSource {
        public override void GetData(object target, Stream outgoingData) {
            var expr = (System.Linq.Expressions.Expression)target;
            var data = new TestVisualizerData(expr);
            base.GetData(data, outgoingData);
        }
    }
}

namespace _testVisualizer {
    class Program {
        [STAThread]
        static void Main(string[] args) {
            Expression<Func<bool>> expr = () => true;

            // This is required to load the Microsoft.VisualStudio.DebuggerVisualizers.DLL
            //var data = new TestVisualizerData(expr);
            var host = new VisualizerDevelopmentHost(null, typeof(TestVisualizer));
            //host.ShowVisualizer();

            Console.ReadKey(true);
        }
    }
}
