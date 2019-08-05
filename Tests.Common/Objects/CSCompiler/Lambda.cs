using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static ExpressionToString.Tests.Categories;
using static ExpressionToString.Tests.Functions;

namespace ExpressionToString.Tests.Objects {
    partial class CSCompiler {
        [Category(Lambdas)]
        public static readonly Expression NoParametersVoidReturn = Expr(() => Console.WriteLine());
        
        [Category(Lambdas)]
        public static readonly Expression OneParameterVoidReturn = Expr((string s) => Console.WriteLine(s));
        
        [Category(Lambdas)]
        public static readonly Expression TwoParametersVoidReturn = Expr((string s1, string s2) => Console.WriteLine(s1 + s2));

        [Category(Lambdas)]
        public static readonly Expression NoParametersNonVoidReturn = Expr(() => "abcd");

        [Category(Lambdas)]
        public static readonly Expression OneParameterNonVoidReturn = Expr((string s) => s);

        [Category(Lambdas)]
        public static readonly Expression TwoParametersNonVoidReturn = Expr((string s1, string s2) => s1 + s2);
    }
}
