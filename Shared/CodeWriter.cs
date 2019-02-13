using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using static System.Linq.Expressions.ExpressionType;
using static ExpressionTreeTransform.Util.Globals;
using ExpressionTreeTransform.Util;

namespace ExpressionTreeTransform {
    public abstract class CodeWriter {
        public static CodeWriter Create(string language, Expression expr) =>
            language == CSharp ? (CodeWriter)new CSharpCodeWriter(expr) :
            language == VisualBasic ? new VBCodeWriter(expr) :
            throw new NotImplementedException("Unknown language");

        public static CodeWriter Create(string language, Expression expr, out Dictionary<object, List<(int start, int length)>> visitedObjects) =>
            language == CSharp ? (CodeWriter)new CSharpCodeWriter(expr, out visitedObjects) :
            language == VisualBasic ? new VBCodeWriter(expr, out visitedObjects) :
            throw new NotImplementedException("Unknown language");

        protected StringBuilder sb = new StringBuilder();
        Dictionary<object, List<(int start, int length)>> visitedObjects;

        public CodeWriter(Expression expr) {
            Write(expr);
        }
        public CodeWriter(Expression expr, out Dictionary<object, List<(int start, int length)>> visitedObjects) {
            this.visitedObjects = new Dictionary<object, List<(int start, int length)>>();
            Write(expr);
            visitedObjects = this.visitedObjects;
        }

        protected void Write(object o) {
            var start = sb.Length;
            switch (o) {
                case Expression expr:
                    WriteExpression(expr);
                    break;
                case MemberBinding binding:
                    WriteBinding(binding);
                    break;
                case ElementInit init:
                    WriteElementInit(init);
                    break;
                // parameter declaration has to be done separately, because even though it's a parameter expression, the declaration is different
            }

            registerVisited(o, start);
        }

        private void WriteExpression(Expression expr) {
            switch (expr.NodeType) {

                #region BinaryExpression

                // mathematical operations
                case Add:
                case AddChecked:
                case Divide:
                case Modulo:
                case Multiply:
                case MultiplyChecked:
                case Power:
                case Subtract:
                case SubtractChecked:

                // bitwise / logical operations
                case And:
                case Or:
                case ExclusiveOr:

                // shift operations
                case LeftShift:
                case RightShift:

                // conditional boolean operators
                case AndAlso:
                case OrElse:

                // comparison operators
                case Equal:
                case NotEqual:
                case GreaterThanOrEqual:
                case GreaterThan:
                case LessThan:
                case LessThanOrEqual:

                // coalescing operators
                case Coalesce:

                // array indexing operations
                case ArrayIndex:
                    WriteBinary(expr as BinaryExpression);
                    break;

                #endregion

                #region UnaryExpression

                case ArrayLength:
                case ExpressionType.Convert:
                case ConvertChecked:
                case Negate:
                case NegateChecked:
                case Not:
                case Quote:
                case TypeAs:
                case UnaryPlus:
                    WriteUnary(expr as UnaryExpression);
                    break;

                #endregion

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

                default:
                    throw new NotImplementedException($"NodeType: {expr.NodeType}, Expression object type: {expr.GetType().Name}");

                    #region Missing cases
                    /*case AddAssign:
                    case AddAssignChecked:
                    case AndAssign:
                    case Assign:
                    case Block:
                    case Conditional:
                    case DebugInfo:
                    case Decrement:
                    case Default:
                    case DivideAssign:
                    case Dynamic:
                    case ExclusiveOrAssign:
                    case Extension:
                    case Goto:
                    case Increment:
                    case Index:
                    case Invoke:
                    case IsFalse:
                    case IsTrue:
                    case Label:
                    case LeftShiftAssign:
                    case Loop:
                    case ModuloAssign:
                    case MultiplyAssign:
                    case MultiplyAssignChecked:
                    case NewArrayBounds:
                    case NewArrayInit:
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
                    case TypeEqual:
                    case TypeIs:
                    case Unbox:
                    */
                    #endregion
            }
        }

        protected void WriteParameterDeclaration(ParameterExpression prm) {
            var start = sb.Length;
            WriteParameterDeclarationImpl(prm);
            registerVisited(prm, start);
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

        //protected abstract void Write(BlockExpression expr) => throw new NotImplementedException();
        //protected abstract void Write(ConditionalExpression expr) => throw new NotImplementedException();
        //protected abstract void Write(DebugInfoExpression expr) => throw new NotImplementedException();
        //protected abstract void Write(DefaultExpression expr) => throw new NotImplementedException();
        //protected abstract void Write(DynamicExpression expr) => throw new NotImplementedException();
        //protected abstract void Write(GotoExpression expr) => throw new NotImplementedException();
        //protected abstract void Write(IndexExpression expr) => throw new NotImplementedException();
        //protected abstract void Write(InvocationExpression expr) => throw new NotImplementedException();
        //protected abstract void Write(LabelExpression expr) => throw new NotImplementedException();
        //protected abstract void Write(LoopExpression expr) => throw new NotImplementedException();
        //protected abstract void Write(NewArrayExpression expr) => throw new NotImplementedException();
        //protected abstract void Write(RuntimeVariablesExpression expr) => throw new NotImplementedException();
        //protected abstract void Write(SwitchExpression expr) => throw new NotImplementedException();
        //protected abstract void Write(TryExpression expr) => throw new NotImplementedException();
        //protected abstract void Write(TypeBinaryExpression expr) => throw new NotImplementedException();

        //protected abstract void WriteBinding(MemberBinding binding);

        protected abstract void WriteElementInit(ElementInit elementInit);
        protected abstract void WriteBinding(MemberBinding binding);

        protected abstract void WriteParameterDeclarationImpl(ParameterExpression prm);
    }
}
