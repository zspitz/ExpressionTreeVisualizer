using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using static System.Linq.Expressions.Expression;

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
        public static readonly ParameterExpression s1 = Parameter(typeof(string), "s1");
        public static readonly ParameterExpression s2 = Parameter(typeof(string), "s2");
        public static readonly ParameterExpression arr = Parameter(typeof(string[]), "arr");
    }
}
