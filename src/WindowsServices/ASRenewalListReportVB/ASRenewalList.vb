﻿Imports System
Imports System.IO
Imports System.Threading
Imports System.Configuration

Imports System.Diagnostics
Imports Microsoft.Reporting.WebForms
Imports System.Net.Mail
Imports System.Text
Imports System.Web
Imports Ionic.Zip
Imports System.Net

Public Class ASRenewalList

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

        'Dim JobDay = CInt(ConfigurationManager.AppSettings("JobMonthlyDay"))

        Dim JobDate = Today.ToString("yyyyMMdd")
        Dim logfile = logpath & "\" & JobDate & ".txt"

        If JobDate >= JobStart And JobDate <= JobEnd And IO.File.Exists(logfile) = False Then



            Dim _data As New List(Of tblAPDRenewalList_SUMMARY)
            Using dc2 As New DataClasses_RawdataDataContext()
                _data = (From c In dc2.tblAPDRenewalList_SUMMARies).ToList()
            End Using


            If _data.Count > 0 Then
                'Dim _files As New List(Of String)
                Dim _files As New Dictionary(Of String, Integer)

                Dim _FiscalYears = (From c In _data
                                    Group By FiscalYear = c.FiscalYear Into MyGroup = Group
                                    Select FiscalYear Order By FiscalYear).ToList()
                For Each FiscalYear In _FiscalYears

                    Dim FileName = SaveToExcel(FiscalYear)
                    _files.Add(FileName, FiscalYear)
                Next





                'Dim FileName = SaveToExcel()
                Using writer As New StreamWriter(logfile, True)
                    writer.WriteLine("Files : " & _files.Count & " - " & DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"))
                    writer.Close()
                End Using

                If (_files.Count > 0) Then
                    WH_genxls(_files)
                End If

            Else
                Console.WriteLine("Data :" & _data.Count.ToString())
                WriteToSysLog("Data :" & _data.Count.ToString(), False)
            End If

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



    Private Function SaveToExcel(ByVal FiscalYear As Int32)



        Dim _filename = String.Format("ASRenewalList_{0}.xls", Now.ToString("yyyyMMddHHmmss"))
        Dim SavePath = uploadpath & "\" & _filename


        Dim viewer As New ReportViewer()
        viewer.ServerReport.ReportServerUrl = New Uri("http://lockthbnk-db07/ReportServer")
        viewer.ServerReport.ReportPath = "/Reports/rptAPDRenewalList"

        Dim parameters As New List(Of ReportParameter)
        parameters.Add(New ReportParameter("FiscalYear", FiscalYear, False))
        viewer.ServerReport.SetParameters(parameters)

        'viewer.ServerReport.ReportServerCredentials = New ReportServerCredentials()



        Dim Bytes() As Byte = viewer.ServerReport.Render("Excel", "", Nothing, Nothing, Nothing, Nothing, Nothing)

        Using Stream As New FileStream(SavePath, FileMode.Create)
            Stream.Write(Bytes, 0, Bytes.Length)
        End Using


        Return SavePath
    End Function

    Private Sub WH_genxls(ByVal files As Dictionary(Of String, Integer))
        Dim _NoticeCode As String = "B0039"

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
        Dim strMailTo As String = ""
        Dim strMailCC As String = ""
        Dim strMailBCC As String = ""

        Dim strSubject As String = ""
        Dim strMessage As New StringBuilder()


        Using dc_portal = New DataClasses_PortalDataContext()

            Using dc2 As New DataClasses_RawdataDataContext()
                'Dim _data = (From c In dc2.tblAPDRenewalList_SUMMARies Order By c.FiscalYear).FirstOrDefault()



                Dim _mailNotification = (From c In dc_portal.MailNotifications Where c.Code.Equals(_NoticeCode)).FirstOrDefault()

                strSubject = _mailNotification.MailSubject.Replace("{date}", String.Format("{0}", Now.ToString("d MMMM yyyy")))

                strMailFrom = _mailNotification.MailFrom
                strMailTo = _mailNotification.MailTo
                strMailCC = _mailNotification.MailCC
                strMailBCC = _mailNotification.MailBcc


                Dim _user = (From c In dc_portal.v_ads_actives Where c.mail.Equals(strMailFrom)).FirstOrDefault()

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


                Dim _MailBody As String = HttpUtility.HtmlDecode(_mailNotification.MailBody)

                Dim _data = (From c In dc2.tblAPDRenewalList_SUMMARies
                             Group By LotNo = c.LotNo Into MyGroup = Group
                             Select LotNo Order By LotNo).ToList()
                '_MailBody = _MailBody.Replace("{date}", String.Format("{0} - {1}", _data(0).Value, _data(_data.Count - 1).Value))



                Dim _LotNo = (From c In dc2.tblAPDRenewalList_SUMMARies
                              Group By LotNo = c.LotNo, FiscalYear = c.FiscalYear Into MyGroup = Group
                              Select LotNo, FiscalYear Order By LotNo).ToList()
                Dim _data_Year = (From c In _LotNo
                                  Group By FiscalYear = c.FiscalYear Into MyGroup = Group
                                  Select FiscalYear Order By FiscalYear).ToList()

                Dim sb As New StringBuilder()
                For Each item In _data_Year
                    Dim _LotNoItem = (From c In _LotNo Where c.FiscalYear.Equals(item) Order By c.LotNo).ToList()
                    Dim _LotNoMin = _LotNoItem.Min(Function(c) c.LotNo).Value
                    Dim _LotNoMax = _LotNoItem.Max(Function(c) c.LotNo).Value
                    'sb.AppendFormat("ปี {0} : ปัจจุบันนำเข้าข้อมูลทั้ง {1} เดือน <br>", item.ToString(), _LotNoItem.Count.ToString())
                    'sb.AppendFormat("Enclosed Is A&S Renewal List Report, period: {0} - {1}, for your reference. <br><br>", _LotNoMin.ToString(), _LotNoMax.ToString())

                    sb.AppendFormat("- FY{0}, the data were expired between {1} – {2} ({3} Months) <br><br>", item.ToString(), _LotNoMin.ToString(), _LotNoMax.ToString(), _LotNoItem.Count.ToString())

                Next

                'ปี FY17 / 18 : ปัจจุบันนำเข้าข้อมูลทั้ง 12 เดือนแล้ว 
                'Enclosed Is A&S Renewal List Report, period: May 2017 - April 2018, for your reference. 

                'ปี FY18 / 19 : ปัจจุบันนำเข้าข้อมูลถึง 518 
                'Enclosed Is A&S Renewal List Report, period: May 2018 for your reference.

                _MailBody = _MailBody.Replace("{period}", sb.ToString())

                _MailBody = _MailBody.Replace("{displayName}", _displayName)
                _MailBody = _MailBody.Replace("{title}", _title)
                _MailBody = _MailBody.Replace("{department}", _department)
                _MailBody = _MailBody.Replace("{company}", _company)
                _MailBody = _MailBody.Replace("{streetAddress}", _streetAddress)
                _MailBody = _MailBody.Replace("{telephoneNumber}", _telephoneNumber)
                _MailBody = _MailBody.Replace("{facsimileTelephoneNumber}", _facsimileTelephoneNumber)
                _MailBody = _MailBody.Replace("{mail}", _mail)

                strMessage.AppendLine(_MailBody)




                Dim MySmtpClient As New System.Net.Mail.SmtpClient(System.Configuration.ConfigurationManager.AppSettings("smtp").ToString())

                Dim msg As New System.Net.Mail.MailMessage()

                '===========================================================
                Dim bodyHTML As String = "<html><body>" & strMessage.ToString() & "<span style='font-size:16.0pt;font-family:Angsana New,serif;color:green'><img src='cid:LOGO_IMAGE2' alt='Logo' /><i>Please consider the environment before printing this e-mail.</i></span></body></html>"
                Dim alternateView As AlternateView = AlternateView.CreateAlternateViewFromString(bodyHTML, Nothing, "text/html")

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
                'msg.SubjectEncoding = Encoding.GetEncoding("windows-874")
                'msg.BodyEncoding = Encoding.GetEncoding("windows-874")

                msg.Body = Nothing 'Body

                msg.From = New MailAddress(strMailFrom) 'Mail From

                'Mail To
                If Not String.IsNullOrEmpty(strMailTo) Then
                    For Each item In strMailTo.Split(";")
                        If Not String.IsNullOrEmpty(item.Trim()) Then msg.To.Add(item)
                    Next
                End If
                'Mail CC
                If Not String.IsNullOrEmpty(strMailCC) Then
                    For Each item In strMailCC.Split(";")
                        If Not String.IsNullOrEmpty(item.Trim()) Then msg.CC.Add(item)
                    Next
                End If
                'Mail Bcc
                If Not String.IsNullOrEmpty(strMailBCC) Then
                    For Each item In strMailBCC.Split(";")
                        If Not String.IsNullOrEmpty(item.Trim()) Then msg.Bcc.Add(item)
                    Next
                End If


                msg.BodyEncoding = Encoding.UTF8
                msg.IsBodyHtml = True
                msg.Priority = Net.Mail.MailPriority.High

                For Each item In files
                    '==============Add an Attachment=========================
                    '1.A&S_ProductionNewStructure 20171020 
                    '2.A&S_Budget VS Actual 20171020 
                    Dim att_data1 = New Attachment(item.Key, "application/vnd.ms-excel")
                    att_data1.Name = String.Format("{0}.xls", "A&S_RenewalList_FiscalYear_" & item.Value)
                    'att_data.Name = "Kawasaki.xls"
                    'att_data.Name = System.Web.HttpUtility.UrlDecode(System.Web.HttpUtility.UrlEncode(String.Format("{0}.xls", "สรุปยอดขายประจำเดือน Kawasaki"), System.Text.Encoding.UTF8))
                    'att_data.Name = "สรุปยอดขายประจำเดือน.xls"
                    'att_data.NameEncoding = Encoding.GetEncoding("windows-874")

                    att_data1.TransferEncoding = System.Net.Mime.TransferEncoding.QuotedPrintable

                    msg.Attachments.Add(att_data1)

                Next

                'Try

                MySmtpClient.Send(msg)

                Console.WriteLine("Send")


                'Catch ex As Exception
                '    Console.WriteLine("Error Sent")
                '    'WriteToFile("Error Sent : " & ex.Message)
                'End Try


            End Using
        End Using

    End Sub

End Class
