using ExpressionToString.Util;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using static ExpressionToString.FormatterNames;
using static System.Linq.Expressions.ExpressionType;
using static ExpressionToString.Util.Functions;

namespace ExpressionToString {
    public abstract class WriterBase {
        internal static WriterBase Create(object o, string formatterName, string language) =>
            formatterName == CSharp ? new CSharpCodeWriter(o) :
            formatterName == VisualBasic ? new VBCodeWriter(o) :
            formatterName == FactoryMethods ? (WriterBase)new FactoryMethodsFormatter(o, ResolveLanguage(language)) :
            throw new NotImplementedException("Unknown formatter");

        public static WriterBase Create(object o, string formatterName, string language, out Dictionary<string, (int start, int length)> pathSpans) =>
            formatterName == CSharp ? new CSharpCodeWriter(o, out pathSpans) :
            formatterName == VisualBasic ? new VBCodeWriter(o, out pathSpans) :
            formatterName == FactoryMethods ? (WriterBase)new FactoryMethodsFormatter(o, ResolveLanguage(language), out pathSpans) :
            throw new NotImplementedException("Unknown language");

        private readonly StringBuilder sb = new StringBuilder();
        private readonly Dictionary<string, (int start, int length)> pathSpans;

        /// <summary>Determines how to render literals and types</summary>
        protected string language { get; private set; }

        // Unfortunately, C# doesn't support union types ...
        protected WriterBase(object o, string language) {
            this.language = language;
            WriteNode("", o);
        }

        protected WriterBase(object o, string language, out Dictionary<string, (int start, int length)> pathSpans) {
            this.language = language;
            this.pathSpans = new Dictionary<string, (int start, int length)>();
            WriteNode("", o);
            pathSpans = this.pathSpans;
        }

        private int indentationLevel = 0;
        protected void Indent() => indentationLevel += 1;
        protected void Dedent() => indentationLevel -= 1;

        protected void WriteEOL(bool dedent = false) {
            sb.AppendLine();
            if (dedent) { indentationLevel = Math.Max(indentationLevel - 1, 0); } // ensures the indentation level is never < 0
            sb.Append(new string(' ', indentationLevel * 4));
        }

        protected void Write(string s) => s.AppendTo(sb);

        private List<string> pathSegments = new List<string>();

        /// <summary>Write a string-rendering of an expression or other type used in expression trees</summary>
        /// <param name="o">Object to be rendered</param>
        /// <param name="parameterDeclaration">For ParameterExpression, this is a parameter declaration</param>
        /// <param name="explicitBlock">For BlockExpression, controls explicit block rendering: true forces the rendering; false prevents the rendering; and null determines automatically</param>
        protected void WriteNode(string pathSegment, object o, bool parameterDeclaration = false, bool? explicitBlock = null) {
            if (!pathSegment.IsNullOrWhitespace()) { pathSegments.Add(pathSegment); }
            var start = sb.Length;
            try {
                switch (o) {
                    case ParameterExpression pexpr when parameterDeclaration:
                        WriteParameterDeclarationImpl(pexpr);
                        break;
                    case BlockExpression bexpr when explicitBlock != null:
                        WriteBlock(bexpr, explicitBlock);
                        break;
                    case Expression expr:
                        WriteExpression(expr);
                        break;
                    case MemberBinding binding:
                        WriteBinding(binding);
                        break;
                    case ElementInit init:
                        WriteElementInit(init);
                        break;
                    case SwitchCase switchCase:
                        WriteSwitchCase(switchCase);
                        break;
                    case CatchBlock catchBlock:
                        WriteCatchBlock(catchBlock);
                        break;
                    case LabelTarget labelTarget:
                        WriteLabelTarget(labelTarget);
                        break;

                    default:
                        throw new NotImplementedException($"Code generation not implemented for type '{o.GetType().Name}'");
                }
            } catch (NotImplementedException ex) {
                sb.AppendLine();
                $"----- Not implemented - {ex.Message} ()".AppendLineTo(sb);
            }

            if (pathSpans != null) {
                pathSpans.Add(pathSegments.Joined("."), (start, sb.Length - start));
                if (pathSegments.Any()) {
                    if (pathSegments.Last() != pathSegment) { throw new InvalidOperationException(); }
                    pathSegments.RemoveLast();
                }
            }
        }
        protected void WriteNode((string pathSegment, object o) x) => WriteNode(x.pathSegment, x.o);

        private readonly HashSet<ExpressionType> binaryExpressionTypes = new[] {
            Add, AddChecked, Divide, Modulo, Multiply, MultiplyChecked, Power, Subtract, SubtractChecked,   // mathematical operators
            And, Or, ExclusiveOr,   // bitwise / logical operations
            LeftShift, RightShift,     // shift operators
            AndAlso, OrElse,        // short-circuit boolean operators
            Equal, NotEqual, GreaterThanOrEqual, GreaterThan,LessThan,LessThanOrEqual,     // comparison operators
            Coalesce,
            ArrayIndex,

            Assign,
            AddAssign, AddAssignChecked,DivideAssign, ModuloAssign,MultiplyAssign, MultiplyAssignChecked, PowerAssign, SubtractAssign, SubtractAssignChecked,
            AndAssign, OrAssign, ExclusiveOrAssign,
            LeftShiftAssign,RightShiftAssign
        }.ToHashSet();

