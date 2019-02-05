using ExpressionTreeTransform.Util;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static ExpressionTreeTransform.Util.Globals;
using static System.Linq.Expressions.ExpressionType;
using static ExpressionTreeTransform.Util.Functions;
using System.Linq;
using System.Runtime.CompilerServices;

namespace ExpressionTreeTransform {
    public class VBCodeWriter : CodeWriter {
        public VBCodeWriter(Expression expr) : base(expr) { }
        public VBCodeWriter(Expression expr, out Dictionary<object, List<(int start, int length)>> visitedObjects) : base(expr, out visitedObjects) { }

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
            if (simpleBinaryOperators.TryGetValue(expr.NodeType, out var @operator)) {
                Write(expr.Left);
                $" {@operator} ".AppendTo(sb);
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

        private static Dictionary<Type, string> conversionFunctions = new Dictionary<Type, string>() {
            {typeof(bool), "CBool"},
            {typeof(byte), "CByte"},
            {typeof(char), "CChar"},
            {typeof(DateTime), "CDate"},
            {typeof(double), "CDbl"},
            {typeof(decimal), "CDec"},
            {typeof(int), "CInt"},
            {typeof(long), "CLng"},
            {typeof(object), "CObj"},
            {typeof(sbyte), "CSByte"},
            {typeof(short), "CShort"},
            {typeof(float), "CSng"},
            {typeof(string), "CStr"},
            {typeof(uint), "CUInt"},
            {typeof(ulong), "CULng"},
            {typeof(ushort), "CUShort" }
        };

        protected override void WriteUnary(UnaryExpression expr) {
            switch (expr.NodeType) {
                case ArrayLength:
                    Write(expr.Operand);
                    ".Length".AppendTo(sb);
                    break;
                case ExpressionType.Convert:
                case ConvertChecked:
                    if (conversionFunctions.TryGetValue(expr.Type, out var conversionFunction)) {
                        conversionFunction.AppendTo(sb);
                        "(".AppendTo(sb);
                        Write(expr.Operand);
                        ")".AppendTo(sb);
                    } else {
                        "CType(".AppendTo(sb);
                        Write(expr.Operand);
                        ", ".AppendTo(sb);
                        expr.Type.FriendlyName(CSharp).AppendTo(sb);
                        ")".AppendTo(sb);
                    }
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

        protected override void WriteConstant(ConstantExpression expr) =>
            RenderLiteral(expr.Value, VisualBasic).AppendTo(sb);

        protected override void WriteMemberAccess(MemberExpression expr) {
            switch (expr.Expression) {
                case ConstantExpression cexpr when cexpr.Type.IsClosureClass():
                    // closed over variable from outer scope
                    expr.Member.Name.Replace("$VB$Local_", "").AppendTo(sb);
                    return;
                case null:
                    // static member
                    $"{expr.Member.DeclaringType.FriendlyName(VisualBasic)}.{expr.Member.Name}".AppendTo(sb);
                    return;
                default:
                    Write(expr.Expression);
                    $".{expr.Member.Name}".AppendTo(sb);
                    return;
            }
        }

        protected override void WriteNew(NewExpression expr) {
            "New ".AppendTo(sb);
            if (expr.Type.IsAnonymous()) {
                "With {".AppendTo(sb);
                expr.Constructor.GetParameters().Select(x => x.Name).Zip(expr.Arguments).ForEachT((name, arg, index) => {
                    if (index > 0) { ", ".AppendTo(sb); }
                    $".{name} = ".AppendTo(sb);
                    Write(arg);
                });
                " }".AppendTo(sb);
            } else {
                expr.Type.FriendlyName(VisualBasic).AppendTo(sb);
                "(".AppendTo(sb);
                expr.Arguments.ForEach((arg, index) => {
                    if (index > 0) { ", ".AppendTo(sb); }
                    Write(arg);
                });
                ")".AppendTo(sb);
            }
        }

        protected override void WriteCall(MethodCallExpression expr) {
            Expression instance = null;
            IEnumerable<Expression> arguments = expr.Arguments;

            if (expr.Object != null) {
                // instance method
                instance = expr.Object;
            } else if (expr.Method.HasAttribute<ExtensionAttribute>()) {
                // extension method
                instance = expr.Arguments[0];
                arguments = expr.Arguments.Skip(1);
            }

            if (instance == null) {
                expr.Method.ReflectedType.FriendlyName(CSharp).AppendTo(sb);
            } else {
                Write(instance);
            }

            $".{expr.Method.Name}".AppendTo(sb);
            if (arguments.Any()) {
                "(".AppendTo(sb);
                arguments.ForEach((arg, index) => {
                    if (index > 0) { ", ".AppendTo(sb); }
                    Write(arg);
                });
                ")".AppendTo(sb);
            }
        }

        protected override void WriteBinding(MemberBinding binding) {
            switch (binding) {
                case MemberAssignment assignmentBinding:
                    binding.Member.Name.AppendTo(sb);
                    " = ".AppendTo(sb);
                    Write(assignmentBinding.Expression);
                    break;
                case MemberListBinding listBinding:
                    throw new NotImplementedException();
                case MemberMemberBinding memberBinding:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }

        protected override void WriteMemberInit(MemberInitExpression expr) {
            Write(expr.NewExpression);
            if (expr.Bindings.Any()) {
                " With { ".AppendTo(sb);
                expr.Bindings.ForEach((binding, index) => {
                    if (index > 0) { ", ".AppendTo(sb); }
                    WriteBinding(binding);
                });
                " }".AppendTo(sb);
            }
        }

        protected override void WriteListInit(ListInitExpression expr) {
            Write(expr.NewExpression);
            " {".AppendTo(sb);
            expr.Initializers.ForEach((init, index) => {
                if (index > 0) { ", ".AppendTo(sb); }
                Write(init);
            });
            "}".AppendTo(sb);
        }

        protected override void WriteElementInit(ElementInit elementInit) {
            var args = elementInit.Arguments;
            switch (args.Count) {
                case 0:
                    throw new NotImplementedException();
                case 1:
                    Write(args.First());
                    break;
                default:
                    "{".AppendTo(sb);
                    args.ForEach((arg, index) => {
                        if (index > 0) { ", ".AppendTo(sb); }
                        Write(arg);
                    });
                    "}".AppendTo(sb);
                    break;
            }
        }
    }
}
