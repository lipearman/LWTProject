Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports BaseConfig
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports DevExpress.Web.ASPxTreeList
Imports System.IO

Partial Class Modules_ucDevxMotorCampaignClassOfRisk
    Inherits PortalModuleControl
    Private RiskGroupID As Integer = 3

    'Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreRender
    '    gridCampaign.DataBind()
    '    gridCampaign.Value = gridCampaign.GridView.GetRowValues(0, gridCampaign.KeyFieldName).ToString()

    '    treeList_Commission.DataBind()
    '    treeList_Commission.ExpandAll()
    '    cmdAddNewCommission.ClientVisible = True
    '    cmdAddNewCommissionAgent.ClientVisible = True
    '    searchTxt.ClientVisible = True
    '    searchBtn.ClientVisible = True
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        SqlDataSource_MotorCampaign.SelectParameters("RiskGroupID").DefaultValue = RiskGroupID
        If (Not IsPostBack) Then
            Session("CampaignID") = Nothing

          


        End If
    End Sub


    Protected Sub btnExportToXlsx_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExportToXlsx.Click
        treeListExporter.WriteXlsxToResponse()
    End Sub
 
    Protected Function GetCellText(ByVal container As TreeListDataCellTemplateContainer) As String
        Dim searchText As String = searchTxt.Text
        Dim cell_text As String = container.Text
        If searchText.Length > cell_text.Length Then
            Return cell_text
        End If
        If searchText <> "" Then
            Dim cell_text_lower As String = cell_text.ToLower()
            Dim serchText_lower As String = searchText.ToLower()
            Dim start_pos As Integer = cell_text_lower.IndexOf(serchText_lower)
            Dim span_length As Integer = ("<span class='highlight'>").Length
            If start_pos >= 0 Then
                cell_text = cell_text.Insert(start_pos, "<span class='highlight'>")
                cell_text = cell_text.Insert(start_pos + span_length + serchText_lower.Length, "</span>")
            End If
        End If
        Return cell_text
    End Function
    'Protected Sub searchBtn_Click(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim iterator As New TreeListNodeIterator(treeList_Commission.RootNode)
    '    Do While iterator.Current IsNot Nothing
    '        CheckNode(iterator.Current)
    '        iterator.GetNext()
    '    Loop
    'End Sub
    Private Sub CheckNode(ByVal node As TreeListNode)
        Dim s_text As String = searchTxt.Text.ToLower()
        Dim node_value As Object = node.GetValue("Name")
        If node_value Is Nothing Then
            Return
        End If
        If node_value.ToString().ToLower().IndexOf(s_text) >= 0 Then
            node.MakeVisible()
        End If
    End Sub



    Protected Sub SqlDataSource1_Selecting(ByVal sender As Object, ByVal e As SqlDataSourceSelectingEventArgs) Handles SqlDataSource_Commission.Selecting
        If gridCampaign.GridView.FocusedRowIndex > -1 Then

            Dim _CampaignID As String = gridCampaign.GridView.GetRowValues(gridCampaign.GridView.FocusedRowIndex, gridCampaign.KeyFieldName).ToString()

            e.Command.CommandText = String.Format("select * from V_Campaign_Commission where CampaignID={0} and IsActive=1", _CampaignID)


        End If

    End Sub

    Protected Sub treeList_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As TreeListHtmlDataCellEventArgs) Handles treeList_Commission.HtmlDataCellPrepared
        If e.GetValue("ID") < 0 Then
            e.Cell.Font.Bold = True
        End If
    End Sub
    'Protected Sub treeList_CustomCallback(ByVal sender As Object, ByVal e As TreeListCustomCallbackEventArgs) Handles treeList_Commission.CustomCallback
    'If e.Argument.Equals("REFRESH") Then
    '    Dim treeList = DirectCast(sender, ASPxTreeList)
    '    treeList.DataBind()
    '    Return
    'End If
    'End Sub
    'Protected Sub treeList_HtmlRowPrepared(ByVal sender As Object, ByVal e As TreeListHtmlRowEventArgs) Handles treeList_Commission.HtmlRowPrepared
    '    Dim RowEditable As ASPxButton = DirectCast(e.Row, DevExpress.Web.ASPxTreeList.Internal.TreeListDataRow).Cells(12).FindControl("RowEditable")
    '    RowEditable.JSProperties.Add("cpID", e.NodeKey(0))

    'End Sub
    'Protected Sub cbCampaign_Callback(ByVal source As Object, ByVal e As CallbackEventArgs) Handles cbCampaign.Callback
    '    Session("CampaignID") = e.Parameter
    'End Sub
    Protected Sub callbackPanel_addnewCommission_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase) Handles callbackPanel_addnewCommission.Callback

        Dim _cmd = e.Parameter.ToString()
        Select Case _cmd
            Case "initdata"
                newCOR.DataBind()
                newEffectiveDate.Value = Nothing
                newExpiryDate.Value = Nothing

                newCOR.Value = Nothing
           
        End Select


        Dim _Risk = newCOR.Value
        Session("Risk") = _Risk

        newgridCommIn.DataBind()
        newgridCommIn.Selection.SelectAll()

        newgridCommOut.DataBind()
        newgridCommOut.Selection.SelectAll()



    End Sub

    Protected Sub callbackPanel_addnewCommissionAgent_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase) Handles callbackPanel_addnewCommissionAgent.Callback


        Dim _cmd = e.Parameter.ToString()

        If Not String.IsNullOrEmpty(_cmd) Then

            Dim _params = _cmd.Split("|")
            Select Case _params(0)

                Case "initdata"
                    gridCommIn.DataBind()
                    gridCommIn.Value = Nothing

                    newAgentEffectiveDate.Value = Nothing
                    newAgentExpiryDate.Value = Nothing

                    newgridCommOut2.DataBind()
                Case "select_commin"

                    newgridCommOut2.DataBind()

            End Select


        End If



    End Sub


    Protected Sub SqlDataSource2_Selecting(ByVal sender As Object, ByVal e As SqlDataSourceSelectingEventArgs) Handles SqlDataSource_MotorCampaignCommIn.Selecting

        If gridCampaign.GridView.FocusedRowIndex > -1 Then

            Dim _CampaignID As String = gridCampaign.GridView.GetRowValues(gridCampaign.GridView.FocusedRowIndex, gridCampaign.KeyFieldName).ToString()

            e.Command.CommandText = String.Format("select * from V_Campaign_CommIn where CampaignID={0} and IsActive=1 ", _CampaignID)


        End If

    End Sub

    Protected Sub SqlDataSource3_Selecting(ByVal sender As Object, ByVal e As SqlDataSourceSelectingEventArgs) Handles SqlDataSource3.Selecting

        If gridCommIn.GridView.FocusedRowIndex > -1 Then

            Dim _CommInID As String = gridCommIn.GridView.GetRowValues(gridCommIn.GridView.FocusedRowIndex, gridCommIn.KeyFieldName).ToString()

            Using dc As New DataClasses_CPSExt()
                Dim _data = (From c In dc.V_Campaign_CommIns Where c.CommInID.Equals(_CommInID)).FirstOrDefault()

                e.Command.CommandText = String.Format("SELECT * from V_AgentRiskComm where Risk='{0}' and IsActive=1 and status='approved' and AgentGroup <> 'LAPSE' ", _data.Risk)

            End Using


        End If

    End Sub


    Protected Sub cbAddNewCommission_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbAddNewCommission.Callback

        Using dc As New DataClasses_CPSExt()
            Dim _newRisk As String = newCOR.Value.ToString()
            Dim _newEffectiveDate As DateTime = newEffectiveDate.Value
            Dim _newExpiryDate As DateTime = newExpiryDate.Value
            Dim _CampaignID As Integer = Convert.ToInt32(Session("CampaignID"))

            Dim _Unwriter As New List(Of String)
            For Each key In newgridCommIn.GetCurrentPageRowValues("Underwriter")
                If newgridCommIn.Selection.IsRowSelectedByKey(key) Then
                    _Unwriter.Add(key.ToString())
                End If
            Next key

            If _Unwriter.Count = 0 Then
                e.Result = "Please select Underwriter"
                Return
            End If

            Dim sb_error As New StringBuilder()
            For Each item_uw In _Unwriter

                Dim _data_commin = (From c In dc.tblCampaign_CommIns Where c.Risk.Equals(_newRisk) _
                   And c.CampaignID.Equals(_CampaignID) _
                   And c.Uwriter.Equals(item_uw) _
                   And ((_newEffectiveDate >= c.EffectiveDate And _newEffectiveDate <= c.ExpiryDate) _
                   Or (_newExpiryDate >= c.EffectiveDate And _newExpiryDate <= c.ExpiryDate))).ToList()

                For Each item In _data_commin
                    sb_error.AppendFormat("{0} ({1} - {2}), ", item_uw, item.EffectiveDate, item.ExpiryDate)
                Next
            Next

            If Not String.IsNullOrEmpty(sb_error.ToString()) Then
                e.Result = "Overlab date " & sb_error.ToString()
                Return
            End If

            Dim _Agent As New List(Of String)
            For Each key In newgridCommOut.GetCurrentPageRowValues("Agent")
                If newgridCommOut.Selection.IsRowSelectedByKey(key) Then
                    _Agent.Add(key.ToString())
                End If
            Next key

            Dim _tblCampaign_CommInList As New List(Of tblCampaign_CommIn)

            For Each item_uw In _Unwriter
                Dim _data_RiskUwriters = (From c In dc.V_RiskUwriters Where c.Risk.Equals(_newRisk) And c.Underwriter.Equals(item_uw)).FirstOrDefault()
                Dim _tblCampaign_CommIn As New tblCampaign_CommIn
                With _tblCampaign_CommIn
                    .CampaignID = _CampaignID
                    .Risk = _data_RiskUwriters.Risk
                    .Uwriter = _data_RiskUwriters.Underwriter
                    .Commission = _data_RiskUwriters.Brokerage
                    .CommissionCal = _data_RiskUwriters.BrokerageCal
                    .ORCommission = _data_RiskUwriters.ORCommissionPercent
                    .ORCommissionCal = _data_RiskUwriters.ORInCal
                    .Admin1 = _data_RiskUwriters.AdminFeeIn1
                    .Admin1Cal = _data_RiskUwriters.AdminFeeIn1Cal
                    .Admin2 = _data_RiskUwriters.AdminFeeIn2
                    .Admin2Cal = _data_RiskUwriters.AdminFeeIn2Cal
                    .EffectiveDate = _newEffectiveDate
                    .ExpiryDate = _newExpiryDate
                    .CreationDate = Now()
                    .CreationBy = HttpContext.Current.User.Identity.Name
                    .IsActive = True


                    If _Agent.Count > 0 Then
                        For Each item_ag In _Agent


                            Dim _data_commout = (From c In dc.Register_AgentRiskComms Where c.Risk.Equals(_newRisk) And c.Agent.Equals(item_ag)).FirstOrDefault()

                            Dim _tblCampaign_CommOut As New tblCampaign_CommOut()
                            _tblCampaign_CommOut.CampaignID = .CampaignID
                            _tblCampaign_CommOut.Risk = .Risk
                            _tblCampaign_CommOut.Uwriter = .Uwriter
                            _tblCampaign_CommOut.Agent = item_ag
                            _tblCampaign_CommOut.Commission = _data_commout.CommissionOut
                            _tblCampaign_CommOut.CommissionCal = _data_commout.CommOutCal
                            _tblCampaign_CommOut.ORCommission = _data_commout.OROut
                            _tblCampaign_CommOut.ORCommissionCal = _data_commout.OROutCal
                            _tblCampaign_CommOut.Admin1 = _data_commout.AdminOut1
                            _tblCampaign_CommOut.Admin1Cal = _data_commout.AdminOut1Cal
                            _tblCampaign_CommOut.Admin2 = _data_commout.AdminOut2
                            _tblCampaign_CommOut.Admin2Cal = _data_commout.AdminOut2Cal
                            _tblCampaign_CommOut.CreationDate = .CreationDate
                            _tblCampaign_CommOut.CreationBy = .CreationBy
                            _tblCampaign_CommOut.IsActive = True
                            _tblCampaign_CommOut.EffectiveDate = _newEffectiveDate
                            _tblCampaign_CommOut.ExpiryDate = _newExpiryDate


                            .tblCampaign_CommOuts.Add(_tblCampaign_CommOut)


                        Next

                    End If

                End With
                _tblCampaign_CommInList.Add(_tblCampaign_CommIn)
            Next
            dc.tblCampaign_CommIns.InsertAllOnSubmit(_tblCampaign_CommInList)
            dc.SubmitChanges()

            e.Result = "success"


        End Using



    End Sub



    Protected Sub cbAddNewCommissionAgent_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbAddNewCommissionAgent.Callback

        Using dc As New DataClasses_CPSExt()
            Dim _CommInID As String = gridCommIn.GridView.GetRowValues(gridCommIn.GridView.FocusedRowIndex, gridCommIn.KeyFieldName).ToString()
            Dim _EffectiveDate As DateTime = newAgentEffectiveDate.Value
            Dim _ExpiryDate As DateTime = newAgentEffectiveDate.Value

            Dim sb_error As New StringBuilder()

            Dim _data_commIn = (From c In dc.tblCampaign_CommIns Where c.CommInID.Equals(_CommInID)).FirstOrDefault()

            If _EffectiveDate < _data_commIn.EffectiveDate Or _EffectiveDate > _data_commIn.ExpiryDate Or _ExpiryDate < _data_commIn.EffectiveDate Or _ExpiryDate > _data_commIn.ExpiryDate Then
                sb_error.AppendFormat("{0} ({1} - {2}), ", _data_commIn.Uwriter, _data_commIn.EffectiveDate.Value.ToString("dd/MM/yyyy"), _data_commIn.ExpiryDate.Value.ToString("dd/MM/yyyy"))
            End If

            If Not String.IsNullOrEmpty(sb_error.ToString()) Then
                e.Result = "Invalid Date Range " & sb_error.ToString()
                Return
            End If






         


            Dim _Agent As New List(Of String)
            For Each key In newgridCommOut2.GetCurrentPageRowValues("Agent")
                If newgridCommOut2.Selection.IsRowSelectedByKey(key) Then
                    _Agent.Add(key.ToString())
                End If
            Next key
            If _Agent.Count = 0 Then
                e.Result = "Please select Agent"
                Return
            End If





            If _Agent.Count > 0 Then
                For Each item_ag In _Agent
                    Dim _data_commOut = (From c In dc.tblCampaign_CommOuts Where c.CommInID.Equals(_CommInID) And c.Agent.Equals(item_ag)).ToList()
                    For Each item In _data_commOut
                        If (_EffectiveDate >= item.EffectiveDate And _EffectiveDate <= item.ExpiryDate) Or (_ExpiryDate >= item.EffectiveDate And _ExpiryDate <= item.ExpiryDate) Then
                            sb_error.AppendFormat("{0} ({1} - {2}), ", item.Agent, item.EffectiveDate.Value.ToString("dd/MM/yyyy"), item.ExpiryDate.Value.ToString("dd/MM/yyyy"))
                        End If
                    Next

                    If Not String.IsNullOrEmpty(sb_error.ToString()) Then
                        e.Result = "Invalid Date Range on " & sb_error.ToString()
                        Return
                    End If
                Next
              


                For Each item_ag In _Agent
                    Dim _data_agentrisk = (From c In dc.Register_AgentRiskComms Where c.Risk.Equals(_data_commIn.Risk) And c.Agent.Equals(item_ag)).FirstOrDefault()
                    Dim _tblCampaign_CommOut As New tblCampaign_CommOut()
                    With _data_agentrisk
                        _tblCampaign_CommOut.CampaignID = _data_commIn.CampaignID
                        _tblCampaign_CommOut.Risk = _data_commIn.Risk
                        _tblCampaign_CommOut.Uwriter = _data_commIn.Uwriter
                        _tblCampaign_CommOut.Agent = item_ag
                        _tblCampaign_CommOut.Commission = _data_agentrisk.CommissionOut
                        _tblCampaign_CommOut.CommissionCal = _data_agentrisk.CommOutCal
                        _tblCampaign_CommOut.ORCommission = _data_agentrisk.OROut
                        _tblCampaign_CommOut.ORCommissionCal = _data_agentrisk.OROutCal
                        _tblCampaign_CommOut.Admin1 = _data_agentrisk.AdminOut1
                        _tblCampaign_CommOut.Admin1Cal = _data_agentrisk.AdminOut1Cal
                        _tblCampaign_CommOut.Admin2 = _data_agentrisk.AdminOut2
                        _tblCampaign_CommOut.Admin2Cal = _data_agentrisk.AdminOut2Cal
                        _tblCampaign_CommOut.CreationDate = .CreationDate
                        _tblCampaign_CommOut.CreationBy = .CreationBy
                        _tblCampaign_CommOut.IsActive = True
                        _tblCampaign_CommOut.EffectiveDate = _EffectiveDate
                        _tblCampaign_CommOut.ExpiryDate = _ExpiryDate
                    End With

                    _data_commIn.tblCampaign_CommOuts.Add(_tblCampaign_CommOut)
                Next

            End If
             
            dc.SubmitChanges()

            e.Result = "success"


        End Using



    End Sub



    Protected Sub callbackPanel_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase) Handles callbackPanel.Callback

        Dim _cmd = e.Parameter.ToString()

        If Not String.IsNullOrEmpty(_cmd) Then

            Dim _params = _cmd.Split("|")
            Select Case _params(0)

                Case "select_campaign"
                    Dim _CampaignID As String = gridCampaign.GridView.GetRowValues(gridCampaign.GridView.FocusedRowIndex, gridCampaign.KeyFieldName).ToString()
                    Session("CampaignID") = _CampaignID
 
 





                Case "search_commission"
                    Dim iterator As New TreeListNodeIterator(treeList_Commission.RootNode)
                    Do While iterator.Current IsNot Nothing
                        CheckNode(iterator.Current)
                        iterator.GetNext()
                    Loop

                Case "delete_commission"
                    Dim _ID As Integer = Convert.ToInt32(_params(1).ToString())
                    If _ID > 0 Then
                        Using dc As New DataClasses_CPSExt()
                            Dim _data = (From c In dc.tblCampaign_CommOuts Where c.CommOutID.Equals(_ID)).FirstOrDefault()
                            dc.tblCampaign_CommOuts.DeleteOnSubmit(_data)
                            dc.SubmitChanges()
                        End Using
                    Else
                        Using dc As New DataClasses_CPSExt()
                            Dim _data = (From c In dc.tblCampaign_CommIns Where c.CommInID.Equals(_ID * -1)).FirstOrDefault()

                            dc.tblCampaign_CommOuts.DeleteAllOnSubmit(_data.tblCampaign_CommOuts)

                            dc.tblCampaign_CommIns.DeleteOnSubmit(_data)

                            dc.SubmitChanges()
                        End Using
                    End If

                Case "REFRESH"

            End Select


            treeList_Commission.DataBind()
            treeList_Commission.ExpandAll()

            cmdAddNewCommission.ClientVisible = True
            cmdAddNewCommissionAgent.ClientVisible = True
            btnExportToXlsx.ClientVisible = True


            searchTxt.ClientVisible = True
            searchBtn.ClientVisible = True
        End If

       
    End Sub



    Protected Sub callbackPanel_popupEditCommission_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase) Handles callbackPanel_popupEditCommission.Callback
        'Dim _result As String = ""

        Dim _ID As Integer = Convert.ToInt32(e.Parameter.ToString())

        lbID.Clear()
        lbID.Add("ID", _ID)

        If _ID > 0 Then
            Using dc As New DataClasses_CPSExt()
                Dim _data = (From c In dc.V_Campaign_CommOuts Where c.CommOutID.Equals(_ID)).FirstOrDefault()
                With _data
                    lbName.Text = .Name
                    lbRisk.Text = .Risk
                    Commission.Value = .Commission
                    CommCal.Value = .CommissionCal
                    ORComm.Value = .ORCommission
                    ORCommCal.Value = .ORCommissionCal
                    Admin1.Value = .Admin1
                    Admin1Cal.Value = .Admin1Cal
                    Admin2.Value = .Admin2
                    Admin2Cal.Value = .Admin2Cal
                    IsActive.Value = .IsActive

                    EffectiveDate.Value = .EffectiveDate
                    ExpiryDate.Value = .ExpiryDate

                    'EffectiveDate.Enabled = False
                    'ExpiryDate.Enabled = False
                End With

            End Using

        Else
            Using dc As New DataClasses_CPSExt()
                Dim _data = (From c In dc.V_Campaign_CommIns Where c.CommInID.Equals(_ID * -1)).FirstOrDefault()

                With _data
                    lbName.Text = .AccountContact
                    lbRisk.Text = .Risk
                    Commission.Value = .Commission
                    CommCal.Value = .CommissionCal
                    ORComm.Value = .ORCommission
                    ORCommCal.Value = .ORCommissionCal
                    Admin1.Value = .Admin1
                    Admin1Cal.Value = .Admin1Cal
                    Admin2.Value = .Admin2
                    Admin2Cal.Value = .Admin2Cal
                    IsActive.Value = .IsActive

                    EffectiveDate.Value = .EffectiveDate
                    ExpiryDate.Value = .ExpiryDate


                    'EffectiveDate.Enabled = True
                    'ExpiryDate.Enabled = True
                End With

            End Using

        End If


        'callbackPanel_popupEditCommission.JSProperties("cpResult") = _result
    End Sub


    Protected Sub cbSaveCommission_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgs) Handles cbSaveCommission.Callback


        Using dc As New DataClasses_CPSExt()

            Dim sb_error As New StringBuilder()

            Dim _ID As Integer = Convert.ToInt32(lbID("ID"))

            Dim _EffectiveDate As DateTime = EffectiveDate.Value
            Dim _ExpiryDate As DateTime = ExpiryDate.Value

            If _ID > 0 Then

                Dim _data = (From c In dc.tblCampaign_CommOuts Where c.CommOutID.Equals(_ID)).FirstOrDefault()


                Dim _data_log As New tblCampaign_CommOutLog
                With _data_log
                    .CommOutID = _data.CommOutID
                    .CampaignID = _data.CampaignID
                    .CommInID = _data.CommInID
                    .Risk = _data.Risk
                    .Uwriter = _data.Uwriter
                    .Agent = _data.Agent
                    .Commission = _data.Commission
                    .CommissionCal = _data.CommissionCal
                    .ORCommission = _data.ORCommission
                    .ORCommissionCal = _data.ORCommissionCal
                    .Admin1 = _data.Admin1
                    .Admin1Cal = _data.Admin1Cal
                    .Admin2 = _data.Admin2
                    .Admin2Cal = _data.Admin2Cal
                    .CreationDate = _data.CreationDate
                    .CreationBy = _data.CreationBy
                    .IsActive = _data.IsActive
                    .EffectiveDate = _data.EffectiveDate
                    .ExpiryDate = _data.ExpiryDate
                End With
                dc.tblCampaign_CommOutLogs.InsertOnSubmit(_data_log)



                Dim _CommInID As Integer = _data.CommInID

                If CommCal.Value.ToString().ToLower() = "x" Then
                    If Convert.ToDouble(Commission.Value) > 17 Then
                        e.Result = "Brokerage Over 17%"
                        Return
                    End If
                End If

                If ORCommCal.Value.ToString().ToLower() = "x" Then
                    If Convert.ToDouble(ORComm.Value) > 99 Then
                        e.Result = ("ORComm over 100%")
                        Return
                    End If
                End If

                If Admin1Cal.SelectedItem.Value.ToString().ToLower() = "x" Then
                    If Convert.ToDouble(Admin1.Value) > 99 Then
                        e.Result = ("Admin1 over 100%")
                        Return
                    End If
                End If

                If Admin2Cal.SelectedItem.Value.ToString().ToLower() = "x" Then
                    If Convert.ToDouble(Admin2.Value) > 99 Then
                        e.Result = ("Admin2 over 100%")
                        Return
                    End If
                End If



                Dim _data_commIn = (From c In dc.tblCampaign_CommIns Where c.CommInID.Equals(_CommInID)).ToList()
                For Each item In _data_commIn
                    If _EffectiveDate < item.EffectiveDate Or _EffectiveDate > item.ExpiryDate Or _ExpiryDate < item.EffectiveDate Or _ExpiryDate > item.ExpiryDate Then
                        sb_error.AppendFormat("{0} ({1} - {2}), ", item.Uwriter, item.EffectiveDate.Value.ToString("dd/MM/yyyy"), item.ExpiryDate.Value.ToString("dd/MM/yyyy"))
                    End If

                Next
                If Not String.IsNullOrEmpty(sb_error.ToString()) Then
                    e.Result = "Invalid Date Range " & sb_error.ToString()
                    Return
                End If


                Dim _data_commout = (From c In dc.tblCampaign_CommOuts Where c.CommInID.Equals(_CommInID) _
                     And Not c.CommOutID.Equals(_ID) And c.Agent.Equals(_data.Agent) _
                     And ((_EffectiveDate >= c.EffectiveDate And _EffectiveDate <= c.ExpiryDate) _
                     Or (_ExpiryDate >= c.EffectiveDate And _ExpiryDate <= c.ExpiryDate))).ToList()

                For Each item In _data_commout
                    sb_error.AppendFormat("{0} ({1} - {2}), ", _data.Agent, item.EffectiveDate, item.ExpiryDate)
                Next
                If Not String.IsNullOrEmpty(sb_error.ToString()) Then
                    e.Result = "Overlab date " & sb_error.ToString()
                    Return
                End If



                With _data

                    .Commission = Convert.ToDouble(Commission.Value)
                    .CommissionCal = CommCal.Value
                    .ORCommission = Convert.ToDouble(ORComm.Value)
                    .ORCommissionCal = ORCommCal.Value
                    .Admin1 = Convert.ToDouble(Admin1.Value)
                    .Admin1Cal = Admin1Cal.Value
                    .Admin2 = Convert.ToDouble(Admin2.Value)
                    .Admin2Cal = Admin2Cal.Value
                    .IsActive = IsActive.Value
                    .EffectiveDate = EffectiveDate.Value
                    .ExpiryDate = ExpiryDate.Value

                End With



            Else
                Dim _data = (From c In dc.tblCampaign_CommIns Where c.CommInID.Equals(_ID * -1)).FirstOrDefault()
                Dim _data_log As New tblCampaign_CommInLog
                With _data_log
                    .CommInID = _data.CommInID
                    .CampaignID = _data.CampaignID
                    .Risk = _data.Risk
                    .Uwriter = _data.Uwriter
                    .Commission = _data.Commission
                    .CommissionCal = _data.CommissionCal
                    .ORCommission = _data.ORCommission
                    .ORCommissionCal = _data.ORCommissionCal
                    .Admin1 = _data.Admin1
                    .Admin1Cal = _data.Admin1Cal
                    .Admin2 = _data.Admin2
                    .Admin2Cal = _data.Admin2Cal
                    .EffectiveDate = _data.EffectiveDate
                    .ExpiryDate = _data.ExpiryDate
                    .CreationDate = _data.CreationDate
                    .CreationBy = _data.CreationBy
                    .IsActive = _data.IsActive
                    .DupFromID = _data.DupFromID
                End With
                dc.tblCampaign_CommInLogs.InsertOnSubmit(_data_log)

                Dim _data_commin = (From c In dc.tblCampaign_CommIns Where c.Risk.Equals(_data.Risk.Trim()) _
                       And c.CampaignID.Equals(_data.CampaignID) _
                       And c.Uwriter.Equals(_data.Uwriter.Trim()) _
                       And Not c.CommInID.Equals(_data.CommInID) _
                       And ((_EffectiveDate >= c.EffectiveDate And _EffectiveDate <= c.ExpiryDate) _
                       Or (_ExpiryDate >= c.EffectiveDate And _ExpiryDate <= c.ExpiryDate))).ToList()

                For Each item In _data_commin
                    sb_error.AppendFormat("{0} ({1} - {2}), ", _data.Uwriter, item.EffectiveDate, item.ExpiryDate)
                Next


                If Not String.IsNullOrEmpty(sb_error.ToString()) Then
                    e.Result = "Overlab date " & sb_error.ToString()
                    Return
                End If



                If CommCal.Value.ToString().ToLower() = "x" Then
                    If Convert.ToDouble(Commission.Value) > 18 Then
                        e.Result = "Brokerage Over 18%"
                        Return
                    End If
                End If

                If ORCommCal.Value.ToString().ToLower() = "x" Then
                    If Convert.ToDouble(ORComm.Value) > 99 Then
                        e.Result = ("ORComm over 100%")
                        Return
                    End If
                End If

                If Admin1Cal.SelectedItem.Value.ToString().ToLower() = "x" Then
                    If Convert.ToDouble(Admin1.Value) > 99 Then
                        e.Result = ("Admin1 over 100%")
                        Return
                    End If
                End If

                If Admin2Cal.SelectedItem.Value.ToString().ToLower() = "x" Then
                    If Convert.ToDouble(Admin2.Value) > 99 Then
                        e.Result = ("Admin2 over 100%")
                        Return
                    End If
                End If


                With _data

                    .Commission = Convert.ToDouble(Commission.Value)
                    .CommissionCal = CommCal.Value
                    .ORCommission = Convert.ToDouble(ORComm.Value)
                    .ORCommissionCal = ORCommCal.Value
                    .Admin1 = Convert.ToDouble(Admin1.Value)
                    .Admin1Cal = Admin1Cal.Value
                    .Admin2 = Convert.ToDouble(Admin2.Value)
                    .Admin2Cal = Admin2Cal.Value
                    .IsActive = IsActive.Value
                    .EffectiveDate = EffectiveDate.Value
                    .ExpiryDate = ExpiryDate.Value

                End With



            End If

            dc.SubmitChanges()

        End Using

        e.Result = "success"
    End Sub



    Protected Sub callbackPanel_MoreInfoLog_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase) Handles callbackPanel_MoreInfoLog.Callback
        Dim _ID As Integer = Convert.ToInt32(e.Parameter.ToString())

        SqlDataSource_CommissionLog.SelectParameters("ID").DefaultValue = _ID
        grid_CommissionLog.DataBind()


    End Sub

End Class
