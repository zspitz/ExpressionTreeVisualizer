using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using static ExpressionToString.Tests.Functions;
using static ExpressionToString.Tests.Categories;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Globals;

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
    }
}
