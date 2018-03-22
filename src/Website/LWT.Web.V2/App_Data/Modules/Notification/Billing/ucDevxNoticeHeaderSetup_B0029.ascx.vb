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

'Imports iTextSharp.text.pdf

Partial Class Modules_ucDevxNoticeHeaderSetup_B0029

    Inherits PortalModuleControl
    Dim _NoticeCode As String = "B0029"
    '*******************************************************
    '
    ' The Page_Load server event handler on this user control is used
    ' to populate the current site settings from the config system
    '
    '*******************************************************
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Page.IsPostBack = False Then
            Using dc_portal As New DataClasses_PortalDataContextExt
                Dim _mailNotification = (From c In dc_portal.MailNotifications Where c.Code.Equals(_NoticeCode)).FirstOrDefault()

                lbNotificationTitle.Text = _mailNotification.Name

                lbNewTitle.Text = _mailNotification.Name

                tbxNewNoticeTitle.Text = _mailNotification.Name
            End Using

            BindData()
        End If
    End Sub
    Protected Sub btnExcelFormat_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcelFormat.Click
        Dim fileName = String.Format("{0}.xls", System.Guid.NewGuid.ToString())
        Dim filePath = String.Format("{0}\{1}", Server.MapPath("~/Template"), "Template for DealerBilling.xls")



        Response.ClearContent()
        Response.AddHeader("content-disposition", "attachment; filename=" & fileName)
        Response.ContentType = "application/octet-stream"
        Response.WriteFile(filePath)
        Response.End()

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
            Dim _data = (From c In dc.V_BillingDealerNotifications Where c.NotificationID.Equals(hdNotificationID.Text) Order By c.AgentCode).ToList()
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

        Grid1.Hidden = True
        pnNotification.Hidden = Not (Grid1.Hidden)

        hdNotificationID.Text = _NotificationID

        Using dc As New DataClasses_CPSExt
            Dim _data = (From c In dc.tblNoticeHeaders Where c.NoticeID.Equals(hdNotificationID.Text)).FirstOrDefault()


            Form1.Title = String.Format("{0} - {1} ({2})", hdNotificationID.Text, _data.NoticeTitle, _data.CreationDate.Value.ToString())


            lbCreationDate.Text = _data.CreationDate.Value.ToString()

            tbxNoticeTitle.Text = _data.NoticeTitle

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

        Using dc As New DataClasses_CPSExt

            Dim _newdata As New tblNoticeHeader With {.NoticeTitle = tbxNewNoticeTitle.Text _
                                                       , .NoticeCode = _NoticeCode _
                                                       , .NoticeDate = dpNewNoticeDate.SelectedDate _
                                                       , .CreationDate = DateTime.Now _
                                                       , .CreationBy = HttpContext.Current.User.Identity.Name _
                                                       , .DueDate = dpNewDueDate.SelectedDate _
                                                    }
            dc.tblNoticeHeaders.InsertOnSubmit(_newdata)
            dc.SubmitChanges()

        End Using


        BindData()
        WindowDataNew.Hidden = True
    End Sub


    Protected Sub btnSave_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Using dc As New DataClasses_CPSExt
            Dim _data = (From c In dc.tblNoticeHeaders Where c.NoticeID.Equals(hdNotificationID.Text)).FirstOrDefault()
            _data.DueDate = dpDueDate.SelectedDate
            '_data.DataFields = tbxDataFields.Text
            _data.NoticeDate = dpNoticeDate.SelectedDate
            _data.NoticeTitle = tbxNoticeTitle.Text
            dc.SubmitChanges()
            Alert.Show("Saved")
        End Using
    End Sub
    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Grid1.Hidden = False
        pnNotification.Hidden = Not (Grid1.Hidden)
        BindData()
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
                '[AgentCode],[ชื่อผู้เอาประกันภัย],[บริษัทประกันภัย],[Client code],[เลขตัวถัง],[วันคุ้มครอง],[ทุนประกัน],[เบี้ยสุทธิ],[อากร],[Vat],[เบี้ยรวมภาษีอากร],[ส่วนลด],[รวมเบี้ยที่ต้องชำระ],[WH Tax 1%],[เบี้ยสุทธิที่ต้องชำระ]
                _BillingNotificationData.Add(New tblNoticeDetail With {.NoticeID = hdNotificationID.Text _
                                                                                , .Code = _row(0).Trim() _
                                                                                , .f01 = _row(1).Trim() _
                                                                                , .f02 = _row(2).Trim() _
                                                                                , .f03 = _row(3).Trim() _
                                                                                , .f04 = _row(4).Trim() _
                                                                                , .f05 = _row(5).Trim() _
                                                                                , .f06 = _row(6).Trim() _
                                                                                , .f07 = _row(7).Trim() _
                                                                                , .f08 = _row(8).Trim() _
                                                                                , .f09 = _row(9).Trim() _
                                                                                , .f10 = _row(10).Trim() _
                                                                                , .f11 = _row(11).Trim() _
                                                                                , .f12 = _row(12).Trim() _
                                                                                , .f13 = _row(13).Trim() _
                                                                                , .f14 = _row(14).Trim() _
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
            Dim _data = (From c In dc.tblNoticeHeaders Where c.NoticeID.Equals(NotificationID)).FirstOrDefault()

            ''Dim myString = "http://www.MySampleCode.com"

            'Dim code128 = New Barcode128()
            'code128.CodeType = Barcode.CODE128
            'code128.ChecksumText = True
            'code128.GenerateChecksum = True
            'code128.Code = "*045746201627080857*"

            'Dim bm = New System.Drawing.Bitmap(code128.CreateDrawingImage(System.Drawing.Color.Black, System.Drawing.Color.White))

            'bm.Save(Context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif)



            'Dim code128 As New Barcode128()
            'code128.setCode(myString.trim())
            'code128.setCodeType(Barcode128.CODE128)
            'Dim code128Image As Image = code128.CreateImageWithBarcode(cb, Nothing, Nothing)
            'code128Image.setAbsolutePosition(10, 700)
            'code128Image.scalePercent(125)
            'doc.add(code128Image)

            'code128.setCodeType(Barcode128.CODE128_UCC)
            'code128Image = code128.CreateImageWithBarcode(cb, Nothing, Nothing)
            'code128Image.setAbsolutePosition(10, 650)
            'code128Image.scalePercent(125)
            'doc.add(code128Image)

            'Dim codeEAN As New BarcodeEAN()
            'codeEAN.setCode(myString.trim())
            'codeEAN.setCodeType(BarcodeEAN.EAN13)
            'Dim codeEANImage As Image = code128.CreateImageWithBarcode(cb, Nothing, Nothing)
            'codeEANImage.setAbsolutePosition(10, 600)
            'codeEANImage.scalePercent(125)
            'doc.add(codeEANImage)

            'Dim qrcode As New BarcodeQRCode(myString.trim(), 1, 1, Nothing)
            'Dim qrcodeImage As Image = qrcode.GetImage()
            'qrcodeImage.setAbsolutePosition(10, 500)
            'qrcodeImage.scalePercent(200)
            'doc.add(qrcodeImage)

            'Dim c128A = New Code128ABarcode("*045746201627080857*")


            Dim _agentdata = (From c In dc.tblNoticeMailContacts _
                              Where c.NoticeCode.Equals(_NoticeCode) _
                              And c.Code.Equals(AgentCode) _
                              ).FirstOrDefault()



            Dim hssfworkbook As New HSSFWorkbook()

            '=====================cell style ===================
            Dim Header_Font = hssfworkbook.CreateFont()
            Header_Font.FontName = "Tahoma"
            Header_Font.FontHeightInPoints = 8
            Header_Font.Boldweight = FontBoldWeight.Bold

            Dim Header_Style As ICellStyle = hssfworkbook.CreateCellStyle()
            Header_Style.SetFont(Header_Font)
            Header_Style.Alignment = HorizontalAlignment.Center
            Header_Style.FillPattern = FillPattern.SparseDots
            Header_Style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index
            Header_Style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index
            Header_Style.BorderBottom = BorderStyle.Thin
            Header_Style.BorderLeft = BorderStyle.Thin
            Header_Style.BorderRight = BorderStyle.Thin
            Header_Style.BorderTop = BorderStyle.Thin
            Header_Style.BottomBorderColor = HSSFColor.Black.Index
            Header_Style.LeftBorderColor = HSSFColor.Black.Index
            Header_Style.RightBorderColor = HSSFColor.Black.Index
            Header_Style.TopBorderColor = HSSFColor.Black.Index



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
            Row_Style_Center.SetFont(Row_Font)
            Row_Style_Center.BorderBottom = BorderStyle.Thin
            Row_Style_Center.BorderLeft = BorderStyle.Thin
            Row_Style_Center.BorderRight = BorderStyle.Thin
            Row_Style_Center.BorderTop = BorderStyle.Thin
            Row_Style_Center.BottomBorderColor = HSSFColor.Black.Index
            Row_Style_Center.LeftBorderColor = HSSFColor.Black.Index
            Row_Style_Center.RightBorderColor = HSSFColor.Black.Index
            Row_Style_Center.TopBorderColor = HSSFColor.Black.Index

            Dim Row_Style_Right As ICellStyle = hssfworkbook.CreateCellStyle()
            Row_Style_Right.SetFont(Row_Font)
            Row_Style_Right.Alignment = HorizontalAlignment.Right
            Row_Style_Right.SetFont(Row_Font)
            Row_Style_Right.BorderBottom = BorderStyle.Thin
            Row_Style_Right.BorderLeft = BorderStyle.Thin
            Row_Style_Right.BorderRight = BorderStyle.Thin
            Row_Style_Right.BorderTop = BorderStyle.Thin
            Row_Style_Right.BottomBorderColor = HSSFColor.Black.Index
            Row_Style_Right.LeftBorderColor = HSSFColor.Black.Index
            Row_Style_Right.RightBorderColor = HSSFColor.Black.Index
            Row_Style_Right.TopBorderColor = HSSFColor.Black.Index

            Dim Row_Font_BOLD = hssfworkbook.CreateFont()
            Row_Font_BOLD.FontName = "Tahoma"
            Row_Font_BOLD.FontHeightInPoints = 8
            Row_Font_BOLD.Boldweight = FontBoldWeight.Bold

            Dim Row_Style_Right_BOLD As ICellStyle = hssfworkbook.CreateCellStyle()
            Row_Style_Right_BOLD.SetFont(Row_Font_BOLD)
            Row_Style_Right_BOLD.Alignment = HorizontalAlignment.Right
            Row_Style_Right_BOLD.BorderBottom = BorderStyle.Thin
            Row_Style_Right_BOLD.BorderLeft = BorderStyle.Thin
            Row_Style_Right_BOLD.BorderRight = BorderStyle.Thin
            Row_Style_Right_BOLD.BorderTop = BorderStyle.Thin
            Row_Style_Right_BOLD.BottomBorderColor = HSSFColor.Black.Index
            Row_Style_Right_BOLD.LeftBorderColor = HSSFColor.Black.Index
            Row_Style_Right_BOLD.RightBorderColor = HSSFColor.Black.Index
            Row_Style_Right_BOLD.TopBorderColor = HSSFColor.Black.Index



            Dim Row_Style_Right_Float As HSSFCellStyle = hssfworkbook.CreateCellStyle
            Row_Style_Right_Float.SetFont(Row_Font)
            Row_Style_Right_Float.Alignment = HorizontalAlignment.Right
            Row_Style_Right_Float.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,#0.0")

            Dim Row_Style_Right_Float_Bold As HSSFCellStyle = hssfworkbook.CreateCellStyle
            Row_Style_Right_Float_Bold.SetFont(Row_Font_BOLD)
            Row_Style_Right_Float_Bold.Alignment = HorizontalAlignment.Right
            Row_Style_Right_Float_Bold.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,#0.0")



            Dim Row_Style_BOLD As ICellStyle = hssfworkbook.CreateCellStyle()
            Row_Style_BOLD.SetFont(Row_Font_BOLD)

            '====================== data new sheet ====================
            Dim sheet1 As ISheet = hssfworkbook.CreateSheet(_agentdata.Name)


            '================== create row ===================
            Dim j As Integer = 0
            Dim row1 As IRow = sheet1.CreateRow(j)
            row1.CreateCell(0).CellStyle = Row_Style_BOLD
            row1.GetCell(0).SetCellValue("บริษัท ล็อคตั้น วัฒนา อินชัวรันส์ โบรคเกอร์ส (ประเทศไทย) จำกัด")

            '================== create row ===================
            j += 1
            Dim row2 As IRow = sheet1.CreateRow(j)
            row2.CreateCell(0).CellStyle = Row_Style_BOLD
            row2.GetCell(0).SetCellValue("รายการวางบิลดีลเลอร์")
            '================== create row ===================
            j += 1
            Dim row3 As IRow = sheet1.CreateRow(j)
            row3.CreateCell(0).CellStyle = Row_Style_BOLD
            row3.GetCell(0).SetCellValue("วันที่วางบิล")

            row3.CreateCell(1).CellStyle = Row_Style_BOLD
            row3.GetCell(1).SetCellValue(MyUtils.GenThaiDate(_data.NoticeDate, 2))

            row3.CreateCell(2).CellStyle = Row_Style_BOLD
            row3.GetCell(2).SetCellValue("กำหนดชำระ")

            row3.CreateCell(3).CellStyle = Row_Style_BOLD
            row3.GetCell(3).SetCellValue(MyUtils.GenThaiDate(_data.DueDate, 2))
            '================== create row ===================
            j += 1
            Dim row4 As IRow = sheet1.CreateRow(j)
            row4.CreateCell(0).CellStyle = Row_Style_BOLD
            row4.GetCell(0).SetCellValue("ดีลเลอร์ ")

            row4.CreateCell(1).CellStyle = Row_Style_BOLD
            row4.GetCell(1).SetCellValue(_agentdata.Name)



            '================== create column ===================
            j += 1
            Dim _fields = "ลำดับที่,ชื่อผู้เอาประกันภัย,บริษัทประกันภัย,Client code,เลขตัวถัง,วันคุ้มครอง,ทุนประกัน,เบี้ยสุทธิ,อากร,Vat,เบี้ยรวมภาษีอากร,ส่วนลด,รวมเบี้ยที่ต้องชำระ,WH Tax 1%,เบี้ยสุทธิที่ต้องชำระ".Split(",")
            Dim frow As IRow = sheet1.CreateRow(j)
            For i = 0 To _fields.Count - 1
                Dim cell = frow.CreateCell(i)
                cell.SetCellValue(_fields(i))
                cell.CellStyle = Header_Style
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

            sheet1.SetColumnWidth(0, _item_f01_Length * 350)
            sheet1.SetColumnWidth(1, _item_f02_Length * 300)
            sheet1.SetColumnWidth(2, _item_f03_Length * 250)
            sheet1.SetColumnWidth(3, _item_f04_Length * 300)
            sheet1.SetColumnWidth(4, _item_f05_Length * 450)
            sheet1.SetColumnWidth(5, _item_f06_Length * 300)
            sheet1.SetColumnWidth(6, _item_f07_Length * 300)

            sheet1.SetColumnWidth(7, _item_f08_Length * 250)
            sheet1.SetColumnWidth(8, _item_f09_Length * 600)
            sheet1.SetColumnWidth(9, _item_f10_Length * 600)
            sheet1.SetColumnWidth(10, _item_f11_Length * 200)
            sheet1.SetColumnWidth(11, _item_f12_Length * 350)
            sheet1.SetColumnWidth(12, _item_f13_Length * 200)
            sheet1.SetColumnWidth(13, _item_f14_Length * 350)
            sheet1.SetColumnWidth(14, _item_f15_Length * 200)

            '================== create cell ===================

            Dim _agentdata_New = (From c In dc.tblNoticeDetails _
                          Where c.NoticeID.Equals(NotificationID) _
                          And c.Code.Equals(AgentCode)
                          ).ToList()
            j += 1

            Dim irow As Integer = 0

            Dim sum_f7 As Decimal = 0
            Dim sum_f8 As Decimal = 0
            Dim sum_f9 As Decimal = 0
            Dim sum_f10 As Decimal = 0
            Dim sum_f11 As Decimal = 0
            Dim sum_f12 As Decimal = 0
            Dim sum_f13 As Decimal = 0
            Dim sum_f14 As Decimal = 0


            For Each _item In _agentdata_New
                irow += 1
                Dim row As IRow = sheet1.CreateRow(j)

                '========================== Data =============================
                row.CreateCell(0).CellStyle = Row_Style_Center
                row.CreateCell(1).CellStyle = Row_Style_Left
                row.CreateCell(2).CellStyle = Row_Style_Left
                row.CreateCell(3).CellStyle = Row_Style_Left
                row.CreateCell(4).CellStyle = Row_Style_Left
                row.CreateCell(5).CellStyle = Row_Style_Right
                row.CreateCell(6).CellStyle = Row_Style_Right
                row.CreateCell(7).CellStyle = Row_Style_Right
                row.CreateCell(8).CellStyle = Row_Style_Right
                row.CreateCell(9).CellStyle = Row_Style_Right
                row.CreateCell(10).CellStyle = Row_Style_Right
                row.CreateCell(11).CellStyle = Row_Style_Right
                row.CreateCell(12).CellStyle = Row_Style_Right
                row.CreateCell(13).CellStyle = Row_Style_Right
                row.CreateCell(14).CellStyle = Row_Style_Right

                row.GetCell(0).SetCellValue(irow.ToString())
                row.GetCell(1).SetCellValue(_item.f01.Trim())
                row.GetCell(2).SetCellValue(_item.f02.Trim())
                row.GetCell(3).SetCellValue(_item.f03.Trim())
                row.GetCell(4).SetCellValue(_item.f04.Trim())
                row.GetCell(5).SetCellValue(_item.f05.Trim())
                row.GetCell(6).SetCellValue(Convert.ToDecimal(_item.f06.Trim()).ToString("###,##0"))
                row.GetCell(7).SetCellValue(Convert.ToDecimal(_item.f07.Trim()).ToString("###,##0.00"))
                row.GetCell(8).SetCellValue(Convert.ToDecimal(_item.f08.Trim()).ToString("###,##0.00"))
                row.GetCell(9).SetCellValue(Convert.ToDecimal(_item.f09.Trim()).ToString("###,##0.00"))
                row.GetCell(10).SetCellValue(Convert.ToDecimal(_item.f10.Trim()).ToString("###,##0.00"))
                row.GetCell(11).SetCellValue(Convert.ToDecimal(_item.f11.Trim()).ToString("###,##0.00"))
                row.GetCell(12).SetCellValue(Convert.ToDecimal(_item.f12.Trim()).ToString("###,##0.00"))
                row.GetCell(13).SetCellValue(Convert.ToDecimal(_item.f13.Trim()).ToString("###,##0.00"))
                row.GetCell(14).SetCellValue(Convert.ToDecimal(_item.f14.Trim()).ToString("###,##0.00"))



                sum_f7 += Convert.ToDecimal(_item.f07)
                sum_f8 += Convert.ToDecimal(_item.f08)
                sum_f9 += Convert.ToDecimal(_item.f09)
                sum_f10 += Convert.ToDecimal(_item.f10)
                sum_f11 += Convert.ToDecimal(_item.f11)
                sum_f12 += Convert.ToDecimal(_item.f12)
                sum_f13 += Convert.ToDecimal(_item.f13)
                sum_f14 += Convert.ToDecimal(_item.f14)

                j += 1

            Next
            '============= sum total =============
            Dim lrow As IRow = sheet1.CreateRow(j)
            lrow.CreateCell(5).CellStyle = Row_Style_Right_BOLD
            lrow.GetCell(5).SetCellValue("Total")

            lrow.CreateCell(6).CellStyle = Row_Style_Right_BOLD
            lrow.GetCell(6).SetCellValue(Convert.ToDecimal(irow).ToString("###,##0"))

            lrow.CreateCell(7).CellStyle = Row_Style_Right_BOLD
            lrow.GetCell(7).SetCellValue(Convert.ToDecimal(sum_f7).ToString("###,##0.00"))

            lrow.CreateCell(8).CellStyle = Row_Style_Right_BOLD
            lrow.GetCell(8).SetCellValue(Convert.ToDecimal(sum_f8).ToString("###,##0.00"))

            lrow.CreateCell(9).CellStyle = Row_Style_Right_BOLD
            lrow.GetCell(9).SetCellValue(Convert.ToDecimal(sum_f9).ToString("###,##0.00"))

            lrow.CreateCell(10).CellStyle = Row_Style_Right_BOLD
            lrow.GetCell(10).SetCellValue(Convert.ToDecimal(sum_f10).ToString("###,##0.00"))

            lrow.CreateCell(11).CellStyle = Row_Style_Right_BOLD
            lrow.GetCell(11).SetCellValue(Convert.ToDecimal(sum_f11).ToString("###,##0.00"))

            lrow.CreateCell(12).CellStyle = Row_Style_Right_BOLD
            lrow.GetCell(12).SetCellValue(Convert.ToDecimal(sum_f12).ToString("###,##0.00"))

            lrow.CreateCell(13).CellStyle = Row_Style_Right_BOLD
            lrow.GetCell(13).SetCellValue(Convert.ToDecimal(sum_f13).ToString("###,##0.00"))

            lrow.CreateCell(14).CellStyle = Row_Style_Right_BOLD
            lrow.GetCell(14).SetCellValue(Convert.ToDecimal(sum_f14).ToString("###,##0.00"))


            Dim _AgentName As String = _agentdata.Name
            '====================== LetterBillingToDealer ==================================
            Dim pdfTemplate As String = String.Format("{0}/{1}", Server.MapPath("~/template"), "/LetterBillingToDealer.pdf")
            Dim _PdfFileName = String.Format("LetterBillingToDealer_{0}.pdf", System.Guid.NewGuid().ToString())
            Dim newPdfFile As String = String.Format("{0}\{1}", Server.MapPath("~/upload"), _PdfFileName)

            Dim pdfReader As New PdfReader(pdfTemplate)
            Dim pdfStamper As New PdfStamper(pdfReader, New FileStream(newPdfFile, FileMode.Create))
            Dim pdfFormFields As AcroFields = pdfStamper.AcroFields
          
            pdfFormFields.SetField("noticedate", MyUtils.GenThaiDate(_data.NoticeDate, 2))
            pdfFormFields.SetField("dealername", _AgentName)

            pdfFormFields.SetField("items", Convert.ToDecimal(irow).ToString("###,##0"))
            pdfFormFields.SetField("netpremium", Convert.ToDecimal(sum_f10).ToString("###,##0.00"))
            pdfFormFields.SetField("discount", Convert.ToDecimal(sum_f11).ToString("###,##0.00"))
            pdfFormFields.SetField("wh1", Convert.ToDecimal(sum_f13).ToString("###,##0.00"))
            pdfFormFields.SetField("totalpremium", Convert.ToDecimal(sum_f14).ToString("###,##0.00"))
            pdfFormFields.SetField("duedate", MyUtils.GenThaiDate(_data.DueDate, 2))

            pdfFormFields.SetField("lwtname1", "คุณศิริลักษณ์ พรมมา")
            pdfFormFields.SetField("lwtname2", "คุณศิริลักษณ์ พรมมา")

            pdfFormFields.SetField("lwtextno", "4610")

            pdfFormFields.SetField("lwtposition", "ผู้จัดการแผนกอาวุโส")
            pdfFormFields.SetField("lwtdepartment", "ฝ่ายประกันภัยธุรกิจรายย่อยและวิสาหกิจขนาดย่อม")


            pdfFormFields.GenerateAppearances = False
            pdfStamper.FormFlattening = False
            pdfStamper.FreeTextFlattening = True
            pdfStamper.Close()
            pdfReader.Close()

            '========================Payement================================
            pdfTemplate = String.Format("{0}/{1}", Server.MapPath("~/template"), "/PaymentForm.pdf")
            _PdfFileName = String.Format("PaymentForm_{0}.pdf", System.Guid.NewGuid().ToString())
            Dim newPaymentPdfFile = String.Format("{0}\{1}", Server.MapPath("~/upload"), _PdfFileName)
            pdfReader = New PdfReader(pdfTemplate)
            pdfStamper = New PdfStamper(pdfReader, New FileStream(newPaymentPdfFile, FileMode.Create))
            pdfFormFields = pdfStamper.AcroFields

            Dim _ascClientCode = _agentdata.Code
            Dim _BC_CitiPrefixCode = "010052800012721"
            Dim _BC_BBLPrefixCode = "010552701132400"
            Dim _BC_Ref1 = "116258"
            Dim _BC_Ref3 = "0"

            'contactno1,contactno2
            'if {?TelNo}="APD1186" then "หมายเลขโทรศัพท์ 0-2353-7100 หรือโทรสาร 0-2353-7003-4"
            'else if {?TelNo}="APD1187" then "หมายเลขโทรศัพท์ 0-2353-7100 หรือโทรสาร 0-2353-7005-6"
            'else "หมายเลขโทรศัพท์ 0-2635-5000 หรือโทรสาร 0-2635-5114"

            'if {?TelNo}="APD1186" then "Tel. 0-2353-7100 or Fax 0-2353-7003-4"
            'else if {?TelNo}="APD1187" then "Tel. 0-2353-7100 or Fax 0-2353-7005-6"
            'else "Tel. 0-2635-5000 or Fax 0-2635-5114"

            pdfFormFields.SetField("contactno1", "หมายเลขโทรศัพท์ 0-2353-7100 หรือโทรสาร 0-2353-7005-6")
            pdfFormFields.SetField("contactno2", "Tel. 0-2353-7100 or Fax 0-2353-7005-6")


            'clientcode1,clientname1,ref1a,ref1b
            pdfFormFields.SetField("clientcode1", "(" & _ascClientCode & ")")
            pdfFormFields.SetField("clientname1", _agentdata.Name)
            pdfFormFields.SetField("ref1a", _BC_Ref1)
            pdfFormFields.SetField("ref1b", _ascClientCode)
            'clientcode2,clientname2,ref2a,ref2b
            pdfFormFields.SetField("clientcode2", "(" & _ascClientCode & ")")
            pdfFormFields.SetField("clientname2", _agentdata.Name)
            pdfFormFields.SetField("ref2a", _BC_Ref1)
            pdfFormFields.SetField("ref2b", _ascClientCode)

            pdfFormFields.SetField("note1", "สำหรับธนาคารกรุงเทพ และ ธนาคารกสิกรไทย เท่านั้น ")
            pdfFormFields.SetField("note2", "สำหรับธนาคารอื่นๆ/ที่ทำการไปรษณีย์/สำนักบริการเอไอเอส ")


            Dim overContent As PdfContentByte = pdfStamper.GetOverContent(1)



            Dim code128_1 As New Barcode128()
            code128_1.Code = String.Format("|{0}{1}{2}{3}", _BC_BBLPrefixCode, _BC_Ref1, _ascClientCode, _BC_Ref3)
            code128_1.StartStopText = False
            code128_1.Size = 7.0F
            Dim barcode1 As iTextSharp.text.Image = code128_1.CreateImageWithBarcode(overContent, Nothing, Nothing)
            barcode1.ScaleToFit(600, 33)
            barcode1.SetAbsolutePosition(41, 41)
            overContent.AddImage(barcode1)



            Dim code128_2 As New Barcode128()
            code128_2.Code = String.Format("|{0}{1}{2}{3}", _BC_CitiPrefixCode, _BC_Ref1, _ascClientCode, _BC_Ref3)
            code128_2.StartStopText = False
            code128_2.Size = 7.0F
            Dim barcode2 As iTextSharp.text.Image = code128_2.CreateImageWithBarcode(overContent, Nothing, Nothing)
            barcode2.ScaleToFit(600, 33)
            barcode2.SetAbsolutePosition(385, 41)
            overContent.AddImage(barcode2)


            pdfFormFields.GenerateAppearances = False
            pdfStamper.FormFlattening = False
            pdfStamper.FreeTextFlattening = True
            pdfStamper.Close()
            pdfReader.Close()
            ''========================================================
















            '================== init  sheet property =================

            Dim _filename = String.Format("รอบวางบิล-{0}_{1}.xls", _AgentName, DateTime.Now.ToString("yyyyMMddHHmmss"))
            Dim _fileDataPath = String.Format("{0}\{1}", Server.MapPath("~/upload"), _filename)
            Dim fs As New FileStream(_fileDataPath, FileMode.Create)
            hssfworkbook.Write(fs)
            fs.Close()

            Try
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


                    Dim _mailNotification = (From c In dc_portal.MailNotifications Where c.Code.Equals(_data.NoticeCode)).FirstOrDefault()
                    'strSubject = _mailNotification.MailSubject.Replace("{dealername}", _AgentName).Replace("{noticedate}", MyUtils.GenThaiDate(_data.NoticeDate, 2))

                    strSubject = (_data.NoticeTitle & " ({dealername}) - {noticedate}").Replace("{dealername}", _AgentName).Replace("{noticedate}", MyUtils.GenThaiDate(_data.NoticeDate, 2))



                    'strMessage.AppendLine("<!DOCTYPE html><html><body>")
                    'Dim NoticeDate1 = New Date(_data.NoticeDate.Value.Year, _data.NoticeDate.Value.Month, 1)                         
                    'Dim NoticeDate2 As Date = New Date(NoticeDate1.Year, NoticeDate1.Month, Date.DaysInMonth(NoticeDate1.Year, NoticeDate1.Month))

                    'strMessage.AppendLine(Server.HtmlDecode(_mailNotification.MailBody).Replace("{freetext}", _data.NoticeTitle).Replace("{duedate1}", MyUtils.GenThaiDate(_data.DueDate, 2)).Replace("{duedate2}", MyUtils.GenThaiDate(_data.DueDate, 2)).Replace("{duedate3}", MyUtils.GenThaiDate(_data.DueDate, 2)))
                    'strMessage.AppendLine("</body></html>")


                    Dim _MailBody As String = Server.HtmlDecode(_mailNotification.MailBody).Replace("{noticedate}", MyUtils.GenThaiDate(_data.NoticeDate, 2))

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
                    strMailTo = _agent.MailTo
                    strMailCC = _agent.MailCC & ";" & _mailNotification.MailCC & ";" & _mail

                  
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

                Dim att_letter = New Attachment(newPdfFile)
                att_letter.Name = "ใบวางบิล.pdf"
                msg.Attachments.Add(att_letter)

                Dim att_payment = New Attachment(newPaymentPdfFile)
                att_payment.Name = "แบบฟอร์มการชำระเงิน.pdf"
                msg.Attachments.Add(att_payment)

                Dim att_data = New Attachment(_fileDataPath)
                att_data.Name = String.Format("{0}.xls", _AgentName)
                msg.Attachments.Add(att_data)

                MySmtpClient.Send(msg)


                Alert.Show("Send")
            Catch ex As Exception
                Throw ex
            End Try

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

    ''btnAddFile
    'Protected Sub btnAddFile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddFile.Click
    '    WindowUploaddata.Title = "Data"
    '    WindowUploaddata.Hidden = False
    'End Sub

    'Protected Sub TabStrip1_TabIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabStrip1.TabIndexChanged
    '    'Select Case TabStrip1.ActiveTabIndex
    '    '    Case 1
    '    '        Tab2.IFrameUrl = "http://www.google.co.th"
    '    '        Tab2.RefreshIFrame()
    '    '    Case 2
    '    '        Tab3.IFrameUrl = "http://www.sanook.com"
    '    '        Tab3.RefreshIFrame()
    '    'End Select
    'End Sub

    Public Sub New()

    End Sub
End Class
