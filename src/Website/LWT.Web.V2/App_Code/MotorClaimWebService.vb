Imports System.Web
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports MotorClaim
Imports System.IO

' To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line.
' <System.Web.Script.Services.ScriptService()> _
<WebService(Namespace:="http://tempuri.org/")> _
<WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)> _
<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Public Class MotorClaimWebService
    Inherits System.Web.Services.WebService



    Public Function GetMotorClaim(ByVal _Data As List(Of ClaimTransaction_DataObject)) As List(Of ClaimTransaction_Data_Result)
        Dim _ReturnResult As New List(Of ClaimTransaction_Data_Result)

        Dim sb_error As New StringBuilder()

        Using dc As New DataClasses_MotorClaimDataExt()



            '===========================================
            Dim _ClaimTransaction_Data As New List(Of tblClaimTransaction_Data)
            'Dim _ClaimTransaction_Result As New List(Of tblClaimTransaction_Result)
            '1. Create TRNo.
            Dim _TRNo As String = ""
            dc.Running_GetByRunningCode(_Data(0).Unwriter, _TRNo)
            Dim _submitdate = Now


            Dim _ip = HttpContext.Current.Request.ServerVariables("REMOTE_ADDR").ToString()

            '2. Get Data
            For Each item In _Data
                Dim _GUID = Guid.NewGuid().ToString()

                '============== Get Date ================
                Dim _tblClaimData As New tblClaimTransaction_Data()
                With _tblClaimData

                    .TRNo = _TRNo
                    .ClaimStatus = ValidateData(item.ClaimStatus, objType._String)
                    .TempPolicy = ValidateData(item.TempPolicy, objType._String)
                    .RefNo = ValidateData(item.RefNo, objType._String)
                    .Version = ValidateData(item.Version, objType._Integer) 'int
                    .PolicyNo = ValidateData(item.PolicyNo, objType._String)
                    .PolicyYear = ValidateData(item.PolicyYear, objType._Integer) 'int
                    .ClaimNo = ValidateData(item.ClaimNo, objType._String)
                    .TransactionDate = ValidateData(item.TransactionDate, objType._String)
                    .Unwriter = ValidateData(item.Unwriter, objType._String)
                    .InsuredName = ValidateData(item.InsuredName, objType._String)
                    .EffectiveDate = ValidateData(item.EffectiveDate, objType._String)
                    .ExpiryDate = ValidateData(item.ExpiryDate, objType._String)
                    .Beneficiary = ValidateData(item.Beneficiary, objType._String)
                    .CarBrand = ValidateData(item.CarBrand, objType._String)
                    .CarModel = ValidateData(item.CarModel, objType._String)
                    .CarLicense = ValidateData(item.CarLicense, objType._String)
                    .CarYear = ValidateData(item.CarYear, objType._String)
                    .ChassisNo = ValidateData(item.ChassisNo, objType._String)
                    .ShowRoomName = ValidateData(item.ShowRoomName, objType._String)
                    .ShowRoomCode = ValidateData(item.ShowRoomCode, objType._String)
                    .ClaimNoticeDate = ValidateData(item.ClaimNoticeDate, objType._String)
                    .ClaimNoticeTime = ValidateData(item.ClaimNoticeTime, objType._String)
                    .ClaimDetails = ValidateData(item.ClaimDetails, objType._String)
                    .ClaimType = ValidateData(item.ClaimType, objType._Integer) 'int
                    .ClaimResult = ValidateData(item.ClaimResult, objType._Integer) 'int
                    .ClaimDamageDetails = ValidateData(item.ClaimDamageDetails, objType._String)
                    .CallCenter = ValidateData(item.CallCenter, objType._String)
                    .AccidentDate = ValidateData(item.AccidentDate, objType._String)
                    .AccidentTime = ValidateData(item.AccidentTime, objType._String)
                    .AccidentPlace = ValidateData(item.AccidentPlace, objType._String)
                    .AccidentTumbon = ValidateData(item.AccidentTumbon, objType._String)
                    .AccidentAmphur = ValidateData(item.AccidentAmphur, objType._String)
                    .AccidentProvince = ValidateData(item.AccidentProvince, objType._String)
                    .AccidentZipcode = ValidateData(item.AccidentZipcode, objType._String)
                    .DriverName = ValidateData(item.DriverName, objType._String)
                    .DriverTel = ValidateData(item.DriverTel, objType._String)
                    .NoticeName = ValidateData(item.NoticeName, objType._String)
                    .NoticeTel = ValidateData(item.NoticeTel, objType._String)
                    .GarageType = ValidateData(item.GarageType, objType._Integer) 'int
                    .GarageCode = ValidateData(item.GarageCode, objType._String)
                    .GarageName = ValidateData(item.GarageName, objType._String)
                    .GaragePlace = ValidateData(item.GaragePlace, objType._String)
                    .GarageTumbon = ValidateData(item.GarageTumbon, objType._String)
                    .GarageAmphur = ValidateData(item.GarageAmphur, objType._String)
                    .GarageProvince = ValidateData(item.GarageProvince, objType._String)
                    .GarageZipcode = ValidateData(item.GarageZipcode, objType._String)
                    .CarRepairDate = ValidateData(item.CarRepairDate, objType._String)
                    .CarReceiveDate = ValidateData(item.CarReceiveDate, objType._String)
                    .ConsentFormNo = ValidateData(item.ConsentFormNo, objType._String)
                    .PartsDealerName = ValidateData(item.PartsDealerName, objType._String)
                    .PaymentDetails = ValidateData(item.PaymentDetails, objType._String)
                    .Amount1 = ValidateData(item.Amount1, objType._Double) 'float
                    .Amount2 = ValidateData(item.Amount2, objType._Double) 'float
                    .Amount3 = ValidateData(item.Amount3, objType._Double) 'float
                    .Remark = ValidateData(item.Remark, objType._String)

                    '.FileUpload = item.FileUpload
                    .GUID = _GUID
                    .SubmitDate = _submitdate
                    '.Status = item.Status
                    '.IsPost = item.IsPost
                    .ip = _ip


                End With




                Dim _item_result As New List(Of ClaimResultMessage)

                InitClaimData(_tblClaimData)

                Select Case _tblClaimData.ClaimStatus
                    Case "00"
                        _item_result = ValidateClaimData00(_tblClaimData)
                    Case "01"
                        _item_result = ValidateClaimData01(_tblClaimData)
                    Case "02"
                        _item_result = ValidateClaimData02(_tblClaimData)
                    Case "99"
                        _item_result = ValidateClaimData99(_tblClaimData)
                    Case "98"
                        _item_result = ValidateClaimData98(_tblClaimData)
                    Case Else
                        _item_result.Add(New ClaimResultMessage With {.ResultCode = "ClaimStatus", .ResultMessage = "Invalid ClaimStatus"})
                End Select


                Dim _r As New ClaimTransaction_Data_Result

                If _item_result.Count > 0 Then
                    _tblClaimData.Status = False

                    Dim _ClaimResultMessage As New List(Of ClaimResultMessage)
                    For Each i_rusult In _item_result
                        _tblClaimData.tblClaimTransaction_Results.Add(New tblClaimTransaction_Result With {.GUID = _tblClaimData.GUID _
                                                                                         , .ResultCode = i_rusult.ResultCode _
                                                                                         , .ResultMessage = i_rusult.ResultMessage _
                                                                                         , .ResultNo = _tblClaimData.RefNo _
                                                                                         , .SubmitDate = _submitdate})

                        _ClaimResultMessage.Add(New ClaimResultMessage With {.ResultCode = i_rusult.ResultCode, .ResultMessage = i_rusult.ResultMessage})
                    Next
                    _r.ResultMessage = _ClaimResultMessage


                Else
                    _tblClaimData.Status = True
                End If

                _r.ResultNo = _tblClaimData.RefNo
                _r.TRNo = _tblClaimData.TRNo
                _r.ResultStatus = _tblClaimData.Status
                _r.SubmitDate = _submitdate

                _ReturnResult.Add(_r)



                _ClaimTransaction_Data.Add(_tblClaimData)
            Next
            dc.tblClaimTransaction_Datas.InsertAllOnSubmit(_ClaimTransaction_Data)
            'dc.tblClaimTransaction_Results.InsertAllOnSubmit(_ClaimTransaction_Result)
            '======= Submit All =============
            dc.SubmitChanges()

        End Using

        Return _ReturnResult
    End Function

    <WebMethod()> _
    Public Function SendMotorClaim(ByVal _UWCode As String, ByVal _Password As String, ByVal _Data As List(Of ClaimTransaction_Data)) As List(Of ClaimTransaction_Data_Result)
        Dim _ReturnResult As New List(Of ClaimTransaction_Data_Result)

        Using dc As New DataClasses_MotorClaimDataExt()

            Dim UWAccess = (From c In dc.Runnings Where c.UWCode.Equals(_UWCode) And c.GUID.Equals(_Password)).FirstOrDefault()
            If UWAccess Is Nothing Then
                Dim _r As New ClaimTransaction_Data_Result
                With _r
                    .ResultStatus = False

                    Dim _ResultMessage As New List(Of ClaimResultMessage)
                    _ResultMessage.Add(New ClaimResultMessage With {.ResultCode = "ER001", .ResultMessage = "Invalid Underwriter Code or Password"})

                    .ResultMessage = _ResultMessage

                    .SubmitDate = Now
                    .TRNo = "-"
                End With

                _ReturnResult.Add(_r)

            Else

                If _Data.Count = 0 Then
                    Dim _r As New ClaimTransaction_Data_Result
                    With _r
                        .ResultStatus = False
                        Dim _ResultMessage As New List(Of ClaimResultMessage)
                        _ResultMessage.Add(New ClaimResultMessage With {.ResultCode = "ER002", .ResultMessage = "No Data"})


                        .ResultMessage = _ResultMessage
                        .SubmitDate = Now
                        .TRNo = "-"
                    End With
                    _ReturnResult.Add(_r)


                    Return _ReturnResult
                End If








                '===========================================
                Dim _ClaimTransaction_Data As New List(Of tblClaimTransaction_Data)
                'Dim _ClaimTransaction_Result As New List(Of tblClaimTransaction_Result)
                '1. Create TRNo.
                Dim _TRNo As String = ""
                dc.Running_GetByRunningCode(_UWCode, _TRNo)
                Dim _submitdate = Now


                Dim _ip = HttpContext.Current.Request.ServerVariables("REMOTE_ADDR").ToString()

                '2. Get Data
                For Each item In _Data
                    Dim _GUID = Guid.NewGuid().ToString()

                    '============== Get Date ================
                    Dim _tblClaimData As New tblClaimTransaction_Data()
                    With _tblClaimData

                        .TRNo = _TRNo
                        .ClaimStatus = item.ClaimStatus
                        .TempPolicy = item.TempPolicy
                        .RefNo = item.RefNo
                        .Version = ValidateData(item.Version, objType._Integer) 'int
                        .PolicyNo = item.PolicyNo
                        .PolicyYear = ValidateData(item.PolicyYear, objType._Integer) 'int
                        .ClaimNo = item.ClaimNo
                        .TransactionDate = item.TransactionDate
                        .Unwriter = _UWCode 'item.Unwriter
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
                        .ClaimType = ValidateData(item.ClaimType, objType._Integer) 'int
                        .ClaimResult = ValidateData(item.ClaimResult, objType._Integer) 'int
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
                        .GarageType = ValidateData(item.GarageType, objType._Integer) 'int
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
                        .Amount1 = ValidateData(item.Amount1, objType._Double) 'float
                        .Amount2 = ValidateData(item.Amount2, objType._Double) 'float
                        .Amount3 = ValidateData(item.Amount3, objType._Double) 'float
                        .Remark = item.Remark

                        '.FileUpload = item.FileUpload
                        .GUID = _GUID
                        .SubmitDate = _submitdate
                        '.Status = item.Status
                        '.IsPost = item.IsPost
                        .ip = _ip


                    End With




                    Dim _item_result As New List(Of ClaimResultMessage)

                    InitClaimData(_tblClaimData)

                    Select Case _tblClaimData.ClaimStatus
                        Case "00"
                            _item_result = ValidateClaimData00(_tblClaimData)
                        Case "01"
                            _item_result = ValidateClaimData01(_tblClaimData)
                        Case "02"
                            _item_result = ValidateClaimData02(_tblClaimData)
                        Case "99"
                            _item_result = ValidateClaimData99(_tblClaimData)
                        Case "98"
                            _item_result = ValidateClaimData98(_tblClaimData)
                    End Select



                    Dim _r As New ClaimTransaction_Data_Result

                    If _item_result.Count > 0 Then
                        _tblClaimData.Status = False

                        Dim _ClaimResultMessage As New List(Of ClaimResultMessage)
                        For Each i_rusult In _item_result
                            _tblClaimData.tblClaimTransaction_Results.Add(New tblClaimTransaction_Result With {.GUID = _tblClaimData.GUID _
                                                                                             , .ResultCode = i_rusult.ResultCode _
                                                                                             , .ResultMessage = i_rusult.ResultMessage _
                                                                                             , .ResultNo = _tblClaimData.RefNo _
                                                                                             , .SubmitDate = _submitdate})

                            _ClaimResultMessage.Add(New ClaimResultMessage With {.ResultCode = i_rusult.ResultCode, .ResultMessage = i_rusult.ResultMessage})
                        Next
                        _r.ResultMessage = _ClaimResultMessage


                    Else
                        _tblClaimData.Status = True
                    End If

                    _r.ResultNo = _tblClaimData.RefNo
                    _r.TRNo = _tblClaimData.TRNo
                    _r.ResultStatus = _tblClaimData.Status
                    _r.SubmitDate = _submitdate

                    _ReturnResult.Add(_r)



                    _ClaimTransaction_Data.Add(_tblClaimData)
                Next
                dc.tblClaimTransaction_Datas.InsertAllOnSubmit(_ClaimTransaction_Data)
                'dc.tblClaimTransaction_Results.InsertAllOnSubmit(_ClaimTransaction_Result)
                '======= Submit All =============
                dc.SubmitChanges()


            End If
        End Using

        Return _ReturnResult
    End Function

    Private Sub InitClaimData(ByRef _data As tblClaimTransaction_Data)
        If Not String.IsNullOrEmpty(_data.ClaimStatus) Then _data.ClaimStatus = Left(_data.ClaimStatus.Trim(), 2)
        'TempPolicy()'	varchar(100) 
        If Not String.IsNullOrEmpty(_data.TempPolicy) Then _data.TempPolicy = Left(_data.TempPolicy.Trim(), 100)
        'RefNo()'	varchar(100)
        If Not String.IsNullOrEmpty(_data.RefNo) Then _data.RefNo = Left(_data.RefNo.Trim(), 100)
        'PolicyNo()'	varchar(100)
        If Not String.IsNullOrEmpty(_data.PolicyNo) Then _data.PolicyNo = Left(_data.PolicyNo.Trim(), 100)
        'ClaimNo()'	varchar(100)
        If Not String.IsNullOrEmpty(_data.ClaimNo) Then _data.ClaimNo = Left(_data.ClaimNo.Trim(), 100)
        'TransactionDate()'	datetime 
        If Not String.IsNullOrEmpty(_data.TransactionDate) Then _data.TransactionDate = Left(_data.TransactionDate.Trim(), 10)
        'Unwriter()'	varchar(10)
        If Not String.IsNullOrEmpty(_data.Unwriter) Then _data.Unwriter = Left(_data.Unwriter.Trim(), 10)

        '================================================================================================================
        'InsuredName varchar(255)
        If Not String.IsNullOrEmpty(_data.InsuredName) Then _data.InsuredName = Left(_data.InsuredName.Trim(), 255)
        'EffectiveDate varchar(10) 
        If Not String.IsNullOrEmpty(_data.EffectiveDate) Then _data.EffectiveDate = Left(_data.EffectiveDate.Trim(), 10)
        'ExpiryDate varchar(10) 
        If Not String.IsNullOrEmpty(_data.ExpiryDate) Then _data.ExpiryDate = Left(_data.ExpiryDate.Trim(), 10)
        'Beneficiary varchar(255) 
        If Not String.IsNullOrEmpty(_data.Beneficiary) Then _data.Beneficiary = Left(_data.Beneficiary.Trim(), 255)
        'CarBrand varchar(50) 
        If Not String.IsNullOrEmpty(_data.CarBrand) Then _data.CarBrand = Left(_data.CarBrand.Trim(), 50)
        'CarModel varchar(100) 
        If Not String.IsNullOrEmpty(_data.CarModel) Then _data.CarModel = Left(_data.CarModel.Trim(), 100)
        'CarLicense varchar(50) 
        If Not String.IsNullOrEmpty(_data.CarLicense) Then _data.CarLicense = Left(_data.CarLicense.Trim(), 50)
        'CarYear varchar(50) 
        If Not String.IsNullOrEmpty(_data.CarYear) Then _data.CarYear = Left(_data.CarYear.Trim(), 50)
        'ChassisNo varchar(50) 
        If Not String.IsNullOrEmpty(_data.ChassisNo) Then _data.ChassisNo = Left(_data.ChassisNo.Trim(), 50)
        'ShowRoomName varchar(100) 
        If Not String.IsNullOrEmpty(_data.ShowRoomName) Then _data.ShowRoomName = Left(_data.ShowRoomName.Trim(), 100)
        'ShowRoomCode varchar(10) 
        If Not String.IsNullOrEmpty(_data.ShowRoomCode) Then _data.ShowRoomCode = Left(_data.ShowRoomCode.Trim(), 10)
        'ClaimNoticeDate  varchar(10) 
        If Not String.IsNullOrEmpty(_data.ClaimNoticeDate) Then _data.ClaimNoticeDate = Left(_data.ClaimNoticeDate.Trim(), 10)
        'ClaimNoticeDate  varchar(10) 
        If Not String.IsNullOrEmpty(_data.ClaimNoticeTime) Then _data.ClaimNoticeTime = Left(_data.ClaimNoticeTime.Trim(), 10)
        'ClaimDetails varchar(255)
        If Not String.IsNullOrEmpty(_data.ClaimDetails) Then _data.ClaimDetails = Left(_data.ClaimDetails.Trim(), 255)
        'CallCenter varchar(50) 
        If Not String.IsNullOrEmpty(_data.CallCenter) Then _data.CallCenter = Left(_data.CallCenter.Trim(), 50)
        'AccidentDate varchar(10)
        If Not String.IsNullOrEmpty(_data.AccidentDate) Then _data.AccidentDate = Left(_data.AccidentDate.Trim(), 10)
        'AccidentTime varchar(10) 
        If Not String.IsNullOrEmpty(_data.AccidentTime) Then _data.AccidentTime = Left(_data.AccidentTime.Trim(), 10)
        'AccidentPlace varchar(255)
        If Not String.IsNullOrEmpty(_data.AccidentPlace) Then _data.AccidentPlace = Left(_data.AccidentPlace.Trim(), 255)
        'AccidentTumbon varchar(255) 
        If Not String.IsNullOrEmpty(_data.AccidentTumbon) Then _data.AccidentTumbon = Left(_data.AccidentTumbon.Trim(), 255)
        'AccidentAmphur varchar(255) 
        If Not String.IsNullOrEmpty(_data.AccidentAmphur) Then _data.AccidentAmphur = Left(_data.AccidentAmphur.Trim(), 255)
        'AccidentProvince varchar(255) 
        If Not String.IsNullOrEmpty(_data.AccidentProvince) Then _data.AccidentProvince = Left(_data.AccidentProvince.Trim(), 255)
        'AccidentZipcode varchar(5) 
        If Not String.IsNullOrEmpty(_data.AccidentZipcode) Then _data.AccidentZipcode = Left(_data.AccidentZipcode.Trim(), 5)
        'DriverName varchar(255) 
        If Not String.IsNullOrEmpty(_data.DriverName) Then _data.DriverName = Left(_data.DriverName.Trim(), 255)
        'DriverTel varchar(100) 
        If Not String.IsNullOrEmpty(_data.DriverTel) Then _data.DriverTel = Left(_data.DriverTel.Trim(), 100)
        'NoticeName varchar(255) 
        If Not String.IsNullOrEmpty(_data.NoticeName) Then _data.NoticeName = Left(_data.NoticeName.Trim(), 255)
        'NoticeTel varchar(100) 
        If Not String.IsNullOrEmpty(_data.NoticeTel) Then _data.NoticeTel = Left(_data.NoticeTel.Trim(), 100)
        'GarageCode varchar(100) 
        If Not String.IsNullOrEmpty(_data.GarageCode) Then _data.GarageCode = Left(_data.GarageCode.Trim(), 10)
        'GarageName varchar(100) 
        If Not String.IsNullOrEmpty(_data.GarageName) Then _data.GarageName = Left(_data.GarageName.Trim(), 100)
        'GaragePlace varchar(255) 
        If Not String.IsNullOrEmpty(_data.GaragePlace) Then _data.GaragePlace = Left(_data.GaragePlace.Trim(), 255)
        'GarageTumbon varchar(255) 
        If Not String.IsNullOrEmpty(_data.GarageTumbon) Then _data.GarageTumbon = Left(_data.GarageTumbon.Trim(), 255)
        'GarageAmphur varchar(255) 
        If Not String.IsNullOrEmpty(_data.GarageAmphur) Then _data.GarageAmphur = Left(_data.GarageAmphur.Trim(), 255)
        'GarageProvince varchar(255) 
        If Not String.IsNullOrEmpty(_data.GarageProvince) Then _data.GarageProvince = Left(_data.GarageProvince.Trim(), 255)
        'GarageZipcode varchar(5) 
        If Not String.IsNullOrEmpty(_data.GarageZipcode) Then _data.GarageZipcode = Left(_data.GarageZipcode.Trim(), 5)
        'CarRepairDate DateTime 
        If Not String.IsNullOrEmpty(_data.CarRepairDate) Then _data.CarRepairDate = Left(_data.CarRepairDate.Trim(), 10)
        'CarReceiveDate DateTime 
        If Not String.IsNullOrEmpty(_data.CarReceiveDate) Then _data.CarReceiveDate = Left(_data.CarReceiveDate.Trim(), 10)
        'ConsentFormNo()'	varchar(50) 
        If Not String.IsNullOrEmpty(_data.ConsentFormNo) Then _data.ConsentFormNo = Left(_data.ConsentFormNo.Trim(), 50)
        'PartsDealerName() '	varchar(255) 
        If Not String.IsNullOrEmpty(_data.PartsDealerName) Then _data.PartsDealerName = Left(_data.PartsDealerName.Trim(), 255)
        'PaymentDetails()  '	varchar(max) 
        If Not String.IsNullOrEmpty(_data.PaymentDetails) Then _data.PaymentDetails = Left(_data.PaymentDetails.Trim(), 255)
        'Remark()  '	varchar(255) 
        If Not String.IsNullOrEmpty(_data.Remark) Then _data.Remark = Left(_data.Remark.Trim(), 255)


    End Sub

    Public Function ValidateClaimData00(ByRef _data As tblClaimTransaction_Data) As List(Of ClaimResultMessage)
        Dim _Result As New List(Of ClaimResultMessage)

        'ClaimStatus()'	varchar(2)
        Dim _ClaimStatus As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.ClaimStatus) Then
            _ClaimStatus.ResultCode = "ClaimStatus"
            _ClaimStatus.ResultMessage = "No data"
            _Result.Add(_ClaimStatus)
        End If

        'TempPolicy()'	varchar(100)
        Dim _TempPolicy As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.TempPolicy) Then
            '_TempPolicy.ResultCode = "TempPolicy"
            '_TempPolicy.ResultMessage = "No data"
            '_Result.Add(_TempPolicy)
            _data.TempPolicy = "-"
        End If

        'RefNo()'	varchar(100)
        Dim _RefNo As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.RefNo) Then
            _RefNo.ResultCode = "RefNo"
            _RefNo.ResultMessage = "No data"
            _Result.Add(_RefNo)

        End If

        'Version()'	int
        Dim _Version As New ClaimResultMessage
        If _data.Version Is Nothing Then
            _Version.ResultCode = "Version"
            _Version.ResultMessage = "Invalid Number"
            _Result.Add(_Version)
        End If

        'PolicyNo()'	varchar(100)
        Dim _PolicyNo As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.PolicyNo) Then
            _PolicyNo.ResultCode = "PolicyNo"
            _PolicyNo.ResultMessage = "No data"
            _Result.Add(_PolicyNo)

        End If

        'PolicyYear()'	int
        Dim _PolicyYear As New ClaimResultMessage
        If _data.PolicyYear Is Nothing Then
            _PolicyYear.ResultCode = "PolicyYear"
            _PolicyYear.ResultMessage = "No data"
            _Result.Add(_PolicyYear)
        End If

        'ClaimNo()'	varchar(100)
        Dim _ClaimNo As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.ClaimNo) Then
            _ClaimNo.ResultCode = "ClaimNo"
            _ClaimNo.ResultMessage = "No data"
            _Result.Add(_ClaimNo)

        End If

        'TransactionDate()'	datetime
        Dim _TransactionDate As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.TransactionDate) Then
            _TransactionDate.ResultCode = "TransactionDate"
            _TransactionDate.ResultMessage = "Invalid Date"
            _Result.Add(_TransactionDate)

        End If

        'Unwriter()'	varchar(10)
        Dim _Unwriter As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.Unwriter) Or String.IsNullOrEmpty(_data.TRNo) Then
            _Unwriter.ResultCode = "Unwriter"
            _Unwriter.ResultMessage = "Invalid Unwriter Code"
            _Result.Add(_Unwriter)
        Else
            Using dc As New DataClasses_MotorClaimDataExt()
                Dim _data_uw = _data.Unwriter
                Dim _uw = (From c In dc.Runnings Where c.UWCode.Equals(_data_uw)).FirstOrDefault()
                If _uw Is Nothing Then
                    _Unwriter.ResultCode = "Unwriter"
                    _Unwriter.ResultMessage = "Invalid Unwriter Code"
                    _Result.Add(_Unwriter)
                End If
            End Using
        End If

        '================================================================================================================

        'InsuredName varchar(255)
        Dim _InsuredName As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.InsuredName) Then
            _InsuredName.ResultCode = "InsuredName"
            _InsuredName.ResultMessage = "No data"
            _Result.Add(_InsuredName)

        End If

        'EffectiveDate varchar(10)
        Dim _EffectiveDate As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.EffectiveDate) Then
            _EffectiveDate.ResultCode = "EffectiveDate"
            _EffectiveDate.ResultMessage = "Invalid Date"
            _Result.Add(_EffectiveDate)


        End If
        'ExpiryDate varchar(10)
        Dim _ExpiryDate As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.ExpiryDate) Then
            _ExpiryDate.ResultCode = "ExpiryDate"
            _ExpiryDate.ResultMessage = "Invalid Date"
            _Result.Add(_ExpiryDate)

        End If
        'Beneficiary varchar(255)
        Dim _Beneficiary As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.Beneficiary) Then
            '_Beneficiary.ResultCode = "Beneficiary"
            '_Beneficiary.ResultMessage = "No data"
            '_Result.Add(_Beneficiary)
            _data.Beneficiary = "-"
        End If

        'CarBrand varchar(50)
        Dim _CarBrand As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.CarBrand) Then
            _CarBrand.ResultCode = "CarBrand"
            _CarBrand.ResultMessage = "No data"
            _Result.Add(_CarBrand)

        End If
        'CarModel varchar(100)
        Dim _CarModel As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.CarModel) Then
            _CarModel.ResultCode = "CarModel"
            _CarModel.ResultMessage = "No data"
            _Result.Add(_CarModel)

        End If
        'CarLicense varchar(50)
        Dim _CarLicense As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.CarLicense) Then
            '_CarLicense.ResultCode = "CarLicense"
            '_CarLicense.ResultMessage = "No data"
            '_Result.Add(_CarLicense)
            _data.CarLicense = "-"
        End If
        'CarYear varchar(50)
        Dim _CarYear As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.CarYear) Then
            _CarYear.ResultCode = "CarYear"
            _CarYear.ResultMessage = "No data"
            _Result.Add(_CarYear)

        End If
        'ChassisNo varchar(50)
        Dim _ChassisNo As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.ChassisNo) Then
            _ChassisNo.ResultCode = "ChassisNo"
            _ChassisNo.ResultMessage = "No data"
            _Result.Add(_ChassisNo)

        End If

        'ShowRoomName varchar(100)
        Dim _ShowRoomName As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.ShowRoomName) Then
            _ShowRoomName.ResultCode = "ShowRoomName"
            _ShowRoomName.ResultMessage = "No data"
            _Result.Add(_ShowRoomName)

        End If
        'ShowRoomCode varchar(10)
        Dim _ShowRoomCode As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.ShowRoomCode) Then
            _ShowRoomCode.ResultCode = "ShowRoomCode"
            _ShowRoomCode.ResultMessage = "No data"
            _Result.Add(_ShowRoomCode)
        Else
            If _data.ShowRoomCode <> "-" Then
                Using dc As New DataClasses_MotorClaimDataExt()
                    Dim _data_showroom = _data.ShowRoomCode
                    Dim _showroom = (From c In dc.tblShowRooms Where c.ShowroomID.Equals(_data_showroom) Or c.ShowRoomCode.Equals(_data_showroom)).FirstOrDefault()
                    If _showroom Is Nothing Then
                        _ShowRoomCode.ResultCode = "ShowRoomCode"
                        _ShowRoomCode.ResultMessage = "Invalid Showroom Code"
                        _Result.Add(_ShowRoomCode)
                    Else
                        _data.ShowRoomCode = _showroom.ShowRoomCode
                    End If
                End Using
            End If
        End If


        'ClaimNoticeDate  varchar(10)
        Dim _ClaimNoticeDate As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.ClaimNoticeDate) Then
            _ClaimNoticeDate.ResultCode = "ClaimNoticeDate"
            _ClaimNoticeDate.ResultMessage = "Invalid Date"
            _Result.Add(_ClaimNoticeDate)

        End If
        'ClaimNoticeDate  varchar(10)
        Dim _ClaimNoticeTime As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.ClaimNoticeTime) Then
            _ClaimNoticeTime.ResultCode = "ClaimNoticeTime"
            _ClaimNoticeTime.ResultMessage = "Invalid Time"
            _Result.Add(_ClaimNoticeTime)

        End If

        'ClaimDetails varchar(max)
        Dim _ClaimDetails As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.ClaimDetails) Then
            _ClaimDetails.ResultCode = "ClaimDetails"
            _ClaimDetails.ResultMessage = "No data"
            _Result.Add(_ClaimDetails)

        End If
        'ClaimType Int
        Dim _ClaimType As New ClaimResultMessage
        If _data.ClaimType Is Nothing Then
            _ClaimType.ResultCode = "ClaimType"
            _ClaimType.ResultMessage = "Invalid Number"
            _Result.Add(_ClaimType)
        End If
        'ClaimResult Int
        Dim _ClaimResult As New ClaimResultMessage
        If _data.ClaimResult Is Nothing Then
            _ClaimResult.ResultCode = "ClaimResult"
            _ClaimResult.ResultMessage = "Invalid Number"
            _Result.Add(_ClaimResult)
        End If
        'ClaimDamageDetails varchar(max)
        Dim _ClaimDamageDetails As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.ClaimDamageDetails) Then
            '_ClaimDamageDetails.ResultCode = "ClaimDamageDetails"
            '_ClaimDamageDetails.ResultMessage = "No data"
            '_Result.Add(_ClaimDamageDetails)
            _data.ClaimDamageDetails = "-"
        End If

        'CallCenter varchar(10)
        Dim _CallCenter As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.CallCenter) Then
            '_CallCenter.ResultCode = "CallCenter"
            '_CallCenter.ResultMessage = "No data"
            '_Result.Add(_CallCenter)
            _data.CallCenter = "-"
        End If
        'AccidentDate varchar(10)
        Dim _AccidentDate As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.AccidentDate) Then
            _AccidentDate.ResultCode = "AccidentDate"
            _AccidentDate.ResultMessage = "Invalid Date"
            _Result.Add(_AccidentDate)

        End If
        'AccidentTime varchar(10)
        Dim _AccidentTime As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.AccidentTime) Then
            _AccidentTime.ResultCode = "AccidentTime"
            _AccidentTime.ResultMessage = "Invalid Time"
            _Result.Add(_AccidentTime)

        End If

        'AccidentPlace varchar(255)
        Dim _AccidentPlace As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.AccidentPlace) Then
            _AccidentPlace.ResultCode = "AccidentPlace"
            _AccidentPlace.ResultMessage = "No data"
            _Result.Add(_AccidentPlace)

        End If
        'AccidentTumbon varchar(255)
        Dim _AccidentTumbon As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.AccidentTumbon) Then
            '_AccidentTumbon.ResultCode = "AccidentTumbon"
            '_AccidentTumbon.ResultMessage = "No data"
            '_Result.Add(_AccidentTumbon)
            _data.AccidentTumbon = "-"
        End If
        'AccidentAmphur varchar(255)
        Dim _AccidentAmphur As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.AccidentAmphur) Then
            '_AccidentAmphur.ResultCode = "AccidentAmphur"
            '_AccidentAmphur.ResultMessage = "No data"
            '_Result.Add(_AccidentAmphur)
            _data.AccidentAmphur = "-"
        End If
        'AccidentProvince varchar(255)
        Dim _AccidentProvince As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.AccidentProvince) Then
            '_AccidentProvince.ResultCode = "AccidentProvince"
            '_AccidentProvince.ResultMessage = "No data"
            '_Result.Add(_AccidentProvince)
            _data.AccidentProvince = "-"
        End If
        'AccidentZipcode varchar(5)
        Dim _AccidentZipcode As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.AccidentZipcode) Then
            '_AccidentZipcode.ResultCode = "AccidentZipcode"
            '_AccidentZipcode.ResultMessage = "No data"
            '_Result.Add(_AccidentZipcode)
            _data.AccidentZipcode = "-"
        End If

        'DriverName varchar(255)
        Dim _DriverName As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.DriverName) Then
            '_DriverName.ResultCode = "DriverName"
            '_DriverName.ResultMessage = "No data"
            '_Result.Add(_DriverName)
            _data.DriverName = "-"
        End If
        'DriverTel varchar(100)
        Dim _DriverTel As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.DriverTel) Then
            '_DriverTel.ResultCode = "DriverTel"
            '_DriverTel.ResultMessage = "No data"
            '_Result.Add(_DriverTel)
            _data.DriverTel = "-"
        End If
        'NoticeName varchar(255)
        Dim _NoticeName As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.NoticeName) Then
            '_NoticeName.ResultCode = "NoticeName"
            '_NoticeName.ResultMessage = "No data"
            '_Result.Add(_NoticeName)
            _data.NoticeName = "-"
        End If
        'NoticeTel varchar(100)
        Dim _NoticeTel As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.NoticeTel) Then
            '_NoticeTel.ResultCode = "NoticeTel"
            '_NoticeTel.ResultMessage = "No data"
            '_Result.Add(_NoticeTel)
            _data.NoticeTel = "-"
        End If

        'GarageType Int
        Dim _GarageType As New ClaimResultMessage
        If _data.GarageType Is Nothing Then
            _GarageType.ResultCode = "GarageType"
            _GarageType.ResultMessage = "Invalid Number"
            _Result.Add(_GarageType)
        Else
            '=============== check garage ==================
            If _data.GarageType = 1 Or _data.GarageType = 2 Then
                ' ''GarageCode varchar(10)
                Dim _GarageCode As New ClaimResultMessage
                If String.IsNullOrEmpty(_data.GarageCode) Then
                    _GarageCode.ResultCode = "GarageCode"
                    _GarageCode.ResultMessage = "No data"
                    _Result.Add(_GarageCode)
                End If

            ElseIf _data.GarageType = 3 Or _data.GarageType = 4 Then

                'GarageName varchar(100)
                Dim _GarageName As New ClaimResultMessage
                If String.IsNullOrEmpty(_data.GarageName) Then
                    '_GarageName.ResultCode = "GarageName"
                    '_GarageName.ResultMessage = "No data"
                    '_Result.Add(_GarageName)
                    _data.GarageName = "-"
                End If
                'GaragePlace varchar(255)
                Dim _GaragePlace As New ClaimResultMessage
                If String.IsNullOrEmpty(_data.GaragePlace) Then
                    '_GaragePlace.ResultCode = "GaragePlace"
                    '_GaragePlace.ResultMessage = "No data"
                    '_Result.Add(_GaragePlace)
                    _data.GaragePlace = "-"
                End If
                'GarageTumbon varchar(255)
                Dim _GarageTumbon As New ClaimResultMessage
                If String.IsNullOrEmpty(_data.GarageTumbon) Then
                    '_GarageTumbon.ResultCode = "GarageTumbon"
                    '_GarageTumbon.ResultMessage = "No data"
                    '_Result.Add(_GarageTumbon)
                    _data.GarageTumbon = "-"
                End If
                'GarageAmphur varchar(255)
                Dim _GarageAmphur As New ClaimResultMessage
                If String.IsNullOrEmpty(_data.GarageAmphur) Then
                    '_GarageAmphur.ResultCode = "GarageAmphur"
                    '_GarageAmphur.ResultMessage = "No data"
                    '_Result.Add(_GarageAmphur)
                    _data.GarageAmphur = "-"
                End If
                'GarageProvince varchar(255)
                Dim _GarageProvince As New ClaimResultMessage
                If String.IsNullOrEmpty(_data.GarageProvince) Then
                    '_GarageProvince.ResultCode = "GarageProvince"
                    '_GarageProvince.ResultMessage = "No data"
                    '_Result.Add(_GarageProvince)
                    _data.GarageProvince = "-"
                End If
                'GarageZipcode varchar(5)
                Dim _GarageZipcode As New ClaimResultMessage
                If String.IsNullOrEmpty(_data.GarageZipcode) Then
                    '_GarageZipcode.ResultCode = "GarageZipcode"
                    '_GarageZipcode.ResultMessage = "No data"
                    '_Result.Add(_GarageZipcode)
                    _data.GarageZipcode = "-"
                End If

            End If
        End If
        'CarRepairDate DateTime
        Dim _CarRepairDate As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.CarRepairDate) Then
            '_CarRepairDate.ResultCode = "CarRepairDate"
            '_CarRepairDate.ResultMessage = "Invalid Date"
            '_Result.Add(_CarRepairDate)
            _data.CarRepairDate = "-"
        End If
        'CarReceiveDate DateTime
        Dim _CarReceiveDate As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.CarReceiveDate) Then
            '_CarReceiveDate.ResultCode = "CarReceiveDate"
            '_CarReceiveDate.ResultMessage = "Invalid Date"
            '_Result.Add(_CarReceiveDate)
            _data.CarReceiveDate = "-"
        End If

        'ConsentFormNo()'	varchar(50)
        Dim _ConsentFormNo As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.ConsentFormNo) Then
            _data.ConsentFormNo = "-"
        End If

        'PartsDealerName() '	varchar(255)
        Dim _PartsDealerName As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.PartsDealerName) Then
            '_PartsDealerName.ResultCode = "PartsDealerName"
            '_PartsDealerName.ResultMessage = "No data"
            '_Result.Add(_PartsDealerName)
            _data.PartsDealerName = "-"
        End If
        'PaymentDetails()  '	varchar(max)
        Dim _PaymentDetails As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.PaymentDetails) Then
            '_PaymentDetails.ResultCode = "PaymentDetails"
            '_PaymentDetails.ResultMessage = "No data"
            '_Result.Add(_PaymentDetails)
            _data.PaymentDetails = "-"
        End If
        'Amount1 float
        Dim _Amount1 As New ClaimResultMessage
        If _data.Amount1 Is Nothing Then
            _Amount1.ResultCode = "Amount1"
            _Amount1.ResultMessage = "Invalid Number"
            _Result.Add(_Amount1)
        End If
        'Amount2 float
        Dim _Amount2 As New ClaimResultMessage
        If _data.Amount2 Is Nothing Then
            _Amount2.ResultCode = "Amount2"
            _Amount2.ResultMessage = "Invalid Number"
            _Result.Add(_Amount2)
        End If
        'Amount3 float
        Dim _Amount3 As New ClaimResultMessage
        If _data.Amount3 Is Nothing Then
            _Amount3.ResultCode = "Amount3"
            _Amount3.ResultMessage = "Invalid Number"
            _Result.Add(_Amount3)
        End If




        Return _Result.ToList()
    End Function

    Public Function ValidateClaimData01(ByRef _data As tblClaimTransaction_Data) As List(Of ClaimResultMessage)
        Dim _Result As New List(Of ClaimResultMessage)

        'ClaimStatus()'	varchar(2)
        Dim _ClaimStatus As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.ClaimStatus) Then
            _ClaimStatus.ResultCode = "ClaimStatus"
            _ClaimStatus.ResultMessage = "No data"
            _Result.Add(_ClaimStatus)
        End If

        'TempPolicy()'	varchar(100)
        Dim _TempPolicy As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.TempPolicy) Then
            _TempPolicy.ResultCode = "TempPolicy"
            _TempPolicy.ResultMessage = "No data"
            _Result.Add(_TempPolicy)

        End If

        'RefNo()'	varchar(100)
        Dim _RefNo As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.RefNo) Then
            _RefNo.ResultCode = "RefNo"
            _RefNo.ResultMessage = "No data"
            _Result.Add(_RefNo)

        End If

        'Version()'	int
        Dim _Version As New ClaimResultMessage
        If _data.Version Is Nothing Then
            _Version.ResultCode = "Version"
            _Version.ResultMessage = "Invalid Number"
            _Result.Add(_Version)
        End If

        'PolicyNo()'	varchar(100)
        Dim _PolicyNo As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.PolicyNo) Then
            _PolicyNo.ResultCode = "PolicyNo"
            _PolicyNo.ResultMessage = "No data"
            _Result.Add(_PolicyNo)

        End If

        'PolicyYear()'	int
        Dim _PolicyYear As New ClaimResultMessage
        If _data.PolicyYear Is Nothing Then
            _PolicyYear.ResultCode = "PolicyYear"
            _PolicyYear.ResultMessage = "No data"
            _Result.Add(_PolicyYear)
        End If

        'ClaimNo()'	varchar(100)
        Dim _ClaimNo As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.ClaimNo) Then
            _ClaimNo.ResultCode = "ClaimNo"
            _ClaimNo.ResultMessage = "No data"
            _Result.Add(_ClaimNo)

        End If

        'TransactionDate()'	datetime
        Dim _TransactionDate As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.TransactionDate) Then
            _TransactionDate.ResultCode = "TransactionDate"
            _TransactionDate.ResultMessage = "Invalid Date"
            _Result.Add(_TransactionDate)

        End If

        'Unwriter()'	varchar(10)
        Dim _Unwriter As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.Unwriter) Or String.IsNullOrEmpty(_data.TRNo) Then
            _Unwriter.ResultCode = "Unwriter"
            _Unwriter.ResultMessage = "Invalid Unwriter Code"
            _Result.Add(_Unwriter)

        Else
            Using dc As New DataClasses_MotorClaimDataExt()
                Dim _data_uw = _data.Unwriter
                Dim _uw = (From c In dc.Runnings Where c.UWCode.Equals(_data_uw)).FirstOrDefault()
                If _uw Is Nothing Then
                    _Unwriter.ResultCode = "Unwriter"
                    _Unwriter.ResultMessage = "Invalid Unwriter Code"
                    _Result.Add(_Unwriter)
                End If
            End Using
        End If

        '================================================================================================================


        'Amount1 float
        Dim _Amount1 As New ClaimResultMessage
        If _data.Amount1 Is Nothing Then
            _Amount1.ResultCode = "Amount1"
            _Amount1.ResultMessage = "Invalid Number"
            _Result.Add(_Amount1)
        End If
        'Amount2 float
        Dim _Amount2 As New ClaimResultMessage
        If _data.Amount2 Is Nothing Then
            _Amount2.ResultCode = "Amount2"
            _Amount2.ResultMessage = "Invalid Number"
            _Result.Add(_Amount2)
        End If
        'Amount3 float
        Dim _Amount3 As New ClaimResultMessage
        If _data.Amount3 Is Nothing Then
            _Amount3.ResultCode = "Amount3"
            _Amount3.ResultMessage = "Invalid Number"
            _Result.Add(_Amount3)
        End If




        Return _Result.ToList()
    End Function

    Public Function ValidateClaimData02(ByRef _data As tblClaimTransaction_Data) As List(Of ClaimResultMessage)
        Dim _Result As New List(Of ClaimResultMessage)

        'ClaimStatus()'	varchar(2)
        Dim _ClaimStatus As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.ClaimStatus) Then
            _ClaimStatus.ResultCode = "ClaimStatus"
            _ClaimStatus.ResultMessage = "No data"
            _Result.Add(_ClaimStatus)
        End If

        'TempPolicy()'	varchar(100)
        Dim _TempPolicy As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.TempPolicy) Then
            _TempPolicy.ResultCode = "TempPolicy"
            _TempPolicy.ResultMessage = "No data"
            _Result.Add(_TempPolicy)

        End If

        'RefNo()'	varchar(100)
        Dim _RefNo As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.RefNo) Then
            _RefNo.ResultCode = "RefNo"
            _RefNo.ResultMessage = "No data"
            _Result.Add(_RefNo)

        End If

        'Version()'	int
        Dim _Version As New ClaimResultMessage
        If _data.Version Is Nothing Then
            _Version.ResultCode = "Version"
            _Version.ResultMessage = "Invalid Number"
            _Result.Add(_Version)
        End If

        'PolicyNo()'	varchar(100)
        Dim _PolicyNo As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.PolicyNo) Then
            _PolicyNo.ResultCode = "PolicyNo"
            _PolicyNo.ResultMessage = "No data"
            _Result.Add(_PolicyNo)

        End If

        'PolicyYear()'	int
        Dim _PolicyYear As New ClaimResultMessage
        If _data.PolicyYear Is Nothing Then
            _PolicyYear.ResultCode = "PolicyYear"
            _PolicyYear.ResultMessage = "No data"
            _Result.Add(_PolicyYear)
        End If

        'ClaimNo()'	varchar(100)
        Dim _ClaimNo As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.ClaimNo) Then
            _ClaimNo.ResultCode = "ClaimNo"
            _ClaimNo.ResultMessage = "No data"
            _Result.Add(_ClaimNo)

        End If

        'TransactionDate()'	datetime
        Dim _TransactionDate As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.TransactionDate) Then
            _TransactionDate.ResultCode = "TransactionDate"
            _TransactionDate.ResultMessage = "Invalid Date"
            _Result.Add(_TransactionDate)

        End If

        'Unwriter()'	varchar(10)
        Dim _Unwriter As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.Unwriter) Or String.IsNullOrEmpty(_data.TRNo) Then
            _Unwriter.ResultCode = "Unwriter"
            _Unwriter.ResultMessage = "Invalid Unwriter Code"
            _Result.Add(_Unwriter)

        Else
            Using dc As New DataClasses_MotorClaimDataExt()
                Dim _data_uw = _data.Unwriter
                Dim _uw = (From c In dc.Runnings Where c.UWCode.Equals(_data_uw)).FirstOrDefault()
                If _uw Is Nothing Then
                    _Unwriter.ResultCode = "Unwriter"
                    _Unwriter.ResultMessage = "Invalid Unwriter Code"
                    _Result.Add(_Unwriter)
                End If
            End Using
        End If

        '================================================================================================================

        'GarageType Int
        Dim _GarageType As New ClaimResultMessage
        If _data.GarageType Is Nothing Then
            _GarageType.ResultCode = "GarageType"
            _GarageType.ResultMessage = "Invalid Number"
            _Result.Add(_GarageType)
        Else
            '=============== check garage ==================
            If _data.GarageType = 1 Or _data.GarageType = 2 Then
                ' ''GarageCode varchar(10)
                Dim _GarageCode As New ClaimResultMessage
                If String.IsNullOrEmpty(_data.GarageCode) Then
                    _GarageCode.ResultCode = "GarageCode"
                    _GarageCode.ResultMessage = "No data"
                    _Result.Add(_GarageCode)
                End If

            ElseIf _data.GarageType = 3 Or _data.GarageType = 4 Then

                'GarageName varchar(100)
                Dim _GarageName As New ClaimResultMessage
                If String.IsNullOrEmpty(_data.GarageName) Then
                    _GarageName.ResultCode = "GarageName"
                    _GarageName.ResultMessage = "No data"
                    _Result.Add(_GarageName)

                End If
                'GaragePlace varchar(255)
                Dim _GaragePlace As New ClaimResultMessage
                If String.IsNullOrEmpty(_data.GaragePlace) Then
                    _GaragePlace.ResultCode = "GaragePlace"
                    _GaragePlace.ResultMessage = "No data"
                    _Result.Add(_GaragePlace)

                End If
                'GarageTumbon varchar(255)
                Dim _GarageTumbon As New ClaimResultMessage
                If String.IsNullOrEmpty(_data.GarageTumbon) Then
                    _GarageTumbon.ResultCode = "GarageTumbon"
                    _GarageTumbon.ResultMessage = "No data"
                    _Result.Add(_GarageTumbon)

                End If
                'GarageAmphur varchar(255)
                Dim _GarageAmphur As New ClaimResultMessage
                If String.IsNullOrEmpty(_data.GarageAmphur) Then
                    _GarageAmphur.ResultCode = "GarageAmphur"
                    _GarageAmphur.ResultMessage = "No data"
                    _Result.Add(_GarageAmphur)

                End If
                'GarageProvince varchar(255)
                Dim _GarageProvince As New ClaimResultMessage
                If String.IsNullOrEmpty(_data.GarageProvince) Then
                    _GarageProvince.ResultCode = "GarageProvince"
                    _GarageProvince.ResultMessage = "No data"
                    _Result.Add(_GarageProvince)

                End If
                'GarageZipcode varchar(5)
                Dim _GarageZipcode As New ClaimResultMessage
                If String.IsNullOrEmpty(_data.GarageZipcode) Then
                    _GarageZipcode.ResultCode = "GarageZipcode"
                    _GarageZipcode.ResultMessage = "No data"
                    _Result.Add(_GarageZipcode)

                End If

            End If
        End If
        'CarRepairDate DateTime
        Dim _CarRepairDate As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.CarRepairDate) Then
            _CarRepairDate.ResultCode = "CarRepairDate"
            _CarRepairDate.ResultMessage = "Invalid Date"
            _Result.Add(_CarRepairDate)

        End If
        'CarReceiveDate DateTime
        Dim _CarReceiveDate As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.CarReceiveDate) Then
            _CarReceiveDate.ResultCode = "CarReceiveDate"
            _CarReceiveDate.ResultMessage = "Invalid Date"
            _Result.Add(_CarReceiveDate)

        End If
        'PartsDealerName() '	varchar(255)
        Dim _PartsDealerName As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.PartsDealerName) Then
            _PartsDealerName.ResultCode = "PartsDealerName"
            _PartsDealerName.ResultMessage = "No data"
            _Result.Add(_PartsDealerName)
        End If
        'PaymentDetails()  '	varchar(max)
        Dim _PaymentDetails As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.PaymentDetails) Then
            _PaymentDetails.ResultCode = "PaymentDetails"
            _PaymentDetails.ResultMessage = "No data"
            _Result.Add(_PaymentDetails)
        End If


        'Amount1 float
        Dim _Amount1 As New ClaimResultMessage
        If _data.Amount1 Is Nothing Then
            _Amount1.ResultCode = "Amount1"
            _Amount1.ResultMessage = "Invalid Number"
            _Result.Add(_Amount1)
        End If
        'Amount2 float
        Dim _Amount2 As New ClaimResultMessage
        If _data.Amount2 Is Nothing Then
            _Amount2.ResultCode = "Amount2"
            _Amount2.ResultMessage = "Invalid Number"
            _Result.Add(_Amount2)
        End If
        'Amount3 float
        Dim _Amount3 As New ClaimResultMessage
        If _data.Amount3 Is Nothing Then
            _Amount3.ResultCode = "Amount3"
            _Amount3.ResultMessage = "Invalid Number"
            _Result.Add(_Amount3)
        End If




        Return _Result.ToList()
    End Function

    Public Function ValidateClaimData98(ByRef _data As tblClaimTransaction_Data) As List(Of ClaimResultMessage)
        Dim _Result As New List(Of ClaimResultMessage)

        'ClaimStatus()'	varchar(2)
        Dim _ClaimStatus As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.ClaimStatus) Then
            _ClaimStatus.ResultCode = "ClaimStatus"
            _ClaimStatus.ResultMessage = "No data"
            _Result.Add(_ClaimStatus)
        End If

        'TempPolicy()'	varchar(100)
        Dim _TempPolicy As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.TempPolicy) Then
            _TempPolicy.ResultCode = "TempPolicy"
            _TempPolicy.ResultMessage = "No data"
            _Result.Add(_TempPolicy)

        End If

        'RefNo()'	varchar(100)
        Dim _RefNo As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.RefNo) Then
            _RefNo.ResultCode = "RefNo"
            _RefNo.ResultMessage = "No data"
            _Result.Add(_RefNo)

        End If

        'Version()'	int
        Dim _Version As New ClaimResultMessage
        If _data.Version Is Nothing Then
            _Version.ResultCode = "Version"
            _Version.ResultMessage = "Invalid Number"
            _Result.Add(_Version)
        End If

        'PolicyNo()'	varchar(100)
        Dim _PolicyNo As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.PolicyNo) Then
            _PolicyNo.ResultCode = "PolicyNo"
            _PolicyNo.ResultMessage = "No data"
            _Result.Add(_PolicyNo)

        End If

        'PolicyYear()'	int
        Dim _PolicyYear As New ClaimResultMessage
        If _data.PolicyYear Is Nothing Then
            _PolicyYear.ResultCode = "PolicyYear"
            _PolicyYear.ResultMessage = "No data"
            _Result.Add(_PolicyYear)
        End If

        'ClaimNo()'	varchar(100)
        Dim _ClaimNo As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.ClaimNo) Then
            _ClaimNo.ResultCode = "ClaimNo"
            _ClaimNo.ResultMessage = "No data"
            _Result.Add(_ClaimNo)

        End If

        'TransactionDate()'	datetime
        Dim _TransactionDate As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.TransactionDate) Then
            _TransactionDate.ResultCode = "TransactionDate"
            _TransactionDate.ResultMessage = "Invalid Date"
            _Result.Add(_TransactionDate)

        End If

        'Unwriter()'	varchar(10)
        Dim _Unwriter As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.Unwriter) Or String.IsNullOrEmpty(_data.TRNo) Then
            _Unwriter.ResultCode = "Unwriter"
            _Unwriter.ResultMessage = "Invalid Unwriter Code"
            _Result.Add(_Unwriter)

        Else
            Using dc As New DataClasses_MotorClaimDataExt()
                Dim _data_uw = _data.Unwriter
                Dim _uw = (From c In dc.Runnings Where c.UWCode.Equals(_data_uw)).FirstOrDefault()
                If _uw Is Nothing Then
                    _Unwriter.ResultCode = "Unwriter"
                    _Unwriter.ResultMessage = "Invalid Unwriter Code"
                    _Result.Add(_Unwriter)
                End If
            End Using
        End If

        '================================================================================================================

        'GarageType Int
        Dim _GarageType As New ClaimResultMessage
        If _data.GarageType Is Nothing Then
            _GarageType.ResultCode = "GarageType"
            _GarageType.ResultMessage = "Invalid Number"
            _Result.Add(_GarageType)
        Else
            '=============== check garage ==================
            If _data.GarageType = 1 Or _data.GarageType = 2 Then
                ' ''GarageCode varchar(10)
                Dim _GarageCode As New ClaimResultMessage
                If String.IsNullOrEmpty(_data.GarageCode) Then
                    _GarageCode.ResultCode = "GarageCode"
                    _GarageCode.ResultMessage = "No data"
                    _Result.Add(_GarageCode)
                End If

            ElseIf _data.GarageType = 3 Or _data.GarageType = 4 Then

                'GarageName varchar(100)
                Dim _GarageName As New ClaimResultMessage
                If String.IsNullOrEmpty(_data.GarageName) Then
                    _GarageName.ResultCode = "GarageName"
                    _GarageName.ResultMessage = "No data"
                    _Result.Add(_GarageName)

                End If
                'GaragePlace varchar(255)
                Dim _GaragePlace As New ClaimResultMessage
                If String.IsNullOrEmpty(_data.GaragePlace) Then
                    _GaragePlace.ResultCode = "GaragePlace"
                    _GaragePlace.ResultMessage = "No data"
                    _Result.Add(_GaragePlace)

                End If
                'GarageTumbon varchar(255)
                Dim _GarageTumbon As New ClaimResultMessage
                If String.IsNullOrEmpty(_data.GarageTumbon) Then
                    _GarageTumbon.ResultCode = "GarageTumbon"
                    _GarageTumbon.ResultMessage = "No data"
                    _Result.Add(_GarageTumbon)

                End If
                'GarageAmphur varchar(255)
                Dim _GarageAmphur As New ClaimResultMessage
                If String.IsNullOrEmpty(_data.GarageAmphur) Then
                    _GarageAmphur.ResultCode = "GarageAmphur"
                    _GarageAmphur.ResultMessage = "No data"
                    _Result.Add(_GarageAmphur)

                End If
                'GarageProvince varchar(255)
                Dim _GarageProvince As New ClaimResultMessage
                If String.IsNullOrEmpty(_data.GarageProvince) Then
                    _GarageProvince.ResultCode = "GarageProvince"
                    _GarageProvince.ResultMessage = "No data"
                    _Result.Add(_GarageProvince)

                End If
                'GarageZipcode varchar(5)
                Dim _GarageZipcode As New ClaimResultMessage
                If String.IsNullOrEmpty(_data.GarageZipcode) Then
                    _GarageZipcode.ResultCode = "GarageZipcode"
                    _GarageZipcode.ResultMessage = "No data"
                    _Result.Add(_GarageZipcode)

                End If

            End If
        End If
        'CarRepairDate DateTime
        Dim _CarRepairDate As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.CarRepairDate) Then
            _CarRepairDate.ResultCode = "CarRepairDate"
            _CarRepairDate.ResultMessage = "Invalid Date"
            _Result.Add(_CarRepairDate)

        End If
        'CarReceiveDate DateTime
        Dim _CarReceiveDate As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.CarReceiveDate) Then
            _CarReceiveDate.ResultCode = "CarReceiveDate"
            _CarReceiveDate.ResultMessage = "Invalid Date"
            _Result.Add(_CarReceiveDate)

        End If
        'PartsDealerName() '	varchar(255)
        Dim _PartsDealerName As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.PartsDealerName) Then
            _PartsDealerName.ResultCode = "PartsDealerName"
            _PartsDealerName.ResultMessage = "No data"
            _Result.Add(_PartsDealerName)
        End If
        'PaymentDetails()  '	varchar(max)
        Dim _PaymentDetails As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.PaymentDetails) Then
            _PaymentDetails.ResultCode = "PaymentDetails"
            _PaymentDetails.ResultMessage = "No data"
            _Result.Add(_PaymentDetails)
        End If


        'Amount1 float
        Dim _Amount1 As New ClaimResultMessage
        If _data.Amount1 Is Nothing Then
            _Amount1.ResultCode = "Amount1"
            _Amount1.ResultMessage = "Invalid Number"
            _Result.Add(_Amount1)
        End If
        'Amount2 float
        Dim _Amount2 As New ClaimResultMessage
        If _data.Amount2 Is Nothing Then
            _Amount2.ResultCode = "Amount2"
            _Amount2.ResultMessage = "Invalid Number"
            _Result.Add(_Amount2)
        End If
        'Amount3 float
        Dim _Amount3 As New ClaimResultMessage
        If _data.Amount3 Is Nothing Then
            _Amount3.ResultCode = "Amount3"
            _Amount3.ResultMessage = "Invalid Number"
            _Result.Add(_Amount3)
        End If

        Return _Result.ToList()
    End Function

    Public Function ValidateClaimData99(ByRef _data As tblClaimTransaction_Data) As List(Of ClaimResultMessage)
        Dim _Result As New List(Of ClaimResultMessage)

        'ClaimStatus()'	varchar(2)
        Dim _ClaimStatus As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.ClaimStatus) Then
            _ClaimStatus.ResultCode = "ClaimStatus"
            _ClaimStatus.ResultMessage = "No data"
            _Result.Add(_ClaimStatus)
        End If

        'TempPolicy()'	varchar(100)
        Dim _TempPolicy As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.TempPolicy) Then
            _TempPolicy.ResultCode = "TempPolicy"
            _TempPolicy.ResultMessage = "No data"
            _Result.Add(_TempPolicy)

        End If

        'RefNo()'	varchar(100)
        Dim _RefNo As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.RefNo) Then
            _RefNo.ResultCode = "RefNo"
            _RefNo.ResultMessage = "No data"
            _Result.Add(_RefNo)

        End If

        'Version()'	int
        Dim _Version As New ClaimResultMessage
        If _data.Version Is Nothing Then
            _Version.ResultCode = "Version"
            _Version.ResultMessage = "Invalid Number"
            _Result.Add(_Version)
        End If

        'PolicyNo()'	varchar(100)
        Dim _PolicyNo As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.PolicyNo) Then
            _PolicyNo.ResultCode = "PolicyNo"
            _PolicyNo.ResultMessage = "No data"
            _Result.Add(_PolicyNo)

        End If

        'PolicyYear()'	int
        Dim _PolicyYear As New ClaimResultMessage
        If _data.PolicyYear Is Nothing Then
            _PolicyYear.ResultCode = "PolicyYear"
            _PolicyYear.ResultMessage = "No data"
            _Result.Add(_PolicyYear)
        End If

        'ClaimNo()'	varchar(100)
        Dim _ClaimNo As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.ClaimNo) Then
            _ClaimNo.ResultCode = "ClaimNo"
            _ClaimNo.ResultMessage = "No data"
            _Result.Add(_ClaimNo)

        End If

        'TransactionDate()'	datetime
        Dim _TransactionDate As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.TransactionDate) Then
            _TransactionDate.ResultCode = "TransactionDate"
            _TransactionDate.ResultMessage = "Invalid Date"
            _Result.Add(_TransactionDate)

        End If

        'Unwriter()'	varchar(10)
        Dim _Unwriter As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.Unwriter) Or String.IsNullOrEmpty(_data.TRNo) Then
            _Unwriter.ResultCode = "Unwriter"
            _Unwriter.ResultMessage = "Invalid Unwriter Code"
            _Result.Add(_Unwriter)

        Else
            Using dc As New DataClasses_MotorClaimDataExt()
                Dim _data_uw = _data.Unwriter
                Dim _uw = (From c In dc.Runnings Where c.UWCode.Equals(_data_uw)).FirstOrDefault()
                If _uw Is Nothing Then
                    _Unwriter.ResultCode = "Unwriter"
                    _Unwriter.ResultMessage = "Invalid Unwriter Code"
                    _Result.Add(_Unwriter)
                End If
            End Using
        End If

        '================================================================================================================

        'GarageType Int
        Dim _GarageType As New ClaimResultMessage
        If _data.GarageType Is Nothing Then
            _GarageType.ResultCode = "GarageType"
            _GarageType.ResultMessage = "Invalid Number"
            _Result.Add(_GarageType)
        Else
            '=============== check garage ==================
            If _data.GarageType = 1 Or _data.GarageType = 2 Then
                ' ''GarageCode varchar(10)
                Dim _GarageCode As New ClaimResultMessage
                If String.IsNullOrEmpty(_data.GarageCode) Then
                    _GarageCode.ResultCode = "GarageCode"
                    _GarageCode.ResultMessage = "No data"
                    _Result.Add(_GarageCode)
                End If

            ElseIf _data.GarageType = 3 Or _data.GarageType = 4 Then

                'GarageName varchar(100)
                Dim _GarageName As New ClaimResultMessage
                If String.IsNullOrEmpty(_data.GarageName) Then
                    _GarageName.ResultCode = "GarageName"
                    _GarageName.ResultMessage = "No data"
                    _Result.Add(_GarageName)

                End If
                'GaragePlace varchar(255)
                Dim _GaragePlace As New ClaimResultMessage
                If String.IsNullOrEmpty(_data.GaragePlace) Then
                    _GaragePlace.ResultCode = "GaragePlace"
                    _GaragePlace.ResultMessage = "No data"
                    _Result.Add(_GaragePlace)

                End If
                'GarageTumbon varchar(255)
                Dim _GarageTumbon As New ClaimResultMessage
                If String.IsNullOrEmpty(_data.GarageTumbon) Then
                    _GarageTumbon.ResultCode = "GarageTumbon"
                    _GarageTumbon.ResultMessage = "No data"
                    _Result.Add(_GarageTumbon)

                End If
                'GarageAmphur varchar(255)
                Dim _GarageAmphur As New ClaimResultMessage
                If String.IsNullOrEmpty(_data.GarageAmphur) Then
                    _GarageAmphur.ResultCode = "GarageAmphur"
                    _GarageAmphur.ResultMessage = "No data"
                    _Result.Add(_GarageAmphur)

                End If
                'GarageProvince varchar(255)
                Dim _GarageProvince As New ClaimResultMessage
                If String.IsNullOrEmpty(_data.GarageProvince) Then
                    _GarageProvince.ResultCode = "GarageProvince"
                    _GarageProvince.ResultMessage = "No data"
                    _Result.Add(_GarageProvince)

                End If
                'GarageZipcode varchar(5)
                Dim _GarageZipcode As New ClaimResultMessage
                If String.IsNullOrEmpty(_data.GarageZipcode) Then
                    _GarageZipcode.ResultCode = "GarageZipcode"
                    _GarageZipcode.ResultMessage = "No data"
                    _Result.Add(_GarageZipcode)

                End If

            End If
        End If
        'CarRepairDate DateTime
        Dim _CarRepairDate As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.CarRepairDate) Then
            _CarRepairDate.ResultCode = "CarRepairDate"
            _CarRepairDate.ResultMessage = "Invalid Date"
            _Result.Add(_CarRepairDate)

        End If
        'CarReceiveDate DateTime
        Dim _CarReceiveDate As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.CarReceiveDate) Then
            _CarReceiveDate.ResultCode = "CarReceiveDate"
            _CarReceiveDate.ResultMessage = "Invalid Date"
            _Result.Add(_CarReceiveDate)

        End If
        'PartsDealerName() '	varchar(255)
        Dim _PartsDealerName As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.PartsDealerName) Then
            _PartsDealerName.ResultCode = "PartsDealerName"
            _PartsDealerName.ResultMessage = "No data"
            _Result.Add(_PartsDealerName)
        End If
        'PaymentDetails()  '	varchar(max)
        Dim _PaymentDetails As New ClaimResultMessage
        If String.IsNullOrEmpty(_data.PaymentDetails) Then
            _PaymentDetails.ResultCode = "PaymentDetails"
            _PaymentDetails.ResultMessage = "No data"
            _Result.Add(_PaymentDetails)
        End If


        'Amount1 float
        Dim _Amount1 As New ClaimResultMessage
        If _data.Amount1 Is Nothing Then
            _Amount1.ResultCode = "Amount1"
            _Amount1.ResultMessage = "Invalid Number"
            _Result.Add(_Amount1)
        End If
        'Amount2 float
        Dim _Amount2 As New ClaimResultMessage
        If _data.Amount2 Is Nothing Then
            _Amount2.ResultCode = "Amount2"
            _Amount2.ResultMessage = "Invalid Number"
            _Result.Add(_Amount2)
        End If
        'Amount3 float
        Dim _Amount3 As New ClaimResultMessage
        If _data.Amount3 Is Nothing Then
            _Amount3.ResultCode = "Amount3"
            _Amount3.ResultMessage = "Invalid Number"
            _Result.Add(_Amount3)
        End If

        Return _Result.ToList()
    End Function

    'Private Function ValidateClaimData(ByRef _data As tblClaimTransaction_Data) As List(Of ClaimResultMessage)
    '    Dim _Result As New List(Of ClaimResultMessage)

    '    Using dc As New DataContext_MotorClaimExt()
    '        Dim _ClaimStatusCode As String = ""
    '        'ClaimStatus()'	varchar(50)
    '        Dim _ClaimStatus As New ClaimResultMessage
    '        If String.IsNullOrEmpty(_data.ClaimStatus) Then
    '            _ClaimStatus.ResultCode = "ClaimStatus"
    '            _ClaimStatus.ResultMessage = "No data"
    '            _Result.Add(_ClaimStatus)
    '        Else
    '            _data.ClaimStatus = Left(_data.ClaimStatus, 50)

    '            _ClaimStatusCode = _data.ClaimStatus
    '        End If

    '        'TempPolicy()'	varchar(100)
    '        Dim _TempPolicy As New ClaimResultMessage
    '        If String.IsNullOrEmpty(_data.TempPolicy) Then
    '            _TempPolicy.ResultCode = "TempPolicy"
    '            _TempPolicy.ResultMessage = "No data"
    '            _Result.Add(_TempPolicy)
    '        Else
    '            _data.TempPolicy = Left(_data.TempPolicy, 100)
    '        End If

    '        'RefNo()'	varchar(100)
    '        Dim _RefNo As New ClaimResultMessage
    '        If String.IsNullOrEmpty(_data.RefNo) Then
    '            _RefNo.ResultCode = "RefNo"
    '            _RefNo.ResultMessage = "No data"
    '            _Result.Add(_RefNo)
    '        Else
    '            _data.RefNo = Left(_data.RefNo, 100)
    '        End If

    '        'Version()'	int
    '        Dim _Version As New ClaimResultMessage
    '        If _data.Version Is Nothing Then
    '            _Version.ResultCode = "Version"
    '            _Version.ResultMessage = "Invalid Number"
    '            _Result.Add(_Version)
    '        End If

    '        'PolicyNo()'	varchar(100)
    '        Dim _PolicyNo As New ClaimResultMessage
    '        If String.IsNullOrEmpty(_data.PolicyNo) Then
    '            _PolicyNo.ResultCode = "PolicyNo"
    '            _PolicyNo.ResultMessage = "No data"
    '            _Result.Add(_PolicyNo)
    '        Else
    '            _data.PolicyNo = Left(_data.PolicyNo, 100)
    '        End If

    '        'PolicyYear()'	int
    '        Dim _PolicyYear As New ClaimResultMessage
    '        If _data.PolicyYear Is Nothing Then
    '            _PolicyYear.ResultCode = "PolicyYear"
    '            _PolicyYear.ResultMessage = "No data"
    '            _Result.Add(_PolicyYear)
    '        End If

    '        'ClaimNoticeNo()'	varchar(100)
    '        Dim _ClaimNoticeNo As New ClaimResultMessage
    '        If String.IsNullOrEmpty(_data.ClaimNoticeNo) Then
    '            _ClaimNoticeNo.ResultCode = "ClaimNoticeNo"
    '            _ClaimNoticeNo.ResultMessage = "No data"
    '            _Result.Add(_ClaimNoticeNo)
    '        Else
    '            _data.ClaimNoticeNo = Left(_data.ClaimNoticeNo, 100)
    '        End If

    '        'ClaimNo()'	varchar(100)
    '        Dim _ClaimNo As New ClaimResultMessage
    '        If String.IsNullOrEmpty(_data.ClaimNo) Then
    '            _ClaimNo.ResultCode = "ClaimNo"
    '            _ClaimNo.ResultMessage = "No data"
    '            _Result.Add(_ClaimNo)
    '        Else
    '            _data.ClaimNo = Left(_data.ClaimNo, 100)
    '        End If

    '        'TransactionDate()'	datetime
    '        Dim _TransactionDate As New ClaimResultMessage
    '        If _data.TransactionDate Is Nothing Then
    '            _TransactionDate.ResultCode = "TransactionDate"
    '            _TransactionDate.ResultMessage = "Invalid Date"
    '            _Result.Add(_TransactionDate)
    '        End If

    '        'Unwriter()'	varchar(10)
    '        Dim _Unwriter As New ClaimResultMessage
    '        If String.IsNullOrEmpty(_data.Unwriter) Then
    '            _Unwriter.ResultCode = "Unwriter"
    '            _Unwriter.ResultMessage = "No data"
    '            _Result.Add(_Unwriter)
    '        Else
    '            _data.Unwriter = Left(_data.Unwriter, 10)
    '        End If



    '        'GarageType Int
    '        Dim _GarageType As New ClaimResultMessage
    '        If _data.GarageType Is Nothing Then
    '            _GarageType.ResultCode = "GarageType"
    '            _GarageType.ResultMessage = "Invalid Number"
    '            _Result.Add(_GarageType)
    '        Else

    '            '=============== check garage ==================
    '            If _data.GarageType = 1 Or _data.GarageType = 2 Then

    '                ''GarageCode varchar(10)
    '                Dim _GarageCode As New ClaimResultMessage
    '                'If String.IsNullOrEmpty(_data.GarageCode) Then
    '                '    _GarageCode.ResultCode = "GarageCode"
    '                '    _GarageCode.ResultMessage = "No data"
    '                '    _Result.Add(_GarageCode)
    '                'Else
    '                '    _data.GarageCode = Left(_data.GarageCode, 10)


    '                'End If

    '                Dim _gcode = _data.GarageCode
    '                Dim chkgCode = (From c In dc.tblGarages Where c.GarageCode.Equals(_gcode)).FirstOrDefault()
    '                If chkgCode Is Nothing Then
    '                    _GarageCode.ResultCode = "GarageCode"
    '                    _GarageCode.ResultMessage = "Invalid GarageCode"
    '                    _Result.Add(_GarageCode)
    '                End If




    '            ElseIf _data.GarageType = 3 Or _data.GarageType = 4 Then

    '                'GarageName varchar(100)
    '                Dim _GarageName As New ClaimResultMessage
    '                If String.IsNullOrEmpty(_data.GarageName) Then
    '                    _GarageName.ResultCode = "GarageName"
    '                    _GarageName.ResultMessage = "No data"
    '                    _Result.Add(_GarageName)
    '                Else
    '                    _data.GarageName = Left(_data.GarageName, 100)
    '                End If
    '                'GaragePlace varchar(255)
    '                Dim _GaragePlace As New ClaimResultMessage
    '                If String.IsNullOrEmpty(_data.GaragePlace) Then
    '                    _GaragePlace.ResultCode = "GaragePlace"
    '                    _GaragePlace.ResultMessage = "No data"
    '                    _Result.Add(_GaragePlace)
    '                Else
    '                    _data.GaragePlace = Left(_data.GaragePlace, 255)
    '                End If
    '                'GarageTumbon varchar(255)
    '                Dim _GarageTumbon As New ClaimResultMessage
    '                If String.IsNullOrEmpty(_data.GarageTumbon) Then
    '                    _GarageTumbon.ResultCode = "GarageTumbon"
    '                    _GarageTumbon.ResultMessage = "No data"
    '                    _Result.Add(_GarageTumbon)
    '                Else
    '                    _data.GarageTumbon = Left(_data.GarageTumbon, 255)
    '                End If
    '                'GarageAmphur varchar(255)
    '                Dim _GarageAmphur As New ClaimResultMessage
    '                If String.IsNullOrEmpty(_data.GarageAmphur) Then
    '                    _GarageAmphur.ResultCode = "GarageAmphur"
    '                    _GarageAmphur.ResultMessage = "No data"
    '                    _Result.Add(_GarageAmphur)
    '                Else
    '                    _data.GarageAmphur = Left(_data.GarageAmphur, 255)
    '                End If
    '                'GarageProvince varchar(255)
    '                Dim _GarageProvince As New ClaimResultMessage
    '                If String.IsNullOrEmpty(_data.GarageProvince) Then
    '                    _GarageProvince.ResultCode = "GarageProvince"
    '                    _GarageProvince.ResultMessage = "No data"
    '                    _Result.Add(_GarageProvince)
    '                Else
    '                    _data.GarageProvince = Left(_data.GarageProvince, 255)
    '                End If
    '                'GarageZipcode varchar(5)
    '                Dim _GarageZipcode As New ClaimResultMessage
    '                If String.IsNullOrEmpty(_data.GarageZipcode) Then
    '                    _GarageZipcode.ResultCode = "GarageZipcode"
    '                    _GarageZipcode.ResultMessage = "No data"
    '                    _Result.Add(_GarageZipcode)
    '                Else
    '                    _data.GarageZipcode = Left(_data.GarageZipcode, 5)
    '                End If



    '            End If
    '        End If










    '        '=============================00 = Open====================================================

    '        If _ClaimStatusCode.Equals("00") Then
    '            'InsuredName varchar(255)
    '            Dim _InsuredName As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.InsuredName) Then
    '                _InsuredName.ResultCode = "InsuredName"
    '                _InsuredName.ResultMessage = "No data"
    '                _Result.Add(_InsuredName)
    '            Else
    '                _data.InsuredName = Left(_data.InsuredName, 255)
    '            End If
    '            'EffectiveDate DateTime
    '            Dim _EffectiveDate As New ClaimResultMessage
    '            If _data.EffectiveDate Is Nothing Then
    '                _EffectiveDate.ResultCode = "EffectiveDate"
    '                _EffectiveDate.ResultMessage = "Invalid Date"
    '                _Result.Add(_EffectiveDate)
    '            End If
    '            'ExpiryDate DateTime
    '            Dim _ExpiryDate As New ClaimResultMessage
    '            If _data.ExpiryDate Is Nothing Then
    '                _ExpiryDate.ResultCode = "ExpiryDate"
    '                _ExpiryDate.ResultMessage = "Invalid Date"
    '                _Result.Add(_ExpiryDate)
    '            End If
    '            'Beneficiary varchar(255)
    '            Dim _Beneficiary As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.Beneficiary) Then
    '                _Beneficiary.ResultCode = "Beneficiary"
    '                _Beneficiary.ResultMessage = "No data"
    '                _Result.Add(_Beneficiary)
    '            Else
    '                _data.Beneficiary = Left(_data.Beneficiary, 255)
    '            End If
    '            'CarBrand varchar(50)
    '            Dim _CarBrand As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.CarBrand) Then
    '                _CarBrand.ResultCode = "CarBrand"
    '                _CarBrand.ResultMessage = "No data"
    '                _Result.Add(_CarBrand)
    '            Else
    '                _data.CarBrand = Left(_data.CarBrand, 50)
    '            End If
    '            'CarModel varchar(100)
    '            Dim _CarModel As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.CarModel) Then
    '                _CarModel.ResultCode = "CarModel"
    '                _CarModel.ResultMessage = "No data"
    '                _Result.Add(_CarModel)
    '            Else
    '                _data.CarModel = Left(_data.CarModel, 100)
    '            End If
    '            'CarLicense varchar(50)
    '            Dim _CarLicense As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.CarLicense) Then
    '                _CarLicense.ResultCode = "CarLicense"
    '                _CarLicense.ResultMessage = "No data"
    '                _Result.Add(_CarLicense)
    '            Else
    '                _data.CarLicense = Left(_data.CarLicense, 50)
    '            End If
    '            'CarYear varchar(50)
    '            Dim _CarYear As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.CarYear) Then
    '                _CarYear.ResultCode = "CarYear"
    '                _CarYear.ResultMessage = "No data"
    '                _Result.Add(_CarYear)
    '            Else
    '                _data.CarYear = Left(_data.CarYear, 50)
    '            End If
    '            'ChassisNo varchar(50)
    '            Dim _ChassisNo As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.ChassisNo) Then
    '                _ChassisNo.ResultCode = "ChassisNo"
    '                _ChassisNo.ResultMessage = "No data"
    '                _Result.Add(_ChassisNo)
    '            Else
    '                _data.ChassisNo = Left(_data.ChassisNo, 50)
    '            End If
    '            'ShowRoomName varchar(100)
    '            Dim _ShowRoomName As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.ShowRoomName) Then
    '                _ShowRoomName.ResultCode = "ShowRoomName"
    '                _ShowRoomName.ResultMessage = "No data"
    '                _Result.Add(_ShowRoomName)
    '            Else
    '                _data.ShowRoomName = Left(_data.ShowRoomName, 100)
    '            End If
    '            'ShowRoomCode varchar(10)
    '            Dim _ShowRoomCode As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.ShowRoomCode) Then
    '                _ShowRoomCode.ResultCode = "ShowRoomCode"
    '                _ShowRoomCode.ResultMessage = "No data"
    '                _Result.Add(_ShowRoomCode)
    '            Else
    '                _data.ShowRoomCode = Left(_data.ShowRoomCode, 10)
    '                '=============== check showroom ==================
    '                Dim _sr = _data.ShowRoomCode
    '                'Dim chkShowRoom = (From c In dc.tblShowRooms Where c.ShowRoomCode.Equals(_sr)).FirstOrDefault()
    '                'If chkShowRoom Is Nothing Then
    '                '    _ShowRoomCode.ResultCode = "ShowRoomCode"
    '                '    _ShowRoomCode.ResultMessage = "Invalid Showroom"
    '                '    _Result.Add(_ShowRoomCode)
    '                'End If
    '            End If
    '            'ClaimNoticeDate DateTime
    '            Dim _ClaimNoticeDate As New ClaimResultMessage
    '            If _data.ClaimNoticeDate Is Nothing Then
    '                _ClaimNoticeDate.ResultCode = "ClaimNoticeDate"
    '                _ClaimNoticeDate.ResultMessage = "Invalid Date"
    '                _Result.Add(_ClaimNoticeDate)
    '            End If
    '            'ClaimDetails varchar(255)
    '            Dim _ClaimDetails As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.ClaimDetails) Then
    '                _ClaimDetails.ResultCode = "ClaimDetails"
    '                _ClaimDetails.ResultMessage = "No data"
    '                _Result.Add(_ClaimDetails)
    '            Else
    '                _data.ClaimDetails = Left(_data.ClaimDetails, 255)
    '            End If
    '            'ClaimType Int
    '            Dim _ClaimType As New ClaimResultMessage
    '            If _data.ClaimType Is Nothing Then
    '                _ClaimType.ResultCode = "ClaimType"
    '                _ClaimType.ResultMessage = "Invalid Number"
    '                _Result.Add(_ClaimType)
    '            End If
    '            'ClaimResult Int
    '            Dim _ClaimResult As New ClaimResultMessage
    '            If _data.ClaimResult Is Nothing Then
    '                _ClaimResult.ResultCode = "ClaimResult"
    '                _ClaimResult.ResultMessage = "Invalid Number"
    '                _Result.Add(_ClaimResult)
    '            End If
    '            'ClaimDamageDetails varchar(255)
    '            Dim _ClaimDamageDetails As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.ClaimDamageDetails) Then
    '                _ClaimDamageDetails.ResultCode = "ClaimDamageDetails"
    '                _ClaimDamageDetails.ResultMessage = "No data"
    '                _Result.Add(_ClaimDamageDetails)
    '            Else
    '                _data.ClaimDamageDetails = Left(_data.ClaimDamageDetails, 255)
    '            End If
    '            'IntGarageName varchar(100)
    '            Dim _IntGarageName As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.IntGarageName) Then
    '                _IntGarageName.ResultCode = "IntGarageName"
    '                _IntGarageName.ResultMessage = "No data"
    '                _Result.Add(_IntGarageName)
    '            Else
    '                _data.IntGarageName = Left(_data.IntGarageName, 100)
    '            End If
    '            'IntGarageType Int
    '            Dim _IntGarageType As New ClaimResultMessage
    '            If _data.IntGarageType Is Nothing Then
    '                _IntGarageType.ResultCode = "IntGarageType"
    '                _IntGarageType.ResultMessage = "Invalid Number"
    '                _Result.Add(_IntGarageType)
    '            End If
    '            'CallCenter varchar(10)
    '            Dim _CallCenter As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.CallCenter) Then
    '                _CallCenter.ResultCode = "CallCenter"
    '                _CallCenter.ResultMessage = "No data"
    '                _Result.Add(_CallCenter)
    '            Else
    '                _data.CallCenter = Left(_data.CallCenter, 10)
    '            End If
    '            'AccidentDate DateTime
    '            Dim _AccidentDate As New ClaimResultMessage
    '            If _data.AccidentDate Is Nothing Then
    '                _AccidentDate.ResultCode = "AccidentDate"
    '                _AccidentDate.ResultMessage = "Invalid Date"
    '                _Result.Add(_AccidentDate)
    '            End If
    '            'AccidentPlace varchar(255)
    '            Dim _AccidentPlace As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.AccidentPlace) Then
    '                _AccidentPlace.ResultCode = "AccidentPlace"
    '                _AccidentPlace.ResultMessage = "No data"
    '                _Result.Add(_AccidentPlace)
    '            Else
    '                _data.AccidentPlace = Left(_data.AccidentPlace, 255)
    '            End If
    '            'AccidentTumbon varchar(255)
    '            Dim _AccidentTumbon As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.AccidentTumbon) Then
    '                _AccidentTumbon.ResultCode = "AccidentTumbon"
    '                _AccidentTumbon.ResultMessage = "No data"
    '                _Result.Add(_AccidentTumbon)
    '            Else
    '                _data.AccidentTumbon = Left(_data.AccidentTumbon, 255)
    '            End If
    '            'AccidentAmphur varchar(255)
    '            Dim _AccidentAmphur As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.AccidentAmphur) Then
    '                _AccidentAmphur.ResultCode = "AccidentAmphur"
    '                _AccidentAmphur.ResultMessage = "No data"
    '                _Result.Add(_AccidentAmphur)
    '            Else
    '                _data.AccidentAmphur = Left(_data.AccidentAmphur, 255)
    '            End If
    '            'AccidentProvince varchar(255)
    '            Dim _AccidentProvince As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.AccidentProvince) Then
    '                _AccidentProvince.ResultCode = "AccidentProvince"
    '                _AccidentProvince.ResultMessage = "No data"
    '                _Result.Add(_AccidentProvince)
    '            Else
    '                _data.AccidentProvince = Left(_data.AccidentProvince, 255)
    '            End If
    '            'AccidentZipcode varchar(5)
    '            Dim _AccidentZipcode As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.AccidentZipcode) Then
    '                _AccidentZipcode.ResultCode = "AccidentZipcode"
    '                _AccidentZipcode.ResultMessage = "No data"
    '                _Result.Add(_AccidentZipcode)
    '            Else
    '                _data.AccidentZipcode = Left(_data.AccidentZipcode, 5)
    '            End If
    '            'DriverName varchar(255)
    '            Dim _DriverName As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.DriverName) Then
    '                _DriverName.ResultCode = "DriverName"
    '                _DriverName.ResultMessage = "No data"
    '                _Result.Add(_DriverName)
    '            Else
    '                _data.DriverName = Left(_data.DriverName, 255)
    '            End If
    '            'DriverTel varchar(100)
    '            Dim _DriverTel As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.DriverTel) Then
    '                _DriverTel.ResultCode = "DriverTel"
    '                _DriverTel.ResultMessage = "No data"
    '                _Result.Add(_DriverTel)
    '            Else
    '                _data.DriverTel = Left(_data.DriverTel, 100)
    '            End If
    '            'NoticeName varchar(255)
    '            Dim _NoticeName As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.NoticeName) Then
    '                _NoticeName.ResultCode = "NoticeName"
    '                _NoticeName.ResultMessage = "No data"
    '                _Result.Add(_NoticeName)
    '            Else
    '                _data.NoticeName = Left(_data.NoticeName, 255)
    '            End If
    '            'NoticeTel varchar(100)
    '            Dim _NoticeTel As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.NoticeTel) Then
    '                _NoticeTel.ResultCode = "NoticeTel"
    '                _NoticeTel.ResultMessage = "No data"
    '                _Result.Add(_NoticeTel)
    '            Else
    '                _data.NoticeTel = Left(_data.NoticeTel, 100)
    '            End If


    '            ''CarRepairDate DateTime
    '            'Dim _CarRepairDate As New ClaimResultMessage
    '            'If _data.CarRepairDate Is Nothing Then
    '            '    _CarRepairDate.ResultCode = "CarRepairDate"
    '            '    _CarRepairDate.ResultMessage = "Invalid Date"
    '            '    _Result.Add(_CarRepairDate)
    '            'End If
    '            ''CarReceiveDate DateTime
    '            'Dim _CarReceiveDate As New ClaimResultMessage
    '            'If _data.CarReceiveDate Is Nothing Then
    '            '    _CarReceiveDate.ResultCode = "CarReceiveDate"
    '            '    _CarReceiveDate.ResultMessage = "Invalid Date"
    '            '    _Result.Add(_CarReceiveDate)
    '            'End If


    '        End If

    '        'ConsentFormNo()'	varchar(50)
    '        Dim _ConsentFormNo As New ClaimResultMessage
    '        If String.IsNullOrEmpty(_data.ConsentFormNo) Then
    '        Else
    '            _data.ConsentFormNo = Left(_data.ConsentFormNo, 50)
    '        End If

    '        'ConsentFormFileName()'	varchar(50)
    '        Dim _ConsentFormFileName As New ClaimResultMessage
    '        If String.IsNullOrEmpty(_data.ConsentFormFileName) Then

    '        Else
    '            _data.ConsentFormFileName = Left(_data.ConsentFormFileName, 50)
    '            ''ConsentFormData()'	binary(1000)
    '            'Dim _ConsentFormData As New ClaimResultMessage
    '            'If _data.ConsentFormData Is Nothing Then
    '            '    _ConsentFormData.ResultCode = "ConsentFormData"
    '            '    _ConsentFormData.ResultMessage = "No data"
    '            '    _Result.Add(_ConsentFormData)
    '            'End If
    '        End If

    '        '==================02 = Payment===================
    '        If _ClaimStatusCode.Equals("02") Then
    '            ''CarRepairDate DateTime
    '            'Dim _CarRepairDate As New ClaimResultMessage
    '            'If _data.CarRepairDate Is Nothing Then
    '            '    _CarRepairDate.ResultCode = "CarRepairDate"
    '            '    _CarRepairDate.ResultMessage = "Invalid Date"
    '            '    _Result.Add(_CarRepairDate)
    '            'End If
    '            ''CarReceiveDate DateTime
    '            'Dim _CarReceiveDate As New ClaimResultMessage
    '            'If _data.CarReceiveDate Is Nothing Then
    '            '    _CarReceiveDate.ResultCode = "CarReceiveDate"
    '            '    _CarReceiveDate.ResultMessage = "Invalid Date"
    '            '    _Result.Add(_CarReceiveDate)
    '            'End If
    '        End If

    '        '=====================================
    '        'If _data.ClaimStatus.Equals("01") _
    '        '    Or _data.ClaimStatus.Equals("02") _
    '        '    Or _data.ClaimStatus.Equals("98") _
    '        '    Or _data.ClaimStatus.Equals("99") Then

    '        'PartsSerialNo()   '	varchar(50)
    '        Dim _PartsSerialNo As New ClaimResultMessage
    '        If String.IsNullOrEmpty(_data.PartsSerialNo) Then
    '            '_PartsSerialNo.ResultCode = "PartsSerialNo"
    '            '_PartsSerialNo.ResultMessage = "No data"
    '            '_Result.Add(_PartsSerialNo)
    '        Else
    '            _data.PartsSerialNo = Left(_data.PartsSerialNo, 50)
    '        End If
    '        'PartsDealerName() '	varchar(255)
    '        Dim _PartsDealerName As New ClaimResultMessage
    '        If String.IsNullOrEmpty(_data.PartsDealerName) Then
    '            '_PartsDealerName.ResultCode = "PartsDealerName"
    '            '_PartsDealerName.ResultMessage = "No data"
    '            '_Result.Add(_PartsDealerName)
    '        Else
    '            _data.PartsDealerName = Left(_data.PartsDealerName, 255)
    '        End If
    '        'PaymentNo()   '	varchar(50)
    '        Dim _PaymentNo As New ClaimResultMessage
    '        If String.IsNullOrEmpty(_data.PaymentNo) Then
    '            '_PaymentNo.ResultCode = "PaymentNo"
    '            '_PaymentNo.ResultMessage = "No data"
    '            '_Result.Add(_PaymentNo)
    '        Else
    '            _data.PaymentNo = Left(_data.PaymentNo, 50)
    '        End If
    '        'PaymentDetails()  '	varchar(255)
    '        Dim _PaymentDetails As New ClaimResultMessage
    '        If String.IsNullOrEmpty(_data.PaymentDetails) Then
    '            '_PaymentDetails.ResultCode = "PaymentDetails"
    '            '_PaymentDetails.ResultMessage = "No data"
    '            '_Result.Add(_PaymentDetails)
    '        Else
    '            _data.PaymentDetails = Left(_data.PaymentDetails, 255)
    '        End If
    '        'Remark()  '	varchar(255)
    '        Dim _Remark As New ClaimResultMessage
    '        If String.IsNullOrEmpty(_data.Remark) Then
    '            '_Remark.ResultCode = "Remark"
    '            '_Remark.ResultMessage = "No data"
    '            '_Result.Add(_Remark)
    '        Else
    '            _data.Remark = Left(_data.Remark, 255)
    '        End If
    '        'End If



    '        '======================================================================================================
    '        'Amount1 float
    '        Dim _Amount1 As New ClaimResultMessage
    '        If _data.Amount1 Is Nothing Then
    '            _Amount1.ResultCode = "Amount1"
    '            _Amount1.ResultMessage = "Invalid Number"
    '            _Result.Add(_Amount1)
    '        End If
    '        'Amount2 float
    '        Dim _Amount2 As New ClaimResultMessage
    '        If _data.Amount2 Is Nothing Then
    '            _Amount2.ResultCode = "Amount2"
    '            _Amount2.ResultMessage = "Invalid Number"
    '            _Result.Add(_Amount2)
    '        End If
    '        'Amount3 float
    '        Dim _Amount3 As New ClaimResultMessage
    '        If _data.Amount3 Is Nothing Then
    '            _Amount3.ResultCode = "Amount3"
    '            _Amount3.ResultMessage = "Invalid Number"
    '            _Result.Add(_Amount3)
    '        End If
    '        'OS_Claims float
    '        Dim _OS_Claims As New ClaimResultMessage
    '        If _data.OS_Claims Is Nothing Then
    '            _OS_Claims.ResultCode = "OS_Claims"
    '            _OS_Claims.ResultMessage = "Invalid Number"
    '            _Result.Add(_OS_Claims)
    '        End If
    '        'Acc_ClaimsPayment	float
    '        Dim _Acc_ClaimsPayment As New ClaimResultMessage
    '        If _data.Acc_ClaimsPayment Is Nothing Then
    '            _Acc_ClaimsPayment.ResultCode = "Acc_ClaimsPayment"
    '            _Acc_ClaimsPayment.ResultMessage = "Invalid Number"
    '            _Result.Add(_Acc_ClaimsPayment)
    '        End If
    '        'Incurred_Claims float
    '        Dim _Incurred_Claims As New ClaimResultMessage
    '        If _data.Incurred_Claims Is Nothing Then
    '            _Incurred_Claims.ResultCode = "Incurred_Claims"
    '            _Incurred_Claims.ResultMessage = "Invalid Number"
    '            _Result.Add(_Incurred_Claims)
    '        End If


    '    End Using

    '    Return _Result.ToList()
    'End Function

    <WebMethod()> _
    Public Function SendConsentForm(ByVal _UWCode As String, ByVal _Password As String, ByVal _Data As List(Of ClaimTransaction_ConsentForm)) As List(Of ClaimTransaction_Data_Result)
        Dim _ReturnResult As New List(Of ClaimTransaction_Data_Result)


        Using dc As New DataClasses_MotorClaimDataExt()
            Dim UWAccess = (From c In dc.Runnings Where c.UWCode.Equals(_UWCode) And c.GUID.Equals(_Password)).FirstOrDefault()
            If UWAccess Is Nothing Then
                Dim _r As New ClaimTransaction_Data_Result
                With _r
                    .ResultStatus = False
                    .ResultMessage.Add(New ClaimResultMessage With {.ResultCode = "ER001", .ResultMessage = "Invalid Underwriter Code or Password"})
                    .SubmitDate = Now
                    .TRNo = "-"
                End With
                _ReturnResult.Add(_r)
            Else

                For Each item In _Data
                    Dim _item_result = ValidateConsentForm(_UWCode, item)
                    Dim _r As New ClaimTransaction_Data_Result

                    _r.SubmitDate = Now
                    _r.ResultNo = item.ConsentFormNo
                    _r.TRNo = item.ConsentFormNo

                    If _item_result.Count > 0 Then
                        Dim _ClaimResultMessage As New List(Of ClaimResultMessage)
                        For Each i_rusult In _item_result
                            _ClaimResultMessage.Add(New ClaimResultMessage With {.ResultCode = i_rusult.ResultCode _
                                                                                , .ResultMessage = i_rusult.ResultMessage})
                        Next
                        _r.ResultMessage = _ClaimResultMessage
                        _r.ResultStatus = False
                    Else
                        _r.ResultStatus = True
                    End If

                    _ReturnResult.Add(_r)

                Next
            End If
        End Using

        Return _ReturnResult
    End Function


    Private Function ValidateConsentForm(ByVal _UWCode As String, ByVal _data As ClaimTransaction_ConsentForm) As List(Of ClaimResultMessage)
        Dim _Result As New List(Of ClaimResultMessage)

        'Dim _Unwriter As New ClaimResultMessage
        Dim _ConsentFormNo As New ClaimResultMessage
        Dim _ConsentFormFileType As New ClaimResultMessage
        Dim _ConsentFormData As New ClaimResultMessage
        Dim _ConsentFormFile As New ClaimResultMessage


        'If String.IsNullOrEmpty(_data.Unwriter) Then
        '    _Unwriter.ResultCode = "Unwriter"
        '    _Unwriter.ResultMessage = "No data"
        '    _Result.Add(_Unwriter)
        'Else
        '    _data.Unwriter = Left(_data.Unwriter, 10)
        'End If

        If String.IsNullOrEmpty(_data.ConsentFormNo) Then
            _ConsentFormNo.ResultCode = "ConsentFormNo"
            _ConsentFormNo.ResultMessage = "No data"
            _Result.Add(_ConsentFormNo)
        Else
            _data.ConsentFormNo = Left(_data.ConsentFormNo, 50)
        End If

        If String.IsNullOrEmpty(_data.ConsentFormFileType) Then
            _ConsentFormFileType.ResultCode = "ConsentFormFileType"
            _ConsentFormFileType.ResultMessage = "No type"
            _Result.Add(_ConsentFormFileType)
        Else
            _data.ConsentFormFileType = Left(_data.ConsentFormFileType, 3)
        End If

        If _data.ConsentFormData Is Nothing Then
            _ConsentFormData.ResultCode = "ConsentFormData"
            _ConsentFormData.ResultMessage = "No data"
            _Result.Add(_ConsentFormData)
        Else

            If _ConsentFormNo IsNot Nothing And _ConsentFormFileType IsNot Nothing Then

                Dim _filepath As String = String.Format("{0}/{1}/ConsentForms/{2}", Server.MapPath("~/App_Data/ConsentForms"), _UWCode, Now.ToString("yyyyMMdd"))

                If Not Directory.Exists(_filepath) Then
                    Directory.CreateDirectory(_filepath)
                End If

                Dim _filename As String = String.Format("{0}/{1}.{2}", _filepath, Regex.Replace(_data.ConsentFormNo, "[\\\/:\*\?""'<>|]", "_"), _data.ConsentFormFileType)
                If File.Exists(_filename) Then
                    _ConsentFormFile.ResultCode = "ConsentFormNo"
                    _ConsentFormFile.ResultMessage = "ConsentFormNo has already in system"
                    _Result.Add(_ConsentFormFile)
                Else
                    File.WriteAllBytes(_filename, _data.ConsentFormData)
                End If

            End If

        End If









        ''ConsentFormFileName()'	varchar(50)
        'Dim _ConsentFormFileName As New ClaimResultMessage
        'If String.IsNullOrEmpty(_data.ConsentFormFileName) Then
        '    _ConsentFormFileName.ResultCode = "ConsentFormFileName"
        '    _ConsentFormFileName.ResultMessage = "No data"
        '    _Result.Add(_ConsentFormFileName)
        'Else


        'End If

        ''ConsentFormData()'	binary(1000)
        'Dim _ConsentFormData As New ClaimResultMessage
        'If _data.ConsentFormData Is Nothing Then
        '    _ConsentFormData.ResultCode = "ConsentFormData"
        '    _ConsentFormData.ResultMessage = "No data"
        '    _Result.Add(_ConsentFormData)
        'Else

        'End If

        Return _Result.ToList()
    End Function



    'Public Function GetMotorClaim(ByVal _Data As List(Of ClaimTransaction_DataObject)) As List(Of ClaimTransaction_Data_Result)
    '    Dim _ReturnResult As New List(Of ClaimTransaction_Data_Result)

    '    Dim sb_error As New StringBuilder()

    '    Using dc As New DataClasses_MotorClaimDataContext()

    '        '===========================================
    '        Dim _ClaimTransaction_Data As New List(Of tblClaimTransaction_Data)
    '        Dim _ClaimTransaction_Result As New List(Of tblClaimTransaction_Result)
    '        '1. Create TRNo.
    '        Dim _TRNo As String = ""
    '        dc.Running_GetByRunningCode(_Data(0).Unwriter, _TRNo)
    '        Dim _submitdate = Now


    '        Dim _ip = HttpContext.Current.Request.ServerVariables("REMOTE_ADDR").ToString()


    '        '2. Get Data
    '        For Each item In _Data
    '            Dim _GUID = Guid.NewGuid().ToString()

    '            '============== Get Date ================
    '            Dim _tblClaimData As New tblClaimTransaction_Data()
    '            With _tblClaimData
    '                .ip = _ip
    '                .TRNo = _TRNo
    '                .ClaimStatus = item.ClaimStatus 'varchar(50)
    '                .TempPolicy = item.TempPolicy 'varchar(100)
    '                .RefNo = item.RefNo 'varchar(100)
    '                .Version = ValidateData(item.Version, objType._Integer) 'int
    '                .PolicyNo = item.PolicyNo 'varchar(100)
    '                .PolicyYear = ValidateData(item.PolicyYear, objType._Integer) 'int
    '                .ClaimNoticeNo = item.ClaimNoticeNo 'varchar(100)
    '                .ClaimNo = item.ClaimNo 'varchar(100)
    '                .TransactionDate = ValidateData(item.TransactionDate, objType._DateTime) 'datetime
    '                .Unwriter = item.Unwriter 'varchar(10)
    '                .InsuredName = item.InsuredName 'varchar(255)
    '                .EffectiveDate = ValidateData(item.EffectiveDate, objType._DateTime) 'datetime
    '                .ExpiryDate = ValidateData(item.ExpiryDate, objType._DateTime) 'datetime
    '                .Beneficiary = item.Beneficiary 'varchar(255)
    '                .CarBrand = item.CarBrand 'varchar(50)
    '                .CarModel = item.CarModel 'varchar(100)
    '                .CarLicense = item.CarLicense 'varchar(50)
    '                .CarYear = item.CarYear 'varchar(50)
    '                .ChassisNo = item.ChassisNo 'varchar(50)
    '                .ShowRoomName = item.ShowRoomName 'varchar(100)
    '                .ShowRoomCode = item.ShowRoomCode 'varchar(10)
    '                .ClaimNoticeDate = ValidateData(item.ClaimNoticeDate, objType._DateTime) 'datetime
    '                .ClaimDetails = item.ClaimDetails 'varchar(255)
    '                .ClaimType = ValidateData(item.ClaimType, objType._Integer) 'int
    '                .ClaimResult = ValidateData(item.ClaimResult, objType._Integer) 'int
    '                .ClaimDamageDetails = item.ClaimDamageDetails 'varchar(255)
    '                .IntGarageName = item.IntGarageName 'varchar(100)
    '                .IntGarageType = ValidateData(item.IntGarageType, objType._Integer) 'int
    '                .CallCenter = item.CallCenter 'varchar(10)
    '                .AccidentDate = ValidateData(item.AccidentDate, objType._DateTime) 'datetime
    '                .AccidentPlace = item.AccidentPlace 'varchar(255)
    '                .AccidentTumbon = item.AccidentTumbon 'varchar(255)
    '                .AccidentAmphur = item.AccidentAmphur 'varchar(255)
    '                .AccidentProvince = item.AccidentProvince 'varchar(255)
    '                .AccidentZipcode = item.AccidentZipcode 'varchar(5)
    '                .DriverName = item.DriverName 'varchar(255)
    '                .DriverTel = item.DriverTel 'varchar(100)
    '                .NoticeName = item.NoticeName 'varchar(255)
    '                .NoticeTel = item.NoticeTel 'varchar(100)
    '                .GarageType = ValidateData(item.GarageType, objType._Integer) 'int
    '                .GarageCode = item.GarageCode 'varchar(10)
    '                .GarageName = item.GarageName 'varchar(100)
    '                .GaragePlace = item.GaragePlace 'varchar(255)
    '                .GarageTumbon = item.GarageTumbon 'varchar(255)
    '                .GarageAmphur = item.GarageAmphur 'varchar(255)
    '                .GarageProvince = item.GarageProvince 'varchar(255)
    '                .GarageZipcode = item.GarageZipcode 'varchar(5)
    '                .CarRepairDate = ValidateData(item.CarRepairDate, objType._DateTime) 'datetime
    '                .CarReceiveDate = ValidateData(item.CarReceiveDate, objType._DateTime) 'datetime
    '                .ConsentFormNo = item.ConsentFormNo 'varchar(50)
    '                .ConsentFormFileName = item.ConsentFormFileName 'varchar(50)


    '                'If item.ConsentFormData IsNot Nothing Then .ConsentFormData = item.ConsentFormData 'binary(1000)

    '                'If item.ConsentFormData IsNot Nothing Then
    '                '    If item.ConsentFormData.Count > 0 Then
    '                '        File.WriteAllBytes(Server.MapPath("~/ConsentForms") & String.Format("/{0}-{1}", _GUID, item.ConsentFormFileName), item.ConsentFormData)
    '                '    End If
    '                'End If

    '                .PartsSerialNo = item.PartsSerialNo 'varchar(50)
    '                .PartsDealerName = item.PartsDealerName 'varchar(255)
    '                .PaymentNo = item.PaymentNo 'varchar(50)
    '                .PaymentDetails = item.PaymentDetails 'varchar(255)
    '                .Remark = item.Remark 'varchar(255)
    '                .Amount1 = ValidateData(item.Amount1, objType._Double) 'float
    '                .Amount2 = ValidateData(item.Amount2, objType._Double) 'float
    '                .Amount3 = ValidateData(item.Amount3, objType._Double) 'float
    '                .OS_Claims = ValidateData(item.OS_Claims, objType._Double) 'float
    '                .Acc_ClaimsPayment = ValidateData(item.Acc_ClaimsPayment, objType._Double) 'float
    '                .Incurred_Claims = ValidateData(item.Incurred_Claims, objType._Double) 'float
    '                .GUID = _GUID
    '                .SubmitDate = _submitdate
    '                '.Status = item.Status 'bit
    '            End With

    '            Dim _item_result = ValidateClaimData(_tblClaimData)

    '            If _item_result.Count > 0 Then
    '                For Each i_rusult In _item_result

    '                    Dim _r As New ClaimTransaction_Data_Result
    '                    With _r
    '                        .ResultStatus = False
    '                        Dim _ResultMessage As New List(Of ClaimResultMessage)
    '                        _ResultMessage.Add(New ClaimResultMessage With {.ResultCode = i_rusult.ResultCode, .ResultMessage = i_rusult.ResultMessage})
    '                        .ResultMessage = _ResultMessage
    '                        .SubmitDate = Now
    '                        .TRNo = "-"
    '                    End With
    '                    _ReturnResult.Add(_r)


    '                    sb_error.AppendFormat("{0} - RefNo. {1} : {2}/{3} <br>", item.ClaimStatus, item.RefNo, i_rusult.ResultCode, i_rusult.ResultMessage)
    '                Next
    '            End If

    '            If Not String.IsNullOrEmpty(sb_error.ToString()) Then
    '                Throw New Exception(sb_error.ToString())
    '            End If


    '            _tblClaimData.Status = True
    '            _ClaimTransaction_Data.Add(_tblClaimData)

    '            '    _ClaimTransaction_Data.Add(_tblClaimData)

    '            '    Dim _r As New ClaimTransaction_Data_Result

    '            '    If _item_result.Count > 0 Then
    '            '        _tblClaimData.Status = False

    '            '        Dim _ClaimResultMessage As New List(Of ClaimResultMessage)
    '            '        For Each i_rusult In _item_result
    '            '            _ClaimTransaction_Result.Add(New tblClaimTransaction_Result With {.GUID = _tblClaimData.GUID _
    '            '                                                                             , .ResultCode = i_rusult.ResultCode _
    '            '                                                                             , .ResultMessage = i_rusult.ResultMessage _
    '            '                                                                             , .ResultNo = _tblClaimData.RefNo _
    '            '                                                                             , .TRNo = _tblClaimData.TRNo _
    '            '                                                                             , .SubmitDate = _submitdate})

    '            '            _ClaimResultMessage.Add(New ClaimResultMessage With {.ResultCode = i_rusult.ResultCode, .ResultMessage = i_rusult.ResultMessage})
    '            '        Next
    '            '        _r.ResultMessage = _ClaimResultMessage


    '            '    Else
    '            '        _tblClaimData.Status = True
    '            '    End If

    '            '    _r.ResultNo = _tblClaimData.RefNo
    '            '    _r.TRNo = _tblClaimData.TRNo
    '            '    _r.ResultStatus = _tblClaimData.Status
    '            '    _r.SubmitDate = _submitdate

    '            '    _ReturnResult.Add(_r)

    '        Next



    '        dc.tblClaimTransaction_Datas.InsertAllOnSubmit(_ClaimTransaction_Data)
    '        dc.tblClaimTransaction_Results.InsertAllOnSubmit(_ClaimTransaction_Result)
    '        '======= Submit All =============
    '        dc.SubmitChanges()





    '    End Using

    '    Return _ReturnResult
    'End Function


    'Private Function ValidateClaimDataObject(ByRef _data As ClaimTransaction_DataObject) As List(Of ClaimResultMessage)
    '    Dim _Result As New List(Of ClaimResultMessage)

    '    Using dc As New DataContext_MotorClaimExt()
    '        Dim _ClaimStatusCode As String = ""
    '        'ClaimStatus()'	varchar(50)
    '        Dim _ClaimStatus As New ClaimResultMessage
    '        If String.IsNullOrEmpty(_data.ClaimStatus) Then
    '            _ClaimStatus.ResultCode = "ClaimStatus"
    '            _ClaimStatus.ResultMessage = "No data"
    '            _Result.Add(_ClaimStatus)
    '        Else
    '            _data.ClaimStatus = Left(_data.ClaimStatus, 50)

    '            _ClaimStatusCode = _data.ClaimStatus
    '        End If

    '        'TempPolicy()'	varchar(100)
    '        Dim _TempPolicy As New ClaimResultMessage
    '        If String.IsNullOrEmpty(_data.TempPolicy) Then
    '            _TempPolicy.ResultCode = "TempPolicy"
    '            _TempPolicy.ResultMessage = "No data"
    '            _Result.Add(_TempPolicy)
    '        Else
    '            _data.TempPolicy = Left(_data.TempPolicy, 100)
    '        End If

    '        'RefNo()'	varchar(100)
    '        Dim _RefNo As New ClaimResultMessage
    '        If String.IsNullOrEmpty(_data.RefNo) Then
    '            _RefNo.ResultCode = "RefNo"
    '            _RefNo.ResultMessage = "No data"
    '            _Result.Add(_RefNo)
    '        Else
    '            _data.RefNo = Left(_data.RefNo, 100)
    '        End If

    '        'Version()'	int
    '        Dim _Version As New ClaimResultMessage
    '        If _data.Version Is Nothing Then
    '            _Version.ResultCode = "Version"
    '            _Version.ResultMessage = "Invalid Number"
    '            _Result.Add(_Version)
    '        End If

    '        'PolicyNo()'	varchar(100)
    '        Dim _PolicyNo As New ClaimResultMessage
    '        If String.IsNullOrEmpty(_data.PolicyNo) Then
    '            _PolicyNo.ResultCode = "PolicyNo"
    '            _PolicyNo.ResultMessage = "No data"
    '            _Result.Add(_PolicyNo)
    '        Else
    '            _data.PolicyNo = Left(_data.PolicyNo, 100)
    '        End If

    '        'PolicyYear()'	int
    '        Dim _PolicyYear As New ClaimResultMessage
    '        If _data.PolicyYear Is Nothing Then
    '            _PolicyYear.ResultCode = "PolicyYear"
    '            _PolicyYear.ResultMessage = "No data"
    '            _Result.Add(_PolicyYear)
    '        End If

    '        'ClaimNoticeNo()'	varchar(100)
    '        Dim _ClaimNoticeNo As New ClaimResultMessage
    '        If String.IsNullOrEmpty(_data.ClaimNoticeNo) Then
    '            _ClaimNoticeNo.ResultCode = "ClaimNoticeNo"
    '            _ClaimNoticeNo.ResultMessage = "No data"
    '            _Result.Add(_ClaimNoticeNo)
    '        Else
    '            _data.ClaimNoticeNo = Left(_data.ClaimNoticeNo, 100)
    '        End If

    '        'ClaimNo()'	varchar(100)
    '        Dim _ClaimNo As New ClaimResultMessage
    '        If String.IsNullOrEmpty(_data.ClaimNo) Then
    '            _ClaimNo.ResultCode = "ClaimNo"
    '            _ClaimNo.ResultMessage = "No data"
    '            _Result.Add(_ClaimNo)
    '        Else
    '            _data.ClaimNo = Left(_data.ClaimNo, 100)
    '        End If

    '        'TransactionDate()'	datetime
    '        Dim _TransactionDate As New ClaimResultMessage
    '        If _data.TransactionDate Is Nothing Then
    '            _TransactionDate.ResultCode = "TransactionDate"
    '            _TransactionDate.ResultMessage = "Invalid Date"
    '            _Result.Add(_TransactionDate)
    '        End If

    '        'Unwriter()'	varchar(10)
    '        Dim _Unwriter As New ClaimResultMessage
    '        If String.IsNullOrEmpty(_data.Unwriter) Then
    '            _Unwriter.ResultCode = "Unwriter"
    '            _Unwriter.ResultMessage = "No data"
    '            _Result.Add(_Unwriter)
    '        Else
    '            _data.Unwriter = Left(_data.Unwriter, 10)
    '        End If



    '        'GarageType Int
    '        Dim _GarageType As New ClaimResultMessage
    '        If _data.GarageType Is Nothing Then
    '            _GarageType.ResultCode = "GarageType"
    '            _GarageType.ResultMessage = "Invalid Number"
    '            _Result.Add(_GarageType)
    '        Else

    '            '=============== check garage ==================
    '            If _data.GarageType = 1 Or _data.GarageType = 2 Then

    '                ''GarageCode varchar(10)
    '                Dim _GarageCode As New ClaimResultMessage
    '                'If String.IsNullOrEmpty(_data.GarageCode) Then
    '                '    _GarageCode.ResultCode = "GarageCode"
    '                '    _GarageCode.ResultMessage = "No data"
    '                '    _Result.Add(_GarageCode)
    '                'Else
    '                '    _data.GarageCode = Left(_data.GarageCode, 10)


    '                'End If

    '                Dim _gcode = _data.GarageCode
    '                Dim chkgCode = (From c In dc.tblGarages Where c.GarageCode.Equals(_gcode)).FirstOrDefault()
    '                If chkgCode Is Nothing Then
    '                    _GarageCode.ResultCode = "GarageCode"
    '                    _GarageCode.ResultMessage = "Invalid GarageCode"
    '                    _Result.Add(_GarageCode)
    '                End If




    '            ElseIf _data.GarageType = 3 Or _data.GarageType = 4 Then

    '                'GarageName varchar(100)
    '                Dim _GarageName As New ClaimResultMessage
    '                If String.IsNullOrEmpty(_data.GarageName) Then
    '                    _GarageName.ResultCode = "GarageName"
    '                    _GarageName.ResultMessage = "No data"
    '                    _Result.Add(_GarageName)
    '                Else
    '                    _data.GarageName = Left(_data.GarageName, 100)
    '                End If
    '                'GaragePlace varchar(255)
    '                Dim _GaragePlace As New ClaimResultMessage
    '                If String.IsNullOrEmpty(_data.GaragePlace) Then
    '                    _GaragePlace.ResultCode = "GaragePlace"
    '                    _GaragePlace.ResultMessage = "No data"
    '                    _Result.Add(_GaragePlace)
    '                Else
    '                    _data.GaragePlace = Left(_data.GaragePlace, 255)
    '                End If
    '                'GarageTumbon varchar(255)
    '                Dim _GarageTumbon As New ClaimResultMessage
    '                If String.IsNullOrEmpty(_data.GarageTumbon) Then
    '                    _GarageTumbon.ResultCode = "GarageTumbon"
    '                    _GarageTumbon.ResultMessage = "No data"
    '                    _Result.Add(_GarageTumbon)
    '                Else
    '                    _data.GarageTumbon = Left(_data.GarageTumbon, 255)
    '                End If
    '                'GarageAmphur varchar(255)
    '                Dim _GarageAmphur As New ClaimResultMessage
    '                If String.IsNullOrEmpty(_data.GarageAmphur) Then
    '                    _GarageAmphur.ResultCode = "GarageAmphur"
    '                    _GarageAmphur.ResultMessage = "No data"
    '                    _Result.Add(_GarageAmphur)
    '                Else
    '                    _data.GarageAmphur = Left(_data.GarageAmphur, 255)
    '                End If
    '                'GarageProvince varchar(255)
    '                Dim _GarageProvince As New ClaimResultMessage
    '                If String.IsNullOrEmpty(_data.GarageProvince) Then
    '                    _GarageProvince.ResultCode = "GarageProvince"
    '                    _GarageProvince.ResultMessage = "No data"
    '                    _Result.Add(_GarageProvince)
    '                Else
    '                    _data.GarageProvince = Left(_data.GarageProvince, 255)
    '                End If
    '                'GarageZipcode varchar(5)
    '                Dim _GarageZipcode As New ClaimResultMessage
    '                If String.IsNullOrEmpty(_data.GarageZipcode) Then
    '                    _GarageZipcode.ResultCode = "GarageZipcode"
    '                    _GarageZipcode.ResultMessage = "No data"
    '                    _Result.Add(_GarageZipcode)
    '                Else
    '                    _data.GarageZipcode = Left(_data.GarageZipcode, 5)
    '                End If



    '            End If
    '        End If










    '        '=============================00 = Open====================================================

    '        If _ClaimStatusCode.Equals("00") Then
    '            'InsuredName varchar(255)
    '            Dim _InsuredName As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.InsuredName) Then
    '                _InsuredName.ResultCode = "InsuredName"
    '                _InsuredName.ResultMessage = "No data"
    '                _Result.Add(_InsuredName)
    '            Else
    '                _data.InsuredName = Left(_data.InsuredName, 255)
    '            End If
    '            'EffectiveDate DateTime
    '            Dim _EffectiveDate As New ClaimResultMessage
    '            If _data.EffectiveDate Is Nothing Then
    '                _EffectiveDate.ResultCode = "EffectiveDate"
    '                _EffectiveDate.ResultMessage = "Invalid Date"
    '                _Result.Add(_EffectiveDate)
    '            End If
    '            'ExpiryDate DateTime
    '            Dim _ExpiryDate As New ClaimResultMessage
    '            If _data.ExpiryDate Is Nothing Then
    '                _ExpiryDate.ResultCode = "ExpiryDate"
    '                _ExpiryDate.ResultMessage = "Invalid Date"
    '                _Result.Add(_ExpiryDate)
    '            End If
    '            'Beneficiary varchar(255)
    '            Dim _Beneficiary As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.Beneficiary) Then
    '                _Beneficiary.ResultCode = "Beneficiary"
    '                _Beneficiary.ResultMessage = "No data"
    '                _Result.Add(_Beneficiary)
    '            Else
    '                _data.Beneficiary = Left(_data.Beneficiary, 255)
    '            End If
    '            'CarBrand varchar(50)
    '            Dim _CarBrand As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.CarBrand) Then
    '                _CarBrand.ResultCode = "CarBrand"
    '                _CarBrand.ResultMessage = "No data"
    '                _Result.Add(_CarBrand)
    '            Else
    '                _data.CarBrand = Left(_data.CarBrand, 50)
    '            End If
    '            'CarModel varchar(100)
    '            Dim _CarModel As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.CarModel) Then
    '                _CarModel.ResultCode = "CarModel"
    '                _CarModel.ResultMessage = "No data"
    '                _Result.Add(_CarModel)
    '            Else
    '                _data.CarModel = Left(_data.CarModel, 100)
    '            End If
    '            'CarLicense varchar(50)
    '            Dim _CarLicense As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.CarLicense) Then
    '                _CarLicense.ResultCode = "CarLicense"
    '                _CarLicense.ResultMessage = "No data"
    '                _Result.Add(_CarLicense)
    '            Else
    '                _data.CarLicense = Left(_data.CarLicense, 50)
    '            End If
    '            'CarYear varchar(50)
    '            Dim _CarYear As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.CarYear) Then
    '                _CarYear.ResultCode = "CarYear"
    '                _CarYear.ResultMessage = "No data"
    '                _Result.Add(_CarYear)
    '            Else
    '                _data.CarYear = Left(_data.CarYear, 50)
    '            End If
    '            'ChassisNo varchar(50)
    '            Dim _ChassisNo As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.ChassisNo) Then
    '                _ChassisNo.ResultCode = "ChassisNo"
    '                _ChassisNo.ResultMessage = "No data"
    '                _Result.Add(_ChassisNo)
    '            Else
    '                _data.ChassisNo = Left(_data.ChassisNo, 50)
    '            End If
    '            'ShowRoomName varchar(100)
    '            Dim _ShowRoomName As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.ShowRoomName) Then
    '                _ShowRoomName.ResultCode = "ShowRoomName"
    '                _ShowRoomName.ResultMessage = "No data"
    '                _Result.Add(_ShowRoomName)
    '            Else
    '                _data.ShowRoomName = Left(_data.ShowRoomName, 100)
    '            End If
    '            'ShowRoomCode varchar(10)
    '            Dim _ShowRoomCode As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.ShowRoomCode) Then
    '                _ShowRoomCode.ResultCode = "ShowRoomCode"
    '                _ShowRoomCode.ResultMessage = "No data"
    '                _Result.Add(_ShowRoomCode)
    '            Else
    '                _data.ShowRoomCode = Left(_data.ShowRoomCode, 10)
    '                '=============== check showroom ==================
    '                Dim _sr = _data.ShowRoomCode
    '                'Dim chkShowRoom = (From c In dc.tblShowRooms Where c.ShowRoomCode.Equals(_sr)).FirstOrDefault()
    '                'If chkShowRoom Is Nothing Then
    '                '    _ShowRoomCode.ResultCode = "ShowRoomCode"
    '                '    _ShowRoomCode.ResultMessage = "Invalid Showroom"
    '                '    _Result.Add(_ShowRoomCode)
    '                'End If
    '            End If
    '            'ClaimNoticeDate DateTime
    '            Dim _ClaimNoticeDate As New ClaimResultMessage
    '            If _data.ClaimNoticeDate Is Nothing Then
    '                _ClaimNoticeDate.ResultCode = "ClaimNoticeDate"
    '                _ClaimNoticeDate.ResultMessage = "Invalid Date"
    '                _Result.Add(_ClaimNoticeDate)
    '            End If
    '            'ClaimDetails varchar(255)
    '            Dim _ClaimDetails As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.ClaimDetails) Then
    '                _ClaimDetails.ResultCode = "ClaimDetails"
    '                _ClaimDetails.ResultMessage = "No data"
    '                _Result.Add(_ClaimDetails)
    '            Else
    '                _data.ClaimDetails = Left(_data.ClaimDetails, 255)
    '            End If
    '            'ClaimType Int
    '            Dim _ClaimType As New ClaimResultMessage
    '            If _data.ClaimType Is Nothing Then
    '                _ClaimType.ResultCode = "ClaimType"
    '                _ClaimType.ResultMessage = "Invalid Number"
    '                _Result.Add(_ClaimType)
    '            End If
    '            'ClaimResult Int
    '            Dim _ClaimResult As New ClaimResultMessage
    '            If _data.ClaimResult Is Nothing Then
    '                _ClaimResult.ResultCode = "ClaimResult"
    '                _ClaimResult.ResultMessage = "Invalid Number"
    '                _Result.Add(_ClaimResult)
    '            End If
    '            'ClaimDamageDetails varchar(255)
    '            Dim _ClaimDamageDetails As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.ClaimDamageDetails) Then
    '                _ClaimDamageDetails.ResultCode = "ClaimDamageDetails"
    '                _ClaimDamageDetails.ResultMessage = "No data"
    '                _Result.Add(_ClaimDamageDetails)
    '            Else
    '                _data.ClaimDamageDetails = Left(_data.ClaimDamageDetails, 255)
    '            End If
    '            'IntGarageName varchar(100)
    '            Dim _IntGarageName As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.IntGarageName) Then
    '                _IntGarageName.ResultCode = "IntGarageName"
    '                _IntGarageName.ResultMessage = "No data"
    '                _Result.Add(_IntGarageName)
    '            Else
    '                _data.IntGarageName = Left(_data.IntGarageName, 100)
    '            End If
    '            'IntGarageType Int
    '            Dim _IntGarageType As New ClaimResultMessage
    '            If _data.IntGarageType Is Nothing Then
    '                _IntGarageType.ResultCode = "IntGarageType"
    '                _IntGarageType.ResultMessage = "Invalid Number"
    '                _Result.Add(_IntGarageType)
    '            End If
    '            'CallCenter varchar(10)
    '            Dim _CallCenter As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.CallCenter) Then
    '                _CallCenter.ResultCode = "CallCenter"
    '                _CallCenter.ResultMessage = "No data"
    '                _Result.Add(_CallCenter)
    '            Else
    '                _data.CallCenter = Left(_data.CallCenter, 10)
    '            End If
    '            'AccidentDate DateTime
    '            Dim _AccidentDate As New ClaimResultMessage
    '            If _data.AccidentDate Is Nothing Then
    '                _AccidentDate.ResultCode = "AccidentDate"
    '                _AccidentDate.ResultMessage = "Invalid Date"
    '                _Result.Add(_AccidentDate)
    '            End If
    '            'AccidentPlace varchar(255)
    '            Dim _AccidentPlace As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.AccidentPlace) Then
    '                _AccidentPlace.ResultCode = "AccidentPlace"
    '                _AccidentPlace.ResultMessage = "No data"
    '                _Result.Add(_AccidentPlace)
    '            Else
    '                _data.AccidentPlace = Left(_data.AccidentPlace, 255)
    '            End If
    '            'AccidentTumbon varchar(255)
    '            Dim _AccidentTumbon As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.AccidentTumbon) Then
    '                _AccidentTumbon.ResultCode = "AccidentTumbon"
    '                _AccidentTumbon.ResultMessage = "No data"
    '                _Result.Add(_AccidentTumbon)
    '            Else
    '                _data.AccidentTumbon = Left(_data.AccidentTumbon, 255)
    '            End If
    '            'AccidentAmphur varchar(255)
    '            Dim _AccidentAmphur As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.AccidentAmphur) Then
    '                _AccidentAmphur.ResultCode = "AccidentAmphur"
    '                _AccidentAmphur.ResultMessage = "No data"
    '                _Result.Add(_AccidentAmphur)
    '            Else
    '                _data.AccidentAmphur = Left(_data.AccidentAmphur, 255)
    '            End If
    '            'AccidentProvince varchar(255)
    '            Dim _AccidentProvince As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.AccidentProvince) Then
    '                _AccidentProvince.ResultCode = "AccidentProvince"
    '                _AccidentProvince.ResultMessage = "No data"
    '                _Result.Add(_AccidentProvince)
    '            Else
    '                _data.AccidentProvince = Left(_data.AccidentProvince, 255)
    '            End If
    '            'AccidentZipcode varchar(5)
    '            Dim _AccidentZipcode As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.AccidentZipcode) Then
    '                _AccidentZipcode.ResultCode = "AccidentZipcode"
    '                _AccidentZipcode.ResultMessage = "No data"
    '                _Result.Add(_AccidentZipcode)
    '            Else
    '                _data.AccidentZipcode = Left(_data.AccidentZipcode, 5)
    '            End If
    '            'DriverName varchar(255)
    '            Dim _DriverName As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.DriverName) Then
    '                _DriverName.ResultCode = "DriverName"
    '                _DriverName.ResultMessage = "No data"
    '                _Result.Add(_DriverName)
    '            Else
    '                _data.DriverName = Left(_data.DriverName, 255)
    '            End If
    '            'DriverTel varchar(100)
    '            Dim _DriverTel As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.DriverTel) Then
    '                _DriverTel.ResultCode = "DriverTel"
    '                _DriverTel.ResultMessage = "No data"
    '                _Result.Add(_DriverTel)
    '            Else
    '                _data.DriverTel = Left(_data.DriverTel, 100)
    '            End If
    '            'NoticeName varchar(255)
    '            Dim _NoticeName As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.NoticeName) Then
    '                _NoticeName.ResultCode = "NoticeName"
    '                _NoticeName.ResultMessage = "No data"
    '                _Result.Add(_NoticeName)
    '            Else
    '                _data.NoticeName = Left(_data.NoticeName, 255)
    '            End If
    '            'NoticeTel varchar(100)
    '            Dim _NoticeTel As New ClaimResultMessage
    '            If String.IsNullOrEmpty(_data.NoticeTel) Then
    '                _NoticeTel.ResultCode = "NoticeTel"
    '                _NoticeTel.ResultMessage = "No data"
    '                _Result.Add(_NoticeTel)
    '            Else
    '                _data.NoticeTel = Left(_data.NoticeTel, 100)
    '            End If


    '            ''CarRepairDate DateTime
    '            'Dim _CarRepairDate As New ClaimResultMessage
    '            'If _data.CarRepairDate Is Nothing Then
    '            '    _CarRepairDate.ResultCode = "CarRepairDate"
    '            '    _CarRepairDate.ResultMessage = "Invalid Date"
    '            '    _Result.Add(_CarRepairDate)
    '            'End If
    '            ''CarReceiveDate DateTime
    '            'Dim _CarReceiveDate As New ClaimResultMessage
    '            'If _data.CarReceiveDate Is Nothing Then
    '            '    _CarReceiveDate.ResultCode = "CarReceiveDate"
    '            '    _CarReceiveDate.ResultMessage = "Invalid Date"
    '            '    _Result.Add(_CarReceiveDate)
    '            'End If


    '        End If

    '        'ConsentFormNo()'	varchar(50)
    '        Dim _ConsentFormNo As New ClaimResultMessage
    '        If String.IsNullOrEmpty(_data.ConsentFormNo) Then
    '        Else
    '            _data.ConsentFormNo = Left(_data.ConsentFormNo, 50)
    '        End If

    '        'ConsentFormFileName()'	varchar(50)
    '        Dim _ConsentFormFileName As New ClaimResultMessage
    '        If String.IsNullOrEmpty(_data.ConsentFormFileName) Then

    '        Else
    '            _data.ConsentFormFileName = Left(_data.ConsentFormFileName, 50)
    '            ''ConsentFormData()'	binary(1000)
    '            'Dim _ConsentFormData As New ClaimResultMessage
    '            'If _data.ConsentFormData Is Nothing Then
    '            '    _ConsentFormData.ResultCode = "ConsentFormData"
    '            '    _ConsentFormData.ResultMessage = "No data"
    '            '    _Result.Add(_ConsentFormData)
    '            'End If
    '        End If

    '        '==================02 = Payment===================
    '        If _ClaimStatusCode.Equals("02") Then
    '            ''CarRepairDate DateTime
    '            'Dim _CarRepairDate As New ClaimResultMessage
    '            'If _data.CarRepairDate Is Nothing Then
    '            '    _CarRepairDate.ResultCode = "CarRepairDate"
    '            '    _CarRepairDate.ResultMessage = "Invalid Date"
    '            '    _Result.Add(_CarRepairDate)
    '            'End If
    '            ''CarReceiveDate DateTime
    '            'Dim _CarReceiveDate As New ClaimResultMessage
    '            'If _data.CarReceiveDate Is Nothing Then
    '            '    _CarReceiveDate.ResultCode = "CarReceiveDate"
    '            '    _CarReceiveDate.ResultMessage = "Invalid Date"
    '            '    _Result.Add(_CarReceiveDate)
    '            'End If
    '        End If

    '        '=====================================
    '        'If _data.ClaimStatus.Equals("01") _
    '        '    Or _data.ClaimStatus.Equals("02") _
    '        '    Or _data.ClaimStatus.Equals("98") _
    '        '    Or _data.ClaimStatus.Equals("99") Then

    '        'PartsSerialNo()   '	varchar(50)
    '        Dim _PartsSerialNo As New ClaimResultMessage
    '        If String.IsNullOrEmpty(_data.PartsSerialNo) Then
    '            '_PartsSerialNo.ResultCode = "PartsSerialNo"
    '            '_PartsSerialNo.ResultMessage = "No data"
    '            '_Result.Add(_PartsSerialNo)
    '        Else
    '            _data.PartsSerialNo = Left(_data.PartsSerialNo, 50)
    '        End If
    '        'PartsDealerName() '	varchar(255)
    '        Dim _PartsDealerName As New ClaimResultMessage
    '        If String.IsNullOrEmpty(_data.PartsDealerName) Then
    '            '_PartsDealerName.ResultCode = "PartsDealerName"
    '            '_PartsDealerName.ResultMessage = "No data"
    '            '_Result.Add(_PartsDealerName)
    '        Else
    '            _data.PartsDealerName = Left(_data.PartsDealerName, 255)
    '        End If
    '        'PaymentNo()   '	varchar(50)
    '        Dim _PaymentNo As New ClaimResultMessage
    '        If String.IsNullOrEmpty(_data.PaymentNo) Then
    '            '_PaymentNo.ResultCode = "PaymentNo"
    '            '_PaymentNo.ResultMessage = "No data"
    '            '_Result.Add(_PaymentNo)
    '        Else
    '            _data.PaymentNo = Left(_data.PaymentNo, 50)
    '        End If
    '        'PaymentDetails()  '	varchar(255)
    '        Dim _PaymentDetails As New ClaimResultMessage
    '        If String.IsNullOrEmpty(_data.PaymentDetails) Then
    '            '_PaymentDetails.ResultCode = "PaymentDetails"
    '            '_PaymentDetails.ResultMessage = "No data"
    '            '_Result.Add(_PaymentDetails)
    '        Else
    '            _data.PaymentDetails = Left(_data.PaymentDetails, 255)
    '        End If
    '        'Remark()  '	varchar(255)
    '        Dim _Remark As New ClaimResultMessage
    '        If String.IsNullOrEmpty(_data.Remark) Then
    '            '_Remark.ResultCode = "Remark"
    '            '_Remark.ResultMessage = "No data"
    '            '_Result.Add(_Remark)
    '        Else
    '            _data.Remark = Left(_data.Remark, 255)
    '        End If
    '        'End If



    '        '======================================================================================================
    '        'Amount1 float
    '        Dim _Amount1 As New ClaimResultMessage
    '        If _data.Amount1 Is Nothing Then
    '            _Amount1.ResultCode = "Amount1"
    '            _Amount1.ResultMessage = "Invalid Number"
    '            _Result.Add(_Amount1)
    '        End If
    '        'Amount2 float
    '        Dim _Amount2 As New ClaimResultMessage
    '        If _data.Amount2 Is Nothing Then
    '            _Amount2.ResultCode = "Amount2"
    '            _Amount2.ResultMessage = "Invalid Number"
    '            _Result.Add(_Amount2)
    '        End If
    '        'Amount3 float
    '        Dim _Amount3 As New ClaimResultMessage
    '        If _data.Amount3 Is Nothing Then
    '            _Amount3.ResultCode = "Amount3"
    '            _Amount3.ResultMessage = "Invalid Number"
    '            _Result.Add(_Amount3)
    '        End If
    '        'OS_Claims float
    '        Dim _OS_Claims As New ClaimResultMessage
    '        If _data.OS_Claims Is Nothing Then
    '            _OS_Claims.ResultCode = "OS_Claims"
    '            _OS_Claims.ResultMessage = "Invalid Number"
    '            _Result.Add(_OS_Claims)
    '        End If
    '        'Acc_ClaimsPayment	float
    '        Dim _Acc_ClaimsPayment As New ClaimResultMessage
    '        If _data.Acc_ClaimsPayment Is Nothing Then
    '            _Acc_ClaimsPayment.ResultCode = "Acc_ClaimsPayment"
    '            _Acc_ClaimsPayment.ResultMessage = "Invalid Number"
    '            _Result.Add(_Acc_ClaimsPayment)
    '        End If
    '        'Incurred_Claims float
    '        Dim _Incurred_Claims As New ClaimResultMessage
    '        If _data.Incurred_Claims Is Nothing Then
    '            _Incurred_Claims.ResultCode = "Incurred_Claims"
    '            _Incurred_Claims.ResultMessage = "Invalid Number"
    '            _Result.Add(_Incurred_Claims)
    '        End If


    '    End Using

    '    Return _Result.ToList()
    'End Function


    Public Function ValidateData(ByVal _obj As Object, ByVal _objType As objType) As Object

        If _obj IsNot Nothing AndAlso _obj.GetType().ToString().ToLower().Equals("system.dbnull") Then

            'If _obj = Nothing Then
            Select Case _objType
                Case objType._Integer
                    _obj = Convert.ToInt32("0")

                Case objType._Double
                    _obj = Convert.ToDouble("0.00")

                    'Case objType._DateTime
                    '    Dim result = DateTime.TryParse(_obj, _obj)

                Case objType._String

                    _obj = ""

            End Select

        Else
            Try
                Select Case _objType
                    Case objType._Integer
                        Dim result = Integer.TryParse(_obj, _obj)

                    Case objType._Double
                        Dim result = Double.TryParse(_obj, _obj)

                    Case objType._DateTime
                        Dim result = DateTime.TryParse(_obj, _obj)
                    Case objType._String
                        'Dim result = _obj.ToString()

                End Select
            Catch ex As Exception
                _obj = Nothing
            End Try


        End If

        Return _obj
    End Function



    Enum objType
        _Integer
        _DateTime
        _String
        _Double
    End Enum
End Class