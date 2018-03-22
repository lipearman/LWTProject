Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports FineUI
Imports System.IO

Imports NPOI.HSSF.UserModel
Imports NPOI.HPSF
Imports NPOI.SS.UserModel
Imports NPOI.SS.Util
Imports NPOI.HSSF.Util
Imports System.Net.Mail


Partial Class Modules_ucDevxNoticeHeaderSetup_B0027
    Inherits PortalModuleControl
    Dim _NoticeCode As String = "B0027"
    '*******************************************************
    '
    ' The Page_Load server event handler on this user control is used
    ' to populate the current site settings from the config system
    '
    '*******************************************************
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Page.IsPostBack = False Then
            Session("ImportMNT2CSVs") = Nothing

        End If
    End Sub
    Private Sub BindData()
        Dim column As FineUI.GridColumn = Grid1.FindColumn(Grid1.SortField)
        BindGridWithSort(column.SortField, Grid1.SortDirection)
    End Sub
    Private Sub BindGridWithSort(ByVal sortField As String, ByVal sortDirection As String)
        If Session("ImportMNT2CSVs") Is Nothing Then
            Dim _appdata As New List(Of MyContext.ImportMNT2CSV)
            Session("ImportMNT2CSVs") = _appdata
        End If

        Dim _data As List(Of MyContext.ImportMNT2CSV) = Session("ImportMNT2CSVs")
        Dim table As DataTable = MyUtils.EQToDataTable(_data)
        Dim view1 As DataView = table.DefaultView
        If _data.Count > 0 Then
            view1.Sort = String.Format("{0} {1}", sortField, sortDirection)
        End If
        Grid1.DataSource = view1
        Grid1.DataBind()
    End Sub

    Protected Sub grid1_Sort(ByVal sender As Object, ByVal e As FineUI.GridSortEventArgs) Handles Grid1.Sort
        BindGridWithSort(e.SortField, e.SortDirection)
    End Sub
    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        PageContext.Refresh()
    End Sub


    Protected Sub Grid1_PageIndexChange(ByVal sender As Object, ByVal e As FineUI.GridPageEventArgs) Handles Grid1.PageIndexChange
        Grid1.PageIndex = e.NewPageIndex
        BindData()
    End Sub



    Protected Sub btnExportForm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportForm.Click

        Dim fileName = String.Format("{0}.xls", System.Guid.NewGuid.ToString())
        Dim filePath = String.Format("{0}\{1}", Server.MapPath("~/upload"), fileName)
        Dim _fields = "Invoice Tax NO,Invoice Tax date,Sum of Gross Premium,Sum of Stamp,Sum of VAT,Sum of discount,Sum of เบี้ยแคมเปญ,Flag".Split(",")
        Dim hssfworkbook As New HSSFWorkbook()
        Dim _data As List(Of MyContext.ImportMNT2CSV) = Session("ImportMNT2CSVs")
        Dim _Insurer = (From c In _data _
                     Group By InsuranceCode = c.InsuranceCode, Memo = c.Memo
                     Into g = Group _
                     Select InsuranceCode, Memo _
               ).ToList()
        For Each InsuranceCode In _Insurer

            Dim _Model = (From c In _data _
                      Where c.InsuranceCode.Equals(InsuranceCode.InsuranceCode) _
                      And c.Memo.Equals(InsuranceCode.Memo) _
                 Group By ModelCode = c.Model_Code
                 Into g = Group _
                 Select ModelCode _
           ).ToList()


            For Each ModelCode In _Model
                Dim _items = (From c In _data _
                Where c.InsuranceCode.Equals(InsuranceCode.InsuranceCode) _
                   And c.Memo.Equals(InsuranceCode.Memo) _
                   And c.Model_Code.Equals(ModelCode)).ToList()
                If _items.Count > 0 Then
                    Dim sheetname As String = String.Format("{0}_{1}_{2}", InsuranceCode.InsuranceCode, InsuranceCode.Memo, ModelCode)

                    '====================== data new sheet ====================
                    Dim sheet1 As ISheet = hssfworkbook.CreateSheet(sheetname)
                    '================== create column ===================
                    Dim frow As IRow = sheet1.CreateRow(0)
                    For i = 0 To _fields.Count - 1
                        Dim cell = frow.CreateCell(i)
                        cell.SetCellValue(_fields(i))

                        sheet1.SetColumnWidth(i, _fields(i).Length * 400)
                    Next

                    Dim iRow As Integer = 1


                    If InsuranceCode.InsuranceCode.Equals("STY") Then
                        Dim _STY = (From c In _items
                                Group By Invoice_Tax_No = c.Invoice_Tax_No, Invoice_Tax_date = c.Invoice_Tax_date
                                Into g = Group _
                                Select Invoice_Tax_No, Invoice_Tax_date _
                          ).ToList()

                        For Each item In _STY
                            Dim row As IRow = sheet1.CreateRow(iRow)
                            row.CreateCell(0).SetCellValue(item.Invoice_Tax_No.ToString())
                            row.CreateCell(1).SetCellValue(item.Invoice_Tax_date.ToString())
                            row.CreateCell(2).SetCellValue(_items.Where(Function(p) p.Invoice_Tax_No.Equals(item.Invoice_Tax_No) And p.Invoice_Tax_date.Equals(item.Invoice_Tax_date)).Sum(Function(p) p.Gross_Premium))
                            row.CreateCell(3).SetCellValue(_items.Where(Function(p) p.Invoice_Tax_No.Equals(item.Invoice_Tax_No) And p.Invoice_Tax_date.Equals(item.Invoice_Tax_date)).Sum(Function(p) p.Stamp))
                            row.CreateCell(4).SetCellValue(_items.Where(Function(p) p.Invoice_Tax_No.Equals(item.Invoice_Tax_No) And p.Invoice_Tax_date.Equals(item.Invoice_Tax_date)).Sum(Function(p) p.VAT))
                            row.CreateCell(5).SetCellValue(_items.Where(Function(p) p.Invoice_Tax_No.Equals(item.Invoice_Tax_No) And p.Invoice_Tax_date.Equals(item.Invoice_Tax_date)).Sum(Function(p) p.Discount))
                            row.CreateCell(6).SetCellValue(_items.Where(Function(p) p.Invoice_Tax_No.Equals(item.Invoice_Tax_No) And p.Invoice_Tax_date.Equals(item.Invoice_Tax_date)).Sum(Function(p) p.CampaignPremium))
                            row.CreateCell(7).SetCellValue("0000")
                            iRow += 1
                        Next
                    Else
                        For Each item In _items
                            Dim row As IRow = sheet1.CreateRow(iRow)
                            row.CreateCell(0).SetCellValue(item.Invoice_Tax_No.ToString())
                            row.CreateCell(1).SetCellValue(item.Invoice_Tax_date.ToString())
                            row.CreateCell(2).SetCellValue(item.Gross_Premium.ToString())
                            row.CreateCell(3).SetCellValue(item.Stamp.ToString())
                            row.CreateCell(4).SetCellValue(item.VAT.ToString())
                            row.CreateCell(5).SetCellValue(item.Discount.ToString())
                            row.CreateCell(6).SetCellValue(item.CampaignPremium.ToString())
                            row.CreateCell(7).SetCellValue(item.Flag.ToString())
                            iRow += 1
                        Next
                    End If



                End If
            Next




        Next

        Dim fs As New FileStream(filePath, FileMode.Create)
        hssfworkbook.Write(fs)
        fs.Close()




        Response.ClearContent()
        Response.AddHeader("content-disposition", "attachment; filename=" & fileName)
        Response.ContentType = "application/octet-stream"
        Response.WriteFile(filePath)
        Response.End()

    End Sub

    Protected Sub btnImportForm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImportForm.Click
        'txtData.Text = ""
        WindowImportApplicationForm.Hidden = False
        'BindTypeOfInsure()
    End Sub

    Protected Sub btnImportData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImportData.Click


        If Not fuAttatch.HasFile Then
            Alert.Show("กรุณาแนบไฟล์")
            Return
        Else
            If Not Right(fuAttatch.ShortFileName, 3).ToLower().StartsWith("xls") Then
                Alert.Show("แนบไฟล์เฉพาะ xls เท่านั้น")
                Return
            End If
        End If

        Dim fileName As String = fuAttatch.ShortFileName


        Using dc As New DataClasses_CPSExt

            Dim newxlsFile As String = String.Format("{0}\{1}", Server.MapPath("~/upload"), String.Format("{0}.xls", System.Guid.NewGuid.ToString()))
            If File.Exists(newxlsFile) Then
                My.Computer.FileSystem.DeleteFile(newxlsFile)
            End If
            fuAttatch.SaveAs(newxlsFile)

            ImportAppForm(newxlsFile)

        End Using


        WindowImportApplicationForm.Hidden = True



    End Sub
    Private Sub ImportAppForm(ByVal FilePath As String)
        Dim _ImportMNT2CSVs As New List(Of MyContext.ImportMNT2CSV)
        Using File = New FileStream(FilePath, FileMode.Open, FileAccess.Read)
            Dim HSSFWorkbook = New HSSFWorkbook(File)
            Dim sheet = HSSFWorkbook.GetSheetAt(0)
            Dim rows = sheet.GetRowEnumerator()
            rows.MoveNext()

            Dim i As Integer = 0

            Using dc As New DataClasses_NLTDBExt

                While rows.MoveNext()
                    i += 1
                    Dim row = DirectCast(rows.Current, HSSFRow)
                    Dim _A As String = row.GetCell(0).ToString()
                    Dim _B As String = row.GetCell(1).ToString()
                    Dim _C As String = row.GetCell(2).ToString()
                    'Dim _D As String = row.GetCell(3).ToString()
                    'Dim _E As String = row.GetCell(4).ToString()
                    Dim _F As String = row.GetCell(5).ToString()
                    Dim _G As String = DirectCast(row.GetCell(6), NPOI.HSSF.UserModel.HSSFCell).DateCellValue.ToString("dd/MM/yyyy")
                    'Dim _H As String = row.GetCell(7).ToString()
                    'Dim _I As String = row.GetCell(8).ToString()
                    'Dim _J As String = row.GetCell(9).ToString()
                    Dim _K As String = DirectCast(row.GetCell(10), NPOI.HSSF.UserModel.HSSFCell).NumericCellValue.ToString("#0.00")
                    Dim _L As String = DirectCast(row.GetCell(11), NPOI.HSSF.UserModel.HSSFCell).NumericCellValue
                    Dim _M As String = DirectCast(row.GetCell(12), NPOI.HSSF.UserModel.HSSFCell).NumericCellValue.ToString("#0.00")
                    'Dim _N As String = row.GetCell(13).ToString()
                    Dim _O As String = DirectCast(row.GetCell(14), NPOI.HSSF.UserModel.HSSFCell).NumericCellValue.ToString("#0.00")
                    Dim _P As String = DirectCast(row.GetCell(15), NPOI.HSSF.UserModel.HSSFCell).NumericCellValue
                    'Dim _Q As String = row.GetCell(16).ToString()
                    'Dim _R As String = row.GetCell(17).ToString()
                    'Dim _S As String = row.GetCell(18).ToString()
                    'Dim _T As String = row.GetCell(19).ToString()
                    'Dim _U As String = row.GetCell(20).ToString()
                    'Dim _V As String = row.GetCell(21).ToString()
                    'Dim _W As String = row.GetCell(22).ToString()
                    'Dim _X As String = row.GetCell(23).ToString()
                    'Dim _Y As String = row.GetCell(24).ToString()
                    'Dim _Z As String = row.GetCell(25).ToString()
                    'Dim _AA As String = row.GetCell(26).ToString()


                    Dim _ImportMNT2CSV_Data As New MyContext.ImportMNT2CSV()
                    With _ImportMNT2CSV_Data
                        .InsuranceCode = _A
                        .Model_Code = _B
                        .Memo = _C
                        .Invoice_Tax_No = _F
                        .Invoice_Tax_date = _G
                        .Gross_Premium = _K
                        .Stamp = _L
                        .VAT = _M
                        .Discount = _O
                        .CampaignPremium = _P
                        .Flag = IIf(_A.Equals("VIB"), "0001", "0000")
                    End With
                    _ImportMNT2CSVs.Add(_ImportMNT2CSV_Data)

                End While
            End Using

            If _ImportMNT2CSVs.Count > 0 Then
                Dim idx As Integer = 0

                Session("ImportMNT2CSVs") = _ImportMNT2CSVs

                WindowImportApplicationForm.Hidden = True
                BindData()

            Else
                Alert.Show("No Data", MessageBoxIcon.Warning)
            End If


        End Using

    End Sub
    'btnSendMail

    Protected Sub btnSendMail_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSendMail.Click
        Dim filePath = Server.MapPath("~/upload")

        Dim _fields = "Invoice Tax NO,Invoice Tax date,Sum of Gross Premium,Sum of Stamp,Sum of VAT,Sum of discount,Sum of เบี้ยแคมเปญ,Flag".Split(",")
        Dim _filesList As New List(Of String)
        Dim _filesSource As New List(Of String)

        Dim _data As List(Of MyContext.ImportMNT2CSV) = Session("ImportMNT2CSVs")
        Dim _Insurer = (From c In _data _
                     Group By InsuranceCode = c.InsuranceCode, Memo = c.Memo
                     Into g = Group _
                     Select InsuranceCode, Memo _
               ).ToList()
        For Each InsuranceCode In _Insurer
            Dim _Model = (From c In _data _
                          Where c.InsuranceCode.Equals(InsuranceCode.InsuranceCode) _
                          And c.Memo.Equals(InsuranceCode.Memo) _
                     Group By ModelCode = c.Model_Code
                     Into g = Group _
                     Select ModelCode _
               ).ToList()
            For Each ModelCode In _Model
                Dim _items = (From c In _data _
                Where c.InsuranceCode.Equals(InsuranceCode.InsuranceCode) _
                   And c.Memo.Equals(InsuranceCode.Memo) _
                   And c.Model_Code.Equals(ModelCode)).ToList()
                If _items.Count > 0 Then
                    Dim filename As String = String.Format("{0}_{1}_{2}.csv", InsuranceCode.InsuranceCode, InsuranceCode.Memo, ModelCode)
                    _filesList.Add(filename)

                    Dim _file_gui As String = System.Guid.NewGuid().ToString()
                    _filesSource.Add(_file_gui)

                    Using sw As New StreamWriter(String.Format("{0}\{1}", filePath, _file_gui), False)

                        If InsuranceCode.InsuranceCode.Equals("STY") Then
                            Dim _STY = (From c In _items
                                    Group By Invoice_Tax_No = c.Invoice_Tax_No, Invoice_Tax_date = c.Invoice_Tax_date
                                    Into g = Group _
                                    Select Invoice_Tax_No, Invoice_Tax_date _
                              ).ToList()

                            For Each item In _STY

                                Dim _Gross_Premium = _items.Where(Function(p) p.Invoice_Tax_No.Equals(item.Invoice_Tax_No) And p.Invoice_Tax_date.Equals(item.Invoice_Tax_date)).Sum(Function(p) Convert.ToDecimal(p.Gross_Premium)).ToString("###0.00")
                                Dim _Stamp = _items.Where(Function(p) p.Invoice_Tax_No.Equals(item.Invoice_Tax_No) And p.Invoice_Tax_date.Equals(item.Invoice_Tax_date)).Sum(Function(p) Convert.ToDecimal(p.Stamp)).ToString("###0.00")
                                Dim _VAT = _items.Where(Function(p) p.Invoice_Tax_No.Equals(item.Invoice_Tax_No) And p.Invoice_Tax_date.Equals(item.Invoice_Tax_date)).Sum(Function(p) Convert.ToDecimal(p.VAT)).ToString("###0.00")
                                Dim _Discount = _items.Where(Function(p) p.Invoice_Tax_No.Equals(item.Invoice_Tax_No) And p.Invoice_Tax_date.Equals(item.Invoice_Tax_date)).Sum(Function(p) Convert.ToDecimal(p.Discount)).ToString("###0.00")
                                Dim _CampaignPremium = _items.Where(Function(p) p.Invoice_Tax_No.Equals(item.Invoice_Tax_No) And p.Invoice_Tax_date.Equals(item.Invoice_Tax_date)).Sum(Function(p) Convert.ToDecimal(p.CampaignPremium)).ToString("###0.00")

                                sw.WriteLine(String.Format("{0},{1},{2},{3},{4},{5},{6},{7}" _
                                                           , item.Invoice_Tax_No.ToString() _
                                                           , item.Invoice_Tax_date.ToString() _
                                                           , _Gross_Premium _
                                                           , _Stamp _
                                                           , _VAT _
                                                           , _Discount _
                                                           , _CampaignPremium _
                                                           , "0000"))
                            Next

                        Else
                            For Each item In _items
                                sw.WriteLine(String.Format("{0},{1},{2},{3},{4},{5},{6},{7}", item.Invoice_Tax_No.ToString(), item.Invoice_Tax_date.ToString(), item.Gross_Premium.ToString(), item.Stamp.ToString(), item.VAT.ToString(), item.Discount.ToString(), item.CampaignPremium.ToString(), item.Flag.ToString()))
                            Next
                        End If




                    End Using
                End If
            Next
        Next


        'Send Mail
        If _filesList.Count > 0 Then

            Dim _displayName As String = ""
            Dim _title As String = ""
            Dim _department As String = ""
            Dim _company As String = ""
            Dim _streetAddress As String = ""
            Dim _telephoneNumber As String = ""
            Dim _facsimileTelephoneNumber As String = ""
            Dim _mail As String = ""

            Dim strMailFrom As String = ""
            Dim strMailCC As String = ""
            Dim strMailTo As String = ""
            Dim strSubject As String = ""
            Dim strMessage As New StringBuilder()

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


                Dim _mailNotification = (From c In dc_portal.MailNotifications Where c.Code.Equals(_NoticeCode)).FirstOrDefault()
                strSubject = _mailNotification.MailSubject
           

                'strMessage.AppendLine("<!DOCTYPE html><html><body>")
                Dim _MailBody As String = Server.HtmlDecode(_mailNotification.MailBody)

                _MailBody = _MailBody.Replace("{displayName}", _displayName)
                _MailBody = _MailBody.Replace("{title}", _title)
                _MailBody = _MailBody.Replace("{department}", _department)
                _MailBody = _MailBody.Replace("{company}", _company)
                _MailBody = _MailBody.Replace("{streetAddress}", _streetAddress)
                _MailBody = _MailBody.Replace("{telephoneNumber}", _telephoneNumber)
                _MailBody = _MailBody.Replace("{facsimileTelephoneNumber}", _facsimileTelephoneNumber)
                _MailBody = _MailBody.Replace("{mail}", _mail)

                strMessage.AppendLine(_MailBody)

                strMailFrom = _mail
                strMailTo = _mail
            End Using


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

            For i As Integer = 0 To _filesList.Count - 1 'Attatch File
                Dim att_data = New Attachment(String.Format("{0}\{1}", filePath, _filesSource(i)))
                att_data.Name = _filesList(i)
                msg.Attachments.Add(att_data)
            Next

            MySmtpClient.Send(msg)


            Alert.Show("Send")
        End If


    End Sub

End Class
