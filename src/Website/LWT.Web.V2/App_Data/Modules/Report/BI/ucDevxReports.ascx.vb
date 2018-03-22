Imports Portal.Components
Imports DevExpress.Web.ASPxTreeList
Imports DevExpress.Web
Imports System.Data.SqlClient
Imports System.Data

Partial Class Modules_ucDevxReports
    Inherits PortalModuleControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        If (Not IsPostBack) Then
            Session("RID") = Nothing
 
        End If 
    End Sub
 
    Protected Sub cbPreview_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbPreview.Callback

        Dim RID = CInt(e.Parameter)
        Session("RID") = RID
        e.Result = RID

    End Sub

End Class
