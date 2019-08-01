using System;

namespace ExpressionToString.Tests {
    public class CompilerGenerated : CompilerGeneratedBase {
        [Obsolete] protected override void RunTest(object o, string csharp, string vb, string factoryMethods) => Runner.RunTest(o, csharp, vb, factoryMethods);
        protected override void RunTest(object o, string objectName) => Runner.RunTest(o, objectName);
    }
    public class Constructed : ConstructedBase {
        [Obsolete] protected override void RunTest(object o, string csharp, string vb, string factoryMethods) => Runner.RunTest(o, csharp, vb, factoryMethods);
        protected override void RunTest(object o, string objectName) => Runner.RunTest(o, objectName);
    }
    public class VBCompilerGenerated : VBCompilerGeneratedBase {
        [Obsolete] protected override void RunTest(object o, string csharp, string vb, string factoryMethods) => Runner.RunTest(o, csharp, vb, factoryMethods);
        protected override void RunTest(object o, string objectName) => Runner.RunTest(o, objectName);
    }
} 
