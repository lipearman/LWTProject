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
Imports DevExpress.Web.Data
Imports DevExpress.Web


'Imports System.Data
Imports System.Collections.Generic
Imports System.Collections
Imports System.Web
'Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports MultiTaskIndicator

Imports DevExpress.Spreadsheet
Imports DevExpress.Spreadsheet.Export

Partial Class Modules_ucDevxNoticeHeaderSetup_B0021
    Inherits PortalModuleControl
    Dim _NoticeCode As String = "B0021"

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        SqlDataSource_gridData.SelectParameters("NoticeCode").DefaultValue = _NoticeCode


        If IsPostBack = False Then
            Session("NoticeID") = Nothing
            Session("AgentCode") = Nothing
            Session("Process") = Nothing
            Session("Applications_Task_B0021") = Nothing
            'progress = 0

        End If

        'If Not Page.IsPostBack Then
        '    CreateDataSource()
        '    Bind()
        'End If
    End Sub
    Protected Sub MyGrid_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles MyGrid.InitNewRow

        Using dc_Portal = New DataClasses_PortalDataContextExt()
            Dim _mail_template = (From c In dc_Portal.MailNotifications Where c.Code.Equals(_NoticeCode)).FirstOrDefault()
            e.NewValues("NoticeTitle") = _mail_template.Name

        End Using

    End Sub
    Protected Sub grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles MyGrid.RowInserting

        Using dc = New DataClasses_CPSExt()


            dc.tblNoticeHeaders.InsertOnSubmit(New tblNoticeHeader With {.NoticeTitle = e.NewValues("NoticeTitle") _
                                                       , .NoticeCode = _NoticeCode _
                                                       , .CreationDate = DateTime.Now _
                                                       , .CreationBy = HttpContext.Current.User.Identity.Name _
                                                       , .DueDate = e.NewValues("DueDate") _
                                                       , .NoticeDate = e.NewValues("NoticeDate")})

            dc.SubmitChanges()
        End Using

        MyGrid.CancelEdit()
        e.Cancel = True

    End Sub
    Protected Sub grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles MyGrid.RowUpdating
        Dim _NoticeID = e.Keys("NoticeID")
        Using dc = New DataClasses_CPSExt()
            Dim _data = (From c In dc.tblNoticeHeaders Where c.NoticeID.Equals(_NoticeID)).FirstOrDefault()

            _data.NoticeTitle = e.NewValues("NoticeTitle")
            _data.NoticeDate = e.NewValues("NoticeDate")
            _data.DueDate = e.NewValues("DueDate")
            _data.ModifyDate = DateTime.Now
            _data.ModifyBy = HttpContext.Current.User.Identity.Name


            dc.SubmitChanges()
        End Using

        MyGrid.CancelEdit()
        e.Cancel = True
    End Sub



    Protected Sub popupDetails_WindowCallback(ByVal source As Object, ByVal e As PopupWindowCallbackArgs) Handles PopupDetails.WindowCallback
        Dim _NoticeID As String = e.Parameter.ToString()
        Session("NoticeID") = _NoticeID

        Using dc = New DataClasses_CPSExt()
            Dim _data = (From c In dc.tblNoticeHeaders Where c.NoticeID.Equals(_NoticeID)).FirstOrDefault()
            PopupDetails.JSProperties("cpHeaderDetailsText") = String.Format("{0} - {1} ({2})", _NoticeID, _data.NoticeTitle, _data.CreationDate.ToString())

            formPreview.DataSource = _data
            formPreview.DataBind()

        End Using

    End Sub



    Protected Sub GridExportData_Click(sender As Object, e As EventArgs)
        GridExportData.WriteXlsxToResponse(System.Guid.NewGuid().ToString(), False, New DevExpress.XtraPrinting.XlsxExportOptionsEx With {.ExportType = DevExpress.Export.ExportType.WYSIWYG})
    End Sub

    Protected Sub GridExportSummary_Click(sender As Object, e As EventArgs)
        GridExportSummary.WriteXlsxToResponse(System.Guid.NewGuid().ToString(), False, New DevExpress.XtraPrinting.XlsxExportOptionsEx With {.ExportType = DevExpress.Export.ExportType.WYSIWYG})
    End Sub

    'Protected Sub MyGridDetails_HtmlRowCreated(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewTableRowEventArgs) Handles MyGridDetails.HtmlRowPrepared
    '    If e.RowType <> DevExpress.Web.GridViewRowType.Data Then
    '        Return
    '    End If
    '    Dim btnRowSendMail As ASPxButton = MyGridDetails.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "btnRowSendMail")
    '    btnRowSendMail.JSProperties.Add("cpAgentCode", e.KeyValue())


    'End Sub


    Protected Sub PopupDataDetails_WindowCallback(ByVal source As Object, ByVal e As PopupWindowCallbackArgs) Handles PopupDataDetails.WindowCallback
        Dim _AgentCode As String = e.Parameter.ToString()
        Session("AgentCode") = _AgentCode
        Dim _NoticeID = Session("NoticeID")
        Using dc = New DataClasses_CPSExt()
            Dim _data = (From c In dc.tblNoticeHeaders Where c.NoticeID.Equals(_NoticeID)).FirstOrDefault()
            PopupDataDetails.JSProperties("cpHeaderDataDetailsText") = String.Format("{0} - {1}", _AgentCode, _data.NoticeTitle)
        End Using

    End Sub

    Protected Sub cbSendMailProcessing_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbSendMailProcessing.Callback

        Dim _AgentCodes = e.Parameter.ToString().Split(",")
        Dim i As Integer = 1
        Dim _tasks = New List(Of Task)()
        Using dc = New DataClasses_CPSExt()
            For Each agentcode In _AgentCodes
                Dim _data = (From c In dc.tblNoticeMailContacts Where c.Code.Equals(agentcode) And c.NoticeCode.Equals(_NoticeCode)).FirstOrDefault()
                _tasks.Add(New Task(_data.Code, String.Format("{0} - {1}", _data.Code, _data.Name)))
                i += 1
            Next
        End Using
 
        Session("Applications_Task_B0021") = _tasks


        e.Result = "success"

    End Sub


    Protected Sub btnDownloadFormat_click(ByVal sender As Object, ByVal e As EventArgs)
        Dim fileName = String.Format("{0}.xlsx", System.Guid.NewGuid.ToString())
        Dim filePath = String.Format("{0}\{1}", Server.MapPath("~/App_Data/Template"), "BillingD500.xlsx")

        Response.ClearContent()
        Response.AddHeader("content-disposition", "attachment; filename=" & fileName)
        Response.ContentType = "application/octet-stream"
        Response.WriteFile(filePath)
        Response.End()

    End Sub

    Protected Sub UploadControl_FileUploadComplete(ByVal sender As Object, ByVal e As FileUploadCompleteEventArgs) Handles UploadControl.FileUploadComplete

        If (Not String.IsNullOrEmpty(e.UploadedFile.FileName)) AndAlso e.IsValid Then
            ASPxSpreadsheet1.WorkDirectory = Server.MapPath("~/App_Data/Temp")
            ASPxSpreadsheet1.SettingsDocumentSelector.UploadSettings.Enabled = True
            Dim _NoticeID = Session("NoticeID")

            Dim filePath = Path.Combine(ASPxSpreadsheet1.WorkDirectory, String.Format("{0}_{1}.xlsx", _NoticeCode, _NoticeID))

            e.UploadedFile.SaveAs(filePath)
            e.CallbackData = "success"
        End If
    End Sub


    Protected Sub Spreadsheet_Callback(ByVal source As Object, ByVal e As CallbackEventArgsBase) Handles ASPxSpreadsheet1.Callback
        LoadFile()
    End Sub

    Private Sub LoadFile()
        Dim _NoticeID = Session("NoticeID")
        ASPxSpreadsheet1.WorkDirectory = Server.MapPath("~/App_Data/Temp")
        Dim filePath = Path.Combine(ASPxSpreadsheet1.WorkDirectory, String.Format("{0}_{1}.xlsx", _NoticeCode, _NoticeID))
        If File.Exists(filePath) Then
            Dim documentID = System.Guid.NewGuid().ToString()
            ASPxSpreadsheet1.Open(documentID, DevExpress.Spreadsheet.DocumentFormat.Xlsx, Function() System.IO.File.ReadAllBytes(filePath))
        End If
    End Sub



    Protected Sub PopupImportData_WindowCallback(ByVal source As Object, ByVal e As PopupWindowCallbackArgs) Handles PopupImportData.WindowCallback
 
        ASPxSpreadsheet1.WorkDirectory = Server.MapPath("~/App_Data/Temp")
        Dim filePath = Path.Combine(ASPxSpreadsheet1.WorkDirectory, "blank.xlsx")
        If File.Exists(filePath) Then
            Dim documentID = System.Guid.NewGuid().ToString()
            ASPxSpreadsheet1.Open(documentID, DevExpress.Spreadsheet.DocumentFormat.Xlsx, Function() System.IO.File.ReadAllBytes(filePath))
        End If

    End Sub




    Protected Sub cbUploadData_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbUploadData.Callback
        Dim _NoticeID = Session("NoticeID")
 
        Dim worksheet_Summary As Worksheet = ASPxSpreadsheet1.Document.Worksheets("Summary")
        Dim range_Summary As Range = worksheet_Summary.GetUsedRange()

         
        ''=============================== Summary ================================================

        Dim _BillingSummary As New List(Of tblNoticeDetail)
        For r As Integer = 1 To range_Summary.RowCount - 1
            '1.Code
            '2 Sub Name
            '3 New
            '4 Renew
            '5 Total
            '6 Vat7%
            '7 Tax3%
            '8 Fee
            '9 Other
            '10 Net Paid
            '11 Remark


            _BillingSummary.Add(New tblNoticeDetail With {.NoticeID = _NoticeID _
                                                                                , .Code = range_Summary(r, 0).Value.ToString _
                                                                                , .f01 = "SUM" _
                                                                                , .f02 = range_Summary(r, 1).Value.ToString _
                                                                                , .f03 = range_Summary(r, 2).Value.NumericValue _
                                                                                , .f04 = range_Summary(r, 3).Value.NumericValue _
                                                                                , .f05 = range_Summary(r, 4).Value.NumericValue _
                                                                                , .f06 = range_Summary(r, 5).Value.NumericValue _
                                                                                , .f07 = range_Summary(r, 6).Value.NumericValue _
                                                                                , .f08 = range_Summary(r, 7).Value.NumericValue _
                                                                                , .f09 = range_Summary(r, 8).Value.NumericValue _
                                                                                , .f10 = range_Summary(r, 9).Value.NumericValue _
                                                                                , .f11 = range_Summary(r, 10).Value.ToString _
                                                                           })
        Next
       

     



       
        ''============================ Data ==================================
        Dim _BillingData As New List(Of tblNoticeDetail)
        Dim worksheet_Data As Worksheet = ASPxSpreadsheet1.Document.Worksheets("Data")
        Dim range_Data As Range = worksheet_Data.GetUsedRange()



        For r As Integer = 1 To range_Data.RowCount - 1
            '1:          Type()
            '2:          AgentCode()
            '3:          Dealer()
            '4:          ClosingDate()
            '5:          INS()
            '6:          ClientCode()
            '7:          DNNo()
            '8:          CustomerName()
            '9:          Detail()
            '10:         Premium()
            '11:         Comm()
            '12:         Discount()
            '13:         Free()
            '14:         Vat()
            '15:         WH()
            '16:         Total()


            _BillingData.Add(New tblNoticeDetail With {.NoticeID = _NoticeID _
                                                                                , .Code = range_Data(r, 1).Value.ToString _
                                                                                , .f01 = range_Data(r, 0).Value.ToString _
                                                                                , .f02 = range_Data(r, 1).Value.ToString _
                                                                                , .f03 = range_Data(r, 2).Value.ToString _
                                                                                , .f04 = range_Data(r, 3).Value.DateTimeValue _
                                                                                , .f05 = range_Data(r, 4).Value.ToString _
                                                                                , .f06 = range_Data(r, 5).Value.ToString _
                                                                                , .f07 = range_Data(r, 6).Value.ToString _
                                                                                , .f08 = range_Data(r, 7).Value.ToString _
                                                                                , .f09 = range_Data(r, 8).Value.ToString _
                                                                                , .f10 = range_Data(r, 9).Value.NumericValue _
                                                                                , .f11 = range_Data(r, 10).Value.NumericValue _
                                                                               , .f12 = range_Data(r, 11).Value.NumericValue _
                                                                               , .f13 = range_Data(r, 12).Value.NumericValue _
                                                                               , .f14 = range_Data(r, 13).Value.NumericValue _
                                                                               , .f15 = range_Data(r, 14).Value.NumericValue _
                                                                               , .f16 = range_Data(r, 15).Value.NumericValue _
                                                                           })

        Next
    
 

   


        Dim sb As New StringBuilder()

        If _BillingData.Count = 0 Or _BillingSummary.Count = 0 Then
            sb.Append("No Summary or Data")
        Else
            Using dc As New DataClasses_CPSExt
                dc.ExecuteCommand("delete from tblNoticeDetail where NoticeID={0}", _NoticeID)

                dc.tblNoticeDetails.InsertAllOnSubmit(_BillingSummary)
                dc.SubmitChanges()

                dc.tblNoticeDetails.InsertAllOnSubmit(_BillingData)
                dc.SubmitChanges()
            End Using

            sb.Append("success")
        End If


 
        e.Result = sb.ToString()

    End Sub

    Protected Sub cbDeleteData_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbDeleteData.Callback

        Dim _NoticeID = Session("NoticeID")
        Dim _AgentCodes = e.Parameter.ToString().Split(",")
   
        Using dc = New DataClasses_CPSExt()
            For Each agentcode In _AgentCodes
                dc.ExecuteCommand("delete from tblNoticeDetail where NoticeID='" & _NoticeID & "' and Code='" & agentcode & "'")
            Next
        End Using

 
        e.Result = "success"


    End Sub

      
End Class
