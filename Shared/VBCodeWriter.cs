using ExpressionToString.Util;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using static ExpressionToString.Globals;
using static ExpressionToString.Util.Functions;
using static System.Linq.Enumerable;
using static System.Linq.Expressions.ExpressionType;
using static System.Linq.Expressions.GotoExpressionKind;
using static ExpressionToString.Util.Methods;
using static ExpressionToString.VBExpressionMetadata;

namespace ExpressionToString {
    public class VBCodeWriter : WriterBase {
        public VBCodeWriter(object o) : base(o, FormatterNames.VisualBasic) { }
        public VBCodeWriter(object o, out Dictionary<string, (int start, int length)> pathSpans) : base(o, FormatterNames.VisualBasic, out pathSpans) { }

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

        private void WriteIndexerAccess(string instancePath, Expression instance, string argBasePath, params Expression[] keys) {
            WriteNode(instancePath, instance);
            Write("(");
            WriteNodes(argBasePath, keys);
            Write(")");
        }
        private void WriteIndexerAccess(string instancePath, Expression instance, string argBasePath, IEnumerable<Expression> keys) =>
            WriteIndexerAccess(instancePath, instance, argBasePath, keys.ToArray());

        private void WriteBinary(ExpressionType nodeType, string leftPath, Expression left, string rightPath, Expression right, bool hasMethod) {
            var isReferenceComparison = IsReferenceComparison(nodeType, left, right, hasMethod);

            if (simpleBinaryOperators.TryGetValue(nodeType, out var @operator)) {
                WriteNode(leftPath, left);
                Write($" {@operator} ");
                WriteNode(rightPath, right);
                return;
            }

            switch (nodeType) {
                case ArrayIndex:
                    WriteNode(leftPath, left);
                    Write("(");
                    WriteNode(rightPath, right);
                    Write(")");
                    return;
                case Coalesce:
                    Write("If(");
                    WriteNode(leftPath, left);
                    Write(", ");
                    WriteNode(rightPath, right);
                    Write(")");
                    return;
                case OrAssign:
                case AndAssign:
                case ExclusiveOrAssign:
                case ModuloAssign:
                    // these don't have a dedicated assigment operator
                    var op = (ExpressionType)Enum.Parse(typeof(ExpressionType), nodeType.ToString().Replace("Assign", ""));
                    WriteNode($"{leftPath}_0", left);
                    Write(" = ");
                    WriteNode(leftPath, left);
                    Write($" {simpleBinaryOperators[op]} ");
                    WriteNode(rightPath, right);
                    return;
                case Equal:
                    WriteNode(leftPath, left);
                    Write(isReferenceComparison ?
                        " Is " :
                        " = "
                    );
                    WriteNode(rightPath, right);
                    return;
                case NotEqual:
                    WriteNode(leftPath, left);
                    Write(isReferenceComparison ?
                        " IsNot " :
                        " <> "
                    );
                    WriteNode(rightPath, right);
                    return;
            }

            throw new NotImplementedException();
        }

        protected override void WriteBinary(BinaryExpression expr) => WriteBinary(expr.NodeType, "Left", expr.Left, "Right", expr.Right, expr.Method != null);

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

