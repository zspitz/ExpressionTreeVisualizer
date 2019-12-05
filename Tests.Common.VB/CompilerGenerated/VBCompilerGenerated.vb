<Trait("Source", "VBCompilerGenerated")>
<Trait("Type", "Expression object")> ' TODO ideally this would be on the base class, but https://github.com/xunit/xunit/issues/1397
<Obsolete>
Public MustInherit Class VBCompilerGeneratedBase
    Inherits TestsBase

    Shared Sub New()
        VB.Load()
    End Sub

    <Obsolete>
    Protected Overrides Function GetObjectSource() As String
        Return "VBCompiler"
    End Function
End Class
