Friend Module Dummy
    Friend Sub DummyMethod()
    End Sub
End Module

Partial Public Class VBCompilerGeneratedBase
    <Fact> <Trait("Category", Method)>
    Sub InstanceMethod0Arguments()
        Dim s = ""
        RunTest(
            Function() s.ToString(),
            "() => s.ToString()",
            "Function() s.ToString"
        )
    End Sub

    <Fact> <Trait("Category", Method)>
    Sub StaticMethod0Arguments()
        RunTest(
            Sub() DummyMethod(),
            "() => Dummy.DummyMethod()",
            "Sub() Dummy.DummyMethod"
        )
    End Sub

    <Fact> <Trait("Category", Method)>
    Sub ExtensionMethod0Arguments()
        Dim lst As IEnumerable(Of String) = New List(Of String)()
        RunTest(
            Function() lst.Distinct,
            "() => lst.Distinct()",
            "Function() lst.Distinct"
        )
    End Sub

    <Fact> <Trait("Category", Method)>
    Sub InstanceMethod1Argument()
        Dim s = ""
        RunTest(
            Function() s.CompareTo(""),
            "() => s.CompareTo("""")",
            "Function() s.CompareTo("""")"
        )
    End Sub

    <Fact> <Trait("Category", Method)>
    Sub StaticMethod1Argument()
        RunTest(
            Function() String.Intern(""),
            "() => string.Intern("""")",
            "Function() String.Intern("""")"
        )
    End Sub

    <Fact> <Trait("Category", Method)>
    Sub ExtensionMethod1Argument()
        Dim lst As IEnumerable(Of String) = New List(Of String)()
        RunTest(
            Function() lst.Take(1),
            "() => lst.Take(1)",
            "Function() lst.Take(1)"
        )
    End Sub

    <Fact> <Trait("Category", Method)>
    Sub InstanceMethod2Arguments()
        Dim s = ""
        RunTest(
            Function() s.Contains("a"c, StringComparer.InvariantCultureIgnoreCase),
            "() => (IEnumerable<char>)s.Contains('a', (IEqualityComparer<char>)StringComparer.InvariantCultureIgnoreCase)",
            "Function() CType(s, IEnumerable(Of Char)).Contains(""a""C, CType(StringComparer.InvariantCultureIgnoreCase, IEqualityComparer(Of Char)))"
        )
    End Sub

    <Fact> <Trait("Category", Method)>
    Sub StaticMethod2Arguments()
        Dim arr As IEnumerable(Of Char) = New Char() {"a"c, "b"c}
        RunTest(
            Function() String.Join(","c, arr),
            "() => string.Join("","", arr)",
            "Function() String.Join("","", arr)"
        )
    End Sub

    <Fact> <Trait("Category", Method)>
    Sub ExtensionMethod2Arguments()
        Dim lst As IEnumerable(Of String) = New List(Of String)()
        Dim comparer As IComparer(Of String) = StringComparer.OrdinalIgnoreCase
        RunTest(
            Function() lst.OrderBy(Function(x) x, comparer),
            "() => lst.OrderBy((string x) => x, comparer)",
            "Function() lst.OrderBy(Function(x As String) x, comparer)"
        )
    End Sub

    <Fact> <Trait("Category", Method)>
    Sub StringConcat()
        RunTest(
            Function(s1 As String, s2 As String) String.Concat(s1, s2),
            "(string s1, string s2) => s1 + s2",
            "Function(s1 As String, s2 As String) s1 + s2"
        )
    End Sub

    <Fact> <Trait("Category", Method)>
    Sub StringConcatOperator()
        RunTest(
            Function(s1 As String, s2 As String) s1 + s2,
            "(string s1, string s2) => s1 + s2",
            "Function(s1 As String, s2 As String) s1 + s2"
        )
    End Sub

    <Fact> <Trait("Category", Method)>
    Sub StringConcatOperatorParamArray()
        RunTest(
            Function(s1 As String, s2 As String) s1 + s2 + s1 + s2 + s1 + s2,
            "(string s1, string s2) => s1 + s2 + s1 + s2 + s1 + s2",
            "Function(s1 As String, s2 As String) s1 + s2 + s1 + s2 + s1 + s2"
        )
    End Sub

    ' This will not compile -- Like operator not supported for current project type
    '<Fact> <Trait("Category", Method)>
    'Sub LikeOperatorStrings()
    '    BuildAssert(
    '        Function(s1 As String, s2 As String) s1 Like s2,
    '        "(string s1, string s2) => LikeOperator.LikeString(s1, s2)",
    '        "Function(s1 As String, s2 As String) s1 Like s2"
    '    )
    'End Sub

    '<Fact> <Trait("Category", Method)>
    'Sub LikeOperatorObjects()
    '    BuildAssert(
    '        Function(o1 As Object, o2 As Object) o1 Like o2,
    '        "(object o1, object o2) => LikeOperator.LikeObject(o1, o2)",
    '        "Function(o1 As Object, o2 As Object) o1 Like o2"
    '    )
    'End Sub
End Class
