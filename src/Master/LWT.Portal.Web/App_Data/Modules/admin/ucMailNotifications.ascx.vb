﻿
Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports FineUI


Public Class ucMailNotifications
    Inherits PortalModuleControl
    '*******************************************************
    '
    ' The Page_Load server event handler on this user control is used
    ' to populate the current site settings from the config system
    '
    '*******************************************************
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If


        'ValidateRequest="false" EnableEventValidation="false" 


        '' Verify that the current user has access to access this page
        'If PortalSecurity.IsInRoles("Admins") = False Then
        '    Response.Redirect("~/Admin/EditAccessDenied.aspx")
        '    'Response.End()
        'End If

        ' If this is the first visit to the page, populate the site data
        If Page.IsPostBack = False Then
           
        End If
    End Sub
End Class