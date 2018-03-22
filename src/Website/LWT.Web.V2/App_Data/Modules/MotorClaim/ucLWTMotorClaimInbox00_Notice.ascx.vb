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


Partial Class Modules_ucLWTMotorClaimInbox00_Notice
    Inherits PortalModuleControl
     
    Protected Sub Page_Load(ByVal s As Object, ByVal e As EventArgs) Handles Me.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")


        End If

        If IsPostBack = False Then
            pnMain.HeaderText = portalSettings.ActiveTab.TabName
            Session("TRID") = Nothing
        End If
    End Sub
   


    Protected Sub btnExportData_Click(sender As Object, e As EventArgs)
        GridExporter.WriteXlsxToResponse(System.Guid.NewGuid().ToString(), False, New DevExpress.XtraPrinting.XlsxExportOptionsEx With {.ExportType = DevExpress.Export.ExportType.Default})
    End Sub




    Protected Sub MailPreviewPanel_Callback(ByVal sender As Object, ByVal e As CallbackEventArgsBase) Handles MailPreviewPanel.Callback
        'Dim id As Integer

        Dim _TRID = e.Parameter.ToString()
        Session("TRID") = _TRID
        formPreview.DataBind()

        'Dim sb As New StringBuilder()


        'Dim text = String.Format(NotFoundMessageFormat, e.Parameter)

        'If Integer.TryParse(e.Parameter, id) Then

        '    Dim data = MotorClaimModel.DataProvider.tblClaimTransaction_Data.AsQueryable()

        '    Dim MyData = data.Where(Function(m) m.TRID.Equals(id)).FirstOrDefault()

        '    formPreview.DataSource = MyData
        '    formPreview.DataBind()

        '    If MyData.ClaimStatus.Equals("CF") Then


        '        formPreview.Items(formPreview.Items.Count - 1).Visible = False
        '    Else

        '        formPreview.Items(formPreview.Items.Count - 1).Visible = True

        '    End If


        '    'If MyData.Status.Value = False Then
        '    Dim result = MotorClaimModel.DataProvider.tblClaimTransaction_Result.AsQueryable()
        '    ASPxGridPreview_Result.DataSource = result.Where(Function(m) m.GUID.Equals(MyData.GUID)).ToList()
        '    ASPxGridPreview_Result.DataBind()

        '    ASPxGridPreview_Result.Visible = True

        '    'Else
        '    '    ASPxGridPreview_Result.Visible = False
        '    'End If


        '    'formPreview.Enabled = Not MyData.Status.Value



        'End If

    End Sub


End Class
