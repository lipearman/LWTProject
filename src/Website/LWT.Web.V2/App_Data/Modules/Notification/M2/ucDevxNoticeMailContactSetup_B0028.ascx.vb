Imports System.Data
Imports System.Web.Security
Imports Portal.Components

Imports NPOI.HSSF.UserModel
Imports NPOI.HPSF
Imports NPOI.SS.UserModel
Imports NPOI.SS.Util
Imports NPOI.HSSF.Util
Imports System.Drawing
Imports iTextSharp.text.pdf

Imports DevExpress.Web
Imports DevExpress.Web.ASPxHtmlEditor
Imports System.Net.Mail

Partial Class Modules_ucDevxNoticeMailContactSetup_B0028
    Inherits PortalModuleControl
    Private NoticeCode As String = "B0028"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If (Not IsPostBack) Then
            SqlDataSource_NoticeMailContact.SelectParameters("NoticeCode").DefaultValue = NoticeCode


            SqlDataSource_NoticeMailContact.UpdateParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name
            SqlDataSource_NoticeMailContact.InsertParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name


            'SqlDataSource_UWLookup.SelectParameters("NoticeCode").DefaultValue = NoticeCode

            MailBody.Toolbars.Add(CreateDemoCustomToolbar("CustomToolbar"))
            MailBody.Toolbars.Add(HtmlEditorToolbar.CreateStandardToolbar1())
            MailBody.Toolbars("StandardToolbar1").Visible = False
            MailBody.Toolbars.Add(HtmlEditorToolbar.CreateStandardToolbar2())
            MailBody.Toolbars("StandardToolbar2").Visible = False

            LoadEmailTemplate()


        End If
    End Sub

    Private Sub LoadEmailTemplate()
        Using _dc As New DataClasses_PortalDataContextExt()

            Dim data = (From c In _dc.MailNotifications Where c.Code.Equals(NoticeCode)).FirstOrDefault()
            With data
                MailSubject.Text = .MailSubject
                MailFrom.Text = .MailFrom
                'MailTo.Text = .MailTo
                MailCC.Text = .MailCC
                'MailBcc.Text = .MailBcc
                MailBody.Html = Server.HtmlDecode(.MailBody)

            End With


        End Using
    End Sub
    Protected Function CreateDemoCustomToolbar(ByVal name As String) As HtmlEditorToolbar
        Return New HtmlEditorToolbar(name, New ToolbarParagraphFormattingEdit() _
                                     , New ToolbarFontNameEdit() _
                                     , New ToolbarFontSizeEdit() _
                                     , New ToolbarFontColorButton() _
                                     , New ToolbarBoldButton() _
                                     , New ToolbarItalicButton() _
                                     , New ToolbarUnderlineButton() _
                                     , New ToolbarJustifyLeftButton() _
                                     , New ToolbarJustifyCenterButton() _
                                     , New ToolbarJustifyRightButton() _
                                     ).CreateDefaultItems()



        '<dx:ToolbarParagraphFormattingEdit ></dx:ToolbarParagraphFormattingEdit>
        '<dx:ToolbarFontNameEdit ></dx:ToolbarFontNameEdit>
        '<dx:ToolbarFontSizeEdit ></dx:ToolbarFontSizeEdit>
        '<dx:ToolbarFontColorButton ></dx:ToolbarFontColorButton>
        '<dx:ToolbarBoldButton ></dx:ToolbarBoldButton>
        '<dx:ToolbarItalicButton></dx:ToolbarItalicButton>
        '<dx:ToolbarUnderlineButton></dx:ToolbarUnderlineButton>
        '<dx:ToolbarJustifyLeftButton></dx:ToolbarJustifyLeftButton>
        '<dx:ToolbarJustifyCenterButton></dx:ToolbarJustifyCenterButton>
        '<dx:ToolbarJustifyRightButton></dx:ToolbarJustifyRightButton>
    End Function

    Protected Sub cbSaveAddNew_Callback(ByVal source As Object, ByVal e As CallbackEventArgs) Handles cbSaveAddNew.Callback

        Using _dc As New DataClasses_CPSExt()
            Dim newData = New tblNoticeMailContact
            With newData
                .Code = newCode.Text ' GridUWLookup.Value.Trim()
                .Name = newName.Text 'GridUWLookup.Text.Trim().Replace(.Code + " -", "").Trim()
                .MailTo = newMailTo.Text
                .MailCC = newMailCC.Text
                .CreationDate = Now()
                .CreationBy = HttpContext.Current.User.Identity.Name
                .ContactName = newContactName.Text
                .NoticeCode = NoticeCode
            End With

            _dc.tblNoticeMailContacts.InsertOnSubmit(newData)
            _dc.SubmitChanges()
        End Using

        e.Result = "success"

    End Sub

    Protected Sub cbSaveEmail_Callback(ByVal source As Object, ByVal e As CallbackEventArgs) Handles cbSaveEmail.Callback

        Using _dc As New DataClasses_PortalDataContextExt()

            Dim data = (From c In _dc.MailNotifications Where c.Code.Equals(NoticeCode)).FirstOrDefault()
            With data
                .MailSubject = MailSubject.Text
                .MailFrom = MailFrom.Text
                '.MailTo = MailTo.Text
                .MailCC = MailCC.Text
                '.MailBcc = MailBcc.Text
                .MailBody = Server.HtmlEncode(MailBody.Html)
            End With

            _dc.SubmitChanges()
        End Using

        e.Result = "success"

    End Sub

    Protected Sub cbTestSendEmail_Callback(ByVal source As Object, ByVal e As CallbackEventArgs) Handles cbTestSendEmail.Callback

        testsendmail()

        e.Result = "success"

    End Sub



    Private Sub testsendmail()
        Using _dc As New DataClasses_PortalDataContextExt()

            Dim strMailFrom As String = ""
            Dim strMailTo As String = ""
            Dim strSubject As String = ""
            Dim strMessage As New StringBuilder()


            Dim _displayName As String = ""
            Dim _title As String = ""
            Dim _department As String = ""
            Dim _company As String = ""
            Dim _streetAddress As String = ""
            Dim _telephoneNumber As String = ""
            Dim _facsimileTelephoneNumber As String = ""
            Dim _mail As String = ""

            Try
                Using dc_portal = New DataClasses_PortalDataContextExt
                    Dim _user = (From c In dc_portal.v_ads_actives Where c.sAMAccountName.Equals(HttpContext.Current.User.Identity.Name)).FirstOrDefault()

                    With _user
                        _displayName = .displayName
                        _title = .title
                        _department = .department
                        _company = .company
                        _streetAddress = .streetaddress
                        _telephoneNumber = .telephoneNumber
                        _facsimileTelephoneNumber = .facsimileTelephoneNumber
                        _mail = .mail
                    End With

                End Using


                Dim data = (From c In _dc.Portal_Users Where c.UserName.Equals(HttpContext.Current.User.Identity.Name)).FirstOrDefault()
                Dim _mailNotification = (From c In _dc.MailNotifications Where c.Code.Equals(NoticeCode)).FirstOrDefault()
                '================== Subject ================
                strSubject = _mailNotification.MailSubject
                '================== mail from ================
                strMailFrom = _mail
                '================== mail to ================
                strMailTo = _mail

                Dim MySmtpClient As New System.Net.Mail.SmtpClient(ConfigurationSettings.AppSettings("smtp"))
                'MySmtpClient.Credentials = New System.Net.NetworkCredential("Your-username", "Your-password")
                'Dim msg As New System.Net.Mail.MailMessage(strMailFrom, strMailTo, strSubject, strMessage.ToString())
                Dim msg As New System.Net.Mail.MailMessage()

                '================== Setting ================
                msg.BodyEncoding = Encoding.UTF8
                msg.IsBodyHtml = True
                msg.Priority = Net.Mail.MailPriority.High
                msg.Subject = strSubject 'Subject
                msg.Body = Nothing 'Body
                msg.From = New MailAddress(strMailFrom) 'Mail From
                Dim _MailTo = strMailTo.Split(";") 'Mail To
                For Each item In _MailTo
                    If Not String.IsNullOrEmpty(item.Trim()) Then msg.To.Add(item)
                Next


                '====================== Add Body=====================================
                Dim _MailBody As String = Server.HtmlDecode(_mailNotification.MailBody)
                '_MailBody = _MailBody.Replace("{contactname}", _contactname)
                '_MailBody = _MailBody.Replace("{groupname}", _groupname)
                '_MailBody = _MailBody.Replace("{month}", _month)

                _MailBody = _MailBody.Replace("{displayName}", _displayName)
                _MailBody = _MailBody.Replace("{title}", _title)
                _MailBody = _MailBody.Replace("{department}", _department)
                _MailBody = _MailBody.Replace("{company}", _company)
                _MailBody = _MailBody.Replace("{streetAddress}", _streetAddress)
                _MailBody = _MailBody.Replace("{telephoneNumber}", _telephoneNumber)
                _MailBody = _MailBody.Replace("{facsimileTelephoneNumber}", _facsimileTelephoneNumber)
                _MailBody = _MailBody.Replace("{mail}", _mail)
                strMessage.AppendLine(_MailBody)

                '====================== Add Image=====================================
                Dim bodyHTML As String = "<html><body>" & strMessage.ToString() & "<span style='font-size:16.0pt;font-family:Angsana New,serif;color:green'><img src='cid:LOGO_IMAGE2' alt='Logo' /><i>Please consider the environment before printing this e-mail.</i></span></body></html>"
                Dim alternateView As AlternateView = alternateView.CreateAlternateViewFromString(bodyHTML, Nothing, "text/html")

                'Dim path_to_the_image_file1 As String = String.Format("{0}\{1}", Server.MapPath("~/images"), "image02.jpg")
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



                MySmtpClient.Send(msg)
            Catch ex As Exception
                Throw ex
            End Try



        End Using
    End Sub
    Protected Sub btnExportData_Click(sender As Object, e As EventArgs) Handles btnExportData.Click
        GridExporter.WriteXlsxToResponse(System.Guid.NewGuid().ToString(), False, New DevExpress.XtraPrinting.XlsxExportOptionsEx With {.ExportType = DevExpress.Export.ExportType.WYSIWYG})
    End Sub
End Class
