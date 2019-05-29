using System;
using System.Linq.Expressions;
using Xunit;
using static ExpressionToString.Tests.Globals;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        Type exceptionType = typeof(InvalidCastException);
        ParameterExpression ex = Parameter(typeof(Exception), "ex");

        [Fact]
        [Trait("Category", Try)]
        public void ConstructSimpleCatch() => RunTest(
            Catch(typeof(Exception), writeLineTrue),
                @"catch {
    Console.WriteLine(true);
}",
                @"Catch
    Console.WriteLine(True)",
                @"Catch(
    typeof(Exception),
    Call(
        typeof(Console).GetMethod(""WriteLine""),
        new[] {
            Constant(true)
        }
    )
)"
        );

        [Fact]
        [Trait("Category", Try)]
        public void ConstructCatchSingleStatement() => RunTest(
            Catch(ex, writeLineTrue),
            @"catch (Exception ex) {
    Console.WriteLine(true);
}",
            @"Catch ex As Exception
    Console.WriteLine(True)",
            @"Catch(ex,
    Call(
        typeof(Console).GetMethod(""WriteLine""),
        new[] {
            Constant(true)
        }
    )
)"
        );

        [Fact]
        [Trait("Category", Try)]
        public void ConstructCatchMultiStatement() => RunTest(
            Catch(ex, Block(writeLineTrue, writeLineTrue)),
            @"catch (Exception ex) {
    Console.WriteLine(true);
    Console.WriteLine(true);
}",
            @"Catch ex As Exception
    Console.WriteLine(True)
    Console.WriteLine(True)", 
            @"Catch(ex,
    Block(new[] {
        Call(
            typeof(Console).GetMethod(""WriteLine""),
            new[] {
                Constant(true)
            }
        ),
        Call(
            typeof(Console).GetMethod(""WriteLine""),
            new[] {
                Constant(true)
            }
        )
    })
)"
        );

        [Fact]
        [Trait("Category", Try)]
        public void ConstructCatchSingleStatementWithType() => RunTest(
            Catch(exceptionType, writeLineTrue),
            @"catch (InvalidCastException) {
    Console.WriteLine(true);
}",
            @"Catch _ As InvalidCastException
    Console.WriteLine(True)",
            @"Catch(
    typeof(InvalidCastException),
    Call(
        typeof(Console).GetMethod(""WriteLine""),
        new[] {
            Constant(true)
        }
    )
)"
        );

        [Fact]
        [Trait("Category", Try)]
        public void ConstructCatchMultiStatementWithType() => RunTest(
            Catch(exceptionType, Block(writeLineTrue, writeLineTrue)),
            @"catch (InvalidCastException) {
    Console.WriteLine(true);
    Console.WriteLine(true);
}",
            @"Catch _ As InvalidCastException
    Console.WriteLine(True)
    Console.WriteLine(True)",
            @"Catch(
    typeof(InvalidCastException),
    Block(new[] {
        Call(
            typeof(Console).GetMethod(""WriteLine""),
            new[] {
                Constant(true)
            }
        ),
        Call(
            typeof(Console).GetMethod(""WriteLine""),
            new[] {
                Constant(true)
            }
        )
    })
)"
        );

        [Fact]
        [Trait("Category", Try)]
        public void ConstructCatchSingleStatementWithFilter() => RunTest(
            Catch(ex, writeLineTrue, Constant(true)),
            @"catch (Exception ex) when (true) {
    Console.WriteLine(true);
}",
            @"Catch ex As Exception When True
    Console.WriteLine(True)",
            @"Catch(ex,
    Call(
        typeof(Console).GetMethod(""WriteLine""),
        new[] {
            Constant(true)
        }
    ),
    Constant(true)
)"
        );

        [Fact]
        [Trait("Category", Try)]
        public void ConstructCatchMultiStatementWithFilter() => RunTest(
            Catch(ex, Block(writeLineTrue, writeLineTrue), Constant(true)),
            @"catch (Exception ex) when (true) {
    Console.WriteLine(true);
    Console.WriteLine(true);
}",
            @"Catch ex As Exception When True
    Console.WriteLine(True)
    Console.WriteLine(True)", 
            @"Catch(ex,
    Block(new[] {
        Call(
            typeof(Console).GetMethod(""WriteLine""),
            new[] {
                Constant(true)
            }
        ),
        Call(
            typeof(Console).GetMethod(""WriteLine""),
            new[] {
                Constant(true)
            }
        )
    }),
    Constant(true)
)"
        );

        [Fact]
        [Trait("Category", Try)]
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
    Console.WriteLine(True)", 
            @"Catch(ex,
    Call(
        typeof(Console).GetMethod(""WriteLine""),
        new[] {
            Constant(true)
        }
    ),
    Block(new[] {
        Constant(true),
        Constant(true)
    })
)"
        );

        [Fact]
        [Trait("Category", Try)]
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
End Try",
            @"TryCatch(
    Constant(true),
    new[] {
        Catch(
            typeof(Exception),
            Constant(true)
        )
    }
)"
        );

        [Fact]
        [Trait("Category", Try)]
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
End Try", 
            @"TryCatchFinally(
    Constant(true),
    Call(
        typeof(Console).GetMethod(""WriteLine""),
        new[] {
            Constant(true)
        }
    ), new[] {
        Catch(ex,
            Constant(true)
        )
    }
)"
        );

        [Fact]
        [Trait("Category", Try)]
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
End Try", 
            @"TryFault(
    Call(
        typeof(Console).GetMethod(""WriteLine""),
        new[] {
            Constant(true)
        }
    ),
    Call(
        typeof(Console).GetMethod(""WriteLine""),
        new[] {
            Constant(true)
        }
    )
)"
        );

        [Fact]
        [Trait("Category", Try)]
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
End Try",
            @"TryFinally(
    Call(
        typeof(Console).GetMethod(""WriteLine""),
        new[] {
            Constant(true)
        }
    ),
    Call(
        typeof(Console).GetMethod(""WriteLine""),
        new[] {
            Constant(true)
        }
    )
)"
        );
    }
}
