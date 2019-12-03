using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionTreeTestObjects {
    public static class Categories {
        public const string Unary = "Unary";
        public const string NewArray = "New array";
        public const string NewObject = "Object creation and initialization";
        public const string Invocation = "Invocation";
        public const string Method = "Method call";
        public const string Member = "Member access (+ closed variables)";
        public const string Literal = "Literal";
        public const string Lambdas = "Lambda";
        public const string Indexer = "Indexer";
        public const string Defaults = "Defaults";
        public const string Binary = "Binary";
        public const string Try = "Try, Catch, Finally";
        public const string SwitchCases = "Switch, CatchBlock";
        public const string RuntimeVars = "Runtime variables";
        public const string MemberBindings = "Member bindings";
        public const string Loops = "Loops";
        public const string Labels = "Labels";
        public const string Gotos = "Gotos";
        public const string Dynamics = "Dynamics";
        public const string Quoted = "Quoted";
        public const string DebugInfos = "DebugInfos";
        public const string Conditionals = "Conditionals";
        public const string Blocks = "Blocks";
        public const string Constants = "Constants";
        public const string TypeChecks = "Type check";
    }
}
