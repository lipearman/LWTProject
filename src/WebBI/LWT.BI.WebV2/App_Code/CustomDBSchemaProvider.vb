Imports Microsoft.VisualBasic
Imports DevExpress.DataAccess.Sql
Imports DevExpress.Xpo.DB
Imports DevExpress.DataAccess.ConnectionParameters
Imports System.Data

Public Class CustomDBSchemaProvider


    Implements IDBSchemaProvider
    Private _PortalId As String

    Public Sub New(ByVal PortalId As String)
        MyBase.New()
        Me._PortalId = PortalId
        'Me._GUID = GUID

    End Sub

    Public Function GetSchema(ByVal connection As SqlDataConnection, _
                              ByVal schemaLoadingMode As SchemaLoadingMode) As DBSchema _
                          Implements IDBSchemaProvider.GetSchema

        Dim tables() As DBTable
        If schemaLoadingMode.HasFlag(schemaLoadingMode.TablesAndViews) Then

 
         
            Dim TableList As New List(Of DataTable)

            Using dc As New DataClasses_PortalBIExt()
                If connection.HasConnectionString Then 'All Schema
                    'Dim ConnecttionString = connection.ConnectionString

                    '========================   tblDataSourceFiles =======================
                    Dim _ds1 = (From c In dc.tblDataSourceFiles Where c.PortalId = LWT.Website.webconfig._PortalID).ToList()
                    For Each item_ds1 In _ds1
                        Dim dt = Portal.Components.SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("PortalBIConnection").ConnectionString, System.Data.CommandType.Text, "select top 1 * from [Portal.BI.RawData].[dbo].[" & item_ds1.GUID & "]").Tables(0)
                        dt.TableName = item_ds1.GUID
                        TableList.Add(dt)
                    Next

                    '========================   tblDashBoard_DataSources =======================
                    Dim ds2 = (From c In dc.tblDashBoard_DataSources Where c.PortalId.Equals(LWT.Website.webconfig._PortalID) And c.CONN_TYPE.Equals("msSqlConnection") And c.CONN.Equals(connection.ConnectionString)).ToList()
                    For Each item_ds2 In ds2
                        Dim myTables = Portal.Components.SqlHelper.ExecuteDataset(item_ds2.CONN, System.Data.CommandType.Text, "SELECT name FROM sys.Views where is_ms_shipped=0").Tables(0)


                        For Each dr As DataRow In myTables.Rows
                            Dim dt = Portal.Components.SqlHelper.ExecuteDataset(item_ds2.CONN, System.Data.CommandType.Text, "select top 0 * from [" & dr("name") & "]").Tables(0)
                            dt.TableName = dr("name")
                            TableList.Add(dt)
                        Next


                    Next

                Else 'DataSource Schema
                    Dim DataSourceID = connection.Name

                    '========================   tblDashBoard_DataSources =======================
                    Dim ds2 = (From c In dc.tblDashBoard_DataSources Where c.DS_ID.Equals(DataSourceID) And c.PortalId.Equals(LWT.Website.webconfig._PortalID) And c.CONN_TYPE.Equals("msSqlConnection")).ToList()
                    For Each item_ds2 In ds2
                        Dim myTables = Portal.Components.SqlHelper.ExecuteDataset(item_ds2.CONN, System.Data.CommandType.Text, "SELECT name FROM sys.Views where is_ms_shipped=0").Tables(0)
                        For Each dr As DataRow In myTables.Rows
                            Dim dt = Portal.Components.SqlHelper.ExecuteDataset(item_ds2.CONN, System.Data.CommandType.Text, "select top 0 * from [" & dr("name") & "]").Tables(0)
                            dt.TableName = dr("name")
                            TableList.Add(dt)
                        Next
                    Next



                End If


                '========================   tables =======================
                tables = New DBTable(TableList.Count() - 1) {}

                For i = 0 To TableList.Count() - 1

                    Dim Table As New DBTable(TableList(i).TableName)

                    For j = 0 To TableList(i).Columns.Count - 1
                        If TableList(i).Columns(j).ColumnName <> "" Or TableList(i).Columns(j).ColumnName.ToLower <> "id" Then
                            Select Case TableList(i).Columns(j).DataType.Name
                                Case "Int16"
                                    Table.AddColumn(New DBColumn With {.Name = TableList(i).Columns(j).ColumnName, .ColumnType = DBColumnType.Int16})
                                Case "Int32"
                                    Table.AddColumn(New DBColumn With {.Name = TableList(i).Columns(j).ColumnName, .ColumnType = DBColumnType.Int32})
                                Case "Int64"
                                    Table.AddColumn(New DBColumn With {.Name = TableList(i).Columns(j).ColumnName, .ColumnType = DBColumnType.Int64})
                                Case "String"
                                    Table.AddColumn(New DBColumn With {.Name = TableList(i).Columns(j).ColumnName, .ColumnType = DBColumnType.String})
                                Case "DateTime"
                                    Table.AddColumn(New DBColumn With {.Name = TableList(i).Columns(j).ColumnName, .ColumnType = DBColumnType.DateTime})
                                Case "Decimal"
                                    Table.AddColumn(New DBColumn With {.Name = TableList(i).Columns(j).ColumnName, .ColumnType = DBColumnType.Decimal})
                                Case "Double"
                                    Table.AddColumn(New DBColumn With {.Name = TableList(i).Columns(j).ColumnName, .ColumnType = DBColumnType.Double})

                                Case "Boolean"
                                    Table.AddColumn(New DBColumn With {.Name = TableList(i).Columns(j).ColumnName, .ColumnType = DBColumnType.Boolean})

                                    'Case "TimeSpan"
                                    '    Table.AddColumn(New DBColumn With {.Name = dt.Columns(j).ColumnName, .ColumnType = DBColumnType.TimeSpan})

                                Case Else
                                    Table.AddColumn(New DBColumn With {.Name = TableList(i).Columns(j).ColumnName})
                            End Select
                        End If
                    Next
                    tables(i) = Table
                Next


                


                ''========================   tblDataSourceFiles =======================
                'Dim _data = (From c In dc.tblDataSourceFiles Where c.PortalId = LWT.Website.webconfig._PortalID).ToList()
                'tables = New DBTable(_data.Count() - 1) {}

                'For i = 0 To _data.Count() - 1

                '    Dim Table As New DBTable(_data(i).GUID)

                '    Dim dt = Portal.Components.SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("PortalBIConnection").ConnectionString, System.Data.CommandType.Text, "select top 1 * from [Portal.BI.RawData].[dbo].[" & _data(i).GUID & "]").Tables(0)

                '    For j = 0 To dt.Columns.Count - 1
                '        If dt.Columns(j).ColumnName <> "" Or dt.Columns(j).ColumnName.ToLower <> "id" Then
                '            Select Case dt.Columns(j).DataType.Name
                '                Case "Int16"
                '                    Table.AddColumn(New DBColumn With {.Name = dt.Columns(j).ColumnName, .ColumnType = DBColumnType.Int16})
                '                Case "Int32"
                '                    Table.AddColumn(New DBColumn With {.Name = dt.Columns(j).ColumnName, .ColumnType = DBColumnType.Int32})
                '                Case "Int64"
                '                    Table.AddColumn(New DBColumn With {.Name = dt.Columns(j).ColumnName, .ColumnType = DBColumnType.Int64})
                '                Case "String"
                '                    Table.AddColumn(New DBColumn With {.Name = dt.Columns(j).ColumnName, .ColumnType = DBColumnType.String})
                '                Case "DateTime"
                '                    Table.AddColumn(New DBColumn With {.Name = dt.Columns(j).ColumnName, .ColumnType = DBColumnType.DateTime})
                '                Case "Decimal"
                '                    Table.AddColumn(New DBColumn With {.Name = dt.Columns(j).ColumnName, .ColumnType = DBColumnType.Decimal})
                '                Case "Double"
                '                    Table.AddColumn(New DBColumn With {.Name = dt.Columns(j).ColumnName, .ColumnType = DBColumnType.Double})

                '                Case "Boolean"
                '                    Table.AddColumn(New DBColumn With {.Name = dt.Columns(j).ColumnName, .ColumnType = DBColumnType.Boolean})

                '                    'Case "TimeSpan"
                '                    '    Table.AddColumn(New DBColumn With {.Name = dt.Columns(j).ColumnName, .ColumnType = DBColumnType.TimeSpan})

                '                Case Else
                '                    Table.AddColumn(New DBColumn With {.Name = dt.Columns(j).ColumnName})
                '            End Select
                '        End If
                '    Next
                '    tables(i) = Table
                'Next






            End Using



            'Dim contactTable As New DBTable("Contact")

            'Dim firstNameColumn As DBColumn = New DBColumn With {.Name = "First"}
            'contactTable.AddColumn(firstNameColumn)
            'Dim lastNameColumn As DBColumn = New DBColumn With {.Name = "Last"}
            'contactTable.AddColumn(lastNameColumn)
            'Dim contactIDColumn As DBColumn = New DBColumn With {.Name = "ContactGUID"}
            'contactTable.AddColumn(contactIDColumn)
            'tables(0) = contactTable

            'Dim vcTable As New DBTable("vClient")
            'Dim calIDColumn As DBColumn = New DBColumn With {.Name = "vCalItemGUID"}
            'vcTable.AddColumn(calIDColumn)
            'Dim contactIDColumn2 As DBColumn = New DBColumn With {.Name = "ContactGUID"}
            'vcTable.AddColumn(contactIDColumn2)
            'tables(1) = vcTable




            'Dim foreignKey As New DBForeignKey({contactIDColumn2}, contactTable.Name,
            '                                   CustomDBSchemaProvider.CreatePrimaryKeys(contactIDColumn.Name))
            'vcTable.ForeignKeys.Add(foreignKey)
        Else
            tables = New DBTable() {}
        End If

        Dim views(-1) As DBTable
        Return New DBSchema(tables, views)

    End Function

    Public Sub LoadColumns(ByVal connection As SqlDataConnection,
                           ByVal ParamArray tables() As DBTable) _
                       Implements IDBSchemaProvider.LoadColumns
        connection.LoadDBColumns(tables)
    End Sub

    Public Shared Function CreatePrimaryKeys(ByVal ParamArray names() As String) As StringCollection
        Dim collection As New StringCollection()
        collection.AddRange(names)
        Return collection
    End Function
