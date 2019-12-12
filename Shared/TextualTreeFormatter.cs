using ExpressionToString.Util;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static ExpressionToString.Util.Functions;
using static ExpressionToString.Globals;
using System.Collections;

namespace ExpressionToString {
    public class TextualTreeFormatter : WriterBase {
        public TextualTreeFormatter(object o, string language) : base(o, language) { }

        public TextualTreeFormatter(object o, string language, out Dictionary<string, (int start, int length)> pathSpans) : base(o, language, out pathSpans) { }

        private void WriteTextualNode(object o) {
            var nodeType = "";
            var typename = "";
            var name = "";
            object? value = null;

            switch (o) {
                case Expression expr:
                    nodeType = expr.NodeType.ToString();
                    typename = $"({expr.Type.FriendlyName(language)})";
                    name = expr.Name();
                    
                    switch (expr) {
                        case ConstantExpression cexpr when !expr.Type.IsClosureClass():
                            value = cexpr.Value;
                            break;
                        case Expression _ when expr.IsClosedVariable():
                        case DefaultExpression _:
                            value = expr.ExtractValue();
                            break;
                    }
                    break;

                case MemberBinding mbind:
                    nodeType = mbind.BindingType.ToString();
                    name = mbind.Member.Name;
                    break;
                case CallSiteBinder binder:
                    nodeType = binder.BinderType();
                    break;
                default:
                    nodeType = o.GetType().FriendlyName(language);
                    break;
            }

            string stringValue = "";
            if (value != null) { stringValue = "= " + StringValue(value, language); }

            Write((nodeType, typename, name, stringValue).Where(x => !x.IsNullOrWhitespace()).Joined(" "));

            var type = o.GetType();
            var preferredOrder = PreferredPropertyOrders.FirstOrDefault(x => x.type.IsAssignableFrom(o.GetType())).propertyNames;
            var childNodes = type.GetProperties()
                .Where(prp =>
                    prp.PropertyType.InheritsFromOrImplementsAny(PropertyTypes) ||
                    prp.PropertyType.InheritsFromOrImplementsAny(NodeTypes)
                )
                .OrderBy(x => {
                    if (preferredOrder == null) { return -1; }
                    return Array.IndexOf(preferredOrder, x.Name);
                })
                .ThenBy(x => x.Name)
                .SelectMany(prp => {
                    if (prp.PropertyType.InheritsFromOrImplements<IEnumerable>()) {
                        return (prp.GetValue(o) as IEnumerable).Cast<object>().Select((x, index) => (name: $"{prp.Name}[{index}]", value: x));
                    } else {
                        return new[] { (prp.Name, prp.GetValue(o)) };
                    }
                })
                .Where(x => x.value != null)
                .ToList();

            if (childNodes.Any()) {
                Indent();
                WriteEOL();
                childNodes.ForEach((node, index) => {
                    if (index > 0) { WriteEOL(); }
                    Write($"· {node.name} - ");
                    WriteNode(node);
                });
                Dedent();
            }
        }

        protected override void WriteBinary(BinaryExpression expr) => WriteTextualNode(expr);
        protected override void WriteUnary(UnaryExpression expr) => WriteTextualNode(expr);
        protected override void WriteLambda(LambdaExpression expr) => WriteTextualNode(expr);
        protected override void WriteParameter(ParameterExpression expr) => WriteTextualNode(expr);
        protected override void WriteConstant(ConstantExpression expr) => WriteTextualNode(expr);
        protected override void WriteMemberAccess(MemberExpression expr) => WriteTextualNode(expr);
        protected override void WriteNew(NewExpression expr) => WriteTextualNode(expr);
        protected override void WriteCall(MethodCallExpression expr) => WriteTextualNode(expr);
        protected override void WriteMemberInit(MemberInitExpression expr) => WriteTextualNode(expr);
        protected override void WriteListInit(ListInitExpression expr) => WriteTextualNode(expr);
        protected override void WriteNewArray(NewArrayExpression expr) => WriteTextualNode(expr);
        protected override void WriteConditional(ConditionalExpression expr, object? metadata) => WriteTextualNode(expr);
        protected override void WriteDefault(DefaultExpression expr) => WriteTextualNode(expr);
        protected override void WriteTypeBinary(TypeBinaryExpression expr) => WriteTextualNode(expr);
        protected override void WriteInvocation(InvocationExpression expr) => WriteTextualNode(expr);
        protected override void WriteIndex(IndexExpression expr) => WriteTextualNode(expr);
        protected override void WriteBlock(BlockExpression expr, object? metadata) => WriteTextualNode(expr);
        protected override void WriteSwitch(SwitchExpression expr) => WriteTextualNode(expr);
        protected override void WriteTry(TryExpression expr) => WriteTextualNode(expr);
        protected override void WriteLabel(LabelExpression expr) => WriteTextualNode(expr);
        protected override void WriteGoto(GotoExpression expr) => WriteTextualNode(expr);
        protected override void WriteLoop(LoopExpression expr) => WriteTextualNode(expr);
        protected override void WriteRuntimeVariables(RuntimeVariablesExpression expr) => WriteTextualNode(expr);
        protected override void WriteDebugInfo(DebugInfoExpression expr) => WriteTextualNode(expr);
        protected override void WriteElementInit(ElementInit elementInit) => WriteTextualNode(elementInit);
        protected override void WriteBinding(MemberBinding binding) => WriteTextualNode(binding);
        protected override void WriteSwitchCase(SwitchCase switchCase) => WriteTextualNode(switchCase);
        protected override void WriteCatchBlock(CatchBlock catchBlock) => WriteTextualNode(catchBlock);
        protected override void WriteLabelTarget(LabelTarget labelTarget) => WriteTextualNode(labelTarget);
        protected override void WriteDynamic(DynamicExpression expr) => WriteTextualNode(expr);
        protected override void WriteBinaryOperationBinder(BinaryOperationBinder binaryOperationBinder, IList<Expression> args) => throw new NotImplementedException();
        protected override void WriteConvertBinder(ConvertBinder convertBinder, IList<Expression> args) => throw new NotImplementedException();
        protected override void WriteCreateInstanceBinder(CreateInstanceBinder createInstanceBinder, IList<Expression> args) => throw new NotImplementedException();
        protected override void WriteDeleteIndexBinder(DeleteIndexBinder deleteIndexBinder, IList<Expression> args) => throw new NotImplementedException();
        protected override void WriteDeleteMemberBinder(DeleteMemberBinder deleteMemberBinder, IList<Expression> args) => throw new NotImplementedException();
        protected override void WriteGetIndexBinder(GetIndexBinder getIndexBinder, IList<Expression> args) => throw new NotImplementedException();
        protected override void WriteGetMemberBinder(GetMemberBinder getMemberBinder, IList<Expression> args) => throw new NotImplementedException();
        protected override void WriteInvokeBinder(InvokeBinder invokeBinder, IList<Expression> args) => throw new NotImplementedException();
        protected override void WriteInvokeMemberBinder(InvokeMemberBinder invokeMemberBinder, IList<Expression> args) => throw new NotImplementedException();
        protected override void WriteSetIndexBinder(SetIndexBinder setIndexBinder, IList<Expression> args) => throw new NotImplementedException();
        protected override void WriteSetMemberBinder(SetMemberBinder setMemberBinder, IList<Expression> args) => throw new NotImplementedException();
        protected override void WriteUnaryOperationBinder(UnaryOperationBinder unaryOperationBinder, IList<Expression> args) => throw new NotImplementedException();
        protected override void WriteParameterDeclarationImpl(ParameterExpression prm) => throw new NotImplementedException();
    }
}
