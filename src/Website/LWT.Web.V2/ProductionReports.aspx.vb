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

Partial Class ProductionReports
    Inherits System.Web.UI.Page
    Private bounded As Boolean
    Private Sub SetYearFilter()
        ''[Production Dim Date].[Date Hierarchy].[Calendar Year]
        'pivotGrid.Fields("[Production Dim Date].[Date Hierarchy].[Calendar Year]").FilterValues.FilterType = DevExpress.XtraPivotGrid.PivotFilterType.Included
        'pivotGrid.Fields("[Production Dim Date].[Date Hierarchy].[Calendar Year]").FilterValues.Add(2016)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load


        'Dim columnIndexValue As String = ColumnIndex.Value, rowIndexValue As String = RowIndex.Value
        'If (Not String.IsNullOrEmpty(columnIndexValue)) AndAlso (Not String.IsNullOrEmpty(rowIndexValue)) AndAlso ASPxPopupControl1.ShowOnPageLoad Then
        '    BindGridView(columnIndexValue, rowIndexValue)
        'End If
        'pivotGrid.ClientSideEvents.CellClick = GetJSCellClickHandler()
        'ASPxPopupControl1.ClientSideEvents.Closing = GetJSPopupClosingHandler()
        'If ASPxGridView1.IsCallback AndAlso (Not String.IsNullOrEmpty(columnIndexValue)) AndAlso (Not String.IsNullOrEmpty(rowIndexValue)) Then
        '    ASPxGridView1.JSProperties.Add("cpShowDrillDownWindow", False)
        'End If

        If (Not IsPostBack) AndAlso (Not IsCallback) Then
            Dim restrictedTypes() As ViewType = {ViewType.PolarArea, ViewType.PolarLine, ViewType.SideBySideGantt, ViewType.SideBySideRangeBar, ViewType.RangeBar, ViewType.Gantt, ViewType.PolarPoint, ViewType.Stock, ViewType.CandleStick, ViewType.Bubble}
            For Each type As ViewType In System.Enum.GetValues(GetType(ViewType))
                If Array.IndexOf(Of ViewType)(restrictedTypes, type) >= 0 Then
                    Continue For
                End If
                ChartType.Items.Add(type.ToString())
            Next type
            ChartType.SelectedIndex = 0

            SetYearFilter()
            'SetOptionsViewCheckBoxes()

        Else
            'SetOptionsViewProperties()
        End If

    End Sub
    'Private Sub SetOptionsViewCheckBoxes()
    '    cbShowColumnGrandTotals.Checked = pivotGrid.OptionsView.ShowColumnGrandTotals
    '    cbShowColumnTotals.Checked = pivotGrid.OptionsView.ShowColumnTotals
    '    cbShowRowGrandTotals.Checked = pivotGrid.OptionsView.ShowRowGrandTotals
    '    cbShowRowTotals.Checked = pivotGrid.OptionsView.ShowRowTotals
    '    cbShowGrandTotalsForSingleValues.Checked = pivotGrid.OptionsView.ShowGrandTotalsForSingleValues
    '    cbShowTotalsForSingleValues.Checked = pivotGrid.OptionsView.ShowTotalsForSingleValues
    'End Sub
    'Private Sub SetOptionsViewProperties()
    '    pivotGrid.OptionsView.ShowColumnGrandTotals = cbShowColumnGrandTotals.Checked
    '    pivotGrid.OptionsView.ShowColumnTotals = cbShowColumnTotals.Checked
    '    pivotGrid.OptionsView.ShowRowGrandTotals = cbShowRowGrandTotals.Checked
    '    pivotGrid.OptionsView.ShowRowTotals = cbShowRowTotals.Checked
    '    pivotGrid.OptionsView.ShowGrandTotalsForSingleValues = cbShowGrandTotalsForSingleValues.Checked
    '    pivotGrid.OptionsView.ShowTotalsForSingleValues = cbShowTotalsForSingleValues.Checked
    'End Sub

    Protected Function GetJSCellClickHandler() As String
        Return String.Format("function (s, e) {{" & ControlChars.CrLf & "    var columnIndex = document.getElementById('{0}')," & ControlChars.CrLf & "        rowIndex = document.getElementById('{1}');" & ControlChars.CrLf & "    columnIndex.value = e.ColumnIndex;" & ControlChars.CrLf & "    rowIndex.value = e.RowIndex;" & ControlChars.CrLf & "    GridView.PerformCallback('D');" & ControlChars.CrLf & "    ShowDrillDown();" & ControlChars.CrLf & "}}", ColumnIndex.ClientID, RowIndex.ClientID)
    End Function
    Protected Function GetJSPopupClosingHandler() As String
        Return String.Format("function (s, e) {{" & ControlChars.CrLf & "    var columnIndex = document.getElementById('{0}')," & ControlChars.CrLf & "        rowIndex = document.getElementById('{1}');" & ControlChars.CrLf & "    columnIndex.value = '';" & ControlChars.CrLf & "    rowIndex.value = '';" & ControlChars.CrLf & "    GridView.SetVisible(false);" & ControlChars.CrLf & "    " & ControlChars.CrLf & "}}", ColumnIndex.ClientID, RowIndex.ClientID)
    End Function
    
 
    'Protected Sub ASPxGridView1_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs) Handles ASPxGridView1.CustomCallback
    '    Dim param() As String = e.Parameters.Split("|"c)
    '    If param(0) <> "D" Then
    '        Return
    '    End If
    '    BindGridView(ColumnIndex.Value, RowIndex.Value)
    '    ASPxGridView1.PageIndex = 0
    '    If e.Parameters = "D" Then
    '        ASPxGridView1.JSProperties("cpShowDrillDownWindow") = True
    '    End If
    'End Sub
    'Protected Sub BindGridView(ByVal columnIndex As String, ByVal rowIndex As String)
    '    If bounded Then
    '        Return
    '    End If
    '    bounded = True
    '    ASPxGridView1.DataSource = pivotGrid.CreateDrillDownDataSource(Int32.Parse(columnIndex), Int32.Parse(rowIndex))
    '    ASPxGridView1.DataBind()
    'End Sub
    Protected Sub btnExportData_Click(sender As Object, e As EventArgs) Handles btnExportData.Click

        Export(1, False)
    End Sub

    Private Sub Export(ByVal _ExportFormat As Integer, ByVal saveAs As Boolean)
        'For Each field As DevExpress.Web.ASPxPivotGrid.PivotGridField In pivotGrid.Fields
        '    If field.ValueFormat IsNot Nothing AndAlso (Not String.IsNullOrEmpty(field.ValueFormat.FormatString)) Then

        '        field.CellStyle.Wrap = DevExpress.Utils.DefaultBoolean.False
        '        field.ValueStyle.Wrap = DevExpress.Utils.DefaultBoolean.False
        '        field.HeaderStyle.Wrap = DevExpress.Utils.DefaultBoolean.False
        '        field.ValueTotalStyle.Wrap = DevExpress.Utils.DefaultBoolean.False


        '        field.UseNativeFormat = DevExpress.Utils.DefaultBoolean.False

        '        field.ExportBestFit = False
        '    End If
        'Next field

        ASPxPivotGridExporter1.OptionsPrint.PrintHeadersOnEveryPage = False
        ASPxPivotGridExporter1.OptionsPrint.MergeColumnFieldValues = True
        ASPxPivotGridExporter1.OptionsPrint.MergeRowFieldValues = True

        ASPxPivotGridExporter1.OptionsPrint.PrintFilterHeaders = DevExpress.Utils.DefaultBoolean.False
        ASPxPivotGridExporter1.OptionsPrint.PrintColumnHeaders = DevExpress.Utils.DefaultBoolean.False
        ASPxPivotGridExporter1.OptionsPrint.PrintRowHeaders = DevExpress.Utils.DefaultBoolean.False
        ASPxPivotGridExporter1.OptionsPrint.PrintDataHeaders = DevExpress.Utils.DefaultBoolean.False




        'ASPxPivotGridExporter1.OptionsPrint.PrintUnusedFilterFields = False
        'ASPxPivotGridExporter1.OptionsPrint.PrintHorzLines = DevExpress.Utils.DefaultBoolean.False
        'ASPxPivotGridExporter1.OptionsPrint.PrintVertLines = DevExpress.Utils.DefaultBoolean.False
        'ASPxPivotGridExporter1.OptionsPrint.PageSettings.Landscape = True

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
    Protected Sub btnChart_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnChart.Click
        popChart.ShowOnPageLoad = True
        WebChart.SeriesTemplate.ChangeView(CType(System.Enum.Parse(GetType(ViewType), ChartType.SelectedItem.Text), ViewType))
        WebChart.DataSource = pivotGrid
        WebChart.DataBind()
    End Sub

End Class
