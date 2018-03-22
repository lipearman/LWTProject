Imports Portal.Components
Imports DevExpress.Web.ASPxTreeList
Imports DevExpress.Web
Imports System.Data.SqlClient
Imports System.Data
Imports DevExpress.Web.ASPxPivotGrid
Imports DevExpress.Data.Linq
Imports System.Reflection
Imports System.Linq.Expressions
'Imports Portal.Web.BI
Imports System.Runtime.Remoting
Imports DevExpress.XtraPivotGrid
Imports DevExpress.XtraPrinting
Imports DevExpress.Export
Imports DevExpress.XtraCharts
Imports DevExpress.Data.PivotGrid

'Imports System.Data.Linq

'Imports DevExpress.XtraPrinting
Imports DevExpress.XtraCharts.Native
Imports DevExpress.XtraPrintingLinks
Imports System.IO

Partial Class applications_BI_Preview_Preview
    Inherits System.Web.UI.Page
    Private Const MaxTopValueCount As Integer = 20

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Init
        If (Not IsPostBack) Then


            Dim restrictedTypes() As ViewType = {ViewType.PolarArea, ViewType.PolarLine, ViewType.SideBySideGantt, ViewType.SideBySideRangeBar, ViewType.RangeBar, ViewType.Gantt, ViewType.PolarPoint, ViewType.Stock, ViewType.CandleStick, ViewType.Bubble}
            For Each type As ViewType In System.Enum.GetValues(GetType(ViewType))
                If Array.IndexOf(Of ViewType)(restrictedTypes, type) >= 0 Then
                    Continue For
                End If
                ChartType.Items.Add(type.ToString())
            Next type

            If Session("RID") IsNot Nothing Then
                Try
                    Dim _RID = CInt(Session("RID"))

                    Using dc As New DataClasses_NissanMotorExt()

                        Dim _data = (From c In dc.V_REPORT_BIs Where c.RID.Equals(_RID)).FirstOrDefault()

                        If _data IsNot Nothing Then

                            InitMasterTemplate(_RID)

                            hdRID.Value = _data.RID


                            BindMasterTemplate()


                            pnEnquiry.Visible = True


                            Dim _contexttable = _data.BIFile.ToString().Split(".")

                            hdContextTypeName.Value = _contexttable(0).ToString()
                            hdTableName.Value = _contexttable(1).ToString()

                            ObjectDataSource1.ContextTypeName = hdContextTypeName.Value
                            ObjectDataSource1.TableName = hdTableName.Value
                        End If

                    End Using
                Catch ex As Exception
                    Dim Msg = ex.Message
                End Try


            End If
        End If

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        ObjectDataSource1.ContextTypeName = hdContextTypeName.Value
        ObjectDataSource1.TableName = hdTableName.Value


        If (Not IsPostBack) Then
            ChartType.SelectedIndex = 0
            'ddlTopCount.SelectedIndex = 0
            'SetSortBySummary()

            'Dim _RID = CInt(Session("RID"))
            'Using dc As New DataClasses_NissanMotorExt()
            '    Dim _report = (From c In dc.V_REPORT_BIs Where c.RID.Equals(_RID)).FirstOrDefault()

            '    If _report IsNot Nothing Then

            '        'ObjectDataSource1.CommandType = CommandType.Text
            '        'ObjectDataSource1.Command.Connection.ConnectionString = _report.VIEW_CONNECTION
            '        'ObjectDataSource1.Command.CommandText = _report.VIEW_QUERY

            '        'ObjectDataSource1.SelectCommand = _report.VIEW_QUERY
            '        'ObjectDataSource1.ConnectionString = _report.VIEW_CONNECTION
            '        'ObjectDataSource1.SelectCommandType = SqlDataSourceCommandType.Text

            '        'pivotGrid.DataSourceID = "ObjectDataSource1"


            '        'ObjectDataSource1.ConnectionString = _report.VIEW_CONNECTION
            '        'If String.IsNullOrEmpty(_report.DESCRIPTION) Then
            '        '    ObjectDataSource1.SelectCommand = _report.VIEW_QUERY
            '        'Else
            '        '    ObjectDataSource1.SelectCommand = _report.DESCRIPTION
            '        'End If


            '        'pivotGrid.DataBind()

            '    End If

            'End Using


        End If


    End Sub

    'Protected Sub SqlDataSource1_Selecting(ByVal sender As Object, ByVal e As EntityDataSourceSelectedEventArgs) Handles ObjectDataSource1.Selecting
    '    e.



    'End Sub



    'Protected Sub SqlDataSource1_Selecting(ByVal sender As Object, ByVal e As LinqServerModeDataSourceSelectEventArgs) Handles ObjectDataSource1.Selecting



    '    Dim _RID = CInt(Session("RID"))
    '    Using dc As New DataClasses_NissanMotorExt()
    '        Dim _report = (From c In dc.V_REPORT_BIs Where c.RID.Equals(_RID)).FirstOrDefault()

    '        If _report IsNot Nothing Then


    '            Dim dc2 As New DataClasses_NissanMotorDataContext(_report.VIEW_CONNECTION)

    '            If String.IsNullOrEmpty(_report.DESCRIPTION) Then
    '                Dim a = dc2.ExecuteQuery(Of V_Nissan)(_report.VIEW_QUERY).ToList()
    '                e.QueryableSource = a.Where(Function(c) c.AppStatus = 3)
    '            Else

    '                Dim a = dc.ExecuteQuery(Of V_Nissan)(_report.DESCRIPTION).ToList()

    '                e.QueryableSource = (From c In a Select c)
    '            End If


    '        End If


    '    End Using


    'Dim _RID = CInt(Session("RID"))
    'Using dc As New DataClasses_NissanMotorExt()
    '    Dim _report = (From c In dc.V_REPORT_BIs Where c.RID.Equals(_RID)).FirstOrDefault()

    '    If _report IsNot Nothing Then
    '        e.Command.CommandType = CommandType.Text
    '        e.Command.Connection.ConnectionString = _report.VIEW_CONNECTION
    '        e.Command.CommandText = _report.VIEW_QUERY

    '        pivotGrid.DataSourceID = "ObjectDataSource1"
    '    End If

    'End Using


    'End Sub

    'Protected Sub ObjectDataSource1_Selecting(ByVal sender As Object, ByVal e As SqlDataSourceSelectingEventArgs) Handles ObjectDataSource1.Selecting
    '    Dim _RID = CInt(Session("RID"))
    '    Using dc As New DataClasses_NissanMotorExt()
    '        Dim _report = (From c In dc.V_REPORT_BIs Where c.RID.Equals(_RID)).FirstOrDefault()

    '        If _report IsNot Nothing Then

    '            e.Command.CommandType = CommandType.Text
    '            e.Command.Connection.ConnectionString = _report.VIEW_CONNECTION
    '            e.Command.CommandText = _report.VIEW_QUERY


    '            If String.IsNullOrEmpty(_report.DESCRIPTION) Then
    '                e.Command.CommandText = _report.VIEW_QUERY
    '            Else
    '                e.Command.CommandText = _report.DESCRIPTION
    '            End If



    '            ObjectDataSource1.DataBind()

    '        End If

    '    End Using


    'End Sub

    'Protected Sub ObjectDataSource1_Init(ByVal sender As Object, ByVal e As EventArgs) Handles ObjectDataSource1.Init
    '    Dim _RID = CInt(Session("RID"))
    '    Using dc As New DataClasses_NissanMotorExt()
    '        Dim _report = (From c In dc.V_REPORT_BIs Where c.RID.Equals(_RID)).FirstOrDefault()

    '        If _report IsNot Nothing Then
    '            'ObjectDataSource1.CommandType = CommandType.Text
    '            'e.Command.Connection.ConnectionString = _report.VIEW_CONNECTION
    '            'e.Command.CommandText = _report.VIEW_QUERY

    '            ObjectDataSource1.ConnectionString = _report.VIEW_CONNECTION
    '            'ObjectDataSource1.SelectCommand = _report.VIEW_QUERY

    '            If String.IsNullOrEmpty(_report.DESCRIPTION) Then
    '                ObjectDataSource1.SelectCommand = _report.VIEW_QUERY
    '            Else
    '                ObjectDataSource1.SelectCommand = _report.DESCRIPTION
    '            End If


    '            'pivotGrid.DataSourceID = "ObjectDataSource1"
    '        End If

    '    End Using
    'End Sub



    'Protected Sub ddlTopCount_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlTopCount.SelectedIndexChanged
    '    If ddlTopCount.SelectedIndex > 0 Then
    '        rdoField.Visible = True
    '        rdoData.Visible = True


    '        rdoField.SelectedIndex = 0
    '        rdoData.SelectedIndex = 0
    '    Else
    '        rdoField.Visible = False
    '        rdoData.Visible = False
    '    End If
    'End Sub




    'Private Sub SetSortBySummary()
    '    ddlField.Items.Clear()
    '    For Each field As DevExpress.Web.ASPxPivotGrid.PivotGridField In pivotGrid.Fields
    '        If field.Area = PivotArea.RowArea Then
    '            ddlField.Items.Add(New ListEditItem(field.Caption, field.FieldName))
    '        End If
    '    Next field


    '    'rdoField.Items.Clear()
    '    'rdoData.Items.Clear()
    '    'For Each field As DevExpress.Web.ASPxPivotGrid.PivotGridField In pivotGrid.Fields
    '    '    If field.Area = PivotArea.RowArea Then 
    '    '        rdoField.Items.Add(New ListEditItem(field.Caption, field.FieldName))
    '    '    End If

    '    '    If field.Area = PivotArea.DataArea Then
    '    '        rdoData.Items.Add(New ListEditItem(field.Caption, field.FieldName))
    '    '    End If
    '    'Next field

    '    'If rdoData.SelectedItem Is Nothing Then
    '    '    Return
    '    'End If
    '    'Dim fieldOrderAmount As DevExpress.Web.ASPxPivotGrid.PivotGridField = pivotGrid.Fields(rdoData.SelectedItem.Value)
    '    'For Each field As DevExpress.Web.ASPxPivotGrid.PivotGridField In pivotGrid.Fields
    '    '    If field IsNot fieldOrderAmount Then
    '    '        field.Visible = False
    '    '    End If
    '    'Next field
    '    'Dim selField As DevExpress.Web.ASPxPivotGrid.PivotGridField = pivotGrid.Fields(rdoField.SelectedItem.Value)
    '    'If selField Is Nothing Then
    '    '    Return
    '    'End If
    '    'selField.Visible = True
    '    'selField.TopValueCount = Integer.Parse(ddlTopCount.SelectedItem.Text)

    'End Sub

    Protected Sub btShowModal_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShowModal.Click
        popPivot.ShowOnPageLoad = True

    End Sub

    Protected Sub btnChart_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnChart.Click
        popChart.ShowOnPageLoad = True
        WebChart.SeriesTemplate.ChangeView(CType(System.Enum.Parse(GetType(ViewType), ChartType.SelectedItem.Text), ViewType))
        WebChart.DataSource = pivotGrid
        WebChart.DataBind()
    End Sub

    'Protected Sub btnTop_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnTop.Click
    '    'popTop.ShowOnPageLoad = True
    '    'SetSortBySummary()
    'End Sub

    Protected Sub ChartType_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChartType.ValueChanged

        WebChart.SeriesTemplate.ChangeView(CType(System.Enum.Parse(GetType(ViewType), ChartType.SelectedItem.Text), ViewType))
        WebChart.DataSource = pivotGrid
        WebChart.DataBind()
    End Sub

    Private Sub InitMasterTemplate(ByVal _RID As String)
        Using dc As New DataClasses_NissanMotorExt()


            'Dim _Fields = (From c In dc.V_REPORT_MASTER_TEMPLATEs Where c.RID.Equals(_RID)).ToList()
            'If _Fields.Count = 0 Then
            '    Dim newMasterField As New List(Of tblReportMaster_Template)

            '    Dim i = 0
            '    For Each _field In _Fields
            '        newMasterField.Add(New tblReportMaster_Template With {.FIELD_ID = _field.FIELD_ID, .RID = _field.RID, .AREA = -1, .ORDERBY = i})
            '        i += 1
            '    Next

            '    dc.tblReportMaster_Templates.InsertAllOnSubmit(newMasterField)
            '    dc.SubmitChanges()

            'Else

            Dim _newFields = (From c In dc.V_REPORT_MASTER_TEMPLATEs Where c.RID.Equals(_RID) And c.MASTER_ID Is Nothing).ToList()
            If _newFields.Count > 0 Then
                Dim newMasterField As New List(Of tblReportMaster_Template)
                Dim i = 0
                For Each _field In _newFields
                    newMasterField.Add(New tblReportMaster_Template With {.FIELD_ID = _field.FIELD_ID, .RID = _field.RID, .AREA = -1, .ORDERBY = i})
                    i += 1
                Next
                dc.tblReportMaster_Templates.InsertAllOnSubmit(newMasterField)
                dc.SubmitChanges()
            End If
            'End If


        End Using

    End Sub

    Private Sub BindMasterTemplate()

        Dim _RID = CInt(hdRID.Value)

        Using dc As New DataClasses_NissanMotorExt()

            Dim _data = (From c In dc.tblReports Where c.RID.Equals(_RID)).FirstOrDefault()
            pivotGrid.Caption = ""
            pivotGrid.Fields.Clear()

            If _data IsNot Nothing Then
                If _data.VIEW_ID Is Nothing Then

                Else
                    pnEnquiry.HeaderText = _data.TITLE
                    Dim _Fields = (From c In dc.V_REPORT_MASTER_TEMPLATEs Where c.RID.Equals(_RID) Order By c.AREA, c.ORDERBY).ToList()
                    For Each _field In _Fields
                        Dim _NewField As New DevExpress.Web.ASPxPivotGrid.PivotGridField() ' = New PivotGridField("InsurerEngGroup", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
                        With _NewField

                            .CellStyle.Wrap = DevExpress.Utils.DefaultBoolean.False
                            .ValueStyle.Wrap = DevExpress.Utils.DefaultBoolean.False
                            .HeaderStyle.Wrap = DevExpress.Utils.DefaultBoolean.False
                            .ValueTotalStyle.Wrap = DevExpress.Utils.DefaultBoolean.False

                            If _field.MASTER_ID IsNot Nothing Then

                                If _field.AREA < 0 Then
                                    .Visible = False
                                Else
                                    .Area = _field.AREA
                                End If

                            Else
                                .Visible = False
                            End If


                            .FieldName = _field.FIELD_NAME
                            .Caption = _field.FIELD_CAPTION
                            .ID = _field.FIELD_ID

                            .CellStyle.Wrap = DevExpress.Utils.DefaultBoolean.False


                            '.ValueFormat.FormatType = _field.CellFormat_FormatType
                            '.ValueFormat.FormatString = _field.CellFormat_FormatString

                            .CellFormat.FormatType = _field.CellFormat_FormatType
                            .CellFormat.FormatString = _field.CellFormat_FormatString

                            .GrandTotalCellFormat.FormatType = _field.CellFormat_FormatType
                            .GrandTotalCellFormat.FormatString = _field.CellFormat_FormatString

                            .SummaryDisplayType = _field.PivotSummaryDisplayType

                            If _field.UnboundExpressionMode = UnboundExpressionMode.UseAggregateFunctions Then
                                If Not String.IsNullOrEmpty(_field.UnboundExpression) Then

                                    .UnboundFieldName = _field.FIELD_NAME
                                    .UnboundExpressionMode = _field.UnboundExpressionMode
                                    .UnboundExpression = _field.UnboundExpression
                                    .UnboundType = _field.UnboundColumnType
                                    .SummaryType = _field.SummaryType

                                    .UseNativeFormat = DevExpress.Utils.DefaultBoolean.False

                                Else
                                    .SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Sum
                                End If

                            Else
                                .SummaryType = _field.SummaryType
                            End If


                            .UseNativeFormat = DevExpress.Utils.DefaultBoolean.False


                            .ExportBestFit = True

                            ''field.SummaryDisplayType= PivotSummaryDisplayType.PercentOfGrandTotal
                            'Dim a = PivotSummaryDisplayType.PercentOfGrandTotal


                            .GroupInterval = _field.GroupInterval

                            .EmptyValueText = 0.0
                            .EmptyCellText = 0.0



                            Dim FilterValues = (From c In dc.tblReportMaster_FilterValues Where c.MASTER_ID.Equals(_field.MASTER_ID)).ToList()
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

                                _NewField.FilterValues.ValuesIncluded = _valuetype.ToArray()
                                _NewField.FilterValues.FilterType = DevExpress.XtraPivotGrid.PivotFilterType.Included




                            End If




                            '.Options.AllowSortBySummary = DevExpress.Utils.DefaultBoolean.True
                            'If _NewField.FieldName = "RISKII" Then
                            '    .TopValueCount = 10
                            '    .SortBySummaryInfo.FieldName = "PREM_IN_T"
                            '    .SortOrder = PivotSortOrder.Descending
                            'End If






                        End With





                        pivotGrid.Fields.Add(_NewField)
                    Next
                End If
            End If



        End Using
    End Sub

    Protected Sub cbSaveBI_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbSaveBI.Callback
        Try
            Using dc As New DataClasses_NissanMotorExt()
                Dim _RID = CInt(hdRID.Value)

                For Each _field As DevExpress.Web.ASPxPivotGrid.PivotGridField In pivotGrid.Fields


                    Dim UserField = (From c In dc.V_REPORT_MASTER_TEMPLATEs Where c.FIELD_ID.Equals(_field.ID) And c.RID.Equals(_RID)).FirstOrDefault()




                    If UserField.MASTER_ID Is Nothing Then


                    Else
                        Dim _Master_Templates = (From c In dc.tblReportMaster_Templates Where c.MASTER_ID.Equals(UserField.MASTER_ID)).FirstOrDefault()
                        If _field.Visible = False Then
                            _Master_Templates.AREA = -1
                        Else
                            _Master_Templates.AREA = _field.Area
                        End If

                        _Master_Templates.ORDERBY = _field.AreaIndex

                        dc.ExecuteCommand("delete tblReportMaster_FilterValues where MASTER_ID=" & UserField.MASTER_ID)

                        If _field.FilterValues.Count > 0 Then
                            Dim _Values As New List(Of tblReportMaster_FilterValue)


                            For Each itemvalue In _field.FilterValues.ValuesIncluded
                                _Values.Add(New tblReportMaster_FilterValue With {.FILTER_TYPE = itemvalue.GetType().ToString(), .FILTER_VALUE = itemvalue.ToString(), .MASTER_ID = UserField.MASTER_ID})

                            Next

                            If _Values.Count > 0 Then
                                dc.tblReportMaster_FilterValues.InsertAllOnSubmit(_Values)
                            End If


                        End If
                        dc.SubmitChanges()
                    End If





                Next



                e.Result = "Save"
            End Using

        Catch ex As Exception
            e.Result = "error"
            'btnGenFields.Visible = True
        End Try



    End Sub

    Protected Sub btnExportData_Click(sender As Object, e As EventArgs) Handles btnExportData.Click

        Export(1, False)
    End Sub


    Private Sub pivotGridControl1_CustomCellDisplayText(sender As Object, e As EventArgs) Handles pivotGrid.AfterPerformCallback
        'SetSortBySummary()
        'If ddlField.SelectedItem Is Nothing Then
        '    ddlField.Text = ""
        'End If
    End Sub
    'Private Sub ddlTopCount_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlTopCount.SelectedIndexChanged

    '    Dim fieldOrderAmount As DevExpress.Web.ASPxPivotGrid.PivotGridField = pivotGrid.Fields("PREM_IN_T")
    '    For Each field As DevExpress.Web.ASPxPivotGrid.PivotGridField In pivotGrid.Fields
    '        If field IsNot fieldOrderAmount Then
    '            'field.Area = PivotArea.FilterArea
    '        End If
    '    Next field
    '    Dim selField As DevExpress.Web.ASPxPivotGrid.PivotGridField = pivotGrid.Fields(ddlField.SelectedItem.Value)
    '    If selField Is Nothing Then
    '        Return
    '    End If
    '    selField.Area = PivotArea.RowArea
    '    selField.TopValueCount = Integer.Parse(ddlTopCount.SelectedItem.Value)

    'End Sub


    Private Sub pivotGrid_FieldValueDisplayText(sender As Object, e As DevExpress.Web.ASPxPivotGrid.PivotFieldDisplayTextEventArgs) Handles pivotGrid.FieldValueDisplayText
        If e.ValueType = DevExpress.XtraPivotGrid.PivotGridValueType.Total Then
            e.DisplayText = "Total"
        End If
        If e.ValueType = DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal Then
            e.DisplayText = "Total"
        End If
    End Sub

    'Private Sub pivotGridControl1_CustomCellDisplayText(sender As Object, e As DevExpress.Web.ASPxPivotGrid.PivotCellDisplayTextEventArgs) Handles pivotGrid.CustomCellDisplayText



    '    If e.DataField Is Nothing Then
    '        Return
    '    End If




    '    Select Case e.DataField.FieldName
    '        Case "NETBROK_P"



    '            Try
    '                Dim _NetIncome As Decimal = Convert.ToDecimal(e.GetFieldValue(pivotGrid.Fields("BROK_IN_T")))
    '                Dim _Premium As Decimal = Convert.ToDecimal(e.GetFieldValue(pivotGrid.Fields("PREM_IN_T")))
    '                Dim v As Decimal = If((_Premium = 0), 0, ((_NetIncome) / _Premium))
    '                e.DisplayText = String.Format("{0:P1}", v)
    '            Catch ex As Exception
    '                e.DisplayText = String.Format("{0:P1}", 0)
    '            End Try

    '        Case "ORIN_P"


    '            Try
    '                Dim _ORIN As Decimal = Convert.ToDecimal(e.GetFieldValue(pivotGrid.Fields("ORIN")))
    '                Dim _Premium As Decimal = Convert.ToDecimal(e.GetFieldValue(pivotGrid.Fields("PREM_IN_T")))
    '                Dim v As Decimal = If((_Premium = 0), 0, ((_ORIN) / _Premium))
    '                e.DisplayText = String.Format("{0:P1}", v)
    '            Catch ex As Exception
    '                e.DisplayText = String.Format("{0:P1}", 0)
    '            End Try

    '        Case "ADMININ1_P"

    '            Try
    '                Dim _ADMININ1 As Decimal = Convert.ToDecimal(e.GetFieldValue(pivotGrid.Fields("ADMININ1")))
    '                Dim _Premium As Decimal = Convert.ToDecimal(e.GetFieldValue(pivotGrid.Fields("PREM_IN_T")))
    '                Dim v As Decimal = If((_Premium = 0), 0, ((_ADMININ1) / _Premium))
    '                e.DisplayText = String.Format("{0:P1}", v)
    '            Catch ex As Exception
    '                e.DisplayText = String.Format("{0:P1}", 0)
    '            End Try

    '        Case "ADMININ2_P"

    '            Try
    '                Dim _ADMININ2 As Decimal = Convert.ToDecimal(e.GetFieldValue(pivotGrid.Fields("ADMININ2")))
    '                Dim _Premium As Decimal = Convert.ToDecimal(e.GetFieldValue(pivotGrid.Fields("PREM_IN_T")))
    '                Dim v As Decimal = If((_Premium = 0), 0, ((_ADMININ2) / _Premium))
    '                e.DisplayText = String.Format("{0:P1}", v)

    '            Catch ex As Exception
    '                e.DisplayText = String.Format("{0:P1}", 0)
    '            End Try
    '        Case "COMMIN_P"


    '            Try
    '                Dim _COMMIN As Decimal = Convert.ToDecimal(e.GetFieldValue(pivotGrid.Fields("COMMIN")))
    '                Dim _Premium As Decimal = Convert.ToDecimal(e.GetFieldValue(pivotGrid.Fields("PREM_IN_T")))
    '                Dim v As Decimal = If((_Premium = 0), 0, ((_COMMIN) / _Premium))
    '                e.DisplayText = String.Format("{0:P1}", v)
    '            Catch ex As Exception
    '                e.DisplayText = String.Format("{0:P1}", 0)
    '            End Try

    '    End Select





    'End Sub

    'Private Sub pivotGridControl1_CustomSummary(sender As Object, e As DevExpress.Web.ASPxPivotGrid.PivotGridCustomSummaryEventArgs) Handles pivotGrid.CustomSummary


    '    Select Case e.FieldName
    '        Case "NETBROK_P"


    '            Try
    '                Dim nsum_Premium As Decimal = 0
    '                Dim dsum_NetIncome As Decimal = 0
    '                Dim ds As PivotDrillDownDataSource = e.CreateDrillDownDataSource()
    '                For i As Integer = 0 To ds.RowCount - 1
    '                    Dim row As PivotDrillDownDataRow = ds(i)
    '                    Dim _NetIncome As Decimal = Convert.ToDecimal(row("BROK_IN_T"))
    '                    Dim _Premium As Decimal = Convert.ToDecimal(row("PREM_IN_T"))
    '                    dsum_NetIncome += _NetIncome
    '                    nsum_Premium += _Premium
    '                Next
    '                e.CustomValue = If((nsum_Premium = 0), 0, dsum_NetIncome / nsum_Premium)

    '            Catch ex As Exception
    '                e.CustomValue = 0
    '            End Try

    '        Case "ORIN_P"

    '            Try
    '                Dim nsum_Premium As Decimal = 0
    '                Dim dsum_NetIncome As Decimal = 0
    '                Dim ds As PivotDrillDownDataSource = e.CreateDrillDownDataSource()
    '                For i As Integer = 0 To ds.RowCount - 1
    '                    Dim row As PivotDrillDownDataRow = ds(i)
    '                    Dim _NetIncome As Decimal = Convert.ToDecimal(row("ORIN"))
    '                    Dim _Premium As Decimal = Convert.ToDecimal(row("PREM_IN_T"))
    '                    dsum_NetIncome += _NetIncome
    '                    nsum_Premium += _Premium
    '                Next
    '                e.CustomValue = If((nsum_Premium = 0), 0, dsum_NetIncome / nsum_Premium)


    '            Catch ex As Exception
    '                e.CustomValue = 0
    '            End Try
    '        Case "ADMININ1_P"


    '            Try
    '                Dim nsum_Premium As Decimal = 0
    '                Dim dsum_NetIncome As Decimal = 0
    '                Dim ds As PivotDrillDownDataSource = e.CreateDrillDownDataSource()
    '                For i As Integer = 0 To ds.RowCount - 1
    '                    Dim row As PivotDrillDownDataRow = ds(i)
    '                    Dim _NetIncome As Decimal = Convert.ToDecimal(row("ADMININ1"))
    '                    Dim _Premium As Decimal = Convert.ToDecimal(row("PREM_IN_T"))
    '                    dsum_NetIncome += _NetIncome
    '                    nsum_Premium += _Premium
    '                Next
    '                e.CustomValue = If((nsum_Premium = 0), 0, dsum_NetIncome / nsum_Premium)

    '            Catch ex As Exception
    '                e.CustomValue = 0
    '            End Try
    '        Case "ADMININ2_P"

    '            Try
    '                Dim nsum_Premium As Decimal = 0
    '                Dim dsum_NetIncome As Decimal = 0
    '                Dim ds As PivotDrillDownDataSource = e.CreateDrillDownDataSource()
    '                For i As Integer = 0 To ds.RowCount - 1
    '                    Dim row As PivotDrillDownDataRow = ds(i)
    '                    Dim _NetIncome As Decimal = Convert.ToDecimal(row("ADMININ2"))
    '                    Dim _Premium As Decimal = Convert.ToDecimal(row("PREM_IN_T"))
    '                    dsum_NetIncome += _NetIncome
    '                    nsum_Premium += _Premium
    '                Next
    '                e.CustomValue = If((nsum_Premium = 0), 0, dsum_NetIncome / nsum_Premium)



    '            Catch ex As Exception
    '                e.CustomValue = 0
    '            End Try
    '        Case "COMMIN_P"

    '            Try
    '                Dim nsum_Premium As Decimal = 0
    '                Dim dsum_NetIncome As Decimal = 0
    '                Dim ds As PivotDrillDownDataSource = e.CreateDrillDownDataSource()
    '                For i As Integer = 0 To ds.RowCount - 1
    '                    Dim row As PivotDrillDownDataRow = ds(i)
    '                    Dim _COMMIN As Decimal = Convert.ToDecimal(row("COMMIN"))
    '                    Dim _Premium As Decimal = Convert.ToDecimal(row("PREM_IN_T"))
    '                    dsum_NetIncome += _COMMIN
    '                    nsum_Premium += _Premium
    '                Next
    '                e.CustomValue = If((nsum_Premium = 0), 0, dsum_NetIncome / nsum_Premium)

    '            Catch ex As Exception
    '                e.CustomValue = 0
    '            End Try
    '    End Select

    'End Sub

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

    'Private Sub Export(ByVal _ExportFormat As Integer, ByVal saveAs As Boolean)


    '    Dim ps As New PrintingSystem()

    '    Dim link1 As New PrintableComponentLink()
    '    link1.Component = ASPxPivotGridExporter1
    '    link1.PrintingSystem = ps

    '    Dim link2 As New PrintableComponentLink()
    '    WebChart.DataBind()
    '    link2.Component = (CType(WebChart, IChartContainer)).Chart
    '    link2.PrintingSystem = ps

    '    Dim compositeLink As New CompositeLink()
    '    compositeLink.Links.AddRange(New Object() {link2, link1})
    '    compositeLink.PrintingSystem = ps
    '    compositeLink.Landscape = True

    '    compositeLink.CreateDocument()

    '    'compositeLink.PrintingSystem.ExportOptions.Pdf.DocumentOptions.Author = "Test"
    '    compositeLink.PrintingSystem.ExportOptions.Xlsx.DocumentOptions.Author = "Test"

    '    Using stream As New MemoryStream()

    '        Dim options = New XlsxExportOptions()
    '        options.ExportMode = XlsxExportMode.SingleFilePageByPage

    '        compositeLink.PrintingSystem.ExportToXlsx(stream, options)
    '        Response.Clear()
    '        Response.Buffer = False
    '        Response.AppendHeader("Content-Type", "application/ms-excel")
    '        'Response.AppendHeader("Content-Transfer-Encoding", "binary")
    '        Response.AppendHeader("Content-Disposition", "attachment; filename=test.xlsx")
    '        Response.BinaryWrite(stream.GetBuffer())
    '        Response.End()
    '    End Using

    '    ps.Dispose()


    'End Sub

End Class
