using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionToString {
    internal class VBExpressionMetadata {
        internal bool IsInMutiline { get; private set; } = false;
        internal ExpressionType? ExpressionType { get; private set; } = null;
        internal bool ReturnBkock { get; private set; } = false;
        internal static VBExpressionMetadata CreateMetadata(bool isInMultiline = false, ExpressionType? expressionType = null, bool returnBlock = false) => new VBExpressionMetadata {
            IsInMutiline = isInMultiline,
            ExpressionType = expressionType,
            ReturnBkock = returnBlock
        };
        internal void Deconstruct(out bool isInMultiline, out ExpressionType? expressionType, out bool returnBlock) {
            isInMultiline = IsInMutiline;
            expressionType = ExpressionType;
            returnBlock = ReturnBkock;
        }
    }
}
