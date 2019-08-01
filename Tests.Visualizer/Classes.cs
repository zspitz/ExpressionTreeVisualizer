using System;

namespace ExpressionToString.Tests.Visualizer {
    public class CompilerGenerated : CompilerGeneratedBase {
        [Obsolete] protected override void RunTest(object o, string csharp, string vb, string factoryMethods) => Runner.RunTest(o);
        protected override void RunTest(object o, string objectName) => Runner.RunTest(o);
    }
    public class Constructed : ConstructedBase {
        [Obsolete] protected override void RunTest(object o, string csharp, string vb, string factoryMethods) => Runner.RunTest(o);
        protected override void RunTest(object o, string objectName) => Runner.RunTest(o);
    }
    public class VBCompilerGenerated : VBCompilerGeneratedBase {
        [Obsolete] protected override void RunTest(object o, string csharp, string vb, string factoryMethods) => Runner.RunTest(o);
        protected override void RunTest(object o, string objectName) => Runner.RunTest(o);
    }
}
