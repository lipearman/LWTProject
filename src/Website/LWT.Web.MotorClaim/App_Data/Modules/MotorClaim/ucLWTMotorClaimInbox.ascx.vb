Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Web
Imports System.Web.UI
Imports DevExpress.Data.Filtering
Imports DevExpress.Web
Imports System.Xml
Imports Portal.Components

'Imports DevExpress.Spreadsheet
Imports System.IO

Imports NPOI.HSSF.UserModel
Imports NPOI.HPSF
Imports NPOI.SS.UserModel
Imports NPOI.SS.Util
Imports NPOI.HSSF.Util

Imports NPOI.XSSF.UserModel
Imports Excel
Imports System.Data
Imports Context
Imports LWT.Website
Imports System.Net.Mail
Imports MotorClaimWebService


Partial Class Modules_ucLWTMotorClaimInbox
    Inherits PortalModuleControl
    Dim _NoticeCode As String = "B0025"
    Private Const PreviewMessageFormat As String = "<div class='MailPreview'>" & "<div class='Subject'>{0}</div>" & "<div class='Info'>" & "<div>From: {1}</div>" & "<div>To: {2}</div>" & "<div>Date: {3:g}</div>" & "</div>" & "<div class='Separator'></div>" & "<div class='Body'>{4}</div>" & "</div>", ReplyMessageFormat As String = "Hi,<br/><br/><br/><br/>Thanks,<br/>Thomas Hardy<br/><br/><br/>----- Original Message -----<br/>Subject: {0}<br/>From: {1}<br/>To: {2}<br/>Date: {3:g}<br/>{4}", NotFoundMessageFormat As String = "<h1>Can't find message with the key={0}</h1>"

    Protected ReadOnly Property SearchText() As String
        Get
            Return Utils.GetSearchText(Page)
        End Get
    End Property
    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Init
        If (Not IsPostBack) Then
        End If


        If (Not Page.IsPostBack) Then
            MailTree.SelectedNode = MailTree.Nodes.FindByText("Claim Inbox")
        End If
    End Sub

    Private Function ShouldBindGrid() As Boolean
        Return (Not Page.IsCallback) OrElse MailGrid.IsCallback
    End Function
    Protected Sub Page_Load(ByVal s As Object, ByVal e As EventArgs) Handles Me.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If

        If Not System.IO.Directory.Exists(Server.MapPath("~/App_Data/ConsentForms/" & HttpContext.Current.User.Identity.Name & "/ConsentForms")) Then
            System.IO.Directory.CreateDirectory(Server.MapPath("~/App_Data/ConsentForms/" & HttpContext.Current.User.Identity.Name & "/ConsentForms"))
        End If

        With ASPxFileManager1
            .Settings.RootFolder = "~/App_Data/ConsentForms"

            .SettingsEditing.AllowCreate = True
            .SettingsEditing.AllowRename = True
            .SettingsEditing.AllowMove = True

            .SettingsUpload.AdvancedModeSettings.EnableMultiSelect = True

        End With




        ActionMenuDataSource.XPath = String.Format("Pages/{0}/Item", Utils.CurrentPageName)
        ActionMenu.ShowPopOutImages = DevExpress.Utils.DefaultBoolean.True

        If ShouldBindGrid() Then
            BindGrid()
        End If

        If Not IsPostBack Then
            Session("MCGUID") = Nothing
            Session("MCTYPE") = Nothing
        End If

    End Sub
    Protected Sub ActionMenu_ItemDataBound(ByVal sender As Object, ByVal e As MenuItemEventArgs)
        Dim itemHierarchyData As IHierarchyData = CType(e.Item.DataItem, IHierarchyData)
        Dim element = CType(itemHierarchyData.Item, XmlElement)

        Dim classAttr = element.Attributes("SpriteClassName")

        e.Item.Image.SpriteProperties.CssClass = classAttr.Value

    End Sub
    Private Sub BindGrid()
        MailGrid.DataSource = SelectMessages()
        MailGrid.DataBind()
        MailGrid.ExpandAll()
    End Sub
    Private Function SelectMessages() As List(Of V_ClaimTransactionData)
        Dim result = MotorClaimModel.DataProvider.V_ClaimTransactionData.AsQueryable()

        Select Case MailTree.SelectedNode.Text
            Case "00 - Open"
                result = result.Where(Function(m) m.ClaimStatus.Equals("00") And m.RankNo = 1).OrderByDescending(Function(m) m.TRID)
            Case "01 - Reserve"
                result = result.Where(Function(m) m.ClaimStatus.Equals("01")).OrderByDescending(Function(m) m.TRID)
            Case "02 - Payment"
                result = result.Where(Function(m) m.ClaimStatus.Equals("02")).OrderByDescending(Function(m) m.TRID)
            Case "99 - Close"
                result = result.Where(Function(m) m.ClaimStatus.Equals("99")).OrderByDescending(Function(m) m.TRID)
            Case "98 - ReOpen"
                result = result.Where(Function(m) m.ClaimStatus.Equals("98")).OrderByDescending(Function(m) m.TRID)
            Case Else
                result = result.Where(Function(m) Not m.ClaimStatus.Equals("CF")).OrderByDescending(Function(m) m.TRID)
        End Select

        Return result.ToList()

    End Function

    Protected Sub MailPreviewPanel_Callback(ByVal sender As Object, ByVal e As CallbackEventArgsBase) Handles MailPreviewPanel.Callback
        Dim id As Integer



        Dim sb As New StringBuilder()


        Dim text = String.Format(NotFoundMessageFormat, e.Parameter)

        If Integer.TryParse(e.Parameter, id) Then

            Dim data = MotorClaimModel.DataProvider.tblClaimTransaction_Data.AsQueryable()

            Dim MyData = data.Where(Function(m) m.TRID.Equals(id)).FirstOrDefault()

            formPreview.DataSource = MyData
            formPreview.DataBind()

            If MyData.ClaimStatus.Equals("CF") Then


                formPreview.Items(formPreview.Items.Count - 1).Visible = False
            Else

                formPreview.Items(formPreview.Items.Count - 1).Visible = True

            End If


            If MyData.Status.Value = False Then
                Dim result = MotorClaimModel.DataProvider.tblClaimTransaction_Result.AsQueryable()
                ASPxGridPreview_Result.DataSource = result.Where(Function(m) m.GUID.Equals(MyData.GUID)).ToList()
                ASPxGridPreview_Result.DataBind()

                ASPxGridPreview_Result.Visible = True

            Else
                ASPxGridPreview_Result.Visible = False
            End If


            formPreview.Enabled = Not MyData.Status.Value

            'ClaimStatus.ReadOnly = Not MyData.Status.Value
            'TempPolicy.ReadOnly = Not MyData.Status.Value
            'RefNo.ReadOnly = Not MyData.Status.Value
            'Version.ReadOnly = Not MyData.Status.Value
            'PolicyNo.ReadOnly = Not MyData.Status.Value
            'PolicyYear.ReadOnly = Not MyData.Status.Value
            'ClaimNo.ReadOnly = Not MyData.Status.Value
            'TransactionDate.ReadOnly = Not MyData.Status.Value
            'Unwriter.ReadOnly = Not MyData.Status.Value
            'InsuredName.ReadOnly = Not MyData.Status.Value
            'EffectiveDate.ReadOnly = Not MyData.Status.Value
            'ExpiryDate.ReadOnly = Not MyData.Status.Value
            'Beneficiary.ReadOnly = Not MyData.Status.Value
            'CarBrand.ReadOnly = Not MyData.Status.Value
            'CarModel.ReadOnly = Not MyData.Status.Value
            'CarLicense.ReadOnly = Not MyData.Status.Value
            'CarYear.ReadOnly = Not MyData.Status.Value
            'ChassisNo.ReadOnly = Not MyData.Status.Value
            'ShowRoomName.ReadOnly = Not MyData.Status.Value
            'ShowRoomCode.ReadOnly = Not MyData.Status.Value
            'ClaimNoticeDate.ReadOnly = Not MyData.Status.Value
            'ClaimNoticeTime.ReadOnly = Not MyData.Status.Value
            'ClaimDetails.ReadOnly = Not MyData.Status.Value
            'ClaimType.ReadOnly = Not MyData.Status.Value
            'ClaimResult.ReadOnly = Not MyData.Status.Value
            'ClaimDamageDetails.ReadOnly = Not MyData.Status.Value
            'CallCenter.ReadOnly = Not MyData.Status.Value
            'AccidentDate.ReadOnly = Not MyData.Status.Value
            'AccidentTime.ReadOnly = Not MyData.Status.Value
            'AccidentPlace.ReadOnly = Not MyData.Status.Value
            'AccidentTumbon.ReadOnly = Not MyData.Status.Value
            'AccidentAmphur.ReadOnly = Not MyData.Status.Value
            'AccidentProvince.ReadOnly = Not MyData.Status.Value
            'AccidentZipcode.ReadOnly = Not MyData.Status.Value
            'DriverName.ReadOnly = Not MyData.Status.Value
            'DriverTel.ReadOnly = Not MyData.Status.Value
            'NoticeName.ReadOnly = Not MyData.Status.Value
            'NoticeTel.ReadOnly = Not MyData.Status.Value
            'GarageType.ReadOnly = Not MyData.Status.Value
            'GarageCode.ReadOnly = Not MyData.Status.Value
            'GarageName.ReadOnly = Not MyData.Status.Value
            'GaragePlace.ReadOnly = Not MyData.Status.Value
            'GarageTumbon.ReadOnly = Not MyData.Status.Value
            'GarageAmphur.ReadOnly = Not MyData.Status.Value
            'GarageProvince.ReadOnly = Not MyData.Status.Value
            'GarageZipcode.ReadOnly = Not MyData.Status.Value
            'ConsentFormNo.ReadOnly = Not MyData.Status.Value
            'CarRepairDate.ReadOnly = Not MyData.Status.Value
            'CarReceiveDate.ReadOnly = Not MyData.Status.Value
            'PartsDealerName.ReadOnly = Not MyData.Status.Value
            'PaymentDetails.ReadOnly = Not MyData.Status.Value
            'Amount1.ReadOnly = Not MyData.Status.Value
            'Amount2.ReadOnly = Not MyData.Status.Value
            'Amount3.ReadOnly = Not MyData.Status.Value
            'Remark.ReadOnly = Not MyData.Status.Value


        End If

    End Sub


    Protected Sub MailGrid_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs) Handles MailGrid.CustomCallback
        If String.IsNullOrEmpty(e.Parameters) Then
            Return
        End If
        Dim args = e.Parameters.Split("|"c)
        If args(0) = "FolderChanged" OrElse args(0) = "Search" Then
            Dim filter As String = If(args(0) = "Search", SearchText, String.Empty)
            MailGrid.SearchPanelFilter = filter
            BindGrid()

        End If

    End Sub


    Protected Sub btnDownloadFormat_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDownloadFormat.Click
        Dim fileName = String.Format("{0}.xlsx", System.Guid.NewGuid.ToString())
        Dim filePath = String.Format("{0}\{1}", Server.MapPath("~/Template"), "ImportFormat.xlsx")

        Response.ClearContent()
        Response.AddHeader("content-disposition", "attachment; filename=" & fileName)
        Response.ContentType = "application/octet-stream"
        Response.WriteFile(filePath)
        Response.End()

    End Sub


    Protected Sub frmImport_Upload_FileUploadComplete(ByVal sender As Object, ByVal e As DevExpress.Web.FileUploadCompleteEventArgs) Handles frmImport_UploadFile.FileUploadComplete

        If btnDownloadFormat.Page Is Nothing AndAlso frmImport_UploadFile.Page IsNot Nothing Then

            Dim _GUID As String = System.Guid.NewGuid().ToString()
            Session("MCGUID") = _GUID





            Dim FilePath = Page.MapPath("~/UploadFiles/")


            If Not System.IO.Directory.Exists(FilePath) Then
                System.IO.Directory.CreateDirectory(FilePath)
            End If

         


            Using conn As New DataClasses_MotorClaimDataExt()
                Dim _data As New List(Of ClaimUpload_DataObject)

                If e.UploadedFile.FileName.ToLower().IndexOf(".xlsx") > -1 Then

                    Dim FileName = String.Format(FilePath & "/{0}.xlsx", _GUID)
                    Dim Extension As String = Path.GetExtension(e.UploadedFile.FileName)
                    e.UploadedFile.SaveAs(FileName)

                    _data = conn.MotorClaimUploadResult(_GUID, "xlsx")
                    Session("MCTYPE") = "xlsx"




                ElseIf e.UploadedFile.FileName.ToLower().IndexOf(".csv") > -1 Then
                    Dim FileName = String.Format(FilePath & "/{0}.csv", _GUID)
                    Dim Extension As String = Path.GetExtension(e.UploadedFile.FileName)
                    e.UploadedFile.SaveAs(FileName)


                    _data = conn.MotorClaimUploadResult(_GUID, "csv")
                    Session("MCTYPE") = "csv"


                End If




                Dim _result1 = (From c In _data).ToList()
                Dim _result2 = (From c In _data Where c.Status.Equals("Complete")).ToList()
                Dim _result3 = (From c In _data Where c.Status.Equals("Incomplete")).ToList()
                If _result3.Count > 0 Then

                    'All           10 รายการ
                    'Complete      9 รายการ
                    'Incomplete    1 รายการ
                    Dim sb As New StringBuilder

                    sb.AppendLine("Result::")
                    sb.AppendLine(String.Format("- All {0} รายการ", _result1.Count.ToString()))
                    sb.AppendLine(String.Format("- Complete {0} รายการ", _result2.Count.ToString()))
                    sb.AppendLine(String.Format("- Incomplete {0} รายการ", _result3.Count.ToString()))


                    e.CallbackData = sb.ToString()

                Else

                    Dim ws As New MotorClaimWebService
                    Dim _newdata As New List(Of ClaimTransaction_DataObject)
                    For Each _item In _data
                        '=========================== add new ========================
                        Dim _itemdata As New ClaimTransaction_DataObject
                        With _itemdata
                            .ClaimStatus = ws.ValidateData(_item.ClaimStatus, objType._String)
                            .TempPolicy = ws.ValidateData(_item.TempPolicy, objType._String)
                            .RefNo = ws.ValidateData(_item.RefNo, objType._String)
                            .Version = ws.ValidateData(_item.Version, objType._Integer) 'int
                            .PolicyNo = ws.ValidateData(_item.PolicyNo, objType._String)
                            .PolicyYear = ws.ValidateData(_item.PolicyYear, objType._Integer) 'int
                            .ClaimNo = ws.ValidateData(_item.ClaimNo, objType._String)
                            .TransactionDate = ws.ValidateData(_item.TransactionDate, objType._String)
                            .Unwriter = ws.ValidateData(_item.Unwriter, objType._String)
                            .InsuredName = ws.ValidateData(_item.InsuredName, objType._String)
                            .EffectiveDate = ws.ValidateData(_item.EffectiveDate, objType._String)
                            .ExpiryDate = ws.ValidateData(_item.ExpiryDate, objType._String)
                            .Beneficiary = ws.ValidateData(_item.Beneficiary, objType._String)
                            .CarBrand = ws.ValidateData(_item.CarBrand, objType._String)
                            .CarModel = ws.ValidateData(_item.CarModel, objType._String)
                            .CarLicense = ws.ValidateData(_item.CarLicense, objType._String)
                            .CarYear = ws.ValidateData(_item.CarYear, objType._String)
                            .ChassisNo = ws.ValidateData(_item.ChassisNo, objType._String)
                            .ShowRoomName = ws.ValidateData(_item.ShowRoomName, objType._String)
                            .ShowRoomCode = ws.ValidateData(_item.ShowRoomCode, objType._String)
                            .ClaimNoticeDate = ws.ValidateData(_item.ClaimNoticeDate, objType._String)
                            .ClaimNoticeTime = ws.ValidateData(_item.ClaimNoticeTime, objType._String)
                            .ClaimDetails = ws.ValidateData(_item.ClaimDetails, objType._String)
                            .ClaimType = ws.ValidateData(_item.ClaimType, objType._Integer) 'int
                            .ClaimResult = ws.ValidateData(_item.ClaimResult, objType._Integer) 'int
                            .ClaimDamageDetails = ws.ValidateData(_item.ClaimDamageDetails, objType._String)
                            .CallCenter = ws.ValidateData(_item.CallCenter, objType._String)
                            .AccidentDate = ws.ValidateData(_item.AccidentDate, objType._String)
                            .AccidentTime = ws.ValidateData(_item.AccidentTime, objType._String)
                            .AccidentPlace = ws.ValidateData(_item.AccidentPlace, objType._String)
                            .AccidentTumbon = ws.ValidateData(_item.AccidentTumbon, objType._String)
                            .AccidentAmphur = ws.ValidateData(_item.AccidentAmphur, objType._String)
                            .AccidentProvince = ws.ValidateData(_item.AccidentProvince, objType._String)
                            .AccidentZipcode = ws.ValidateData(_item.AccidentZipcode, objType._String)
                            .DriverName = ws.ValidateData(_item.DriverName, objType._String)
                            .DriverTel = ws.ValidateData(_item.DriverTel, objType._String)
                            .NoticeName = ws.ValidateData(_item.NoticeName, objType._String)
                            .NoticeTel = ws.ValidateData(_item.NoticeTel, objType._String)
                            .GarageType = ws.ValidateData(_item.GarageType, objType._Integer) 'int
                            .GarageCode = ws.ValidateData(_item.GarageCode, objType._String)
                            .GarageName = ws.ValidateData(_item.GarageName, objType._String)
                            .GaragePlace = ws.ValidateData(_item.GaragePlace, objType._String)
                            .GarageTumbon = ws.ValidateData(_item.GarageTumbon, objType._String)
                            .GarageAmphur = ws.ValidateData(_item.GarageAmphur, objType._String)
                            .GarageProvince = ws.ValidateData(_item.GarageProvince, objType._String)
                            .GarageZipcode = ws.ValidateData(_item.GarageZipcode, objType._String)
                            .CarRepairDate = ws.ValidateData(_item.CarRepairDate, objType._String)
                            .CarReceiveDate = ws.ValidateData(_item.CarReceiveDate, objType._String)
                            .ConsentFormNo = ws.ValidateData(_item.ConsentFormNo, objType._String)
                            .PartsDealerName = ws.ValidateData(_item.PartsDealerName, objType._String)
                            .PaymentDetails = ws.ValidateData(_item.PaymentDetails, objType._String)
                            .Amount1 = ws.ValidateData(_item.Amount1, objType._Double) 'float
                            .Amount2 = ws.ValidateData(_item.Amount2, objType._Double) 'float
                            .Amount3 = ws.ValidateData(_item.Amount3, objType._Double) 'float
                            .Remark = ws.ValidateData(_item.Remark, objType._String)
                        End With
                        _newdata.Add(_itemdata)
                    Next

                    Dim _result = ws.GetMotorClaim(_newdata)

                    e.CallbackData = String.Format("success {0} รายการ", _newdata.Count)
                End If
            End Using






        End If
    End Sub


    'Private Function GetExcelSheetsXlsX(ByVal FilePath As String) As String
    '    Dim _GUID As String = System.Guid.NewGuid().ToString()
    '    Dim _data As New List(Of ClaimTransaction_DataObject)
    '    Dim _ClaimTransaction_Data As New List(Of tblClaimTransaction_Data)
    '    Dim _ClaimResultMessage As New List(Of ClaimResultMessage)



    '    Using ws As New MotorClaimWebService
    '        Using File = New FileStream(FilePath, FileMode.Open, FileAccess.Read)

    '            ''1. Reading from a binary Excel file ('97-2003 format; *.xls)
    '            'Dim excelReader As IExcelDataReader = ExcelReaderFactory.CreateBinaryReader(Stream)
    '            '...
    '            '2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
    '            Dim excelReader As IExcelDataReader = ExcelReaderFactory.CreateOpenXmlReader(File)
    '            '...
    '            '3. DataSet - The result of each spreadsheet will be created in the result.Tables
    '            'Dim result As DataSet = excelReader.AsDataSet()
    '            '...
    '            '4. DataSet - Create column names from first row
    '            excelReader.IsFirstRowAsColumnNames = True
    '            Dim result As DataSet = excelReader.AsDataSet()

    '            For Each irow As DataRow In result.Tables(0).Rows
    '                Dim _ClaimStatus = irow(0)
    '                Dim _TempPolicy = irow(1)
    '                Dim _RefNo = irow(2)
    '                Dim _Version = irow(3)
    '                Dim _PolicyNo = irow(4)
    '                Dim _PolicyYear = irow(5)
    '                Dim _ClaimNo = irow(6)
    '                Dim _TransactionDate = irow(7)
    '                Dim _Unwriter = irow(8)
    '                Dim _InsuredName = irow(9)
    '                Dim _EffectiveDate = irow(10)
    '                Dim _ExpiryDate = irow(11)
    '                Dim _Beneficiary = irow(12)
    '                Dim _CarBrand = irow(13)
    '                Dim _CarModel = irow(14)
    '                Dim _CarLicense = irow(15)
    '                Dim _CarYear = irow(16)
    '                Dim _ChassisNo = irow(17)
    '                Dim _ShowRoomName = irow(18)
    '                Dim _ShowRoomCode = irow(19)
    '                Dim _ClaimNoticeDate = irow(20)
    '                Dim _ClaimNoticeTime = irow(21)
    '                Dim _ClaimDetails = irow(22)
    '                Dim _ClaimType = irow(23)
    '                Dim _ClaimResult = irow(24)
    '                Dim _ClaimDamageDetails = irow(25)
    '                Dim _CallCenter = irow(26)
    '                Dim _AccidentDate = irow(27)
    '                Dim _AccidentTime = irow(28)
    '                Dim _AccidentPlace = irow(29)
    '                Dim _AccidentTumbon = irow(30)
    '                Dim _AccidentAmphur = irow(31)
    '                Dim _AccidentProvince = irow(32)
    '                Dim _AccidentZipcode = irow(33)
    '                Dim _DriverName = irow(34)
    '                Dim _DriverTel = irow(35)
    '                Dim _NoticeName = irow(36)
    '                Dim _NoticeTel = irow(37)
    '                Dim _GarageType = irow(38)
    '                Dim _GarageCode = irow(39)
    '                Dim _GarageName = irow(40)
    '                Dim _GaragePlace = irow(41)
    '                Dim _GarageTumbon = irow(42)
    '                Dim _GarageAmphur = irow(43)
    '                Dim _GarageProvince = irow(44)
    '                Dim _GarageZipcode = irow(45)
    '                Dim _CarRepairDate = irow(46)
    '                Dim _CarReceiveDate = irow(47)
    '                Dim _ConsentFormNo = irow(48)
    '                Dim _PartsDealerName = irow(49)
    '                Dim _PaymentDetails = irow(50)
    '                Dim _Amount1 = irow(51)
    '                Dim _Amount2 = irow(52)
    '                Dim _Amount3 = irow(53)
    '                Dim _Remark = irow(54)

    '                '======================= Validate ========================================
    '                Dim _tblClaimData As New tblClaimTransaction_Data()
    '                With _tblClaimData
    '                    .TRNo = _RefNo.ToString()
    '                    '.ClaimStatus = _ClaimStatus.ToString()
    '                    '.TempPolicy = _TempPolicy.ToString()
    '                    '.RefNo = _RefNo.ToString()
    '                    '.Version = _Version.ToString()
    '                    '.PolicyNo = _PolicyNo.ToString()
    '                    '.PolicyYear = _PolicyYear.ToString()
    '                    '.ClaimNo = _ClaimNo.ToString()
    '                    '.TransactionDate = _TransactionDate.ToString()
    '                    '.Unwriter = _Unwriter.ToString()
    '                    '.InsuredName = _InsuredName.ToString()
    '                    '.EffectiveDate = _EffectiveDate.ToString()
    '                    '.ExpiryDate = _ExpiryDate.ToString()
    '                    '.Beneficiary = _Beneficiary.ToString()
    '                    '.CarBrand = _CarBrand.ToString()
    '                    '.CarModel = _CarModel.ToString()
    '                    '.CarLicense = _CarLicense.ToString()
    '                    '.CarYear = _CarYear.ToString()
    '                    '.ChassisNo = _ChassisNo.ToString()
    '                    '.ShowRoomName = _ShowRoomName.ToString()
    '                    '.ShowRoomCode = _ShowRoomCode.ToString()
    '                    '.ClaimNoticeDate = _ClaimNoticeDate.ToString()
    '                    '.ClaimNoticeTime = _ClaimNoticeTime.ToString()
    '                    '.ClaimDetails = _ClaimDetails.ToString()
    '                    '.ClaimType = _ClaimType.ToString()
    '                    '.ClaimResult = _ClaimResult.ToString()
    '                    '.ClaimDamageDetails = _ClaimDamageDetails.ToString()
    '                    '.CallCenter = _CallCenter.ToString()
    '                    '.AccidentDate = _AccidentDate.ToString()
    '                    '.AccidentTime = _AccidentTime.ToString()
    '                    '.AccidentPlace = _AccidentPlace.ToString()
    '                    '.AccidentTumbon = _AccidentTumbon.ToString()
    '                    '.AccidentAmphur = _AccidentAmphur.ToString()
    '                    '.AccidentProvince = _AccidentProvince.ToString()
    '                    '.AccidentZipcode = _AccidentZipcode.ToString()
    '                    '.DriverName = _DriverName.ToString()
    '                    '.DriverTel = _DriverTel.ToString()
    '                    '.NoticeName = _NoticeName.ToString()
    '                    '.NoticeTel = _NoticeTel.ToString()
    '                    '.GarageType = _GarageType.ToString()
    '                    '.GarageCode = _GarageCode.ToString()
    '                    '.GarageName = _GarageName.ToString()
    '                    '.GaragePlace = _GaragePlace.ToString()
    '                    '.GarageTumbon = _GarageTumbon.ToString()
    '                    '.GarageAmphur = _GarageAmphur.ToString()
    '                    '.GarageProvince = _GarageProvince.ToString()
    '                    '.GarageZipcode = _GarageZipcode.ToString()
    '                    '.CarRepairDate = _CarRepairDate.ToString()
    '                    '.CarReceiveDate = _CarReceiveDate.ToString()
    '                    '.ConsentFormNo = _ConsentFormNo.ToString()
    '                    '.PartsDealerName = _PartsDealerName.ToString()
    '                    '.PaymentDetails = _PaymentDetails.ToString()
    '                    '.Amount1 = _Amount1.ToString()
    '                    '.Amount2 = _Amount2.ToString()
    '                    '.Amount3 = _Amount3.ToString()
    '                    '.Remark = _Remark.ToString()
    '                    .ClaimStatus = ws.ValidateData(_ClaimStatus, objType._String)
    '                    .TempPolicy = ws.ValidateData(_TempPolicy, objType._String)
    '                    .RefNo = ws.ValidateData(_RefNo, objType._String)
    '                    .Version = ws.ValidateData(_Version, objType._Integer) 'int
    '                    .PolicyNo = ws.ValidateData(_PolicyNo, objType._String)
    '                    .PolicyYear = ws.ValidateData(_PolicyYear, objType._Integer) 'int
    '                    .ClaimNo = ws.ValidateData(_ClaimNo, objType._String)
    '                    .TransactionDate = ws.ValidateData(_TransactionDate, objType._String)
    '                    .Unwriter = ws.ValidateData(_Unwriter, objType._String)
    '                    .InsuredName = ws.ValidateData(_InsuredName, objType._String)
    '                    .EffectiveDate = ws.ValidateData(_EffectiveDate, objType._String)
    '                    .ExpiryDate = ws.ValidateData(_ExpiryDate, objType._String)
    '                    .Beneficiary = ws.ValidateData(_Beneficiary, objType._String)
    '                    .CarBrand = ws.ValidateData(_CarBrand, objType._String)
    '                    .CarModel = ws.ValidateData(_CarModel, objType._String)
    '                    .CarLicense = ws.ValidateData(_CarLicense, objType._String)
    '                    .CarYear = ws.ValidateData(_CarYear, objType._String)
    '                    .ChassisNo = ws.ValidateData(_ChassisNo, objType._String)
    '                    .ShowRoomName = ws.ValidateData(_ShowRoomName, objType._String)
    '                    .ShowRoomCode = ws.ValidateData(_ShowRoomCode, objType._String)
    '                    .ClaimNoticeDate = ws.ValidateData(_ClaimNoticeDate, objType._String)
    '                    .ClaimNoticeTime = ws.ValidateData(_ClaimNoticeTime, objType._String)
    '                    .ClaimDetails = ws.ValidateData(_ClaimDetails, objType._String)
    '                    .ClaimType = ws.ValidateData(_ClaimType, objType._Integer) 'int
    '                    .ClaimResult = ws.ValidateData(_ClaimResult, objType._Integer) 'int
    '                    .ClaimDamageDetails = ws.ValidateData(_ClaimDamageDetails, objType._String)
    '                    .CallCenter = ws.ValidateData(_CallCenter, objType._String)
    '                    .AccidentDate = ws.ValidateData(_AccidentDate, objType._String)
    '                    .AccidentTime = ws.ValidateData(_AccidentTime, objType._String)
    '                    .AccidentPlace = ws.ValidateData(_AccidentPlace, objType._String)
    '                    .AccidentTumbon = ws.ValidateData(_AccidentTumbon, objType._String)
    '                    .AccidentAmphur = ws.ValidateData(_AccidentAmphur, objType._String)
    '                    .AccidentProvince = ws.ValidateData(_AccidentProvince, objType._String)
    '                    .AccidentZipcode = ws.ValidateData(_AccidentZipcode, objType._String)
    '                    .DriverName = ws.ValidateData(_DriverName, objType._String)
    '                    .DriverTel = ws.ValidateData(_DriverTel, objType._String)
    '                    .NoticeName = ws.ValidateData(_NoticeName, objType._String)
    '                    .NoticeTel = ws.ValidateData(_NoticeTel, objType._String)
    '                    .GarageType = ws.ValidateData(_GarageType, objType._Integer) 'int
    '                    .GarageCode = ws.ValidateData(_GarageCode, objType._String)
    '                    .GarageName = ws.ValidateData(_GarageName, objType._String)
    '                    .GaragePlace = ws.ValidateData(_GaragePlace, objType._String)
    '                    .GarageTumbon = ws.ValidateData(_GarageTumbon, objType._String)
    '                    .GarageAmphur = ws.ValidateData(_GarageAmphur, objType._String)
    '                    .GarageProvince = ws.ValidateData(_GarageProvince, objType._String)
    '                    .GarageZipcode = ws.ValidateData(_GarageZipcode, objType._String)
    '                    .CarRepairDate = ws.ValidateData(_CarRepairDate, objType._String)
    '                    .CarReceiveDate = ws.ValidateData(_CarReceiveDate, objType._String)
    '                    .ConsentFormNo = ws.ValidateData(_ConsentFormNo, objType._String)
    '                    .PartsDealerName = ws.ValidateData(_PartsDealerName, objType._String)
    '                    .PaymentDetails = ws.ValidateData(_PaymentDetails, objType._String)
    '                    .Amount1 = ws.ValidateData(_Amount1, objType._Double) 'float
    '                    .Amount2 = ws.ValidateData(_Amount2, objType._Double) 'float
    '                    .Amount3 = ws.ValidateData(_Amount3, objType._Double) 'float
    '                    .Remark = ws.ValidateData(_Remark, objType._String)
    '                End With

    '                Dim _item_result As New List(Of ClaimResultMessage)
    '                Select Case _tblClaimData.ClaimStatus
    '                    Case "00"
    '                        _item_result = ws.ValidateClaimData00(_tblClaimData)
    '                    Case "01"
    '                        _item_result = ws.ValidateClaimData01(_tblClaimData)
    '                    Case "02"
    '                        _item_result = ws.ValidateClaimData02(_tblClaimData)
    '                    Case "99"
    '                        _item_result = ws.ValidateClaimData99(_tblClaimData)
    '                    Case "98"
    '                        _item_result = ws.ValidateClaimData98(_tblClaimData)
    '                    Case Else
    '                        _item_result.Add(New ClaimResultMessage With {.ResultCode = "ClaimStatus", .ResultMessage = "Invalid ClaimStatus"})
    '                End Select

    '                If _item_result.Count > 0 Then
    '                    For Each i_rusult In _item_result
    '                        _ClaimResultMessage.Add(New ClaimResultMessage With {.ResultCode = String.Format("RefNo.{0} ({1}) ,{2}", _tblClaimData.RefNo, _tblClaimData.ClaimStatus, i_rusult.ResultCode), .ResultMessage = i_rusult.ResultMessage})
    '                    Next

    '                Else

    '                    '=========================== add new ========================
    '                    Dim _item As New ClaimTransaction_DataObject
    '                    With _item
    '                        .ClaimStatus = _ClaimStatus.ToString()
    '                        .TempPolicy = _TempPolicy.ToString()
    '                        .RefNo = _RefNo.ToString()
    '                        .Version = _Version.ToString()
    '                        .PolicyNo = _PolicyNo.ToString()
    '                        .PolicyYear = _PolicyYear.ToString()
    '                        .ClaimNo = _ClaimNo.ToString()
    '                        .TransactionDate = _TransactionDate.ToString()
    '                        .Unwriter = _Unwriter.ToString()
    '                        .InsuredName = _InsuredName.ToString()
    '                        .EffectiveDate = _EffectiveDate.ToString()
    '                        .ExpiryDate = _ExpiryDate.ToString()
    '                        .Beneficiary = _Beneficiary.ToString()
    '                        .CarBrand = _CarBrand.ToString()
    '                        .CarModel = _CarModel.ToString()
    '                        .CarLicense = _CarLicense.ToString()
    '                        .CarYear = _CarYear.ToString()
    '                        .ChassisNo = _ChassisNo.ToString()
    '                        .ShowRoomName = _ShowRoomName.ToString()
    '                        .ShowRoomCode = _ShowRoomCode.ToString()
    '                        .ClaimNoticeDate = _ClaimNoticeDate.ToString()
    '                        .ClaimNoticeTime = _ClaimNoticeTime.ToString()
    '                        .ClaimDetails = _ClaimDetails.ToString()
    '                        .ClaimType = _ClaimType.ToString()
    '                        .ClaimResult = _ClaimResult.ToString()
    '                        .ClaimDamageDetails = _ClaimDamageDetails.ToString()
    '                        .CallCenter = _CallCenter.ToString()
    '                        .AccidentDate = _AccidentDate.ToString()
    '                        .AccidentTime = _AccidentTime.ToString()
    '                        .AccidentPlace = _AccidentPlace.ToString()
    '                        .AccidentTumbon = _AccidentTumbon.ToString()
    '                        .AccidentAmphur = _AccidentAmphur.ToString()
    '                        .AccidentProvince = _AccidentProvince.ToString()
    '                        .AccidentZipcode = _AccidentZipcode.ToString()
    '                        .DriverName = _DriverName.ToString()
    '                        .DriverTel = _DriverTel.ToString()
    '                        .NoticeName = _NoticeName.ToString()
    '                        .NoticeTel = _NoticeTel.ToString()
    '                        .GarageType = _GarageType.ToString()
    '                        .GarageCode = _GarageCode.ToString()
    '                        .GarageName = _GarageName.ToString()
    '                        .GaragePlace = _GaragePlace.ToString()
    '                        .GarageTumbon = _GarageTumbon.ToString()
    '                        .GarageAmphur = _GarageAmphur.ToString()
    '                        .GarageProvince = _GarageProvince.ToString()
    '                        .GarageZipcode = _GarageZipcode.ToString()
    '                        .CarRepairDate = _CarRepairDate.ToString()
    '                        .CarReceiveDate = _CarReceiveDate.ToString()
    '                        .ConsentFormNo = _ConsentFormNo.ToString()
    '                        .PartsDealerName = _PartsDealerName.ToString()
    '                        .PaymentDetails = _PaymentDetails.ToString()
    '                        .Amount1 = _Amount1.ToString()
    '                        .Amount2 = _Amount2.ToString()
    '                        .Amount3 = _Amount3.ToString()
    '                        .Remark = _Remark.ToString()
    '                    End With

    '                    _data.Add(_item)
    '                End If





    '            Next



    '            'If _data.Count > 0 Then
    '            '    If _ClaimResultMessage.Count > 0 Then
    '            '        Dim sb As New StringBuilder
    '            '        For Each _result In _ClaimResultMessage
    '            '            sb.AppendFormat(String.Format("{0} : {1}<br>", _result.ResultCode, _result.ResultMessage))
    '            '        Next
    '            '        Throw New Exception(sb.ToString())
    '            '    Else
    '            '        Dim _result = ws.GetMotorClaim(_data)
    '            '    End If



    '            'Else
    '            '    _GUID = ""
    '            'End If

    '            If _ClaimResultMessage.Count > 0 Then
    '                Dim sb As New StringBuilder
    '                For Each _result In _ClaimResultMessage
    '                    sb.AppendFormat(String.Format("{0} : {1}<br>", _result.ResultCode, _result.ResultMessage))
    '                Next
    '                Throw New Exception(sb.ToString())
    '            Else
    '                If _data.Count > 0 Then
    '                    Dim _result = ws.GetMotorClaim(_data)
    '                Else
    '                    _GUID = ""
    '                End If
    '            End If
    '        End Using

    '    End Using

    '    Return _GUID

    'End Function



    Protected Sub bnExportXLS_Click(sender As Object, e As EventArgs) Handles bnExportXLS.Click
        GridExporter.WriteXlsxToResponse(System.Guid.NewGuid().ToString(), False, New DevExpress.XtraPrinting.XlsxExportOptionsEx With {.ExportType = DevExpress.Export.ExportType.WYSIWYG})
    End Sub

    Protected Sub cbSave_Callback(ByVal source As Object, ByVal e As CallbackEventArgs) Handles cbSave.Callback
        Dim _TRID = e.Parameter

        Dim _ClaimStatus = ClaimStatus.Value
        Dim _TempPolicy = TempPolicy.Text
        Dim _RefNo = RefNo.Text
        Dim _Version = Version.Value
        Dim _PolicyNo = PolicyNo.Text
        Dim _PolicyYear = PolicyYear.Value
        Dim _ClaimNo = ClaimNo.Text
        Dim _TransactionDate = TransactionDate.Text
        Dim _Unwriter = Unwriter.Value
        Dim _InsuredName = InsuredName.Text
        Dim _EffectiveDate = EffectiveDate.Text
        Dim _ExpiryDate = ExpiryDate.Text
        Dim _Beneficiary = Beneficiary.Text
        Dim _CarBrand = CarBrand.Text
        Dim _CarModel = CarModel.Text
        Dim _CarLicense = CarLicense.Text
        Dim _CarYear = CarYear.Text
        Dim _ChassisNo = ChassisNo.Text
        Dim _ShowRoomName = ShowRoomName.Text
        Dim _ShowRoomCode = ShowRoomCode.Value
        Dim _ClaimNoticeDate = ClaimNoticeDate.Text
        Dim _ClaimNoticeTime = ClaimNoticeTime.Text
        Dim _ClaimDetails = ClaimDetails.Text
        Dim _ClaimType = ClaimType.Value
        Dim _ClaimResult = ClaimResult.Value
        Dim _ClaimDamageDetails = ClaimDamageDetails.Text
        Dim _CallCenter = CallCenter.Text
        Dim _AccidentDate = AccidentDate.Text
        Dim _AccidentTime = AccidentTime.Text
        Dim _AccidentPlace = AccidentPlace.Text
        Dim _AccidentTumbon = AccidentTumbon.Text
        Dim _AccidentAmphur = AccidentAmphur.Text
        Dim _AccidentProvince = AccidentProvince.Text
        Dim _AccidentZipcode = AccidentZipcode.Text
        Dim _DriverName = DriverName.Text
        Dim _DriverTel = DriverTel.Text
        Dim _NoticeName = NoticeName.Text
        Dim _NoticeTel = NoticeTel.Text
        Dim _GarageType = GarageType.Value
        Dim _GarageCode = GarageCode.Text
        Dim _GarageName = GarageName.Text
        Dim _GaragePlace = GaragePlace.Text
        Dim _GarageTumbon = GarageTumbon.Text
        Dim _GarageAmphur = GarageAmphur.Text
        Dim _GarageProvince = GarageProvince.Text
        Dim _GarageZipcode = GarageZipcode.Text
        Dim _ConsentFormNo = ConsentFormNo.Text
        Dim _CarRepairDate = CarRepairDate.Text
        Dim _CarReceiveDate = CarReceiveDate.Text
        Dim _PartsDealerName = PartsDealerName.Text
        Dim _PaymentDetails = PaymentDetails.Text
        Dim _Amount1 = Amount1.Value
        Dim _Amount2 = Amount2.Value
        Dim _Amount3 = Amount3.Value
        Dim _Remark = Remark.Text





        Dim _chkitem As New tblClaimTransaction_Data
        With _chkitem
            .TRNo = _TRID 'dummy

            .ClaimStatus = _ClaimStatus
            .TempPolicy = _TempPolicy
            .RefNo = _RefNo
            .Version = CInt(_Version)
            .PolicyNo = _PolicyNo
            .PolicyYear = CInt(_PolicyYear)
            .ClaimNo = _ClaimNo
            .TransactionDate = _TransactionDate
            .Unwriter = _Unwriter
            .InsuredName = _InsuredName
            .EffectiveDate = _EffectiveDate
            .ExpiryDate = _ExpiryDate
            .Beneficiary = _Beneficiary
            .CarBrand = _CarBrand
            .CarModel = _CarModel
            .CarLicense = _CarLicense
            .CarYear = _CarYear
            .ChassisNo = _ChassisNo
            .ShowRoomName = _ShowRoomName
            .ShowRoomCode = _ShowRoomCode
            .ClaimNoticeDate = _ClaimNoticeDate
            .ClaimNoticeTime = _ClaimNoticeTime
            .ClaimDetails = _ClaimDetails
            .ClaimType = CInt(_ClaimType)
            .ClaimResult = CInt(_ClaimResult)
            .ClaimDamageDetails = _ClaimDamageDetails
            .CallCenter = _CallCenter
            .AccidentDate = _AccidentDate
            .AccidentTime = _AccidentTime
            .AccidentPlace = _AccidentPlace
            .AccidentTumbon = _AccidentTumbon
            .AccidentAmphur = _AccidentAmphur
            .AccidentProvince = _AccidentProvince
            .AccidentZipcode = _AccidentZipcode
            .DriverName = _DriverName
            .DriverTel = _DriverTel
            .NoticeName = _NoticeName
            .NoticeTel = _NoticeTel
            .GarageType = CInt(_GarageType)
            .GarageCode = _GarageCode
            .GarageName = _GarageName
            .GaragePlace = _GaragePlace
            .GarageTumbon = _GarageTumbon
            .GarageAmphur = _GarageAmphur
            .GarageProvince = _GarageProvince
            .GarageZipcode = _GarageZipcode
            .ConsentFormNo = _ConsentFormNo
            .CarRepairDate = _CarRepairDate
            .CarReceiveDate = _CarReceiveDate
            .PartsDealerName = _PartsDealerName
            .PaymentDetails = _PaymentDetails
            .Amount1 = CDbl(_Amount1)
            .Amount2 = CDbl(_Amount2)
            .Amount3 = CDbl(_Amount3)
            .Remark = _Remark
        End With

        Dim ws As New MotorClaimWebService
        Dim _item_result As New List(Of ClaimResultMessage)

        Select Case _ClaimStatus
            Case "00"
                _item_result = ws.ValidateClaimData00(_chkitem)
            Case "01"
                _item_result = ws.ValidateClaimData01(_chkitem)
            Case "02"
                _item_result = ws.ValidateClaimData02(_chkitem)
            Case "99"
                _item_result = ws.ValidateClaimData99(_chkitem)
            Case "98"
                _item_result = ws.ValidateClaimData98(_chkitem)
        End Select


        If _item_result.Count = 0 Then
            Using dc As New DataClasses_MotorClaimDataContext()
                Dim _data = (From c In dc.tblClaimTransaction_Datas Where c.TRID = _TRID).FirstOrDefault()
                With _data
                    .ClaimStatus = _ClaimStatus
                    .TempPolicy = _TempPolicy
                    .RefNo = _RefNo
                    .Version = CInt(_Version)
                    .PolicyNo = _PolicyNo
                    .PolicyYear = CInt(_PolicyYear)
                    .ClaimNo = _ClaimNo
                    .TransactionDate = _TransactionDate
                    .Unwriter = _Unwriter
                    .InsuredName = _InsuredName
                    .EffectiveDate = _EffectiveDate
                    .ExpiryDate = _ExpiryDate
                    .Beneficiary = _Beneficiary
                    .CarBrand = _CarBrand
                    .CarModel = _CarModel
                    .CarLicense = _CarLicense
                    .CarYear = _CarYear
                    .ChassisNo = _ChassisNo
                    .ShowRoomName = _ShowRoomName
                    .ShowRoomCode = _ShowRoomCode
                    .ClaimNoticeDate = _ClaimNoticeDate
                    .ClaimNoticeTime = _ClaimNoticeTime
                    .ClaimDetails = _ClaimDetails
                    .ClaimType = CInt(_ClaimType)
                    .ClaimResult = CInt(_ClaimResult)
                    .ClaimDamageDetails = _ClaimDamageDetails
                    .CallCenter = _CallCenter
                    .AccidentDate = _AccidentDate
                    .AccidentTime = _AccidentTime
                    .AccidentPlace = _AccidentPlace
                    .AccidentTumbon = _AccidentTumbon
                    .AccidentAmphur = _AccidentAmphur
                    .AccidentProvince = _AccidentProvince
                    .AccidentZipcode = _AccidentZipcode
                    .DriverName = _DriverName
                    .DriverTel = _DriverTel
                    .NoticeName = _NoticeName
                    .NoticeTel = _NoticeTel
                    .GarageType = CInt(_GarageType)
                    .GarageCode = _GarageCode
                    .GarageName = _GarageName
                    .GaragePlace = _GaragePlace
                    .GarageTumbon = _GarageTumbon
                    .GarageAmphur = _GarageAmphur
                    .GarageProvince = _GarageProvince
                    .GarageZipcode = _GarageZipcode
                    .ConsentFormNo = _ConsentFormNo
                    .CarRepairDate = _CarRepairDate
                    .CarReceiveDate = _CarReceiveDate
                    .PartsDealerName = _PartsDealerName
                    .PaymentDetails = _PaymentDetails
                    .Amount1 = CDbl(_Amount1)
                    .Amount2 = CDbl(_Amount2)
                    .Amount3 = CDbl(_Amount3)
                    .Remark = _Remark
                End With


                If String.IsNullOrEmpty(_data.TRNo) Then
                    Dim _TRNo As String = ""
                    dc.Running_GetByRunningCode(_Unwriter, _TRNo)
                    _data.TRNo = _TRNo
                End If

                _data.Status = True
                dc.SubmitChanges()
                e.Result = "success"

            End Using


        Else
            e.Result = _item_result(0).ResultMessage
        End If





    End Sub



    Protected Sub ASPxGridViewNotice_HtmlRowCreated(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewTableRowEventArgs) Handles ASPxGridViewNotice.HtmlRowPrepared


        If e.RowType = GridViewRowType.Group Then
            e.Row.Font.Bold = True
        End If

    End Sub



    Protected Sub btnExportNotice_Click(sender As Object, e As EventArgs) Handles btnExportNotice.Click
        ASPxGridViewExporterNotice.WriteXlsxToResponse(System.Guid.NewGuid().ToString(), False, New DevExpress.XtraPrinting.XlsxExportOptionsEx With {.ExportType = DevExpress.Export.ExportType.WYSIWYG})
    End Sub




    Protected Sub cbSendNotice_Callback(ByVal source As Object, ByVal e As CallbackEventArgs) Handles cbSendNotice.Callback

        Dim a = e.Parameter


        Using dc As New DataClasses_MotorClaimDataExt
            Dim _data = (From c In dc.V_ClaimTransactionData_Notices).ToList()
            If _data.Count > 0 Then

                Dim _dealer = (From c In _data _
                    Group By DealerCode = c.DealerCode
                    Into g = Group _
                    Select DealerCode _
                ).ToList()

                For Each dealercode In _dealer
                    WH_genxls(dealercode)
                Next

                e.Result = "success"
            Else
                e.Result = "No Data"
            End If
        End Using



    End Sub



    Private Sub WH_genxls(ByVal dealercode As String)

        Using dc As New DataClasses_MotorClaimDataExt
            'เฉพาะสำนักงานใหญ่
            Dim _dealer = (From c In dc.tblShowRooms _
                              Where c.DealerCode.Equals(dealercode) _
                              And c.BranchCode.Equals("01") _
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
            Dim sheet1 As ISheet = hssfworkbook.CreateSheet(_dealer.ShowRoomName)
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

            Dim _data_claim_00 = (From c In dc.V_ClaimTransactionData_Notices _
                          Where c.DealerCode.Equals(dealercode)).ToList()

            For Each _item In _data_claim_00

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


                '"ดีลเลอร์,ผู้เอาประกันภัย,เลขตัวถัง,เลขที่กรมธรรม์,เลขที่เคลม,วันที่เกิดเหตุ,สถานที่เกิดเหตุ,วันคุ้มครอง,ผู้รับผลประโยชน์,ประกันภัย,วันที่ติดต่อ,กรณีเข้าซ่อมแล้วกรุณาระบุชื่ออู่,ลูกค้าเข้าซ่อมเอง(ระบุ '/'),ประกันภัยแนะนำ(ระบุ '/'),ยังไม่เข้าซ่อม(ระบุ '/'),หมายเหตุ(อื่นๆ)".Split(",")
                row.GetCell(0).SetCellValue(_item.DealerName.Trim()) 'ดีลเลอร์
                row.GetCell(1).SetCellValue(_item.InsuredName.Trim()) 'ผู้เอาประกันภัย
                row.GetCell(2).SetCellValue(_item.ChassisNo.Trim()) 'เลขตัวถัง
                row.GetCell(3).SetCellValue(_item.PolicyNo.Trim()) 'เลขที่กรมธรรม์
                row.GetCell(4).SetCellValue(_item.ClaimNo.Trim()) 'เลขที่เคลม
                row.GetCell(5).SetCellValue(String.Format("{0} {1}", _item.AccidentDate.Trim(), _item.AccidentTime.Trim())) 'วันที่เกิดเหตุ
                row.GetCell(6).SetCellValue(_item.AccidentPlace.Trim()) 'สถานที่เกิดเหตุ
                row.GetCell(7).SetCellValue(_item.EffectiveDate.Trim()) 'วันคุ้มครอง
                row.GetCell(8).SetCellValue(_item.Beneficiary.Trim()) 'ผู้รับผลประโยชน์
                row.GetCell(9).SetCellValue(_item.UWName.Trim()) 'ประกันภัย

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

            'Dim _AgentNameTemp = Left(_agentdata.ShowRoomName, _agentdata.ShowRoomName.IndexOf("จำกัด"))
            Dim _DealerNameTemp = _dealer.ShowRoomName
            Dim _DealerName As String = String.Format("บริษัท {0} จำกัด", _DealerNameTemp)

            '================== init  sheet property =================
            'Dim _data = (From c In dc.tblNoticeHeaders Where c.NoticeID.Equals(NotificationID)).FirstOrDefault()

            Dim _filename = String.Format("{0}.xls", System.Guid.NewGuid())
            Dim _fileDataPath = String.Format("{0}\{1}", Server.MapPath("~/uploadfiles"), _filename)
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


            'Dim _mail_template = (From c In dc.MailNotifications Where c.Code.Equals(_NoticeCode)).FirstOrDefault()
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

                Dim _mailNotification = (From c In dc_portal.MailNotifications Where c.Code.Equals(_NoticeCode)).FirstOrDefault()
                strSubject = _mailNotification.MailSubject.Replace("{Name}", _DealerName)

                Dim _noticeDateFrom = CDate(_data_claim_00.Min(Function(c) c.ClaimNoticeDate).ToString())
                Dim _noticeDateTo = CDate(_data_claim_00.Max(Function(c) c.ClaimNoticeDate).ToString())


                strSubject = strSubject.Replace("{date}", String.Format("{0} - {1}", MyUtils.GenThaiDate(_noticeDateFrom, 2), MyUtils.GenThaiDate(_noticeDateTo, 2)))

                'strMessage.AppendLine("<!DOCTYPE html><html><body>")
                'strMessage.AppendLine(Server.HtmlDecode(_mailNotification.MailBody.Replace("{date}", String.Format("{0} - {1}", Utils.GenThaiDate(dpNoticeDate.SelectedDate.Value, 2), Utils.GenThaiDate(dpDueDate.SelectedDate.Value, 2)))))
                'strMessage.AppendLine("</body></html>")


                Dim _MailBody As String = Server.HtmlDecode(_mailNotification.MailBody.Replace("{date}", String.Format("{0} - {1}", MyUtils.GenThaiDate(_noticeDateFrom, 2), MyUtils.GenThaiDate(_noticeDateTo, 2))))

                _MailBody = _MailBody.Replace("{displayName}", _displayName)
                _MailBody = _MailBody.Replace("{title}", _title)
                _MailBody = _MailBody.Replace("{department}", _department)
                _MailBody = _MailBody.Replace("{company}", _company)
                _MailBody = _MailBody.Replace("{streetAddress}", _streetAddress)
                _MailBody = _MailBody.Replace("{telephoneNumber}", _telephoneNumber)
                _MailBody = _MailBody.Replace("{facsimileTelephoneNumber}", _facsimileTelephoneNumber)
                _MailBody = _MailBody.Replace("{mail}", _mail)

                strMessage.AppendLine(_MailBody)

                'strMailFrom = _mailNotification.MailFrom
                'strMailCC = _mailNotification.MailCC
                'strMailTo = _agent.MailTo

                strMailFrom = _mail
                strMailTo = _mail
            End Using


            Dim MySmtpClient As New System.Net.Mail.SmtpClient("LOCKTHBNK-EXCH1.asia.lockton.com")
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
            att_data.Name = String.Format("{0}.xls", _DealerName)
            msg.Attachments.Add(att_data)

            MySmtpClient.Send(msg)

            '============ todo ==============
            ''============= Set Post ===============
            'For Each _ID In _data_claim_00
            '    dc.ExecuteCommand("update tblClaimTransaction_Data set IsPost=1,PostDate=Getdate()  where TRID = '" & _ID.TRID.ToString() & "'")
            'Next
        End Using

    End Sub


    Protected Sub frmImport_gvApp_HtmlDataCellPrepared(sender As Object, e As DevExpress.Web.ASPxGridViewTableDataCellEventArgs) Handles ASPxGridViewResult.HtmlDataCellPrepared

        If e.GetValue("Message") IsNot Nothing Then
            If e.DataColumn.FieldName <> "Status" _
           And e.DataColumn.FieldName <> "Message" _
           And e.GetValue("Message").ToString.IndexOf(String.Format("#{0}#", e.DataColumn.FieldName.ToString())) > -1 Then

                e.Cell.BackColor = System.Drawing.Color.Red
                e.Cell.ForeColor = System.Drawing.Color.White

            End If
        End If


    End Sub
    Protected Sub callbackPanel_resultdetails_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase) Handles callbackPanel_resultdetails.Callback
        Using conn As New DataClasses_MotorClaimDataExt()
            Dim _RunNo = e.Parameter

            'Dim _data = conn.MotorClaimUploadResult(Session("MCGUID"))

            Dim _data As New List(Of ClaimUpload_DataObject)

            Dim FilePath = Page.MapPath("~/UploadFiles/")
            Dim excel_FileName = String.Format(FilePath & "/{0}.xlsx", Session("MCGUID"))
            Dim csv_FileName = String.Format(FilePath & "/{0}.csv", Session("MCGUID"))
          
            If File.Exists(excel_FileName) Then
                _data = conn.MotorClaimUploadResult(Session("MCGUID"), "xlsx")

            ElseIf File.Exists(csv_FileName) Then
                _data = conn.MotorClaimUploadResult(Session("MCGUID"), "csv")
            End If


            Dim _result = (From c In _data Where c.RunNo.Equals(_RunNo)).FirstOrDefault()

            Dim sb As New StringBuilder

            If Not String.IsNullOrEmpty(_result.Message) Then
                Dim _a = _result.Message.ToString().Split("|")
                For Each _message In _a
                    If Not String.IsNullOrEmpty(_message.Trim) Then
                        Dim _Fields = _message.Replace("[", "").Replace("]", "").Split(",")
                        sb.Append(String.Format("- {0} : {1}<br>", _Fields(0), _Fields(1)))
                    End If
                Next

                litText.Text = "Incomplete <br>" & sb.ToString()

            Else
                litText.Text = "Complete"
            End If



        End Using

        'Using context = New NorthwindContext()
        '    Dim employeeId As Integer = Convert.ToInt32(e.Parameter)
        '    Dim employee = context.Employees.Single(Function(em) em.EmployeeID = employeeId)
        '    edBinaryImage.Value = employee.Photo
        '    litText.Text = employee.Notes
        'End Using
    End Sub

End Class
