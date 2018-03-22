Imports Portal.Components
Imports DevExpress.Web.ASPxTreeList
Imports DevExpress.Web
Imports System.Data.SqlClient
Imports System.Data
'Imports Portal.Web.BI

Partial Class Modules_ucDevxDashBoardAssignment
    Inherits PortalModuleControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        treeList.ExpandAll()

        If (Not IsPostBack) Then
            'Literal1.Text = String.Format("<img src='http://lockthbnkedc01/sites/HRDweb/LWT%20Member%20Photos/{0}.jpg' id='logo' Width='100px' />", HttpContext.Current.User.Identity.Name)


            Session("assignusername") = Nothing



        End If
    End Sub

    Protected Sub cbSave_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbSave.Callback

        Dim _username = Session("assignusername")
        Using dc As New DataClasses_LWTReportsExt()

            Dim _Nodes = treeList.GetAllNodes()
            For Each item In _Nodes
                dc.ExecuteCommand("delete tblReport_Assignment where UserName='" & _username & "' and RID=" & item.Key)
            Next

            Dim _SelectedNodes = treeList.GetSelectedNodes()
            Dim _newnode As New List(Of tblReport_Assignment)
            For Each item In _SelectedNodes
                _newnode.Add(New tblReport_Assignment With {.RID = item.Key, .UserName = _username})
            Next
            dc.tblReport_Assignments.InsertAllOnSubmit(_newnode)

            dc.SubmitChanges()

            e.Result = "Save"
        End Using


    End Sub

    Protected Sub callbackPanel_view_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase) Handles callbackPanel_view.Callback
        Dim _username = e.Parameter.ToString()
        Session("assignusername") = _username


        treeList.DataBind()
        treeList.UnselectAll()

        Using dc As New DataClasses_LWTReportsExt()
            Dim _node = (From c In dc.tblReport_Assignments Where c.UserName.Equals(_username)).ToList()
            For Each item In _node
                If treeList.FindNodeByKeyValue(item.RID.ToString()) IsNot Nothing Then
                    treeList.FindNodeByKeyValue(item.RID.ToString()).Selected = True
                End If
            Next
        End Using

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
