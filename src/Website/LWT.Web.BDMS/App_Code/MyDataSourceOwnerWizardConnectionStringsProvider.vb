Imports Microsoft.VisualBasic
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Native
Imports DevExpress.DataAccess.Web
Imports System.Data.SqlClient


Public Class MyDataSourceOwnerWizardConnectionStringsProvider
    Implements IDataSourceWizardConnectionStringsProvider
    Private _PortalId As String
    Private _Owner As String

    Public Sub New(ByVal PortalId As String, ByVal Owner As String)
        MyBase.New()
        Me._PortalId = PortalId
        Me._Owner = Owner

    End Sub

    'Public Function GetConnectionDescriptions() As Dictionary(Of String, String)

    'End Function

    'Public Function GetDataConnectionParameters(ByVal name As String) As DataConnectionParametersBase

    'End Function

    Public Function GetConnectionDescriptions() As Dictionary(Of String, String) Implements IDataSourceWizardConnectionStringsProvider.GetConnectionDescriptions
        Dim connections As New Dictionary(Of String, String)()

        ' Customize the loaded connections list.  
        connections.Remove("LocalSqlServer")
        'connections.Add("msAccessConnection", "MS Access Connection")


        'Using dc As New DataClasses_PortalBIExt()
        '    Dim _data = (From c In dc.tblDashBoard_DataSources Where c.PortalId = Me._PortalId).ToList()
        '    For Each item In _data

        '        connections.Add(item.DS_ID, item.TITLE)

        '    Next
        'End Using

        Return connections
    End Function

    Public Function GetDataConnectionParameters(name As String) As DataConnectionParametersBase Implements IDataSourceWizardConnectionStringsProvider.GetDataConnectionParameters
        ' Return custom connection parameters for the custom connection.

        'Using dc As New DataClasses_PortalBIExt()
        '    Dim _data = (From c In dc.tblDashBoard_DataSources Where c.DS_ID = name).ToList()
        '    For Each item In _data

        '        Select Case item.CONN_TYPE
        '            Case "msSqlConnection"
        '                Dim SQLbuilder = New SqlConnectionStringBuilder(item.CONN)
        '                Return New MsSqlConnectionParameters(SQLbuilder.DataSource, SQLbuilder.InitialCatalog, SQLbuilder.UserID, SQLbuilder.Password, MsSqlAuthorizationType.SqlServer)

        '            Case "olapConnection"
        '                Return New OlapConnectionParameters(item.CONN)

        '                'Case Else
        '                '    Return AppConfigHelper.LoadConnectionParameters(name)
        '        End Select
        '    Next
        'End Using




 
        Return AppConfigHelper.LoadConnectionParameters(name)
    End Function
End Class
