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


Partial Class Modules_ucLWTMotorClaimInbox_VIRI
    Inherits PortalModuleControl 
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

 
        If Not IsPostBack Then

            pnMain.HeaderText = portalSettings.ActiveTab.TabName

            Session("MCGUID") = Nothing
            Session("MCTYPE") = Nothing

            Session("TRID") = Nothing
        End If


    End Sub
    Protected Sub ActionMenu_ItemDataBound(ByVal sender As Object, ByVal e As MenuItemEventArgs)
        Dim itemHierarchyData As IHierarchyData = CType(e.Item.DataItem, IHierarchyData)
        Dim element = CType(itemHierarchyData.Item, XmlElement)

        Dim classAttr = element.Attributes("SpriteClassName")

        e.Item.Image.SpriteProperties.CssClass = classAttr.Value

    End Sub

    Protected Sub MailPreviewPanel_Callback(ByVal sender As Object, ByVal e As CallbackEventArgsBase) Handles MailPreviewPanel.Callback

        Dim _TRID = e.Parameter.ToString()
        Session("TRID") = _TRID
        formPreview.DataBind()



    End Sub

 



    Protected Sub bnExportXLS_Click(sender As Object, e As EventArgs)
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
             

            Dim _data As New List(Of ClaimUpload_DataObject)

            Dim FilePath = Page.MapPath("~/UploadFiles/")
            Dim excel_FileName = String.Format(FilePath & "/{0}.xlsx", Session("MCGUID"))
            'Dim csv_FileName = String.Format(FilePath & "/{0}.csv", Session("MCGUID"))

            'If File.Exists(excel_FileName) Then
            '    _data = conn.MotorClaimUploadResult(Session("MCGUID"), "xlsx")

            'ElseIf File.Exists(csv_FileName) Then
            '    _data = conn.MotorClaimUploadResult(Session("MCGUID"), "csv")
            'End If

            _data = conn.MotorClaimUploadResult_VIRI(Session("MCGUID"))

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
