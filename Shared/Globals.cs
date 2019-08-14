using ExpressionToString.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using static System.Linq.Expressions.ExpressionType;

namespace ExpressionToString {
    public static class FormatterNames {
        public const string CSharp = "C#";
        public const string VisualBasic = "Visual Basic";
        public const string FactoryMethods = "Factory methods";
        public const string ObjectNotation = "Object notation";
        public const string DebugView = "DebugView";
        public const string TextualTree = "Textual tree";
    }

    public static class Globals {
        public readonly static HashSet<MethodInfo> stringConcats;
        public readonly static HashSet<MethodInfo> stringFormats;

        static Globals() {
            var methods = typeof(string)
                .GetMethods()
                .Where(x => {
                    switch (x.Name) {
                        case "Concat":
                            return x.GetParameters().All(
                                y => y.ParameterType.In(typeof(string), typeof(string[]))
                            );
                        case "Format":
                            return x.GetParameters().First().ParameterType == typeof(string);
                        default: return false;
                    }
                })
                .ToLookup(x => x.Name);

            stringConcats = methods["Concat"].ToHashSet();
            stringFormats = methods["Format"].ToHashSet();
        }

        public readonly static List<Type> NodeTypes = new List<Type>() {
            typeof(Expression),
            typeof(MemberBinding),
            typeof(ElementInit),
            typeof(SwitchCase),
            typeof(CatchBlock),
            typeof(LabelTarget)
        };

        public readonly static List<Type> PropertyTypes = NodeTypes.Select(x => typeof(IEnumerable<>).MakeGenericType(x)).ToList();

        public readonly static Dictionary<ExpressionType, string> BinaryUnaryMethods = new Dictionary<ExpressionType, string>() {
            { Add, "Add" },
            { AddAssign, "AddAssign" },
            { AddAssignChecked, "AddAssignChecked" },
            { AddChecked, "AddChecked" },
            { And, "And" },
            { AndAlso, "AndAlso" },
            { AndAssign, "AndAssign" },
            { ArrayIndex, "ArrayIndex" },
            { ArrayLength, "ArrayLength" },
            { Assign, "Assign" },
            { Block, "Block" },
            { Call, "Call" },
            { Coalesce, "Coalesce" },
            { Conditional, "Conditional" },
            { Constant, "Constant" },
            { ExpressionType.Convert, "Convert" },
            { ConvertChecked, "ConvertChecked" },
            { DebugInfo, "DebugInfo" },
            { Decrement, "Decrement" },
            { Default, "Default" },
            { Divide, "Divide" },
            { DivideAssign, "DivideAssign" },
            { Dynamic, "Dynamic" },
            { Equal, "Equal" },
            { ExclusiveOr, "ExclusiveOr" },
            { ExclusiveOrAssign, "ExclusiveOrAssign" },
            { Extension, "Extension" },
            { Goto, "Goto" },
            { GreaterThan, "GreaterThan" },
            { GreaterThanOrEqual, "GreaterThanOrEqual" },
            { Increment, "Increment" },
            { Index, "Index" },
            { Invoke, "Invoke" },
            { IsFalse, "IsFalse" },
            { IsTrue, "IsTrue" },
            { Label, "Label" },
            { Lambda, "Lambda" },
            { LeftShift, "LeftShift" },
            { LeftShiftAssign, "LeftShiftAssign" },
            { LessThan, "LessThan" },
            { LessThanOrEqual, "LessThanOrEqual" },
            { ListInit, "ListInit" },
            { Loop, "Loop" },
            { MemberAccess, "MemberAccess" },
            { MemberInit, "MemberInit" },
            { Modulo, "Modulo" },
            { ModuloAssign, "ModuloAssign" },
            { Multiply, "Multiply" },
            { MultiplyAssign, "MultiplyAssign" },
            { MultiplyAssignChecked, "MultiplyAssignChecked" },
            { MultiplyChecked, "MultiplyChecked" },
            { Negate, "Negate" },
            { NegateChecked, "NegateChecked" },
            { New, "New" },
            { NewArrayBounds, "NewArrayBounds" },
            { NewArrayInit, "NewArrayInit" },
            { Not, "Not" },
            { NotEqual, "NotEqual" },
            { OnesComplement, "OnesComplement" },
            { Or, "Or" },
            { OrAssign, "OrAssign" },
            { OrElse, "OrElse" },
            { Parameter, "Parameter" },
            { PostDecrementAssign, "PostDecrementAssign" },
            { PostIncrementAssign, "PostIncrementAssign" },
            { Power, "Power" },
            { PowerAssign, "PowerAssign" },
            { PreDecrementAssign, "PreDecrementAssign" },
            { PreIncrementAssign, "PreIncrementAssign" },
            { Quote, "Quote" },
            { RightShift, "RightShift" },
            { RightShiftAssign, "RightShiftAssign" },
            { RuntimeVariables, "RuntimeVariables" },
            { Subtract, "Subtract" },
            { SubtractAssign, "SubtractAssign" },
            { SubtractAssignChecked, "SubtractAssignChecked" },
            { SubtractChecked, "SubtractChecked" },
            { Switch, "Switch" },
            { Throw, "Throw" },
            { Try, "Try" },
            { TypeAs, "TypeAs" },
            { TypeEqual, "TypeEqual" },
            { TypeIs, "TypeIs" },
            { UnaryPlus, "UnaryPlus" },
            { Unbox, "Unbox" },
        };

        public readonly static List<(Type type, string[] propertyNames)> PreferredPropertyOrders = new List<(Type, string[])> {
            (typeof(LambdaExpression), new [] {"Parameters", "Body" } ),
            (typeof(BinaryExpression), new [] {"Left", "Right", "Conversion"}),
            (typeof(BlockExpression), new [] { "Variables", "Expressions"}),
            (typeof(CatchBlock), new [] { "Variable", "Filter", "Body"}),
            (typeof(ConditionalExpression), new [] { "Test", "IfTrue", "IfFalse"}),
            (typeof(IndexExpression), new [] { "Object", "Arguments" }),
            (typeof(InvocationExpression), new [] {"Arguments", "Expression"}),
            (typeof(ListInitExpression), new [] {"NewExpression", "Initializers"}),
            (typeof(MemberInitExpression), new [] {"NewExpression", "Bindings"}),
            (typeof(MethodCallExpression), new [] {"Object", "Arguments"}),
            (typeof(SwitchCase), new [] {"TestValues", "Body"}),
            (typeof(SwitchExpression), new [] {"SwitchValue", "Cases", "DefaultBody"}),
            (typeof(TryExpression), new [] {"Body", "Handlers", "Finally", "Fault"}),
            (typeof(DynamicExpression), new [] {"Binder", "Arguments"})
        };
    }
}