End Class



'Public Class CustomDBSchemaProviderEx

'    ' Creates a new class that defines a custom data store schema by implementing the 
'    ' IDBSchemaProvider interface.

'    Implements IDBSchemaProviderEx

'    Public Sub LoadColumns(ByVal connection As SqlDataConnection, ByVal ParamArray tables() As DBTable) _
'        Implements IDBSchemaProviderEx.LoadColumns
'        connection.LoadDBColumns(tables)
'    End Sub

'    Public Shared Function CreatePrimaryKeys(ByVal ParamArray names() As String) As StringCollection
'        Dim collection As New StringCollection()
'        collection.AddRange(names)
'        Return collection
'    End Function

'    Public Function GetTables(ByVal connection As SqlDataConnection,
'                              ByVal ParamArray tableList() As String) As DBTable() _
'                          Implements IDBSchemaProviderEx.GetTables

'        Dim cp = TryCast(connection.ConnectionParameters, Access97ConnectionParameters)

'        Dim tables(1) As DBTable

'        If cp IsNot Nothing AndAlso cp.FileName = "..\..\Data\nwind.mdb" Then

'            ' Creates two tables with required columns to be added to a data store schema.
'            Dim categoriesTable As New DBTable("Categories")
'            Dim categoryNameColumn1 As DBColumn = New DBColumn With {.Name = "CategoryName"}
'            categoriesTable.AddColumn(categoryNameColumn1)
'            Dim categoryIdColumn1 As DBColumn = New DBColumn With {.Name = "CategoryID"}
'            categoriesTable.AddColumn(categoryIdColumn1)
'            tables(0) = categoriesTable

