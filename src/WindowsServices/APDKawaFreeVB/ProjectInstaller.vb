Imports System.ComponentModel
Imports System.Configuration
Imports System.Configuration.Install
Imports System.ServiceProcess
Imports System.Reflection
 
Public Class ProjectInstaller

    Public Sub New()
        'MyBase.New()

        ' ''This call is required by the Component Designer.
        'InitializeComponent()

        Me.Installers.Add(GetServiceInstaller())
        Me.Installers.Add(GetServiceProcessInstaller())

         
    End Sub
    Private Function GetServiceInstaller() As ServiceInstaller
        Dim installer As New ServiceInstaller()
        installer.ServiceName = GetConfigurationValue("ServiceName")
        installer.DisplayName = GetConfigurationValue("DisplayName")
        installer.Description = GetConfigurationValue("Description")
        installer.StartType = ServiceStartMode.Automatic
        installer.DelayedAutoStart = False
        Return installer
    End Function

    Private Function GetServiceProcessInstaller() As ServiceProcessInstaller
        Dim installer As New ServiceProcessInstaller()
        installer.Account = ServiceAccount.LocalSystem
        'installer.Username = GetConfigurationValue("Username")
        'installer.Password = MyUtils.Decrypt(GetConfigurationValue("Password"))

        Return installer
    End Function

    Private Function GetConfigurationValue(key As String) As String
        Dim service As Assembly = Assembly.GetAssembly(GetType(ProjectInstaller))
        Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(service.Location)
        If config.AppSettings.Settings(key) IsNot Nothing Then
            Return config.AppSettings.Settings(key).Value
        Else
            Throw New IndexOutOfRangeException(Convert.ToString("Settings collection does not contain the requested key:") & key)
        End If
    End Function

    Protected Overrides Sub OnAfterInstall(savedState As IDictionary)
        MyBase.OnAfterInstall(savedState)

        'The following code starts the services after it is installed.
        Using serviceController As New System.ServiceProcess.ServiceController(GetConfigurationValue("ServiceName"))
            serviceController.Start()
        End Using
    End Sub


End Class