        private void WriteUnary(ExpressionType nodeType, string operandPath, Expression operand, Type type, string expressionTypename) {
            switch (nodeType) {
                case ArrayLength:
                    WriteNode(operandPath, operand);
                    Write(".Length");
                    break;
                case ExpressionType.Convert:
                case ConvertChecked:
                case Unbox:
                    if (conversionFunctions.TryGetValue(type, out var conversionFunction)) {
                        Write(conversionFunction);
                        Write("(");
                        WriteNode(operandPath, operand);
                        Write(")");
                    } else {
                        Write("CType(");
                        WriteNode(operandPath, operand);
                        Write($", {type.FriendlyName(language)})");
                    }
                    break;
                case Negate:
                case NegateChecked:
                    Write("-");
                    WriteNode(operandPath, operand);
                    break;
                case Not:
                    Write("Not ");
                    WriteNode(operandPath, operand);
                    break;
                case TypeAs:
                    Write("TryCast(");
                    WriteNode(operandPath, operand);
                    Write($", {type.FriendlyName(language)})");
                    break;

                case PreIncrementAssign:
                    Write("(");
                    WriteNode($"{operandPath}_0", operand);
                    Write(" += 1 : ");
                    WriteNode(operandPath, operand);
                    Write(")");
                    return;
                case PostIncrementAssign:
                    Write("(");
                    WriteNode($"{operandPath}_0", operand);
                    Write(" += 1 : ");
                    WriteNode(operandPath, operand);
                    Write(" - 1)");
                    return;
                case PreDecrementAssign:
                    Write("(");
                    WriteNode($"{operandPath}_0", operand);
                    Write(" -= 1 : ");
                    WriteNode(operandPath, operand);
                    Write(")");
                    return;
                case PostDecrementAssign:
                    Write("(");
                    WriteNode($"{operandPath}_0", operand);
                    Write(" -= 1 : ");
                    WriteNode(operandPath, operand);
                    Write(" + 1)");
                    return;

                case IsTrue:
                    WriteNode(operandPath, operand);
                    break;
                case IsFalse:
                    Write("Not ");
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
                    Write("Throw");
                    if (operand != null) {
                        Write(" ");
                        WriteNode(operandPath, operand);
                    }
                    break;

                case Quote:
                    TrimEnd(true);
                    WriteEOL();
                    Write("' --- Quoted - begin");
                    Indent();
                    WriteEOL();
                    WriteNode(operandPath, operand);
                    WriteEOL(true);
                    Write("' --- Quoted - end");
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
            var lambdaKeyword = expr.ReturnType == typeof(void) ? "Sub" : "Function";
            Write($"{lambdaKeyword}(");
            expr.Parameters.ForEach((prm, index) => {
                if (index > 0) { Write(", "); }
                WriteNode($"Parameters[{index}]", prm, true);
            });
            Write(")");

            if (CanInline(expr.Body)) {
                Write(" ");
                WriteNode("Body", expr.Body);
                return;
            }

            Indent();
            WriteEOL();
            if (expr.Body.Type != typeof(void)) { Write("Return "); }
            WriteNode("Body", expr.Body, CreateMetadata(true, Lambda));
            WriteEOL(true);
            Write($"End {lambdaKeyword}");
        }

        protected override void WriteParameterDeclarationImpl(ParameterExpression prm) {
            if (prm.IsByRef) { Write("ByRef "); }
            Write($"{prm.Name} As {prm.Type.FriendlyName(language)}");
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
            Write("New ");
            Write(type.FriendlyName(language));
            if (args.Count > 0) {
                Write("(");
                WriteNodes(argsPath, args);
                Write(")");
            }
        }

        protected override void WriteNew(NewExpression expr) {
            if (expr.Type.IsAnonymous()) {
                Write("New With {");
                Indent();
                WriteEOL();
                expr.Constructor.GetParameters().Select(x => x.Name).Zip(expr.Arguments).ForEachT((name, arg, index) => {
                    if (index > 0) {
                        Write(",");
                        WriteEOL();
                    }
                    // write as `.property = member` only if the source name is different from the target name
                    // otheriwse just write `member`
                    if (!(arg is MemberExpression mexpr && mexpr.Member.Name.Replace("$VB$Local_", "") == name)) {
                        Write($".{name} = ");
                    }
                    WriteNode($"Arguments[{index}]", arg);
                });
                WriteEOL(true);
                Write("}");
                return;
            }
            WriteNew(expr.Type, "Arguments", expr.Arguments);
        }

        static readonly MethodInfo power = typeof(Math).GetMethod("Pow");

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
                var indexerMethods = expr.Method.ReflectedType.GetIndexers(true).SelectMany(x => new[] { x.GetMethod, x.SetMethod }).ToList();
                isIndexer = expr.Method.In(indexerMethods);
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

            if (expr.Method == power) {
                WriteNode("Arguments[0]", expr.Arguments[0]);
                Write(" ^ ");
                WriteNode("Arguments[1]", expr.Arguments[1]);
                return;
            }

            // Microsoft.VisualBasic.CompilerServices is not available to .NET Standard
            if (expr.Method.DeclaringType.FullName == "Microsoft.VisualBasic.CompilerServices.LikeOperator") {
                WriteNode("Arguments[0]", expr.Arguments[0]);
                Write(" Like ");
                WriteNode("Arguments[1]", expr.Arguments[1]);
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
            Write($".{expr.Method.Name}");
            if (arguments.Any()) {
                Write("(");
                WriteNodes(arguments);
                Write(")");
            }
        }

        protected override void WriteBinding(MemberBinding binding) {
            Write(".");
            Write(binding.Member.Name);
            Write(" = ");
            if (binding is MemberAssignment assignmentBinding) {
                WriteNode("Expression", assignmentBinding.Expression);
                return;
            }

            IEnumerable<object> items = null;
            string initializerKeyword = "";
            string itemsPath = "";
            switch (binding) {
                case MemberListBinding listBinding when listBinding.Initializers.Count > 0:
                    items = listBinding.Initializers.Cast<object>();
                    initializerKeyword = "From ";
                    itemsPath = "Initializers";
                    break;
                case MemberMemberBinding memberBinding when memberBinding.Bindings.Count > 0:
                    items = memberBinding.Bindings.Cast<object>();
                    initializerKeyword = "With ";
                    itemsPath = "Bindings";
                    break;
            }

            Write($"{initializerKeyword}{{");

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
                Write(" With {");
                Indent();
                WriteEOL();
                WriteNodes("Bindings", expr.Bindings, true);
                WriteEOL(true);
                Write("}");
            }
        }

