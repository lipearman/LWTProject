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

Partial Class Modules_ucDevxNoticeHeaderSetup_B0020
    Inherits PortalModuleControl
    Dim _NoticeCode As String = "B0020"
    '*******************************************************
    '
    ' The Page_Load server event handler on this user control is used
    ' to populate the current site settings from the config system
    '
    '*******************************************************
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Page.IsPostBack = False Then
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
        Dim column As FineUI.GridColumn = Grid3.FindColumn(Grid3.SortField)
        BindAgentDataWithSort(column.SortField, Grid3.SortDirection)
    End Sub


    Private Sub BindAgentDataWithSort(ByVal sortField As String, ByVal sortDirection As String)

        Using dc As New DataClasses_CPSExt()

            Dim _data = dc.ExecuteQuery(Of MyContext.V_Billing_Notification)("select * from V_BillingWH1Notification where NotificationID={0}  ", hdNotificationID.Text).ToList()
            Dim table As DataTable = MyUtils.EQToDataTable(_data)
            Dim view1 As DataView = table.DefaultView
            If _data.Count > 0 Then
                view1.Sort = String.Format("{0} {1}", sortField, sortDirection)
            End If
            Grid3.DataSource = view1
            Grid3.DataBind()

            Grid3.SelectAllRows()

        End Using
    End Sub
    Private Sub BindViewAgentData(ByVal AgentCode As String)

        Using dc As New DataClasses_CPSExt
            Dim _data = (From c In dc.tblNoticeDetails Where c.Code.Equals(AgentCode) And c.NoticeID.Equals(hdNotificationID.Text)).ToList()
            Grid2.DataSource = _data
            Grid2.DataBind()


            Dim WHTotal As Decimal = 0.0F
            For Each item In _data
                WHTotal += Convert.ToDecimal(item.f11)
            Next

            Dim summary = New Newtonsoft.Json.Linq.JObject()
            summary.Add("f11", WHTotal.ToString("F2"))
            Grid2.SummaryData = summary
        End Using

        BindErrorAgentData()
    End Sub
    Private Sub BindErrorAgentData()

        Using dc As New DataClasses_CPSExt
            Dim _data = (From c In dc.tblNoticeDetails Where c.NoticeID.Equals(hdNotificationID.Text) And c.Code.Equals("")).ToList()
            Grid4.DataSource = _data
            Grid4.DataBind()
        End Using
    End Sub
    Protected Sub grid1_Sort(ByVal sender As Object, ByVal e As FineUI.GridSortEventArgs) Handles Grid1.Sort
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

        End Using

        BindAgentData()


    End Sub

    Protected Sub grid3_Sort(ByVal sender As Object, ByVal e As FineUI.GridSortEventArgs) Handles Grid3.Sort
        BindAgentDataWithSort(e.SortField, e.SortDirection)
    End Sub

    Protected Sub Grid3_RowDoubleClick(sender As Object, e As FineUI.GridRowClickEventArgs) Handles Grid3.RowDoubleClick
        Dim _AgentCode As String = Grid3.DataKeys(e.RowIndex + (Grid3.PageIndex * Grid3.PageSize))(0)
        Dim _AgentName As String = Grid3.DataKeys(e.RowIndex + (Grid3.PageIndex * Grid3.PageSize))(1)

        BindViewAgentData(_AgentCode)
        WindowViewAgentData.Title = String.Format("{0} - {1}", _AgentCode, _AgentName)
        WindowViewAgentData.Hidden = False

    End Sub

    Protected Sub Grid3_RowCommand(ByVal sender As Object, ByVal e As FineUI.GridCommandEventArgs) Handles Grid3.RowCommand
        Dim _AgentCode As String = Grid3.DataKeys(e.RowIndex + (Grid3.PageIndex * Grid3.PageSize))(0)
        Dim _AgentName As String = Grid3.DataKeys(e.RowIndex + (Grid3.PageIndex * Grid3.PageSize))(1)

        Select Case e.CommandName
            Case "btnViewAgentData"
                BindViewAgentData(_AgentCode)
                WindowViewAgentData.Title = String.Format("{0} - {1}", _AgentCode, _AgentName)
                WindowViewAgentData.Hidden = False
            Case "btnSend"
                WH_genxls(hdNotificationID.Text, _AgentCode)

        End Select

    End Sub



    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        PageContext.Refresh()
    End Sub
    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        'WindowAgent.Hidden = False
        'tbxAgentCode.Text = ""
        'tbxAgentCode.Readonly = False
        'tbxAgentName.Text = ""
        'tbxMailto.Text = ""
        'hdAgentId.Text = ""
        'WindowAgent.Title = "New Agent"


        Using dc As New DataClasses_CPSExt



            ', .DataFields = "[M2_AgenCode],[M2_SHOWROOM],[M2_INSURER],[Project],[M2_CHASSIS NO],[M2_CLIENT Code],[M2_Name Surname],[Effective],[NetPremium],[M2_VMIStamp],[WH]" _
            Dim _newdata As New tblNoticeHeader With {.NoticeTitle = "แจ้งนำส่ง WH1% ด่วน" _
                                                       , .NoticeCode = _NoticeCode _
                                                       , .CreationDate = DateTime.Now _
                                                       , .CreationBy = HttpContext.Current.User.Identity.Name.ToString()
                                                    }
            dc.tblNoticeHeaders.InsertOnSubmit(_newdata)
            dc.SubmitChanges()
        End Using


        BindData()
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

    Protected Sub btnUploadData_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUploadData.Click
        WindowUploaddata.Hidden = False
    End Sub
    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click


        Using dc As New DataClasses_CPSExt
            Dim _BillingNotificationData As New List(Of tblNoticeDetail)

            Dim i As Integer = 0
            Dim reader = New StringReader(tbxdata.Text)
            Dim line As String
            line = reader.ReadLine()

            Using dc_NLTDB As New DataClasses_NLTDBExt


                While line IsNot Nothing
                    Dim _row() As String = line.Split(vbTab)
                    Dim _M2_AgenCode As String = ""
                    Dim _M2_SHOWROOM As String = ""
                    Dim _M2_INSURER As String = _row(12)

                    Dim _M2_CHASSIS_NO As String = ""
                    Dim _M2_Name_Surname As String = ""
                    Dim _Effective As String = ""

                    Dim _M2_CLIENT_Code As String = _row(8)

                    Dim _app = (From c In dc_NLTDB.Applications Where c.ClientCode.Equals(_M2_CLIENT_Code) And c.Status <> 7).FirstOrDefault()
                    If _app IsNot Nothing Then
                        _M2_Name_Surname = String.Format("{0} {1}", _app.Name, _app.SurName)
                        _M2_CHASSIS_NO = _app.VIN
                        _Effective = _app.StartingDate.Value.ToString("dd/MM/yyyy")

                        'Dim _Insuerer = (From c In dc_NLTDB.Insurers Where c.InsurerID.Equals(_app.InsurerID)).FirstOrDefault()
                        '_M2_INSURER = _Insuerer.InsurerNameThai.Replace("(TB)", "").Replace("(AYCAL)", "").Replace("(KTB)", "").Trim()


                        Dim _showroom = (From c In dc_NLTDB.Showrooms Where c.ShowroomID.Equals(_app.ShowroomCode)).FirstOrDefault()
                        _M2_SHOWROOM = _showroom.CompanyName

                        Dim _HO = (From c In dc_NLTDB.HeadOffices Where c.HeadID.Equals(_showroom.HeadID)).FirstOrDefault()
                        _M2_AgenCode = _HO.DealerCode


                    End If


                    Dim _NetPremium As String = _row(13)
                    Dim _M2_VMIStamp As String = _row(16)
                    Dim _WH As String = _row(23)
                    Dim _Project As String = _row(26)


                    _BillingNotificationData.Add(New tblNoticeDetail With {.NoticeID = hdNotificationID.Text _
                                                                                       , .Code = _M2_AgenCode _
                                                                                       , .f01 = _M2_AgenCode _
                                                                                       , .f02 = _M2_SHOWROOM _
                                                                                       , .f03 = _M2_INSURER _
                                                                                       , .f04 = _Project _
                                                                                       , .f05 = _M2_CHASSIS_NO _
                                                                                       , .f06 = _M2_CLIENT_Code _
                                                                                       , .f07 = _M2_Name_Surname _
                                                                                       , .f08 = _Effective _
                                                                                       , .f09 = _NetPremium _
                                                                                       , .f10 = _M2_VMIStamp _
                                                                                       , .f11 = _WH _
                                                                                  })

                    '_BillingNotificationData.Add(New Billing_NotificationData With {.NotificationID = hdNotificationID.Text _
                    '                                                                    , .AgentCode = _row(0) _
                    '                                                                    , .f01 = _row(0) _
                    '                                                                    , .f02 = _row(1) _
                    '                                                                    , .f03 = _row(2) _
                    '                                                                    , .f04 = _row(3) _
                    '                                                                    , .f05 = _row(4) _
                    '                                                                    , .f06 = _row(5) _
                    '                                                                    , .f07 = _row(6) _
                    '                                                                    , .f08 = _row(7) _
                    '                                                                    , .f09 = _row(8) _
                    '                                                                    , .f10 = _row(9) _
                    '                                                                    , .f11 = _row(10) _
                    '                                                               })



                    line = reader.ReadLine()
                End While

            End Using

            dc.ExecuteCommand("delete from tblNoticeDetail where NoticeID={0}", hdNotificationID.Text)

            dc.tblNoticeDetails.InsertAllOnSubmit(_BillingNotificationData)

            dc.SubmitChanges()

            Alert.Show(_BillingNotificationData.Count)
        End Using

        tbxdata.Text = ""

        BindAgentData()

        WindowUploaddata.Hidden = True
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


            Dim _agentdata = (From c In dc.tblNoticeDetails Where c.NoticeID.Equals(NotificationID) And c.Code.Equals(AgentCode)).ToList()

            Dim _Insurer = (From c In _agentdata _
                            Group By insurer = c.f03 Into MyGroup = Group _
                            Select insurer).ToList()



            Dim hssfworkbook As New HSSFWorkbook()

            '=====================cell style ===================
            Dim Header_Font = hssfworkbook.CreateFont()
            Header_Font.FontName = "Tahoma"
            Header_Font.FontHeightInPoints = 8
            Header_Font.Boldweight = FontBoldWeight.Bold

            Dim Header_Style As iCellStyle = hssfworkbook.CreateCellStyle()
            Header_Style.SetFont(Header_Font)
            Header_Style.Alignment = HorizontalAlignment.Center
            Header_Style.FillPattern = FillPattern.SparseDots
            Header_Style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Orange.Index
            Header_Style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Orange.Index

            Dim Row_Font = hssfworkbook.CreateFont()
            Row_Font.FontName = "Tahoma"
            Row_Font.FontHeightInPoints = 8

            Dim Row_Style_Left As ICellStyle = hssfworkbook.CreateCellStyle()
            Row_Style_Left.SetFont(Row_Font)

            Dim Row_Style_Center As ICellStyle = hssfworkbook.CreateCellStyle()
            Row_Style_Center.SetFont(Row_Font)
            Row_Style_Center.Alignment = HorizontalAlignment.Center

            Dim Row_Style_Right As ICellStyle = hssfworkbook.CreateCellStyle()
            Row_Style_Right.SetFont(Row_Font)
            Row_Style_Right.Alignment = HorizontalAlignment.Right



            Dim Row_Font_Bold = hssfworkbook.CreateFont()
            Row_Font_Bold.FontName = "Tahoma"
            Row_Font_Bold.FontHeightInPoints = 8
            'Row_Font_Bold.Underline = DirectCast(FontUnderlineType.DOUBLE, Byte)
            Row_Font_Bold.Boldweight = DirectCast(FontBoldWeight.Bold, Short)
            Dim Row_Style_Right_Bold As ICellStyle = hssfworkbook.CreateCellStyle()
            Row_Style_Right_Bold.SetFont(Row_Font_Bold)
            Row_Style_Right_Bold.Alignment = HorizontalAlignment.Right

            For Each insurer_name In _Insurer

                Dim sheet1 As ISheet = hssfworkbook.CreateSheet(insurer_name)
                '================== create column ===================
                Dim _fields = "M2_AgenCode,M2_SHOWROOM,M2_INSURER,Project,M2_CHASSIS NO,M2_CLIENT Code,M2_Name Surname,Effective,NetPremium,M2_VMIStamp,WH".Split(",") '_data.DataFields.Split(",")
                Dim frow As IRow = sheet1.CreateRow(0)
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

                '================== create cell ===================
                Dim x As Integer = 1
                Dim j As Integer = 1
                Dim total As Decimal = 0


                Dim SheetData = (From c In _agentdata Where c.f03.Equals(insurer_name)).ToList()
                For Each _item In SheetData

                    Dim row As IRow = sheet1.CreateRow(j)

                    '================== init column Length =====================
                    If _item.f01.Length > _item_f01_Length Then
                        sheet1.SetColumnWidth(0, _item.f01.Length * 400)
                        _item_f01_Length = _item.f01.Length
                    End If
                    If _item.f02.Length > _item_f02_Length Then
                        sheet1.SetColumnWidth(1, _item.f02.Length * 400)
                        _item_f02_Length = _item.f02.Length
                    End If
                    If _item.f03.Length > _item_f03_Length Then
                        sheet1.SetColumnWidth(2, _item.f03.Length * 400)
                        _item_f03_Length = _item.f03.Length
                    End If
                    If _item.f04.Length > _item_f04_Length Then
                        sheet1.SetColumnWidth(3, _item.f04.Length * 400)
                        _item_f04_Length = _item.f04.Length
                    End If
                    If _item.f05.Length > _item_f05_Length Then
                        sheet1.SetColumnWidth(4, _item.f05.Length * 400)
                        _item_f05_Length = _item.f05.Length
                    End If
                    If _item.f06.Length > _item_f06_Length Then
                        sheet1.SetColumnWidth(5, _item.f06.Length * 400)
                        _item_f06_Length = _item.f06.Length
                    End If
                    If _item.f07.Length > _item_f07_Length Then
                        sheet1.SetColumnWidth(6, _item.f07.Length * 400)
                        _item_f07_Length = _item.f07.Length
                    End If
                    If _item.f08.Length > _item_f08_Length Then
                        sheet1.SetColumnWidth(7, _item.f08.Length * 400)
                        _item_f08_Length = _item.f08.Length
                    End If
                    If _item.f09.Length > _item_f09_Length Then
                        sheet1.SetColumnWidth(8, _item.f09.Length * 400)
                        _item_f09_Length = _item.f09.Length
                    End If
                    If _item.f10.Length > _item_f10_Length Then
                        sheet1.SetColumnWidth(9, _item.f10.Length * 400)
                        _item_f10_Length = _item.f10.Length
                    End If
                    If _item.f11.Length > _item_f11_Length Then
                        sheet1.SetColumnWidth(10, _item.f11.Length * 400)
                        _item_f11_Length = _item.f11.Length
                    End If


                    '========================== Data =============================
                    row.CreateCell(0).CellStyle = Row_Style_Center
                    row.CreateCell(1).CellStyle = Row_Style_Left
                    row.CreateCell(2).CellStyle = Row_Style_Left
                    row.CreateCell(3).CellStyle = Row_Style_Center
                    row.CreateCell(4).CellStyle = Row_Style_Left
                    row.CreateCell(5).CellStyle = Row_Style_Left
                    row.CreateCell(6).CellStyle = Row_Style_Left

                    row.CreateCell(7).CellStyle = Row_Style_Right
                    row.CreateCell(8).CellStyle = Row_Style_Right
                    row.CreateCell(9).CellStyle = Row_Style_Right
                    row.CreateCell(10).CellStyle = Row_Style_Right

                    row.GetCell(0).SetCellValue(_item.f01.Trim())
                    row.GetCell(1).SetCellValue(_item.f02.Trim())
                    row.GetCell(2).SetCellValue(_item.f03.Trim())
                    row.GetCell(3).SetCellValue(_item.f04.Trim())
                    row.GetCell(4).SetCellValue(_item.f05.Trim())
                    row.GetCell(5).SetCellValue(_item.f06.Trim())
                    row.GetCell(6).SetCellValue(_item.f07.Trim())
                    row.GetCell(7).SetCellValue(_item.f08.Trim())
                    row.GetCell(8).SetCellValue(Convert.ToDecimal(_item.f09.Trim()).ToString("###,##0.00"))
                    row.GetCell(9).SetCellValue(Convert.ToDecimal(_item.f10.Trim()).ToString("###,##0.00"))
                    row.GetCell(10).SetCellValue(Convert.ToDecimal(_item.f11.Trim()).ToString("###,##0.00"))


                    total += Convert.ToDecimal(_item.f11)
                    j += 1

                Next

                '============= sum total =============
                Dim lrow As IRow = sheet1.CreateRow(j)
                lrow.CreateCell(10).CellStyle = Row_Style_Right_Bold
                lrow.GetCell(10).SetCellValue(Convert.ToDecimal(total).ToString("###,##0.00"))

            Next





            ''================== init  sheet property =================
            'sheet1.SetAutoFilter(New CellRangeAddress(0, 0, 0, 10))
            'sheet1.ProtectSheet("sheet1")

            Dim _filename = String.Format("{0}-แจ้งติดตามหนังสืบรับรองหัก ณ ที่จ่าย_{1}.xls", AgentCode, DateTime.Now.ToString("yyyyMMddHHmmss"))
            Dim _path = String.Format("{0}/{1}", Server.MapPath("~/upload"), _filename)
            Dim fs As New FileStream(_path, FileMode.Create)
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

                    Dim _mailNotification = (From c In dc_portal.MailNotifications Where c.Code.Equals(_data.NoticeCode)).FirstOrDefault()
                    strSubject = _mailNotification.MailSubject.Replace("{agentcode}", _agent.Code).Replace("{companyname}", _agent.Name)

                    'strMessage.AppendLine("<!DOCTYPE html><html><body>")
                    'strMessage.AppendLine(Server.HtmlDecode(_mailNotification.MailBody))
                    'strMessage.AppendLine("</body></html>")

                    strMailFrom = _mail

                    strMailCC = _mailNotification.MailCC & ";" & _mail

                    strMailTo = _agent.MailTo

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
                End Using


                Dim MySmtpClient As New System.Net.Mail.SmtpClient(ConfigurationSettings.AppSettings("smtp"))
                ''MySmtpClient.Credentials = New System.Net.NetworkCredential("Your-username", "Your-password")
                'Dim msg As New System.Net.Mail.MailMessage(strFrom, strTo, strSubject, strMessage.ToString())
                'msg.BodyEncoding = Encoding.UTF8
                'msg.IsBodyHtml = True
                'msg.Priority = Net.Mail.MailPriority.High
                ''Add an Attachment
                'Dim at = New Attachment(_path)
                'at.Name = _filename

                'msg.Attachments.Add(at)
                'MySmtpClient.Send(msg)



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
                Dim at = New Attachment(_path)
                at.Name = _filename
                msg.Attachments.Add(at)

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
    Protected Sub btnExportForm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExportForm.Click
        Response.ClearContent()
        Response.AddHeader("content-disposition", "attachment; filename=ErrorData.xls")
        Response.ContentType = "application/excel"
        Response.ContentEncoding = System.Text.Encoding.UTF8
        Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble())
        Response.Write(MyUtils.GetGridTableHtml(Grid4, ("M2_AgenCode,M2_SHOWROOM,M2_INSURER,Project,M2_CHASSIS NO,M2_CLIENT Code,M2_Name Surname,Effective,NetPremium,M2_VMIStamp,WH").Split(",")))
        Response.End()

    End Sub

End Class
