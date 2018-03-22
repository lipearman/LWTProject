Imports DevExpress.Web.ASPxSpreadsheet
Imports System.Data.SqlClient

Partial Class Test
    Inherits System.Web.UI.Page


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim _RawData As New List(Of GBPRawData)
        Dim _Data As New GBPRawData

        Dim _ASPxSpreadsheet As New ASPxSpreadsheet



        '_ASPxSpreadsheet.Open(Server.MapPath("~/App_Data/Modules/Financial/Data/Document/") & "/201708.xlsx")



        For Each Worksheet In _ASPxSpreadsheet.Document.Worksheets
            If Worksheet.Index = 0 Then
                Continue For
            End If
            '======================== REVENUE ======================
            _Data = New GBPRawData
            _Data.ASAT = Worksheet.Cells("G6").Value.DateTimeValue
            _Data.OPERATION = Worksheet.Cells("V3").Value.TextValue.Trim()
            _Data.CURRENCY = Worksheet.Cells("R13").Value.TextValue.Trim()
            _Data.GROUPNAME = Worksheet.Cells("Q14").Value.TextValue.Trim()

            _Data.NAME = Worksheet.Cells("Q15").Value.TextValue.Trim()
            _Data.ACT = Worksheet.Cells("C15").Value.NumericValue
            _Data.BUD = Worksheet.Cells("G15").Value.NumericValue
            _Data.PY = Worksheet.Cells("K15").Value.NumericValue
            _RawData.Add(_Data)

            _Data = New GBPRawData
            _Data.ASAT = Worksheet.Cells("G6").Value.DateTimeValue
            _Data.OPERATION = Worksheet.Cells("V3").Value.TextValue.Trim()
            _Data.CURRENCY = Worksheet.Cells("R13").Value.TextValue.Trim()
            _Data.GROUPNAME = Worksheet.Cells("Q14").Value.TextValue.Trim()

            _Data.NAME = Worksheet.Cells("Q16").Value.TextValue.Trim()
            _Data.ACT = Worksheet.Cells("C16").Value.NumericValue
            _Data.BUD = Worksheet.Cells("G16").Value.NumericValue
            _Data.PY = Worksheet.Cells("K16").Value.NumericValue
            _RawData.Add(_Data)

            _Data = New GBPRawData
            _Data.ASAT = Worksheet.Cells("G6").Value.DateTimeValue
            _Data.OPERATION = Worksheet.Cells("V3").Value.TextValue.Trim()
            _Data.CURRENCY = Worksheet.Cells("R13").Value.TextValue.Trim()
            _Data.GROUPNAME = Worksheet.Cells("Q14").Value.TextValue.Trim()

            _Data.NAME = Worksheet.Cells("Q17").Value.TextValue.Trim()
            _Data.ACT = Worksheet.Cells("C17").Value.NumericValue
            _Data.BUD = Worksheet.Cells("G17").Value.NumericValue
            _Data.PY = Worksheet.Cells("K17").Value.NumericValue
            _RawData.Add(_Data)

            '======================= OPERATING EXPENSES ============
            _Data = New GBPRawData
            _Data.ASAT = Worksheet.Cells("G6").Value.DateTimeValue
            _Data.OPERATION = Worksheet.Cells("V3").Value.TextValue.Trim()
            _Data.CURRENCY = Worksheet.Cells("R13").Value.TextValue.Trim()
            _Data.GROUPNAME = Worksheet.Cells("Q19").Value.TextValue.Trim()

            _Data.NAME = Worksheet.Cells("Q22").Value.TextValue.Trim()
            _Data.ACT = Worksheet.Cells("C22").Value.NumericValue
            _Data.BUD = Worksheet.Cells("G22").Value.NumericValue
            _Data.PY = Worksheet.Cells("K22").Value.NumericValue
            _RawData.Add(_Data)

            _Data = New GBPRawData
            _Data.ASAT = Worksheet.Cells("G6").Value.DateTimeValue
            _Data.OPERATION = Worksheet.Cells("V3").Value.TextValue.Trim()
            _Data.CURRENCY = Worksheet.Cells("R13").Value.TextValue.Trim()
            _Data.GROUPNAME = Worksheet.Cells("Q19").Value.TextValue.Trim()

            _Data.NAME = Worksheet.Cells("Q42").Value.TextValue.Trim()
            _Data.ACT = Worksheet.Cells("C42").Value.NumericValue
            _Data.BUD = Worksheet.Cells("G42").Value.NumericValue
            _Data.PY = Worksheet.Cells("K42").Value.NumericValue
            _RawData.Add(_Data)

            _Data = New GBPRawData
            _Data.ASAT = Worksheet.Cells("G6").Value.DateTimeValue
            _Data.OPERATION = Worksheet.Cells("V3").Value.TextValue.Trim()
            _Data.CURRENCY = Worksheet.Cells("R13").Value.TextValue.Trim()
            _Data.GROUPNAME = Worksheet.Cells("Q19").Value.TextValue.Trim()

            _Data.NAME = Worksheet.Cells("Q50").Value.TextValue.Trim()
            _Data.ACT = Worksheet.Cells("C50").Value.NumericValue
            _Data.BUD = Worksheet.Cells("G50").Value.NumericValue
            _Data.PY = Worksheet.Cells("K50").Value.NumericValue
            _RawData.Add(_Data)

            _Data = New GBPRawData
            _Data.ASAT = Worksheet.Cells("G6").Value.DateTimeValue
            _Data.OPERATION = Worksheet.Cells("V3").Value.TextValue.Trim()
            _Data.CURRENCY = Worksheet.Cells("R13").Value.TextValue.Trim()
            _Data.GROUPNAME = Worksheet.Cells("Q19").Value.TextValue.Trim()

            _Data.NAME = Worksheet.Cells("Q61").Value.TextValue.Trim()
            _Data.ACT = Worksheet.Cells("C61").Value.NumericValue
            _Data.BUD = Worksheet.Cells("G61").Value.NumericValue
            _Data.PY = Worksheet.Cells("K61").Value.NumericValue
            _RawData.Add(_Data)

            _Data = New GBPRawData
            _Data.ASAT = Worksheet.Cells("G6").Value.DateTimeValue
            _Data.OPERATION = Worksheet.Cells("V3").Value.TextValue.Trim()
            _Data.CURRENCY = Worksheet.Cells("R13").Value.TextValue.Trim()
            _Data.GROUPNAME = Worksheet.Cells("Q19").Value.TextValue.Trim()

            _Data.NAME = Worksheet.Cells("Q62").Value.TextValue.Trim()
            _Data.ACT = Worksheet.Cells("C62").Value.NumericValue
            _Data.BUD = Worksheet.Cells("G62").Value.NumericValue
            _Data.PY = Worksheet.Cells("K62").Value.NumericValue
            _RawData.Add(_Data)

            _Data = New GBPRawData
            _Data.ASAT = Worksheet.Cells("G6").Value.DateTimeValue
            _Data.OPERATION = Worksheet.Cells("V3").Value.TextValue.Trim()
            _Data.CURRENCY = Worksheet.Cells("R13").Value.TextValue.Trim()
            _Data.GROUPNAME = Worksheet.Cells("Q19").Value.TextValue.Trim()

            _Data.NAME = Worksheet.Cells("Q64").Value.TextValue.Trim()
            _Data.ACT = Worksheet.Cells("C64").Value.NumericValue
            _Data.BUD = Worksheet.Cells("G64").Value.NumericValue
            _Data.PY = Worksheet.Cells("K64").Value.NumericValue
            _RawData.Add(_Data)


            _Data = New GBPRawData
            _Data.ASAT = Worksheet.Cells("G6").Value.DateTimeValue
            _Data.OPERATION = Worksheet.Cells("V3").Value.TextValue.Trim()
            _Data.CURRENCY = Worksheet.Cells("R13").Value.TextValue.Trim()
            _Data.GROUPNAME = "OTHER EXPENSES"

            _Data.NAME = Worksheet.Cells("Q66").Value.TextValue.Trim()
            _Data.ACT = Worksheet.Cells("C66").Value.NumericValue
            _Data.BUD = Worksheet.Cells("G66").Value.NumericValue
            _Data.PY = Worksheet.Cells("K66").Value.NumericValue
            _RawData.Add(_Data)

            _Data = New GBPRawData
            _Data.ASAT = Worksheet.Cells("G6").Value.DateTimeValue
            _Data.OPERATION = Worksheet.Cells("V3").Value.TextValue.Trim()
            _Data.CURRENCY = Worksheet.Cells("R13").Value.TextValue.Trim()
            _Data.GROUPNAME = "OTHER EXPENSES"

            _Data.NAME = Worksheet.Cells("Q66").Value.TextValue.Trim()
            _Data.ACT = Worksheet.Cells("C66").Value.NumericValue
            _Data.BUD = Worksheet.Cells("G66").Value.NumericValue
            _Data.PY = Worksheet.Cells("K66").Value.NumericValue
            _RawData.Add(_Data)

            _Data = New GBPRawData
            _Data.ASAT = Worksheet.Cells("G6").Value.DateTimeValue
            _Data.OPERATION = Worksheet.Cells("V3").Value.TextValue.Trim()
            _Data.CURRENCY = Worksheet.Cells("R13").Value.TextValue.Trim()
            _Data.GROUPNAME = "OTHER EXPENSES"

            _Data.NAME = Worksheet.Cells("Q67").Value.TextValue.Trim()
            _Data.ACT = Worksheet.Cells("C67").Value.NumericValue
            _Data.BUD = Worksheet.Cells("G67").Value.NumericValue
            _Data.PY = Worksheet.Cells("K67").Value.NumericValue
            _RawData.Add(_Data)

            '======================= ADJUSTED EBITDA ===============
            _Data = New GBPRawData
            _Data.ASAT = Worksheet.Cells("G6").Value.DateTimeValue
            _Data.OPERATION = Worksheet.Cells("V3").Value.TextValue.Trim()
            _Data.CURRENCY = Worksheet.Cells("R13").Value.TextValue.Trim()
            _Data.GROUPNAME = Worksheet.Cells("Q69").Value.TextValue.Trim()

            _Data.NAME = Worksheet.Cells("Q69").Value.TextValue.Trim()
            _Data.ACT = Worksheet.Cells("C69").Value.NumericValue
            _Data.BUD = Worksheet.Cells("G69").Value.NumericValue
            _Data.PY = Worksheet.Cells("K69").Value.NumericValue
            _RawData.Add(_Data)


            _Data = New GBPRawData
            _Data.ASAT = Worksheet.Cells("G6").Value.DateTimeValue
            _Data.OPERATION = Worksheet.Cells("V3").Value.TextValue.Trim()
            _Data.CURRENCY = Worksheet.Cells("R13").Value.TextValue.Trim()
            _Data.GROUPNAME = "OTHER EXPENSES"

            _Data.NAME = Worksheet.Cells("Q72").Value.TextValue.Trim()
            _Data.ACT = Worksheet.Cells("C72").Value.NumericValue
            _Data.BUD = Worksheet.Cells("G72").Value.NumericValue
            _Data.PY = Worksheet.Cells("K72").Value.NumericValue
            _RawData.Add(_Data)


            _Data = New GBPRawData
            _Data.ASAT = Worksheet.Cells("G6").Value.DateTimeValue
            _Data.OPERATION = Worksheet.Cells("V3").Value.TextValue.Trim()
            _Data.CURRENCY = Worksheet.Cells("R13").Value.TextValue.Trim()
            _Data.GROUPNAME = "OTHER EXPENSES"

            _Data.NAME = Worksheet.Cells("Q73").Value.TextValue.Trim()
            _Data.ACT = Worksheet.Cells("C73").Value.NumericValue
            _Data.BUD = Worksheet.Cells("G73").Value.NumericValue
            _Data.PY = Worksheet.Cells("K73").Value.NumericValue
            _RawData.Add(_Data)


            _Data = New GBPRawData
            _Data.ASAT = Worksheet.Cells("G6").Value.DateTimeValue
            _Data.OPERATION = Worksheet.Cells("V3").Value.TextValue.Trim()
            _Data.CURRENCY = Worksheet.Cells("R13").Value.TextValue.Trim()
            _Data.GROUPNAME = "OTHER EXPENSES"

            _Data.NAME = Worksheet.Cells("Q74").Value.TextValue.Trim()
            _Data.ACT = Worksheet.Cells("C74").Value.NumericValue
            _Data.BUD = Worksheet.Cells("G74").Value.NumericValue
            _Data.PY = Worksheet.Cells("K74").Value.NumericValue
            _RawData.Add(_Data)
            '====================== PBT ============================
            _Data = New GBPRawData
            _Data.ASAT = Worksheet.Cells("G6").Value.DateTimeValue
            _Data.OPERATION = Worksheet.Cells("V3").Value.TextValue.Trim()
            _Data.CURRENCY = Worksheet.Cells("R13").Value.TextValue.Trim()
            _Data.GROUPNAME = Worksheet.Cells("Q76").Value.TextValue.Trim()

            _Data.NAME = Worksheet.Cells("Q76").Value.TextValue.Trim()
            _Data.ACT = Worksheet.Cells("C76").Value.NumericValue
            _Data.BUD = Worksheet.Cells("G76").Value.NumericValue
            _Data.PY = Worksheet.Cells("K76").Value.NumericValue
            _RawData.Add(_Data)

            _Data = New GBPRawData
            _Data.ASAT = Worksheet.Cells("G6").Value.DateTimeValue
            _Data.OPERATION = Worksheet.Cells("V3").Value.TextValue.Trim()
            _Data.CURRENCY = Worksheet.Cells("R13").Value.TextValue.Trim()
            _Data.GROUPNAME = "OTHER EXPENSES"

            _Data.NAME = Worksheet.Cells("Q78").Value.TextValue.Trim()
            _Data.ACT = Worksheet.Cells("C78").Value.NumericValue
            _Data.BUD = Worksheet.Cells("G78").Value.NumericValue
            _Data.PY = Worksheet.Cells("K78").Value.NumericValue
            _RawData.Add(_Data)


            '===================== PAT =======================
            _Data = New GBPRawData
            _Data.ASAT = Worksheet.Cells("G6").Value.DateTimeValue
            _Data.OPERATION = Worksheet.Cells("V3").Value.TextValue.Trim()
            _Data.CURRENCY = Worksheet.Cells("R13").Value.TextValue.Trim()
            _Data.GROUPNAME = Worksheet.Cells("Q80").Value.TextValue.Trim()

            _Data.NAME = Worksheet.Cells("Q80").Value.TextValue.Trim()
            _Data.ACT = Worksheet.Cells("C80").Value.NumericValue
            _Data.BUD = Worksheet.Cells("G80").Value.NumericValue
            _Data.PY = Worksheet.Cells("K80").Value.NumericValue
            _RawData.Add(_Data)

        Next


        If _RawData.Count > 0 Then

            grid.DataSource = _RawData
            grid.DataBind()


            Dim sDestConstr = ConfigurationManager.ConnectionStrings("PortalBIRawDataConnection").ConnectionString

            Dim dm As New Datamanage
            Dim dt = dm.ToDataTable(_RawData)


            Using bulkCopy As New SqlBulkCopy(sDestConstr)
                bulkCopy.DestinationTableName = "[tblFinancial]"
                bulkCopy.BulkCopyTimeout = 3600000
                bulkCopy.BatchSize = 5000
                'You can mannualy set the column mapping by the following way.
                For i As Integer = 0 To dt.Columns.Count - 1
                    If dt.Columns(i).ColumnName.IndexOf("Column") = -1 Then bulkCopy.ColumnMappings.Add(dt.Columns(i).ColumnName, dt.Columns(i).ColumnName)
                Next
                bulkCopy.WriteToServer(dt)
                'End Using
            End Using
        End If

    End Sub


