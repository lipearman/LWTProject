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
Imports DevExpress.XtraPivotGrid.Localization
Imports DevExpress.XtraCharts.Web
Imports System.Drawing
Imports DevExpress.XtraCharts.Native
Imports System.Drawing.Imaging
Imports DevExpress.XtraPrintingLinks

Partial Class applications_BI_User_PreviewExcel
    Inherits System.Web.UI.Page
    Private Const MaxTopValueCount As Integer = 20
    Public legenttitle As String = ""

    Private Shared ReadOnly SeriesLabelPatternItems() As String = {"{V:## ##0.0}", "{A}", "{V}", "{A}: {V}", "{S}"}
    Private Shared ReadOnly LegendPatternItems() As String = {"{A}", "{V}", "{A}: {V}", "{S}"}
    Private Shared ReadOnly AxisXPatternItems() As String = {"{A}", "Country: {A}"}
    Private Shared ReadOnly AxisYPatternItems() As String = {"{V:#,0}", "{V}"}

    Shared Sub New()
        MyPivotGridLocalizer.Active = New MyPivotGridLocalizer()
    End Sub


    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreRender



    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Init
        MyPivotGridLocalizer.Active = New MyPivotGridLocalizer()




        If (Not IsPostBack) Then
            'Session("DSGUID") = Nothing

            If Session("GUID") Is Nothing Then
                Response.End()
            Else
                'Dim _DSGUID = CInt(Session("DSGUID"))
                'hdBID.Value = _BID
                Dim _GUID = Session("GUID")

                'hdBID.Value = _BID

                BindMasterTemplate(_GUID)

                BindSort()

                BindSortBySummary()

                pivotGrid.DataBind()


            End If
        End If


    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If (Not IsPostBack) AndAlso (Not IsCallback) Then
            Dim restrictedTypes() As ViewType = {ViewType.PolarArea, ViewType.PolarLine, ViewType.SideBySideGantt, ViewType.SideBySideRangeBar, ViewType.RangeBar, ViewType.Gantt, ViewType.PolarPoint, ViewType.Stock, ViewType.CandleStick, ViewType.Bubble}
            For Each type As ViewType In System.Enum.GetValues(GetType(ViewType))
                If Array.IndexOf(Of ViewType)(restrictedTypes, type) >= 0 Then
                    Continue For
                End If
                ChartType.Items.Add(type.ToString())
            Next type
            ChartType.SelectedIndex = 0
        End If

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
    Private Sub BindSort()
        Using dc As New DataClasses_PortalBIExt()
            For Each _field As DevExpress.Web.ASPxPivotGrid.PivotGridField In pivotGrid.Fields
                'If _field.Visible = True Then
                Dim _Attributes = (From c In dc.tblDataSourceBI_Fields Where c.SortBySummaryInfo Is Nothing And c.FIELD.Equals(_field.FieldName) And c.BID.Equals(hdBID.Value)).FirstOrDefault()
                If _Attributes IsNot Nothing Then
                    If _Attributes.ORDERBY = 0 Or _Attributes.ORDERBY = 1 Then
                        _field.SortOrder = _Attributes.ORDERBY
                    End If

                    If _Attributes.AREAINDEX IsNot Nothing Then
                        _field.AreaIndex = _Attributes.AREAINDEX
                    End If
                End If
                'Else
                '    '_field.SortOrder = PivotSortOrder.Ascending
                '    '_field.SortMode = PivotSortMode.Default


                'End If
            Next


        End Using
    End Sub
    Private Sub BindSortBySummary()
        Using dc As New DataClasses_PortalBIExt()
            Dim data = (From c In dc.tblDataSourceBI_Fields Where c.SortBySummaryInfo IsNot Nothing And c.BID.Equals(hdBID.Value)).ToList()

            For Each _field As DevExpress.Web.ASPxPivotGrid.PivotGridField In pivotGrid.Fields

                For Each item In data
                    If item.FIELD = _field.FieldName Then


                        For Each _suminfofield As DevExpress.Web.ASPxPivotGrid.PivotGridField In pivotGrid.Fields
                            For Each itemsuminfo In data

                                If itemsuminfo.SortBySummaryInfo = _suminfofield.FieldName Then

                                    _field.SortBySummaryInfo.Field = _suminfofield
                                    Exit For
                                End If

                            Next
                        Next

                        Exit For
                    End If


                Next
            Next
        End Using
    End Sub



    Private Sub BindChart()
        If Not String.IsNullOrEmpty(ChartType.SelectedItem.Text) Then

            WebChart.SeriesTemplate.ChangeView(CType(System.Enum.Parse(GetType(ViewType), ChartType.SelectedItem.Text), ViewType))

        Else


            WebChart.SeriesTemplate.ChangeView(CType(System.Enum.Parse(GetType(ViewType), "Bar"), ViewType))


        End If





        WebChart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center
        WebChart.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside

        WebChart.Legend.Direction = LegendDirection.LeftToRight
        WebChart.Legend.Border.Color = Color.White

        'WebChart.Legend.MaxVerticalPercentage = 50
        'WebChart.Legend.MaxHorizontalPercentage = 50




        Dim _titlesize As New System.Drawing.Font("Tahoma", 10)

        WebChart.Legend.Title.Font = _titlesize

        Dim sb As New StringBuilder()

        Dim _fd As New List(Of DevExpress.Web.ASPxPivotGrid.PivotGridField)

        For Each item As DevExpress.Web.ASPxPivotGrid.PivotGridField In pivotGrid.Fields
            _fd.Add(item)
        Next

        Dim _fields = (From c In _fd Order By c.AreaIndex).ToList()

        For Each _field In _fields
            If _field.Area = PivotArea.ColumnArea Then
                If String.IsNullOrEmpty(sb.ToString()) Then
                    sb.Append(_field.Caption)
                Else
                    sb.Append("|" & _field.Caption)
                End If
            End If
        Next




        If Not String.IsNullOrEmpty(sb.ToString()) Then
            WebChart.Legend.Title.Visible = True
            WebChart.Legend.Title.Text = (sb.ToString())
        End If

        WebChart.Legend.TextVisible = True


        WebChart.DataSource = pivotGrid
        WebChart.DataBind()

        Dim diagram As XYDiagram = TryCast(WebChart.Diagram, XYDiagram)
        If diagram IsNot Nothing Then
            diagram.AxisY.Label.TextPattern = "{V:N0}"
        End If

        'diagram.AxisX.Label.TextPattern = "{V:#,0}"


        'WebChart.Series.Label.TextPattern = "{V:#,0}"

        'Dim a = WebChart.Series(0).LegendTextPattern = "{V:#,0}"


        'WebChart.DataSource = pivotGrid
        WebChart.DataBind()



        'chartDesigner.OpenChart(WebChart)
    End Sub
    Private Sub BindMasterTemplate(ByVal _GUID As String)

        Using dc As New DataClasses_PortalBIExt()
            Dim _BI = (From c In dc.tblDataSourceBIs Where c.GUID.Equals(_GUID)).FirstOrDefault()
            popChart.HeaderText = _BI.TITLE
            pnEnquiry.HeaderText = String.Format("{0} - {1}", _BI.TITLE, _BI.DESCRIPTION)
            hdBID.Value = _BI.BID


            cbShowColumnGrandTotals.Checked = _BI.ShowColumnGrandTotals.Value
            cbShowColumnTotals.Checked = _BI.ShowColumnTotals.Value

            cbShowRowGrandTotals.Checked = _BI.ShowRowGrandTotals.Value
            cbShowRowTotals.Checked = _BI.ShowRowTotals.Value

            cbShowGrandTotalsForSingleValues.Checked = _BI.ShowGrandTotalsForSingleValues.Value
            cbShowTotalsForSingleValues.Checked = _BI.ShowTotalsForSingleValues.Value

            pivotGrid.OptionsView.ShowColumnGrandTotals = _BI.ShowColumnGrandTotals.Value
            pivotGrid.OptionsView.ShowColumnTotals = _BI.ShowColumnTotals.Value

            pivotGrid.OptionsView.ShowRowGrandTotals = _BI.ShowRowGrandTotals.Value
            pivotGrid.OptionsView.ShowRowTotals = _BI.ShowRowTotals.Value

            pivotGrid.OptionsView.ShowGrandTotalsForSingleValues = _BI.ShowGrandTotalsForSingleValues.Value
            pivotGrid.OptionsView.ShowTotalsForSingleValues = _BI.ShowTotalsForSingleValues.Value



            Dim _datasource = (From c In dc.tblDataSourceFiles Where c.ID.Equals(_BI.DS_ID)).FirstOrDefault()
            pivotGrid.Caption = ""
            pivotGrid.Fields.Clear()
            Session("DSGUID") = _datasource.GUID


            pivotGrid.OptionsView.ShowColumnGrandTotals = _BI.ShowColumnGrandTotals.Value
            pivotGrid.OptionsView.ShowColumnTotals = _BI.ShowColumnTotals.Value
            pivotGrid.OptionsView.ShowRowGrandTotals = _BI.ShowRowGrandTotals.Value
            pivotGrid.OptionsView.ShowRowTotals = _BI.ShowRowTotals.Value
            pivotGrid.OptionsView.ShowGrandTotalsForSingleValues = _BI.ShowGrandTotalsForSingleValues.Value
            pivotGrid.OptionsView.ShowTotalsForSingleValues = _BI.ShowTotalsForSingleValues.Value

            If _datasource IsNot Nothing Then

                'pnEnquiry.HeaderText = String.Format("{0} - {1}", _datasource.FileName, _datasource.SheetName)
                Dim _Fields = (From c In dc.tblDataSourceFile_Fields Where c.DS_ID.Equals(_datasource.ID) Order By c.FIELD_NAME).ToList()
                For Each _field In _Fields
                    Dim _NewField As New DevExpress.Web.ASPxPivotGrid.PivotGridField() ' = New PivotGridField("InsurerEngGroup", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
                    With _NewField
                        .Visible = False
                        .FieldName = _field.FIELD_NAME
                        .Caption = _field.FIELD_CAPTION


                        Dim _BI_Fields = (From c In dc.tblDataSourceBI_Fields Where c.BID.Equals(_BI.BID) And c.FIELD.Equals(_field.FIELD_NAME)).FirstOrDefault()
                        If _BI_Fields Is Nothing Then

                            ''========================AREA ==========================
                            'If _field.AREA > -1 Then
                            '    .Visible = True
                            '    .Area = _field.AREA
                            '    '============================== Sort ==========================
                            '    If _field.ORDERBY = 0 Or _field.ORDERBY = 1 Then
                            '        .SortOrder = _field.ORDERBY
                            '    End If

                            '    If _field.AREAINDEX > -1 Then
                            '        .AreaIndex = _field.AREAINDEX
                            '    End If
                            'Else
                            .Visible = False
                            'End If

                        Else

                            '========================AREA ==========================
                            If _BI_Fields.AREA > -1 Then
                                .Visible = True
                                .Area = _BI_Fields.AREA
                                '============================== Sort ==========================
                                If _BI_Fields.ORDERBY = 0 Or _BI_Fields.ORDERBY = 1 Then
                                    .SortOrder = _BI_Fields.ORDERBY
                                End If

                                If _BI_Fields.AREAINDEX > -1 Then
                                    .AreaIndex = _BI_Fields.AREAINDEX
                                End If
                            Else
                                .Visible = False
                            End If

                        End If





                        '======================= Style =========================
                        .CellStyle.Wrap = DevExpress.Utils.DefaultBoolean.False
                        .ValueStyle.Wrap = DevExpress.Utils.DefaultBoolean.False
                        .HeaderStyle.Wrap = DevExpress.Utils.DefaultBoolean.False
                        .ValueTotalStyle.Wrap = DevExpress.Utils.DefaultBoolean.False

                        '======================= Format =========================
                        .ValueFormat.FormatString = _field.CellFormat_FormatString
                        .ValueFormat.FormatType = _field.CellFormat_FormatType

                        .CellFormat.FormatString = _field.CellFormat_FormatString
                        .CellFormat.FormatType = _field.CellFormat_FormatType

                        .TotalCellFormat.FormatString = _field.CellFormat_FormatString
                        .TotalCellFormat.FormatType = _field.CellFormat_FormatType

                        .TotalValueFormat.FormatString = _field.CellFormat_FormatString
                        .TotalValueFormat.FormatType = _field.CellFormat_FormatType

                        .GrandTotalCellFormat.FormatString = _field.CellFormat_FormatString
                        .GrandTotalCellFormat.FormatType = _field.CellFormat_FormatType

                        .UseNativeFormat = DevExpress.Utils.DefaultBoolean.True
                        .ExportBestFit = True

                        .EmptyValueText = 0.0
                        .EmptyCellText = 0.0

                        .Options.AllowSortBySummary = DevExpress.Utils.DefaultBoolean.True


                        .SummaryDisplayType = _field.PivotSummaryDisplayType

                        '======================= UseAggregateFunctions =========================
                        If _field.UnboundExpressionMode = UnboundExpressionMode.UseAggregateFunctions Then
                            If Not String.IsNullOrEmpty(_field.UnboundExpression) Then

                                .UnboundFieldName = _field.FIELD_NAME
                                .UnboundExpressionMode = _field.UnboundExpressionMode
                                .UnboundExpression = _field.UnboundExpression
                                .UnboundType = _field.UnboundColumnType
                                .SummaryType = _field.SummaryType
                            Else
                                .SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Sum
                            End If

                        Else
                            .SummaryType = _field.SummaryType
                        End If


                        '============================== Filter ==========================
                        Dim FilterValues = (From c In dc.tblDataSourceBI_Field_Filters Where c.FIELD_NAME.Equals(_field.FIELD_NAME) And c.BID.Equals(_BI.BID)).ToList()
                        If FilterValues.Count > 0 Then



                            Dim _valuetype As New List(Of Object)
                            For Each item In FilterValues
                                Select Case item.FILTER_TYPE
                                    Case "System.Double"
                                        _valuetype.Add(Convert.ToDouble(item.FILTER_VALUE))
                                    Case "System.Int32"
                                        _valuetype.Add(Convert.ToInt32(item.FILTER_VALUE))
                                    Case "System.DateTime"
                                        _valuetype.Add(Convert.ToDateTime(item.FILTER_VALUE))
                                    Case "System.String"
                                        _valuetype.Add(item.FILTER_VALUE)
                                    Case Else
                                        _valuetype.Add(item.FILTER_VALUE)
                                End Select
                            Next

                            .Options.AllowFilter = DevExpress.Utils.DefaultBoolean.False

                            .FilterValues.SetValues(_valuetype.ToArray(), PivotFilterType.Included, True)
                        End If

                    End With

                    pivotGrid.Fields.Add(_NewField)
                Next
            End If



            pivotGrid.DataBind()
        End Using
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







    Protected Sub btnChart_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnChart.Click
        popChart.ShowOnPageLoad = True

        BindChart()
    End Sub





    Protected Sub WebChartControl1_CustomCallback(ByVal sender As Object, ByVal e As CustomCallbackEventArgs) Handles WebChart.CustomCallback
        BindChart()
    End Sub
    Protected Sub WebChartControl1_BoundDataChanged(ByVal sender As Object, ByVal e As EventArgs) Handles WebChart.BoundDataChanged
        Dim chart = DirectCast(sender, WebChartControl)
        'If chart.Series.Count > 1 Then
        '    chart.Series(1).CrosshairLabelPattern = "{V:N2}"
        'End If

        'For Each item As Series In chart.Series
        '    item.CrosshairLabelPattern = "{V:N2}"
        'Next

    End Sub
    Protected Sub cmdExportChart_Click(sender As Object, e As EventArgs) Handles cmdExportChart.Click
        BindChart()

        Dim fileName As String = String.Format("Export_{0}.jpg", DateTime.Now.ToString("yyyyMMddHHmmss"))

        Dim chart As WebChartControl = WebChart

        ' Set the page's content type to PNG files
        ' and clears all content output from the buffer stream.
        Response.ContentType = "image/png"
        Response.AppendHeader("content-disposition", Convert.ToString("attachment; filename=") & fileName)
        Response.Clear()

        ' Buffer response so that page is sent
        ' after processing is complete.
        Response.BufferOutput = True
        DirectCast(chart, IChartContainer).Chart.ExportToImage(Response.OutputStream, ImageFormat.Jpeg)

        ' Send the output to the client.
        Response.Flush()


    End Sub





    Protected Sub pivotGridControl1_FieldValueDisplayText(sender As Object, e As DevExpress.Web.ASPxPivotGrid.PivotFieldDisplayTextEventArgs) Handles pivotGrid.FieldValueDisplayText
        If e.ValueType = PivotGridValueType.GrandTotal Then
            If e.IsColumn Then
                'e.DisplayText = "Total"

            Else
                e.DisplayText = "Grand Total"
            End If


        End If
    End Sub

    Protected Sub btnExportWithChart_Click(sender As Object, e As EventArgs) Handles btnExportWithChart.Click

        ASPxPivotGridExporter1.OptionsPrint.PrintHeadersOnEveryPage = False
        ASPxPivotGridExporter1.OptionsPrint.MergeColumnFieldValues = True
        ASPxPivotGridExporter1.OptionsPrint.MergeRowFieldValues = True

        ASPxPivotGridExporter1.OptionsPrint.PrintFilterHeaders = DevExpress.Utils.DefaultBoolean.False
        ASPxPivotGridExporter1.OptionsPrint.PrintColumnHeaders = DevExpress.Utils.DefaultBoolean.True
        ASPxPivotGridExporter1.OptionsPrint.PrintRowHeaders = DevExpress.Utils.DefaultBoolean.True
        ASPxPivotGridExporter1.OptionsPrint.PrintDataHeaders = DevExpress.Utils.DefaultBoolean.False

        Dim ps As New PrintingSystem()

        Dim ChartTitle As New Link()
        AddHandler ChartTitle.CreateDetailArea, AddressOf ChartTitle_CreateDetailArea

        Dim link1 As New PrintableComponentLink(ps)
        BindChart()
        link1.Component = (CType(WebChart, IChartContainer)).Chart

        Dim GridTitle As New Link()
        AddHandler GridTitle.CreateDetailArea, AddressOf GridTitle_CreateDetailArea

        Dim link2 As New PrintableComponentLink(ps)
        link2.Component = ASPxPivotGridExporter1


        Dim compositeLink As New CompositeLink(ps)
        compositeLink.Links.AddRange(New Object() {ChartTitle, link1, GridTitle, link2})

        compositeLink.CreateDocument()
        Using stream As New IO.MemoryStream()
            compositeLink.PrintingSystem.ExportToXls(stream)
            Dim fileName As String = Guid.NewGuid().ToString()
            Response.Clear()
            Response.Buffer = False
            Response.AppendHeader("Content-Type", "application/xls")
            Response.AppendHeader("Content-Transfer-Encoding", "binary")
            Response.AppendHeader("Content-Disposition", "attachment; filename=" & fileName & ".xls")
            Response.BinaryWrite(stream.ToArray())
            Response.End()
        End Using
        ps.Dispose()
    End Sub
    Private Sub ChartTitle_CreateDetailArea(ByVal sender As Object, ByVal e As CreateAreaEventArgs)
        Dim tb As New TextBrick()
        tb.BorderWidth = 0
        tb.Rect = New Rectangle(200, 0, 400, 50)
        tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Center
        tb.VertAlignment = DevExpress.Utils.VertAlignment.Bottom
        tb.Text = pnEnquiry.HeaderText '"Additional Header information here..."
        tb.Font = New Font("Tahoma", 14, FontStyle.Regular)
        e.Graph.DrawBrick(tb)

    End Sub
    Private Sub GridTitle_CreateDetailArea(ByVal sender As Object, ByVal e As CreateAreaEventArgs)
        Dim tb As New TextBrick()
        tb.BorderWidth = 0
        tb.Rect = New Rectangle(200, 0, 400, 50)
        tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Center
        tb.VertAlignment = DevExpress.Utils.VertAlignment.Bottom
        'tb.Text = pnEnquiry.HeaderText '"Additional Header information here..."
        tb.Font = New Font("Tahoma", 14, FontStyle.Regular)
        e.Graph.DrawBrick(tb)

    End Sub

End Class
Class MyPivotGridLocalizer
    Inherits PivotGridLocalizer
    Public Overrides Function GetLocalizedString(ByVal id As DevExpress.XtraPivotGrid.Localization.PivotGridStringId) As String
        Select Case id
            Case PivotGridStringId.GrandTotal : Return "Total"
            Case PivotGridStringId.Total : Return "Total"
            Case PivotGridStringId.CustomizationFormDeferLayoutUpdate : Return "Disable Instant Information Update In Layout"
        End Select
        Return MyBase.GetLocalizedString(id)
    End Function
End Class