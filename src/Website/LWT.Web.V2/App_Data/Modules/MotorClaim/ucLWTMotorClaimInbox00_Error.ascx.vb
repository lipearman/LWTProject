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


Partial Class Modules_ucLWTMotorClaimInbox00_Error
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
            pnMain.HeaderText = portalSettings.ActiveTab.TabName
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
     
    Protected Sub MailPreviewPanel_Callback(ByVal sender As Object, ByVal e As CallbackEventArgsBase) Handles MailPreviewPanel.Callback
        'Dim id As Integer

        Dim _TRID = e.Parameter.ToString()
        Session("TRID") = _TRID
        formPreview.DataBind()
 
    End Sub



 
     
    Protected Sub btnExportData_Click(sender As Object, e As EventArgs)
        GridExporter.WriteXlsxToResponse(System.Guid.NewGuid().ToString(), False, New DevExpress.XtraPrinting.XlsxExportOptionsEx With {.ExportType = DevExpress.Export.ExportType.Default})
    End Sub

    
 

End Class
