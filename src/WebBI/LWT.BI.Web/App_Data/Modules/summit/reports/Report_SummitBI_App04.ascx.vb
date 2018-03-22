Imports Portal.Components
Imports DevExpress.Web.ASPxTreeList
Imports DevExpress.Web
Imports System.Data.SqlClient
Imports System.Data
Imports DevExpress.Web.ASPxPivotGrid
Imports DevExpress.Data.Linq
Imports System.Reflection
Imports System.Linq.Expressions
Imports System.Runtime.Remoting
Imports DevExpress.XtraPivotGrid
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports DevExpress.XtraCharts
Imports DevExpress.Data.PivotGrid
Imports DevExpress.XtraCharts.Web


Partial Class Reports_Report_SummitBI_App04
    Inherits PortalModuleControl
    Private Const MaxTopValueCount As Integer = 20


    Private Sub init_chart()
        Dim restrictedTypes() As ViewType = {ViewType.PolarArea, ViewType.PolarLine, ViewType.SideBySideGantt, ViewType.SideBySideRangeBar, ViewType.RangeBar, ViewType.Gantt, ViewType.PolarPoint, ViewType.Stock, ViewType.CandleStick, ViewType.Bubble}
        ChartType.Items.Clear()
        ChartType.Items.Add("")
        For Each type As ViewType In System.Enum.GetValues(GetType(ViewType))
            If Array.IndexOf(Of ViewType)(restrictedTypes, type) >= 0 Then
                Continue For
            End If
            ChartType.Items.Add(type.ToString())
        Next type

        ChartType.SelectedIndex = 0
    End Sub
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Init
        If (Not IsPostBack) Then
            pivotGrid.OptionsView.ShowColumnGrandTotals = False
            pivotGrid.OptionsView.ShowColumnTotals = False
            pivotGrid.OptionsView.ShowRowGrandTotals = False
            pivotGrid.OptionsView.ShowRowTotals = False
            pivotGrid.OptionsView.ShowGrandTotalsForSingleValues = False
            pivotGrid.OptionsView.ShowTotalsForSingleValues = False

            init_chart()


        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load


        If (Not IsPostBack) AndAlso (Not Page.IsCallback) Then


            SetOptionsViewCheckBoxes()
        Else

            SetOptionsViewProperties()
        End If


    End Sub





    Private Sub SetOptionsViewCheckBoxes()
        cbShowColumnGrandTotals.Checked = pivotGrid.OptionsView.ShowColumnGrandTotals
        cbShowColumnTotals.Checked = pivotGrid.OptionsView.ShowColumnTotals
        cbShowRowGrandTotals.Checked = pivotGrid.OptionsView.ShowRowGrandTotals
        cbShowRowTotals.Checked = pivotGrid.OptionsView.ShowRowTotals
        cbShowGrandTotalsForSingleValues.Checked = pivotGrid.OptionsView.ShowGrandTotalsForSingleValues
        cbShowTotalsForSingleValues.Checked = pivotGrid.OptionsView.ShowTotalsForSingleValues
    End Sub
    Private Sub SetOptionsViewProperties()
        pivotGrid.OptionsView.ShowColumnGrandTotals = cbShowColumnGrandTotals.Checked
        pivotGrid.OptionsView.ShowColumnTotals = cbShowColumnTotals.Checked
        pivotGrid.OptionsView.ShowRowGrandTotals = cbShowRowGrandTotals.Checked
        pivotGrid.OptionsView.ShowRowTotals = cbShowRowTotals.Checked
        pivotGrid.OptionsView.ShowGrandTotalsForSingleValues = cbShowGrandTotalsForSingleValues.Checked
        pivotGrid.OptionsView.ShowTotalsForSingleValues = cbShowTotalsForSingleValues.Checked
    End Sub
    Protected Sub btnExportData_Click(sender As Object, e As EventArgs) Handles btnExportData.Click
        Export(1, False)
    End Sub
    Private Sub Export(ByVal _ExportFormat As Integer, ByVal saveAs As Boolean)

        ASPxPivotGridExporter1.OptionsPrint.PrintHeadersOnEveryPage = False
        ASPxPivotGridExporter1.OptionsPrint.MergeColumnFieldValues = True
        ASPxPivotGridExporter1.OptionsPrint.MergeRowFieldValues = True

        ASPxPivotGridExporter1.OptionsPrint.PrintFilterHeaders = DevExpress.Utils.DefaultBoolean.False
        ASPxPivotGridExporter1.OptionsPrint.PrintColumnHeaders = DevExpress.Utils.DefaultBoolean.False
        ASPxPivotGridExporter1.OptionsPrint.PrintRowHeaders = DevExpress.Utils.DefaultBoolean.False
        ASPxPivotGridExporter1.OptionsPrint.PrintDataHeaders = DevExpress.Utils.DefaultBoolean.False


        Dim fileName As String = Guid.NewGuid().ToString()
        Dim options As XlsxExportOptionsEx
        Select Case _ExportFormat
            Case 0
                ASPxPivotGridExporter1.ExportPdfToResponse(fileName, saveAs)
            Case 1
                options = New XlsxExportOptionsEx() With {.ExportType = ExportType.WYSIWYG, .SheetName = Left(pnEnquiry.HeaderText, 30)}
                ASPxPivotGridExporter1.ExportXlsxToResponse(fileName, options, saveAs)
            Case 2
                ASPxPivotGridExporter1.ExportMhtToResponse(fileName, "utf-8", "Data", True, saveAs)
            Case 3
                ASPxPivotGridExporter1.ExportRtfToResponse(fileName, saveAs)
            Case 4
                ASPxPivotGridExporter1.ExportTextToResponse(fileName, saveAs)
            Case 5
                ASPxPivotGridExporter1.ExportHtmlToResponse(fileName, "utf-8", "Data", True, saveAs)
            Case 6
                options = New XlsxExportOptionsEx() With {.ExportType = ExportType.DataAware, .AllowGrouping = DevExpress.Utils.DefaultBoolean.True, .TextExportMode = TextExportMode.Value, .AllowFixedColumns = DevExpress.Utils.DefaultBoolean.True, .AllowFixedColumnHeaderPanel = DevExpress.Utils.DefaultBoolean.True, .RawDataMode = True}
                ASPxPivotGridExporter1.ExportXlsxToResponse(fileName, options, saveAs)
        End Select
    End Sub

    Protected Sub WebChartControl1_CustomCallback(ByVal sender As Object, ByVal e As CustomCallbackEventArgs) Handles WebChart.CustomCallback
        If Not String.IsNullOrEmpty(e.Parameter) Then
            WebChart.SeriesTemplate.ChangeView(CType(System.Enum.Parse(GetType(ViewType), e.Parameter), ViewType))
            WebChart.DataSource = pivotGrid
            WebChart.DataBind()
        End If


    End Sub
End Class
