Friend Module Dummy
    Friend Sub DummyMethod()
    End Sub
End Module

Partial Public Class VBCompilerGeneratedBase
    <Fact>
    Sub InstanceMethod0Arguments()
        Dim s = ""
        RunTest(
            Function() s.ToString(),
            "() => s.ToString()",
            "Function() s.ToString"
        )
    End Sub

    <Fact>
    Sub StaticMethod0Arguments()
        RunTest(
            Sub() DummyMethod(),
            "() => Dummy.DummyMethod()",
            "Sub() Dummy.DummyMethod"
        )
    End Sub

    <Fact>
    Sub ExtensionMethod0Arguments()
        Dim lst As IEnumerable(Of String) = New List(Of String)()
        RunTest(
            Function() lst.Distinct,
            "() => lst.Distinct()",
            "Function() lst.Distinct"
        )
    End Sub

    <Fact>
    Sub InstanceMethod1Argument()
        Dim s = ""
        RunTest(
            Function() s.CompareTo(""),
            "() => s.CompareTo("""")",
            "Function() s.CompareTo("""")"
        )
    End Sub

    <Fact>
    Sub StaticMethod1Argument()
        RunTest(
            Function() String.Intern(""),
            "() => string.Intern("""")",
            "Function() String.Intern("""")"
        )
    End Sub

    <Fact>
    Sub ExtensionMethod1Argument()
        Dim lst As IEnumerable(Of String) = New List(Of String)()
        RunTest(
            Function() lst.Take(1),
            "() => lst.Take(1)",
            "Function() lst.Take(1)"
        )
    End Sub

    <Fact>
    Sub InstanceMethod2Arguments()
        Dim s = ""
        RunTest(
            Function() s.Contains("a"c, StringComparer.InvariantCultureIgnoreCase),
            "() => (IEnumerable<char>)s.Contains('a', (IEqualityComparer<char>)StringComparer.InvariantCultureIgnoreCase)",
            "Function() CType(s, IEnumerable(Of Char)).Contains(""a""C, CType(StringComparer.InvariantCultureIgnoreCase, IEqualityComparer(Of Char)))"
        )
    End Sub

    <Fact>
    Sub StaticMethod2Arguments()
        Dim arr As IEnumerable(Of Char) = New Char() {"a"c, "b"c}
        RunTest(
            Function() String.Join(","c, arr),
            "() => string.Join("","", arr)",
            "Function() String.Join("","", arr)"
        )
    End Sub

    <Fact>
    Sub ExtensionMethod2Arguments()
        Dim lst As IEnumerable(Of String) = New List(Of String)()
        Dim comparer As IComparer(Of String) = StringComparer.OrdinalIgnoreCase
        RunTest(
            Function() lst.OrderBy(Function(x) x, comparer),
            "() => lst.OrderBy((string x) => x, comparer)",
            "Function() lst.OrderBy(Function(x As String) x, comparer)"
        )
    End Sub

    <Fact>
    Sub StringConcat()
        RunTest(
            Function(s1 As String, s2 As String) String.Concat(s1, s2),
            "(string s1, string s2) => s1 + s2",
            "Function(s1 As String, s2 As String) s1 + s2"
        )
    End Sub

    <Fact>
    Sub StringConcatOperator()
        RunTest(
            Function(s1 As String, s2 As String) s1 + s2,
            "(string s1, string s2) => s1 + s2",
            "Function(s1 As String, s2 As String) s1 + s2"
        )
    End Sub

    <Fact>
    Sub StringConcatOperatorParamArray()
        RunTest(
            Function(s1 As String, s2 As String) s1 + s2 + s1 + s2 + s1 + s2,
            "(string s1, string s2) => s1 + s2 + s1 + s2 + s1 + s2",
            "Function(s1 As String, s2 As String) s1 + s2 + s1 + s2 + s1 + s2"
        )
    End Sub

    ' This will not compile -- Like operator not supported for current project type
    '<Fact>
    'Sub LikeOperatorStrings()
    '    BuildAssert(
    '        Function(s1 As String, s2 As String) s1 Like s2,
    '        "(string s1, string s2) => LikeOperator.LikeString(s1, s2)",
    '        "Function(s1 As String, s2 As String) s1 Like s2"
    '    )
    'End Sub

    '<Fact>
    'Sub LikeOperatorObjects()
    '    BuildAssert(
    '        Function(o1 As Object, o2 As Object) o1 Like o2,
    '        "(object o1, object o2) => LikeOperator.LikeObject(o1, o2)",
    '        "Function(o1 As Object, o2 As Object) o1 Like o2"
    '    )
    'End Sub
End Class
