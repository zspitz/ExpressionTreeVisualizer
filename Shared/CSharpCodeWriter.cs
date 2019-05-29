using ExpressionToString.Util;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using static ExpressionToString.Globals;
using static ExpressionToString.Util.Functions;
using static ExpressionToString.Util.Methods;
using static System.Linq.Enumerable;
using static System.Linq.Expressions.ExpressionType;
using static System.Linq.Expressions.GotoExpressionKind;

namespace ExpressionToString {
    public class CSharpCodeWriter : WriterBase {
        public CSharpCodeWriter(object o) : base(o, FormatterNames.CSharp) { }
        public CSharpCodeWriter(object o, out Dictionary<string, (int start, int length)> pathSpans) : base(o, FormatterNames.CSharp, out pathSpans) { }

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

        private void WriteIndexerAccess(string instancePath, Expression instance, string argBasePath, params Expression[] keys) {
            WriteNode(instancePath, instance);
            Write("[");
            WriteNodes(argBasePath, keys);
            Write("]");
        }
        private void WriteIndexerAccess(string instancePath, Expression instance, string argBasePath, IEnumerable<Expression> keys) =>
            WriteIndexerAccess(instancePath, instance, argBasePath, keys.ToArray());

        private void WriteBinary(ExpressionType nodeType, string leftPath, Expression left, string rightPath, Expression right) {
            if (simpleBinaryOperators.TryGetValue(nodeType, out var @operator)) {
                WriteNode(leftPath, left);
                Write($" {@operator} ");
                WriteNode(rightPath, right);
                return;
            }

            switch (nodeType) {
                case ArrayIndex:
                    WriteNode(leftPath, left);
                    Write("[");
                    WriteNode(rightPath, right);
                    Write("]");
                    return;
                case Power:
                    Write("Math.Pow(");
                    WriteNode(leftPath, left);
                    Write(", ");
                    WriteNode(rightPath, right);
                    Write(")");
                    return;
                case PowerAssign:
                    WriteNode($"{leftPath}_0", left);
                    Write(" = ");
                    Write("Math.Pow(");
                    WriteNode(leftPath, left);
                    Write(", ");
                    WriteNode(rightPath, right);
                    Write(")");
                    return;
            }

            throw new NotImplementedException();
        }

        protected override void WriteBinary(BinaryExpression expr) => WriteBinary(expr.NodeType, "Left", expr.Left, "Right", expr.Right);

        private void WriteUnary(ExpressionType nodeType, string operandPath, Expression operand, Type type, string expressionTypename) {
            switch (nodeType) {
                case ArrayLength:
                    WriteNode(operandPath, operand);
                    Write(".Length");
                    break;
                case ExpressionType.Convert:
                case ConvertChecked:
                case Unbox:
                    Write($"({type.FriendlyName(language)})");
                    WriteNode(operandPath, operand);
                    break;
                case Negate:
                case NegateChecked:
                    Write("-");
                    WriteNode(operandPath, operand);
                    break;
                case Not:
                    if (type == typeof(bool)) {
                        Write("!");
                    } else {
                        Write("~");
                    }
                    WriteNode(operandPath, operand);
                    break;
                case OnesComplement:
                    Write("~");
                    WriteNode(operandPath, operand);
                    break;
                case TypeAs:
                    WriteNode(operandPath, operand);
                    Write($" as {type.FriendlyName(language)}");
                    break;
                case PreIncrementAssign:
                    Write("++");
                    WriteNode(operandPath, operand);
                    break;
                case PostIncrementAssign:
                    WriteNode(operandPath, operand);
                    Write("++");
                    break;
                case PreDecrementAssign:
                    Write("--");
                    WriteNode(operandPath, operand);
                    break;
                case PostDecrementAssign:
                    WriteNode(operandPath, operand);
                    Write("--");
                    break;
                case IsTrue:
                    WriteNode(operandPath, operand);
                    break;
                case IsFalse:
                    Write("!");
                    WriteNode(operandPath, operand);
                    break;
                case Increment:
                    WriteNode(operandPath, operand);
                    Write(" += 1");
                    break;
                case Decrement:
                    WriteNode(operandPath, operand);
                    Write(" -= 1");
                    break;
                case Throw:
                    Write("throw");
                    if (operand != null) {
                        Write(" ");
                        WriteNode(operandPath, operand);
                    }
                    break;
                case Quote:
                    TrimEnd(true);
                    WriteEOL();
                    Write("// --- Quoted - begin");
                    Indent();
                    WriteEOL();
                    WriteNode(operandPath, operand);
                    WriteEOL(true);
                    Write("// --- Quoted - end");
                    break;
                case UnaryPlus:
                    Write("+");
                    WriteNode(operandPath, operand);
                    break;
                default:
                    throw new NotImplementedException($"NodeType: {nodeType}, Expression object type: {expressionTypename}");
            }
        }

