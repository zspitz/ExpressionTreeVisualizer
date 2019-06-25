using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ExpressionToString.Util {
    public static class MatchExtensions {
        public static void Deconstruct(this Match match, out string item1, out string item2) {
            item1 = match.Groups[1].Value;
            item2 = match.Groups[2].Value;
        }
    }
}
