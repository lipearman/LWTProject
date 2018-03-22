Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports DevExpress.Web
Imports DevExpress.Web.Rendering
Imports DevExpress.Web.Data
Imports LWT.Website
Imports DevExpress.Data.Linq
Imports DevExpress.Data.Filtering

Partial Class Reports_Report_SummitBI_App01
    Inherits PortalModuleControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If


        'If (Not IsPostBack) Then
        '    binddata()
        'End If


    End Sub


    Protected Sub btnExportData_Click(sender As Object, e As EventArgs) ' Handles btnExportData.Click
        'GridExporter.WriteXlsxToResponse(Guid.NewGuid().ToString(), False, New DevExpress.XtraPrinting.XlsxExportOptionsEx With {.ExportType = DevExpress.Export.ExportType.WYSIWYG})

        GridExporter.WriteXlsxToResponse(Guid.NewGuid().ToString(), False, New DevExpress.XtraPrinting.XlsxExportOptionsEx With {.ExportType = DevExpress.Export.ExportType.Default})

    End Sub

    'Protected Sub SqlDataSource_Selecting(ByVal sender As Object, ByVal e As LinqServerModeDataSourceSelectEventArgs) Handles SqlDataSource_gridData.Selecting


    '    Using dc As New DataClasses_LWTReportsExt()


    '        If DateFrom.Value IsNot Nothing And DateTo.Value IsNot Nothing Then

    '            Dim _DateFrom As Date = DateFrom.Value
    '            Dim _DateTo As Date = DateTo.Value

    '            Select Case FilterDateType.SelectedItem.Value
    '                Case "DateMakeContract"
    '                    e.QueryableSource = dc.RawData_SummitBI_App01s.Where(Function(c) c.DateMakeContract >= _DateFrom And c.DateMakeContract < _DateTo.AddDays(1))
    '                Case "DateMakePolicy"
    '                    e.QueryableSource = dc.RawData_SummitBI_App01s.Where(Function(c) c.DateMakePolicy.Value >= _DateFrom And c.DateMakePolicy.Value < _DateTo.AddDays(1))
    '                Case "DateBegin"
    '                    e.QueryableSource = dc.RawData_SummitBI_App01s.Where(Function(c) c.DateBegin.Value >= _DateFrom And c.DateBegin.Value < _DateTo.AddDays(1))
    '                Case "DateEnd"
    '                    e.QueryableSource = dc.RawData_SummitBI_App01s.Where(Function(c) c.DateEnd.Value >= _DateFrom And c.DateEnd.Value < _DateTo.AddDays(1))
    '                Case Else
    '                    'e.QueryableSource = dc.RawData_SummitBI_App01s.Where(Function(c) c.DateEnd Is Nothing)
    '            End Select
    '        Else
    '            'e.QueryableSource = dc.RawData_SummitBI_App01s.Where(Function(c) c.DateEnd Is Nothing)
    '        End If

    '    End Using

    '    'e.KeyExpression = "ContractNo"

    '    'gridData.DataSource = e.QueryableSource
    '    'gridData.DataBind()
    'End Sub

    Protected Sub SqlDataSource_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles SqlDataSource_gridData.Selecting

        If DateFrom.Value IsNot Nothing And DateTo.Value IsNot Nothing Then

            Dim _DateFrom As Date = DateFrom.Value
            Dim _DateTo As Date = DateTo.Value

            Select Case FilterDateType.SelectedItem.Value
                Case "DateMakeContract"
                    e.Command.CommandText = "select * from RawData_SummitBI_App01 where convert(varchar,DateMakeContract,112) between '" & _DateFrom.ToString("yyyyMMdd") & "' and '" & _DateTo.ToString("yyyyMMdd") & "'"
                Case "DateMakePolicy"
                    e.Command.CommandText = "select * from RawData_SummitBI_App01 where convert(varchar,DateMakePolicy,112) between '" & _DateFrom.ToString("yyyyMMdd") & "' and '" & _DateTo.ToString("yyyyMMdd") & "'"
                Case "DateBegin"
                    e.Command.CommandText = "select * from RawData_SummitBI_App01 where convert(varchar,DateBegin,112) between '" & _DateFrom.ToString("yyyyMMdd") & "' and '" & _DateTo.ToString("yyyyMMdd") & "'"
                Case "DateEnd"
                    e.Command.CommandText = "select * from RawData_SummitBI_App01 where convert(varchar,DateEnd,112) between '" & _DateFrom.ToString("yyyyMMdd") & "' and '" & _DateTo.ToString("yyyyMMdd") & "'"
                Case Else
                    e.Command.CommandText = "select * from RawData_SummitBI_App01 where DateMakeContract is null "
            End Select
        Else
            e.Command.CommandText = "select * from RawData_SummitBI_App01 where DateMakeContract is null "
        End If


    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        gridData.DataBind()
    End Sub


    'Protected Sub GridData_Load(sender As Object, e As EventArgs) Handles gridData.Load
    '    If Not DateFrom.Value Is Nothing And Not DateTo.Value Is Nothing Then
    '        binddata()
    '    End If
    'End Sub
    'Private Sub binddata()

    '    Select Case FilterDateType.SelectedItem.Value

    '        Case "DateMakeContract"
    '            Dim op1 = New BinaryOperator("DateMakeContract", DateFrom.Value, BinaryOperatorType.GreaterOrEqual)
    '            Dim op2 = New BinaryOperator("DateMakeContract", DateAdd(DateInterval.Day, 1, DateTo.Value), BinaryOperatorType.Less)
    '            Dim op = New GroupOperator(GroupOperatorType.And, New CriteriaOperator() {op1, op2})
    '            gridData.FilterExpression = op.ToString()
    '        Case "DateMakePolicy"
    '            Dim op1 = New BinaryOperator("DateMakePolicy", DateFrom.Value, BinaryOperatorType.GreaterOrEqual)
    '            Dim op2 = New BinaryOperator("DateMakePolicy", DateAdd(DateInterval.Day, 1, DateTo.Value), BinaryOperatorType.Less)
    '            Dim op = New GroupOperator(GroupOperatorType.And, New CriteriaOperator() {op1, op2})
    '            gridData.FilterExpression = op.ToString()

    '        Case "DateBegin"
    '            Dim op1 = New BinaryOperator("DateBegin", DateFrom.Value, BinaryOperatorType.GreaterOrEqual)
    '            Dim op2 = New BinaryOperator("DateBegin", DateAdd(DateInterval.Day, 1, DateTo.Value), BinaryOperatorType.Less)
    '            Dim op = New GroupOperator(GroupOperatorType.And, New CriteriaOperator() {op1, op2})
    '            gridData.FilterExpression = op.ToString()
    '        Case "DateEnd"
    '            Dim op1 = New BinaryOperator("DateEnd", DateFrom.Value, BinaryOperatorType.GreaterOrEqual)
    '            Dim op2 = New BinaryOperator("DateEnd", DateAdd(DateInterval.Day, 1, DateTo.Value), BinaryOperatorType.Less)
    '            Dim op = New GroupOperator(GroupOperatorType.And, New CriteriaOperator() {op1, op2})
    '            gridData.FilterExpression = op.ToString()

    '        Case Else
    '            Dim op1 = New BinaryOperator("DateBegin", DBNull.Value, BinaryOperatorType.Equal)
    '            Dim op = New GroupOperator(GroupOperatorType.And, New CriteriaOperator() {op1})
    '            gridData.FilterExpression = op.ToString()


    '    End Select


    'End Sub


    'Protected Sub SqlDataSource_gridData_Selecting(sender As Object, e As LinqServerModeDataSourceSelectEventArgs) Handles SqlDataSource_gridData.Selecting
    '    Dim data = New DataClasses_LWTReportsExt().RawData_SummitBI_App01s
    '    'Dim MQDetails = New DataClasses_CPSExt().tblMotorQuotationDetails

    '    'Dim a = MQDetails.Where(Function(p) p.QuotationNo.Equals(editQuotationNo.Text)).FirstOrDefault()
    '    'If a Is Nothing Then
    '    '    e.QueryableSource = data.Where(Function(p) p.CoverageType.Equals(1))
    '    'Else
    '    '    e.QueryableSource = data.Where(Function(p) p.CoverageType.Equals(1) And p.CarUse.Equals(a.CarUse) And p.CarType.Equals(a.CarType))
    '    'End If
    '    'e.QueryableSource = data

    '    '<dx:ListEditItem Text="วันที่ติดต่อ" Value="DateMakeContract" Selected="true" />
    '    '<dx:ListEditItem Text="วันที่ทำ" Value="DateMakePolicy"  />
    '    '<dx:ListEditItem Text="วันเริ่มคุ้มครอง" Value="DateBegin"  />
    '    '<dx:ListEditItem Text="วันหมดอายุ" Value="DateEnd"   />

    '    'Select Case FilterDateType.SelectedItem.Value

    '    '    Case "DateMakeContract"
    '    '        e.QueryableSource = data.Where(Function(p) p.DateMakeContract.Value >= DateFrom.Value And p.DateMakeContract.Value <= DateTo.Value)
    '    '    Case "DateMakePolicy"
    '    '        e.QueryableSource = data.Where(Function(p) p.DateMakePolicy.Value >= DateFrom.Value And p.DateMakePolicy.Value <= DateTo.Value)
    '    '    Case "DateBegin"
    '    '        e.QueryableSource = data.Where(Function(p) p.DateBegin.Value >= DateFrom.Value And p.DateBegin.Value <= DateTo.Value)
    '    '    Case "DateEnd"
    '    '        e.QueryableSource = data.Where(Function(p) p.DateEnd.Value >= DateFrom.Value And p.DateEnd.Value <= DateTo.Value)
    '    '    Case Else
    '    '        e.QueryableSource = data.Where(Function(p) p.ContractNo.Equals(""))
    '    'End Select

    'End Sub

End Class
