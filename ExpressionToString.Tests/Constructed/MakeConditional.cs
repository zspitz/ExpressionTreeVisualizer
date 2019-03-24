using System;
using System.Linq.Expressions;
using Xunit;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Runners;
using static ExpressionToString.Util.Functions;
using static System.Linq.Expressions.Expression;

namespace ExpressionToString.Tests.Constructed {
    [Trait("Source", FactoryMethods)]
    public class MakeConditional
    {
        MethodCallExpression writeLineTrue;
        MethodCallExpression writeLineFalse;

        MemberExpression trueLength;
        MemberExpression falseLength;

        // note that the NodeType of the expression constructed Conditional factory method can be either typeof(void) or some other type
        // the NodeTypeof IfThen and IfThenElse is always typeof(void)

        [Fact]
        public void VoidConditionalWithElse() => BuildAssert(
            Condition(
                Constant(true),
                writeLineTrue,
                writeLineFalse
            ), @"
if (true) {
    Console.WriteLine(true);
} else {
    Console.WriteLine(false);
}".TrimStart(), @"
If True Then
    Console.WriteLine(True)
Else
    Console.WriteLine(False)
End If".TrimStart());

        [Fact]
        public void VoidConditional1WithElse() => BuildAssert(
            IfThenElse(
                Constant(true),
                writeLineTrue,
                writeLineFalse
            ), @"
if (true) {
    Console.WriteLine(true);
} else {
    Console.WriteLine(false);
}".TrimStart(), @"
If True Then
    Console.WriteLine(True)
Else
    Console.WriteLine(False)
End If".TrimStart());

        [Fact]
        public void VoidConditionalWithoutElse() => BuildAssert(
            Condition(
                Constant(true),
                writeLineTrue,
                Expression.Default(typeof(void))
            ), @"
if (true) {
    Console.WriteLine(true);
}".TrimStart(), @"
If True Then
    Console.WriteLine(True)
End If".TrimStart());

        [Fact]
        public void VoidConditional1WithoutElse() => BuildAssert(
            IfThen(
                Constant(true),
                writeLineTrue
            ), @"
if (true) {
    Console.WriteLine(true);
}".TrimStart(), @"
If True Then
    Console.WriteLine(True)
End If".TrimStart());

        [Fact]
        public void NonVoidConditionalWithElse() => BuildAssert(
            Condition(
                Constant(true),
                trueLength,
                falseLength
            ), 
            "true ? \"true\".Length : \"false\".Length",
            "If(True, \"true\".Length, \"false\".Length)"
        );

        [Fact]
        public void NonVoidConditionalWithoutElse() => BuildAssert(
            Condition(
                Constant(true),
                trueLength,
                Expression.Default(typeof(int))
            ),
            "true ? \"true\".Length : default(int)",
            "If(True, \"true\".Length, CType(Nothing, Integer))"
        );

        [Fact]
        public void MultilineTestPart() => BuildAssert(
            Condition(
                Block(Constant(true), Constant(true)),
                trueLength,
                falseLength
            ),
            @"{
    true;
    true;
} ? ""true"".Length : ""false"".Length",
            @"If((
    True
    True
), ""true"".Length, ""false"".Length)"
        );

        [Fact]
        public void MultilineTestPart1() => BuildAssert(
            IfThen(
                Block(Constant(true), Constant(true)),
                writeLineTrue
            ), @"
if ({
    true;
    true;
}) {
    Console.WriteLine(true);
}".TrimStart(), @"
If
    True
    True
Then
    Console.WriteLine(True)
End If".TrimStart());

        public MakeConditional() {
            var writeLine = GetMethod(() => Console.WriteLine(true));
            writeLineTrue = Call(writeLine, Constant(true));
            writeLineFalse = Call(writeLine, Constant(false));

            var stringLength = GetMember(() => "".Length);
            trueLength = MakeMemberAccess(Constant("true"), stringLength);
            falseLength = MakeMemberAccess(Constant("false"), stringLength);
        }
    }
}
