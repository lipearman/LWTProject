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

Partial Class applications_BI_User_Preview
    Inherits System.Web.UI.Page
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

                'Try
                Dim _RID = CInt(Session("RID"))

                Using dc As New DataClasses_NissanMotorExt()

                    Dim _check_assign = (From c In dc.tblReport_Assignments Where c.RID.Equals(_RID) And c.UserName.ToLower().Equals(HttpContext.Current.User.Identity.Name)).FirstOrDefault()
                    If _check_assign Is Nothing Then
                        Response.Redirect("~/Admin/EditAccessDenied.aspx")
                        Return
                    End If

                    Dim _data = (From c In dc.V_REPORT_BIs Where c.RID.Equals(_RID)).FirstOrDefault()

                    If _data IsNot Nothing Then
                        hdRID.Value = _data.RID


                        InitTemplate(_RID)
                        BindMasterTemplate()

                        Dim _contexttable = _data.BIFile.ToString().Split(".")

                        hdContextTypeName.Value = _contexttable(0).ToString()
                        hdTableName.Value = _contexttable(1).ToString()

                        ObjectDataSource1.ContextTypeName = hdContextTypeName.Value
                        ObjectDataSource1.TableName = hdTableName.Value

                        pnEnquiry.Visible = True
                    End If

                End Using
                'Catch ex As Exception
                '    Throw ex

                'End Try


            End If
        End If
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        ObjectDataSource1.ContextTypeName = hdContextTypeName.Value
        ObjectDataSource1.TableName = hdTableName.Value
        If (Not IsPostBack) Then
            ChartType.SelectedIndex = 0

        End If


    End Sub

    Private Sub InitTemplate(ByVal _RID As String)

        Using dc As New DataClasses_NissanMotorExt()
            Dim _data = (From c In dc.V_REPORT_BIs Where c.RID.Equals(_RID)).FirstOrDefault()

            If _data IsNot Nothing Then
                Dim _NewFields As New List(Of tblReportUser_Template)

                'formLayout.Visible = True
                'pnEnquiry.HeaderText = _data.TITLE

                Dim sb As New StringBuilder()
                sb.AppendLine("SELECT V_REPORT_MASTER_TEMPLATE.FIELD_ID")
                sb.AppendLine(",V_REPORT_MASTER_TEMPLATE.FIELD_CAPTION as TEMPLATE_CAPTION")
                sb.AppendLine(",V_REPORT_MASTER_TEMPLATE.FIELD_NAME as TEMPLATE_NAME")
                sb.AppendLine(",V_REPORT_MASTER_TEMPLATE.SummaryType")
                sb.AppendLine(",V_REPORT_MASTER_TEMPLATE.CellFormat_FormatType")
                sb.AppendLine(",V_REPORT_MASTER_TEMPLATE.CellFormat_FormatString")

                sb.AppendLine(",V_REPORT_MASTER_TEMPLATE.UnboundExpressionMode")
                sb.AppendLine(",V_REPORT_MASTER_TEMPLATE.UnboundExpression")
                sb.AppendLine(",V_REPORT_MASTER_TEMPLATE.UnboundColumnType")

                sb.AppendLine(",V_REPORT_MASTER_TEMPLATE.GroupInterval")
                sb.AppendLine(",V_REPORT_MASTER_TEMPLATE.RID")
                sb.AppendLine(",V_REPORT_MASTER_TEMPLATE.MASTER_ID")
                sb.AppendLine(",isnull(V_REPORT_MASTER_TEMPLATE.AREA,-1) as TEMPLATE_AREA      ")
                sb.AppendLine(",V_REPORT_MASTER_TEMPLATE.ORDERBY as TEMPLATE_ORDERBY      ")
                sb.AppendLine(",isnull(tblReportUser_Template.ID,0) as ID ")
                sb.AppendLine(",tblReportUser_Template.OWNER")
                sb.AppendLine(",isnull(tblReportUser_Template.AREA,-1) AREA      ")
                sb.AppendLine("FROM V_REPORT_MASTER_TEMPLATE")
                sb.AppendLine("left join dbo.tblReportUser_Template")
                sb.AppendLine("on V_REPORT_MASTER_TEMPLATE.FIELD_ID = tblReportUser_Template.FIELD_ID")
                sb.AppendLine("and V_REPORT_MASTER_TEMPLATE.RID = tblReportUser_Template.RID")
                sb.AppendLine("and tblReportUser_Template.OWNER = '" & HttpContext.Current.User.Identity.Name & "'")
                sb.AppendLine("where V_REPORT_MASTER_TEMPLATE.RID = '" & _RID & "'")
                'sb.AppendLine("and V_REPORT_MASTER_TEMPLATE.AREA in(0,1,2,3) ")
                Dim _Fields = dc.ExecuteQuery(Of V_REPORT_USER_TEMPLATE)(sb.ToString()).ToList()



                For Each _field In _Fields
                    If _field.ID = 0 Then
                        _NewFields.Add(New tblReportUser_Template With {.FIELD_ID = _field.FIELD_ID _
                                                                       , .RID = _field.RID _
                                                                       , .AREA = _field.TEMPLATE_AREA _
                                                                       , .OWNER = HttpContext.Current.User.Identity.Name _
                                                                       , .ORDERBY = _field.TEMPLATE_ORDERBY})
                    End If

                Next

                'If _NewFields.Count > 0 Then
                '    dc.tblReportUser_Templates.InsertAllOnSubmit(_NewFields)
                '    dc.SubmitChanges()
                'End If



                If _NewFields.Count > 0 Then
                    dc.tblReportUser_Templates.InsertAllOnSubmit(_NewFields)
                    dc.SubmitChanges()


                    Dim _FilterValues As New List(Of tblReportUser_FilterValue)
                    Dim DataUser = (From c In dc.tblReportUser_Templates Where c.RID.Equals(_RID) And c.OWNER.Equals(HttpContext.Current.User.Identity.Name)).ToList()
                    For Each itemuser In DataUser
                        Dim DataMaster = (From c In dc.tblReportMaster_Templates Where c.RID.Equals(itemuser.RID) And c.FIELD_ID.Equals(itemuser.FIELD_ID)).FirstOrDefault()
                        If DataMaster IsNot Nothing Then

                            Dim DataMaster_Filter = (From c In dc.tblReportMaster_FilterValues Where c.MASTER_ID.Equals(DataMaster.MASTER_ID)).ToList()
                            For Each itemfilter In DataMaster_Filter

                                _FilterValues.Add(New tblReportUser_FilterValue With {.FILTER_TYPE = itemfilter.FILTER_TYPE _
                                                                                     , .FILTER_VALUE = itemfilter.FILTER_VALUE _
                                                                                     , .ID = itemuser.ID})

                            Next

                        End If
                    Next

                    If _FilterValues.Count > 0 Then
                        dc.tblReportUser_FilterValues.InsertAllOnSubmit(_FilterValues)
                        dc.SubmitChanges()
                    End If

                End If

            End If



        End Using


    End Sub



    Private Sub BindMasterTemplate()
        Using dc As New DataClasses_NissanMotorExt()
            Dim _RID = CInt(hdRID.Value)

            Dim _data = (From c In dc.tblReports Where c.RID.Equals(_RID)).FirstOrDefault()
            pivotGrid.Caption = ""
            pivotGrid.Fields.Clear()

            If _data IsNot Nothing Then
                If _data.VIEW_ID Is Nothing Then

                Else
                    pnEnquiry.HeaderText = _data.TITLE
                    Dim _Fields = (From c In dc.V_REPORT_USER_TEMPLATEs Where c.RID.Equals(_RID) And c.OWNER.Equals(HttpContext.Current.User.Identity.Name) Order By c.AREA, c.ORDERBY).ToList()



                    'For Each _field In _Fields
                    '    Dim _NewField As New DevExpress.Web.ASPxPivotGrid.PivotGridField()
                    '    With _NewField
                    '        If _field.AREA < 0 Then
                    '            .Visible = False
                    '        Else
                    '            .Area = _field.AREA
                    '        End If


                    '        '.Caption = _field.TEMPLATE_CAPTION
                    '        '.ID = _field.FIELD_ID
                    '        '.FieldName = _field.TEMPLATE_NAME


                    '        '.SummaryType = _field.SummaryType
                    '        '.CellFormat.FormatType = _field.CellFormat_FormatType
                    '        '.CellFormat.FormatString = _field.CellFormat_FormatString
                    '        '.GrandTotalCellFormat.FormatType = _field.CellFormat_FormatType
                    '        '.GrandTotalCellFormat.FormatString = _field.CellFormat_FormatString


                    '        .FieldName = _field.TEMPLATE_NAME
                    '        .Caption = _field.TEMPLATE_CAPTION
                    '        .ID = _field.FIELD_ID




                    '        '.ValueFormat.FormatType = _field.CellFormat_FormatType
                    '        '.ValueFormat.FormatString = _field.CellFormat_FormatString

                    '        .CellFormat.FormatType = _field.CellFormat_FormatType
                    '        .CellFormat.FormatString = _field.CellFormat_FormatString

                    '        .GrandTotalCellFormat.FormatType = _field.CellFormat_FormatType
                    '        .GrandTotalCellFormat.FormatString = _field.CellFormat_FormatString

                    '        .SummaryDisplayType = _field.PivotSummaryDisplayType

                    '        If _field.UnboundExpressionMode = UnboundExpressionMode.UseAggregateFunctions Then
                    '            If Not String.IsNullOrEmpty(_field.UnboundExpression) Then

                    '                .UnboundFieldName = _field.TEMPLATE_NAME
                    '                .UnboundExpressionMode = _field.UnboundExpressionMode
                    '                .UnboundExpression = _field.UnboundExpression
                    '                .UnboundType = _field.UnboundColumnType
                    '                .SummaryType = _field.SummaryType
                    '            Else
                    '                .SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Sum
                    '            End If

                    '        Else
                    '            .SummaryType = _field.SummaryType
                    '        End If


                    '        .GroupInterval = _field.GroupInterval

                    '        .EmptyValueText = 0
                    '        .EmptyCellText = 0


                    '        .Options.AllowSortBySummary = DevExpress.Utils.DefaultBoolean.True



                    '        ''============ Bind Master Filter =============                                                      
                    '        'Dim DataMaster_Filter = (From c In dc.tblReportMaster_FilterValues Where c.MASTER_ID.Equals(_field.MASTER_ID)).ToList()
                    '        'If DataMaster_Filter.Count > 0 Then
                    '        '    Dim _valuetype As New List(Of Object)
                    '        '    For Each item In DataMaster_Filter
                    '        '        Select Case item.FILTER_TYPE
                    '        '            Case "System.Double"
                    '        '                _valuetype.Add(Convert.ToDouble(item.FILTER_VALUE))
                    '        '            Case "System.Int32"
                    '        '                _valuetype.Add(Convert.ToInt32(item.FILTER_VALUE))
                    '        '            Case "System.DateTime"
                    '        '                _valuetype.Add(Convert.ToDateTime(item.FILTER_VALUE))
                    '        '            Case "System.String"
                    '        '                _valuetype.Add(item.FILTER_VALUE)
                    '        '            Case Else
                    '        '                _valuetype.Add(item.FILTER_VALUE)
                    '        '        End Select
                    '        '    Next

                    '        '    _NewField.Options.AllowFilter = DevExpress.Utils.DefaultBoolean.False
                    '        '    _NewField.FilterValues.ValuesIncluded = _valuetype.ToArray()
                    '        '    _NewField.FilterValues.FilterType = DevExpress.XtraPivotGrid.PivotFilterType.Included


                    '        'Else


                    '        Dim FilterValues = (From c In dc.tblReportUser_FilterValues Where c.ID.Equals(_field.ID)).ToList()
                    '        If FilterValues.Count > 0 Then
                    '            Dim _valuetype As New List(Of Object)
                    '            For Each item In FilterValues
                    '                Select Case item.FILTER_TYPE
                    '                    Case "System.Double"
                    '                        _valuetype.Add(Convert.ToDouble(item.FILTER_VALUE))
                    '                    Case "System.Int32"
                    '                        _valuetype.Add(Convert.ToInt32(item.FILTER_VALUE))
                    '                    Case "System.DateTime"
                    '                        _valuetype.Add(Convert.ToDateTime(item.FILTER_VALUE))
                    '                    Case "System.String"
                    '                        _valuetype.Add(item.FILTER_VALUE)
                    '                    Case Else
                    '                        _valuetype.Add(item.FILTER_VALUE)
                    '                End Select

                    '            Next
                    '            _NewField.Options.AllowFilter = DevExpress.Utils.DefaultBoolean.True
                    '            _NewField.FilterValues.ValuesIncluded = _valuetype.ToArray()
                    '            _NewField.FilterValues.FilterType = DevExpress.XtraPivotGrid.PivotFilterType.Included
                    '        End If

                    '        'End If


                    '    End With

                    '    pivotGrid.Fields.Add(_NewField)
                    'Next



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


                            .FieldName = _field.TEMPLATE_NAME
                            .Caption = _field.TEMPLATE_CAPTION
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

                                    .UnboundFieldName = _field.TEMPLATE_NAME
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



                            'Dim FilterValues = (From c In dc.tblReportMaster_FilterValues Where c.MASTER_ID.Equals(_field.MASTER_ID)).ToList()
                            'If FilterValues.Count > 0 Then
                            '    Dim _valuetype As New List(Of Object)
                            '    For Each item In FilterValues
                            '        Select Case item.FILTER_TYPE
                            '            Case "System.Double"
                            '                _valuetype.Add(Convert.ToDouble(item.FILTER_VALUE))
                            '            Case "System.Int32"
                            '                _valuetype.Add(Convert.ToInt32(item.FILTER_VALUE))
                            '            Case "System.DateTime"
                            '                _valuetype.Add(Convert.ToDateTime(item.FILTER_VALUE))
                            '            Case "System.String"
                            '                _valuetype.Add(item.FILTER_VALUE)
                            '            Case Else
                            '                _valuetype.Add(item.FILTER_VALUE)
                            '        End Select

                            '    Next

                            '    _NewField.FilterValues.ValuesIncluded = _valuetype.ToArray()
                            '    _NewField.FilterValues.FilterType = DevExpress.XtraPivotGrid.PivotFilterType.Included

                            'End If

                            ''============ Bind Master Filter =============                                                      
                            'Dim DataMaster_Filter = (From c In dc.tblReportMaster_FilterValues Where c.MASTER_ID.Equals(_field.MASTER_ID)).ToList()
                            'If DataMaster_Filter.Count > 0 Then
                            '    Dim _valuetype As New List(Of Object)
                            '    For Each item In DataMaster_Filter
                            '        Select Case item.FILTER_TYPE
                            '            Case "System.Double"
                            '                _valuetype.Add(Convert.ToDouble(item.FILTER_VALUE))
                            '            Case "System.Int32"
                            '                _valuetype.Add(Convert.ToInt32(item.FILTER_VALUE))
                            '            Case "System.DateTime"
                            '                _valuetype.Add(Convert.ToDateTime(item.FILTER_VALUE))
                            '            Case "System.String"
                            '                _valuetype.Add(item.FILTER_VALUE)
                            '            Case Else
                            '                _valuetype.Add(item.FILTER_VALUE)
                            '        End Select
                            '    Next

                            '    '_NewField.Options.AllowFilter = DevExpress.Utils.DefaultBoolean.False
                            '    _NewField.Options.AllowFilter = DevExpress.Utils.DefaultBoolean.True
                            '    _NewField.FilterValues.ValuesIncluded = _valuetype.ToArray()
                            '    _NewField.FilterValues.FilterType = DevExpress.XtraPivotGrid.PivotFilterType.Included


                            'Else


                            Dim FilterValues = (From c In dc.tblReportUser_FilterValues Where c.ID.Equals(_field.ID)).ToList()
                            If FilterValues.Count > 0 Then
                                Dim _valuetype As New List(Of Object)
                                For Each item In FilterValues


                                    Select Case item.FILTER_TYPE
                                        Case "System.Double"
                                            If Not _valuetype.Contains(Convert.ToDouble(item.FILTER_VALUE)) Then
                                                _valuetype.Add(Convert.ToDouble(item.FILTER_VALUE))
                                            End If

                                        Case "System.Int32"
                                            If Not _valuetype.Contains(Convert.ToInt32(item.FILTER_VALUE)) Then
                                                _valuetype.Add(Convert.ToInt32(item.FILTER_VALUE))
                                            End If

                                        Case "System.DateTime"
                                            'Dim format() = {"dd/mm/yyyy hh:mm:ss tt"}
                                            'Dim expenddt As Date
                                            'Date.TryParseExact(item.FILTER_VALUE, format,
                                            '    System.Globalization.DateTimeFormatInfo.InvariantInfo,
                                            '    System.Globalization.DateTimeStyles.None, expenddt)

                                            '_valuetype.Add(expenddt)
                                            If Not _valuetype.Contains(Convert.ToDateTime(item.FILTER_VALUE.Split(" ")(0))) Then
                                                _valuetype.Add(Convert.ToDateTime(item.FILTER_VALUE.Split(" ")(0)))
                                            End If


                                        Case "System.String"
                                            If Not _valuetype.Contains((item.FILTER_VALUE)) Then
                                                _valuetype.Add(item.FILTER_VALUE)
                                            End If

                                        Case Else
                                            If Not _valuetype.Contains((item.FILTER_VALUE)) Then
                                                _valuetype.Add(item.FILTER_VALUE)
                                            End If
                                    End Select




                                Next
                                'Try
                                _NewField.Options.AllowFilter = DevExpress.Utils.DefaultBoolean.True
                                _NewField.FilterValues.ValuesIncluded = _valuetype.ToArray()
                                _NewField.FilterValues.FilterType = DevExpress.XtraPivotGrid.PivotFilterType.Included

                                'Catch ex As Exception

                                'End Try
                            End If

                            'End If

                        End With





                        pivotGrid.Fields.Add(_NewField)
                    Next
                End If
            End If



        End Using
    End Sub






    Protected Sub btShowModal_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btShowModal.Click
        popPivot.ShowOnPageLoad = True

    End Sub

    Protected Sub btnChart_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnChart.Click
        popChart.ShowOnPageLoad = True
        WebChart.SeriesTemplate.ChangeView(CType(System.Enum.Parse(GetType(ViewType), ChartType.SelectedItem.Text), ViewType))
        WebChart.DataSource = pivotGrid
        WebChart.DataBind()
    End Sub

    Protected Sub ChartType_ValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ChartType.ValueChanged

        WebChart.SeriesTemplate.ChangeView(CType(System.Enum.Parse(GetType(ViewType), ChartType.SelectedItem.Text), ViewType))
        WebChart.DataSource = pivotGrid
        WebChart.DataBind()
    End Sub








    'Private Sub SetOptionsViewProperties()
    '    pivotGrid.OptionsView.ShowColumnGrandTotals = cbShowColumnGrandTotals.Checked
    '    pivotGrid.OptionsView.ShowColumnTotals = cbShowColumnTotals.Checked
    '    pivotGrid.OptionsView.ShowRowGrandTotals = cbShowRowGrandTotals.Checked
    '    pivotGrid.OptionsView.ShowRowTotals = cbShowRowTotals.Checked
    '    pivotGrid.OptionsView.ShowGrandTotalsForSingleValues = cbShowGrandTotalsForSingleValues.Checked
    '    pivotGrid.OptionsView.ShowTotalsForSingleValues = cbShowTotalsForSingleValues.Checked
    'End Sub
    'Private Sub SetOptionsViewCheckBoxes()
    '    cbShowColumnGrandTotals.Checked = pivotGrid.OptionsView.ShowColumnGrandTotals
    '    cbShowColumnTotals.Checked = pivotGrid.OptionsView.ShowColumnTotals
    '    cbShowRowGrandTotals.Checked = pivotGrid.OptionsView.ShowRowGrandTotals
    '    cbShowRowTotals.Checked = pivotGrid.OptionsView.ShowRowTotals
    '    cbShowGrandTotalsForSingleValues.Checked = pivotGrid.OptionsView.ShowGrandTotalsForSingleValues
    '    cbShowTotalsForSingleValues.Checked = pivotGrid.OptionsView.ShowTotalsForSingleValues
    'End Sub



    Protected Sub cbSaveBI_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbSaveBI.Callback
        Try
            Using dc As New DataClasses_NissanMotorExt()
                Dim _RID = CInt(hdRID.Value)
                Dim i = 0
                For Each _field As DevExpress.Web.ASPxPivotGrid.PivotGridField In pivotGrid.Fields

                    If _field.Options.AllowFilter <> DevExpress.Utils.DefaultBoolean.False Then

                        Dim UserTemplateField = (From c In dc.V_REPORT_USER_TEMPLATEs Where c.FIELD_ID.Equals(_field.ID) And c.RID.Equals(_RID) And c.OWNER.Equals(HttpContext.Current.User.Identity.Name)).FirstOrDefault()

                        If UserTemplateField IsNot Nothing Then

                            Dim UserField = (From c In dc.tblReportUser_Templates Where c.ID.Equals(UserTemplateField.ID)).FirstOrDefault()

                            If _field.Visible = False Then
                                UserField.AREA = -1
                            Else
                                UserField.AREA = _field.Area
                            End If

                            UserField.ORDERBY = _field.AreaIndex
                            i += 1



                            dc.ExecuteCommand("delete tblReportUser_FilterValues where ID=" & UserField.ID)
                            dc.SubmitChanges()


                            If _field.FilterValues.Count > 0 Then

                                Dim _Values As New List(Of tblReportUser_FilterValue)
                                For Each itemvalue In _field.FilterValues.ValuesIncluded
                                    _Values.Add(New tblReportUser_FilterValue With {.FILTER_TYPE = itemvalue.GetType().ToString(), .FILTER_VALUE = itemvalue.ToString(), .ID = UserField.ID})
                                Next

                                If _Values.Count > 0 Then
                                    dc.tblReportUser_FilterValues.InsertAllOnSubmit(_Values)
                                End If
                            End If
                            dc.SubmitChanges()


                        End If





                    End If


                Next



                e.Result = "Save"
            End Using

        Catch ex As Exception
            e.Result = "error"
            'btnGenFields.Visible = True
        End Try



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

    Protected Sub btnExportData_Click(sender As Object, e As EventArgs) Handles btnExportData.Click
        'ASPxPivotGridExporter1.ExportXlsxToResponse(Guid.NewGuid().ToString(), False)

        'ASPxPivotGridExporter1.ExportXlsxToResponse(Guid.NewGuid().ToString(), New DevExpress.XtraPrinting.XlsxExportOptionsEx With {.ExportType = DevExpress.Export.ExportType.WYSIWYG})


        Export(1, False)
    End Sub

    Private Sub pivotGrid_FieldValueDisplayText(sender As Object, e As DevExpress.Web.ASPxPivotGrid.PivotFieldDisplayTextEventArgs) Handles pivotGrid.FieldValueDisplayText
        If e.ValueType = DevExpress.XtraPivotGrid.PivotGridValueType.Total Then
            e.DisplayText = "Total"
        End If
        If e.ValueType = DevExpress.XtraPivotGrid.PivotGridValueType.GrandTotal Then
            e.DisplayText = "Total"
        End If
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






End Class
