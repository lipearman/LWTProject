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

Partial Class Modules_ucDevxNoticeHeaderSetup_B0019R
    Inherits PortalModuleControl
    Dim _NoticeCode As String = "B0019R"
    '*******************************************************
    '
    ' The Page_Load server event handler on this user control is used
    ' to populate the current site settings from the config system
    '
    '*******************************************************
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Page.IsPostBack = False Then
            'BindEmailNotification()
            BindData()
        End If
    End Sub

    'Private Sub BindEmailNotification()

    '    Using dc As New DataClasses_PortalDataContext(webconfig._PortalConn)
    '        Dim _data = (From c In dc.MailNotifications Where c.Code.Equals(_MailNotifications)).FirstOrDefault()
    '        MainRegionPanel.Title = _data.Name
    '        lbNewTitle.Text = _data.Name
    '        Grid1.Title = _data.Name
    '    End Using


    'End Sub
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


    Private Sub BindInsurerData()
        Using dc As New DataClasses_CPSExt
            Dim _data = (From c In dc.tblNoticeDetails _
                         Where c.NoticeID.Equals(hdNotificationID.Text) _
                         Group By UWCode = c.f11, UWName = c.f12 _
                         Into g = Group _
                         Select UWCode, UWName, Clients = g.Count() _
                   ).ToList()
            Grid3.DataSource = _data
            Grid3.DataBind()
        End Using
    End Sub


    Protected Sub Grid1_Sort(ByVal sender As Object, ByVal e As FineUI.GridSortEventArgs) Handles Grid1.Sort
        BindGridWithSort(e.SortField, e.SortDirection)
    End Sub
    Protected Sub Grid1_PageIndexChange(ByVal sender As Object, ByVal e As FineUI.GridPageEventArgs) Handles Grid1.PageIndexChange
        Grid1.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Protected Sub Grid1_RowCommand(ByVal sender As Object, ByVal e As FineUI.GridCommandEventArgs) Handles Grid1.RowCommand
        Dim _NotificationID As String = Grid1.DataKeys(e.RowIndex)(0)

        Select Case e.CommandName
            Case "btnUpload"
                hdNotificationID.Text = _NotificationID
                WindowUploaddata.Title = "Data"
                WindowUploaddata.Hidden = False

            Case "btnExport"

                Using dc As New DataClasses_CPSExt

                    Dim _data = (From c In dc.tblNoticeDetails Where c.NoticeID.Equals(_NotificationID)).ToList()

                    If _data.Count > 0 Then
                        ExportData(_NotificationID)
                    Else
                        Alert.Show("No Data")
                    End If

                End Using



            Case "btnSendMail"

                Using dc As New DataClasses_CPSExt

                    Dim _data = (From c In dc.tblNoticeDetails Where c.NoticeID.Equals(_NotificationID)).ToList()

                    If _data.Count > 0 Then

                        Dim _Insurer = (From c In dc.tblNoticeDetails _
                           Where c.NoticeID.Equals(_NotificationID) _
                           Group By UWCode = c.f11, UWName = c.f12 _
                           Into g = Group _
                           Select UWCode, UWName, Clients = g.Count() _
                            ).ToList()

                        For Each iInsurer In _Insurer
                            WH_genxls(_NotificationID, iInsurer.UWCode)
                        Next

                        Dim _itemdata = (From c In dc.tblNoticeHeaders Where c.NoticeID.Equals(_NotificationID)).FirstOrDefault()
                        If _itemdata IsNot Nothing Then

                            If _itemdata.SendDate Is Nothing Then
                                _itemdata.SendDate = DateTime.Now()
                                _itemdata.SendBy = HttpContext.Current.User.Identity.Name.ToString()
                            Else
                                _itemdata.ReSendDate = DateTime.Now()
                                _itemdata.ReSendBy = HttpContext.Current.User.Identity.Name.ToString()
                            End If


                            dc.SubmitChanges()
                        End If
                        BindData()

                        Alert.Show("done")


                    Else
                        Alert.Show("No Data")
                    End If

                End Using

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
            ' , .DataFields = "No,Client Code,ผู้เอาประกันภัย,New,Policy No. หรือ เลขรับแจ้ง,หมายเลขตัวถัง/ทะเบียนรถ, SumInsure , VMI Premium ,Beneficiary,Insurer,ShowRoom,Type,Amendment Details,รายการข้อมูลเดิม,รายการที่แก้ไขใหม่" _
            Dim _newdata As New tblNoticeHeader With {.NoticeTitle = "แจ้งยกเลิกกรมธรรม์" _
                                                       , .NoticeCode = _NoticeCode _
                                                       , .CreationDate = DateTime.Now _
                                                       , .CreationBy = HttpContext.Current.User.Identity.Name _
                                                       , .NoticeDate = dpNewNoticeDate.SelectedDate _
                                                    }
            dc.tblNoticeHeaders.InsertOnSubmit(_newdata)
            dc.SubmitChanges()

        End Using
        BindData()
        WindowDataNew.Hidden = True
    End Sub
    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Grid1.Hidden = False
        pnNotification.Hidden = Not (Grid1.Hidden)
    End Sub
    Protected Sub Grid3_RowCommand(ByVal sender As Object, ByVal e As FineUI.GridCommandEventArgs) Handles Grid3.RowCommand
        Dim _UWCode As String = Grid3.DataKeys(e.RowIndex + (Grid3.PageIndex * Grid3.PageSize))(0)

        Select Case e.CommandName
            Case "btnSend"
                WH_genxls(hdNotificationID.Text, _UWCode)
        End Select
    End Sub
    Protected Sub btnUploadData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadData.Click
        WindowUploaddata.Title = "Data"
        WindowUploaddata.Hidden = False
    End Sub

    Private Sub importdata()
        Dim sb As New StringBuilder()

        Dim _BillingNotificationData As New List(Of tblNoticeDetail)

        Dim i As Integer = 0
        Dim reader = New StringReader(tbxdata.Text)
        Dim line As String
        line = reader.ReadLine()


        Using dc As New DataClasses_TIDMaster_LWTReportsExt

            While line IsNot Nothing

                Dim sb_Error As New StringBuilder()

                Dim _row() As String = line.Split(vbTab)
                Dim _IDCodeCRV = _row(0).ToString().Trim() 'IDCodeCRV
                Dim _clientcode = _row(1).ToString().Trim() 'Client Code
                Dim _freetext1 = _row(2).ToString().Trim() 'ยกเลิก/ถอน
                Dim _freetext2 = _row(3).ToString().Trim() 'ให้มีผล
                Dim _freetext3 = _row(4).ToString().Trim() 'สาเหตุ 
                Dim _freetext4 = _row(5).ToString().Trim() 'แจ้งเบี้ยถอนประกัน
                Dim _freetext5 = _row(6).ToString().Trim() 'หมายเหตุ 

                'Dim _data = dc_TIDMotor.SP_APPLICATION_SEARCH(_IDCodeCRV).FirstOrDefault()
                '_clientname = _data.ClientName
                Dim _data = (From c In dc.V_M1_Applications Where c.REFNO.Equals(_IDCodeCRV)).FirstOrDefault()


                If _data Is Nothing Then
                    sb_Error.AppendFormat("Client: '{0}', IDCodeCRV: {1} ;ไม่พบข้อมูล<br>", _clientcode, _IDCodeCRV)
                End If

                If Not String.IsNullOrEmpty(sb_Error.ToString()) Then
                    sb.Append(sb_Error.ToString())
                Else
                    Dim _itemdata As New tblNoticeDetail()
                    With _itemdata

                        .NoticeID = hdNotificationID.Text
                        'Client Code,ClientName,New/Renew,EffectiveDate,เลขรับแจ้ง(NLT New),เลขตัวถัง(New NLT),ยกเลิก/ถอน,ให้มีผล,สาเหตุ
                        .Code = _data.ClientCode 'Code Client Code
                        .f01 = _data.ClientName 'ClientName
                        .f02 = _data.RNType 'New/Renew
                        .f03 = _data.StartDate.Value.ToString("dd/MM/yyyy") 'EffectiveDate
                        .f04 = IIf(_data.RNType.Equals("N"), _data.PolicyNo, _data.CarPlate) 'Policy No. หรือ เลขรับแจ้ง, ทะเบียนรถ
                        .f05 = _data.CHASSIS   'หมายเลขตัวถัง 
                        .f06 = _freetext1 'ยกเลิก/ถอน
                        .f07 = _freetext2 'ให้มีผล
                        Dim _Remarks = _freetext3.Split("|")
                        .f08 = _Remarks(1) 'สาเหตุ
                        .f09 = _freetext4 'แจ้งเบี้ยถอนประกัน
                        .f10 = _freetext5 'หมายเหตุ


                        Using dc_cps As New DataClasses_CPSExt
                            'Select Case _data.Motor
                            '    Case "M1"
                            Dim _uw = (From c In dc_cps.tblNoticeMailContacts Where c.M1Code.Equals(_data.InsurerCode) And c.NoticeCode.Equals(_NoticeCode)).FirstOrDefault()
                            .f11 = _uw.Code.Trim() 'UWCode                                     
                            .f12 = _uw.Name.Trim() 'InsurerName

                            '    Case "M2"
                            '        .f11 = _data.InsurerCode
                            '        Dim _uw = (From c In dc_cps.tblNoticeMailContacts Where c.Code.Equals(.f11) And c.NoticeCode.Equals(_NoticeCode)).FirstOrDefault()
                            '        .f12 = _uw.Name.Trim() 'InsurerName

                            'End Select
                        End Using
                    End With
                    _BillingNotificationData.Add(_itemdata)
                End If


                line = reader.ReadLine()
            End While
        End Using


        If Not String.IsNullOrEmpty(sb.ToString()) Then
            Alert.Show(sb.ToString())
        Else
            Using dc As New DataClasses_CPSExt

                dc.ExecuteCommand("delete from tblNoticeDetail where NoticeID={0}", hdNotificationID.Text)

                dc.tblNoticeDetails.InsertAllOnSubmit(_BillingNotificationData)

                dc.SubmitChanges()

                Alert.Show(_BillingNotificationData.Count)
            End Using

            tbxdata.Text = ""

            WindowUploaddata.Hidden = True
        End If

    End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Select Case WindowUploaddata.Title
            Case "Data"
                importdata()

        End Select

        'BindInsurerData()
    End Sub
    Protected Sub btnExcelFormat_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcelFormat.Click
        Dim fileName = String.Format("{0}.xls", System.Guid.NewGuid.ToString())
        Dim filePath = String.Format("{0}\{1}", Server.MapPath("~/Template"), "Template for Cancellation.xls")



        Response.ClearContent()
        Response.AddHeader("content-disposition", "attachment; filename=" & fileName)
        Response.ContentType = "application/octet-stream"
        Response.WriteFile(filePath)
        Response.End()

    End Sub

    Private Sub deleteClientData(ByVal NotificationID As String, ByVal ClientCode As String)
        Using dc As New DataClasses_CPSExt

            Dim _datas = (From c In dc.tblNoticeDetails Where c.NoticeID.Equals(NotificationID) And c.Code.Equals(ClientCode)).ToList()
            If _datas.Count > 0 Then

                dc.tblNoticeDetails.DeleteAllOnSubmit(_datas)
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

            Grid1.Hidden = False
            pnNotification.Hidden = Not (Grid1.Hidden)
        Else
            Alert.Show("No select data")
        End If
    End Sub

    Protected Sub Grid3_RowDoubleClick(sender As Object, e As FineUI.GridRowClickEventArgs) Handles Grid3.RowDoubleClick
        Dim _UWCode As String = Grid3.DataKeys(e.RowIndex + (Grid3.PageIndex * Grid3.PageSize))(0)
        Dim _UWName As String = Grid3.DataKeys(e.RowIndex + (Grid3.PageIndex * Grid3.PageSize))(1)

        BindViewData(_UWCode)

        WindowViewData.Title = String.Format("{0} - {1}", _UWCode, _UWName)
        WindowViewData.Hidden = False

    End Sub

    Private Sub BindViewData(ByVal UWCode As String)
        Using dc As New DataClasses_CPSExt
            Dim _data = (From c In dc.tblNoticeDetails Where c.f11.Equals(UWCode) And c.NoticeID.Equals(hdNotificationID.Text)).ToList()
            dgData.DataSource = _data
            dgData.DataBind()
        End Using
    End Sub

    Protected Sub btnDeleteData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteData.Click
        If Grid3.SelectedRowIndexArray.Length > 0 Then
            For Each _item In Grid3.SelectedRowIndexArray
                Dim UWCode = Grid3.DataKeys(_item)(0)
                deleteData(hdNotificationID.Text, UWCode)
            Next
            BindInsurerData()
            Alert.Show("done")
        Else
            Alert.Show("No select data")
        End If
    End Sub
    Private Sub deleteData(ByVal NotificationID As String, ByVal UWCode As String)
        Using dc As New DataClasses_CPSExt

            Dim _data = (From c In dc.tblNoticeDetails Where c.NoticeID.Equals(NotificationID) And c.f11.Equals(UWCode)).ToList()
            If _data.Count > 0 Then

                dc.tblNoticeDetails.DeleteAllOnSubmit(_data)
                dc.SubmitChanges()

            End If

        End Using
    End Sub



    Private Sub WH_genxls(ByVal NotificationID As String, ByVal UWCode As String)

        Using dc As New DataClasses_CPSExt


            Dim hssfworkbook As New HSSFWorkbook()

            '=====================cell style ===================
            Dim Header_Font = hssfworkbook.CreateFont()
            Header_Font.FontName = "Tahoma"
            Header_Font.FontHeightInPoints = 10
            Header_Font.Boldweight = FontBoldWeight.Bold

            Dim Header_Style As ICellStyle = hssfworkbook.CreateCellStyle()
            Header_Style.SetFont(Header_Font)
            Header_Style.Alignment = HorizontalAlignment.Center
            Header_Style.FillPattern = FillPattern.SparseDots
            Header_Style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Lime.Index
            Header_Style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Lime.Index
            Header_Style.BorderBottom = BorderStyle.Thin
            Header_Style.BorderLeft = BorderStyle.Thin
            Header_Style.BorderRight = BorderStyle.Thin
            Header_Style.BorderTop = BorderStyle.Thin
            Header_Style.BottomBorderColor = HSSFColor.Black.Index
            Header_Style.LeftBorderColor = HSSFColor.Black.Index
            Header_Style.RightBorderColor = HSSFColor.Black.Index
            Header_Style.TopBorderColor = HSSFColor.Black.Index

            Dim Header_Style2 As ICellStyle = hssfworkbook.CreateCellStyle()
            Header_Style2.SetFont(Header_Font)
            Header_Style2.Alignment = HorizontalAlignment.Center
            Header_Style2.BorderBottom = BorderStyle.Thin
            Header_Style2.BorderLeft = BorderStyle.Thin
            Header_Style2.BorderRight = BorderStyle.Thin
            Header_Style2.BorderTop = BorderStyle.Thin
            Header_Style2.BottomBorderColor = HSSFColor.Black.Index
            Header_Style2.LeftBorderColor = HSSFColor.Black.Index
            Header_Style2.RightBorderColor = HSSFColor.Black.Index
            Header_Style2.TopBorderColor = HSSFColor.Black.Index

            Dim Header_Style3 As ICellStyle = hssfworkbook.CreateCellStyle()
            Header_Style3.SetFont(Header_Font)
            Header_Style3.Alignment = HorizontalAlignment.Center
            Header_Style3.FillPattern = FillPattern.SparseDots
            Header_Style3.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightYellow.Index
            Header_Style3.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.LightYellow.Index
            Header_Style3.BorderBottom = BorderStyle.Thin
            Header_Style3.BorderLeft = BorderStyle.Thin
            Header_Style3.BorderRight = BorderStyle.Thin
            Header_Style3.BorderTop = BorderStyle.Thin
            Header_Style3.BottomBorderColor = HSSFColor.Black.Index
            Header_Style3.LeftBorderColor = HSSFColor.Black.Index
            Header_Style3.RightBorderColor = HSSFColor.Black.Index
            Header_Style3.TopBorderColor = HSSFColor.Black.Index


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
            Row_Style_Left.VerticalAlignment = VerticalAlignment.Center

            Dim Row_Style_Left_bg As ICellStyle = hssfworkbook.CreateCellStyle()
            Row_Style_Left_bg.SetFont(Row_Font)
            Row_Style_Left_bg.FillPattern = FillPattern.SparseDots
            Row_Style_Left_bg.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index
            Row_Style_Left_bg.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index
            Row_Style_Left_bg.BorderBottom = BorderStyle.Thin
            Row_Style_Left_bg.BorderLeft = BorderStyle.Thin
            Row_Style_Left_bg.BorderRight = BorderStyle.Thin
            Row_Style_Left_bg.BorderTop = BorderStyle.Thin
            Row_Style_Left_bg.BottomBorderColor = HSSFColor.Black.Index
            Row_Style_Left_bg.LeftBorderColor = HSSFColor.Black.Index
            Row_Style_Left_bg.RightBorderColor = HSSFColor.Black.Index
            Row_Style_Left_bg.TopBorderColor = HSSFColor.Black.Index
            Row_Style_Left_bg.VerticalAlignment = VerticalAlignment.Center

            Dim Row_Font_BOLD = hssfworkbook.CreateFont()
            Row_Font_BOLD.FontName = "Tahoma"
            Row_Font_BOLD.FontHeightInPoints = 10
            Row_Font_BOLD.Boldweight = FontBoldWeight.Bold

            Dim Row_Style_BOLD As ICellStyle = hssfworkbook.CreateCellStyle()
            Row_Style_BOLD.SetFont(Row_Font_BOLD)

            Dim _ClientData = (From c In dc.tblNoticeDetails _
                         Where c.NoticeID.Equals(NotificationID) And c.f11.Equals(UWCode)).ToList()
            Dim _InsurerName As String = _ClientData(0).f12
            '====================== data new sheet ====================
            Dim sheet1 As ISheet = hssfworkbook.CreateSheet(_InsurerName)

            '================== create row ===================
            Dim j As Integer = 0

            '================== create column ===================
            Dim _fields = "No,Client Code,ClientName,New/Renew,EffectiveDate,เลขรับแจ้ง(NLT New),เลขตัวถัง(New NLT),ยกเลิก/ถอน,ให้มีผล,สาเหตุ,แจ้งเบี้ยถอนประกัน,หมายเหตุ".Split(",")
            Dim frow As IRow = sheet1.CreateRow(j)
            For i = 0 To _fields.Count - 1
                Dim cell = frow.CreateCell(i)
                cell.SetCellValue(_fields(i))
                cell.CellStyle = Header_Style

                sheet1.SetColumnWidth(i, _fields(i).Length * 400)
            Next

            Dim itemNo As Integer = 0
            For Each _item In _ClientData
                itemNo += 1
                j += 1
                Dim row As IRow = sheet1.CreateRow(j)
                For i = 0 To _fields.Count - 1
                    row.CreateCell(i).CellStyle = Row_Style_Left
                Next

                '========================== Data =============================
                'No,Client Code,ClientName,New/Renew,EffectiveDate,เลขรับแจ้ง(NLT New),เลขตัวถัง(New NLT),ยกเลิก/ถอน,ให้มีผล,สาเหตุ
                row.GetCell(0).SetCellValue(itemNo) 'No
                row.GetCell(1).SetCellValue(_item.Code) 'Client Code
                row.GetCell(2).SetCellValue(_item.f01) 'ClientName
                row.GetCell(3).SetCellValue(_item.f02) 'New/Renew
                row.GetCell(4).SetCellValue(_item.f03) 'EffectiveDate
                row.GetCell(5).SetCellValue(_item.f04) 'เลขรับแจ้ง(NLT New)
                row.GetCell(6).SetCellValue(_item.f05) 'เลขตัวถัง(New NLT)
                row.GetCell(7).SetCellValue(_item.f06) 'ยกเลิก/ถอน
                row.GetCell(8).SetCellValue(_item.f07) 'ให้มีผล
                row.GetCell(9).SetCellValue(_item.f08) 'สาเหตุ  
                row.GetCell(10).SetCellValue(_item.f09) 'แจ้งเบี้ยถอนประกัน
                row.GetCell(11).SetCellValue(_item.f10) 'หมายเหตุ 
            Next




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


            Dim _Insurer = (From c In dc.tblNoticeMailContacts Where c.Code.Equals(UWCode) And c.NoticeCode.Equals(_NoticeCode)).FirstOrDefault()
            Dim strMailFrom As String = "" '"nissan2@asia.lockton.com"
            Dim strMailCC As String = ""

            Dim strMailTo As String = ""
            '"siriwimol@asia.lockton.com,dusit@asia.lockton.com,parichat@asia.lockton.com" '_agent.Mailto '  Replace("dusit@asia.lockton.com,parichat@asia.lockton.com", ";", ",") '

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
                'รายการแก้ไข Amendment ประจำวันที่ {date}  จำนวน {items} รายการ
                strSubject = _mailNotification.MailSubject.Replace("{insurer}", _InsurerName)

                'strMessage.AppendLine("<!DOCTYPE html><html><body>")
                'strMessage.AppendLine(Server.HtmlDecode(_mailNotification.MailBody).Replace("{insurer}", _InsurerName))
                'strMessage.AppendLine("</body></html>")

                Dim _MailBody As String = Server.HtmlDecode(_mailNotification.MailBody).Replace("{insurer}", _InsurerName)

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

                strMailTo = _Insurer.MailTo
                strMailCC = _Insurer.MailCC & ";" & _mailNotification.MailCC & ";" & _mail


                'strMailTo = "dusit@asia.lockton.com;laddawan@asia.lockton.com;ratika@asia.lockton.com;anat@asia.lockton.com" '_agent.Mailto
                'strMailTo = "dusit@asia.lockton.com;juthamat@asia.lockton.com;ratika@asia.lockton.com;laddawan@asia.lockton.com"

            End Using


            Dim MySmtpClient As New System.Net.Mail.SmtpClient(ConfigurationSettings.AppSettings("smtp"))
            'MySmtpClient.Credentials = New System.Net.NetworkCredential("Your-username", "Your-password")


            'Dim msg As New System.Net.Mail.MailMessage(strMailFrom, strMailTo, strSubject, strMessage.ToString())
            Dim msg As New System.Net.Mail.MailMessage()


            '===========================================================
            Dim bodyHTML As String = "<html><body>" & strMessage.ToString() & "<span style='font-size:16.0pt;font-family:Angsana New,serif;color:green'><img src='cid:LOGO_IMAGE2' alt='Logo' /><i>Please consider the environment before printing this e-mail.</i></span></body></html>"
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

            msg.BodyEncoding = Encoding.UTF8
            msg.IsBodyHtml = True
            msg.Priority = Net.Mail.MailPriority.High

            Dim att_data = New Attachment(_fileDataPath)
            att_data.Name = String.Format("CancelPolicy_{0}.xls", DateTime.Now.ToString("dd.MM.yyyy"))
            msg.Attachments.Add(att_data)

            MySmtpClient.Send(msg)


            Alert.Show("Send")
            'Catch ex As Exception
            '    Throw ex
            'End Try

        End Using




    End Sub


    Protected Sub btnSend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSend.Click

        If Grid3.SelectedRowIndexArray.Length > 0 Then
            Dim sb As New StringBuilder()
            For Each _item In Grid3.SelectedRowIndexArray
                'sb.Append(Grid3.DataKeys(_item)(0))
                Dim _UWCode = Grid3.DataKeys(_item)(0)
                WH_genxls(hdNotificationID.Text, _UWCode)
            Next

            Using dc As New DataClasses_CPSExt
                Dim _data = (From c In dc.tblNoticeHeaders Where c.NoticeID.Equals(hdNotificationID.Text)).FirstOrDefault()
                If _data IsNot Nothing Then

                    If _data.SendDate Is Nothing Then
                        _data.SendDate = DateTime.Now()
                        _data.SendBy = HttpContext.Current.User.Identity.Name.ToString()
                    Else
                        _data.ReSendDate = DateTime.Now()
                        _data.ReSendBy = HttpContext.Current.User.Identity.Name.ToString()
                    End If


                    dc.SubmitChanges()
                End If
            End Using
            Alert.Show("done")
        Else
            Alert.Show("No data select")
        End If
    End Sub

    Protected Sub btnExport_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExport.Click

        ExportData(hdNotificationID.Text)

    End Sub

    Private Sub ExportData(ByVal NotificationID As String)


        Dim fileName = String.Format("{0}.xls", System.Guid.NewGuid.ToString())
        Dim filePath = String.Format("{0}\{1}", Server.MapPath("~/upload"), fileName)

        Dim hssfworkbook As New HSSFWorkbook()
        '=====================cell style ===================
        Dim Header_Font = hssfworkbook.CreateFont()
        Header_Font.FontName = "Tahoma"
        Header_Font.FontHeightInPoints = 10
        Header_Font.Boldweight = FontBoldWeight.Bold

        Dim Header_Style As ICellStyle = hssfworkbook.CreateCellStyle()
        Header_Style.SetFont(Header_Font)
        Header_Style.Alignment = HorizontalAlignment.Center
        Header_Style.FillPattern = FillPattern.SparseDots
        Header_Style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Lime.Index
        Header_Style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Lime.Index
        Header_Style.BorderBottom = BorderStyle.Thin
        Header_Style.BorderLeft = BorderStyle.Thin
        Header_Style.BorderRight = BorderStyle.Thin
        Header_Style.BorderTop = BorderStyle.Thin
        Header_Style.BottomBorderColor = HSSFColor.Black.Index
        Header_Style.LeftBorderColor = HSSFColor.Black.Index
        Header_Style.RightBorderColor = HSSFColor.Black.Index
        Header_Style.TopBorderColor = HSSFColor.Black.Index

        Dim Header_Style2 As ICellStyle = hssfworkbook.CreateCellStyle()
        Header_Style2.SetFont(Header_Font)
        Header_Style2.Alignment = HorizontalAlignment.Center
        Header_Style2.BorderBottom = BorderStyle.Thin
        Header_Style2.BorderLeft = BorderStyle.Thin
        Header_Style2.BorderRight = BorderStyle.Thin
        Header_Style2.BorderTop = BorderStyle.Thin
        Header_Style2.BottomBorderColor = HSSFColor.Black.Index
        Header_Style2.LeftBorderColor = HSSFColor.Black.Index
        Header_Style2.RightBorderColor = HSSFColor.Black.Index
        Header_Style2.TopBorderColor = HSSFColor.Black.Index

        Dim Header_Style3 As ICellStyle = hssfworkbook.CreateCellStyle()
        Header_Style3.SetFont(Header_Font)
        Header_Style3.Alignment = HorizontalAlignment.Center
        Header_Style3.FillPattern = FillPattern.SparseDots
        Header_Style3.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightYellow.Index
        Header_Style3.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.LightYellow.Index
        Header_Style3.BorderBottom = BorderStyle.Thin
        Header_Style3.BorderLeft = BorderStyle.Thin
        Header_Style3.BorderRight = BorderStyle.Thin
        Header_Style3.BorderTop = BorderStyle.Thin
        Header_Style3.BottomBorderColor = HSSFColor.Black.Index
        Header_Style3.LeftBorderColor = HSSFColor.Black.Index
        Header_Style3.RightBorderColor = HSSFColor.Black.Index
        Header_Style3.TopBorderColor = HSSFColor.Black.Index

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
        Row_Style_Left.VerticalAlignment = VerticalAlignment.Center

        Dim Row_Style_Left_bg As ICellStyle = hssfworkbook.CreateCellStyle()
        Row_Style_Left_bg.SetFont(Row_Font)
        Row_Style_Left_bg.FillPattern = FillPattern.SparseDots
        Row_Style_Left_bg.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index
        Row_Style_Left_bg.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index
        Row_Style_Left_bg.BorderBottom = BorderStyle.Thin
        Row_Style_Left_bg.BorderLeft = BorderStyle.Thin
        Row_Style_Left_bg.BorderRight = BorderStyle.Thin
        Row_Style_Left_bg.BorderTop = BorderStyle.Thin
        Row_Style_Left_bg.BottomBorderColor = HSSFColor.Black.Index
        Row_Style_Left_bg.LeftBorderColor = HSSFColor.Black.Index
        Row_Style_Left_bg.RightBorderColor = HSSFColor.Black.Index
        Row_Style_Left_bg.TopBorderColor = HSSFColor.Black.Index
        Row_Style_Left_bg.VerticalAlignment = VerticalAlignment.Center

        Dim Row_Font_BOLD = hssfworkbook.CreateFont()
        Row_Font_BOLD.FontName = "Tahoma"
        Row_Font_BOLD.FontHeightInPoints = 10
        Row_Font_BOLD.Boldweight = FontBoldWeight.Bold

        Dim Row_Style_BOLD As ICellStyle = hssfworkbook.CreateCellStyle()
        Row_Style_BOLD.SetFont(Row_Font_BOLD)

        Using dc As New DataClasses_CPSExt
            Dim _Insurer = (From c In dc.tblNoticeDetails _
                         Where c.NoticeID.Equals(NotificationID) _
                         Group By UWCode = c.f11, UWName = c.f12 _
                         Into g = Group _
                         Select UWCode, UWName, Clients = g.Count() _
                   ).ToList()

            For Each iInsurer In _Insurer

                Dim _ClientData = (From c In dc.tblNoticeDetails _
                         Where c.NoticeID.Equals(NotificationID) And c.f11.Equals(iInsurer.UWCode)).ToList()

                '====================== data new sheet ====================
                Dim sheet1 As ISheet = hssfworkbook.CreateSheet(iInsurer.UWName)

                '================== create row ===================
                Dim j As Integer = 0

                '================== create column ===================


                Dim _fields = "No,Client Code,ClientName,New/Renew,EffectiveDate,เลขรับแจ้ง(NLT New),เลขตัวถัง(New NLT),ยกเลิก/ถอน,ให้มีผล,สาเหตุ,แจ้งเบี้ยถอนประกัน,หมายเหตุ".Split(",")
                Dim frow As IRow = sheet1.CreateRow(j)
                For i = 0 To _fields.Count - 1
                    Dim cell = frow.CreateCell(i)
                    cell.SetCellValue(_fields(i))
                    cell.CellStyle = Header_Style

                    sheet1.SetColumnWidth(i, _fields(i).Length * 400)
                Next

                Dim itemNo As Integer = 0
                For Each _item In _ClientData
                    itemNo += 1
                    j += 1
                    Dim row As IRow = sheet1.CreateRow(j)
                    For i = 0 To _fields.Count - 1
                        row.CreateCell(i).CellStyle = Row_Style_Left
                    Next

                    '========================== Data =============================
                    'Client Code,ClientName,New/Renew,EffectiveDate,เลขรับแจ้ง(NLT New),เลขตัวถัง(New NLT),ยกเลิก/ถอน,ให้มีผล,สาเหตุ
                    row.GetCell(0).SetCellValue(itemNo) 'No
                    row.GetCell(1).SetCellValue(_item.Code) 'Client Code
                    row.GetCell(2).SetCellValue(_item.f01) 'ClientName
                    row.GetCell(3).SetCellValue(_item.f02) 'New/Renew
                    row.GetCell(4).SetCellValue(_item.f03) 'EffectiveDate
                    row.GetCell(5).SetCellValue(_item.f04) 'เลขรับแจ้ง(NLT New)
                    row.GetCell(6).SetCellValue(_item.f05) 'เลขตัวถัง(New NLT)
                    row.GetCell(7).SetCellValue(_item.f06) 'ยกเลิก/ถอน
                    row.GetCell(8).SetCellValue(_item.f07) 'ให้มีผล
                    row.GetCell(9).SetCellValue(_item.f08) 'สาเหตุ
                    row.GetCell(10).SetCellValue(_item.f09) 'แจ้งเบี้ยถอนประกัน
                    row.GetCell(11).SetCellValue(_item.f10) 'หมายเหตุ

                Next

            Next
        End Using



        Dim fs As New FileStream(filePath, FileMode.Create)
        hssfworkbook.Write(fs)
        fs.Close()


        Response.ClearContent()
        Response.AddHeader("content-disposition", "attachment; filename=" & fileName)
        Response.ContentType = "application/octet-stream"
        Response.WriteFile(filePath)
        Response.End()


    End Sub

End Class
