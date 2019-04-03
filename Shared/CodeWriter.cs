using ExpressionToString.Util;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using static ExpressionToString.FormatterNames;
using static System.Linq.Expressions.ExpressionType;

namespace ExpressionToString {
    public abstract class CodeWriter {
        public static CodeWriter Create(string language, object o) =>
            language == CSharp ? (CodeWriter)new CSharpCodeWriter(o) :
            language == VisualBasic ? new VBCodeWriter(o) :
            throw new NotImplementedException("Unknown language");

        public static CodeWriter Create(string language, object o, out Dictionary<object, List<(int start, int length)>> visitedObjects) =>
            language == CSharp ? (CodeWriter)new CSharpCodeWriter(o, out visitedObjects) :
            language == VisualBasic ? new VBCodeWriter(o, out visitedObjects) :
            throw new NotImplementedException("Unknown language");

        private readonly StringBuilder sb = new StringBuilder();
        private readonly Dictionary<object, List<(int start, int length)>> visitedObjects;

        // Unfortunately, C# doesn't support union types ...
        protected CodeWriter(object o) => Write(o);

        protected CodeWriter(object o, out Dictionary<object, List<(int start, int length)>> visitedObjects) {
            this.visitedObjects = new Dictionary<object, List<(int start, int length)>>();
            Write(o);
            visitedObjects = this.visitedObjects;
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

        /// <summary>Write a string-rendering of an expression or other type used in expression trees</summary>
        /// <param name="o">Object to be rendered</param>
        /// <param name="parameterDeclaration">For ParameterExpression, this is a parameter declaration</param>
        /// <param name="explicitBlock">For BlockExpression, controls explicit block rendering: true forces the rendering; false prevents the rendering; and null determines automatically</param>
        protected void Write(object o, bool parameterDeclaration = false, bool? explicitBlock = null) {
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

                    default:
                        throw new NotImplementedException($"Code generation not implemented for type '{o.GetType().Name}'");
                }
            } catch (Exception ex) {
                sb.AppendLine();
                $"----- {ex.GetType().Name} - {ex.Message} ()".AppendLineTo(sb);
            }

            registerVisited(o, start);
        }

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

                default:
                    throw new NotImplementedException($"NodeType: {expr.NodeType}, Expression object type: {expr.GetType().Name}");

                    #region Missing cases
                    /*case DebugInfo:
                    case Dynamic:
                    case Extension:
                    case Goto:
                    case Label:
                    case Loop:
                    case RuntimeVariables:
                    case Quote:
                    case Unbox:
                    */
                    #endregion
            }
        }

        protected void WriteList<T>(IEnumerable<T> items, bool writeEOL, string delimiter = ", ") {
            if (writeEOL) { delimiter = delimiter.TrimEnd(); }
            items.ForEach((arg, index) => {
                if (index > 0) {
                    delimiter.AppendTo(sb);
                    if (writeEOL) { WriteEOL(); }
                }
                Write(arg);
            });
        }

        protected void WriteList<T>(IEnumerable<T> items, string delimiter = ", ") => WriteList(items, false, delimiter);

        protected void TrimEnd() => sb.TrimEnd(false);

        private void registerVisited(object o, int start) {
            if (visitedObjects == null) { return; }
            if (!visitedObjects.TryGetValue(o, out var spans)) {
                spans = new List<(int start, int length)>();
                visitedObjects[o] = spans;
            }
            spans.Add((start, sb.Length - start));
        }

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

        // other types
        protected abstract void WriteElementInit(ElementInit elementInit);
        protected abstract void WriteBinding(MemberBinding binding);
        protected abstract void WriteSwitchCase(SwitchCase switchCase);
        protected abstract void WriteCatchBlock(CatchBlock catchBlock);

        //protected abstract void Write(GotoExpression expr) => throw new NotImplementedException();
        //protected abstract void Write(LoopExpression expr) => throw new NotImplementedException();
        //protected abstract void WriteLabelTarget(LabelTarget labelTarget);

        //protected abstract void Write(DynamicExpression expr) => throw new NotImplementedException();
        //protected abstract void WriteIDynamicExpression(IDynamicExpression iDynamicExpression); 

        //protected abstract void Write(DebugInfoExpression expr) => throw new NotImplementedException();
        //protected abstract void WriteSymbolDocumentInfo(SymbolDocumentInfo symbolDocumentInfo); 

        //protected abstract void Write(RuntimeVariablesExpression expr) => throw new NotImplementedException();

        //protected abstract void WriteIArgumentProvider(IArgumentProvider iArgumentProvider); 

        protected abstract void WriteParameterDeclarationImpl(ParameterExpression prm);
    }
}
