Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports DevExpress.Web.ASPxTreeList
Imports DevExpress.Web



Public Class ucUsers
    Inherits PortalModuleControl
















    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If

        If Page.IsPostBack = False Then
            Session("UserID") = Nothing
            '' Obtain PortalSettings from Current Context
            ''Dim portalSettings As PortalSettings = CType(Context.Items(ConfigurationSettings.AppSettings("PortalContextName")), PortalSettings)
            ''BindProject()
            'BindProjectRole()
            ''BindRole()
            'BindData()


            ''siteName.Text = portalSettings.PortalName
            ''showEdit.Checked = portalSettings.AlwaysShowEditButton
            ''BindEmpGroup()
            ''BindData()
            ''SimpleForm1.Hidden = True
            ''SimpleForm2.Hidden = True
            ''gpModuleRole.Hidden = True
            ''SimpleForm1.Hidden = True




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

            'Select Case cmbMode.SelectedIndex
            '    Case 1
            '        node.AllowSelect = Not node.HasChildren
            '    Case 2
            '        node.AllowSelect = node.HasChildren
            '    Case 3
            '        node.AllowSelect = node.Level > 2
            'End Select


        Loop
    End Sub

    Protected Sub cbOpenPopup_Callback(ByVal source As Object, ByVal e As CallbackEventArgs) Handles cbOpenPopup.Callback
        Dim _username = e.Parameter

        Dim _dc As New DataClasses_PortalDataContextExt()
        Dim _data_user = (From c In _dc.Portal_Users Where c.UserName = _username).FirstOrDefault()

        Session("UserID") = _data_user.UserID



        'If _data_user.Count > 0 Then
        '    Dim sb As New StringBuilder()
        '    'For Each _item In _data_user
        '    '    sb.Append(_item.AECode.Trim() & ";")
        '    'Next
        '    e.Result = sb.ToString()
        'Else
        e.Result = ""
        'End If

        'Using dc As New DataClasses_PortalDataContextExt()
        '    Dim _data = (From c In dc.Portal_Users Where c.UserName = "dusit").ToList()

        '    formLayout.DataSource = _data
        '    formLayout.DataBind()
        'End Using


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

                'Try
                '    treeList.FindNodeByKeyValue(item.UserID.ToString()).Selected = True
                'Catch ex As Exception

                'End Try
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
                dc.Portal_UserRoles.DeleteAllOnSubmit(_data_del)
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