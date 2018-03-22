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
Imports System.Drawing
Imports DevExpress.XtraCharts.Web
Imports DevExpress.XtraCharts.Native
Imports System.Drawing.Imaging



Imports DevExpress.XtraPrintingLinks

Partial Class applications_BI_Master_Preview
    Inherits System.Web.UI.Page
    Private Const MaxTopValueCount As Integer = 20

    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreRender



    End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Init
        If (Not IsPostBack) Then

            If Session("GUID") IsNot Nothing Then

                Dim _GUID = Session("GUID")

                'hdBID.Value = Session("GUID")

                'BindData(_BID)

                BindData2(_GUID)
                BindFilter()
                BindSort()
                BindSortBySummary()


                pivotGrid.DataBind()

                'pivotGrid.Prefilter.Enabled = True
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


        'pivotGrid.DataBind()


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


    'Protected Sub btShowModal_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShowModal.Click
    '    popPivot.ShowOnPageLoad = True

    'End Sub


    'Private Sub BindData(ByVal _BID As Integer)
    '    Using dc As New DataClasses_PortalBIExt()
    '        pivotGrid.Caption = ""
    '        pivotGrid.Fields.Clear()

    '        Dim _BI = (From c In dc.tblBIs Where c.BID.Equals(_BID)).FirstOrDefault()
    '        pnEnquiry.HeaderText = String.Format("{0} - {1}", _BI.TITLE, _BI.DESCRIPTION)

    '        Dim _OLAP = (From c In dc.V_Cubes Where c.CUBE_ID.Equals(_BI.CUBE_ID)).FirstOrDefault()

    '        pivotGrid.OLAPConnectionString = _OLAP.CONNECTING

    '        pivotGrid.OLAPDataProvider = OLAPDataProvider.OleDb
    '        'pivotGrid.BeginUpdate()

    '        Dim _Fields As New List(Of String)



    '        Dim _Attributes = (From c In dc.V_BIAttributes Where c.BID.Equals(_BI.BID) Order By c.AREA, c.AREAINDEX).ToList()

    '        If _Attributes.Count > 0 Then
    '            '========================== Measure/Attribute =========================
    '            For Each _Attribute In _Attributes.Where(Function(c) c.Type.Equals("Measure") Or c.Type.Equals("Attribute")).ToList()

    '                Dim _NewField As New DevExpress.Web.ASPxPivotGrid.PivotGridField()
    '                With _NewField
    '                    '======================= Field =========================
    '                    .ID = _Attribute.ATTRIBUTE
    '                    .FieldName = _Attribute.FIELD
    '                    .Caption = _Attribute.ATTRIBUTE
    '                    .DisplayFolder = _Attribute.FOLDER
    '                    ''======================= Area =========================
    '                    'If _Attribute.AREA < 0 Then
    '                    '    .Visible = False
    '                    'Else
    '                    '    .Visible = True
    '                    '    .Area = _Attribute.AREA
    '                    'End If

    '                    ''======================= Style =========================
    '                    '.CellStyle.Wrap = DevExpress.Utils.DefaultBoolean.False
    '                    '.ValueStyle.Wrap = DevExpress.Utils.DefaultBoolean.False
    '                    '.HeaderStyle.Wrap = DevExpress.Utils.DefaultBoolean.False
    '                    '.ValueTotalStyle.Wrap = DevExpress.Utils.DefaultBoolean.False

    '                    '======================= Format =========================
    '                    '.ValueFormat.FormatString = _Attribute.FormatType
    '                    '.ValueFormat.FormatType = DevExpress.Utils.FormatType.Custom
    '                    '.CellFormat.FormatString = _Attribute.FormatType
    '                    '.CellFormat.FormatType = DevExpress.Utils.FormatType.Custom
    '                    '.TotalCellFormat.FormatString = _Attribute.FormatType
    '                    '.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Custom
    '                    '.TotalValueFormat.FormatString = _Attribute.FormatType
    '                    '.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Custom
    '                    '.GrandTotalCellFormat.FormatString = _Attribute.FormatType
    '                    '.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Custom

    '                    '.UseNativeFormat = DevExpress.Utils.DefaultBoolean.True
    '                    '.UseDecimalValuesForMaxMinSummary = True
    '                    '.ExportBestFit = True

    '                    '.EmptyValueText = 0.0
    '                    '.EmptyCellText = 0.0



    '                End With
    '                pivotGrid.Fields.Add(_NewField)
    '            Next
    '            ''============================= Hierarchy ===========================
    '            'For Each _Hierarchy In _Attributes.Where(Function(c) c.Type.Equals("Hierarchy")).ToList()
    '            '    Dim _group = New PivotGridWebGroup()
    '            '    _group.Caption = _Hierarchy.ATTRIBUTE
    '            '    _group.ShowNewValues = True
    '            '    _group.Hierarchy = _Hierarchy.FIELD
    '            '    pivotGrid.Groups.Add(_group)
    '            'Next


    '            ''=======================================================================
    '            '_Fields = _Attributes.Where(Function(c) c.Type.Equals("Measure") Or c.Type.Equals("Attribute")).Select(Function(c) c.FIELD).ToList()




    '        End If



    '        Dim _Attributes_Hidden = (From c In dc.V_Attributes Where c.CUBE_ID.Equals(_BI.CUBE_ID) And Not _Fields.Contains(c.FIELD)).ToList()
    '        If _Attributes_Hidden.Count > 0 Then

    '            For Each _Attribute In _Attributes_Hidden.Where(Function(c) c.Type.Equals("Measure") Or c.Type.Equals("Attribute")).ToList()

    '                Dim _NewField As New DevExpress.Web.ASPxPivotGrid.PivotGridField()
    '                With _NewField
    '                    '======================= Field =========================
    '                    '.ID = _Attribute.ATTRIBUTE
    '                    .FieldName = _Attribute.FIELD
    '                    '.Caption = _Attribute.ATTRIBUTE
    '                    .DisplayFolder = _Attribute.FOLDER
    '                    ''======================= Area =========================
    '                    '.Visible = False

    '                    ''======================= Style =========================
    '                    '.CellStyle.Wrap = DevExpress.Utils.DefaultBoolean.False
    '                    '.ValueStyle.Wrap = DevExpress.Utils.DefaultBoolean.False
    '                    '.HeaderStyle.Wrap = DevExpress.Utils.DefaultBoolean.False
    '                    '.ValueTotalStyle.Wrap = DevExpress.Utils.DefaultBoolean.False

    '                    ''======================= Format =========================
    '                    '.ValueFormat.FormatString = _Attribute.FormatType
    '                    '.ValueFormat.FormatType = DevExpress.Utils.FormatType.Custom
    '                    '.CellFormat.FormatString = _Attribute.FormatType
    '                    '.CellFormat.FormatType = DevExpress.Utils.FormatType.Custom
    '                    '.TotalCellFormat.FormatString = _Attribute.FormatType
    '                    '.TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Custom
    '                    '.TotalValueFormat.FormatString = _Attribute.FormatType
    '                    '.TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Custom
    '                    '.GrandTotalCellFormat.FormatString = _Attribute.FormatType
    '                    '.GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Custom

    '                    '.UseNativeFormat = DevExpress.Utils.DefaultBoolean.True
    '                    '.UseDecimalValuesForMaxMinSummary = True
    '                    '.ExportBestFit = True

    '                    '.EmptyValueText = 0.0
    '                    '.EmptyCellText = 0.0



    '                End With
    '                pivotGrid.Fields.Add(_NewField)
    '            Next





    '            'For Each _Hierarchy In _Attributes_Hidden.Where(Function(c) c.Type.Equals("Hierarchy")).ToList()
    '            '    Dim _group = New PivotGridWebGroup()
    '            '    _group.Caption = _Hierarchy.ATTRIBUTE
    '            '    _group.ShowNewValues = True
    '            '    _group.Hierarchy = _Hierarchy.FIELD
    '            '    pivotGrid.Groups.Add(_group)
    '            'Next
    '        End If


    '    End Using


    'End Sub

    Private Sub BindData2(ByVal _GUID As String)
        Using dc As New DataClasses_PortalBIExt()
            pivotGrid.Caption = ""
            pivotGrid.Fields.Clear()

            Dim _BI = (From c In dc.tblBIs Where c.GUID.Equals(_GUID)).FirstOrDefault()
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


            Dim _OLAP = (From c In dc.V_Cubes Where c.CUBE_ID.Equals(_BI.CUBE_ID)).FirstOrDefault()

            pivotGrid.OLAPConnectionString = _OLAP.CONNECTING
            pivotGrid.OLAPDataProvider = OLAPDataProvider.Default
            pivotGrid.BeginUpdate()
            pivotGrid.RetrieveFields(PivotArea.FilterArea, False)

            For Each _field As DevExpress.Web.ASPxPivotGrid.PivotGridField In pivotGrid.Fields
                Dim _tblAttributes = (From c In dc.tblAttributes Where c.CUBE_ID.Equals(_OLAP.CUBE_ID) And c.FIELD.Equals(_field.FieldName)).FirstOrDefault()
                With _field
                    '======================= Field =========================
                    '.ID = _tblAttributes.ATTRIBUTE
                    .FieldName = _tblAttributes.FIELD
                    .Caption = _tblAttributes.ATTRIBUTE
                    .DisplayFolder = _tblAttributes.FOLDER

                    '======================= Style =========================
                    .CellStyle.Wrap = DevExpress.Utils.DefaultBoolean.False
                    .ValueStyle.Wrap = DevExpress.Utils.DefaultBoolean.False
                    .HeaderStyle.Wrap = DevExpress.Utils.DefaultBoolean.False
                    .ValueTotalStyle.Wrap = DevExpress.Utils.DefaultBoolean.False

                    '======================= Format =========================
                    .ValueFormat.FormatString = _tblAttributes.FormatType
                    .ValueFormat.FormatType = DevExpress.Utils.FormatType.Custom
                    .CellFormat.FormatString = _tblAttributes.FormatType
                    .CellFormat.FormatType = DevExpress.Utils.FormatType.Custom
                    .TotalCellFormat.FormatString = _tblAttributes.FormatType
                    .TotalCellFormat.FormatType = DevExpress.Utils.FormatType.Custom
                    .TotalValueFormat.FormatString = _tblAttributes.FormatType
                    .TotalValueFormat.FormatType = DevExpress.Utils.FormatType.Custom
                    .GrandTotalCellFormat.FormatString = _tblAttributes.FormatType
                    .GrandTotalCellFormat.FormatType = DevExpress.Utils.FormatType.Custom

                    .UseNativeFormat = DevExpress.Utils.DefaultBoolean.True
                    .ExportBestFit = True

                    '.EmptyValueText = 0.0
                    '.EmptyCellText = 0.0
                End With


                Dim _Attribute = (From c In dc.V_BIAttributes Where c.BID.Equals(_BI.BID) And c.FIELD.Equals(_field.FieldName) Order By c.AREA, c.AREAINDEX).FirstOrDefault()
                If _Attribute IsNot Nothing Then
                    With _field
                        '======================= Field =========================
                        '.ID = _tblAttributes.ATTRIBUTE
                        '.FieldName = _tblAttributes.FIELD
                        '.Caption = _tblAttributes.ATTRIBUTE
                        '.DisplayFolder = _tblAttributes.FOLDER
                        '======================= Area =========================
                        If _Attribute.AREA < 0 Then
                            .Visible = False
                        Else
                            .Visible = True
                            .Area = _Attribute.AREA
                        End If
                    End With
                End If




            Next

            pivotGrid.EndUpdate()

            'pivotGrid.DataBind()
        End Using


    End Sub


    Private Sub BindFilter()
        Using dc As New DataClasses_PortalBIExt()
            For Each _field As DevExpress.Web.ASPxPivotGrid.PivotGridField In pivotGrid.Fields
                If _field.Visible = True Then
                    Dim FilterValues = (From c In dc.tblBIFilters Where c.FIELD.Equals(_field.FieldName) And c.BID.Equals(hdBID.Value)).ToList()
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
                        _field.FilterValues.SetValues(_valuetype.ToArray(), PivotFilterType.Included, True)


                    End If
                End If
            Next
        End Using
    End Sub

    Private Sub BindSort()
        Using dc As New DataClasses_PortalBIExt()


            For Each _field As DevExpress.Web.ASPxPivotGrid.PivotGridField In pivotGrid.Fields
                'If _field.Visible = True Then
                Dim _Attributes = (From c In dc.tblBIAttributes Where c.FIELD.Equals(_field.FieldName) And c.BID.Equals(hdBID.Value)).FirstOrDefault()
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
            Dim data = (From c In dc.tblBIAttributes Where c.SortBySummaryInfo <> "" And c.BID = hdBID.Value).ToList()

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



    Protected Sub btnExportData_Click(sender As Object, e As EventArgs) Handles btnExportData.Click
        Export(1, False)

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
        tb.Font = New Font("Tahoma", 16, FontStyle.Regular)
        e.Graph.DrawBrick(tb)

    End Sub
    Private Sub GridTitle_CreateDetailArea(ByVal sender As Object, ByVal e As CreateAreaEventArgs)
        Dim tb As New TextBrick()
        tb.BorderWidth = 0
        tb.Rect = New Rectangle(200, 0, 400, 50)
        tb.HorzAlignment = DevExpress.Utils.HorzAlignment.Center
        tb.VertAlignment = DevExpress.Utils.VertAlignment.Bottom
        'tb.Text = pnEnquiry.HeaderText '"Additional Header information here..."
        tb.Font = New Font("Tahoma", 16, FontStyle.Regular)
        e.Graph.DrawBrick(tb)

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
    'Protected Sub btnChart_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnChart.Click
    '    popChart.ShowOnPageLoad = True

    '    If Not String.IsNullOrEmpty(ChartType.SelectedItem.Text) Then

    '        WebChart.SeriesTemplate.ChangeView(CType(System.Enum.Parse(GetType(ViewType), ChartType.SelectedItem.Text), ViewType))

    '    End If
    '    WebChart.DataSource = pivotGrid
    '    WebChart.DataBind()
    'End Sub
    Protected Sub btnChart_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnChart.Click
        popChart.ShowOnPageLoad = True

        BindChart()
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


    Protected Sub cbSaveBI_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbSaveBI.Callback
        Try
            Using dc As New DataClasses_PortalBIExt()
                Dim _BID = CInt(hdBID.Value)


                Dim _BI = (From c In dc.tblBIs Where c.BID.Equals(_BID)).FirstOrDefault()

                With _BI
                    .ShowColumnGrandTotals = cbShowColumnGrandTotals.Checked
                    .ShowColumnTotals = cbShowColumnTotals.Checked

                    .ShowRowGrandTotals = cbShowRowGrandTotals.Checked
                    .ShowRowTotals = cbShowRowTotals.Checked

                    .ShowGrandTotalsForSingleValues = cbShowGrandTotalsForSingleValues.Checked
                    .ShowTotalsForSingleValues = cbShowTotalsForSingleValues.Checked
                End With
                pivotGrid.OptionsView.ShowColumnGrandTotals = _BI.ShowColumnGrandTotals.Value
                pivotGrid.OptionsView.ShowColumnTotals = _BI.ShowColumnTotals.Value
                pivotGrid.OptionsView.ShowRowGrandTotals = _BI.ShowRowGrandTotals.Value
                pivotGrid.OptionsView.ShowRowTotals = _BI.ShowRowTotals.Value
                pivotGrid.OptionsView.ShowGrandTotalsForSingleValues = _BI.ShowGrandTotalsForSingleValues.Value
                pivotGrid.OptionsView.ShowTotalsForSingleValues = _BI.ShowTotalsForSingleValues.Value





                '================================ Delete Filter =========================
                Dim _BIFilter = (From c In dc.tblBIFilters Where c.BID.Equals(_BID)).ToList()
                dc.tblBIFilters.DeleteAllOnSubmit(_BIFilter)
                dc.SubmitChanges()

                '================================ Delete tblBIAttributes =========================
                Dim _BIAttribute = (From c In dc.tblBIAttributes Where c.BID.Equals(_BID)).ToList()
                dc.tblBIAttributes.DeleteAllOnSubmit(_BIAttribute)
                dc.SubmitChanges()


                '================================ Add tblBIAttribute_Users =========================
                Dim _newfields As New List(Of tblBIAttribute)
                For Each _field As DevExpress.Web.ASPxPivotGrid.PivotGridField In pivotGrid.Fields
                    If _field.Visible = True Then
                        Dim _BI_Field = (From c In dc.tblBIAttributes Where c.BID.Equals(_BID) And c.FIELD.Equals(_field.FieldName)).FirstOrDefault()
                        If _BI_Field IsNot Nothing Then
                            _BI_Field.AREA = _field.Area
                            _BI_Field.ORDERBY = _field.SortOrder
                            _BI_Field.AREAINDEX = _field.AreaIndex

                            If _field.SortBySummaryInfo.Field IsNot Nothing Then
                                _BI_Field.SortBySummaryInfo = _field.SortBySummaryInfo.Field.FieldName
                            Else
                                _BI_Field.SortBySummaryInfo = Nothing
                            End If

                            dc.SubmitChanges()



                        Else
                            Dim _newfield As New tblBIAttribute
                            _newfield.BID = _BID
                            _newfield.FIELD = _field.FieldName
                            _newfield.AREA = _field.Area
                            _newfield.ORDERBY = _field.SortOrder
                            _newfield.AREAINDEX = _field.AreaIndex

                            If _field.SortBySummaryInfo.Field IsNot Nothing Then _newfield.SortBySummaryInfo = _field.SortBySummaryInfo.Field.FieldName
                            _newfields.Add(_newfield)
                        End If

                    ElseIf _field.Visible = False Then
                        Dim _BI_Field = (From c In dc.tblBIAttributes Where c.BID.Equals(_BID) And c.FIELD.Equals(_field.FieldName)).FirstOrDefault()
                        If _BI_Field IsNot Nothing Then
                            dc.tblBIAttributes.DeleteOnSubmit(_BI_Field)
                            dc.SubmitChanges()
                        End If
                    End If
                Next

                If _newfields.Count > 0 Then
                    dc.tblBIAttributes.InsertAllOnSubmit(_newfields)
                    dc.SubmitChanges()
                End If





                '================================ Add Filter =========================
                Dim _NewFilters As New List(Of tblBIFilter)
                For Each _field As DevExpress.Web.ASPxPivotGrid.PivotGridField In pivotGrid.Fields
                    If _field.Visible = True Then
                        If _field.FilterValues.HasFilter Then
                            For Each itemvalue In _field.FilterValues.ValuesIncluded
                                _NewFilters.Add(New tblBIFilter With {.BID = _BID, .FIELD = _field.FieldName, .FILTER_TYPE = itemvalue.GetType().ToString(), .FILTER_VALUE = itemvalue.ToString()})
                            Next
                        End If
                    End If
                Next
                dc.tblBIFilters.InsertAllOnSubmit(_NewFilters)
                dc.SubmitChanges()





                'Dim _BIFilter = (From c In dc.tblBIFilters Where c.BID.Equals(_BID)).ToList()
                'dc.tblBIFilters.DeleteAllOnSubmit(_BIFilter)

                'Dim _BIAttribute = (From c In dc.tblBIAttributes Where c.BID.Equals(_BID)).ToList()
                'dc.tblBIAttributes.DeleteAllOnSubmit(_BIAttribute)


                'Dim _BIAttributes As New List(Of tblBIAttribute)
                'For Each _field As DevExpress.Web.ASPxPivotGrid.PivotGridField In pivotGrid.Fields
                '    If _field.Visible = True Then
                '        Dim _Attribute As New tblBIAttribute

                '        _Attribute.FIELD = _field.FieldName
                '        _Attribute.BID = _BID
                '        _Attribute.AREA = _field.Area
                '        _Attribute.ORDERBY = _field.SortOrder
                '        _Attribute.AREAINDEX = _field.AreaIndex
                '        If _field.FilterValues.HasFilter Then
                '            For Each itemvalue In _field.FilterValues.ValuesIncluded
                '                _Attribute.tblBIFilters.Add(New tblBIFilter With {.FIELD = _Attribute.FIELD, .BID = _Attribute.BID, .FILTER_TYPE = itemvalue.GetType().ToString(), .FILTER_VALUE = itemvalue.ToString()})
                '            Next
                '        End If
                '        _BIAttributes.Add(_Attribute)
                '    End If
                'Next

                'dc.tblBIAttributes.InsertAllOnSubmit(_BIAttributes)
                'dc.SubmitChanges()


                e.Result = "Save"
            End Using

        Catch ex As Exception
            e.Result = "error"
        End Try



    End Sub

    'Protected Sub ChartType_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChartType.ValueChanged

    '    WebChart.SeriesTemplate.ChangeView(CType(System.Enum.Parse(GetType(ViewType), ChartType.SelectedItem.Text), ViewType))
    '    WebChart.DataSource = pivotGrid
    '    WebChart.DataBind()
    'End Sub




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


    'Private Sub PivotGridControl1_CustomCellDisplayText(sender As System.Object, e As DevExpress.Web.ASPxPivotGrid.PivotCellDisplayTextEventArgs) Handles pivotGrid.
    '    If e.RowValueType = PivotGridValueType.GrandTotal Then
    '    ' do something
    '    'e.DataField = "Total"
    '    End If

    '    ' format row totals
    '    If e.ColumnValueType = PivotGridValueType.Total Then
    '        ' do something
    '        'e.ColumnCustomTotal = "Total"
    '    End If
    'End Sub
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

        'Dim ps As New PrintingSystem()

        'Dim link1 As New PrintableComponentLink(ps)
        ''WebChart.DataBind()
        'BindChart()
        'link1.Component = (CType(WebChart, IChartContainer)).Chart


        'Dim link2 As New PrintableComponentLink(ps)
        'link2.Component = ASPxPivotGridExporter1


        'Dim compositeLink As New CompositeLink(ps)
        'compositeLink.Links.AddRange(New Object() {link1, link2})

        'compositeLink.CreateDocument()
        'Using stream As New IO.MemoryStream()
        '    compositeLink.PrintingSystem.ExportToXls(stream)
        '    Response.Clear()
        '    Response.Buffer = False
        '    Response.AppendHeader("Content-Type", "application/xls")
        '    Response.AppendHeader("Content-Transfer-Encoding", "binary")
        '    Response.AppendHeader("Content-Disposition", "attachment; filename=test.xls")
        '    Response.BinaryWrite(stream.ToArray())
        '    Response.End()
        'End Using
        'ps.Dispose()
    End Sub

End Class
