Imports Microsoft.VisualBasic
Imports Portal.Components
Imports Microsoft.VisualBasic.FileIO
Imports System.Runtime.Caching
Imports System.Data
Imports System.IO
Imports Excel
Imports Context
Imports MotorClaimWebService 
Imports ElencySolutions.CsvHelper

Partial Public Class DataClasses_MotorClaimDataExt
    Inherits DataClasses_MotorClaimDataContext
    Private Shared ReadOnly Property ConnectionString() As String
        Get
            Dim Conn As String = ConfigurationManager.ConnectionStrings("MotorClaimConnectionString").ConnectionString

            Return Conn
        End Get
    End Property
    Public Sub New()
        MyBase.New(ConnectionString)
    End Sub

    Public Function MotorClaimUploadResult(ByVal MCGUID As String, ByVal MCTYPE As String) As List(Of ClaimUpload_DataObject)
        Dim _Result = New List(Of ClaimUpload_DataObject)

        Select Case MCTYPE
            Case "xlsx"
                _Result = MotorClaimUploadResult_xlsx(MCGUID)
            Case "csv"
                _Result = MotorClaimUploadResult_csv(MCGUID)
        End Select

        Return _Result

    End Function


    Private Function MotorClaimUploadResult_xlsx(ByVal MCGUID As String) As List(Of ClaimUpload_DataObject)
        Dim _Result = New List(Of ClaimUpload_DataObject)

        If MCGUID Is Nothing Then
            Return _Result
        End If

        Dim MyCache As New FileCache(AppDomain.CurrentDomain.BaseDirectory & "\UploadFiles\", False, Nothing)

        If MyCache(MCGUID) Is Nothing Then

            Dim FilePath = AppDomain.CurrentDomain.BaseDirectory & "\UploadFiles\"
            Dim FileName = String.Format(FilePath & "/{0}.xlsx", MCGUID)

            Dim _data As New List(Of ClaimTransaction_DataObject)

            Dim _RunNo As Integer = 0

            Using ws As New MotorClaimWebService
                Using File = New FileStream(FileName, FileMode.Open, FileAccess.Read)

                    ''1. Reading from a binary Excel file ('97-2003 format; *.xls)
                    'Dim excelReader As IExcelDataReader = ExcelReaderFactory.CreateBinaryReader(Stream)
                    '...
                    '2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
                    Dim excelReader As IExcelDataReader = ExcelReaderFactory.CreateOpenXmlReader(File)
                    '...
                    '3. DataSet - The result of each spreadsheet will be created in the result.Tables
                    'Dim result As DataSet = excelReader.AsDataSet()
                    '...
                    '4. DataSet - Create column names from first row
                    excelReader.IsFirstRowAsColumnNames = True
                    Dim result As DataSet = excelReader.AsDataSet()

                    For Each irow As DataRow In result.Tables(0).Rows
                        Dim _ClaimStatus = irow(0)
                        Dim _TempPolicy = irow(1)
                        Dim _RefNo = irow(2)
                        Dim _Version = irow(3)
                        Dim _PolicyNo = irow(4)
                        Dim _PolicyYear = irow(5)
                        Dim _ClaimNo = irow(6)
                        Dim _TransactionDate = irow(7)
                        Dim _Unwriter = irow(8)
                        Dim _InsuredName = irow(9)
                        Dim _EffectiveDate = irow(10)
                        Dim _ExpiryDate = irow(11)
                        Dim _Beneficiary = irow(12)
                        Dim _CarBrand = irow(13)
                        Dim _CarModel = irow(14)
                        Dim _CarLicense = irow(15)
                        Dim _CarYear = irow(16)
                        Dim _ChassisNo = irow(17)
                        Dim _ShowRoomName = irow(18)
                        Dim _ShowRoomCode = irow(19)
                        Dim _ClaimNoticeDate = irow(20)
                        Dim _ClaimNoticeTime = irow(21)
                        Dim _ClaimDetails = irow(22)
                        Dim _ClaimType = irow(23)
                        Dim _ClaimResult = irow(24)
                        Dim _ClaimDamageDetails = irow(25)
                        Dim _CallCenter = irow(26)
                        Dim _AccidentDate = irow(27)
                        Dim _AccidentTime = irow(28)
                        Dim _AccidentPlace = irow(29)
                        Dim _AccidentTumbon = irow(30)
                        Dim _AccidentAmphur = irow(31)
                        Dim _AccidentProvince = irow(32)
                        Dim _AccidentZipcode = irow(33)
                        Dim _DriverName = irow(34)
                        Dim _DriverTel = irow(35)
                        Dim _NoticeName = irow(36)
                        Dim _NoticeTel = irow(37)
                        Dim _GarageType = irow(38)
                        Dim _GarageCode = irow(39)
                        Dim _GarageName = irow(40)
                        Dim _GaragePlace = irow(41)
                        Dim _GarageTumbon = irow(42)
                        Dim _GarageAmphur = irow(43)
                        Dim _GarageProvince = irow(44)
                        Dim _GarageZipcode = irow(45)
                        Dim _CarRepairDate = irow(46)
                        Dim _CarReceiveDate = irow(47)
                        Dim _ConsentFormNo = irow(48)
                        Dim _PartsDealerName = irow(49)
                        Dim _PaymentDetails = irow(50)
                        Dim _Amount1 = irow(51)
                        Dim _Amount2 = irow(52)
                        Dim _Amount3 = irow(53)
                        Dim _Remark = irow(54)

                        '======================= Validate ========================================
                        Dim _tblClaimData As New tblClaimTransaction_Data()
                        With _tblClaimData
                            .TRNo = _RefNo.ToString()

                            .ClaimStatus = ws.ValidateData(_ClaimStatus, objType._String)
                            .TempPolicy = ws.ValidateData(_TempPolicy, objType._String)
                            .RefNo = ws.ValidateData(_RefNo, objType._String)
                            .Version = ws.ValidateData(_Version, objType._Integer) 'int
                            .PolicyNo = ws.ValidateData(_PolicyNo, objType._String)
                            .PolicyYear = ws.ValidateData(_PolicyYear, objType._Integer) 'int
                            .ClaimNo = ws.ValidateData(_ClaimNo, objType._String)
                            .TransactionDate = ws.ValidateData(_TransactionDate, objType._String)
                            .Unwriter = ws.ValidateData(_Unwriter, objType._String)
                            .InsuredName = ws.ValidateData(_InsuredName, objType._String)
                            .EffectiveDate = ws.ValidateData(_EffectiveDate, objType._String)
                            .ExpiryDate = ws.ValidateData(_ExpiryDate, objType._String)
                            .Beneficiary = ws.ValidateData(_Beneficiary, objType._String)
                            .CarBrand = ws.ValidateData(_CarBrand, objType._String)
                            .CarModel = ws.ValidateData(_CarModel, objType._String)
                            .CarLicense = ws.ValidateData(_CarLicense, objType._String)
                            .CarYear = ws.ValidateData(_CarYear, objType._String)
                            .ChassisNo = ws.ValidateData(_ChassisNo, objType._String)
                            .ShowRoomName = ws.ValidateData(_ShowRoomName, objType._String)
                            .ShowRoomCode = ws.ValidateData(_ShowRoomCode, objType._String)
                            .ClaimNoticeDate = ws.ValidateData(_ClaimNoticeDate, objType._String)
                            .ClaimNoticeTime = ws.ValidateData(_ClaimNoticeTime, objType._String)
                            .ClaimDetails = ws.ValidateData(_ClaimDetails, objType._String)
                            .ClaimType = ws.ValidateData(_ClaimType, objType._Integer) 'int
                            .ClaimResult = ws.ValidateData(_ClaimResult, objType._Integer) 'int
                            .ClaimDamageDetails = ws.ValidateData(_ClaimDamageDetails, objType._String)
                            .CallCenter = ws.ValidateData(_CallCenter, objType._String)
                            .AccidentDate = ws.ValidateData(_AccidentDate, objType._String)
                            .AccidentTime = ws.ValidateData(_AccidentTime, objType._String)
                            .AccidentPlace = ws.ValidateData(_AccidentPlace, objType._String)
                            .AccidentTumbon = ws.ValidateData(_AccidentTumbon, objType._String)
                            .AccidentAmphur = ws.ValidateData(_AccidentAmphur, objType._String)
                            .AccidentProvince = ws.ValidateData(_AccidentProvince, objType._String)
                            .AccidentZipcode = ws.ValidateData(_AccidentZipcode, objType._String)
                            .DriverName = ws.ValidateData(_DriverName, objType._String)
                            .DriverTel = ws.ValidateData(_DriverTel, objType._String)
                            .NoticeName = ws.ValidateData(_NoticeName, objType._String)
                            .NoticeTel = ws.ValidateData(_NoticeTel, objType._String)
                            .GarageType = ws.ValidateData(_GarageType, objType._Integer) 'int
                            .GarageCode = ws.ValidateData(_GarageCode, objType._String)
                            .GarageName = ws.ValidateData(_GarageName, objType._String)
                            .GaragePlace = ws.ValidateData(_GaragePlace, objType._String)
                            .GarageTumbon = ws.ValidateData(_GarageTumbon, objType._String)
                            .GarageAmphur = ws.ValidateData(_GarageAmphur, objType._String)
                            .GarageProvince = ws.ValidateData(_GarageProvince, objType._String)
                            .GarageZipcode = ws.ValidateData(_GarageZipcode, objType._String)
                            .CarRepairDate = ws.ValidateData(_CarRepairDate, objType._String)
                            .CarReceiveDate = ws.ValidateData(_CarReceiveDate, objType._String)
                            .ConsentFormNo = ws.ValidateData(_ConsentFormNo, objType._String)
                            .PartsDealerName = ws.ValidateData(_PartsDealerName, objType._String)
                            .PaymentDetails = ws.ValidateData(_PaymentDetails, objType._String)
                            .Amount1 = ws.ValidateData(_Amount1, objType._Double) 'float
                            .Amount2 = ws.ValidateData(_Amount2, objType._Double) 'float
                            .Amount3 = ws.ValidateData(_Amount3, objType._Double) 'float
                            .Remark = ws.ValidateData(_Remark, objType._String)

                        End With

                        Dim _item_result As New List(Of ClaimResultMessage)
                        Select Case _tblClaimData.ClaimStatus
                            Case "00"
                                _item_result = ws.ValidateClaimData00(_tblClaimData)
                            Case "01"
                                _item_result = ws.ValidateClaimData01(_tblClaimData)
                            Case "02"
                                _item_result = ws.ValidateClaimData02(_tblClaimData)
                            Case "99"
                                _item_result = ws.ValidateClaimData99(_tblClaimData)
                            Case "98"
                                _item_result = ws.ValidateClaimData98(_tblClaimData)
                        End Select


                        _RunNo += 1
                        '=========================== add new ========================
                        Dim _itemdata As New ClaimUpload_DataObject
                        With _itemdata
                            .RunNo = _RunNo.ToString()

                            .ClaimStatus = _ClaimStatus.ToString()
                            .TempPolicy = _TempPolicy.ToString()
                            .RefNo = _RefNo.ToString()
                            .Version = _Version.ToString()
                            .PolicyNo = _PolicyNo.ToString()
                            .PolicyYear = _PolicyYear.ToString()
                            .ClaimNo = _ClaimNo.ToString()
                            .TransactionDate = _TransactionDate.ToString()
                            .Unwriter = _Unwriter.ToString()
                            .InsuredName = _InsuredName.ToString()
                            .EffectiveDate = _EffectiveDate.ToString()
                            .ExpiryDate = _ExpiryDate.ToString()
                            .Beneficiary = _Beneficiary.ToString()
                            .CarBrand = _CarBrand.ToString()
                            .CarModel = _CarModel.ToString()
                            .CarLicense = _CarLicense.ToString()
                            .CarYear = _CarYear.ToString()
                            .ChassisNo = _ChassisNo.ToString()
                            .ShowRoomName = _ShowRoomName.ToString()
                            .ShowRoomCode = _ShowRoomCode.ToString()
                            .ClaimNoticeDate = _ClaimNoticeDate.ToString()
                            .ClaimNoticeTime = _ClaimNoticeTime.ToString()
                            .ClaimDetails = _ClaimDetails.ToString()
                            .ClaimType = _ClaimType.ToString()
                            .ClaimResult = _ClaimResult.ToString()
                            .ClaimDamageDetails = _ClaimDamageDetails.ToString()
                            .CallCenter = _CallCenter.ToString()
                            .AccidentDate = _AccidentDate.ToString()
                            .AccidentTime = _AccidentTime.ToString()
                            .AccidentPlace = _AccidentPlace.ToString()
                            .AccidentTumbon = _AccidentTumbon.ToString()
                            .AccidentAmphur = _AccidentAmphur.ToString()
                            .AccidentProvince = _AccidentProvince.ToString()
                            .AccidentZipcode = _AccidentZipcode.ToString()
                            .DriverName = _DriverName.ToString()
                            .DriverTel = _DriverTel.ToString()
                            .NoticeName = _NoticeName.ToString()
                            .NoticeTel = _NoticeTel.ToString()
                            .GarageType = _GarageType.ToString()
                            .GarageCode = _GarageCode.ToString()
                            .GarageName = _GarageName.ToString()
                            .GaragePlace = _GaragePlace.ToString()
                            .GarageTumbon = _GarageTumbon.ToString()
                            .GarageAmphur = _GarageAmphur.ToString()
                            .GarageProvince = _GarageProvince.ToString()
                            .GarageZipcode = _GarageZipcode.ToString()
                            .CarRepairDate = _CarRepairDate.ToString()
                            .CarReceiveDate = _CarReceiveDate.ToString()
                            .ConsentFormNo = _ConsentFormNo.ToString()
                            .PartsDealerName = _PartsDealerName.ToString()
                            .PaymentDetails = _PaymentDetails.ToString()
                            .Amount1 = _Amount1.ToString()
                            .Amount2 = _Amount2.ToString()
                            .Amount3 = _Amount3.ToString()
                            .Remark = _Remark.ToString()
                        End With


                        If _item_result.Count > 0 Then

                            _itemdata.Status = "Incomplete"

                            Dim sbRemark As New StringBuilder
                            For Each i_rusult In _item_result
                                sbRemark.AppendLine(String.Format("[#{0}#,{1}]|", i_rusult.ResultCode, i_rusult.ResultMessage))
                            Next
                            _itemdata.Message = sbRemark.ToString()
                        Else
                            _itemdata.Status = "Complete"
                        End If



                        _Result.Add(_itemdata)
                    Next

                End Using

            End Using

        Else
            _Result = MyCache(MCGUID)
        End If


        Return _Result

    End Function

    Private Function MotorClaimUploadResult_csv(ByVal MCGUID As String) As List(Of ClaimUpload_DataObject)
        Dim _Result = New List(Of ClaimUpload_DataObject)

        If MCGUID Is Nothing Then
            Return _Result
        End If

        Dim MyCache As New FileCache(AppDomain.CurrentDomain.BaseDirectory & "\UploadFiles\", False, Nothing)

        If MyCache(MCGUID) Is Nothing Then
            Dim FilePath = AppDomain.CurrentDomain.BaseDirectory & "\UploadFiles\"
            Dim FileName = String.Format(FilePath & "/{0}.csv", MCGUID)

            Dim _data As New List(Of ClaimTransaction_DataObject)

            Dim _RunNo As Integer = 0

            Using ws As New MotorClaimWebService
               

                Dim irow = New CsvReader(FileName, Encoding.Default)

                irow.ReadNextRecord()
               



                ' ''1. Reading from a binary Excel file ('97-2003 format; *.xls)
                ''Dim excelReader As IExcelDataReader = ExcelReaderFactory.CreateBinaryReader(Stream)
                ''...
                ''2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
                'Dim excelReader As IExcelDataReader = ExcelReaderFactory.CreateOpenXmlReader(File)
                ''...
                ''3. DataSet - The result of each spreadsheet will be created in the result.Tables
                ''Dim result As DataSet = excelReader.AsDataSet()
                ''...
                ''4. DataSet - Create column names from first row
                'excelReader.IsFirstRowAsColumnNames = True
                'Dim result As DataSet = excelReader.AsDataSet()

                While irow.ReadNextRecord()
                    Dim _ClaimStatus = irow.Fields(0)
                    Dim _TempPolicy = irow.Fields(1)
                    Dim _RefNo = irow.Fields(2)
                    Dim _Version = irow.Fields(3)
                    Dim _PolicyNo = irow.Fields(4)
                    Dim _PolicyYear = irow.Fields(5)
                    Dim _ClaimNo = irow.Fields(6)
                    Dim _TransactionDate = irow.Fields(7)
                    Dim _Unwriter = irow.Fields(8)
                    Dim _InsuredName = irow.Fields(9)
                    Dim _EffectiveDate = irow.Fields(10)
                    Dim _ExpiryDate = irow.Fields(11)
                    Dim _Beneficiary = irow.Fields(12)
                    Dim _CarBrand = irow.Fields(13)
                    Dim _CarModel = irow.Fields(14)
                    Dim _CarLicense = irow.Fields(15)
                    Dim _CarYear = irow.Fields(16)
                    Dim _ChassisNo = irow.Fields(17)
                    Dim _ShowRoomName = irow.Fields(18)
                    Dim _ShowRoomCode = irow.Fields(19)
                    Dim _ClaimNoticeDate = irow.Fields(20)
                    Dim _ClaimNoticeTime = irow.Fields(21)
                    Dim _ClaimDetails = irow.Fields(22)
                    Dim _ClaimType = irow.Fields(23)
                    Dim _ClaimResult = irow.Fields(24)
                    Dim _ClaimDamageDetails = irow.Fields(25)
                    Dim _CallCenter = irow.Fields(26)
                    Dim _AccidentDate = irow.Fields(27)
                    Dim _AccidentTime = irow.Fields(28)
                    Dim _AccidentPlace = irow.Fields(29)
                    Dim _AccidentTumbon = irow.Fields(30)
                    Dim _AccidentAmphur = irow.Fields(31)
                    Dim _AccidentProvince = irow.Fields(32)
                    Dim _AccidentZipcode = irow.Fields(33)
                    Dim _DriverName = irow.Fields(34)
                    Dim _DriverTel = irow.Fields(35)
                    Dim _NoticeName = irow.Fields(36)
                    Dim _NoticeTel = irow.Fields(37)
                    Dim _GarageType = irow.Fields(38)
                    Dim _GarageCode = irow.Fields(39)
                    Dim _GarageName = irow.Fields(40)
                    Dim _GaragePlace = irow.Fields(41)
                    Dim _GarageTumbon = irow.Fields(42)
                    Dim _GarageAmphur = irow.Fields(43)
                    Dim _GarageProvince = irow.Fields(44)
                    Dim _GarageZipcode = irow.Fields(45)
                    Dim _CarRepairDate = irow.Fields(46)
                    Dim _CarReceiveDate = irow.Fields(47)
                    Dim _ConsentFormNo = irow.Fields(48)
                    Dim _PartsDealerName = irow.Fields(49)
                    Dim _PaymentDetails = irow.Fields(50)
                    Dim _Amount1 = irow.Fields(51)
                    Dim _Amount2 = irow.Fields(52)
                    Dim _Amount3 = irow.Fields(53)
                    Dim _Remark = irow.Fields(54)

                    '======================= Validate ========================================
                    Dim _tblClaimData As New tblClaimTransaction_Data()
                    With _tblClaimData
                        .TRNo = _RefNo.ToString()

                        .ClaimStatus = ws.ValidateData(_ClaimStatus, objType._String)
                        .TempPolicy = ws.ValidateData(_TempPolicy, objType._String)
                        .RefNo = ws.ValidateData(_RefNo, objType._String)
                        .Version = ws.ValidateData(_Version, objType._Integer) 'int
                        .PolicyNo = ws.ValidateData(_PolicyNo, objType._String)
                        .PolicyYear = ws.ValidateData(_PolicyYear, objType._Integer) 'int
                        .ClaimNo = ws.ValidateData(_ClaimNo, objType._String)
                        .TransactionDate = ws.ValidateData(_TransactionDate, objType._String)
                        .Unwriter = ws.ValidateData(_Unwriter, objType._String)
                        .InsuredName = ws.ValidateData(_InsuredName, objType._String)
                        .EffectiveDate = ws.ValidateData(_EffectiveDate, objType._String)
                        .ExpiryDate = ws.ValidateData(_ExpiryDate, objType._String)
                        .Beneficiary = ws.ValidateData(_Beneficiary, objType._String)
                        .CarBrand = ws.ValidateData(_CarBrand, objType._String)
                        .CarModel = ws.ValidateData(_CarModel, objType._String)
                        .CarLicense = ws.ValidateData(_CarLicense, objType._String)
                        .CarYear = ws.ValidateData(_CarYear, objType._String)
                        .ChassisNo = ws.ValidateData(_ChassisNo, objType._String)
                        .ShowRoomName = ws.ValidateData(_ShowRoomName, objType._String)
                        .ShowRoomCode = ws.ValidateData(_ShowRoomCode, objType._String)
                        .ClaimNoticeDate = ws.ValidateData(_ClaimNoticeDate, objType._String)
                        .ClaimNoticeTime = ws.ValidateData(_ClaimNoticeTime, objType._String)
                        .ClaimDetails = ws.ValidateData(_ClaimDetails, objType._String)
                        .ClaimType = ws.ValidateData(_ClaimType, objType._Integer) 'int
                        .ClaimResult = ws.ValidateData(_ClaimResult, objType._Integer) 'int
                        .ClaimDamageDetails = ws.ValidateData(_ClaimDamageDetails, objType._String)
                        .CallCenter = ws.ValidateData(_CallCenter, objType._String)
                        .AccidentDate = ws.ValidateData(_AccidentDate, objType._String)
                        .AccidentTime = ws.ValidateData(_AccidentTime, objType._String)
                        .AccidentPlace = ws.ValidateData(_AccidentPlace, objType._String)
                        .AccidentTumbon = ws.ValidateData(_AccidentTumbon, objType._String)
                        .AccidentAmphur = ws.ValidateData(_AccidentAmphur, objType._String)
                        .AccidentProvince = ws.ValidateData(_AccidentProvince, objType._String)
                        .AccidentZipcode = ws.ValidateData(_AccidentZipcode, objType._String)
                        .DriverName = ws.ValidateData(_DriverName, objType._String)
                        .DriverTel = ws.ValidateData(_DriverTel, objType._String)
                        .NoticeName = ws.ValidateData(_NoticeName, objType._String)
                        .NoticeTel = ws.ValidateData(_NoticeTel, objType._String)
                        .GarageType = ws.ValidateData(_GarageType, objType._Integer) 'int
                        .GarageCode = ws.ValidateData(_GarageCode, objType._String)
                        .GarageName = ws.ValidateData(_GarageName, objType._String)
                        .GaragePlace = ws.ValidateData(_GaragePlace, objType._String)
                        .GarageTumbon = ws.ValidateData(_GarageTumbon, objType._String)
                        .GarageAmphur = ws.ValidateData(_GarageAmphur, objType._String)
                        .GarageProvince = ws.ValidateData(_GarageProvince, objType._String)
                        .GarageZipcode = ws.ValidateData(_GarageZipcode, objType._String)
                        .CarRepairDate = ws.ValidateData(_CarRepairDate, objType._String)
                        .CarReceiveDate = ws.ValidateData(_CarReceiveDate, objType._String)
                        .ConsentFormNo = ws.ValidateData(_ConsentFormNo, objType._String)
                        .PartsDealerName = ws.ValidateData(_PartsDealerName, objType._String)
                        .PaymentDetails = ws.ValidateData(_PaymentDetails, objType._String)
                        .Amount1 = ws.ValidateData(_Amount1, objType._Double) 'float
                        .Amount2 = ws.ValidateData(_Amount2, objType._Double) 'float
                        .Amount3 = ws.ValidateData(_Amount3, objType._Double) 'float
                        .Remark = ws.ValidateData(_Remark, objType._String)

                    End With

                    Dim _item_result As New List(Of ClaimResultMessage)
                    Select Case _tblClaimData.ClaimStatus
                        Case "00"
                            _item_result = ws.ValidateClaimData00(_tblClaimData)
                        Case "01"
                            _item_result = ws.ValidateClaimData01(_tblClaimData)
                        Case "02"
                            _item_result = ws.ValidateClaimData02(_tblClaimData)
                        Case "99"
                            _item_result = ws.ValidateClaimData99(_tblClaimData)
                        Case "98"
                            _item_result = ws.ValidateClaimData98(_tblClaimData)
                    End Select


                    _RunNo += 1
                    '=========================== add new ========================
                    Dim _itemdata As New ClaimUpload_DataObject
                    With _itemdata
                        .RunNo = _RunNo.ToString()

                        .ClaimStatus = _ClaimStatus.ToString()
                        .TempPolicy = _TempPolicy.ToString()
                        .RefNo = _RefNo.ToString()
                        .Version = _Version.ToString()
                        .PolicyNo = _PolicyNo.ToString()
                        .PolicyYear = _PolicyYear.ToString()
                        .ClaimNo = _ClaimNo.ToString()
                        .TransactionDate = _TransactionDate.ToString()
                        .Unwriter = _Unwriter.ToString()
                        .InsuredName = _InsuredName.ToString()
                        .EffectiveDate = _EffectiveDate.ToString()
                        .ExpiryDate = _ExpiryDate.ToString()
                        .Beneficiary = _Beneficiary.ToString()
                        .CarBrand = _CarBrand.ToString()
                        .CarModel = _CarModel.ToString()
                        .CarLicense = _CarLicense.ToString()
                        .CarYear = _CarYear.ToString()
                        .ChassisNo = _ChassisNo.ToString()
                        .ShowRoomName = _ShowRoomName.ToString()
                        .ShowRoomCode = _ShowRoomCode.ToString()
                        .ClaimNoticeDate = _ClaimNoticeDate.ToString()
                        .ClaimNoticeTime = _ClaimNoticeTime.ToString()
                        .ClaimDetails = _ClaimDetails.ToString()
                        .ClaimType = _ClaimType.ToString()
                        .ClaimResult = _ClaimResult.ToString()
                        .ClaimDamageDetails = _ClaimDamageDetails.ToString()
                        .CallCenter = _CallCenter.ToString()
                        .AccidentDate = _AccidentDate.ToString()
                        .AccidentTime = _AccidentTime.ToString()
                        .AccidentPlace = _AccidentPlace.ToString()
                        .AccidentTumbon = _AccidentTumbon.ToString()
                        .AccidentAmphur = _AccidentAmphur.ToString()
                        .AccidentProvince = _AccidentProvince.ToString()
                        .AccidentZipcode = _AccidentZipcode.ToString()
                        .DriverName = _DriverName.ToString()
                        .DriverTel = _DriverTel.ToString()
                        .NoticeName = _NoticeName.ToString()
                        .NoticeTel = _NoticeTel.ToString()
                        .GarageType = _GarageType.ToString()
                        .GarageCode = _GarageCode.ToString()
                        .GarageName = _GarageName.ToString()
                        .GaragePlace = _GaragePlace.ToString()
                        .GarageTumbon = _GarageTumbon.ToString()
                        .GarageAmphur = _GarageAmphur.ToString()
                        .GarageProvince = _GarageProvince.ToString()
                        .GarageZipcode = _GarageZipcode.ToString()
                        .CarRepairDate = _CarRepairDate.ToString()
                        .CarReceiveDate = _CarReceiveDate.ToString()
                        .ConsentFormNo = _ConsentFormNo.ToString()
                        .PartsDealerName = _PartsDealerName.ToString()
                        .PaymentDetails = _PaymentDetails.ToString()
                        .Amount1 = _Amount1.ToString()
                        .Amount2 = _Amount2.ToString()
                        .Amount3 = _Amount3.ToString()
                        .Remark = _Remark.ToString()
                    End With


                    If _item_result.Count > 0 Then

                        _itemdata.Status = "Incomplete"

                        Dim sbRemark As New StringBuilder
                        For Each i_rusult In _item_result
                            sbRemark.AppendLine(String.Format("[#{0}#,{1}]|", i_rusult.ResultCode, i_rusult.ResultMessage))
                        Next
                        _itemdata.Message = sbRemark.ToString()
                    Else
                        _itemdata.Status = "Complete"
                    End If



                    _Result.Add(_itemdata)
                End While

            End Using



        Else
            _Result = MyCache(MCGUID)
        End If


        Return _Result

    End Function


End Class


