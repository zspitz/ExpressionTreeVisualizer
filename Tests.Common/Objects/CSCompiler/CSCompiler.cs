using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static ExpressionToString.Tests.Functions;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests.Objects {
    public static partial class CSCompiler {
        [Category(Defaults)]
        public static readonly Expression DefaultRefType = Expr(() => default(string));

        [Category(Defaults)]
        public static readonly Expression DefaultValueType = Expr(() => default(int));

        [Category(Conditionals)]
        public static readonly Expression Conditional = Expr((int i) => i > 10 ? i : i + 10);

        public static readonly Expression TypeCheck = IIFE(() => {
            object o = "";
            return Expr(() => o is string);
        });

        [Category(Invocation)]
        public static readonly Expression InvocationNoArguments = IIFE(() => {
            Func<int> del = () => DateTime.Now.Day;
            return Expr(() => del());
        });

        [Category(Invocation)]
        public static readonly Expression InvocationOneArgument = IIFE(() => {
            Func<int, int> del = (int i) => DateTime.Now.Day;
            return Expr(() => del(5));
        });

        [Category(Member)]
        public static readonly Expression InstanceMember = IIFE(() => {
            var s = "";
            return Expr(() => s.Length);
        });

        [Category(Member)]
        public static readonly Expression ClosedVariable = IIFE(() => {
            var s = "";
            return Expr(() => s);
        });

        [Category(Member)]
        public static readonly Expression StaticMember = Expr(() => string.Empty);

        [Category(Indexer)]
        public static readonly Expression ArraySingleIndex = IIFE(() => {
            var arr = new string[] { };
            return Expr(() => arr[5]);
        });

        [Category(Indexer)]
        public static readonly Expression ArrayMultipleIndex = IIFE(() => {
            var arr = new string[,] { };
            return Expr(() => arr[5, 6]);
        });

        [Category(Indexer)]
        public static readonly Expression TypeIndexer = IIFE(() => {
            var lst = new List<string>();
            return Expr(() => lst[3]);
        });
    }
}
