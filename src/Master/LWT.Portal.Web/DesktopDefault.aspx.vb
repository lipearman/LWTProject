Imports Portal.Components
Imports System.Security
Imports System.Data
Imports DevExpress.Web
Public Class DesktopDefault
    Inherits BasePage

    'Protected tabIndex As Integer = 0
    'Protected tabId As Integer = 0
    'Protected parenttabId As Integer = 0
    Protected parenttabname As String = ""
    Protected parenttablink As String = ""
    Protected tabname As String = ""
    Protected SiteName As String
    Protected mainMenu As String
    Protected subMenu As String
    Protected portalTabs As ArrayList
    Protected PageID As String = ""
    Protected UserName As String = ""

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        '' Get TabIndex from querystring
        'If Not Request.Params("tabindex") Is Nothing Then tabIndex = Int32.Parse(Request.Params("tabindex"))
        ''Get TabID from querystring
        'If Not Request.Params("tabid") Is Nothing Then tabId = Int32.Parse(Request.Params("tabid"))

        If Not Request.Params("pageid") Is Nothing Then PageID = Request.Params("pageid")

        'generateDynamicMenu()

        GenDynamicMenu()
        'generateDynamicSubMenu()
        generateDynamicControl()
    End Sub

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        UserName = IIf(String.IsNullOrEmpty(HttpContext.Current.User.Identity.Name), "Guest", HttpContext.Current.User.Identity.Name)

    End Sub

    Private Sub generateDynamicControl()
        ''*********************************************************************
        '' Obtain PortalSettings from Current Context
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
        'ContentPanel1.Title = portalSettings.ActiveTab.TabName
        ' Ensure that the visiting user has access to the current page


        'Dim tabIndex As Integer = 0
        'Dim tabId As Integer = ConfigurationSettings.AppSettings("DefaultTabId")

        '' Get TabIndex from querystring
        'If Not (Request.Params("tabindex") Is Nothing) Then
        '    tabIndex = Int32.Parse(Request.Params("tabindex"))
        'End If

        '' Get TabID from querystring
        'If Not (Request.Params("tabid") Is Nothing) And Request.Params("tabid") <> "" Then
        '    tabId = Int32.Parse(Request.Params("tabid"))
        'End If


        'If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles) = True Then
        '    'PageContext.Redirect(String.Format("~/Admin/AccessDenied.aspx?tabId={0}&tabIndex={1}", tabId, tabIndex))

        '    Response.Redirect("~/Admin/AccessDenied.aspx")
        '    'Window1.Hidden = True
        'Else
        '    'PageManager1.EnableAjax = False
        '    'Window1.Hidden = False
        '    'Window1.IsModal = True
        'End If

        '' Dynamically inject a signin login module into the top left-hand corner
        '' of the home page if the client is not yet authenticated
        ''LeftPane.Controls.Add(Page.LoadControl("~/Modules/modulemenu.ascx"))
        ''LeftPane.Visible = True
        'If Request.IsAuthenticated = False And portalSettings.ActiveTab.TabIndex = 0 Then
        '    'ContentPane.Controls.AddAt(0, Page.LoadControl("~/Modules/SignIn.ascx"))
        '    ContentPane.Controls.Add(Page.LoadControl("~/Modules/SignIn.ascx"))
        '    ContentPane.Visible = True
        'Else

        'End If

        ' Dynamically Populate the Left, Center and Right pane sections of the portal page
        If portalSettings.ActiveTab.Modules.Count > 0 Then




            ' Loop through each entry in the configuration system for this tab
            Dim _moduleSettings As ModuleSettings
            For Each _moduleSettings In portalSettings.ActiveTab.Modules

                If PortalSecurity.IsInRoles(_moduleSettings.AuthorizedEditRoles, _moduleSettings.TabId) Then



                    'Dim parent As ContentPanel = Page.FindControl("ContentPanel1")

                    ' If no caching is specified, create the user control instance and dynamically
                    ' inject it into the page.  Otherwise, create a cached module instance that
                    ' may or may not optionally inject the module into the tree
                    If _moduleSettings.CacheTimeOut = 0 Then

                        If Not String.IsNullOrEmpty(_moduleSettings.DesktopSourceFile) Then
                            Dim _ascx = String.Format("~/App_Data/Modules/{0}", _moduleSettings.DesktopSourceFile)
                            If System.IO.File.Exists(Server.MapPath(_ascx)) Then

                                Dim portalModule As PortalModuleControl = CType(Page.LoadControl(_ascx), PortalModuleControl)

                                portalModule.PortalId = portalSettings.PortalId
                                portalModule.ModuleConfiguration = _moduleSettings

                                ContentPanel1.Controls.Add(portalModule)
                            End If
                        End If
                    Else

                        Dim portalModule As New CachedPortalModuleControl

                        portalModule.PortalId = portalSettings.PortalId
                        portalModule.ModuleConfiguration = _moduleSettings

                        ContentPanel1.Controls.Add(portalModule)
                    End If

                    ' Dynamically inject separator break between portal modules
                    ContentPanel1.Controls.Add(New LiteralControl("<" + "br" + ">"))
                    ContentPanel1.Visible = True


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

    Public Sub GenDynamicMenu()

        Dim portalSettings As PortalSettings = CType(Context.Items(ConfigurationSettings.AppSettings("PortalContextName")), PortalSettings)
        SiteName = portalSettings.PortalName
        Dim _tabs = (From c In portalSettings.DesktopTabs Where Not c.ParentId Is Nothing And c.PortalId = portalSettings.PortalId Order By c.Sortpath).ToList()

        Dim _currtab = (From c In portalSettings.DesktopTabs Where c.PageID = PageID And c.ParentId IsNot Nothing And c.PortalId = portalSettings.PortalId Order By c.Sortpath).FirstOrDefault()

        parenttabname = ""


        If Not _currtab Is Nothing Then
            Dim parentname = GetSiteMap(_currtab, _tabs)

            If Not String.IsNullOrEmpty(parentname) Then
                Dim _tempname = parentname.Split(";")

                For i As Integer = _tempname.Count - 1 To 0 Step -1

                    If Not String.IsNullOrEmpty(_tempname(i)) Then
                        parenttabname = _tempname(i) & " <img src='css/skins/images/arrow_off.png' /> " & parenttabname
                    End If

                Next

                parenttabname = parenttabname & ("<a style='color: blue;' onclick=""LoadingPanel.Show();"" href='" + String.Format("DesktopDefault.aspx?pageid={0}", _currtab.PageID.ToString()) + "'>" + _currtab.TabName + "</a>")
            Else
                parenttabname = ("<a style='color: blue;' onclick=""LoadingPanel.Show();"" href='" + String.Format("DesktopDefault.aspx?pageid={0}", _currtab.PageID.ToString()) + "'>" + _currtab.TabName + "</a>")
            End If




            'If _parenttabs.ParentId = 1 Then
            '    'parenttabId = _parenttabs.TabId
            '    parenttabname = _parenttabs.TabName
            'Else
            '    Dim _m_tabs = (From c In portalSettings.DesktopTabs Where c.TabId = _parenttabs.ParentId And c.PortalId = portalSettings.PortalId Order By c.Sortpath).FirstOrDefault()
            '    'parenttabId = _m_tabs.TabId
            '    parenttabname = _m_tabs.TabName

            'End If
            'Else
            '    Dim _m_tabs = (From c In portalSettings.DesktopTabs Where c.TabId = tabId And c.PortalId = portalSettings.PortalId Order By c.Sortpath).FirstOrDefault()
            '    If Not _m_tabs Is Nothing Then
            '        'parenttabId = _m_tabs.TabId
            '        parenttabname = _m_tabs.TabName
            '    End If
        End If



        Dim sbMenu As New StringBuilder()

        Dim _menuObj = (From c In _tabs Where c.ParentId = 1).ToList()
        For Each _menu In _menuObj 'mainmenu
            If PortalSecurity.IsInRoles(_menu.AuthorizedRoles, _menu.TabId) And _menu.AuthorizedRoles.Count > 0 Then

                sbMenu.Append("<li><a href='#'>" & _menu.TabName & "</a>")
                Dim _submenuObj = (From c In _tabs Where c.ParentId = _menu.TabId).ToList()
                If _submenuObj.Count > 0 Then
                    sbMenu.Append("<ul>")

                    sbMenu.Append(GenSubMenu(_menu.TabId, _submenuObj, _tabs))
                    'For Each _submenu In _submenuObj 'submenu
                    '    If PortalSecurity.IsInRoles(_submenu.AccessRoles) And Not String.IsNullOrEmpty(_submenu.AccessRoles) Then
                    '        Dim _subsubmenuObj = (From c In _tabs Where c.ParentId = _submenu.TabId).ToList()
                    '        If _subsubmenuObj.Count > 0 Then
                    '            sbMenu.Append("<li><a href='#'>" & _submenu.TabName & "</a>")
                    '            sbMenu.Append("<ul>")
                    '            For Each _subsubmenu In _subsubmenuObj 'submenu
                    '                If PortalSecurity.IsInRoles(_subsubmenu.AccessRoles) And Not String.IsNullOrEmpty(_subsubmenu.AccessRoles) Then
                    '                    sbMenu.Append("<li><a onclick=""LoadingPanel.Show();"" href='" + String.Format("DesktopDefault.aspx?tabindex={0}&tabid={1}", _subsubmenu.TabOrder.ToString(), _subsubmenu.TabId.ToString()) + "'>" + _subsubmenu.TabName + "</a></li>")
                    '                End If
                    '            Next
                    '            sbMenu.Append("</ul>")
                    '            sbMenu.Append("</li>")
                    '        Else
                    '            sbMenu.Append("<li><a onclick=""LoadingPanel.Show();"" href='" + String.Format("DesktopDefault.aspx?tabindex={0}&tabid={1}", _submenu.TabOrder.ToString(), _submenu.TabId.ToString()) + "'>" + _submenu.TabName + "</a></li>")
                    '        End If
                    '    End If
                    'Next



                    sbMenu.Append("</ul>")
                End If
                sbMenu.Append("</li>")
            End If
        Next






        mainMenu = sbMenu.ToString()

    End Sub




    Private Function GenSubMenu(ByVal parentId As Integer, ByVal _submenuObj As List(Of DesktopTab), ByVal _tabs As List(Of DesktopTab)) As String
        Dim sbSubMenu As New StringBuilder()
        For Each _submenu In _submenuObj 'submenu
            If PortalSecurity.IsInRoles(_submenu.AuthorizedRoles, _submenu.TabId) And _submenu.AuthorizedRoles.Count > 0 Then
                Dim _subsubmenuObj = (From c In _tabs Where c.ParentId = _submenu.TabId).ToList()
                If _subsubmenuObj.Count > 0 Then
                    sbSubMenu.Append("<li><a href='#'>" & _submenu.TabName & "</a>")
                    sbSubMenu.Append("<ul>")
                    'For Each _subsubmenu In _subsubmenuObj 'submenu
                    '    If PortalSecurity.IsInRoles(_subsubmenu.AccessRoles) And Not String.IsNullOrEmpty(_subsubmenu.AccessRoles) Then
                    '        sbSubMenu.Append("<li><a onclick=""LoadingPanel.Show();"" href='" + String.Format("DesktopDefault.aspx?tabindex={0}&tabid={1}", _subsubmenu.TabOrder.ToString(), _subsubmenu.TabId.ToString()) + "'>" + _subsubmenu.TabName + "</a></li>")
                    '    End If
                    'Next
                    sbSubMenu.Append(GenSubMenu(_submenu.TabId, _subsubmenuObj, _tabs))

                    sbSubMenu.Append("</ul>")
                    sbSubMenu.Append("</li>")
                Else
                    sbSubMenu.Append("<li><a  onclick=""LoadingPanel.Show();"" href='" + String.Format("DesktopDefault.aspx?pageid={0}", _submenu.PageID.ToString()) + "'>" + _submenu.TabName + "</a></li>")
                End If
            End If
        Next

        Return sbSubMenu.ToString()
    End Function





    Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit

        'ASPxWebControl.SetIECompatibilityMode(8, Me.Page)

        'Dim roles() As String
        Dim projectRoles As Hashtable
        Dim custom As ICustomPrincipal
        If Request.IsAuthenticated Then
            If Not (Session("CustomPrincipal") Is Nothing) Then
                HttpContext.Current.User = CType(Session("CustomPrincipal"), ICustomPrincipal)
            Else

                Dim users As New UsersDB
                projectRoles = ProjectMember.GetProjectRoles()

                If TypeOf HttpContext.Current.User.Identity Is System.Security.Principal.WindowsIdentity Then
                    custom = New CustomWindowsPrincipal(CType(Context.User.Identity, System.Security.Principal.WindowsIdentity), projectRoles)
                Else
                    Dim _roles = users.GetRoles(User.Identity.Name)
                    custom = New CustomFormsPrincipal(Context.User.Identity, _roles.Select(Function(x) x.RoleCode).ToArray(), projectRoles)
                End If


                HttpContext.Current.User = custom
                Session("CustomPrincipal") = custom
            End If
        End If
    End Sub

End Class