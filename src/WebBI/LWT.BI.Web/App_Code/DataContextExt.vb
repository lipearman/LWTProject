Imports Microsoft.VisualBasic
Imports Portal.Components
Imports Microsoft.VisualBasic.FileIO
Imports System.Runtime.Caching
Imports System.Configuration
Imports DevExpress.DataAccess.Excel
Imports System.ComponentModel

Partial Public Class DataClasses_PortalBIExt
    Inherits DataClasses_PortalBIDataContext
    Private Shared ReadOnly Property ConnectionString() As String
        Get
            Dim Conn As String = ConfigurationManager.ConnectionStrings("PortalBIConnection").ConnectionString

            Return Conn
        End Get
    End Property
    Public Sub New()
        MyBase.New(ConnectionString)
    End Sub
    Public Function GetColumnName(colNum As Integer) As String
        Dim d As Integer
        Dim m As Integer
        Dim name As String
        d = colNum
        name = ""
        Do While (d > 0)
            m = (d - 1) Mod 26
            name = Chr(65 + m) + name
            d = Int((d - m) / 26)
        Loop
        GetColumnName = name
    End Function

    Public Function GetExcelPivotData(ByVal BID As String) As DevExpress.DataAccess.Native.Excel.DataView
        Dim excelDataSource As New ExcelDataSource()


        Using dc As New DataClasses_PortalBIExt()
            Dim _BIs = (From c In dc.tblDataSourceBIs Where c.BID.Equals(BID)).FirstOrDefault()

            Dim _data = (From c In dc.tblDataSourceFiles Where c.ID.Equals(_BIs.DS_ID)).FirstOrDefault()
            If _data IsNot Nothing Then
                'Dim ds As New ExcelDataSource()
                'ds.FileName = AppDomain.CurrentDomain.BaseDirectory & _data.SourceFile.Replace("~", "")
                'Dim sheet As New ExcelWorksheetSettings(_data.SheetName)
                'ds.SourceOptions = New ExcelSourceOptions(sheet)
                'ds.Fill()
                'Dim _r As DevExpress.DataAccess.Native.Excel.DataView = TryCast(DirectCast(ds, IListSource).GetList(), DevExpress.DataAccess.Native.Excel.DataView)
                'Dim CellRange = String.Format("A1:{0}{1}", GetColumnName(_r.Columns.Count), (_r.Count + 1))



                excelDataSource.FileName = AppDomain.CurrentDomain.BaseDirectory & _data.SourceFile.Replace("~", "")
                Dim worksheetSettings As New ExcelWorksheetSettings(_data.SheetName)

                excelDataSource.SourceOptions = New ExcelSourceOptions(worksheetSettings)

                excelDataSource.SourceOptions.UseFirstRowAsHeader = True
                excelDataSource.SourceOptions.SkipEmptyRows = True

                excelDataSource.Fill()


            End If



        End Using

        Dim resultView As DevExpress.DataAccess.Native.Excel.DataView = TryCast(DirectCast(excelDataSource, IListSource).GetList(), DevExpress.DataAccess.Native.Excel.DataView)

        Return resultView






        'Dim csvData = New List(Of Context.V_OutstandingPremium_APD)

        'Dim MyCache As New FileCache(ConfigurationSettings.AppSettings("CachePath"), False, Nothing)

        'If MyCache(_GUID) Is Nothing Then



        '    MyCache(_GUID) = csvData
        'Else
        '    csvData = MyCache(_GUID)
        'End If

        'Return csvData
    End Function



End Class


Partial Public Class DataClasses_LWTReportsExt
    Inherits DataClasses_LWTReportsDataContext
    Private Shared ReadOnly Property ConnectionString() As String
        Get
            Dim Conn As String = ConfigurationManager.ConnectionStrings("LWTReportsConnectionString").ConnectionString

            Return Conn
        End Get
    End Property
    Public Sub New()
        MyBase.New(ConnectionString)
    End Sub
End Class


Partial Public Class DataClasses_SIBISLWTReportsExt
    Inherits DataClasses_SIBISDB_LWTReportsDataContext
    Private Shared ReadOnly Property ConnectionString() As String
        Get
            Dim Conn As String = ConfigurationManager.ConnectionStrings("LWTReportsSIBISDBConnectionString").ConnectionString

            Return Conn
        End Get
    End Property
    Public Sub New()
        MyBase.New(ConnectionString)
    End Sub



End Class



Partial Public Class DataClasses_NLTDB_LWTReportsExt
    Inherits DataClasses_NLTDB_LWTReportsDataContext
    Private Shared ReadOnly Property ConnectionString() As String
        Get
            Dim Conn As String = ConfigurationManager.ConnectionStrings("LWTReportsNLTDBConnectionString").ConnectionString

            Return Conn
        End Get
    End Property
    Public Sub New()
        MyBase.New(ConnectionString)
    End Sub
End Class

