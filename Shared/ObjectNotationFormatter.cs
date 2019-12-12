using ExpressionToString.Util;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static ExpressionToString.Util.Functions;
using static ExpressionToString.FormatterNames;
using static ExpressionToString.Globals;
using System.Collections;
using System.Reflection;

namespace ExpressionToString {
    public class ObjectNotationFormatter : WriterBase {
        public ObjectNotationFormatter(object o, string language) : base(o, language) { }

        public ObjectNotationFormatter(object o, string language, out Dictionary<string, (int start, int length)> pathSpans) : base(o, language, out pathSpans) { }

        // TODO represent parameters using variables, except for first usage where variable is defined
        // TODO if a given type always has the same node type, don't include

        private static readonly HashSet<Type> hideNodeType = new HashSet<Type>() {
            typeof(BlockExpression),
            typeof(ConditionalExpression),
            typeof(ConstantExpression),
            typeof(DebugInfoExpression),
            typeof(DefaultExpression),
            typeof(DynamicExpression),
            typeof(GotoExpression),
            typeof(IndexExpression),
            typeof(InvocationExpression),
            typeof(LabelExpression),
            typeof(ListInitExpression),
            typeof(LoopExpression),
            typeof(MemberExpression),
            typeof(MemberInitExpression),
            typeof(MethodCallExpression),
            typeof(NewExpression),
            typeof(ParameterExpression),
            typeof(RuntimeVariablesExpression),
            typeof(SwitchExpression),
            typeof(TryExpression)
        };

        private void WriteObjectCreation(object o) {
            var type = writeNew(o);
            var preferredOrder = PreferredPropertyOrders.FirstOrDefault(x => x.type.IsAssignableFrom(o.GetType())).propertyNames;
            var properties = type.GetProperties().Where(x => {
                if (x.Name.In("CanReduce", "TailCall", "CanReduce", "IsLifted", "IsLiftedToNull", "ArgumentCount")) { return false; }
                if (x.Name == "NodeType" && hideNodeType.Contains(type)) { return false; }
                return true;
            }).ToList();

            if (properties.None()) {
                if (language == CSharp) { Write("()"); }
                return;
            }

            if (language == VisualBasic) { Write(" With"); }
            Write(" {");
            Indent();
            WriteEOL();

            properties.OrderBy(x => {
                if (x.Name.In("NodeType", "Type")) { return -2; }
                if (preferredOrder == null) { return -1; }
                var indexOf = Array.IndexOf(preferredOrder, x.Name);
                if (indexOf == -1) { return 1000; }
                return indexOf;
            })
            .ThenBy(x => x.Name)
            .Select(x => {
                object value;
                try {
                    value = x.GetValue(o);
                } catch (Exception ex) {
                    value = ex.Message;
                }
                return (x, value);
            })
            .WhereT((_, value) => {
                if (value == null) { return false; }
                if (value is IEnumerable seq && seq.None()) { return false; }
                return true;
            })
            .ForEachT((x, value, index) => {
                if (index > 0) {
                    Write(",");
                    WriteEOL();
                }
                if (language == VisualBasic) { Write("."); }
                Write(x.Name);
                Write(" = ");

                if (x.PropertyType.InheritsFromOrImplementsAny(PropertyTypes)) {

                    // https://github.com/zspitz/ExpressionToString/issues/91
                    //var parameterDeclaration = o is LambdaExpression && x.Name == "Parameters";
                    //WriteCollection(value as IEnumerable, x.Name, parameterDeclaration);

                    WriteCollection((IEnumerable)value, x.Name);
                } else if (x.PropertyType.InheritsFromOrImplementsAny(NodeTypes)) {
                    WriteNode(x.Name, value);
                } else {
                    Write(RenderLiteral(value, language));
                }
            });

            WriteEOL(true);
            Write("}");
        }

