Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports LWT.Website


Imports Microsoft.VisualBasic
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.IO
Imports System.Web.UI
Imports DevExpress.Web
Imports DevExpress.Web.ASPxTreeList
Imports DevExpress.Web.Data
Imports DevExpress.Web.Demos



Partial Class Modules_ucModuleDefs
    Inherits PortalModuleControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If

        pnMain.HeaderImage.IconID = portalSettings.ActiveTab.IconID
        pnMain.HeaderText = portalSettings.ActiveTab.TabName

        Session("PortalId") = webconfig._PortalID


        ObjectDataSource_ModuleFiles.SelectParameters("rootpath").DefaultValue = Server.MapPath("~/App_Data/Modules")
        ObjectDataSource_ModuleFiles.SelectParameters("pathname").DefaultValue = ConfigurationSettings.AppSettings("ModulePathName")




        SqlDataSource_Modules.InsertParameters("ModuleDefDesc").DefaultValue = portalSettings.PortalName
    End Sub

     




End Class
