Imports Portal.Components
Imports System.Security
Imports System.Data
Imports DevExpress.Web
Imports LWT.Website
Imports DevExpress.Web.ASPxTreeList


Public Class DesktopDefault
    Inherits BasePage
    Protected SiteName As String
    Protected NavigateBar As String
    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            ASPxWebControl.RedirectOnCallback("~/Admin/AccessDenied.aspx")
        End If
 
        SiteName = portalSettings.PortalName
 
        sidebar()

        generateDynamicControl()

    End Sub
    Private Sub sidebar()
        Dim portalSettings As PortalSettings = CType(Context.Items(ConfigurationSettings.AppSettings("PortalContextName")), PortalSettings)

        Using dc As New DataClasses_PortalDataContextExt()
            Dim _tabs = (From c In dc.v_UserTabs Where c.PortalId = PortalSettings.PortalId And c.UserName.ToLower() = HttpContext.Current.User.Identity.Name.ToLower() Order By c.Sortpath, c.TabOrder).ToList()
            Dim _menuObj = (From c In _tabs Where c.ParentId = 1).ToList()
            For Each _menu In _menuObj 'mainmenu
                'Dim _GroupNode = navTreeView.Nodes.Add(_menu.TabName, _menu.TabId, "icon icon-settings", "javascript:void(0);")
                'Dim _GroupNode = navTreeView.Nodes.Add(_menu.TabName, _menu.TabId, "icon icon-settings", ResolveUrl("DesktopDefault.aspx?PageId=" & _menu.PageId))
                Dim _GroupNode = navTreeView.Nodes.Add(_menu.TabName, _menu.TabId, "icon icon-settings", _menu.TabName & ".html")


                Dim _subMenuHeader = (From c In _tabs Where c.ParentId = _menu.TabId).ToList()
                For Each item In _subMenuHeader
                    _GroupNode.Nodes.Add(item.TabName, item.TabId, "", item.TabName & ".html")
                    '_GroupNode.Nodes.Add(item.TabName, item.TabId, "", ResolveUrl("DesktopDefault.aspx?PageId=" & item.PageId))
                    '_GroupNode.Nodes.Add(item.TabName, item.TabId, "", "javascript:alert('" & item.PageId & "');")
                Next
            Next

            NavigateBar = dc.ExecuteQuery(Of String)("select top 1 Navigation from V_PageID where PageID='" & portalSettings.ActiveTab.PageID & "'").FirstOrDefault()

        End Using

        navTreeView.ExpandAll()
        navTreeView.SelectedNode = navTreeView.Nodes.FindAllRecursive(Function(n) ResolveUrl(n.NavigateUrl).Equals(Request.Url.LocalPath, StringComparison.InvariantCultureIgnoreCase)).FirstOrDefault()
        If navTreeView.SelectedNode IsNot Nothing AndAlso navTreeView.SelectedNode.Parent IsNot Nothing Then
            navTreeView.SelectedNode.Parent.CssClass = "active-parent"
        End If


    End Sub
     

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


    'Protected Sub btnLogout_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnLogout.Click
    '    FormsAuthentication.SignOut()
    '    Session("CustomPrincipal") = Nothing
    '    HttpContext.Current.Response.Clear()
    '    Response.Redirect("~/Default.aspx")
    'End Sub
    Protected Sub cbLoginOut_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbLoginOut.Callback

        FormsAuthentication.SignOut()
        Session("CustomPrincipal") = Nothing
        HttpContext.Current.Response.Clear()

        ASPxWebControl.RedirectOnCallback("~/Default.aspx")

    End Sub
End Class