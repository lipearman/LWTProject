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

Imports DevExpress.XtraPrinting
Imports DevExpress.Export

Partial Class Modules_ucDevxAPDOutStandingEnquiryV2
    Inherits PortalModuleControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(LWT.Website.webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If

        pnMain.HeaderText = portalSettings.ActiveTab.TabName
        pnMain.HeaderImage.IconID = portalSettings.ActiveTab.IconID



    End Sub
    Protected Sub Grid_ToolbarItemClick(ByVal source As Object, ByVal e As ASPxGridToolbarItemClickEventArgs) Handles gridRawData.ToolbarItemClick

        Select Case e.Item.Name
            Case "ExportToXLS"
                GridExporter.WriteXlsToResponse("ASOutstanding.xls", New XlsExportOptionsEx() With {.ExportType = ExportType.WYSIWYG})
            Case "ExportToXLSX"
                GridExporter.WriteXlsxToResponse("ASOutstanding.xlsx", New XlsxExportOptionsEx() With {.ExportType = ExportType.WYSIWYG})
            Case Else
        End Select
    End Sub

    Protected Sub frmImport_Upload_FileUploadComplete(ByVal sender As Object, ByVal e As DevExpress.Web.FileUploadCompleteEventArgs) Handles frmImport_UploadFile.FileUploadComplete

        If frmImport_UploadFile.Page IsNot Nothing Then
            Dim _GUID As String = System.Guid.NewGuid().ToString()
            Dim FilePath = Page.MapPath("~/UploadFiles/")
            If Not System.IO.Directory.Exists(FilePath) Then
                System.IO.Directory.CreateDirectory(FilePath)
            End If
            Dim FileName As String = ""
            If e.UploadedFile.FileName.ToLower().IndexOf(".xlsx") > -1 Then
                FileName = String.Format(FilePath & "/{0}.xlsx", _GUID)
                e.UploadedFile.SaveAs(FileName)
            ElseIf e.UploadedFile.FileName.ToLower().IndexOf(".xls") > -1 Then
                FileName = String.Format(FilePath & "/{0}.xls", _GUID)
                e.UploadedFile.SaveAs(FileName)
            End If

            Dim conn1 = ConfigurationManager.ConnectionStrings("PortalBIRawDataConnection").ConnectionString

            Dim InsertedTableName = "tblAccountingMasterData"
            Dim dm As New Datamanage

            Try


                dm.ValidateDataToServer(FileName, conn1, InsertedTableName)

                HostingEnvironment.Cache.Remove(InsertedTableName)

                dm.ReplaceDataToServer(FileName, conn1, InsertedTableName)

                e.CallbackData = "success"

            Catch ex As Exception
                e.CallbackData = ex.Message
            End Try




        End If
    End Sub

End Class


