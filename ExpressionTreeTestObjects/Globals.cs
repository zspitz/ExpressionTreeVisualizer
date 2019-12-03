using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using static System.Linq.Expressions.Expression;
using System.Linq;
using static ExpressionTreeTestObjects.Functions;

namespace ExpressionTreeTestObjects {
    internal static class Globals {
        internal static readonly ParameterExpression i = Parameter(typeof(int), "i");
        internal static readonly ParameterExpression j = Parameter(typeof(int), "j");
        internal static readonly ParameterExpression x = Parameter(typeof(double), "x");
        internal static readonly ParameterExpression y = Parameter(typeof(double), "y");
        internal static readonly ParameterExpression b1 = Parameter(typeof(bool), "b1");
        internal static readonly ParameterExpression b2 = Parameter(typeof(bool), "b2");
        internal static readonly ParameterExpression s = Parameter(typeof(string), "s");
        internal static readonly ParameterExpression s1 = Parameter(typeof(string), "s1");
        internal static readonly ParameterExpression s2 = Parameter(typeof(string), "s2");
        internal static readonly ParameterExpression arr = Parameter(typeof(string[]), "arr");
        internal static readonly ParameterExpression lst = Parameter(typeof(List<int>), "lst");
        internal static readonly ParameterExpression lstString = Parameter(typeof(List<string>), "lstString");

        internal static readonly MethodCallExpression writeLineTrue;
        internal static readonly MethodCallExpression writeLineFalse;
        internal static readonly MemberExpression trueLength;
        internal static readonly MemberExpression falseLength;

        internal static readonly MethodInfo writeline0 = GetMethod(() => Console.WriteLine());
        internal static readonly MethodInfo writeline1 = GetMethod(() => Console.WriteLine(""));
        internal static readonly MethodInfo concat = GetMethod(() => string.Concat("", ""));

        internal static readonly ParameterExpression arr2D = Parameter(typeof(string[,]), "arr2d");

        internal static readonly PropertyInfo listIndexer = typeof(List<string>).GetIndexers(true).Single();

        internal static readonly SymbolDocumentInfo document = SymbolDocument("source.txt");

        static Globals() {
            var writeLine = GetMethod(() => Console.WriteLine(true));
            writeLineTrue = Call(writeLine, Constant(true));
            writeLineFalse = Call(writeLine, Constant(false));

            var stringLength = GetMember(() => "".Length);
            trueLength = MakeMemberAccess(Constant("true"), stringLength);
            falseLength = MakeMemberAccess(Constant("false"), stringLength);
        }
    }
}
