using System.Diagnostics;
using ExpressionTreeVisualizer.Serialization;

[assembly: DebuggerVisualizer(
    visualizer: typeof(ExpressionTreeVisualizer.Visualizer),
    visualizerObjectSource: typeof(ExpressionTreeVisualizer.VisualizerDataObjectSource),
    Target = typeof(System.Linq.Expressions.Expression),
    Description = "Expression Tree Visualizer")]

[assembly: DebuggerVisualizer(
    visualizer: typeof(ExpressionTreeVisualizer.Visualizer),
    visualizerObjectSource: typeof(ExpressionTreeVisualizer.VisualizerDataObjectSource),
    Target = typeof(System.Linq.Expressions.ElementInit),
    Description = "Expression Tree Visualizer")]

[assembly: DebuggerVisualizer(
    visualizer: typeof(ExpressionTreeVisualizer.Visualizer),
    visualizerObjectSource: typeof(ExpressionTreeVisualizer.VisualizerDataObjectSource),
    Target = typeof(System.Linq.Expressions.MemberBinding),
    Description = "Expression Tree Visualizer")]

[assembly: DebuggerVisualizer(
    visualizer: typeof(ExpressionTreeVisualizer.Visualizer),
    visualizerObjectSource: typeof(ExpressionTreeVisualizer.VisualizerDataObjectSource),
    Target = typeof(System.Linq.Expressions.SwitchCase),
    Description = "Expression Tree Visualizer")]

[assembly: DebuggerVisualizer(
    visualizer: typeof(ExpressionTreeVisualizer.Visualizer),
    visualizerObjectSource: typeof(ExpressionTreeVisualizer.VisualizerDataObjectSource),
    Target = typeof(System.Linq.Expressions.CatchBlock),
    Description = "Expression Tree Visualizer")]

[assembly: DebuggerVisualizer(
    visualizer: typeof(ExpressionTreeVisualizer.Visualizer),
    visualizerObjectSource: typeof(ExpressionTreeVisualizer.VisualizerDataObjectSource),
    Target = typeof(System.Linq.Expressions.LabelTarget),
    Description = "Expression Tree Visualizer")]

namespace ExpressionTreeVisualizer {
    public abstract class VisualizerWindowBase : Periscope.VisualizerWindowBase<VisualizerWindow, Config> { }

    public class Visualizer : Periscope.VisualizerBase<VisualizerWindow, Config> {
        public override Config GetInitialConfig() => new Config() { Formatter = "C#" };
    }
}
