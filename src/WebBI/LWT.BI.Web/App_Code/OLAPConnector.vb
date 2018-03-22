Imports Microsoft.VisualBasic
Imports System.Text
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports DevExpress.Web.ASPxPivotGrid
Imports DevExpress.Web
Imports DevExpress.XtraPivotGrid.Data

Public Class OLAPConnector
    Private Const ProviderDownloadUrl As String = "http://www.microsoft.com/downloads/details.aspx?FamilyID=50b97994-8453-4998-8226-fa42ec403d17#ASOLEDB", NoProviderErrorString As String = "To connect to olap cubes, you should have Microsoft SQL Server 2005<br />" & "Analysis Services 9.0 (or newer) OLE DB Provider installed on your system. You can get<br />" & "the latest version of this provider here:<br />" & "<a href=""" + ProviderDownloadUrl & """ target=""_blank"">" + ProviderDownloadUrl & "</a>.", ExceptionErrorString As String = "Unfortunately, an unexpected exception was raised when trying to connect to the OLAP datasource:"

    Public Shared Function TryConnect(ByVal pivot As ASPxPivotGrid) As String
        If (Not OLAPMetaGetter.IsProviderAvailable) Then
            pivot.OLAPConnectionString = Nothing
            Return NoProviderErrorString
        End If
        Try
            pivot.DataBind()
        Catch exception As OLAPConnectionException
            pivot.OLAPConnectionString = Nothing
            Return ExceptionErrorString & "<br />" & exception.Message
        End Try
        Return Nothing
    End Function

    Public Shared Function CreateErrorPanel(ByVal [error] As String) As Control
        Dim panel As New ASPxRoundPanel()
        panel.ShowHeader = False
        panel.Style("margin-bottom") = "10px"
        Dim table As New Table()
        table.Rows.Add(New TableRow())
        table.Rows(0).Cells.Add(New TableCell())
        table.Rows(0).Cells.Add(New TableCell())
        panel.Controls.Add(table)
        Dim errorIcon As New Image()
        errorIcon.ImageUrl = "~/Content/Demo/Error.gif"
        errorIcon.AlternateText = "Error"
        table.Rows(0).Cells(0).Controls.Add(errorIcon)
        table.Rows(0).Cells(1).Text = [error]
        Return panel
    End Function
End Class
