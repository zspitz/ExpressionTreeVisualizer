using ExpressionToString.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.DataGenerator {
    class CompilerGeneratedTestData : CompilerGeneratedBase {
        protected override void RunTest(object o, string csharp, string vb, string factoryMethods) => Runner.WriteData(o, factoryMethods);
    }
    class ConstructedTestData : ConstructedBase {
        protected override void RunTest(object o, string csharp, string vb, string factoryMethods) => Runner.WriteData(o, factoryMethods);
    }
    class VBCompilerGeneratedTestData : VBCompilerGeneratedBase {
        protected override void RunTest(object o, string csharp, string vb, string factoryMethods) => Runner.WriteData(o, factoryMethods);
    }
}
