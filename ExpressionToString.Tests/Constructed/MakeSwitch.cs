using System.Linq.Expressions;
using Xunit;
using static ExpressionToString.Tests.Globals;
using static ExpressionToString.Tests.Runners;
using static System.Linq.Expressions.Expression;

namespace ExpressionToString.Tests.Constructed {
    [Trait("Source", FactoryMethods)]
    public class MakeSwitch {
        static readonly SwitchCase singleValueCase = SwitchCase(
            Block(writeLineTrue, writeLineTrue),
            Constant(5)
        );
        static readonly SwitchCase multiValueCase = SwitchCase(
            Block(writeLineTrue, writeLineTrue),
            Constant(5),
            Constant(6)
        );

        [Fact]
        public void SingleValueSwitchCase() => BuildAssert(
            singleValueCase,
            @"case 5:
    Console.WriteLine(true);
    Console.WriteLine(true);
    break;",
            @"Case 5
    Console.WriteLine(True)
    Console.WriteLine(True)"
        );

        [Fact]
        public void MultiValueSwitchCase() => BuildAssert(
            multiValueCase,
            @"case 5:
case 6:
    Console.WriteLine(true);
    Console.WriteLine(true);
    break;",
            @"Case 5, 6
    Console.WriteLine(True)
    Console.WriteLine(True)"
        );

        [Fact]
        public void SingleValueSwitchCase1() => BuildAssert(
            SwitchCase(writeLineTrue, Constant(5)),
            @"case 5:
    Console.WriteLine(true);
    break;",
            @"Case 5
    Console.WriteLine(True)"
);

        [Fact]
        public void MultiValueSwitchCase1() => BuildAssert(
            SwitchCase(writeLineTrue, Constant(5), Constant(6)),
            @"case 5:
case 6:
    Console.WriteLine(true);
    break;",
            @"Case 5, 6
    Console.WriteLine(True)"
        );

        [Fact]
        public void SwitchOnExpressionWithDefault() => BuildAssert(
            Switch(i, Block(
                    typeof(void),
                    Constant(true),
                    Constant(true)
                ), SwitchCase(
                    writeLineTrue,
                    Constant(4)
                ), SwitchCase(
                    writeLineFalse,
                    Constant(5)
                )
            ),
            @"switch (i) {
    case 4:
        Console.WriteLine(true);
        break;
    case 5:
        Console.WriteLine(false);
        break;
    default:
        true;
        true;
}", @"Select Case i
    Case 4
        Console.WriteLine(True)
    Case 5
        Console.WriteLine(False)
    Case Else
        True
        True
End Select"
        );

        [Fact]
        public void SwitchOnMultipleStatementsWithDefault() => BuildAssert(
            Switch(Block(i, j), Block(
                    typeof(void),
                    Constant(true),
                    Constant(true)
                ), SwitchCase(
                    writeLineTrue,
                    Constant(4)
                ), SwitchCase(
                    writeLineFalse,
                    Constant(5)
                )
            ),
            @"switch ({
    i;
    j;
}) {
    case 4:
        Console.WriteLine(true);
        break;
    case 5:
        Console.WriteLine(false);
        break;
    default:
        true;
        true;
}", @"Select Case Block
        i
        j
    End Block
    Case 4
        Console.WriteLine(True)
    Case 5
        Console.WriteLine(False)
    Case Else
        True
        True
End Select"
        );

        [Fact]
        public void SwitchOnExpressionWithoutDefault() => BuildAssert(
            Switch(i, SwitchCase(
                    writeLineTrue,
                    Constant(4)
                ), SwitchCase(
                    writeLineFalse,
                    Constant(5)
                )
            ),
            @"switch (i) {
    case 4:
        Console.WriteLine(true);
        break;
    case 5:
        Console.WriteLine(false);
        break;
}", @"Select Case i
    Case 4
        Console.WriteLine(True)
    Case 5
        Console.WriteLine(False)
End Select"
        );

        [Fact]
        public void SwithOnMultipleStatementsWithoutDefault() => BuildAssert(
            Switch(Block(i, j), SwitchCase(
                    writeLineTrue,
                    Constant(4)
                ), SwitchCase(
                    writeLineFalse,
                    Constant(5)
                )
            ),
            @"switch ({
    i;
    j;
}) {
    case 4:
        Console.WriteLine(true);
        break;
    case 5:
        Console.WriteLine(false);
        break;
}", @"Select Case Block
        i
        j
    End Block
    Case 4
        Console.WriteLine(True)
    Case 5
        Console.WriteLine(False)
End Select"
        );
    }
}
