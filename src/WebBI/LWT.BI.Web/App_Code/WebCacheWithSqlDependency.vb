
Imports System.Data.SqlClient
Imports System.Web.Caching
Imports System.Web.Hosting
Imports System.Data
Imports System.Runtime.Caching
Imports Portal.Components
Namespace WebCacheWithSqlDependency




    Public Class DbManager

        Private Shared Function GetCacheData(cacheItemName As String) As Object
            Return HostingEnvironment.Cache.[Get](cacheItemName)

        End Function

        Private Shared Sub SetCacheData(cacheItemName As String, dataSet As Object, connString As String, tableName As String)
            Dim cacheEntryname As String = "BICache"

            'Many articles online state that the following statement should be run only once at the start of the application. 
            'Few articles say the object get replaced if called again with same connection string.
            SqlDependency.Start(connString)

            'The following statements needs to be run only once against the connection string and the database table, hence may also be moved to an appropriate place in code.
            SqlCacheDependencyAdmin.EnableNotifications(connString)
            SqlCacheDependencyAdmin.EnableTableForNotifications(connString, tableName)

            Dim dependency As New SqlCacheDependency(cacheEntryname, tableName)
            HostingEnvironment.Cache.Insert(cacheItemName, dataSet, dependency)

        End Sub

        Public Shared Function GetBITable(tableName As String) As DataTable

            If String.IsNullOrEmpty(tableName) Then
                tableName = "_Blank"
            End If

            Dim sqlQuery As String = "SELECT * FROM [dbo].[" & tableName & "]"
            'Dim tableName As String = isDataFromCache

            Dim connStringName As String = "PortalBIRawDataConnection"
            Dim connString As String = System.Configuration.ConfigurationManager.ConnectionStrings(connStringName).ToString()

            SqlHelper.ExecuteNonQuery(connString, CommandType.Text, "exec AspNet_SqlCacheRegisterTableStoredProcedure '" & tableName & "';")

            'isDataFromCache = False
            Dim dtTemp As DataTable = Nothing
            Dim cacheItemName As String = sqlQuery
            'assuming the commandtext can be used to uniquely identify the cached item
            Dim obj As Object = GetCacheData(cacheItemName)
            dtTemp = DirectCast(obj, DataTable)

            If dtTemp Is Nothing Then
                Dim cnMain As New SqlConnection(connString)
                Dim da As New SqlDataAdapter(sqlQuery, cnMain)

                dtTemp = New DataTable()
                da.Fill(dtTemp)


                SetCacheData(cacheItemName, dtTemp, connString, tableName)
            Else
                'isDataFromCache = True
            End If

            Return dtTemp

        End Function



        'Public Shared Function GetData(ByVal tableName As String) As DataTable
        '    Dim table As New DataTable()
        '    ''HttpContext.Current.Cache.Remove(tableName)
        '    '' Check for cache if it is then get it from cache
        '    'If HttpContext.Current.Cache(tableName) IsNot Nothing Then
        '    '    table = DirectCast(HttpContext.Current.Cache(tableName), DataTable)
        '    '    'lblMessage.Text += "<br />Being loaded from the Cache"
        '    'Else
        '    ' get the connection
        '    Dim connStringName As String = "PortalBIRawDataConnection"
        '    Dim connString As String = System.Configuration.ConfigurationManager.ConnectionStrings(connStringName).ToString()

        '    Using conn As New SqlConnection(connString)
        '        ' write the sql statement to execute
        '        Dim sql As String = "SELECT * FROM [" & tableName & "]"
        '        ' instantiate the command object to fire
        '        Using cmd As New SqlCommand(sql, conn)
        '            ' get the adapter object and attach the command object to it
        '            Using ad As New SqlDataAdapter(cmd)
        '                ' fire Fill method to fetch the data and fill into DataTable
        '                ad.Fill(table)
        '            End Using

        '            Dim dependency As New SqlCacheDependency(cmd)
        '            HttpContext.Current.Cache.Insert(tableName, table, dependency)


        '        End Using
        '        'Dim dependency As New SqlCacheDependency("Portal.BI.RawData", tableName)

        '        ' store data into Cache
        '        'HttpContext.Current.Cache.Insert(tableName, table, dependency)
        '        'lblMessage.Text += "<br />Being loaded from the database"
        '    End Using
        '    'End If
        '    ' '' specify the data source for the GridView
        '    ''GridView1.DataSource = table
        '    ' '' bind the data now
        '    ''GridView1.DataBind()


        '    Return table
        'End Function









        'Public Shared Function GetData(ByVal DSGUID As String) As DataTable
        '    Dim _Result As New DataTable()

        '    If DSGUID Is Nothing Then

        '        Dim connStringName As String = "PortalBIRawDataConnection"
        '        Dim connString As String = System.Configuration.ConfigurationManager.ConnectionStrings(connStringName).ToString()
        '        Using conn As New SqlConnection(connString)
        '            ' write the sql statement to execute
        '            Dim sql As String = "SELECT * FROM [_Blank]"
        '            ' instantiate the command object to fire
        '            Using cmd As New SqlCommand(sql, conn)
        '                ' get the adapter object and attach the command object to it
        '                Using ad As New SqlDataAdapter(cmd)
        '                    ' fire Fill method to fetch the data and fill into DataTable
        '                    ad.Fill(_Result)
        '                End Using
        '            End Using
        '        End Using


        '        Return _Result
        '    End If

        '    Dim FilePath = AppDomain.CurrentDomain.BaseDirectory & "\App_Data\FileCache\"

        '    Dim MyCache As New FileCache(FilePath, False, Nothing)

        '    If MyCache(DSGUID) Is Nothing Then

        '        Dim connStringName As String = "PortalBIRawDataConnection"
        '        Dim connString As String = System.Configuration.ConfigurationManager.ConnectionStrings(connStringName).ToString()
        '        Using conn As New SqlConnection(connString)
        '            ' write the sql statement to execute
        '            Dim sql As String = "SELECT * FROM [" & DSGUID & "]"
        '            ' instantiate the command object to fire
        '            Using cmd As New SqlCommand(sql, conn)
        '                ' get the adapter object and attach the command object to it
        '                Using ad As New SqlDataAdapter(cmd)
        '                    ' fire Fill method to fetch the data and fill into DataTable
        '                    ad.Fill(_Result)
        '                End Using
        '            End Using
        '        End Using

        '        MyCache(DSGUID) = _Result

        '    Else
        '        _Result = MyCache(DSGUID)
        '    End If


        '    Return _Result

        'End Function
    End Class

End Namespace





