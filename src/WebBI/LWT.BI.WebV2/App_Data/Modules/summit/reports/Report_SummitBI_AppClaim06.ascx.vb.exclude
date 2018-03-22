Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports DevExpress.Web
Imports DevExpress.Web.Rendering
Imports DevExpress.Web.Data
Imports LWT.Website
Imports DevExpress.Data.Filtering

Partial Class Reports_Report_SummitBI_AppClaim06
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


    Protected Sub btnExportData_Click(sender As Object, e As EventArgs) 'Handles btnExportData.Click
        GridExporter.WriteXlsxToResponse(Guid.NewGuid().ToString(), False, New DevExpress.XtraPrinting.XlsxExportOptionsEx With {.ExportType = DevExpress.Export.ExportType.Default})
    End Sub
    Protected Sub ASPxGridView1_CustomUnboundColumnData(sender As Object, e As ASPxGridViewColumnDataEventArgs) Handles gridData.CustomUnboundColumnData
        If e.Column.FieldName = "Number" Then
            e.Value = e.ListSourceRowIndex + 1
        End If
    End Sub
    'protected void ASPxGridView1_CustomUnboundColumnData(object sender, ASPxGridViewColumnDataEventArgs e) {
    '        if (e.Column.FieldName == "Number") {
    '            e.Value = string.Format("Item #{0}", e.ListSourceRowIndex);
    '        }
    '    }

    'Protected Sub GridData_Load(sender As Object, e As EventArgs) Handles gridData.Load
    '    If Not DateFrom.Value Is Nothing And Not DateTo.Value Is Nothing Then
    '        binddata()
    '    End If
    'End Sub
    'Private Sub binddata()
    '    'SqlDataSource_gridData.DataSourceMode = SqlDataSourceMode.DataSet ' SqlDataSourceMode.DataReader
    '    ''============ todo2=======================


    '    'SqlDataSource_gridData.SelectCommand = String.Format("SELECT * FROM RawData_SummitBI_AppClaim06 where convert(varchar,[วันที่เกิดเหตุ],112) between '{0}' and '{1}' ", DirectCast(DateFrom.Value, Date).ToString("yyyyMMdd"), DirectCast(DateTo.Value, Date).ToString("yyyyMMdd"))
    '    'SqlDataSource_gridData.DataBind()

    '    'gridData.DataSourceID = "SqlDataSource_gridData"
    '    'gridData.DataBind()

    '    ''SqlHelper.ExecuteReader("", CommandType.Text, "")
    '    Select Case FilterDateType.SelectedItem.Value
    '        Case "วันที่เกิดเหตุ"
    '            Dim op1 = New BinaryOperator("วันที่เกิดเหตุ", DateFrom.Value, BinaryOperatorType.GreaterOrEqual)
    '            Dim op2 = New BinaryOperator("วันที่เกิดเหตุ", DateAdd(DateInterval.Day, 1, DateTo.Value), BinaryOperatorType.Less)
    '            Dim op = New GroupOperator(GroupOperatorType.And, New CriteriaOperator() {op1, op2})
    '            gridData.FilterExpression = op.ToString()
    '        Case "วันที่แจ้งเปิดเคลม"
    '            Dim op1 = New BinaryOperator("วันที่แจ้งเปิดเคลม", DateFrom.Value, BinaryOperatorType.GreaterOrEqual)
    '            Dim op2 = New BinaryOperator("วันที่แจ้งเปิดเคลม", DateAdd(DateInterval.Day, 1, DateTo.Value), BinaryOperatorType.Less)
    '            Dim op = New GroupOperator(GroupOperatorType.And, New CriteriaOperator() {op1, op2})
    '            gridData.FilterExpression = op.ToString()
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


    Protected Sub SqlDataSource_Selecting(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.SqlDataSourceSelectingEventArgs) Handles SqlDataSource_gridData.Selecting

        If DateFrom.Value IsNot Nothing And DateTo.Value IsNot Nothing Then

            Dim _DateFrom As Date = DateFrom.Value
            Dim _DateTo As Date = DateTo.Value

            Select Case FilterDateType.SelectedItem.Value
                Case "วันที่เกิดเหตุ"
                    e.Command.CommandText = "select * from RawData_SummitBI_AppClaim06 where convert(varchar,AccidentDate,112) between '" & _DateFrom.ToString("yyyyMMdd") & "' and '" & _DateTo.ToString("yyyyMMdd") & "'"

                Case "วันที่แจ้งเปิดเคลม"
                    e.Command.CommandText = "select * from RawData_SummitBI_AppClaim06 where convert(varchar,ClaimDate2,112) between '" & _DateFrom.ToString("yyyyMMdd") & "' and '" & _DateTo.ToString("yyyyMMdd") & "'"

                Case "DateMakeContract"
                    e.Command.CommandText = "select * from RawData_SummitBI_AppClaim06 where convert(varchar,DateMakeContract,112) between '" & _DateFrom.ToString("yyyyMMdd") & "' and '" & _DateTo.ToString("yyyyMMdd") & "'"
                Case "DateMakePolicy"
                    e.Command.CommandText = "select * from RawData_SummitBI_AppClaim06 where convert(varchar,DateMakePolicy,112) between '" & _DateFrom.ToString("yyyyMMdd") & "' and '" & _DateTo.ToString("yyyyMMdd") & "'"
                Case "DateBegin"
                    e.Command.CommandText = "select * from RawData_SummitBI_AppClaim06 where convert(varchar,DateBegin,112) between '" & _DateFrom.ToString("yyyyMMdd") & "' and '" & _DateTo.ToString("yyyyMMdd") & "'"
                Case "DateEnd"
                    e.Command.CommandText = "select * from RawData_SummitBI_AppClaim06 where convert(varchar,DateEnd,112) between '" & _DateFrom.ToString("yyyyMMdd") & "' and '" & _DateTo.ToString("yyyyMMdd") & "'"
                Case Else
                    e.Command.CommandText = "select * from RawData_SummitBI_AppClaim06 where DateMakeContract is null "
            End Select
        Else
            e.Command.CommandText = "select * from RawData_SummitBI_AppClaim06 where DateMakeContract is null "
        End If

    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        gridData.DataBind()
    End Sub



End Class
