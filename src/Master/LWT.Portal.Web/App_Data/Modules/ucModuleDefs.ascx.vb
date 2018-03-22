Imports System.Data
Imports System.Web.Security
Imports Portal.Components

Public Class ucModuleDefs
    Inherits PortalModuleControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
        'If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
        '    Response.Redirect("~/Admin/AccessDenied.aspx")
        'End If
    End Sub

End Class