Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Configuration
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DashboardWeb
Imports DevExpress.DashboardCommon

Partial Class Demo
    Inherits System.Web.UI.Page
    Private Shared dataBaseDashboardStorage As New DataBaseEditaleDashboardStorage(ConfigurationManager.ConnectionStrings("DashboardStorageConnection").ConnectionString)


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Init
        'ASPxWebDashboard1.WorkingMode = WorkingMode.Viewer
        ASPxWebDashboard1.SetDashboardStorage(dataBaseDashboardStorage)
    End Sub

    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load




    '    If Page.IsPostBack = False Then
    '        ASPxWebDashboard1.AllowCreateNewDashboard = False
    '        ASPxWebDashboard1.AllowOpenDashboard = False
    '        '    'ASPxWebDashboard1.WorkingMode = WorkingMode.Designer
    '        ASPxWebDashboard1.UseCardLegacyLayout = False

    '        '    'ASPxWebDashboard1.SetDashboardStorage(dataBaseDashboardStorage)

    '        '    'DevExpress.DataAccess.Web.IDataSourceWizardConnectionStringsProvider()

    '        '    'ASPxWebDashboard1.UseDashboardConfigurator = True

    '        '    'ASPxWebDashboard1.DashboardId = "SalesDashboard"

    '        ASPxWebDashboard1.InitialDashboardId = 1
    '        Dim olapDataSource As New DashboardOlapDataSource("OLAP Data Source", "olapConnection")
    '        Dim dataSourceStorage As New DataSourceInMemoryStorage()
    '        dataSourceStorage.RegisterDataSource("olapDataSource", olapDataSource.SaveToXml())
    '        ASPxWebDashboard1.SetDataSourceStorage(dataSourceStorage)



    '    End If



    'End Sub

    'Protected Sub ASPxDashboardViewer1_ConfigureDataConnection(sender As Object, e As DevExpress.DashboardWeb.ConfigureDataConnectionWebEventArgs) Handles ASPxWebDashboard1.ConfigureDataConnection
    '    Dim parameters As OlapConnectionParameters = DirectCast(e.ConnectionParameters, OlapConnectionParameters)
    '    If parameters IsNot Nothing Then
    '        Dim a = ASPxWebDashboard1.InitialDashboardId


    '        DashboardConfigurator.PassCredentials = True

    '        Dim obj As New OlapConnectionParameters With {.ConnectionString = "data source=http://172.16.40.234/OLAP/msmdpump.dll;initial catalog=LWTProduction;cube name='LWTProductionCube';User Id=lockthbnk-db07\biuser;Password=Password123"}

    '        e.ConnectionParameters = obj


    '    End If
    'End Sub


    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
    '    ' Specifies the dashboard identifier.
    '    ASPxDashboardViewer1.DashboardId = "SalesDashboard"
    'End Sub

    ' Handles the DashboardLoading event.
    'Protected Sub ASPxDashboardViewer1_DashboardLoading(ByVal sender As Object, _
    '                                   ByVal e As DashboardLoadingEventArgs)

    '    ' Checks the identifier of the required dashboard.
    '    If e.DashboardId = "SalesDashboard" Then

    '        ' Writes the dashboard XML definition from a file to a string.
    '        Dim definitionPath As String = Server.MapPath("App_Data/SalesDashboard.xml")
    '        Dim dashboardDefinition As String = File.ReadAllText(definitionPath)

    '        ' Specifies the dashboard XML definition.
    '        e.DashboardXml = dashboardDefinition
    '    End If
    'End Sub

    '' Handles the ConfigureDataConnection event.
    'Protected Sub ASPxDashboardViewer1_ConfigureDataConnection(ByVal sender As Object, _
    '                                   ByVal e As ConfigureDataConnectionWebEventArgs)
    '    If e.DataSourceName = "SQL Data Source 1" Then

    '        ' Gets connection parameters used to establish a connection to the database.
    '        Dim parameters As Access97ConnectionParameters =
    '            CType(e.ConnectionParameters, Access97ConnectionParameters)
    '        Dim databasePath As String = Server.MapPath("App_Data/nwind.mdb")

    '        ' Specifies the path to a database file.                    
    '        parameters.FileName = databasePath
    '    End If
    'End Sub

End Class
