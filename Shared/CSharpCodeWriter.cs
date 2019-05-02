using ExpressionToString.Util;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using static ExpressionToString.FormatterNames;
using static ExpressionToString.Globals;
using static ExpressionToString.Util.Functions;
using static ExpressionToString.Util.Methods;
using static System.Linq.Enumerable;
using static System.Linq.Expressions.ExpressionType;
using static System.Linq.Expressions.GotoExpressionKind;

namespace ExpressionToString {
    public class CSharpCodeWriter : CodeWriter {
        public CSharpCodeWriter(object o) : base(o) { }
        public CSharpCodeWriter(object o, out Dictionary<object, List<(int start, int length)>> visitedObjects) : base(o, out visitedObjects) { }

        // TODO handle order of operations -- https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/

        private static readonly Dictionary<ExpressionType, string> simpleBinaryOperators = new Dictionary<ExpressionType, string>() {
            [Add] = "+",
            [AddChecked] = "+",
            [Divide] = "/",
            [Modulo] = "%",
            [Multiply] = "*",
            [MultiplyChecked] = "*",
            [Subtract] = "-",
            [SubtractChecked] = "-",
            [And] = "&",
            [Or] = "|",
            [ExclusiveOr] = "^",
            [AndAlso] = "&&",
            [OrElse] = "||",
            [Equal] = "==",
            [NotEqual] = "!=",
            [GreaterThanOrEqual] = ">=",
            [GreaterThan] = ">",
            [LessThan] = "<",
            [LessThanOrEqual] = "<=",
            [Coalesce] = "??",
            [LeftShift] = "<<",
            [RightShift] = ">>",
            [Assign] = "=",
            [AddAssign] = "+=",
            [AddAssignChecked] = "+=",
            [AndAssign] = "&=",
            [DivideAssign] = "/=",
            [ExclusiveOrAssign] = "^=",
            [LeftShiftAssign] = "<<=",
            [ModuloAssign] = "%=",
            [MultiplyAssign] = "*=",
            [MultiplyAssignChecked] = "*=",
            [OrAssign] = "|=",
            [RightShiftAssign] = ">>=",
            [SubtractAssign] = "-=",
            [SubtractAssignChecked] = "-="
        };

        private void WriteIndexerAccess(Expression instance, params Expression[] keys) {
            Write(instance);
            Write("[");
            WriteList(keys);
            Write("]");
        }
        private void WriteIndexerAccess(Expression instance, IEnumerable<Expression> keys) => 
            WriteIndexerAccess(instance, keys.ToArray());

        private void WriteBinary(ExpressionType nodeType, Expression left, Expression right) {
            if (simpleBinaryOperators.TryGetValue(nodeType, out var @operator)) {
                Write(left);
                Write($" {@operator} ");
                Write(right);
                return;
            }

            switch (nodeType) {
                case ArrayIndex:
                    WriteIndexerAccess(left, right);
                    return;
                case Power:
                    Write("Math.Pow(");
                    Write(left);
                    Write(", ");
                    Write(right);
                    Write(")");
                    return;
                case PowerAssign:
                    Write(left);
                    Write(" = ");
                    Write("Math.Pow(");
                    Write(left);
                    Write(", ");
                    Write(right);
                    Write(")");
                    return;
            }

            throw new NotImplementedException();
        }

        protected override void WriteBinary(BinaryExpression expr) => WriteBinary(expr.NodeType, expr.Left, expr.Right);

