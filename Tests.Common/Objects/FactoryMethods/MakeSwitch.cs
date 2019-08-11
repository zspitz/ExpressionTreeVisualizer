using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static ExpressionToString.Tests.Functions;
using static ExpressionToString.Tests.Categories;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Globals;

namespace ExpressionToString.Tests.Objects {
    partial class FactoryMethods {
        [Category(SwitchCases)]
        public static readonly SwitchCase SingleValueSwitchCase = SwitchCase(
            Block(writeLineTrue, writeLineTrue),
            Constant(5)
        );

        [Category(SwitchCases)]
        public static readonly SwitchCase MultiValueSwitchCase = SwitchCase(
            Block(writeLineTrue, writeLineTrue),
            Constant(5),
            Constant(6)
        );

        [Category(SwitchCases)]
        public static readonly SwitchCase SingleValueSwitchCase1 = SwitchCase(writeLineTrue, Constant(5));

        [Category(SwitchCases)]
        public static readonly SwitchCase MultiValueSwitchCase1 = SwitchCase(writeLineTrue, Constant(5), Constant(6));

        [Category(SwitchCases)]
        public static readonly Expression SwitchOnExpressionWithDefaultSingleStatement = Switch(i, Empty(),
            SwitchCase(
                writeLineTrue,
                Constant(4)
            ), SwitchCase(
                writeLineFalse,
                Constant(5)
            )
        );

        [Category(SwitchCases)]
        public static readonly Expression SwitchOnExpressionWithDefaultMultiStatement = Switch(i,
            Block(
                typeof(void),
                Constant(true),
                Constant(true)
            ), SwitchCase(
                writeLineTrue,
                Constant(4)
            ), SwitchCase(
                writeLineFalse,
                Constant(5)
            )
        );

        [Category(SwitchCases)]
        public static readonly Expression SwitchOnMultipleStatementsWithDefault = Switch(Block(i, j), Block(
                typeof(void),
                Constant(true),
                Constant(true)
            ), SwitchCase(
                writeLineTrue,
                Constant(4)
            ), SwitchCase(
                writeLineFalse,
                Constant(5)
            )
        );

        [Category(SwitchCases)]
        public static readonly Expression SwitchOnExpressionWithoutDefault = Switch(i, SwitchCase(
                writeLineTrue,
                Constant(4)
            ), SwitchCase(
                writeLineFalse,
                Constant(5)
            )
        );

        [Category(SwitchCases)]
        public static readonly Expression SwitchOnMultipleStatementsWithoutDefault = Switch(Block(i, j), SwitchCase(
                writeLineTrue,
                Constant(4)
            ), SwitchCase(
                writeLineFalse,
                Constant(5)
            )
        );
    }
}