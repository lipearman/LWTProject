Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports DevExpress.Web
Imports System.IO
Imports System.Drawing
Imports DevExpress.Web.Data
Imports System.Net.Mail
Imports Spire.Xls

Partial Class Modules_ucDevxClassOfRiskAmendRequest
    Inherits PortalModuleControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        SqlDataSource_ClassOfRisk_Amend.SelectParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name
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

    Protected Sub callbackPanel_MoreInfo_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase) Handles callbackPanel_MoreInfo.Callback

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

            End With
        End Using

        Session("AmendRiskID") = _AmendRiskID

        gridCommInRequest.DataBind()
        gridCommOutRequest.DataBind()

    End Sub








    Protected Sub cbApproveAmend_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbApproveAmend.Callback
        Using dc As New DataClasses_CPSExt()
            Dim _AmendRiskID As String = Session("AmendRiskID").ToString()

            Dim _data = (From c In dc.Register_ClassOfRisk_Amends Where c.AmendRiskID.Equals(_AmendRiskID)).FirstOrDefault()
            _data.ApproveDate = Now()
            _data.ApproveBy = HttpContext.Current.User.Identity.Name

            SendMailApprove(_AmendRiskID)

            '====================== Start Amend Class Of Risk =======================
            '1. Update Register.ClassOfRisks
            Dim _data_cor = (From c In dc.Register_ClassOfRisks Where c.Risk.Trim().Equals(_data.Risk.Trim())).FirstOrDefault()
            Dim _Register_ClassOfRisk_Log As New Register_ClassOfRisk_Log
            With _Register_ClassOfRisk_Log             
                .Risk = _data_cor.risk
                .Description = _data_cor.Description
                .RiskGroupI = _data_cor.RiskGroupI
                .RiskGroupII = _data_cor.RiskGroupII
                .RiskShortDesc = _data_cor.RiskShortDesc
                .RiskGovernment = _data_cor.RiskGovernment
                .Department = _data_cor.Department
                .RiskGroupID = _data_cor.RiskGroupID
                .RiskYear = _data_cor.RiskYear
                .EffectiveDate = _data_cor.EffectiveDate
                .ExpiryDate = _data_cor.ExpiryDate
                .CreationDate = _data_cor.CreationDate
                .CreationBy = _data_cor.CreationBy
                .RequestDate = _data_cor.RequestDate
                .RequestBy = _data_cor.RequestBy
                .ApproveDate = _data_cor.ApproveDate
                .ApproveBy = _data_cor.ApproveBy
                .IsActive = _data_cor.IsActive
                .ModifyBy = _data_cor.ModifyBy
                .ModifyDate = _data_cor.ModifyDate
                .Remark = _data_cor.Remark
                .IsGeneralCode = _data_cor.IsGeneralCode
                .InsuranceLine = _data_cor.InsuranceLine
                .Prefix = _data_cor.Prefix
                .AmendRiskID = _AmendRiskID
            End With
            dc.Register_ClassOfRisk_Logs.InsertOnSubmit(_Register_ClassOfRisk_Log)


            '1. Update Register.RiskUwriter
            Dim _data_uwrisk = (From c In dc.Register_RiskUwriter_Amends Where c.AmendRiskID.Equals(_AmendRiskID)).ToList()


            Using dc1 As New DataClasses_SIBISExt()
                If _data_uwrisk.Count > 0 Then

                    Dim _Register_RiskUwriter_LogLst As New List(Of Register_RiskUwriter_Log)

                    Dim _RiskUwriter = (From c In dc1.RiskUwriters Where c.Risk.Trim().Equals(_data.Risk.Trim())).ToList()
                    For Each item_ur In _data_uwrisk

                        For Each item_RiskUwriter In _RiskUwriter
                            If item_RiskUwriter.Risk.Trim().Equals(item_ur.Risk.Trim()) And item_RiskUwriter.Uwriter.Trim().Equals(item_ur.Uwriter.Trim()) Then
                                With item_RiskUwriter
                                    '.Risk = item_ur.Risk
                                    '.Uwriter = item_ur.Uwriter
                                    .Description = item_ur.Description
                                    .Brokerage = item_ur.Brokerage
                                    .AgentCommission = item_ur.AgentCommission
                                    .PremiumWarantydays = item_ur.PremiumWarantydays
                                    .NewRenewaldays = item_ur.NewRenewaldays
                                    .NewNewBusdays = item_ur.NewNewBusdays
                                    .RenRenewaldays = item_ur.RenRenewaldays
                                    .RenNewBusdays = item_ur.RenNewBusdays
                                    .AutoCalculation = item_ur.AutoCalculation
                                    .ORCommissionPercent = item_ur.ORCommissionPercent
                                    .OROutRate = item_ur.OROutRate
                                    .AdminFeeIn1 = item_ur.AdminFeeIn1
                                    .AdminFeeIn1Cal = item_ur.AdminFeeIn1Cal
                                    .AdminFeeIn2 = item_ur.AdminFeeIn2
                                    .AdminFeeIn2Cal = item_ur.AdminFeeIn2Cal
                                    .ORCalFrom = item_ur.ORCalFrom
                                    .OffsetORFlag = item_ur.OffsetORFlag
                                    .OffsetAdm1Flag = item_ur.OffsetAdm1Flag
                                    .OffsetAdm2Flag = item_ur.OffsetAdm2Flag
                                    .BrokerageCal = item_ur.BrokerageCal
                                    .ORInCal = item_ur.ORInCal
                                End With

                                Dim _Register_RiskUwriters = (From c In dc.Register_RiskUwriters Where c.Risk.Trim().Equals(item_ur.Risk.Trim()) And c.Uwriter.Trim().Equals(item_ur.Uwriter.Trim())).FirstOrDefault()
                                Dim _Register_RiskUwriter_Log As New Register_RiskUwriter_Log
                                With _Register_RiskUwriter_Log
                                    .Risk = _Register_RiskUwriters.Risk
                                    .Uwriter = _Register_RiskUwriters.Uwriter
                                    .Description = _Register_RiskUwriters.Description
                                    .Brokerage = _Register_RiskUwriters.Brokerage
                                    .AgentCommission = _Register_RiskUwriters.AgentCommission
                                    .PremiumWarantydays = _Register_RiskUwriters.PremiumWarantydays
                                    .NewRenewaldays = _Register_RiskUwriters.NewRenewaldays
                                    .NewNewBusdays = _Register_RiskUwriters.NewNewBusdays
                                    .RenRenewaldays = _Register_RiskUwriters.RenRenewaldays
                                    .RenNewBusdays = _Register_RiskUwriters.RenNewBusdays
                                    .AutoCalculation = _Register_RiskUwriters.AutoCalculation
                                    .ORCommissionPercent = _Register_RiskUwriters.ORCommissionPercent
                                    .OROutRate = _Register_RiskUwriters.OROutRate
                                    .AdminFeeIn1 = _Register_RiskUwriters.AdminFeeIn1
                                    .AdminFeeIn1Cal = _Register_RiskUwriters.AdminFeeIn1Cal
                                    .AdminFeeIn2 = _Register_RiskUwriters.AdminFeeIn2
                                    .AdminFeeIn2Cal = _Register_RiskUwriters.AdminFeeIn2Cal
                                    .ORCalFrom = _Register_RiskUwriters.ORCalFrom
                                    .OffsetORFlag = _Register_RiskUwriters.OffsetORFlag
                                    .OffsetAdm1Flag = _Register_RiskUwriters.OffsetAdm1Flag
                                    .OffsetAdm2Flag = _Register_RiskUwriters.OffsetAdm2Flag
                                    .BrokerageCal = _Register_RiskUwriters.BrokerageCal
                                    .ORInCal = _Register_RiskUwriters.ORInCal
                                    .CreationDate = _Register_RiskUwriters.CreationDate
                                    .CreationBy = _Register_RiskUwriters.CreationBy
                                    .RequestDate = _Register_RiskUwriters.RequestDate
                                    .RequestBy = _Register_RiskUwriters.RequestBy
                                    .ApproveDate = _Register_RiskUwriters.ApproveDate
                                    .ApproveBy = _Register_RiskUwriters.ApproveBy
                                    .IsActive = _Register_RiskUwriters.IsActive
                                    .Remark = _Register_RiskUwriters.Remark
                                    .ModifyBy = _Register_RiskUwriters.ModifyBy
                                    .ModifyDate = _Register_RiskUwriters.ModifyDate
                                    .AmendRiskID = _AmendRiskID
                                End With
                                _Register_RiskUwriter_LogLst.Add(_Register_RiskUwriter_Log)

                            End If
                        Next
                    Next


                    If _Register_RiskUwriter_LogLst.Count > 0 Then
                        dc.Register_RiskUwriter_Logs.InsertAllOnSubmit(_Register_RiskUwriter_LogLst)
                    End If
                End If

                '2. update Register.AgentRiskComm
                Dim _data_agrisk = (From c In dc.Register_AgentRiskComm_Amends Where c.AmendRiskID.Equals(_AmendRiskID)).ToList()
                If _data_agrisk.Count > 0 Then

                    Dim _Register_AgentRiskComm_LogLst As New List(Of Register_AgentRiskComm_Log)

                    Dim _AgentRiskComm = (From c In dc1.AgentRiskComms Where c.Risk.Trim().Equals(_data.Risk.Trim())).ToList()
                    For Each itemar In _data_agrisk
                        For Each item_AgentRiskComm In _AgentRiskComm
                            If item_AgentRiskComm.Risk.Trim().Equals(itemar.Risk.Trim()) And item_AgentRiskComm.Agent.Trim().Equals(itemar.Agent.Trim()) Then
                                With item_AgentRiskComm
                                    '.Risk = itemar.Risk
                                    '.Agent = itemar.Agent
                                    .CommissionOut = itemar.CommissionOut
                                    .OROut = itemar.OROut
                                    .OROutCalFrom = itemar.OROutCalFrom
                                    .AdminOut1 = itemar.AdminOut1
                                    .AdminOut1Cal = itemar.AdminOut1Cal
                                    .AdminOut2 = itemar.AdminOut2
                                    .AdminOut2Cal = itemar.AdminOut2Cal
                                    .EntryBy = itemar.EntryBy
                                    .EntryDate = itemar.EntryDate
                                    .CommOutCal = itemar.CommOutCal
                                    .OROutCal = itemar.OROutCal
                                End With

                                Dim _Register_AgentRiskComms = (From c In dc.Register_AgentRiskComms Where c.Risk.Trim().Equals(itemar.Risk.Trim()) And c.Agent.Trim().Equals(itemar.Agent.Trim())).FirstOrDefault()
                                Dim _Register_AgentRiskComm_Log As New Register_AgentRiskComm_Log
                                With _Register_AgentRiskComm_Log                                    
                                    .Agent = _Register_AgentRiskComms.Agent
                                    .Risk = _Register_AgentRiskComms.Risk
                                    .CommissionOut = _Register_AgentRiskComms.CommissionOut
                                    .OROut = _Register_AgentRiskComms.OROut
                                    .OROutCalFrom = _Register_AgentRiskComms.OROutCalFrom
                                    .AdminOut1 = _Register_AgentRiskComms.AdminOut1
                                    .AdminOut1Cal = _Register_AgentRiskComms.AdminOut1Cal
                                    .AdminOut2 = _Register_AgentRiskComms.AdminOut2
                                    .AdminOut2Cal = _Register_AgentRiskComms.AdminOut2Cal
                                    .EntryBy = _Register_AgentRiskComms.EntryBy
                                    .EntryDate = _Register_AgentRiskComms.EntryDate
                                    .CommOutCal = _Register_AgentRiskComms.CommOutCal
                                    .OROutCal = _Register_AgentRiskComms.OROutCal
                                    .CreationDate = _Register_AgentRiskComms.CreationDate
                                    .CreationBy = _Register_AgentRiskComms.CreationBy
                                    .RequestDate = _Register_AgentRiskComms.RequestDate
                                    .RequestBy = _Register_AgentRiskComms.RequestBy
                                    .ApproveDate = _Register_AgentRiskComms.ApproveDate
                                    .ApproveBy = _Register_AgentRiskComms.ApproveBy
                                    .IsActive = _Register_AgentRiskComms.IsActive
                                    .ModifyBy = _Register_AgentRiskComms.ModifyBy
                                    .ModifyDate = _Register_AgentRiskComms.ModifyDate
                                    .AmendRiskID = _AmendRiskID
                                End With
                                _Register_AgentRiskComm_LogLst.Add(_Register_AgentRiskComm_Log)


                            End If
                        Next
                    Next
                    If _Register_AgentRiskComm_LogLst.Count > 0 Then
                        dc.Register_AgentRiskComm_Logs.InsertAllOnSubmit(_Register_AgentRiskComm_LogLst)
                    End If
                End If

                dc1.SubmitChanges()
            End Using

            '====================== End Amend Class Of Risk =======================


            dc.SubmitChanges()
        End Using

        e.Result = "success"

    End Sub

    Protected Sub SendMailApprove(ByVal _AmendRiskID As Integer)
        Dim _filename1 As String = String.Format("CommIn-{0}.xls", Guid.NewGuid().ToString())
        Dim _path1 = String.Format("{0}/{1}", Server.MapPath("~/saved_files"), _filename1)
        Dim _filename2 As String = String.Format("CommOut-{0}.xls", Guid.NewGuid().ToString())
        Dim _path2 = String.Format("{0}/{1}", Server.MapPath("~/saved_files"), _filename2)

        Using dc As New DataClasses_CPSExt()
            Dim _data_cor = (From c In dc.V_ClassOfRisk_Amends Where c.AmendRiskID.Equals(_AmendRiskID) And c.RequestDate IsNot Nothing And c.ApproveDate Is Nothing).FirstOrDefault()


            Dim _data_uwrisk = (From c In dc.V_RiskUwriter_Amends Where c.AmendRiskID.Equals(_AmendRiskID)).ToList()
            If _data_uwrisk.Count > 0 Then

                '================================== Comm In ========================================
                'initialize a new instance of Workbook
                Dim workbook1 As New Workbook()
                'open a template Excel file
                workbook1.LoadFromFile(Server.MapPath("~\Template\ClassOfRiskCommIn_Amend.xlsx"))
                'get the first sheet 
                Dim worksheet1 As Worksheet = workbook1.Worksheets(0)
                '===================== Header ==============================
                worksheet1.Range("F3").Text = "Approved By"

                worksheet1.Range("B4").Text = _data_cor.RequestBy
                worksheet1.Range("B5").Text = _data_cor.RequestDate
                'worksheet.Range("D4").Text = "Div."
                worksheet1.Range("D5").Text = _data_cor.Department


                worksheet1.Range("F4").Text = HttpContext.Current.User.Identity.Name
                worksheet1.Range("F5").Text = Now()

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



            Dim _data_agrisk = (From c In dc.V_AgentRiskComm_Amends Where c.AmendRiskID.Equals(_AmendRiskID)).ToList()
            If _data_agrisk.Count > 0 Then
                '================================== Comm Out ========================================
                'initialize a new instance of Workbook
                Dim workbook2 As New Workbook()
                'open a template Excel file
                workbook2.LoadFromFile(Server.MapPath("~\Template\ClassOfRiskCommOut_Amend.xlsx"))
                'get the first sheet 
                Dim worksheet2 As Worksheet = workbook2.Worksheets(0)
                '===================== Header ==============================
                worksheet2.Range("F3").Text = "Approved By"

                worksheet2.Range("B4").Text = _data_cor.RequestBy
                worksheet2.Range("B5").Text = _data_cor.RequestDate
                'worksheet.Range("D4").Text = "Div."
                worksheet2.Range("D5").Text = _data_cor.Department

                worksheet2.Range("F4").Text = HttpContext.Current.User.Identity.Name
                worksheet2.Range("F5").Text = Now()

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
            Dim _mailNotification = (From c In dc_portal.MailNotifications Where c.Code.Equals("N0014")).FirstOrDefault()
            Dim _user_from = (From c In dc_portal.V_LWT_USERs Where c.sAMAccountName.Equals(HttpContext.Current.User.Identity.Name)).FirstOrDefault()
            Dim _user_to = (From c In dc_portal.V_LWT_USERs Where c.sAMAccountName.Equals(_data_cor.RequestBy)).FirstOrDefault()


            strSubject = _mailNotification.MailSubject.Replace("{name}", _user_from.displayName).Replace("{date}", DateTime.Now.ToString())
            strMessage.AppendLine(Server.HtmlDecode(_mailNotification.MailBody.Replace("{fromname}", _user_from.displayName).Replace("{toname}", _user_to.givenname)))
            strMailFrom = _user_from.mail
            strMailCC = _mailNotification.MailCC & ";" & _user_from.mail
            strMailTo = _user_to.mail

            'strMailFrom = _user_from.mail
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
                att1.Name = String.Format("ApproveAmend_CommIn_By_{0}_{1}.xls", HttpContext.Current.User.Identity.Name, DateTime.Now.ToString("yyyyMMddHHmmss"))
                msg.Attachments.Add(att1)

            End If
            If File.Exists(_path2) Then
                Dim att2 = New Attachment(_path2)
                att2.Name = String.Format("ApproveAmend_CommOut_By_{0}_{1}.xls", HttpContext.Current.User.Identity.Name, DateTime.Now.ToString("yyyyMMddHHmmss"))
                msg.Attachments.Add(att2)
            End If

            MySmtpClient.Send(msg)
        End Using






    End Sub





    Protected Sub cbRejectAmend_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbRejectAmend.Callback
        Using dc As New DataClasses_CPSExt()
            Dim _AmendRiskID As String = Session("AmendRiskID").ToString()

            Dim _data = (From c In dc.Register_ClassOfRisk_Amends Where c.AmendRiskID.Equals(_AmendRiskID) And c.RequestDate IsNot Nothing And c.ApproveDate Is Nothing).FirstOrDefault()
            _data.RequestDate = Nothing
            _data.RequestBy = Nothing

            SendMailReject(_AmendRiskID)

            dc.SubmitChanges()
        End Using

        e.Result = "success"
    End Sub

    Protected Sub SendMailReject(ByVal _AmendRiskID As Integer)
        Dim _filename1 As String = String.Format("CommIn-{0}.xls", Guid.NewGuid().ToString())
        Dim _path1 = String.Format("{0}/{1}", Server.MapPath("~/saved_files"), _filename1)
        Dim _filename2 As String = String.Format("CommOut-{0}.xls", Guid.NewGuid().ToString())
        Dim _path2 = String.Format("{0}/{1}", Server.MapPath("~/saved_files"), _filename2)

        Using dc As New DataClasses_CPSExt()
            Dim _data_cor = (From c In dc.V_ClassOfRisk_Amends Where c.AmendRiskID.Equals(_AmendRiskID) And c.RequestDate IsNot Nothing And c.ApproveDate Is Nothing).FirstOrDefault()


            Dim _data_uwrisk = (From c In dc.V_RiskUwriter_Amends Where c.AmendRiskID.Equals(_AmendRiskID)).ToList()
            If _data_uwrisk.Count > 0 Then

                '================================== Comm In ========================================
                'initialize a new instance of Workbook
                Dim workbook1 As New Workbook()
                'open a template Excel file
                workbook1.LoadFromFile(Server.MapPath("~\Template\ClassOfRiskCommIn_Amend.xlsx"))
                'get the first sheet 
                Dim worksheet1 As Worksheet = workbook1.Worksheets(0)
                '===================== Header ==============================
                worksheet1.Range("F3").Text = "Rejected By"

                worksheet1.Range("B4").Text = _data_cor.RequestBy
                worksheet1.Range("B5").Text = _data_cor.RequestDate
                'worksheet.Range("D4").Text = "Div."
                worksheet1.Range("D5").Text = _data_cor.Department


                worksheet1.Range("F4").Text = HttpContext.Current.User.Identity.Name
                worksheet1.Range("F5").Text = Now()

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



            Dim _data_agrisk = (From c In dc.V_AgentRiskComm_Amends Where c.AmendRiskID.Equals(_AmendRiskID)).ToList()
            If _data_agrisk.Count > 0 Then
                '================================== Comm Out ========================================
                'initialize a new instance of Workbook
                Dim workbook2 As New Workbook()
                'open a template Excel file
                workbook2.LoadFromFile(Server.MapPath("~\Template\ClassOfRiskCommOut_Amend.xlsx"))
                'get the first sheet 
                Dim worksheet2 As Worksheet = workbook2.Worksheets(0)
                '===================== Header ==============================
                worksheet2.Range("F3").Text = "Rejected By"

                worksheet2.Range("B4").Text = _data_cor.RequestBy
                worksheet2.Range("B5").Text = _data_cor.RequestDate
                'worksheet.Range("D4").Text = "Div."
                worksheet2.Range("D5").Text = _data_cor.Department

                worksheet2.Range("F4").Text = HttpContext.Current.User.Identity.Name
                worksheet2.Range("F5").Text = Now()

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
            Dim _mailNotification = (From c In dc_portal.MailNotifications Where c.Code.Equals("N0015")).FirstOrDefault()

            Dim _user_from = (From c In dc_portal.V_LWT_USERs Where c.sAMAccountName.Equals(HttpContext.Current.User.Identity.Name)).FirstOrDefault()
            Dim _user_to = (From c In dc_portal.V_LWT_USERs Where c.sAMAccountName.Equals(_data_cor.RequestBy)).FirstOrDefault()

            strSubject = _mailNotification.MailSubject.Replace("{name}", _user_from.displayName).Replace("{date}", DateTime.Now.ToString())
            strMessage.AppendLine(Server.HtmlDecode(_mailNotification.MailBody.Replace("{fromname}", _user_from.displayName).Replace("{toname}", _user_to.givenname)))

            strMailFrom = _user_from.mail
            strMailCC = _mailNotification.MailCC & ";" & _user_from.mail
            strMailTo = _user_to.mail

            'strMailFrom = _user_from.mail
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
                att1.Name = String.Format("RejectAmend_CommIn_By_{0}_{1}.xls", HttpContext.Current.User.Identity.Name, DateTime.Now.ToString("yyyyMMddHHmmss"))
                msg.Attachments.Add(att1)

            End If
            If File.Exists(_path2) Then
                Dim att2 = New Attachment(_path2)
                att2.Name = String.Format("RejectAmend_CommOut_By_{0}_{1}.xls", HttpContext.Current.User.Identity.Name, DateTime.Now.ToString("yyyyMMddHHmmss"))
                msg.Attachments.Add(att2)
            End If

            MySmtpClient.Send(msg)
        End Using






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
