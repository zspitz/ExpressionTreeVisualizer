using ExpressionToString.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using static ExpressionToString.FormatterNames;
using static ExpressionToString.Globals;
using static ExpressionToString.Util.Functions;
using static System.Linq.Enumerable;
using static System.Linq.Expressions.ExpressionType;
using static System.Linq.Expressions.GotoExpressionKind;

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
            [GreaterThanOrEqual] = ">=",
            [GreaterThan] = ">",
            [LessThan] = "<",
            [LessThanOrEqual] = "<=",
            [LeftShift] = "<<",
            [RightShift] = ">>",
            [Power] = "^",
            [Assign] = "=",
            [AddAssign] = "+=",
            [AddAssignChecked] = "+=",
            [DivideAssign] = "/=",
            [LeftShiftAssign] = "<<=",
            [MultiplyAssign] = "*=",
            [MultiplyAssignChecked] = "*=",
            [PowerAssign] = "^=",
            [RightShiftAssign] = ">>=",
            [SubtractAssign] = "-=",
            [SubtractAssignChecked] = "-="
        };

        protected override void WriteBinary(BinaryExpression expr) {
            if (simpleBinaryOperators.TryGetValue(expr.NodeType, out var @operator)) {
                Write(expr.Left);
                Write($" {@operator} ");
                Write(expr.Right);
                return;
            }

            switch (expr.NodeType) {
                case ArrayIndex:
                    Write(expr.Left);
                    Write("(");
                    Write(expr.Right);
                    Write(")");
                    return;
                case Coalesce:
                    Write("If(");
                    Write(expr.Left);
                    Write(", ");
                    Write(expr.Right);
                    Write(")");
                    return;
                case OrAssign:
                case AndAssign:
                case ExclusiveOrAssign:
                case ModuloAssign:
                    var op = (ExpressionType)Enum.Parse(typeof(ExpressionType), expr.NodeType.ToString().Replace("Assign", ""));
                    Write(expr.Left);
                    Write(" = ");
                    Write(expr.Left);
                    Write($" {simpleBinaryOperators[op]} ");
                    Write(expr.Right);
                    return;
                case Equal:
                    Write(expr.Left);
                    Write(expr.IsReferenceComparison() ?
                        " Is " :
                        " = "
                    );
                    Write(expr.Right);
                    return;
                case NotEqual:
                    Write(expr.Left);
                    Write(expr.IsReferenceComparison() ?
                        " IsNot " :
                        " <> "
                    );
                    Write(expr.Right);
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
                    Write(".Length");
                    break;
                case ExpressionType.Convert:
                case ConvertChecked:
                    if (conversionFunctions.TryGetValue(expr.Type, out var conversionFunction)) {
                        Write(conversionFunction);
                        Write("(");
                        Write(expr.Operand);
                        Write(")");
                    } else {
                        Write("CType(");
                        Write(expr.Operand);
                        Write($", {expr.Type.FriendlyName(VisualBasic)})");
                    }
                    break;
                case Negate:
                case NegateChecked:
                    Write("-");
                    Write(expr.Operand);
                    break;
                case Not:
                    Write("Not ");
                    Write(expr.Operand);
                    break;
                case TypeAs:
                    Write("TryCast(");
                    Write(expr.Operand);
                    Write($", {expr.Type.FriendlyName(VisualBasic)})");
                    break;

                case PreIncrementAssign:
                    Write("(");
                    Write(expr.Operand);
                    Write(" += 1 : ");
                    Write(expr.Operand);
                    Write(")");
                    return;
                case PostIncrementAssign:
                    Write("(");
                    Write(expr.Operand);
                    Write(" += 1 : ");
                    Write(expr.Operand);
                    Write(" - 1)");
                    return;
                case PreDecrementAssign:
                    Write("(");
                    Write(expr.Operand);
                    Write(" -= 1 : ");
                    Write(expr.Operand);
                    Write(")");
                    return;
                case PostDecrementAssign:
                    Write("(");
                    Write(expr.Operand);
                    Write(" -= 1 : ");
                    Write(expr.Operand);
                    Write(" + 1)");
                    return;

                case IsTrue:
                    Write(expr.Operand);
                    break;
                case IsFalse:
                    Write("Not ");
                    Write(expr.Operand);
                    break;

                case Increment:
                    Write(expr.Operand);
                    Write(" += 1");
                    break;
                case Decrement:
                    Write(expr.Operand);
                    Write(" -= 1");
                    break;

                case Throw:
                    Write("Throw");
                    if (expr.Operand != null) {
                        Write(" ");
                        Write(expr.Operand);
                    }
                    break;

                default:
                    throw new NotImplementedException($"NodeType: {expr.NodeType}, Expression object type: {expr.GetType().Name}");
            }
        }

        protected override void WriteLambda(LambdaExpression expr) {
            if (expr.ReturnType == typeof(void)) {
                Write("Sub");
            } else {
                Write("Function");
            }
            Write("(");
            expr.Parameters.ForEach((prm, index) => {
                if (index > 0) { Write(", "); }
                Write(prm, true);
            });
            Write(") ");
            Write(expr.Body);
        }

        protected override void WriteParameterDeclarationImpl(ParameterExpression prm) =>
            Write($"{prm.Name} As {prm.Type.FriendlyName(VisualBasic)}");

        protected override void WriteParameter(ParameterExpression expr) => Write(expr.Name);

        protected override void WriteConstant(ConstantExpression expr) =>
            Write(RenderLiteral(expr.Value, VisualBasic));

        protected override void WriteMemberAccess(MemberExpression expr) {
            switch (expr.Expression) {
                case ConstantExpression cexpr when cexpr.Type.IsClosureClass():
                case MemberExpression mexpr when mexpr.Type.IsClosureClass():
                    // closed over variable from outer scope
                    Write(expr.Member.Name.Replace("$VB$Local_", ""));
                    return;
                case null:
                    // static member
                    Write($"{expr.Member.DeclaringType.FriendlyName(VisualBasic)}.{expr.Member.Name}");
                    return;
                default:
                    Write(expr.Expression);
                    Write($".{expr.Member.Name}");
                    return;
            }
        }

        protected override void WriteNew(NewExpression expr) {
            Write("New ");
            if (expr.Type.IsAnonymous()) {
                Write("With {");
                Indent();
                WriteEOL();
                expr.Constructor.GetParameters().Select(x => x.Name).Zip(expr.Arguments).ForEachT((name, arg, index) => {
                    if (index > 0) {
                        Write(",");
                        WriteEOL();
                    }
                    if (!(arg is MemberExpression mexpr && mexpr.Member.Name.Replace("$VB$Local_", "") == name)) {
                        Write($".{name} = ");
                    }
                    Write(arg);
                });
                WriteEOL(true);
                Write("}");
            } else {
                Write(expr.Type.FriendlyName(VisualBasic));
                if (expr.Arguments.Count > 0) {
                    Write("(");
                    WriteList(expr.Arguments);
                    Write(")");
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
                Write("(");
                WriteList(expr.Arguments);
                Write(")");
                return;
            }

            if (expr.Method.In(stringFormats) && expr.Arguments[0] is ConstantExpression cexpr && cexpr.Value is string format) {
                var parts = ParseFormatString(format);
                Write("$\"");
                foreach (var (literal, index, alignment, itemFormat) in parts) {
                    Write(literal.Replace("{", "{{").Replace("}", "}}"));
                    if (index == null) { break; }
                    Write("{");
                    Write(expr.Arguments[index.Value + 1]);
                    if (alignment != null) { Write($", {alignment}"); }
                    if (itemFormat != null) { Write($":{itemFormat}"); }
                    Write("}");
                }
                Write("\"");
                return;
            }

            if (expr.Method == power) {
                Write(expr.Arguments[0]);
                Write(" ^ ");
                Write(expr.Arguments[1]);
                return;
            }

            // Microsoft.VisualBasic.CompilerServices is not available to .NET Standard
            if (expr.Method.DeclaringType.FullName == "Microsoft.VisualBasic.CompilerServices.LikeOperator") {
                Write(expr.Arguments[0]);
                Write(" Like ");
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
                Write(expr.Method.ReflectedType.FriendlyName(VisualBasic));
            } else {
                Write(instance);
            }

            Write($".{expr.Method.Name}");
            if (arguments.Any()) {
                Write("(");
                WriteList(arguments);
                Write(")");
            }
        }

        protected override void WriteBinding(MemberBinding binding) {
            Write(".");
            Write(binding.Member.Name);
            Write(" = ");
            if (binding is MemberAssignment assignmentBinding) {
                Write(assignmentBinding.Expression);
                return;
            }

            

            IEnumerable<object> items = null;
            string initializerKeyword = "";
            switch (binding) {
                case MemberListBinding listBinding when listBinding.Initializers.Count > 0:
                    items = listBinding.Initializers.Cast<object>();
                    initializerKeyword = "From ";
                    break;
                case MemberMemberBinding memberBinding when memberBinding.Bindings.Count > 0:
                    items = memberBinding.Bindings.Cast<object>();
                    initializerKeyword = "With ";
                    break;
            }

            Write($"{initializerKeyword}{{");

            if (items != null) {
                Indent();
                WriteEOL();
                WriteList(items, true);
                WriteEOL(true);
            }

            Write("}");
        }

        protected override void WriteMemberInit(MemberInitExpression expr) {
            Write(expr.NewExpression);
            if (expr.Bindings.Count > 0) {
                Write(" With {");
                Indent();
                WriteEOL();
                WriteList(expr.Bindings, true);
                WriteEOL(true);
                Write("}");
            }
        }

        protected override void WriteListInit(ListInitExpression expr) {
            Write(expr.NewExpression);
            Write(" From {");
            Indent();
            WriteEOL();
            WriteList(expr.Initializers, true);
            WriteEOL(true);
            Write("}");
        }

        protected override void WriteElementInit(ElementInit elementInit) {
            var args = elementInit.Arguments;
            switch (args.Count) {
                case 0:
                    throw new NotImplementedException();
                case 1:
                    Write(args[0]);
                    break;
                default:
                    Write("{");
                    Indent();
                    WriteEOL();
                    WriteList(args, true);
                    WriteEOL(true);
                    Write("}");
                    break;
            }
        }

        protected override void WriteNewArray(NewArrayExpression expr) {
            switch (expr.NodeType) {
                case NewArrayInit:
                    var elementType = expr.Type.GetElementType();
                    if (expr.Expressions.None() || expr.Expressions.Any(x => x.Type != elementType)) {
                        Write($"New {expr.Type.FriendlyName(VisualBasic)} ");
                    }
                    Write("{ ");
                    expr.Expressions.ForEach((arg, index) => {
                        if (index > 0) { Write(", "); }
                        if (arg.NodeType == NewArrayInit) { Write("("); }
                        Write(arg);
                        if (arg.NodeType == NewArrayInit) { Write(")"); }
                    });
                    Write(" }");
                    break;
                case NewArrayBounds:
                    (string left, string right) specifierChars = ("(", ")");
                    var nestedArrayTypes = expr.Type.NestedArrayTypes().ToList();
                    Write($"New {nestedArrayTypes.Last().root.FriendlyName(VisualBasic)}");
                    nestedArrayTypes.ForEachT((current, _, arrayTypeIndex) => {
                        Write(specifierChars.left);
                        if (arrayTypeIndex == 0) {
                            expr.Expressions.ForEach((x, index) => {
                                if (index > 0) { Write(", "); }
                                // because in VB.NET the upper bound of an array is specified, not the numbe of items
                                if (x is ConstantExpression cexpr) {
                                    string newValue = (((dynamic)cexpr.Value) - 1).ToString();
                                    Write(newValue);
                                } else {
                                    Write(Expression.SubtractChecked(x, Expression.Constant(1)));
                                }
                            });
                        } else {
                            Write(Repeat("", current.GetArrayRank()).Joined());
                        }
                        Write(specifierChars.right);
                    });
                    Write(" {}");
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        private bool IndentIfBlockSyntax(Expression expr, (bool leading, bool trailing) nonblockSpaces, bool? explicitBlock = false) {
            if (IsBlockSyntax(expr)) {
                if (explicitBlock ?? false) {
                    Write(expr, false, true);
                } else {
                    Indent();
                    WriteEOL();
                    Write(expr, false, explicitBlock);
                    WriteEOL(true);
                }
                return true;
            } else {
                if (nonblockSpaces.leading) { Write(" "); }
                Write(expr);
                if (nonblockSpaces.trailing) { Write(" "); }
                return false;
            }
        }

        protected override void WriteConditional(ConditionalExpression expr) {
            if (expr.Type == typeof(void)) {
                var lastClauseIsBlock = false;
                Write("If");
                IndentIfBlockSyntax(expr.Test, (true, true));
                Write("Then");
                lastClauseIsBlock = IsBlockSyntax(expr.IfTrue);
                IndentIfBlockSyntax(expr.IfTrue, (true, !expr.IfFalse.IsEmpty()));
                if (!expr.IfFalse.IsEmpty()) {
                    Write("Else");
                    lastClauseIsBlock = IndentIfBlockSyntax(expr.IfFalse, (true, false));
                }
                if (lastClauseIsBlock) {
                    Write("End If");
                }
            } else {
                Write("If(");
                IndentIfBlockSyntax(expr.Test, (false, false), true);
                Write(", ");
                Write(expr.IfTrue);
                Write(", ");
                Write(expr.IfFalse);
                Write(")");
            }
        }

        protected override void WriteDefault(DefaultExpression expr) =>
            Write($"CType(Nothing, {expr.Type.FriendlyName(VisualBasic)})");

        protected override void WriteTypeBinary(TypeBinaryExpression expr) {
            switch (expr.NodeType) {
                case TypeIs:
                    Write("TypeOf ");
                    Write(expr.Expression);
                    Write($" Is {expr.TypeOperand.FriendlyName(VisualBasic)}");
                    break;
                case TypeEqual:
                    Write(expr.Expression);
                    Write($".GetType = GetType({expr.TypeOperand.FriendlyName(VisualBasic)})");
                    break;
            }
        }

        protected override void WriteInvocation(InvocationExpression expr) {
            if (expr.Expression is LambdaExpression) { Write("("); }
            Write(expr.Expression);
            if (expr.Expression is LambdaExpression) { Write(")"); }
            Write("(");
            WriteList(expr.Arguments);
            Write(")");
        }

        protected override void WriteIndex(IndexExpression expr) {
            Write(expr.Object);
            Write("(");
            WriteList(expr.Arguments);
            Write(")");
        }

        protected override void WriteBlock(BlockExpression expr, bool? explicitBlock = null) {
            var useExplicitBlock = explicitBlock ?? expr.Variables.Count > 0;
            if (useExplicitBlock) {
                Write("Block");
                Indent();
                WriteEOL();
                expr.Variables.ForEach((v, index) => {
                    if (index > 0) { WriteEOL(); }
                    Write("Dim ");
                    Write(v, true);
                });
            }
            expr.Expressions.ForEach((subexpr, index) => {
                if (index > 0 || expr.Variables.Count > 0) { WriteEOL(); }
                if (subexpr is LabelExpression) { TrimEnd(); }
                Write(subexpr);
            });
            if (useExplicitBlock) {
                WriteEOL(true);
                Write("End Block");
            }
        }

        private bool IsBlockSyntax(Expression expr) {
            switch (expr) {
                case ConditionalExpression cexpr when cexpr.Type == typeof(void):
                case BlockExpression _:
                case SwitchExpression _:
                case TryExpression _:
                case RuntimeVariablesExpression _:
                    return true;
            }
            return false;
        }

        protected override void WriteSwitchCase(SwitchCase switchCase) {
            Write("Case ");
            WriteList(switchCase.TestValues);
            Indent();
            WriteEOL();
            Write(switchCase.Body);
        }

        protected override void WriteSwitch(SwitchExpression expr) {
            Write("Select Case ");
            Indent();
            Write(expr.SwitchValue, false, true);
            WriteEOL();
            expr.Cases.ForEach((switchCase, index) => {
                if (index > 0) { WriteEOL(); }
                Write(switchCase);
                Dedent();
            });
            if (expr.DefaultBody != null) {
                if (expr.Cases.Count > 0) { WriteEOL(); }
                Write("Case Else");
                Indent();
                WriteEOL();
                Write(expr.DefaultBody);
                Dedent();
            }
            WriteEOL(true);
            Write("End Select");
        }

        protected override void WriteCatchBlock(CatchBlock catchBlock) {
            Write("Catch");
            if (catchBlock.Variable != null) {
                Write(" ");
                Write(catchBlock.Variable, true);
            } else if (catchBlock.Test != null && catchBlock.Test != typeof(Exception)) {
                Write($" _ As {catchBlock.Test.FriendlyName(VisualBasic)}");
            }
            if (catchBlock.Filter != null) {
                Write(" When ");
                Write(catchBlock.Filter, false, true);
            }
            Indent();
            WriteEOL();
            Write(catchBlock.Body);
        }

        protected override void WriteTry(TryExpression expr) {
            Write("Try");
            Indent();
            WriteEOL();
            Write(expr.Body,false, false);
            WriteEOL(true);
            expr.Handlers.ForEach(catchBlock => {
                Write(catchBlock);
                WriteEOL(true);
            });
            if (expr.Fault != null) {
                Write("Fault");
                Indent();
                WriteEOL();
                Write(expr.Fault,false, false);
                WriteEOL(true);
            }
            if (expr.Finally != null) {
                Write("Finally");
                Indent();
                WriteEOL();
                Write(expr.Finally, false, false);
                WriteEOL(true);
            }
            Write("End Try");
        }

        protected override void WriteLabel(LabelExpression expr) {
            Write(expr.Target);
            Write(":");
        }

        protected override void WriteGoto(GotoExpression expr) {
            string gotoKeyword = "";
            switch (expr.Kind) {
                case Break:
                    gotoKeyword = "Exit";
                    break;
                case Continue:
                    gotoKeyword = "Continue";
                    break;
                case GotoExpressionKind.Goto:
                    gotoKeyword = "Goto";
                    break;
                case Return:
                    gotoKeyword = "Return";
                    break;
                default:
                    throw new NotImplementedException();
            }
            Write(gotoKeyword);
            if (!(expr.Target?.Name).IsNullOrWhitespace()) {
                Write(" ");
                Write(expr.Target.Name);
            }
            if (expr.Value != null) {
                Write(" ");
                Write(expr.Value);
            }
        }

        protected override void WriteLabelTarget(LabelTarget labelTarget) => Write(labelTarget.Name);

        protected override void WriteLoop(LoopExpression expr) {
            Write("Do");
            Indent();
            WriteEOL();
            Write(expr.Body);
            WriteEOL(true);
            Write("Loop");
        }

        protected override void WriteRuntimeVariables(RuntimeVariablesExpression expr) {
            Write("' Variables -- ");
            expr.Variables.ForEach((x, index) => {
                if (index > 0) { Write(", "); }
                Write(x, true);
            });
        }
    }
}
