namespace ExpressionToString.Tests {
    public class CompilerGenerated : CompilerGeneratedBase {
        protected override void RunTest(object o, string csharp, string vb, string factoryMethods) => Runner.RunTest(o, csharp, vb, factoryMethods);
    }
    public class Constructed : ConstructedBase {
        protected override void RunTest(object o, string csharp, string vb, string factoryMethods) => Runner.RunTest(o, csharp, vb, factoryMethods);
    }
    public class VBCompilerGenerated : VBCompilerGeneratedBase {
        protected override void RunTest(object o, string csharp, string vb, string factoryMethods) => Runner.RunTest(o, csharp, vb, factoryMethods);
    }
} 
