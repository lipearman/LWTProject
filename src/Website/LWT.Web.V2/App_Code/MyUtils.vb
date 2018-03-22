Imports Microsoft.VisualBasic
Imports System.Collections
Imports System.Configuration
Imports System.Data
Imports System.Linq
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Xml.Linq
Imports System.Text
Imports System.DirectoryServices
Imports FineUI
Imports System.IO
Imports ExcelDataReader

Public Class MyUtils
    Protected Shared ReadOnly VALID_FILE_TYPES As New List(Of String)() From { _
    "jpg", _
    "bmp", _
    "gif", _
    "jpeg", _
    "png" _
}


    Public Shared Function EQToDataTable(ByVal parIList As System.Collections.IEnumerable) As System.Data.DataTable
        Dim ret As New System.Data.DataTable()
        Try
            Dim ppi As System.Reflection.PropertyInfo() = Nothing
            If parIList Is Nothing Then Return ret
            For Each itm In parIList
                If ppi Is Nothing Then
                    ppi = DirectCast(itm.[GetType](), System.Type).GetProperties()
                    For Each pi As System.Reflection.PropertyInfo In ppi
                        Dim colType As System.Type = pi.PropertyType
                        If (colType.IsGenericType) AndAlso
                           (colType.GetGenericTypeDefinition() Is GetType(System.Nullable(Of ))) Then colType = colType.GetGenericArguments()(0)
                        ret.Columns.Add(New System.Data.DataColumn(pi.Name, colType))
                    Next
                End If
                Dim dr As System.Data.DataRow = ret.NewRow
                For Each pi As System.Reflection.PropertyInfo In ppi
                    dr(pi.Name) = If(pi.GetValue(itm, Nothing) Is Nothing, DBNull.Value, pi.GetValue(itm, Nothing))
                Next
                ret.Rows.Add(dr)
            Next
            'For Each c As System.Data.DataColumn In ret.Columns
            '    c.ColumnName = c.ColumnName.Replace("_", " ")
            'Next
        Catch ex As Exception
            ret = New System.Data.DataTable()
        End Try
        Return ret
    End Function



    'Public Shared Function AuthenticateUser(domain As String, username As String, password As String, LdapPath As String, ByRef Errmsg As String) As Boolean
    '    Errmsg = ""
    '    Dim domainAndUsername As String = domain & "\" & username
    '    Dim entry As New DirectoryEntry(LdapPath, domainAndUsername, password)
    '    Try
    '        ' Bind to the native AdsObject to force authentication.
    '        Dim obj As [Object] = entry.NativeObject
    '        Dim search As New DirectorySearcher(entry)
    '        search.Filter = "(SAMAccountName=" & username & ")"
    '        search.PropertiesToLoad.Add("cn")
    '        Dim result As SearchResult = search.FindOne()
    '        If result Is Nothing Then
    '            Return False
    '        End If
    '        ' Update the new path to the user in the directory
    '        LdapPath = result.Path
    '        Dim _filterAttribute As String = DirectCast(result.Properties("cn")(0), [String])
    '    Catch ex As Exception
    '        Errmsg = ex.Message
    '        Return False
    '        Throw New Exception("Error authenticating user." + ex.Message)
    '    End Try
    '    Return True
    'End Function

    Public Shared Function ValidateFileType(fileName As String) As Boolean
        Dim fileType As String = [String].Empty
        Dim lastDotIndex As Integer = fileName.LastIndexOf(".")
        If lastDotIndex >= 0 Then
            fileType = fileName.Substring(lastDotIndex + 1).ToLower()
        End If

        If VALID_FILE_TYPES.Contains(fileType) Then
            Return True
        Else
            Return False
        End If
    End Function


    Public Shared Function GetGridTableHtmlHeaderFormat(grid__1 As Grid, columns As String()) As String
        Dim sb As New StringBuilder()

        Dim columnHeaderTexts As New List(Of String)(columns)
        Dim columnIndexs As New List(Of Integer)()

        sb.Append("<table cellspacing=""0"" rules=""all"" border=""1"" style=""border-collapse:collapse;"">")

        sb.Append("<tr>")
        For Each column As GridColumn In grid__1.Columns
            If columnHeaderTexts.Contains(column.HeaderText) Then
                sb.AppendFormat("<td>{0}</td>", column.HeaderText)
                columnIndexs.Add(column.ColumnIndex)
            End If
        Next
        sb.Append("</tr>")


        sb.Append("</table>")

        Return sb.ToString()
    End Function

    Public Shared Function GetGridTableHtml(grid__1 As Grid, columns As String()) As String
        Dim sb As New StringBuilder()

        Dim columnHeaderTexts As New List(Of String)(columns)
        Dim columnIndexs As New List(Of Integer)()

        sb.Append("<table cellspacing=""0"" rules=""all"" border=""1"" style=""border-collapse:collapse;"">")

        sb.Append("<tr>")
        'For Each column As GridColumn In grid__1.Columns
        '    If columnHeaderTexts.Contains(column.HeaderText) Then
        '        sb.AppendFormat("<td>{0}</td>", column.HeaderText)
        '        columnIndexs.Add(column.ColumnIndex)
        '    End If
        'Next

        For Each column In columnHeaderTexts
            For Each c As GridColumn In grid__1.Columns
                If c.HeaderText.Equals(column) Then
                    sb.AppendFormat("<td>{0}</td>", column)
                    columnIndexs.Add(c.ColumnIndex)
                End If
            Next
        Next
        sb.Append("</tr>")


        For Each row As GridRow In grid__1.Rows
            sb.Append("<tr>")
            'Dim columnIndex As Integer = 0


            'For Each value As Object In row.Values
            'If columnIndexs.Contains(columnIndex) Then
            '    Dim html As String = value.ToString()
            '    sb.AppendFormat("<td>{0}</td>", html)
            'End If
            'columnIndex += 1
            'Next

            For Each columnIndex In columnIndexs
                Dim html As String = row.Values(columnIndex).ToString()
                sb.AppendFormat("<td>{0}</td>", html)
            Next


            sb.Append("</tr>")
        Next

        sb.Append("</table>")

        Return sb.ToString()
    End Function


    Public Shared Function GenThaiDate(ByVal _Date As Date, ByVal intParameter As Integer) As String
        Dim strMonth As String = String.Empty
        Select Case _Date.Month.ToString
            Case "1"
                If intParameter = 1 Then
                    strMonth = "ม.ค"
                ElseIf intParameter = 2 Then
                    strMonth = "มกราคม"
                End If

            Case "2"
                If intParameter = 1 Then
                    strMonth = "ก.พ"
                ElseIf intParameter = 2 Then
                    strMonth = "กุมภาพันธ์"
                End If

            Case "3"
                If intParameter = 1 Then
                    strMonth = "มี.ค"
                ElseIf intParameter = 2 Then
                    strMonth = "มีนาคม"
                End If

            Case "4"
                If intParameter = 1 Then
                    strMonth = "เม.ษ"
                ElseIf intParameter = 2 Then
                    strMonth = "เมษายน"
                End If

            Case "5"
                If intParameter = 1 Then
                    strMonth = "พ.ค"
                ElseIf intParameter = 2 Then
                    strMonth = "พฤษภาคม"
                End If

            Case "6"
                If intParameter = 1 Then
                    strMonth = "มิ.ย"
                ElseIf intParameter = 2 Then
                    strMonth = "มิถุนายน"
                End If

            Case "7"
                If intParameter = 1 Then
                    strMonth = "ก.ค"
                ElseIf intParameter = 2 Then
                    strMonth = "กรกฏาคม"
                End If

            Case "8"
                If intParameter = 1 Then
                    strMonth = "ส.ค"
                ElseIf intParameter = 2 Then
                    strMonth = "สิงหาคม"
                End If

            Case "9"
                If intParameter = 1 Then
                    strMonth = "ก.ย"
                ElseIf intParameter = 2 Then
                    strMonth = "กันยายน"
                End If

            Case "10"
                If intParameter = 1 Then
                    strMonth = "ต.ค"
                ElseIf intParameter = 2 Then
                    strMonth = "ตุลาคม"
                End If

            Case "11"
                If intParameter = 1 Then
                    strMonth = "พ.ย"
                ElseIf intParameter = 2 Then
                    strMonth = "พฤศจิกายน"
                End If

            Case "12"
                If intParameter = 1 Then
                    strMonth = "ธ.ค"
                ElseIf intParameter = 2 Then
                    strMonth = "ธันวาคม"
                End If

        End Select


        Return String.Format("{0} {1} {2}", _Date.Day.ToString(), strMonth, IIf(_Date.Year > 2500, _Date.Year.ToString(), (_Date.Year + 543).ToString()))

    End Function
    Public Shared Function GenThaiMonthlyDate(ByVal _Date As Date, ByVal intParameter As Integer) As String
        Dim strMonth As String = String.Empty
        Select Case _Date.Month.ToString
            Case "1"
                If intParameter = 1 Then
                    strMonth = "ม.ค"
                ElseIf intParameter = 2 Then
                    strMonth = "มกราคม"
                End If

            Case "2"
                If intParameter = 1 Then
                    strMonth = "ก.พ"
                ElseIf intParameter = 2 Then
                    strMonth = "กุมภาพันธ์"
                End If

            Case "3"
                If intParameter = 1 Then
                    strMonth = "มี.ค"
                ElseIf intParameter = 2 Then
                    strMonth = "มีนาคม"
                End If

            Case "4"
                If intParameter = 1 Then
                    strMonth = "เม.ษ"
                ElseIf intParameter = 2 Then
                    strMonth = "เมษายน"
                End If

            Case "5"
                If intParameter = 1 Then
                    strMonth = "พ.ค"
                ElseIf intParameter = 2 Then
                    strMonth = "พฤษภาคม"
                End If

            Case "6"
                If intParameter = 1 Then
                    strMonth = "มิ.ย"
                ElseIf intParameter = 2 Then
                    strMonth = "มิถุนายน"
                End If

            Case "7"
                If intParameter = 1 Then
                    strMonth = "ก.ค"
                ElseIf intParameter = 2 Then
                    strMonth = "กรกฏาคม"
                End If

            Case "8"
                If intParameter = 1 Then
                    strMonth = "ส.ค"
                ElseIf intParameter = 2 Then
                    strMonth = "สิงหาคม"
                End If

            Case "9"
                If intParameter = 1 Then
                    strMonth = "ก.ย"
                ElseIf intParameter = 2 Then
                    strMonth = "กันยายน"
                End If

            Case "10"
                If intParameter = 1 Then
                    strMonth = "ต.ค"
                ElseIf intParameter = 2 Then
                    strMonth = "ตุลาคม"
                End If

            Case "11"
                If intParameter = 1 Then
                    strMonth = "พ.ย"
                ElseIf intParameter = 2 Then
                    strMonth = "พฤศจิกายน"
                End If

            Case "12"
                If intParameter = 1 Then
                    strMonth = "ธ.ค"
                ElseIf intParameter = 2 Then
                    strMonth = "ธันวาคม"
                End If

        End Select


        Return String.Format("{0} {1}", strMonth, IIf(_Date.Year > 2500, _Date.Year.ToString(), (_Date.Year + 543).ToString()))

    End Function



    Public Shared Function ExcelToDataTable(fileName As String) As DataTable
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
