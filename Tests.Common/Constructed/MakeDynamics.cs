using System;
using Xunit;
using Microsoft.CSharp.RuntimeBinder;
using static Microsoft.CSharp.RuntimeBinder.Binder;
using static System.Linq.Expressions.Expression;
using System.Linq.Expressions;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
        readonly CSharpBinderFlags flags = CSharpBinderFlags.None;
        readonly Type context = typeof(ConstructedBase);
        readonly CSharpArgumentInfo[] argInfos = new[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
        readonly CSharpArgumentInfo[] argInfos2 = new[] {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
        };
        readonly ParameterExpression obj = Parameter(typeof(object), "obj");
        readonly ConstantExpression key = Constant("key");
        readonly ConstantExpression key1 = Constant(1);
        readonly ConstantExpression value = Constant(42);
        readonly ConstantExpression arg1 = Constant("arg1");
        readonly ConstantExpression arg2 = Constant(15);


        // TODO tests for the following types in System.Dynamic:
        //      CreateInstanceBinder (can't create from Microsoft.CSharp.RuntimeBinder classes because Microsoft.CSharp.RuntimeBinder.CSharpInvokeBinder inherits directly from DynamicMetaObjectBinder)

        // TODO what about VB runtime binder?

        [Fact]
        [Trait("Category", Dynamics)]
        public void ConstructGetIndex() {
            var binder = GetIndex(flags, context, argInfos);
            var expr = Dynamic(binder, typeof(object), obj, key);

            RunTest(
                expr,
                "obj[\"key\"]",
                "obj(\"key\")", 
                @"Dynamic(
    #CSharpGetIndexBinder,
    typeof(object), new [] {
        obj,
        Constant(""key"")
    }
)"
            );
        }

        [Fact]
        [Trait("Category", Dynamics)]
        public void ConstructGetIndexMultipleKeys() {
            var binder = GetIndex(flags, context, argInfos);
            var expr = Dynamic(binder, typeof(object), obj, key, key1);

            RunTest(
                expr,
                "obj[\"key\", 1]",
                "obj(\"key\", 1)", 
                @"Dynamic(
    #CSharpGetIndexBinder,
    typeof(object), new [] {
        obj,
        Constant(""key""),
        Constant(1)
    }
)"
            );
        }

        [Fact]
        [Trait("Category", Dynamics)]
        public void ConstructGetMember() {
            var binder = GetMember(flags, "Data", context, argInfos);
            var expr = Dynamic(binder, typeof(object), obj);

            RunTest(
                expr,
                "obj.Data",
                "obj.Data",
                @"Dynamic(
    #CSharpGetMemberBinder,
    typeof(object), new [] { obj }
)"
            );
        }

        [Fact]
        [Trait("Category", Dynamics)]
        public void ConstructInvocationNoArguments() {
            var binder = Invoke(flags, context, argInfos);
            var expr = Dynamic(binder, typeof(object), obj);

            RunTest(
                expr,
                "obj()",
                "obj",
                @"Dynamic(
    #CSharpInvokeBinder,
    typeof(object), new [] { obj }
)"
            );
        }

        [Fact]
        [Trait("Category", Dynamics)]
        public void ConstructInvocationWithArguments() {
            var binder = Invoke(flags, context, argInfos);
            var expr = Dynamic(binder, typeof(object), obj, arg1, arg2);

            RunTest(
                expr,
                "obj(\"arg1\", 15)",
                "obj(\"arg1\", 15)", 
                @"Dynamic(
    #CSharpInvokeBinder,
    typeof(object), new [] {
        obj,
        Constant(""arg1""),
        Constant(15)
    }
)"
            );
        }

        [Fact]
        [Trait("Category", Dynamics)]
        public void ConstructMemberInvocationNoArguments() {
            var binder = InvokeMember(flags, "Method", new Type[] { }, context, argInfos);
            var expr = Dynamic(binder, typeof(object), obj);

            RunTest(
                expr,
                "obj.Method()",
                "obj.Method",
                @"Dynamic(
    #CSharpInvokeMemberBinder,
    typeof(object), new [] { obj }
)"
            );
        }

        [Fact]
        [Trait("Category", Dynamics)]
        public void ConstructMemberInvocationWithArguments() {
            var binder = InvokeMember(flags, "Method", new Type[] { }, context, argInfos);
            var expr = Dynamic(binder, typeof(object), obj, arg1, arg2);

            RunTest(
                expr,
                "obj.Method(\"arg1\", 15)",
                "obj.Method(\"arg1\", 15)", 
                @"Dynamic(
    #CSharpInvokeMemberBinder,
    typeof(object), new [] {
        obj,
        Constant(""arg1""),
        Constant(15)
    }
)"
            );
        }

        [Fact]
        [Trait("Category", Dynamics)]
        public void ConstructSetIndex() {
            var binder = SetIndex(flags, context, argInfos2);
            var expr = Dynamic(binder, typeof(object), obj, value, key);

            RunTest(
                expr,
                "obj[\"key\"] = 42",
                "obj(\"key\") = 42", 
                @"Dynamic(
    #CSharpSetIndexBinder,
    typeof(object), new [] {
        obj,
        Constant(42),
        Constant(""key"")
    }
)"
            );
        }

        [Fact]
        [Trait("Category", Dynamics)]
        public void ConstructSetIndexMultipleKeys() {
            var binder = SetIndex(flags, context, argInfos2);
            var expr = Dynamic(binder, typeof(object), obj, value, key, key1);

            RunTest(
                expr,
                "obj[\"key\", 1] = 42",
                "obj(\"key\", 1) = 42",
                @"Dynamic(
    #CSharpSetIndexBinder,
    typeof(object), new [] {
        obj,
        Constant(42),
        Constant(""key""),
        Constant(1)
    }
)"
            );
        }

        [Fact]
        [Trait("Category", Dynamics)]
        public void ConstructSetMember() {
            var binder = SetMember(flags, "Data", context, argInfos);
            var expr = Dynamic(binder, typeof(object), obj, value);

            RunTest(
                expr,
                "obj.Data = 42",
                "obj.Data = 42", 
                @"Dynamic(
    #CSharpSetMemberBinder,
    typeof(object), new [] {
        obj,
        Constant(42)
    }
)"
            );
        }


        // TODO create tests specifically for the classes in Microsoft.CSharp.RuntimeBinder
        // TODO including invoking methods with generic parameters

        //[Fact]
        //public void ConstructGenericMemberInvocationNoArguments() {
        //    var obj = Parameter(typeof(object), "obj");
        //    var binder = InvokeMember(flags, "Method", new Type[] { typeof(string), typeof(int)}, context, argInfos);

        //    var expr = Dynamic(binder, typeof(object), obj);

        //    BuildAssert(
        //        expr,
        //        "obj.Method()",
        //        "obj.Method"
        //    );
        //}

        //[Fact]
        //public void ConstructGenericMemberInvocationWithArguments() {
        //    var obj = Parameter(typeof(object), "obj");
        //    var arg1 = Constant("arg1");
        //    var arg2 = Constant(15);

        //    var binder = InvokeMember(flags, "Method", new Type[] { typeof(string), typeof(int) }, context, argInfos);

        //    var expr = Dynamic(binder, typeof(object), obj, arg1, arg2);

        //    BuildAssert(
        //        expr,
        //        "obj.Method<string, int>(\"arg1\", 15)",
        //        "obj(Of String, Integer)(\"arg1\", 15)"
        //    );
        //}
    }
}
