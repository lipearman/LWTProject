Imports Portal.Components
Imports System.Security
Imports System.Data
Imports DevExpress.Web
Imports LWT.Website
Imports FineUI
Imports DevExpress.Web.ASPxTreeList


Public Class DesktopDefault
    Inherits BasePage

    'Protected tabIndex As Integer = 0
    'Protected tabId As Integer = 0
    'Protected parenttabId As Integer = 0
    'Protected parenttabname As String = ""
    'Protected parenttablink As String = ""
    'Protected tabname As String = ""
    Protected SiteName As String
    Protected mainMenu As String
    Protected subMenu As String
    'Protected portalTabs As ArrayList
    'Protected PageID As String = ""
    'Protected TabID As String = ""
    Protected UserName As String = ""

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
        'If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
        '    ASPxWebControl.RedirectOnCallback("~/Admin/AccessDenied.aspx")
        'End If


        SqlDataSource_leftTree.SelectParameters("PortalId").DefaultValue = portalSettings.PortalId
        SqlDataSource_leftTree.SelectParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name

        If IsPostBack = False Then


            'Session("PageID") = webconfig._DefaultPageID


            GenTopMenu()
        End If

    End Sub
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UserName = IIf(String.IsNullOrEmpty(HttpContext.Current.User.Identity.Name), "Guest", HttpContext.Current.User.Identity.Name)
    End Sub
    Public Sub GenTopMenu()

        Dim portalSettings As PortalSettings = CType(Context.Items(ConfigurationSettings.AppSettings("PortalContextName")), PortalSettings)
        SiteName = portalSettings.PortalName

        Dim _tabs = (From c In portalSettings.DesktopTabs Where c.ParentId = 1 And c.PortalId = portalSettings.PortalId Order By c.TabOrder).ToList()

        Dim _TopMenu As New List(Of v_DesktopTab)

        Dim _PageCount As Integer = 0
        For Each _menu In _tabs
            If PortalSecurity.IsInRoles(_menu.AuthorizedRoles, _menu.TabId) And _menu.AuthorizedRoles.Count > 0 Then
                _PageCount += 1
                If Session("PageID") Is Nothing Then
                    If _PageCount = 1 Then
                        Session("PageID") = _menu.PageID
                    End If
                End If
                _TopMenu.Add(New v_DesktopTab With {.TabId = _menu.TabId, .TabName = _menu.TabName, .PageId = _menu.PageID})
            End If
        Next

        gridTopMenu.DataSource = _TopMenu
        gridTopMenu.DataBind()

    End Sub

    Protected Sub cbTopMenu_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbTopMenu.Callback
        Dim _PageId = e.Parameter.ToString()
        Session("PageID") = _PageId
        Dim portalSettings As PortalSettings = CType(Context.Items(ConfigurationSettings.AppSettings("PortalContextName")), PortalSettings)
        e.Result = _PageId
    End Sub



    Protected Sub leftTree_CustomCallback(ByVal sender As Object, ByVal e As TreeListCustomCallbackEventArgs)
        DirectCast(sender, ASPxTreeList).DataBind()
        'If DirectCast(sender, ASPxTreeList).Nodes.Count > 0 Then
        '    DirectCast(sender, ASPxTreeList).Nodes(0).Selected = True
        'End If
    End Sub


    'leftTree
    Protected Sub leftTree_HtmlRowPrepared(ByVal sender As Object, ByVal e As TreeListHtmlRowEventArgs)
        'If e.Level = 1 Then
        '    'e.Row.Cells(0).Style("background-image") = "res/icon/folder.png"

        'Else


        'End If

    End Sub
    Protected Sub leftTree_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As TreeListHtmlDataCellEventArgs)

        e.Cell.HorizontalAlign = HorizontalAlign.Left
        'e.Column.CellStyle.HorizontalAlign = HorizontalAlign.Left
        e.Cell.Style("Background-Repeat") = "no-repeat"

        If e.Level = 1 Then
            e.Cell.Style("Background-Image") = "res/icon/folder.png"
        Else
            e.Cell.Style("Background-Image") = "res/icon/page.png"


        End If


    End Sub
    Protected Sub leftTree_CustomDataCallback(ByVal sender As Object, ByVal e As TreeListCustomDataCallbackEventArgs)
        'Dim _tabid As String = e.Argument.ToString()
        'Dim node As TreeListNode = DirectCast(sender, ASPxTreeList).FindNodeByKeyValue(_tabid)

        'Dim portalSettings As PortalSettings = CType(Context.Items(ConfigurationSettings.AppSettings("PortalContextName")), PortalSettings)

        'Using dc As New DataClasses_PortalDataContextExt()
        '    Dim _tab = (From c In portalSettings.DesktopTabs Where c.TabId = _tabid And c.PortalId = portalSettings.PortalId).FirstOrDefault()

        '    Dim _modules = (From c In dc.v_ModuleSettings Where c.TabId = _tabid).ToList()

        '    If _modules.Count > 0 Then
        '        e.Result = _tab.PageID
        '    Else
        '        e.Result = ""
        '    End If
        'End Using

        Dim _pageid As String = e.Argument.ToString()

        e.Result = _pageid
    End Sub

    'Protected Sub leftTree_DataBound(ByVal sender As Object, ByVal e As EventArgs)
    '    'DirectCast(sender, ASPxTreeList).Nodes(0).Expanded = True
    'End Sub
    Protected Sub leftTree_DataBound(ByVal sender As Object, ByVal e As EventArgs)
        If DirectCast(sender, ASPxTreeList).Nodes.Count > 0 Then
            DirectCast(sender, ASPxTreeList).Nodes(0).Expanded = True
        End If

    End Sub

     














    'Private Sub ResolveSubTree(ByVal dr As DataRow, ByVal treeNode As FineUI.TreeNode)
    '    For Each row As DataRow In dr.GetChildRows("TreeRelation")
    '        Dim node = New FineUI.TreeNode()
    '        node.Text = row("Text").ToString()
    '        node.EnableCheckBox = False
    '        node.NavigateUrl = row("URL").ToString()
    '        node.Target = "mainframe"
    '        node.NodeID = row("Id").ToString()
    '        treeNode.Nodes.Add(node)
    '        ResolveSubTree(row, node)
    '    Next
    'End Sub

    'Private Sub LoadData()
    '    Dim table = CreateDataTable()
    '    Dim ds = New DataSet()
    '    ds.Tables.Add(table)
    '    ds.Relations.Add("TreeRelation", ds.Tables(0).Columns("Id"), ds.Tables(0).Columns("ParentId"), False)
    '    For Each row As DataRow In ds.Tables(0).Rows
    '        If row.IsNull("ParentId") Then
    '            Dim node = New FineUI.TreeNode()
    '            node.Text = row("Text").ToString()
    '            node.Expanded = True
    '            node.Target = "mainframe"
    '            leftTree.Nodes.Add(node)
    '            ResolveSubTree(row, node)
    '        End If
    '    Next
    'End Sub

    'Private Function CreateDataTable() As DataTable
    '    Dim table = New DataTable()
    '    table.Columns.Add(New DataColumn("Id", GetType(String)))
    '    table.Columns.Add(New DataColumn("Text", GetType(String)))
    '    table.Columns.Add(New DataColumn("URL", GetType(String)))
    '    table.Columns.Add(New DataColumn("IsLeaf", GetType(Integer)))
    '    table.Columns.Add(New DataColumn("ParentId", GetType(String)))
    '    table.Columns.Add(New DataColumn("LinksID", GetType(Integer)))

    '    Dim _TreeMenuRoles As New List(Of MyContext.TreeMenuRoles)


    '    '==========================================
    '    ' Obtain PortalSettings from Current Context
    '    Dim portalSettings As PortalSettings = CType(Context.Items(ConfigurationSettings.AppSettings("PortalContextName")), PortalSettings)

    '    Dim config As Configuration = New Configuration
    '    portalTabs = New ArrayList

    '    Dim _PageID = Session("PageID")
    '    Dim _tab = (From c In portalSettings.DesktopTabs Where c.PageID = _PageID).FirstOrDefault()
    '    Dim tabs = (From c In portalSettings.DesktopTabs Where c.Sortpath.StartsWith(_tab.Sortpath) And c.PortalId = portalSettings.PortalId Order By c.Sortpath).ToList()

    '    For Each tab As DesktopTab In tabs
    '        If PortalSecurity.IsInRoles(tab.AuthorizedRoles, tab.TabId) Then
    '            Dim row2 = table.NewRow()
    '            row2("Id") = tab.TabId.ToString()
    '            row2("Text") = tab.TabName.ToString()
    '            row2("IsLeaf") = False
    '            row2("ParentId") = IIf(tab.PageID = _PageID, DBNull.Value, tab.ParentId.ToString())
    '            row2("URL") = String.Format("pages.aspx?pageid={0}", tab.PortalId.ToString())

    '            table.Rows.Add(row2)
    '        End If
    '    Next tab

    '    Return table
    'End Function


















    'Private Sub gridTopMenu_ItemCommand(ByVal sender As Object, ByVal e As RepeaterCommandEventArgs) Handles gridTopMenu.ItemCommand

    '    Select Case e.CommandName
    '        Case "topmenu"
    '            Dim _PageId = e.CommandArgument.ToString()

    '            'Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "ButtonClickScript", "selectMenu('" & _PageId & "');", False)


    '    End Select

    'End Sub




    Private Sub generateDynamicControl()
        ''*********************************************************************
        '' Obtain PortalSettings from Current Context
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(ConfigurationSettings.AppSettings("PortalContextName")), PortalSettings)


        ' Dynamically Populate the Left, Center and Right pane sections of the portal page
        If portalSettings.ActiveTab.Modules.Count > 0 Then

            ' Loop through each entry in the configuration system for this tab
            Dim _moduleSettings As ModuleSettings
            For Each _moduleSettings In portalSettings.ActiveTab.Modules

                If PortalSecurity.IsInRoles(_moduleSettings.AuthorizedEditRoles, _moduleSettings.TabId) Then

                    ''Dim parent As ContentPanel = Page.FindControl("ContentPanel1")

                    '' If no caching is specified, create the user control instance and dynamically
                    '' inject it into the page.  Otherwise, create a cached module instance that
                    '' may or may not optionally inject the module into the tree
                    'If _moduleSettings.CacheTimeOut = 0 Then

                    '    If Not String.IsNullOrEmpty(_moduleSettings.DesktopSourceFile) Then
                    '        Dim _ascx = String.Format("~/App_Data/Modules/{0}", _moduleSettings.DesktopSourceFile)
                    '        If System.IO.File.Exists(Server.MapPath(_ascx)) Then

                    '            Dim portalModule As PortalModuleControl = CType(Page.LoadControl(_ascx), PortalModuleControl)

                    '            portalModule.PortalId = portalSettings.PortalId
                    '            portalModule.ModuleConfiguration = _moduleSettings

                    '            ContentPanel1.Controls.Add(portalModule)
                    '        End If
                    '    End If
                    'Else

                    '    Dim portalModule As New CachedPortalModuleControl

                    '    portalModule.PortalId = portalSettings.PortalId
                    '    portalModule.ModuleConfiguration = _moduleSettings

                    '    ContentPanel1.Controls.Add(portalModule)
                    'End If

                    '' Dynamically inject separator break between portal modules
                    'ContentPanel1.Controls.Add(New LiteralControl("<" + "br" + ">"))
                    'ContentPanel1.Visible = True


                End If

            Next _moduleSettings
        End If
    End Sub


    Private Function GetSiteMap(ByVal _CurrentTab As DesktopTab, ByVal _tabs As List(Of DesktopTab)) As String
        Dim sbTabName As New StringBuilder()
        Dim _ParentTab = (From c In _tabs Where c.TabId = _CurrentTab.ParentId).FirstOrDefault()
        If _ParentTab IsNot Nothing Then
            sbTabName.Append(GetSiteMap(_ParentTab, _tabs))
            sbTabName.Append(_ParentTab.TabName & ";")
            'sbTabName.Append("<a onclick=""LoadingPanel.Show();"" href='" + String.Format("DesktopDefault.aspx?tabindex={0}&tabid={1}", _ParentTab.TabOrder.ToString(), _ParentTab.TabId.ToString()) + "'>" + _ParentTab.TabName + "</a>")

        End If
        Return sbTabName.ToString()
    End Function

    'Public Sub GenDynamicMenuV2()

    '    Dim portalSettings As PortalSettings = CType(Context.Items(ConfigurationSettings.AppSettings("PortalContextName")), PortalSettings)
    '    SiteName = portalSettings.PortalName

    '    Dim _tabs = (From c In portalSettings.DesktopTabs Where Not c.ParentId Is Nothing And c.PortalId = portalSettings.PortalId Order By c.Sortpath).ToList()
    '    Dim _currtab = (From c In portalSettings.DesktopTabs Where c.PageID = PageID And c.ParentId IsNot Nothing And c.PortalId = portalSettings.PortalId Order By c.Sortpath).FirstOrDefault()

    '    parenttabname = ""


    '    If Not _currtab Is Nothing Then
    '        Dim parentname = GetSiteMap(_currtab, _tabs)

    '        If Not String.IsNullOrEmpty(parentname) Then
    '            Dim _tempname = parentname.Split(";")

    '            For i As Integer = _tempname.Count - 1 To 0 Step -1

    '                If Not String.IsNullOrEmpty(_tempname(i)) Then
    '                    parenttabname = _tempname(i) & " <img src='css/skins/images/arrow_off.png' /> " & parenttabname
    '                End If

    '            Next

    '            parenttabname = parenttabname & ("<a style='color: blue;' onclick=""LoadingPanel.Show();"" href='" + String.Format("DesktopDefault.aspx?pageid={0}", _currtab.PageID.ToString()) + "'>" + _currtab.TabName + "</a>")
    '        Else
    '            parenttabname = ("<a style='color: blue;' onclick=""LoadingPanel.Show();"" href='" + String.Format("DesktopDefault.aspx?pageid={0}", _currtab.PageID.ToString()) + "'>" + _currtab.TabName + "</a>")
    '        End If




    '        If _parenttabs.ParentId = 1 Then
    '            'parenttabId = _parenttabs.TabId
    '            parenttabname = _parenttabs.TabName
    '        Else
    '            Dim _m_tabs = (From c In portalSettings.DesktopTabs Where c.TabId = _parenttabs.ParentId And c.PortalId = portalSettings.PortalId Order By c.Sortpath).FirstOrDefault()
    '            'parenttabId = _m_tabs.TabId
    '            parenttabname = _m_tabs.TabName

    '        End If
    '    Else
    '        Dim _m_tabs = (From c In portalSettings.DesktopTabs Where c.TabId = tabId And c.PortalId = portalSettings.PortalId Order By c.Sortpath).FirstOrDefault()
    '        If Not _m_tabs Is Nothing Then
    '            'parenttabId = _m_tabs.TabId
    '            parenttabname = _m_tabs.TabName
    '        End If
    '    End If



    '    Dim sbMenu As New StringBuilder()

    '    Dim _menuObj = (From c In _tabs Where c.ParentId = 1).ToList()
    '    For Each _menu In _menuObj 'mainmenu
    '        If PortalSecurity.IsInRoles(_menu.AuthorizedRoles, _menu.TabId) And _menu.AuthorizedRoles.Count > 0 Then

    '            sbMenu.Append("<li ><span><a href='#' ><img src='../img/star_list.png'>" & _menu.TabName & "</a></span>")
    '            Dim _submenuObj = (From c In _tabs Where c.ParentId = _menu.TabId).ToList()
    '            If _submenuObj.Count > 0 Then
    '                sbMenu.Append("<ul class='sublist'>")
    '                sbMenu.Append(GenSubMenuV2(_menu.TabId, _submenuObj, _tabs))
    '                sbMenu.Append("</ul>")
    '            End If
    '            sbMenu.Append("</li>")
    '        End If
    '    Next






    '    mainMenu = sbMenu.ToString()

    'End Sub




    'Private Function GenSubMenuV2(ByVal parentId As Integer, ByVal _submenuObj As List(Of DesktopTab), ByVal _tabs As List(Of DesktopTab)) As String
    '    Dim sbSubMenu As New StringBuilder()
    '    For Each _submenu In _submenuObj 'submenu
    '        If PortalSecurity.IsInRoles(_submenu.AuthorizedRoles, _submenu.TabId) And _submenu.AuthorizedRoles.Count > 0 Then
    '            Dim _subsubmenuObj = (From c In _tabs Where c.ParentId = _submenu.TabId).ToList()
    '            If _subsubmenuObj.Count > 0 Then
    '                sbSubMenu.Append("<li>" & _submenu.TabName & "<span class='arrow'></span>")
    '                sbSubMenu.Append("<ul class='sublist-menu'>")
    '                sbSubMenu.Append(GenSubMenuV2(_submenu.TabId, _subsubmenuObj, _tabs))
    '                sbSubMenu.Append("</ul>")
    '                sbSubMenu.Append("</li><li class='divider'></li>")
    '            Else
    '                sbSubMenu.Append("<li><a onclick=""LoadingPanel.Show();"" href='" + String.Format("DesktopDefault.aspx?pageid={0}", _submenu.PageID.ToString()) + "'>" + _submenu.TabName + "</a></li><li class='divider'></li>")
    '            End If
    '        End If
    '    Next

    '    Return sbSubMenu.ToString()
    'End Function



    'Public Sub GenDynamicMenu()

    '    Dim portalSettings As PortalSettings = CType(Context.Items(ConfigurationSettings.AppSettings("PortalContextName")), PortalSettings)
    '    SiteName = portalSettings.PortalName

    '    Dim _tabs = (From c In portalSettings.DesktopTabs Where Not c.ParentId Is Nothing And c.PortalId = portalSettings.PortalId Order By c.Sortpath).ToList()
    '    Dim _currtab = (From c In portalSettings.DesktopTabs Where c.PageID = PageID And c.ParentId IsNot Nothing And c.PortalId = portalSettings.PortalId Order By c.Sortpath).FirstOrDefault()

    '    parenttabname = ""


    '    If Not _currtab Is Nothing Then
    '        Dim parentname = GetSiteMap(_currtab, _tabs)

    '        If Not String.IsNullOrEmpty(parentname) Then
    '            Dim _tempname = parentname.Split(";")

    '            For i As Integer = _tempname.Count - 1 To 0 Step -1

    '                If Not String.IsNullOrEmpty(_tempname(i)) Then
    '                    parenttabname = _tempname(i) & " <img src='css/skins/images/arrow_off.png' /> " & parenttabname
    '                End If

    '            Next

    '            parenttabname = parenttabname & ("<a style='color: blue;' onclick=""LoadingPanel.Show();"" href='" + String.Format("DesktopDefault.aspx?pageid={0}", _currtab.PageID.ToString()) + "'>" + _currtab.TabName + "</a>")
    '        Else
    '            parenttabname = ("<a style='color: blue;' onclick=""LoadingPanel.Show();"" href='" + String.Format("DesktopDefault.aspx?pageid={0}", _currtab.PageID.ToString()) + "'>" + _currtab.TabName + "</a>")
    '        End If




    '        'If _parenttabs.ParentId = 1 Then
    '        '    'parenttabId = _parenttabs.TabId
    '        '    parenttabname = _parenttabs.TabName
    '        'Else
    '        '    Dim _m_tabs = (From c In portalSettings.DesktopTabs Where c.TabId = _parenttabs.ParentId And c.PortalId = portalSettings.PortalId Order By c.Sortpath).FirstOrDefault()
    '        '    'parenttabId = _m_tabs.TabId
    '        '    parenttabname = _m_tabs.TabName

    '        'End If
    '        'Else
    '        '    Dim _m_tabs = (From c In portalSettings.DesktopTabs Where c.TabId = tabId And c.PortalId = portalSettings.PortalId Order By c.Sortpath).FirstOrDefault()
    '        '    If Not _m_tabs Is Nothing Then
    '        '        'parenttabId = _m_tabs.TabId
    '        '        parenttabname = _m_tabs.TabName
    '        '    End If
    '    End If



    '    Dim sbMenu As New StringBuilder()

    '    Dim _menuObj = (From c In _tabs Where c.ParentId = 1).ToList()
    '    For Each _menu In _menuObj 'mainmenu
    '        If PortalSecurity.IsInRoles(_menu.AuthorizedRoles, _menu.TabId) And _menu.AuthorizedRoles.Count > 0 Then

    '            sbMenu.Append("<li><a href='#'>" & _menu.TabName & "</a>")
    '            Dim _submenuObj = (From c In _tabs Where c.ParentId = _menu.TabId).ToList()
    '            If _submenuObj.Count > 0 Then
    '                sbMenu.Append("<ul>")

    '                sbMenu.Append(GenSubMenu(_menu.TabId, _submenuObj, _tabs))
    '                'For Each _submenu In _submenuObj 'submenu
    '                '    If PortalSecurity.IsInRoles(_submenu.AccessRoles) And Not String.IsNullOrEmpty(_submenu.AccessRoles) Then
    '                '        Dim _subsubmenuObj = (From c In _tabs Where c.ParentId = _submenu.TabId).ToList()
    '                '        If _subsubmenuObj.Count > 0 Then
    '                '            sbMenu.Append("<li><a href='#'>" & _submenu.TabName & "</a>")
    '                '            sbMenu.Append("<ul>")
    '                '            For Each _subsubmenu In _subsubmenuObj 'submenu
    '                '                If PortalSecurity.IsInRoles(_subsubmenu.AccessRoles) And Not String.IsNullOrEmpty(_subsubmenu.AccessRoles) Then
    '                '                    sbMenu.Append("<li><a onclick=""LoadingPanel.Show();"" href='" + String.Format("DesktopDefault.aspx?tabindex={0}&tabid={1}", _subsubmenu.TabOrder.ToString(), _subsubmenu.TabId.ToString()) + "'>" + _subsubmenu.TabName + "</a></li>")
    '                '                End If
    '                '            Next
    '                '            sbMenu.Append("</ul>")
    '                '            sbMenu.Append("</li>")
    '                '        Else
    '                '            sbMenu.Append("<li><a onclick=""LoadingPanel.Show();"" href='" + String.Format("DesktopDefault.aspx?tabindex={0}&tabid={1}", _submenu.TabOrder.ToString(), _submenu.TabId.ToString()) + "'>" + _submenu.TabName + "</a></li>")
    '                '        End If
    '                '    End If
    '                'Next



    '                sbMenu.Append("</ul>")
    '            End If
    '            sbMenu.Append("</li>")
    '        End If
    '    Next






    '    mainMenu = sbMenu.ToString()

    'End Sub
    'Private Function GenSubMenu(ByVal parentId As Integer, ByVal _submenuObj As List(Of DesktopTab), ByVal _tabs As List(Of DesktopTab)) As String
    '    Dim sbSubMenu As New StringBuilder()
    '    For Each _submenu In _submenuObj 'submenu
    '        If PortalSecurity.IsInRoles(_submenu.AuthorizedRoles, _submenu.TabId) And _submenu.AuthorizedRoles.Count > 0 Then
    '            Dim _subsubmenuObj = (From c In _tabs Where c.ParentId = _submenu.TabId).ToList()
    '            If _subsubmenuObj.Count > 0 Then
    '                sbSubMenu.Append("<li><a href='#'>" & _submenu.TabName & "</a>")
    '                sbSubMenu.Append("<ul>")
    '                'For Each _subsubmenu In _subsubmenuObj 'submenu
    '                '    If PortalSecurity.IsInRoles(_subsubmenu.AccessRoles) And Not String.IsNullOrEmpty(_subsubmenu.AccessRoles) Then
    '                '        sbSubMenu.Append("<li><a onclick=""LoadingPanel.Show();"" href='" + String.Format("DesktopDefault.aspx?tabindex={0}&tabid={1}", _subsubmenu.TabOrder.ToString(), _subsubmenu.TabId.ToString()) + "'>" + _subsubmenu.TabName + "</a></li>")
    '                '    End If
    '                'Next
    '                sbSubMenu.Append(GenSubMenu(_submenu.TabId, _subsubmenuObj, _tabs))

    '                sbSubMenu.Append("</ul>")
    '                sbSubMenu.Append("</li>")
    '            Else
    '                sbSubMenu.Append("<li><a  onclick=""LoadingPanel.Show();"" href='" + String.Format("DesktopDefault.aspx?pageid={0}", _submenu.PageID.ToString()) + "'>" + _submenu.TabName + "</a></li>")
    '            End If
    '        End If
    '    Next

    '    Return sbSubMenu.ToString()
    'End Function



    'Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

    '    'ASPxWebControl.SetIECompatibilityMode(8, Me.Page)

    '    'Dim roles() As String
    '    Dim projectRoles As Hashtable
    '    Dim custom As ICustomPrincipal
    '    If Request.IsAuthenticated Then
    '        If Not (Session("CustomPrincipal") Is Nothing) Then
    '            HttpContext.Current.User = CType(Session("CustomPrincipal"), ICustomPrincipal)
    '        Else

    '            Dim users As New UsersDB
    '            projectRoles = ProjectMember.GetProjectRoles()

    '            If TypeOf HttpContext.Current.User.Identity Is System.Security.Principal.WindowsIdentity Then
    '                custom = New CustomWindowsPrincipal(CType(Context.User.Identity, System.Security.Principal.WindowsIdentity), projectRoles)
    '            Else
    '                Dim _roles = users.GetRoles(User.Identity.Name)
    '                custom = New CustomFormsPrincipal(Context.User.Identity, _roles.Select(Function(x) x.RoleCode).ToArray(), projectRoles)
    '            End If


    '            HttpContext.Current.User = custom
    '            Session("CustomPrincipal") = custom
    '        End If
    '    End If
    'End Sub

End Class