        protected override void WriteUnary(UnaryExpression expr) =>
            WriteUnary(expr.NodeType, "Operand", expr.Operand, expr.Type, expr.GetType().Name);

        protected override void WriteLambda(LambdaExpression expr) {
            Write("(");
            WriteNodes("Parameters", expr.Parameters, false, ", ", true);
            Write(") => ");
            WriteNode("Body", expr.Body);
        }

        protected override void WriteParameterDeclarationImpl(ParameterExpression prm) {
            if (prm.IsByRef) { Write("ref "); }
            Write($"{prm.Type.FriendlyName(language)} {prm.Name}");
        }

        protected override void WriteParameter(ParameterExpression expr) => Write(expr.Name);

        protected override void WriteConstant(ConstantExpression expr) =>
            Write(RenderLiteral(expr.Value, language));

        protected override void WriteMemberAccess(MemberExpression expr) {
            switch (expr.Expression) {
                case ConstantExpression cexpr when cexpr.Type.IsClosureClass():
                case MemberExpression mexpr when mexpr.Type.IsClosureClass():
                    // closed over variable from outer scope
                    Write(expr.Member.Name.Replace("$VB$Local_", ""));
                    return;
                case null:
                    // static member
                    Write($"{expr.Member.DeclaringType.FriendlyName(language)}.{expr.Member.Name}");
                    return;
                default:
                    WriteNode("Expression", expr.Expression);
                    Write($".{expr.Member.Name}");
                    return;
            }
        }

