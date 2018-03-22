Imports Portal.Components
Imports DevExpress.Web
Imports DevExpress.Data.Filtering
Imports System.IO
Imports DevExpress.Data.Linq
Partial Class Modules_ucDevxNonClosingEnquiry
    Inherits PortalModuleControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If (Not IsPostBack) Then
            Dim _data As New DataClasses_SIBISLWTReportsExt()
            '_data.ExecuteCommand("exec SP_GetNonClosing")
            GridData.ExpandAll()
        End If
    End Sub

    Protected Sub btnExportData_Click(sender As Object, e As EventArgs) Handles btnExportData.Click
        GridExporter.WriteXlsToResponse(Guid.NewGuid().ToString(), False)
    End Sub

End Class
