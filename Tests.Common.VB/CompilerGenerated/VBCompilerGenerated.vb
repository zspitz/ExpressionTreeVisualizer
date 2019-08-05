<Trait("Source", "VBCompilerGenerated")>
<Trait("Type", "Expression object")> ' TODO ideally this would be on the base class, but https://github.com/xunit/xunit/issues/1397
Public MustInherit Class VBCompilerGeneratedBase
    Inherits TestsBase

    Protected Overrides Function GetObjectContainerType() As Type
        If TypeOf Me Is VBCompilerGeneratedBase Then Return GetType(Objects.VBCompiler)
        Return MyBase.GetObjectContainerType()
    End Function
End Class
