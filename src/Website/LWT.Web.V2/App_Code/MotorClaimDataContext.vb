Imports Microsoft.VisualBasic
Imports Portal.Components
Imports Microsoft.VisualBasic.FileIO
Imports System.Runtime.Caching
Imports System.Data
Imports System.IO
Imports Excel
'Imports Context
Imports MotorClaimWebService
Imports ElencySolutions.CsvHelper


Namespace MotorClaim
    Public Class DataClasses_MotorClaimDataExt
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

            'MotorClaimUploadResult_Test(MCGUID)

            Select Case MCTYPE
                Case "xlsx"
                    _Result = MotorClaimUploadResult_xlsx(MCGUID)
                Case "csv"
                    _Result = MotorClaimUploadResult_csv(MCGUID)
            End Select

            Return _Result

        End Function

        Private Function MotorClaimUploadResult_Test(ByVal MCGUID As String) As List(Of ClaimUpload_DataObject)
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


            Dim THAWS As New THAWebservices.MotorClaimWebService()

            Dim THA_data As New List(Of THAWebservices.ClaimTransaction_Data)

            For Each item In _Result

                Dim iData As New THAWebservices.ClaimTransaction_Data

                With iData


                    .ClaimStatus = item.ClaimStatus
                    .TempPolicy = item.TempPolicy
                    .RefNo = item.RefNo
                    .Version = item.Version
                    .PolicyNo = item.PolicyNo
                    .PolicyYear = item.PolicyYear
                    .ClaimNo = item.ClaimNo
                    .TransactionDate = item.TransactionDate
                    .Unwriter = "U00082" 'item.Unwriter
                    .InsuredName = item.InsuredName
                    .EffectiveDate = item.EffectiveDate
                    .ExpiryDate = item.ExpiryDate
                    .Beneficiary = item.Beneficiary
                    .CarBrand = item.CarBrand
                    .CarModel = item.CarModel
                    .CarLicense = item.CarLicense
                    .CarYear = item.CarYear
                    .ChassisNo = item.ChassisNo
                    .ShowRoomName = item.ShowRoomName
                    .ShowRoomCode = item.ShowRoomCode
                    .ClaimNoticeDate = item.ClaimNoticeDate
                    .ClaimNoticeTime = item.ClaimNoticeTime
                    .ClaimDetails = item.ClaimDetails
                    .ClaimType = item.ClaimType
                    .ClaimResult = item.ClaimResult
                    .ClaimDamageDetails = item.ClaimDamageDetails
                    .CallCenter = item.CallCenter
                    .AccidentDate = item.AccidentDate
                    .AccidentTime = item.AccidentTime
                    .AccidentPlace = item.AccidentPlace
                    .AccidentTumbon = item.AccidentTumbon
                    .AccidentAmphur = item.AccidentAmphur
                    .AccidentProvince = item.AccidentProvince
                    .AccidentZipcode = item.AccidentZipcode
                    .DriverName = item.DriverName
                    .DriverTel = item.DriverTel
                    .NoticeName = item.NoticeName
                    .NoticeTel = item.NoticeTel
                    .GarageType = item.GarageType
                    .GarageCode = item.GarageCode
                    .GarageName = item.GarageName
                    .GaragePlace = item.GaragePlace
                    .GarageTumbon = item.GarageTumbon
                    .GarageAmphur = item.GarageAmphur
                    .GarageProvince = item.GarageProvince
                    .GarageZipcode = item.GarageZipcode
                    .CarRepairDate = item.CarRepairDate
                    .CarReceiveDate = item.CarReceiveDate
                    .ConsentFormNo = item.ConsentFormNo
                    .PartsDealerName = item.PartsDealerName
                    .PaymentDetails = item.PaymentDetails
                    .Amount1 = item.Amount1
                    .Amount2 = item.Amount2
                    .Amount3 = item.Amount3
                    .Remark = item.Remark


                End With
                THA_data.Add(iData)
            Next

            Dim a = THAWS.SendMotorClaim("", "", THA_data.ToArray())




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


                    Dim irow = New ElencySolutions.CsvHelper.CsvReader(FileName, Encoding.Default)

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









        Public Function MotorClaimUploadResult_VIRI(ByVal MCGUID As String) As List(Of ClaimUpload_DataObject)
            Dim _Result = New List(Of ClaimUpload_DataObject)

            _Result = MotorClaimUploadResult_VIRI_xlsx(MCGUID)

            'Select Case MCTYPE
            '    Case "xlsx"
            '        _Result = MotorClaimUploadResult_xlsx(MCGUID)
            '    Case "csv"
            '        _Result = MotorClaimUploadResult_csv(MCGUID)
            'End Select

            Return _Result

        End Function

        Private Function MotorClaimUploadResult_VIRI_xlsx(ByVal MCGUID As String) As List(Of ClaimUpload_DataObject)
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
                            If Not irow(10).GetType().ToString().ToLower().Equals("system.dbnull") Then







                                Dim _ClaimStatus = "00"
                                Dim _TempPolicy = "-"
                                Dim _RefNo = ws.ValidateData(irow(10), objType._String).ToString().Trim()
                                Dim _Version = 0
                                Dim _PolicyNo = ws.ValidateData(irow(3), objType._String).ToString().Trim()
                                Dim _PolicyYear = 0
                                If ws.ValidateData(irow(4), objType._String).ToString.Trim().Equals("ต่ออายุ") Then
                                    _PolicyYear = 1
                                End If
                                Dim _ClaimNo = ws.ValidateData(irow(10), objType._String).ToString().Trim()
                                Dim _TransactionDate = CDate(irow(1)).ToString("yyyy-MM-dd")
                                Dim _Unwriter = "U00121"
                                Dim _InsuredName = ws.ValidateData(irow(2), objType._String).ToString().Trim()
                                Dim _EffectiveDate = CDate(irow(7)).ToString("yyyy-MM-dd")
                                Dim _ExpiryDate = CDate(irow(8)).ToString("yyyy-MM-dd")
                                Dim _Beneficiary = ws.ValidateData(irow(9), objType._String).ToString().Trim()
                                Dim _CarBrand = "NISSAN"
                                Dim _CarModel = ws.ValidateData(irow(11), objType._String).ToString().Trim()
                                Dim _CarLicense = ws.ValidateData(irow(12), objType._String).ToString().Trim()
                                Dim _CarYear = "-"
                                Dim _ChassisNo = ws.ValidateData(irow(13), objType._String).ToString().Trim()
                                Dim _ShowRoomName = ws.ValidateData(irow(5), objType._String).ToString().Trim()
                                Dim _ShowRoomCode = ws.ValidateData(irow(6), objType._String).ToString().Trim()
                                Dim _ClaimNoticeDate = DirectCast(irow(1), DateTime).ToString("yyyy-MM-dd")
                                Dim _ClaimNoticeTime = DirectCast(irow(1), DateTime).ToString("HH:mm")
                                Dim _ClaimDetails = ws.ValidateData(irow(18), objType._String).ToString().Trim()
                                Dim _ClaimType As Integer = 0 '1=เคลมสด, 2=แห้ง
                                If ws.ValidateData(irow(19), objType._String).ToString.Equals("เคลมสด") Then
                                    _ClaimType = 1
                                ElseIf ws.ValidateData(irow(19), objType._String).ToString.Equals("เคลมแห้ง") Then
                                    _ClaimType = 2
                                End If
                                Dim _ClaimResult As Integer = 0 '1=ถูก, 2=ผิด, 3=ประมาทร่วม, 4=รอผลคดี
                                If ws.ValidateData(irow(20), objType._String).ToString.Equals("เคลมถูก") Then
                                    _ClaimResult = 1
                                ElseIf ws.ValidateData(irow(20), objType._String).ToString.Equals("เคลมผิด") Then
                                    _ClaimResult = 2
                                ElseIf ws.ValidateData(irow(20), objType._String).ToString.Equals("ประมาทร่วม") Then
                                    _ClaimResult = 3
                                End If
                                Dim _ClaimDamageDetails = "-"
                                Dim _CallCenter = "-"
                                Dim _AccidentDate = DirectCast(irow(0), DateTime).ToString("yyyy-MM-dd")
                                Dim _AccidentTime = DirectCast(irow(0), DateTime).ToString("HH:mm")
                                Dim _AccidentPlace = ws.ValidateData(irow(14), objType._String).ToString().Trim()
                                Dim _AccidentTumbon = "-"
                                Dim _AccidentAmphur = "-"
                                Dim _AccidentProvince = "-"
                                Dim _AccidentZipcode = "-"
                                Dim _DriverName = "-"
                                Dim _DriverTel = "-"
                                Dim _NoticeName = ws.ValidateData(irow(22), objType._String).ToString().Trim()
                                Dim _NoticeTel = "0" & ws.ValidateData(irow(23), objType._String).ToString().Trim()
                                Dim _GarageType = 0
                                Dim _GarageCode = "-"
                                Dim _GarageName = "-"
                                Dim _GaragePlace = "-"
                                Dim _GarageTumbon = "-"
                                Dim _GarageAmphur = "-"
                                Dim _GarageProvince = "-"
                                Dim _GarageZipcode = "-"
                                Dim _CarRepairDate = "-"
                                Dim _CarReceiveDate = "-"
                                Dim _ConsentFormNo = "-"
                                Dim _PartsDealerName = "-"
                                Dim _PaymentDetails = "-"
                                Dim _Amount1 = ws.ValidateData(irow(21), objType._Double) 'irow(21)
                                Dim _Amount2 = 0
                                Dim _Amount3 = 0
                                Dim _Remark = ws.ValidateData(irow(24), objType._String).ToString().Trim()

                                Using dc_MC As New DataClasses_MotorClaimDataExt()
                                    If Not String.IsNullOrEmpty(_ShowRoomCode) Then
                                        Dim _Showroom = (From c In dc_MC.tblShowRooms Where c.ShowRoomCode.Equals(_ShowRoomCode) Or c.ShowroomID.ToString().Equals(_ShowRoomCode)).FirstOrDefault()
                                        If _Showroom IsNot Nothing Then
                                            _ShowRoomName = _Showroom.ShowRoomName
                                            _ShowRoomCode = _Showroom.ShowRoomCode
                                        Else
                                            _ShowRoomCode = Nothing
                                        End If
                                    End If



                                    If Not String.IsNullOrEmpty(_ChassisNo) And String.IsNullOrEmpty(_ShowRoomCode) Then
                                        Using dc_M2 As New DataClasses_NLTDBExt()
                                            Dim _App = (From c In dc_M2.Applications Where c.VIN.Equals(_ChassisNo) Order By c.AID Descending).FirstOrDefault()
                                            If _App IsNot Nothing Then
                                                Dim _dealer = (From c In dc_MC.tblShowRooms Where c.ShowroomID.Equals(_App.ShowroomCode) And Not (c.DealerCode Is Nothing) And c.IsActive = True).FirstOrDefault()
                                                If _dealer IsNot Nothing Then
                                                    _ShowRoomName = _dealer.ShowRoomName
                                                    _ShowRoomCode = _dealer.ShowRoomCode
                                                Else
                                                    _ShowRoomCode = _App.ShowroomCode
                                                    Dim _Showroom2 = (From c In dc_M2.Showrooms Where c.ShowroomID.Equals(_App.ShowroomCode)).FirstOrDefault()
                                                    If _Showroom2 IsNot Nothing Then
                                                        _ShowRoomName = _Showroom2.CompanyName
                                                    End If
                                                End If
                                            End If
                                        End Using
                                    End If






                                End Using




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
                                    If _Amount1 Is Nothing Then
                                        .Amount1 = "0"
                                    Else
                                        .Amount1 = _Amount1.ToString()
                                    End If
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
                            End If

                        Next

                    End Using

                End Using

            Else
                _Result = MyCache(MCGUID)
            End If


            Return _Result

        End Function


    End Class

    <Serializable()> _
    Public Class ClaimResultMessage
        Private _ResultCode As String
        Private _ResultMessage As String
        Public Property ResultCode() As String
            Get
                Return _ResultCode
            End Get
            Set(ByVal value As String)
                _ResultCode = value
            End Set
        End Property

        Public Property ResultMessage() As String
            Get
                Return _ResultMessage
            End Get
            Set(ByVal value As String)
                _ResultMessage = value
            End Set
        End Property
    End Class

    <Serializable()> _
    Public Class ClaimTransaction_Data_Result
        Private _TRNo As String
        Private _ResultNo As String
        Private _ResultStatus As Boolean
        Private _ResultMessage As List(Of ClaimResultMessage)
        Private _SubmitDate As DateTime
        Public Property TRNo() As String
            Get
                Return _TRNo
            End Get
            Set(ByVal value As String)
                _TRNo = value
            End Set
        End Property
        Public Property ResultNo() As String
            Get
                Return _ResultNo
            End Get
            Set(ByVal value As String)
                _ResultNo = value
            End Set
        End Property

        Public Property ResultStatus() As Boolean
            Get
                Return _ResultStatus
            End Get
            Set(ByVal value As Boolean)
                _ResultStatus = value
            End Set
        End Property

        Public Property ResultMessage() As List(Of ClaimResultMessage)
            Get
                Return _ResultMessage
            End Get
            Set(ByVal value As List(Of ClaimResultMessage))
                _ResultMessage = value
            End Set
        End Property

        Public Property SubmitDate() As DateTime
            Get
                Return _SubmitDate
            End Get
            Set(ByVal value As DateTime)
                _SubmitDate = value
            End Set
        End Property

    End Class

    <Serializable()> _
    Public Class ClaimTransaction_Data
        'Private _TRID As Integer
        'Private _TRNo As String
        Private _ClaimStatus As String
        Private _TempPolicy As String
        Private _RefNo As String
        Private _Version As Integer
        Private _PolicyNo As String
        Private _PolicyYear As Integer
        Private _ClaimNo As String
        Private _TransactionDate As String
        Private _Unwriter As String
        Private _InsuredName As String
        Private _EffectiveDate As String
        Private _ExpiryDate As String
        Private _Beneficiary As String
        Private _CarBrand As String
        Private _CarModel As String
        Private _CarLicense As String
        Private _CarYear As String
        Private _ChassisNo As String
        Private _ShowRoomName As String
        Private _ShowRoomCode As String
        Private _ClaimNoticeDate As String
        Private _ClaimNoticeTime As String
        Private _ClaimDetails As String
        Private _ClaimType As Integer
        Private _ClaimResult As Integer
        Private _ClaimDamageDetails As String
        Private _CallCenter As String
        Private _AccidentDate As String
        Private _AccidentTime As String
        Private _AccidentPlace As String
        Private _AccidentTumbon As String
        Private _AccidentAmphur As String
        Private _AccidentProvince As String
        Private _AccidentZipcode As String
        Private _DriverName As String
        Private _DriverTel As String
        Private _NoticeName As String
        Private _NoticeTel As String
        Private _GarageType As Integer
        Private _GarageCode As String
        Private _GarageName As String
        Private _GaragePlace As String
        Private _GarageTumbon As String
        Private _GarageAmphur As String
        Private _GarageProvince As String
        Private _GarageZipcode As String
        Private _CarRepairDate As String
        Private _CarReceiveDate As String
        Private _ConsentFormNo As String
        Private _PartsDealerName As String
        Private _PaymentDetails As String
        Private _Amount1 As Double
        Private _Amount2 As Double
        Private _Amount3 As Double
        Private _Remark As String


        Public Property ClaimStatus() As String
            Get
                Return _ClaimStatus
            End Get
            Set(ByVal value As String)
                _ClaimStatus = value
            End Set
        End Property

        Public Property TempPolicy() As String
            Get
                Return _TempPolicy
            End Get
            Set(ByVal value As String)
                _TempPolicy = value
            End Set
        End Property

        Public Property RefNo() As String
            Get
                Return _RefNo
            End Get
            Set(ByVal value As String)
                _RefNo = value
            End Set
        End Property

        Public Property Version() As Integer
            Get
                Return _Version
            End Get
            Set(ByVal value As Integer)
                _Version = value
            End Set
        End Property

        Public Property PolicyNo() As String
            Get
                Return _PolicyNo
            End Get
            Set(ByVal value As String)
                _PolicyNo = value
            End Set
        End Property

        Public Property PolicyYear() As Integer
            Get
                Return _PolicyYear
            End Get
            Set(ByVal value As Integer)
                _PolicyYear = value
            End Set
        End Property

        Public Property ClaimNo() As String
            Get
                Return _ClaimNo
            End Get
            Set(ByVal value As String)
                _ClaimNo = value
            End Set
        End Property

        Public Property TransactionDate() As String
            Get
                Return _TransactionDate
            End Get
            Set(ByVal value As String)
                _TransactionDate = value
            End Set
        End Property

        Public Property Unwriter() As String
            Get
                Return _Unwriter
            End Get
            Set(ByVal value As String)
                _Unwriter = value
            End Set
        End Property

        Public Property InsuredName() As String
            Get
                Return _InsuredName
            End Get
            Set(ByVal value As String)
                _InsuredName = value
            End Set
        End Property

        Public Property EffectiveDate() As String
            Get
                Return _EffectiveDate
            End Get
            Set(ByVal value As String)
                _EffectiveDate = value
            End Set
        End Property

        Public Property ExpiryDate() As String
            Get
                Return _ExpiryDate
            End Get
            Set(ByVal value As String)
                _ExpiryDate = value
            End Set
        End Property

        Public Property Beneficiary() As String
            Get
                Return _Beneficiary
            End Get
            Set(ByVal value As String)
                _Beneficiary = value
            End Set
        End Property

        Public Property CarBrand() As String
            Get
                Return _CarBrand
            End Get
            Set(ByVal value As String)
                _CarBrand = value
            End Set
        End Property

        Public Property CarModel() As String
            Get
                Return _CarModel
            End Get
            Set(ByVal value As String)
                _CarModel = value
            End Set
        End Property

        Public Property CarLicense() As String
            Get
                Return _CarLicense
            End Get
            Set(ByVal value As String)
                _CarLicense = value
            End Set
        End Property

        Public Property CarYear() As String
            Get
                Return _CarYear
            End Get
            Set(ByVal value As String)
                _CarYear = value
            End Set
        End Property

        Public Property ChassisNo() As String
            Get
                Return _ChassisNo
            End Get
            Set(ByVal value As String)
                _ChassisNo = value
            End Set
        End Property

        Public Property ShowRoomName() As String
            Get
                Return _ShowRoomName
            End Get
            Set(ByVal value As String)
                _ShowRoomName = value
            End Set
        End Property

        Public Property ShowRoomCode() As String
            Get
                Return _ShowRoomCode
            End Get
            Set(ByVal value As String)
                _ShowRoomCode = value
            End Set
        End Property

        Public Property ClaimNoticeDate() As String
            Get
                Return _ClaimNoticeDate
            End Get
            Set(ByVal value As String)
                _ClaimNoticeDate = value
            End Set
        End Property

        Public Property ClaimNoticeTime() As String
            Get
                Return _ClaimNoticeTime
            End Get
            Set(ByVal value As String)
                _ClaimNoticeTime = value
            End Set
        End Property

        Public Property ClaimDetails() As String
            Get
                Return _ClaimDetails
            End Get
            Set(ByVal value As String)
                _ClaimDetails = value
            End Set
        End Property

        Public Property ClaimType() As Integer
            Get
                Return _ClaimType
            End Get
            Set(ByVal value As Integer)
                _ClaimType = value
            End Set
        End Property

        Public Property ClaimResult() As Integer
            Get
                Return _ClaimResult
            End Get
            Set(ByVal value As Integer)
                _ClaimResult = value
            End Set
        End Property

        Public Property ClaimDamageDetails() As String
            Get
                Return _ClaimDamageDetails
            End Get
            Set(ByVal value As String)
                _ClaimDamageDetails = value
            End Set
        End Property

        Public Property CallCenter() As String
            Get
                Return _CallCenter
            End Get
            Set(ByVal value As String)
                _CallCenter = value
            End Set
        End Property

        Public Property AccidentDate() As String
            Get
                Return _AccidentDate
            End Get
            Set(ByVal value As String)
                _AccidentDate = value
            End Set
        End Property

        Public Property AccidentTime() As String
            Get
                Return _AccidentTime
            End Get
            Set(ByVal value As String)
                _AccidentTime = value
            End Set
        End Property

        Public Property AccidentPlace() As String
            Get
                Return _AccidentPlace
            End Get
            Set(ByVal value As String)
                _AccidentPlace = value
            End Set
        End Property

        Public Property AccidentTumbon() As String
            Get
                Return _AccidentTumbon
            End Get
            Set(ByVal value As String)
                _AccidentTumbon = value
            End Set
        End Property

        Public Property AccidentAmphur() As String
            Get
                Return _AccidentAmphur
            End Get
            Set(ByVal value As String)
                _AccidentAmphur = value
            End Set
        End Property

        Public Property AccidentProvince() As String
            Get
                Return _AccidentProvince
            End Get
            Set(ByVal value As String)
                _AccidentProvince = value
            End Set
        End Property

        Public Property AccidentZipcode() As String
            Get
                Return _AccidentZipcode
            End Get
            Set(ByVal value As String)
                _AccidentZipcode = value
            End Set
        End Property

        Public Property DriverName() As String
            Get
                Return _DriverName
            End Get
            Set(ByVal value As String)
                _DriverName = value
            End Set
        End Property

        Public Property DriverTel() As String
            Get
                Return _DriverTel
            End Get
            Set(ByVal value As String)
                _DriverTel = value
            End Set
        End Property

        Public Property NoticeName() As String
            Get
                Return _NoticeName
            End Get
            Set(ByVal value As String)
                _NoticeName = value
            End Set
        End Property

        Public Property NoticeTel() As String
            Get
                Return _NoticeTel
            End Get
            Set(ByVal value As String)
                _NoticeTel = value
            End Set
        End Property

        Public Property GarageType() As Integer
            Get
                Return _GarageType
            End Get
            Set(ByVal value As Integer)
                _GarageType = value
            End Set
        End Property

        Public Property GarageCode() As String
            Get
                Return _GarageCode
            End Get
            Set(ByVal value As String)
                _GarageCode = value
            End Set
        End Property

        Public Property GarageName() As String
            Get
                Return _GarageName
            End Get
            Set(ByVal value As String)
                _GarageName = value
            End Set
        End Property

        Public Property GaragePlace() As String
            Get
                Return _GaragePlace
            End Get
            Set(ByVal value As String)
                _GaragePlace = value
            End Set
        End Property

        Public Property GarageTumbon() As String
            Get
                Return _GarageTumbon
            End Get
            Set(ByVal value As String)
                _GarageTumbon = value
            End Set
        End Property

        Public Property GarageAmphur() As String
            Get
                Return _GarageAmphur
            End Get
            Set(ByVal value As String)
                _GarageAmphur = value
            End Set
        End Property

        Public Property GarageProvince() As String
            Get
                Return _GarageProvince
            End Get
            Set(ByVal value As String)
                _GarageProvince = value
            End Set
        End Property

        Public Property GarageZipcode() As String
            Get
                Return _GarageZipcode
            End Get
            Set(ByVal value As String)
                _GarageZipcode = value
            End Set
        End Property

        Public Property CarRepairDate() As String
            Get
                Return _CarRepairDate
            End Get
            Set(ByVal value As String)
                _CarRepairDate = value
            End Set
        End Property

        Public Property CarReceiveDate() As String
            Get
                Return _CarReceiveDate
            End Get
            Set(ByVal value As String)
                _CarReceiveDate = value
            End Set
        End Property

        Public Property ConsentFormNo() As String
            Get
                Return _ConsentFormNo
            End Get
            Set(ByVal value As String)
                _ConsentFormNo = value
            End Set
        End Property

        Public Property PartsDealerName() As String
            Get
                Return _PartsDealerName
            End Get
            Set(ByVal value As String)
                _PartsDealerName = value
            End Set
        End Property

        Public Property PaymentDetails() As String
            Get
                Return _PaymentDetails
            End Get
            Set(ByVal value As String)
                _PaymentDetails = value
            End Set
        End Property

        Public Property Amount1() As Double
            Get
                Return _Amount1
            End Get
            Set(ByVal value As Double)
                _Amount1 = value
            End Set
        End Property

        Public Property Amount2() As Double
            Get
                Return _Amount2
            End Get
            Set(ByVal value As Double)
                _Amount2 = value
            End Set
        End Property

        Public Property Amount3() As Double
            Get
                Return _Amount3
            End Get
            Set(ByVal value As Double)
                _Amount3 = value
            End Set
        End Property

        Public Property Remark() As String
            Get
                Return _Remark
            End Get
            Set(ByVal value As String)
                _Remark = value
            End Set
        End Property

    End Class


    <Serializable()> _
    Public Class ClaimTransaction_ConsentForm
        'Private _Unwriter As String
        Private _ConsentFormNo As String
        Private _ConsentFormFileType As String
        Private _ConsentFormData As Byte()
        'Public Property Unwriter() As String
        '    Get
        '        Return _Unwriter
        '    End Get
        '    Set(ByVal value As String)
        '        _Unwriter = value
        '    End Set
        'End Property
        Public Property ConsentFormNo() As String
            Get
                Return _ConsentFormNo
            End Get
            Set(ByVal value As String)
                _ConsentFormNo = value
            End Set
        End Property
        Public Property ConsentFormFileType() As String
            Get
                Return _ConsentFormFileType
            End Get
            Set(ByVal value As String)
                _ConsentFormFileType = value
            End Set
        End Property
        Public Property ConsentFormData() As Byte()
            Get
                Return _ConsentFormData
            End Get
            Set(ByVal value As Byte())
                _ConsentFormData = value
            End Set
        End Property

    End Class




    <Serializable()> _
    Public Class ClaimTransaction_DataObject
        Private _ClaimStatus As String
        Private _TempPolicy As String
        Private _RefNo As String
        Private _Version As Integer
        Private _PolicyNo As String
        Private _PolicyYear As Integer
        Private _ClaimNo As String
        Private _TransactionDate As String
        Private _Unwriter As String
        Private _InsuredName As String
        Private _EffectiveDate As String
        Private _ExpiryDate As String
        Private _Beneficiary As String
        Private _CarBrand As String
        Private _CarModel As String
        Private _CarLicense As String
        Private _CarYear As String
        Private _ChassisNo As String
        Private _ShowRoomName As String
        Private _ShowRoomCode As String
        Private _ClaimNoticeDate As String
        Private _ClaimNoticeTime As String
        Private _ClaimDetails As String
        Private _ClaimType As Integer
        Private _ClaimResult As Integer
        Private _ClaimDamageDetails As String
        Private _CallCenter As String
        Private _AccidentDate As String
        Private _AccidentTime As String
        Private _AccidentPlace As String
        Private _AccidentTumbon As String
        Private _AccidentAmphur As String
        Private _AccidentProvince As String
        Private _AccidentZipcode As String
        Private _DriverName As String
        Private _DriverTel As String
        Private _NoticeName As String
        Private _NoticeTel As String
        Private _GarageType As Integer
        Private _GarageCode As String
        Private _GarageName As String
        Private _GaragePlace As String
        Private _GarageTumbon As String
        Private _GarageAmphur As String
        Private _GarageProvince As String
        Private _GarageZipcode As String
        Private _CarRepairDate As String
        Private _CarReceiveDate As String
        Private _ConsentFormNo As String
        Private _PartsDealerName As String
        Private _PaymentDetails As String
        Private _Amount1 As Double
        Private _Amount2 As Double
        Private _Amount3 As Double
        Private _Remark As String

        Public Property ClaimStatus() As String
            Get
                Return _ClaimStatus
            End Get
            Set(ByVal value As String)
                _ClaimStatus = value
            End Set
        End Property

        Public Property TempPolicy() As String
            Get
                Return _TempPolicy
            End Get
            Set(ByVal value As String)
                _TempPolicy = value
            End Set
        End Property

        Public Property RefNo() As String
            Get
                Return _RefNo
            End Get
            Set(ByVal value As String)
                _RefNo = value
            End Set
        End Property

        Public Property Version() As Integer
            Get
                Return _Version
            End Get
            Set(ByVal value As Integer)
                _Version = value
            End Set
        End Property

        Public Property PolicyNo() As String
            Get
                Return _PolicyNo
            End Get
            Set(ByVal value As String)
                _PolicyNo = value
            End Set
        End Property

        Public Property PolicyYear() As Integer
            Get
                Return _PolicyYear
            End Get
            Set(ByVal value As Integer)
                _PolicyYear = value
            End Set
        End Property

        Public Property ClaimNo() As String
            Get
                Return _ClaimNo
            End Get
            Set(ByVal value As String)
                _ClaimNo = value
            End Set
        End Property

        Public Property TransactionDate() As String
            Get
                Return _TransactionDate
            End Get
            Set(ByVal value As String)
                _TransactionDate = value
            End Set
        End Property

        Public Property Unwriter() As String
            Get
                Return _Unwriter
            End Get
            Set(ByVal value As String)
                _Unwriter = value
            End Set
        End Property

        Public Property InsuredName() As String
            Get
                Return _InsuredName
            End Get
            Set(ByVal value As String)
                _InsuredName = value
            End Set
        End Property

        Public Property EffectiveDate() As String
            Get
                Return _EffectiveDate
            End Get
            Set(ByVal value As String)
                _EffectiveDate = value
            End Set
        End Property

        Public Property ExpiryDate() As String
            Get
                Return _ExpiryDate
            End Get
            Set(ByVal value As String)
                _ExpiryDate = value
            End Set
        End Property

        Public Property Beneficiary() As String
            Get
                Return _Beneficiary
            End Get
            Set(ByVal value As String)
                _Beneficiary = value
            End Set
        End Property

        Public Property CarBrand() As String
            Get
                Return _CarBrand
            End Get
            Set(ByVal value As String)
                _CarBrand = value
            End Set
        End Property

        Public Property CarModel() As String
            Get
                Return _CarModel
            End Get
            Set(ByVal value As String)
                _CarModel = value
            End Set
        End Property

        Public Property CarLicense() As String
            Get
                Return _CarLicense
            End Get
            Set(ByVal value As String)
                _CarLicense = value
            End Set
        End Property

        Public Property CarYear() As String
            Get
                Return _CarYear
            End Get
            Set(ByVal value As String)
                _CarYear = value
            End Set
        End Property

        Public Property ChassisNo() As String
            Get
                Return _ChassisNo
            End Get
            Set(ByVal value As String)
                _ChassisNo = value
            End Set
        End Property

        Public Property ShowRoomName() As String
            Get
                Return _ShowRoomName
            End Get
            Set(ByVal value As String)
                _ShowRoomName = value
            End Set
        End Property

        Public Property ShowRoomCode() As String
            Get
                Return _ShowRoomCode
            End Get
            Set(ByVal value As String)
                _ShowRoomCode = value
            End Set
        End Property

        Public Property ClaimNoticeDate() As String
            Get
                Return _ClaimNoticeDate
            End Get
            Set(ByVal value As String)
                _ClaimNoticeDate = value
            End Set
        End Property

        Public Property ClaimNoticeTime() As String
            Get
                Return _ClaimNoticeTime
            End Get
            Set(ByVal value As String)
                _ClaimNoticeTime = value
            End Set
        End Property

        Public Property ClaimDetails() As String
            Get
                Return _ClaimDetails
            End Get
            Set(ByVal value As String)
                _ClaimDetails = value
            End Set
        End Property

        Public Property ClaimType() As Integer
            Get
                Return _ClaimType
            End Get
            Set(ByVal value As Integer)
                _ClaimType = value
            End Set
        End Property

        Public Property ClaimResult() As Integer
            Get
                Return _ClaimResult
            End Get
            Set(ByVal value As Integer)
                _ClaimResult = value
            End Set
        End Property

        Public Property ClaimDamageDetails() As String
            Get
                Return _ClaimDamageDetails
            End Get
            Set(ByVal value As String)
                _ClaimDamageDetails = value
            End Set
        End Property

        Public Property CallCenter() As String
            Get
                Return _CallCenter
            End Get
            Set(ByVal value As String)
                _CallCenter = value
            End Set
        End Property

        Public Property AccidentDate() As String
            Get
                Return _AccidentDate
            End Get
            Set(ByVal value As String)
                _AccidentDate = value
            End Set
        End Property

        Public Property AccidentTime() As String
            Get
                Return _AccidentTime
            End Get
            Set(ByVal value As String)
                _AccidentTime = value
            End Set
        End Property

        Public Property AccidentPlace() As String
            Get
                Return _AccidentPlace
            End Get
            Set(ByVal value As String)
                _AccidentPlace = value
            End Set
        End Property

        Public Property AccidentTumbon() As String
            Get
                Return _AccidentTumbon
            End Get
            Set(ByVal value As String)
                _AccidentTumbon = value
            End Set
        End Property

        Public Property AccidentAmphur() As String
            Get
                Return _AccidentAmphur
            End Get
            Set(ByVal value As String)
                _AccidentAmphur = value
            End Set
        End Property

        Public Property AccidentProvince() As String
            Get
                Return _AccidentProvince
            End Get
            Set(ByVal value As String)
                _AccidentProvince = value
            End Set
        End Property

        Public Property AccidentZipcode() As String
            Get
                Return _AccidentZipcode
            End Get
            Set(ByVal value As String)
                _AccidentZipcode = value
            End Set
        End Property

        Public Property DriverName() As String
            Get
                Return _DriverName
            End Get
            Set(ByVal value As String)
                _DriverName = value
            End Set
        End Property

        Public Property DriverTel() As String
            Get
                Return _DriverTel
            End Get
            Set(ByVal value As String)
                _DriverTel = value
            End Set
        End Property

        Public Property NoticeName() As String
            Get
                Return _NoticeName
            End Get
            Set(ByVal value As String)
                _NoticeName = value
            End Set
        End Property

        Public Property NoticeTel() As String
            Get
                Return _NoticeTel
            End Get
            Set(ByVal value As String)
                _NoticeTel = value
            End Set
        End Property

        Public Property GarageType() As Integer
            Get
                Return _GarageType
            End Get
            Set(ByVal value As Integer)
                _GarageType = value
            End Set
        End Property

        Public Property GarageCode() As String
            Get
                Return _GarageCode
            End Get
            Set(ByVal value As String)
                _GarageCode = value
            End Set
        End Property

        Public Property GarageName() As String
            Get
                Return _GarageName
            End Get
            Set(ByVal value As String)
                _GarageName = value
            End Set
        End Property

        Public Property GaragePlace() As String
            Get
                Return _GaragePlace
            End Get
            Set(ByVal value As String)
                _GaragePlace = value
            End Set
        End Property

        Public Property GarageTumbon() As String
            Get
                Return _GarageTumbon
            End Get
            Set(ByVal value As String)
                _GarageTumbon = value
            End Set
        End Property

        Public Property GarageAmphur() As String
            Get
                Return _GarageAmphur
            End Get
            Set(ByVal value As String)
                _GarageAmphur = value
            End Set
        End Property

        Public Property GarageProvince() As String
            Get
                Return _GarageProvince
            End Get
            Set(ByVal value As String)
                _GarageProvince = value
            End Set
        End Property

        Public Property GarageZipcode() As String
            Get
                Return _GarageZipcode
            End Get
            Set(ByVal value As String)
                _GarageZipcode = value
            End Set
        End Property

        Public Property CarRepairDate() As String
            Get
                Return _CarRepairDate
            End Get
            Set(ByVal value As String)
                _CarRepairDate = value
            End Set
        End Property

        Public Property CarReceiveDate() As String
            Get
                Return _CarReceiveDate
            End Get
            Set(ByVal value As String)
                _CarReceiveDate = value
            End Set
        End Property

        Public Property ConsentFormNo() As String
            Get
                Return _ConsentFormNo
            End Get
            Set(ByVal value As String)
                _ConsentFormNo = value
            End Set
        End Property

        Public Property PartsDealerName() As String
            Get
                Return _PartsDealerName
            End Get
            Set(ByVal value As String)
                _PartsDealerName = value
            End Set
        End Property

        Public Property PaymentDetails() As String
            Get
                Return _PaymentDetails
            End Get
            Set(ByVal value As String)
                _PaymentDetails = value
            End Set
        End Property

        Public Property Amount1() As Double
            Get
                Return _Amount1
            End Get
            Set(ByVal value As Double)
                _Amount1 = value
            End Set
        End Property

        Public Property Amount2() As Double
            Get
                Return _Amount2
            End Get
            Set(ByVal value As Double)
                _Amount2 = value
            End Set
        End Property

        Public Property Amount3() As Double
            Get
                Return _Amount3
            End Get
            Set(ByVal value As Double)
                _Amount3 = value
            End Set
        End Property

        Public Property Remark() As String
            Get
                Return _Remark
            End Get
            Set(ByVal value As String)
                _Remark = value
            End Set
        End Property


    End Class



    <Serializable()> _
    Public Class ClaimUpload_DataObject
        Private _RunNo As String
        Private _ClaimStatus As String
        Private _TempPolicy As String
        Private _RefNo As String
        Private _Version As String
        Private _PolicyNo As String
        Private _PolicyYear As String
        Private _ClaimNo As String
        Private _TransactionDate As String
        Private _Unwriter As String
        Private _InsuredName As String
        Private _EffectiveDate As String
        Private _ExpiryDate As String
        Private _Beneficiary As String
        Private _CarBrand As String
        Private _CarModel As String
        Private _CarLicense As String
        Private _CarYear As String
        Private _ChassisNo As String
        Private _ShowRoomName As String
        Private _ShowRoomCode As String
        Private _ClaimNoticeDate As String
        Private _ClaimNoticeTime As String
        Private _ClaimDetails As String
        Private _ClaimType As String
        Private _ClaimResult As String
        Private _ClaimDamageDetails As String
        Private _CallCenter As String
        Private _AccidentDate As String
        Private _AccidentTime As String
        Private _AccidentPlace As String
        Private _AccidentTumbon As String
        Private _AccidentAmphur As String
        Private _AccidentProvince As String
        Private _AccidentZipcode As String
        Private _DriverName As String
        Private _DriverTel As String
        Private _NoticeName As String
        Private _NoticeTel As String
        Private _GarageType As String
        Private _GarageCode As String
        Private _GarageName As String
        Private _GaragePlace As String
        Private _GarageTumbon As String
        Private _GarageAmphur As String
        Private _GarageProvince As String
        Private _GarageZipcode As String
        Private _CarRepairDate As String
        Private _CarReceiveDate As String
        Private _ConsentFormNo As String
        Private _PartsDealerName As String
        Private _PaymentDetails As String
        Private _Amount1 As String
        Private _Amount2 As String
        Private _Amount3 As String
        Private _Remark As String

        Private _Status As String
        Private _Message As String


        Public Property RunNo() As String
            Get
                Return _RunNo
            End Get
            Set(ByVal value As String)
                _RunNo = value
            End Set
        End Property

        Public Property ClaimStatus() As String
            Get
                Return _ClaimStatus
            End Get
            Set(ByVal value As String)
                _ClaimStatus = value
            End Set
        End Property

        Public Property TempPolicy() As String
            Get
                Return _TempPolicy
            End Get
            Set(ByVal value As String)
                _TempPolicy = value
            End Set
        End Property

        Public Property RefNo() As String
            Get
                Return _RefNo
            End Get
            Set(ByVal value As String)
                _RefNo = value
            End Set
        End Property

        Public Property Version() As String
            Get
                Return _Version
            End Get
            Set(ByVal value As String)
                _Version = value
            End Set
        End Property

        Public Property PolicyNo() As String
            Get
                Return _PolicyNo
            End Get
            Set(ByVal value As String)
                _PolicyNo = value
            End Set
        End Property

        Public Property PolicyYear() As String
            Get
                Return _PolicyYear
            End Get
            Set(ByVal value As String)
                _PolicyYear = value
            End Set
        End Property

        Public Property ClaimNo() As String
            Get
                Return _ClaimNo
            End Get
            Set(ByVal value As String)
                _ClaimNo = value
            End Set
        End Property

        Public Property TransactionDate() As String
            Get
                Return _TransactionDate
            End Get
            Set(ByVal value As String)
                _TransactionDate = value
            End Set
        End Property

        Public Property Unwriter() As String
            Get
                Return _Unwriter
            End Get
            Set(ByVal value As String)
                _Unwriter = value
            End Set
        End Property

        Public Property InsuredName() As String
            Get
                Return _InsuredName
            End Get
            Set(ByVal value As String)
                _InsuredName = value
            End Set
        End Property

        Public Property EffectiveDate() As String
            Get
                Return _EffectiveDate
            End Get
            Set(ByVal value As String)
                _EffectiveDate = value
            End Set
        End Property

        Public Property ExpiryDate() As String
            Get
                Return _ExpiryDate
            End Get
            Set(ByVal value As String)
                _ExpiryDate = value
            End Set
        End Property

        Public Property Beneficiary() As String
            Get
                Return _Beneficiary
            End Get
            Set(ByVal value As String)
                _Beneficiary = value
            End Set
        End Property

        Public Property CarBrand() As String
            Get
                Return _CarBrand
            End Get
            Set(ByVal value As String)
                _CarBrand = value
            End Set
        End Property

        Public Property CarModel() As String
            Get
                Return _CarModel
            End Get
            Set(ByVal value As String)
                _CarModel = value
            End Set
        End Property

        Public Property CarLicense() As String
            Get
                Return _CarLicense
            End Get
            Set(ByVal value As String)
                _CarLicense = value
            End Set
        End Property

        Public Property CarYear() As String
            Get
                Return _CarYear
            End Get
            Set(ByVal value As String)
                _CarYear = value
            End Set
        End Property

        Public Property ChassisNo() As String
            Get
                Return _ChassisNo
            End Get
            Set(ByVal value As String)
                _ChassisNo = value
            End Set
        End Property

        Public Property ShowRoomName() As String
            Get
                Return _ShowRoomName
            End Get
            Set(ByVal value As String)
                _ShowRoomName = value
            End Set
        End Property

        Public Property ShowRoomCode() As String
            Get
                Return _ShowRoomCode
            End Get
            Set(ByVal value As String)
                _ShowRoomCode = value
            End Set
        End Property

        Public Property ClaimNoticeDate() As String
            Get
                Return _ClaimNoticeDate
            End Get
            Set(ByVal value As String)
                _ClaimNoticeDate = value
            End Set
        End Property

        Public Property ClaimNoticeTime() As String
            Get
                Return _ClaimNoticeTime
            End Get
            Set(ByVal value As String)
                _ClaimNoticeTime = value
            End Set
        End Property

        Public Property ClaimDetails() As String
            Get
                Return _ClaimDetails
            End Get
            Set(ByVal value As String)
                _ClaimDetails = value
            End Set
        End Property

        Public Property ClaimType() As String
            Get
                Return _ClaimType
            End Get
            Set(ByVal value As String)
                _ClaimType = value
            End Set
        End Property

        Public Property ClaimResult() As String
            Get
                Return _ClaimResult
            End Get
            Set(ByVal value As String)
                _ClaimResult = value
            End Set
        End Property

        Public Property ClaimDamageDetails() As String
            Get
                Return _ClaimDamageDetails
            End Get
            Set(ByVal value As String)
                _ClaimDamageDetails = value
            End Set
        End Property

        Public Property CallCenter() As String
            Get
                Return _CallCenter
            End Get
            Set(ByVal value As String)
                _CallCenter = value
            End Set
        End Property

        Public Property AccidentDate() As String
            Get
                Return _AccidentDate
            End Get
            Set(ByVal value As String)
                _AccidentDate = value
            End Set
        End Property

        Public Property AccidentTime() As String
            Get
                Return _AccidentTime
            End Get
            Set(ByVal value As String)
                _AccidentTime = value
            End Set
        End Property

        Public Property AccidentPlace() As String
            Get
                Return _AccidentPlace
            End Get
            Set(ByVal value As String)
                _AccidentPlace = value
            End Set
        End Property

        Public Property AccidentTumbon() As String
            Get
                Return _AccidentTumbon
            End Get
            Set(ByVal value As String)
                _AccidentTumbon = value
            End Set
        End Property

        Public Property AccidentAmphur() As String
            Get
                Return _AccidentAmphur
            End Get
            Set(ByVal value As String)
                _AccidentAmphur = value
            End Set
        End Property

        Public Property AccidentProvince() As String
            Get
                Return _AccidentProvince
            End Get
            Set(ByVal value As String)
                _AccidentProvince = value
            End Set
        End Property

        Public Property AccidentZipcode() As String
            Get
                Return _AccidentZipcode
            End Get
            Set(ByVal value As String)
                _AccidentZipcode = value
            End Set
        End Property

        Public Property DriverName() As String
            Get
                Return _DriverName
            End Get
            Set(ByVal value As String)
                _DriverName = value
            End Set
        End Property

        Public Property DriverTel() As String
            Get
                Return _DriverTel
            End Get
            Set(ByVal value As String)
                _DriverTel = value
            End Set
        End Property

        Public Property NoticeName() As String
            Get
                Return _NoticeName
            End Get
            Set(ByVal value As String)
                _NoticeName = value
            End Set
        End Property

        Public Property NoticeTel() As String
            Get
                Return _NoticeTel
            End Get
            Set(ByVal value As String)
                _NoticeTel = value
            End Set
        End Property

        Public Property GarageType() As String
            Get
                Return _GarageType
            End Get
            Set(ByVal value As String)
                _GarageType = value
            End Set
        End Property

        Public Property GarageCode() As String
            Get
                Return _GarageCode
            End Get
            Set(ByVal value As String)
                _GarageCode = value
            End Set
        End Property

        Public Property GarageName() As String
            Get
                Return _GarageName
            End Get
            Set(ByVal value As String)
                _GarageName = value
            End Set
        End Property

        Public Property GaragePlace() As String
            Get
                Return _GaragePlace
            End Get
            Set(ByVal value As String)
                _GaragePlace = value
            End Set
        End Property

        Public Property GarageTumbon() As String
            Get
                Return _GarageTumbon
            End Get
            Set(ByVal value As String)
                _GarageTumbon = value
            End Set
        End Property

        Public Property GarageAmphur() As String
            Get
                Return _GarageAmphur
            End Get
            Set(ByVal value As String)
                _GarageAmphur = value
            End Set
        End Property

        Public Property GarageProvince() As String
            Get
                Return _GarageProvince
            End Get
            Set(ByVal value As String)
                _GarageProvince = value
            End Set
        End Property

        Public Property GarageZipcode() As String
            Get
                Return _GarageZipcode
            End Get
            Set(ByVal value As String)
                _GarageZipcode = value
            End Set
        End Property

        Public Property CarRepairDate() As String
            Get
                Return _CarRepairDate
            End Get
            Set(ByVal value As String)
                _CarRepairDate = value
            End Set
        End Property

        Public Property CarReceiveDate() As String
            Get
                Return _CarReceiveDate
            End Get
            Set(ByVal value As String)
                _CarReceiveDate = value
            End Set
        End Property

        Public Property ConsentFormNo() As String
            Get
                Return _ConsentFormNo
            End Get
            Set(ByVal value As String)
                _ConsentFormNo = value
            End Set
        End Property

        Public Property PartsDealerName() As String
            Get
                Return _PartsDealerName
            End Get
            Set(ByVal value As String)
                _PartsDealerName = value
            End Set
        End Property

        Public Property PaymentDetails() As String
            Get
                Return _PaymentDetails
            End Get
            Set(ByVal value As String)
                _PaymentDetails = value
            End Set
        End Property

        Public Property Amount1() As String
            Get
                Return _Amount1
            End Get
            Set(ByVal value As String)
                _Amount1 = value
            End Set
        End Property

        Public Property Amount2() As String
            Get
                Return _Amount2
            End Get
            Set(ByVal value As String)
                _Amount2 = value
            End Set
        End Property

        Public Property Amount3() As String
            Get
                Return _Amount3
            End Get
            Set(ByVal value As String)
                _Amount3 = value
            End Set
        End Property

        Public Property Remark() As String
            Get
                Return _Remark
            End Get
            Set(ByVal value As String)
                _Remark = value
            End Set
        End Property
        Public Property Status() As String
            Get
                Return _Status
            End Get
            Set(ByVal value As String)
                _Status = value
            End Set
        End Property

        Public Property Message() As String
            Get
                Return _Message
            End Get
            Set(ByVal value As String)
                _Message = value
            End Set
        End Property

    End Class
End Namespace

