using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;
using System.Linq.Expressions;
using Xunit;
using static Microsoft.CodeAnalysis.LanguageNames;

namespace ExpressionTreeTransform.Tests {
    public class CompilerGenerated {
        private readonly Mapper _mapper = new Mapper();

        private void buildAssert<T>(Expression<Func<T>>expr, string stringExpression) {
            var mapped = _mapper.GetSyntaxNode(expr, CSharp);
            var node = CSharpSyntaxTree.ParseText($@"
using System;
using System.Linq.Expressions;
class Class1 {{
    void method1() {{
        var expr = {stringExpression};
    }}
}}
").GetRoot().DescendantNodes().OfType<EqualsValueClauseSyntax>().First().ChildNodes().First();

            Assert.True(node.GetDiagnostics().None());
            Assert.True(mapped.IsEquivalentTo(node, false)); //Not sure why we need false here
        }

        [Fact]
        public void ReturnBooleanTruel() => buildAssert(() => true, "() => true");

        [Fact]
        public void ReturnBooleanFalse() => buildAssert(() => false, "() => false");

    }
}
