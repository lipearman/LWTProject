Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports DevExpress.Web
Imports System.IO
Imports System.Drawing
Imports System.Net.Mail
Imports Spire.Xls

Partial Class Modules_ucDevxClassOfRiskSetup
    Inherits PortalModuleControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        'SqlDataSource_COR.SelectParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name

        SqlDataSource_COR.UpdateParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name


        If (Not IsPostBack) Then


        End If
    End Sub

    'Protected Sub btnClassOfRiskFormat_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClassOfRiskFormat.Click
    '    Dim fileName = String.Format("{0}.xls", System.Guid.NewGuid.ToString())
    '    Dim filePath = String.Format("{0}\{1}", Server.MapPath("~/Template"), "ClassOfRisk.xlsx")



    '    Response.ClearContent()
    '    Response.AddHeader("content-disposition", "attachment; filename=" & fileName)
    '    Response.ContentType = "application/octet-stream"
    '    Response.WriteFile(filePath)
    '    Response.End()

    'End Sub


    Protected Sub cbAddNewRisk_Callback(ByVal source As Object, ByVal e As CallbackEventArgs) Handles cbAddNewRisk.Callback
        Using dc_CPS As New DataClasses_CPSExt()
            Dim NewRisk As String = ""

            If newIsGeneralCode.Checked = True Then

                Dim _PREFIX = newPrefix.Text.Trim().ToUpper()

                Dim _maxClass = (From c In dc_CPS.Register_ClassOfRisks Where c.Risk.StartsWith(_PREFIX & "0") Order By c.Risk Descending).FirstOrDefault()

                If _maxClass Is Nothing Then
                    NewRisk = _PREFIX & Right("00000001", 8 - Len(_PREFIX))
                Else
                    Dim _maxno = Right(_maxClass.Risk, 8 - Len(_PREFIX))

                    If IsNumeric(Right(_maxClass.Risk, 8 - Len(_PREFIX))) Then
                        NewRisk = _PREFIX & Right("00000000" & (Convert.ToInt32(_maxno) + 1).ToString(), 8 - Len(_PREFIX))
                    Else
                        NewRisk = _PREFIX & Right("00000001", 8 - Len(_PREFIX))
                    End If

                End If
            Else

                Using dc_SIBIS As New DataClasses_SIBISExt()
                    Dim _data_cor = (From c In dc_SIBIS.ClassOfRisks Where c.Risk.Equals(newRiskCode.Text.ToUpper())).FirstOrDefault()
                    If _data_cor IsNot Nothing Then
                        e.Result = String.Format("Risk {0} has already in SIBIS.", newRiskCode.Text.ToUpper())

                        Return
                    Else
                        NewRisk = newRiskCode.Text.ToUpper()
                    End If
                End Using

            End If




            If String.IsNullOrEmpty(NewRisk.Trim()) Then
                e.Result = "Invalid Risk Code"
            Else
                Dim new_cor As New Register_ClassOfRisk()

                With new_cor
                    .Risk = NewRisk
                    .Department = newDepartment.Text
                    .Description = newDescription.Text
                    .Prefix = newPrefix.Text

                    '.RiskGovernment = _RISKGRP.RiskGovernment
                    '.RiskGroupI = _RISKGRP.RiskGroupI
                    '.RiskGroupII = _RISKGRP.RiskGroupII
                    '.RiskShortDesc = _RISKGRP.RiskShortDesc

                    .InsuranceLine = Convert.ToInt32(newInsuranceLine.Value)
                    .RiskGroupID = Convert.ToInt32(newInsuranceLine.Value)



                    .CreationBy = HttpContext.Current.User.Identity.Name
                    .CreationDate = DateTime.Now()

                    .IsActive = True
                    .IsGeneralCode = False

                End With
                dc_CPS.Register_ClassOfRisks.InsertOnSubmit(new_cor)

                dc_CPS.SubmitChanges()

                e.Result = "success"
            End If
        End Using




    End Sub

    Protected Sub cbImportCOR_Callback(ByVal source As Object, ByVal e As CallbackEventArgs) Handles cbImportCOR.Callback
        Using dc_CPS As New DataClasses_CPSExt()
            Dim _data = (From c In dc_CPS.Register_ClassOfRisks Where c.Risk.Equals(GridCORLookup.Value)).FirstOrDefault()
            If _data Is Nothing Then

                Using dc_SIBIS As New DataClasses_SIBISExt()

                    Dim _data_cor = (From c In dc_SIBIS.ClassOfRisks Where c.Risk.Equals(GridCORLookup.Value)).FirstOrDefault()

                    Dim _data_cor_uw = (From c In dc_SIBIS.RiskUwriters Where c.Risk.Equals(GridCORLookup.Value)).ToList()

                    Dim _data_cor_ag = (From c In dc_SIBIS.AgentRiskComms Where c.Risk.Equals(GridCORLookup.Value)).ToList()

                    'Dim _data_uw = (From c In dc_SIBIS.Underwriters Where c.Underwriter.Equals(_data_cor_uw(0).Uwriter)).FirstOrDefault()

                    If _data_cor IsNot Nothing Then
                        Dim new_cor As New Register_ClassOfRisk()

                        With new_cor
                            .Risk = _data_cor.Risk
                            .RiskGovernment = _data_cor.RiskGovernment
                            .RiskGroupI = _data_cor.RiskGroupI
                            .RiskGroupII = _data_cor.RiskGroupII
                            .RiskShortDesc = _data_cor.RiskShortDesc
                            .Department = _data_cor.Department
                            .Description = _data_cor.Description
                            '.RiskGroupID = RiskGroup.Value
                            .InsuranceLine = Convert.ToInt32(InsuranceLine.Value.ToString())
                            .RiskGroupID = Convert.ToInt32(InsuranceLine.Value.ToString())

                            .CreationBy = HttpContext.Current.User.Identity.Name
                            .CreationDate = DateTime.Now()
                            .RequestBy = HttpContext.Current.User.Identity.Name
                            .RequestDate = DateTime.Now()
                            .ApproveBy = HttpContext.Current.User.Identity.Name
                            .ApproveDate = DateTime.Now()

                            .IsActive = True
                            .IsGeneralCode = False

                        End With
                        dc_CPS.Register_ClassOfRisks.InsertOnSubmit(new_cor)

                        If _data_cor_uw.Count > 0 Then
                            Dim new_uw_list As New List(Of Register_RiskUwriter)
                            For Each item_uw In _data_cor_uw


                                Dim new_uw As New Register_RiskUwriter
                                With new_uw
                                    .Risk = item_uw.Risk
                                    .Uwriter = item_uw.Uwriter
                                    .Description = item_uw.Description
                                    .Brokerage = item_uw.Brokerage
                                    .AgentCommission = item_uw.AgentCommission
                                    .PremiumWarantydays = item_uw.PremiumWarantydays
                                    .NewRenewaldays = item_uw.NewRenewaldays
                                    .NewNewBusdays = item_uw.NewNewBusdays
                                    .RenRenewaldays = item_uw.RenRenewaldays
                                    .RenNewBusdays = item_uw.RenNewBusdays
                                    .AutoCalculation = item_uw.AutoCalculation
                                    .ORCommissionPercent = item_uw.ORCommissionPercent
                                    .OROutRate = item_uw.OROutRate
                                    .AdminFeeIn1 = item_uw.AdminFeeIn1
                                    .AdminFeeIn1Cal = item_uw.AdminFeeIn1Cal
                                    .AdminFeeIn2 = item_uw.AdminFeeIn2
                                    .AdminFeeIn2Cal = item_uw.AdminFeeIn2Cal
                                    .ORCalFrom = item_uw.ORCalFrom
                                    .OffsetORFlag = item_uw.OffsetORFlag
                                    .OffsetAdm1Flag = item_uw.OffsetAdm1Flag
                                    .OffsetAdm2Flag = item_uw.OffsetAdm2Flag
                                    .BrokerageCal = item_uw.BrokerageCal
                                    .ORInCal = item_uw.ORInCal


                                    .CreationBy = HttpContext.Current.User.Identity.Name
                                    .CreationDate = DateTime.Now()
                                    .RequestBy = HttpContext.Current.User.Identity.Name
                                    .RequestDate = DateTime.Now()
                                    .ApproveBy = HttpContext.Current.User.Identity.Name
                                    .ApproveDate = DateTime.Now()

                                    .IsActive = True
                                End With
                                new_uw_list.Add(new_uw)

                            Next

                            If new_uw_list.Count > 0 Then dc_CPS.Register_RiskUwriters.InsertAllOnSubmit(new_uw_list)
                        End If

                        Dim new_ag_list As New List(Of Register_AgentRiskComm)
                        For Each item_ag In _data_cor_ag
                            Dim new_ag As New Register_AgentRiskComm
                            With new_ag
                                .Agent = item_ag.Agent
                                .Risk = item_ag.Risk
                                .CommissionOut = item_ag.CommissionOut
                                .OROut = item_ag.OROut
                                .OROutCalFrom = item_ag.OROutCalFrom
                                .AdminOut1 = item_ag.AdminOut1
                                .AdminOut1Cal = item_ag.AdminOut1Cal
                                .AdminOut2 = item_ag.AdminOut2
                                .AdminOut2Cal = item_ag.AdminOut2Cal
                                .EntryBy = item_ag.EntryBy
                                .EntryDate = item_ag.EntryDate
                                .CommOutCal = item_ag.CommOutCal
                                .OROutCal = item_ag.OROutCal

                                .CreationBy = HttpContext.Current.User.Identity.Name
                                .CreationDate = DateTime.Now()
                                .RequestBy = HttpContext.Current.User.Identity.Name
                                .RequestDate = DateTime.Now()
                                .ApproveBy = HttpContext.Current.User.Identity.Name
                                .ApproveDate = DateTime.Now()

                                .IsActive = True
                            End With
                            new_ag_list.Add(new_ag)
                        Next
                        If new_ag_list.Count > 0 Then dc_CPS.Register_AgentRiskComms.InsertAllOnSubmit(new_ag_list)


                        dc_CPS.SubmitChanges()
                    End If
                End Using
                e.Result = "success"
            Else
                e.Result = String.Format("Risk {0} has already in system.", GridCORLookup.Value)
            End If
        End Using


    End Sub

    Protected Sub gridCOR_HtmlRowCreated(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewTableRowEventArgs) Handles gridCOR.HtmlRowPrepared
        'ColorGroupRow(e, Color.Aquamarine, 0)
        If e.RowType = GridViewRowType.Group Then
            e.Row.Font.Bold = True
        End If


        If e.RowType <> DevExpress.Web.GridViewRowType.Data Then
            Return
        End If
        Dim _CoverageID = e.KeyValue().ToString()
        Dim _ApproveDate = e.GetValue("ApproveDate").ToString()
        Dim _RequestDate = e.GetValue("RequestDate").ToString()
        Dim _CreationBy = e.GetValue("CreationBy").ToString()
        Dim RowEditable As ASPxButton = gridCOR.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "RowEditable")
        RowEditable.JSProperties.Add("cpVisibleIndex", e.VisibleIndex)
        If String.IsNullOrEmpty(_ApproveDate) And String.IsNullOrEmpty(_RequestDate) Then
            RowEditable.Visible = True
        Else
            RowEditable.Visible = False
        End If

        If _CreationBy.ToString().ToLower() <> HttpContext.Current.User.Identity.Name.ToString().ToLower() Then
            RowEditable.Visible = False
        End If

        'Dim RowDeleteable As ASPxButton = gridCOR.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "RowDeleteable")
        'If _Editable.Equals("1") Then
        '    RowEditable.Visible = True
        '    RowDeleteable.Visible = True


        '    If RowEditable.JSProperties.Keys.Count = 0 Then
        '        RowEditable.JSProperties.Add("cpVisibleIndex", e.VisibleIndex)
        '    End If
        '    If RowDeleteable.JSProperties.Keys.Count = 0 Then
        '        RowDeleteable.JSProperties.Add("cpVisibleIndex", e.VisibleIndex)
        '    End If
        'Else
        '    RowEditable.Visible = False
        '    RowDeleteable.Visible = False
        'End If

    End Sub

    Private Sub ColorGroupRow(ByVal e As ASPxGridViewTableRowEventArgs, ByVal backColor As Color, ByVal rowIndex As Integer)
        If e.RowType = GridViewRowType.Group AndAlso gridCOR.GetRowLevel(e.VisibleIndex) = rowIndex Then
            'e.Row.BackColor = backColor
            e.Row.Font.Bold = True
        Else
            'If e.Row.Cells.Count > rowIndex Then
            '    e.Row.Cells(rowIndex).BackColor = backColor
            'End If
            'If rowIndex = 0 Then
            '    ColorGroupRow(e, Color.Lavender, 1)
            'End If
        End If
    End Sub



    Protected Sub callbackPanel_MoreInfo_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase) Handles callbackPanel_MoreInfo.Callback

        btnRequestCOR.Visible = False
        'btnRequestCommIn.Visible = False
        'btnRequestCommOut.Visible = False

        Dim _Risk As String = e.Parameter.ToString()

        Using dc As New DataClasses_CPSExt()

            Dim _data_cor = (From c In dc.V_ClassOfRisks Where c.Risk.Equals(_Risk)).FirstOrDefault()
            lbRisk.Text = _data_cor.Risk
            With _data_cor
                lbDepartment.Text = .Department
                lbRiskShortDesc.Text = .RiskShortDesc
                lbDescription.Text = .Description
                lbRiskGroupI.Text = .RiskGroupI
                lbRiskGroupII.Text = .RiskGroupII
                lbRiskGovernment.Text = .RiskGovernment
                lbInsuranceLine.Text = .InsuranceLine
                lbStatus.ImageUrl = String.Format("~/images/{0}.gif", .status)

                'If .status.Equals("new") Then
                '    btnRequestCOR.Visible = True
                'End If


                'Dim _newcomm_in = (From c In dc.V_RiskUwriters Where c.Risk.Equals(_Risk) And c.status.Equals("new")).ToList()
                'If btnRequestCOR.Visible = False And _newcomm_in.Count > 0 Then
                '    btnRequestCommIn.Visible = True
                'End If

                'Dim _newcomm_out = (From c In dc.V_AgentRiskComms Where c.Risk.Equals(_Risk) And c.status.Equals("new")).ToList()
                'If btnRequestCOR.Visible = False And btnRequestCommIn.Visible = False And _newcomm_out.Count > 0 Then
                '    btnRequestCommOut.Visible = True
                'End If


                Dim _newcomm_in = (From c In dc.V_RiskUwriters Where c.Risk.Equals(_Risk) And c.status.Equals("new")).ToList()
                If _newcomm_in.Count > 0 Then
                    btnRequestCOR.Visible = True
                End If

                Dim _newcomm_out = (From c In dc.V_AgentRiskComms Where c.Risk.Equals(_Risk) And c.status.Equals("new")).ToList()
                If _newcomm_out.Count > 0 Then
                    btnRequestCOR.Visible = True
                End If
            End With
        End Using
        Session("Risk") = _Risk

        gridCommIn.DataBind()
        gridCommOut.DataBind()

    End Sub

    Protected Sub cbRequestCOR_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbRequestCOR.Callback

        Using dc As New DataClasses_CPSExt()
            Dim _Risk As String = e.Parameter.ToString()
            Dim _data_cor = (From c In dc.Register_ClassOfRisks Where c.Risk.Equals(_Risk)).FirstOrDefault()
            If _data_cor.RequestDate Is Nothing Then
                _data_cor.RequestDate = Now()
                _data_cor.RequestBy = HttpContext.Current.User.Identity.Name
            End If


            Dim _data_uwrisk = (From c In dc.Register_RiskUwriters Where c.Risk.Equals(_Risk) And c.RequestDate Is Nothing And c.ApproveDate Is Nothing).ToList()
            If _data_uwrisk.Count > 0 Then
                For Each item_ur In _data_uwrisk
                    item_ur.RequestDate = Now()
                    item_ur.RequestBy = HttpContext.Current.User.Identity.Name
                Next
            End If

            Dim _data_agrisk = (From c In dc.Register_AgentRiskComms Where c.Risk.Equals(_Risk) And c.RequestDate Is Nothing And c.ApproveDate Is Nothing).ToList()
            If _data_agrisk.Count > 0 Then
                For Each item_ar In _data_agrisk
                    item_ar.RequestDate = Now()
                    item_ar.RequestBy = HttpContext.Current.User.Identity.Name
                Next
            End If


            If _data_uwrisk.Count > 0 Or _data_agrisk.Count > 0 Then

                SendMailRequest(_Risk)


                dc.SubmitChanges()


                e.Result = "success"

            Else
                e.Result = "No Commission In/Out"
            End If

        End Using
    End Sub

    'Protected Sub cbRequestCommIn_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbRequestCommIn.Callback

    '    Using dc As New DataClasses_CPSExt()
    '        Dim _Risk As String = e.Parameter.ToString()


    '        Dim _data_uwrisk = (From c In dc.Register_RiskUwriters Where c.Risk.Equals(_Risk) And c.RequestDate Is Nothing And c.ApproveDate Is Nothing).FirstOrDefault()
    '        If _data_uwrisk IsNot Nothing Then
    '            _data_uwrisk.RequestDate = Now()
    '            _data_uwrisk.RequestBy = HttpContext.Current.User.Identity.Name
    '        End If


    '        dc.SubmitChanges()

    '        e.Result = "success"
    '    End Using



    'End Sub
    'Protected Sub cbRequestCommOut_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbRequestCommOut.Callback

    '    Using dc As New DataClasses_CPSExt()
    '        Dim _Risk As String = e.Parameter.ToString()


    '        Dim _data_agrisk = (From c In dc.Register_AgentRiskComms Where c.Risk.Equals(_Risk) And c.RequestDate Is Nothing And c.ApproveDate Is Nothing).ToList()
    '        For Each item In _data_agrisk
    '            item.RequestDate = Now()
    '            item.RequestBy = HttpContext.Current.User.Identity.Name
    '        Next

    '        dc.SubmitChanges()

    '        e.Result = "success"
    '    End Using

    'End Sub

    ''cbImportCoverageData
    'Protected Sub cbImportCoverageData_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbImportCoverageData.Callback

    '    Using dc As New DataClasses_CPSExt()
    '        'Dim _RISKGRP = (From c In dc.tblRiskGroups Where c.RiskGroupID.Equals(newImportRiskGroup.Value)).FirstOrDefault()
    '        Dim i As Integer = 0
    '        Dim reader = New StringReader(tbxdata.Text)
    '        Dim line As String
    '        line = reader.ReadLine()
    '        Dim _ClassOfRisk As New List(Of Register_ClassOfRisk) 'Register_ClassOfRisk
    '        While line IsNot Nothing


    '            Dim _row() As String = line.Split(vbTab)
    '            Dim _IsGenClass = _row(0).ToString()
    '            Dim _Class = _row(1).ToString()
    '            Dim _Description = _row(2).ToString()
    '            Dim _Department = _row(3).ToString()
    '            Dim item As New Register_ClassOfRisk
    '            With item
    '                .Risk = _Class.Trim().ToUpper()
    '                .Department = _Department
    '                .Description = _Description

    '                .RiskGroupI = _RISKGRP.RiskGroupI
    '                .RiskGroupII = _RISKGRP.RiskGroupII
    '                .RiskGovernment = _RISKGRP.RiskGovernment
    '                .RiskShortDesc = _RISKGRP.RiskShortDesc
    '                .RiskGroupID = _RISKGRP.RiskGroupID

    '                .IsActive = True
    '                .CreationDate = Now()
    '                .CreationBy = HttpContext.Current.User.Identity.Name

    '                If _IsGenClass.Trim().Equals("1") Then
    '                    .IsGeneralCode = True
    '                Else
    '                    .IsGeneralCode = False
    '                End If


    '                _ClassOfRisk.Add(item)

    '                line = reader.ReadLine()
    '            End With
    '        End While







    '        Dim sbError As New StringBuilder()

    '        If _ClassOfRisk.Count = 0 Then
    '            e.Result = "No Class Import Data"
    '            Return
    '        End If

    '        'Error1. Check _IsGenClass<>1 and Risk is empty
    '        Dim Error1 As String = ""
    '        For index = 0 To _ClassOfRisk.Count - 1
    '            If _ClassOfRisk(index).IsGeneralCode = False And _ClassOfRisk(index).Risk.Equals("") Then
    '                Error1 = Error1 & ", " & (index + 1).ToString()
    '            End If
    '        Next
    '        If Not String.IsNullOrEmpty(Error1) Then
    '            e.Result = "No Class in Row : " & Error1
    '            Return
    '        End If


    '        'Error2. Check _IsGenClass<>1 and Risk is already in SIBIS
    '        Dim Error2 As String = ""
    '        Using dc_SIBIS As New DataClasses_SIBISExt()
    '            For index = 0 To _ClassOfRisk.Count - 1
    '                If _ClassOfRisk(index).IsGeneralCode = False And Not _ClassOfRisk(index).Risk.Equals("") Then
    '                    Dim _risk1 = _ClassOfRisk(index).Risk.ToString().ToUpper()
    '                    Dim _risk2 = (From c In dc_SIBIS.ClassOfRisks Where c.Risk.Trim().Equals(_risk1)).FirstOrDefault()
    '                    If _risk2 IsNot Nothing Then
    '                        Error2 = Error2 & ", " & _risk1
    '                    End If
    '                End If
    '            Next
    '        End Using

    '        If Not String.IsNullOrEmpty(Error2) Then
    '            e.Result = "Already Class in SIBIS : " & Error2
    '            Return
    '        End If




    '        'Error3. Check _IsGenClass<>1 and Risk is already in CPS
    '        Dim Error3 As String = ""

    '        For index = 0 To _ClassOfRisk.Count - 1
    '            If _ClassOfRisk(index).IsGeneralCode = False And Not _ClassOfRisk(index).Risk.Equals("") Then
    '                Dim _risk1 = _ClassOfRisk(index).Risk.ToString().ToUpper()
    '                Dim _risk2 = (From c In dc.Register_ClassOfRisks Where c.Risk.Trim().Equals(_risk1)).FirstOrDefault()
    '                If _risk2 IsNot Nothing Then
    '                    Error3 = Error3 & ", " & _risk1
    '                End If
    '            End If
    '        Next
    '        If Not String.IsNullOrEmpty(Error3) Then
    '            e.Result = "Already Class in System : " & Error3
    '            Return
    '        End If




    '        'Insert  Check _IsGenClass<>1 and New Risk
    '        Dim _Manual_Risk = (From c In _ClassOfRisk Where c.IsGeneralCode = False).ToList()
    '        dc.Register_ClassOfRisks.InsertAllOnSubmit(_Manual_Risk)
    '        dc.SubmitChanges()


    '        'Insert  Check _IsGenClass = 1
    '        Dim _Gen_Risk = (From c In _ClassOfRisk Where c.IsGeneralCode = True).ToList()
    '        For Each item_risk In _Gen_Risk
    '            Dim _maxClass = (From c In dc.Register_ClassOfRisks Where c.Risk.StartsWith(_RISKGRP.Prefix) Order By c.Risk Descending).FirstOrDefault()
    '            Dim NewRisk As String = ""
    '            If _maxClass Is Nothing Then
    '                NewRisk = _RISKGRP.Prefix & "000001"
    '            Else
    '                Dim _maxno = Convert.ToInt32(Right(_maxClass.Risk, 6)) + 1
    '                NewRisk = _RISKGRP.Prefix & Right("00000000" & _maxno.ToString(), 6)
    '            End If

    '            item_risk.Risk = NewRisk

    '            dc.Register_ClassOfRisks.InsertOnSubmit(item_risk)
    '            dc.SubmitChanges()
    '        Next



    '        e.Result = "success"

    '    End Using

    'End Sub



    'cbSendRequestClassOfRisk
    Protected Sub SendMailRequest(ByVal _Risk As String)
        Dim _filename1 As String = String.Format("CommIn-{0}.xls", Guid.NewGuid().ToString())
        Dim _path1 = String.Format("{0}/{1}", Server.MapPath("~/saved_files"), _filename1)
        Dim _filename2 As String = String.Format("CommOut-{0}.xls", Guid.NewGuid().ToString())
        Dim _path2 = String.Format("{0}/{1}", Server.MapPath("~/saved_files"), _filename2)

        Using dc As New DataClasses_CPSExt()
            Dim _data_cor = (From c In dc.Register_ClassOfRisks Where c.Risk.Equals(_Risk)).FirstOrDefault()
            Dim _data_uwrisk = (From c In dc.V_RiskUwriters Where c.Risk.Equals(_Risk) And c.RequestDate Is Nothing And c.ApproveDate Is Nothing).ToList()
            If _data_uwrisk.Count > 0 Then

                '================================== Comm In ========================================
                'initialize a new instance of Workbook
                Dim workbook1 As New Workbook()
                'open a template Excel file
                workbook1.LoadFromFile(Server.MapPath("~\Template\ClassOfRiskCommIn_Register.xlsx"))
                'get the first sheet 
                Dim worksheet1 As Worksheet = workbook1.Worksheets(0)
                '===================== Header ==============================
                worksheet1.Range("B4").Text = HttpContext.Current.User.Identity.Name
                worksheet1.Range("B5").Text = Now().ToString("dd/MM/yyyy")
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
                            worksheet1.Range("F11").Text = .Brokerage_display
                            worksheet1.Range("G11").Text = .ORCommissionPercent_display
                            worksheet1.Range("H11").Text = .AdminFeeIn1_display
                            worksheet1.Range("I11").Text = .AdminFeeIn2_display
                            worksheet1.Range("J11").Text = IIf(.OffsetORFlag, "P", "")
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
                            worksheet1.Range(String.Format("F{0}", irow)).Text = .Brokerage_display
                            worksheet1.Range(String.Format("G{0}", irow)).Text = .ORCommissionPercent_display
                            worksheet1.Range(String.Format("H{0}", irow)).Text = .AdminFeeIn1_display
                            worksheet1.Range(String.Format("I{0}", irow)).Text = .AdminFeeIn2_display
                            worksheet1.Range(String.Format("J{0}", irow)).Text = IIf(.OffsetORFlag, "P", "")
                            i = i + 1
                            worksheet1.InsertRow(startrow + i)
                        End If
                    End With
                Next
                workbook1.SaveToFile(_path1, ExcelVersion.Version2010)
            End If



            Dim _data_agrisk = (From c In dc.V_AgentRiskComms Where c.Risk.Equals(_Risk) And c.RequestDate Is Nothing And c.ApproveDate Is Nothing).ToList()
            If _data_agrisk.Count > 0 Then
                '================================== Comm Out ========================================
                'initialize a new instance of Workbook
                Dim workbook2 As New Workbook()
                'open a template Excel file
                workbook2.LoadFromFile(Server.MapPath("~\Template\ClassOfRiskCommOut_Register.xlsx"))
                'get the first sheet 
                Dim worksheet2 As Worksheet = workbook2.Worksheets(0)
                '===================== Header ==============================
                worksheet2.Range("B4").Text = HttpContext.Current.User.Identity.Name
                worksheet2.Range("B5").Text = Now().ToString("dd/MM/yyyy")
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

                            worksheet2.Range("G10").Text = .CommissionOut_Display
                            worksheet2.Range("H10").Text = .OROut_Display
                            worksheet2.Range("I10").Text = .AdminOut1_Display
                            worksheet2.Range("J10").Text = .AdminOut2_Display
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
                            worksheet2.Range(String.Format("G{0}", irow)).Text = .CommissionOut_Display
                            worksheet2.Range(String.Format("H{0}", irow)).Text = .OROut_Display
                            worksheet2.Range(String.Format("I{0}", irow)).Text = .AdminOut1_Display
                            worksheet2.Range(String.Format("J{0}", irow)).Text = .AdminOut2_Display
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
            Dim _mailNotification = (From c In dc_portal.MailNotifications Where c.Code.Equals("N0019")).FirstOrDefault()
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
                att1.Name = String.Format("Request_CommIn_By_{0}_{1}.xls", HttpContext.Current.User.Identity.Name, DateTime.Now.ToString("yyyyMMddHHmmss"))
                msg.Attachments.Add(att1)

            End If
            If File.Exists(_path2) Then
                Dim att2 = New Attachment(_path2)
                att2.Name = String.Format("Request_CommOut_By_{0}_{1}.xls", HttpContext.Current.User.Identity.Name, DateTime.Now.ToString("yyyyMMddHHmmss"))
                msg.Attachments.Add(att2)
            End If

            MySmtpClient.Send(msg)
        End Using

 

       

 
    End Sub

End Class
