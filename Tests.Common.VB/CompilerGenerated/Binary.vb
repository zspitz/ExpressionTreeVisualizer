Partial Public Class VBCompilerGeneratedBase
    <Fact> <Trait("Category", Binary)>
    Sub Add()
        Dim x As Double = 0, y As Double = 0
        RunTest(Function() x + y, "() => x + y", "Function() x + y")
    End Sub

    <Fact> <Trait("Category", Binary)>
    Sub AddChecked()
        Dim x As Double = 0, y As Double = 0
        RunTest(Function() x + y, "() => x + y", "Function() x + y")
    End Sub

    <Fact> <Trait("Category", Binary)>
    Sub Divide()
        Dim x As Double = 0, y As Double = 0
        RunTest(Function() x / y, "() => x / y", "Function() x / y")
    End Sub

    <Fact> <Trait("Category", Binary)>
    Sub Modulo()
        Dim x As Double = 0, y As Double = 0
        RunTest(Function() x Mod y, "() => x % y", "Function() x Mod y")
    End Sub

    <Fact> <Trait("Category", Binary)>
    Sub Multiply()
        Dim x As Double = 0, y As Double = 0
        RunTest(Function() x * y, "() => x * y", "Function() x * y")
    End Sub

    <Fact> <Trait("Category", Binary)>
    Sub MultiplyChecked()
        Dim x As Double = 0, y As Double = 0
        RunTest(Function() x * y, "() => x * y", "Function() x * y")
    End Sub

    <Fact> <Trait("Category", Binary)>
    Sub Subtract()
        Dim x As Double = 0, y As Double = 0
        RunTest(Function() x - y, "() => x - y", "Function() x - y")
    End Sub

    <Fact> <Trait("Category", Binary)>
    Sub SubtractChecked()
        Dim x As Double = 0, y As Double = 0
        RunTest(Function() x - y, "() => x - y", "Function() x - y")
    End Sub

    <Fact> <Trait("Category", Binary)>
    Sub AndBitwise()
        Dim i As Integer = 0, j As Integer = 0
        RunTest(Function() i And j, "() => i & j", "Function() i And j")
    End Sub

    <Fact> <Trait("Category", Binary)>
    Sub OrBitwise()
        Dim i As Integer = 0, j As Integer = 0
        RunTest(Function() i Or j, "() => i | j", "Function() i Or j")
    End Sub

    <Fact> <Trait("Category", Binary)>
    Sub ExclusiveOrBitwise()
        Dim i As Integer = 0, j As Integer = 0
        RunTest(Function() i Xor j, "() => i ^ j", "Function() i Xor j")
    End Sub

    <Fact> <Trait("Category", Binary)>
    Sub AndLogical()
        Dim b1 As Boolean = True, b2 As Boolean = True
        RunTest(Function() b1 And b2, "() => b1 & b2", "Function() b1 And b2")
    End Sub

    <Fact> <Trait("Category", Binary)>
    Sub OrLogical()
        Dim b1 As Boolean = True, b2 As Boolean = True
        RunTest(Function() b1 Or b2, "() => b1 | b2", "Function() b1 Or b2")
    End Sub

    <Fact> <Trait("Category", Binary)>
    Sub ExclusiveOrLogical()
        Dim b1 As Boolean = True, b2 As Boolean = True
        RunTest(Function() b1 Xor b2, "() => b1 ^ b2", "Function() b1 Xor b2")
    End Sub

    <Fact> <Trait("Category", Binary)>
    Sub [AndAlso]()
        Dim b1 As Boolean = True, b2 As Boolean = True
        RunTest(Function() b1 AndAlso b2, "() => b1 && b2", "Function() b1 AndAlso b2")
    End Sub

    <Fact> <Trait("Category", Binary)>
    Sub [OrElse]()
        Dim b1 As Boolean = True, b2 As Boolean = True
        RunTest(Function() b1 OrElse b2, "() => b1 || b2", "Function() b1 OrElse b2")
    End Sub

    <Fact> <Trait("Category", Binary)>
    Sub Equal()
        Dim i As Integer = 0, j As Integer = 0
        RunTest(Function() i = j, "() => i == j", "Function() i = j")
    End Sub

    <Fact> <Trait("Category", Binary)>
    Sub NotEqual()
        Dim i As Integer = 0, j As Integer = 0
        RunTest(Function() i <> j, "() => i != j", "Function() i <> j")
    End Sub

    <Fact> <Trait("Category", Binary)>
    Sub GreaterThanOrEqual()
        Dim i As Integer = 0, j As Integer = 0
        RunTest(Function() i >= j, "() => i >= j", "Function() i >= j")
    End Sub

    <Fact> <Trait("Category", Binary)>
    Sub GreaterThan()
        Dim i As Integer = 0, j As Integer = 0
        RunTest(Function() i > j, "() => i > j", "Function() i > j")
    End Sub

    <Fact> <Trait("Category", Binary)>
    Sub LessThan()
        Dim i As Integer = 0, j As Integer = 0
        RunTest(Function() i < j, "() => i < j", "Function() i < j")
    End Sub

    <Fact> <Trait("Category", Binary)>
    Sub LessThanOrEqual()
        Dim i As Integer = 0, j As Integer = 0
        RunTest(Function() i <= j, "() => i <= j", "Function() i <= j")
    End Sub

    <Fact> <Trait("Category", Binary)>
    Sub Coalesce()
        Dim s1 As String = Nothing, s2 As String = Nothing
        RunTest(Function() If(s1, s2), "() => s1 ?? s2", "Function() If(s1, s2)")
    End Sub

    ' https://docs.microsoft.com/en-us/dotnet/visual-basic/language-reference/operators/left-shift-operator#remarks
    Shared sizeMasks As New Dictionary(Of Type, Integer) From {
        {GetType(SByte), 7},
        {GetType(Byte), 7},
        {GetType(Short), 15},
        {GetType(UShort), 15},
        {GetType(Integer), 31},
        {GetType(UInteger), 31},
        {GetType(Long), 63},
        {GetType(ULong), 63}
    }

    <Fact> <Trait("Category", Binary)>
    Sub LeftShift()
        Dim i As Integer = 0, j As Integer = 0
        RunTest(Function() i << j, $"() => i << j & {sizeMasks(i.GetType())}", $"Function() i << j And {sizeMasks(i.GetType())}")
    End Sub

    <Fact> <Trait("Category", Binary)>
    Sub RightShift()
        Dim i As Integer = 0, j As Integer = 0
        RunTest(Function() i >> j, $"() => i >> j & {sizeMasks(i.GetType())}", $"Function() i >> j And {sizeMasks(i.GetType())}")
    End Sub

    <Fact> <Trait("Category", Binary)>
    Sub ArrayIndex()
        Dim arr = New String() {}
        RunTest(Function() arr(0), "() => arr[0]", "Function() arr(0)")
    End Sub

    <Fact> <Trait("Category", Binary)>
    Sub Power()
        Dim x As Double = 0, y As Double = 0
        RunTest(Function() x ^ y, "() => Math.Pow(x, y)", "Function() x ^ y")
    End Sub
End Class
