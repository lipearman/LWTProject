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


Partial Class Modules_ucUWConsentForm
    Inherits PortalModuleControl
    Protected Sub Page_Load(ByVal s As Object, ByVal e As EventArgs) Handles Me.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If

        If Not System.IO.Directory.Exists(Server.MapPath("~/App_Data/ConsentForms/" & HttpContext.Current.User.Identity.Name & "/ConsentForms")) Then
            System.IO.Directory.CreateDirectory(Server.MapPath("~/App_Data/ConsentForms/" & HttpContext.Current.User.Identity.Name & "/ConsentForms"))
        End If

      
        If IsPostBack = False Then
            Dim _Portal_User = MotorClaimModel.DataProvider.Portal_User_Data.AsQueryable()
            Dim _UWCode = _Portal_User.Where(Function(m) m.UserName.Equals(HttpContext.Current.User.Identity.Name)).FirstOrDefault()


            Dim _Path = "~/App_Data/ConsentForms/" & _UWCode.PasswordQuestion & "/ConsentForms"


            If Not Directory.Exists(Server.MapPath(_Path)) Then
                Directory.CreateDirectory(Server.MapPath(_Path))
            End If

            With ASPxFileManager1



                .Settings.RootFolder = _Path
                .SettingsUpload.AdvancedModeSettings.TemporaryFolder = .Settings.RootFolder
                '.SettingsFolders.Visible = False
                '.SettingsToolbar.ShowPath = False
                '.SettingsToolbar.ShowRefreshButton = False
                '.SettingsToolbar.ShowDownloadButton = False


                .SettingsEditing.AllowCreate = True
                .SettingsEditing.AllowRename = True
                .SettingsEditing.AllowMove = True

                '.SettingsUpload.UseAdvancedUploadMode = False
                '.SettingsUpload.AdvancedModeSettings.PacketSize.MinValue = 10
                .SettingsUpload.AdvancedModeSettings.EnableMultiSelect = True

            End With




        End If

      


    End Sub
End Class
