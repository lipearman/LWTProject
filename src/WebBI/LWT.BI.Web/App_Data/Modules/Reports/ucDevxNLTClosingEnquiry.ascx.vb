Imports Portal.Components
Imports DevExpress.Web
Imports DevExpress.Data.Filtering
Imports System.IO
Imports DevExpress.Data.Linq

Partial Class Modules_ucDevxNLTClosingEnquiry
    Inherits PortalModuleControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

    End Sub
    'Private Sub BindInsurer()
    '    Dim _dc = New DataClasses_LWTReport_NLTExt().V_NLT_INSURERs
    '    Dim _data = _dc.OrderBy(Function(p) p.InsurerNameThai).ToList()
    '    CompanyCode.TextField = "InsurerNameThai"
    '    CompanyCode.ValueField = "InsurerCode"
    '    CompanyCode.DataSource = _data
    '    CompanyCode.DataBind()


    '    CompanyCode.Items.Insert(0, New ListEditItem("ทั้งหมด", " "))
    '    CompanyCode.SelectedIndex = 0
    'End Sub

    Protected Sub LinqServerModeDataSource1_Selecting(sender As Object, e As LinqServerModeDataSourceSelectEventArgs) Handles LinqServerModeDataSource1.Selecting

        Dim _data = New DataClasses_NLTDB_LWTReportsExt().V_NLT_Closings
        e.QueryableSource = _data.Where(Function(p) p.ClosingDate >= DirectCast(ClosingFrom.Value, Date) And p.ClosingDate <= DirectCast(ClosingTo.Value, Date))

    End Sub
    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        GridData.DataBind()
    End Sub

    Protected Sub btnExportData_Click(sender As Object, e As EventArgs) Handles btnExportData.Click
        GridExporter.WriteXlsxToResponse(Guid.NewGuid().ToString(), False)
    End Sub
     
End Class
