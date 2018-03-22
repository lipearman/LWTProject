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
Imports MotorClaim
Imports LWT.Website
Imports System.Net.Mail
Imports MotorClaimWebService


Partial Class Modules_ucUWMotorClaimInbox02
    Inherits PortalModuleControl
    Dim _NoticeCode As String = "B0025"
    Private Const PreviewMessageFormat As String = "<div class='MailPreview'>" & "<div class='Subject'>{0}</div>" & "<div class='Info'>" & "<div>From: {1}</div>" & "<div>To: {2}</div>" & "<div>Date: {3:g}</div>" & "</div>" & "<div class='Separator'></div>" & "<div class='Body'>{4}</div>" & "</div>", ReplyMessageFormat As String = "Hi,<br/><br/><br/><br/>Thanks,<br/>Thomas Hardy<br/><br/><br/>----- Original Message -----<br/>Subject: {0}<br/>From: {1}<br/>To: {2}<br/>Date: {3:g}<br/>{4}", NotFoundMessageFormat As String = "<h1>Can't find message with the key={0}</h1>"

    Protected ReadOnly Property SearchText() As String
        Get
            Return Utils.GetSearchText(Page)
        End Get
    End Property


    Private Function ShouldBindGrid() As Boolean
        Return (Not Page.IsCallback) OrElse MailGrid.IsCallback
    End Function
    Protected Sub Page_Load(ByVal s As Object, ByVal e As EventArgs) Handles Me.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If

        'If Not System.IO.Directory.Exists(Server.MapPath("~/App_Data/ConsentForms/" & HttpContext.Current.User.Identity.Name & "/ConsentForms")) Then
        '    System.IO.Directory.CreateDirectory(Server.MapPath("~/App_Data/ConsentForms/" & HttpContext.Current.User.Identity.Name & "/ConsentForms"))
        'End If

        If Not IsPostBack Then
            Session("MCGUID") = Nothing
            Session("MCTYPE") = Nothing

            Dim _Portal_User = MotorClaimModel.DataProvider.Portal_User_Data.AsQueryable()
            Dim _UWCode = _Portal_User.Where(Function(m) m.UserName.Equals(HttpContext.Current.User.Identity.Name)).FirstOrDefault()
            SqlDataSource_gridData.SelectParameters("Unwriter").DefaultValue = _UWCode.PasswordQuestion
            SqlDataSource_gridData.SelectParameters("ClaimStatus").DefaultValue = "02"
        End If


    End Sub



    Protected Sub MailPreviewPanel_Callback(ByVal sender As Object, ByVal e As CallbackEventArgsBase) Handles MailPreviewPanel.Callback
        Dim id As Integer



        Dim sb As New StringBuilder()


        Dim text = String.Format(NotFoundMessageFormat, e.Parameter)

        If Integer.TryParse(e.Parameter, id) Then

            Dim data = MotorClaimModel.DataProvider.tblClaimTransaction_Data.AsQueryable()

            Dim MyData = data.Where(Function(m) m.TRID.Equals(id)).FirstOrDefault()

            formPreview.DataSource = MyData
            formPreview.DataBind()

            


            If MyData.Status.Value = False Then
                Dim result = MotorClaimModel.DataProvider.tblClaimTransaction_Result.AsQueryable()
                ASPxGridPreview_Result.DataSource = result.Where(Function(m) m.GUID.Equals(MyData.GUID)).ToList()
                ASPxGridPreview_Result.DataBind()

                ASPxGridPreview_Result.Visible = True

            Else
                ASPxGridPreview_Result.Visible = False
            End If



        End If

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

                    Dim _Portal_User = MotorClaimModel.DataProvider.Portal_User_Data.AsQueryable()
                    Dim _UWCode = _Portal_User.Where(Function(m) m.UserName.Equals(HttpContext.Current.User.Identity.Name)).FirstOrDefault()

                    Dim _Unwriter = _UWCode.PasswordQuestion


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
                            .Unwriter = _Unwriter
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


    Protected Sub btnDownloadFormat_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDownloadFormat.Click
        Dim fileName = String.Format("{0}.xlsx", System.Guid.NewGuid.ToString())
        Dim filePath = String.Format("{0}\{1}", Server.MapPath("~/Template"), "ImportFormat.xlsx")

        Response.ClearContent()
        Response.AddHeader("content-disposition", "attachment; filename=" & fileName)
        Response.ContentType = "application/octet-stream"
        Response.WriteFile(filePath)
        Response.End()

    End Sub





    Protected Sub bnExportXLS_Click(sender As Object, e As EventArgs) Handles bnExportXLS.Click
        GridExporter.WriteXlsxToResponse(System.Guid.NewGuid().ToString(), False, New DevExpress.XtraPrinting.XlsxExportOptionsEx With {.ExportType = DevExpress.Export.ExportType.Default})
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


    End Sub

End Class
