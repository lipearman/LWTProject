Imports Portal.Components
Imports LWT.Website

Partial Class Applications_AppMasterPage
    Inherits System.Web.UI.MasterPage

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(LWT.Website.webconfig._PortalContextName), PortalSettings)
        'If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
        '    Response.Redirect("~/Admin/AccessDenied.aspx")
        'End If
    End Sub


End Class

