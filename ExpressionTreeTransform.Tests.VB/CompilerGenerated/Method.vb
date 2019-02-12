Friend Module Dummy
    Friend Sub DummyMethod()
    End Sub
End Module

Public Class Method
    <Fact> Public Sub InstanceMethod0Arguments()
        Dim s = ""
        BuildAssert(
            Function() s.ToString(),
            "() => s.ToString()",
            "Function() s.ToString"
        )
    End Sub

    <Fact> Public Sub StaticMethod0Arguments()
        BuildAssert(
            Sub() DummyMethod(),
            "() => Dummy.DummyMethod()",
            "Sub() Dummy.DummyMethod"
        )
    End Sub

    <Fact> Public Sub ExtensionMethod0Arguments()
        Dim lst As IEnumerable(Of String) = New List(Of String)()
        BuildAssert(
            Function() lst.Distinct,
            "() => lst.Distinct()",
            "Function() lst.Distinct"
        )
    End Sub

    <Fact> Public Sub InstanceMethod1Argument()
        Dim s = ""
        BuildAssert(
            Function() s.CompareTo(""),
            "() => s.CompareTo("""")",
            "Function() s.CompareTo("""")"
        )
    End Sub

    <Fact> Public Sub StaticMethod1Argument()
        BuildAssert(
            Function() String.Intern(""),
            "() => string.Intern("""")",
            "Function() String.Intern("""")"
        )
    End Sub

    <Fact> Public Sub ExtensionMethod1Argument()
        Dim lst As IEnumerable(Of String) = New List(Of String)()
        BuildAssert(
            Function() lst.Take(1),
            "() => lst.Take(1)",
            "Function() lst.Take(1)"
        )
    End Sub

    <Fact> Public Sub InstanceMethod2Arguments()
        Dim s = ""
        BuildAssert(
            Function() s.Contains("a"c, StringComparison.InvariantCultureIgnoreCase),
            "() => s.Contains('a', StringComparison.InvariantCultureIgnoreCase)",
            "Function() s.Contains(""a""C, StringComparison.InvariantCultureIgnoreCase)"
        )
    End Sub

    <Fact> Public Sub StaticMethod2Arguments()
        Dim arr As IEnumerable(Of Char) = New Char() {"a"c, "b"c}
        BuildAssert(
            Function() String.Join(","c, arr),
            "() => string.Join(',', arr)",
            "Function() String.Join("",""C, arr)"
        )
    End Sub

    <Fact> Public Sub ExtensionMethod2Arguments()
        Dim lst As IEnumerable(Of String) = New List(Of String)()
        Dim comparer As IComparer(Of String) = StringComparer.OrdinalIgnoreCase
        BuildAssert(
            Function() lst.OrderBy(Function(x) x, comparer),
            "() => lst.OrderBy((string x) => x, comparer)",
            "Function() lst.OrderBy(Function(x As String) x, comparer)"
        )
    End Sub

    <Fact> Public Sub StringConcat()
        BuildAssert(
            Function(s1 As String, s2 As String) String.Concat(s1, s2),
            "(string s1, string s2) => s1 + s2",
            "Function(s1 As String, s2 As String) s1 + s2"
        )
    End Sub

    <Fact> Public Sub StringConcatOperator()
        BuildAssert(
            Function(s1 As String, s2 As String) s1 + s2,
            "(string s1, string s2) => s1 + s2",
            "Function(s1 As String, s2 As String) s1 + s2"
        )
    End Sub

    <Fact> Public Sub StringConcatOperatorParamArray()
        BuildAssert(
            Function(s1 As String, s2 As String) s1 + s2 + s1 + s2 + s1 + s2,
            "(string s1, string s2) => s1 + s2 + s1 + s2 + s1 + s2",
            "Function(s1 As String, s2 As String) s1 + s2 + s1 + s2 + s1 + s2"
        )
    End Sub
End Class