        private readonly HashSet<ExpressionType> unaryExpressionTypes = new[] {
            ArrayLength, ExpressionType.Convert, ConvertChecked, Negate, NegateChecked, Not, Quote, TypeAs, UnaryPlus, IsTrue, IsFalse,
            PreIncrementAssign, PreDecrementAssign, PostIncrementAssign, PostDecrementAssign,
            Increment, Decrement,
            Throw
        }.ToHashSet();

        private void WriteExpression(Expression expr) {
            switch (expr.NodeType) {

                case var nodeType when nodeType.In(binaryExpressionTypes):
                    WriteBinary(expr as BinaryExpression);
                    break;

                case var nodeType when nodeType.In(unaryExpressionTypes):
                    WriteUnary(expr as UnaryExpression);
                    break;

                case Lambda:
                    WriteLambda(expr as LambdaExpression);
                    break;

                case Parameter:
                    WriteParameter(expr as ParameterExpression);
                    break;

                case Constant:
                    WriteConstant(expr as ConstantExpression);
                    break;

                case MemberAccess:
                    WriteMemberAccess(expr as MemberExpression);
                    break;

                case New:
                    WriteNew(expr as NewExpression);
                    break;

                case Call:
                    WriteCall(expr as MethodCallExpression);
                    break;

                case MemberInit:
                    WriteMemberInit(expr as MemberInitExpression);
                    break;

                case ListInit:
                    WriteListInit(expr as ListInitExpression);
                    break;

                case NewArrayInit:
                case NewArrayBounds:
                    WriteNewArray(expr as NewArrayExpression);
                    break;

                case Conditional:
                    WriteConditional(expr as ConditionalExpression);
                    break;

                case Default:
                    WriteDefault(expr as DefaultExpression);
                    break;

                case TypeIs:
                case TypeEqual:
                    WriteTypeBinary(expr as TypeBinaryExpression);
                    break;

                case Invoke:
                    WriteInvocation(expr as InvocationExpression);
                    break;

                case Index:
                    WriteIndex(expr as IndexExpression);
                    break;

                case Block:
                    WriteBlock(expr as BlockExpression);
                    break;

                case Switch:
                    WriteSwitch(expr as SwitchExpression);
                    break;

                case Try:
                    WriteTry(expr as TryExpression);
                    break;

                case Label:
                    WriteLabel(expr as LabelExpression);
                    break;

                case Goto:
                    WriteGoto(expr as GotoExpression);
                    break;

                case Loop:
                    WriteLoop(expr as LoopExpression);
                    break;

                case RuntimeVariables:
                    WriteRuntimeVariables(expr as RuntimeVariablesExpression);
                    break;

                case DebugInfo:
                    WriteDebugInfo(expr as DebugInfoExpression);
                    break;

                case Dynamic:
                    WriteDynamic(expr as DynamicExpression);
                    break;

                default:
                    throw new NotImplementedException($"NodeType: {expr.NodeType}, Expression object type: {expr.GetType().Name}");
            }
        }

        protected virtual void WriteDynamic(DynamicExpression expr) {
            switch (expr.Binder) {
                case BinaryOperationBinder binaryOperationBinder:
                    WriteBinaryOperationBinder(binaryOperationBinder, expr.Arguments);
                    break;
                case ConvertBinder convertBinder:
                    WriteConvertBinder(convertBinder, expr.Arguments);
                    break;
                case CreateInstanceBinder createInstanceBinder:
                    WriteCreateInstanceBinder(createInstanceBinder, expr.Arguments);
                    break;
                case DeleteIndexBinder deleteIndexBinder:
                    WriteDeleteIndexBinder(deleteIndexBinder, expr.Arguments);
                    break;
                case DeleteMemberBinder deleteMemberBinder:
                    WriteDeleteMemberBinder(deleteMemberBinder, expr.Arguments);
                    break;
                case GetIndexBinder getIndexBinder:
                    WriteGetIndexBinder(getIndexBinder, expr.Arguments);
                    break;
                case GetMemberBinder getMemberBinder:
                    WriteGetMemberBinder(getMemberBinder, expr.Arguments);
                    break;
                case InvokeBinder invokeBinder:
                    WriteInvokeBinder(invokeBinder, expr.Arguments);
                    break;
                case InvokeMemberBinder invokeMemberBinder:
                    WriteInvokeMemberBinder(invokeMemberBinder, expr.Arguments);
                    break;
                case SetIndexBinder setIndexBinder:
                    WriteSetIndexBinder(setIndexBinder, expr.Arguments);
                    break;
                case SetMemberBinder setMemberBinder:
                    WriteSetMemberBinder(setMemberBinder, expr.Arguments);
                    break;
                case UnaryOperationBinder unaryOperationBinder:
                    WriteUnaryOperationBinder(unaryOperationBinder, expr.Arguments);
                    break;

                default:
                    throw new NotImplementedException($"Dynamic expression with binder type {expr.Binder} not implemented");
            }
        }

