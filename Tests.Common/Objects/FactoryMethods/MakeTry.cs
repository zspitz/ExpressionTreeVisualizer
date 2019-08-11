using System;
using System.Linq.Expressions;
using static ExpressionToString.Tests.Categories;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Globals;

namespace ExpressionToString.Tests.Objects {
    partial class FactoryMethods {
        static Type exceptionType = typeof(InvalidCastException);
        static ParameterExpression ex = Parameter(typeof(Exception), "ex");

        [Category(Try)]
        public static readonly Expression ConstructTryFault = TryFault(writeLineTrue, writeLineTrue);

        [Category(Try)]
        public static readonly Expression ConstructTryFinally = TryFinally(writeLineTrue, writeLineTrue);

        [Category(Try)]
        public static readonly CatchBlock ConstructSimpleCatch = Catch(typeof(Exception), writeLineTrue);

        [Category(Try)]
        public static readonly CatchBlock ConstructCatchSingleStatement = Catch(ex, writeLineTrue);

        [Category(Try)]
        public static readonly CatchBlock ConstructCatchMultiStatement = Catch(ex, Block(writeLineTrue, writeLineTrue));

        [Category(Try)]
        public static readonly CatchBlock ConstructCatchSingleStatementWithType = Catch(exceptionType, writeLineTrue);

        [Category(Try)]
        public static readonly CatchBlock ConstructCatchMultiStatementWithType = Catch(exceptionType, Block(writeLineTrue, writeLineTrue));

        [Category(Try)]
        public static readonly CatchBlock ConstructCatchSingleStatementWithFilter = Catch(ex, writeLineTrue, Constant(true));

        [Category(Try)]
        public static readonly CatchBlock ConstructCatchMultiStatementWithFilter = Catch(ex, Block(writeLineTrue, writeLineTrue), Constant(true));

        [Category(Try)]
        public static readonly CatchBlock ConstructCatchWithMultiStatementFilter = Catch(ex, writeLineTrue, Block(Constant(true), Constant(true)));

        [Category(Try)]
        public static readonly Expression ConstructTryCatch = TryCatch(Constant(true), Catch(typeof(Exception), Constant(true)));

        [Category(Try)]
        public static readonly Expression ConstructTryCatchFinally = TryCatchFinally(Constant(true), writeLineTrue, Catch(ex, Constant(true)));
    }
}