        protected override void WriteListInit(ListInitExpression expr) {
            WriteNode("NewExpression", expr.NewExpression);
            Write(" From {");
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
                    if (expr.Expressions.None() || expr.Expressions.Any(x => x.Type != elementType)) {
                        Write($"New {expr.Type.FriendlyName(language)} ");
                    }
                    Write("{ ");
                    expr.Expressions.ForEach((arg, index) => {
                        if (index > 0) { Write(", "); }
                        if (arg.NodeType == NewArrayInit) { Write("("); }
                        WriteNode($"Expressions[{index}]", arg);
                        if (arg.NodeType == NewArrayInit) { Write(")"); }
                    });
                    Write(" }");
                    break;
                case NewArrayBounds:
                    (string left, string right) specifierChars = ("(", ")");
                    var nestedArrayTypes = expr.Type.NestedArrayTypes().ToList();
                    Write($"New {nestedArrayTypes.Last().root.FriendlyName(language)}");
                    nestedArrayTypes.ForEachT((current, _, arrayTypeIndex) => {
                        Write(specifierChars.left);
                        if (arrayTypeIndex == 0) {
                            expr.Expressions.ForEach((x, index) => {
                                if (index > 0) { Write(", "); }

                                // because in VB.NET the upper bound of an array is specified, not the numbe of items
                                Expression newExpr;
                                if (x is ConstantExpression cexpr) {
                                    object newValue = ((dynamic)cexpr.Value) - 1;
                                    newExpr = Expression.Constant(newValue);
                                } else {
                                    newExpr = Expression.SubtractChecked(x, Expression.Constant(1));
                                }

                                WriteNode($"Expressions[{index}]", newExpr);
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

        protected override void WriteConditional(ConditionalExpression expr, object metadata) {
            var parentIsConditional = ((VBExpressionMetadata)metadata ?? CreateMetadata(false, null)).ExpressionType == Conditional;

            if (expr.Type != typeof(void)) {
                Write("If(");
                WriteNode("Test", expr.Test);
                Write(", ");
                WriteNode("IfTrue", expr.IfTrue);
                Write(", ");
                WriteNode("IfFalse", expr.IfFalse);
                Write(")");
                return;
            }

            var outgoingMetadata = CreateMetadata(true, Conditional);

            if (CanInline(expr.Test)) {
                Write("If ");
                WriteNode("Test", expr.Test);
                Write(" Then");
            } else {
                Write("If");
                Indent();
                WriteEOL();
                WriteNode("Test", expr.Test, outgoingMetadata);
                WriteEOL(true);
                Write("Then");
            }

            var canInline = new[] { expr.IfTrue, expr.IfFalse }.All(x => CanInline(x)) && !parentIsConditional;
            if (canInline) {
                Write(" ");
                WriteNode("IfTrue", expr.IfTrue);
                if (!expr.IfFalse.IsEmpty()) {
                    Write(" Else ");
                    WriteNode("IfFalse", expr.IfFalse);
                }
                return;
            }

            Indent();
            WriteEOL();
            WriteNode("IfTrue", expr.IfTrue, outgoingMetadata);
            WriteEOL(true);
            if (expr.IfFalse.IsEmpty()) {
                Write("End If");
                return;
            }

            Write("Else");
            if (expr.IfFalse is ConditionalExpression) {
                Write(" ");
                WriteNode("IfFalse", expr.IfFalse, outgoingMetadata);
            } else {
                Indent();
                WriteEOL();
                WriteNode("IfFalse", expr.IfFalse, outgoingMetadata);
                WriteEOL(true);
                Write("End If");
            }
        }

        protected override void WriteDefault(DefaultExpression expr) =>
            Write($"CType(Nothing, {expr.Type.FriendlyName(language)})");

        protected override void WriteTypeBinary(TypeBinaryExpression expr) {
            switch (expr.NodeType) {
                case TypeIs:
                    Write("TypeOf ");
                    WriteNode("Expression", expr.Expression);
                    Write($" Is {expr.TypeOperand.FriendlyName(language)}");
                    break;
                case TypeEqual:
                    WriteNode("Expression", expr.Expression);
                    Write($".GetType = GetType({expr.TypeOperand.FriendlyName(language)})");
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

        protected override void WriteBlock(BlockExpression expr, object metadata) {
            var (isInMultiline, parentType) = (VBExpressionMetadata)metadata ?? CreateMetadata(false, null);
            var useBlockConstruct = !isInMultiline ||
                (expr.Variables.Any() && parentType == Block);
            if (useBlockConstruct) {
                Write("Block");
                Indent();
                WriteEOL();
            }
            expr.Variables.ForEach((v, index) => {
                if (index > 0) { WriteEOL(); }
                Write("Dim ");
                WriteNode($"Variables[{index}]", v, true);
            });
            expr.Expressions.ForEach((subexpr, index) => {
                if (index > 0 || expr.Variables.Count > 0) { WriteEOL(); }
                if (subexpr is LabelExpression) { TrimEnd(); }
                WriteNode($"Expressions[{index}]", subexpr, CreateMetadata(true, Block));
            });
            if (useBlockConstruct) {
                WriteEOL(true);
                Write("End Block");
            }
        }

        private bool CanInline(Expression expr) {
            switch (expr) {
                case ConditionalExpression cexpr when cexpr.Type == typeof(void):
                case BlockExpression bexpr when
                    bexpr.Expressions.Count > 1 ||
                    bexpr.Variables.Any() ||
                    (bexpr.Expressions.Count == 1 && CanInline(bexpr.Expressions.First())):
                case SwitchExpression _:
                case LambdaExpression _:
                case TryExpression _:
                    return false;
                case RuntimeVariablesExpression _:
                    throw new NotImplementedException();
            }
            return true;
        }

        protected override void WriteSwitchCase(SwitchCase switchCase) {
            Write("Case ");
            WriteNodes("TestValues", switchCase.TestValues);
            Indent();
            WriteEOL();
            WriteNode("Body", switchCase.Body, CreateMetadata(true, null));
            Dedent();
        }

        protected override void WriteSwitch(SwitchExpression expr) {
            Write("Select Case ");
            Indent();
            WriteNode("SwitchValue", expr.SwitchValue);
            WriteEOL();
            WriteNodes("Cases", expr.Cases, true, "");
            if (expr.DefaultBody != null) {
                if (expr.Cases.Count > 0) { WriteEOL(); }
                Write("Case Else");
                Indent();
                WriteEOL();
                WriteNode("DefaultBody", expr.DefaultBody, CreateMetadata(true, Switch));
                Dedent();
            }
            WriteEOL(true);
            Write("End Select");
        }

        protected override void WriteCatchBlock(CatchBlock catchBlock) {
            Write("Catch");
            if (catchBlock.Variable != null) {
                Write(" ");
                WriteNode("Variable", catchBlock.Variable, true);
            } else if (catchBlock.Test != null && catchBlock.Test != typeof(Exception)) {
                Write($" _ As {catchBlock.Test.FriendlyName(language)}");
            }
            if (catchBlock.Filter != null) {
                Write(" When ");
                WriteNode("Filter", catchBlock.Filter);
            }
            Indent();
            WriteEOL();
            WriteNode("Body", catchBlock.Body, CreateMetadata(true, null));
        }

        protected override void WriteTry(TryExpression expr) {
            Write("Try");
            Indent();
            WriteEOL();
            WriteNode("Body", expr.Body, CreateMetadata(true, Try));
            WriteEOL(true);
            expr.Handlers.ForEach((catchBlock, index) => {
                WriteNode($"Handlers[{index}]", catchBlock);
                WriteEOL(true);
            });
            if (expr.Fault != null) {
                Write("Fault");
                Indent();
                WriteEOL();
                WriteNode("Fault", expr.Fault, CreateMetadata(true, Try));
                WriteEOL(true);
            }
            if (expr.Finally != null) {
                Write("Finally");
                Indent();
                WriteEOL();
                WriteNode("Finally", expr.Finally, CreateMetadata(true, Try));
                WriteEOL(true);
            }
            Write("End Try");
        }

        protected override void WriteLabel(LabelExpression expr) {
            WriteNode("Target", expr.Target);
            Write(":");
        }

        protected override void WriteGoto(GotoExpression expr) {
            string gotoKeyword;
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
                WriteNode("Target", expr.Target);
            }
            if (expr.Value != null) {
                Write(" ");
                WriteNode("Value", expr.Value);
            }
        }

        protected override void WriteLabelTarget(LabelTarget labelTarget) => Write(labelTarget.Name);

        protected override void WriteLoop(LoopExpression expr) {
            Write("Do");
            Indent();
            WriteEOL();
            WriteNode("Body", expr.Body, CreateMetadata(true, Loop));
            WriteEOL(true);
            Write("Loop");
        }

        protected override void WriteRuntimeVariables(RuntimeVariablesExpression expr) {
            Write("' Variables -- ");
            expr.Variables.ForEach((x, index) => {
                if (index > 0) { Write(", "); }
                WriteNode($"Variables[{index}]", x, true);
            });
        }

        protected override void WriteDebugInfo(DebugInfoExpression expr) {
            var filename = expr.Document.FileName;
            Write("' ");
            var comment =
                expr.IsClear ?
                $"Clear debug info from {filename}" :
                $"Debug to {filename} -- L{expr.StartLine}C{expr.StartColumn} : L{expr.EndLine}C{expr.EndColumn}";
            Write(comment);
        }

        protected override void WriteBinaryOperationBinder(BinaryOperationBinder binder, IList<Expression> args) {
            VerifyCount(args, 2);
            WriteBinary(binder.Operation, "Arguments[0]", args[0], "Arguments[1]", args[1], false);
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
            Write("(");
            WriteNodes(args.Skip(1).Select((arg, index) => ($"Arguments[{index + 1}]", arg)));
            Write(")");
        }

        protected override void WriteGetMemberBinder(GetMemberBinder binder, IList<Expression> args) {
            VerifyCount(args, 1);
            WriteNode("Arguments[0]", args[0]);
            Write($".{binder.Name}");
        }

        protected override void WriteInvokeBinder(InvokeBinder binder, IList<Expression> args) {
            VerifyCount(args, 1, null);
            WriteNode("Arguments[0]", args[0]);
            if (args.Count > 1) {
                Write("(");
                WriteNodes(args.Skip(1).Select((arg, index) => ($"Arguments[{index + 1}]", arg)));
                Write(")");
            }
        }

        protected override void WriteInvokeMemberBinder(InvokeMemberBinder binder, IList<Expression> args) {
            VerifyCount(args, 1, null);
            WriteNode("Arguments[0]", args[0]);
            Write($".{binder.Name}");
            if (args.Count > 1) {
                Write("(");
                WriteNodes(args.Skip(1).Select((arg, index) => ($"Arguments[{index + 1}]", arg)));
                Write(")");
            }
        }

        protected override void WriteSetIndexBinder(SetIndexBinder binder, IList<Expression> args) {
            VerifyCount(args, 3, null);
            WriteNode("Arguments[0]", args[0]);
            Write("(");
            WriteNodes(args.Skip(2).Select((arg, index) => ($"Arguments[{index + 2}]", arg)));
            Write(") = ");
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
