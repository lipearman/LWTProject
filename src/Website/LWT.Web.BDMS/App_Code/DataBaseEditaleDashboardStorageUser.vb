Imports Microsoft.VisualBasic
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Linq
Imports System.Text
Imports System.Xml.Linq
Imports DevExpress.DashboardWeb

Public Class DataBaseEditaleDashboardStorageUser
    Implements IEditableDashboardStorage
    Private _GUID As String
    Private _connectionString As String
    Private _User As String
    'Public Sub New(ByVal connectionString As String, ByVal User As String)
    '    MyBase.New()
    '    Me.user = User
    '    Me.connectionString = connectionString
    '    DashboardConfigurator.PassCredentials = True

    'End Sub

    Public Sub New(ByVal connectionString As String, ByVal GUID As String, User As String)
        MyBase.New()
        Me._connectionString = connectionString
        Me._GUID = GUID
        Me._User = User
        'DashboardConfigurator.PassCredentials = True

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

            Dim InsertCommand As New SqlCommand("INSERT INTO tblDashBoard_Data (Dashboard, Caption,GUID) " & "output INSERTED.ID " & "VALUES (@Dashboard, @Caption,newid())")
            InsertCommand.Parameters.Add("Caption", SqlDbType.NVarChar).Value = dashboardName
            InsertCommand.Parameters.Add("Dashboard", SqlDbType.VarBinary).Value = stream.ToArray()
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
            Dim GetCommand As New SqlCommand("SELECT ID, Caption FROM V_Dashboard_Assignment where DB_GUID=@DB_GUID and [UserName]=@UserName Order By [No]")
            GetCommand.Parameters.Add("DB_GUID", SqlDbType.VarChar).Value = _GUID
            GetCommand.Parameters.Add("UserName", SqlDbType.VarChar).Value = _User
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
        Using connection As New SqlConnection(_connectionString)
            connection.Open()
            Dim stream As New MemoryStream()
            document.Save(stream)
            stream.Position = 0

            Dim InsertCommand As New SqlCommand("UPDATE tblDashBoard_Data Set Dashboard = @Dashboard " & "WHERE ID = @ID")
            InsertCommand.Parameters.Add("ID", SqlDbType.Int).Value = Convert.ToInt32(dashboardID)
            InsertCommand.Parameters.Add("Dashboard", SqlDbType.VarBinary).Value = stream.ToArray()
            InsertCommand.Connection = connection
            InsertCommand.ExecuteNonQuery()

            connection.Close()
        End Using

    End Sub

End Class
