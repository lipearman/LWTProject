Imports System.Data
Imports System.Web.Security
Imports Portal.Components

Imports DataBind
Imports DevExpress.Web

Partial Class Modules_ucDevxCMIPremiumSetup
    Inherits PortalModuleControl
    'Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
    '    Dim headerFilterMode As HeaderFilterMode
    '    If hFModeCheckBox.Checked Then
    '        headerFilterMode = HeaderFilterMode.CheckedList
    '    Else
    '        headerFilterMode = HeaderFilterMode.List
    '    End If
    '    For Each column As GridViewDataColumn In grid.Columns
    '        column.Settings.HeaderFilterMode = headerFilterMode
    '    Next column
    'End Sub



    Protected Sub grid_HeaderFilterFillItems(ByVal sender As Object, ByVal e As ASPxGridViewHeaderFilterEventArgs)
 
        If e.Column.FieldName = "CMIGrossPremium" Then
            PrepareQuantityFilterItems(e)
        End If
    End Sub
    'Protected Overridable Sub PrepareTotalFilterItems(ByVal e As ASPxGridViewHeaderFilterEventArgs)
    '    e.Values.Clear()
    '    If e.Column.Settings.HeaderFilterMode = HeaderFilterMode.List Then
    '        e.AddShowAll()
    '    End If
    '    Dim [step] As Integer = 1000
    '    Dim [stepend] As Integer = 0
    '    For i As Integer = 0 To 9
    '        Dim start As Double = [step] * i
    '        Dim [end] As Double = start + [step] - 0.01
    '        [stepend] = [end]
    '        e.AddValue(String.Format("from {0:n0} to {1:n0}", start, [end]), "", String.Format("[CMINetPremium] >= {0} and [CMINetPremium] <= {1}", start, [end]))
    '    Next i
    '    e.AddValue(String.Format("> {0:n0}", [stepend]), "", "[CMINetPremium] > " & [stepend].ToString())
    'End Sub

    Protected Overridable Sub PrepareQuantityFilterItems(ByVal e As ASPxGridViewHeaderFilterEventArgs)
        Dim max As Integer = 3000
        'For i As Integer = 0 To e.Values.Count - 1
        '    Dim value As Integer
        '    If (Not Integer.TryParse(e.Values(i).Value, value)) Then
        '        Continue For
        '    End If
        '    If value > max Then
        '        max = value
        '    End If
        'Next i

        e.Values.Clear()

        If e.Column.Settings.HeaderFilterMode = HeaderFilterMode.List Then
            e.AddShowAll()
        End If
        Dim [step] As Integer = 1000
        Dim [stepend] As Integer = 0
        For i As Integer = 0 To max / [step]
            Dim start As Integer = [step] * i
            Dim [end] As Integer = start + [step] - 1
            [stepend] = [end]
            e.AddValue(String.Format("ราคา {0:n0} ถึง {1:n0} บาท", start, [end]), "", String.Format("[CMIGrossPremium] >= {0} and [CMIGrossPremium] <= {1}", start, [end]))
        Next i

        e.AddValue(String.Format("ราคามากกว่า {0:n0} บาท", [stepend]), "", "[CMIGrossPremium] > " & [stepend].ToString())
    End Sub
End Class
