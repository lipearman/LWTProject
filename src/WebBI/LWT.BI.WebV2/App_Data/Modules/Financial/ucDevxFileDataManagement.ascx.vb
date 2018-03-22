Imports Portal.Components
Imports DevExpress.Web.ASPxTreeList
Imports DevExpress.Web
Imports System.Data.SqlClient
Imports System.Data
Imports DevExpress.Web.Data
Imports System.IO
Imports DevExpress.DataAccess.Excel
Imports DevExpress.XtraPivotGrid
Imports DevExpress.Web.ASPxPivotGrid
Imports System.Web.Hosting
Imports DevExpress.Web.ASPxSpreadsheet

Partial Class Modules_Financial_ucDevxFileDataManagement
    Inherits PortalModuleControl
  


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If (Not IsPostBack) Then
            Dim _DocumentBrowsingFolderPath As String = ""
            _DocumentBrowsingFolderPath = Server.MapPath("~/App_Data/Modules/Financial/Data/Document")
            Spreadsheet.WorkDirectory = _DocumentBrowsingFolderPath
            Spreadsheet.SettingsDocumentSelector.UploadSettings.Enabled = True
            'Spreadsheet.SettingsDocumentSelector.EditingSettings.AllowDelete = True

            Spreadsheet.RibbonMode = SpreadsheetRibbonMode.OneLineRibbon

            'DevxSpreadsheetUtils.HideAllTabsExceptOpenFileAndFullScreen(Spreadsheet)

            'Dim filePath = Path.Combine(Spreadsheet.WorkDirectory, "BreakevenAnalysis.xlsx")
            'Spreadsheet.Open(filePath)

            'Spreadsheet.Document.Worksheets.ActiveWorksheet.ActiveView.ShowHeadings = False
            'Spreadsheet.Document.Worksheets.ActiveWorksheet.ActiveView.ShowGridlines = False



            Dim fileEntries As String() = Directory.GetFiles(_DocumentBrowsingFolderPath, "*.xlsx")
            If fileEntries.Count > 0 Then


                Dim LastFileName = (From c In fileEntries Order By c Descending).FirstOrDefault()


                Spreadsheet.Open(LastFileName)

                Spreadsheet.Document.Worksheets.ActiveWorksheet = Spreadsheet.Document.Worksheets(0)


                'Spreadsheet.Document.Worksheets.ActiveWorksheet.Selection = Spreadsheet.Document.Worksheets.ActiveWorksheet(0, 1)

                Spreadsheet.Document.Worksheets.ActiveWorksheet.ScrollTo(0, 0)
            End If






        End If

    End Sub

End Class