        private void WriteUnary(ExpressionType nodeType, Expression operand, Type type, string expressionTypename) {
            switch (nodeType) {
                case ArrayLength:
                    Write(operand);
                    Write(".Length");
                    break;
                case ExpressionType.Convert:
                case ConvertChecked:
                case Unbox:
                    Write($"({type.FriendlyName(CSharp)})");
                    Write(operand);
                    break;
                case Negate:
                case NegateChecked:
                    Write("-");
                    Write(operand);
                    break;
                case Not:
                    if (type == typeof(bool)) {
                        Write("!");
                    } else {
                        Write("~");
                    }
                    Write(operand);
                    break;
                case OnesComplement:
                    Write("~");
                    Write(operand);
                    break;
                case TypeAs:
                    Write(operand);
                    Write($" as {type.FriendlyName(CSharp)}");
                    break;
                case PreIncrementAssign:
                    Write("++");
                    Write(operand);
                    break;
                case PostIncrementAssign:
                    Write(operand);
                    Write("++");
                    break;
                case PreDecrementAssign:
                    Write("--");
                    Write(operand);
                    break;
                case PostDecrementAssign:
                    Write(operand);
                    Write("--");
                    break;
                case IsTrue:
                    Write(operand);
                    break;
                case IsFalse:
                    Write("!");
                    Write(operand);
                    break;
                case Increment:
                    Write(operand);
                    Write(" += 1");
                    break;
                case Decrement:
                    Write(operand);
                    Write(" -= 1");
                    break;
                case Throw:
                    Write("throw");
                    if (operand != null) {
                        Write(" ");
                        Write(operand);
                    }
                    break;
                case Quote:
                    TrimEnd(true);
                    WriteEOL();
                    Write("// --- Quoted - begin");
                    Indent();
                    WriteEOL();
                    Write(operand);
                    WriteEOL(true);
                    Write("// --- Quoted - end");
                    break;
                case UnaryPlus:
                    Write("+");
                    Write(operand);
                    break;
                default:
                    throw new NotImplementedException($"NodeType: {nodeType}, Expression object type: {expressionTypename}");
            }
        }

        protected override void WriteUnary(UnaryExpression expr) => 
            WriteUnary(expr.NodeType, expr.Operand, expr.Type, expr.GetType().Name);

        protected override void WriteLambda(LambdaExpression expr) {
            Write("(");
            // we can't use WriteList here, because we have to call WriteParameterDeclaration
            expr.Parameters.ForEach((prm, index) => {
                if (index > 0) { Write(", "); }
                Write(prm, true);
            });
            Write(") => ");
            Write(expr.Body);
        }

        protected override void WriteParameterDeclarationImpl(ParameterExpression prm) {
            if (prm.IsByRef) { Write("ref "); }
            Write($"{prm.Type.FriendlyName(CSharp)} {prm.Name}");
        }

        protected override void WriteParameter(ParameterExpression expr) => Write(expr.Name);

        protected override void WriteConstant(ConstantExpression expr) =>
            Write(RenderLiteral(expr.Value, CSharp));

        protected override void WriteMemberAccess(MemberExpression expr) {
            switch (expr.Expression) {
                case ConstantExpression cexpr when cexpr.Type.IsClosureClass():
                case MemberExpression mexpr when mexpr.Type.IsClosureClass():
                    // closed over variable from outer scope
                    Write(expr.Member.Name.Replace("$VB$Local_", ""));
                    return;
                case null:
                    // static member
                    Write($"{expr.Member.DeclaringType.FriendlyName(CSharp)}.{expr.Member.Name}");
                    return;
                default:
                    Write(expr.Expression);
                    Write($".{expr.Member.Name}");
                    return;
            }
        }

        private void WriteNew(Type type, IList<Expression> args) {
            Write("new ");
            Write(type.FriendlyName(CSharp));
            Write("(");
            WriteList(args);
            Write(")");
        }

        protected override void WriteNew(NewExpression expr) {
            if (expr.Type.IsAnonymous()) {
                Write("new {");
                Indent();
                WriteEOL();
                expr.Constructor.GetParameters().Select(x => x.Name).Zip(expr.Arguments).ForEachT((name, arg, index) => {
                    if (index > 0) {
                        Write(",");
                        WriteEOL();
                    }
                    // write as `property = member` only if the source name is different from the target name
                    if (!(arg is MemberExpression mexpr && mexpr.Member.Name.Replace("$VB$Local_", "") == name)) {
                        Write($"{name} = ");
                    }
                    Write(arg);
                });
                WriteEOL(true);
                Write("}");
                return;
            }
            WriteNew(expr.Type, expr.Arguments);
        }

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
                WriteIndexerAccess(expr.Object, expr.Arguments);
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
                Write(expr.Method.ReflectedType.FriendlyName(CSharp));
            } else {
                Write(instance);
            }

