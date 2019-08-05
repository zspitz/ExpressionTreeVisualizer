using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using static ExpressionToString.Tests.Functions;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests.Objects {
    public static partial class FactoryMethods {
        //[Category(Blocks)]
        //public static readonly Expression BlockNoVariables = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Blocks)]
        //public static readonly Expression BlockSingleVariable = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Blocks)]
        //public static readonly Expression BlockMultipleVariable = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Blocks)]
        //public static readonly Expression NestedInlineBlock = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Blocks)]
        //public static readonly Expression NestedBlockInTest = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Blocks)]
        //public static readonly Expression NestedBlockInBlockSyntax = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Blocks)]
        //public static readonly Expression NestedInlineBlockWithVariable = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Blocks)]
        //public static readonly Expression NestedBlockInTestWithVariables = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Blocks)]
        //public static readonly Expression NestedBlockInBlockSyntaxWithVariable = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Constants)]
        //public static readonly Expression Random = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Constants)]
        //public static readonly Expression ValueTuple = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Constants)]
        //public static readonly Expression OldTuple = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Constants)]
        //public static readonly Expression DateTime = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Constants)]
        //public static readonly Expression TimeSpan = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Constants)]
        //public static readonly Expression Array = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Constants)]
        //public static readonly Expression Type = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Constants)]
        //public static readonly Expression DifferentTypeForNodeAndValue = IIFE(() => {

        //    return Expr();
        //});
        //[Category(DebugInfos)]
        //public static readonly Expression MakeDebugInfo = IIFE(() => {

        //    return Expr();
        //});
        //[Category(DebugInfos)]
        //public static readonly Expression MakeClearDebugInfo = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Dynamics)]
        //public static readonly Expression ConstructMemberInvocationNoArguments = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Dynamics)]
        //public static readonly Expression ConstructMemberInvocationWithArguments = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Dynamics)]
        //public static readonly Expression ConstructSetIndex = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Dynamics)]
        //public static readonly Expression ConstructSetIndexMultipleKeys = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Dynamics)]
        //public static readonly Expression ConstructSetMember = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Dynamics)]
        //public static readonly Expression ConstructGetIndex = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Dynamics)]
        //public static readonly Expression ConstructGetIndexMultipleKeys = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Dynamics)]
        //public static readonly Expression ConstructGetMember = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Dynamics)]
        //public static readonly Expression ConstructInvocationNoArguments = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Dynamics)]
        //public static readonly Expression ConstructInvocationWithArguments = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Gotos)]
        //public static readonly Expression MakeBreak = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Gotos)]
        //public static readonly Expression MakeBreakWithValue = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Gotos)]
        //public static readonly Expression MakeContinue = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Gotos)]
        //public static readonly Expression MakeGotoWithoutValue = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Gotos)]
        //public static readonly Expression MakeGotoWithValue = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Gotos)]
        //public static readonly Expression MakeReturn = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Gotos)]
        //public static readonly Expression MakeReturnWithValue = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Labels)]
        //public static readonly Expression ConstructLabel = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Labels)]
        //public static readonly Expression ConstructLabel1 = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Lambdas)]
        //public static readonly Expression MakeByRefParameter = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Lambdas)]
        //public static readonly Expression NoParametersVoidReturn = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Lambdas)]
        //public static readonly Expression OneParameterVoidReturn = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Lambdas)]
        //public static readonly Expression TwoParametersVoidReturn = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Lambdas)]
        //public static readonly Expression NoParametersNonVoidReturn = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Lambdas)]
        //public static readonly Expression OneParameterNonVoidReturn = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Lambdas)]
        //public static readonly Expression TwoParametersNonVoidReturn = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Lambdas)]
        //public static readonly Expression NamedLambda = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Lambdas)]
        //public static readonly Expression MultilineLambda = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Lambdas)]
        //public static readonly Expression NestedLambda = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Lambdas)]
        //public static readonly Expression LambdaMultilineBlockNonvoidReturn = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Lambdas)]
        //public static readonly Expression LambdaMultilineNestedBlockNonvoidReturn = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Loops)]
        //public static readonly Expression EmptyLoop = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Loops)]
        //public static readonly Expression EmptyLoop1 = IIFE(() => {

        //    return Expr();
        //});
        //[Category(MemberBindings)]
        //public static readonly Expression StaticMember = IIFE(() => {

        //    return Expr();
        //});
        //[Category(MemberBindings)]
        //public static readonly Expression MakeMemberBind = IIFE(() => {

        //    return Expr();
        //});
        //[Category(MemberBindings)]
        //public static readonly Expression MakeElementInit = IIFE(() => {

        //    return Expr();
        //});
        //[Category(MemberBindings)]
        //public static readonly Expression MakeElementInit2Arguments = IIFE(() => {

        //    return Expr();
        //});
        //[Category(MemberBindings)]
        //public static readonly Expression MakeMemberMemberBind = IIFE(() => {

        //    return Expr();
        //});
        //[Category(MemberBindings)]
        //public static readonly Expression MakeListBinding = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Method)]
        //public static readonly Expression InstanceMethod0Arguments = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Method)]
        //public static readonly Expression StaticMethod0Arguments = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Method)]
        //public static readonly Expression ExtensionMethod0Arguments = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Method)]
        //public static readonly Expression InstanceMethod1Argument = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Method)]
        //public static readonly Expression StaticMethod1Argument = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Method)]
        //public static readonly Expression ExtensionMethod1Argument = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Method)]
        //public static readonly Expression InstanceMethod2Arguments = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Method)]
        //public static readonly Expression StaticMethod2Arguments = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Method)]
        //public static readonly Expression ExtensionMethod2Arguments = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Method)]
        //public static readonly Expression StringConcat = IIFE(() => {

        //    return Expr();
        //});
        //[Category(NewArray)]
        //public static readonly Expression SingleDimensionInit = IIFE(() => {

        //    return Expr();
        //});
        //[Category(NewArray)]
        //public static readonly Expression SingleDimensionInitExplicitType = IIFE(() => {

        //    return Expr();
        //});
        //[Category(NewArray)]
        //public static readonly Expression SingleDimensionWithBounds = IIFE(() => {

        //    return Expr();
        //});
        //[Category(NewArray)]
        //public static readonly Expression MultidimensionWithBounds = IIFE(() => {

        //    return Expr();
        //});
        //[Category(NewArray)]
        //public static readonly Expression JaggedWithElementsImplicitType = IIFE(() => {

        //    return Expr();
        //});
        //[Category(NewArray)]
        //public static readonly Expression JaggedWithElementsExplicitType = IIFE(() => {

        //    return Expr();
        //});
        //[Category(NewArray)]
        //public static readonly Expression JaggedWithBounds = IIFE(() => {

        //    return Expr();
        //});
        //[Category(NewArray)]
        //public static readonly Expression ArrayOfMultidimensionalArray = IIFE(() => {

        //    return Expr();
        //});
        //[Category(NewArray)]
        //public static readonly Expression MultidimensionalArrayOfArray = IIFE(() => {

        //    return Expr();
        //});
        //[Category(NewObject)]
        //public static readonly Expression NamedType = IIFE(() => {

        //    return Expr();
        //});
        //[Category(NewObject)]
        //public static readonly Expression NamedTypeWithInitializer = IIFE(() => {

        //    return Expr();
        //});
        //[Category(NewObject)]
        //public static readonly Expression NamedTypeWithInitializers = IIFE(() => {

        //    return Expr();
        //});
        //[Category(NewObject)]
        //public static readonly Expression NamedTypeConstructorParameters = IIFE(() => {

        //    return Expr();
        //});
        //[Category(NewObject)]
        //public static readonly Expression NamedTypeConstructorParametersWithInitializers = IIFE(() => {

        //    return Expr();
        //});
        //[Category(NewObject)]
        //public static readonly Expression CollectionTypeWithInitializer = IIFE(() => {

        //    return Expr();
        //});
        //[Category(NewObject)]
        //public static readonly Expression CollectionTypeWithMultiElementInitializers = IIFE(() => {

        //    return Expr();
        //});
        //[Category(NewObject)]
        //public static readonly Expression CollectionTypeWithSingleOrMultiElementInitializers = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Quoted)]
        //public static readonly Expression MakeQuoted = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Quoted)]
        //public static readonly Expression MakeQuoted1 = IIFE(() => {

        //    return Expr();
        //});
        //[Category(RuntimeVars)]
        //public static readonly Expression ConstructRuntimeVariables = IIFE(() => {

        //    return Expr();
        //});
        //[Category(RuntimeVars)]
        //public static readonly Expression RuntimeVariablesWithinBlock = IIFE(() => {

        //    return Expr();
        //});
        //[Category(SwitchCases)]
        //public static readonly Expression SingleValueSwitchCase = IIFE(() => {

        //    return Expr();
        //});
        //[Category(SwitchCases)]
        //public static readonly Expression MultiValueSwitchCase = IIFE(() => {

        //    return Expr();
        //});
        //[Category(SwitchCases)]
        //public static readonly Expression SingleValueSwitchCase1 = IIFE(() => {

        //    return Expr();
        //});
        //[Category(SwitchCases)]
        //public static readonly Expression MultiValueSwitchCase1 = IIFE(() => {

        //    return Expr();
        //});
        //[Category(SwitchCases)]
        //public static readonly Expression SwitchOnExpressionWithDefaultSingleStatement = IIFE(() => {

        //    return Expr();
        //});
        //[Category(SwitchCases)]
        //public static readonly Expression SwitchOnExpressionWithDefaultMultiStatement = IIFE(() => {

        //    return Expr();
        //});
        //[Category(SwitchCases)]
        //public static readonly Expression SwitchOnMultipleStatementsWithDefault = IIFE(() => {

        //    return Expr();
        //});
        //[Category(SwitchCases)]
        //public static readonly Expression SwitchOnExpressionWithoutDefault = IIFE(() => {

        //    return Expr();
        //});
        //[Category(SwitchCases)]
        //public static readonly Expression SwitchOnMultipleStatementsWithoutDefault = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Try)]
        //public static readonly Expression ConstructTryFault = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Try)]
        //public static readonly Expression ConstructTryFinally = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Try)]
        //public static readonly Expression ConstructSimpleCatch = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Try)]
        //public static readonly Expression ConstructCatchSingleStatement = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Try)]
        //public static readonly Expression ConstructCatchMultiStatement = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Try)]
        //public static readonly Expression ConstructCatchSingleStatementWithType = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Try)]
        //public static readonly Expression ConstructCatchMultiStatementWithType = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Try)]
        //public static readonly Expression ConstructCatchSingleStatementWithFilter = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Try)]
        //public static readonly Expression ConstructCatchMultiStatementWithFilter = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Try)]
        //public static readonly Expression ConstructCatchWithMultiStatementFilter = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Try)]
        //public static readonly Expression ConstructTryCatch = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Try)]
        //public static readonly Expression ConstructTryCatchFinally = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Unary)]
        //public static readonly Expression ConstructArrayLength = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Unary)]
        //public static readonly Expression ConstructConvert = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Unary)]
        //public static readonly Expression ConstructConvertChecked = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Unary)]
        //public static readonly Expression ConstructConvertCheckedForReferenceType = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Unary)]
        //public static readonly Expression ConstructNegate = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Unary)]
        //public static readonly Expression ConstructBitwiseNot = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Unary)]
        //public static readonly Expression ConstructLogicalNot = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Unary)]
        //public static readonly Expression ConstructTypeAs = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Unary)]
        //public static readonly Expression ConstructPostDecrementAssign = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Unary)]
        //public static readonly Expression ConstructPostIncrementAssign = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Unary)]
        //public static readonly Expression ConstructPreDecrementAssign = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Unary)]
        //public static readonly Expression ConstructPreIncrementAssign = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Unary)]
        //public static readonly Expression ConstructIsTrue = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Unary)]
        //public static readonly Expression ConstructIsFalse = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Unary)]
        //public static readonly Expression ConstructIncrement = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Unary)]
        //public static readonly Expression ConstructDecrement = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Unary)]
        //public static readonly Expression ConstructThrow = IIFE(() => {

        //    return Expr();
        //});
        //[Category(Unary)]
        //public static readonly Expression ConstructRethrow = IIFE(() => {

        //    return Expr();
        //});

    }
}
