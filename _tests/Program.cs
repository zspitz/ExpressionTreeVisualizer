using ExpressionTreeTransform;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Linq;
using System.Linq.Expressions;
using static Microsoft.CodeAnalysis.LanguageNames;
using Microsoft.CodeAnalysis.VisualBasic;
using Microsoft.CodeAnalysis.VisualBasic.Syntax;

namespace _tests {
    class Program {
        private static SyntaxNode expressionNode(string fieldExpression) => CSharpSyntaxTree.ParseText($@"
using System;
using System.Linq.Expressions;
class Class1 {{
    var exor = {fieldExpression};
}}
").GetRoot().DescendantNodes().OfType<EqualsValueClauseSyntax>().First().ChildNodes().First();

        static void Main(string[] args) {
            var mapper = new Mapper();
            Expression<Func<bool>> expr = () => true;
            //var mapped = mapper.GetSyntaxNode(expr, CSharp);

            //var root = expressionNode("() => true");

            var mapped = mapper.GetSyntaxNode(expr, VisualBasic, out var expressionIDs);
            var root = VisualBasicSyntaxTree.ParseText("Dim expr = Function() True").GetRoot();
            var node = root.DescendantNodes().OfType<EqualsValueSyntax>().First().ChildNodes().First();
            Console.WriteLine(mapped.IsEquivalentTo(node, false));
            Console.WriteLine(node.IsEquivalentTo(mapped, false));

            //Console.WriteLine(mapped.ToFullString());
            //mapped = mapped.NormalizeWhitespace("    ", true);
            //Console.WriteLine(mapped.ToFullString());
            //Console.ReadKey(true);

            //var tree = CSharpSyntaxTree.ParseText("() => true");
            //foreach (var d in tree.GetDiagnostics()) {
            //    Console.WriteLine(d);
            //}

            //tree = VisualBasicSyntaxTree.ParseText("Function() True");
            //foreach (var d in tree.GetDiagnostics()) {
            //    Console.WriteLine(d);
            //}

            Console.ReadKey(true);
        }
    }
}
