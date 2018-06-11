Imports Portal.Components
Imports System.Security
Imports System.Data
Imports DevExpress.Web
Imports LWT.Website

Partial Class Pages
    Inherits BasePage
    Protected PageID As String = ""
    Protected parenttabname As String = ""


    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            'Response.Redirect("~/Admin/AccessDenied.aspx")
            ASPxWebControl.RedirectOnCallback("~/Admin/AccessDenied.aspx")
        End If


        If Not Request.Params("pageid") Is Nothing Then PageID = Request.Params("pageid")


        generateDynamicControl()

        GenSiteNavigate()
    End Sub
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        
    End Sub
    Private Function GetSiteMap(ByVal _CurrentTab As DesktopTab, ByVal _tabs As List(Of DesktopTab)) As String
        Dim sbTabName As New StringBuilder()
        Dim _ParentTab = (From c In _tabs Where c.TabId = _CurrentTab.ParentId).FirstOrDefault()
        If _ParentTab IsNot Nothing Then
            sbTabName.Append(GetSiteMap(_ParentTab, _tabs))
            sbTabName.Append(_ParentTab.TabName & ";")
        End If
        Return sbTabName.ToString()
    End Function

    Public Sub GenSiteNavigate()

        Dim portalSettings As PortalSettings = CType(Context.Items(ConfigurationSettings.AppSettings("PortalContextName")), PortalSettings)
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
                parenttabname = parenttabname & ("<b>" + _currtab.TabName + "</b>")
            Else
                parenttabname = ("<b>" + _currtab.TabName + "</b>")
            End If
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

End Class
