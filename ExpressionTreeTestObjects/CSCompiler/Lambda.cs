using System;
using System.Linq.Expressions;
using static ExpressionTreeTestObjects.Categories;
using static ExpressionTreeTestObjects.Functions;

namespace ExpressionTreeTestObjects {
    partial class CSCompiler {
        [Category(Lambdas)]
        internal static readonly Expression NoParametersVoidReturn = Expr(() => Console.WriteLine());
        
        [Category(Lambdas)]
        internal static readonly Expression OneParameterVoidReturn = Expr((string s) => Console.WriteLine(s));
        
        [Category(Lambdas)]
        internal static readonly Expression TwoParametersVoidReturn = Expr((string s1, string s2) => Console.WriteLine(s1 + s2));

        [Category(Lambdas)]
        internal static readonly Expression NoParametersNonVoidReturn = Expr(() => "abcd");

        [Category(Lambdas)]
        internal static readonly Expression OneParameterNonVoidReturn = Expr((string s) => s);

        [Category(Lambdas)]
        internal static readonly Expression TwoParametersNonVoidReturn = Expr((string s1, string s2) => s1 + s2);
    }
}
