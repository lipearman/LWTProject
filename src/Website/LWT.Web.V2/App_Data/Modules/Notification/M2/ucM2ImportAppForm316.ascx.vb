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
Imports DevExpress.DataAccess.Excel
Imports System.ComponentModel
Imports ExcelDataReader
Partial Class Modules_ucM2ImportAppForm316
    Inherits PortalModuleControl

    '*******************************************************
    '
    ' The Page_Load server event handler on this user control is used
    ' to populate the current site settings from the config system
    '
    '*******************************************************
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Page.IsPostBack = False Then
            Session("View") = Nothing

        End If
    End Sub
    Private Sub BindData()
        Dim column As FineUI.GridColumn = Grid1.FindColumn(Grid1.SortField)
        BindGridWithSort(column.SortField, Grid1.SortDirection)
    End Sub
    Private Sub BindGridWithSort(ByVal sortField As String, ByVal sortDirection As String)
        If Session("View") Is Nothing Then
            Dim _appdata As New List(Of MyContext.ImportMotorAppForm316)
            Session("View") = _appdata
        End If

        Dim _data As List(Of MyContext.ImportMotorAppForm316) = Session("View")
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

        Dim hssfworkbook As New HSSFWorkbook()
        '====================== data new sheet ====================
        Dim sheet1 As ISheet = hssfworkbook.CreateSheet("Application")
        '================== create column ===================
        Dim _fields = "ReferenceNo,Title,Name,SurName,LoanContractNo,Address1,Address2,Mobile,OfficePhone,HomePhone,Insurer,TypeInsurance,CarPrice,Brand,Model,ModelCode,YearOfCar,CC,Seat,Weight,EngineNo,ChassisNo,Colour,LicenseNo,RegisterdName,UseOfCar,SumInsured,StartingDate,Showroom,ShowroomCode,ShowroomContactName,ShowroomContactPhone,CMIStatus,Leasing,Beneficiary,Agent,CoverageType,BillingName1,BillingAddress1,BillingTotals1,BillingName2,BillingAddress2,BillingTotals2,TempID,BatchNo,IDCard".Split(",")
        Dim frow As IRow = sheet1.CreateRow(0)
        For i = 0 To _fields.Count - 1
            Dim cell = frow.CreateCell(i)
            cell.SetCellValue(_fields(i))
        Next


        Dim columnHeaderTexts As New List(Of String)(_fields)
        Dim columnIndexs As New List(Of Integer)()
        For Each column In columnHeaderTexts
            For Each c As GridColumn In Grid1.Columns
                If c.HeaderText.Equals(column) Then
                    columnIndexs.Add(c.ColumnIndex)
                End If
            Next
        Next

        Dim iRow As Integer = 1
        For Each item_row As GridRow In Grid1.Rows
            Dim row As IRow = sheet1.CreateRow(iRow)

            Dim iCol As Integer = 0
            For Each columnIndex In columnIndexs
                Dim _CellData As String = item_row.Values(columnIndex).ToString().Replace("&#160;", "")

                If iCol = 27 Then
                    'StartingDate (27)


                    'cell.CellStyle = styles["cell"]; 
                    'cell.CellStyle.DataFormat = dataFormatCustom.GetFormat("yyyyMMdd HH:mm:ss");
                    row.CreateCell(iCol).CellStyle.DataFormat = hssfworkbook.CreateDataFormat().GetFormat("dd/MM/yyyy")
                    row.CreateCell(iCol).SetCellValue(DateTime.Parse(_CellData.Trim()))
                Else
                    row.CreateCell(iCol).SetCellValue(_CellData.Trim())
                End If






                iCol += 1
            Next

            iRow += 1
        Next


        Dim fs As New FileStream(filePath, FileMode.Create)
        hssfworkbook.Write(fs)
        fs.Close()



        Response.ClearContent()

        Response.AddHeader("content-disposition", "attachment; filename=" & fileName)
        Response.ContentType = "application/octet-stream"


        ''Response.ContentEncoding = System.Text.Encoding.UTF8
        'Response.BinaryWrite(System.Text.Encoding.UTF8.GetPreamble())
        'Response.Write(Utils.GetGridTableHtml(Grid1, ("ReferenceNo,Title,Name,SurName,LoanContractNo,Address1,Address2,Mobile,OfficePhone,HomePhone,Insurer,TypeInsurance,CarPrice,Brand,Model,ModelCode,YearOfCar,CC,Seat,Weight,EngineNo,ChassisNo,Colour,LicenseNo,RegisterdName,UseOfCar,SumInsured,StartingDate,Showroom,ShowroomCode,ShowroomContactName,ShowroomContactPhone,CMIStatus,Leasing,Beneficiary,Agent,CoverageType,BillingName1,BillingAddress1,BillingTotals1,BillingName2,BillingAddress2,BillingTotals2,TempID,BatchNo,IDCard").Split(",")))
        'Response.End()


        Response.WriteFile(filePath)
        'Response.Flush();
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
            If Not Right(fuAttatch.ShortFileName, 3).ToLower().Equals("xls") Then
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

            'ImportAppForm(newxlsFile)

            ImportAppForm2(newxlsFile)
        End Using


        WindowImportApplicationForm.Hidden = True



    End Sub
    Private Sub ImportAppForm(ByVal FilePath As String)
        '============ Master Table =================
        Dim _Portal_Table As New List(Of Portal_Table)
        Using dc As New DataClasses_PortalDataContextExt
            _Portal_Table = (From c In dc.Portal_Tables Where c.TABLE_NUMBER.Equals("T0002")).ToList()
        End Using

        Dim _NLTDB_Insurer As New List(Of Insurer)
        Using dc As New DataClasses_NLTDBExt
            _NLTDB_Insurer = (From c In dc.Insurers Where c.InsurerPrefix.EndsWith("(TB)")).ToList()
        End Using
        '===========================================

        Dim _ImportMotorAppForm316 As New List(Of MyContext.ImportMotorAppForm316)




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
                    Dim _B1 As String = row.GetCell(0).ToString()
                    Dim _B2 As String = row.GetCell(1).ToString()
                    Dim _B3 As String = row.GetCell(2).ToString()
                    Dim _B4 As String = row.GetCell(3).ToString()
                    Dim _B5 As String = row.GetCell(4).ToString()
                    Dim _B6 As String = row.GetCell(5).ToString()
                    Dim _B7 As String = row.GetCell(6).ToString()
                    Dim _B8 As String = row.GetCell(7).ToString()
                    Dim _B9 As String = row.GetCell(8).ToString()
                    Dim _B10 As String = row.GetCell(9).ToString()
                    Dim _B11 As String = row.GetCell(10).ToString()
                    Dim _B12 As String = row.GetCell(11).ToString()
                    Dim _B13 As String = row.GetCell(12).ToString()
                    Dim _B14 As String = row.GetCell(13).ToString()
                    Dim _B15 As String = row.GetCell(14).ToString()
                    Dim _B16 As String = row.GetCell(15).ToString()
                    Dim _B17 As String = row.GetCell(16).ToString()
                    Dim _B18 As String = row.GetCell(17).ToString()
                    Dim _B19 As String = row.GetCell(18).ToString()
                    Dim _B20 As String = row.GetCell(19).ToString()
                    Dim _B21 As String = row.GetCell(20).ToString()
                    Dim _B22 As String = row.GetCell(21).ToString()
                    Dim _B23 As String = row.GetCell(22).ToString()
                    Dim _B24 As String = row.GetCell(23).ToString()
                    Dim _B25 As String = row.GetCell(24).ToString()
                    Dim _B26 As String = row.GetCell(25).ToString()
                    Dim _B27 As String = row.GetCell(26).ToString()
                    Dim _B28 As String = row.GetCell(27).ToString()
                    Dim _B29 As String = row.GetCell(28).ToString()
                    Dim _B30 As String = row.GetCell(29).ToString()
                    Dim _B31 As String = row.GetCell(30).ToString()
                    Dim _B32 As String = row.GetCell(31).ToString()
                    Dim _B33 As String = row.GetCell(32).ToString()
                    Dim _B34 As String = row.GetCell(33).ToString()
                    Dim _B35 As String = row.GetCell(34).ToString()
                    Dim _B36 As String = row.GetCell(35).ToString()
                    Dim _B37 As String = row.GetCell(36).ToString()
                    Dim _B38 As String = row.GetCell(37).ToString()
                    Dim _B39 As String = row.GetCell(38).ToString()
                    Dim _B40 As String = row.GetCell(39).ToString()
                    Dim _B41 As String = row.GetCell(40).ToString()
                    Dim _B42 As String = row.GetCell(41).ToString()
                    Dim _B43 As String = row.GetCell(42).ToString()
                    Dim _B44 As String = row.GetCell(43).ToString()
                    Dim _B45 As String = row.GetCell(44).ToString()
                    Dim _B46 As String = row.GetCell(45).ToString()
                    Dim _B47 As String = row.GetCell(46).ToString()
                    Dim _B48 As String = row.GetCell(47).ToString()
                    Dim _B49 As String = row.GetCell(48).ToString()
                    Dim _B50 As String = row.GetCell(49).ToString()
                    Dim _B51 As String = row.GetCell(50).ToString()
                    Dim _B52 As String = row.GetCell(51).ToString()
                    Dim _B53 As String = row.GetCell(52).ToString()
                    Dim _B54 As String = row.GetCell(53).ToString()
                    Dim _B55 As String = row.GetCell(54).ToString()
                    Dim _B56 As String = row.GetCell(55).ToString()
                    Dim _B57 As String = row.GetCell(56).ToString()
                    Dim _B58 As String = row.GetCell(57).ToString()
                    Dim _B59 As String = row.GetCell(58).ToString()
                    Dim _B60 As String = row.GetCell(59).ToString()
                    Dim _B61 As String = row.GetCell(60).ToString()
                    Dim _B62 As String = row.GetCell(61).ToString()
                    Dim _B63 As String = row.GetCell(62).ToString()
                    Dim _B64 As String = row.GetCell(63).ToString()
                    Dim _B65 As String = row.GetCell(64).ToString()
                    Dim _B66 As String = row.GetCell(65).ToString()
                    Dim _B67 As String = row.GetCell(66).ToString()
                    Dim _B68 As String = row.GetCell(67).ToString()
                    Dim _B69 As String = row.GetCell(68).ToString()
                    Dim _B70 As String = row.GetCell(69).ToString()
                    Dim _B71 As String = row.GetCell(70).ToString()
                    Dim _B72 As String = row.GetCell(71).ToString()
                    Dim _B73 As String = row.GetCell(72).ToString()
                    Dim _B74 As String = row.GetCell(73).ToString()
                    Dim _B75 As String = row.GetCell(74).ToString()
                    Dim _B76 As String = row.GetCell(75).ToString()
                    Dim _B77 As String = row.GetCell(76).ToString()
                    Dim _B78 As String = row.GetCell(77).ToString()
                    Dim _B79 As String = row.GetCell(78).ToString()
                    Dim _B80 As String = row.GetCell(79).ToString()
                    Dim _B81 As String = row.GetCell(80).ToString()
                    Dim _B82 As String = row.GetCell(81).ToString()
                    Dim _B83 As String = row.GetCell(82).ToString()
                    Dim _B84 As String = row.GetCell(83).ToString()
                    Dim _B85 As String = row.GetCell(84).ToString()
                    Dim _B86 As String = row.GetCell(85).ToString()
                    Dim _B87 As String = row.GetCell(86).ToString()
                    Dim _B88 As String = row.GetCell(87).ToString()
                    Dim _B89 As String = row.GetCell(88).ToString()

                    Dim _ImportMotorAppForm316_Data As New MyContext.ImportMotorAppForm316()
                    With _ImportMotorAppForm316_Data
                        .RowNo = i

                        .ReferenceNo = _B1

                        .Title = ""
                        Dim _Title_TH = (From c In _Portal_Table Where c.ITEM_DESC_TH IsNot Nothing And _B15.StartsWith(c.ITEM_DESC_TH) And Not c.ITEM_DESC_TH.StartsWith("นาง") Select c.ITEM_DESC_TH).FirstOrDefault()
                        'Dim _Title_EN = (From c In _Portal_Table Where c.ITEM_DESC_EN IsNot Nothing And _B15.StartsWith(c.ITEM_DESC_EN) Select c.ITEM_DESC_EN).Single()
                        If _Title_TH IsNot Nothing Then
                            .Title = _Title_TH
                        Else

                            If _B15.ToString().StartsWith("นางสาว") Then
                                .Title = "นางสาว"
                            Else
                                If _B15.ToString().StartsWith("นาง") Then
                                    .Title = "นาง"
                                End If
                            End If
                            If _B15.ToString().StartsWith("พันโท") Then .Title = "พันโท"
                            If _B15.ToString().StartsWith("บจก.") Then .Title = "บจก."
                            If _B15.ToString().StartsWith("MR.") Then .Title = "MR."
                            If _B15.ToString().StartsWith("Mr.") Then .Title = "Mr."

                        End If
                        If Not String.IsNullOrEmpty(.Title) Then
                            _B15 = _B15.Replace(.Title, "")
                            'Else
                            '    '.Title = _B15
                            '    '.Name = _B15
                            '    '.SurName = _B15
                        End If
                        Dim _Name = _B15.Split(" ")
                        If _Name.Count = 2 Then
                            .Name = _Name(0)
                            .SurName = _Name(1)
                        Else
                            .Name = _B15
                            .SurName = ""
                        End If



                        .LoanContractNo = "0"
                        .Address1 = "444 อาคารเอ็มบีเค ทาวเวอร์ ถนนพญาไท แขวงวังใหม่ เขตปทุมวัน กรุงเทพฯ 10330"
                        .Address2 = "999/1 เดอะไนน์ทาวเวอร์ อาคาร A ชั้น 4 ถนนพระราม9 แขวงสวนหลวง เขตสวนหลวง กรุงเทพฯ 10250"
                        .Mobile = "0"
                        .OfficePhone = "02-3089600"
                        .HomePhone = "0"

                        Dim _ins = (From c In _NLTDB_Insurer Where c.InsurerNameThai.IndexOf(_B8) > -1).FirstOrDefault()
                        If _ins IsNot Nothing Then
                            'Dim _indID = (From c In _NLTDB_Insurer Where c.InsurerCode.Equals(_ins.InsurerCode) Order By c.InsurerNameThai).FirstOrDefault()
                            .Insurer = _ins.InsurerID
                        End If


                        '.CarPrice = _B50
                        .Brand = "Nissan"


                        Dim _TypeOfInsure = _B89.Split(":")

                        If _TypeOfInsure.Count <> 2 Then
                            Alert.Show(String.Format("รายการของ {0} TypeOfInsure ไม่ถูกต้อง : {1}", _B15, _B89))
                            Return
                        End If

                        Dim _typInsID As Integer = CInt(_TypeOfInsure(1).Trim())
                        .TypeInsurance = _typInsID

                        Dim _GroupCodeID As Integer = 0
                        'CarPrice and ModelCode


                        Dim _Model1 = (From c In dc.V_Models Where _B83.IndexOf(c.ModelCode) > -1 And c.ModelCode <> "-").FirstOrDefault()
                        If _Model1 IsNot Nothing Then
                            .Model = _Model1.Model
                            .ModelCode = _Model1.ModelCode
                            .CarPrice = _Model1.CarPrice
                            _GroupCodeID = _Model1.GroupCodeID
                        Else 'CarPrice
                            Dim _Model2 = (From c In dc.V_Models Where c.CarPrice.Equals(_B50) And c.ModelCode <> "-").FirstOrDefault()
                            If _Model2 Is Nothing Then
                                Alert.Show(String.Format("รายการของ {0} ไม่พบราคารถ {1} ในระบบ", _B15, _B50))
                                Return
                            End If
                            .Model = _Model2.Model
                            .ModelCode = _Model2.ModelCode
                            .CarPrice = _Model2.CarPrice
                            _GroupCodeID = _Model2.GroupCodeID
                        End If



                        If _B66.Equals("TRUE") Then .UseOfCar = "Personal"
                        If _B67.Equals("TRUE") Then .UseOfCar = "Commercial"

                        'If _B75.Equals("TRUE") Then .UseOfCar = "Personal"
                        'If _B76.Equals("TRUE") Then .UseOfCar = "Commercial"

                        'Dim _TypeOfInsure_Data = (From c In dc.TypeOfInsurrances Where c.typInsID.Equals(_typInsID)).FirstOrDefault()
                        'Dim _Suminsure = (From c In dc.Premiums Where c.InsuerID.Equals(.Insurer) _
                        '               And c.GroupCodeID.Equals(_GroupCodeID) _
                        '               And c.projID.Equals(_TypeOfInsure_Data.projID) _
                        '               And c.SchemeGroup.Equals(_TypeOfInsure_Data.Code.Trim()) _
                        '               And c.CarRegis.Equals(.UseOfCar)).FirstOrDefault()

                        'If _Suminsure Is Nothing Then
                        '    Alert.Show(String.Format("รายการของ {0} ไม่มีทุนประกันในระบบ", _B15))
                        '    Return
                        'End If
                        '.SumInsured = _Suminsure.SumInsured
                        .SumInsured = _B26


                        .YearOfCar = _B35
                        .CC = _B38
                        .Seat = _B84
                        .Weight = "0"
                        .EngineNo = _B39.Replace("'", "")
                        .ChassisNo = _B40.Replace("'", "")
                        .Colour = _B36
                        .LicenseNo = "ป้ายแดง" '_B37
                        .RegisterdName = ""





                        Dim _Startdate = _B64.Split("/")
                        If CInt(_Startdate(2)) > 2500 Then _Startdate(2) = (CInt(_Startdate(2)) - 543).ToString()
                        .StartingDate = String.Format("{0}/{1}/{2}", _Startdate(0), _Startdate(1), _Startdate(2))
                        '.StartingDate = _B64

                        Dim _Showroom = _B51.Split(":")
                        If _Showroom.Count <> 2 Then
                            Alert.Show(String.Format("รายการของ {0} showroom ไม่ถูกต้อง : {1}", _B15, _B51))
                            Return
                        End If
                        .Showroom = _B51.Trim()
                        .ShowroomCode = _Showroom(1).Trim()
                        '.ShowroomContactName = _B31
                        '.ShowroomContactPhone = _B32

                        .CMIStatus = "0"
                        .Leasing = "4"
                        .Beneficiary = "4"
                        .Agent = "316"
                        .CoverageType = "ประเภท 1"
                        .BillingName1 = ""
                        .BillingAddress1 = "999/1 เดอะไนน์ทาวเวอร์ อาคาร A ชั้น 4 ถนนพระราม9 แขวงสวนหลวง เขตสวนหลวง กรุงเทพฯ 10250"
                        .BillingTotals1 = ""
                        .BillingName2 = ""
                        .BillingAddress2 = ""
                        .BillingTotals2 = ""
                        .TempID = ""
                        .BatchNo = ""
                        .IDCard = _B16.Replace("'", "")

                        '_B89


                    End With
                    _ImportMotorAppForm316.Add(_ImportMotorAppForm316_Data)

                End While
            End Using

            If _ImportMotorAppForm316.Count > 0 Then
                Dim idx As Integer = 0

                Session("View") = _ImportMotorAppForm316

                WindowImportApplicationForm.Hidden = True
                BindData()

            Else
                Alert.Show("No Data", MessageBoxIcon.Warning)
            End If


        End Using

        'Using hssfworkbook As New HSSFWorkbook()

        '    End



        '    Dim i As Integer = 0
        '    Dim reader = New StringReader(txtData.Text)
        '    Dim line As String
        '    line = reader.ReadLine()
        '    Dim sb_error As New StringBuilder()
        '    Dim _ImportMotorAppForm316 As New List(Of Context.ImportMotorAppForm316)

        '    While line IsNot Nothing
        '        i += 1
        '        Dim _row() As String = line.Split(vbTab)

        '        If _row.Count = 88 Then
        '            Dim _B1 As String = _row(0)
        '            Dim _B2 As String = _row(1)
        '            Dim _B3 As String = _row(2)
        '            Dim _B4 As String = _row(3)
        '            Dim _B5 As String = _row(4)
        '            Dim _B6 As String = _row(5)
        '            Dim _B7 As String = _row(6)
        '            Dim _B8 As String = _row(7)
        '            Dim _B9 As String = _row(8)
        '            Dim _B10 As String = _row(9)
        '            Dim _B11 As String = _row(10)
        '            Dim _B12 As String = _row(11)
        '            Dim _B13 As String = _row(12)
        '            Dim _B14 As String = _row(13)
        '            Dim _B15 As String = _row(14)
        '            Dim _B16 As String = _row(15)
        '            Dim _B17 As String = _row(16)
        '            Dim _B18 As String = _row(17)
        '            Dim _B19 As String = _row(18)
        '            Dim _B20 As String = _row(19)
        '            Dim _B21 As String = _row(20)
        '            Dim _B22 As String = _row(21)
        '            Dim _B23 As String = _row(22)
        '            Dim _B24 As String = _row(23)
        '            Dim _B25 As String = _row(24)
        '            Dim _B26 As String = _row(25)
        '            Dim _B27 As String = _row(26)
        '            Dim _B28 As String = _row(27)
        '            Dim _B29 As String = _row(28)
        '            Dim _B30 As String = _row(29)
        '            Dim _B31 As String = _row(30)
        '            Dim _B32 As String = _row(31)
        '            Dim _B33 As String = _row(32)
        '            Dim _B34 As String = _row(33)
        '            Dim _B35 As String = _row(34)
        '            Dim _B36 As String = _row(35)
        '            Dim _B37 As String = _row(36)
        '            Dim _B38 As String = _row(37)
        '            Dim _B39 As String = _row(38)
        '            Dim _B40 As String = _row(39)
        '            Dim _B41 As String = _row(40)
        '            Dim _B42 As String = _row(41)
        '            Dim _B43 As String = _row(42)
        '            Dim _B44 As String = _row(43)
        '            Dim _B45 As String = _row(44)
        '            Dim _B46 As String = _row(45)
        '            Dim _B47 As String = _row(46)
        '            Dim _B48 As String = _row(47)
        '            Dim _B49 As String = _row(48)
        '            Dim _B50 As String = _row(49)
        '            Dim _B51 As String = _row(50)
        '            Dim _B52 As String = _row(51)
        '            Dim _B53 As String = _row(52)
        '            Dim _B54 As String = _row(53)
        '            Dim _B55 As String = _row(54)
        '            Dim _B56 As String = _row(55)
        '            Dim _B57 As String = _row(56)
        '            Dim _B58 As String = _row(57)
        '            Dim _B59 As String = _row(58)
        '            Dim _B60 As String = _row(59)
        '            Dim _B61 As String = _row(60)
        '            Dim _B62 As String = _row(61)
        '            Dim _B63 As String = _row(62)
        '            Dim _B64 As String = _row(63)
        '            Dim _B65 As String = _row(64)
        '            Dim _B66 As String = _row(65)
        '            Dim _B67 As String = _row(66)
        '            Dim _B68 As String = _row(67)
        '            Dim _B69 As String = _row(68)
        '            Dim _B70 As String = _row(69)
        '            Dim _B71 As String = _row(70)
        '            Dim _B72 As String = _row(71)
        '            Dim _B73 As String = _row(72)
        '            Dim _B74 As String = _row(73)
        '            Dim _B75 As String = _row(74)
        '            Dim _B76 As String = _row(75)
        '            Dim _B77 As String = _row(76)
        '            Dim _B78 As String = _row(77)
        '            Dim _B79 As String = _row(78)
        '            Dim _B80 As String = _row(79)
        '            Dim _B81 As String = _row(80)
        '            Dim _B82 As String = _row(81)
        '            Dim _B83 As String = _row(82)

        '            Dim _B84 As String = _row(83)
        '            Dim _B85 As String = _row(84)
        '            Dim _B86 As String = _row(85)
        '            Dim _B87 As String = _row(86)
        '            Dim _B88 As String = _row(87)




        '            Dim _ImportMotorAppForm316_Data As New Context.ImportMotorAppForm316()
        '            With _ImportMotorAppForm316_Data
        '                .ReferenceNo = _B1

        '                .Title = ""
        '                Dim _Title_TH = (From c In _Portal_Table Where c.ITEM_DESC_TH IsNot Nothing And _B15.StartsWith(c.ITEM_DESC_TH) And c.ITEM_DESC_TH IsNot "นาง" Select c.ITEM_DESC_TH).FirstOrDefault()
        '                'Dim _Title_EN = (From c In _Portal_Table Where c.ITEM_DESC_EN IsNot Nothing And _B15.StartsWith(c.ITEM_DESC_EN) Select c.ITEM_DESC_EN).Single()
        '                If _Title_TH IsNot Nothing Then
        '                    .Title = _Title_TH
        '                Else

        '                    If _B15.ToString().StartsWith("นางสาว") Then
        '                        .Title = "นางสาว"
        '                    Else
        '                        If _B15.ToString().StartsWith("นาง") Then
        '                            .Title = "นาง"
        '                        End If
        '                    End If
        '                    If _B15.ToString().StartsWith("พันโท") Then .Title = "พันโท"
        '                    If _B15.ToString().StartsWith("บจก.") Then .Title = "บจก."
        '                    If _B15.ToString().StartsWith("MR.") Then .Title = "MR."
        '                    If _B15.ToString().StartsWith("Mr.") Then .Title = "Mr."

        '                End If
        '                If Not String.IsNullOrEmpty(.Title) Then
        '                    _B15 = _B15.Replace(.Title, "")
        '                    'Else
        '                    '    '.Title = _B15
        '                    '    '.Name = _B15
        '                    '    '.SurName = _B15
        '                End If
        '                Dim _Name = _B15.Split(" ")
        '                .Name = _Name(0)
        '                .SurName = _Name(1)


        '                .LoanContractNo = "0"
        '                .Address1 = "444 อาคารเอ็มบีเค ทาวเวอร์ ถนนพญาไท แขวงวังใหม่ เขตปทุมวัน กรุงเทพมหานคร 10330"
        '                .Address2 = "999/1 เดอะไนน์ทาวเวอร์ อาคาร A ชั้น 4 ถนนพระราม9 แขวงสวนหลวง เขตสวนหลวง กรุงเทพฯ 10250"
        '                .Mobile = "0"
        '                .OfficePhone = "02-3089600"
        '                .HomePhone = "0"

        '                Dim _ins = (From c In _NLTDB_Insurer Where c.InsurerNameThai.IndexOf(_B8) > -1).FirstOrDefault()
        '                If _ins IsNot Nothing Then
        '                    'Dim _indID = (From c In _NLTDB_Insurer Where c.InsurerCode.Equals(_ins.InsurerCode) Order By c.InsurerNameThai).FirstOrDefault()
        '                    .Insurer = _ins.InsurerID
        '                End If

        '                .TypeInsurance = ddlTypeOfInsure.SelectedValue

        '                .CarPrice = _B50
        '                .Brand = "Nissan"

        '                Using dc As New DataClasses_NLTDBDataContext(webconfig._NLTDBConn)
        '                    'CarPrice and ModelCode
        '                    Dim _Model1 = (From c In dc.V_Models Where _B83.IndexOf(c.ModelCode) > -1 And c.ModelCode <> "-").FirstOrDefault()
        '                    If _Model1 IsNot Nothing Then
        '                        .Model = _Model1.Model
        '                        .ModelCode = _Model1.ModelCode
        '                    Else 'CarPrice
        '                        Dim _Model2 = (From c In dc.V_Models Where c.CarPrice.Equals(_B50) And c.ModelCode <> "-").FirstOrDefault()
        '                        .Model = _Model2.Model
        '                        .ModelCode = _Model2.ModelCode
        '                    End If
        '                End Using

        '                .YearOfCar = _B35
        '                .CC = _B38
        '                .Seat = _B84
        '                .Weight = "0"
        '                .EngineNo = _B39.Replace("'", "")
        '                .ChassisNo = _B40.Replace("'", "")
        '                .Colour = _B36
        '                .LicenseNo = "ป้ายแดง" '_B37
        '                .RegisterdName = ""

        '                If _B66.Equals("TRUE") Then .UseOfCar = "Personal"
        '                If _B67.Equals("TRUE") Then .UseOfCar = "Commercial"

        '                .SumInsured = _B26

        '                Dim _Startdate = _B64.Split("/")
        '                .StartingDate = String.Format("{0}/{1}/{2}", _Startdate(1), _Startdate(0), _Startdate(2))

        '                Dim _Showroom = _B51.Split(":")

        '                .Showroom = _B51.Trim()
        '                .ShowroomCode = _Showroom(1).Trim()
        '                '.ShowroomContactName = _B31
        '                '.ShowroomContactPhone = _B32

        '                .CMIStatus = "0"
        '                .Leasing = "4"
        '                .Beneficiary = "4"
        '                .Agent = "316"
        '                .CoverageType = "ประเภท 1"
        '                .BillingName1 = ""
        '                .BillingAddress1 = "999/1 เดอะไนน์ทาวเวอร์ อาคาร A ชั้น 4 ถนนพระราม9 แขวงสวนหลวง เขตสวนหลวง กรุงเทพฯ 10250"
        '                .BillingTotals1 = ""
        '                .BillingName2 = ""
        '                .BillingAddress2 = ""
        '                .BillingTotals2 = ""
        '                .TempID = ""
        '                .BatchNo = ""
        '                .IDCard = _B16 '.Replace("'", "")
        '            End With
        '            _ImportMotorAppForm316.Add(_ImportMotorAppForm316_Data)

        '        Else
        '            sb_error.AppendFormat("Row {0} invalid format data : <br> {1} <br>", i.ToString(), line)
        '        End If


        '        line = reader.ReadLine()
        '    End While


        '    If Not String.IsNullOrEmpty(sb_error.ToString()) Then
        '        Alert.Show(sb_error.ToString(), MessageBoxIcon.Error)
        '    Else
        '        If _ImportMotorAppForm316.Count > 0 Then
        '            Dim idx As Integer = 0

        '            Session("View") = _ImportMotorAppForm316

        '            WindowImportApplicationForm.Hidden = True
        '            BindData()

        '        Else
        '            Alert.Show("No Data", MessageBoxIcon.Warning)
        '        End If

        '    End If
    End Sub


    Private Function ExcelToDataTable(fileName As String) As DataTable
        ' Open file and return as Stream
        Using stream = File.Open(fileName, FileMode.Open, FileAccess.Read)
            Using reader = ExcelReaderFactory.CreateReader(stream)
                Dim result = reader.AsDataSet(New ExcelDataSetConfiguration() With { _
                                              .UseColumnDataType = True _
                                            , .ConfigureDataTable = Function(data) New ExcelDataTableConfiguration() With { _
                                                            .UseHeaderRow = True _
                    } _
                })
                'Get all the tables
                Dim table = result.Tables
                ' store it in data table
                Dim resultTable = table(0)
                Return resultTable
            End Using
        End Using
    End Function


    Private Sub ImportAppForm2(ByVal FilePath As String)
        '============ Master Table =================
        Dim _Portal_Table As New List(Of Portal_Table)
        Using dc As New DataClasses_PortalDataContextExt
            _Portal_Table = (From c In dc.Portal_Tables Where c.TABLE_NUMBER.Equals("T0002")).ToList()
        End Using

        Dim _NLTDB_Insurer As New List(Of Insurer)
        Using dc As New DataClasses_NLTDBExt
            _NLTDB_Insurer = (From c In dc.Insurers Where c.InsurerPrefix.EndsWith("(TB)")).ToList()
        End Using
        '===========================================

        Dim _ImportMotorAppForm316 As New List(Of MyContext.ImportMotorAppForm316)

        'Dim excelDataSource As New ExcelDataSource()
        'excelDataSource.FileName = FilePath
        'Dim worksheetSettings As New ExcelWorksheetSettings("ข้อมูลรับแจ้ง")
        'excelDataSource.SourceOptions = New ExcelSourceOptions(worksheetSettings)
        'excelDataSource.SourceOptions.UseFirstRowAsHeader = True
        'excelDataSource.SourceOptions.SkipEmptyRows = True
        'excelDataSource.Fill()
        'Dim resultView As DevExpress.DataAccess.Native.Excel.DataView = TryCast(DirectCast(excelDataSource, IListSource).GetList(), DevExpress.DataAccess.Native.Excel.DataView)

        'For Each item In resultView
        '    Dim a = item
        'Next

        'Dim dt = ExcelToDataTable(FilePath)

        'For i = 0 To dt.Rows.Count - 1 
        '    Dim _A1 As String = dt.Rows(i)(0) 'วันที่รับแจ้ง
        '    Dim _A2 As String = dt.Rows(i)(1).ToString() 'Code ประกันภัย
        '    Dim _A3 As String = dt.Rows(i)(2).ToString() 'เลขสติ๊กเกอร์
        '    Dim _A4 As String = dt.Rows(i)(3).ToString() 'กธ พรบ เลขที่
        '    Dim _A5 As String = dt.Rows(i)(4).ToString() 'ชื่อประกันภัย
        '    Dim _A6 As String = dt.Rows(i)(5).ToString() 'เลขที่รับแจ้ง
        '    Dim _A7 As String = dt.Rows(i)(6).ToString() 'ตลาดสาขา
        '    Dim _A8 As String = dt.Rows(i)(7).ToString() 'Code ตลาดสาขา
        '    Dim _A9 As String = dt.Rows(i)(8).ToString() 'ผู้แจ้ง
        '    Dim _A10 As String = dt.Rows(i)(9).ToString() 'กลุ่ม
        '    Dim _A11 As String = dt.Rows(i)(10).ToString() 'ผู้รับแจ้ง
        '    Dim _A12 As String = dt.Rows(i)(11).ToString() 'ชื่อผู้เอาประกันภัย
        '    Dim _A13 As String = dt.Rows(i)(12).ToString() 'เลขที่บัตร
        '    Dim _A14 As String = dt.Rows(i)(13).ToString() 'ที่อยุ่ปัจจุบัน/ภพ.20
        '    Dim _A15 As String = dt.Rows(i)(14).ToString() 'ที่อยุ่ปัจจุบัน/ภพ.20
        '    Dim _A16 As String = dt.Rows(i)(15).ToString() 'เบอร์โทร
        '    Dim _A17 As String = dt.Rows(i)(16).ToString() 'ที่อยู่ส่งเอกสาร
        '    Dim _A18 As String = dt.Rows(i)(17).ToString() 'ที่อยู่ส่งเอกสาร1
        '    Dim _A19 As String = dt.Rows(i)(18).ToString() 'เบอร์โทร1
        '    Dim _A20 As String = dt.Rows(i)(19).ToString() 'ทุนประกัน
        '    Dim _A21 As String = dt.Rows(i)(20).ToString() 'เบี้ยประกันสุทธิ
        '    Dim _A22 As String = dt.Rows(i)(21).ToString() 'เบี้ยประกันรวม
        '    Dim _A23 As String = dt.Rows(i)(22).ToString() 'เบี้ยพรบ สุทธิ
        '    Dim _A24 As String = dt.Rows(i)(23).ToString() 'เบี้ยพรบ รวม
        '    Dim _A25 As String = dt.Rows(i)(24).ToString() 'เบี้ยประกัน+พรบ
        '    Dim _A26 As String = dt.Rows(i)(25).ToString() 'เลขคุ้มครองชั่วคราว
        '    Dim _A27 As String = dt.Rows(i)(26).ToString() 'ยี่ห้อ
        '    Dim _A28 As String = dt.Rows(i)(27).ToString() 'รุ่น
        '    Dim _A29 As String = dt.Rows(i)(28).ToString() 'ปี
        '    Dim _A30 As String = dt.Rows(i)(29).ToString() 'สี
        '    Dim _A31 As String = dt.Rows(i)(30).ToString() 'เลขทะเบียน
        '    Dim _A32 As String = dt.Rows(i)(31).ToString() 'ขนาดเครื่องยนต์
        '    Dim _A33 As String = dt.Rows(i)(32).ToString() 'เลขเครื่องยนต์
        '    Dim _A34 As String = dt.Rows(i)(33).ToString() 'เลขตัวถัง
        '    Dim _A35 As String = dt.Rows(i)(34).ToString() 'เหตุผลกรณี เลขเครื่อง/เลขถังซ้ำ
        '    Dim _A36 As String = dt.Rows(i)(35).ToString() 'ภาคสมัครใจ แถม/ไม่แถม
        '    Dim _A37 As String = dt.Rows(i)(36).ToString() 'ภาคบังคับ แถม/ไม่แถม
        '    Dim _A38 As String = dt.Rows(i)(37).ToString() 'PATTERN RATE
        '    Dim _A39 As String = dt.Rows(i)(38).ToString() 'ราคารถ
        '    Dim _A40 As String = dt.Rows(i)(39).ToString() 'ราคากลาง
        '    Dim _A41 As String = dt.Rows(i)(40).ToString() 'ชื่อ Dealer
        '    Dim _A42 As String = dt.Rows(i)(41).ToString() 'ประเภทการประกันภัย
        '    Dim _A43 As String = dt.Rows(i)(42).ToString() 'ผู้ขับขี่
        '    Dim _A44 As String = dt.Rows(i)(43).ToString() 'ชื่อ 1
        '    Dim _A45 As String = dt.Rows(i)(44) 'วัน/เดือน/ปีเกิด1
        '    Dim _A46 As String = dt.Rows(i)(45).ToString() 'เลขที่ ID1
        '    Dim _A47 As String = dt.Rows(i)(46).ToString() 'ใบขับขี่ 1 เลขที่
        '    Dim _A48 As String = dt.Rows(i)(47).ToString() 'ชื่อ 2
        '    Dim _A49 As String = dt.Rows(i)(48) 'วัน/เดือน/ปีเกิด2
        '    Dim _A50 As String = dt.Rows(i)(49).ToString() 'เลขที่ ID2
        '    Dim _A51 As String = dt.Rows(i)(50).ToString() 'ใบขับขี่ 2 เลขที่
        '    Dim _A52 As String = dt.Rows(i)(51) 'วันที่คุ้มครอง
        '    Dim _A53 As String = dt.Rows(i)(52) 'วันสิ้นสุดคุ้มครอง
        '    Dim _A54 As String = dt.Rows(i)(53).ToString() 'ประเภทการใช้รถ
        '    Dim _A55 As String = dt.Rows(i)(54).ToString() 'หมายเหตุอื่นๆ
        '    Dim _A56 As String = dt.Rows(i)(55).ToString() 'รหัส
        '    Dim _A57 As String = dt.Rows(i)(56).ToString() 'ชนิดรถ
        '    Dim _A58 As String = dt.Rows(i)(57).ToString() 'ประเภทรถ
        '    Dim _A59 As String = dt.Rows(i)(58).ToString() 'ประเภทรถอื่นๆ
        '    Dim _A60 As String = dt.Rows(i)(59).ToString() 'ประเภทการซ่อม
        '    Dim _A61 As String = dt.Rows(i)(60).ToString() 'Code Rebate
        '    Dim _A62 As String = dt.Rows(i)(61).ToString() 'หมายเหตุ
        '    Dim _A63 As String = dt.Rows(i)(62).ToString() 'จำนวนที่นั่ง
        '    Dim _A64 As String = dt.Rows(i)(63).ToString() 'เลขประจำตัวผู้เสียภาษี
        '    Dim _A65 As String = dt.Rows(i)(64).ToString() 'ชื่อกรรมการบริษัท
        '    Dim _A66 As String = dt.Rows(i)(65).ToString() 'ประเภทเอกสาร
        '    Dim _A67 As String = dt.Rows(i)(66).ToString() 'จดทะเบียนภาษีมูลค่าเพิ่มหรือไม่
        '    Dim _A68 As String = dt.Rows(i)(67).ToString() 'สาขาที่
        '    Dim _A69 As String = dt.Rows(i)(68).ToString() 'ผู้รับผลประโยชน์
        '    Dim _A70 As String = dt.Rows(i)(69).ToString() 'เลขที่ใบคำขอเช่าซื้อ

        '    Dim _A71 As String = dt.Rows(i)(70).ToString() 'TypeOfInsure
        '    Dim _A72 As String = dt.Rows(i)(71).ToString() 'Showroom
        'Next

        'For Each item In dt.Rows
        '    Dim a = item
        'Next

        'Using File = New FileStream(FilePath, FileMode.Open, FileAccess.Read)
        '    Dim HSSFWorkbook = New HSSFWorkbook(File)
        '    Dim sheet = HSSFWorkbook.GetSheetAt(0)
        '    Dim rows = sheet.GetRowEnumerator()
        '    rows.MoveNext()

        '    Dim i As Integer = 0
        Dim dt = ExcelToDataTable(FilePath)
        Using dc As New DataClasses_NLTDBExt

            'While rows.MoveNext()
            '    i += 1
            '    Dim row = DirectCast(rows.Current, HSSFRow)
            '    Dim _A1 As String = row.GetCell(0).ToString() 'วันที่รับแจ้ง
            '    Dim _A2 As String = row.GetCell(1).ToString() 'Code ประกันภัย
            '    Dim _A3 As String = row.GetCell(2).ToString() 'เลขสติ๊กเกอร์
            '    Dim _A4 As String = row.GetCell(3).ToString() 'กธ พรบ เลขที่
            '    Dim _A5 As String = row.GetCell(4).ToString() 'ชื่อประกันภัย
            '    Dim _A6 As String = row.GetCell(5).ToString() 'เลขที่รับแจ้ง
            '    Dim _A7 As String = row.GetCell(6).ToString() 'ตลาดสาขา
            '    Dim _A8 As String = row.GetCell(7).ToString() 'Code ตลาดสาขา
            '    Dim _A9 As String = row.GetCell(8).ToString() 'ผู้แจ้ง
            '    Dim _A10 As String = row.GetCell(9).ToString() 'กลุ่ม
            '    Dim _A11 As String = row.GetCell(10).ToString() 'ผู้รับแจ้ง
            '    Dim _A12 As String = row.GetCell(11).ToString() 'ชื่อผู้เอาประกันภัย
            '    Dim _A13 As String = row.GetCell(12).ToString() 'เลขที่บัตร
            '    Dim _A14 As String = row.GetCell(13).ToString() 'ที่อยุ่ปัจจุบัน/ภพ.20
            '    Dim _A15 As String = row.GetCell(14).ToString() 'ที่อยุ่ปัจจุบัน/ภพ.20
            '    Dim _A16 As String = row.GetCell(15).ToString() 'เบอร์โทร
            '    Dim _A17 As String = row.GetCell(16).ToString() 'ที่อยู่ส่งเอกสาร
            '    Dim _A18 As String = row.GetCell(17).ToString() 'ที่อยู่ส่งเอกสาร1
            '    Dim _A19 As String = row.GetCell(18).ToString() 'เบอร์โทร1
            '    Dim _A20 As String = row.GetCell(19).ToString() 'ทุนประกัน
            '    Dim _A21 As String = row.GetCell(20).ToString() 'เบี้ยประกันสุทธิ
            '    Dim _A22 As String = row.GetCell(21).ToString() 'เบี้ยประกันรวม
            '    Dim _A23 As String = row.GetCell(22).ToString() 'เบี้ยพรบ สุทธิ
            '    Dim _A24 As String = row.GetCell(23).ToString() 'เบี้ยพรบ รวม
            '    Dim _A25 As String = row.GetCell(24).ToString() 'เบี้ยประกัน+พรบ
            '    Dim _A26 As String = row.GetCell(25).ToString() 'เลขคุ้มครองชั่วคราว
            '    Dim _A27 As String = row.GetCell(26).ToString() 'ยี่ห้อ
            '    Dim _A28 As String = row.GetCell(27).ToString() 'รุ่น
            '    Dim _A29 As String = row.GetCell(28).ToString() 'ปี
            '    Dim _A30 As String = row.GetCell(29).ToString() 'สี
            '    Dim _A31 As String = row.GetCell(30).ToString() 'เลขทะเบียน
            '    Dim _A32 As String = row.GetCell(31).ToString() 'ขนาดเครื่องยนต์
            '    Dim _A33 As String = row.GetCell(32).ToString() 'เลขเครื่องยนต์
            '    Dim _A34 As String = row.GetCell(33).ToString() 'เลขตัวถัง
            '    Dim _A35 As String = row.GetCell(34).ToString() 'เหตุผลกรณี เลขเครื่อง/เลขถังซ้ำ
            '    Dim _A36 As String = row.GetCell(35).ToString() 'ภาคสมัครใจ แถม/ไม่แถม
            '    Dim _A37 As String = row.GetCell(36).ToString() 'ภาคบังคับ แถม/ไม่แถม
            '    Dim _A38 As String = row.GetCell(37).ToString() 'PATTERN RATE
            '    Dim _A39 As String = row.GetCell(38).ToString() 'ราคารถ
            '    Dim _A40 As String = row.GetCell(39).ToString() 'ราคากลาง
            '    Dim _A41 As String = row.GetCell(40).ToString() 'ชื่อ Dealer
            '    Dim _A42 As String = row.GetCell(41).ToString() 'ประเภทการประกันภัย
            '    Dim _A43 As String = row.GetCell(42).ToString() 'ผู้ขับขี่
            '    Dim _A44 As String = row.GetCell(43).ToString() 'ชื่อ 1
            '    Dim _A45 As String = row.GetCell(44).ToString() 'วัน/เดือน/ปีเกิด1
            '    Dim _A46 As String = row.GetCell(45).ToString() 'เลขที่ ID1
            '    Dim _A47 As String = row.GetCell(46).ToString() 'ใบขับขี่ 1 เลขที่
            '    Dim _A48 As String = row.GetCell(47).ToString() 'ชื่อ 2
            '    Dim _A49 As String = row.GetCell(48).ToString() 'วัน/เดือน/ปีเกิด2
            '    Dim _A50 As String = row.GetCell(49).ToString() 'เลขที่ ID2
            '    Dim _A51 As String = row.GetCell(50).ToString() 'ใบขับขี่ 2 เลขที่
            '    Dim _A52 As String = DirectCast(row.GetCell(51), NPOI.HSSF.UserModel.HSSFCell).DateCellValue 'วันที่คุ้มครอง
            '    Dim _A53 As String = row.GetCell(52).ToString() 'วันสิ้นสุดคุ้มครอง
            '    Dim _A54 As String = row.GetCell(53).ToString() 'ประเภทการใช้รถ
            '    Dim _A55 As String = row.GetCell(54).ToString() 'หมายเหตุอื่นๆ
            '    Dim _A56 As String = row.GetCell(55).ToString() 'รหัส
            '    Dim _A57 As String = row.GetCell(56).ToString() 'ชนิดรถ
            '    Dim _A58 As String = row.GetCell(57).ToString() 'ประเภทรถ
            '    Dim _A59 As String = row.GetCell(58).ToString() 'ประเภทรถอื่นๆ
            '    Dim _A60 As String = row.GetCell(59).ToString() 'ประเภทการซ่อม
            '    Dim _A61 As String = row.GetCell(60).ToString() 'Code Rebate
            '    Dim _A62 As String = row.GetCell(61).ToString() 'หมายเหตุ
            '    Dim _A63 As String = row.GetCell(62).ToString() 'จำนวนที่นั่ง
            '    Dim _A64 As String = row.GetCell(63).ToString() 'เลขประจำตัวผู้เสียภาษี
            '    Dim _A65 As String = row.GetCell(64).ToString() 'ชื่อกรรมการบริษัท
            '    Dim _A66 As String = row.GetCell(65).ToString() 'ประเภทเอกสาร
            '    Dim _A67 As String = row.GetCell(66).ToString() 'จดทะเบียนภาษีมูลค่าเพิ่มหรือไม่
            '    Dim _A68 As String = row.GetCell(67).ToString() 'สาขาที่
            '    Dim _A69 As String = row.GetCell(68).ToString() 'ผู้รับผลประโยชน์
            '    Dim _A70 As String = row.GetCell(69).ToString() 'เลขที่ใบคำขอเช่าซื้อ

            '    Dim _A71 As String = row.GetCell(70).ToString() 'TypeOfInsure
            '    Dim _A72 As String = row.GetCell(71).ToString() 'Showroom

            For i = 0 To dt.Rows.Count - 1
                Dim _A1 As Date = dt.Rows(i)(0) 'วันที่รับแจ้ง
                Dim _A2 As String = dt.Rows(i)(1).ToString() 'Code ประกันภัย
                Dim _A3 As String = dt.Rows(i)(2).ToString() 'เลขสติ๊กเกอร์
                Dim _A4 As String = dt.Rows(i)(3).ToString() 'กธ พรบ เลขที่
                Dim _A5 As String = dt.Rows(i)(4).ToString() 'ชื่อประกันภัย
                Dim _A6 As String = dt.Rows(i)(5).ToString() 'เลขที่รับแจ้ง
                Dim _A7 As String = dt.Rows(i)(6).ToString() 'ตลาดสาขา
                Dim _A8 As String = dt.Rows(i)(7).ToString() 'Code ตลาดสาขา
                Dim _A9 As String = dt.Rows(i)(8).ToString() 'ผู้แจ้ง
                Dim _A10 As String = dt.Rows(i)(9).ToString() 'กลุ่ม
                Dim _A11 As String = dt.Rows(i)(10).ToString() 'ผู้รับแจ้ง
                Dim _A12 As String = dt.Rows(i)(11).ToString() 'ชื่อผู้เอาประกันภัย
                Dim _A13 As String = dt.Rows(i)(12).ToString() 'เลขที่บัตร
                Dim _A14 As String = dt.Rows(i)(13).ToString() 'ที่อยุ่ปัจจุบัน/ภพ.20
                Dim _A15 As String = dt.Rows(i)(14).ToString() 'ที่อยุ่ปัจจุบัน/ภพ.20
                Dim _A16 As String = dt.Rows(i)(15).ToString() 'เบอร์โทร
                Dim _A17 As String = dt.Rows(i)(16).ToString() 'ที่อยู่ส่งเอกสาร
                Dim _A18 As String = dt.Rows(i)(17).ToString() 'ที่อยู่ส่งเอกสาร1
                Dim _A19 As String = dt.Rows(i)(18).ToString() 'เบอร์โทร1
                Dim _A20 As String = dt.Rows(i)(19).ToString() 'ทุนประกัน
                Dim _A21 As String = dt.Rows(i)(20).ToString() 'เบี้ยประกันสุทธิ
                Dim _A22 As String = dt.Rows(i)(21).ToString() 'เบี้ยประกันรวม
                Dim _A23 As String = dt.Rows(i)(22).ToString() 'เบี้ยพรบ สุทธิ
                Dim _A24 As String = dt.Rows(i)(23).ToString() 'เบี้ยพรบ รวม
                Dim _A25 As String = dt.Rows(i)(24).ToString() 'เบี้ยประกัน+พรบ
                Dim _A26 As String = dt.Rows(i)(25).ToString() 'เลขคุ้มครองชั่วคราว
                Dim _A27 As String = dt.Rows(i)(26).ToString() 'ยี่ห้อ
                Dim _A28 As String = dt.Rows(i)(27).ToString() 'รุ่น
                Dim _A29 As String = dt.Rows(i)(28).ToString() 'ปี
                Dim _A30 As String = dt.Rows(i)(29).ToString() 'สี
                Dim _A31 As String = dt.Rows(i)(30).ToString() 'เลขทะเบียน
                Dim _A32 As String = dt.Rows(i)(31).ToString() 'ขนาดเครื่องยนต์
                Dim _A33 As String = dt.Rows(i)(32).ToString() 'เลขเครื่องยนต์
                Dim _A34 As String = dt.Rows(i)(33).ToString() 'เลขตัวถัง
                Dim _A35 As String = dt.Rows(i)(34).ToString() 'เหตุผลกรณี เลขเครื่อง/เลขถังซ้ำ
                Dim _A36 As String = dt.Rows(i)(35).ToString() 'ภาคสมัครใจ แถม/ไม่แถม
                Dim _A37 As String = dt.Rows(i)(36).ToString() 'ภาคบังคับ แถม/ไม่แถม
                ''Dim _A38 As String = dt.Rows(i)(37).ToString() 'PATTERN RATE
                Dim _A38 As String = dt.Rows(i)(37).ToString() 'ราคารถ
                Dim _A39 As String = dt.Rows(i)(38).ToString() 'ราคากลาง
                Dim _A40 As String = dt.Rows(i)(39).ToString() 'ชื่อ Dealer
                Dim _A41 As String = dt.Rows(i)(40).ToString() 'ประเภทการประกันภัย
                Dim _A42 As String = dt.Rows(i)(41).ToString() 'ผู้ขับขี่
                Dim _A43 As String = dt.Rows(i)(42).ToString() 'ชื่อ 1
                Dim _A44 As String = dt.Rows(i)(43).ToString() 'วัน/เดือน/ปีเกิด1
                Dim _A45 As String = dt.Rows(i)(44).ToString() 'เลขที่ ID1
                Dim _A46 As String = dt.Rows(i)(45).ToString() 'ใบขับขี่ 1 เลขที่
                Dim _A47 As String = dt.Rows(i)(46).ToString() 'ชื่อ 2
                Dim _A48 As String = dt.Rows(i)(47).ToString() 'วัน/เดือน/ปีเกิด2
                Dim _A49 As String = dt.Rows(i)(48).ToString() 'เลขที่ ID2
                Dim _A50 As String = dt.Rows(i)(49).ToString() 'ใบขับขี่ 2 เลขที่
                Dim _A51 As Date = dt.Rows(i)(50) 'วันที่คุ้มครอง
                Dim _A52 As Date = dt.Rows(i)(51) 'วันสิ้นสุดคุ้มครอง
                Dim _A53 As String = dt.Rows(i)(52).ToString() 'ประเภทการใช้รถ
                Dim _A54 As String = dt.Rows(i)(53).ToString() 'หมายเหตุอื่นๆ
                'Dim _A56 As String = dt.Rows(i)(55).ToString() 'รหัส
                Dim _A55 As String = dt.Rows(i)(54).ToString() 'ชนิดรถ
                Dim _A56 As String = dt.Rows(i)(55).ToString() 'ประเภทรถ
                Dim _A57 As String = dt.Rows(i)(56).ToString() 'ประเภทรถอื่นๆ
                Dim _A58 As String = dt.Rows(i)(57).ToString() 'ประเภทการซ่อม
                Dim _A59 As String = dt.Rows(i)(58).ToString() 'Code Rebate
                Dim _A60 As String = dt.Rows(i)(59).ToString() 'หมายเหตุ
                Dim _A61 As String = dt.Rows(i)(60).ToString() 'จำนวนที่นั่ง
                Dim _A62 As String = dt.Rows(i)(61).ToString() 'เลขประจำตัวผู้เสียภาษี
                Dim _A63 As String = dt.Rows(i)(62).ToString() 'ชื่อกรรมการบริษัท
                Dim _A64 As String = dt.Rows(i)(63).ToString() 'ประเภทเอกสาร
                Dim _A65 As String = dt.Rows(i)(64).ToString() 'จดทะเบียนภาษีมูลค่าเพิ่มหรือไม่
                Dim _A66 As String = dt.Rows(i)(65).ToString() 'สาขาที่
                Dim _A67 As String = dt.Rows(i)(66).ToString() 'ผู้รับผลประโยชน์
                Dim _A68 As String = dt.Rows(i)(67).ToString() 'เลขที่ใบคำขอเช่าซื้อ

                Dim _A69 As String = dt.Rows(i)(68).ToString() 'TypeOfInsure
                Dim _A70 As String = dt.Rows(i)(69).ToString() 'Showroom

                'If String.IsNullOrEmpty(_A1) Then Exit While


                Dim _ImportMotorAppForm316_Data As New MyContext.ImportMotorAppForm316()
                With _ImportMotorAppForm316_Data
                    .RowNo = i

                    .ReferenceNo = ""

                    .Title = ""
                    Dim _Title_TH = (From c In _Portal_Table Where c.ITEM_DESC_TH IsNot Nothing And _A12.StartsWith(c.ITEM_DESC_TH) And Not c.ITEM_DESC_TH.StartsWith("นาง") Select c.ITEM_DESC_TH).FirstOrDefault()
                    'Dim _Title_EN = (From c In _Portal_Table Where c.ITEM_DESC_EN IsNot Nothing And _B15.StartsWith(c.ITEM_DESC_EN) Select c.ITEM_DESC_EN).Single()
                    If _Title_TH IsNot Nothing Then
                        .Title = _Title_TH
                    Else

                        If _A12.ToString().StartsWith("นางสาว") Then
                            .Title = "นางสาว"
                        Else
                            If _A12.ToString().StartsWith("นาง") Then
                                .Title = "นาง"
                            End If
                        End If
                        If _A12.ToString().StartsWith("พันโท") Then .Title = "พันโท"
                        If _A12.ToString().StartsWith("บจก.") Then .Title = "บจก."
                        If _A12.ToString().StartsWith("MR.") Then .Title = "MR."
                        If _A12.ToString().StartsWith("Mr.") Then .Title = "Mr."

                    End If
                    If Not String.IsNullOrEmpty(.Title) Then
                        _A12 = _A12.Replace(.Title, "")
                        'Else
                        '    '.Title = _B15
                        '    '.Name = _B15
                        '    '.SurName = _B15
                    End If
                    Dim _Name = _A12.Split(" ")
                    If _Name.Count = 2 Then
                        .Name = _Name(0)
                        .SurName = _Name(1)
                    Else
                        .Name = _A12
                        .SurName = ""
                    End If



                    .LoanContractNo = "0"
                    .Address1 = "444 อาคารเอ็มบีเค ทาวเวอร์ ถนนพญาไท แขวงวังใหม่ เขตปทุมวัน กรุงเทพฯ 10330"
                    .Address2 = "999/1 เดอะไนน์ทาวเวอร์ อาคาร A ชั้น 4 ถนนพระราม9 แขวงสวนหลวง เขตสวนหลวง กรุงเทพฯ 10250"
                    .Mobile = "0"
                    .OfficePhone = "02-3089600"
                    .HomePhone = "0"

                    Dim _ins = (From c In _NLTDB_Insurer Where c.InsurerNameThai.IndexOf(_A5) > -1).FirstOrDefault()
                    If _ins IsNot Nothing Then
                        'Dim _indID = (From c In _NLTDB_Insurer Where c.InsurerCode.Equals(_ins.InsurerCode) Order By c.InsurerNameThai).FirstOrDefault()
                        .Insurer = _ins.InsurerID
                    End If


                    '.CarPrice = _B50
                    .Brand = _A27


                    Dim _TypeOfInsure = _A69.Split(":")

                    If _TypeOfInsure.Count <> 2 Then
                        Alert.Show(String.Format("รายการของ {0} TypeOfInsure ไม่ถูกต้อง : {1}", _A12, _A69))
                        Return
                    End If

                    Dim _typInsID As Integer = CInt(_TypeOfInsure(1).Trim())
                    .TypeInsurance = _typInsID

                    Dim _GroupCodeID As Integer = 0
                    'CarPrice and ModelCode


                    Dim _Model1 = (From c In dc.V_Models Where _A28.IndexOf(c.ModelCode) > -1 And c.ModelCode <> "-").FirstOrDefault()
                    If _Model1 IsNot Nothing Then
                        .Model = _Model1.Model
                        .ModelCode = _Model1.ModelCode
                        .CarPrice = _Model1.CarPrice
                        _GroupCodeID = _Model1.GroupCodeID
                    Else 'CarPrice
                        Dim _Model2 = (From c In dc.V_Models Where c.CarPrice.Equals(_A38.Replace("'", "")) And c.ModelCode <> "-").FirstOrDefault()
                        If _Model2 Is Nothing Then
                            Alert.Show(String.Format("รายการของ {0} ไม่พบราคารถ {1} ในระบบ", _A12, _A38))
                            Return
                        End If
                        .Model = _Model2.Model
                        .ModelCode = _Model2.ModelCode
                        .CarPrice = _Model2.CarPrice
                        _GroupCodeID = _Model2.GroupCodeID
                    End If



                    'If _A54.Equals("ใช้เป็นรถส่วนบุคคล") Then .UseOfCar = "Personal"
                    .UseOfCar = "Personal"
                    'If _B67.Equals("TRUE") Then .UseOfCar = "Commercial"

                    'If _B75.Equals("TRUE") Then .UseOfCar = "Personal"
                    'If _B76.Equals("TRUE") Then .UseOfCar = "Commercial"

                    'Dim _TypeOfInsure_Data = (From c In dc.TypeOfInsurrances Where c.typInsID.Equals(_typInsID)).FirstOrDefault()
                    'Dim _Suminsure = (From c In dc.Premiums Where c.InsuerID.Equals(.Insurer) _
                    '               And c.GroupCodeID.Equals(_GroupCodeID) _
                    '               And c.projID.Equals(_TypeOfInsure_Data.projID) _
                    '               And c.SchemeGroup.Equals(_TypeOfInsure_Data.Code.Trim()) _
                    '               And c.CarRegis.Equals(.UseOfCar)).FirstOrDefault()

                    'If _Suminsure Is Nothing Then
                    '    Alert.Show(String.Format("รายการของ {0} ไม่มีทุนประกันในระบบ", _B15))
                    '    Return
                    'End If
                    '.SumInsured = _Suminsure.SumInsured
                    .SumInsured = _A20.Replace("'", "")


                    .YearOfCar = _A29.Replace("'", "")
                    .CC = _A32.Replace("'", "")
                    .Seat = _A61.Replace("'", "")
                    .Weight = "0"
                    .EngineNo = _A33.Replace("'", "")
                    .ChassisNo = _A34.Replace("'", "")
                    .Colour = _A30
                    .LicenseNo = "ป้ายแดง"
                    .RegisterdName = ""





                    Dim _Startdate = CDate(_A51).ToString("dd/MM/yyyy").Split("/")
                    If CInt(_Startdate(2)) > 2500 Then _Startdate(2) = (CInt(_Startdate(2)) - 543).ToString()
                    .StartingDate = String.Format("{0}/{1}/{2}", _Startdate(0), _Startdate(1), _Startdate(2))
                    ''.StartingDate = _B64
                    '.StartingDate = CDate(_A52).ToString("dd/MM/yyyy")


                    Dim _Showroom = _A70.Split(":")
                    If _Showroom.Count <> 2 Then
                        Alert.Show(String.Format("รายการของ {0} showroom ไม่ถูกต้อง : {1}", _A12, _A70))
                        Return
                    End If
                    .Showroom = _Showroom(0).Trim()
                    .ShowroomCode = _Showroom(1).Trim()
                    '.ShowroomContactName = _B31
                    '.ShowroomContactPhone = _B32

                    .CMIStatus = "0"
                    .Leasing = "4"
                    .Beneficiary = "4"
                    .Agent = "316"
                    .CoverageType = "ประเภท 1"
                    .BillingName1 = ""
                    .BillingAddress1 = "999/1 เดอะไนน์ทาวเวอร์ อาคาร A ชั้น 4 ถนนพระราม9 แขวงสวนหลวง เขตสวนหลวง กรุงเทพฯ 10250"
                    .BillingTotals1 = ""
                    .BillingName2 = ""
                    .BillingAddress2 = ""
                    .BillingTotals2 = ""
                    .TempID = ""
                    .BatchNo = ""
                    .IDCard = _A13.Replace("'", "")

                    '_B89


                End With
                _ImportMotorAppForm316.Add(_ImportMotorAppForm316_Data)
            Next
            'End While
        End Using

        If _ImportMotorAppForm316.Count > 0 Then
            Dim idx As Integer = 0

            Session("View") = _ImportMotorAppForm316

            WindowImportApplicationForm.Hidden = True
            BindData()

        Else
            Alert.Show("No Data", MessageBoxIcon.Warning)
        End If


        'End Using


    End Sub


    'Private Sub ImportAppForm()
    '    '============ Master Table =================
    '    Dim _Portal_Table As New List(Of Portal_Table)
    '    Using dc As New DataClasses_PortalDataContext(webconfig._PortalConn)
    '        _Portal_Table = (From c In dc.Portal_Tables Where c.TABLE_NUMBER.Equals("T0002")).ToList()
    '    End Using

    '    'Dim _NLTDB_InsurerUnique As New List(Of InsurerUnique)
    '    Dim _NLTDB_Insurer As New List(Of Insurer)
    '    Using dc As New DataClasses_NLTDBDataContext(webconfig._NLTDBConn)
    '        '_NLTDB_InsurerUnique = (From c In dc.InsurerUniques).ToList()
    '        _NLTDB_Insurer = (From c In dc.Insurers Where c.InsurerPrefix.EndsWith("(TB)")).ToList()
    '    End Using
    '    '===========================================

    '    Dim i As Integer = 0
    '    Dim reader = New StringReader(txtData.Text)
    '    Dim line As String
    '    line = reader.ReadLine()
    '    Dim sb_error As New StringBuilder()
    '    Dim _ImportMotorAppForm316 As New List(Of Context.ImportMotorAppForm316)

    '    While line IsNot Nothing
    '        i += 1
    '        Dim _row() As String = line.Split(vbTab)

    '        If _row.Count = 88 Then
    '            Dim _B1 As String = _row(0)
    '            Dim _B2 As String = _row(1)
    '            Dim _B3 As String = _row(2)
    '            Dim _B4 As String = _row(3)
    '            Dim _B5 As String = _row(4)
    '            Dim _B6 As String = _row(5)
    '            Dim _B7 As String = _row(6)
    '            Dim _B8 As String = _row(7)
    '            Dim _B9 As String = _row(8)
    '            Dim _B10 As String = _row(9)
    '            Dim _B11 As String = _row(10)
    '            Dim _B12 As String = _row(11)
    '            Dim _B13 As String = _row(12)
    '            Dim _B14 As String = _row(13)
    '            Dim _B15 As String = _row(14)
    '            Dim _B16 As String = _row(15)
    '            Dim _B17 As String = _row(16)
    '            Dim _B18 As String = _row(17)
    '            Dim _B19 As String = _row(18)
    '            Dim _B20 As String = _row(19)
    '            Dim _B21 As String = _row(20)
    '            Dim _B22 As String = _row(21)
    '            Dim _B23 As String = _row(22)
    '            Dim _B24 As String = _row(23)
    '            Dim _B25 As String = _row(24)
    '            Dim _B26 As String = _row(25)
    '            Dim _B27 As String = _row(26)
    '            Dim _B28 As String = _row(27)
    '            Dim _B29 As String = _row(28)
    '            Dim _B30 As String = _row(29)
    '            Dim _B31 As String = _row(30)
    '            Dim _B32 As String = _row(31)
    '            Dim _B33 As String = _row(32)
    '            Dim _B34 As String = _row(33)
    '            Dim _B35 As String = _row(34)
    '            Dim _B36 As String = _row(35)
    '            Dim _B37 As String = _row(36)
    '            Dim _B38 As String = _row(37)
    '            Dim _B39 As String = _row(38)
    '            Dim _B40 As String = _row(39)
    '            Dim _B41 As String = _row(40)
    '            Dim _B42 As String = _row(41)
    '            Dim _B43 As String = _row(42)
    '            Dim _B44 As String = _row(43)
    '            Dim _B45 As String = _row(44)
    '            Dim _B46 As String = _row(45)
    '            Dim _B47 As String = _row(46)
    '            Dim _B48 As String = _row(47)
    '            Dim _B49 As String = _row(48)
    '            Dim _B50 As String = _row(49)
    '            Dim _B51 As String = _row(50)
    '            Dim _B52 As String = _row(51)
    '            Dim _B53 As String = _row(52)
    '            Dim _B54 As String = _row(53)
    '            Dim _B55 As String = _row(54)
    '            Dim _B56 As String = _row(55)
    '            Dim _B57 As String = _row(56)
    '            Dim _B58 As String = _row(57)
    '            Dim _B59 As String = _row(58)
    '            Dim _B60 As String = _row(59)
    '            Dim _B61 As String = _row(60)
    '            Dim _B62 As String = _row(61)
    '            Dim _B63 As String = _row(62)
    '            Dim _B64 As String = _row(63)
    '            Dim _B65 As String = _row(64)
    '            Dim _B66 As String = _row(65)
    '            Dim _B67 As String = _row(66)
    '            Dim _B68 As String = _row(67)
    '            Dim _B69 As String = _row(68)
    '            Dim _B70 As String = _row(69)
    '            Dim _B71 As String = _row(70)
    '            Dim _B72 As String = _row(71)
    '            Dim _B73 As String = _row(72)
    '            Dim _B74 As String = _row(73)
    '            Dim _B75 As String = _row(74)
    '            Dim _B76 As String = _row(75)
    '            Dim _B77 As String = _row(76)
    '            Dim _B78 As String = _row(77)
    '            Dim _B79 As String = _row(78)
    '            Dim _B80 As String = _row(79)
    '            Dim _B81 As String = _row(80)
    '            Dim _B82 As String = _row(81)
    '            Dim _B83 As String = _row(82)

    '            Dim _B84 As String = _row(83)
    '            Dim _B85 As String = _row(84)
    '            Dim _B86 As String = _row(85)
    '            Dim _B87 As String = _row(86)
    '            Dim _B88 As String = _row(87)




    '            Dim _ImportMotorAppForm316_Data As New Context.ImportMotorAppForm316()
    '            With _ImportMotorAppForm316_Data
    '                .ReferenceNo = _B1

    '                .Title = ""
    '                Dim _Title_TH = (From c In _Portal_Table Where c.ITEM_DESC_TH IsNot Nothing And _B15.StartsWith(c.ITEM_DESC_TH) And c.ITEM_DESC_TH IsNot "นาง" Select c.ITEM_DESC_TH).FirstOrDefault()
    '                'Dim _Title_EN = (From c In _Portal_Table Where c.ITEM_DESC_EN IsNot Nothing And _B15.StartsWith(c.ITEM_DESC_EN) Select c.ITEM_DESC_EN).Single()
    '                If _Title_TH IsNot Nothing Then
    '                    .Title = _Title_TH
    '                Else

    '                    If _B15.ToString().StartsWith("นางสาว") Then
    '                        .Title = "นางสาว"
    '                    Else
    '                        If _B15.ToString().StartsWith("นาง") Then
    '                            .Title = "นาง"
    '                        End If
    '                    End If
    '                    If _B15.ToString().StartsWith("พันโท") Then .Title = "พันโท"
    '                    If _B15.ToString().StartsWith("บจก.") Then .Title = "บจก."
    '                    If _B15.ToString().StartsWith("MR.") Then .Title = "MR."
    '                    If _B15.ToString().StartsWith("Mr.") Then .Title = "Mr."

    '                End If
    '                If Not String.IsNullOrEmpty(.Title) Then
    '                    _B15 = _B15.Replace(.Title, "")
    '                    'Else
    '                    '    '.Title = _B15
    '                    '    '.Name = _B15
    '                    '    '.SurName = _B15
    '                End If
    '                Dim _Name = _B15.Split(" ")
    '                .Name = _Name(0)
    '                .SurName = _Name(1)


    '                .LoanContractNo = "0"
    '                .Address1 = "444 อาคารเอ็มบีเค ทาวเวอร์ ถนนพญาไท แขวงวังใหม่ เขตปทุมวัน กรุงเทพมหานคร 10330"
    '                .Address2 = "999/1 เดอะไนน์ทาวเวอร์ อาคาร A ชั้น 4 ถนนพระราม9 แขวงสวนหลวง เขตสวนหลวง กรุงเทพฯ 10250"
    '                .Mobile = "0"
    '                .OfficePhone = "02-3089600"
    '                .HomePhone = "0"

    '                Dim _ins = (From c In _NLTDB_Insurer Where c.InsurerNameThai.IndexOf(_B8) > -1).FirstOrDefault()
    '                If _ins IsNot Nothing Then
    '                    'Dim _indID = (From c In _NLTDB_Insurer Where c.InsurerCode.Equals(_ins.InsurerCode) Order By c.InsurerNameThai).FirstOrDefault()
    '                    .Insurer = _ins.InsurerID
    '                End If

    '                .TypeInsurance = ddlTypeOfInsure.SelectedValue

    '                .CarPrice = _B50
    '                .Brand = "Nissan"

    '                Using dc As New DataClasses_NLTDBDataContext(webconfig._NLTDBConn)
    '                    'CarPrice and ModelCode
    '                    Dim _Model1 = (From c In dc.V_Models Where _B83.IndexOf(c.ModelCode) > -1 And c.ModelCode <> "-").FirstOrDefault()
    '                    If _Model1 IsNot Nothing Then
    '                        .Model = _Model1.Model
    '                        .ModelCode = _Model1.ModelCode
    '                    Else 'CarPrice
    '                        Dim _Model2 = (From c In dc.V_Models Where c.CarPrice.Equals(_B50) And c.ModelCode <> "-").FirstOrDefault()
    '                        .Model = _Model2.Model
    '                        .ModelCode = _Model2.ModelCode
    '                    End If
    '                End Using

    '                .YearOfCar = _B35
    '                .CC = _B38
    '                .Seat = _B84
    '                .Weight = "0"
    '                .EngineNo = _B39.Replace("'", "")
    '                .ChassisNo = _B40.Replace("'", "")
    '                .Colour = _B36
    '                .LicenseNo = "ป้ายแดง" '_B37
    '                .RegisterdName = ""

    '                If _B66.Equals("TRUE") Then .UseOfCar = "Personal"
    '                If _B67.Equals("TRUE") Then .UseOfCar = "Commercial"

    '                .SumInsured = _B26

    '                Dim _Startdate = _B64.Split("/")
    '                .StartingDate = String.Format("{0}/{1}/{2}", _Startdate(1), _Startdate(0), _Startdate(2))

    '                Dim _Showroom = _B51.Split(":")

    '                .Showroom = _B51.Trim()
    '                .ShowroomCode = _Showroom(1).Trim()
    '                '.ShowroomContactName = _B31
    '                '.ShowroomContactPhone = _B32

    '                .CMIStatus = "0"
    '                .Leasing = "4"
    '                .Beneficiary = "4"
    '                .Agent = "316"
    '                .CoverageType = "ประเภท 1"
    '                .BillingName1 = ""
    '                .BillingAddress1 = "999/1 เดอะไนน์ทาวเวอร์ อาคาร A ชั้น 4 ถนนพระราม9 แขวงสวนหลวง เขตสวนหลวง กรุงเทพฯ 10250"
    '                .BillingTotals1 = ""
    '                .BillingName2 = ""
    '                .BillingAddress2 = ""
    '                .BillingTotals2 = ""
    '                .TempID = ""
    '                .BatchNo = ""
    '                .IDCard = _B16 '.Replace("'", "")
    '            End With
    '            _ImportMotorAppForm316.Add(_ImportMotorAppForm316_Data)

    '        Else
    '            sb_error.AppendFormat("Row {0} invalid format data : <br> {1} <br>", i.ToString(), line)
    '        End If


    '        line = reader.ReadLine()
    '    End While


    '    If Not String.IsNullOrEmpty(sb_error.ToString()) Then
    '        Alert.Show(sb_error.ToString(), MessageBoxIcon.Error)
    '    Else
    '        If _ImportMotorAppForm316.Count > 0 Then
    '            Dim idx As Integer = 0

    '            Session("View") = _ImportMotorAppForm316

    '            WindowImportApplicationForm.Hidden = True
    '            BindData()

    '        Else
    '            Alert.Show("No Data", MessageBoxIcon.Warning)
    '        End If

    '    End If
    'End Sub


    'Private Sub BindTypeOfInsure()

    '    Dim myList As New List(Of Context.JQueryFeature)()

    '    Using dc As New DataClasses_NLTDBDataContext(webconfig._NLTDBConn)

    '        myList.Add(New Context.JQueryFeature("" _
    '                                                     , "" _
    '                                                     , 0 _
    '                                                     , False _
    '                                                     ))


    '        Dim _Project = (From c In dc.Projects).ToList()

    '        For Each item_Project In _Project

    '            Dim _TOI = (From c In dc.TypeOfInsurrances Where c.projID.Equals(item_Project.projID)).ToList()
    '            If _TOI.Count > 0 Then
    '                myList.Add(New Context.JQueryFeature("" _
    '                                                         , item_Project.projCode _
    '                                                         , 1 _
    '                                                         , False _
    '                                                         ))
    '                For Each item_TOI In _TOI
    '                    myList.Add(New Context.JQueryFeature(item_TOI.typInsID _
    '                                                   , String.Format("{0} ({1})", item_TOI.Name, item_TOI.typInsID) _
    '                                                   , 2 _
    '                                                   , True _
    '                                                   ))
    '                Next
    '            End If
    '        Next


    '    End Using


    '    ddlTypeOfInsure.DataTextField = "Name"
    '    ddlTypeOfInsure.DataValueField = "Id"
    '    ddlTypeOfInsure.DataSimulateTreeLevelField = "Level"
    '    ddlTypeOfInsure.DataEnableSelectField = "EnableSelect"
    '    ddlTypeOfInsure.DataSource = myList
    '    ddlTypeOfInsure.DataBind()

    'End Sub


    Protected Sub btnExcelFormat_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcelFormat.Click
        Dim fileName = String.Format("{0}.xls", System.Guid.NewGuid.ToString())
        Dim filePath = String.Format("{0}\{1}", Server.MapPath("~/Template"), "Template for Thanachart.xls")



        Response.ClearContent()
        Response.AddHeader("content-disposition", "attachment; filename=" & fileName)
        Response.ContentType = "application/octet-stream"
        Response.WriteFile(filePath)
        Response.End()

    End Sub
End Class
