using System;
using System.Dynamic;
using System.Runtime.CompilerServices;

namespace ExpressionTreeVisualizer.Util {
    public static class CallSiteBinderExtensions {
        public static string BinderType(this CallSiteBinder callSiteBinder) {
            switch (callSiteBinder) {
                case BinaryOperationBinder _:
                    return "BinaryOperation";
                case ConvertBinder _:
                    return "Convert";
                case CreateInstanceBinder _:
                    return "CreateInstance";
                case DeleteIndexBinder _:
                    return "DeleteIndex";
                case DeleteMemberBinder _:
                    return "DeleteMember";
                case GetIndexBinder _:
                    return "GetIndex";
                case GetMemberBinder _:
                    return "GetMember";
                case InvokeBinder _:
                    return "Invoke";
                case InvokeMemberBinder _:
                    return "InvokeMember";
                case SetIndexBinder _:
                    return "SetIndex";
                case SetMemberBinder _:
                    return "SetMember";
                case UnaryOperationBinder _:
                    return "UnaryOperation";
                case DynamicMetaObjectBinder _:
                    return "Dynamic";
                default:
                    return "(Unknown binder)";
            }
        }
    }
}
