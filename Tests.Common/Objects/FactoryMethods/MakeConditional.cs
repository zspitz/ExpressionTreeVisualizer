using System.Linq.Expressions;
using static ExpressionToString.Tests.Functions;
using static ExpressionToString.Tests.Categories;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Globals;

namespace ExpressionToString.Tests.Objects {

    // note that the NodeType of the expression constructed Conditional factory method can be either typeof(void) or some other type
    // the NodeTypeof IfThen and IfThenElse is always typeof(void)

    partial class FactoryMethods {

        [Category(Conditionals)]
        public static readonly Expression VoidConditionalWithElse = Condition(
            Constant(true),
            writeLineTrue,
            writeLineFalse
        );

        [Category(Conditionals)]
        public static readonly Expression VoidConditional1WithElse = IfThenElse(
            Constant(true),
            writeLineTrue,
            writeLineFalse
        );

        [Category(Conditionals)]
        public static readonly Expression VoidConditionalWithoutElse = Condition(
            Constant(true),
            writeLineTrue,
            Empty()
        );

        [Category(Conditionals)]
        public static readonly Expression VoidConditional1WithoutElse = IfThen(
            Constant(true),
            writeLineTrue
        );

        [Category(Conditionals)]
        public static readonly Expression NonVoidConditionalWithElse = Condition(
            Constant(true),
            trueLength,
            falseLength
        );

        [Category(Conditionals)]
        public static readonly Expression NonVoidConditionalWithoutElse = Condition(
            Constant(true),
            trueLength,
            Default(typeof(int))
        );

        [Category(Conditionals)]
        public static readonly Expression MultilineTestPart = Condition(
            Block(Constant(true), Constant(true)),
            trueLength,
            falseLength
        );


        [Category(Conditionals)]
        public static readonly Expression MultilineTestPart1 = IfThen(
            Block(Constant(true), Constant(true)),
            writeLineTrue
        );

        [Category(Conditionals)]
        public static readonly Expression MultilineIfTrue = IfThen(
            Constant(true),
            Block(writeLineTrue, writeLineTrue)
        );

        [Category(Conditionals)]
        public static readonly Expression MultilineIfFalse = IfThenElse(
            Constant(true),
            writeLineTrue,
            Block(writeLineFalse, writeLineFalse)
        );

        [Category(Conditionals)]
        public static readonly Expression NestedIfThen = IfThen(
            Constant(true),
            IfThen(
                Constant(true),
                writeLineTrue
            )
        );

        [Category(Conditionals)]
        public static readonly Expression NestedElse = IfThenElse(
            Constant(true),
            writeLineTrue,
            IfThen(
                Constant(true),
                writeLineTrue
            )
        );

        [Category(Conditionals)]
        public static readonly Expression MakeConditional = IIFE(() => {
            var i = Parameter(typeof(int), "i");
            return Condition(
                GreaterThan(i, Constant(10)),
                i,
                Add(i, Constant(10))
            );
        });
    }
}