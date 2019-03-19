using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using static System.Linq.Expressions.ExpressionType;
using static ExpressionToString.FormatterNames;
using ExpressionToString.Util;
using System.Diagnostics;

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

        private StringBuilder sb = new StringBuilder();
        Dictionary<object, List<(int start, int length)>> visitedObjects;

        // Unfortunately, C# doesn't support union types ...
        public CodeWriter(object o) => Write(o);
        public CodeWriter(object o, out Dictionary<object, List<(int start, int length)>> visitedObjects) {
            this.visitedObjects = new Dictionary<object, List<(int start, int length)>>();
            Write(o);
            visitedObjects = this.visitedObjects;
        }

        protected void Write(string s) => s.AppendTo(sb);

        protected void Write(object o, bool parameterDeclaration = false) {
            var start = sb.Length;
            try {
                switch (o) {
                    // parameter declaration has to be done separately, because even though it's a parameter expression, the declaration is different
                    case ParameterExpression pexpr when parameterDeclaration:
                        WriteParameterDeclarationImpl(pexpr);
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
            ArrayIndex
        }.ToHashSet();

        private readonly HashSet<ExpressionType> unaryExpressionTypes = new[] {
            ArrayLength, ExpressionType.Convert, ConvertChecked, Negate, NegateChecked, Not, Quote, TypeAs, UnaryPlus
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

                default:
                    throw new NotImplementedException($"NodeType: {expr.NodeType}, Expression object type: {expr.GetType().Name}");

                    #region Missing cases
                    /*case AddAssign:
                    case AddAssignChecked:
                    case AndAssign:
                    case Assign:
                    case Block:
                    case DebugInfo:
                    case Decrement:
                    case DivideAssign:
                    case Dynamic:
                    case ExclusiveOrAssign:
                    case Extension:
                    case Goto:
                    case Increment:
                    case IsFalse:
                    case IsTrue:
                    case Label:
                    case LeftShiftAssign:
                    case Loop:
                    case ModuloAssign:
                    case MultiplyAssign:
                    case MultiplyAssignChecked:
                    case OnesComplement:
                    case OrAssign:
                    case PostDecrementAssign:
                    case PostIncrementAssign:
                    case PowerAssign:
                    case PreDecrementAssign:
                    case PreIncrementAssign:
                    case RightShiftAssign:
                    case RuntimeVariables:
                    case SubtractAssign:
                    case SubtractAssignChecked:
                    case Switch:
                    case Throw:
                    case Try:
                    case Unbox:
                    */
                    #endregion
            }
        }

        protected void WriteList<T>(IEnumerable<T> items, string delimiter = ", ") {
            items.ForEach((arg, index) => {
                if (index > 0) { delimiter.AppendTo(sb); }
                Write(arg);
            });
        }

        private void registerVisited(object o, int start) {
            if (visitedObjects == null) { return; }
            if (!visitedObjects.TryGetValue(o, out var spans)) {
                spans = new List<(int start, int length)>();
                visitedObjects[o] = spans;
            }
            spans.Add((start, sb.Length - start));
        }

        public override string ToString() => sb.ToString();

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

        //protected abstract void Write(BlockExpression expr) => throw new NotImplementedException();
        //protected abstract void Write(DebugInfoExpression expr) => throw new NotImplementedException();
        //protected abstract void Write(DynamicExpression expr) => throw new NotImplementedException();
        //protected abstract void Write(GotoExpression expr) => throw new NotImplementedException();
        //protected abstract void Write(LabelExpression expr) => throw new NotImplementedException();
        //protected abstract void Write(LoopExpression expr) => throw new NotImplementedException();
        //protected abstract void Write(RuntimeVariablesExpression expr) => throw new NotImplementedException();
        //protected abstract void Write(SwitchExpression expr) => throw new NotImplementedException();
        //protected abstract void Write(TryExpression expr) => throw new NotImplementedException();

        //protected abstract void WriteBinding(MemberBinding binding);

        protected abstract void WriteElementInit(ElementInit elementInit);
        protected abstract void WriteBinding(MemberBinding binding);

        protected abstract void WriteParameterDeclarationImpl(ParameterExpression prm);
    }
}
