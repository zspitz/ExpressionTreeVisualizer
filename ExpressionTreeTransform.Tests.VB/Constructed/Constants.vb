Public Class Constants
    <Fact> Sub Random()
        BuildAssert(Constant(New Random), "#Random", "#Random")
    End Sub
End Class
