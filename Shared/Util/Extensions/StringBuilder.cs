using System.Text;

namespace ExpressionToString.Util {
    public static class StringBuilderExtensions {
        //https://stackoverflow.com/a/24769702/111794
        public static StringBuilder TrimEnd(this StringBuilder sb, bool trimEOL = true) {
            if (sb.Length == 0) return sb;

            int i = sb.Length - 1;
            for (; i >= 0; i--) {
                var c = sb[i];
                if (!char.IsWhiteSpace(c)) {
                    break;
                }
                if (!trimEOL && (c == '\n' || c == '\r')) {
                    break;
                }
            }

            if (i < sb.Length - 1)
                sb.Length = i + 1;

            return sb;
        }
    }
}
