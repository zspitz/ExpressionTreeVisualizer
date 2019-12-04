using System.Linq.Expressions;
using static ExpressionTreeTestObjects.Categories;
using static System.Linq.Expressions.Expression;
using static ExpressionTreeTestObjects.Globals;
using System.Linq;
using System.Collections;

namespace ExpressionTreeTestObjects {
    [ObjectContainer]
    internal static partial class FactoryMethods {
        [Category(Quoted)]
        internal static readonly Expression MakeQuoted = Block(
            new[] { x },
            Quote(
                Lambda(writeLineTrue)
            )
        );

        [Category(Quoted)]
        internal static readonly Expression MakeQuoted1 = Lambda(
            Quote(
                Lambda(writeLineTrue)
            )
        );

        [Category(DebugInfos)]
        internal static readonly Expression MakeDebugInfo = DebugInfo(document, 1, 2, 3, 4);

        [Category(DebugInfos)]
        internal static readonly Expression MakeClearDebugInfo = ClearDebugInfo(document);

        [Category(Loops)]
        internal static readonly Expression EmptyLoop = Loop(Constant(true));

        [Category(Loops)]
        internal static readonly Expression EmptyLoop1 = Loop(
             Block(
                 Constant(true),
                 Constant(true)
             )
         );

        [Category(Member)]
        internal static readonly Expression InstanceMember = MakeMemberAccess(
            Constant(""),
            typeof(string).GetMember("Length").Single()
        );

        [Category(Member)]
        internal static readonly Expression StaticMember = MakeMemberAccess(null, typeof(string).GetMember("Empty").Single());

        [Category(RuntimeVars)]
        internal static readonly Expression ConstructRuntimeVariables = RuntimeVariables(x, s1);

        [Category(RuntimeVars)]
        internal static readonly Expression RuntimeVariablesWithinBlock = Block(
            new[] { s2 }, //forces an explicit block
            Constant(true),
            RuntimeVariables(x, s1)
        );

        [Category(Defaults)]
        internal static readonly Expression MakeDefaultRefType = Default(typeof(string));

        [Category(Defaults)]
        internal static readonly Expression MakeDefaultValueType = Default(typeof(int));

        [Category(TypeChecks)]
        internal static readonly Expression MakeTypeCheck = TypeIs(
            Constant(""),
            typeof(string)
        );

        [Category(TypeChecks)]
        internal static readonly Expression MakeTypeEqual = TypeEqual(
            Constant(""),
            typeof(IEnumerable)
        );

        [Category(Invocation)]
        internal static readonly Expression MakeInvocation = Invoke(
            Lambda(Constant(5))
        );
    }
}
