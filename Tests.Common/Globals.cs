using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Util.Functions;
using ExpressionToString.Util;
using System.Linq;
using System.Diagnostics;

namespace ExpressionToString.Tests {
    public static class Globals {
        public const string CSharpCompiler = "C# compiler-generated";
        public const string VBCompiler = "VB.NET compiler-generated";
        public const string FactoryMethods = "Factory methods";

        public static readonly ParameterExpression i = Parameter(typeof(int), "i");
        public static readonly ParameterExpression j = Parameter(typeof(int), "j");
        public static readonly ParameterExpression x = Parameter(typeof(double), "x");
        public static readonly ParameterExpression y = Parameter(typeof(double), "y");
        public static readonly ParameterExpression b1 = Parameter(typeof(bool), "b1");
        public static readonly ParameterExpression b2 = Parameter(typeof(bool), "b2");
        public static readonly ParameterExpression s = Parameter(typeof(string), "s");
        public static readonly ParameterExpression s1 = Parameter(typeof(string), "s1");
        public static readonly ParameterExpression s2 = Parameter(typeof(string), "s2");
        public static readonly ParameterExpression arr = Parameter(typeof(string[]), "arr");
        public static readonly ParameterExpression lst = Parameter(typeof(List<int>), "lst");
        public static readonly ParameterExpression lstString = Parameter(typeof(List<string>), "lstString");

        public static readonly MethodCallExpression writeLineTrue;
        public static readonly MethodCallExpression writeLineFalse;
        public static readonly MemberExpression trueLength;
        public static readonly MemberExpression falseLength;

        public static readonly MethodInfo writeline0 = GetMethod(() => Console.WriteLine());
        public static readonly MethodInfo writeline1 = GetMethod(() => Console.WriteLine(""));
        public static readonly MethodInfo concat = GetMethod(() => string.Concat("", ""));

        public static readonly ParameterExpression arr2D = Parameter(typeof(string[,]), "arr2d");

        public static readonly PropertyInfo listIndexer = typeof(List<string>).GetIndexers(true).Single();

        public static readonly SymbolDocumentInfo document = SymbolDocument("source.txt");

        static Globals() {
            var writeLine = GetMethod(() => Console.WriteLine(true));
            writeLineTrue = Call(writeLine, Constant(true));
            writeLineFalse = Call(writeLine, Constant(false));

            var stringLength = GetMember(() => "".Length);
            trueLength = MakeMemberAccess(Constant("true"), stringLength);
            falseLength = MakeMemberAccess(Constant("false"), stringLength);
        }
    }

    public static class Categories {
        public const string Unary = "Unary";
        public const string NewArray = "New array";
        public const string NewObject = "Object creation and initialization";
        public const string Invocation = "Invocation";
        public const string Method = "Method call";
        public const string Member = "Member access (+ closed variables)";
        public const string Literal = "Literal";
        public const string Lambdas = "Lambda";
        public const string Indexer = "Indexer";
        public const string Defaults = "Defaults";
        public const string Binary = "Binary";
        public const string Try = "Try, Catch, Finally";
        public const string SwitchCases = "Switch, CatchBlock";
        public const string RuntimeVars = "Runtime variables";
        public const string MemberBindings = "Member bindings";
        public const string Loops = "Loops";
        public const string Labels = "Labels";
        public const string Gotos = "Gotos";
        public const string Dynamics = "Dynamics";
        public const string Quoted = "Quoted";
        public const string DebugInfos = "DebugInfos";
        public const string Conditionals = "Conditionals";
        public const string Blocks = "Blocks";
        public const string Constants = "Constants";
        public const string TypeChecks = "Type check";
    }
}
