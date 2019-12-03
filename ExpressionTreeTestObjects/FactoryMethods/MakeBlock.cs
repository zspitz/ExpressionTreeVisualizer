using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static ExpressionTreeTestObjects.Functions;
using static ExpressionTreeTestObjects.Categories;
using static System.Linq.Expressions.Expression;
using static ExpressionTreeTestObjects.Globals;

namespace ExpressionTreeTestObjects {
    partial class FactoryMethods {
        [Category(Blocks)]
        internal static readonly Expression BlockNoVariables = Block(
            Constant(true),
            Constant(true)
        );

        [Category(Blocks)]
        internal static readonly Expression BlockSingleVariable = Block(
            new[] { i },
            Constant(true),
            Constant(true)
        );

        [Category(Blocks)]
        internal static readonly Expression BlockMultipleVariable = Block(
            new[] { i, s1 },
            Constant(true),
            Constant(true)
        );

        [Category(Blocks)]
        internal static readonly Expression NestedInlineBlock = Block(
            Constant(true),
            Block(
                Constant(true),
                Constant(true)
            ),
            Constant(true)
        );

        [Category(Blocks)]
        internal static readonly Expression NestedBlockInTest = IfThen(
            NestedInlineBlock,
            Constant(true)
        );

        [Category(Blocks)]
        internal static readonly Expression NestedBlockInBlockSyntax = IfThen(
            Constant(true),
            NestedInlineBlock
        );

        [Category(Blocks)]
        internal static readonly BlockExpression NestedInlineBlockWithVariable = Block(
            Constant(true),
            Block(
                new[] { s1 },
                Constant(true),
                Constant(true)
            ),
            Constant(true)
        );

        [Category(Blocks)]
        internal static readonly Expression NestedBlockInTestWithVariables = IfThen(
            NestedInlineBlockWithVariable,
            Constant(true)
        );

        [Category(Blocks)]
        internal static readonly Expression NestedBlockInBlockSyntaxWithVariable = IfThen(
            Constant(true),
            NestedInlineBlockWithVariable
        );
    }
}