            Write($".{expr.Method.Name}(");
            WriteList(arguments);
            Write(")");
        }

        protected override void WriteBinding(MemberBinding binding) {
            Write(binding.Member.Name);
            Write(" = ");
            if (binding is MemberAssignment assignmentBinding) {
                Write(assignmentBinding.Expression);
                return;
            }

            Write("{");

            IEnumerable<object> items = null;
            switch (binding) {
                case MemberListBinding listBinding when listBinding.Initializers.Count > 0:
                    items = listBinding.Initializers.Cast<object>();
                    break;
                case MemberMemberBinding memberBinding when memberBinding.Bindings.Count > 0:
                    items = memberBinding.Bindings.Cast<object>();
                    break;
            }
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
                Write(" {");
                Indent();
                WriteEOL();
                WriteList(expr.Bindings, true);
                WriteEOL(true);
                Write("}");
            }
        }

        protected override void WriteListInit(ListInitExpression expr) {
            Write(expr.NewExpression);
            Write(" {");
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
                    Write("new ");
                    if (elementType.IsArray || expr.Expressions.None() || expr.Expressions.Any(x => x.Type != elementType)) {
                        Write(expr.Type.FriendlyName(CSharp));
                    } else {
                        Write("[]");
                    }
                    Write(" { ");
                    WriteList(expr.Expressions);
                    Write(" }");
                    break;
                case NewArrayBounds:
                    (string left, string right) specifierChars = ("[", "]");
                    var nestedArrayTypes = expr.Type.NestedArrayTypes().ToList();
                    Write($"new {nestedArrayTypes.Last().root.FriendlyName(CSharp)}");
                    nestedArrayTypes.ForEachT((current, _, index) => {
                        Write(specifierChars.left);
                        if (index == 0) {
                            WriteList(expr.Expressions);
                        } else {
                            Write(Repeat("", current.GetArrayRank()).Joined());
                        }
                        Write(specifierChars.right);
                    });
                    break;
                default:
                    throw new NotImplementedException();
            }
        }

        protected override void WriteConditional(ConditionalExpression expr) {
            if (expr.Type == typeof(void)) { // if block, or if..else block
                Write("if (");
                Write(expr.Test, false, true);
                Write(") ");
                Write(expr.IfTrue, false, true);
                WriteSemicolon(expr.IfTrue);
                if (!expr.IfFalse.IsEmpty()) {
                    Write(" else ");
                    Write(expr.IfFalse, false, true);
                    WriteSemicolon(expr.IfFalse);
                }
            } else {
                Write(expr.Test, false, true);
                Write(" ? ");
                Write(expr.IfTrue);
                Write(" : ");
                Write(expr.IfFalse);
            }
        }

        protected override void WriteDefault(DefaultExpression expr) =>
            Write($"default({expr.Type.FriendlyName(CSharp)})");

        protected override void WriteTypeBinary(TypeBinaryExpression expr) {
            Write(expr.Expression);
            var typeName = expr.TypeOperand.FriendlyName(CSharp);
            switch (expr.NodeType) {
                case TypeIs:
                    Write($" is {typeName}");
                    break;
                case TypeEqual:
                    Write($".GetType() == typeof({typeName})");
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

        protected override void WriteIndex(IndexExpression expr) => WriteIndexerAccess(expr.Object, expr.Arguments);

        protected override void WriteBlock(BlockExpression expr, bool? explicitBlock) {
            var useExplicitBlock = explicitBlock ?? expr.Variables.Count > 0;
            if (useExplicitBlock) {
                Write("{");
                Indent();
                WriteEOL();
                expr.Variables.ForEach((v, index) => {
                    if (index > 0) { WriteEOL(); }
                    Write(v, true);
                    Write(";");
                });
            }
            expr.Expressions.ForEach((subexpr, index) => {
                if (index > 0 || expr.Variables.Count > 0) { WriteEOL(); }
                if (subexpr is LabelExpression) { TrimEnd(); }
                Write(subexpr);
                WriteSemicolon(subexpr);
            });
            if (useExplicitBlock) {
                WriteEOL(true);
                Write("}");
            }
        }

        private void WriteSemicolon(Expression expr) {
            switch (expr) {
                case ConditionalExpression cexpr when cexpr.Type == typeof(void):
                case BlockExpression _:
                case SwitchExpression _:
                case LabelExpression _:
                case TryExpression _:
                case RuntimeVariablesExpression _:
                case UnaryExpression bexpr when bexpr.NodeType == Quote:
                    return;
            }
            Write(";");
        }

        protected override void WriteSwitchCase(SwitchCase switchCase) {
            switchCase.TestValues.ForEach((testValue, index) => {
                if (index > 0) { WriteEOL(); }
                Write("case ");
                Write(testValue);
                Write(":");
            });
            Indent();
            WriteEOL();
            Write(switchCase.Body, false, false);
            WriteSemicolon(switchCase.Body);
            WriteEOL();
            Write("break;");
        }

        protected override void WriteSwitch(SwitchExpression expr) {
            Write("switch (");
            Write(expr.SwitchValue, false, true);
            Write(") {");
            Indent();
            WriteEOL();
            expr.Cases.ForEach((switchCase, index) => {
                if (index > 0) { WriteEOL(); }
                Write(switchCase);
                Dedent();
            });
            if (expr.DefaultBody != null) {
                WriteEOL();
                Write("default:");
                Indent();
                WriteEOL();
                Write(expr.DefaultBody);
                WriteSemicolon(expr.DefaultBody);
                Dedent();
            }
            WriteEOL(true);
            Write("}");
        }

        protected override void WriteCatchBlock(CatchBlock catchBlock) {
            Write("catch ");
            if (catchBlock.Variable != null || catchBlock.Test != typeof(Exception)) {
                Write("(");
                if (catchBlock.Variable != null) {
                    Write(catchBlock.Variable, true);
                } else {
                    Write(catchBlock.Test.FriendlyName(CSharp));
                }
                Write(") ");
                if (catchBlock.Filter != null) {
                    Write("when (");
                    Write(catchBlock.Filter, false, true);
                    Write(") ");
                }
            }
            Write("{");
            Indent();
            WriteEOL();
            Write(catchBlock.Body);
            WriteSemicolon(catchBlock.Body);
            WriteEOL(true);
            Write("}");
        }

        protected override void WriteTry(TryExpression expr) {
            Write("try {");
            Indent();
            WriteEOL();
            Write(expr.Body);
            WriteSemicolon(expr.Body);
            WriteEOL(true);
            Write("}");
            expr.Handlers.ForEach(catchBlock => {
                Write(" ");
                Write(catchBlock);
            });
            if (expr.Fault != null) {
                Write(" fault {");
                Indent();
                WriteEOL();
                Write(expr.Fault);
                WriteSemicolon(expr.Fault);
                WriteEOL(true);
                Write("}");
            }
            if (expr.Finally != null) {
                Write(" finally {");
                Indent();
                WriteEOL();
                Write(expr.Finally);
                WriteSemicolon(expr.Finally);
                WriteEOL(true);
                Write("}");
            }
        }

        protected override void WriteLabel(LabelExpression expr) {
            Write(expr.Target);
            Write(":");
        }

        protected override void WriteGoto(GotoExpression expr) {
            string gotoKeyword = "";
            switch (expr.Kind) {
                case Break:
                    gotoKeyword = "break";
                    break;
                case Continue:
                    gotoKeyword = "continue";
                    break;
                case GotoExpressionKind.Goto:
                    gotoKeyword = "goto";
                    break;
                case Return:
                    gotoKeyword = "return";
                    break;
                default:
                    throw new NotImplementedException();
            }
            Write(gotoKeyword);
            if (!(expr.Target?.Name).IsNullOrWhitespace()) {
                Write(" ");
                Write(expr.Target);
            }
            if (expr.Value != null) {
                Write(" ");
                Write(expr.Value);
            }
        }

        protected override void WriteLabelTarget(LabelTarget labelTarget) => Write(labelTarget.Name);

        protected override void WriteLoop(LoopExpression expr) {
            Write("while (true) {");
            Indent();
            WriteEOL();
            Write(expr.Body);
            WriteSemicolon(expr.Body);
            WriteEOL(true);
            Write("}");
        }

        protected override void WriteRuntimeVariables(RuntimeVariablesExpression expr) {
            Write("// variables -- ");
            expr.Variables.ForEach((x, index) => {
                if (index > 0) { Write(", "); }
                Write(x, true);
            });
        }

        protected override void WriteDebugInfo(DebugInfoExpression expr) {
            var filename = expr.Document.FileName;
            Write("// ");
            var comment =
                expr.IsClear ?
                $"Clear debug info from {filename}" :
                $"Debug to {filename} -- L{expr.StartLine}C{expr.StartColumn} : L{expr.EndLine}C{expr.EndColumn}";
            Write(comment);
        }

        protected override void WriteBinaryOperationBinder(BinaryOperationBinder binder, IList<Expression> args) {
            VerifyCount(args, 2);
            WriteBinary(binder.Operation, args[0], args[1]);
        }

        protected override void WriteConvertBinder(ConvertBinder binder, IList<Expression> args) {
            VerifyCount(args, 1);
            WriteUnary(ExpressionType.Convert, args[0], binder.Type, typeof(ConvertBinder).Name);
        }

        protected override void WriteCreateInstanceBinder(CreateInstanceBinder binder, IList<Expression> args) => 
            WriteNew(binder.ReturnType, args);

        protected override void WriteDeleteIndexBinder(DeleteIndexBinder binder, IList<Expression> args) => 
            throw new NotImplementedException();
        protected override void WriteDeleteMemberBinder(DeleteMemberBinder binder, IList<Expression> args) => 
            throw new NotImplementedException();

        protected override void WriteGetIndexBinder(GetIndexBinder binder, IList<Expression> args) {
            VerifyCount(args, 2, null);
            WriteIndexerAccess(args[0],args.Skip(1));
        }

        protected override void WriteGetMemberBinder(GetMemberBinder binder, IList<Expression> args) {
            VerifyCount(args, 1);
            Write(args[0]);
            Write($".{binder.Name}");
        }

        protected override void WriteInvokeBinder(InvokeBinder binder, IList<Expression> args) {
            VerifyCount(args, 1, null);
            Write(args[0]);
            Write("(");
            WriteList(args.Skip(1));
            Write(")");
        }

        protected override void WriteInvokeMemberBinder(InvokeMemberBinder binder, IList<Expression> args) {
            VerifyCount(args, 1, null);
            Write(args[0]);
            Write($".{binder.Name}(");
            WriteList(args.Skip(1));
            Write(")");
        }

        protected override void WriteSetIndexBinder(SetIndexBinder binder, IList<Expression> args) {
            VerifyCount(args, 3, null);
            WriteIndexerAccess(args[0], args.Skip(2));
            Write(" = ");
            Write(args[1]);
        }

        protected override void WriteSetMemberBinder(SetMemberBinder binder, IList<Expression> args) {
            VerifyCount(args, 2);
            Write(args[0]);
            Write($".{binder.Name} = ");
            Write(args[1]);
        }

        protected override void WriteUnaryOperationBinder(UnaryOperationBinder binder, IList<Expression> args) {
            VerifyCount(args, 1);
            WriteUnary(binder.Operation, args[0], binder.ReturnType, binder.GetType().Name);
        }
    }
}