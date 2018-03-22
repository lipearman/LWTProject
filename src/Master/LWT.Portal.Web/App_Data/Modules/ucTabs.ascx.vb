
Imports System.Data
Imports System.Web.Security
Imports Portal.Components

Imports DevExpress.Web.ASPxTreeList
Imports System.IO
Imports DevExpress.Web.Internal
Imports DevExpress.Web.Data
Imports DevExpress.Web

Public Class ucTabs
    Inherits PortalModuleControl

    '*******************************************************
    '
    ' The Page_Load server event handler on this user control is used
    ' to populate the current site settings from the config system
    '
    '*******************************************************
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
        'If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
        '    Response.Redirect("~/Admin/AccessDenied.aspx")
        'End If

        '' Verify that the current user has access to access this page
        'If PortalSecurity.IsInRoles("Admins") = False Then
        '    Response.Redirect("~/Admin/EditAccessDenied.aspx")
        '    'Response.End()
        'End If

        ' If this is the first visit to the page, populate the site data
        If Page.IsPostBack = False Then

            Session("PortalId") = Nothing
            Session("TabId") = Nothing
            ' Obtain PortalSettings from Current Context
            'Dim portalSettings As PortalSettings = CType(Context.Items(ConfigurationSettings.AppSettings("PortalContextName")), PortalSettings)


            'siteName.Text = portalSettings.PortalName
            'showEdit.Checked = portalSettings.AlwaysShowEditButton
            'BindProject()
            'BindData()
            'SimpleForm1.Hidden = True
            'SimpleForm2.Hidden = True
            'gpModuleRole.Hidden = True
            'SimpleForm1.Hidden = True

        End If


        'tree.ExpandAll()

    End Sub
    Protected Sub tree_NodeInserting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles tree.NodeInserting
        'CustomDataSource.Rows.Add(e.NewValues("Id"), e.NewValues("Description"), e.NewValues("ParentId"))


        Dim _Name = e.NewValues("Name")
        Dim _ParentId = CInt(e.NewValues("ParentId"))
        Dim _PortalId = e.NewValues("PortalId")

        If _ParentId < 0 Then
            _PortalId = _ParentId * -1
            _ParentId = 1
        Else
            Using dc As New DataClasses_PortalDataContextExt()
                Dim _tab_data = (From c In dc.PortalCfg_Tabs Where c.TabId.Equals(_ParentId)).FirstOrDefault()
                If _tab_data IsNot Nothing Then
                    _PortalId = _tab_data.PortalId
                End If
            End Using
        End If

        e.NewValues("ParentId") = _ParentId
        e.NewValues("PortalId") = _PortalId


        'e.Cancel = True
        'tree.CancelEdit()
    End Sub

    Protected Sub tree_CommandColumnButtonInitialize(ByVal sender As Object, ByVal e As TreeListCommandColumnButtonEventArgs) Handles tree.CommandColumnButtonInitialize
        'If e.ButtonType <> TreeListCommandColumnButtonType.Custom Then
        '    Return

        'End If

        If (e.ButtonType = TreeListCommandColumnButtonType.Custom And e.NodeKey < 0) Or (e.ButtonType = TreeListCommandColumnButtonType.Edit And e.NodeKey < 0) Then
            e.Visible = DevExpress.Utils.DefaultBoolean.False
        Else

        End If


        'If (e.ButtonType = TreeListCommandColumnButtonType.Custom And e.NodeKey < 0) _
        '   Or (e.ButtonType = TreeListCommandColumnButtonType.Edit And e.NodeKey < 0) _
        '   Or (e.ButtonType = TreeListCommandColumnButtonType.New And e.NodeKey > 0) Then
        '    e.Visible = DevExpress.Utils.DefaultBoolean.False
        'End If

    End Sub

    Protected Sub tree_ProcessDragNode(ByVal sender As Object, ByVal e As TreeListNodeDragEventArgs) Handles tree.ProcessDragNode
        Dim oldPath As String = e.Node.GetValue("TabId").ToString()
        Dim destination As String = If(e.NewParentNode Is tree.RootNode, Session("PortalId"), e.NewParentNode.GetValue("TabId").ToString())
        'Dim newPath As String = destination & Path.DirectorySeparatorChar + Path.GetFileName(oldPath)
        'FileManagerHelper.MovePath(oldPath, newPath)


        Using dc As New DataClasses_PortalDataContextExt()
            Dim Data = (From c In dc.PortalCfg_Tabs Where c.TabId.Equals(oldPath)).FirstOrDefault()

            If destination < 0 Then
                destination = 1
            End If
            Data.ParentId = destination
            dc.SubmitChanges()
        End Using

        'tree.RefreshVirtualTree()
        e.Handled = True
    End Sub

    Protected Sub tree_CellEditorInitialize(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxTreeList.TreeListColumnEditorEventArgs) Handles tree.CellEditorInitialize
        Dim ASPxGridView1 = DirectCast(sender, ASPxTreeList)
        'If (ASPxGridView1.IsNewNodeEditing) Then Return
        If (ASPxGridView1.IsNewNodeEditing And e.Column.FieldName = "TabName") Then
            e.Editor.Value = "New Page"
        End If

    End Sub

    'Protected Sub cmbProject_SelectedIndexChanged(ByVal sender As Object, ByVal e As CallbackEventArgsBase) Handles cmbProject.Callback

    '    Session("PortalId") = cmbProject.Value
    '    tree.ClientVisible = True

    '    'tree.DataBind()


    'End Sub

    Protected Sub tree_CustomCallback(ByVal sender As Object, ByVal e As TreeListCustomCallbackEventArgs) Handles tree.CustomCallback
        'If e.Argument = "upload_complete" Then
        '    Dim node As TreeListNode = tree.FocusedNode
        '    Dim folderName As String = If(node Is Nothing, FileManagerHelper.RootFolder, node.GetValue(FileManagerHelper.FullPathName).ToString())
        '    Dim uploadedName As String = FileManagerHelper.EndUploadFile(folderName)
        '    If (Not String.IsNullOrEmpty(uploadedName)) Then
        '        If node IsNot Nothing Then
        '            node.Expanded = True
        '        End If
        '        tree.RefreshVirtualTree()
        '        FocusByPath(uploadedName)
        '    End If
        'End If
        Session("PortalId") = Nothing
        Session("TabId") = Nothing
        Select Case e.Argument
            Case "cmdEdit"
                Dim _TabId = tree.FocusedNode.GetValue("TabId")
                Dim _PortalId As Integer = CInt(tree.FocusedNode.GetValue("PortalId"))

                Session("TabId") = _TabId

                'open employee edit page
                'DevExpress.Web.ASPxWebControl.RedirectOnCallback("employee_edit.aspx")
                'DevExpress.Web.ASPxWebControl.

                Session("PortalId") = _PortalId
            Case "cmdMoveUp"
                Dim _TabId As Integer = tree.FocusedNode.GetValue("TabId")
                'Dim _TabOrder As Integer = tree.FocusedNode.GetValue("TabOrder")
                Dim _ParentId As Integer = tree.FocusedNode.GetValue("ParentId")
                'Dim _PortalId As Integer = CInt(cmbProject.Value)
                Dim _PortalId As Integer = CInt(tree.FocusedNode.GetValue("PortalId"))
                Session("PortalId") = _PortalId
                Using dc As New DataClasses_PortalDataContextExt()
                    If _ParentId < 0 Then
                        _ParentId = 1
                    End If

                    Dim _Nodes = (From c In dc.PortalCfg_Tabs Where c.ParentId.Equals(_ParentId) And c.PortalId.Equals(_PortalId) Order By c.TabOrder).ToList()
                    For i As Integer = 0 To _Nodes.Count - 1
                        _Nodes(i).TabOrder = i + 1
                    Next i
                    dc.SubmitChanges()


                    Dim _Old = (From c In dc.PortalCfg_Tabs Where c.TabId.Equals(_TabId)).FirstOrDefault()
                    Dim _New = (From c In dc.PortalCfg_Tabs Where c.TabOrder < _Old.TabOrder And c.PortalId.Equals(_Old.PortalId) And c.ParentId.Equals(_Old.ParentId) Order By c.TabOrder Descending).FirstOrDefault()
                    If _New IsNot Nothing Then
                        Dim _OldOrder As Integer = _Old.TabOrder
                        _Old.TabOrder = _New.TabOrder
                        _New.TabOrder = _OldOrder
                        dc.SubmitChanges()
                    End If
                End Using

            Case "cmdMoveDown"
                Dim _TabId As Integer = tree.FocusedNode.GetValue("TabId")
                'Dim _TabOrder As Integer = tree.FocusedNode.GetValue("TabOrder")
                Dim _ParentId As Integer = tree.FocusedNode.GetValue("ParentId")
                'Dim _PortalId As Integer = CInt(cmbProject.Value)
                Dim _PortalId As Integer = CInt(tree.FocusedNode.GetValue("PortalId"))
                Session("PortalId") = _PortalId
                Using dc As New DataClasses_PortalDataContextExt()
                    If _ParentId < 0 Then
                        _ParentId = 1
                    End If
                    Dim _Nodes = (From c In dc.PortalCfg_Tabs Where c.ParentId.Equals(_ParentId) And c.PortalId.Equals(_PortalId) Order By c.TabOrder).ToList()
                    For i As Integer = 0 To _Nodes.Count - 1
                        _Nodes(i).TabOrder = i + 1
                    Next i
                    dc.SubmitChanges()


                    Dim _Old = (From c In dc.PortalCfg_Tabs Where c.TabId.Equals(_TabId)).FirstOrDefault()

                    Dim _New = (From c In dc.PortalCfg_Tabs Where c.TabOrder > _Old.TabOrder And c.PortalId.Equals(_Old.PortalId) And c.ParentId.Equals(_Old.ParentId) Order By c.TabOrder Descending).FirstOrDefault()

                    If _New IsNot Nothing Then
                        Dim _OldOrder As Integer = _Old.TabOrder
                        _Old.TabOrder = _OldOrder + 1 '_New.TabOrder

                        _New.TabOrder = _OldOrder

                        dc.SubmitChanges()


                    End If
                End Using
        End Select


        tree.DataBind()
        lbAvailable.DataBind()
        lbChoosen.DataBind()

    End Sub
    Protected Sub tree_CustomDataCallback(ByVal sender As Object, ByVal e As TreeListCustomDataCallbackEventArgs) Handles tree.CustomDataCallback
        Dim key As String = e.Argument.ToString()
        Dim node As TreeListNode = tree.FindNodeByKeyValue(key)
        e.Result = node
    End Sub


    Protected Sub callbackPanel_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase) Handles callbackPanel.Callback

        If tree.FocusedNode IsNot Nothing Then
            lbTabName.Text = "Page: " & tree.FocusedNode.GetValue("Name")
            Session("TabId") = tree.FocusedNode.GetValue("TabId")
            Session("PortalId") = tree.FocusedNode.GetValue("PortalId")
        Else
            lbTabName.Text = ""
            Session("TabId") = Nothing
            Session("PortalId") = Nothing
        End If



        tree.DataBind()
        lbAvailable.DataBind()
        lbChoosen.DataBind()
    End Sub


    Protected Sub cbSaveModule_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbSaveModule.Callback

        If tree.FocusedNode Is Nothing Then
            Return
        End If

        Dim _ModuleDefId As String = e.Parameter

        Dim _TabId As Integer = tree.FocusedNode.GetValue("TabId")
        'Dim _PortalId As Integer = CInt(cmbProject.Value)
        Using dc As New DataClasses_PortalDataContextExt()

            If String.IsNullOrEmpty(_ModuleDefId) Then
                'delete all
                dc.ExecuteCommand("delete from PortalCfg_Modules where TabId={0}", _TabId)

            Else
                'delete not in (PortalCfg_Modules)
                dc.ExecuteCommand("delete from PortalCfg_Modules where TabId=" & _TabId & " and ModuleDefId not in(" & _ModuleDefId & ")")
                Dim _id = _ModuleDefId.Split(",")


                Dim _newmodules As New List(Of PortalCfg_Module)

                For Each _mdfid In _id

                    Dim _data = (From c In dc.PortalCfg_Modules Where c.ModuleDefId.Equals(_mdfid) And c.TabId.Equals(_TabId)).FirstOrDefault()
                    If _data Is Nothing Then
                        Dim _data_moduledef = (From c In dc.PortalCfg_ModuleDefinitions Where c.ModuleDefId.Equals(_mdfid)).FirstOrDefault()
                        If _data_moduledef IsNot Nothing Then
                            Dim _newmodule As New PortalCfg_Module
                            With _newmodule
                                .ModuleDefId = _mdfid
                                .TabId = _TabId

                                .ModuleTitle = _data_moduledef.ModuleDefName
                                .ModuleOrder = 0
                                .PaneName = "ContentPane"

                                .CacheTimeout = 0
                                .ShowMobile = False
                                .EditRoles = webconfig._AdminRole

                            End With
                            _newmodules.Add(_newmodule)
                        End If
                       
                    End If

                Next
                If _newmodules.Count > 0 Then
                    dc.PortalCfg_Modules.InsertAllOnSubmit(_newmodules)
                    dc.SubmitChanges()
                End If
               



            End If
        End Using




        e.Result = "Save"

    End Sub
End Class