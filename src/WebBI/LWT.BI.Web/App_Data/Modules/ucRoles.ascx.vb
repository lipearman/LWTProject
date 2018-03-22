Imports System.Data
Imports System.Web.Security
Imports Portal.Components

Imports DevExpress.Web
Imports DevExpress.Web.ASPxTreeList
Imports LWT.Website

Partial Class Modules_ucRoles
    Inherits PortalModuleControl
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load







        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)


        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If


        pnMain.HeaderImage.IconID = portalSettings.ActiveTab.IconID
        pnMain.HeaderText = portalSettings.ActiveTab.TabName

        Session("PortalId") = webconfig._PortalID
        tree.ExpandAll()
        treeList.ExpandAll()
        If Page.IsPostBack = False Then

            Session("RoleID") = Nothing
        End If



    End Sub
    Protected Sub tree_CustomCallback(ByVal sender As Object, ByVal e As TreeListCustomCallbackEventArgs) Handles tree.CustomCallback

        Session("RoleID") = Nothing

        Select Case e.Argument
            Case "cmdEdit"
                Dim _RoleID = tree.FocusedNode.GetValue("RoleID")
                Session("RoleID") = _RoleID
        End Select


        'treeList.DataBind()
    End Sub

    Protected Sub tree_DataBound(ByVal sender As Object, ByVal e As EventArgs) Handles tree.DataBound
        If tree.Nodes.Count > 0 Then
            tree.Nodes(0).Expanded = True
        End If

    End Sub


    Protected Sub tree_CommandColumnButtonInitialize(ByVal sender As Object, ByVal e As TreeListCommandColumnButtonEventArgs) Handles tree.CommandColumnButtonInitialize


        If (e.ButtonType = TreeListCommandColumnButtonType.Custom And e.NodeKey < 0) _
            Or (e.ButtonType = TreeListCommandColumnButtonType.Edit And e.NodeKey < 0) _
            Or (e.ButtonType = TreeListCommandColumnButtonType.New And e.NodeKey > 0) Then
            e.Visible = DevExpress.Utils.DefaultBoolean.False
        End If
    End Sub


    Protected Sub tree_NodeInserting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles tree.NodeInserting

        Dim _Name = e.NewValues("RoleName")
        e.NewValues("PortalId") = webconfig._PortalID
        e.NewValues("RoleName") = _Name
        e.NewValues("RoleDescription") = _Name
        e.NewValues("RoleCode") = String.Format("R{0}", DateTime.Now.ToString("yyyyMMddHHmmss"))

        'e.Cancel = True
        'tree.CancelEdit()
    End Sub




    Protected Sub callbackPanel_tabs_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase) Handles callbackPanel_tabs.Callback
        treeList.UnselectAll()
        Dim RoleID As Integer = 0
        Dim RoleName As String = ""

        RoleID = CInt(tree.FocusedNode.GetValue("RoleID").ToString())
        RoleName = tree.FocusedNode.GetValue("RoleName").ToString()

        displayRole.Text = "Role :" & RoleName

        Session("RoleID") = RoleID


        Using dc As New DataClasses_PortalDataContextExt()
            Dim _node = (From c In dc.Portal_TabRoles Where c.RoleID.Equals(RoleID)).ToList()
            For Each item In _node
                Dim _childnode = treeList.FindNodeByKeyValue(item.TabID.ToString())
                'If _childnode IsNot Nothing Then 
                '    _childnode.Selected = True
                'End If
                If _childnode IsNot Nothing AndAlso Not _childnode.HasChildren Then
                    'treeList.FindNodeByKeyValue(item.TabID.ToString()).Selected = True
                    _childnode.Selected = True
                End If
            Next
        End Using

        'treeList.DataBind()
    End Sub

    Sub Recursive(ByVal _ParentID As Integer, ByVal _Tabs As List(Of PortalCfg_Tab), ByRef _tabid As List(Of Integer))

        'If value >= 100 Then
        '    Return value
        'End If
        Dim _Parent = (From c In _Tabs Where c.TabId = _ParentID).FirstOrDefault()
        If _Parent IsNot Nothing Then
            _tabid.Add(_ParentID)
            If _Parent.ParentId IsNot Nothing Then
                Recursive(_Parent.ParentId, _Tabs, _tabid)
            End If
        End If
    End Sub


    Protected Sub cbSaveTabRoles_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbSaveTabRoles.Callback
        Dim _SelectedNodes = treeList.GetSelectedNodes()
        Dim _selectedTabIds As New List(Of Integer)


        Using dc As New DataClasses_PortalDataContextExt()
            Dim _Tabs = (From c In dc.PortalCfg_Tabs Where c.PortalId = webconfig._PortalID).ToList()
            For Each item In _SelectedNodes
                Dim _Child = (From c In _Tabs Where c.TabId = item.Key).FirstOrDefault()

                If _Child IsNot Nothing Then
                    _selectedTabIds.Add(item.Key)

                    If _Child.ParentId IsNot Nothing Then
                        Dim _Parent = (From c In _Tabs Where c.TabId = _Child.ParentId).FirstOrDefault()

                        If _Parent IsNot Nothing Then
                            Recursive(_Parent.TabId, _Tabs, _selectedTabIds)
                        End If
                    End If

                End If


            Next


            If _selectedTabIds.Count > 0 Then

                Dim _tabids = _selectedTabIds.Distinct().ToList()



                Dim RoleID As Integer = CInt(Session("RoleID"))

                '=================== Delete TabId ==================
                Dim _data_del = (From c In dc.Portal_TabRoles Where c.RoleID.Equals(RoleID) And Not _tabids.Contains(c.TabID)).ToList()
                If _data_del.Count > 0 Then
                    dc.Portal_TabRoles.DeleteAllOnSubmit(_data_del)
                End If
                '=================== Add New TabId ==================
                Dim _newnode As New List(Of Portal_TabRole)
                For Each TabID In _tabids
                    Dim _data = (From c In dc.Portal_TabRoles Where c.RoleID.Equals(RoleID) And c.TabID.Equals(TabID)).FirstOrDefault()
                    If _data Is Nothing Then
                        _newnode.Add(New Portal_TabRole With {.TabID = TabID, .RoleID = RoleID})
                    End If
                Next
                If _newnode.Count > 0 Then
                    dc.Portal_TabRoles.InsertAllOnSubmit(_newnode)

                End If

                dc.SubmitChanges()

            End If




            'Dim _SelectedNodes = treeList.GetSelectedNodes()


            ''=================== Delete TabId ==================
            'Dim _selectedTabId As New List(Of Integer)
            'For Each item In _SelectedNodes 
            '    '_selectedTabId.Add(item.Key)
            'Next

            'Dim _data_del = (From c In dc.Portal_TabRoles Where c.RoleID.Equals(RoleID) And Not _selectedTabId.Contains(c.TabID)).ToList()
            'If _data_del.Count > 0 Then
            '    dc.Portal_TabRoles.DeleteAllOnSubmit(_data_del)
            'End If
            ''=================== Add New TabId ==================
            'Dim _newnode As New List(Of Portal_TabRole)
            'For Each item In _SelectedNodes
            '    Dim _data = (From c In dc.Portal_TabRoles Where c.RoleID.Equals(RoleID) And c.TabID.Equals(item.Key)).FirstOrDefault()
            '    If _data Is Nothing Then
            '        _newnode.Add(New Portal_TabRole With {.TabID = item.Key, .RoleID = RoleID})
            '    End If
            'Next
            'If _newnode.Count > 0 Then
            '    dc.Portal_TabRoles.InsertAllOnSubmit(_newnode)

            'End If

            ''dc.SubmitChanges()

            e.Result = "Save"
        End Using


    End Sub
End Class
