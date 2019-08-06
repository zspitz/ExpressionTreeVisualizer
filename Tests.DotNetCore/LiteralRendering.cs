using System;
using System.Collections.Generic;
using Xunit;
using static ExpressionToString.Util.Functions;
using ExpressionToString.Util;
using static ExpressionToString.FormatterNames;

namespace Tests.DotNetCore {
    [Trait("Type", "Literal rendering")]
    public class LiteralRendering {
        [Theory]
        [MemberData(nameof(TestData))]
        public static void TestLiteral(object o, string language, string expected) {
            var (actualHasLiteral, actual) = TryRenderLiteral(o, language);
            var expectedHasLiteral = !expected.StartsWith("#") || expected.EndsWith("#");
            Assert.Equal(expectedHasLiteral, actualHasLiteral);
            Assert.Equal(expected, actual);
        }

       public static TheoryData<object, string, string> TestData;
        static LiteralRendering() {
            var testData = new List<(object, (string neutral, string csharp, string vb))>() {
                { null, ("␀", "null", "Nothing") },
                {5, ("5","5","5") },
                {17.2, ("17.2","17.2","17.2") },
                {true, ("True","true","True") },
                {false, ("False","false","False") },
                {'a', ("#Char","'a'","\"a\"C") },
                { "abcd", ("\"abcd\"", "\"abcd\"", "\"abcd\"") },
                { "ab\rcd", ("#String", "\"ab\\rcd\"", "\"ab\rcd\"") },
                { DayOfWeek.Thursday, ("DayOfWeek.Thursday","DayOfWeek.Thursday","DayOfWeek.Thursday") },
                { new object[] {1}, ("#Object[]", "new[] { 1 }", "{ 1 }")},
                {Tuple.Create(1,"2"), ("(1, \"2\")", "(1, \"2\")", "(1, \"2\")") },
                {(1,"2"), ("(1, \"2\")", "(1, \"2\")", "(1, \"2\")") },
            };

            var timerType = typeof(System.Timers.Timer);

            // populate with reflection test data
            new List<(object, (string csharp, string vb))>() {
                {typeof(string), ("typeof(string)", "GetType(String)") },
                {typeof(string).MakeByRefType(), ("typeof(string).MakeByRef()", "GetType(String).MakeByRef()") },
                {timerType.GetConstructor(new Type[] { }), ("typeof(Timer).GetConstructor()", "GetType(Timer).GetConstructor()") },
                {timerType.GetEvent("Elapsed"), ("typeof(Timer).GetEvent(\"Elapsed\")", "GetType(Timer).GetEvent(\"Elapsed\")")},
                {typeof(string).GetField("Empty"), ("typeof(string).GetField(\"Empty\")", "GetType(String).GetField(\"Empty\")") },
                { GetMethod(() => Console.WriteLine()), ("typeof(Console).GetMethod(\"WriteLine\")", "GetType(Console).GetMethod(\"WriteLine\")") },
                {GetMember(() => "".Length), ("typeof(string).GetProperty(\"Length\")", "GetType(String).GetProperty(\"Length\")") }
            }.SelectT((o, x) => {
                var (csharp, vb) = x;
                return (o, ($"#{o.GetType().Name}", csharp, vb));
            }).AddRangeTo(testData);

            var dte = new DateTime(1981, 1, 1);
            testData.Add(dte, ("#DateTime", "#DateTime", $"#{dte.ToString()}#"));

            var ts = new TimeSpan(5, 4, 3, 2, 1);
            testData.Add(ts, ("#TimeSpan", "#TimeSpan", $"#{ts.ToString()}#"));

            TestData = new TheoryData<object, string, string>();
            foreach (var (o, expected) in testData) {
                var (neutral, csharp, vb) = expected;
                TestData.Add(o, "", neutral);
                TestData.Add(o, CSharp, csharp);
                TestData.Add(o, VisualBasic, vb);
            }
        }
    }
}
