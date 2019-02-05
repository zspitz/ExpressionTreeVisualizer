Public Class Unsorted
    <Fact> Sub ReturnObjectCreation()
        BuildAssert(Function() New Date(1981, 1, 1), "() => new DateTime(1981, 1, 1)", "Function() New Date(1981, 1, 1)")
    End Sub
End Class

