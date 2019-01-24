using ExpressionTreeTransform.Util;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static ExpressionTreeTransform.Util.Globals;
using static System.Linq.Expressions.ExpressionType;

namespace ExpressionTreeTransform {
    public class VBCodeWriter : CodeWriter {
        public VBCodeWriter(Expression expr) : base(expr) { }
        public VBCodeWriter(Expression expr, out Dictionary<object, (int start, int length)> visitedObjects) : base(expr, out visitedObjects) { }

        private static readonly Dictionary<ExpressionType, string> simpleBinaryOperators = new Dictionary<ExpressionType, string>() {
            [Add] = "+",
            [AddChecked] = "+",
            [Divide] = "/",
            [Modulo] = "Mod",
            [Multiply] = "*",
            [MultiplyChecked] = "*",
            [Subtract] = "-",
            [SubtractChecked] = "-",
            [And] = "And",
            [Or] = "Or",
            [ExclusiveOr] = "Xor",
            [AndAlso] = "AndAlso",
            [OrElse] = "OrElse",
            [Equal] = "=",
            [NotEqual] = "<>",
            [GreaterThanOrEqual] = ">=",
            [GreaterThan] = ">",
            [LessThan] = "<",
            [LessThanOrEqual] = "<=",
            [LeftShift] = "<<",
            [RightShift] = ">>",
            [Power] = "^"
        };

        protected override void WriteBinary(BinaryExpression expr) {
            if (simpleBinaryOperators.TryGetValue(expr.NodeType, out var operand)) {
                Write(expr.Left);
                $" {operand} ".AppendTo(sb);
                Write(expr.Right);
                return;
            }

            switch (expr.NodeType) {
                case ArrayIndex:
                    Write(expr.Left);
                    "(".AppendTo(sb);
                    Write(expr.Right);
                    ")".AppendTo(sb);
                    return;
                case Coalesce:
                    "If(".AppendTo(sb);
                    Write(expr.Left);
                    ", ".AppendTo(sb);
                    Write(expr.Right);
                    ")".AppendTo(sb);
                    return;
            }

            throw new NotImplementedException();
        }

        protected override void WriteUnary(UnaryExpression expr) {
            switch (expr.NodeType) {
                case ArrayLength:
                    Write(expr.Operand);
                    ".Length".AppendTo(sb);
                    break;
                case ExpressionType.Convert:
                case ConvertChecked:
                    "CType(".AppendTo(sb);
                    Write(expr.Operand);
                    ", ".AppendTo(sb);
                    expr.Type.FriendlyName(CSharp).AppendTo(sb);
                    ")".AppendTo(sb);
                    break;
                case Negate:
                case NegateChecked:
                    "-".AppendTo(sb);
                    Write(expr.Operand);
                    break;
                case Not:
                    "Not ".AppendTo(sb);
                    Write(expr.Operand);
                    break;
                case TypeAs:
                    "TryCast(".AppendTo(sb);
                    Write(expr.Operand);
                    $", {expr.Type.FriendlyName(VisualBasic)})".AppendTo(sb);
                    break;
                default:
                    throw new NotImplementedException($"NodeType: {expr.NodeType}, Expression object type: {expr.GetType().Name}");
            }
        }

        protected override void WriteLambda(LambdaExpression expr) {
            if (expr.Type == typeof(void)) {
                "Sub".AppendTo(sb);
            } else {
                "Function".AppendTo(sb);
            }
            "(".AppendTo(sb);
            expr.Parameters.ForEach((prm, index) => {
                if (index>0) { ", ".AppendTo(sb); }
                WriteParameterDeclaration(prm);
            });
            ") ".AppendTo(sb);
            Write(expr.Body);
        }

        protected override void WriteParameterDeclarationImpl(ParameterExpression prm) =>
            $"{prm.Name} As {prm.Type.FriendlyName(VisualBasic)}".AppendTo(sb);

        protected override void WriteParameter(ParameterExpression expr) => expr.Name.AppendTo(sb);

        protected override void WriteConstant(ConstantExpression expr) {
            throw new NotImplementedException();
        }

        protected override void WriteMemberAccess(MemberExpression expr) {
            throw new NotImplementedException();
        }

        protected override void WriteNew(NewExpression expr) {
            throw new NotImplementedException();
        }

        protected override void WriteCall(MethodCallExpression expr) {
            throw new NotImplementedException();
        }

        protected override void WriteMemberInit(MemberInitExpression expr) {
            throw new NotImplementedException();
        }

        protected override void WriteListInit(ListInitExpression expr) {
            throw new NotImplementedException();
        }

        protected override void WriteBinding(MemberBinding binding) {
            throw new NotImplementedException();
        }

        protected override void WriteElementInit(ElementInit elementInit) {
            throw new NotImplementedException();
        }
    }
}
