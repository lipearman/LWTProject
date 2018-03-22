Imports Portal.Components
Imports System.Data.SqlClient
Imports System.Data
Imports DevExpress.Web
Imports DevExpress.XtraPivotGrid
Imports DevExpress.Web.Data

Imports Microsoft.VisualBasic
Imports System
Imports System.Web.UI
Imports DevExpress.Data
Imports System.IO
Imports DevExpress.Web.Demos



Partial Class Modules_ucDevxNotification
    Inherits PortalModuleControl
    'Protected Property SubmissionID() As String
    '    Get
    '        Return HiddenField.Get("SubmissionID").ToString()
    '    End Get
    '    Set(ByVal value As String)
    '        HiddenField.Set("SubmissionID", value)
    '    End Set
    'End Property
    'Private ReadOnly Property UploadedFilesStorage() As UploadedFilesStorage
    '    Get
    '        Return UploadControlHelper.GetUploadedFilesStorageByKey(SubmissionID)
    '    End Get
    'End Property


    Protected Sub Page_Load(ByVal sender As Object, ByVal args As EventArgs) Handles Me.Load

        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(LWT.Website.webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")

        End If
        'pnMain.HeaderImage.IconID = portalSettings.ActiveTab.IconID

        pnMain.HeaderText = "Medical Malpractice Notification" 'portalSettings.ActiveTab.TabName


        If (Not IsPostBack) Then
            Grid.DataBind()
            Grid.ExpandRow(1)
        End If

        'If (Not IsPostBack) Then
        '    SubmissionID = UploadControlHelper.GenerateUploadedFilesStorageKey()
        '    UploadControlHelper.AddUploadedFilesStorage(SubmissionID)
        'End If
    End Sub
    Protected Sub Grid_FillContextMenuItems(ByVal sender As Object, ByVal e As ASPxGridViewContextMenuEventArgs) Handles Grid.FillContextMenuItems
        If e.MenuType = GridViewContextMenuType.Rows Then
            'Dim item = e.CreateItem("Export", "Export")
            'item.BeginGroup = True
            'e.Items.Insert(e.Items.IndexOfCommand(GridViewContextMenuCommand.Refresh), item)

            'AddMenuSubItem(item, "PDF", "ExportToPDF", "export_exporttopdf_16x16", True)
            'AddMenuSubItem(item, "XLS", "ExportToXLS", "export_exporttoxls_16x16", True)


            'Dim itemView = e.CreateItem("Open", "OpenRow")
            'itemView.BeginGroup = False
            'itemView.Image.IconID = "print_preview_16x16office2013"

            'e.Items.Insert(e.Items.IndexOfCommand(GridViewContextMenuCommand.EditRow), itemView)

            'Dim itemEdit = e.CreateItem("Edit", "ModifyRow")
            'itemEdit.BeginGroup = False
            'itemEdit.Image.IconID = "edit_edit_16x16"

            'e.Items.Insert(e.Items.IndexOfCommand(GridViewContextMenuCommand.EditRow), itemEdit)

            'Dim itemDelete = e.CreateItem("Delete", "DelRow")
            'itemDelete.BeginGroup = False
            'itemDelete.Image.IconID = "edit_delete_16x16"

            'e.Items.Insert(e.Items.IndexOfCommand(GridViewContextMenuCommand.EditRow), itemDelete)

        End If
    End Sub
    Private Shared Sub AddMenuSubItem(ByVal parentItem As GridViewContextMenuItem, ByVal text As String, ByVal name As String, ByVal iconID As String, ByVal isPostBack As Boolean)
        Dim exportToXlsItem = parentItem.Items.Add(text, name)
        exportToXlsItem.Image.IconID = iconID
    End Sub

    Protected Sub Grid_ContextMenuItemClick(ByVal sender As Object, ByVal e As ASPxGridViewContextMenuItemClickEventArgs) Handles Grid.ContextMenuItemClick
        Select Case e.Item.Name
            Case "ExportToPDF"
                GridExporter.WritePdfToResponse()
            Case "ExportToXLS"
                GridExporter.WriteXlsToResponse()
        End Select
    End Sub

    Protected Sub Grid_AddSummaryItemViaContextMenu(ByVal sender As Object, ByVal e As ASPxGridViewAddSummaryItemViaContextMenuEventArgs) Handles Grid.AddSummaryItemViaContextMenu
        If e.SummaryItem.FieldName = "TotalClaim" AndAlso e.SummaryItem.SummaryType = SummaryItemType.Average Then
            e.SummaryItem.ValueDisplayFormat = "{0:0.00}"
        End If
    End Sub

    Private totalSum As Integer

    Protected Sub Grid_CustomSummaryCalculate(ByVal sender As Object, ByVal e As CustomSummaryEventArgs) Handles Grid.CustomSummaryCalculate
        If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Start Then
            totalSum = 0
        End If
        If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Calculate Then
            If Grid.Selection.IsRowSelectedByKey(e.GetValue(Grid.KeyFieldName)) Then
                totalSum += Convert.ToInt32(e.FieldValue)
            End If
        End If
        If e.SummaryProcess = DevExpress.Data.CustomSummaryProcess.Finalize Then
            e.TotalValue = totalSum
        End If
    End Sub

    Protected Sub Grid_ToolbarItemClick(ByVal source As Object, ByVal e As ASPxGridToolbarItemClickEventArgs) Handles Grid.ToolbarItemClick
        Select Case e.Item.Name
            Case "ExportToPDF"
                GridExporter.WritePdfToResponse()
            Case "ExportToXLS"
                GridExporter.WriteXlsToResponse()
            Case "ExportToXLSX"
                GridExporter.WriteXlsxToResponse()
            Case Else
        End Select
    End Sub

    Protected Sub Grid_CustomJSProperties(ByVal sender As Object, ByVal e As ASPxGridViewClientJSPropertiesEventArgs) Handles Grid.CustomJSProperties
        e.Properties("cpRowCount") = DirectCast(sender, ASPxGridView).VisibleRowCount
    End Sub


    Protected Sub popupViewForm_WindowCallback(ByVal source As Object, ByVal e As PopupWindowCallbackArgs) Handles popupViewForm.WindowCallback
        Dim _REFNo As String = e.Parameter.ToString()

        Session("REFNo") = _REFNo


        Using dc As New DataClasses_BDMSExt()
            Dim _data = (From c In dc.V_ClaimDetails Where c.REFNO.Equals(_REFNo)).FirstOrDefault()
            viewForm.DataSource = _data
            viewForm.DataBind()

        End Using

      




    End Sub

    Protected Sub popupEditForm_WindowCallback(ByVal source As Object, ByVal e As PopupWindowCallbackArgs) Handles popupEditForm.WindowCallback
        Dim _REFNo = Session("REFNo").ToString()
        Using dc As New DataClasses_BDMSExt()
            Dim _data = (From c In dc.tblClaims Where c.REFNO.Equals(_REFNo)).FirstOrDefault()
            editFormPage1.DataSource = _data
            editFormPage1.DataBind()

            editFormPage2.DataSource = _data
            editFormPage2.DataBind()
        End Using
    End Sub


    'Protected Sub DocumentsUploadControl_FileUploadComplete(ByVal sender As Object, ByVal e As DevExpress.Web.FileUploadCompleteEventArgs) Handles DocumentsUploadControl.FileUploadComplete
    '    Dim isSubmissionExpired As Boolean = False
    '    If UploadedFilesStorage Is Nothing Then
    '        isSubmissionExpired = True
    '        UploadControlHelper.AddUploadedFilesStorage(SubmissionID)
    '    End If
    '    Dim tempFileInfo As UploadedFileInfo = UploadControlHelper.AddUploadedFileInfo(SubmissionID, e.UploadedFile.FileName)

    '    e.UploadedFile.SaveAs(tempFileInfo.FilePath)

    '    If e.IsValid Then
    '        e.CallbackData = tempFileInfo.UniqueFileName & "|" & isSubmissionExpired
    '    End If
    'End Sub



    Private Const UploadDirectory As String = "~/UploadControl/UploadImages/"
    Protected Sub UploadControl_FileUploadComplete(ByVal sender As Object, ByVal e As FileUploadCompleteEventArgs) Handles UploadControl.FileUploadComplete
        Dim resultExtension As String = Path.GetExtension(e.UploadedFile.FileName)
        Dim resultFileName As String = Path.ChangeExtension(Path.GetRandomFileName(), resultExtension)
        Dim resultFileUrl As String = UploadDirectory & resultFileName
        Dim resultFilePath As String = MapPath(resultFileUrl)
        e.UploadedFile.SaveAs(resultFilePath)

        UploadingUtils.RemoveFileWithDelay(resultFileName, resultFilePath, 5)

        Dim name As String = e.UploadedFile.FileName
        Dim url As String = ResolveClientUrl(resultFileUrl)
        Dim sizeInKilobytes As Long = e.UploadedFile.ContentLength / 1024
        Dim sizeText As String = sizeInKilobytes.ToString() & " KB"
        e.CallbackData = name & "|" & url & "|" & sizeText
    End Sub

End Class

'Public Class UploadedFilesStorage
'    Public Property Path() As String
'    Public Property Key() As String
'    Public Property LastUsageTime() As DateTime

'    Public Property Files() As IList(Of UploadedFileInfo)
'End Class
'Public Class UploadedFileInfo
'    Public Property UniqueFileName() As String
'    Public Property OriginalFileName() As String
'    Public Property FilePath() As String
'    Public Property FileSize() As String
'End Class


'Public NotInheritable Class UploadControlHelper
'    Private Const DisposeTimeout As Integer = 5
'    Private Const FolderKey As String = "UploadDirectory"
'    Private Const TempDirectory As String = "~/UploadControl/Temp/"
'    Private Shared ReadOnly storageListLocker As Object = New Object()

'    Private Sub New()
'    End Sub
'    Private Shared ReadOnly Property Context() As HttpContext
'        Get
'            Return HttpContext.Current
'        End Get
'    End Property
'    Private Shared ReadOnly Property RootDirectory() As String
'        Get
'            Return Context.Request.MapPath(TempDirectory)
'        End Get
'    End Property

'    Private Shared uploadedFilesStorageList_Renamed As IList(Of UploadedFilesStorage)
'    Private Shared ReadOnly Property UploadedFilesStorageList() As IList(Of UploadedFilesStorage)
'        Get
'            Return uploadedFilesStorageList_Renamed
'        End Get
'    End Property

'    Shared Sub New()
'        uploadedFilesStorageList_Renamed = New List(Of UploadedFilesStorage)()
'    End Sub

'    Private Shared Function CreateTempDirectoryCore() As String
'        Dim uploadDirectory As String = Path.Combine(RootDirectory, Path.GetRandomFileName())
'        Directory.CreateDirectory(uploadDirectory)

'        Return uploadDirectory
'    End Function
'    Public Shared Function GetUploadedFilesStorageByKey(ByVal key As String) As UploadedFilesStorage
'        SyncLock storageListLocker
'            Return GetUploadedFilesStorageByKeyUnsafe(key)
'        End SyncLock
'    End Function
'    Private Shared Function GetUploadedFilesStorageByKeyUnsafe(ByVal key As String) As UploadedFilesStorage
'        Dim storage As UploadedFilesStorage = UploadedFilesStorageList.Where(Function(i) i.Key = key).SingleOrDefault()
'        If storage IsNot Nothing Then
'            storage.LastUsageTime = DateTime.Now
'        End If
'        Return storage
'    End Function
'    Public Shared Function GenerateUploadedFilesStorageKey() As String
'        Return Guid.NewGuid().ToString("N")
'    End Function
'    Public Shared Sub AddUploadedFilesStorage(ByVal key As String)
'        SyncLock storageListLocker
'            Dim storage As UploadedFilesStorage = New UploadedFilesStorage With {.Key = key, .Path = CreateTempDirectoryCore(), .LastUsageTime = DateTime.Now, .Files = New List(Of UploadedFileInfo)()}
'            UploadedFilesStorageList.Add(storage)
'        End SyncLock
'    End Sub
'    Public Shared Sub RemoveUploadedFilesStorage(ByVal key As String)
'        SyncLock storageListLocker
'            Dim storage As UploadedFilesStorage = GetUploadedFilesStorageByKeyUnsafe(key)
'            If storage IsNot Nothing Then
'                Directory.Delete(storage.Path, True)
'                UploadedFilesStorageList.Remove(storage)
'            End If
'        End SyncLock
'    End Sub
'    Public Shared Sub RemoveOldStorages()
'        If (Not Directory.Exists(RootDirectory)) Then
'            Directory.CreateDirectory(RootDirectory)
'        End If

'        SyncLock storageListLocker
'            Dim existingDirectories() As String = Directory.GetDirectories(RootDirectory)
'            For Each directoryPath As String In existingDirectories
'                Dim storage As UploadedFilesStorage = UploadedFilesStorageList.Where(Function(i) i.Path = directoryPath).SingleOrDefault()
'                If storage Is Nothing OrElse (DateTime.Now - storage.LastUsageTime).TotalMinutes > DisposeTimeout Then
'                    Directory.Delete(directoryPath, True)
'                    If storage IsNot Nothing Then
'                        UploadedFilesStorageList.Remove(storage)
'                    End If
'                End If
'            Next directoryPath
'        End SyncLock
'    End Sub
'    Public Shared Function AddUploadedFileInfo(ByVal key As String, ByVal originalFileName As String) As UploadedFileInfo
'        Dim currentStorage As UploadedFilesStorage = GetUploadedFilesStorageByKey(key)
'        Dim fileInfo As UploadedFileInfo = New UploadedFileInfo With {.FilePath = Path.Combine(currentStorage.Path, Path.GetRandomFileName()), .OriginalFileName = originalFileName, .UniqueFileName = GetUniqueFileName(currentStorage, originalFileName)}
'        currentStorage.Files.Add(fileInfo)

'        Return fileInfo
'    End Function
'    Public Shared Function GetDemoFileInfo(ByVal key As String, ByVal fileName As String) As UploadedFileInfo
'        Dim currentStorage As UploadedFilesStorage = GetUploadedFilesStorageByKey(key)
'        Return currentStorage.Files.Where(Function(i) i.UniqueFileName = fileName).SingleOrDefault()
'    End Function
'    Public Shared Function GetUniqueFileName(ByVal currentStorage As UploadedFilesStorage, ByVal fileName As String) As String
'        Dim baseName As String = Path.GetFileNameWithoutExtension(fileName)
'        Dim ext As String = Path.GetExtension(fileName)
'        Dim index As Integer = 1

'        Do While currentStorage.Files.Any(Function(i) i.UniqueFileName = fileName)
'            fileName = String.Format("{0} ({1}){2}", baseName, index, ext)
'            index += 1
'        Loop

'        Return fileName
'    End Function
'End Class