'            Dim productsTable As New DBTable("Products")
'            Dim productNameColumn2 As DBColumn = New DBColumn With {.Name = "ProductName"}
'            productsTable.AddColumn(productNameColumn2)
'            Dim categoryIdColumn2 As DBColumn = New DBColumn With {.Name = "CategoryID"}
'            productsTable.AddColumn(categoryIdColumn2)
'            tables(1) = productsTable

'            ' Creates a foreign key for the 'Products' table that points to the 'CategoryID' 
'            ' column in the 'Categories' table.
'            Dim foreignKey As New DBForeignKey({categoryIdColumn2}, categoriesTable.Name, _
'                                               CustomDBSchemaProvider.CreatePrimaryKeys(categoryIdColumn1.Name))
'            productsTable.ForeignKeys.Add(foreignKey)
'        Else
'            tables = New DBTable() {}
'        End If
'        Return tables
'    End Function

'    Public Function GetViews(ByVal connection As SqlDataConnection, _
'                             ByVal ParamArray viewList() As String) As DBTable() _
'                         Implements IDBSchemaProviderEx.GetViews
'        Dim views(-1) As DBTable
'        Return views
'    End Function

'    Public Function GetProcedures(ByVal connection As SqlDataConnection, _
'                                  ByVal ParamArray procedureList() As String) As DBStoredProcedure() _
'                              Implements IDBSchemaProviderEx.GetProcedures
'        Dim storedProcedures(-1) As DBStoredProcedure
'        Return storedProcedures
'    End Function




