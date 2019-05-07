Imports System.Linq.Expressions

Public Class Binary
    <Fact>
    Sub Add()
        Dim x As Double = 0, y As Double = 0
        RunTest(Function() x + y, "() => x + y", "Function() x + y")
    End Sub

    <Fact>
    Sub AddChecked()
        Dim x As Double = 0, y As Double = 0
        RunTest(Function() x + y, "() => x + y", "Function() x + y")
    End Sub

    <Fact>
    Sub Divide()
        Dim x As Double = 0, y As Double = 0
        RunTest(Function() x / y, "() => x / y", "Function() x / y")
    End Sub

    <Fact>
    Sub Modulo()
        Dim x As Double = 0, y As Double = 0
        RunTest(Function() x Mod y, "() => x % y", "Function() x Mod y")
    End Sub

    <Fact>
    Sub Multiply()
        Dim x As Double = 0, y As Double = 0
        RunTest(Function() x * y, "() => x * y", "Function() x * y")
    End Sub

    <Fact>
    Sub MultiplyChecked()
        Dim x As Double = 0, y As Double = 0
        RunTest(Function() x * y, "() => x * y", "Function() x * y")
    End Sub

    <Fact>
    Sub Subtract()
        Dim x As Double = 0, y As Double = 0
        RunTest(Function() x - y, "() => x - y", "Function() x - y")
    End Sub

    <Fact>
    Sub SubtractChecked()
        Dim x As Double = 0, y As Double = 0
        RunTest(Function() x - y, "() => x - y", "Function() x - y")
    End Sub

    <Fact>
    Sub AndBitwise()
        Dim i As Integer = 0, j As Integer = 0
        RunTest(Function() i And j, "() => i & j", "Function() i And j")
    End Sub

    <Fact>
    Sub OrBitwise()
        Dim i As Integer = 0, j As Integer = 0
        RunTest(Function() i Or j, "() => i | j", "Function() i Or j")
    End Sub

    <Fact>
    Sub ExclusiveOrBitwise()
        Dim i As Integer = 0, j As Integer = 0
        RunTest(Function() i Xor j, "() => i ^ j", "Function() i Xor j")
    End Sub

    <Fact>
    Sub AndLogical()
        Dim b1 As Boolean = True, b2 As Boolean = True
        RunTest(Function() b1 And b2, "() => b1 & b2", "Function() b1 And b2")
    End Sub

    <Fact>
    Sub OrLogical()
        Dim b1 As Boolean = True, b2 As Boolean = True
        RunTest(Function() b1 Or b2, "() => b1 | b2", "Function() b1 Or b2")
    End Sub

    <Fact>
    Sub ExclusiveOrLogical()
        Dim b1 As Boolean = True, b2 As Boolean = True
        RunTest(Function() b1 Xor b2, "() => b1 ^ b2", "Function() b1 Xor b2")
    End Sub

    <Fact>
    Sub [AndAlso]()
        Dim b1 As Boolean = True, b2 As Boolean = True
        RunTest(Function() b1 AndAlso b2, "() => b1 && b2", "Function() b1 AndAlso b2")
    End Sub

    <Fact>
    Sub [OrElse]()
        Dim b1 As Boolean = True, b2 As Boolean = True
        RunTest(Function() b1 OrElse b2, "() => b1 || b2", "Function() b1 OrElse b2")
    End Sub

    <Fact>
    Sub Equal()
        Dim i As Integer = 0, j As Integer = 0
        RunTest(Function() i = j, "() => i == j", "Function() i = j")
    End Sub

    <Fact>
    Sub NotEqual()
        Dim i As Integer = 0, j As Integer = 0
        RunTest(Function() i <> j, "() => i != j", "Function() i <> j")
    End Sub

    <Fact>
    Sub GreaterThanOrEqual()
        Dim i As Integer = 0, j As Integer = 0
        RunTest(Function() i >= j, "() => i >= j", "Function() i >= j")
    End Sub

    <Fact>
    Sub GreaterThan()
        Dim i As Integer = 0, j As Integer = 0
        RunTest(Function() i > j, "() => i > j", "Function() i > j")
    End Sub

    <Fact>
    Sub LessThan()
        Dim i As Integer = 0, j As Integer = 0
        RunTest(Function() i < j, "() => i < j", "Function() i < j")
    End Sub

    <Fact>
    Sub LessThanOrEqual()
        Dim i As Integer = 0, j As Integer = 0
        RunTest(Function() i <= j, "() => i <= j", "Function() i <= j")
    End Sub

    <Fact>
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

    <Fact>
    Sub LeftShift()
        Dim i As Integer = 0, j As Integer = 0
        RunTest(Function() i << j, $"() => i << j & {sizeMasks(i.GetType())}", $"Function() i << j And {sizeMasks(i.GetType())}")
    End Sub

    <Fact>
    Sub RightShift()
        Dim i As Integer = 0, j As Integer = 0
        RunTest(Function() i >> j, $"() => i >> j & {sizeMasks(i.GetType())}", $"Function() i >> j And {sizeMasks(i.GetType())}")
    End Sub

    <Fact>
    Sub ArrayIndex()
        Dim arr = New String() {}
        RunTest(Function() arr(0), "() => arr[0]", "Function() arr(0)")
    End Sub

    <Fact>
    Sub Power()
        Dim x As Double = 0, y As Double = 0
        RunTest(Function() x ^ y, "() => Math.Pow(x, y)", "Function() x ^ y")
    End Sub
End Class
