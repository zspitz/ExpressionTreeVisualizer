using ExpressionToString.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using static ExpressionToString.Util.Functions;
using static ExpressionToString.FormatterNames;
using static System.Linq.Expressions.ExpressionType;
using static System.Linq.Enumerable;
using static ExpressionToString.Globals;

namespace ExpressionToString {
    public class VBCodeWriter : CodeWriter {
        public VBCodeWriter(object o) : base(o) { }
        public VBCodeWriter(object o, out Dictionary<object, List<(int start, int length)>> visitedObjects) : base(o, out visitedObjects) { }

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
                        expr.Type.FriendlyName(VisualBasic).AppendTo(sb);
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
            if (expr.ReturnType == typeof(void)) {
                "Sub".AppendTo(sb);
            } else {
                "Function".AppendTo(sb);
            }
            "(".AppendTo(sb);
            expr.Parameters.ForEach((prm, index) => {
                if (index > 0) { ", ".AppendTo(sb); }
                Write(prm, true);
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
                case MemberExpression mexpr when mexpr.Type.IsClosureClass():
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
                    if (!(arg is MemberExpression mexpr && mexpr.Member.Name.Replace("$VB$Local_", "") == name)) {
                        $".{name} = ".AppendTo(sb);
                    }
                    Write(arg);
                });
                "}".AppendTo(sb);
            } else {
                expr.Type.FriendlyName(VisualBasic).AppendTo(sb);
                if (expr.Arguments.Any()) {
                    "(".AppendTo(sb);
                    WriteList(expr.Arguments);
                    ")".AppendTo(sb);
                }
            }
        }

        static readonly MethodInfo power = typeof(Math).GetMethod("Pow");

        protected override void WriteCall(MethodCallExpression expr) {
            if (expr.Method.In(stringConcats)) {
                var firstArg = expr.Arguments[0];
                IEnumerable<Expression> argsToWrite = null;
                if (firstArg is NewArrayExpression newArray && firstArg.NodeType == NewArrayInit) {
                    argsToWrite = newArray.Expressions;
                } else if (expr.Arguments.All(x => x.Type == typeof(string))) {
                    argsToWrite = expr.Arguments;
                }
                if (argsToWrite != null) {
                    WriteList(argsToWrite, " + ");
                    return;
                }
            }

            bool isIndexer = false;
            if ((expr.Object?.Type.IsArray ?? false) && expr.Method.Name == "Get") {
                isIndexer = true;
            } else {
                var indexerMethods = expr.Method.ReflectedType.GetIndexers(true).SelectMany(x => new[] { x.GetMethod, x.SetMethod }).ToList();
                isIndexer = expr.Method.In(indexerMethods);
            }
            if (isIndexer) {
                Write(expr.Object);
                "(".AppendTo(sb);
                WriteList(expr.Arguments);
                ")".AppendTo(sb);
                return;
            }

            if (expr.Method.In(stringFormats) && expr.Arguments[0] is ConstantExpression cexpr && cexpr.Value is string format) {
                var parts = ParseFormatString(format);
                "$\"".AppendTo(sb);
                foreach (var (literal, index, alignment, itemFormat) in parts) {
                    literal.Replace("{", "{{").Replace("}", "}}").AppendTo(sb);
                    if (index == null) { break; }
                    "{".AppendTo(sb);
                    Write(expr.Arguments[index.Value + 1]);
                    if (alignment != null) { $", {alignment}".AppendTo(sb); }
                    if (itemFormat != null) { $":{itemFormat}".AppendTo(sb); }
                    "}".AppendTo(sb);
                }
                "\"".AppendTo(sb);
                return;
            }

            if (expr.Method == power) {
                Write(expr.Arguments[0]);
                " ^ ".AppendTo(sb);
                Write(expr.Arguments[1]);
                return;
            }

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
                expr.Method.ReflectedType.FriendlyName(VisualBasic).AppendTo(sb);
            } else {
                Write(instance);
            }

            $".{expr.Method.Name}".AppendTo(sb);
            if (arguments.Any()) {
                "(".AppendTo(sb);
                WriteList(arguments);
                ")".AppendTo(sb);
            }
        }

        protected override void WriteBinding(MemberBinding binding) {
            switch (binding) {
                case MemberAssignment assignmentBinding:
                    ".".AppendTo(sb);
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
                " With {".AppendTo(sb);
                WriteList(expr.Bindings);
                "}".AppendTo(sb);
            }
        }

        protected override void WriteListInit(ListInitExpression expr) {
            Write(expr.NewExpression);
            " From {".AppendTo(sb);
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
                    WriteList(args);
                    "}".AppendTo(sb);
                    break;
            }
        }

        protected override void WriteNewArray(NewArrayExpression expr) {
            switch (expr.NodeType) {
                case NewArrayInit:
                    var elementType = expr.Type.GetElementType();
                    if (expr.Expressions.None() || expr.Expressions.Any(x => x.Type != elementType)) {
                        $"New {expr.Type.FriendlyName(VisualBasic)} ".AppendTo(sb);
                    }
                    "{ ".AppendTo(sb);
                    expr.Expressions.ForEach((arg, index) => {
                        if (index > 0) { ", ".AppendTo(sb); }
                        if (arg.NodeType == NewArrayInit) { "(".AppendTo(sb); }
                        Write(arg);
                        if (arg.NodeType == NewArrayInit) { ")".AppendTo(sb); }
                    });
                    " }".AppendTo(sb);
                    break;
                case NewArrayBounds:
                    (string left, string right) specifierChars = ("(", ")");
                    var nestedArrayTypes = expr.Type.NestedArrayTypes().ToList();
                    $"New {nestedArrayTypes.Last().root.FriendlyName(VisualBasic)}".AppendTo(sb);
                    nestedArrayTypes.ForEachT((current, _, arrayTypeIndex) => {
                        specifierChars.left.AppendTo(sb);
                        if (arrayTypeIndex == 0) {
                            expr.Expressions.ForEach((x, index) => {
                                if (index > 0) { ", ".AppendTo(sb); }
                                // because in VB.NET the upper bound of an array is specified, not the numbe of items
                                if (x is ConstantExpression cexpr) {
                                    string newValue = (((dynamic)cexpr.Value) - 1).ToString();
                                    newValue.AppendTo(sb);
                                } else {
                                    Write(Expression.SubtractChecked(x, Expression.Constant(1)));
                                }
                            });
                        } else {
                            Repeat("", current.GetArrayRank()).Joined().AppendTo(sb);
                        }
                        specifierChars.right.AppendTo(sb);
                    });
                    " {}".AppendTo(sb);
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        protected override void WriteConditional(ConditionalExpression expr) {
            "If(".AppendTo(sb);
            Write(expr.Test);
            ", ".AppendTo(sb);
            Write(expr.IfTrue);
            ", ".AppendTo(sb);
            Write(expr.IfFalse);
            ")".AppendTo(sb);
        }

        protected override void WriteDefault(DefaultExpression expr) =>
            $"CType(Nothing, {expr.Type.FriendlyName(VisualBasic)})".AppendTo(sb);

        protected override void WriteTypeBinary(TypeBinaryExpression expr) {
            switch (expr.NodeType) {
                case TypeIs:
                    "TypeOf ".AppendTo(sb);
                    Write(expr.Expression);
                    $" Is {expr.TypeOperand.FriendlyName(VisualBasic)}".AppendTo(sb);
                    break;
                case TypeEqual:
                    Write(expr.Expression);
                    $".GetType = GetType({expr.TypeOperand.FriendlyName(VisualBasic)})".AppendTo(sb);
                    break;
            }
        }

        protected override void WriteInvocation(InvocationExpression expr) {
            if (expr.Expression is LambdaExpression) { "(".AppendTo(sb); }
            Write(expr.Expression);
            if (expr.Expression is LambdaExpression) { ")".AppendTo(sb); }
            "(".AppendTo(sb);
            WriteList(expr.Arguments);
            ")".AppendTo(sb);
        }

        protected override void WriteIndex(IndexExpression expr) {
            Write(expr.Object);
            "(".AppendTo(sb);
            WriteList(expr.Arguments);
            ")".AppendTo(sb);
        }
    }
}
