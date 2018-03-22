Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports BaseConfig
'Imports DataBind
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Drawing
Imports System.Net.Mail
Imports System.IO
Imports Spire.Xls

Partial Class Modules_ucDevxAgentRiskCommSetup
    Inherits PortalModuleControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        'SqlDataSource_AgentRiskComm.SelectParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name

        SqlDataSource_AgentRiskComm.UpdateParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name

        ' SqlDataSource_COR.SelectParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name


        If (Not IsPostBack) Then


        End If
    End Sub


    Protected Sub callbackPanel_popupAgentRiskComm_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase) Handles callbackPanel_popupAgentRiskComm.Callback

        Dim _cmd = e.Parameter.ToString()

        If Not String.IsNullOrEmpty(_cmd) Then

            Dim _params = _cmd.Split("|")
            Select Case _params(0)

                Case "select_ag"
                    Dim _agent As String = _params(1)
                    Session("Agent") = _agent
                    gridCOR.DataBind()


                Case "new"
                    Session("Agent") = ""

                    Agent.DataBind()

                    Brokerage.Value = 0
                    BrokerageCal.SelectedIndex = 0
                    [OR].Value = 0
                    ORCal.SelectedIndex = 0
                    Admin1.Value = 0
                    Admin1Cal.SelectedIndex = 0
                    Admin2.Value = 0
                    Admin2Cal.SelectedIndex = 0

                    CalFrom.SelectedIndex = 0
                    OffsetORFlag.Checked = True

                    IsActive.Checked = True

            End Select


        End If





    End Sub

    Protected Sub cbAddAgentRisk_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbAddAgentRisk.Callback

        Dim _dc = New DataClasses_CPSExt()

        Dim _Commission = Convert.ToDouble(Brokerage.Value)
        Dim _CommissionCal = BrokerageCal.Value.ToString()

        If _CommissionCal.ToUpper() = "X" Then
            If _Commission > 17 Then
                e.Result = "Brokerage Over 17%"
                Return
            End If
        End If

        If ORCal.SelectedItem.Value = "x" Then
            If [OR].Value > 99 Then
                e.Result = ("ORComm over 100%")
                Return
            End If
        End If
        If Admin1Cal.SelectedItem.Value = "x" Then
            If Admin1.Value > 99 Then
                e.Result = ("AdminFee1 over 100%")
                Return
            End If
        End If
        If Admin2Cal.SelectedItem.Value = "x" Then
            If Admin2.Value > 99 Then
                e.Result = ("AdminFeeIn2 over 100%")
                Return
            End If
        End If


        Dim _risk As New List(Of String)
        For Each key In gridCOR.GetCurrentPageRowValues("Risk")
            If gridCOR.Selection.IsRowSelectedByKey(key) Then
                _risk.Add(key.ToString())
            End If
        Next key


        If _risk.Count > 0 Then
            Dim _AgentRiskComm As New List(Of Register_AgentRiskComm)
            For Each item_risk In _risk
                Dim newAgentRiskComm As New Register_AgentRiskComm()
                With newAgentRiskComm


                    .Risk = item_risk
                    .Agent = Agent.GridView.GetRowValues(Agent.GridView.FocusedRowIndex, "Agent")

                    .CommissionOut = Convert.ToDouble(Brokerage.Value)
                    .CommOutCal = BrokerageCal.Value.ToString()

                    .OROut = Convert.ToDouble([OR].Value)
                    .OROutCal = ORCal.Value.ToString()

                    .AdminOut1 = Convert.ToDouble(Admin1.Value)
                    .AdminOut1Cal = Admin1Cal.Value.ToString()
                    .AdminOut2 = Convert.ToDouble(Admin2.Value)
                    .AdminOut2Cal = Admin2Cal.Value.ToString()

                    .OROutCalFrom = CalFrom.Value.ToString()

                    .EntryBy = HttpContext.Current.User.Identity.Name
                    .EntryDate = DateTime.Now()


                    .CreationDate = DateTime.Now()
                    .CreationBy = HttpContext.Current.User.Identity.Name
                    .IsActive = IsActive.Checked

                End With
                _AgentRiskComm.Add(newAgentRiskComm)
            Next


            Using dc As New DataClasses_CPSExt()
                dc.Register_AgentRiskComms.InsertAllOnSubmit(_AgentRiskComm)
                dc.SubmitChanges()
            End Using

            e.Result = "success"
        Else
            e.Result = "No selecte Risk"
        End If




    End Sub

    Protected Sub gridAgentRiskComm_HtmlRowCreated(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewTableRowEventArgs) Handles gridAgentRiskComm.HtmlRowPrepared

        'ColorGroupRow(e, Color.Aquamarine, 0)
        If e.RowType = GridViewRowType.Group Then
            e.Row.Font.Bold = True
        End If

        If e.RowType <> DevExpress.Web.GridViewRowType.Data Then
            Return
        End If
        Dim _ID = e.KeyValue().ToString()
        Dim _ApproveDate = e.GetValue("ApproveDate").ToString()
        Dim _RequestDate = e.GetValue("RequestDate").ToString()
        Dim _CreationBy = e.GetValue("CreationBy").ToString()
        Dim RowEditable As ASPxButton = gridAgentRiskComm.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "RowEditable")
        RowEditable.JSProperties.Add("cpVisibleIndex", e.VisibleIndex)
        If String.IsNullOrEmpty(_ApproveDate) And String.IsNullOrEmpty(_RequestDate) Then
            RowEditable.Visible = True
        Else
            RowEditable.Visible = False
        End If

        If _CreationBy.ToString().ToLower() <> HttpContext.Current.User.Identity.Name.ToString().ToLower() Then
            RowEditable.Visible = False
        End If



        Dim RowDeleteable As ASPxButton = gridAgentRiskComm.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "RowDeleteable")
        If RowEditable.Visible Then
            RowEditable.Visible = True
            RowDeleteable.Visible = True


            If RowEditable.JSProperties.Keys.Count = 0 Then
                RowEditable.JSProperties.Add("cpVisibleIndex", e.VisibleIndex)
            End If
            If RowDeleteable.JSProperties.Keys.Count = 0 Then
                RowDeleteable.JSProperties.Add("cpVisibleIndex", e.VisibleIndex)
            End If
        Else
            RowEditable.Visible = False
            RowDeleteable.Visible = False
        End If


    End Sub





    Protected Sub gridAgentRiskComm_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles gridAgentRiskComm.RowUpdating
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

        If CommOutCal.SelectedItem.Value = "x" Then
            If CommissionOut.Value > 17 Then
                Throw New Exception("Brokerage over 17%")
            End If
        End If
        If OROutCal.SelectedItem.Value = "x" Then
            If OROut.Value > 99 Then
                Throw New Exception("ORComm over 100%")
            End If
        End If
        If AdminOut1Cal.SelectedItem.Value = "x" Then
            If AdminOut1.Value > 99 Then
                Throw New Exception("AdminFee1 over 100%")
            End If
        End If
        If AdminOut2Cal.SelectedItem.Value = "x" Then
            If AdminOut2.Value > 99 Then
                Throw New Exception("AdminFeeIn2 over 100%")
            End If
        End If

        'Using dc As New DataClasses_CPSExt()
        '    Dim _data = From  c In dc.Register_RiskUwriters Where c.Risk.Equals(
        'End Using

        e.NewValues("CommissionOut") = CommissionOut.Value
        e.NewValues("CommOutCal") = CommOutCal.SelectedItem.Value
        e.NewValues("OROut") = OROut.Value
        e.NewValues("OROutCal") = OROutCal.SelectedItem.Value
        e.NewValues("AdminOut1") = AdminOut1.Value
        e.NewValues("AdminOut1Cal") = AdminOut1Cal.SelectedItem.Value
        e.NewValues("AdminOut2") = AdminOut2.Value
        e.NewValues("AdminOut2Cal") = AdminOut2Cal.SelectedItem.Value
        e.NewValues("OROutCalFrom") = OROutCalFrom.SelectedItem.Value
        e.NewValues("IsActive") = IsActive.Checked


    End Sub


    Private Sub ColorGroupRow(ByVal e As ASPxGridViewTableRowEventArgs, ByVal backColor As Color, ByVal rowIndex As Integer)
        If e.RowType = GridViewRowType.Group AndAlso gridAgentRiskComm.GetRowLevel(e.VisibleIndex) = rowIndex Then
            'e.Row.BackColor = backColor

            e.Row.Font.Bold = True
        Else
            If e.Row.Cells.Count > rowIndex Then
                'e.Row.Cells(rowIndex).BackColor = backColor
                'e.Row.Font.Bold = True
            End If
            If rowIndex = 0 Then
                'ColorGroupRow(e, Color.Lavender, 1)

            End If
        End If
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

End Class
