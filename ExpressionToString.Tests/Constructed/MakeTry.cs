using System;
using System.Linq.Expressions;
using Xunit;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Runners;
using static System.Linq.Expressions.Expression;

namespace ExpressionToString.Tests.Constructed {
    [Trait("Source", FactoryMethods)]
    public class MakeTry {
        Type exceptionType = typeof(InvalidCastException);
        ParameterExpression ex = Parameter(typeof(Exception), "ex");

        [Fact]
        public void ConstructSimpleCatch() => RunTest(
            Catch(typeof(Exception), writeLineTrue),
                @"catch {
    Console.WriteLine(true);
}",
                @"Catch
    Console.WriteLine(True)"
        );

        [Fact]
        public void ConstructCatchSingleStatement() => RunTest(
            Catch(ex, writeLineTrue),
            @"catch (Exception ex) {
    Console.WriteLine(true);
}",
            @"Catch ex As Exception
    Console.WriteLine(True)"
        );

        [Fact]
        public void ConstructCatchMultiStatement() => RunTest(
            Catch(ex, Block(writeLineTrue, writeLineTrue)),
            @"catch (Exception ex) {
    Console.WriteLine(true);
    Console.WriteLine(true);
}",
            @"Catch ex As Exception
    Console.WriteLine(True)
    Console.WriteLine(True)"
        );

        [Fact]
        public void ConstructCatchSingleStatementWithType() => RunTest(
            Catch(exceptionType, writeLineTrue),
            @"catch (InvalidCastException) {
    Console.WriteLine(true);
}",
            @"Catch _ As InvalidCastException
    Console.WriteLine(True)"
        );

        [Fact]
        public void ConstructCatchMultiStatementWithType() => RunTest(
            Catch(exceptionType, Block(writeLineTrue, writeLineTrue)),
            @"catch (InvalidCastException) {
    Console.WriteLine(true);
    Console.WriteLine(true);
}",
            @"Catch _ As InvalidCastException
    Console.WriteLine(True)
    Console.WriteLine(True)"
        );

        [Fact]
        public void ConstructCatchSingleStatementWithFilter() => RunTest(
            Catch(ex, writeLineTrue, Constant(true)),
            @"catch (Exception ex) when (true) {
    Console.WriteLine(true);
}",
            @"Catch ex As Exception When True
    Console.WriteLine(True)"
        );

        [Fact]
        public void ConstructCatchMultiStatementWithFilter() => RunTest(
            Catch(ex, Block(writeLineTrue, writeLineTrue), Constant(true)),
            @"catch (Exception ex) when (true) {
    Console.WriteLine(true);
    Console.WriteLine(true);
}",
            @"Catch ex As Exception When True
    Console.WriteLine(True)
    Console.WriteLine(True)"
        );

        [Fact]
        public void ConstructCatchWithMultiStatementFilter() => RunTest(
            Catch(ex, writeLineTrue, Block(Constant(true), Constant(true))),
            @"catch (Exception ex) when ({
    true;
    true;
}) {
    Console.WriteLine(true);
}",
            @"Catch ex As Exception When Block
    True
    True
End Block
    Console.WriteLine(True)"
        );

        [Fact]
        public void ConstructTryCatch() => RunTest(
            TryCatch(Constant(true),Catch(typeof(Exception), Constant(true))),
            @"try {
    true;
} catch {
    true;
}",
            @"Try
    True
Catch
    True
End Try"
        );

        [Fact]
        public void ConstructTryCatchFinally() => RunTest(
            TryCatchFinally(Constant(true), writeLineTrue, Catch(ex, Constant(true))),
            @"try {
    true;
} catch (Exception ex) {
    true;
} finally {
    Console.WriteLine(true);
}",
            @"Try
    True
Catch ex As Exception
    True
Finally
    Console.WriteLine(True)
End Try"
        );

        [Fact]
        public void ConstructTryFault() => RunTest(
            TryFault(writeLineTrue, writeLineTrue),
            @"try {
    Console.WriteLine(true);
} fault {
    Console.WriteLine(true);
}",
            @"Try
    Console.WriteLine(True)
Fault
    Console.WriteLine(True)
End Try"
        );

        [Fact]
        public void ConstructTryFinally() => RunTest(
            TryFinally(writeLineTrue, writeLineTrue),
            @"try {
    Console.WriteLine(true);
} finally {
    Console.WriteLine(true);
}",
            @"Try
    Console.WriteLine(True)
Finally
    Console.WriteLine(True)
End Try"
        );
    }
}
