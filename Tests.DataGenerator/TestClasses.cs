using ExpressionToString.Tests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.DataGenerator {
    class CompilerGeneratedTestData : CompilerGeneratedBase {
        [Obsolete] protected override void RunTest(object o, string csharp, string vb, string factoryMethods) => Runner.WriteData(o, factoryMethods);
        protected override void RunTest(object o, string objectName) { }
    }

    class ConstructedTestData : ConstructedBase {
        [Obsolete] protected override void RunTest(object o, string csharp, string vb, string factoryMethods) => Runner.WriteData(o, factoryMethods);
        protected override void RunTest(object o, string objectName) { }
    }
    class VBCompilerGeneratedTestData : VBCompilerGeneratedBase {
        [Obsolete] protected override void RunTest(object o, string csharp, string vb, string factoryMethods) => Runner.WriteData(o, factoryMethods);
        protected override void RunTest(object o, string objectName) { }
    }
}
