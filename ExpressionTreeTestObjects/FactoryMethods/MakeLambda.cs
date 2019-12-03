using System.Linq.Expressions;
using static ExpressionTreeTestObjects.Categories;
using static System.Linq.Expressions.Expression;
using static ExpressionTreeTestObjects.Globals;

namespace ExpressionTreeTestObjects {
    internal static partial class FactoryMethods {
        [Category(Lambdas)]
        internal static readonly Expression NoParametersVoidReturn = Lambda(Call(writeline0));

        [Category(Lambdas)]
        internal static readonly Expression OneParameterVoidReturn = Lambda(Call(writeline1, s), s);

        [Category(Lambdas)]
        internal static readonly Expression TwoParametersVoidReturn = Lambda(Call(writeline1, Add(s1, s2, concat)), s1, s2);

        [Category(Lambdas)]
        internal static readonly Expression NoParametersNonVoidReturn = Lambda(Constant("abcd"));

        [Category(Lambdas)]
        internal static readonly Expression OneParameterNonVoidReturn = Lambda(s, s);

        [Category(Lambdas)]
        internal static readonly Expression TwoParametersNonVoidReturn = Lambda(Add(s1, s2, concat), s1, s2);

        [Category(Lambdas)]
        internal static readonly Expression NamedLambda = Lambda(
            Add(s1, s2, concat),
            "name",
            new[] { s1, s2 }
        );

        [Category(Lambdas)]
        internal static readonly Expression MultilineLambda = Lambda(
            IfThen(Constant(true), writeLineTrue)
        );

        [Category(Lambdas)]
        internal static readonly Expression NestedLambda = Lambda(
            Lambda(Add(s1, s2, concat), s1, s2)
        );

        [Category(Lambdas)]
        internal static readonly Expression LambdaMultilineBlockNonvoidReturn = Lambda(
            Block(
                Constant(true),
                Constant(true)
            )
        );

        [Category(Lambdas)]
        internal static readonly Expression LambdaMultilineNestedBlockNonvoidReturn = Lambda(
            Block(
                Constant(true),
                Block(
                    new[] { s1, s2 },
                    Constant(true),
                    Constant(true)
                )
            )
        );

        [Category(Lambdas)]
        internal static readonly Expression MakeByRefParameter = Lambda(
            Constant(true),
            Parameter(typeof(string).MakeByRefType(), "s4")
        );
    }
}
