Imports System
Imports System.IO
Imports System.IO.Compression
Imports System.Collections
Imports System.ComponentModel
Imports System.Web.UI
Imports System.Configuration
Imports System.Threading
Imports System.Globalization
Imports System.Text
Imports Portal.Components

Public MustInherit Class BasePage
    Inherits System.Web.UI.Page

    Public Shared _CompareQTCommand As String

    Private _formatter As New ObjectStateFormatter()
    Private CompressViewState As New CompressionViewState

    Protected Overloads Overrides Sub SavePageStateToPersistenceMedium(ByVal viewState As Object)
        Dim ms As New MemoryStream()
        _formatter.Serialize(ms, viewState)
        Dim viewStateArray As Byte() = ms.ToArray()
        ClientScript.RegisterHiddenField("__COMPRESSEDVIEWSTATE", Convert.ToBase64String(CompressViewState.Compress(viewStateArray)))
    End Sub
    Protected Overloads Overrides Function LoadPageStateFromPersistenceMedium() As Object
        Dim vsString As String = Request.Form("__COMPRESSEDVIEWSTATE")
        Dim bytes As Byte() = Convert.FromBase64String(vsString)
        bytes = CompressViewState.Decompress(bytes)
        Return _formatter.Deserialize(Convert.ToBase64String(bytes))
    End Function

End Class
