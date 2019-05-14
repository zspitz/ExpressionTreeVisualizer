using ExpressionToString.Util;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static ExpressionToString.Globals;
using static ExpressionToString.Util.Functions;

namespace ExpressionToString {
    public class FactoryMethodsFormatter : WriterBase {
        private void WriteUsing() {
            string @using;
            switch (language) {
                case FormatterNames.CSharp:
                    @using = "// using static System.Linq.Expressions.Expression";
                    break;
                case FormatterNames.VisualBasic:
                    @using = "' Imports System.Linq.Expressions.Expression";
                    break;
                default:
                    throw new InvalidOperationException("Invalid language");
            }
            Write(@using);
            WriteEOL();
        }

        public FactoryMethodsFormatter(object o, string language) : base(o, language) {
            WriteUsing();
        }
        public FactoryMethodsFormatter(object o, string language, out Dictionary<string, (int start, int length)> pathSpans) : base(o, language, out pathSpans) {
            WriteUsing();
        }

        //private void WriteMethodCall(string name, params (string path, object arg)[] args) {
        //    Write(name);
        //    Write("(");
        //    bool lastArgIsNodeType = false;
        //    args.ForEach((pathArg, indexer) => {
        //        var (path, arg) = pathArg;
        //        if (indexer > 0) { Write(", "); }
        //        if (NodeTypes.Any(x => x.IsAssignableFrom(pathArg.GetType()))) {
        //            lastArgIsNodeType = true;
        //            Indent();
        //            WriteEOL();
        //            WriteNode(arg);
        //        } else {
        //            lastArgIsNodeType = false;
        //            Write(RenderLiteral(arg, language));
        //        }
        //    });
        //    if (lastArgIsNodeType) { WriteEOL(true); }
        //    Write(")");
        //}

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
        protected override void WriteConditional(ConditionalExpression expr) => throw new NotImplementedException();
        protected override void WriteDefault(DefaultExpression expr) => throw new NotImplementedException();
        protected override void WriteTypeBinary(TypeBinaryExpression expr) => throw new NotImplementedException();
        protected override void WriteInvocation(InvocationExpression expr) => throw new NotImplementedException();
        protected override void WriteIndex(IndexExpression expr) => throw new NotImplementedException();
        protected override void WriteBlock(BlockExpression expr, bool? explicitBlock = null) => throw new NotImplementedException();
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
