using System;
using Xunit;
using static ExpressionToString.Tests.Globals;

namespace ExpressionToString.Tests {
    [Obsolete]
    [Trait("Source", FactoryMethods)]
    [Trait("Type", "Expression object")] // TODO ideally this would be on the base class, but https://github.com/xunit/xunit/issues/1397
    public abstract partial class ConstructedBase : TestsBase { }
}
