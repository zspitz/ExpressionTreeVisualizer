namespace ExpressionToString.Tests.Visualizer {
    public class CompilerGenerated : CompilerGeneratedBase {
        protected override void RunTest(object o, string csharp, string vb, string factoryMethods) => Runner.RunTest(o);
    }
    public class Constructed : ConstructedBase {
        protected override void RunTest(object o, string csharp, string vb, string factoryMethods) => Runner.RunTest(o);
    }
    public class VBCompilerGenerated : VBCompilerGeneratedBase {
        protected override void RunTest(object o, string csharp, string vb, string factoryMethods) => Runner.RunTest(o);
    }
}
