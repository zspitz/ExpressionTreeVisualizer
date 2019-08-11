using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static ExpressionToString.Tests.Functions;
using static ExpressionToString.Tests.Categories;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Globals;

namespace ExpressionToString.Tests.Objects {
    partial class FactoryMethods {
        [Category(Blocks)]
        public static readonly Expression BlockNoVariables = Block(
            Constant(true),
            Constant(true)
        );

        [Category(Blocks)]
        public static readonly Expression BlockSingleVariable = Block(
            new[] { i },
            Constant(true),
            Constant(true)
        );

        [Category(Blocks)]
        public static readonly Expression BlockMultipleVariable = Block(
            new[] { i, s1 },
            Constant(true),
            Constant(true)
        );

        [Category(Blocks)]
        public static readonly Expression NestedInlineBlock = Block(
            Constant(true),
            Block(
                Constant(true),
                Constant(true)
            ),
            Constant(true)
        );

        [Category(Blocks)]
        public static readonly Expression NestedBlockInTest = IfThen(
            NestedInlineBlock,
            Constant(true)
        );

        [Category(Blocks)]
        public static readonly Expression NestedBlockInBlockSyntax = IfThen(
            Constant(true),
            NestedInlineBlock
        );

        [Category(Blocks)]
        public static readonly BlockExpression NestedInlineBlockWithVariable = Block(
            Constant(true),
            Block(
                new[] { s1 },
                Constant(true),
                Constant(true)
            ),
            Constant(true)
        );

        [Category(Blocks)]
        public static readonly Expression NestedBlockInTestWithVariables = IfThen(
            NestedInlineBlockWithVariable,
            Constant(true)
        );

        [Category(Blocks)]
        public static readonly Expression NestedBlockInBlockSyntaxWithVariable = IfThen(
            Constant(true),
            NestedInlineBlockWithVariable
        );
    }
}