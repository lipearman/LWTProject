Imports Portal.Components
Imports DevExpress.Web.ASPxTreeList
Imports DevExpress.Web
Imports System.Data.SqlClient
Imports System.Data
Imports DevExpress.Web.Data
Imports System.IO
Imports DevExpress.DataAccess.Excel
Imports DevExpress.XtraPivotGrid
Imports DevExpress.Web.ASPxPivotGrid
Imports System.Web.Hosting

Imports DevExpress.XtraPrinting
Imports DevExpress.Export

Partial Class Modules_ucDevxLWTRenewalRawdata
    Inherits PortalModuleControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(LWT.Website.webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If

        pnMain.HeaderText = portalSettings.ActiveTab.TabName
        pnMain.HeaderImage.IconID = portalSettings.ActiveTab.IconID



    End Sub

    Protected Sub Grid_ToolbarItemClick(ByVal source As Object, ByVal e As ASPxGridToolbarItemClickEventArgs) Handles gridRawData.ToolbarItemClick

        Select Case e.Item.Name
            Case "ExportToXLS"
                GridExporter.WriteXlsToResponse("RawData.xls", New XlsExportOptionsEx() With {.ExportType = ExportType.Default})
            Case "ExportToXLSX"
                GridExporter.WriteXlsxToResponse("RawData.xlsx", New XlsxExportOptionsEx() With {.ExportType = ExportType.Default})
            Case Else
        End Select
    End Sub
End Class