End Class

<Serializable()> _
Public Class GBPRawData
    Private _ASAT As String
    Public Property ASAT() As DateTime
        Get
            Return _ASAT
        End Get
        Set(ByVal value As DateTime)
            _ASAT = value
        End Set
    End Property

    Private _OPERATION As String
    Public Property OPERATION() As String
        Get
            Return _OPERATION
        End Get
        Set(ByVal value As String)
            _OPERATION = value
        End Set
    End Property



    Private _CURRENCY As String
    Public Property CURRENCY() As String
        Get
            Return _CURRENCY
        End Get
        Set(ByVal value As String)
            _CURRENCY = value
        End Set
    End Property


    Private _GROUPNAME As String
    Public Property GROUPNAME() As String
        Get
            Return _GROUPNAME
        End Get
        Set(ByVal value As String)
            _GROUPNAME = value
        End Set
    End Property

    Private _NAME As String
    Public Property NAME() As String
        Get
            Return _NAME
        End Get
        Set(ByVal value As String)
            _NAME = value
        End Set
    End Property

    Private _LEGAL As String
    Public Property LEGAL() As String
        Get
            Return _LEGAL
        End Get
        Set(ByVal value As String)
            _LEGAL = value
        End Set
    End Property

    Private _ACT As String
    Public Property ACT() As Double
        Get
            Return _ACT
        End Get
        Set(ByVal value As Double)
            _ACT = value
        End Set
    End Property

    Private _BUD As String
    Public Property BUD() As Double
        Get
            Return _BUD
        End Get
        Set(ByVal value As Double)
            _BUD = value
        End Set
    End Property

    Private _PY As String
    Public Property PY() As Double
        Get
            Return _PY
        End Get
        Set(ByVal value As Double)
            _PY = value
        End Set
    End Property
End Class


