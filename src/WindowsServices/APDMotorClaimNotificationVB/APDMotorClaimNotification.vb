Imports System
Imports System.IO
Imports System.Threading
Imports System.Configuration

Imports System.Diagnostics
Imports System.Net.Mail
Imports System.Text
Imports System.Web
Imports System.Net
Imports NPOI.HSSF.UserModel
Imports NPOI.SS.UserModel
Imports NPOI.HSSF.Util

Public Class APDMotorClaimNotification
    Dim uploadpath As String = IO.Path.GetDirectoryName(Diagnostics.Process.GetCurrentProcess().MainModule.FileName) & "\uploadfiles"
    Dim logpath As String = IO.Path.GetDirectoryName(Diagnostics.Process.GetCurrentProcess().MainModule.FileName) & "\log"
    Dim imgpath As String = IO.Path.GetDirectoryName(Diagnostics.Process.GetCurrentProcess().MainModule.FileName) & "\images"



    Protected Overrides Sub OnStart(ByVal args() As String)
        Me.WriteToSysLog("Service started at ", False)
        Me.ScheduleService()
    End Sub

    Protected Overrides Sub OnStop()
        Me.WriteToSysLog("Service stopped at ", False)
        Me.Schedular.Dispose()
    End Sub


    Private Schedular As Timer

    Public Sub ScheduleService()
        Try
            Schedular = New Timer(New TimerCallback(AddressOf SchedularCallback))
            Dim mode As String = ConfigurationManager.AppSettings("Mode").ToUpper()
            Me.WriteToSysLog((Convert.ToString("Service Mode: ") & mode), False)

            'Set the Default Time.
            Dim scheduledTime As DateTime = DateTime.MinValue

            If mode = "DAILY" Then
                'Get the Scheduled Time from AppSettings.
                scheduledTime = DateTime.Parse(System.Configuration.ConfigurationManager.AppSettings("ScheduledTime"))
                If DateTime.Now > scheduledTime Then
                    'If Scheduled Time is passed set Schedule for the next day.
                    scheduledTime = scheduledTime.AddDays(1)

                    RunJob()
                End If
            End If

            If mode.ToUpper() = "INTERVAL" Then
                'Get the Interval in Minutes from AppSettings.
                Dim intervalMinutes As Integer = Convert.ToInt32(ConfigurationManager.AppSettings("IntervalMinutes"))

                'Set the Scheduled Time by adding the Interval to Current Time.
                scheduledTime = DateTime.Now.AddMinutes(intervalMinutes)
                If DateTime.Now > scheduledTime Then
                    'If Scheduled Time is passed set Schedule for the next Interval.
                    scheduledTime = scheduledTime.AddMinutes(intervalMinutes)

                    RunJob()
                End If
            End If

            Dim timeSpan As TimeSpan = scheduledTime.Subtract(DateTime.Now)
            Dim schedule As String = String.Format("{0} day(s) {1} hour(s) {2} minute(s) {3} seconds(s)", timeSpan.Days, timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds)

            Me.WriteToSysLog((Convert.ToString("Service scheduled to run after: ") & schedule), False)

            'Get the difference in Minutes between the Scheduled and Current Time.
            Dim dueTime As Integer = Convert.ToInt32(timeSpan.TotalMilliseconds)

            'Change the Timer's Due Time.
            Schedular.Change(dueTime, Timeout.Infinite)



            '===================================================
        Catch ex As Exception
            WriteToSysLog("Service Error on: " + ex.Message + ex.StackTrace, True)

            ''Stop the Windows Service.
            'Using serviceController As New System.ServiceProcess.ServiceController(System.Configuration.ConfigurationManager.AppSettings("ServiceName"))
            '    serviceController.[Stop]()
            'End Using
        End Try
    End Sub


    Private Sub RunJob()
        '======================= init folder =======================
        
        '=========== uploadfiles ===========
        If IO.Directory.Exists(uploadpath) = False Then
            IO.Directory.CreateDirectory(uploadpath)
        End If
        '=========== log ===========
        If IO.Directory.Exists(logpath) = False Then
            IO.Directory.CreateDirectory(logpath)
        End If
        '========== imgpath ========
        If IO.Directory.Exists(imgpath) = False Then
            IO.Directory.CreateDirectory(logpath)
        End If

        '========================Run App===========================
        Dim JobStart = ConfigurationManager.AppSettings("JobStart")
        Dim JobEnd = ConfigurationManager.AppSettings("JobEnd")

        Dim JobDate = Today.ToString("yyyyMMdd")
        Dim logfile = logpath & "\" & JobDate & ".txt"

        If JobDate >= JobStart And JobDate <= JobEnd And IO.File.Exists(logfile) = False Then

            Using dc As New DataClasses_MotorClaimDataContext(System.Configuration.ConfigurationManager.ConnectionStrings("APDMotorClaimNotificationVB.My.MySettings.MotorClaimConnectionString").ConnectionString)

                Dim _Claim_data = (From c In dc.V_ClaimDaily_00_Notice_Dailies).ToList()


                Dim _data = (From c In _Claim_data Where c.Status = True And c.IsPost = False And c.SubmitDate < DateTime.Today).ToList()
                'Dim _data = (From c In dc.V_ClaimDaily_00_Notice_Dailies Where c.Status = True And c.SubmitDate < DateTime.Today).Take(100).ToList()

                Console.WriteLine("Data :" & _data.Count.ToString())
                WriteToSysLog("Data :" & _data.Count.ToString(), False)

                Using writer As New StreamWriter(logfile, True)
                    writer.WriteLine("Data : " & _data.Count.ToString() & " - " & DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"))
                    writer.Close()
                End Using

                If _data.Count > 0 Then
                    Dim _AgentCodes = (From c In _data _
                              Group By AgentCode = c.AgentCode Into MyGroup = Group _
                              Select AgentCode).ToList()
                    For Each _AgentCode In _AgentCodes
                        WH_genxls(_AgentCode, _data)
                    Next
                End If


            End Using
        Else
            Using writer As New StreamWriter(logfile, True)
                writer.WriteLine("Already Run - " & DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"))
                writer.Close()
            End Using
        End If
    End Sub

    Private Sub SchedularCallback(e As Object)
        Me.WriteToSysLog("Service Log: ", False)
        Me.ScheduleService()
    End Sub

    Private Sub WriteToSysLog(text As String, IsError As Boolean)
        Dim sSource As String
        Dim sLog As String
        Dim sEvent As String
        Dim sMachine As String

        sSource = System.Configuration.ConfigurationManager.AppSettings("ServiceName")
        sLog = "Application"
        sEvent = String.Format("{0} {1}", text, DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"))
        sMachine = "."

        If Not EventLog.SourceExists(sSource, sMachine) Then
            EventLog.CreateEventSource(sSource, sLog, sMachine)
        End If

        Dim ELog As New EventLog(sLog, sMachine, sSource)

        If IsError Then
            ELog.WriteEntry(sEvent, EventLogEntryType.Error, 234, CType(3, Short))
        Else
            ELog.WriteEntry(sEvent)
        End If


    End Sub





    Private Sub WH_genxls(ByVal _AgentCode As String, ByVal _ClaimData As List(Of V_ClaimDaily_00_Notice_Daily))
        Dim _NoticeCode As String = "B0032"





        Using dc As New DataClasses_MotorClaimDataContext(System.Configuration.ConfigurationManager.ConnectionStrings("APDMotorClaimNotificationVB.My.MySettings.MotorClaimConnectionString").ConnectionString)

            Dim _Dealer = (From c In dc.tblDealers _
                              Where c.AgentCode.Equals(_AgentCode) _
                              ).FirstOrDefault()

            Console.WriteLine("Dealer :" & _Dealer.DealerNameEN)



            Dim hssfworkbook As New HSSFWorkbook()

            '=====================cell style ===================
            Dim Header_Font = hssfworkbook.CreateFont()
            Header_Font.FontName = "Tahoma"
            Header_Font.FontHeightInPoints = 8
            Header_Font.Boldweight = FontBoldWeight.Bold

            Dim Header_Style_Yellow As ICellStyle = hssfworkbook.CreateCellStyle()
            Header_Style_Yellow.SetFont(Header_Font)
            Header_Style_Yellow.Alignment = HorizontalAlignment.Center
            Header_Style_Yellow.FillPattern = FillPattern.SparseDots
            Header_Style_Yellow.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Yellow.Index
            Header_Style_Yellow.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Yellow.Index
            Header_Style_Yellow.BorderBottom = BorderStyle.Thin
            Header_Style_Yellow.BorderLeft = BorderStyle.Thin
            Header_Style_Yellow.BorderRight = BorderStyle.Thin
            Header_Style_Yellow.BorderTop = BorderStyle.Thin
            Header_Style_Yellow.BottomBorderColor = HSSFColor.Black.Index
            Header_Style_Yellow.LeftBorderColor = HSSFColor.Black.Index
            Header_Style_Yellow.RightBorderColor = HSSFColor.Black.Index
            Header_Style_Yellow.TopBorderColor = HSSFColor.Black.Index

            Dim Header_Style_AQUA As ICellStyle = hssfworkbook.CreateCellStyle()
            Header_Style_AQUA.SetFont(Header_Font)
            Header_Style_AQUA.Alignment = HorizontalAlignment.Center
            Header_Style_AQUA.FillPattern = FillPattern.SparseDots
            Header_Style_AQUA.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Aqua.Index
            Header_Style_AQUA.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Aqua.Index
            Header_Style_AQUA.BorderBottom = BorderStyle.Thin
            Header_Style_AQUA.BorderLeft = BorderStyle.Thin
            Header_Style_AQUA.BorderRight = BorderStyle.Thin
            Header_Style_AQUA.BorderTop = BorderStyle.Thin
            Header_Style_AQUA.BottomBorderColor = HSSFColor.Black.Index
            Header_Style_AQUA.LeftBorderColor = HSSFColor.Black.Index
            Header_Style_AQUA.RightBorderColor = HSSFColor.Black.Index
            Header_Style_AQUA.TopBorderColor = HSSFColor.Black.Index

            Dim Header_Style_PINK As ICellStyle = hssfworkbook.CreateCellStyle()
            Header_Style_PINK.SetFont(Header_Font)
            Header_Style_PINK.Alignment = HorizontalAlignment.Center
            Header_Style_PINK.FillPattern = FillPattern.SparseDots
            Header_Style_PINK.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Pink.Index
            Header_Style_PINK.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Pink.Index
            Header_Style_PINK.BorderBottom = BorderStyle.Thin
            Header_Style_PINK.BorderLeft = BorderStyle.Thin
            Header_Style_PINK.BorderRight = BorderStyle.Thin
            Header_Style_PINK.BorderTop = BorderStyle.Thin
            Header_Style_PINK.BottomBorderColor = HSSFColor.Black.Index
            Header_Style_PINK.LeftBorderColor = HSSFColor.Black.Index
            Header_Style_PINK.RightBorderColor = HSSFColor.Black.Index
            Header_Style_PINK.TopBorderColor = HSSFColor.Black.Index

            Dim Header_Style_ORANGE As ICellStyle = hssfworkbook.CreateCellStyle()
            Header_Style_ORANGE.SetFont(Header_Font)
            Header_Style_ORANGE.Alignment = HorizontalAlignment.Center
            Header_Style_ORANGE.FillPattern = FillPattern.SparseDots
            Header_Style_ORANGE.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightOrange.Index
            Header_Style_ORANGE.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.LightOrange.Index
            Header_Style_ORANGE.BorderBottom = BorderStyle.Thin
            Header_Style_ORANGE.BorderLeft = BorderStyle.Thin
            Header_Style_ORANGE.BorderRight = BorderStyle.Thin
            Header_Style_ORANGE.BorderTop = BorderStyle.Thin
            Header_Style_ORANGE.BottomBorderColor = HSSFColor.Black.Index
            Header_Style_ORANGE.LeftBorderColor = HSSFColor.Black.Index
            Header_Style_ORANGE.RightBorderColor = HSSFColor.Black.Index
            Header_Style_ORANGE.TopBorderColor = HSSFColor.Black.Index

            Dim Header_Style_GREEN As ICellStyle = hssfworkbook.CreateCellStyle()
            Header_Style_GREEN.SetFont(Header_Font)
            Header_Style_GREEN.Alignment = HorizontalAlignment.Center
            Header_Style_GREEN.FillPattern = FillPattern.SparseDots
            Header_Style_GREEN.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.BrightGreen.Index
            Header_Style_GREEN.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.BrightGreen.Index
            Header_Style_GREEN.BorderBottom = BorderStyle.Thin
            Header_Style_GREEN.BorderLeft = BorderStyle.Thin
            Header_Style_GREEN.BorderRight = BorderStyle.Thin
            Header_Style_GREEN.BorderTop = BorderStyle.Thin
            Header_Style_GREEN.BottomBorderColor = HSSFColor.Black.Index
            Header_Style_GREEN.LeftBorderColor = HSSFColor.Black.Index
            Header_Style_GREEN.RightBorderColor = HSSFColor.Black.Index
            Header_Style_GREEN.TopBorderColor = HSSFColor.Black.Index

            Dim Row_Font = hssfworkbook.CreateFont()
            Row_Font.FontName = "Tahoma"
            Row_Font.FontHeightInPoints = 8

            Dim Row_Style_Left As ICellStyle = hssfworkbook.CreateCellStyle()
            Row_Style_Left.SetFont(Row_Font)
            Row_Style_Left.BorderBottom = BorderStyle.Thin
            Row_Style_Left.BorderLeft = BorderStyle.Thin
            Row_Style_Left.BorderRight = BorderStyle.Thin
            Row_Style_Left.BorderTop = BorderStyle.Thin
            Row_Style_Left.BottomBorderColor = HSSFColor.Black.Index
            Row_Style_Left.LeftBorderColor = HSSFColor.Black.Index
            Row_Style_Left.RightBorderColor = HSSFColor.Black.Index
            Row_Style_Left.TopBorderColor = HSSFColor.Black.Index

            Dim Row_Style_Center As ICellStyle = hssfworkbook.CreateCellStyle()
            Row_Style_Center.SetFont(Row_Font)
            Row_Style_Center.Alignment = HorizontalAlignment.Center

            Dim Row_Style_Right As ICellStyle = hssfworkbook.CreateCellStyle()
            Row_Style_Right.SetFont(Row_Font)
            Row_Style_Right.Alignment = HorizontalAlignment.Right


            Dim Row_Font_BOLD = hssfworkbook.CreateFont()
            Row_Font_BOLD.FontName = "Tahoma"
            Row_Font_BOLD.FontHeightInPoints = 8
            Row_Font_BOLD.Boldweight = FontBoldWeight.Bold

            Dim Row_Style_Right_BOLD As ICellStyle = hssfworkbook.CreateCellStyle()
            Row_Style_Right_BOLD.SetFont(Row_Font_BOLD)
            Row_Style_Right_BOLD.Alignment = HorizontalAlignment.Right


            Dim Row_Style_Right_Float As HSSFCellStyle = hssfworkbook.CreateCellStyle
            Row_Style_Right_Float.SetFont(Row_Font)
            Row_Style_Right_Float.Alignment = HorizontalAlignment.Right
            Row_Style_Right_Float.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,#0.0")

            Dim Row_Style_Right_Float_Bold As HSSFCellStyle = hssfworkbook.CreateCellStyle
            Row_Style_Right_Float_Bold.SetFont(Row_Font_BOLD)
            Row_Style_Right_Float_Bold.Alignment = HorizontalAlignment.Right
            Row_Style_Right_Float_Bold.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,#0.0")



            '1 ดีลเลอร์()
            '2 ผู้เอาประกันภัย()
            '3 เลขตัวถัง()
            '4 เลขที่กรมธรรม์()
            '5 เลขที่เคลม()
            '6 วันที่เกิดเหตุ()
            '7 สถานที่เกิดเหตุ()
            '8 วันคุ้มครอง()
            '9 ผู้รับผลประโยชน์()
            '10 ประกันภัย
            '11.ผู้แจ้งเคลม
            '12.เบอร์โทรผู้แจ้งเคลม
            '13 วันที่ติดต่อ()
            '14 กรณีเข้าซ่อมแล้วกรุณาระบุชื่ออู่()
            '15 ลูกค้าเข้าซ่อมเอง(ระบุ '/')
            '16 ประกันภัยแนะนำ(ระบุ '/')
            '17 ยังไม่เข้าซ่อม(ระบุ '/')
            '18 หมายเหตุ(อื่นๆ



            '====================== data new sheet ====================
            Dim sheet1 As ISheet = hssfworkbook.CreateSheet(_Dealer.DealerNameTH)

            '================== create column ===================
            Dim _fields = "ดีลเลอร์,ผู้เอาประกันภัย,เลขตัวถัง,เลขที่กรมธรรม์,เลขที่เคลม,วันที่เกิดเหตุ,สถานที่เกิดเหตุ,วันคุ้มครอง,ผู้รับผลประโยชน์,ประกันภัย,ผู้แจ้งเคลม,เบอร์โทรผู้แจ้งเคลม,วันที่ติดต่อ,กรณีเข้าซ่อมแล้วกรุณาระบุชื่ออู่,ลูกค้าเข้าซ่อมเอง(ระบุ '/'),ประกันภัยแนะนำ(ระบุ '/'),ยังไม่เข้าซ่อม(ระบุ '/'),หมายเหตุ(อื่นๆ)".Split(",")
            Dim frow As IRow = sheet1.CreateRow(0)
            For i = 0 To _fields.Count - 1
                Dim cell = frow.CreateCell(i)
                cell.SetCellValue(_fields(i))

                Select Case i
                    Case 12
                        cell.CellStyle = Header_Style_AQUA
                    Case 13, 14, 15
                        cell.CellStyle = Header_Style_PINK
                    Case 16
                        cell.CellStyle = Header_Style_ORANGE
                    Case 17
                        cell.CellStyle = Header_Style_GREEN
                    Case Else
                        cell.CellStyle = Header_Style_Yellow

                End Select


            Next

            '================== init Column Length =====================
            Dim _item_f01_Length = frow.GetCell(0).StringCellValue.Length
            Dim _item_f02_Length = frow.GetCell(1).StringCellValue.Length
            Dim _item_f03_Length = frow.GetCell(2).StringCellValue.Length
            Dim _item_f04_Length = frow.GetCell(3).StringCellValue.Length
            Dim _item_f05_Length = frow.GetCell(4).StringCellValue.Length
            Dim _item_f06_Length = frow.GetCell(5).StringCellValue.Length
            Dim _item_f07_Length = frow.GetCell(6).StringCellValue.Length
            Dim _item_f08_Length = frow.GetCell(7).StringCellValue.Length
            Dim _item_f09_Length = frow.GetCell(8).StringCellValue.Length
            Dim _item_f10_Length = frow.GetCell(9).StringCellValue.Length
            Dim _item_f11_Length = frow.GetCell(10).StringCellValue.Length
            Dim _item_f12_Length = frow.GetCell(11).StringCellValue.Length
            Dim _item_f13_Length = frow.GetCell(12).StringCellValue.Length
            Dim _item_f14_Length = frow.GetCell(13).StringCellValue.Length
            Dim _item_f15_Length = frow.GetCell(14).StringCellValue.Length
            Dim _item_f16_Length = frow.GetCell(15).StringCellValue.Length
            Dim _item_f17_Length = frow.GetCell(16).StringCellValue.Length
            Dim _item_f18_Length = frow.GetCell(17).StringCellValue.Length

            sheet1.SetColumnWidth(0, _item_f01_Length * 400)
            sheet1.SetColumnWidth(1, _item_f02_Length * 400)
            sheet1.SetColumnWidth(2, _item_f03_Length * 400)
            sheet1.SetColumnWidth(3, _item_f04_Length * 400)
            sheet1.SetColumnWidth(4, _item_f05_Length * 400)
            sheet1.SetColumnWidth(5, _item_f06_Length * 400)
            sheet1.SetColumnWidth(6, _item_f07_Length * 400)
            sheet1.SetColumnWidth(7, _item_f08_Length * 400)
            sheet1.SetColumnWidth(8, _item_f09_Length * 400)
            sheet1.SetColumnWidth(9, _item_f10_Length * 400)
            sheet1.SetColumnWidth(10, _item_f11_Length * 400)
            sheet1.SetColumnWidth(11, _item_f12_Length * 400)
            sheet1.SetColumnWidth(12, _item_f13_Length * 400)
            sheet1.SetColumnWidth(13, _item_f14_Length * 400)
            sheet1.SetColumnWidth(14, _item_f15_Length * 400)
            sheet1.SetColumnWidth(15, _item_f16_Length * 400)
            sheet1.SetColumnWidth(16, _item_f17_Length * 400)
            sheet1.SetColumnWidth(17, _item_f18_Length * 400)
            '================== create cell ===================
            Dim j As Integer = 1
            Dim _ShowData = (From c In _ClaimData Where c.AgentCode.Equals(_AgentCode)).ToList()

            For Each _item In _ShowData

                Dim row As IRow = sheet1.CreateRow(j)

                '========================== Data =============================
                row.CreateCell(0).CellStyle = Row_Style_Left
                row.CreateCell(1).CellStyle = Row_Style_Left
                row.CreateCell(2).CellStyle = Row_Style_Left
                row.CreateCell(3).CellStyle = Row_Style_Left
                row.CreateCell(4).CellStyle = Row_Style_Left
                row.CreateCell(5).CellStyle = Row_Style_Left
                row.CreateCell(6).CellStyle = Row_Style_Left
                row.CreateCell(7).CellStyle = Row_Style_Left
                row.CreateCell(8).CellStyle = Row_Style_Left
                row.CreateCell(9).CellStyle = Row_Style_Left
                row.CreateCell(10).CellStyle = Row_Style_Left
                row.CreateCell(11).CellStyle = Row_Style_Left

                row.GetCell(0).SetCellValue(_item.ShowRoomName.Trim())
                row.GetCell(1).SetCellValue(_item.FullCustomerName.Trim())
                row.GetCell(2).SetCellValue(_item.ChassisNo.Trim())
                row.GetCell(3).SetCellValue(_item.PolicyNo.Trim())
                row.GetCell(4).SetCellValue(_item.ClaimNo.Trim())
                row.GetCell(5).SetCellValue(_item.AccidentDate.Trim())
                row.GetCell(6).SetCellValue(_item.AccidentPlace.Trim())
                row.GetCell(7).SetCellValue(_item.EffectiveDate.Trim())
                row.GetCell(8).SetCellValue(_item.Beneficiary.Trim())
                row.GetCell(9).SetCellValue(_item.InsurerName.Trim())
                row.GetCell(10).SetCellValue(_item.NoticeName)
                row.GetCell(11).SetCellValue(_item.NoticeTel)

                row.CreateCell(12).CellStyle = Header_Style_AQUA
                row.CreateCell(13).CellStyle = Header_Style_PINK
                row.CreateCell(14).CellStyle = Header_Style_PINK
                row.CreateCell(15).CellStyle = Header_Style_PINK
                row.CreateCell(16).CellStyle = Header_Style_ORANGE
                row.CreateCell(17).CellStyle = Header_Style_GREEN

                row.GetCell(12).SetCellValue("")
                row.GetCell(13).SetCellValue("")
                row.GetCell(14).SetCellValue("")
                row.GetCell(15).SetCellValue("")
                row.GetCell(16).SetCellValue("")
                row.GetCell(17).SetCellValue("")



                j += 1

            Next

            Dim _ShowRoomName As String = String.Format("บริษัท {0} จำกัด", _Dealer.DealerNameTH)



            '================== init  sheet property =================
            'If IO.Directory.Exists(IO.Path.GetDirectoryName(Diagnostics.Process.GetCurrentProcess().MainModule.FileName) & "\uploadfiles") = False Then
            '    IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(Diagnostics.Process.GetCurrentProcess().MainModule.FileName) & "\uploadfiles")
            'End If

            Dim _filename = String.Format("{0}.xls", System.Guid.NewGuid())
            Dim _fileDataPath = uploadpath & "\" & _filename

            Dim fs As New FileStream(_fileDataPath, FileMode.Create)
            hssfworkbook.Write(fs)
            fs.Close()




            'Try
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


            Using dc_portal = New DataClasses_PortalDataContext(System.Configuration.ConfigurationManager.ConnectionStrings("APDMotorClaimNotificationVB.My.MySettings.PortalConnectionString").ConnectionString)
                Dim _user = (From c In dc_portal.v_ads_actives Where c.sAMAccountName.Equals(System.Configuration.ConfigurationManager.AppSettings("sendername"))).FirstOrDefault()

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




                Dim _mailNotification = (From c In dc.MailNotifications Where c.Code.Equals(_NoticeCode)).FirstOrDefault()

                strSubject = _mailNotification.MailSubject.Replace("{Name}", _ShowRoomName)
                strSubject = strSubject.Replace("{date}", String.Format("{0}", MyUtils.GenThaiDate(Today.AddDays(-1), 2)))


                Dim _MailBody As String = HttpUtility.HtmlDecode(_mailNotification.MailBody.Replace("{date}", String.Format("{0}", MyUtils.GenThaiDate(Today.AddDays(-1), 2))))

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
                strMailTo = _Dealer.Email
                strMailCC = _mailNotification.MailCC


                'strMailFrom = _mail
                'strMailTo = _mail
                'strMailCC = _mailNotification.MailCC

                'strMailFrom = "dusit@asia.lockton.com"
                'strMailTo = "dusit@asia.lockton.com"
            End Using


            Dim MySmtpClient As New System.Net.Mail.SmtpClient(System.Configuration.ConfigurationManager.AppSettings("smtp").ToString())
            'MySmtpClient.Credentials = New System.Net.NetworkCredential("Your-username", "Your-password")

            'Dim msg As New System.Net.Mail.MailMessage(strMailFrom, strMailTo, strSubject, strMessage.ToString())
            Dim msg As New System.Net.Mail.MailMessage()

            '===========================================================
            Dim bodyHTML As String = "<html><body>" & strMessage.ToString() & "<span style='font-size:16.0pt;font-family:Angsana New,serif;color:green'><img src='cid:LOGO_IMAGE2' alt='Logo' /><i>Please consider the environment before printing this e-mail.</i></span></body></html>"
            Dim alternateView As AlternateView = alternateView.CreateAlternateViewFromString(bodyHTML, Nothing, "text/html")

            'Dim path_to_the_image_file1 As String = String.Format("{0}\{1}", Server.MapPath("~/images"), "maillockton30.jpg")
            Dim path_to_the_image_file2 As String = imgpath & "\mailsmallpicture.jpg"

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
            msg.Body = Nothing 'Body

            msg.From = New MailAddress(strMailFrom) 'Mail From

            'Dim _MailTo = strMailTo 'Mail To
            If Not String.IsNullOrEmpty(strMailTo) Then
                For Each item In strMailTo.Split(";")
                    If Not String.IsNullOrEmpty(item.Trim()) Then msg.To.Add(item)
                Next
            End If


            'Dim _MailCC = strMailCC 'Mail CC
            If Not String.IsNullOrEmpty(strMailCC) Then
                For Each item In strMailCC.Split(";")
                    If Not String.IsNullOrEmpty(item.Trim()) Then msg.CC.Add(item)
                Next
            End If

            msg.BodyEncoding = Encoding.UTF8
            msg.IsBodyHtml = True
            msg.Priority = Net.Mail.MailPriority.High
            '==============Add an Attachment=========================

            Dim att_data = New Attachment(_fileDataPath)
            att_data.Name = String.Format("{0}.xls", _ShowRoomName)
            msg.Attachments.Add(att_data)

            'Try

            MySmtpClient.Send(msg)
            Console.WriteLine("Send to : " & _Dealer.DealerNameEN)

            Console.WriteLine("Update TRID")

            For Each _TRID In _ShowData
                dc.ExecuteCommand("update tblClaimTransaction_Data Set IsPost=1,PostDate=getdate() where TRID='" & _TRID.TRID.ToString() & "' ")
            Next



            'Catch ex As Exception
            '    Console.WriteLine("Error Sent to : " & _Dealer.DealerNameEN)
            '    WriteToSysLog("Error Sent to : " & _Dealer.DealerNameEN & ", Message" & ex.Message, True)
            'End Try





        End Using

    End Sub


End Class
