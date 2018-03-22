Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports DevExpress.Web
Imports DevExpress.Web.Rendering
Imports DevExpress.Web.Data
Imports LWT.Website
Imports DevExpress.Data.Linq
Imports DevExpress.Data.Filtering
Imports System
Imports System.IO
Imports System.Linq

Partial Class ucDevxRawDataAPDProduction
    Inherits PortalModuleControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If

        If (Not IsPostBack) Then
            binddata()
        End If
    End Sub

    Protected Sub btnExportData_Click(sender As Object, e As EventArgs) Handles btnExportData.Click
        GridExporter.WriteXlsxToResponse(Guid.NewGuid().ToString(), False, New DevExpress.XtraPrinting.XlsxExportOptionsEx With {.ExportType = DevExpress.Export.ExportType.WYSIWYG})

        'GridExporter.WriteXlsxToResponse(Guid.NewGuid().ToString(), True, New DevExpress.XtraPrinting.XlsxExportOptionsEx With {.TextExportMode = DevExpress.XtraPrinting.TextExportMode.Text})

        'Using stream As New MemoryStream
        '    GridExporter.WriteXlsx(stream)
        '    'GridExporter.WriteCsvToResponse("CallRecords_" + DateTime.Now.ToShortDateString().Replace("/", ""), True, "csv", stream)
        'End Using


        'Dim fileName = String.Format("{0}.xlsx", System.Guid.NewGuid.ToString())
        'Dim filePath = String.Format("{0}\{1}", Server.MapPath("~/saved_files"), fileName)

        'Using memoryStream As New MemoryStream()
        '    GridExporter.WriteXlsx(memoryStream)
        '    memoryStream.Seek(0, SeekOrigin.Begin)
        '    Using fileStream As New FileStream(filePath, FileMode.Create, FileAccess.Write)
        '        memoryStream.WriteTo(fileStream)
        '    End Using
        'End Using

    End Sub

    Protected Sub GridData_Load(sender As Object, e As EventArgs) Handles gridData.Load
        If Not DateFrom.Value Is Nothing And Not DateTo.Value Is Nothing Then
            binddata()
        End If
    End Sub
    Private Sub binddata()
        Select Case FilterDateType.SelectedItem.Value
            Case "BillingDate"
                Dim op1 = New BinaryOperator("BillingDate", DateFrom.Value, BinaryOperatorType.GreaterOrEqual)
                Dim op2 = New BinaryOperator("BillingDate", DateAdd(DateInterval.Day, 1, DateTo.Value), BinaryOperatorType.Less)
                Dim op = New GroupOperator(GroupOperatorType.And, New CriteriaOperator() {op1, op2})
                gridData.FilterExpression = op.ToString()

            Case "PeriodFrom"
                Dim op1 = New BinaryOperator("PeriodFrom", DateFrom.Value, BinaryOperatorType.GreaterOrEqual)
                Dim op2 = New BinaryOperator("PeriodFrom", DateAdd(DateInterval.Day, 1, DateTo.Value), BinaryOperatorType.Less)
                Dim op = New GroupOperator(GroupOperatorType.And, New CriteriaOperator() {op1, op2})
                gridData.FilterExpression = op.ToString()
            Case "PeriodTo"
                Dim op1 = New BinaryOperator("PeriodTo", DateFrom.Value, BinaryOperatorType.GreaterOrEqual)
                Dim op2 = New BinaryOperator("PeriodTo", DateAdd(DateInterval.Day, 1, DateTo.Value), BinaryOperatorType.Less)
                Dim op = New GroupOperator(GroupOperatorType.And, New CriteriaOperator() {op1, op2})
                gridData.FilterExpression = op.ToString()

            Case Else
                Dim op1 = New BinaryOperator("Invoiceno", DBNull.Value, BinaryOperatorType.Equal)
                Dim op = New GroupOperator(GroupOperatorType.And, New CriteriaOperator() {op1})
                gridData.FilterExpression = op.ToString()
        End Select
    End Sub

End Class
