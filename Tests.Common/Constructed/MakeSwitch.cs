using System.Linq.Expressions;
using Xunit;
using static ExpressionToString.Tests.Globals;
using static System.Linq.Expressions.Expression;
using static ExpressionToString.Tests.Categories;

namespace ExpressionToString.Tests {
    public partial class ConstructedBase {
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
        [Trait("Category", SwitchCases)]
       
        public void SingleValueSwitchCase() => RunTest(
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
        [Trait("Category", SwitchCases)]
        public void MultiValueSwitchCase() => RunTest(
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
        [Trait("Category", SwitchCases)]
        public void SingleValueSwitchCase1() => RunTest(
            SwitchCase(writeLineTrue, Constant(5)),
            @"case 5:
    Console.WriteLine(true);
    break;",
            @"Case 5
    Console.WriteLine(True)"
);

        [Fact]
        [Trait("Category", SwitchCases)]
        public void MultiValueSwitchCase1() => RunTest(
            SwitchCase(writeLineTrue, Constant(5), Constant(6)),
            @"case 5:
case 6:
    Console.WriteLine(true);
    break;",
            @"Case 5, 6
    Console.WriteLine(True)"
        );

        [Fact]
        [Trait("Category", SwitchCases)]
        public void SwitchOnExpressionWithDefaultSingleStatement() => RunTest(
            Switch(i, Empty(), SwitchCase(
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
        default(void);
}", @"Select Case i
    Case 4
        Console.WriteLine(True)
    Case 5
        Console.WriteLine(False)
    Case Else
        CType(Nothing, Void)
End Select"
        );

        [Fact]
        [Trait("Category", SwitchCases)]
        public void SwitchOnExpressionWithDefaultMultiStatement() => RunTest(
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
        [Trait("Category", SwitchCases)]
        public void SwitchOnMultipleStatementsWithDefault() => RunTest(
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
        [Trait("Category", SwitchCases)]
        public void SwitchOnExpressionWithoutDefault() => RunTest(
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
        [Trait("Category", SwitchCases)]
        public void SwithOnMultipleStatementsWithoutDefault() => RunTest(
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
