using System.Linq.Expressions;
using static ExpressionToString.Tests.Categories;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Globals;
using System.Linq;

namespace ExpressionToString.Tests.Objects {
    public static partial class FactoryMethods {
        [Category(Quoted)]
        public static readonly Expression MakeQuoted = Block(
            new[] { x },
            Quote(
                Lambda(writeLineTrue)
            )
        );

        [Category(Quoted)]
        public static readonly Expression MakeQuoted1 = Lambda(
            Quote(
                Lambda(writeLineTrue)
            )
        );

        [Category(DebugInfos)]
        public static readonly Expression MakeDebugInfo = DebugInfo(document, 1, 2, 3, 4);

        [Category(DebugInfos)]
        public static readonly Expression MakeClearDebugInfo = ClearDebugInfo(document);

        [Category(Loops)]
        public static readonly Expression EmptyLoop = Loop(Constant(true));

        [Category(Loops)]
        public static readonly Expression EmptyLoop1 = Loop(
             Block(
                 Constant(true),
                 Constant(true)
             )
         );

        [Category(Member)]
        public static readonly Expression InstanceMember = MakeMemberAccess(
            Constant(""), 
            typeof(string).GetMember("Length").Single()
        );

        [Category(Member)]
        public static readonly Expression StaticMember = MakeMemberAccess(null, typeof(string).GetMember("Empty").Single());

        [Category(RuntimeVars)]
        public static readonly Expression ConstructRuntimeVariables = RuntimeVariables(x, s1);

        [Category(RuntimeVars)]
        public static readonly Expression RuntimeVariablesWithinBlock = Block(
            new[] { s2 }, //forces an explicit block
            Constant(true),
            RuntimeVariables(x, s1)
        );

        [Category(Defaults)]
        public static readonly Expression MakeDefaultRefType = Default(typeof(string));

        [Category(Defaults)]
        public static readonly Expression MakeDefaultValueType = Default(typeof(int));
    }
}
