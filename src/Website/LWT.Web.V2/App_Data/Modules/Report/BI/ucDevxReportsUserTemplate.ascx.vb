Imports Portal.Components
Imports DevExpress.Web.ASPxTreeList
Imports DevExpress.Web
Imports System.Data.SqlClient
Imports System.Data
Imports DevExpress.Web.ASPxPivotGrid
'Imports Portal.Web.BI
Imports DevExpress.Data.Linq


Partial Class Modules_ucDevxReportsUserTemplate
    Inherits PortalModuleControl

    Public Function PreviewBI(ByVal RID As String, ByVal VIEW_ID As String) As String
        Dim sb As New StringBuilder
        If Not String.IsNullOrEmpty(RID) And Not String.IsNullOrEmpty(VIEW_ID) Then

            sb.AppendFormat("<a class=""dxeHyperlink_Office2010Blue"" href=""javascript:ShowUserBIPopup('{0}');"" style=""font-weight: bold;"">View</a>", RID)

        End If
        Return sb.ToString()
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If (Not IsPostBack) Then
            '    treeList.DataBind()
            '    Dim firstUnread As TreeListNode = treeList.FindNodeByFieldValue("IsNew", True)
            '    firstUnread.Focus()
            '    treeList.ExpandToLevel(2)
            '    'messageLiteral.Text = GetEntryText(firstUnread)

            Session("RID") = Nothing


            'pivotGrid.OptionsView.ShowColumnGrandTotals = True
            'pivotGrid.OptionsView.ShowColumnTotals = True
            'pivotGrid.OptionsView.ShowRowGrandTotals = True
            'pivotGrid.OptionsView.ShowRowTotals = True
            'pivotGrid.OptionsView.ShowGrandTotalsForSingleValues = True
            'pivotGrid.OptionsView.ShowTotalsForSingleValues = True



            'SqlDataSource_Data.SelectParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name

            'treeList.DataBind()
            



        End If



    End Sub

    Sub Recursive(ByVal _ParentID As Integer, ByVal _Reports As List(Of tblReport), ByRef _RID As List(Of Integer))

        'If value >= 100 Then
        '    Return value
        'End If
        Dim _Parent = (From c In _Reports Where c.RID = _ParentID).FirstOrDefault()
        If _Parent IsNot Nothing Then
            _RID.Add(_ParentID)
            If _Parent.ParentID IsNot Nothing Then
                Recursive(_Parent.ParentID, _Reports, _RID)
            End If
        End If
    End Sub
    Protected Sub cbPreview_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbPreview.Callback
        Dim RID = CInt(e.Parameter)
        Session("RID") = RID
        e.Result = RID
    End Sub


    'Protected Sub SqlDataSource1_Selecting(ByVal sender As Object, ByVal e As SqlDataSourceSelectingEventArgs) Handles ObjectDataSource1.Selecting
    '    Dim _RID = CInt(Session("RID"))
    '    Using dc As New DataClasses_LWTReportsExt()
    '        Dim _report = (From c In dc.V_REPORT_BIs Where c.RID.Equals(_RID)).FirstOrDefault()

    '        If _report IsNot Nothing Then
    '            e.Command.CommandType = CommandType.Text
    '            e.Command.Connection.ConnectionString = _report.VIEW_CONNECTION
    '            e.Command.CommandText = _report.VIEW_QUERY

    '            pivotGrid.DataSourceID = "ObjectDataSource1"
    '        End If

    '    End Using


    'End Sub
    'Protected Sub SqlDataSource1_Selecting(ByVal sender As Object, ByVal e As EventArgs) Handles ObjectDataSource1.Init
    '    Dim _RID = CInt(Session("RID"))
    '    Using dc As New DataClasses_LWTReportsExt()
    '        Dim _report = (From c In dc.V_REPORT_BIs Where c.RID.Equals(_RID)).FirstOrDefault()

    '        If _report IsNot Nothing Then
    '            'ObjectDataSource1.CommandType = CommandType.Text
    '            'e.Command.Connection.ConnectionString = _report.VIEW_CONNECTION
    '            'e.Command.CommandText = _report.VIEW_QUERY

    '            ObjectDataSource1.ConnectionString = _report.VIEW_CONNECTION
    '            ObjectDataSource1.SelectCommand = _report.VIEW_QUERY

    '            pivotGrid.DataSourceID = "ObjectDataSource1"
    '        End If

    '    End Using


    'End Sub

    Protected Sub callbackPanel_view_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgs) 'Handles callbackPanel_view.Callback

        Dim _RID = e.Parameter.ToString()
        Session("RID") = _RID

        'pivotGrid.Caption = ""
        'formLayout.Visible = False
        'pivotGrid.Fields.Clear()


        Using dc As New DataClasses_LWTReportsExt()
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
                sb.AppendLine(",V_REPORT_MASTER_TEMPLATE.GrandTotalCellFormat_FormatType")
                sb.AppendLine(",V_REPORT_MASTER_TEMPLATE.GrandTotalCellFormat_FormatString")
                sb.AppendLine(",V_REPORT_MASTER_TEMPLATE.GroupInterval")
                sb.AppendLine(",V_REPORT_MASTER_TEMPLATE.RID")
                sb.AppendLine(",V_REPORT_MASTER_TEMPLATE.MASTER_ID")
                sb.AppendLine(",V_REPORT_MASTER_TEMPLATE.AREA as TEMPLATE_AREA      ")
                sb.AppendLine(",isnull(tblReportUser_Template.ID,0) as ID ")
                sb.AppendLine(",tblReportUser_Template.OWNER")
                sb.AppendLine(",isnull(tblReportUser_Template.AREA,-1) AREA      ")
                sb.AppendLine("FROM LWTReports.dbo.V_REPORT_MASTER_TEMPLATE")
                sb.AppendLine("left join dbo.tblReportUser_Template")
                sb.AppendLine("on V_REPORT_MASTER_TEMPLATE.FIELD_ID = tblReportUser_Template.FIELD_ID")
                sb.AppendLine("and V_REPORT_MASTER_TEMPLATE.RID = tblReportUser_Template.RID")
                sb.AppendLine("and tblReportUser_Template.OWNER = '" & HttpContext.Current.User.Identity.Name & "'")
                sb.AppendLine("where V_REPORT_MASTER_TEMPLATE.RID = '" & _RID & "'")
                Dim _Fields = dc.ExecuteQuery(Of V_REPORT_USER_TEMPLATE)(sb.ToString()).ToList()


                'Dim _Fields = (From c In dc.V_REPORT_USER_TEMPLATEs Where c.RID.Equals(_RID) And (c.OWNER.Equals(HttpContext.Current.User.Identity.Name) Or c.OWNER Is Nothing)).ToList()



                'Dim _MasterFields = (From z In dc.V_REPORT_MASTER_TEMPLATEs Where z.RID.Equals(_RID) _
                '         Group Join p In dc.V_REPORT_USER_TEMPLATEs On z.RID Equals p.RID Into Group _
                '         From gp In Group.DefaultIfEmpty() Select z, gp.ID, gp.OWNER).ToList()

                'Dim q = (From item In _userProfileRepository.Table
                '                Let Country = (From p In _countryRepository.Table Where p.CountryId = item.CurrentLocationCountry Select p.Name).FirstOrDefault
                '                Let State = (From p In _stateRepository.Table Where p.CountryId = item.CurrentLocationCountry Select p.Name).FirstOrDefault
                '                Let City = (From p In _stateRepository.Table Where p.CountryId = item.CurrentLocationCountry Select p.Name).FirstOrDefault
                '            Where item.UserId = item.ProfileId.ToString)

                '  Dim records = From z In MyEntities.Albums
                'Group Join p In MyEntities.Picutres On z.AlbumID Equals p.AlbumID Into Group
                'From gp In Group.DefaultIfEmpty()
                'Select z, gp


                For Each _field In _Fields

                    If _field.ID = 0 Then
                        _NewFields.Add(New tblReportUser_Template With {.FIELD_ID = _field.FIELD_ID, .RID = _field.RID, .AREA = _field.AREA, .OWNER = HttpContext.Current.User.Identity.Name})
                    End If




                    Dim _NewField As New PivotGridField() ' = New PivotGridField("InsurerEngGroup", DevExpress.XtraPivotGrid.PivotArea.FilterArea)
                    With _NewField

                        _NewField.EmptyCellText = _field.FIELD_ID

                        If _field.ID > 0 Then

                            If _field.AREA < 0 Then
                                .Visible = False
                            Else
                                .Area = _field.AREA
                            End If

                        Else
                            .Visible = False
                        End If
                        .Caption = _field.TEMPLATE_CAPTION
                        .FieldName = _field.TEMPLATE_NAME
                        .SummaryType = _field.SummaryType
                        .CellFormat.FormatType = _field.CellFormat_FormatType
                        .CellFormat.FormatString = _field.CellFormat_FormatString
                        .GrandTotalCellFormat.FormatType = _field.CellFormat_FormatType
                        .GrandTotalCellFormat.FormatString = _field.CellFormat_FormatString
                        .GroupInterval = _field.GroupInterval
                    End With
                    'pivotGrid.Fields.Add(_NewField)
                Next



                If _NewFields.Count > 0 Then
                    dc.tblReportUser_Templates.InsertAllOnSubmit(_NewFields)
                    dc.SubmitChanges()
                End If
            End If



        End Using
        'pivotGrid.DataSourceID = "ObjectDataSource1"
        'pivotGrid.DataBind()

        e.Result = _RID
    End Sub



    Protected Sub SqlDataSource1_Selecting(ByVal sender As Object, ByVal e As SqlDataSourceSelectingEventArgs) Handles SqlDataSource_Data.Selecting
        'If gridCampaign.GridView.FocusedRowIndex > -1 Then

        '    Dim _CampaignID As String = gridCampaign.GridView.GetRowValues(gridCampaign.GridView.FocusedRowIndex, gridCampaign.KeyFieldName).ToString()

        '    e.Command.CommandText = String.Format("select * from V_Campaign_Commission where CampaignID={0} and IsActive=1", _CampaignID)


        'End If
        Dim _RID As New List(Of Integer)

        Using dc As New DataClasses_LWTReportsExt()
            Dim _data = (From c In dc.tblReport_Assignments Where c.UserName.Equals(HttpContext.Current.User.Identity.Name)).ToList()


            Dim _Reports = (From c In dc.tblReports Where c.REPORT_TYPE.Equals("BI")).ToList()


            For Each item In _data
                Dim _Child = (From c In _Reports Where c.RID = item.RID).FirstOrDefault()

                _RID.Add(item.RID)
                If _Child IsNot Nothing Then
                    If _Child.ParentID IsNot Nothing Then
                        Dim _Parent = (From c In _Reports Where c.RID = _Child.ParentID).FirstOrDefault()

                        If _Parent IsNot Nothing Then
                            Recursive(_Parent.RID, _Reports, _RID)
                        End If
                    End If

                End If


            Next

            If _RID.Count > 0 Then
                Dim sb As New StringBuilder()

                If _RID.Count = 1 Then
                    sb.Append(_RID(0).ToString)
                Else
                    For Each _ID In _RID
                        If String.IsNullOrEmpty(sb.ToString()) Then
                            sb.Append(_ID.ToString)
                        Else
                            sb.Append("," & _ID.ToString)
                        End If

                    Next
                End If

                e.Command.CommandText = String.Format("select * from tblReport where RID in({0}) and REPORT_TYPE='BI' order by No", sb.ToString())

            Else
                e.Command.CommandText = "select * from tblReport where RID is null and REPORT_TYPE='BI'"

            End If

           
        End Using
    End Sub


    'Protected Sub SqlDataSource1_Selecting(ByVal sender As Object, ByVal e As LinqServerModeDataSourceSelectEventArgs) Handles ObjectDataSource1.Selecting


    '    Dim _RID As New List(Of Integer)

    '    Using dc As New DataClasses_LWTReportsExt()
    '        Dim _data = (From c In dc.tblReport_Assignments Where c.UserName.Equals(HttpContext.Current.User.Identity.Name)).ToList()


    '        Dim _Reports = (From c In dc.tblReports Where c.REPORT_TYPE.Equals("BI")).ToList()


    '        For Each item In _data
    '            Dim _Child = (From c In _Reports Where c.RID = item.RID).FirstOrDefault()

    '            _RID.Add(item.RID)
    '            If _Child IsNot Nothing Then
    '                If _Child.ParentID IsNot Nothing Then
    '                    Dim _Parent = (From c In _Reports Where c.RID = _Child.ParentID).FirstOrDefault()

    '                    If _Parent IsNot Nothing Then
    '                        Recursive(_Parent.RID, _Reports, _RID)
    '                    End If
    '                End If

    '            End If


    '        Next



    '        Dim _MyReport = From c In dc.tblReports Where _RID.Contains(c.RID)

    '        e.QueryableSource = _MyReport

    '    End Using



    'End Sub


End Class
