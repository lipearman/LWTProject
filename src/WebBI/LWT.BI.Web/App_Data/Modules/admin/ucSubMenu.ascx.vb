Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports LWT.Website

Partial Class Modules_ucSubMenu
    Inherits PortalModuleControl
    Public submenu As String

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If




        GenSubMenu()
    End Sub

    Public Sub GenSubMenu()
        Dim sbTopMenu As New StringBuilder()
        Dim sbDlMenu As New StringBuilder()
        Dim sbSubMenu As New StringBuilder()
        Dim idx As Integer = 0


        Dim strSubMenuStartTag As String = ""
        Dim strSubMenuEndTag As String = ""

        Dim portalSettings As PortalSettings = CType(Context.Items(ConfigurationSettings.AppSettings("PortalContextName")), PortalSettings)
  
        'Dim _tabs = (From c In portalSettings.DesktopTabs Where Not c.ParentId Is Nothing And c.PortalId = portalSettings.PortalId Order By c.Sortpath).ToList()

        Using dc As New DataClasses_PortalDataContextExt()
            Dim _tabs = (From c In dc.v_UserTabs Where c.PortalId = portalSettings.PortalId And c.UserName.ToLower() = HttpContext.Current.User.Identity.Name.ToLower() Order By c.Sortpath, c.TabOrder).ToList()


            Dim _menuObj = (From c In _tabs Where c.ParentId = 1 And c.PageId = portalSettings.ActiveTab.PageID).ToList()
            For Each _menu In _menuObj 'mainmenu
                '============================ top and dl menu =============================
                idx += 1


                '============================ SubMenu level1 =============================
                Dim _subMenuHeader = (From c In _tabs Where c.ParentId = _menu.TabId).ToList()
                Dim sbSubMenuL2 As New StringBuilder()

                If _subMenuHeader.Count > 0 Then



                    Dim idx_sub As Integer = 0

                    For Each _subHeader In _subMenuHeader 'subHeader
                        '============================ SubMenu level2 =============================
                        Dim _submenu = (From c In _tabs Where c.ParentId = _subHeader.TabId).ToList()
                        If _submenu.Count > 0 Then
                            'If idx_sub = 0 Then
                            '    sbSubMenuL2.AppendLine("<div class=""dropdown""  id=""dropdown" & idx.ToString() & """><div class=""container"">")
                            'End If

                            sbSubMenuL2.AppendLine("<div class=""column"" style=""margin-bottom: 30px;""><h4 style=""margin-bottom: 12px;font-size: 18px;color: #093373;font-family: ""GothamBold"", Arial, Helvetica, sans-serif;font-size: 16px;text-transform: uppercase;"">" & _subHeader.TabName & "</h4><ul>")
                            For Each item In _submenu 'submenu
                                sbSubMenuL2.AppendLine("<li style=""margin-bottom: 10px;""><a href=""DesktopDefault.aspx?PageId=" & item.PageId & """ style=""color:#6d6d6d;text-decoration: none;"">" & item.TabName & "</a></li>")
                            Next
                            sbSubMenuL2.AppendLine("</ul></div>")

                            'If idx_sub >= _subMenuHeader.Count - 1 Then
                            '    sbSubMenuL2.AppendLine("</div>")
                            'End If
                        Else
                            If idx_sub = 0 Then
                                'sbSubMenuL2.AppendLine("<div class=""dropdown""  id=""dropdown" & idx.ToString() & """><div class=""container"">")
                                sbSubMenuL2.AppendLine("<div class=""column""><ul>")
                            End If

                            sbSubMenuL2.AppendLine("<li style=""line-height: 140%;margin-bottom: 12px;""><a href=""DesktopDefault.aspx?PageId=" & _subHeader.PageId & """ style=""color:#6d6d6d;text-decoration: none;"">" & _subHeader.TabName & "</a></li>")

                            If idx_sub >= _subMenuHeader.Count - 1 Then
                                sbSubMenuL2.AppendLine("</ul></div>")
                            End If

                        End If

                        idx_sub += 1
                    Next


                End If
                sbSubMenu.AppendLine(sbSubMenuL2.ToString())
            Next

        End Using


        submenu = sbSubMenu.ToString()
    End Sub





End Class
