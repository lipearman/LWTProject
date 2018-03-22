Imports Portal.Components
Imports DevExpress.Web.ASPxTreeList
Imports DevExpress.Web
Imports System.Data.SqlClient
Imports System.Data
'Imports Portal.Web.BI

Partial Class Modules_ucDevxBIFileOwnerAssignment
    Inherits PortalModuleControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(LWT.Website.webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If
        pnMain.HeaderText = portalSettings.ActiveTab.TabName
        pnMain.HeaderImage.IconID = portalSettings.ActiveTab.IconID
        treeList.ExpandAll()

        If (Not IsPostBack) Then
            Session("assignusername") = Nothing
        End If


        SqlDataSource_Report.SelectParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name
        SqlDataSource_Report.SelectParameters("PortalId").DefaultValue = portalSettings.PortalId
    End Sub

    Protected Sub cbSave_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbSave.Callback

        Dim _username = Session("assignusername")
        Using dc As New DataClasses_PortalBIExt

            Dim _Nodes = treeList.GetAllNodes()
            For Each item In _Nodes
                dc.ExecuteCommand("delete tblDataSourceBI_Assignment where UserName='" & _username & "' and BID=" & item.Key)
            Next

            Dim _SelectedNodes = treeList.GetSelectedNodes()
            Dim _newnode As New List(Of tblDataSourceBI_Assignment)
            For Each item In _SelectedNodes
                _newnode.Add(New tblDataSourceBI_Assignment With {.BID = item.Key, .USERNAME = _username})
            Next
            dc.tblDataSourceBI_Assignments.InsertAllOnSubmit(_newnode)

            dc.SubmitChanges()

            e.Result = "Save"
        End Using


    End Sub

    Protected Sub callbackPanel_view_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase) Handles callbackPanel_view.Callback
        Dim _username = e.Parameter.ToString()
        Session("assignusername") = _username


        treeList.DataBind()
        treeList.UnselectAll()

        Using dc As New DataClasses_PortalBIExt
            Dim _node = (From c In dc.tblDataSourceBI_Assignments Where c.USERNAME.Equals(_username)).ToList()
            For Each item In _node
                Try
                    treeList.FindNodeByKeyValue(item.BID.ToString()).Selected = True
                Catch ex As Exception

                End Try

            Next
        End Using

    End Sub

    Protected Sub treeList_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As TreeListHtmlDataCellEventArgs) Handles treeList.HtmlDataCellPrepared
        If e.Column.FieldName = "BID" Or e.Column.FieldName = "SourceName" Then
            e.Cell.HorizontalAlign = HorizontalAlign.Left
            e.Column.CellStyle.HorizontalAlign = HorizontalAlign.Left
        End If

        If e.Column.FieldName = "TITLE" Then
            Dim _BID = e.NodeKey
            Using dc As New DataClasses_PortalBIExt()
                Dim _rpt = dc.tblDataSourceBIs.Where(Function(x) x.BID.Equals(_BID)).FirstOrDefault()
                If _rpt.DS_ID Is Nothing Then
                    e.Cell.Text = _rpt.TITLE
                    e.Cell.Font.Bold = True
                    'Dim c = System.Drawing.Color.FromArgb(0, 0, e.NodeKey)
                    'e.Cell.BackColor = c
                End If
            End Using
        End If

    End Sub

    'protected void ASPxTreeList1_HtmlRowPrepared(object sender, TreeListHtmlRowEventArgs e) {
    '        ASPxTreeList tree = sender as ASPxTreeList;
    '        if (Convert.ToInt32(e.NodeKey) % 3 == 1) {
    '            tree.FindNodeByKeyValue(e.NodeKey).Selected = true; // or false
    '        }
    '}
    'Protected Sub ASPxTreeList1_HtmlRowPrepared(sender As Object, e As TreeListHtmlRowEventArgs) Handles treeList.HtmlRowPrepared
    '    Dim tree As ASPxTreeList = TryCast(sender, ASPxTreeList)

    '    If e.GetValue("SelectRID") IsNot System.DBNull.Value Then
    '        Dim SelectRID = CInt(e.GetValue("SelectRID"))


    '        If Convert.ToInt32(e.NodeKey) = SelectRID Then
    '            tree.FindNodeByKeyValue(e.NodeKey).Selected = True
    '        End If

    '    Else

    '        tree.FindNodeByKeyValue(e.NodeKey).Selected = False

    '    End If


    'End Sub
End Class
