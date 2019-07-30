using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ExpressionToString.Util;

namespace Tests.DataGenerator {
    public class NodeTypeExpressionTypeMapper : ExpressionVisitor {
        public static HashSet<(Type, ExpressionType)> maps = new HashSet<(Type, ExpressionType)>();
        public override Expression Visit(Expression node) {
            if (node != null) {
                maps.Add((node.GetType().BaseTypes(false, true).First(x => x.IsPublic && !x.IsInterface), node.NodeType));
            }
            return base.Visit(node);
        }

        public object VisitExt(object node) {
            switch (node) {
                case Expression expr:
                    return Visit(expr);
                case MemberBinding mbind:
                    return VisitMemberBinding(mbind);
                case ElementInit init:
                    return VisitElementInit(init);
                case SwitchCase switchCase:
                    return VisitSwitchCase(switchCase);
                case CatchBlock catchBlock:
                    return VisitCatchBlock(catchBlock);
                case LabelTarget labelTarget:
                    return VisitLabelTarget(labelTarget);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
