'Imports Portal.Web.BI
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DashboardWeb
Imports System.Data.SqlClient

Partial Class applications_DB_Preview
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        ''ddl_GroupSource.DataBind()
        If (Not IsPostBack) Then
            If Session("DB_ID") IsNot Nothing Then

                Try
                    Dim _DB_ID = CInt(Session("DB_ID"))

                    Using dc As New DataClasses_PortalBIExt()

                        Dim _data = (From c In dc.tblDashBoard_DataSources Where c.DB_ID.Equals(_DB_ID)).FirstOrDefault()

                        ASPxDashboardViewer1.DashboardSource = "~/App_Data/DashBoard/" & _data.DB_XML
                        Session("Title") = _data.DB_TITLE


                        Dim SQLbuilder = New SqlConnectionStringBuilder(_data.DB_CONN)
                        Session("ServerName") = SQLbuilder.DataSource
                        Session("DatabaseName") = SQLbuilder.InitialCatalog
                        Session("UserName") = SQLbuilder.UserID
                        Session("Password") = SQLbuilder.Password

                     
                    End Using
                Catch ex As Exception

                End Try


            End If
        End If



    End Sub




    Protected Sub ASPxDashboardViewer1_ConfigureDataConnection(sender As Object, e As DevExpress.DashboardWeb.ConfigureDataConnectionWebEventArgs) Handles ASPxDashboardViewer1.ConfigureDataConnection
        '<Parameter Name="server" Value="localhost" />
        '<Parameter Name="database" Value="LWTReports" />
        '<Parameter Name="useIntegratedSecurity" Value="False" />
        '<Parameter Name="read only" Value="1" />
        '<Parameter Name="generateConnectionHelper" Value="false" />
        '<Parameter Name="useDB_ID" Value="sa" />
        '<Parameter Name="password" Value="rsa" />
        ' Gets the connection parameters used to establish a connection to the database.
        Dim parameters As MsSqlConnectionParameters = CType(e.ConnectionParameters, MsSqlConnectionParameters)
        If parameters IsNot Nothing Then
            parameters.AuthorizationType = MsSqlAuthorizationType.SqlServer
            parameters.ServerName = Session("ServerName")
            parameters.DatabaseName = Session("DatabaseName")
            parameters.UserName = Session("UserName")
            parameters.Password = Session("Password")
            parameters.AuthorizationType = MsSqlAuthorizationType.SqlServer

        End If

    End Sub
End Class