        private void WriteNew(Type type, string argsPath, IList<Expression> args) {
            Write("new ");
            Write(type.FriendlyName(language));
            Write("(");
            WriteNodes(argsPath, args);
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
                    // otheriwse just write `member`
                    if (!(arg is MemberExpression mexpr && mexpr.Member.Name.Replace("$VB$Local_", "") == name)) {
                        Write($"{name} = ");
                    }
                    WriteNode($"Arguments[{index}]", arg);
                });
                WriteEOL(true);
                Write("}");
                return;
            }
            WriteNew(expr.Type, "Arguments", expr.Arguments);
        }

        protected override void WriteCall(MethodCallExpression expr) {
            if (expr.Method.In(stringConcats)) {
                var firstArg = expr.Arguments[0];
                IEnumerable<Expression> argsToWrite = null;
                string argsPath = "";
                if (firstArg is NewArrayExpression newArray && firstArg.NodeType == NewArrayInit) {
                    argsToWrite = newArray.Expressions;
                    argsPath = "Arguments[0].Expressions";
                } else if (expr.Arguments.All(x => x.Type == typeof(string))) {
                    argsToWrite = expr.Arguments;
                    argsPath = "Arguments";
                }
                if (argsToWrite != null) {
                    WriteNodes(argsPath, argsToWrite, " + ");
                    return;
                }
            }

            bool isIndexer = false;
            if ((expr.Object?.Type.IsArray ?? false) && expr.Method.Name == "Get") {
                isIndexer = true;
            } else {
                isIndexer = expr.Method.IsIndexerMethod();
            }
            if (isIndexer) {
                WriteIndexerAccess("Object", expr.Object, "Arguments", expr.Arguments);
                return;
            }

            if (expr.Method.In(stringFormats) && expr.Arguments[0] is ConstantExpression cexpr && cexpr.Value is string format) {
                var parts = ParseFormatString(format);
                Write("$\"");
                foreach (var (literal, index, alignment, itemFormat) in parts) {
                    Write(literal.Replace("{", "{{").Replace("}", "}}"));
                    if (index == null) { break; }
                    Write("{");
                    WriteNode($"Arguments[{index.Value + 1}]", expr.Arguments[index.Value + 1]);
                    if (alignment != null) { Write($", {alignment}"); }
                    if (itemFormat != null) { Write($":{itemFormat}"); }
                    Write("}");
                }
                Write("\"");
                return;
            }

            var instance = (path: "Object", o: expr.Object);
            var arguments = expr.Arguments.Select((x, index) => ($"Arguments[{index}]", x));

            if (expr.Object == null && expr.Method.HasAttribute<ExtensionAttribute>()) {
                instance = (path: "Arguments[0]", expr.Arguments[0]);
                arguments = expr.Arguments.Skip(1).Select((x, index) => ($"Arguments[{index + 1}]", x));
            }

            if (instance.o == null) {
                // static non-extension method -- write the type name
                Write(expr.Method.ReflectedType.FriendlyName(language));
            } else {
                // instance method, or extension method
                WriteNode(instance);
            }
            Write($".{expr.Method.Name}(");
            WriteNodes(arguments);
            Write(")");
        }

        protected override void WriteBinding(MemberBinding binding) {
            Write(binding.Member.Name);
            Write(" = ");
            if (binding is MemberAssignment assignmentBinding) {
                WriteNode("Expression", assignmentBinding.Expression);
                return;
            }

            Write("{");

            IEnumerable<object> items = null;
            string itemsPath = "";
            switch (binding) {
                case MemberListBinding listBinding when listBinding.Initializers.Count > 0:
                    items = listBinding.Initializers.Cast<object>();
                    itemsPath = "Initializers";
                    break;
                case MemberMemberBinding memberBinding when memberBinding.Bindings.Count > 0:
                    items = memberBinding.Bindings.Cast<object>();
                    itemsPath = "Bindings";
                    break;
            }
            if (items != null) {
                Indent();
                WriteEOL();
                WriteNodes(itemsPath, items, true);
                WriteEOL(true);
            }

            Write("}");
        }

        protected override void WriteMemberInit(MemberInitExpression expr) {
            WriteNode("NewExpression", expr.NewExpression);
            if (expr.Bindings.Count > 0) {
                Write(" {");
                Indent();
                WriteEOL();
                WriteNodes("Bindings", expr.Bindings, true);
                WriteEOL(true);
                Write("}");
            }
        }

        protected override void WriteListInit(ListInitExpression expr) {
            WriteNode("NewExpression", expr.NewExpression);
            Write(" {");
            Indent();
            WriteEOL();
            WriteNodes("Initializers", expr.Initializers, true);
            WriteEOL(true);
            Write("}");
        }

        protected override void WriteElementInit(ElementInit elementInit) {
            var args = elementInit.Arguments;
            switch (args.Count) {
                case 0:
                    throw new NotImplementedException();
                case 1:
                    WriteNode("Arguments[0]", args[0]);
                    break;
                default:
                    Write("{");
                    Indent();
                    WriteEOL();
                    WriteNodes("Arguments", args, true);
                    WriteEOL(true);
                    Write("}");
                    break;
            }
        }

        protected override void WriteNewArray(NewArrayExpression expr) {
            switch (expr.NodeType) {
                case NewArrayInit:
                    var elementType = expr.Type.GetElementType();
                    Write("new");
                    if (elementType.IsArray || expr.Expressions.None() || expr.Expressions.Any(x => x.Type != elementType)) {
                        Write(" ");
                        Write(expr.Type.FriendlyName(language));
                    } else {
                        Write("[]");
                    }
                    Write(" { ");
                    WriteNodes("Expressions", expr.Expressions);
                    Write(" }");
                    break;
                case NewArrayBounds:
                    (string left, string right) specifierChars = ("[", "]");
                    var nestedArrayTypes = expr.Type.NestedArrayTypes().ToList();
                    Write($"new {nestedArrayTypes.Last().root.FriendlyName(language)}");
                    nestedArrayTypes.ForEachT((current, _, index) => {
                        Write(specifierChars.left);
                        if (index == 0) {
                            WriteNodes("Expressions", expr.Expressions);
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
                WriteNode("Test", expr.Test, false, true);
                Write(") ");
                WriteNode("IfTrue", expr.IfTrue, false, true);
                WriteSemicolon(expr.IfTrue);
                if (!expr.IfFalse.IsEmpty()) {
                    Write(" else ");
                    WriteNode("IfFalse", expr.IfFalse, false, true);
                    WriteSemicolon(expr.IfFalse);
                }
            } else {
                WriteNode("Test", expr.Test, false, true);
                Write(" ? ");
                WriteNode("IfTrue", expr.IfTrue);
                Write(" : ");
                WriteNode("IfFalse", expr.IfFalse);
            }
        }

        protected override void WriteDefault(DefaultExpression expr) =>
            Write($"default({expr.Type.FriendlyName(language)})");

        protected override void WriteTypeBinary(TypeBinaryExpression expr) {
            WriteNode("Expression", expr.Expression);
            var typeName = expr.TypeOperand.FriendlyName(language);
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
            WriteNode("Expression", expr.Expression);
            if (expr.Expression is LambdaExpression) { Write(")"); }
            Write("(");
            WriteNodes("Arguments", expr.Arguments);
            Write(")");
        }

        protected override void WriteIndex(IndexExpression expr) =>
            WriteIndexerAccess("Object", expr.Object, "Arguments", expr.Arguments);

        protected override void WriteBlock(BlockExpression expr, bool? explicitBlock) {
            var useExplicitBlock = explicitBlock ?? expr.Variables.Count > 0;
            if (useExplicitBlock) {
                Write("{");
                Indent();
                WriteEOL();
                expr.Variables.ForEach((v, index) => {
                    if (index > 0) { WriteEOL(); }
                    WriteNode($"Variables[{index}]", v, true);
                    Write(";");
                });
            }
            expr.Expressions.ForEach((subexpr, index) => {
                if (index > 0 || expr.Variables.Count > 0) { WriteEOL(); }
                if (subexpr is LabelExpression) { TrimEnd(); }
                WriteNode($"Expressions[{index}]", subexpr);
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
                WriteNode($"TestValues[{index}]", testValue);
                Write(":");
            });
            Indent();
            WriteEOL();
            WriteNode("Body", switchCase.Body, false, false);
            WriteSemicolon(switchCase.Body);
            WriteEOL();
            Write("break;");
        }

        protected override void WriteSwitch(SwitchExpression expr) {
            Write("switch (");
            WriteNode("SwitchValue", expr.SwitchValue, false, true);
            Write(") {");
            Indent();
            WriteEOL();
            expr.Cases.ForEach((switchCase, index) => {
                if (index > 0) { WriteEOL(); }
                WriteNode($"Cases[{index}]", switchCase);
                Dedent();
            });
            if (expr.DefaultBody != null) {
                WriteEOL();
                Write("default:");
                Indent();
                WriteEOL();
                WriteNode("DefaultBody", expr.DefaultBody);
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
                    WriteNode("Variable", catchBlock.Variable, true);
                } else {
                    Write(catchBlock.Test.FriendlyName(language));
                }
                Write(") ");
                if (catchBlock.Filter != null) {
                    Write("when (");
                    WriteNode("Filter", catchBlock.Filter, false, true);
                    Write(") ");
                }
            }
            Write("{");
            Indent();
            WriteEOL();
            WriteNode("Body", catchBlock.Body);
            WriteSemicolon(catchBlock.Body);
            WriteEOL(true);
            Write("}");
        }

        protected override void WriteTry(TryExpression expr) {
            Write("try {");
            Indent();
            WriteEOL();
            WriteNode("Body", expr.Body);
            WriteSemicolon(expr.Body);
            WriteEOL(true);
            Write("}");
            expr.Handlers.ForEach((catchBlock, index) => {
                Write(" ");
                WriteNode($"Handlers[{index}]", catchBlock);
            });
            if (expr.Fault != null) {
                Write(" fault {");
                Indent();
                WriteEOL();
                WriteNode("Fault", expr.Fault);
                WriteSemicolon(expr.Fault);
                WriteEOL(true);
                Write("}");
            }
            if (expr.Finally != null) {
                Write(" finally {");
                Indent();
                WriteEOL();
                WriteNode("Finally", expr.Finally);
                WriteSemicolon(expr.Finally);
                WriteEOL(true);
                Write("}");
            }
        }

        protected override void WriteLabel(LabelExpression expr) {
            WriteNode("Target", expr.Target);
            Write(":");
        }

        protected override void WriteGoto(GotoExpression expr) {
            string gotoKeyword;
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
                WriteNode("Target", expr.Target);
            }
            if (expr.Value != null) {
                Write(" ");
                WriteNode("Value", expr.Value);
            }
        }

        protected override void WriteLabelTarget(LabelTarget labelTarget) => Write(labelTarget.Name);

        protected override void WriteLoop(LoopExpression expr) {
            Write("while (true) {");
            Indent();
            WriteEOL();
            WriteNode("Body", expr.Body);
            WriteSemicolon(expr.Body);
            WriteEOL(true);
            Write("}");
        }

        protected override void WriteRuntimeVariables(RuntimeVariablesExpression expr) {
            Write("// variables -- ");
            expr.Variables.ForEach((x, index) => {
                if (index > 0) { Write(", "); }
                WriteNode($"Variables[{index}]", x, true);
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
            WriteBinary(binder.Operation, "Arguments[0]", args[0], "Arguments[1]", args[1]);
        }

        protected override void WriteConvertBinder(ConvertBinder binder, IList<Expression> args) {
            VerifyCount(args, 1);
            WriteUnary(ExpressionType.Convert, "Arguments[0]", args[0], binder.Type, typeof(ConvertBinder).Name);
        }

        protected override void WriteCreateInstanceBinder(CreateInstanceBinder binder, IList<Expression> args) =>
            WriteNew(binder.ReturnType, "Arguments", args);

        protected override void WriteDeleteIndexBinder(DeleteIndexBinder binder, IList<Expression> args) =>
            throw new NotImplementedException();
        protected override void WriteDeleteMemberBinder(DeleteMemberBinder binder, IList<Expression> args) =>
            throw new NotImplementedException();

        protected override void WriteGetIndexBinder(GetIndexBinder binder, IList<Expression> args) {
            VerifyCount(args, 2, null);
            WriteNode("Arguments[0]", args[0]);
            Write("[");
            WriteNodes(args.Skip(1).Select((arg, index) => ($"Arguments[{index + 1}]", arg)));
            Write("]");
        }

        protected override void WriteGetMemberBinder(GetMemberBinder binder, IList<Expression> args) {
            VerifyCount(args, 1);
            WriteNode("Arguments[0]", args[0]);
            Write($".{binder.Name}");
        }

        protected override void WriteInvokeBinder(InvokeBinder binder, IList<Expression> args) {
            VerifyCount(args, 1, null);
            WriteNode("Arguments[0]", args[0]);
            Write("(");
            WriteNodes(args.Skip(1).Select((arg, index) => ($"Arguments[{index + 1}]", arg)));
            Write(")");
        }

        protected override void WriteInvokeMemberBinder(InvokeMemberBinder binder, IList<Expression> args) {
            VerifyCount(args, 1, null);
            WriteNode("Arguments[0]", args[0]);
            Write($".{binder.Name}(");
            WriteNodes(args.Skip(1).Select((arg, index) => ($"Arguments[{index + 1}]", arg)));
            Write(")");
        }

        protected override void WriteSetIndexBinder(SetIndexBinder binder, IList<Expression> args) {
            VerifyCount(args, 3, null);
            WriteNode("Arguments[0]", args[0]);
            Write("[");
            WriteNodes(args.Skip(2).Select((arg, index) => ($"Arguments[{index + 2}]", arg)));
            Write("] = ");
            WriteNode("Arguments[1]", args[1]);
        }

        protected override void WriteSetMemberBinder(SetMemberBinder binder, IList<Expression> args) {
            VerifyCount(args, 2);
            WriteNode("Arguments[0]", args[0]);
            Write($".{binder.Name} = ");
            WriteNode("Arguments[1]", args[1]);
        }

        protected override void WriteUnaryOperationBinder(UnaryOperationBinder binder, IList<Expression> args) {
            VerifyCount(args, 1);
            WriteUnary(binder.Operation, "Arguments[0]", args[0], binder.ReturnType, binder.GetType().Name);
        }
    }
}