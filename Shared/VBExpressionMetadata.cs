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
        internal static VBExpressionMetadata CreateMetadata(bool isInMultiline = false, ExpressionType? expressionType = null) => new VBExpressionMetadata {
            IsInMutiline = isInMultiline,
            ExpressionType = expressionType
        };
        internal void Deconstruct(out bool isInMultiline, out ExpressionType? expressionType) {
            isInMultiline = IsInMutiline;
            expressionType = ExpressionType;
        }
    }
}
