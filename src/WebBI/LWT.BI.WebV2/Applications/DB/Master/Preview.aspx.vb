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
Imports System.Web.Script.Serialization
Imports DevExpress.DataAccess.Sql
Imports System.Data.SqlClient
Imports System.Data
Imports DevExpress.Web

Partial Class applications_DB_Preview
    Inherits System.Web.UI.Page
    Private Shared dataBaseDashboardStorage As New DataBaseEditaleDashboardStorage(ConfigurationManager.ConnectionStrings("PortalBIConnection").ConnectionString, "")
    Private Shared _GUID As String


    'Public Property DashboardDataSourceInMemoryStorage() As Object
    '    Get
    '        Return m_DashboardDataSourceInMemoryStorage
    '    End Get
    '    Private Set(value As Object)
    '        m_DashboardDataSourceInMemoryStorage = Value
    '    End Set
    'End Property
    'Private m_DashboardDataSourceInMemoryStorage As Object




    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Init
        ASPxWebDashboard1.WorkingMode = WorkingMode.Viewer
        DashboardConfigurator.PassCredentials = True
        'dataBaseDashboardStorage = New DataBaseEditaleDashboardStorage(ConfigurationManager.ConnectionStrings("PortalBIConnection").ConnectionString, Session("GUID").ToString())
        'ASPxWebDashboard1.SetDashboardStorage(dataBaseDashboardStorage)


        dataBaseDashboardStorage = New DataBaseEditaleDashboardStorage(ConfigurationManager.ConnectionStrings("PortalBIConnection").ConnectionString, Session("GUID").ToString())

        ASPxWebDashboard1.SetDashboardStorage(dataBaseDashboardStorage)
        ASPxWebDashboard1.SetConnectionStringsProvider(New MyDataSourceWizardConnectionStringsProvider(LWT.Website.webconfig._PortalID))
        ASPxWebDashboard1.UseDashboardConfigurator = False
        'ASPxWebDashboard1.EnableCustomSql = True
        ASPxWebDashboard1.SetDBSchemaProvider(New CustomDBSchemaProvider(LWT.Website.webconfig._PortalID))

        'If Not Page.IsPostBack Then
        Using dc As New DataClasses_PortalBIExt()
            Dim dataSourceStorage As New DataSourceInMemoryStorage()

            'Dim ds = (From c In dc.tblDashBoard_DataSources Where c.PortalId.Equals(LWT.Website.webconfig._PortalID) And c.CONN_TYPE.Equals("msSqlConnection")).ToList()
            'For Each item_ds In ds

            '    Dim sqlbuilder As New SqlConnectionStringBuilder(item_ds.CONN)
            '    Dim sqlParams As New MsSqlConnectionParameters()
            '    sqlParams.AuthorizationType = MsSqlAuthorizationType.SqlServer
            '    sqlParams.DatabaseName = sqlbuilder.InitialCatalog
            '    sqlParams.ServerName = sqlbuilder.DataSource
            '    sqlParams.UserName = sqlbuilder.UserID
            '    sqlParams.Password = sqlbuilder.Password
            '    Dim tables = Portal.Components.SqlHelper.ExecuteDataset(item_ds.CONN, System.Data.CommandType.Text, "SELECT name FROM sys.Views union all SELECT name FROM sys.Tables").Tables
            '    For Each tb As DataTable In tables
            '        Dim tablename = tb.Rows(0)("name")
            '        Dim sqlDataSource As New DashboardSqlDataSource(tablename, sqlParams)
            '        sqlDataSource.Connection.ConnectionString = item_ds.CONN

            '        Dim Columns As New List(Of String)
            '        Dim dt = Portal.Components.SqlHelper.ExecuteDataset(item_ds.CONN, System.Data.CommandType.Text, "select top 1 * from [" & tablename & "]").Tables(0)
            '        For j = 0 To dt.Columns.Count - 1
            '            If dt.Columns(j).ColumnName <> "" Or dt.Columns(j).ColumnName.ToLower <> "id" Then
            '                Columns.Add(dt.Columns(j).ColumnName)
            '            End If
            '        Next

            '        Dim selectQuery As SelectQuery = SelectQueryFluentBuilder.AddTable(tablename).SelectColumns(Columns.ToArray()).Build(tablename)
            '        sqlDataSource.Queries.Add(selectQuery)
            '        dataSourceStorage.RegisterDataSource(tablename, sqlDataSource.SaveToXml())
            '    Next

            'Next





            'Dim ds2 = (From c In dc.tblDashBoard_DataSources Where c.PortalId.Equals(LWT.Website.webconfig._PortalID) And c.CONN_TYPE.Equals("msSqlConnection")).ToList()
            'For Each item_ds2 In ds2



            '    Dim sqlbuilder As New SqlConnectionStringBuilder(item_ds2.CONN)
            '    Dim sqlParams As New MsSqlConnectionParameters()
            '    sqlParams.AuthorizationType = MsSqlAuthorizationType.SqlServer
            '    sqlParams.DatabaseName = sqlbuilder.InitialCatalog
            '    sqlParams.ServerName = sqlbuilder.DataSource
            '    sqlParams.UserName = sqlbuilder.UserID
            '    sqlParams.Password = sqlbuilder.Password


            '    Dim myTables = Portal.Components.SqlHelper.ExecuteDataset(item_ds2.CONN, System.Data.CommandType.Text, "SELECT name FROM sys.Views where is_ms_shipped=0").Tables(0)


            '    For Each dr As DataRow In myTables.Rows


            '        Dim dt = Portal.Components.SqlHelper.ExecuteDataset(item_ds2.CONN, System.Data.CommandType.Text, "select top 0 * from [" & dr("name") & "]").Tables(0)



            '        Dim sqlDataSource As New DashboardSqlDataSource(dr("name"), sqlParams)
            '        sqlDataSource.Connection.ConnectionString = item_ds2.CONN
            '        Dim Columns As New List(Of String)

            '        For j = 0 To dt.Columns.Count - 1
            '            If dt.Columns(j).ColumnName <> "" Or dt.Columns(j).ColumnName.ToLower <> "id" Then
            '                Columns.Add(dt.Columns(j).ColumnName)
            '            End If
            '        Next
            '        Dim selectQuery As SelectQuery = SelectQueryFluentBuilder.AddTable(dr("name")).SelectColumns(Columns.ToArray()).Build(dr("name"))
            '        sqlDataSource.Queries.Add(selectQuery)
            '        dataSourceStorage.RegisterDataSource(dr("name"), sqlDataSource.SaveToXml())


            '    Next



            'Next

























            Dim sqlbuilder As New SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings("PortalBIRawDataConnection").ConnectionString)
            Dim sqlParams As New MsSqlConnectionParameters()
            sqlParams.AuthorizationType = MsSqlAuthorizationType.SqlServer
            sqlParams.DatabaseName = sqlbuilder.InitialCatalog
            sqlParams.ServerName = sqlbuilder.DataSource
            sqlParams.UserName = sqlbuilder.UserID
            sqlParams.Password = sqlbuilder.Password
            Dim _data = (From c In dc.tblDataSourceFiles Where c.PortalId = LWT.Website.webconfig._PortalID).ToList()
            For Each item In _data
                Dim sqlDataSource As New DashboardSqlDataSource(item.Title, sqlParams)
                sqlDataSource.Connection.ConnectionString = ConfigurationManager.ConnectionStrings("PortalBIRawDataConnection").ConnectionString
                Dim Columns As New List(Of String)
                Dim dt = Portal.Components.SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("PortalBIConnection").ConnectionString, System.Data.CommandType.Text, "select top 1 * from [Portal.BI.RawData].[dbo].[" & item.GUID & "]").Tables(0)
                For j = 0 To dt.Columns.Count - 1
                    If dt.Columns(j).ColumnName <> "" Or dt.Columns(j).ColumnName.ToLower <> "id" Then
                        Columns.Add(dt.Columns(j).ColumnName)
                    End If
                Next
                Dim selectQuery As SelectQuery = SelectQueryFluentBuilder.AddTable(item.GUID).SelectColumns(Columns.ToArray()).Build(item.Title)
                sqlDataSource.Queries.Add(selectQuery)
                dataSourceStorage.RegisterDataSource(item.Title, sqlDataSource.SaveToXml())
            Next



            ASPxWebDashboard1.SetDataSourceStorage(dataSourceStorage)
        End Using
        'End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load




    End Sub


    'Protected Sub ASPxDashboardViewer1_ConfigureDataConnection(sender As Object, e As DevExpress.DashboardWeb.ConfigureDataConnectionWebEventArgs) Handles ASPxWebDashboard1.ConfigureDataConnection
    '    'Dim parameters As MsSqlConnectionParameters = DirectCast(e.ConnectionParameters, MsSqlConnectionParameters)
    '    'If parameters IsNot Nothing Then
    '    '    If ASPxWebDashboard1.DashboardId IsNot Nothing Then
    '    '        Using dc As New DataClasses_PortalBIExt()

    '    '            Dim DashboardId = ASPxWebDashboard1.DashboardId

    '    '            Dim _data = (From c In dc.V_Dashboard_Datas Where c.ID = DashboardId).FirstOrDefault()

    '    '            Dim _conn = _data.CONNECTING

    '    '            DashboardConfigurator.PassCredentials = True

    '    '            Dim obj As New MsSqlConnectionParameters With {.ConnectionString = _conn}

    '    '            e.ConnectionParameters = obj

    '    '        End Using
    '    '    End If
    '    'End If


    '    'Dim a = e.ConnectionParameters

    '    'Select Case e.ConnectionParameters.GetType().Name
    '    '    Case "MsSqlConnectionParameters"

    '    '        Using dc As New DataClasses_PortalBIExt()


    '    '            Dim sqlParams As MsSqlConnectionParameters = DirectCast(e.ConnectionParameters, MsSqlConnectionParameters)

    '    '            Dim sqlDataSource As New DashboardSqlDataSource(e.ConnectionParameters.GetType().Name, sqlParams)


    '    '            Dim _data = (From c In dc.tblDataSourceFiles Where c.PortalId = LWT.Website.webconfig._PortalID).ToList()

    '    '            For Each item In _data
    '    '                Dim selectQuery As SelectQuery = SelectQueryFluentBuilder.AddTable(item.Title).SelectAllColumnsFromTable().Build(item.GUID)
    '    '                sqlDataSource.Queries.Add(selectQuery)

    '    '            Next


    '    '            Dim dataSourceStorage As New DataSourceInMemoryStorage()
    '    '            dataSourceStorage.RegisterDataSource("msSqlConnection", sqlDataSource.SaveToXml())
    '    '            ASPxWebDashboard1.SetDataSourceStorage(dataSourceStorage)



    '    '        End Using



    '    '    Case "OlapConnectionParameters"
    '    'End Select



    'End Sub


    Protected Sub ASPxDashboard1_CustomDataCallback(ByVal sender As Object, ByVal e As DevExpress.Web.CustomDataCallbackEventArgs) Handles ASPxWebDashboard1.CustomDataCallback
        Dim parameters As Dictionary(Of String, String) = (New JavaScriptSerializer()).Deserialize(Of Dictionary(Of String, String))(e.Parameter)
        If Not parameters.ContainsKey("ExtensionName") Then
            Return
        End If

        'Dim newDashboardStorage As New CustomDashboardFileStorage("~/App_Data/Dashboards")
        If parameters("ExtensionName") = "dxdde-delete-dashboard" AndAlso parameters.ContainsKey("DashboardID") Then
            dataBaseDashboardStorage.DeleteDashboard(parameters("DashboardID"))
        End If



    End Sub




    Protected Sub cbPreview_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbPreview.Callback

        Dim _dashboardid = CInt(e.Parameter)

        Using dc As New DataClasses_PortalBIExt()
            Dim _data = (From c In dc.tblDashBoard_Datas Where c.ID = _dashboardid).FirstOrDefault()
            Session("GUID") = _data.GUID
            e.Result = _data.GUID

            'Dim _data = (From c In dc.V_Dashboard_Datas Where c.ParentID = _DB_ID).ToList()

            'If _data.Count > 0 Then
            '    'Dim _DB = (From c In dc.tblDashBoard_Datas Where c.DB_GUID = _data.GUID).Count
            '    Session("GUID") = _data(0).DB_GUID
            '    e.Result = _data(0).DB_GUID

            'Else
            '    e.Result = ""
            'End If

        End Using


        'Session("GUID") = e.Parameter
        'e.Result = e.Parameter


    End Sub
End Class
