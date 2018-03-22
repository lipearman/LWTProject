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
Imports System.ComponentModel
Imports DevExpress.XtraCharts.Web
Imports System.Drawing
Imports DevExpress.XtraPivotGrid.Localization
Imports DevExpress.XtraCharts.Native
Imports System.Drawing.Imaging

Partial Class applications_BI_Master_PreviewExcel
    Inherits System.Web.UI.Page
    Private Const MaxTopValueCount As Integer = 20
    Private bounded As Boolean
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

            Session("DSGUID") = Nothing

            If Session("BID") Is Nothing Then
                Response.End()
            Else
                'Dim _DSGUID = CInt(Session("DSGUID"))
                'hdBID.Value = _DSGUID
                BindMasterTemplate(Session("BID"))
                BindSort()
                BindFilter(Session("BID"))




                pivotGrid.DataBind()
            End If
        End If


    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        'WebChart.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Center
        'WebChart.Legend.AlignmentVertical = LegendAlignmentVertical.BottomOutside
        'WebChart.Legend.MaxVerticalPercentage = 100
        'WebChart.Legend.MaxHorizontalPercentage = 100



        Dim columnIndexValue As String = ColumnIndex.Value, rowIndexValue As String = RowIndex.Value
        If (Not String.IsNullOrEmpty(columnIndexValue)) AndAlso (Not String.IsNullOrEmpty(rowIndexValue)) AndAlso ASPxPopupControl1.ShowOnPageLoad Then
            BindGridView(columnIndexValue, rowIndexValue)
        End If
        pivotGrid.ClientSideEvents.CellClick = GetJSCellClickHandler()
        ASPxPopupControl1.ClientSideEvents.Closing = GetJSPopupClosingHandler()

        If ASPxGridView1.IsCallback AndAlso (Not String.IsNullOrEmpty(columnIndexValue)) AndAlso (Not String.IsNullOrEmpty(rowIndexValue)) Then
            ASPxGridView1.JSProperties.Add("cpShowDrillDownWindow", False)
        End If

        'If (Not IsPostBack) AndAlso (Not IsCallback) Then
        '    Dim restrictedTypes() As ViewType = {ViewType.PolarArea, ViewType.PolarLine, ViewType.SideBySideGantt, ViewType.SideBySideRangeBar, ViewType.RangeBar, ViewType.Gantt, ViewType.PolarPoint, ViewType.Stock, ViewType.CandleStick, ViewType.Bubble}
        '    For Each type As ViewType In System.Enum.GetValues(GetType(ViewType))
        '        If Array.IndexOf(Of ViewType)(restrictedTypes, type) >= 0 Then
        '            Continue For
        '        End If
        '        ChartType.Items.Add(type.ToString())
        '    Next type
        '    ChartType.SelectedIndex = 0
        'End If




        If (Not IsPostBack) AndAlso (Not Page.IsCallback) Then
            SetOptionsViewCheckBoxes()
        Else
            SetOptionsViewProperties()
        End If



    End Sub

    Private Sub BindChart()
        WebChart.SeriesTemplate.ChangeView(CType(System.Enum.Parse(GetType(ViewType), "Bar"), ViewType))
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

            legenttitle = WebChart.Legend.Title.Text
        Else
            WebChart.Legend.Title.Visible = False
            legenttitle = ""

        End If

        WebChart.Legend.TextVisible = True


        WebChart.DataSource = pivotGrid
        WebChart.DataBind()

        Dim diagram As XYDiagram = TryCast(WebChart.Diagram, XYDiagram)
        diagram.AxisY.Label.TextPattern = "{V:N0}"
        'diagram.AxisX.Label.TextPattern = "{V:#,0}"
        'WebChart.Series.Label.TextPattern = "{V:#,0}"
        'Dim a = WebChart.Series(0).LegendTextPattern = "{V:#,0}"
        'WebChart.DataSource = pivotGrid
        WebChart.DataBind()



        'chartDesigner.OpenChart(WebChart)
    End Sub
    Protected Sub WebChartControl1_CustomDrawSeries(ByVal sender As Object, ByVal e As CustomDrawSeriesEventArgs) Handles WebChart.CustomDrawSeries


        'Dim sb As New StringBuilder()

        'Dim _fd As New List(Of DevExpress.Web.ASPxPivotGrid.PivotGridField)



        'For Each item As DevExpress.Web.ASPxPivotGrid.PivotGridField In pivotGrid.Fields
        '    _fd.Add(item)
        'Next

        'Dim _fields = (From c In _fd Order By c.AreaIndex).ToList()

        'For Each _field In _fields
        '    If _field.Area = PivotArea.DataArea Then
        '        sb.Append("|" & _field.Caption)
        '        'Exit For
        '    End If
        'Next


        ''Dim a = ASPxPivotCustomizationControl2.




        'e.LegendText = e.LegendText & sb.ToString()

    End Sub



    Private Sub BindSort()
        Using dc As New DataClasses_PortalBIExt()
            Dim data = (From c In dc.tblDataSourceBI_Fields Where c.SortBySummaryInfo <> "" And c.BID = hdBID.Value).ToList()

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


    Private Sub BindFilter(ByVal _BID As Integer)


        Using dc As New DataClasses_PortalBIExt()
            For Each _field As DevExpress.Web.ASPxPivotGrid.PivotGridField In pivotGrid.Fields
                If _field.Visible = True Then
                    Dim FilterValues = (From c In dc.tblDataSourceBI_Field_Filters Where c.FIELD_NAME.Equals(_field.FieldName) And c.BID.Equals(_BID)).ToList()
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



    Private Sub BindMasterTemplate(ByVal _BID As String)

        Using dc As New DataClasses_PortalBIExt()
            Dim _BI = (From c In dc.tblDataSourceBIs Where c.BID.Equals(_BID)).FirstOrDefault()
            popChart.HeaderText = _BI.TITLE

            pnEnquiry.HeaderText = String.Format("{0} - {1}", _BI.TITLE, _BI.DESCRIPTION)

            hdBID.Value = _BI.BID.ToString()

            Dim _datasource = (From c In dc.tblDataSourceFiles Where c.ID.Equals(_BI.DS_ID)).FirstOrDefault()
            pivotGrid.Caption = ""
            pivotGrid.Fields.Clear()

            Session("DSGUID") = _datasource.GUID

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


            If _datasource IsNot Nothing Then


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

                                'If Not String.IsNullOrEmpty(_BI_Fields.SortBySummaryInfo) Then


                                '    Dim _NewFieldSummaryInfo As New DevExpress.Web.ASPxPivotGrid.PivotGridField()
                                '    _NewFieldSummaryInfo.FieldName = _BI_Fields.SortBySummaryInfo

                                '    .SortBySummaryInfo.Field = _NewFieldSummaryInfo



                                'End If
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

                        '======================= UseAggregateFunctions =========================
                        If _field.UnboundExpressionMode = UnboundExpressionMode.UseAggregateFunctions Then
                            If Not String.IsNullOrEmpty(_field.UnboundExpression) Then

                                .UnboundFieldName = _field.FIELD_NAME
                                .UnboundExpressionMode = _field.UnboundExpressionMode
                                .UnboundExpression = _field.UnboundExpression
                                .UnboundType = _field.UnboundColumnType
                                .SummaryType = _field.SummaryType

                                 
                                .AllowedAreas = PivotGridAllowedAreas.DataArea

                            Else
                                .SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Sum
                            End If

                        Else
                            .SummaryType = _field.SummaryType
                        End If


                        ''============================== Filter ==========================
                        ''Try
                        'Dim FilterValues = (From c In dc.tblDataSourceBI_Field_Filters Where c.FIELD_NAME.Equals(_field.FIELD_NAME) And c.BID.Equals(_BI.BID.ToString())).ToList()
                        'If FilterValues.Count > 0 Then
                        '    Dim _valuetype As New List(Of Object)
                        '    For Each item In FilterValues
                        '        Select Case item.FILTER_TYPE
                        '            Case "System.Double"
                        '                _valuetype.Add(Convert.ToDouble(item.FILTER_VALUE))
                        '            Case "System.Int32"
                        '                _valuetype.Add(Convert.ToInt32(item.FILTER_VALUE))
                        '            Case "System.DateTime"
                        '                '_valuetype.Add(Convert.ToDateTime(item.FILTER_VALUE))
                        '                _valuetype.Add(item.FILTER_VALUE)
                        '            Case "System.String"
                        '                _valuetype.Add(item.FILTER_VALUE)
                        '            Case Else
                        '                _valuetype.Add(item.FILTER_VALUE)
                        '        End Select
                        '    Next
                        '    .FilterValues.SetValues(_valuetype.ToArray(), PivotFilterType.Included, True)
                        'End If
                        ''Catch ex As Exception

                        ''End Try




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

    Protected Sub cbSaveBI_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbSaveBI.Callback

        Using dc As New DataClasses_PortalBIExt()
            Dim _BID = CInt(hdBID.Value)






            Dim _BI = (From c In dc.tblDataSourceBIs Where c.BID.Equals(_BID)).FirstOrDefault()

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
            Dim _Filters = (From c In dc.tblDataSourceBI_Field_Filters Where c.BID.Equals(_BID)).ToList()
            dc.tblDataSourceBI_Field_Filters.DeleteAllOnSubmit(_Filters)
            dc.SubmitChanges()

            '================================ Add Filter =========================
            Dim _NewFilters As New List(Of tblDataSourceBI_Field_Filter)
            For Each _field As DevExpress.Web.ASPxPivotGrid.PivotGridField In pivotGrid.Fields
                If _field.Visible = True Then
                    If _field.FilterValues.HasFilter Then
                        For Each itemvalue In _field.FilterValues.ValuesIncluded
                            _NewFilters.Add(New tblDataSourceBI_Field_Filter With {.BID = _BID, .FIELD_NAME = _field.FieldName, .FILTER_TYPE = itemvalue.GetType().ToString(), .FILTER_VALUE = itemvalue.ToString()})
                        Next
                    End If
                End If
            Next
            dc.tblDataSourceBI_Field_Filters.InsertAllOnSubmit(_NewFilters)
            dc.SubmitChanges()
            ''=================================== Update Order  ==================================
            'Dim _Fields = (From c In dc.tblDataSourceBI_Fields Where c.BID.Equals(_BID)).ToList()
            'For Each _field In _Fields
            '    _field.AREA = -1
            '    _field.ORDERBY = 0
            '    _field.AREAINDEX = -1

            '    For Each _pivotfield As DevExpress.Web.ASPxPivotGrid.PivotGridField In pivotGrid.Fields
            '        If _field.FIELD = _pivotfield.FieldName And _pivotfield.Visible = True Then
            '            _field.AREA = _pivotfield.Area
            '            _field.ORDERBY = _pivotfield.SortOrder
            '            _field.AREAINDEX = _pivotfield.AreaIndex
            '            Exit For
            '        End If

            '    Next
            'Next




            Dim _newfields As New List(Of tblDataSourceBI_Field)
            For Each _field As DevExpress.Web.ASPxPivotGrid.PivotGridField In pivotGrid.Fields
                If _field.Visible = True Then
                    Dim _BI_Field = (From c In dc.tblDataSourceBI_Fields Where c.BID.Equals(_BID) And c.FIELD.Equals(_field.FieldName)).FirstOrDefault()
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
                        Dim _newfield As New tblDataSourceBI_Field
                        _newfield.BID = _BID
                        _newfield.FIELD = _field.FieldName
                        _newfield.AREA = _field.Area
                        _newfield.ORDERBY = _field.SortOrder
                        _newfield.AREAINDEX = _field.AreaIndex

                        If _field.SortBySummaryInfo.Field IsNot Nothing Then _newfield.SortBySummaryInfo = _field.SortBySummaryInfo.Field.FieldName
                        _newfields.Add(_newfield)
                    End If

                ElseIf _field.Visible = False Then
                    Dim _BI_Field = (From c In dc.tblDataSourceBI_Fields Where c.BID.Equals(_BID) And c.FIELD.Equals(_field.FieldName)).FirstOrDefault()
                    If _BI_Field IsNot Nothing Then
                        dc.tblDataSourceBI_Fields.DeleteOnSubmit(_BI_Field)
                        dc.SubmitChanges()
                    End If
                End If
            Next

            If _newfields.Count > 0 Then
                dc.tblDataSourceBI_Fields.InsertAllOnSubmit(_newfields)
                dc.SubmitChanges()
            End If




            e.Result = "Save"
        End Using

    End Sub

    'Protected Sub ChartType_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChartType.ValueChanged
    '    WebChart.SeriesTemplate.ChangeView(CType(System.Enum.Parse(GetType(ViewType), ChartType.SelectedItem.Text), ViewType))
    '    WebChart.DataSource = pivotGrid
    '    WebChart.DataBind()
    'End Sub





    Protected Sub pivotGridControl1_FieldValueDisplayText(sender As Object, e As DevExpress.Web.ASPxPivotGrid.PivotFieldDisplayTextEventArgs) Handles pivotGrid.FieldValueDisplayText
        If e.ValueType = PivotGridValueType.GrandTotal Then
            If e.IsColumn Then
                'e.DisplayText = "Total"

            Else
                e.DisplayText = "Grand Total"
            End If


        End If
    End Sub





    Protected Sub bnExportXLS_Click(sender As Object, e As EventArgs)
        GridDrillDownExporter.WriteXlsxToResponse(System.Guid.NewGuid().ToString(), False, New DevExpress.XtraPrinting.XlsxExportOptionsEx With {.ExportType = DevExpress.Export.ExportType.WYSIWYG})
    End Sub



    Protected Sub ASPxGridView1_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs) Handles ASPxGridView1.CustomCallback
        Dim param() As String = e.Parameters.Split("|"c)
        If param(0) <> "D" Then
            Return
        End If
        BindGridView(ColumnIndex.Value, RowIndex.Value)
        ASPxGridView1.PageIndex = 0

        If e.Parameters = "D" Then
            ASPxGridView1.JSProperties("cpShowDrillDownWindow") = True
        End If
    End Sub
    Protected Sub BindGridView(ByVal columnIndex As String, ByVal rowIndex As String)
        If bounded Then
            Return
        End If
        bounded = True
        'ASPxGridView1.DataSource = ConvertPivotDrillDownDataSourceToDataTable(pivotGrid.CreateDrillDownDataSource(Int32.Parse(columnIndex), Int32.Parse(rowIndex)))
        ASPxGridView1.DataSource = pivotGrid.CreateDrillDownDataSource(Int32.Parse(columnIndex), Int32.Parse(rowIndex))

        ASPxGridView1.DataBind()
    End Sub
    Protected Function GetJSCellClickHandler() As String
        Return String.Format("function (s, e) {{" & ControlChars.CrLf & " var columnIndex = document.getElementById('{0}')," & ControlChars.CrLf & " rowIndex = document.getElementById('{1}');" & ControlChars.CrLf & " columnIndex.value = e.ColumnIndex;" & ControlChars.CrLf & " rowIndex.value = e.RowIndex;" & ControlChars.CrLf & " GridView.PerformCallback('D');" & ControlChars.CrLf & " ShowDrillDown();" & ControlChars.CrLf & "}}", ColumnIndex.ClientID, RowIndex.ClientID)
    End Function
    Protected Function GetJSPopupClosingHandler() As String
        Return String.Format("function (s, e) {{" & ControlChars.CrLf & " var columnIndex = document.getElementById('{0}')," & ControlChars.CrLf & " rowIndex = document.getElementById('{1}');" & ControlChars.CrLf & " columnIndex.value = '';" & ControlChars.CrLf & " rowIndex.value = '';" & ControlChars.CrLf & " GridView.SetVisible(false);" & ControlChars.CrLf & " " & ControlChars.CrLf & "}}", ColumnIndex.ClientID, RowIndex.ClientID)
    End Function


    Protected Function ConvertPivotDrillDownDataSourceToDataTable(source As PivotDrillDownDataSource) As DataTable
        Dim result As New DataTable()
        Dim dataProperties As ITypedList = TryCast(source, ITypedList)
        If dataProperties Is Nothing Then
            Return result
        End If
        For Each prop As PropertyDescriptor In dataProperties.GetItemProperties(Nothing)
            result.Columns.Add(prop.Name, prop.PropertyType)
        Next
        For row As Integer = 0 To source.RowCount - 1
            Dim values As New List(Of Object)()
            For Each col As DataColumn In result.Columns
                values.Add(source.GetValue(row, col.ColumnName))
            Next
            result.Rows.Add(values.ToArray())
        Next
        Return result
    End Function







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
    Protected Sub WebChartControl1_CustomDrawSeriesPoint(ByVal sender As Object, ByVal e As CustomDrawSeriesPointEventArgs) Handles WebChart.CustomDrawSeriesPoint
        'e.LegendText = CStr((CType(e.SeriesPoint.Tag, DataRowView))("OfficialName"))
        'e.LabelText = "LabelText"
        'e.LegendText = "LegendText"
        'e.SecondLabelText = "SecondLabelText"


    End Sub


    'Protected Sub WebChartControl1_CustomDrawCrosshair(ByVal sender As Object, ByVal e As CustomDrawCrosshairEventArgs) Handles WebChart.CustomDrawCrosshair
    '    For Each item In e.CrosshairElementGroups
    '        item.HeaderElement.Text = "xxx"
    '    Next

    'End Sub

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


    End Sub


    'Private Sub pivotGridControl1_CustomSummary(sender As Object, e As DevExpress.Web.ASPxPivotGrid.PivotGridCustomSummaryEventArgs) Handles pivotGrid.CustomSummary
    '    Dim source As PivotDrillDownDataSource = e.CreateDrillDownDataSource()
    '    If e.DataField.FieldName = "Member Count" Then
    '        e.CustomValue = source.OfType(Of PivotDrillDownDataRow)().[Select](Function(r) r("Member")).Distinct().Count()
    '    End If
    '    'If e.DataField.FieldName = "CountDistinct" Then
    '    '    e.CustomValue = source.OfType(Of PivotDrillDownDataRow)().Where(Function(r) Convert.ToInt32(r("ID")) = 1).Count()
    '    'End If

    'End Sub




    'Private Sub PivotGridControl1_CustomSummary(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxPivotGrid.PivotGridCustomSummaryEventArgs) Handles pivotGrid.CustomSummary
    '    Dim name As String = e.DataField.FieldName
    '    If e.DataField.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Custom Then
    '        Dim list As IList = e.CreateDrillDownDataSource()
    '        Dim ht As Hashtable = New Hashtable
    '        For i As Integer = 0 To list.Count - 1
    '            Dim row As DevExpress.XtraPivotGrid.PivotDrillDownDataRow = CType(list(i), DevExpress.XtraPivotGrid.PivotDrillDownDataRow)
    '            Dim v As Object = row(name)
    '            If Not IsNothing(v) AndAlso (Not v Is DBNull.Value) Then
    '                ht(v) = v
    '            End If
    '        Next
    '        e.CustomValue = ht.Count
    '    End If
    'End Sub

    'Protected Sub ASPxPivotGrid1_CustomCellValue(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxPivotGrid.PivotCellValueEventArgs) Handles pivotGrid.CustomCellValue

    '    ' Exits if the processed cell does not belong to a Custom Total.
    '    If e.ColumnCustomTotal Is Nothing AndAlso e.RowCustomTotal Is Nothing Then
    '        Return
    '    End If

    '    '' Obtains a list of summary values against which
    '    '' the Custom Total will be calculated.
    '    'Dim summaryValues As ArrayList = GetSummaryValues(e)

    '    '' Obtains the name of the Custom Total that should be calculated.
    '    'Dim customTotalName As String = GetCustomTotalName(e)

    '    '' Calculates the Custom Total value and assigns it to the Value event parameter.
    '    'e.Value = GetCustomTotalValue(summaryValues, customTotalName)
    'End Sub


    '' Returns the Custom Total name.
    'Private Function GetCustomTotalName(ByVal e As DevExpress.Web.ASPxPivotGrid.PivotCellValueEventArgs) As String
    '    Return If(e.ColumnCustomTotal IsNot Nothing,
    '              e.ColumnCustomTotal.Tag.ToString(),
    '              e.RowCustomTotal.Tag.ToString())
    'End Function
    '' Returns a list of summary values against which
    '' a Custom Total will be calculated.
    'Private Function GetSummaryValues(ByVal e As DevExpress.Web.ASPxPivotGrid.PivotCellValueEventArgs) As ArrayList
    '    Dim values As New ArrayList()

    '    ' Creates a summary data source.
    '    Dim sds As PivotSummaryDataSource = e.CreateSummaryDataSource()

    '    ' Iterates through summary data source records
    '    ' and copies summary values to an array.
    '    For i As Integer = 0 To sds.RowCount - 1
    '        Dim value As Object = sds.GetValue(i, e.DataField)
    '        If value Is Nothing Then
    '            Continue For
    '        End If
    '        values.Add(value)
    '    Next i

    '    ' Sorts summary values.
    '    values.Sort()

    '    ' Returns the summary values array.
    '    Return values
    'End Function
    '' Returns the Custom Total value by an array of summary values.
    'Private Function GetCustomTotalValue(ByVal values As ArrayList,
    '                                     ByVal customTotalName As String) As Object

    '    ' Returns a null value if the provided array is empty.
    '    If values.Count = 0 Then
    '        Return Nothing
    '    End If

    '    ' If the Median Custom Total should be calculated,
    '    ' calls the GetMedian method.
    '    If customTotalName = "Median" Then
    '        Return GetMedian(values)
    '    End If

    '    ' If the Quartiles Custom Total should be calculated,
    '    ' calls the GetQuartiles method.
    '    If customTotalName = "Quartiles" Then
    '        Return GetQuartiles(values)
    '    End If

    '    ' Otherwise, returns a null value.
    '    Return Nothing
    'End Function
    '' Calculates a median for the specified sorted sample.
    'Private Function GetMedian(ByVal values As ArrayList) As Decimal
    '    If (values.Count Mod 2) = 0 Then
    '        Return (DirectCast(values(values.Count \ 2 - 1), Decimal) +
    '                DirectCast(values(values.Count \ 2), Decimal)) / 2
    '    Else
    '        Return DirectCast(values(values.Count \ 2), Decimal)
    '    End If
    'End Function

    '' Calculates the first and third quartiles for the specified sorted sample
    '' and returns them inside a formatted string.
    'Private Function GetQuartiles(ByVal values As ArrayList) As String
    '    Dim part1 As New ArrayList()
    '    Dim part2 As New ArrayList()
    '    If (values.Count Mod 2) = 0 Then
    '        part1 = values.GetRange(0, values.Count \ 2)
    '        part2 = values.GetRange(values.Count \ 2, values.Count \ 2)
    '    Else
    '        part1 = values.GetRange(0, values.Count \ 2 + 1)
    '        part2 = values.GetRange(values.Count \ 2, values.Count \ 2 + 1)
    '    End If
    '    Return String.Format("({0}, {1})", GetMedian(part1).ToString("c2"),
    '                         GetMedian(part2).ToString("c2"))
    'End Function
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