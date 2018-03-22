Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports DevExpress.Web
Imports DevExpress.Web.Rendering
Imports DevExpress.Web.Data

Partial Class Modules_ucDevxAPDDemoSummitRawData
    Inherits PortalModuleControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(LWT.Website.webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If
    End Sub


    Protected Sub btnExportData_Click(sender As Object, e As EventArgs) Handles btnExportData.Click
        'GridExporter.WriteXlsxToResponse(Guid.NewGuid().ToString(), False)
        GridExporter.WriteXlsxToResponse(Guid.NewGuid().ToString(), False, New DevExpress.XtraPrinting.XlsxExportOptionsEx With {.ExportType = DevExpress.Export.ExportType.WYSIWYG})


    End Sub



    Protected Sub GridData_Load(sender As Object, e As EventArgs) Handles gridData.Load
        If Not DateFrom.Value Is Nothing And Not DateTo.Value Is Nothing Then
            binddata()
        End If
    End Sub
    Private Sub binddata()
        SqlDataSource_gridData.DataSourceMode = SqlDataSourceMode.DataReader
        '============ todo2=======================
        SqlDataSource_gridData.SelectCommand = String.Format("SELECT ROW_NUMBER() OVER(ORDER BY EffectiveDate) AS [RowNo], * FROM SummitRawData where  convert(varchar,EffectiveDate,112) between '{0}' and '{1}' ", DirectCast(DateFrom.Value, Date).ToString("yyyyMMdd"), DirectCast(DateTo.Value, Date).ToString("yyyyMMdd"))
        SqlDataSource_gridData.DataBind()

        gridData.DataSourceID = "SqlDataSource_gridData"
        gridData.DataBind()



    End Sub

End Class