        private void WriteCollection(IEnumerable seq, string pathSegment) {
            writeNew(seq);
            var items = seq.Cast<object>().ToList();
            if (items.None() ) {
                if (language==CSharp) { Write("()"); }
                return;
            }
            if (language == VisualBasic) { Write(" From"); }
            Write(" {");
            Indent();
            WriteEOL();

            // https://github.com/zspitz/ExpressionToString/issues/91
            //WriteNodes(pathSegment, items, true, ",", parameterDeclaration)
            ;
            WriteNodes(pathSegment, items, true);
            WriteEOL(true);
            Write("}");
        }

        private Type writeNew(object o) {
            Write(
                language == CSharp ? "new " :
                language == VisualBasic ? "New " :
                ""
            );
            var t = o.GetType().BaseTypes(false, true).First(x => x.IsPublic && !x.IsInterface);
            Write(t.FriendlyName(language));
            return t;
        }

        protected override void WriteBinary(BinaryExpression expr) => WriteObjectCreation(expr);
        protected override void WriteUnary(UnaryExpression expr) => WriteObjectCreation(expr);
        protected override void WriteLambda(LambdaExpression expr) => WriteObjectCreation(expr);

        // https://github.com/zspitz/ExpressionToString/issues/91
        //protected override void WriteParameter(ParameterExpression expr) => Write(expr.Name);
        protected override void WriteParameter(ParameterExpression expr) => WriteObjectCreation(expr);

        protected override void WriteConstant(ConstantExpression expr) => WriteObjectCreation(expr);
        protected override void WriteMemberAccess(MemberExpression expr) => WriteObjectCreation(expr);
        protected override void WriteNew(NewExpression expr) => WriteObjectCreation(expr);
        protected override void WriteCall(MethodCallExpression expr) => WriteObjectCreation(expr);
        protected override void WriteMemberInit(MemberInitExpression expr) => WriteObjectCreation(expr);
        protected override void WriteListInit(ListInitExpression expr) => WriteObjectCreation(expr);
        protected override void WriteNewArray(NewArrayExpression expr) => WriteObjectCreation(expr);
        protected override void WriteConditional(ConditionalExpression expr, object? metadata) => WriteObjectCreation(expr);
        protected override void WriteDefault(DefaultExpression expr) => WriteObjectCreation(expr);
        protected override void WriteTypeBinary(TypeBinaryExpression expr) => WriteObjectCreation(expr);
        protected override void WriteInvocation(InvocationExpression expr) => WriteObjectCreation(expr);
        protected override void WriteIndex(IndexExpression expr) => WriteObjectCreation(expr);
        protected override void WriteBlock(BlockExpression expr, object? metadata) => WriteObjectCreation(expr);
        protected override void WriteSwitch(SwitchExpression expr) => WriteObjectCreation(expr);
        protected override void WriteTry(TryExpression expr) => WriteObjectCreation(expr);
        protected override void WriteLabel(LabelExpression expr) => WriteObjectCreation(expr);
        protected override void WriteGoto(GotoExpression expr) => WriteObjectCreation(expr);
        protected override void WriteLoop(LoopExpression expr) => WriteObjectCreation(expr);
        protected override void WriteRuntimeVariables(RuntimeVariablesExpression expr) => WriteObjectCreation(expr);
        protected override void WriteDebugInfo(DebugInfoExpression expr) => WriteObjectCreation(expr);
        protected override void WriteElementInit(ElementInit elementInit) => WriteObjectCreation(elementInit);
        protected override void WriteBinding(MemberBinding binding) => WriteObjectCreation(binding);
        protected override void WriteSwitchCase(SwitchCase switchCase) => WriteObjectCreation(switchCase);
        protected override void WriteCatchBlock(CatchBlock catchBlock) => WriteObjectCreation(catchBlock);
        protected override void WriteLabelTarget(LabelTarget labelTarget) => WriteObjectCreation(labelTarget);

        protected override void WriteDynamic(DynamicExpression expr) => WriteObjectCreation(expr);

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

        // We can't use this yet
        // https://github.com/zspitz/ExpressionToString/issues/91
        protected override void WriteParameterDeclarationImpl(ParameterExpression prm) {
            if (language == CSharp) {
                Write($"var ");
            } else if (language == VisualBasic) {
                Write($"Dim ");
            }
            Write($"{prm.Name} = ");
            WriteObjectCreation(prm);
        }
    }
}
