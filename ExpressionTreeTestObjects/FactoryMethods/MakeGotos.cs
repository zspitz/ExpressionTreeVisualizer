using System.Linq.Expressions;
using static ExpressionTreeTestObjects.Categories;
using static System.Linq.Expressions.Expression;

namespace ExpressionTreeTestObjects {
    partial class FactoryMethods {
        private static LabelTarget labelTarget = Label("target");

        [Category(Gotos)]
        internal static readonly Expression MakeBreak = Break(labelTarget);

        [Category(Gotos)]
        internal static readonly Expression MakeBreakWithValue = Break(labelTarget, Constant(5));

        [Category(Gotos)]
        internal static readonly Expression MakeContinue = Continue(labelTarget);

        [Category(Gotos)]
        internal static readonly Expression MakeGotoWithoutValue = Goto(labelTarget);

        [Category(Gotos)]
        internal static readonly Expression MakeGotoWithValue = Goto(labelTarget, Constant(5));

        [Category(Gotos)]
        internal static readonly Expression MakeReturn = Return(labelTarget);

        [Category(Gotos)]
        internal static readonly Expression MakeReturnWithValue = Return(labelTarget, Constant(5));
    }
}
