
Imports Microsoft.Reporting.WebForms
Imports Portal.Components

Partial Class Default2
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        Dim ReportFile = "rptAPDRenewalList.rdl"
        Dim ds = SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("PortalBIRawDataConnection").ConnectionString, Data.CommandType.Text, "select * from tblAPDRenewalList_SUMMARY")

        ReportViewer1.Reset()
        ReportViewer1.LocalReport.Dispose()
        ReportViewer1.LocalReport.DataSources.Clear()
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("/reports/" & ReportFile)
        ReportViewer1.LocalReport.DataSources.Add(New ReportDataSource("DataSet1", ds.Tables(0)))
        ReportViewer1.LocalReport.SetParameters(New ReportParameter("FiscalYear", "2018"))
        ReportViewer1.LocalReport.Refresh()


    End Sub
End Class
