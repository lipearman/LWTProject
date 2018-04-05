Imports Microsoft.VisualBasic
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DataAccess.Native
Imports DevExpress.DataAccess.Web
Imports System.Data.SqlClient


Public Class MyXtraReportsDataSourceWizardConnectionStringsProvider
    Implements IDataSourceWizardConnectionStringsProvider
     

    Public Function GetConnectionDescriptions() As Dictionary(Of String, String) Implements IDataSourceWizardConnectionStringsProvider.GetConnectionDescriptions
        Dim connections As New Dictionary(Of String, String)()

        ' Customize the loaded connections list.  
        connections.Remove("LocalSqlServer")
        'connections.Add("msAccessConnection", "MS Access Connection")


        Using dc As New DataClasses_PortalBIExt()
            Dim _data = (From c In dc.tblDashBoard_DataSources Where c.PortalId = LWT.Website.webconfig._PortalID And c.CONN_TYPE.Equals("msSqlConnection")).ToList()
            For Each item In _data

                connections.Add(item.DS_ID, item.TITLE)

                Dim a = HttpContext.Current.User.Identity.Name
            Next
        End Using

        'connections.Add("msSqlConnection", "MS SQL Connection")
        'connections.Add("olapConnection", "OLAP Data Source")

        Return connections
    End Function

    Public Function GetDataConnectionParameters(name As String) As DataConnectionParametersBase Implements IDataSourceWizardConnectionStringsProvider.GetDataConnectionParameters
        ' Return custom connection parameters for the custom connection.

        Using dc As New DataClasses_PortalBIExt()
            Dim _data = (From c In dc.tblDashBoard_DataSources Where c.DS_ID = name).ToList()
            For Each item In _data

                Select Case item.CONN_TYPE
                    Case "msSqlConnection"
                        Dim SQLbuilder = New SqlConnectionStringBuilder(item.CONN)
                        Return New MsSqlConnectionParameters(SQLbuilder.DataSource, SQLbuilder.InitialCatalog, SQLbuilder.UserID, SQLbuilder.Password, MsSqlAuthorizationType.SqlServer)

                        'Case "olapConnection"
                        '    Return New OlapConnectionParameters(item.CONN)

                        'Case Else
                        '    Return AppConfigHelper.LoadConnectionParameters(name)
                End Select
            Next
        End Using





        'If name = "msAccessConnection" Then
        '    Return New Access97ConnectionParameters("|DataDirectory|nwind.mdb", "", "")
        'ElseIf name = "msSqlConnection" Then
        '    Dim SQLbuilder = New SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings("LWTReportsSIBISDBConnectionString").ConnectionString())
        '    Return New MsSqlConnectionParameters(SQLbuilder.DataSource, SQLbuilder.InitialCatalog, SQLbuilder.UserID, SQLbuilder.Password, MsSqlAuthorizationType.Windows)

        'End If
        Return AppConfigHelper.LoadConnectionParameters(name)
    End Function

End Class
