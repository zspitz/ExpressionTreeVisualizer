using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static ExpressionTreeTestObjects.Functions;
using static ExpressionTreeTestObjects.Categories;

namespace ExpressionTreeTestObjects {
    [ObjectContainer]
     static internal partial class CSCompiler {
        [Category(Defaults)]
        internal static readonly Expression DefaultRefType = Expr(() => default(string));

        [Category(Defaults)]
        internal static readonly Expression DefaultValueType = Expr(() => default(int));

        [Category(Conditionals)]
        internal static readonly Expression Conditional = Expr((int i) => i > 10 ? i : i + 10);

        internal static readonly Expression TypeCheck = IIFE(() => {
            object o = "";
            return Expr(() => o is string);
        });

        [Category(Invocation)]
        internal static readonly Expression InvocationNoArguments = IIFE(() => {
            Func<int> del = () => DateTime.Now.Day;
            return Expr(() => del());
        });

        [Category(Invocation)]
        internal static readonly Expression InvocationOneArgument = IIFE(() => {
            Func<int, int> del = (int i) => DateTime.Now.Day;
            return Expr(() => del(5));
        });

        [Category(Member)]
        internal static readonly Expression InstanceMember = IIFE(() => {
            var s = "";
            return Expr(() => s.Length);
        });

        [Category(Member)]
        internal static readonly Expression ClosedVariable = IIFE(() => {
            var s = "";
            return Expr(() => s);
        });

        [Category(Member)]
        internal static readonly Expression StaticMember = Expr(() => string.Empty);

        [Category(Indexer)]
        internal static readonly Expression ArraySingleIndex = IIFE(() => {
            var arr = new string[] { };
            return Expr(() => arr[5]);
        });

        [Category(Indexer)]
        internal static readonly Expression ArrayMultipleIndex = IIFE(() => {
            var arr = new string[,] { };
            return Expr(() => arr[5, 6]);
        });

        [Category(Indexer)]
        internal static readonly Expression TypeIndexer = IIFE(() => {
            var lst = new List<string>();
            return Expr(() => lst[3]);
        });
    }
}
