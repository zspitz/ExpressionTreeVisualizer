Public Class Binary
    <Fact>
    Public Sub Add()
        Dim x As Double = 0, y As Double = 0
        BuildAssert(Function() x + y, "() => x + y", "Function() x + y")
    End Sub

    <Fact>
    Public Sub AddChecked()
        Dim x As Double = 0, y As Double = 0
        BuildAssert(Function() x + y, "() => x + y", "Function() x + y")
    End Sub

    <Fact>
    Public Sub Divide()
        Dim x As Double = 0, y As Double = 0
        BuildAssert(Function() x / y, "() => x / y", "Function() x / y")
    End Sub

    <Fact>
    Public Sub Modulo()
        Dim x As Double = 0, y As Double = 0
        BuildAssert(Function() x Mod y, "() => x % y", "Function() x Mod y")
    End Sub

    <Fact>
    Public Sub Multiply()
        Dim x As Double = 0, y As Double = 0
        BuildAssert(Function() x * y, "() => x * y", "Function() x * y")
    End Sub

    <Fact>
    Public Sub MultiplyChecked()
        Dim x As Double = 0, y As Double = 0
        BuildAssert(Function() x * y, "() => x * y", "Function() x * y")
    End Sub

    <Fact>
    Public Sub Subtract()
        Dim x As Double = 0, y As Double = 0
        BuildAssert(Function() x - y, "() => x - y", "Function() x - y")
    End Sub

    <Fact>
    Public Sub SubtractChecked()
        Dim x As Double = 0, y As Double = 0
        BuildAssert(Function() x - y, "() => x - y", "Function() x - y")
    End Sub

    <Fact>
    Public Sub AndBitwise()
        Dim i As Integer = 0, j As Integer = 0
        BuildAssert(Function() i And j, "() => i & j", "Function() i And j")
    End Sub

    <Fact>
    Public Sub OrBitwise()
        Dim i As Integer = 0, j As Integer = 0
        BuildAssert(Function() i Or j, "() => i | j", "Function() i Or j")
    End Sub

    <Fact>
    Public Sub ExclusiveOrBitwise()
        Dim i As Integer = 0, j As Integer = 0
        BuildAssert(Function() i Xor j, "() => i ^ j", "Function() i Xor j")
    End Sub

    <Fact>
    Public Sub AndLogical()
        Dim b1 As Boolean = True, b2 As Boolean = True
        BuildAssert(Function() b1 And b2, "() => b1 & b2", "Function() b1 And b2")
    End Sub

    <Fact>
    Public Sub OrLogical()
        Dim b1 As Boolean = True, b2 As Boolean = True
        BuildAssert(Function() b1 Or b2, "() => b1 | b2", "Function() b1 Or b2")
    End Sub

    <Fact>
    Public Sub ExclusiveOrLogical()
        Dim b1 As Boolean = True, b2 As Boolean = True
        BuildAssert(Function() b1 Xor b2, "() => b1 ^ b2", "Function() b1 Xor b2")
    End Sub

    <Fact>
    Public Sub [AndAlso]()
        Dim b1 As Boolean = True, b2 As Boolean = True
        BuildAssert(Function() b1 AndAlso b2, "() => b1 && b2", "Function() b1 AndAlso b2")
    End Sub

    <Fact>
    Public Sub [OrElse]()
        Dim b1 As Boolean = True, b2 As Boolean = True
        BuildAssert(Function() b1 OrElse b2, "() => b1 || b2", "Function() b1 OrElse b2")
    End Sub

    <Fact>
    Public Sub Equal()
        Dim i As Integer = 0, j As Integer = 0
        BuildAssert(Function() i = j, "() => i == j", "Function() i = j")
    End Sub

    <Fact>
    Public Sub NotEqual()
        Dim i As Integer = 0, j As Integer = 0
        BuildAssert(Function() i <> j, "() => i != j", "Function() i <> j")
    End Sub

    <Fact>
    Public Sub GreaterThanOrEqual()
        Dim i As Integer = 0, j As Integer = 0
        BuildAssert(Function() i >= j, "() => i >= j", "Function() i >= j")
    End Sub

    <Fact>
    Public Sub GreaterThan()
        Dim i As Integer = 0, j As Integer = 0
        BuildAssert(Function() i > j, "() => i > j", "Function() i > j")
    End Sub

    <Fact>
    Public Sub LessThan()
        Dim i As Integer = 0, j As Integer = 0
        BuildAssert(Function() i < j, "() => i < j", "Function() i < j")
    End Sub

    <Fact>
    Public Sub LessThanOrEqual()
        Dim i As Integer = 0, j As Integer = 0
        BuildAssert(Function() i <= j, "() => i <= j", "Function() i <= j")
    End Sub

    <Fact>
    Public Sub Coalesce()
        Dim s1 As String = Nothing, s2 As String = Nothing
        BuildAssert(Function() If(s1, s2), "() => s1 ?? s2", "Function() If(s1, s2)")
    End Sub

    <Fact>
    Public Sub LeftShift()
        Dim i As Integer = 0, j As Integer = 0
        BuildAssert(Function() i << j, "() => i << j", "Function() i << j")
    End Sub

    <Fact>
    Public Sub RightShift()
        Dim i As Integer = 0, j As Integer = 0
        BuildAssert(Function() i >> j, "() => i >> j", "Function() i >> j")
    End Sub

    <Fact> Public Sub ArrayIndex()
        Dim arr = New String() {}
        BuildAssert(Function() arr(0), "() => arr[0]", "Function() arr(0)")
    End Sub

    <Fact> Public Sub Power()
        Dim x As Double = 0, y As Double = 0
        BuildAssert(Function() x ^ y, "() => Math.Pow(x, y)", "Function() x ^ y")
    End Sub
End Class
