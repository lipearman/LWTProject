Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports DevExpress.Web
Imports System.IO
Imports System.Drawing
Imports DevExpress.Web.Data
Imports System.Net.Mail
Imports Spire.Xls

Partial Class Modules_ucDevxClassOfRiskAmend
    Inherits PortalModuleControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        'SqlDataSource_ClassOfRisk_Amend.SelectParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name
        SqlDataSource_CommInRequest.UpdateParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name
        SqlDataSource_CommOutRequest.UpdateParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name


        If (Not IsPostBack) Then
            Session("Risk") = Nothing
            Session("AmendRiskID") = Nothing
        End If
    End Sub

    Protected Sub gridCOR_HtmlRowCreated(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewTableRowEventArgs) Handles grid_ClassOfRisk_Amend.HtmlRowPrepared

        If e.RowType = GridViewRowType.Group Then
            e.Row.Font.Bold = True
        End If
    End Sub


    Protected Sub callbackPanel_AddNewAmendRisk_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase) Handles callbackPanel_AddNewAmendRisk.Callback
        Dim _cmd = e.Parameter.ToString()
        Select Case _cmd
            Case "initdata"
                newCOR.Text = ""
                newCOR.DataBind()
                Session("Risk") = Nothing
                Session("AmendRiskID") = Nothing
                btnAddNewAmendRisk.ClientVisible = False

            Case "select_cor"
                Dim _Risk As String = newCOR.GridView.GetRowValues(newCOR.GridView.FocusedRowIndex, newCOR.KeyFieldName).ToString()
                Session("Risk") = _Risk
                btnAddNewAmendRisk.ClientVisible = True
        End Select


        newgridCommIn.DataBind()
        newgridCommIn.Selection.UnselectAll()
        newgridCommOut.DataBind()
        newgridCommOut.Selection.UnselectAll()

    End Sub


    Protected Sub cbAddNewAmendRisk_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbAddNewAmendRisk.Callback

        Dim _Risk As String = newCOR.GridView.GetRowValues(newCOR.GridView.FocusedRowIndex, newCOR.KeyFieldName).ToString()


        Dim _Unwriter As New List(Of String)
        For Each key In newgridCommIn.GetCurrentPageRowValues("Underwriter")
            If newgridCommIn.Selection.IsRowSelectedByKey(key) Then
                _Unwriter.Add(key.ToString())
            End If
        Next key

        Dim _Agent As New List(Of String)
        For Each key In newgridCommOut.GetCurrentPageRowValues("Agent")
            If newgridCommOut.Selection.IsRowSelectedByKey(key) Then
                _Agent.Add(key.ToString())
            End If
        Next key

        If _Unwriter.Count = 0 And _Agent.Count = 0 Then
            e.Result = "No select Underwriter or Agent"
            Return
        Else

            Using dc As New DataClasses_CPSExt()
                Dim _data_cor = (From c In dc.Register_ClassOfRisks Where c.Risk.Equals(_Risk)).FirstOrDefault()
                Dim _Amend_Class_Of_Risk As New Register_ClassOfRisk_Amend

                With _Amend_Class_Of_Risk
                    .Risk = _data_cor.Risk
                    .Description = _data_cor.Description
                    .RiskGroupI = _data_cor.RiskGroupI
                    .RiskGroupII = _data_cor.RiskGroupII
                    .RiskShortDesc = _data_cor.RiskShortDesc
                    .RiskGovernment = _data_cor.RiskGovernment
                    .Department = _data_cor.Department
                    .RiskGroupID = _data_cor.RiskGroupID
                    .RiskYear = _data_cor.RiskYear
                    .IsActive = _data_cor.IsActive
                    .Remark = _data_cor.Remark
                    .IsGeneralCode = _data_cor.IsGeneralCode
                    .InsuranceLine = _data_cor.InsuranceLine
                    .Prefix = _data_cor.Prefix

                    .CreationDate = Now()
                    .CreationBy = HttpContext.Current.User.Identity.Name
                End With

                For Each item_uw In _Unwriter
                    Dim _Amend_UWRisk As New Register_RiskUwriter_Amend
                    Dim _data_commin = (From c In dc.Register_RiskUwriters Where c.Risk.Equals(_Risk) And c.Uwriter.Equals(item_uw)).FirstOrDefault()
                    With _Amend_UWRisk
                        .Risk = _data_commin.Risk
                        .Uwriter = _data_commin.Uwriter
                        .Description = _data_commin.Description
                        .Brokerage = _data_commin.Brokerage
                        .AgentCommission = _data_commin.AgentCommission
                        .PremiumWarantydays = _data_commin.PremiumWarantydays
                        .NewRenewaldays = _data_commin.NewRenewaldays
                        .NewNewBusdays = _data_commin.NewNewBusdays
                        .RenRenewaldays = _data_commin.RenRenewaldays
                        .RenNewBusdays = _data_commin.RenNewBusdays
                        .AutoCalculation = _data_commin.AutoCalculation
                        .ORCommissionPercent = _data_commin.ORCommissionPercent
                        .OROutRate = _data_commin.OROutRate
                        .AdminFeeIn1 = _data_commin.AdminFeeIn1
                        .AdminFeeIn1Cal = _data_commin.AdminFeeIn1Cal
                        .AdminFeeIn2 = _data_commin.AdminFeeIn2
                        .AdminFeeIn2Cal = _data_commin.AdminFeeIn2Cal
                        .ORCalFrom = _data_commin.ORCalFrom
                        .OffsetORFlag = _data_commin.OffsetORFlag
                        .OffsetAdm1Flag = _data_commin.OffsetAdm1Flag
                        .OffsetAdm2Flag = _data_commin.OffsetAdm2Flag
                        .BrokerageCal = _data_commin.BrokerageCal
                        .ORInCal = _data_commin.ORInCal
                        .IsActive = _data_commin.IsActive
                        .Remark = _data_commin.Remark
                    End With
                    _Amend_Class_Of_Risk.Register_RiskUwriter_Amends.Add(_Amend_UWRisk)
                Next



                For Each item_ag In _Agent
                    Dim _Amend_AgRisk As New Register_AgentRiskComm_Amend
                    Dim _data_commout = (From c In dc.Register_AgentRiskComms Where c.Risk.Equals(_Risk) And c.Agent.Equals(item_ag)).FirstOrDefault()
                    With _Amend_AgRisk
                        .Agent = _data_commout.Agent
                        .Risk = _data_commout.Risk
                        .CommissionOut = _data_commout.CommissionOut
                        .OROut = _data_commout.OROut
                        .OROutCalFrom = _data_commout.OROutCalFrom
                        .AdminOut1 = _data_commout.AdminOut1
                        .AdminOut1Cal = _data_commout.AdminOut1Cal
                        .AdminOut2 = _data_commout.AdminOut2
                        .AdminOut2Cal = _data_commout.AdminOut2Cal
                        .EntryBy = _data_commout.EntryBy
                        .EntryDate = _data_commout.EntryDate
                        .CommOutCal = _data_commout.CommOutCal
                        .OROutCal = _data_commout.OROutCal
                        .IsActive = _data_commout.IsActive
                    End With
                    _Amend_Class_Of_Risk.Register_AgentRiskComm_Amends.Add(_Amend_AgRisk)
                Next


                dc.Register_ClassOfRisk_Amends.InsertOnSubmit(_Amend_Class_Of_Risk)
                dc.SubmitChanges()
            End Using


            e.Result = "success"

        End If

    End Sub


    Protected Sub callbackPanel_EditAmend_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase) Handles callbackPanel_EditAmend.Callback

        Dim _AmendRiskID As Integer = Convert.ToInt32(e.Parameter.ToString())

        Using dc As New DataClasses_CPSExt()

            Dim _data_cor = (From c In dc.Register_ClassOfRisk_Amends Where c.AmendRiskID.Equals(_AmendRiskID)).FirstOrDefault()
            lbRisk.Text = _data_cor.Risk
            With _data_cor
                lbDepartment.Text = .Department
                lbRiskShortDesc.Text = .RiskShortDesc
                lbDescription.Text = .Description
                lbRiskGroupI.Text = .RiskGroupI
                lbRiskGroupII.Text = .RiskGroupII
                lbRiskGovernment.Text = .RiskGovernment

                lbInsuranceLine.Text = ""
                Select Case .InsuranceLine
                    Case 1
                        lbInsuranceLine.Text = "Life"
                    Case 2
                        lbInsuranceLine.Text = "Non Life"
                End Select


                lbCreateBy.Text = .CreationBy
                lbCreateDate.Text = .CreationDate
                lbRequestBy.Text = .RequestBy
                lbRequestdate.Text = IIf(.RequestDate Is Nothing, "", .RequestDate)
                lbApproveBy.Text = .ApproveBy
                lbApproveDate.Text = IIf(.ApproveDate Is Nothing, "", .ApproveDate)
            End With
        End Using

        Session("AmendRiskID") = _AmendRiskID

        gridCommInRequest.DataBind()
        gridCommOutRequest.DataBind()

    End Sub


    Protected Sub grid_HtmlRowCreated(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewTableRowEventArgs) Handles grid_ClassOfRisk_Amend.HtmlRowPrepared

        If e.RowType <> DevExpress.Web.GridViewRowType.Data Then
            Return
        End If

        Dim RowEditable As ASPxButton = grid_ClassOfRisk_Amend.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "RowEditable")
        RowEditable.JSProperties.Add("cpAmendRiskID", e.KeyValue().ToString())
        Dim RowDeleteable As ASPxButton = grid_ClassOfRisk_Amend.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "RowDeleteable")
        RowDeleteable.JSProperties.Add("cpVisibleIndex", e.VisibleIndex)


        Dim _CreationBy = e.GetValue("CreationBy").ToString()
        Dim _ApproveDate = e.GetValue("ApproveDate").ToString()
        Dim _RequestDate = e.GetValue("RequestDate").ToString()
        If String.IsNullOrEmpty(_ApproveDate) And String.IsNullOrEmpty(_RequestDate) Then
            RowEditable.Visible = True
            RowDeleteable.Visible = True
        Else
            RowEditable.Visible = False
            RowDeleteable.Visible = False
        End If

        If _CreationBy.ToString().ToLower() <> HttpContext.Current.User.Identity.Name.ToString().ToLower() Then
            RowEditable.Visible = False
            RowDeleteable.Visible = False
        End If
    End Sub


    Protected Sub gridCommInRequest_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles gridCommInRequest.RowUpdating
        Dim _gridCoverage = DirectCast(sender, ASPxGridView)
        Dim _frmEditUnderwriter As ASPxFormLayout = TryCast(_gridCoverage.FindEditFormTemplateControl("frmEditUnderwriter"), ASPxFormLayout)

        Dim Brokerage As ASPxSpinEdit = TryCast(_frmEditUnderwriter.FindControl("Brokerage"), ASPxSpinEdit)
        Dim BrokerageCal As ASPxRadioButtonList = TryCast(_frmEditUnderwriter.FindControl("BrokerageCal"), ASPxRadioButtonList)
        Dim ORCommissionPercent As ASPxSpinEdit = TryCast(_frmEditUnderwriter.FindControl("OR"), ASPxSpinEdit)
        Dim ORInCal As ASPxRadioButtonList = TryCast(_frmEditUnderwriter.FindControl("ORCal"), ASPxRadioButtonList)
        Dim AdminFeeIn1 As ASPxSpinEdit = TryCast(_frmEditUnderwriter.FindControl("Admin1"), ASPxSpinEdit)
        Dim AdminFeeIn1Cal As ASPxRadioButtonList = TryCast(_frmEditUnderwriter.FindControl("Admin1Cal"), ASPxRadioButtonList)
        Dim AdminFeeIn2 As ASPxSpinEdit = TryCast(_frmEditUnderwriter.FindControl("Admin2"), ASPxSpinEdit)
        Dim AdminFeeIn2Cal As ASPxRadioButtonList = TryCast(_frmEditUnderwriter.FindControl("Admin2Cal"), ASPxRadioButtonList)
        Dim OffsetORFlag As ASPxCheckBox = TryCast(_frmEditUnderwriter.FindControl("OffsetORFlag"), ASPxCheckBox)
        Dim ORCalFrom As ASPxComboBox = TryCast(_frmEditUnderwriter.FindControl("ORCalFrom"), ASPxComboBox)
        Dim Remark As ASPxMemo = TryCast(_frmEditUnderwriter.FindControl("Remark"), ASPxMemo)
        Dim IsActive As ASPxCheckBox = TryCast(_frmEditUnderwriter.FindControl("IsActive"), ASPxCheckBox)

        If BrokerageCal.Value = "x" Then
            If Brokerage.Value > 18 Then
                Throw New Exception("Brokerage over 18%")
            End If
        End If
        If ORInCal.Value = "x" Then
            If ORCommissionPercent.Value > 99 Then
                Throw New Exception("ORComm over 100%")
            End If
        End If
        If AdminFeeIn1Cal.Value = "x" Then
            If AdminFeeIn1.Value > 99 Then
                Throw New Exception("AdminFee1 over 100%")
            End If
        End If
        If AdminFeeIn2Cal.Value = "x" Then
            If AdminFeeIn2.Value > 99 Then
                Throw New Exception("AdminFeeIn2 over 100%")
            End If
        End If


        e.NewValues("Brokerage") = Brokerage.Value
        e.NewValues("BrokerageCal") = BrokerageCal.Value
        e.NewValues("ORCommissionPercent") = ORCommissionPercent.Value
        e.NewValues("ORInCal") = ORInCal.Value
        e.NewValues("AdminFeeIn1") = AdminFeeIn1.Value
        e.NewValues("AdminFeeIn1Cal") = AdminFeeIn1Cal.Value
        e.NewValues("AdminFeeIn2") = AdminFeeIn2.Value
        e.NewValues("AdminFeeIn2Cal") = AdminFeeIn2Cal.Value
        e.NewValues("ORCalFrom") = ORCalFrom.Value
        e.NewValues("OffsetORFlag") = OffsetORFlag.Checked
        e.NewValues("OffsetAdm1Flag") = OffsetORFlag.Checked
        e.NewValues("OffsetAdm2Flag") = OffsetORFlag.Checked
        e.NewValues("Remark") = Remark.Text
        e.NewValues("IsActive") = IsActive.Checked


    End Sub


    Protected Sub gridCommOutRequest_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles gridCommOutRequest.RowUpdating
        Dim _grid = DirectCast(sender, ASPxGridView)
        Dim _frmEditAgentRisk As ASPxFormLayout = TryCast(_grid.FindEditFormTemplateControl("frmEditAgentRisk"), ASPxFormLayout)

        Dim CommissionOut As ASPxSpinEdit = TryCast(_frmEditAgentRisk.FindControl("CommissionOut"), ASPxSpinEdit)
        Dim CommOutCal As ASPxRadioButtonList = TryCast(_frmEditAgentRisk.FindControl("CommOutCal"), ASPxRadioButtonList)
        Dim OROut As ASPxSpinEdit = TryCast(_frmEditAgentRisk.FindControl("OROut"), ASPxSpinEdit)
        Dim OROutCal As ASPxRadioButtonList = TryCast(_frmEditAgentRisk.FindControl("OROutCal"), ASPxRadioButtonList)
        Dim AdminOut1 As ASPxSpinEdit = TryCast(_frmEditAgentRisk.FindControl("AdminOut1"), ASPxSpinEdit)
        Dim AdminOut1Cal As ASPxRadioButtonList = TryCast(_frmEditAgentRisk.FindControl("AdminOut1Cal"), ASPxRadioButtonList)
        Dim AdminOut2 As ASPxSpinEdit = TryCast(_frmEditAgentRisk.FindControl("AdminOut2"), ASPxSpinEdit)
        Dim AdminOut2Cal As ASPxRadioButtonList = TryCast(_frmEditAgentRisk.FindControl("AdminOut2Cal"), ASPxRadioButtonList)
        Dim OROutCalFrom As ASPxComboBox = TryCast(_frmEditAgentRisk.FindControl("OROutCalFrom"), ASPxComboBox)
        Dim IsActive As ASPxCheckBox = TryCast(_frmEditAgentRisk.FindControl("IsActive"), ASPxCheckBox)

        If CommOutCal.Value = "x" Then
            If CommissionOut.Value > 17 Then
                Throw New Exception("Brokerage over 17%")
            End If
        End If
        If OROutCal.Value = "x" Then
            If OROut.Value > 99 Then
                Throw New Exception("ORComm over 100%")
            End If
        End If
        If AdminOut1Cal.Value = "x" Then
            If AdminOut1.Value > 99 Then
                Throw New Exception("AdminFee1 over 100%")
            End If
        End If
        If AdminOut2Cal.Value = "x" Then
            If AdminOut2.Value > 99 Then
                Throw New Exception("AdminFeeIn2 over 100%")
            End If
        End If


        e.NewValues("CommissionOut") = CommissionOut.Value
        e.NewValues("CommOutCal") = CommOutCal.Value
        e.NewValues("OROut") = OROut.Value
        e.NewValues("OROutCal") = OROutCal.Value
        e.NewValues("AdminOut1") = AdminOut1.Value
        e.NewValues("AdminOut1Cal") = AdminOut1Cal.Value
        e.NewValues("AdminOut2") = AdminOut2.Value
        e.NewValues("AdminOut2Cal") = AdminOut2Cal.Value
        e.NewValues("OROutCalFrom") = OROutCalFrom.Value
        e.NewValues("IsActive") = IsActive.Checked


    End Sub


    Protected Sub cbRequestAmend_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbRequestAmend.Callback

        Using dc As New DataClasses_CPSExt()
            Dim _AmendRiskID As String = Session("AmendRiskID").ToString()

            Dim _data = (From c In dc.Register_ClassOfRisk_Amends Where c.AmendRiskID.Equals(_AmendRiskID)).FirstOrDefault()
            _data.RequestDate = Now()
            _data.RequestBy = HttpContext.Current.User.Identity.Name


            SendMailRequest(_AmendRiskID)

            dc.SubmitChanges()


        End Using

        e.Result = "success"

    End Sub
    Protected Sub SendMailRequest(ByVal _AmendRiskID As Integer)
        Dim _filename1 As String = String.Format("CommIn-{0}.xls", Guid.NewGuid().ToString())
        Dim _path1 = String.Format("{0}/{1}", Server.MapPath("~/saved_files"), _filename1)
        Dim _filename2 As String = String.Format("CommOut-{0}.xls", Guid.NewGuid().ToString())
        Dim _path2 = String.Format("{0}/{1}", Server.MapPath("~/saved_files"), _filename2)

        Using dc As New DataClasses_CPSExt()
            Dim _data_cor = (From c In dc.V_ClassOfRisk_Amends Where c.AmendRiskID.Equals(_AmendRiskID)).FirstOrDefault()


            Dim _data_uwrisk = (From c In dc.V_RiskUwriter_Amends Where c.AmendRiskID.Equals(_AmendRiskID) And c.RequestDate Is Nothing And c.ApproveDate Is Nothing).ToList()
            If _data_uwrisk.Count > 0 Then

                '================================== Comm In ========================================
                'initialize a new instance of Workbook
                Dim workbook1 As New Workbook()
                'open a template Excel file
                workbook1.LoadFromFile(Server.MapPath("~\Template\ClassOfRiskCommIn_Amend.xlsx"))
                'get the first sheet 
                Dim worksheet1 As Worksheet = workbook1.Worksheets(0)
                '===================== Header ==============================
                worksheet1.Range("B4").Text = HttpContext.Current.User.Identity.Name
                worksheet1.Range("B5").Text = Now()
                'worksheet.Range("D4").Text = "Div."
                worksheet1.Range("D5").Text = _data_cor.Department
                '===================== Underwriter ==============================

                Dim i As Integer = 0
                For Each item_ur In _data_uwrisk
                    With item_ur
                        If i = 0 Then
                            worksheet1.Range("A11").Text = .Underwriter
                            worksheet1.Range("B11").Text = .AccountContact
                            worksheet1.Range("C11").Text = _data_cor.Risk
                            If _data_cor.RiskShortDesc IsNot Nothing Then worksheet1.Range("D11").Text = _data_cor.RiskShortDesc
                            If _data_cor.Description IsNot Nothing Then worksheet1.Range("E11").Text = _data_cor.Description

                            '====================== Old =====================
                            Dim _data_uwrisk_old = (From c In dc.V_RiskUwriters Where c.Risk.Equals(item_ur.Risk) And c.Underwriter.Equals(item_ur.Underwriter)).FirstOrDefault()
                            worksheet1.Range("F11").Text = _data_uwrisk_old.Brokerage_display
                            worksheet1.Range("G11").Text = _data_uwrisk_old.ORCommissionPercent_display
                            worksheet1.Range("H11").Text = _data_uwrisk_old.AdminFeeIn1_display
                            worksheet1.Range("I11").Text = _data_uwrisk_old.AdminFeeIn2_display
                            worksheet1.Range("J11").Text = IIf(_data_uwrisk_old.OffsetORFlag, "P", "")

                            '====================== New =====================
                            worksheet1.Range("K11").Text = .Brokerage_display
                            worksheet1.Range("L11").Text = .ORCommissionPercent_display
                            worksheet1.Range("M11").Text = .AdminFeeIn1_display
                            worksheet1.Range("N11").Text = .AdminFeeIn2_display
                            worksheet1.Range("O11").Text = IIf(.OffsetORFlag, "P", "")


                            i = i + 1
                        Else
                            Dim startrow As Integer = 11
                            Dim irow As String = (startrow + i).ToString()
                            worksheet1.Copy(worksheet1.Range("A11:J11"), worksheet1.Range(String.Format("A{0}:E{0}", irow, irow)), True)
                            worksheet1.Range(String.Format("A{0}", irow)).Text = .Underwriter
                            worksheet1.Range(String.Format("B{0}", irow)).Text = .AccountContact
                            worksheet1.Range(String.Format("C{0}", irow)).Text = _data_cor.Risk
                            If _data_cor.RiskShortDesc IsNot Nothing Then
                                worksheet1.Range(String.Format("D{0}", irow)).Text = _data_cor.RiskShortDesc
                            Else
                                worksheet1.Range(String.Format("D{0}", irow)).Text = ""
                            End If

                            If _data_cor.Description IsNot Nothing Then
                                worksheet1.Range(String.Format("E{0}", irow)).Text = _data_cor.Description
                            Else
                                worksheet1.Range(String.Format("E{0}", irow)).Text = ""
                            End If

                            '================================= Old =============================

                            Dim _data_uwrisk_old = (From c In dc.V_RiskUwriters Where c.Risk.Equals(item_ur.Risk) And c.Underwriter.Equals(item_ur.Underwriter)).FirstOrDefault()
                            worksheet1.Range(String.Format("F{0}", irow)).Text = _data_uwrisk_old.Brokerage_display
                            worksheet1.Range(String.Format("G{0}", irow)).Text = _data_uwrisk_old.ORCommissionPercent_display
                            worksheet1.Range(String.Format("H{0}", irow)).Text = _data_uwrisk_old.AdminFeeIn1_display
                            worksheet1.Range(String.Format("I{0}", irow)).Text = _data_uwrisk_old.AdminFeeIn2_display
                            worksheet1.Range(String.Format("J{0}", irow)).Text = IIf(_data_uwrisk_old.OffsetORFlag, "P", "")

                            '================================= New =============================
                            worksheet1.Range(String.Format("K{0}", irow)).Text = .Brokerage_display
                            worksheet1.Range(String.Format("L{0}", irow)).Text = .ORCommissionPercent_display
                            worksheet1.Range(String.Format("M{0}", irow)).Text = .AdminFeeIn1_display
                            worksheet1.Range(String.Format("N{0}", irow)).Text = .AdminFeeIn2_display
                            worksheet1.Range(String.Format("O{0}", irow)).Text = IIf(.OffsetORFlag, "P", "")

                            i = i + 1
                            worksheet1.InsertRow(startrow + i)
                        End If
                    End With
                Next
                workbook1.SaveToFile(_path1, ExcelVersion.Version2010)
            End If



            Dim _data_agrisk = (From c In dc.V_AgentRiskComm_Amends Where c.AmendRiskID.Equals(_AmendRiskID) And c.RequestDate Is Nothing And c.ApproveDate Is Nothing).ToList()
            If _data_agrisk.Count > 0 Then
                '================================== Comm Out ========================================
                'initialize a new instance of Workbook
                Dim workbook2 As New Workbook()
                'open a template Excel file
                workbook2.LoadFromFile(Server.MapPath("~\Template\ClassOfRiskCommOut_Amend.xlsx"))
                'get the first sheet 
                Dim worksheet2 As Worksheet = workbook2.Worksheets(0)
                '===================== Header ==============================
                worksheet2.Range("B4").Text = HttpContext.Current.User.Identity.Name
                worksheet2.Range("B5").Text = Now()
                'worksheet.Range("D4").Text = "Div."
                worksheet2.Range("D5").Text = _data_cor.Department
                '===================== Underwriter ==============================
                Dim i As Integer = 0
                For Each item_ag In _data_agrisk
                    With item_ag
                        If i = 0 Then
                            worksheet2.Range("A10").Text = "-"
                            worksheet2.Range("B10").Text = "TBA"
                            worksheet2.Range("C10").Text = .Agent
                            worksheet2.Range("D10").Text = .Name
                            worksheet2.Range("E10").Text = _data_cor.Risk

                            If _data_cor.RiskShortDesc IsNot Nothing Then
                                worksheet2.Range("F10").Text = _data_cor.RiskShortDesc
                            Else
                                worksheet2.Range("F10").Text = ""
                            End If
                            '=============================== Old ======================
                            Dim _data_agrisk_old = (From c In dc.V_AgentRiskComms Where c.Risk.Equals(item_ag.Risk) And c.Agent.Equals(item_ag.Agent)).FirstOrDefault()
                            worksheet2.Range("G10").Text = _data_agrisk_old.CommissionOut_Display
                            worksheet2.Range("H10").Text = _data_agrisk_old.OROut_Display
                            worksheet2.Range("I10").Text = _data_agrisk_old.AdminOut1_Display
                            worksheet2.Range("J10").Text = _data_agrisk_old.AdminOut2_Display
                            '=============================== New ======================
                            worksheet2.Range("K10").Text = .CommissionOut_Display
                            worksheet2.Range("L10").Text = .OROut_Display
                            worksheet2.Range("M10").Text = .AdminOut1_Display
                            worksheet2.Range("N10").Text = .AdminOut2_Display

                            i = i + 1
                        Else
                            Dim startrow As Integer = 10
                            Dim irow As String = (startrow + i).ToString()
                            worksheet2.Copy(worksheet2.Range("A10:J10"), worksheet2.Range(String.Format("A{0}:E{0}", irow, irow)), True)
                            worksheet2.Range(String.Format("A{0}", irow)).Text = "-"
                            worksheet2.Range(String.Format("B{0}", irow)).Text = "TBA"
                            worksheet2.Range(String.Format("C{0}", irow)).Text = .Agent
                            worksheet2.Range(String.Format("D{0}", irow)).Text = .Name
                            worksheet2.Range(String.Format("E{0}", irow)).Text = _data_cor.Risk
                            'worksheet2.Range(String.Format("F{0}", irow)).Text = _data_cor.RiskShortDesc
                            If _data_cor.RiskShortDesc IsNot Nothing Then
                                worksheet2.Range(String.Format("F{0}", irow)).Text = _data_cor.RiskShortDesc
                            Else
                                worksheet2.Range(String.Format("F{0}", irow)).Text = ""
                            End If

                            Dim _data_agrisk_old = (From c In dc.V_AgentRiskComms Where c.Risk.Equals(item_ag.Risk) And c.Agent.Equals(item_ag.Agent)).FirstOrDefault()

                            '===================================== Old =============================
                            worksheet2.Range(String.Format("G{0}", irow)).Text = _data_agrisk_old.CommissionOut_Display
                            worksheet2.Range(String.Format("H{0}", irow)).Text = _data_agrisk_old.OROut_Display
                            worksheet2.Range(String.Format("I{0}", irow)).Text = _data_agrisk_old.AdminOut1_Display
                            worksheet2.Range(String.Format("J{0}", irow)).Text = _data_agrisk_old.AdminOut2_Display

                            '===================================== New =============================
                            worksheet2.Range(String.Format("K{0}", irow)).Text = .CommissionOut_Display
                            worksheet2.Range(String.Format("L{0}", irow)).Text = .OROut_Display
                            worksheet2.Range(String.Format("M{0}", irow)).Text = .AdminOut1_Display
                            worksheet2.Range(String.Format("N{0}", irow)).Text = .AdminOut2_Display

                            i = i + 1
                            worksheet2.InsertRow(startrow + i)
                        End If
                    End With
                Next
                workbook2.SaveToFile(_path2, ExcelVersion.Version2010)
            End If











            '================================== SendMail =======================================

            'Send Mail
            Dim strMailFrom As String = ""
            Dim strMailCC As String = ""
            Dim strMailTo As String = ""
            Dim strSubject As String = ""
            Dim strMessage As New StringBuilder()

            Dim dc_portal = New DataClasses_PortalDataContextExt()
            Dim _mailNotification = (From c In dc_portal.MailNotifications Where c.Code.Equals("N0016")).FirstOrDefault()
            Dim _user = (From c In dc_portal.V_LWT_USERs Where c.sAMAccountName.Equals(HttpContext.Current.User.Identity.Name)).FirstOrDefault()
            strSubject = _mailNotification.MailSubject.Replace("{name}", _user.displayName).Replace("{date}", DateTime.Now.ToString())
            strMessage.AppendLine(Server.HtmlDecode(_mailNotification.MailBody.Replace("{fromname}", _user.displayName)))
            strMailFrom = _user.mail
            strMailCC = _mailNotification.MailCC & ";" & _user.mail
            strMailTo = _mailNotification.MailTo

            'strMailFrom = _user.mail
            ''strMailCC = _mailNotification.MailCC & ";" & _user.mail
            'strMailTo = "dusit@asia.lockton.com" '_mailNotification.MailTo



            Dim MySmtpClient As New System.Net.Mail.SmtpClient(ConfigurationSettings.AppSettings("smtp"))
            Dim msg As New System.Net.Mail.MailMessage()
            msg.BodyEncoding = Encoding.UTF8
            msg.IsBodyHtml = True
            msg.Priority = Net.Mail.MailPriority.High

            '===========================================================
            Dim bodyHTML As String = "<html><body>" & strMessage.ToString() & "<br><span style='font-size:16.0pt;font-family:Angsana New,serif;color:green'><img src='cid:LOGO_IMAGE2' alt='Logo' /><i>Please consider the environment before printing this e-mail.</i></span></body></html>"
            Dim alternateView As AlternateView = alternateView.CreateAlternateViewFromString(bodyHTML, Nothing, "text/html")

            'Dim path_to_the_image_file1 As String = String.Format("{0}\{1}", Server.MapPath("~/images"), "maillockton30.jpg")
            Dim path_to_the_image_file2 As String = String.Format("{0}\{1}", Server.MapPath("~/images"), "mailsmallpicture.jpg")

            ''Create the LinkedResource here
            'Dim logo1 As New LinkedResource(path_to_the_image_file1, "image/jpeg")  'Content Type is set as image/jpeg
            'logo1.ContentId = "LOGO_IMAGE1"
            'logo1.TransferEncoding = Net.Mime.TransferEncoding.Base64
            'alternateView.LinkedResources.Add(logo1)

            Dim logo2 As New LinkedResource(path_to_the_image_file2, "image/jpeg")  'Content Type is set as image/jpeg
            logo2.ContentId = "LOGO_IMAGE2"
            logo2.TransferEncoding = Net.Mime.TransferEncoding.Base64
            alternateView.LinkedResources.Add(logo2)


            msg.AlternateViews.Add(alternateView)
            '===========================================================

            msg.Subject = strSubject 'Subject
            msg.Body = Nothing

            msg.From = New MailAddress(strMailFrom) 'Mail From

            Dim _MailTo = strMailTo.Split(";") 'Mail To
            For Each item In _MailTo
                If Not String.IsNullOrEmpty(item.Trim()) Then msg.To.Add(item)
            Next

            Dim _MailCC = strMailCC.Split(";") 'Mail CC
            For Each item In _MailCC
                If Not String.IsNullOrEmpty(item.Trim()) Then msg.CC.Add(item)
            Next

            If File.Exists(_path1) Then
                Dim att1 = New Attachment(_path1)
                att1.Name = String.Format("RequestAmend_CommIn_By_{0}_{1}.xls", HttpContext.Current.User.Identity.Name, DateTime.Now.ToString("yyyyMMddHHmmss"))
                msg.Attachments.Add(att1)

            End If
            If File.Exists(_path2) Then
                Dim att2 = New Attachment(_path2)
                att2.Name = String.Format("RequestAmend_CommOut_By_{0}_{1}.xls", HttpContext.Current.User.Identity.Name, DateTime.Now.ToString("yyyyMMddHHmmss"))
                msg.Attachments.Add(att2)
            End If

            MySmtpClient.Send(msg)
        End Using






    End Sub


    Protected Sub callbackPanel_MoreInfo_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase) Handles callbackPanel_MoreInfo.Callback

        Dim _AmendRiskID As Integer = Convert.ToInt32(e.Parameter.ToString())

        Using dc As New DataClasses_CPSExt()

            Dim _data_cor = (From c In dc.Register_ClassOfRisk_Amends Where c.AmendRiskID.Equals(_AmendRiskID)).FirstOrDefault()
            ASPxLabel1.Text = _data_cor.Risk
            With _data_cor
                ASPxLabel3.Text = .Department
                ASPxLabel5.Text = .RiskShortDesc
                ASPxLabel2.Text = .Description
                ASPxLabel6.Text = .RiskGroupI
                ASPxLabel7.Text = .RiskGroupII
                ASPxLabel8.Text = .RiskGovernment

                ASPxLabel4.Text = ""
                Select Case .InsuranceLine
                    Case 1
                        ASPxLabel4.Text = "Life"
                    Case 2
                        ASPxLabel4.Text = "Non Life"
                End Select


                ASPxLabel9.Text = .CreationBy
                ASPxLabel10.Text = .CreationDate
                ASPxLabel11.Text = .RequestBy
                ASPxLabel12.Text = IIf(.RequestDate Is Nothing, "", .RequestDate)
                ASPxLabel13.Text = .ApproveBy
                ASPxLabel14.Text = IIf(.ApproveDate Is Nothing, "", .ApproveDate)
            End With
        End Using

        Session("AmendRiskID") = _AmendRiskID

        ASPxGridView1.DataBind()
        ASPxGridView2.DataBind()

    End Sub


    Protected Sub detailGrid_CommIn_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
        Dim _ID As String = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
        Using dc As New DataClasses_CPSExt()
            Dim _data = (From c In dc.V_RiskUwriter_Amends Where c.ID.Equals(_ID)).FirstOrDefault()
            Session("Risk") = _data.Risk.Trim()
            Session("Underwriter") = _data.Underwriter.Trim()
        End Using
    End Sub
    Protected Sub detailGrid_CommOut_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
        Dim _ID As String = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
        Using dc As New DataClasses_CPSExt()
            Dim _data = (From c In dc.V_AgentRiskComm_Amends Where c.ID.Equals(_ID)).FirstOrDefault()
            Session("Risk") = _data.Risk.Trim()
            Session("Agent") = _data.Agent.Trim()
        End Using
    End Sub
End Class
