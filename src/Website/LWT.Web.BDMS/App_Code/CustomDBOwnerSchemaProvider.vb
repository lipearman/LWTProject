Imports Microsoft.VisualBasic
Imports DevExpress.DataAccess.Sql
Imports DevExpress.Xpo.DB
Imports DevExpress.DataAccess.ConnectionParameters
Public Class CustomDBOwnerSchemaProvider


    Implements IDBSchemaProvider
    Private _PortalId As String
    Private _Owner As String

    Public Sub New(ByVal PortalId As String, ByVal Owner As String)
        MyBase.New()
        Me._PortalId = PortalId
        Me._Owner = Owner

    End Sub

    Public Function GetSchema(ByVal connection As SqlDataConnection, _
                              ByVal schemaLoadingMode As SchemaLoadingMode) As DBSchema _
                          Implements IDBSchemaProvider.GetSchema

        Dim tables() As DBTable
        If schemaLoadingMode.HasFlag(schemaLoadingMode.TablesAndViews) Then



            Using dc As New DataClasses_PortalBIExt()


                Dim _data = (From c In dc.tblDataSourceFiles Where c.PortalId = LWT.Website.webconfig._PortalID And c.Owner = Me._Owner).ToList()
                tables = New DBTable(_data.Count() - 1) {}

                For i = 0 To _data.Count() - 1

                    Dim Table As New DBTable(_data(i).GUID)

                    Dim dt = Portal.Components.SqlHelper.ExecuteDataset(ConfigurationManager.ConnectionStrings("PortalBIConnection").ConnectionString, System.Data.CommandType.Text, "select top 1 * from [Portal.BI.RawData].[dbo].[" & _data(i).GUID & "]").Tables(0)

                    For j = 0 To dt.Columns.Count - 1
                        If dt.Columns(j).ColumnName <> "" Or dt.Columns(j).ColumnName.ToLower <> "id" Then

                            Select Case dt.Columns(j).DataType.Name
                                Case "Int16"
                                    Table.AddColumn(New DBColumn With {.Name = dt.Columns(j).ColumnName, .ColumnType = DBColumnType.Int16})
                                Case "Int32"
                                    Table.AddColumn(New DBColumn With {.Name = dt.Columns(j).ColumnName, .ColumnType = DBColumnType.Int32})
                                Case "Int64"
                                    Table.AddColumn(New DBColumn With {.Name = dt.Columns(j).ColumnName, .ColumnType = DBColumnType.Int64})
                                Case "String"
                                    Table.AddColumn(New DBColumn With {.Name = dt.Columns(j).ColumnName, .ColumnType = DBColumnType.String})
                                Case "DateTime"
                                    Table.AddColumn(New DBColumn With {.Name = dt.Columns(j).ColumnName, .ColumnType = DBColumnType.DateTime})
                                Case "Decimal"
                                    Table.AddColumn(New DBColumn With {.Name = dt.Columns(j).ColumnName, .ColumnType = DBColumnType.Decimal})
                                Case "Double"
                                    Table.AddColumn(New DBColumn With {.Name = dt.Columns(j).ColumnName, .ColumnType = DBColumnType.Double})

                                Case "Boolean"
                                    Table.AddColumn(New DBColumn With {.Name = dt.Columns(j).ColumnName, .ColumnType = DBColumnType.Boolean})

                                    'Case "TimeSpan"
                                    '    Table.AddColumn(New DBColumn With {.Name = dt.Columns(j).ColumnName, .ColumnType = DBColumnType.TimeSpan})

                                Case Else
                                    Table.AddColumn(New DBColumn With {.Name = dt.Columns(j).ColumnName})
                            End Select


                        End If
                    Next
                    tables(i) = Table
                Next


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
