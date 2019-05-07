using System;

namespace ExpressionToString.Tests {
    public class CompilerGenerated : CompilerGeneratedBase {
        protected override void RunTest(object o, string csharp, string vb) => Runner.RunTest(o, csharp, vb);
    }
    public class Constructed : ConstructedBase {
        protected override void RunTest(object o, string csharp, string vb) => Runner.RunTest(o, csharp, vb);
    }
    public class VBCompilerGenerated : VBCompilerGeneratedBase {
        protected override void RunTest(object o, string csharp, string vb) => Runner.RunTest(o, csharp, vb);
    }
} 