'    'Implements IDBSchemaProvider

'    'Public Function GetSchema(ByVal connection As SqlDataConnection, _
'    '                          ByVal schemaLoadingMode As SchemaLoadingMode) As DBSchema _
'    '                      Implements IDBSchemaProvider.GetSchema

'    '    Dim tables() As DBTable
'    '    If schemaLoadingMode.HasFlag(schemaLoadingMode.TablesAndViews) Then

'    '        tables = New DBTable(1) {}

'    '        Dim contactTable As New DBTable("Contact")

'    '        Dim firstNameColumn As DBColumn = New DBColumn With {.Name = "First"}
'    '        contactTable.AddColumn(firstNameColumn)
'    '        Dim lastNameColumn As DBColumn = New DBColumn With {.Name = "Last"}
'    '        contactTable.AddColumn(lastNameColumn)
'    '        Dim contactIDColumn As DBColumn = New DBColumn With {.Name = "ContactGUID"}
'    '        contactTable.AddColumn(contactIDColumn)
'    '        tables(0) = contactTable

'    '        Dim vcTable As New DBTable("vClient")
'    '        Dim calIDColumn As DBColumn = New DBColumn With {.Name = "vCalItemGUID"}
'    '        vcTable.AddColumn(calIDColumn)
'    '        Dim contactIDColumn2 As DBColumn = New DBColumn With {.Name = "ContactGUID"}
'    '        vcTable.AddColumn(contactIDColumn2)
'    '        tables(1) = vcTable

'    '        Dim foreignKey As New DBForeignKey({contactIDColumn2}, contactTable.Name,
'    '                                           CustomDBSchemaProvider.CreatePrimaryKeys(contactIDColumn.Name))
'    '        vcTable.ForeignKeys.Add(foreignKey)
'    '    Else
'    '        tables = New DBTable() {}
'    '    End If

'    '    Dim views(-1) As DBTable
'    '    Return New DBSchema(tables, views)

'    'End Function

'    'Public Sub LoadColumns(ByVal connection As SqlDataConnection,
'    '                       ByVal ParamArray tables() As DBTable) _
'    '                   Implements IDBSchemaProvider.LoadColumns
'    '    connection.LoadDBColumns(tables)
'    'End Sub

'    'Public Shared Function CreatePrimaryKeys(ByVal ParamArray names() As String) As StringCollection
'    '    Dim collection As New StringCollection()
'    '    collection.AddRange(names)
'    '    Return collection
'    'End Function
'End Class
