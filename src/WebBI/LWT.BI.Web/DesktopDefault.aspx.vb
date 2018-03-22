Imports Portal.Components
Imports System.Security
Imports System.Data
Imports DevExpress.Web
Imports LWT.Website
'Imports FineUI
Imports DevExpress.Web.ASPxTreeList


Public Class DesktopDefault
    Inherits BasePage
    Protected SiteName As String
    Protected topMenu As String
    Protected dlMenu As String
    Protected subMenu As String
    Protected UserName As String = ""
    Protected PageName As String = ""

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
        ''If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
        ''    ASPxWebControl.RedirectOnCallback("~/Admin/AccessDenied.aspx")
        ''End If

        ''SqlDataSource_Tabs.SelectParameters("PortalId").DefaultValue = portalSettings.PortalId
        ''SqlDataSource_Tabs.SelectParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name
        'SiteName = portalSettings.PortalName


        ''If ViewState("PageID") Is Nothing Then
        ''    ViewState("PageID") = webconfig._DefaultPageID
        ''End If
        UserName = IIf(String.IsNullOrEmpty(HttpContext.Current.User.Identity.Name), "Guest", HttpContext.Current.User.Identity.Name)

        GenTopMenu()
        generateDynamicControl()

    End Sub
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'UserName = IIf(String.IsNullOrEmpty(HttpContext.Current.User.Identity.Name), "Guest", HttpContext.Current.User.Identity.Name)
        'If Page.IsPostBack = False Then
        '    'BuildMenu(ASPxMenu1, SqlDataSource_Tabs)
        '    GenTopMenu()
        '    generateDynamicControl()
        'End If
    End Sub

    Public Sub GenTopMenu()
        Dim sbTopMenu As New StringBuilder()
        Dim sbDlMenu As New StringBuilder()
        Dim sbSubMenu As New StringBuilder()
        Dim idx As Integer = 0


        Dim strSubMenuStartTag As String = ""
        Dim strSubMenuEndTag As String = ""

        Dim portalSettings As PortalSettings = CType(Context.Items(ConfigurationSettings.AppSettings("PortalContextName")), PortalSettings)
        SiteName = portalSettings.PortalName
        txtSiteName.Text = portalSettings.PortalName

        PageName = portalSettings.ActiveTab.TabName
        'Dim _tabs = (From c In portalSettings.DesktopTabs Where Not c.ParentId Is Nothing And c.PortalId = portalSettings.PortalId Order By c.Sortpath).ToList()

        Using dc As New DataClasses_PortalDataContextExt()
            Dim _tabs = (From c In dc.v_UserTabs Where c.PortalId = portalSettings.PortalId And c.UserName.ToLower() = HttpContext.Current.User.Identity.Name.ToLower() Order By c.Sortpath, c.TabOrder).ToList()


            Dim _menuObj = (From c In _tabs Where c.ParentId = 1).ToList()
            For Each _menu In _menuObj 'mainmenu
                '============================ top and dl menu =============================
                idx += 1
                sbTopMenu.AppendLine("<li class=""menu""><a href=""DesktopDefault.aspx?PageId=" & _menu.PageId & """ class=""menu"" id=""" & idx.ToString() & """ title=""" & _menu.TabName & """>" & _menu.TabName & "</a></li>")
                sbDlMenu.AppendLine("<li><a href=""#"" >" & _menu.TabName & "</a></li>")


                '============================ SubMenu level1 =============================
                Dim _subMenuHeader = (From c In _tabs Where c.ParentId = _menu.TabId).ToList()
                Dim sbSubMenuL2 As New StringBuilder()

                If _subMenuHeader.Count > 0 Then



                    Dim idx_sub As Integer = 0

                    For Each _subHeader In _subMenuHeader 'subHeader
                        '============================ SubMenu level2 =============================
                        Dim _submenu = (From c In _tabs Where c.ParentId = _subHeader.TabId).ToList()
                        If _submenu.Count > 0 Then
                            If idx_sub = 0 Then
                                sbSubMenuL2.AppendLine("<div class=""dropdown""  id=""dropdown" & idx.ToString() & """><div class=""container"">")
                            End If

                            sbSubMenuL2.AppendLine("<div class=""column""><h4>" & _subHeader.TabName & "</h4><ul>")
                            For Each item In _submenu 'submenu
                                sbSubMenuL2.AppendLine("<li style=""font-family: Tahoma, Geneva, Arial, Helvetica, sans-serif;line-height: 180%;""><a href=""DesktopDefault.aspx?PageId=" & item.PageId & """ style=""color:write;text-decoration: none;"">" & item.TabName & "</a></li>")
                            Next
                            sbSubMenuL2.AppendLine("</ul></div>")

                            If idx_sub >= _subMenuHeader.Count - 1 Then
                                sbSubMenuL2.AppendLine("</div></div>")
                            End If
                        Else
                            If idx_sub = 0 Then
                                sbSubMenuL2.AppendLine("<div class=""dropdown""  id=""dropdown" & idx.ToString() & """><div class=""container"">")
                                sbSubMenuL2.AppendLine("<div class=""column""><ul>")
                            End If

                            sbSubMenuL2.AppendLine("<li style=""font-family: Tahoma, Geneva, Arial, Helvetica, sans-serif;line-height: 180%;""><a href=""DesktopDefault.aspx?PageId=" & _subHeader.PageId & """ style=""color:write;text-decoration: none;"">" & _subHeader.TabName & "</a></li>")

                            If idx_sub >= _subMenuHeader.Count - 1 Then
                                sbSubMenuL2.AppendLine("</ul></div></div></div>")
                            End If

                        End If

                        idx_sub += 1
                    Next


                End If
                sbSubMenu.AppendLine(sbSubMenuL2.ToString())
            Next

        End Using





        topMenu = sbTopMenu.ToString()
        dlMenu = sbDlMenu.ToString()
        subMenu = sbSubMenu.ToString()
    End Sub













    Private Sub generateDynamicControl()
        ''*********************************************************************
        '' Obtain PortalSettings from Current Context
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(ConfigurationSettings.AppSettings("PortalContextName")), PortalSettings)
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

                                container.Controls.Add(portalModule)
                            End If
                        End If
                    Else

                        Dim portalModule As New CachedPortalModuleControl

                        portalModule.PortalId = portalSettings.PortalId
                        portalModule.ModuleConfiguration = _moduleSettings

                        container.Controls.Add(portalModule)
                    End If

                    ' Dynamically inject separator break between portal modules
                    container.Controls.Add(New LiteralControl("<" + "br" + ">"))
                    container.Visible = True


                End If

            Next _moduleSettings
        End If
    End Sub


    Protected Sub btnLogout_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogout.Click
        Response.Redirect("~/SignIn.aspx", False)
    End Sub

End Class