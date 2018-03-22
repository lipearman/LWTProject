Imports System.Data
Imports System.Web.Security
Imports Portal.Components

Imports DevExpress.Web
Imports DevExpress.Web.ASPxHtmlEditor
Imports System.Net.Mail
Imports DevExpress.Spreadsheet
Imports System.IO


Imports NPOI.HSSF.UserModel
Imports NPOI.HPSF
Imports NPOI.SS.UserModel
Imports NPOI.SS.Util
Imports NPOI.HSSF.Util
Imports System.Drawing
Imports iTextSharp.text.pdf

Partial Class Modules_ucDevxNoticeHeaderSetup_B0013
    Inherits PortalModuleControl
    Private NoticeCode As String = "B0013"
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        SqlDataSource_NoticeHeader.SelectParameters("NoticeCode").DefaultValue = NoticeCode
        SqlDataSource_Details.SelectParameters("NoticeCode").DefaultValue = NoticeCode
        'SqlDataSource_Details_TR.SelectParameters("NoticeCode").DefaultValue = NoticeCode




        'SqlDataSource_Details_TR
        'DevExpress.Web.ASPxThemes.IconID.MailSend32x32
    End Sub
    Protected Sub btnExcelFormat_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExcelFormat.Click
        Dim fileName = String.Format("{0}.xls", System.Guid.NewGuid.ToString())
        Dim filePath = String.Format("{0}\{1}", Server.MapPath("~/Template"), "TBAPolicy.xls")



        Response.ClearContent()
        Response.AddHeader("content-disposition", "attachment; filename=" & fileName)
        Response.ContentType = "application/octet-stream"
        Response.WriteFile(filePath)
        Response.End()

    End Sub

    'Protected Sub cbAddNewHeader_Callback(ByVal source As Object, ByVal e As CallbackEventArgs) Handles cbAddNewHeader.Callback

    '    Using _dc As New DataClasses_CPSExt()
    '        Dim newData = New tblNoticeHeader
    '        With newData
    '            .NoticeTitle = "แจ้งนำส่งกรมธรรม์ประกันภัยด่วน - " & Now.ToString("dd/MM/yyyy HH:mm:ss")
    '            .CreationDate = Now()
    '            .CreationBy = HttpContext.Current.User.Identity.Name
    '            .NoticeCode = NoticeCode
    '        End With

    '        _dc.tblNoticeHeaders.InsertOnSubmit(newData)
    '        _dc.SubmitChanges()
    '    End Using

    '    e.Result = "success"

    'End Sub


    Protected Sub detailGrid_DataSelect(ByVal sender As Object, ByVal e As EventArgs)
        Session("NoticeID") = (TryCast(sender, ASPxGridView)).GetMasterRowKeyValue()
    End Sub

    Protected Sub DocumentsUploadControl_FileUploadComplete(ByVal sender As Object, ByVal e As DevExpress.Web.FileUploadCompleteEventArgs) Handles uploaddata.FileUploadComplete
        Dim isSubmissionExpired As Boolean = False
        Dim _GUID = System.Guid.NewGuid().ToString()
        Dim _FILE = Server.MapPath("~/saved_files/") & "/" & _GUID & ".xls"
        e.UploadedFile.SaveAs(_FILE)
        ImportAppForm(_FILE)
        'If e.IsValid Then
        '    e.CallbackData = tempFileInfo.UniqueFileName & "|" & isSubmissionExpired
        'End If


        'Using _dc As New DataClasses_CPSExt()
        '    Dim newData = New tblNoticeHeader
        '    With newData
        '        .NoticeTitle = "แจ้งนำส่งกรมธรรม์ประกันภัยด่วน - " & Now.ToString("dd/MM/yyyy HH:mm:ss")
        '        .CreationDate = Now()
        '        .CreationBy = HttpContext.Current.User.Identity.Name
        '        .NoticeCode = NoticeCode
        '    End With

        '    _dc.tblNoticeHeaders.InsertOnSubmit(newData)
        '    _dc.SubmitChanges()
        'End Using




    End Sub


    Private Sub ImportAppForm(ByVal FilePath As String)
        Using dc As New DataClasses_CPSExt()

            Dim _tblNoticeHeader As New tblNoticeHeader
            With _tblNoticeHeader
                .NoticeTitle = "แจ้งนำส่งกรมธรรม์ประกันภัยด่วน - " & Now.ToString("dd/MM/yyyy HH:mm:ss")
                .CreationDate = Now()
                .CreationBy = HttpContext.Current.User.Identity.Name
                .NoticeCode = NoticeCode
            End With


            Using File = New FileStream(FilePath, FileMode.Open, FileAccess.Read)
                Dim HSSFWorkbook = New HSSFWorkbook(File)
                '====================== NOTR ============================
                Dim sheet_NOTR = HSSFWorkbook.GetSheetAt(0)
                Dim rows = sheet_NOTR.GetRowEnumerator()
                rows.MoveNext()
                Dim i As Integer = 0
                While rows.MoveNext()
                    i += 1
                    Dim row = DirectCast(rows.Current, HSSFRow)


                    'A.Client Code
                    'B.D/N No
                    'C.D/N Date
                    'D.Insurer()
                    'E.ชื่อบริษัทประกันภัย()
                    'F.เลขรับแจ้ง()
                    'G.เลขตัวถัง()
                    'H.Policy No.
                    'I.Period From
                    'J.Period To
                    'K.NetPremium Premium
                    'L.TotalPremium()
                    'Dim _A As String = row.GetCell(0).ToString()
                    'Dim _B As String = row.GetCell(1).ToString()
                    'Dim _C As String = row.GetCell(2).ToString()
                    'Dim _D As String = row.GetCell(3).ToString()
                    'Dim _E As String = row.GetCell(4).ToString()
                    'Dim _F As String = row.GetCell(5).ToString()
                    'Dim _G As String = row.GetCell(6).ToString()
                    'Dim _H As String = row.GetCell(7).ToString()
                    'Dim _I As String = row.GetCell(8).ToString()
                    'Dim _J As String = row.GetCell(9).ToString()
                    'Dim _K As String = row.GetCell(10).ToString()
                    'Dim _L As String = row.GetCell(11).ToString()


                    Dim _NOTR As New tblNoticeDetail

                    With _NOTR
                        .f01 = "NOTR"
                        If row.GetCell(3) IsNot Nothing Then .Code = row.GetCell(3).ToString()


                        If row.GetCell(0) IsNot Nothing Then .f02 = row.GetCell(0).ToString()
                        If row.GetCell(1) IsNot Nothing Then .f03 = row.GetCell(1).ToString()
                        If row.GetCell(2) IsNot Nothing Then .f04 = row.GetCell(2).ToString()
                        If row.GetCell(3) IsNot Nothing Then .f05 = row.GetCell(3).ToString()
                        If row.GetCell(4) IsNot Nothing Then .f06 = row.GetCell(4).ToString()
                        If row.GetCell(5) IsNot Nothing Then .f07 = row.GetCell(5).ToString()
                        If row.GetCell(6) IsNot Nothing Then .f08 = row.GetCell(6).ToString()
                        If row.GetCell(7) IsNot Nothing Then .f09 = row.GetCell(7).ToString()
                        If row.GetCell(8) IsNot Nothing Then .f10 = row.GetCell(8).ToString()
                        If row.GetCell(9) IsNot Nothing Then .f11 = row.GetCell(9).ToString()
                        If row.GetCell(10) IsNot Nothing Then .f12 = row.GetCell(10).ToString()
                        If row.GetCell(11) IsNot Nothing Then .f13 = row.GetCell(11).ToString()
                    End With
                    _tblNoticeHeader.tblNoticeDetails.Add(_NOTR)

                End While



                '====================== TR ============================
                Dim sheet_TR = HSSFWorkbook.GetSheetAt(1)
                rows = sheet_TR.GetRowEnumerator()
                rows.MoveNext()
                i = 0
                While rows.MoveNext()
                    i += 1
                    Dim row = DirectCast(rows.Current, HSSFRow)
                    'A.Client Code
                    'B.D/N No
                    'C.D/N Date
                    'D.Insurer()
                    'E.ชื่อบริษัทประกันภัย()
                    'F.รายละเอียด()
                    'G.เลขรับแจ้ง()
                    'H.เลขตัวถัง()
                    'I.Policy No.
                    'J.Period From
                    'K.Period To
                    'L.NetPremium Premium
                    'M.TotalPremium()
                    'N.TRNO()
                    'O.TRDATE()
                    'P.Balance Amount

                    'Dim _A As String = row.GetCell(0).ToString()
                    'Dim _B As String = row.GetCell(1).ToString()
                    'Dim _C As String = row.GetCell(2).ToString()
                    'Dim _D As String = row.GetCell(3).ToString()
                    'Dim _E As String = row.GetCell(4).ToString()
                    'Dim _F As String = row.GetCell(5).ToString()
                    'Dim _G As String = row.GetCell(6).ToString()
                    'Dim _H As String = row.GetCell(7).ToString()
                    'Dim _I As String = row.GetCell(8).ToString()
                    'Dim _J As String = row.GetCell(9).ToString()
                    'Dim _K As String = row.GetCell(10).ToString()
                    'Dim _L As String = row.GetCell(11).ToString()
                    'Dim _M As String = row.GetCell(12).ToString()
                    'Dim _N As String = row.GetCell(13).ToString()
                    'Dim _O As String = row.GetCell(14).ToString()
                    'Dim _P As String = row.GetCell(15).ToString()


                    Dim _TR As New tblNoticeDetail

                    With _TR

                        .f01 = "TR"
                        If row.GetCell(3) IsNot Nothing Then .Code = row.GetCell(3).ToString()

                        If row.GetCell(0) IsNot Nothing Then .f02 = row.GetCell(0).ToString()
                        If row.GetCell(1) IsNot Nothing Then .f03 = row.GetCell(1).ToString()
                        If row.GetCell(2) IsNot Nothing Then .f04 = row.GetCell(2).ToString()
                        If row.GetCell(3) IsNot Nothing Then .f05 = row.GetCell(3).ToString()
                        If row.GetCell(4) IsNot Nothing Then .f06 = row.GetCell(4).ToString()
                        If row.GetCell(5) IsNot Nothing Then .f07 = row.GetCell(5).ToString()
                        If row.GetCell(6) IsNot Nothing Then .f08 = row.GetCell(6).ToString()
                        If row.GetCell(7) IsNot Nothing Then .f09 = row.GetCell(7).ToString()
                        If row.GetCell(8) IsNot Nothing Then .f10 = row.GetCell(8).ToString()
                        If row.GetCell(9) IsNot Nothing Then .f11 = row.GetCell(9).ToString()
                        If row.GetCell(10) IsNot Nothing Then .f12 = row.GetCell(10).ToString()
                        If row.GetCell(11) IsNot Nothing Then .f13 = row.GetCell(11).ToString()
                        If row.GetCell(12) IsNot Nothing Then .f14 = row.GetCell(12).ToString()
                        If row.GetCell(13) IsNot Nothing Then .f15 = row.GetCell(13).ToString()
                        If row.GetCell(14) IsNot Nothing Then .f16 = row.GetCell(14).ToString()
                        If row.GetCell(15) IsNot Nothing Then .f17 = row.GetCell(15).ToString()

                    End With
                    _tblNoticeHeader.tblNoticeDetails.Add(_TR)



                End While

                dc.tblNoticeHeaders.InsertOnSubmit(_tblNoticeHeader)
                dc.SubmitChanges()
            End Using



        End Using

    End Sub




    Protected Sub callbackPanel_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase) Handles callbackPanel_TR.Callback
        Dim _Code As String = e.Parameter.ToString()
        SqlDataSource_Details_TR.SelectParameters("Code").DefaultValue = _Code
        SqlDataSource_Details_TR.DataBind()
    End Sub

    Protected Sub callbackPanel_NOTR_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase) Handles callbackPanel_NOTR.Callback
        Dim _Code As String = e.Parameter.ToString()
        SqlDataSource_Details_NOTR.SelectParameters("Code").DefaultValue = _Code
        SqlDataSource_Details_NOTR.DataBind()
    End Sub

    ''detailGrid_TR
    'Protected Sub detailGrid_TR_CustomUnboundColumnData(ByVal source As Object, ByVal e As DevExpress.Web.ASPxGridViewColumnDataEventArgs) Handles detailGrid_TR.CustomUnboundColumnData
    '    If e.Column.FieldName = "No" Then
    '        e.Value = String.Format("{0}", e.ListSourceRowIndex)
    '    End If
    'End Sub


    Protected Sub detailGrid_CustomCallback(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewCustomCallbackEventArgs)


        'Dim masterKeyValue As Object = masterGrid.GetRowValues(Convert.ToInt32(e.Parameters), "CategoryID")
        'Session("CategoryID") = masterKeyValue
        'detailGrid.DataSource = dsProducts
        'detailGrid.PageIndex = 0
        'detailGrid.DataBind()

        Dim _Code As New List(Of String)
        Select Case e.Parameters
            Case "sendmail"
                For Each key In sender.GetCurrentPageRowValues("Code")
                    If sender.Selection.IsRowSelectedByKey(key) Then
                        '_Code.Add(key.ToString())
                        WH_genxls(Session("NoticeID"), key.ToString())
                    End If
                Next key




            Case "delete"
                For Each key In sender.GetCurrentPageRowValues("Code")
                    If sender.Selection.IsRowSelectedByKey(key) Then
                        _Code.Add(key.ToString())
                    End If
                Next key
        End Select






    End Sub






















    Private Sub WH_genxls(ByVal NoticeID As String, ByVal Code As String)
        Using dc As New DataClasses_CPSExt()
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




            Dim sheetnotr As ISheet = hssfworkbook.CreateSheet("Data NoTR")
            '================== create column ===================
            Dim _fields = "No.,Client Code,D/N No,D/N Date,Insurer,ชื่อบริษัทประกันภัย,เลขรับแจ้ง,เลขตัวถัง,Policy No.,Period From,Period To,NetPremium Premium,TotalPremium".Split(",") '_data.DataFields.Split(",")
            Dim frow As IRow = sheetnotr.CreateRow(0)
            For i = 0 To _fields.Count - 1
                Dim cell = frow.CreateCell(i)
                cell.SetCellValue(_fields(i))
                cell.CellStyle = Header_Style
            Next

            ''================== init Column Length =====================
            'Dim _item_f01_Length = frow.GetCell(0).StringCellValue.Length
            'Dim _item_f02_Length = frow.GetCell(1).StringCellValue.Length
            'Dim _item_f03_Length = frow.GetCell(2).StringCellValue.Length
            'Dim _item_f04_Length = frow.GetCell(3).StringCellValue.Length
            'Dim _item_f05_Length = frow.GetCell(4).StringCellValue.Length
            'Dim _item_f06_Length = frow.GetCell(5).StringCellValue.Length
            'Dim _item_f07_Length = frow.GetCell(6).StringCellValue.Length
            'Dim _item_f08_Length = frow.GetCell(7).StringCellValue.Length
            'Dim _item_f09_Length = frow.GetCell(8).StringCellValue.Length
            'Dim _item_f10_Length = frow.GetCell(9).StringCellValue.Length
            'Dim _item_f11_Length = frow.GetCell(10).StringCellValue.Length
            'Dim _item_f12_Length = frow.GetCell(11).StringCellValue.Length
            'sheet1.SetColumnWidth(0, _item_f01_Length * 400)
            'sheet1.SetColumnWidth(1, _item_f02_Length * 400)
            'sheet1.SetColumnWidth(2, _item_f03_Length * 400)
            'sheet1.SetColumnWidth(3, _item_f04_Length * 400)
            'sheet1.SetColumnWidth(4, _item_f05_Length * 400)
            'sheet1.SetColumnWidth(5, _item_f06_Length * 400)
            'sheet1.SetColumnWidth(6, _item_f07_Length * 400)
            'sheet1.SetColumnWidth(7, _item_f08_Length * 400)
            'sheet1.SetColumnWidth(8, _item_f09_Length * 400)
            'sheet1.SetColumnWidth(9, _item_f10_Length * 400)
            'sheet1.SetColumnWidth(10, _item_f11_Length * 400)

            '================== create cell ===================
            Dim x As Integer = 1
            Dim j As Integer = 1
            'Dim total As Decimal = 0

            Dim data_notr = (From c In dc.tblNoticeDetails Where c.NoticeID.Equals(NoticeID) And c.f01.Equals("NOTR") And c.Code.Equals(Code)).ToList()
            For Each _item In data_notr
                Dim row As IRow = sheetnotr.CreateRow(j)
                ''================== init column Length =====================
                'If _item.f01.Length > _item_f01_Length Then
                '    sheet1.SetColumnWidth(0, _item.f01.Length * 400)
                '    _item_f01_Length = _item.f01.Length
                'End If
                'If _item.f02.Length > _item_f02_Length Then
                '    sheet1.SetColumnWidth(1, _item.f02.Length * 400)
                '    _item_f02_Length = _item.f02.Length
                'End If
                'If _item.f03.Length > _item_f03_Length Then
                '    sheet1.SetColumnWidth(2, _item.f03.Length * 400)
                '    _item_f03_Length = _item.f03.Length
                'End If
                'If _item.f04.Length > _item_f04_Length Then
                '    sheet1.SetColumnWidth(3, _item.f04.Length * 400)
                '    _item_f04_Length = _item.f04.Length
                'End If
                'If _item.f05.Length > _item_f05_Length Then
                '    sheet1.SetColumnWidth(4, _item.f05.Length * 400)
                '    _item_f05_Length = _item.f05.Length
                'End If
                'If _item.f06.Length > _item_f06_Length Then
                '    sheet1.SetColumnWidth(5, _item.f06.Length * 400)
                '    _item_f06_Length = _item.f06.Length
                'End If
                'If _item.f07.Length > _item_f07_Length Then
                '    sheet1.SetColumnWidth(6, _item.f07.Length * 400)
                '    _item_f07_Length = _item.f07.Length
                'End If
                'If _item.f08.Length > _item_f08_Length Then
                '    sheet1.SetColumnWidth(7, _item.f08.Length * 400)
                '    _item_f08_Length = _item.f08.Length
                'End If
                'If _item.f09.Length > _item_f09_Length Then
                '    sheet1.SetColumnWidth(8, _item.f09.Length * 400)
                '    _item_f09_Length = _item.f09.Length
                'End If
                'If _item.f10.Length > _item_f10_Length Then
                '    sheet1.SetColumnWidth(9, _item.f10.Length * 400)
                '    _item_f10_Length = _item.f10.Length
                'End If
                'If _item.f11.Length > _item_f11_Length Then
                '    sheet1.SetColumnWidth(10, _item.f11.Length * 400)
                '    _item_f11_Length = _item.f11.Length
                'End If


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
                row.CreateCell(12).CellStyle = Row_Style_Left

                row.GetCell(0).SetCellValue(j)
                row.GetCell(1).SetCellValue(_item.f02.Trim())
                row.GetCell(2).SetCellValue(_item.f03.Trim())
                row.GetCell(3).SetCellValue(_item.f04.Trim())
                row.GetCell(4).SetCellValue(_item.f05.Trim())
                row.GetCell(5).SetCellValue(_item.f06.Trim())
                row.GetCell(6).SetCellValue(_item.f07.Trim())
                row.GetCell(7).SetCellValue(_item.f08.Trim())
                row.GetCell(8).SetCellValue(_item.f09.Trim())
                row.GetCell(9).SetCellValue(_item.f10.Trim())
                row.GetCell(10).SetCellValue(_item.f11.Trim())
                row.GetCell(11).SetCellValue(_item.f12.Trim())
                row.GetCell(12).SetCellValue(_item.f13.Trim())

                'row.GetCell(8).SetCellValue(Convert.ToDecimal(_item.f09.Trim()).ToString("###,##0.00"))
                'row.GetCell(9).SetCellValue(Convert.ToDecimal(_item.f10.Trim()).ToString("###,##0.00"))
                'row.GetCell(10).SetCellValue(Convert.ToDecimal(_item.f11.Trim()).ToString("###,##0.00"))


                'total += Convert.ToDecimal(_item.f11)
                j += 1

            Next






            Dim sheettr As ISheet = hssfworkbook.CreateSheet("Data TR")
            '================== create column ===================
            Dim _fields2 = "No.,Client Code,D/N No,D/N Date,Insurer,ชื่อบริษัทประกันภัย,รายละเอียด,เลขรับแจ้ง,เลขตัวถัง,Policy No.,Period From,Period To,NetPremium Premium,TotalPremium,TRNO,TRDATE,Balance Amount".Split(",") '_data.DataFields.Split(",")
            Dim frow2 As IRow = sheettr.CreateRow(0)
            For i = 0 To _fields2.Count - 1
                Dim cell = frow2.CreateCell(i)
                cell.SetCellValue(_fields2(i))
                cell.CellStyle = Header_Style
            Next
            Dim data_tr = (From c In dc.tblNoticeDetails Where c.NoticeID.Equals(NoticeID) And c.f01.Equals("TR") And c.Code.Equals(Code)).ToList()
            j = 1
            For Each _item In data_tr
                Dim row As IRow = sheettr.CreateRow(j)
                ''================== init column Length =====================
                'If _item.f01.Length > _item_f01_Length Then
                '    sheet1.SetColumnWidth(0, _item.f01.Length * 400)
                '    _item_f01_Length = _item.f01.Length
                'End If
                'If _item.f02.Length > _item_f02_Length Then
                '    sheet1.SetColumnWidth(1, _item.f02.Length * 400)
                '    _item_f02_Length = _item.f02.Length
                'End If
                'If _item.f03.Length > _item_f03_Length Then
                '    sheet1.SetColumnWidth(2, _item.f03.Length * 400)
                '    _item_f03_Length = _item.f03.Length
                'End If
                'If _item.f04.Length > _item_f04_Length Then
                '    sheet1.SetColumnWidth(3, _item.f04.Length * 400)
                '    _item_f04_Length = _item.f04.Length
                'End If
                'If _item.f05.Length > _item_f05_Length Then
                '    sheet1.SetColumnWidth(4, _item.f05.Length * 400)
                '    _item_f05_Length = _item.f05.Length
                'End If
                'If _item.f06.Length > _item_f06_Length Then
                '    sheet1.SetColumnWidth(5, _item.f06.Length * 400)
                '    _item_f06_Length = _item.f06.Length
                'End If
                'If _item.f07.Length > _item_f07_Length Then
                '    sheet1.SetColumnWidth(6, _item.f07.Length * 400)
                '    _item_f07_Length = _item.f07.Length
                'End If
                'If _item.f08.Length > _item_f08_Length Then
                '    sheet1.SetColumnWidth(7, _item.f08.Length * 400)
                '    _item_f08_Length = _item.f08.Length
                'End If
                'If _item.f09.Length > _item_f09_Length Then
                '    sheet1.SetColumnWidth(8, _item.f09.Length * 400)
                '    _item_f09_Length = _item.f09.Length
                'End If
                'If _item.f10.Length > _item_f10_Length Then
                '    sheet1.SetColumnWidth(9, _item.f10.Length * 400)
                '    _item_f10_Length = _item.f10.Length
                'End If
                'If _item.f11.Length > _item_f11_Length Then
                '    sheet1.SetColumnWidth(10, _item.f11.Length * 400)
                '    _item_f11_Length = _item.f11.Length
                'End If


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
                row.CreateCell(12).CellStyle = Row_Style_Left
                row.CreateCell(13).CellStyle = Row_Style_Left
                row.CreateCell(14).CellStyle = Row_Style_Left
                row.CreateCell(15).CellStyle = Row_Style_Left
                row.CreateCell(16).CellStyle = Row_Style_Left


                row.GetCell(0).SetCellValue(j)
                row.GetCell(1).SetCellValue(_item.f02.Trim())
                row.GetCell(2).SetCellValue(_item.f03.Trim())
                row.GetCell(3).SetCellValue(_item.f04.Trim())
                row.GetCell(4).SetCellValue(_item.f05.Trim())
                row.GetCell(5).SetCellValue(_item.f06.Trim())
                row.GetCell(6).SetCellValue(_item.f07.Trim())
                row.GetCell(7).SetCellValue(_item.f08.Trim())
                row.GetCell(8).SetCellValue(_item.f09.Trim())
                row.GetCell(9).SetCellValue(_item.f10.Trim())
                row.GetCell(10).SetCellValue(_item.f11.Trim())
                row.GetCell(11).SetCellValue(_item.f12.Trim())
                row.GetCell(12).SetCellValue(_item.f13.Trim())
                row.GetCell(13).SetCellValue(_item.f14.Trim())
                row.GetCell(14).SetCellValue(_item.f15.Trim())
                row.GetCell(15).SetCellValue(_item.f16.Trim())
                row.GetCell(16).SetCellValue(_item.f17.Trim())

                'row.GetCell(8).SetCellValue(Convert.ToDecimal(_item.f09.Trim()).ToString("###,##0.00"))
                'row.GetCell(9).SetCellValue(Convert.ToDecimal(_item.f10.Trim()).ToString("###,##0.00"))
                'row.GetCell(10).SetCellValue(Convert.ToDecimal(_item.f11.Trim()).ToString("###,##0.00"))


                'total += Convert.ToDecimal(_item.f11)
                j += 1

            Next









            ''================== init  sheet property =================
            'sheet1.SetAutoFilter(New CellRangeAddress(0, 0, 0, 10))
            'sheet1.ProtectSheet("sheet1")

            Dim _filename = String.Format("{0}-แจ้งนำส่งกรมธรรม์ประกันภัยด่วน_{1}.xls", Code, DateTime.Now.ToString("yyyyMMddHHmmss"))
            Dim _path = String.Format("{0}\{1}", Server.MapPath("~/upload"), _filename)
            Dim fs As New FileStream(_path, FileMode.Create)
            hssfworkbook.Write(fs)
            fs.Close()

            Try


                Dim contact = (From c In dc.tblNoticeMailContacts Where c.Code.Equals(Code) And c.NoticeCode.Equals(NoticeCode)).FirstOrDefault()
                Dim strMailFrom As String = ""
                Dim strMailCC As String = ""

                Dim strMailTo As String = ""
                '"siriwimol@asia.lockton.com,dusit@asia.lockton.com,parichat@asia.lockton.com" '_agent.Mailto '  Replace("dusit@asia.lockton.com,parichat@asia.lockton.com", ";", ",") '
                Dim strSubject As String = ""
                Dim strMessage As New StringBuilder()


                Using dc_portal = New DataClasses_PortalDataContextExt
                    Dim _mailNotification = (From c In dc_portal.MailNotifications Where c.Code.Equals(NoticeCode)).FirstOrDefault()
                    strSubject = _mailNotification.MailSubject.Replace("{company}", contact.Name)
                    'strMessage.AppendLine("<!DOCTYPE html><html><body>")
                    strMessage.AppendLine(Server.HtmlDecode(_mailNotification.MailBody.Replace("{to}", contact.ContactName)))
                    'strMessage.AppendLine("</body></html>")
                    strMailFrom = "dusit@asia.lockton.com" '_mailNotification.MailFrom
                    strMailTo = "dusit@asia.lockton.com" 'contact.MailTo
                    'strMailCC = _mailNotification.MailCC & ";" & contact.MailCC
                    'strMailTo = _mailNotification.MailCC
                End Using


                Dim MySmtpClient As New System.Net.Mail.SmtpClient(ConfigurationSettings.AppSettings("smtp"))
                Dim msg As New System.Net.Mail.MailMessage()
                '===========================================================
                Dim bodyHTML As String = "<html><body>" & strMessage.ToString() & "<br><span style='font-size:16.0pt;font-family:Angsana New,serif;color:green'><img src='cid:LOGO_IMAGE2' alt='Logo' /><i>Please consider the environment before printing this e-mail.</i></span></body></html>"
                Dim alternateView As AlternateView = alternateView.CreateAlternateViewFromString(bodyHTML, Nothing, "text/html")
                Dim path_to_the_image_file2 As String = String.Format("{0}\{1}", Server.MapPath("~/images"), "mailsmallpicture.jpg")
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


            Catch ex As Exception
                Throw ex
            End Try

        End Using





    End Sub














End Class
