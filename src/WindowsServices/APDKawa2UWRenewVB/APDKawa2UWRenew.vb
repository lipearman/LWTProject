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

Public Class APDKawa2UWRenew

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
        'Try
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
        'Catch ex As Exception
        '    WriteToSysLog("Service Error on: " + ex.Message + ex.StackTrace, True)

        '    ''Stop the Windows Service.
        '    'Using serviceController As New System.ServiceProcess.ServiceController(System.Configuration.ConfigurationManager.AppSettings("ServiceName"))
        '    '    serviceController.[Stop]()
        '    'End Using
        'End Try
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
            Using dc As New DataClasses_MCYDataContext(Global.APDKawa2UWRenewVB.My.MySettings.Default.LWTReportsConnectionString)

                If Not Today.ToString("dddd").ToLower().Equals("saturday") And Not Today.ToString("dddd").ToLower().Equals("sunday") Then

                    '============================ non ASIA ===========================
                    Console.WriteLine("Start")
                    Dim _data = (From c In dc.V_KAWA2_UW_Non_ASIAs Where c.Renew = True).ToList()
                    Console.WriteLine("Data :" & _data.Count.ToString())
                    Using writer As New StreamWriter(logfile, True)
                        writer.WriteLine("Data : " & _data.Count.ToString() & " - " & DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"))
                        writer.Close()
                    End Using

                    If _data.Count > 0 Then
                        Dim _UWCodes = (From c In _data
                                        Group By UWCode = c.UWCode Into MyGroup = Group
                                        Select UWCode).ToList()
                        For Each _UWCode In _UWCodes
                            WH_genxls(_UWCode, _data)
                        Next
                    End If



                    '============================ ASIA ===========================
                    Dim _data_asia = (From c In dc.V_KAWA2_UW_ASIAs Where c.Renew = True).ToList()
                    If _data_asia.Count > 0 Then
                        WH_genxls_ASIA("ASIA", _data_asia)

                    End If


                    '============================ VIRI New===========================
                    Dim MyConn = Global.APDKawa2UWRenewVB.My.MySettings.Default.LWTReportsConnectionString
                    Dim ds As Data.DataSet = SqlHelper.ExecuteDataset(MyConn, "MCY_2010.dbo.sp_V_KAWA2_UW_VIRI", "1")
                    Dim _insurer As Data.DataSet = SqlHelper.ExecuteDataset(MyConn, CommandType.Text, "select * from V_KAWA2_UW_EMAIL where UWCode='VIRI' and MailTo is not null")

                    If ds.Tables(0).Rows.Count > 0 And _insurer.Tables(0).Rows.Count Then
                        WH_genxls_VIRI("U00121", ds.Tables(0), _insurer.Tables(0).Rows(0))
                    End If

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

    Private Sub WH_genxls_ASIA(ByVal _UWCode As String, ByVal _Data As List(Of V_KAWA2_UW_ASIA))
        Dim _NoticeCode As String = "B0030R"

        Dim _Insurer = (From c In _Data _
                          Where c.UWCode.Equals(_UWCode) _
                          ).FirstOrDefault()

        Console.WriteLine("Insurer :" & _Insurer.UWName)


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

        Dim Header_Style_SkyBlue As ICellStyle = hssfworkbook.CreateCellStyle()
        Header_Style_SkyBlue.SetFont(Header_Font)
        Header_Style_SkyBlue.Alignment = HorizontalAlignment.Center
        Header_Style_SkyBlue.FillPattern = FillPattern.SparseDots
        Header_Style_SkyBlue.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.SkyBlue.Index
        Header_Style_SkyBlue.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.SkyBlue.Index
        Header_Style_SkyBlue.BorderBottom = BorderStyle.Thin
        Header_Style_SkyBlue.BorderLeft = BorderStyle.Thin
        Header_Style_SkyBlue.BorderRight = BorderStyle.Thin
        Header_Style_SkyBlue.BorderTop = BorderStyle.Thin
        Header_Style_SkyBlue.BottomBorderColor = HSSFColor.Black.Index
        Header_Style_SkyBlue.LeftBorderColor = HSSFColor.Black.Index
        Header_Style_SkyBlue.RightBorderColor = HSSFColor.Black.Index
        Header_Style_SkyBlue.TopBorderColor = HSSFColor.Black.Index

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

        Dim Row_Style_Center As ICellStyle = hssfworkbook.CreateCellStyle()
        Row_Style_Center.SetFont(Row_Font)
        Row_Style_Center.Alignment = HorizontalAlignment.Center
        Row_Style_Center.BorderBottom = BorderStyle.Thin
        Row_Style_Center.BorderLeft = BorderStyle.Thin
        Row_Style_Center.BorderRight = BorderStyle.Thin
        Row_Style_Center.BorderTop = BorderStyle.Thin
        Row_Style_Center.BottomBorderColor = HSSFColor.Black.Index
        Row_Style_Center.LeftBorderColor = HSSFColor.Black.Index
        Row_Style_Center.RightBorderColor = HSSFColor.Black.Index
        Row_Style_Center.TopBorderColor = HSSFColor.Black.Index

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



        '====================== data new sheet ====================
        Dim sheet1 As ISheet = hssfworkbook.CreateSheet(_Insurer.UWName)

        '================== create column ===================
        Dim _fields = "No.,C1,C2,C3,C4,C5,C6,C7,C8,C9,C10,C11,C12,C13,C14,C15,C16,C17,C18,C19,C20,C21,C22,C23,C24,C25,C26,C27,C28,C29,C30,C31,C32,C33,C34,C35,C36,C37,C38,C39,C40,C41,C42,C43,C44,C45,C46,C47,C48,C49,C50,C51,C52,C53,C54,C55,C56,C57,C58,C59,C60,C61,C62,C63,C64,C65,C66,C67,C68,C69,C70,C71,C72,C73,C74,C75,C76,C77,C78,C79,C80,C81,C82,C83,C84,C85,C86,C87,C88,C89,C90,C91,C92,C93,C94,C95,C96,C97,C98,C99,C100,C101,C102,C103,C104,C105,C106,C107,C108,C109,C110,C111,C112,C113,C114,C115,C116,C117,C118,C119,C120,C121,C122,C123,C124,C125,C126,C127,C128,C129,C130,C131,C132,C133,C134,C135,C136,C137,C138,C139,C140,C141,C142,C143,C144,C145,C146,C147,C148,C149,C150,C151,C152,C153,C154,C155,C156,C157,C158,C159".Split(",")
        Dim frow As IRow = sheet1.CreateRow(0)
        For i = 0 To _fields.Count - 1
            Dim cell = frow.CreateCell(i)
            cell.SetCellValue(_fields(i))
            cell.CellStyle = Header_Style_SkyBlue
        Next
        '================== create cell ===================
        Dim j As Integer = 1
        Dim _SheetData = (From c In _Data Where c.UWCode.Equals(_Insurer.UWCode)).ToList()

        For Each _item In _SheetData

            Dim row As IRow = sheet1.CreateRow(j)

            '========================== Create Row =============================
            row.CreateCell(0).CellStyle = Row_Style_Center
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
            row.CreateCell(12).CellStyle = Row_Style_Left
            row.CreateCell(13).CellStyle = Row_Style_Left
            row.CreateCell(14).CellStyle = Row_Style_Left
            row.CreateCell(15).CellStyle = Row_Style_Left
            row.CreateCell(16).CellStyle = Row_Style_Left
            row.CreateCell(17).CellStyle = Row_Style_Left
            row.CreateCell(18).CellStyle = Row_Style_Left
            row.CreateCell(19).CellStyle = Row_Style_Left
            row.CreateCell(20).CellStyle = Row_Style_Left
            row.CreateCell(21).CellStyle = Row_Style_Left
            row.CreateCell(22).CellStyle = Row_Style_Left
            row.CreateCell(23).CellStyle = Row_Style_Left
            row.CreateCell(24).CellStyle = Row_Style_Left
            row.CreateCell(25).CellStyle = Row_Style_Left
            row.CreateCell(26).CellStyle = Row_Style_Left
            row.CreateCell(27).CellStyle = Row_Style_Left
            row.CreateCell(28).CellStyle = Row_Style_Left
            row.CreateCell(29).CellStyle = Row_Style_Left
            row.CreateCell(30).CellStyle = Row_Style_Left
            row.CreateCell(31).CellStyle = Row_Style_Left
            row.CreateCell(32).CellStyle = Row_Style_Left
            row.CreateCell(33).CellStyle = Row_Style_Left
            row.CreateCell(34).CellStyle = Row_Style_Left
            row.CreateCell(35).CellStyle = Row_Style_Left
            row.CreateCell(36).CellStyle = Row_Style_Left
            row.CreateCell(37).CellStyle = Row_Style_Left
            row.CreateCell(38).CellStyle = Row_Style_Left
            row.CreateCell(39).CellStyle = Row_Style_Left
            row.CreateCell(40).CellStyle = Row_Style_Left
            row.CreateCell(41).CellStyle = Row_Style_Left
            row.CreateCell(42).CellStyle = Row_Style_Left
            row.CreateCell(43).CellStyle = Row_Style_Left
            row.CreateCell(44).CellStyle = Row_Style_Left
            row.CreateCell(45).CellStyle = Row_Style_Left
            row.CreateCell(46).CellStyle = Row_Style_Left
            row.CreateCell(47).CellStyle = Row_Style_Left
            row.CreateCell(48).CellStyle = Row_Style_Left
            row.CreateCell(49).CellStyle = Row_Style_Left
            row.CreateCell(50).CellStyle = Row_Style_Left
            row.CreateCell(51).CellStyle = Row_Style_Left
            row.CreateCell(52).CellStyle = Row_Style_Left
            row.CreateCell(53).CellStyle = Row_Style_Left
            row.CreateCell(54).CellStyle = Row_Style_Left
            row.CreateCell(55).CellStyle = Row_Style_Left
            row.CreateCell(56).CellStyle = Row_Style_Left
            row.CreateCell(57).CellStyle = Row_Style_Left
            row.CreateCell(58).CellStyle = Row_Style_Left
            row.CreateCell(59).CellStyle = Row_Style_Left
            row.CreateCell(60).CellStyle = Row_Style_Left
            row.CreateCell(61).CellStyle = Row_Style_Left
            row.CreateCell(62).CellStyle = Row_Style_Left
            row.CreateCell(63).CellStyle = Row_Style_Left
            row.CreateCell(64).CellStyle = Row_Style_Left
            row.CreateCell(65).CellStyle = Row_Style_Left
            row.CreateCell(66).CellStyle = Row_Style_Left
            row.CreateCell(67).CellStyle = Row_Style_Left
            row.CreateCell(68).CellStyle = Row_Style_Left
            row.CreateCell(69).CellStyle = Row_Style_Left
            row.CreateCell(70).CellStyle = Row_Style_Left
            row.CreateCell(71).CellStyle = Row_Style_Left
            row.CreateCell(72).CellStyle = Row_Style_Left
            row.CreateCell(73).CellStyle = Row_Style_Left
            row.CreateCell(74).CellStyle = Row_Style_Left
            row.CreateCell(75).CellStyle = Row_Style_Left
            row.CreateCell(76).CellStyle = Row_Style_Left
            row.CreateCell(77).CellStyle = Row_Style_Left
            row.CreateCell(78).CellStyle = Row_Style_Left
            row.CreateCell(79).CellStyle = Row_Style_Left
            row.CreateCell(80).CellStyle = Row_Style_Left
            row.CreateCell(81).CellStyle = Row_Style_Left
            row.CreateCell(82).CellStyle = Row_Style_Left
            row.CreateCell(83).CellStyle = Row_Style_Left
            row.CreateCell(84).CellStyle = Row_Style_Left
            row.CreateCell(85).CellStyle = Row_Style_Left
            row.CreateCell(86).CellStyle = Row_Style_Left
            row.CreateCell(87).CellStyle = Row_Style_Left
            row.CreateCell(88).CellStyle = Row_Style_Left
            row.CreateCell(89).CellStyle = Row_Style_Left
            row.CreateCell(90).CellStyle = Row_Style_Left
            row.CreateCell(91).CellStyle = Row_Style_Left
            row.CreateCell(92).CellStyle = Row_Style_Left
            row.CreateCell(93).CellStyle = Row_Style_Left
            row.CreateCell(94).CellStyle = Row_Style_Left
            row.CreateCell(95).CellStyle = Row_Style_Left
            row.CreateCell(96).CellStyle = Row_Style_Left
            row.CreateCell(97).CellStyle = Row_Style_Left
            row.CreateCell(98).CellStyle = Row_Style_Left
            row.CreateCell(99).CellStyle = Row_Style_Left
            row.CreateCell(100).CellStyle = Row_Style_Left
            row.CreateCell(101).CellStyle = Row_Style_Left
            row.CreateCell(102).CellStyle = Row_Style_Left
            row.CreateCell(103).CellStyle = Row_Style_Left
            row.CreateCell(104).CellStyle = Row_Style_Left
            row.CreateCell(105).CellStyle = Row_Style_Left
            row.CreateCell(106).CellStyle = Row_Style_Left
            row.CreateCell(107).CellStyle = Row_Style_Left
            row.CreateCell(108).CellStyle = Row_Style_Left
            row.CreateCell(109).CellStyle = Row_Style_Left
            row.CreateCell(110).CellStyle = Row_Style_Left
            row.CreateCell(111).CellStyle = Row_Style_Left
            row.CreateCell(112).CellStyle = Row_Style_Left
            row.CreateCell(113).CellStyle = Row_Style_Left
            row.CreateCell(114).CellStyle = Row_Style_Left
            row.CreateCell(115).CellStyle = Row_Style_Left
            row.CreateCell(116).CellStyle = Row_Style_Left
            row.CreateCell(117).CellStyle = Row_Style_Left
            row.CreateCell(118).CellStyle = Row_Style_Left
            row.CreateCell(119).CellStyle = Row_Style_Left
            row.CreateCell(120).CellStyle = Row_Style_Left
            row.CreateCell(121).CellStyle = Row_Style_Left
            row.CreateCell(122).CellStyle = Row_Style_Left
            row.CreateCell(123).CellStyle = Row_Style_Left
            row.CreateCell(124).CellStyle = Row_Style_Left
            row.CreateCell(125).CellStyle = Row_Style_Left
            row.CreateCell(126).CellStyle = Row_Style_Left
            row.CreateCell(127).CellStyle = Row_Style_Left
            row.CreateCell(128).CellStyle = Row_Style_Left
            row.CreateCell(129).CellStyle = Row_Style_Left
            row.CreateCell(130).CellStyle = Row_Style_Left
            row.CreateCell(131).CellStyle = Row_Style_Left
            row.CreateCell(132).CellStyle = Row_Style_Left
            row.CreateCell(133).CellStyle = Row_Style_Left
            row.CreateCell(134).CellStyle = Row_Style_Left
            row.CreateCell(135).CellStyle = Row_Style_Left
            row.CreateCell(136).CellStyle = Row_Style_Left
            row.CreateCell(137).CellStyle = Row_Style_Left
            row.CreateCell(138).CellStyle = Row_Style_Left
            row.CreateCell(139).CellStyle = Row_Style_Left
            row.CreateCell(140).CellStyle = Row_Style_Left
            row.CreateCell(141).CellStyle = Row_Style_Left
            row.CreateCell(142).CellStyle = Row_Style_Left
            row.CreateCell(143).CellStyle = Row_Style_Left
            row.CreateCell(144).CellStyle = Row_Style_Left
            row.CreateCell(145).CellStyle = Row_Style_Left
            row.CreateCell(146).CellStyle = Row_Style_Left
            row.CreateCell(147).CellStyle = Row_Style_Left
            row.CreateCell(148).CellStyle = Row_Style_Left
            row.CreateCell(149).CellStyle = Row_Style_Left
            row.CreateCell(150).CellStyle = Row_Style_Left
            row.CreateCell(151).CellStyle = Row_Style_Left
            row.CreateCell(152).CellStyle = Row_Style_Left
            row.CreateCell(153).CellStyle = Row_Style_Left
            row.CreateCell(154).CellStyle = Row_Style_Left
            row.CreateCell(155).CellStyle = Row_Style_Left
            row.CreateCell(156).CellStyle = Row_Style_Left
            row.CreateCell(157).CellStyle = Row_Style_Left
            row.CreateCell(158).CellStyle = Row_Style_Left
            row.CreateCell(159).CellStyle = Row_Style_Left

            '========================== Set Value =============================
            row.GetCell(0).SetCellValue(j)
            row.GetCell(1).SetCellValue(_item.C1)
            row.GetCell(2).SetCellValue(_item.C2)
            row.GetCell(3).SetCellValue(_item.C3)
            row.GetCell(4).SetCellValue(_item.C4)
            row.GetCell(5).SetCellValue(_item.C5)
            row.GetCell(6).SetCellValue(_item.C6)
            row.GetCell(7).SetCellValue(_item.C7)
            row.GetCell(8).SetCellValue(String.Format("({0})", _item.C8))
            row.GetCell(9).SetCellValue(_item.C9)
            row.GetCell(10).SetCellValue(_item.C10)
            row.GetCell(11).SetCellValue(_item.C11)
            row.GetCell(12).SetCellValue(_item.C12)
            row.GetCell(13).SetCellValue(_item.C13)
            row.GetCell(14).SetCellValue(_item.C14)
            row.GetCell(15).SetCellValue(_item.C15)
            row.GetCell(16).SetCellValue(_item.C16)
            row.GetCell(17).SetCellValue(_item.C17)
            row.GetCell(18).SetCellValue(_item.C18)
            row.GetCell(19).SetCellValue(_item.C19)
            row.GetCell(20).SetCellValue(_item.C20)
            row.GetCell(21).SetCellValue(_item.C21)
            row.GetCell(22).SetCellValue(_item.C22)
            row.GetCell(23).SetCellValue(_item.C23)
            row.GetCell(24).SetCellValue(_item.C24)
            row.GetCell(25).SetCellValue(_item.C25)
            row.GetCell(26).SetCellValue(_item.C26)
            row.GetCell(27).SetCellValue(_item.C27)
            row.GetCell(28).SetCellValue(_item.C28)
            row.GetCell(29).SetCellValue(_item.C29)
            row.GetCell(30).SetCellValue(_item.C30)
            row.GetCell(31).SetCellValue(_item.C31)
            row.GetCell(32).SetCellValue(_item.C32)
            row.GetCell(33).SetCellValue(_item.C33.Value)
            row.GetCell(34).SetCellValue(_item.C34)
            row.GetCell(35).SetCellValue(_item.C35)
            row.GetCell(36).SetCellValue(_item.C36)
            row.GetCell(37).SetCellValue(_item.C37)
            row.GetCell(38).SetCellValue(_item.C38)
            row.GetCell(39).SetCellValue(_item.C39)
            row.GetCell(40).SetCellValue(_item.C40)
            row.GetCell(41).SetCellValue(_item.C41)
            row.GetCell(42).SetCellValue(_item.C42)
            row.GetCell(43).SetCellValue(_item.C43)
            row.GetCell(44).SetCellValue(_item.C44)
            row.GetCell(45).SetCellValue(_item.C45)
            row.GetCell(46).SetCellValue(_item.C46)
            row.GetCell(47).SetCellValue(_item.C47)
            row.GetCell(48).SetCellValue(_item.C48.Value)
            row.GetCell(49).SetCellValue(_item.C49)
            row.GetCell(50).SetCellValue(_item.C50)
            row.GetCell(51).SetCellValue(_item.C51)
            row.GetCell(52).SetCellValue(_item.C52)
            row.GetCell(53).SetCellValue(_item.C53)
            row.GetCell(54).SetCellValue(_item.C54)
            row.GetCell(55).SetCellValue(_item.C55)
            row.GetCell(56).SetCellValue(_item.C56)
            row.GetCell(57).SetCellValue(_item.C57)
            row.GetCell(58).SetCellValue(_item.C58)
            row.GetCell(59).SetCellValue(_item.C59)
            row.GetCell(60).SetCellValue(_item.C60)
            row.GetCell(61).SetCellValue(_item.C61)
            row.GetCell(62).SetCellValue(_item.C62)
            row.GetCell(63).SetCellValue(_item.C63)
            row.GetCell(64).SetCellValue(_item.C64)
            row.GetCell(65).SetCellValue(_item.C65)
            row.GetCell(66).SetCellValue(_item.C66)
            row.GetCell(67).SetCellValue(_item.C67)
            row.GetCell(68).SetCellValue(_item.C68)
            row.GetCell(69).SetCellValue(_item.C69)
            row.GetCell(70).SetCellValue(_item.C70)
            row.GetCell(71).SetCellValue(_item.C71)
            row.GetCell(72).SetCellValue(_item.C72)
            row.GetCell(73).SetCellValue(_item.C73)
            row.GetCell(74).SetCellValue(_item.C74)
            row.GetCell(75).SetCellValue(_item.C75)
            row.GetCell(76).SetCellValue(_item.C76)
            row.GetCell(77).SetCellValue(_item.C77)
            row.GetCell(78).SetCellValue(_item.C78)
            row.GetCell(79).SetCellValue(_item.C79)
            row.GetCell(80).SetCellValue(_item.C80)
            row.GetCell(81).SetCellValue(_item.C81)
            row.GetCell(82).SetCellValue(_item.C82)
            row.GetCell(83).SetCellValue(_item.C83)
            row.GetCell(84).SetCellValue(_item.C84)
            row.GetCell(85).SetCellValue(_item.C85)
            row.GetCell(86).SetCellValue(_item.C86)
            row.GetCell(87).SetCellValue(_item.C87)
            row.GetCell(88).SetCellValue(_item.C88)
            row.GetCell(89).SetCellValue(_item.C89)
            row.GetCell(90).SetCellValue(_item.C90)
            row.GetCell(91).SetCellValue(_item.C91)
            row.GetCell(92).SetCellValue(_item.C92)
            row.GetCell(93).SetCellValue(_item.C93.Value)
            row.GetCell(94).SetCellValue(_item.C94.Value)
            row.GetCell(95).SetCellValue(_item.C95.Value)
            row.GetCell(96).SetCellValue(_item.C96.Value)
            row.GetCell(97).SetCellValue(_item.C97.Value)
            row.GetCell(98).SetCellValue(_item.C98.Value)
            row.GetCell(99).SetCellValue(_item.C99)
            row.GetCell(100).SetCellValue(_item.C100)
            row.GetCell(101).SetCellValue(_item.C101)
            row.GetCell(102).SetCellValue(_item.C102)
            row.GetCell(103).SetCellValue(_item.C103)
            row.GetCell(104).SetCellValue(_item.C104)
            row.GetCell(105).SetCellValue(_item.C105)
            row.GetCell(106).SetCellValue(_item.C106)
            row.GetCell(107).SetCellValue(_item.C107)
            row.GetCell(108).SetCellValue(_item.C108)
            row.GetCell(109).SetCellValue(_item.C109)
            row.GetCell(110).SetCellValue(_item.C110)
            row.GetCell(111).SetCellValue(_item.C111)
            row.GetCell(112).SetCellValue(_item.C112)
            row.GetCell(113).SetCellValue(_item.C113)
            row.GetCell(114).SetCellValue(_item.C114)
            row.GetCell(115).SetCellValue(_item.C115)
            row.GetCell(116).SetCellValue(_item.C116)
            row.GetCell(117).SetCellValue(_item.C117)
            row.GetCell(118).SetCellValue(_item.C118)
            row.GetCell(119).SetCellValue(_item.C119)
            row.GetCell(120).SetCellValue(_item.C120)
            row.GetCell(121).SetCellValue(_item.C121)
            row.GetCell(122).SetCellValue(_item.C122)
            row.GetCell(123).SetCellValue(_item.C123)
            row.GetCell(124).SetCellValue(_item.C124)
            row.GetCell(125).SetCellValue(_item.C125)
            row.GetCell(126).SetCellValue(_item.C126)
            row.GetCell(127).SetCellValue(_item.C127)
            row.GetCell(128).SetCellValue(_item.C128)
            row.GetCell(129).SetCellValue(_item.C129)
            row.GetCell(130).SetCellValue(_item.C130)
            row.GetCell(131).SetCellValue(_item.C131)
            row.GetCell(132).SetCellValue(_item.C132)
            row.GetCell(133).SetCellValue(_item.C133)
            row.GetCell(134).SetCellValue(_item.C134)
            row.GetCell(135).SetCellValue(_item.C135)
            row.GetCell(136).SetCellValue(_item.C136)
            row.GetCell(137).SetCellValue(_item.C137)
            row.GetCell(138).SetCellValue(_item.C138)
            row.GetCell(139).SetCellValue(_item.C139)
            row.GetCell(140).SetCellValue(_item.C140)
            row.GetCell(141).SetCellValue(_item.C141)
            row.GetCell(142).SetCellValue(_item.C142)
            row.GetCell(143).SetCellValue(_item.C143)
            row.GetCell(144).SetCellValue(_item.C144)
            row.GetCell(145).SetCellValue(_item.C145)
            row.GetCell(146).SetCellValue(_item.C146)
            row.GetCell(147).SetCellValue(_item.C147)
            row.GetCell(148).SetCellValue(_item.C148)
            row.GetCell(149).SetCellValue(_item.C149)
            row.GetCell(150).SetCellValue(_item.C150.Value)
            row.GetCell(151).SetCellValue(_item.C151.Value)
            row.GetCell(152).SetCellValue(_item.C152.Value)
            row.GetCell(153).SetCellValue(_item.C153.Value)
            row.GetCell(154).SetCellValue(_item.C154)
            row.GetCell(155).SetCellValue(_item.C155)
            row.GetCell(156).SetCellValue(_item.C156)
            row.GetCell(157).SetCellValue(_item.C157)
            row.GetCell(158).SetCellValue(_item.C158)
            row.GetCell(159).SetCellValue(_item.C159)




            j += 1

        Next
        For i = 0 To _fields.Count - 1
            sheet1.AutoSizeColumn(i)
        Next


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


        Dim DataDate As String
        If Today.ToString("dddd").ToLower().Equals("monday") Then
            DataDate = String.Format("{0} - {1}", MyUtils.GenThaiDate(Today.AddDays(-3), 2), MyUtils.GenThaiDate(Today.AddDays(-1), 2))
        Else
            DataDate = MyUtils.GenThaiDate(Today.AddDays(-1), 2)
        End If

        Using dc_portal = New DataClasses_PortalDataContext()
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


            Dim _mailNotification = (From c In dc_portal.MailNotifications Where c.Code.Equals(_NoticeCode)).FirstOrDefault()
            strSubject = _mailNotification.MailSubject.Replace("{name}", _Insurer.UWName)
            strSubject = strSubject.Replace("{date}", String.Format("{0}", DataDate))


            Dim _MailBody As String = HttpUtility.HtmlDecode(_mailNotification.MailBody)

            _MailBody = _MailBody.Replace("{name}", _Insurer.UWName)
            _MailBody = _MailBody.Replace("{date}", DataDate)
            _MailBody = _MailBody.Replace("{itemno}", _SheetData.Count.ToString())


            _MailBody = _MailBody.Replace("{displayName}", _displayName)
            _MailBody = _MailBody.Replace("{title}", _title)
            _MailBody = _MailBody.Replace("{department}", _department)
            _MailBody = _MailBody.Replace("{company}", _company)
            _MailBody = _MailBody.Replace("{streetAddress}", _streetAddress)
            _MailBody = _MailBody.Replace("{telephoneNumber}", _telephoneNumber)
            _MailBody = _MailBody.Replace("{facsimileTelephoneNumber}", _facsimileTelephoneNumber)
            _MailBody = _MailBody.Replace("{mail}", _mail)

            strMessage.AppendLine(_MailBody)

            ''strMailFrom = _mailNotification.MailFrom
            ''strMailCC = _mailNotification.MailCC
            ''strMailTo = _agent.MailTo

            'strMailFrom = _mail
            ''strMailTo = System.Configuration.ConfigurationManager.AppSettings("mailto")
            ''strMailCC = System.Configuration.ConfigurationManager.AppSettings("mailcc")
            'strMailTo = _mailNotification.MailTo
            ''strMailCC = _mailNotification.MailCC

            ''strMailTo = _Insurer.MailTo
            ''strMailCC = IIf(String.IsNullOrEmpty(_Insurer.MailCC), "", _Insurer.MailCC & ";") & IIf(String.IsNullOrEmpty(_mailNotification.MailCC), "", _mailNotification.MailCC)

            strMailFrom = _mail
            strMailTo = _Insurer.MailTo
            strMailCC = IIf(String.IsNullOrEmpty(_Insurer.MailCC), "", _Insurer.MailCC & ";") & IIf(String.IsNullOrEmpty(_mailNotification.MailCC), "", _mailNotification.MailCC)

        End Using


        Dim MySmtpClient As New System.Net.Mail.SmtpClient(System.Configuration.ConfigurationManager.AppSettings("smtp").ToString())

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

        Dim _MailTo = strMailTo.Split(";") 'Mail To
        For Each item In _MailTo
            If Not String.IsNullOrEmpty(item.Trim()) Then msg.To.Add(item)
        Next

        Dim _MailCC = strMailCC.Split(";") 'Mail CC
        For Each item In _MailCC
            If Not String.IsNullOrEmpty(item.Trim()) Then msg.CC.Add(item)
        Next

        msg.BodyEncoding = Encoding.UTF8
        msg.IsBodyHtml = True
        msg.Priority = Net.Mail.MailPriority.High
        '==============Add an Attachment=========================

        Dim att_data = New Attachment(_fileDataPath)
        att_data.Name = String.Format("สรุปงานต่ออายุวันที่ {0} {1}.xls", DataDate, _Insurer.UWName)
        msg.Attachments.Add(att_data)

        Try

            MySmtpClient.Send(msg)
            Console.WriteLine("Send to : " & _Insurer.UWName)


        Catch ex As Exception
            Console.WriteLine("Error Sent to : " & _Insurer.UWName)
            WriteToSysLog("Error Sent to : " & _Insurer.UWName & ", Message" & ex.Message, True)
        End Try



    End Sub

    Private Sub WH_genxls(ByVal _UWCode As String, ByVal _Data As List(Of V_KAWA2_UW_Non_ASIA))
        Dim _NoticeCode As String = "B0030R"


        Dim _Insurer = (From c In _Data _
                          Where c.UWCode.Equals(_UWCode) _
                          ).FirstOrDefault()



        Console.WriteLine("Insurer :" & _Insurer.UWName)


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

        Dim Header_Style_SkyBlue As ICellStyle = hssfworkbook.CreateCellStyle()
        Header_Style_SkyBlue.SetFont(Header_Font)
        Header_Style_SkyBlue.Alignment = HorizontalAlignment.Center
        Header_Style_SkyBlue.FillPattern = FillPattern.SparseDots
        Header_Style_SkyBlue.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.SkyBlue.Index
        Header_Style_SkyBlue.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.SkyBlue.Index
        Header_Style_SkyBlue.BorderBottom = BorderStyle.Thin
        Header_Style_SkyBlue.BorderLeft = BorderStyle.Thin
        Header_Style_SkyBlue.BorderRight = BorderStyle.Thin
        Header_Style_SkyBlue.BorderTop = BorderStyle.Thin
        Header_Style_SkyBlue.BottomBorderColor = HSSFColor.Black.Index
        Header_Style_SkyBlue.LeftBorderColor = HSSFColor.Black.Index
        Header_Style_SkyBlue.RightBorderColor = HSSFColor.Black.Index
        Header_Style_SkyBlue.TopBorderColor = HSSFColor.Black.Index

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

        Dim Row_Style_Center As ICellStyle = hssfworkbook.CreateCellStyle()
        Row_Style_Center.SetFont(Row_Font)
        Row_Style_Center.Alignment = HorizontalAlignment.Center
        Row_Style_Center.BorderBottom = BorderStyle.Thin
        Row_Style_Center.BorderLeft = BorderStyle.Thin
        Row_Style_Center.BorderRight = BorderStyle.Thin
        Row_Style_Center.BorderTop = BorderStyle.Thin
        Row_Style_Center.BottomBorderColor = HSSFColor.Black.Index
        Row_Style_Center.LeftBorderColor = HSSFColor.Black.Index
        Row_Style_Center.RightBorderColor = HSSFColor.Black.Index
        Row_Style_Center.TopBorderColor = HSSFColor.Black.Index

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



        '====================== data new sheet ====================
        Dim sheet1 As ISheet = hssfworkbook.CreateSheet(_Insurer.UWName)

        '================== create column ===================
        Dim _fields = "No.,Broker_Code,PlanCode,PlanName,UWCode1,Fleet_Name,Introducer_Name,Client Code,คำนำหน้า,Insured Name,ID Card,Insured's Address,Beneficiary_Name,Model,License No,Vehicle Code,Year,Veh_ChassisNo,MVL Own Damage,MVL Deductible,MVL Net Premium,MVL Stamp,MVL Vat,MVL Total,MVL Effective,MVL Expire,ใบเสร็จ MVL ออกในนาม,เลขที่หนังสือรับรอง,เลขที่กรมธรรม์,วันที่ Lockton รับงาน,IDCard,เลขที่สัญญา,โทรศัพท์,Beneficiary,TPBIPerPerson,TPBIPerTime,TPPD,BLDriver,BLPassenger,MedicalFee,BailDriver,RenewYear".Split(",")
        Dim frow As IRow = sheet1.CreateRow(0)
        For i = 0 To _fields.Count - 1
            Dim cell = frow.CreateCell(i)
            cell.SetCellValue(_fields(i))
            cell.CellStyle = Header_Style_SkyBlue
        Next
        '================== create cell ===================
        Dim j As Integer = 1
        Dim _SheetData = (From c In _Data Where c.UWCode.Equals(_Insurer.UWCode)).ToList()

        For Each _item In _SheetData

            Dim row As IRow = sheet1.CreateRow(j)

            '========================== Create Row =============================
            row.CreateCell(0).CellStyle = Row_Style_Center
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
            row.CreateCell(12).CellStyle = Row_Style_Left
            row.CreateCell(13).CellStyle = Row_Style_Left
            row.CreateCell(14).CellStyle = Row_Style_Left
            row.CreateCell(15).CellStyle = Row_Style_Left
            row.CreateCell(16).CellStyle = Row_Style_Left
            row.CreateCell(17).CellStyle = Row_Style_Left
            row.CreateCell(18).CellStyle = Row_Style_Left
            row.CreateCell(19).CellStyle = Row_Style_Left
            row.CreateCell(20).CellStyle = Row_Style_Left
            row.CreateCell(21).CellStyle = Row_Style_Left
            row.CreateCell(22).CellStyle = Row_Style_Left
            row.CreateCell(23).CellStyle = Row_Style_Left
            row.CreateCell(24).CellStyle = Row_Style_Left
            row.CreateCell(25).CellStyle = Row_Style_Left
            row.CreateCell(26).CellStyle = Row_Style_Left
            row.CreateCell(27).CellStyle = Row_Style_Left
            row.CreateCell(28).CellStyle = Row_Style_Left
            row.CreateCell(29).CellStyle = Row_Style_Left
            row.CreateCell(30).CellStyle = Row_Style_Left
            row.CreateCell(31).CellStyle = Row_Style_Left
            row.CreateCell(32).CellStyle = Row_Style_Left
            row.CreateCell(33).CellStyle = Row_Style_Left
            row.CreateCell(34).CellStyle = Row_Style_Left
            row.CreateCell(35).CellStyle = Row_Style_Left
            row.CreateCell(36).CellStyle = Row_Style_Left
            row.CreateCell(37).CellStyle = Row_Style_Left
            row.CreateCell(38).CellStyle = Row_Style_Left
            row.CreateCell(39).CellStyle = Row_Style_Left
            row.CreateCell(40).CellStyle = Row_Style_Left
            row.CreateCell(41).CellStyle = Row_Style_Left

            '========================== Set Value =============================
            row.GetCell(0).SetCellValue(j)
            row.GetCell(1).SetCellValue(_item.Broker_Code)
            row.GetCell(2).SetCellValue(_item.PlanCode)
            row.GetCell(3).SetCellValue(_item.PlanName)
            row.GetCell(4).SetCellValue(_item.UWCode)
            row.GetCell(5).SetCellValue(_item.Fleet_Name)
            row.GetCell(6).SetCellValue(_item.Introducer_Name)
            row.GetCell(7).SetCellValue(_item.Client_Code)
            row.GetCell(8).SetCellValue(_item.cTitle)
            row.GetCell(9).SetCellValue(_item.Insured_Name)
            row.GetCell(10).SetCellValue(_item.ID_Card)
            row.GetCell(11).SetCellValue(_item.Insured_s_Address)
            row.GetCell(12).SetCellValue(_item.Beneficiary_Name)
            row.GetCell(13).SetCellValue(_item.Model)
            row.GetCell(14).SetCellValue(_item.License_No)
            row.GetCell(15).SetCellValue(_item.Vehicle_Code)
            row.GetCell(16).SetCellValue(_item.Year)
            row.GetCell(17).SetCellValue(_item.Veh_ChassisNo)
            row.GetCell(18).SetCellValue(_item.MVL_Own_Damage)
            row.GetCell(19).SetCellValue(_item.MVL_Deductible)
            row.GetCell(20).SetCellValue(_item.MVL_Net_Premium)
            row.GetCell(21).SetCellValue(_item.MVL_Stamp)
            row.GetCell(22).SetCellValue(_item.MVL_Vat)
            row.GetCell(23).SetCellValue(_item.MVL_Total.Value)
            row.GetCell(24).SetCellValue(_item.MVL_Effective)
            row.GetCell(25).SetCellValue(_item.MVL_Expire)
            row.GetCell(26).SetCellValue(_item.Billing)
            row.GetCell(27).SetCellValue(_item.TempPolicyNo)
            row.GetCell(28).SetCellValue(_item.PolicyNo)
            row.GetCell(29).SetCellValue(_item.DateMakePolicy.Value.ToString("dd/MM/yyyy"))
            row.GetCell(30).SetCellValue(_item.CIDCard)
            row.GetCell(31).SetCellValue(_item.ContractNo)
            row.GetCell(32).SetCellValue(_item.cPhone)
            row.GetCell(33).SetCellValue(_item.Beneficiary)
            row.GetCell(34).SetCellValue(_item.TPBIPerPerson)
            row.GetCell(35).SetCellValue(_item.TPBIPerTime)
            row.GetCell(36).SetCellValue(_item.TPPD)
            row.GetCell(37).SetCellValue(_item.BLDriver)
            row.GetCell(38).SetCellValue(_item.BLPassenger)
            row.GetCell(39).SetCellValue(_item.MedicalFee)
            row.GetCell(40).SetCellValue(_item.BailDriver)
            row.GetCell(41).SetCellValue(_item.RenewYear)


            'For i = 0 To 41
            '    row.Sheet.AutoSizeColumn(i)
            'Next

            j += 1

        Next
        For i = 0 To _fields.Count - 1
            sheet1.AutoSizeColumn(i)
        Next


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


        Dim DataDate As String
        If Today.ToString("dddd").ToLower().Equals("monday") Then
            DataDate = String.Format("{0} - {1}", MyUtils.GenThaiDate(Today.AddDays(-3), 2), MyUtils.GenThaiDate(Today.AddDays(-1), 2))
        Else
            DataDate = MyUtils.GenThaiDate(Today.AddDays(-1), 2)
        End If



        Using dc_portal = New DataClasses_PortalDataContext()
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




            Dim _mailNotification = (From c In dc_portal.MailNotifications Where c.Code.Equals(_NoticeCode)).FirstOrDefault()
            strSubject = _mailNotification.MailSubject.Replace("{name}", _Insurer.UWName)
            strSubject = strSubject.Replace("{date}", String.Format("{0}", DataDate))


            Dim _MailBody As String = HttpUtility.HtmlDecode(_mailNotification.MailBody)

            _MailBody = _MailBody.Replace("{name}", _Insurer.UWName)
            _MailBody = _MailBody.Replace("{date}", DataDate)
            _MailBody = _MailBody.Replace("{itemno}", _SheetData.Count.ToString())


            _MailBody = _MailBody.Replace("{displayName}", _displayName)
            _MailBody = _MailBody.Replace("{title}", _title)
            _MailBody = _MailBody.Replace("{department}", _department)
            _MailBody = _MailBody.Replace("{company}", _company)
            _MailBody = _MailBody.Replace("{streetAddress}", _streetAddress)
            _MailBody = _MailBody.Replace("{telephoneNumber}", _telephoneNumber)
            _MailBody = _MailBody.Replace("{facsimileTelephoneNumber}", _facsimileTelephoneNumber)
            _MailBody = _MailBody.Replace("{mail}", _mail)

            strMessage.AppendLine(_MailBody)

            ''strMailFrom = _mailNotification.MailFrom
            ''strMailCC = _mailNotification.MailCC
            ''strMailTo = _agent.MailTo

            'strMailFrom = _mail
            ''strMailTo = System.Configuration.ConfigurationManager.AppSettings("mailto")
            ''strMailCC = System.Configuration.ConfigurationManager.AppSettings("mailcc")

            'strMailTo = _mailNotification.MailTo
            'strMailCC = _mailNotification.MailCC

            ''strMailTo = _Insurer.MailTo
            ''strMailCC = IIf(String.IsNullOrEmpty(_Insurer.MailCC), "", _Insurer.MailCC & ";") & IIf(String.IsNullOrEmpty(_mailNotification.MailCC), "", _mailNotification.MailCC)


            strMailFrom = _mail
            strMailTo = _Insurer.MailTo
            strMailCC = IIf(String.IsNullOrEmpty(_Insurer.MailCC), "", _Insurer.MailCC & ";") & IIf(String.IsNullOrEmpty(_mailNotification.MailCC), "", _mailNotification.MailCC)

        End Using


        Dim MySmtpClient As New System.Net.Mail.SmtpClient(System.Configuration.ConfigurationManager.AppSettings("smtp").ToString())

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
        att_data.Name = String.Format("สรุปงานต่ออายุวันที่ {0} {1}.xls", DataDate, _Insurer.UWName)
        msg.Attachments.Add(att_data)

        Try

            MySmtpClient.Send(msg)
            Console.WriteLine("Send to : " & _Insurer.UWName)


        Catch ex As Exception
            Console.WriteLine("Error Sent to : " & _Insurer.UWName)
            WriteToSysLog("Error Sent to : " & _Insurer.UWName & ", Message" & ex.Message, True)
        End Try



    End Sub

    Private Sub WH_genxls_VIRI(ByVal _UWCode As String, ByVal _Data As Data.DataTable, ByVal _Insurer As Data.DataRow)
        Dim _NoticeCode As String = "B0030R"

        Dim _InsurerName = _Data.Rows(0)("บริษัทประกันภัย").ToString()

        Dim hssfworkbook As New HSSFWorkbook()

        '=====================cell style ===================
        Dim Header_Font = hssfworkbook.CreateFont()
        With Header_Font
            .FontName = "Calibri"
            .FontHeightInPoints = 10
            .Boldweight = FontBoldWeight.Bold
            .Color = NPOI.HSSF.Util.HSSFColor.White.Index
        End With
        Dim Header_Style As ICellStyle = hssfworkbook.CreateCellStyle()
        With Header_Style
            .SetFont(Header_Font)
            .Alignment = HorizontalAlignment.Center
            .FillPattern = FillPattern.SparseDots
            .FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightBlue.Index
            .FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.LightBlue.Index
            .BorderBottom = BorderStyle.Thin
            .BorderLeft = BorderStyle.Thin
            .BorderRight = BorderStyle.Thin
            .BorderTop = BorderStyle.Thin
            .BottomBorderColor = HSSFColor.Black.Index
            .LeftBorderColor = HSSFColor.Black.Index
            .RightBorderColor = HSSFColor.Black.Index
            .TopBorderColor = HSSFColor.Black.Index
        End With

        Dim Row_Font = hssfworkbook.CreateFont()
        With Row_Font
            .FontName = "Calibri"
            .FontHeightInPoints = 10
        End With

        Dim Row_Style As ICellStyle = hssfworkbook.CreateCellStyle()
        With Row_Style
            .SetFont(Row_Font)
            .BorderBottom = BorderStyle.Thin
            .BorderLeft = BorderStyle.Thin
            .BorderRight = BorderStyle.Thin
            .BorderTop = BorderStyle.Thin
            .BottomBorderColor = HSSFColor.Black.Index
            .LeftBorderColor = HSSFColor.Black.Index
            .RightBorderColor = HSSFColor.Black.Index
            .TopBorderColor = HSSFColor.Black.Index
        End With
        '====================== data new sheet ====================
        Dim sheet1 As ISheet = hssfworkbook.CreateSheet(_InsurerName)
        '================== create column ===================
        Dim frow As IRow = sheet1.CreateRow(0)
        For i = 0 To _Data.Columns.Count - 1
            Dim cell = frow.CreateCell(i)
            cell.SetCellValue(_Data.Columns(i).ColumnName)
            cell.CellStyle = Header_Style
        Next
        '================== create cell ===================
        Dim j As Integer = 1
        Dim _SheetData = _Data
        For Each _item As Data.DataRow In _SheetData.Rows
            Dim row As IRow = sheet1.CreateRow(j)
            '========================== Create Row and Set Value=============================
            For i = 0 To _SheetData.Columns.Count - 1
                row.CreateCell(i).CellStyle = Row_Style
                row.GetCell(i).SetCellValue(_item(i).ToString())
            Next
            j += 1
        Next
        For i = 0 To _SheetData.Columns.Count - 1
            sheet1.AutoSizeColumn(i)
        Next

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


        Dim DataDate As String
        If Today.ToString("dddd").ToLower().Equals("monday") Then
            DataDate = String.Format("{0} - {1}", MyUtils.GenThaiDate(Today.AddDays(-3), 2), MyUtils.GenThaiDate(Today.AddDays(-1), 2))
        Else
            DataDate = MyUtils.GenThaiDate(Today.AddDays(-1), 2)
        End If



        Using dc_portal = New DataClasses_PortalDataContext()
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




            Dim _mailNotification = (From c In dc_portal.MailNotifications Where c.Code.Equals(_NoticeCode)).FirstOrDefault()
            strSubject = _mailNotification.MailSubject.Replace("{name}", _InsurerName)
            strSubject = strSubject.Replace("{date}", String.Format("{0}", DataDate))


            Dim _MailBody As String = HttpUtility.HtmlDecode(_mailNotification.MailBody)

            _MailBody = _MailBody.Replace("{name}", _InsurerName)
            _MailBody = _MailBody.Replace("{date}", DataDate)
            _MailBody = _MailBody.Replace("{itemno}", _SheetData.Rows.Count.ToString())


            _MailBody = _MailBody.Replace("{displayName}", _displayName)
            _MailBody = _MailBody.Replace("{title}", _title)
            _MailBody = _MailBody.Replace("{department}", _department)
            _MailBody = _MailBody.Replace("{company}", _company)
            _MailBody = _MailBody.Replace("{streetAddress}", _streetAddress)
            _MailBody = _MailBody.Replace("{telephoneNumber}", _telephoneNumber)
            _MailBody = _MailBody.Replace("{facsimileTelephoneNumber}", _facsimileTelephoneNumber)
            _MailBody = _MailBody.Replace("{mail}", _mail)

            strMessage.AppendLine(_MailBody)

            ''strMailFrom = _mailNotification.MailFrom
            ''strMailCC = _mailNotification.MailCC
            ''strMailTo = _agent.MailTo

            'strMailFrom = _mail
            ''strMailTo = System.Configuration.ConfigurationManager.AppSettings("mailto")
            ''strMailCC = System.Configuration.ConfigurationManager.AppSettings("mailcc")

            'strMailTo = _mailNotification.MailTo
            'strMailCC = _mailNotification.MailCC

            ''strMailTo = _Insurer.MailTo
            ''strMailCC = IIf(String.IsNullOrEmpty(_Insurer.MailCC), "", _Insurer.MailCC & ";") & IIf(String.IsNullOrEmpty(_mailNotification.MailCC), "", _mailNotification.MailCC)


            strMailFrom = _mail
            strMailTo = _Insurer("MailTo").ToString()
            strMailCC = IIf(String.IsNullOrEmpty(_Insurer("MailCC").ToString()), "", _Insurer("MailCC").ToString() & ";") & IIf(String.IsNullOrEmpty(_mailNotification.MailCC), "", _mailNotification.MailCC)

        End Using


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
        att_data.Name = String.Format("สรุปงานต่ออายุวันที่ {0} {1}.xls", DataDate, _InsurerName)
        msg.Attachments.Add(att_data)

        Try

            MySmtpClient.Send(msg)
            Console.WriteLine("Send to : " & _InsurerName)


        Catch ex As Exception
            Console.WriteLine("Error Sent to : " & _InsurerName)
            WriteToSysLog("Error Sent to : " & _InsurerName & ", Message" & ex.Message, True)
        End Try



    End Sub


End Class
