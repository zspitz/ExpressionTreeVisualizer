using System;
using System.Collections.Generic;
using Xunit;
using static ExpressionToString.Util.Functions;

namespace ExpressionToString.Tests {
    [Trait("Type", "FormatStringParsing")]
    public class FormatStringParsing {
        private static void RunTest(string format, List<(string, int?, int?, string)> expected) =>
            Assert.Equal(expected, ParseFormatString(format));

        [Fact]
        public void IndexOnly() => RunTest(
            "0 = {0} 1 = {1} 2 = {2} 3 = {3} 4 = {4}",
            new List<(string, int?, int?, string)> {
                ("0 = ", 0, null, null),
                (" 1 = ", 1, null, null),
                (" 2 = ", 2, null, null),
                (" 3 = ", 3, null, null),
                (" 4 = ", 4, null, null)
            }
        );

        [Fact]
        public void NullFormat() {
            var ex = Assert.Throws<ArgumentNullException>(() => ParseFormatString(null));
            Assert.Equal("Value cannot be null.\r\nParameter name: format", ex.Message);
        }

        [Fact]
        public void NegativeIndex() {
            var ex = Assert.Throws<FormatException>(() => ParseFormatString("{-1}"));
            Assert.Equal("Negative number not allowed", ex.Message);
        }

        [Fact]
        public void ItemFormat() => RunTest(
            "{0:X2}",
            new List<(string, int?, int?, string)> {
                ("", 0, null, "X2")
            }
        );

        [Fact]
        public void EmptyStringItemFormat() => RunTest(
            "{0:}",
            new List<(string, int?, int?, string)> {
                ("", 0, null, "")
            }
        );

        [Fact]
        public void MissingEndBraceEscapedStartBrace() {
            var ex = Assert.Throws<FormatException>(() => ParseFormatString("{0:{{"));
            Assert.Equal("Unexpected end of text", ex.Message);
        }

        [Fact]
        public void MissingEndBraceEscapedEndBrace() {
            var ex = Assert.Throws<FormatException>(() => ParseFormatString("{0:}}"));
            Assert.Equal("Unexpected end of text", ex.Message);
        }
    }
}
