
Imports System.Data
Imports System.Web.Security
Imports Portal.Components
'Imports FineUI

Imports System.Collections.Generic
Imports System.Linq
'Imports System.Data
Imports System.Reflection
Imports LWT.Website

Partial Class Modules_ucSiteSettings
    Inherits PortalModuleControl

    '*******************************************************
    '
    ' The Page_Load server event handler on this user control is used
    ' to populate the current site settings from the config system
    '
    '*******************************************************
    'Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Init
    '    Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
    '    If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
    '        Response.Redirect("~/Admin/AccessDenied.aspx")
    '    End If
    '    Session("PortalId") = webconfig._PortalID
    '    pnMain.HeaderImage.IconID = "export_exporttomht_16x16office2013"
    '    pnMain.HeaderText = portalSettings.ActiveTab.TabName
    'End Sub


    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If
        Session("PortalId") = webconfig._PortalID
       

        '' Verify that the current user has access to access this page
        'If PortalSecurity.IsInRoles("Admins") = False Then
        '    Response.Redirect("~/Admin/EditAccessDenied.aspx")
        '    'Response.End()
        'End If

        'pnMain.HeaderStyle.BackColor = System.Drawing.Color.Orange

        pnMain.HeaderImage.IconID = portalSettings.ActiveTab.IconID

        pnMain.HeaderText = portalSettings.ActiveTab.TabName


        ' If this is the first visit to the page, populate the site data
        If Page.IsPostBack = False Then

            ' Obtain PortalSettings from Current Context
            'Dim portalSettings As PortalSettings = CType(Context.Items(ConfigurationSettings.AppSettings("PortalContextName")), PortalSettings)


            'siteName.Text = portalSettings.PortalName
            'showEdit.Checked = portalSettings.AlwaysShowEditButton

            'BindData()

        End If
    End Sub

End Class
