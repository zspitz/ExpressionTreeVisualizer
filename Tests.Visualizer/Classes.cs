using System;

namespace ExpressionToString.Tests.Visualizer {
    public class CompilerGenerated : CompilerGeneratedBase {
        protected override void RunTest(object o, string objectName) => Runner.RunTest(o);
    }
    public class Constructed : ConstructedBase {
        protected override void RunTest(object o, string objectName) => Runner.RunTest(o);
    }
    public class VBCompilerGenerated : VBCompilerGeneratedBase {
        protected override void RunTest(object o, string objectName) => Runner.RunTest(o);
    }
}
