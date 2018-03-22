Imports Microsoft.VisualBasic
Imports Portal.Components
Imports System.Web.Configuration
Public Class webconfig

    Public Shared _PortalConn As String = ConfigurationSettings.AppSettings("PortalConnectionString")
    Public Shared _PortalID As String = ConfigurationSettings.AppSettings("PortalID")
    Public Shared _PortalContextName As String = ConfigurationSettings.AppSettings("PortalContextName")
    Public Shared _DefaultPageID As String = ConfigurationSettings.AppSettings("DefaultPageID")

    Public Shared _EmpGroupID As String = ConfigurationSettings.AppSettings("EmpGroupID")
    Public Shared _IsDomainSignIn As String = ConfigurationSettings.AppSettings("IsDomainSignin")
    Public Shared _DirectoryPath As String = ConfigurationSettings.AppSettings("DirectoryPath")
    Public Shared _DirectoryDomain As String = ConfigurationSettings.AppSettings("DirectoryDomain")
    Public Shared _AdminRole As String = ConfigurationSettings.AppSettings("AdminRole")
    Public Shared _GuestRole As String = ConfigurationSettings.AppSettings("GuestRole")
End Class
