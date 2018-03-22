Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports DevExpress.Web
Imports DevExpress.Web.Rendering
Imports DevExpress.Web.Data
Imports LWT.Website

Partial Class Modules_ucGarage
    Inherits PortalModuleControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If (Not IsPostBack) Then
            Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
            If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
                Response.Redirect("~/Admin/AccessDenied.aspx")
            End If
        End If
    End Sub


    Protected Sub btnExportData_Click(sender As Object, e As EventArgs) Handles btnExportData.Click
        GridExporter.WriteXlsxToResponse(Guid.NewGuid().ToString(), False, New DevExpress.XtraPrinting.XlsxExportOptionsEx With {.ExportType = DevExpress.Export.ExportType.WYSIWYG})
    End Sub

End Class
