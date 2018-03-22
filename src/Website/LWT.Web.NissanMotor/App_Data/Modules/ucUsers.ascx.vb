Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports DevExpress.Web.ASPxTreeList
Imports DevExpress.Web
Imports LWT.Website

Partial Class Modules_ucUsers
    Inherits PortalModuleControl

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If
        Session("PortalId") = webconfig._PortalID


        If Page.IsPostBack = False Then
            Session("UserID") = Nothing


        End If
    End Sub


    Protected Sub treeList_DataBound(ByVal sender As Object, ByVal e As EventArgs) Handles treeList.DataBound
        Dim iterator As TreeListNodeIterator = treeList.CreateNodeIterator()
        Dim node As TreeListNode
        Do
            node = iterator.GetNext()
            If node Is Nothing Then
                Exit Do
            End If

            node.AllowSelect = node.Level > 1



        Loop
    End Sub

    Protected Sub cbOpenPopup_Callback(ByVal source As Object, ByVal e As CallbackEventArgs) Handles cbOpenPopup.Callback
        Dim _username = e.Parameter

        Dim _dc As New DataClasses_PortalDataContextExt()
        Dim _data_user = (From c In _dc.Portal_Users Where c.UserName = _username).FirstOrDefault()

        Session("UserID") = _data_user.UserID



        e.Result = "save"


    End Sub

    'Protected Sub cbformLayout_Callback(ByVal source As Object, ByVal e As CallbackEventArgs) Handles cbformLayout.Callback
    '    formLayout.DataBind()
    'End Sub
    Protected Sub callbackPanel_formLayout_Callback(ByVal source As Object, ByVal e As CallbackEventArgsBase) Handles callbackPanel_formLayout.Callback
        formLayout.DataBind()

        treeList.UnselectAll()

        Dim UserID As Integer = CInt(Session("UserID"))
        Using dc As New DataClasses_PortalDataContextExt()
            Dim _node = (From c In dc.Portal_UserRoles Where c.UserID.Equals(UserID)).ToList()
            For Each item In _node
                If treeList.FindNodeByKeyValue(item.RoleID.ToString()) IsNot Nothing Then
                    treeList.FindNodeByKeyValue(item.RoleID.ToString()).Selected = True
                End If


            Next
        End Using

        treeList.DataBind()
    End Sub


    Protected Sub cbSaveUserRoles_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbSaveUserRoles.Callback



        Dim UserID As Integer = CInt(Session("UserID"))
        Using dc As New DataClasses_PortalDataContextExt()


            Dim _data_user = (From c In dc.Portal_Users Where c.UserID.Equals(UserID)).FirstOrDefault()
            With _data_user
                .PasswordQuestion = txtPasswordQuestion.Text
                .PasswordAnswer = txtPasswordAnswer.Text
                .Comment = txtComment.Text
                .IsApproved = cbIsApproved.Checked
                .IsLocked = cbIsLocked.Checked
                .ExpiredDate = deExpiredDate.Value

            End With





            Dim _SelectedNodes = treeList.GetSelectedNodes()


            '=================== Delete   ==================
            Dim _selectedRoleId As New List(Of Integer)
            For Each item In _SelectedNodes
                _selectedRoleId.Add(item.Key)
            Next
            Dim _data_del = (From c In dc.Portal_UserRoles Where c.UserID.Equals(UserID) And Not _selectedRoleId.Contains(c.RoleID)).ToList()
            If _data_del.Count > 0 Then
                For Each _item_userroles In _data_del
                    Dim _rols_portal = (From c In dc.Portal_Roles Where c.RoleID.Equals(_item_userroles.RoleID) And c.PortalID.Equals(webconfig._PortalID)).FirstOrDefault()
                    If _rols_portal IsNot Nothing Then
                        dc.Portal_UserRoles.DeleteOnSubmit(_item_userroles)
                    End If
                Next
            End If
            '=================== Add New   ==================
            Dim _newnode As New List(Of Portal_UserRole)
            For Each item In _SelectedNodes
                Dim _data = (From c In dc.Portal_UserRoles Where c.UserID.Equals(UserID) And c.RoleID.Equals(item.Key)).FirstOrDefault()
                If _data Is Nothing Then
                    _newnode.Add(New Portal_UserRole With {.UserID = UserID, .RoleID = item.Key})
                End If
            Next
            If _newnode.Count > 0 Then
                dc.Portal_UserRoles.InsertAllOnSubmit(_newnode)

            End If

            dc.SubmitChanges()

            e.Result = "Save"
        End Using


    End Sub
End Class
