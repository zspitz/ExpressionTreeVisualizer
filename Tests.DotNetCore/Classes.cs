using System;
using Xunit;

namespace ExpressionToString.Tests {
    [Collection("Expected data from file")]
    public class CompilerGenerated : CompilerGeneratedBase {
        [Obsolete]
        protected override void RunTest(object o, string objectName) => Runner.RunTest(o, objectName, fixture);

        public CompilerGenerated(ExpectedDataFixture fixture) => this.fixture = fixture;
        ExpectedDataFixture fixture;
    }

    [Collection("Expected data from file")]
    public class Constructed : ConstructedBase {
        [Obsolete]
        protected override void RunTest(object o, string objectName) => Runner.RunTest(o, objectName, fixture);

        public Constructed(ExpectedDataFixture fixture) => this.fixture = fixture;
        ExpectedDataFixture fixture;
    }

    [Collection("Expected data from file")]
    public class VBCompilerGenerated : VBCompilerGeneratedBase {
        [Obsolete]
        protected override void RunTest(object o, string objectName) => Runner.RunTest(o, objectName, fixture);

        public VBCompilerGenerated(ExpectedDataFixture fixture) => this.fixture = fixture;
        ExpectedDataFixture fixture;
    }
} 
