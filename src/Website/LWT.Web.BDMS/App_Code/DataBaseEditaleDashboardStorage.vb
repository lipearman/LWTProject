Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Xml.Linq
Imports DevExpress.DashboardWeb

Public Class DataBaseEditaleDashboardStorage
    Implements IEditableDashboardStorage
    Private _GUID As String
    Private _connectionString As String

    'Public Sub New(ByVal connectionString As String, ByVal User As String)
    '    MyBase.New()
    '    Me.user = User
    '    Me.connectionString = connectionString
    '    DashboardConfigurator.PassCredentials = True

    'End Sub

    Public Sub New(ByVal connectionString As String, ByVal GUID As String)
        MyBase.New()
        Me._connectionString = connectionString
        Me._GUID = GUID

    End Sub

    Public Sub DeleteDashboard(ByVal dashboardID As String)
        Using connection As New SqlConnection(_connectionString)
            connection.Open()
            Dim DeleteCommand As New SqlCommand("Delete tblDashBoard_Data where ID=@ID")
            DeleteCommand.Parameters.Add("ID", SqlDbType.Int).Value = dashboardID
            DeleteCommand.Connection = connection
            DeleteCommand.ExecuteNonQuery()

            connection.Close()
        End Using
    End Sub

    Private Function AddDashboard(ByVal document As XDocument, ByVal dashboardName As String) As String Implements IEditableDashboardStorage.AddDashboard
        Using connection As New SqlConnection(_connectionString)
            connection.Open()
            Dim stream As New MemoryStream()
            document.Save(stream)
            stream.Position = 0


            'Dim tr As New StringReader("<?xml version=""1.0"" encoding=""utf-8""?><Dashboard><Title Text=""Production Report"" /><DataConnections><OlapDataConnection Name=""http://172.16.40.234/OLAP/msmdpump.dll"" ConnectionString=""provider=msolap;data source=data source=http://172.16.40.234/OLAP/msmdpump.dll;initial catalog=LWTProduction;cube name='LWTProductionCube';User Id=lockthbnk-db07\biuser;Password=Password123"" /></DataConnections><DataSources><DataSource ComponentName=""dataSource1"" Name=""Data Source 1""><OlapDataProvider DataConnection=""http://172.16.40.234/OLAP/msmdpump.dll"" /></DataSource></DataSources></Dashboard>")
            'Dim doc = XDocument.Load(tr)
            'doc.Save(stream)
            'stream.Position = 0

            Dim InsertCommand As New SqlCommand("INSERT INTO tblDashBoard_Data (Dashboard, Caption,DB_GUID,GUID) " & "output INSERTED.ID " & "VALUES (@Dashboard, @Caption,@DB_GUID,Newid())")
            InsertCommand.Parameters.Add("Caption", SqlDbType.NVarChar).Value = dashboardName
            InsertCommand.Parameters.Add("Dashboard", SqlDbType.VarBinary).Value = stream.ToArray()
            InsertCommand.Parameters.Add("DB_GUID", SqlDbType.NVarChar).Value = Me._GUID
            InsertCommand.Connection = connection
            Dim ID As String = InsertCommand.ExecuteScalar().ToString()
            connection.Close()
            Return ID
        End Using
    End Function


    Private Function LoadDashboard(ByVal dashboardID As String) As XDocument Implements IDashboardStorage.LoadDashboard
        Using connection As New SqlConnection(_connectionString)
            connection.Open()
            Dim GetCommand As New SqlCommand("SELECT  Dashboard FROM tblDashBoard_Data WHERE ID=@ID")
            GetCommand.Parameters.Add("ID", SqlDbType.Int).Value = Convert.ToInt32(dashboardID)
            GetCommand.Connection = connection
            Dim reader As SqlDataReader = GetCommand.ExecuteReader()
            reader.Read()
            Dim data() As Byte = TryCast(reader.GetValue(0), Byte())
            Dim stream As New MemoryStream(data)
            connection.Close()
            Return XDocument.Load(stream)
        End Using
    End Function

    Private Function GetAvailableDashboardsInfo() As IEnumerable(Of DashboardInfo) Implements IDashboardStorage.GetAvailableDashboardsInfo

        Dim list As New List(Of DashboardInfo)()
        Using connection As New SqlConnection(_connectionString)
            connection.Open()
            Dim GetCommand As New SqlCommand("SELECT ID, Caption FROM tblDashBoard_Data where DB_GUID=@DB_GUID")
            GetCommand.Parameters.Add("DB_GUID", SqlDbType.VarChar).Value = _GUID
            GetCommand.Connection = connection
            Dim reader As SqlDataReader = GetCommand.ExecuteReader()
            Do While reader.Read()
                Dim ID As String = reader.GetInt32(0).ToString()
                Dim Caption As String = reader.GetString(1)
                list.Add(New DashboardInfo() With {.ID = ID, .Name = Caption})
            Loop
            connection.Close()
        End Using
        Return list
    End Function

    Private Sub SaveDashboard(ByVal dashboardID As String, ByVal document As XDocument) Implements IDashboardStorage.SaveDashboard


        Dim Caption = document.Root.Elements("Title").Select(Function(e) e.Attribute("Text")).Select(Function(x) x.Value).FirstOrDefault()

        Using connection As New SqlConnection(_connectionString)
            connection.Open()
            Dim stream As New MemoryStream()
            document.Save(stream)
            stream.Position = 0

            Dim updateCommand As New SqlCommand("UPDATE tblDashBoard_Data Set Dashboard = @Dashboard, Caption=@Caption " & "WHERE ID = @ID")
            updateCommand.Parameters.Add("ID", SqlDbType.Int).Value = Convert.ToInt32(dashboardID)
            updateCommand.Parameters.Add("Dashboard", SqlDbType.VarBinary).Value = stream.ToArray()
            updateCommand.Parameters.Add("Caption", SqlDbType.VarChar).Value = Caption
            updateCommand.Connection = connection
            updateCommand.ExecuteNonQuery()

            connection.Close()
        End Using

    End Sub

End Class
