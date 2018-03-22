Imports Portal.Components
Imports DevExpress.Web.ASPxTreeList
Imports DevExpress.Web

Partial Class Modules_ucDevxDashBoard
    Inherits PortalModuleControl
    Public Function PreviewDB(ByVal DB_ID As String) As String
        Dim sb As New StringBuilder
        If Not String.IsNullOrEmpty(DB_ID) Then

            sb.AppendFormat("<a class=""dxeHyperlink_Office2010Blue"" href=""javascript:ShowDetailPopup('{0}');"" style=""font-weight: bold;"">View</a>", DB_ID)

        End If
        Return sb.ToString()
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
   
    End Sub

    Protected Sub treeList_HtmlDataCellPrepared(sender As Object, e As TreeListHtmlDataCellEventArgs) Handles treeList.HtmlDataCellPrepared
        If e.Column.Name = "View" Then

            


        End If
    End Sub


    Protected Sub cbPreview_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbPreview.Callback
        Dim DB_ID = CInt(e.Parameter)
        Session("DB_ID") = DB_ID
        e.Result = DB_ID
    End Sub

     
End Class
