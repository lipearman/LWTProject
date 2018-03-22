Imports System.Data
Imports System.Web.Security
Imports Portal.Components

Imports NPOI.HSSF.UserModel
Imports NPOI.HPSF
Imports NPOI.SS.UserModel
Imports NPOI.SS.Util
Imports NPOI.HSSF.Util
Imports System.Drawing

Imports DevExpress.Web
Imports DevExpress.Web.ASPxHtmlEditor
Imports System.Net.Mail
Imports LWT.Website

Partial Class Modules_ucLWTShowRoom
    Inherits PortalModuleControl 
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load


        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If
        If (Not IsPostBack) Then
            pnMain.HeaderText = portalSettings.ActiveTab.TabName
            SqlDataSource_NoticeMailContact.UpdateParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name
            SqlDataSource_NoticeMailContact.InsertParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name


        End If
    End Sub
     
 
    Protected Sub btnExportData_Click(sender As Object, e As EventArgs)
        GridExporter.WriteXlsxToResponse(System.Guid.NewGuid().ToString(), False, New DevExpress.XtraPrinting.XlsxExportOptionsEx With {.ExportType = DevExpress.Export.ExportType.WYSIWYG})
    End Sub
End Class
