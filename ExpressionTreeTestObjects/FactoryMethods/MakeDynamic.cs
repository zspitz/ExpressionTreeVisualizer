using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using static ExpressionTreeTestObjects.Functions;
using static ExpressionTreeTestObjects.Categories;
using static System.Linq.Expressions.Expression;
using Microsoft.CSharp.RuntimeBinder;
using static Microsoft.CSharp.RuntimeBinder.Binder;


namespace ExpressionTreeTestObjects {
    partial class FactoryMethods {
        static readonly CSharpBinderFlags flags = CSharpBinderFlags.None;
        static readonly Type context = typeof(FactoryMethods);
        static readonly CSharpArgumentInfo[] argInfos = new[] { CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null) };
        static readonly CSharpArgumentInfo[] argInfos2 = new[] {
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
            CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
        };
        static readonly ParameterExpression obj = Parameter(typeof(object), "obj");
        static readonly ConstantExpression key = Constant("key");
        static readonly ConstantExpression key1 = Constant(1);
        static readonly ConstantExpression value = Constant(42);
        static readonly ConstantExpression arg1 = Constant("arg1");
        static readonly ConstantExpression arg2 = Constant(15);

        // TODO write test objects for the following types in System.Dynamic:
        //      CreateInstanceBinder (can't create from Microsoft.CSharp.RuntimeBinder classes because Microsoft.CSharp.RuntimeBinder.CSharpInvokeBinder inherits directly from DynamicMetaObjectBinder)

        // TODO what about VB runtime binder?

        [Category(Dynamics)]
        internal static readonly Expression ConstructMemberInvocationNoArguments = IIFE(() => {
            var binder = InvokeMember(flags, "Method", new Type[] { }, context, argInfos);
            return Dynamic(binder, typeof(object), obj);
        });

        [Category(Dynamics)]
        internal static readonly Expression ConstructMemberInvocationWithArguments = IIFE(() => {
            var binder = InvokeMember(flags, "Method", new Type[] { }, context, argInfos);
            return Dynamic(binder, typeof(object), obj, arg1, arg2);
        });

        [Category(Dynamics)]
        internal static readonly Expression ConstructSetIndex = IIFE(() => {
            var binder = SetIndex(flags, context, argInfos2);
            return Dynamic(binder, typeof(object), obj, value, key);
        });

        [Category(Dynamics)]
        internal static readonly Expression ConstructSetIndexMultipleKeys = IIFE(() => {
            var binder = SetIndex(flags, context, argInfos2);
            return Dynamic(binder, typeof(object), obj, value, key, key1);
        });

        [Category(Dynamics)]
        internal static readonly Expression ConstructSetMember = IIFE(() => {
            var binder = SetMember(flags, "Data", context, argInfos);
            return Dynamic(binder, typeof(object), obj, value);
        });

    [Category(Dynamics)]
      internal static readonly Expression ConstructGetIndex = IIFE(() => {
          var binder = GetIndex(flags, context, argInfos);
          return Dynamic(binder, typeof(object), obj, key);
      });

        [Category(Dynamics)]
      internal static readonly Expression ConstructGetIndexMultipleKeys = IIFE(() => {
          var binder = GetIndex(flags, context, argInfos);
          return Dynamic(binder, typeof(object), obj, key, key1);
      });

        [Category(Dynamics)]
        internal static readonly Expression ConstructGetMember = IIFE(() => {
            var binder = GetMember(flags, "Data", context, argInfos);
            return Dynamic(binder, typeof(object), obj);
        });

        [Category(Dynamics)]
        internal static readonly Expression ConstructInvocationNoArguments = IIFE(() => {
            var binder = Invoke(flags, context, argInfos);
            return Dynamic(binder, typeof(object), obj);
        });

        [Category(Dynamics)]
        internal static readonly Expression ConstructInvocationWithArguments = IIFE(() => {
            var binder = Invoke(flags, context, argInfos);
            return Dynamic(binder, typeof(object), obj, arg1, arg2);
        });

        // TODO create test objects specifically for the classes in Microsoft.CSharp.RuntimeBinder
        // TODO including invoking methods with generic parameters

        //internal static readonly ConstructGenericMemberInvocationNoArguments = IIFE(() => {
        //    var obj = Parameter(typeof(object), "obj");
        //    var binder = InvokeMember(flags, "Method", new Type[] { typeof(string), typeof(int)}, context, argInfos);

        //    return Dynamic(binder, typeof(object), obj);

        //    C#: obj.Method()
        //    VB: obj.Method
        //});

        //[Fact]
        //internal static readonly ConstructGenericMemberInvocationWithArguments = IIFE(() => {
        //    var obj = Parameter(typeof(object), "obj");
        //    var arg1 = Constant("arg1");
        //    var arg2 = Constant(15);

        //    var binder = InvokeMember(flags, "Method", new Type[] { typeof(string), typeof(int) }, context, argInfos);

        //    return Dynamic(binder, typeof(object), obj, arg1, arg2);

        //    C#: obj.Method<string, int>(\"arg1\", 15)
        //    VB: obj(Of String, Integer)(\"arg1\", 15)
        //}
    }
}