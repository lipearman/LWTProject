Imports System.Data
Imports System.Web.Security
Imports Portal.Components

Imports DevExpress.Web
Imports System.IO
Imports System.Drawing
Imports System.Net.Mail
Imports Spire.Xls
Partial Class Modules_ucDevxClassOfRiskRequest
    Inherits PortalModuleControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        SqlDataSource_COR.SelectParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name


        If (Not IsPostBack) Then

        End If
    End Sub

    Protected Sub gridCOR_HtmlRowCreated(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewTableRowEventArgs) Handles gridCOR.HtmlRowPrepared
        'ColorGroupRow(e, Color.Aquamarine, 0)
        If e.RowType = GridViewRowType.Group Then
            e.Row.Font.Bold = True
        End If
    End Sub

    Private Sub ColorGroupRow(ByVal e As ASPxGridViewTableRowEventArgs, ByVal backColor As Color, ByVal rowIndex As Integer)
        If e.RowType = GridViewRowType.Group AndAlso gridCOR.GetRowLevel(e.VisibleIndex) = rowIndex Then
            e.Row.BackColor = backColor
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

        Dim _Risk As String = e.Parameter.ToString()

        Using dc As New DataClasses_CPSExt()

            Dim _data_cor = (From c In dc.V_ClassOfRisks Where c.Risk.Equals(_Risk)).FirstOrDefault()
            lbRisk.Text = _data_cor.Risk
            With _data_cor
                lbDepartment.Text = .Department
                RiskShortDesc.Text = .RiskShortDesc
                lbDescription.Text = .Description
                RiskGroup1.Value = .RiskGroupI
                RiskGroup2.Value = .RiskGroupII
                RiskGovernment.Value = .RiskGovernment
                lbInsuranceLine.Text = .InsuranceLine
                lbStatus.ImageUrl = String.Format("~/images/{0}.gif", .status)

                lbCommIn_Request.Text = .CommIn_Request
                lbCommOut_Request.Text = .CommOut_Request



            End With
        End Using
        Session("Risk") = _Risk
        gridCommIn.DataBind()
        gridCommOut.DataBind()

    End Sub


    Protected Sub cbSaveCOR_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbSaveCOR.Callback

        Using dc As New DataClasses_CPSExt()
            Dim _Risk As String = e.Parameter.ToString()
            Dim _data_cor = (From c In dc.Register_ClassOfRisks Where c.Risk.Equals(_Risk)).FirstOrDefault()

            With _data_cor
                .RiskGroupI = RiskGroup1.Value
                .RiskGroupII = RiskGroup2.Value
                .RiskShortDesc = RiskShortDesc.Text
                .RiskGovernment = RiskGovernment.Value
            End With


            dc.SubmitChanges()

            e.Result = "success"
        End Using



    End Sub

    Protected Sub cbRejectCOR_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbRejectCOR.Callback

        Using dc As New DataClasses_CPSExt()
            Dim _Risk As String = e.Parameter.ToString()
            Dim _data_cor = (From c In dc.Register_ClassOfRisks Where c.Risk.Equals(_Risk)).FirstOrDefault()
            _data_cor.RequestBy = Nothing
            _data_cor.RequestDate = Nothing

            Dim _data_uwrisk = (From c In dc.Register_RiskUwriters Where c.Risk.Equals(_Risk) And c.ApproveDate Is Nothing And c.RequestDate IsNot Nothing).ToList()

            For Each _data_uwrisk_item In _data_uwrisk
                _data_uwrisk_item.RequestBy = Nothing
                _data_uwrisk_item.RequestDate = Nothing
            Next

            Dim _data_agrisk = (From c In dc.Register_AgentRiskComms Where c.Risk.Equals(_Risk) And c.ApproveDate Is Nothing And c.RequestDate IsNot Nothing).ToList()
            For Each item In _data_agrisk
                item.RequestBy = Nothing
                item.RequestDate = Nothing
            Next




            If _data_uwrisk.Count > 0 Or _data_agrisk.Count > 0 Then

                SendMailReject(_Risk)

                dc.SubmitChanges()


                e.Result = "success"

            Else
                e.Result = "No Commission In/Out"
            End If

        End Using

    End Sub

    Protected Sub SendMailReject(ByVal _Risk As String)
        Dim _filename1 As String = String.Format("CommIn-{0}.xls", Guid.NewGuid().ToString())
        Dim _path1 = String.Format("{0}/{1}", Server.MapPath("~/saved_files"), _filename1)
        Dim _filename2 As String = String.Format("CommOut-{0}.xls", Guid.NewGuid().ToString())
        Dim _path2 = String.Format("{0}/{1}", Server.MapPath("~/saved_files"), _filename2)

        Using dc As New DataClasses_CPSExt()
            Dim _data_cor = (From c In dc.Register_ClassOfRisks Where c.Risk.Equals(_Risk)).FirstOrDefault()


            Dim _data_uwrisk = (From c In dc.V_RiskUwriters Where c.Risk.Equals(_Risk) And c.RequestDate IsNot Nothing And c.ApproveDate Is Nothing).ToList()
            If _data_uwrisk.Count > 0 Then

                '================================== Comm In ========================================
                'initialize a new instance of Workbook
                Dim workbook1 As New Workbook()
                'open a template Excel file
                workbook1.LoadFromFile(Server.MapPath("~\Template\ClassOfRiskCommIn_Register.xlsx"))
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



            Dim _data_agrisk = (From c In dc.V_AgentRiskComms Where c.Risk.Equals(_Risk) And c.RequestDate IsNot Nothing And c.ApproveDate Is Nothing).ToList()
            If _data_agrisk.Count > 0 Then
                '================================== Comm Out ========================================
                'initialize a new instance of Workbook
                Dim workbook2 As New Workbook()
                'open a template Excel file
                workbook2.LoadFromFile(Server.MapPath("~\Template\ClassOfRiskCommOut_Register.xlsx"))
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
            Dim _mailNotification = (From c In dc_portal.MailNotifications Where c.Code.Equals("N0018")).FirstOrDefault()

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
                att1.Name = String.Format("Reject_CommIn_By_{0}_{1}.xls", HttpContext.Current.User.Identity.Name, DateTime.Now.ToString("yyyyMMddHHmmss"))
                msg.Attachments.Add(att1)

            End If
            If File.Exists(_path2) Then
                Dim att2 = New Attachment(_path2)
                att2.Name = String.Format("Reject_CommOut_By_{0}_{1}.xls", HttpContext.Current.User.Identity.Name, DateTime.Now.ToString("yyyyMMddHHmmss"))
                msg.Attachments.Add(att2)
            End If

            MySmtpClient.Send(msg)
        End Using






    End Sub





    Protected Sub cbApproveCOR_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbApproveCOR.Callback

        Using dc As New DataClasses_CPSExt()
            Dim _Risk As String = e.Parameter.ToString()
            Dim _data_cor = (From c In dc.Register_ClassOfRisks Where c.Risk.Equals(_Risk)).FirstOrDefault()


            With _data_cor
                .RiskGroupI = RiskGroup1.Value
                .RiskGroupII = RiskGroup2.Value
                .RiskShortDesc = RiskShortDesc.Text
                .RiskGovernment = RiskGovernment.Value

                .ApproveBy = HttpContext.Current.User.Identity.Name
                .ApproveDate = Now()
            End With



            Dim _data_uwrisk = (From c In dc.Register_RiskUwriters Where c.Risk.Equals(_Risk) And c.ApproveDate Is Nothing And c.RequestDate IsNot Nothing).ToList()
            For Each item_ur In _data_uwrisk
                item_ur.ApproveBy = HttpContext.Current.User.Identity.Name
                item_ur.ApproveDate = Now()


            Next

            Dim _data_agrisk = (From c In dc.Register_AgentRiskComms Where c.Risk.Equals(_Risk) And c.ApproveDate Is Nothing And c.RequestDate IsNot Nothing).ToList()
            For Each item_ar In _data_agrisk
                item_ar.ApproveBy = HttpContext.Current.User.Identity.Name
                item_ar.ApproveDate = Now()
            Next

            '====================== Start Create New Class Of Risk =======================
            If _data_uwrisk.Count > 0 Or _data_agrisk.Count > 0 Then
                SendMailApprove(_Risk)

                Using dc1 As New DataClasses_SIBISExt()
                    '1. Insert New Class Of Risk
                    Dim _data_class = (From c In dc1.ClassOfRisks Where c.Risk.Trim().Equals(_Risk.Trim())).FirstOrDefault()
                    If _data_class Is Nothing Then
                        dc1.ClassOfRisks.InsertOnSubmit(New ClassOfRisk With {.Risk = _data_cor.Risk _
                                                                                                  , .Department = _data_cor.Department _
                                                                                                  , .Description = _data_cor.Description _
                                                                                                  , .RiskGovernment = _data_cor.RiskGovernment _
                                                                                                  , .RiskShortDesc = _data_cor.RiskShortDesc _
                                                                                                  , .RiskGroupI = _data_cor.RiskGroupI _
                                                                                                  , .RiskGroupII = _data_cor.RiskGroupII _
                                                                                                  })
                    End If

                    '2. Insert new Register.RiskUwriter  
                    If _data_uwrisk.Count > 0 Then
                        Dim _RiskUwritersLst As New List(Of RiskUwriter)

                        For Each item_ur In _data_uwrisk

                            _RiskUwritersLst.Add(New RiskUwriter With {.Risk = item_ur.Risk _
                                                                               , .Uwriter = item_ur.Uwriter _
                                                                                , .Description = item_ur.Description _
                                                                                , .Brokerage = item_ur.Brokerage _
                                                                                , .AgentCommission = 0 _
                                                                                , .PremiumWarantydays = 0 _
                                                                                , .NewRenewaldays = 0 _
                                                                                , .NewNewBusdays = 0 _
                                                                                , .RenRenewaldays = 0 _
                                                                                , .RenNewBusdays = 0 _
                                                                                , .AutoCalculation = item_ur.AutoCalculation _
                                                                                , .ORCommissionPercent = item_ur.ORCommissionPercent _
                                                                                , .OROutRate = 0 _
                                                                                , .AdminFeeIn1 = item_ur.AdminFeeIn1 _
                                                                                , .AdminFeeIn1Cal = item_ur.AdminFeeIn1Cal _
                                                                                , .AdminFeeIn2 = item_ur.AdminFeeIn2 _
                                                                                , .AdminFeeIn2Cal = item_ur.AdminFeeIn2Cal _
                                                                                , .ORCalFrom = item_ur.ORCalFrom _
                                                                                , .OffsetORFlag = item_ur.OffsetORFlag _
                                                                                , .OffsetAdm1Flag = item_ur.OffsetAdm1Flag _
                                                                                , .OffsetAdm2Flag = item_ur.OffsetAdm2Flag _
                                                                                , .BrokerageCal = item_ur.BrokerageCal _
                                                                                , .ORInCal = item_ur.ORInCal _
                                                                                 })

                        Next
                        dc1.RiskUwriters.InsertAllOnSubmit(_RiskUwritersLst)

                    End If

                    '3. Insert new Register.AgentRiskComm
                    If _data_agrisk.Count > 0 Then
                        Dim _AgentRiskCommLst As New List(Of AgentRiskComm)

                        For Each item_ar In _data_agrisk
                            _AgentRiskCommLst.Add(New AgentRiskComm With {.Risk = item_ar.Risk _
                                                                                    , .Agent = item_ar.Agent _
                                                                                    , .CommissionOut = item_ar.CommissionOut _
                                                                                    , .OROut = item_ar.OROut _
                                                                                    , .OROutCalFrom = item_ar.OROutCalFrom _
                                                                                    , .AdminOut1 = item_ar.AdminOut1 _
                                                                                    , .AdminOut1Cal = item_ar.AdminOut1Cal _
                                                                                    , .AdminOut2 = item_ar.AdminOut2 _
                                                                                    , .AdminOut2Cal = item_ar.AdminOut2Cal _
                                                                                    , .EntryBy = item_ar.EntryBy _
                                                                                    , .EntryDate = item_ar.EntryDate _
                                                                                    , .CommOutCal = item_ar.CommOutCal _
                                                                                    , .OROutCal = item_ar.OROutCal _
                                                                                })


                        Next

                        dc1.AgentRiskComms.InsertAllOnSubmit(_AgentRiskCommLst)
                    End If
                    

                    dc1.SubmitChanges()
                End Using

                '====================== End Create New Class Of Risk =======================

                dc.SubmitChanges()


                e.Result = "success"

            Else
                e.Result = "No Commission In/Out"
            End If

        End Using

    End Sub




    Protected Sub SendMailApprove(ByVal _Risk As String)
        Dim _filename1 As String = String.Format("CommIn-{0}.xls", Guid.NewGuid().ToString())
        Dim _path1 = String.Format("{0}/{1}", Server.MapPath("~/saved_files"), _filename1)
        Dim _filename2 As String = String.Format("CommOut-{0}.xls", Guid.NewGuid().ToString())
        Dim _path2 = String.Format("{0}/{1}", Server.MapPath("~/saved_files"), _filename2)

        Using dc As New DataClasses_CPSExt()
            Dim _data_cor = (From c In dc.Register_ClassOfRisks Where c.Risk.Equals(_Risk)).FirstOrDefault()


            Dim _data_uwrisk = (From c In dc.V_RiskUwriters Where c.Risk.Equals(_Risk) And c.RequestDate IsNot Nothing And c.ApproveDate Is Nothing).ToList()
            If _data_uwrisk.Count > 0 Then

                '================================== Comm In ========================================
                'initialize a new instance of Workbook
                Dim workbook1 As New Workbook()
                'open a template Excel file
                workbook1.LoadFromFile(Server.MapPath("~\Template\ClassOfRiskCommIn_Register.xlsx"))
                'get the first sheet 
                Dim worksheet1 As Worksheet = workbook1.Worksheets(0)
                '===================== Header ==============================
              
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



            Dim _data_agrisk = (From c In dc.V_AgentRiskComms Where c.Risk.Equals(_Risk) And c.RequestDate IsNot Nothing And c.ApproveDate Is Nothing).ToList()
            If _data_agrisk.Count > 0 Then
                '================================== Comm Out ========================================
                'initialize a new instance of Workbook
                Dim workbook2 As New Workbook()
                'open a template Excel file
                workbook2.LoadFromFile(Server.MapPath("~\Template\ClassOfRiskCommOut_Register.xlsx"))
                'get the first sheet 
                Dim worksheet2 As Worksheet = workbook2.Worksheets(0)
                '===================== Header ==============================
             
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
            Dim _mailNotification = (From c In dc_portal.MailNotifications Where c.Code.Equals("N0017")).FirstOrDefault()
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
                att1.Name = String.Format("Approved_CommIn_By_{0}_{1}.xls", HttpContext.Current.User.Identity.Name, DateTime.Now.ToString("yyyyMMddHHmmss"))
                msg.Attachments.Add(att1)

            End If
            If File.Exists(_path2) Then
                Dim att2 = New Attachment(_path2)
                att2.Name = String.Format("Approved_CommOut_By_{0}_{1}.xls", HttpContext.Current.User.Identity.Name, DateTime.Now.ToString("yyyyMMddHHmmss"))
                msg.Attachments.Add(att2)
            End If

            MySmtpClient.Send(msg)
        End Using






    End Sub


End Class
