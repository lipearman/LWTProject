Imports Portal.Components
Imports DevExpress.Web.ASPxTreeList
Imports DevExpress.Web
Imports System.Data.SqlClient
Imports System.Data
Imports DevExpress.Web.Data
Imports System.IO
Imports DevExpress.DataAccess.Excel
Imports DevExpress.XtraPivotGrid
Imports DevExpress.Web.ASPxPivotGrid
Imports System.Web.Hosting

Partial Class Modules_ucXtraReportsAdminManager
    Inherits PortalModuleControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(LWT.Website.webconfig._PortalContextName), PortalSettings)



        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If
        pnMain.HeaderText = portalSettings.ActiveTab.TabName
        pnMain.HeaderImage.IconID = portalSettings.ActiveTab.IconID
    End Sub


    Protected Sub grid_CellEditorInitialize(ByVal sender As Object, ByVal e As ASPxGridViewEditorEventArgs) Handles grid.CellEditorInitialize




        'If e.Column.FieldName.Equals("DATABASE") And Not grid.IsNewRowEditing Then

        '    e.Editor.ClientEnabled = False

        'End If

    End Sub

    Protected Sub cbPreview_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbPreview.Callback

        Dim _ReportGUID = e.Parameter

        'Using dc As New DataClasses_PortalBIExt()
        '    Dim Data = (From c In dc.tblXtraReports Where c.ReportID.Equals(_BID)).FirstOrDefault()
        '    Session("GUID") = Data.GUID

        '    e.Result = Data.GUID
        'End Using
        Session("reportGUID") = _ReportGUID

        e.Result = _ReportGUID
    End Sub
End Class


