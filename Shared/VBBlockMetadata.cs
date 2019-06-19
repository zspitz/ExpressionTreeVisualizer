using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionToString {
    internal class VBBlockMetadata {
        internal bool IsInMutiline { get; private set; }
        internal bool ParentIsBlock { get; set; }
        internal static VBBlockMetadata CreateMetadata(bool isInMultiline, bool parentIsBlock) => new VBBlockMetadata {
            IsInMutiline = isInMultiline,
            ParentIsBlock = parentIsBlock
        };
        internal void Deconstruct(out bool isInMultiline, out bool parentIsBlock) {
            isInMultiline = IsInMutiline;
            parentIsBlock = ParentIsBlock;
        }
    }
}
