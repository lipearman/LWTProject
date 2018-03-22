Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports FineUI
Imports System.IO
Imports System.Web.Mail
Imports System.Net.Mail

Imports NPOI.HSSF.UserModel
Imports NPOI.HPSF
Imports NPOI.SS.UserModel
Imports NPOI.SS.Util
Imports NPOI.HSSF.Util
Imports System.Drawing
Imports iTextSharp.text.pdf

Partial Class Modules_ucDevxNoticeHeaderSetup_B0025

    Inherits PortalModuleControl
    Dim _NoticeCode As String = "B0025"
    '*******************************************************
    '
    ' The Page_Load server event handler on this user control is used
    ' to populate the current site settings from the config system
    '
    '*******************************************************
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Page.IsPostBack = False Then

            'btnAddFile.OnClientClick = Window1.GetShowReference("~/Modules/PLUpload/BillingMNTUpload.aspx", "Module File")

            BindData()
        End If
    End Sub
    Private Sub BindData()
        Dim column As FineUI.GridColumn = Grid1.FindColumn(Grid1.SortField)
        BindGridWithSort(column.SortField, Grid1.SortDirection)
    End Sub
    Private Sub BindGridWithSort(ByVal sortField As String, ByVal sortDirection As String)
        Using dc As New DataClasses_CPSExt()
            Dim _data = dc.ExecuteQuery(Of MyContext.NoticeHeader)("select top 1000 * from tblNoticeHeader where NoticeCode='" & _NoticeCode & "' ").ToList()
            Dim table As DataTable = MyUtils.EQToDataTable(_data)
            Dim view1 As DataView = table.DefaultView
            If _data.Count > 0 Then
                view1.Sort = String.Format("{0} {1}", sortField, sortDirection)
            End If
            Grid1.DataSource = view1
            Grid1.DataBind()
        End Using
    End Sub

    Private Sub BindAgentData()
        Using dc As New DataClasses_CPSExt
            Dim _data = (From c In dc.V_WeeklyClaimDealerNotifications Where c.NotificationID.Equals(hdNotificationID.Text) Order By c.AgentCode).ToList()
            Grid3.DataSource = _data
            Grid3.DataBind()

        End Using

    End Sub
    Private Sub BindViewAgentDataNew(ByVal AgentCode As String)

        Using dc As New DataClasses_CPSExt
            Dim _data = (From c In dc.tblNoticeDetails Where c.Code.Equals(AgentCode) _
                   And c.NoticeID.Equals(hdNotificationID.Text)).ToList()
            dgData.DataSource = _data
            dgData.DataBind()


        End Using
    End Sub
    Protected Sub Grid1_Sort(ByVal sender As Object, ByVal e As FineUI.GridSortEventArgs) Handles Grid1.Sort
        BindGridWithSort(e.SortField, e.SortDirection)
    End Sub
    Protected Sub Grid1_PageIndexChange(ByVal sender As Object, ByVal e As FineUI.GridPageEventArgs) Handles Grid1.PageIndexChange
        Grid1.PageIndex = e.NewPageIndex
        BindData()
    End Sub
    Protected Sub Grid1_RowDoubleClick(sender As Object, e As FineUI.GridRowClickEventArgs) Handles Grid1.RowDoubleClick

        Dim _NotificationID As String = Grid1.DataKeys(e.RowIndex + (Grid1.PageIndex * Grid1.PageSize))(0)

        'btnAddFile.OnClientClick = Window1.GetShowReference("~/Modules/PLUpload/BillingMNTUpload.aspx?ID={0}" & _NotificationID, "Add File")

        Grid1.Hidden = True
        pnNotification.Hidden = Not (Grid1.Hidden)

        hdNotificationID.Text = _NotificationID

        Using dc As New DataClasses_CPSExt
            Dim _data = (From c In dc.tblNoticeHeaders Where c.NoticeID.Equals(hdNotificationID.Text)).FirstOrDefault()

            Form1.Title = String.Format("{0} - {1} ({2})", hdNotificationID.Text, _data.NoticeTitle, _data.CreationDate.Value.ToString())


            lbNotificationTitle.Text = _data.NoticeTitle
            lbCreationDate.Text = _data.CreationDate.Value.ToString()

            lbSendDate.Text = ""
            If _data.SendDate IsNot Nothing Then
                lbSendDate.Text = _data.SendDate.ToString()
                btnResend.Hidden = False
                btnSend.Hidden = True

            Else
                btnResend.Hidden = True
                btnSend.Hidden = False
            End If

            lbReSendDate.Text = ""
            If _data.ReSendDate IsNot Nothing Then
                lbReSendDate.Text = _data.ReSendDate.ToString()
            End If

            If _data.NoticeDate IsNot Nothing Then
                dpNoticeDate.SelectedDate = _data.NoticeDate
            End If
            If _data.DueDate IsNot Nothing Then
                dpDueDate.SelectedDate = _data.DueDate
            End If

        End Using

        BindAgentData()


    End Sub
    Protected Sub Grid3_RowDoubleClick(sender As Object, e As FineUI.GridRowClickEventArgs) Handles Grid3.RowDoubleClick
        Dim _AgentCode As String = Grid3.DataKeys(e.RowIndex + (Grid3.PageIndex * Grid3.PageSize))(0)
        Dim _AgentName As String = Grid3.DataKeys(e.RowIndex + (Grid3.PageIndex * Grid3.PageSize))(1)

        BindViewAgentDataNew(_AgentCode)

        WindowViewAgentData.Title = String.Format("{0} - {1}", _AgentCode, _AgentName)
        WindowViewAgentData.Hidden = False

    End Sub
    Protected Sub Grid3_RowCommand(ByVal sender As Object, ByVal e As FineUI.GridCommandEventArgs) Handles Grid3.RowCommand
        Dim _AgentCode As String = Grid3.DataKeys(e.RowIndex + (Grid3.PageIndex * Grid3.PageSize))(0)
        Dim _AgentName As String = Grid3.DataKeys(e.RowIndex + (Grid3.PageIndex * Grid3.PageSize))(1)

        Select Case e.CommandName
            Case "btnSend"
                WH_genxls(hdNotificationID.Text, _AgentCode)
        End Select
    End Sub
    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        PageContext.Refresh()
    End Sub
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        WindowDataNew.Hidden = False
    End Sub
    Protected Sub btnSaveNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveNew.Click



        'If Not fuAttatch.HasFile Then
        '    Alert.Show("กรุณาแนบไฟล์")
        '    Return
        'Else
        '    If Not Right(fuAttatch.ShortFileName, 3).ToLower().Equals("pdf") Then
        '        Alert.Show("แนบไฟล์เฉพาะ pdf เท่านั้น")
        '        Return
        '    End If
        'End If

        'Dim fileName As String = fuAttatch.ShortFileName


        Using dc As New DataClasses_CPSExt
            ', .DataFields = "[AgentCode],[ดีลเลอร์],[ผู้เอาประกันภัย],[เลขตัวถัง],[เลขที่กรมธรรม์],[เลขที่เคลม],[วันที่เกิดเหตุ],[ สถานที่เกิดเหตุ ],[วันคุ้มครอง],[ผู้รับผลประโยชน์],[ประกันภัย]" _
            Dim _newdata As New tblNoticeHeader With {.NoticeTitle = "ข้อมูลตรวจสอบเคลม การเกิดเหตุรายสัปดาห์" _
                                                       , .NoticeCode = _NoticeCode _
                                                       , .CreationDate = DateTime.Now _
                                                       , .CreationBy = HttpContext.Current.User.Identity.Name _
                                                       , .NoticeDate = dpNewNoticeDate.SelectedDate _
                                                       , .DueDate = dpNewDueDate.SelectedDate _
                                                    }
            dc.tblNoticeHeaders.InsertOnSubmit(_newdata)
            dc.SubmitChanges()
            'btnSaveNew

            'Dim newPdfFile As String = String.Format("{0}\{1}", Server.MapPath("~/upload"), String.Format("{0}.pdf", _newdata.NotificationID))
            'If File.Exists(newPdfFile) Then
            '    My.Computer.FileSystem.DeleteFile(newPdfFile)
            'End If
            'fuAttatch.SaveAs(newPdfFile)

            'CreateLetterToDealerFile(_newdata.NotificationID, _newdata.NoticeDate, _newdata.DueDate)

        End Using


        BindData()
        WindowDataNew.Hidden = True
    End Sub

    'Private Sub CreateLetterToDealerFile(ByVal _NotificationID As String, ByVal _NoticeDate As DateTime, ByVal _DueDate As DateTime)
    '    Dim pdfTemplate As String = String.Format("{0}/{1}", Server.MapPath("~/upload"), "/LetterToDealerMaster.pdf")
    '    Dim _PdfFileName = String.Format("LetterToDealer_{0}.pdf", _NotificationID)
    '    Dim newPdfFile As String = String.Format("{0}/{1}", Server.MapPath("~/upload"), _PdfFileName)

    '    If File.Exists(newPdfFile) Then
    '        My.Computer.FileSystem.DeleteFile(newPdfFile)
    '    End If

    '    Dim pdfReader As New PdfReader(pdfTemplate)
    '    Dim pdfStamper As New PdfStamper(pdfReader, New FileStream(newPdfFile, FileMode.Create))
    '    Dim pdfFormFields As AcroFields = pdfStamper.AcroFields
    '    pdfFormFields.SetField("untitled1", Utils.GenThaiDate(_NoticeDate, 2))
    '    pdfFormFields.SetField("untitled2", String.Format("ภายในวันที่ {0}", Utils.GenThaiDate(_DueDate, 2)))
    '    pdfFormFields.SetField("untitled3", "02-353-7666 คุณรังสินี")
    '    pdfStamper.Close()


    'End Sub

    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Using dc As New DataClasses_CPSExt
            Dim _data = (From c In dc.tblNoticeHeaders Where c.NoticeID.Equals(hdNotificationID.Text)).FirstOrDefault()

            _data.DueDate = dpDueDate.SelectedDate
            _data.NoticeDate = dpNoticeDate.SelectedDate
            dc.SubmitChanges()
            Alert.Show("Saved")
        End Using
    End Sub
    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Grid1.Hidden = False
        pnNotification.Hidden = Not (Grid1.Hidden)
    End Sub
    Protected Sub btnSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSend.Click, btnResend.Click

        If Grid3.SelectedRowIndexArray.Length > 0 Then
            Dim sb As New StringBuilder()
            For Each _item In Grid3.SelectedRowIndexArray
                'sb.Append(Grid3.DataKeys(_item)(0))
                Dim AgentCode = Grid3.DataKeys(_item)(0)
                WH_genxls(hdNotificationID.Text, AgentCode)

            Next
            Using dc As New DataClasses_CPSExt
                Dim _data = (From c In dc.tblNoticeHeaders Where c.NoticeID.Equals(hdNotificationID.Text)).FirstOrDefault()
                If _data IsNot Nothing Then

                    If _data.SendDate Is Nothing Then
                        _data.SendDate = DateTime.Now()
                    Else
                        _data.ReSendDate = DateTime.Now()
                    End If


                    dc.SubmitChanges()
                End If
            End Using
            Alert.Show("done")
        Else
            Alert.Show("No data select")
        End If
    End Sub
    Protected Sub btnUploadData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadData.Click
        WindowUploaddata.Title = "Data"
        WindowUploaddata.Hidden = False
    End Sub
    Private Sub importdata()

        Using dc As New DataClasses_CPSExt
            Dim _BillingNotificationData As New List(Of tblNoticeDetail)

            Dim i As Integer = 0
            Dim reader = New StringReader(tbxdata.Text)
            Dim line As String
            line = reader.ReadLine()

            While line IsNot Nothing
                Dim _row() As String = line.Split(vbTab)
                Dim _f02 As String = _row(1).ToString()

                'If _f02.IndexOf("จำกัด") > -1 Then
                '    Dim _temp = _f02.Replace("จำกัด", "|").Split("|")
                '    _f02 = _temp(0) & "จำกัด"
                'End If

                '0 [AgentCode]
                '1 [ดีลเลอร์]
                '2 [ผู้เอาประกันภัย]
                '3 [เลขตัวถัง]
                '4 [เลขที่กรมธรรม์]
                '5 [เลขที่เคลม]
                '6 [วันที่เกิดเหตุ]
                '7 [สถานที่เกิดเหตุ ]
                '8 [วันคุ้มครอง]
                '9 [ผู้รับผลประโยชน์]
                '10 [ประกันภัย]

                '11.ผู้แจ้งเคลม
                '12.เบอร์โทรผู้แจ้งเคลม

                _BillingNotificationData.Add(New tblNoticeDetail With {.NoticeID = hdNotificationID.Text _
                                                                                    , .Code = _row(0) _
                                                                                    , .f01 = _row(1) _
                                                                                    , .f02 = _row(2) _
                                                                                    , .f03 = _row(3) _
                                                                                    , .f04 = _row(4) _
                                                                                    , .f05 = _row(5) _
                                                                                    , .f06 = _row(6) _
                                                                                    , .f07 = _row(7) _
                                                                                    , .f08 = _row(8) _
                                                                                    , .f09 = _row(9) _
                                                                                    , .f10 = _row(10) _
                                                                                    , .f11 = _row(11) _
                                                                                    , .f12 = _row(12) _
                                                                               })



                line = reader.ReadLine()
            End While


            dc.ExecuteCommand("delete from tblNoticeDetail where NoticeID={0}", hdNotificationID.Text)

            dc.tblNoticeDetails.InsertAllOnSubmit(_BillingNotificationData)

            dc.SubmitChanges()

            Alert.Show(_BillingNotificationData.Count)
        End Using

        tbxdata.Text = ""

        WindowUploaddata.Hidden = True
    End Sub
    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Select Case WindowUploaddata.Title
            Case "Data"
                importdata()

        End Select

        BindAgentData()
    End Sub
    Protected Sub btnDeleteData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteData.Click
        If Grid3.SelectedRowIndexArray.Length > 0 Then
            For Each _item In Grid3.SelectedRowIndexArray
                Dim AgentCode = Grid3.DataKeys(_item)(0)
                deleteAgentData(hdNotificationID.Text, AgentCode)
            Next
            BindAgentData()
            Alert.Show("done")
        Else
            Alert.Show("No select data")
        End If
    End Sub

    Private Sub WH_genxls(ByVal NotificationID As String, ByVal AgentCode As String)

        Using dc As New DataClasses_CPSExt

            Dim _agentdata = (From c In dc.V_WeeklyClaimDealerNotifications _
                              Where c.NotificationID.Equals(NotificationID) _
                              And c.AgentCode.Equals(AgentCode) _
                              ).FirstOrDefault()


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
            Dim sheet1 As ISheet = hssfworkbook.CreateSheet(_agentdata.AgentName)
            '================== create column ===================
            Dim _fields = "ดีลเลอร์,ผู้เอาประกันภัย,เลขตัวถัง,เลขที่กรมธรรม์,เลขที่เคลม,วันที่เกิดเหตุ,สถานที่เกิดเหตุ,วันคุ้มครอง,ผู้รับผลประโยชน์,ประกันภัย,วันที่ติดต่อ,กรณีเข้าซ่อมแล้วกรุณาระบุชื่ออู่,ลูกค้าเข้าซ่อมเอง(ระบุ '/'),ประกันภัยแนะนำ(ระบุ '/'),ยังไม่เข้าซ่อม(ระบุ '/'),หมายเหตุ(อื่นๆ)".Split(",")
            Dim frow As IRow = sheet1.CreateRow(0)
            For i = 0 To _fields.Count - 1
                Dim cell = frow.CreateCell(i)
                cell.SetCellValue(_fields(i))

                Select Case i
                    Case 10
                        cell.CellStyle = Header_Style_AQUA
                    Case 11, 12, 13
                        cell.CellStyle = Header_Style_PINK
                    Case 14
                        cell.CellStyle = Header_Style_ORANGE
                    Case 15
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
            'Dim _item_f17_Length = frow.GetCell(16).StringCellValue.Length
            'Dim _item_f18_Length = frow.GetCell(17).StringCellValue.Length

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
            'sheet1.SetColumnWidth(16, _item_f17_Length * 400)
            'sheet1.SetColumnWidth(17, _item_f18_Length * 400) 
            '================== create cell ===================
            Dim j As Integer = 1
            Dim _agentdata_New = (From c In dc.tblNoticeDetails _
                          Where c.NoticeID.Equals(NotificationID) _
                          And c.Code.Equals(AgentCode) _
                          ).ToList()

            For Each _item In _agentdata_New

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

                row.GetCell(0).SetCellValue(_item.f01.Trim())
                row.GetCell(1).SetCellValue(_item.f02.Trim())
                row.GetCell(2).SetCellValue(_item.f03.Trim())
                row.GetCell(3).SetCellValue(_item.f04.Trim())
                row.GetCell(4).SetCellValue(_item.f05.Trim())
                row.GetCell(5).SetCellValue(_item.f06.Trim())
                row.GetCell(6).SetCellValue(_item.f07.Trim())
                row.GetCell(7).SetCellValue(_item.f08.Trim())
                row.GetCell(8).SetCellValue(_item.f09.Trim())
                row.GetCell(9).SetCellValue(_item.f10.Trim())

                'row.GetCell(10).SetCellValue(_item.f11.Trim())
                'row.GetCell(11).SetCellValue(_item.f12.Trim())




                row.CreateCell(10).CellStyle = Header_Style_AQUA
                row.CreateCell(11).CellStyle = Header_Style_PINK
                row.CreateCell(12).CellStyle = Header_Style_PINK
                row.CreateCell(13).CellStyle = Header_Style_PINK
                row.CreateCell(14).CellStyle = Header_Style_ORANGE
                row.CreateCell(15).CellStyle = Header_Style_GREEN

                row.GetCell(10).SetCellValue("")
                row.GetCell(11).SetCellValue("")
                row.GetCell(12).SetCellValue("")
                row.GetCell(13).SetCellValue("")
                row.GetCell(14).SetCellValue("")
                row.GetCell(15).SetCellValue("")



                j += 1

            Next

            Dim _AgentNameTemp = Left(_agentdata.AgentName, _agentdata.AgentName.IndexOf("จำกัด"))
            Dim _AgentName As String = _AgentNameTemp & "จำกัด"

            '================== init  sheet property =================
            Dim _data = (From c In dc.tblNoticeHeaders Where c.NoticeID.Equals(NotificationID)).FirstOrDefault()

            Dim _filename = String.Format("{0}.xls", System.Guid.NewGuid())
            Dim _fileDataPath = String.Format("{0}\{1}", Server.MapPath("~/upload"), _filename)
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


            Dim _agent = (From c In dc.tblNoticeMailContacts Where c.Code.Equals(AgentCode) And c.NoticeCode.Equals(_NoticeCode)).FirstOrDefault()
            Dim strMailFrom As String = ""
            Dim strMailCC As String = ""
            Dim strMailTo As String = "" '"dusit@asia.lockton.com;yutthanan@asia.lockton.com"
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

                Dim _mailNotification = (From c In dc_portal.MailNotifications Where c.Code.Equals(_data.NoticeCode)).FirstOrDefault()
                strSubject = _mailNotification.MailSubject.Replace("{Name}", _AgentName)
                strSubject = strSubject.Replace("{date}", String.Format("{0} - {1}", MyUtils.GenThaiDate(dpNoticeDate.SelectedDate.Value, 2), MyUtils.GenThaiDate(dpDueDate.SelectedDate.Value, 2)))

                'strMessage.AppendLine("<!DOCTYPE html><html><body>")
                'strMessage.AppendLine(Server.HtmlDecode(_mailNotification.MailBody.Replace("{date}", String.Format("{0} - {1}", Utils.GenThaiDate(dpNoticeDate.SelectedDate.Value, 2), Utils.GenThaiDate(dpDueDate.SelectedDate.Value, 2)))))
                'strMessage.AppendLine("</body></html>")


                Dim _MailBody As String = Server.HtmlDecode(_mailNotification.MailBody.Replace("{date}", String.Format("{0} - {1}", MyUtils.GenThaiDate(dpNoticeDate.SelectedDate.Value, 2), MyUtils.GenThaiDate(dpDueDate.SelectedDate.Value, 2))))

                _MailBody = _MailBody.Replace("{displayName}", _displayName)
                _MailBody = _MailBody.Replace("{title}", _title)
                _MailBody = _MailBody.Replace("{department}", _department)
                _MailBody = _MailBody.Replace("{company}", _company)
                _MailBody = _MailBody.Replace("{streetAddress}", _streetAddress)
                _MailBody = _MailBody.Replace("{telephoneNumber}", _telephoneNumber)
                _MailBody = _MailBody.Replace("{facsimileTelephoneNumber}", _facsimileTelephoneNumber)
                _MailBody = _MailBody.Replace("{mail}", _mail)

                strMessage.AppendLine(_MailBody)

                strMailFrom = _mailNotification.MailFrom
                strMailCC = _mailNotification.MailCC
                strMailTo = _agent.MailTo


            End Using


            Dim MySmtpClient As New System.Net.Mail.SmtpClient(ConfigurationSettings.AppSettings("smtp"))
            'MySmtpClient.Credentials = New System.Net.NetworkCredential("Your-username", "Your-password")

            'Dim msg As New System.Net.Mail.MailMessage(strMailFrom, strMailTo, strSubject, strMessage.ToString())
            Dim msg As New System.Net.Mail.MailMessage()

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
            att_data.Name = String.Format("{0}.xls", _AgentName)
            msg.Attachments.Add(att_data)

            MySmtpClient.Send(msg)


            Alert.Show("Send")
            'Catch ex As Exception
            '    Throw ex
            'End Try

        End Using



    End Sub

    Private Sub deleteAgentData(ByVal NotificationID As String, ByVal AgentCode As String)
        Using dc As New DataClasses_CPSExt

            Dim _agentdatas = (From c In dc.tblNoticeDetails Where c.NoticeID.Equals(NotificationID) And c.Code.Equals(AgentCode)).ToList()
            If _agentdatas.Count > 0 Then

                dc.tblNoticeDetails.DeleteAllOnSubmit(_agentdatas)
                dc.SubmitChanges()

            End If

        End Using
    End Sub

    Protected Sub btnDeleteAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteAll.Click
        If Grid1.SelectedRowIndexArray.Length > 0 Then
            Using dc As New DataClasses_CPSExt
                For Each _item In Grid1.SelectedRowIndexArray
                    Dim NotificationID = Grid1.DataKeys(_item)(0)
                    '[Billing.NotificationData]
                    dc.ExecuteCommand("delete from tblNoticeDetail where NoticeID=" & NotificationID)
                    '[Billing.Notification]
                    dc.ExecuteCommand("delete from tblNoticeHeader where NoticeID=" & NotificationID)
                Next
            End Using
            BindData()
            Alert.Show("done")
        Else
            Alert.Show("No select data")
        End If
    End Sub


End Class
