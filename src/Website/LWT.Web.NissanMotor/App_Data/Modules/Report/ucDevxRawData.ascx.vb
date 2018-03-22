﻿Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports DevExpress.Web
Imports DevExpress.Web.Rendering
Imports DevExpress.Web.Data

Partial Class Modules_ucDevxRawData
    Inherits PortalModuleControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        If (Not IsPostBack) Then

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
        SqlDataSource_gridData.SelectCommand = String.Format("SELECT ROW_NUMBER() OVER(ORDER BY DateClosing) AS [RowNo],CusTitle + CusName + ' ' + CusSurname as InsuredName,case when [AppStatus] = '1' then 'TN' when [AppStatus] = '2' then 'DC' when [AppStatus] = '3' then 'CL' when [AppStatus] = '4' then 'PN'when [AppStatus] = '7' then 'C' end as [Status],  * FROM vw_Bi111 where  convert(varchar," & FilterDateType.Value & ",112) between '{0}' and '{1}' ", DirectCast(DateFrom.Value, Date).ToString("yyyyMMdd"), DirectCast(DateTo.Value, Date).ToString("yyyyMMdd"))
        SqlDataSource_gridData.DataBind()

        gridData.DataSourceID = "SqlDataSource_gridData"
        gridData.DataBind()



    End Sub

End Class