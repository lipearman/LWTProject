
Imports System.Data.SqlClient
Imports System.IO
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Sql
Imports DevExpress.XtraReports.UI

Partial Class Applications_XtraReports_Master_Preview
    Inherits System.Web.UI.Page

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Init

        'New MyDataSourceWizardConnectionStringsProvider(LWT.Website.webconfig._PortalID)

        'reportDesigner.DataSources.Add(New KeyValuePair(Of String, Object))

        If (Not IsPostBack) Then
            If Session("reportGUID") IsNot Nothing Then

                Dim reportGUID = Session("reportGUID")

                'Using dc As New DataClasses_PortalBIExt()
                '    Dim item = (From c In dc.tblDashBoard_DataSources Where c.PortalId = LWT.Website.webconfig._PortalID And c.DS_ID = 3).FirstOrDefault()

                '    Dim SQLbuilder = New SqlConnectionStringBuilder(item.CONN)
                '    Dim connectionParameters As New MsSqlConnectionParameters(SQLbuilder.DataSource, SQLbuilder.InitialCatalog, SQLbuilder.UserID, SQLbuilder.Password, MsSqlAuthorizationType.SqlServer)

                '    Dim ds As New SqlDataSource(connectionParameters)
                '    Dim Query = New CustomSqlQuery()
                '    Query.Name = item.TITLE
                '    Query.Sql = "SELECT * FROM tblAPDRenewalList_RAWDATA"
                '    ds.Queries.Add(Query)
                '    ds.RebuildResultSchema()
                '    reportDesigner.DataSources.Add(item.TITLE, ds)


                'End Using


                reportDesigner.OpenReport(reportGUID)

                'Dim storage As New CustomReportStorageWebExtension()
                'storage.CanSetData(reportID)
                'Dim layoutBytes As Byte() = storage.GetData(reportID)

                'If layoutBytes IsNot Nothing AndAlso layoutBytes.Length > 0 Then


                '    Dim report As New XtraReport


                '    Using stream As New MemoryStream(layoutBytes)
                '        stream.Seek(0, SeekOrigin.Begin)
                '        report = XtraReport.FromStream(stream, True)
                '    End Using


                '    If (report IsNot Nothing) Then

                '        reportDesigner.OpenReport(reportID)

                '        Dim a = reportDesigner.DesignMode
                '    End If

                'End If
            End If
        End If

    End Sub


End Class
