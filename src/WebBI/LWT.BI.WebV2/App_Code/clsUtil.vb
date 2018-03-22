Imports Microsoft.VisualBasic
Imports System.Data
Imports System.IO
Imports Portal.Components
Imports System.Data.OleDb
Imports System.Data.SqlClient
'Imports Excel
Imports DevExpress.XtraPivotGrid
Imports System.Web.Hosting
Imports ExcelDataReader
Imports System.ComponentModel

Public Class Datamanage
    Public Function ToDataTable(Of T)(data As IList(Of T)) As DataTable
        Dim properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetType(T))
        Dim dt As New DataTable()
        For i As Integer = 0 To properties.Count - 1
            Dim [property] As PropertyDescriptor = properties(i)
            dt.Columns.Add([property].Name, [property].PropertyType)
        Next
        Dim values As Object() = New Object(properties.Count - 1) {}
        For Each item As T In data
            For i As Integer = 0 To values.Length - 1
                values(i) = properties(i).GetValue(item)
            Next
            dt.Rows.Add(values)
        Next
        Return dt
    End Function



    Public Sub ImportDataToServer(ByVal ExcelPath As String, _
               ByVal sDestConstr As String, _
               ByVal InsertedTableName As String)
        Dim sSourceConstr As String = ""

        'Try
        Dim dt = ExcelToDataTable(ExcelPath)

        'If dt.Rows.Count > 65535 Then
        '    Throw New Exception("Cannot import data more than 65535 rows")
        'End If

        'Using File = New FileStream(ExcelPath, FileMode.Open, FileAccess.Read)

        '    If ExcelPath.ToLower().IndexOf(".xlsx") > -1 Then
        '        ' ''1. Reading from a binary Excel file ('97-2003 format; *.xls)
        '        ''Dim excelReader As IExcelDataReader = ExcelReaderFactory.CreateBinaryReader(Stream)
        '        ''...
        '        ''2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
        '        'Dim excelReader As IExcelDataReader = ExcelReaderFactory.CreateOpenXmlReader(File)
        '        ''...
        '        ''3. DataSet - The result of each spreadsheet will be created in the result.Tables
        '        ''Dim result As DataSet = excelReader.AsDataSet()
        '        ''...
        '        ''4. DataSet - Create column names from first row
        '        'excelReader.IsFirstRowAsColumnNames = True
        '        'Dim result As DataSet = excelReader.AsDataSet()

        '        'dt = result.Tables(0)

        '        sSourceConstr = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", ExcelPath)

        '    ElseIf ExcelPath.ToLower().IndexOf(".xls") > -1 Then

        '        ''1. Reading from a binary Excel file ('97-2003 format; *.xls)
        '        'Dim excelReader As IExcelDataReader = ExcelReaderFactory.CreateBinaryReader(File)
        '        ''...
        '        ' ''2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
        '        ''Dim excelReader As IExcelDataReader = ExcelReaderFactory.CreateOpenXmlReader(File)
        '        ' ''...
        '        ''3. DataSet - The result of each spreadsheet will be created in the result.Tables
        '        ''Dim result As DataSet = excelReader.AsDataSet()
        '        ''...
        '        ''4. DataSet - Create column names from first row
        '        'excelReader.IsFirstRowAsColumnNames = True
        '        'Dim result As DataSet = excelReader.AsDataSet()

        '        'dt = result.Tables(0)

        '        sSourceConstr = String.Format("Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0}; Extended Properties=""Excel 8.0;HDR=YES;""", ExcelPath)

        '    End If



        'End Using


        'dt = ReadExcelFile(sSourceConstr).Tables(0)



        Dim sqlQuery As New StringBuilder()
        sqlQuery.AppendLine("IF object_id('" & InsertedTableName & "') is not null")
        sqlQuery.AppendLine("DROP TABLE [" & InsertedTableName & "];")
        sqlQuery.AppendLine("CREATE TABLE [" + InsertedTableName + "] ([ID] [int] IDENTITY(1,1) NOT NULL")
        'sqlQuery.AppendLine("CREATE TABLE [" + InsertedTableName + "] (")
        For i As Integer = 0 To dt.Columns.Count - 1

            If dt.Columns(i).ColumnName.StartsWith("Column") = False Then



                'If i > 0 Then sqlQuery.AppendLine(",")
                sqlQuery.AppendLine(",")


                sqlQuery.AppendLine(" [" + dt.Columns(i).ColumnName + "] ")
                Dim columnType As String = dt.Columns(i).DataType.ToString()

                ' You will get different data type in  C#(you need to change it SQLServer):Boolean,Byte,Char,DateTime,Decimal,Double,Int16,Int32,Int64,SByte,Single, String,TimeSpan,UInt16,UInt32,UInt64
                Select Case columnType
                    Case "System.Int32"
                        sqlQuery.AppendLine(" int ")

                    Case "System.Int64"
                        sqlQuery.AppendLine(" bigint ")

                    Case "System.Int16"
                        sqlQuery.AppendLine(" tinyint ")

                    Case "System.Decimal", "System.Double"
                        sqlQuery.AppendLine(" decimal(18,2) ")

                    Case "System.DateTime"
                        sqlQuery.AppendLine(" datetime ")

                    Case Else
                        Dim colname = dt.Columns(i).ColumnName
                        Dim maxlength As Integer = dt.AsEnumerable.Max(Function(x) x(colname).ToString.Length)
                        If maxlength > 0 Then

                            If maxlength < 50 Then
                                sqlQuery.AppendLine(" nvarchar(50) ")
                            Else
                                sqlQuery.AppendLine(" nvarchar(" & maxlength.ToString() & ") ")
                            End If

                        Else
                            sqlQuery.AppendLine(" nvarchar(255) ")
                        End If

                End Select

            End If

        Next


        Dim strSQL = sqlQuery.ToString() & ",CONSTRAINT [PK_" & InsertedTableName & "] PRIMARY KEY ([ID]) ) ON [PRIMARY]"
        'Dim strSQL = sqlQuery.ToString() & ")"

        SqlHelper.ExecuteNonQuery(sDestConstr, CommandType.Text, strSQL)

        Dim sSourceConnection As New OleDbConnection(sSourceConstr)
        Using sSourceConnection
            'Dim sql As String = String.Format("Select * FROM [{0}$]", dt.TableName)
            'Dim command As New OleDbCommand(sql, sSourceConnection)
            'sSourceConnection.Open()
            'Using dr As OleDbDataReader = command.ExecuteReader()

            Using bulkCopy As New SqlBulkCopy(sDestConstr)
                bulkCopy.DestinationTableName = "[" & InsertedTableName & "]"
                bulkCopy.BulkCopyTimeout = 3600000
                bulkCopy.BatchSize = 5000

                'You can mannualy set the column mapping by the following way.
                For i As Integer = 0 To dt.Columns.Count - 1
                    If dt.Columns(i).ColumnName.IndexOf("Column") = -1 Then bulkCopy.ColumnMappings.Add(dt.Columns(i).ColumnName, dt.Columns(i).ColumnName)
                Next
                bulkCopy.WriteToServer(dt)
            End Using
            'End Using
        End Using

        SqlHelper.ExecuteNonQuery(sDestConstr, CommandType.Text, "exec [dbo].[AspNet_SqlCacheRegisterTableStoredProcedure] N'" & InsertedTableName & "'")


        WebCacheWithSqlDependency.DbManager.GetBITable(InsertedTableName)
    End Sub

    Public Sub ValidateDataToServer(ByVal ExcelPath As String, _
           ByVal sDestConstr As String, _
           ByVal InsertedTableName As String)
        Dim sSourceConstr As String = ""

        'Dim dt As New DataTable
        'Using File = New FileStream(ExcelPath, FileMode.Open, FileAccess.Read)

        '    If ExcelPath.ToLower().IndexOf(".xlsx") > -1 Then

        '        sSourceConstr = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", ExcelPath)

        '    ElseIf ExcelPath.ToLower().IndexOf(".xls") > -1 Then

        '        sSourceConstr = String.Format("Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0}; Extended Properties=""Excel 8.0;HDR=YES;""", ExcelPath)

        '    End If

        'End Using

        'dt = ReadExcelFile(sSourceConstr).Tables(0)

        Dim dt = ExcelToDataTable(ExcelPath)

        Dim DS = SqlHelper.ExecuteDataset(sDestConstr, CommandType.Text, String.Format("Select * FROM [{0}]", InsertedTableName))

        Dim iCol = (From c As DataColumn In dt.Columns Where Not c.ColumnName.StartsWith("Column") Select c.ColumnName).ToArray()
        Dim jCol = (From c As DataColumn In DS.Tables(0).Columns Select c.ColumnName).ToArray()


        'If iCol.Count <> jCol.Count Then
        '    Throw New Exception("Invalid Column number ")
        'End If


        Dim sb As New StringBuilder()
        For Each item As String In iCol
            Dim a = (From c In jCol Where c = item).FirstOrDefault()
            If a Is Nothing Then
                sb.Append(item & ",")
            End If
        Next


        If Not String.IsNullOrEmpty(sb.ToString()) Then


            Throw New Exception("Invalid Field: " & Left(sb.ToString(), sb.ToString().Length - 1))

        End If


    End Sub

    Public Sub AppendDataToServer(ByVal ExcelPath As String, _
             ByVal sDestConstr As String, _
             ByVal InsertedTableName As String)


        'Dim sSourceConstr As String = ""

        ''Try
        'Dim dt As New DataTable
        'Using File = New FileStream(ExcelPath, FileMode.Open, FileAccess.Read)

        '    If ExcelPath.ToLower().IndexOf(".xlsx") > -1 Then
        '        ''1. Reading from a binary Excel file ('97-2003 format; *.xls)
        '        'Dim excelReader As IExcelDataReader = ExcelReaderFactory.CreateBinaryReader(Stream)
        '        '...
        '        '2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
        '        Dim excelReader As IExcelDataReader = ExcelReaderFactory.CreateOpenXmlReader(File)
        '        '...
        '        '3. DataSet - The result of each spreadsheet will be created in the result.Tables
        '        'Dim result As DataSet = excelReader.AsDataSet()
        '        '...
        '        '4. DataSet - Create column names from first row
        '        excelReader.IsFirstRowAsColumnNames = True
        '        Dim result As DataSet = excelReader.AsDataSet()

        '        dt = result.Tables(0)

        '        sSourceConstr = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", ExcelPath)


        '    ElseIf ExcelPath.ToLower().IndexOf(".xls") > -1 Then

        '        '1. Reading from a binary Excel file ('97-2003 format; *.xls)
        '        Dim excelReader As IExcelDataReader = ExcelReaderFactory.CreateBinaryReader(File)
        '        '...
        '        ''2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
        '        'Dim excelReader As IExcelDataReader = ExcelReaderFactory.CreateOpenXmlReader(File)
        '        ''...
        '        '3. DataSet - The result of each spreadsheet will be created in the result.Tables
        '        'Dim result As DataSet = excelReader.AsDataSet()
        '        '...
        '        '4. DataSet - Create column names from first row
        '        excelReader.IsFirstRowAsColumnNames = True
        '        Dim result As DataSet = excelReader.AsDataSet()

        '        dt = result.Tables(0)

        '        sSourceConstr = String.Format("Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0}; Extended Properties=""Excel 8.0;HDR=YES;""", ExcelPath)

        '    End If

        'End Using

        Dim dt = ExcelToDataTable(ExcelPath)
        'If dt.Rows.Count > 65535 Then
        '    Throw New Exception("Cannot import data more than 65535 rows")
        'End If
        'Dim sSourceConnection As New OleDbConnection(sSourceConstr)
        'Using sSourceConnection
        'Dim sql As String = String.Format("Select * FROM [{0}$]", dt.TableName)
        'Dim command As New OleDbCommand(sql, sSourceConnection)
        'sSourceConnection.Open()
        'Using dr As OleDbDataReader = command.ExecuteReader()
        Using bulkCopy As New SqlBulkCopy(sDestConstr)
            bulkCopy.DestinationTableName = "[" & InsertedTableName & "]"
            bulkCopy.BulkCopyTimeout = 3600000
            bulkCopy.BatchSize = 5000
            'You can mannualy set the column mapping by the following way.
            For i As Integer = 0 To dt.Columns.Count - 1
                If dt.Columns(i).ColumnName.IndexOf("Column") = -1 Then bulkCopy.ColumnMappings.Add(dt.Columns(i).ColumnName, dt.Columns(i).ColumnName)
            Next
            bulkCopy.WriteToServer(dt)
            'End Using
        End Using
        'End Using

        SqlHelper.ExecuteNonQuery(sDestConstr, CommandType.Text, "exec [dbo].[AspNet_SqlCacheUpdateChangeIdStoredProcedure] N'" & InsertedTableName & "'")

        Dim sqlQuery As String = "SELECT * FROM [dbo].[" & InsertedTableName & "]"
        HostingEnvironment.Cache.Remove(sqlQuery)
        WebCacheWithSqlDependency.DbManager.GetBITable(InsertedTableName)
    End Sub

    Public Sub ReplaceDataToServer(ByVal ExcelPath As String, _
         ByVal sDestConstr As String, _
         ByVal InsertedTableName As String)


        Dim sSourceConstr As String = ""

        ''Try
        'Dim dt As New DataTable
        'Using File = New FileStream(ExcelPath, FileMode.Open, FileAccess.Read)

        '    If ExcelPath.ToLower().IndexOf(".xlsx") > -1 Then
        '        ''1. Reading from a binary Excel file ('97-2003 format; *.xls)
        '        'Dim excelReader As IExcelDataReader = ExcelReaderFactory.CreateBinaryReader(Stream)
        '        '...
        '        '2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
        '        Dim excelReader As IExcelDataReader = ExcelReaderFactory.CreateOpenXmlReader(File)
        '        '...
        '        '3. DataSet - The result of each spreadsheet will be created in the result.Tables
        '        'Dim result As DataSet = excelReader.AsDataSet()
        '        '...
        '        '4. DataSet - Create column names from first row
        '        excelReader.IsFirstRowAsColumnNames = True
        '        Dim result As DataSet = excelReader.AsDataSet()

        '        dt = result.Tables(0)

        '        sSourceConstr = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", ExcelPath)


        '    ElseIf ExcelPath.ToLower().IndexOf(".xls") > -1 Then

        '        '1. Reading from a binary Excel file ('97-2003 format; *.xls)
        '        Dim excelReader As IExcelDataReader = ExcelReaderFactory.CreateBinaryReader(File)
        '        '...
        '        ''2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
        '        'Dim excelReader As IExcelDataReader = ExcelReaderFactory.CreateOpenXmlReader(File)
        '        ''...
        '        '3. DataSet - The result of each spreadsheet will be created in the result.Tables
        '        'Dim result As DataSet = excelReader.AsDataSet()
        '        '...
        '        '4. DataSet - Create column names from first row
        '        excelReader.IsFirstRowAsColumnNames = True
        '        Dim result As DataSet = excelReader.AsDataSet()

        '        dt = result.Tables(0)

        '        sSourceConstr = String.Format("Provider=Microsoft.Jet.OLEDB.4.0; Data Source={0}; Extended Properties=""Excel 8.0;HDR=YES;""", ExcelPath)

        '    End If

        'End Using


        Dim dt = ExcelToDataTable(ExcelPath)
        'If dt.Rows.Count > 65535 Then
        '    Throw New Exception("Cannot import data more than 65535 rows")
        'End If

        SqlHelper.ExecuteNonQuery(sDestConstr, CommandType.Text, String.Format("TRUNCATE TABLE [{0}]", InsertedTableName))




        'Dim sSourceConnection As New OleDbConnection(sSourceConstr)
        'Using sSourceConnection
        'Dim sql As String = String.Format("Select * FROM [{0}$]", dt.TableName)
        'Dim command As New OleDbCommand(sql, sSourceConnection)
        'sSourceConnection.Open()
        'Using dr As OleDbDataReader = command.ExecuteReader()
        Using bulkCopy As New SqlBulkCopy(sDestConstr)
            bulkCopy.DestinationTableName = "[" & InsertedTableName & "]"
            bulkCopy.BulkCopyTimeout = 3600000
            bulkCopy.BatchSize = 5000
            'You can mannualy set the column mapping by the following way.
            For i As Integer = 0 To dt.Columns.Count - 1
                If dt.Columns(i).ColumnName.IndexOf("Column") = -1 Then bulkCopy.ColumnMappings.Add(dt.Columns(i).ColumnName, dt.Columns(i).ColumnName)
            Next
            'bulkCopy.WriteToServer(dr)
            bulkCopy.WriteToServer(dt)
            'End Using
        End Using
        'End Using

        SqlHelper.ExecuteNonQuery(sDestConstr, CommandType.Text, "exec [dbo].[AspNet_SqlCacheUpdateChangeIdStoredProcedure] N'" & InsertedTableName & "'")


        Dim sqlQuery As String = "SELECT * FROM [dbo].[" & InsertedTableName & "]"
        HostingEnvironment.Cache.Remove(sqlQuery)
        WebCacheWithSqlDependency.DbManager.GetBITable(InsertedTableName)
    End Sub




    Public Sub RetrieveFields(ByVal sDestConstr As String, _
                              ByVal _GUID As String)
        Using dc As New DataClasses_PortalBIExt()
            Dim _data = (From c In dc.tblDataSourceFiles Where c.GUID.Equals(_GUID)).FirstOrDefault()
            If _data IsNot Nothing Then

                Dim dr = SqlHelper.ExecuteReader(sDestConstr, CommandType.Text, "select top 1 * from [" & _GUID & "]")

                Dim apg As New DevExpress.Web.ASPxPivotGrid.ASPxPivotGrid()
                apg.DataSource = dr
                apg.DataBind()
                apg.BeginUpdate()
                apg.RetrieveFields(PivotArea.FilterArea, False)
                apg.EndUpdate()

                Dim _newfieldList As New List(Of tblDataSourceFile_Field)

                For Each _field As DevExpress.Web.ASPxPivotGrid.PivotGridField In apg.Fields
                    Dim _tblAttributes = (From c In dc.tblDataSourceFile_Fields Where c.DS_ID.Equals(_data.ID) And c.FIELD_NAME.Equals(_field.FieldName)).FirstOrDefault()
                    If _tblAttributes Is Nothing Then

                        Dim _newfield As New tblDataSourceFile_Field
                        With _newfield

                            .FIELD_CAPTION = _field.FieldName
                            .FIELD_NAME = _field.FieldName
                            .DS_ID = _data.ID

                            .SummaryType = 0
                            .CellFormat_FormatType = 1
                            .CellFormat_FormatString = "{0:N0}"

                            .UnboundColumnType = 0
                            .UnboundExpressionMode = 0
                            .UnboundExpression = ""
                            .PivotSummaryDisplayType = 0
                            .GroupInterval = 0

                            .AREA = -1
                            .AREAINDEX = -1
                            .ORDERBY = 0

                        End With

                        _newfieldList.Add(_newfield)

                    End If
                Next


                If _newfieldList.Count > 0 Then
                    dc.tblDataSourceFile_Fields.InsertAllOnSubmit(_newfieldList)
                    dc.SubmitChanges()
                End If
            End If

        End Using

    End Sub



    Public Sub DeleteData(ByVal sDestConstr As String, _
                          ByVal _DS_ID As String, _
                          ByVal _GUID As String)

        Dim sb As New StringBuilder()

        sb.Append("exec [dbo].[AspNet_SqlCacheUnRegisterTableStoredProcedure] N'" & _GUID & "';")

        sb.Append("drop table [dbo].[" & _GUID & "];")
        sb.Append("delete from [Portal.BI].dbo.tblDataSourceFile where ID='" & _DS_ID & "';")
        sb.Append("delete from [Portal.BI].dbo.tblDataSourceFile_Field where DS_ID='" & _DS_ID & "';")
        sb.Append("delete from [Portal.BI].dbo.tblDataSourceBI_Assignment where BID in(select BID from [Portal.BI].dbo.tblDataSourceBI where DS_ID='" & _DS_ID & "');")
        sb.Append("delete from [Portal.BI].dbo.tblDataSourceBI_Field where BID in(select BID from [Portal.BI].dbo.tblDataSourceBI where DS_ID='" & _DS_ID & "');")
        sb.Append("delete from [Portal.BI].dbo.tblDataSourceBI_Field_Filter where BID in(select BID from [Portal.BI].dbo.tblDataSourceBI where DS_ID='" & _DS_ID & "');")
        sb.Append("delete from [Portal.BI].dbo.tblDataSourceBI where DS_ID='" & _DS_ID & "';")



        SqlHelper.ExecuteNonQuery(sDestConstr, CommandType.Text, sb.ToString())


        Dim sqlQuery As String = "SELECT * FROM [dbo].[" & _GUID & "]"
        HostingEnvironment.Cache.Remove(sqlQuery)

    End Sub



    'Private Function ReadExcelFile(ByVal connectionString As String) As DataSet
    '    Dim ds As New DataSet()


    '    Using conn As New OleDbConnection(connectionString)
    '        conn.Open()
    '        Dim cmd As New OleDbCommand()
    '        cmd.Connection = conn

    '        ' Get all Sheets in Excel File
    '        Dim dtSheet As DataTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, Nothing)

    '        ' Loop through all Sheets to get data
    '        For Each dr As DataRow In dtSheet.Rows
    '            Dim sheetName As String = dr("TABLE_NAME").ToString()

    '            If Not sheetName.EndsWith("$") Then
    '                Continue For
    '            End If

    '            ' Get all rows from the Sheet
    '            cmd.CommandText = (Convert.ToString("SELECT * FROM [") & sheetName) + "]"

    '            Dim dt As New DataTable()
    '            dt.TableName = sheetName

    '            Dim da As New OleDbDataAdapter(cmd)
    '            da.Fill(dt)

    '            ds.Tables.Add(dt)
    '        Next

    '        cmd = Nothing
    '        conn.Close()
    '    End Using

    '    Return ds
    'End Function


    Public Function ExcelToDataTable(fileName As String) As DataTable
        ' Open file and return as Stream
        Using stream = File.Open(fileName, FileMode.Open, FileAccess.Read)
            Using reader = ExcelReaderFactory.CreateReader(stream)
                Dim result = reader.AsDataSet(New ExcelDataSetConfiguration() With { _
                                              .UseColumnDataType = True _
                                            , .ConfigureDataTable = Function(data) New ExcelDataTableConfiguration() With { _
                                                            .UseHeaderRow = True _
                    } _
                })
                'Get all the tables
                Dim table = result.Tables
                ' store it in data table
                Dim resultTable = table(0)
                Return resultTable
            End Using
        End Using
    End Function
End Class
