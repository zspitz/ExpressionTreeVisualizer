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
            "Function() s.ToString",
            "Lambda(
    Call(s,
        typeof(string).GetMethod(""ToString"")
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", Method)>
    Sub StaticMethod0Arguments()
        RunTest(
            Sub() DummyMethod(),
            "() => Dummy.DummyMethod()",
            "Sub() Dummy.DummyMethod",
            "Lambda(
    Call(
        typeof(Dummy).GetMethod(""DummyMethod"")
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", Method)>
    Sub ExtensionMethod0Arguments()
        Dim lst As IEnumerable(Of String) = New List(Of String)()
        RunTest(
            Function() lst.Distinct,
            "() => lst.Distinct()",
            "Function() lst.Distinct",
            "Lambda(
    Call(
        typeof(Enumerable).GetMethod(""Distinct""),
        lst
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", Method)>
    Sub InstanceMethod1Argument()
        Dim s = ""
        RunTest(
            Function() s.CompareTo(""),
            "() => s.CompareTo("""")",
            "Function() s.CompareTo("""")",
            "Lambda(
    Call(s,
        typeof(string).GetMethod(""CompareTo""),
        Constant("""")
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", Method)>
    Sub StaticMethod1Argument()
        RunTest(
            Function() String.Intern(""),
            "() => string.Intern("""")",
            "Function() String.Intern("""")",
            "Lambda(
    Call(
        typeof(string).GetMethod(""Intern""),
        Constant("""")
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", Method)>
    Sub ExtensionMethod1Argument()
        Dim lst As IEnumerable(Of String) = New List(Of String)()
        RunTest(
            Function() lst.Take(1),
            "() => lst.Take(1)",
            "Function() lst.Take(1)",
            "Lambda(
    Call(
        typeof(Enumerable).GetMethod(""Take""),
        lst,
        Constant(1)
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", Method)>
    Sub InstanceMethod2Arguments()
        Dim s = ""
        RunTest(
            Function() s.Contains("a"c, StringComparer.InvariantCultureIgnoreCase),
            "() => (IEnumerable<char>)s.Contains('a', (IEqualityComparer<char>)StringComparer.InvariantCultureIgnoreCase)",
            "Function() CType(s, IEnumerable(Of Char)).Contains(""a""C, CType(StringComparer.InvariantCultureIgnoreCase, IEqualityComparer(Of Char)))",
            "Lambda(
    Call(
        typeof(Enumerable).GetMethod(""Contains""),
        Convert(s,
            typeof(IEnumerable<char>)
        ),
        Constant('a'),
        Convert(
            MakeMemberAccess(null,
                typeof(StringComparer).GetProperty(""InvariantCultureIgnoreCase"")
            ),
            typeof(IEqualityComparer<char>)
        )
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", Method)>
    Sub StaticMethod2Arguments()
        Dim arr As IEnumerable(Of Char) = New Char() {"a"c, "b"c}
        RunTest(
            Function() String.Join(","c, arr),
            "() => string.Join("","", arr)",
            "Function() String.Join("","", arr)",
            "Lambda(
    Call(
        typeof(string).GetMethod(""Join""),
        Constant("",""), arr
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", Method)>
    Sub ExtensionMethod2Arguments()
        Dim lst As IEnumerable(Of String) = New List(Of String)()
        Dim comparer As IComparer(Of String) = StringComparer.OrdinalIgnoreCase
        RunTest(
            Function() lst.OrderBy(Function(x) x, comparer),
            "() => lst.OrderBy((string x) => x, comparer)",
            "Function() lst.OrderBy(Function(x As String) x, comparer)",
            "Lambda(
    Call(
        typeof(Enumerable).GetMethod(""OrderBy""),
        lst,
        Lambda(x,
            var x = Parameter(
                typeof(string),
                ""x""
            )
        ),
        comparer
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", Method)>
    Sub StringConcat()
        RunTest(
            Function(s1 As String, s2 As String) String.Concat(s1, s2),
            "(string s1, string s2) => s1 + s2",
            "Function(s1 As String, s2 As String) s1 + s2",
            "Lambda(
    Call(
        typeof(string).GetMethod(""Concat""),
        s1, s2
    ),
    var s1 = Parameter(
        typeof(string),
        ""s1""
    ),
    var s2 = Parameter(
        typeof(string),
        ""s2""
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", Method)>
    Sub StringConcatOperator()
        RunTest(
            Function(s1 As String, s2 As String) s1 + s2,
            "(string s1, string s2) => s1 + s2",
            "Function(s1 As String, s2 As String) s1 + s2",
            "Lambda(
    Call(
        typeof(string).GetMethod(""Concat""),
        s1, s2
    ),
    var s1 = Parameter(
        typeof(string),
        ""s1""
    ),
    var s2 = Parameter(
        typeof(string),
        ""s2""
    )
)"
        )
    End Sub

    <Fact> <Trait("Category", Method)>
    Sub StringConcatOperatorParamArray()
        RunTest(
            Function(s1 As String, s2 As String) s1 + s2 + s1 + s2 + s1 + s2,
            "(string s1, string s2) => s1 + s2 + s1 + s2 + s1 + s2",
            "Function(s1 As String, s2 As String) s1 + s2 + s1 + s2 + s1 + s2",
            "Lambda(
    Call(
        typeof(string).GetMethod(""Concat""),
        NewArrayInit(
            typeof(string),
            s1, s2, s1, s2, s1, s2
        )
    ),
    var s1 = Parameter(
        typeof(string),
        ""s1""
    ),
    var s2 = Parameter(
        typeof(string),
        ""s2""
    )
)"
        )
    End Sub

    'This will not compile -- Like operator not supported for current project type (of the test project)
    '<Fact> <Trait("Category", Method)>
    'Sub LikeOperatorStrings()
    '    RunTest(
    '        Function(s1 As String, s2 As String) s1 Like s2,
    '        "(string s1, string s2) => LikeOperator.LikeString(s1, s2)",
    '        "Function(s1 As String, s2 As String) s1 Like s2",
    '        ""
    '    )
    'End Sub

    '<Fact> <Trait("Category", Method)>
    'Sub LikeOperatorObjects()
    '    RunTest(
    '        Function(o1 As Object, o2 As Object) o1 Like o2,
    '        "(object o1, object o2) => LikeOperator.LikeObject(o1, o2)",
    '        "Function(o1 As Object, o2 As Object) o1 Like o2",
    '        ""
    '    )
    'End Sub
End Class
