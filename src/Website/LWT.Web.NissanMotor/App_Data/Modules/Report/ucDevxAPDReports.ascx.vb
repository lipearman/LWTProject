Imports Portal.Components
Imports DevExpress.Web.ASPxTreeList
Imports DevExpress.Web
Imports System.Data.SqlClient
Imports System.Data

Partial Class Modules_ucDevxAPDReports
    Inherits PortalModuleControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(LWT.Website.webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If
        If (Not IsPostBack) Then
            Session("RID") = Nothing

        End If
    End Sub

    Protected Sub cbPreview_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbPreview.Callback

        Dim RID = CInt(e.Parameter)
        Session("RID") = RID
        e.Result = RID

    End Sub


    'Protected Sub ASPxGridView1_HtmlFooterCellPrepared(sender As Object, e As ASPxGridViewTableFooterCellEventArgs)
    '    If e.Column.Name = "RID" Then
    '        e.Cell.HorizontalAlign = HorizontalAlign.Left
    '    End If
    '    'If e.Column.Name = "CategoryName" Then
    '    '    e.Cell.HorizontalAlign = HorizontalAlign.Justify
    '    'End If
    'End Sub
    'Protected Sub treeList_HtmlRowPrepared(ByVal sender As Object, ByVal e As TreeListHtmlRowEventArgs) Handles treeList.HtmlRowPrepared


    '    'e.Row.Cells(3).HorizontalAlign = HorizontalAlign.Left
    '    'e.Row.Cells(4).HorizontalAlign = HorizontalAlign.Left
    '    'e.Row.Cells(5).HorizontalAlign = HorizontalAlign.Left

    'End Sub
    Protected Sub treeList_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As TreeListHtmlDataCellEventArgs) Handles treeList.HtmlDataCellPrepared




        If e.Column.FieldName = "RID" Or e.Column.FieldName = "VIEW_ID" Then
            e.Cell.HorizontalAlign = HorizontalAlign.Left
            e.Column.CellStyle.HorizontalAlign = HorizontalAlign.Left
        End If

        If e.Column.FieldName = "RID" Then
            Dim _RID = e.NodeKey

            Using dc As New DataClasses_NissanMotorExt()
                Dim _rpt = dc.tblReports.Where(Function(x) x.RID.Equals(_RID)).FirstOrDefault()
                If _rpt.VIEW_ID Is Nothing Then
                    e.Cell.Text = _rpt.TITLE
                    e.Cell.Font.Bold = True
                    'Dim c = System.Drawing.Color.FromArgb(0, 0, e.NodeKey)
                    'e.Cell.BackColor = c
                End If
            End Using


        End If

    End Sub

End Class
