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
    Private Shared dataBaseDashboardStorage As New DataBaseEditaleDashboardStorage(ConfigurationManager.ConnectionStrings("PortalBIConnection").ConnectionString, "757a354d-f3ad-4f9f-8456-f6c264fb93ab")

    Public Property DashboardDataSourceInMemoryStorage() As Object
        Get
            Return m_DashboardDataSourceInMemoryStorage
        End Get
        Private Set(value As Object)
            m_DashboardDataSourceInMemoryStorage = Value
        End Set
    End Property
    Private m_DashboardDataSourceInMemoryStorage As Object


    'Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
    '    Dim newDashboardStorage As New DashboardFileStorage("~/App_Data/Dashboards")
    '    ASPxWebDashboard1.SetDashboardStorage(newDashboardStorage)

    '    Dim ds As New DashboardOlapDataSource("Olap Data Source 1", "OlapConnection")
    '    Dim dataSourceStorage As New DataSourceInMemoryStorage()
    '    dataSourceStorage.RegisterDataSource(ds.SaveToXml())
    '    ASPxWebDashboard1.SetDataSourceStorage(dataSourceStorage)
    'End Sub

    'Protected Sub ASPxDashboard1_ConfigureDataConnection(sender As Object, e As ConfigureDataConnectionWebEventArgs) 'Handles ASPxWebDashboard1.ConfigureDataConnection
    '    If e.ConnectionName = "OlapConnection" Then
    '        Dim connectionString As String = String.Format("provider=msolap;data source={0};initial catalog=APDProductionBudgetCube;timeout=1200;Cube Name=APDProductionBudgetCube", Server.MapPath("~/App_Data/APDProductionBudgetCube.cub"))
    '        e.ConnectionParameters = New OlapConnectionParameters() With { _
    '            .ConnectionString = connectionString _
    '        }
    '    End If
    'End Sub


    'Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Init
    '    ''ASPxWebDashboard1.WorkingMode = WorkingMode.Viewer
    '    ''ASPxWebDashboard1.SetDashboardStorage(dataBaseDashboardStorage)
    '    'DashboardConfigurator.PassCredentials = True
    '    ''ASPxWebDashboard1.UseDashboardConfigurator = True
    '    'ASPxWebDashboard1.SetConnectionStringsProvider(New MyDataSourceWizardConnectionStringsProvider())


    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        'Dim newDashboardStorage = New DashboardFileStorage("~/App_Data/Dashboards")
        'ASPxWebDashboard1.SetDashboardStorage(newDashboardStorage)

        DashboardConfigurator.PassCredentials = True
        ASPxWebDashboard1.SetDashboardStorage(dataBaseDashboardStorage)

        Dim olapDataSource As New DashboardOlapDataSource("OLAP Data Source", "olapConnection")
        Dim dataSourceStorage As New DataSourceInMemoryStorage()
        dataSourceStorage.RegisterDataSource("olapDataSource", olapDataSource.SaveToXml())
        ASPxWebDashboard1.SetDataSourceStorage(dataSourceStorage)



    End Sub

    Protected Sub ASPxDashboardViewer1_ConfigureDataConnection(sender As Object, e As DevExpress.DashboardWeb.ConfigureDataConnectionWebEventArgs) Handles ASPxWebDashboard1.ConfigureDataConnection
        'DevExpress.DataAccess.Sql.SqlDataSource.DisableCustomQueryValidation = True

        'Dim parameters As OlapConnectionParameters = DirectCast(e.ConnectionParameters, OlapConnectionParameters)

        'Dim obj As New OlapConnectionParameters With {.ConnectionString = "Provider=MSOLAP;data source=C:\Users\dusit\Desktop\APDProductionBudgetCube.cub;initial catalog=APDProductionBudgetCube;timeout=1200;Cube Name=APDProductionBudgetCube;"}

        'e.ConnectionParameters = obj



        'If e.ConnectionName = "olapConnection" Then

        '    Dim obj As New OlapConnectionParameters With {.ConnectionString = "provider=msolap;data source=~/App_Data/Cubes/APDProductionBudgetCube.cub;initial catalog=APDProductionBudgetCube;timeout=1200;Cube Name=APDProductionBudgetCube"}

        '    e.ConnectionParameters = obj


        '    'string connectionString = string.Format("provider=msolap;data source=~/App_Data/Cubes/APDProductionBudgetCube.cub;initial catalog=APDProductionBudgetCube;timeout=1200;Cube Name=APDProductionBudgetCube", Server.MapPath("~/App_Data/APDProductionBudgetCube.cub"));
        '    'e.ConnectionParameters = new OlapConnectionParameters() { ConnectionString = connectionString };
        'End If


        If e.ConnectionName = "OlapConnection" Then
            Dim connectionString As String = String.Format("provider=msolap;data source={0};initial catalog=LWTProductionCube;timeout=1200;Cube Name=LWTProductionCube", Server.MapPath("~/App_Data/Cubes/LWTProductionCube.cub"))
            e.ConnectionParameters = New OlapConnectionParameters() With { _
                .ConnectionString = connectionString _
            }
        End If

    End Sub






    'Protected Sub ASPxDashboardViewer1_ConfigureDataConnection(sender As Object, e As DevExpress.DashboardWeb.ConfigureDataConnectionWebEventArgs) Handles ASPxWebDashboard1.ConfigureDataConnection
    '    Dim parameters As OlapConnectionParameters = DirectCast(e.ConnectionParameters, OlapConnectionParameters)
    '    If parameters IsNot Nothing Then
    '        Dim a = ASPxWebDashboard1.InitialDashboardId


    '        DashboardConfigurator.PassCredentials = True

    '        Dim obj As New OlapConnectionParameters With {.ConnectionString = "data source=C:\\temp\\APDProductionBudgetCube.cub;initial catalog=APDProductionBudgetCube;timeout=1200;Cube Name=APDProductionBudgetCube;"}

    '        e.ConnectionParameters = obj


    '    End If
    '    'Dim offlineCubeParameters As New OlapConnectionParameters("provider=msolap;data source=d:\\AdventureWorks.cub;initial catalog=Adventure Works;Cube Name=Adventure Works;")
    '    'Dim offlineCubeConnection As New OlapDataConnection("Offline Cube", offlineCubeParameters)
    '    'Dim sqlProvider As New OlapDataProvider(offlineCubeConnection)
    '    'Dim olapDataSource As New DataSource("SQL Data Source", sqlProvider)
    '    'ASPxWebDashboard1.Dashboard.DataSources.Add(olapDataSource)


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
