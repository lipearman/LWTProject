Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Collections.Generic
Imports System
Imports DevExpress.XtraReports.UI
Imports System.Web
Imports System.ServiceModel

Public Class CustomReportStorageWebExtension
    Inherits DevExpress.XtraReports.Web.Extensions.ReportStorageWebExtension
    Private _context As HttpContext
    Private dc As DataClasses_PortalBIExt

    Public Sub New(ByVal context As HttpContext)
        Me._context = context
        dc = New DataClasses_PortalBIExt
    End Sub

    Public Overrides Function CanSetData(ByVal GUID As String) As Boolean
        Return (dc.tblXtraReports.Where(Function(c) c.GUID.Equals(GUID)).Count > 0)
    End Function

    Public Overrides Function GetData(ByVal GUID As String) As Byte()
        Dim reportData As Byte() = Nothing

        Dim data = dc.tblXtraReports.Single(Function(c) c.GUID.Equals(GUID))

        If data IsNot Nothing Then
            If data.LayoutData IsNot Nothing Then
                reportData = data.LayoutData.ToArray()
            End If

        End If

        Return reportData

    End Function

    Public Overrides Function GetUrls() As Dictionary(Of String, String)
        Dim reports As New Dictionary(Of String, String)
        Dim data = dc.tblXtraReports.ToList()
        For Each item In data
            reports.Add(item.ReportID, item.DisplayName)
        Next
        Return reports
    End Function

    Public Overrides Function IsValidUrl(ByVal GUID As String) As Boolean
        Try
            Dim data = dc.tblXtraReports.Single(Function(c) c.GUID.Equals(GUID))
            If data IsNot Nothing Then
                Return True
            Else
                Return False
            End If
        Catch
            Return False
        End Try
    End Function

    Public Overrides Sub SetData(ByVal report As XtraReport, ByVal GUID As String)
        Try
            Dim data = dc.tblXtraReports.Single(Function(c) c.GUID.Equals(GUID))
            If data IsNot Nothing Then
                Using ms As New MemoryStream()
                    report.SaveLayoutToXml(ms)
                    data.LayoutData = ms.GetBuffer()
                    data.ModifyBy = Me._context.User.Identity.Name
                    data.ModifyDate = Now()
                End Using
                dc.SubmitChanges()
            End If
        Catch ex As Exception
            'Pass readable exception message to the Web Report Designer
            Throw New FaultException(ex.Message)
        End Try
    End Sub

    Public Overrides Function SetNewData(ByVal report As XtraReport, ByVal DisplayName As String) As String

        Dim newData As New tblXtraReport

        Using ms As New MemoryStream()
            report.SaveLayoutToXml(ms)
            newData.LayoutData = ms.GetBuffer()
            newData.DisplayName = DisplayName
            newData.Owner = Me._context.User.Identity.Name
            newData.CreateDate = Now()
            newData.GUID = System.Guid.NewGuid().ToString()
        End Using


        dc.tblXtraReports.InsertOnSubmit(newData)
        dc.SubmitChanges()


        Return newData.ReportID.ToString()

    End Function
End Class