        protected void WriteNodes<T>(IEnumerable<(string pathSegment, T o)> pathsItems, bool writeEOL, string delimiter = ", ", bool parameterDeclaration = false) {
            if (writeEOL) { delimiter = delimiter.TrimEnd(); }
            pathsItems.ForEachT((pathSegment, arg, index) => {
                if (index > 0) {
                    delimiter.AppendTo(sb);
                    if (writeEOL) { WriteEOL(); }
                }
                WriteNode(pathSegment, arg, parameterDeclaration);
            });
        }
        protected void WriteNodes<T>(IEnumerable<(string pathSegment, T o)> pathsItems, string delimiter = ", ") =>
            WriteNodes(pathsItems, false, delimiter);

        protected void WriteNodes<T>(string pathSegment, IEnumerable<T> items, bool writeEOL, string delimiter = ", ", bool parameterDeclaration = false) =>
            WriteNodes(items.Select((arg, index) => ($"{pathSegment}[{index}]", arg)), writeEOL, delimiter, parameterDeclaration);

        protected void WriteNodes<T>(string pathSegment, IEnumerable<T> items, string delimiter = ", ") => WriteNodes(pathSegment, items, false, delimiter);

        protected void TrimEnd(bool trimEOL = false) => sb.TrimEnd(trimEOL);

        public override string ToString() => sb.ToString();

        // .NET 3.5 expression types
        protected abstract void WriteBinary(BinaryExpression expr);
        protected abstract void WriteUnary(UnaryExpression expr);
        protected abstract void WriteLambda(LambdaExpression expr);
        protected abstract void WriteParameter(ParameterExpression expr);
        protected abstract void WriteConstant(ConstantExpression expr);
        protected abstract void WriteMemberAccess(MemberExpression expr);
        protected abstract void WriteNew(NewExpression expr);
        protected abstract void WriteCall(MethodCallExpression expr);
        protected abstract void WriteMemberInit(MemberInitExpression expr);
        protected abstract void WriteListInit(ListInitExpression expr);
        protected abstract void WriteNewArray(NewArrayExpression expr);
        protected abstract void WriteConditional(ConditionalExpression expr);
        protected abstract void WriteDefault(DefaultExpression expr);
        protected abstract void WriteTypeBinary(TypeBinaryExpression expr);
        protected abstract void WriteInvocation(InvocationExpression expr);
        protected abstract void WriteIndex(IndexExpression expr);

        // .NET 4 expression types
        protected abstract void WriteBlock(BlockExpression expr, bool? explicitBlock = null);
        protected abstract void WriteSwitch(SwitchExpression expr);
        protected abstract void WriteTry(TryExpression expr);
        protected abstract void WriteLabel(LabelExpression expr);
        protected abstract void WriteGoto(GotoExpression expr);
        protected abstract void WriteLoop(LoopExpression expr);
        protected abstract void WriteRuntimeVariables(RuntimeVariablesExpression expr);
        protected abstract void WriteDebugInfo(DebugInfoExpression expr);

        // other types
        protected abstract void WriteElementInit(ElementInit elementInit);
        protected abstract void WriteBinding(MemberBinding binding);
        protected abstract void WriteSwitchCase(SwitchCase switchCase);
        protected abstract void WriteCatchBlock(CatchBlock catchBlock);
        protected abstract void WriteLabelTarget(LabelTarget labelTarget);
        //protected abstract void WriteIArgumentProvider(IArgumentProvider iArgumentProvider); 

        // binders
        protected abstract void WriteBinaryOperationBinder(BinaryOperationBinder binaryOperationBinder, IList<Expression> args);
        protected abstract void WriteConvertBinder(ConvertBinder convertBinder, IList<Expression> args);
        protected abstract void WriteCreateInstanceBinder(CreateInstanceBinder createInstanceBinder, IList<Expression> args);
        protected abstract void WriteDeleteIndexBinder(DeleteIndexBinder deleteIndexBinder, IList<Expression> args);
        protected abstract void WriteDeleteMemberBinder(DeleteMemberBinder deleteMemberBinder, IList<Expression> args);
        protected abstract void WriteGetIndexBinder(GetIndexBinder getIndexBinder, IList<Expression> args);
        protected abstract void WriteGetMemberBinder(GetMemberBinder getMemberBinder, IList<Expression> args);
        protected abstract void WriteInvokeBinder(InvokeBinder invokeBinder, IList<Expression> args);
        protected abstract void WriteInvokeMemberBinder(InvokeMemberBinder invokeMemberBinder, IList<Expression> args);
        protected abstract void WriteSetIndexBinder(SetIndexBinder setIndexBinder, IList<Expression> args);
        protected abstract void WriteSetMemberBinder(SetMemberBinder setMemberBinder, IList<Expression> args);
        protected abstract void WriteUnaryOperationBinder(UnaryOperationBinder unaryOperationBinder, IList<Expression> args);

        protected abstract void WriteParameterDeclarationImpl(ParameterExpression prm);
    }
}
