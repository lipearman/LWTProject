Imports Portal.Components
Imports DevExpress.Web
Imports DevExpress.Data.Filtering
Imports System.IO
Imports DevExpress.Data.Linq

Partial Class Modules_ucDevxMTRenewTREnquiry
    Inherits PortalModuleControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        'Dim headerFilterMode As HeaderFilterMode
        ''If hFModeCheckBox.Checked Then
        ''headerFilterMode = headerFilterMode.CheckedList
        ''Else
        'headerFilterMode = headerFilterMode.List
        ''End If
        'For Each column As GridViewDataColumn In GridClaim.Columns
        '    column.Settings.HeaderFilterMode = headerFilterMode
        'Next column


        'DevExpress.Web.ASPxThemes.IconID.ExportExporttoxls32x32

        If Not IsPostBack Then
            Session("V_MT_RENEW_TRs") = Nothing
            BindInsurer()
        End If
    End Sub
    Private Sub BindInsurer()
        Dim _dc = New DataClasses_SIBISLWTReportsExt().V_CompanyInsures
        Dim _data = _dc.OrderBy(Function(p) p.CompanyName).ToList()
        CompanyCode.TextField = "CompanyName"
        CompanyCode.ValueField = "CompanyCode"
        CompanyCode.DataSource = _data
        CompanyCode.DataBind()
        CompanyCode.SelectedIndex = 0
    End Sub

    'Protected Sub LinqServerModeDataSource1_Selecting(sender As Object, e As LinqServerModeDataSourceSelectEventArgs) Handles LinqServerModeDataSource1.Selecting
    '    If Session("V_MT_RENEW_TRs") Is Nothing Then
    '        Dim _data = New DataClasses_LWTReports_SIBISDataContext().V_MT_RENEW_TRs
    '        Session("V_MT_RENEW_TRs") = _data.Where(Function(p) p.CompanyCode.Equals(CompanyCode.Value) And p.ClosingDate >= DirectCast(ClosingFrom.Value, Date) And p.ClosingDate <= DirectCast(ClosingTo.Value, Date))
    '    End If


    '    e.QueryableSource = Session("V_MT_RENEW_TRs")

    'End Sub
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click

        'Dim _data = New DataClasses_LWTReports_SIBISDataContext().V_MT_RENEW_TRs
        'Session("V_MT_RENEW_TRs") = _data.Where(Function(p) p.CompanyCode.Equals(CompanyCode.Value) And p.ClosingDate >= DirectCast(ClosingFrom.Value, Date) And p.ClosingDate <= DirectCast(ClosingTo.Value, Date))
        'GridData.DataSource = Session("V_MT_RENEW_TRs")

       binddata()
    End Sub


    'Protected Sub LinqServerModeDataSource1_Selecting(sender As Object, e As EventArgs) Handles GridData.PageIndexChanged
    '   binddata()
    'End Sub
    Protected Sub GridData_Load(sender As Object, e As EventArgs) Handles GridData.Load
        If Not CompanyCode.Value Is Nothing And Not ClosingFrom.Value Is Nothing And Not ClosingTo.Value Is Nothing Then
            binddata()
        End If
    End Sub
    Private Sub binddata()
        LinqServerModeDataSource1.DataSourceMode = SqlDataSourceMode.DataReader

        LinqServerModeDataSource1.SelectCommand = String.Format("SELECT * FROM V_MT_RENEW_TR where CompanyCode='{0}' and convert(varchar,ClosingDate,112) between '{1}' and '{2}'", CompanyCode.Value, DirectCast(ClosingFrom.Value, Date).ToString("yyyyMMdd"), DirectCast(ClosingTo.Value, Date).ToString("yyyyMMdd"))
        LinqServerModeDataSource1.DataBind()



        GridData.DataSourceID = "LinqServerModeDataSource1"
        GridData.DataBind()



    End Sub

    Protected Sub btnExportData_Click(sender As Object, e As EventArgs) Handles btnExportData.Click
        Try
            GridExporter.WriteXlsToResponse(Guid.NewGuid().ToString(), False)
        Catch ex As Exception

        End Try

    End Sub
End Class
