Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports System.Net.Mail
Imports System.IO
Imports Spire.Xls

Partial Class Modules_ucDevxRiskUnderwriterSetup
    Inherits PortalModuleControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        'SqlDataSource_RiskUwriter.SelectParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name

        SqlDataSource_RiskUwriter.UpdateParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name

        'SqlDataSource_COR.SelectParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name


        If (Not IsPostBack) Then


        End If
    End Sub

    'Protected Sub callbackPanel_MoreInfoCommOut_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase) Handles callbackPanel_MoreInfoCommOut.Callback

    '    Dim _Risk As String = e.Parameter.ToString()

    '    If Not String.IsNullOrEmpty(_Risk) Then

    '        Using dc As New DataClasses_CPSExt()

    '            Dim _data = (From c In dc.V_RiskUwriters Where c.Risk.Equals(_Risk)).FirstOrDefault()

    '            With _data
    '                lbRisk.Text = .Risk
    '                lbUnderWriterName.Text = .Name
    '                lbCommIn.Text = String.Format("Brokerage:{0} , ORComm:{1} , AdminFee1:{2} , AdminFee2:{3}", .Brokerage_display, .ORCommissionPercent_display, .AdminFeeIn1_display, .AdminFeeIn2_display)


    '                Dim _overcomm = (From c In dc.V_AgentRiskComms Where c.Risk.Equals(_Risk) And c.HasOverComm = True).FirstOrDefault()
    '                If _overcomm IsNot Nothing Then
    '                    imgHasOverComm.Visible = True
    '                Else
    '                    imgHasOverComm.Visible = False
    '                End If


    '            End With
    '        End Using
    '    End If


    '    Session("Risk") = _Risk

    '    gridCommOut.DataBind()

    'End Sub

    Protected Sub callbackPanel_popupRiskUnderwriter_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase) Handles callbackPanel_popupRiskUnderwriter.Callback

        Dim _cmd = e.Parameter.ToString()

        If Not String.IsNullOrEmpty(_cmd) Then

            Dim _params = _cmd.Split("|")
            Select Case _params(0)

                Case "select_uw"
                    Dim _Underwriter As String = _params(1)
                    Session("Underwriter") = _Underwriter
                    gridCOR.DataBind()


                Case "new"
                    Session("Uwriter") = ""

                    Uwriter.DataBind()

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
                    'OffsetAdm1Flag.Checked = True
                    'OffsetAdm2Flag.Checked = True
                    Remark.Text = ""

                    'EffectiveDate.Value = Now()
                    'ExpiryDate.Value = Now.AddYears(10)

                    UWIsActive.Checked = True

            End Select


        End If





    End Sub

    Protected Sub cbAddInsurer_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbAddInsurer.Callback

        Dim _dc = New DataClasses_CPSExt()

        Dim _Commission = Convert.ToDouble(Brokerage.Value)
        Dim _CommissionCal = BrokerageCal.Value.ToString()

        If _CommissionCal.ToUpper() = "X" Then
            If _Commission > 18 Then
                e.Result = "Brokerage Over 18%"
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
                Dim dr = gridCOR.GetRowValuesByKeyValue(key, "Description")
                _risk.Add(key.ToString() & "|" & dr.ToString())
            End If
        Next key


        If _risk.Count > 0 Then
            Dim _RiskUwriter As New List(Of Register_RiskUwriter)
            For Each item_risk In _risk
                Dim newRiskUwriter As New Register_RiskUwriter()
                With newRiskUwriter

                    Dim _RiskDesc = item_risk.Split("|")

                    .Risk = _RiskDesc(0)
                    .Description = _RiskDesc(1)
                    .Uwriter = Uwriter.Value

                    .Brokerage = Convert.ToDouble(Brokerage.Value)
                    .BrokerageCal = BrokerageCal.Value.ToString()

                    .ORCommissionPercent = Convert.ToDouble([OR].Value)
                    .ORInCal = ORCal.Value.ToString()


                    .OROutRate = 0
                    .AgentCommission = 0
                    .PremiumWarantydays = 0
                    .NewRenewaldays = 0
                    .NewNewBusdays = 0
                    .RenRenewaldays = 0
                    .RenNewBusdays = 0
                    .AutoCalculation = True

                    .AdminFeeIn1 = Convert.ToDouble(Admin1.Value)
                    .AdminFeeIn1Cal = Admin1Cal.Value.ToString()
                    .AdminFeeIn2 = Convert.ToDouble(Admin2.Value)
                    .AdminFeeIn2Cal = Admin2Cal.Value.ToString()

                    .ORCalFrom = CalFrom.Value.ToString()

                    .OffsetORFlag = OffsetORFlag.Checked
                    .OffsetAdm1Flag = OffsetORFlag.Checked
                    .OffsetAdm2Flag = OffsetORFlag.Checked
                    .Remark = Remark.Text

                    .CreationDate = DateTime.Now()
                    .CreationBy = HttpContext.Current.User.Identity.Name
                    .IsActive = UWIsActive.Checked

                End With
                _RiskUwriter.Add(newRiskUwriter)
            Next


            Using dc As New DataClasses_CPSExt()
                dc.Register_RiskUwriters.InsertAllOnSubmit(_RiskUwriter)
                dc.SubmitChanges()
            End Using

            e.Result = "success"
        Else
            e.Result = "No selecte Risk"
        End If




    End Sub

    Protected Sub gridRiskUwriter_HtmlRowCreated(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewTableRowEventArgs) Handles gridRiskUwriter.HtmlRowPrepared
        If e.RowType = GridViewRowType.Group Then
            'e.Row.BackColor = backColor
            e.Row.Font.Bold = True
        End If

        If e.RowType <> DevExpress.Web.GridViewRowType.Data Then
            Return
        End If
        Dim _ID = e.KeyValue().ToString()
        Dim _ApproveDate = e.GetValue("ApproveDate").ToString()
        Dim _RequestDate = e.GetValue("RequestDate").ToString()
        Dim _CreationBy = e.GetValue("CreationBy").ToString()
        Dim RowEditable As ASPxButton = gridRiskUwriter.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "RowEditable")
        RowEditable.JSProperties.Add("cpVisibleIndex", e.VisibleIndex)
        If String.IsNullOrEmpty(_ApproveDate) And String.IsNullOrEmpty(_RequestDate) Then
            RowEditable.Visible = True
        Else
            RowEditable.Visible = False
        End If

        If _CreationBy.ToString().ToLower() <> HttpContext.Current.User.Identity.Name.ToString().ToLower() Then
            RowEditable.Visible = False
        End If

        Dim RowDeleteable As ASPxButton = gridRiskUwriter.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "RowDeleteable")
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


    Protected Sub gridRiskUwriter_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles gridRiskUwriter.RowUpdating
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

        If BrokerageCal.SelectedItem.Value = "x" Then
            If Brokerage.Value > 18 Then
                Throw New Exception("Brokerage over 18%")
            End If
        End If
        If ORInCal.SelectedItem.Value = "x" Then
            If ORCommissionPercent.Value > 99 Then
                Throw New Exception("ORComm over 100%")
            End If
        End If
        If AdminFeeIn1Cal.SelectedItem.Value = "x" Then
            If AdminFeeIn1.Value > 99 Then
                Throw New Exception("AdminFee1 over 100%")
            End If
        End If
        If AdminFeeIn2Cal.SelectedItem.Value = "x" Then
            If AdminFeeIn2.Value > 99 Then
                Throw New Exception("AdminFeeIn2 over 100%")
            End If
        End If


        e.NewValues("Brokerage") = Brokerage.Value
        e.NewValues("BrokerageCal") = BrokerageCal.SelectedItem.Value
        e.NewValues("ORCommissionPercent") = ORCommissionPercent.Value
        e.NewValues("ORInCal") = ORInCal.SelectedItem.Value
        e.NewValues("AdminFeeIn1") = AdminFeeIn1.Value
        e.NewValues("AdminFeeIn1Cal") = AdminFeeIn1Cal.SelectedItem.Value
        e.NewValues("AdminFeeIn2") = AdminFeeIn2.Value
        e.NewValues("AdminFeeIn2Cal") = AdminFeeIn2Cal.SelectedItem.Value
        e.NewValues("ORCalFrom") = ORCalFrom.SelectedItem.Value
        e.NewValues("OffsetORFlag") = OffsetORFlag.Checked
        e.NewValues("OffsetAdm1Flag") = OffsetORFlag.Checked
        e.NewValues("OffsetAdm2Flag") = OffsetORFlag.Checked
        e.NewValues("Remark") = Remark.Text
        e.NewValues("IsActive") = IsActive.Checked


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
            Dim _user_from = (From c In dc_portal.V_LWT_USERs Where c.sAMAccountName.Equals(HttpContext.Current.User.Identity.Name)).FirstOrDefault()
            strSubject = _mailNotification.MailSubject.Replace("{name}", _user_from.displayName).Replace("{date}", DateTime.Now.ToString())
            strMessage.AppendLine(Server.HtmlDecode(_mailNotification.MailBody.Replace("{fromname}", _user_from.displayName)))
            strMailFrom = _user_from.mail
            strMailCC = _mailNotification.MailCC & ";" & _user_from.mail
            strMailTo = _mailNotification.MailTo

            'strMailFrom = _user.mail
            'strMailCC = _mailNotification.MailCC & ";" & _user.mail
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
