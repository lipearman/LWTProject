﻿Imports Portal.Components
Imports System.Data.SqlClient
Imports System.Data
Imports DevExpress.Web

Partial Class Modules_ucDevxDashBoardDataSource
    Inherits PortalModuleControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(LWT.Website.webconfig._PortalContextName), PortalSettings)



        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If
        pnMain.HeaderText = portalSettings.ActiveTab.TabName
        pnMain.HeaderImage.IconID = portalSettings.ActiveTab.IconID


        SqlDataSource_Data.SelectParameters("PortalId").DefaultValue = portalSettings.PortalId
        SqlDataSource_Data.InsertParameters("PortalId").DefaultValue = portalSettings.PortalId

    End Sub


    'Protected Sub grid_CellEditorInitialize(ByVal sender As Object, ByVal e As ASPxGridViewEditorEventArgs) Handles grid.CellEditorInitialize




    '    If e.Column.FieldName.Equals("DATABASE") And Not grid.IsNewRowEditing Then

    '        e.Editor.ClientEnabled = False

    '    End If

    'End Sub

End Class
