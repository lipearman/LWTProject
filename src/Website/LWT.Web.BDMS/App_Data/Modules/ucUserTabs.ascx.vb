Imports Portal.Components
Imports DevExpress.Web.ASPxTreeList
Imports DevExpress.Web
Imports System.Data.SqlClient
Imports System.Data
Imports LWT.Website


Public Class ucUserTabs
    Inherits PortalModuleControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load


        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If
        pnMain.HeaderText = portalSettings.ActiveTab.TabName
        pnMain.HeaderImage.IconID = portalSettings.ActiveTab.IconID
        Session("PortalId") = webconfig._PortalID

        If (Not IsPostBack) Then
            Session("UserName") = Nothing
        End If
    End Sub



    Protected Sub callbackPanel_view_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase) Handles callbackPanel_view.Callback
        Dim _username = e.Parameter.ToString()
        Session("UserName") = _username


        treeList.DataBind()
        treeList.UnselectAll()

        Using dc As New DataClasses_PortalDataContextExt()

          


            Dim _user = (From c In dc.Portal_Users Where c.UserName.Equals(_username)).FirstOrDefault()

            Dim _node = (From c In dc.Portal_UserTabs Where c.USERID.Equals(_user.UserID) And c.PortalId.Equals(webconfig._PortalID)).ToList()
            For Each item In _node
                Dim _childnode = treeList.FindNodeByKeyValue(item.TABID.ToString())
                If _childnode IsNot Nothing AndAlso Not _childnode.HasChildren Then
                    _childnode.Selected = True
                End If
            Next


            Dim _defaultPage = (From c In dc.Portal_Users_DefaultPages Where c.PortalId.Equals(webconfig._PortalID) And c.UserId = _user.UserID).FirstOrDefault()
            If _defaultPage IsNot Nothing Then
        
                Dim defaulttab = (From c In dc.v_DesktopTabs Where c.TabId = _defaultPage.TabId And c.PortalId.Equals(webconfig._PortalID)).FirstOrDefault()

                callbackPanel_view.JSProperties("cpDefaultTabId") = _defaultPage.TabId.ToString()
                callbackPanel_view.JSProperties("cpDefaultTabName") = defaulttab.TabName

            Else
                callbackPanel_view.JSProperties("cpDefaultTabId") = ""
                callbackPanel_view.JSProperties("cpDefaultTabName") = ""
            End If
 
        End Using



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
    Protected Sub cbSave_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbSave.Callback
        Dim _defaultTabId = e.Parameter.ToString()
        'Dim _username = Session("UserName")
        'Using dc As New DataClasses_PortalDataContextExt()

        '    Dim _Nodes = treeList.GetAllNodes()
        '    For Each item In _Nodes
        '        dc.ExecuteCommand("delete tblReport_Assignment where UserName='" & _username & "' and RID=" & item.Key)
        '    Next

        '    Dim _SelectedNodes = treeList.GetSelectedNodes()
        '    Dim _newnode As New List(Of tblReport_Assignment)
        '    For Each item In _SelectedNodes
        '        _newnode.Add(New tblReport_Assignment With {.RID = item.Key, .UserName = _username})
        '    Next
        '    dc.tblReport_Assignments.InsertAllOnSubmit(_newnode)

        '    dc.SubmitChanges()

        '    e.Result = "Save"
        'End Using

        Dim _SelectedNodes = treeList.GetSelectedNodes()
        Dim _selectedTabIds As New List(Of Integer)
        Dim _username = Session("UserName")


        Using dc As New DataClasses_PortalDataContextExt()
            Dim _user = (From c In dc.Portal_Users Where c.UserName.Equals(_username)).FirstOrDefault()
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


           

            Dim _defaultPage = (From c In dc.Portal_Users_DefaultPages Where c.PortalId.Equals(webconfig._PortalID) And c.UserId = _user.UserID).FirstOrDefault()
            If _defaultPage IsNot Nothing Then 'Update
                If String.IsNullOrEmpty(_defaultTabId) Then 'delete
                    dc.Portal_Users_DefaultPages.DeleteOnSubmit(_defaultPage)
                Else 'update
                    _defaultPage.TabId = CInt(_defaultTabId)
                End If
            Else 'new
                If Not String.IsNullOrEmpty(_defaultTabId) Then 'insert
                    Dim NewData As New Portal_Users_DefaultPage

                    With NewData
                        .PortalId = webconfig._PortalID
                        .UserId = _user.UserID
                        .TabId = CInt(_defaultTabId)
                    End With

                    dc.Portal_Users_DefaultPages.InsertOnSubmit(NewData)
                End If
            End If



            If _selectedTabIds.Count > 0 Then

                Dim _tabids = _selectedTabIds.Distinct().ToList()

                '=================== Delete TabId ==================
                Dim _data_del = (From c In dc.Portal_UserTabs Where c.USERID.Equals(_user.UserID) And c.PortalId.Equals(webconfig._PortalID) And Not _tabids.Contains(c.TABID)).ToList()
                If _data_del.Count > 0 Then
                    dc.Portal_UserTabs.DeleteAllOnSubmit(_data_del)
                End If

                '=================== Add New TabId ==================
                Dim _newnode As New List(Of Portal_UserTab)
                For Each TabID In _tabids
                    Dim _data = (From c In dc.Portal_UserTabs Where c.USERID.Equals(_user.UserID) And c.TABID.Equals(TabID)).FirstOrDefault()
                    If _data Is Nothing Then
                        _newnode.Add(New Portal_UserTab With {.TABID = TabID, .USERID = _user.UserID, .PortalId = webconfig._PortalID})
                    End If
                Next
                If _newnode.Count > 0 Then
                    dc.Portal_UserTabs.InsertAllOnSubmit(_newnode)
                End If

               

            End If

            dc.SubmitChanges()

            e.Result = "Save"


        End Using


        'Dim _username = Session("UserName")
        'Using dc As New DataClasses_PortalDataContextExt()
        '    Dim _user = (From c In dc.Portal_Users Where c.UserName.Equals(_username)).FirstOrDefault()

        '    Dim _SelectedNodes = treeList.GetSelectedNodes()

        '    '=================== Delete TabId ==================
        '    Dim _selectedTabId As New List(Of Integer)
        '    For Each item In _SelectedNodes
        '        _selectedTabId.Add(item.Key)
        '    Next
        '    Dim _data_del = (From c In dc.Portal_UserTabs Where c.USERID.Equals(_user.UserID) And Not _selectedTabId.Contains(c.TABID)).ToList()
        '    If _data_del.Count > 0 Then
        '        dc.Portal_UserTabs.DeleteAllOnSubmit(_data_del)
        '    End If
        '    '=================== Add New TabId ==================
        '    Dim _newnode As New List(Of Portal_UserTab)
        '    For Each item In _SelectedNodes
        '        Dim _data = (From c In dc.Portal_UserTabs Where c.USERID.Equals(_user.UserID) And c.TABID.Equals(item.Key)).FirstOrDefault()
        '        If _data Is Nothing Then
        '            _newnode.Add(New Portal_UserTab With {.TABID = item.Key, .USERID = _user.UserID})
        '        End If
        '    Next
        '    If _newnode.Count > 0 Then
        '        dc.Portal_UserTabs.InsertAllOnSubmit(_newnode)

        '    End If

        '    dc.SubmitChanges()

        '    e.Result = "Save"
        'End Using

    End Sub



    Protected Sub TreeList2_CustomJSProperties(ByVal sender As Object, ByVal e As TreeListCustomJSPropertiesEventArgs)
        Dim treeList As ASPxTreeList = TryCast(sender, ASPxTreeList)
        Dim nameTable As New Hashtable()
        For Each node As TreeListNode In treeList.GetVisibleNodes()
            nameTable.Add(node.Key, String.Format("{0}", node("TabName")))
        Next node
        e.Properties("cpDefaultPage") = nameTable
    End Sub
End Class