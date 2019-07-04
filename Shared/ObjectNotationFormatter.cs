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
        public ObjectNotationFormatter(object o, string language) : base(o, ResolveLanguage(language)) { }

        public ObjectNotationFormatter(object o, string language, out Dictionary<string, (int start, int length)> pathSpans) : base(o, ResolveLanguage(language), out pathSpans) { }

        // TODO order of properties
        // TODO represent parameters using variables, except for first usage where variable is defined

        private void WriteObjectCreation(object o) {
            //var newKeyword =
            //    language == CSharp ? "new " :
            //    language == VisualBasic ? "New " :
            //    "";
            //var withKeyword = language == VisualBasic ? " With " : "";
            //var period = language == VisualBasic ? "." : "";

            var type = o.GetType();
            var typename = type.FriendlyName(language);

            //var values = type.GetProperties()
            //    .Where(prp => prp.Name != "CanReduce")
            //    .Select(prp => (name: prp.Name, value: prp.GetValue(o)))
            //    .WhereT((_,value) => value != null)
            //    .SelectMany(x => {
            //        var (name, value) = x;
            //        switch (value) {
            //            case IEnumerable enumerable when !(o is string):
            //                return enumerable.ToObjectList()
            //                    .Select((y, index) => {
            //                        if (language == CSharp) {
            //                            return ($"{name}[{index}]", y);
            //                        } else {
            //                            return ($"{name}({index})", y);
            //                        }
            //                    })
            //                    .WhereT((_, innerValue) => innerValue != null)
            //                    .ToList();
            //            default:
            //                values = type.GetProperties()
            //                    .Select(prp => ($"."))
            //                break;
            //        }




            //        var valueType = value.GetType();
            //        if (valueType != typeof(string) && valueType.InheritsFromOrImplements(IEnumerable)) {

            //        } else {

            //        }
            //    })

            //switch (o) {
            //    case IEnumerable enumerable when !(o is string):
            //        values = enumerable.ToObjectList()
            //            .Select((x, index) => ($"[{index}]", x))
            //            .WhereT((_, value) => value != null)
            //            .ToList();
            //        break;
            //    default:
            //        values =type.GetProperties()
            //            .Select(prp => ($"."))
            //        break;
            //}

            //if (type != typeof(string) && type.InheritsFromOrImplements<IEnumerable>()) {
            //    values = (o as IEnumerable).ToObjectList()
            //        .Select((x, index) => ($"[{index}]", x))
            //        .Where(x => x.Item2 != null)
            //        .ToList();
            //} else {
            //    values = t.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            //        .Where(fld => !fld.IsStatic)
            //        .Select(fld => ($".{fld.Name}", fld.GetValue(o)))
            //        .Where(x => x.Item2 != null)
            //        .ToList();
            //}

            //var values = t.GetProperties()
            //    .Where(prp => prp.Name != "CanReduce")
            //    .Select(prp => (prp.Name, prp.GetValue(o)))
            //    .ToList();



            //if (language == CSharp) {
            //    Write($"new {typename} ");
            //} else { // language == VisualBasic

            //}




            var props = o.GetType().GetProperties().Where(x => x.Name != "CanReduce").ToList();

            //Write($"{newKeyword}{typename} {{");


            switch (language) {
                case CSharp:
                    Write($"new {o.GetType().FriendlyName(language)}");
                    if (props.Any()) {
                        Write("{");
                        Indent();
                        WriteEOL();
                        props.ForEach((p, index) => {
                            if (index > 0) {
                                Write(",");
                                WriteEOL();
                            }
                            Write(p.Name);
                            Write(" = ");

                            object value = p.GetValue(o);
                            if (p.PropertyType.InheritsFromOrImplementsAny(PropertyTypes)) {
                                var arglist = (value as IEnumerable).ToObjectList();
                                Write("new[] {");
                                WriteNodes(p.Name, arglist, true);
                                Write("}");
                            } else if (p.PropertyType.InheritsFromOrImplementsAny(NodeTypes)) {

                            } else {

                            }
                        });
                        WriteEOL(true);
                        Write("}");
                    } else {
                        Write("()");
                    }
                    break;
                case VisualBasic:
                    Write($"New {o.GetType().FriendlyName(language)} With {{");
                    Write("}");
                    break;
                default:
                    Write($"{o.GetType().FriendlyName(language)} {{");
                    Write("}");
                    break;

            }
        }

        protected override void WriteBinary(BinaryExpression expr) => throw new NotImplementedException();
        protected override void WriteUnary(UnaryExpression expr) => throw new NotImplementedException();
        protected override void WriteLambda(LambdaExpression expr) => throw new NotImplementedException();
        protected override void WriteParameter(ParameterExpression expr) => throw new NotImplementedException();
        protected override void WriteConstant(ConstantExpression expr) => throw new NotImplementedException();
        protected override void WriteMemberAccess(MemberExpression expr) => throw new NotImplementedException();
        protected override void WriteNew(NewExpression expr) => throw new NotImplementedException();
        protected override void WriteCall(MethodCallExpression expr) => throw new NotImplementedException();
        protected override void WriteMemberInit(MemberInitExpression expr) => throw new NotImplementedException();
        protected override void WriteListInit(ListInitExpression expr) => throw new NotImplementedException();
        protected override void WriteNewArray(NewArrayExpression expr) => throw new NotImplementedException();
        protected override void WriteConditional(ConditionalExpression expr, object metadata) => throw new NotImplementedException();
        protected override void WriteDefault(DefaultExpression expr) => throw new NotImplementedException();
        protected override void WriteTypeBinary(TypeBinaryExpression expr) => throw new NotImplementedException();
        protected override void WriteInvocation(InvocationExpression expr) => throw new NotImplementedException();
        protected override void WriteIndex(IndexExpression expr) => throw new NotImplementedException();
        protected override void WriteBlock(BlockExpression expr, object metadata) => throw new NotImplementedException();
        protected override void WriteSwitch(SwitchExpression expr) => throw new NotImplementedException();
        protected override void WriteTry(TryExpression expr) => throw new NotImplementedException();
        protected override void WriteLabel(LabelExpression expr) => throw new NotImplementedException();
        protected override void WriteGoto(GotoExpression expr) => throw new NotImplementedException();
        protected override void WriteLoop(LoopExpression expr) => throw new NotImplementedException();
        protected override void WriteRuntimeVariables(RuntimeVariablesExpression expr) => throw new NotImplementedException();
        protected override void WriteDebugInfo(DebugInfoExpression expr) => throw new NotImplementedException();
        protected override void WriteElementInit(ElementInit elementInit) => throw new NotImplementedException();
        protected override void WriteBinding(MemberBinding binding) => throw new NotImplementedException();
        protected override void WriteSwitchCase(SwitchCase switchCase) => throw new NotImplementedException();
        protected override void WriteCatchBlock(CatchBlock catchBlock) => throw new NotImplementedException();
        protected override void WriteLabelTarget(LabelTarget labelTarget) => throw new NotImplementedException();
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
