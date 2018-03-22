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
Imports SqlBulkTools

Partial Class Modules_ucDevxAPDOutStandingImportV2
    Inherits PortalModuleControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(LWT.Website.webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If

        pnMain.HeaderText = portalSettings.ActiveTab.TabName
        pnMain.HeaderImage.IconID = portalSettings.ActiveTab.IconID

        SqlDataSource_RawData.SelectParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name

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

            Dim constr = ConfigurationManager.ConnectionStrings("PortalBIRawDataConnection").ConnectionString

            'Dim InsertedTableName = "tblAccountingDNRemark"
            Dim dm As New Datamanage
            'Try

            Dim dt1 = dm.ExcelToDataTable(FileName)
            '    Using bulkCopy As New SqlBulkCopy(conn1)
            '        bulkCopy.DestinationTableName = "[" & InsertedTableName & "]"
            '        bulkCopy.BulkCopyTimeout = 3600000
            '        bulkCopy.BatchSize = 5000
            '        bulkCopy.ColumnMappings.Add("Inv No.", "Inv No.")
            '        bulkCopy.ColumnMappings.Add("Remark", "Remark")

            '        bulkCopy.WriteToServer(dt)

            '        bulkCopy.WriteToServer(
            '    End Using


            '    e.CallbackData = "success"

            'Catch ex As Exception
            '    e.CallbackData = ex.Message
            'End Try
            Try
                Dim dt2 As New DataTable()
                dt2.Columns.AddRange(New DataColumn(3) {New DataColumn("Inv No.", GetType(Double)), _
                                                       New DataColumn("Remark", GetType(String)), _
                                                       New DataColumn("BY", GetType(String)), _
                                                       New DataColumn("LastDate", GetType(DateTime))})

                For Each row As DataRow In dt1.Rows
                    If Not DBNull.Value.Equals(row("Remark")) Then
                        Dim _InvNo As Double = Double.Parse(row("Inv No."))
                        Dim _Remark As String = row("Remark") 'IIf(DBNull.Value.Equals(row("Remark")), "", row("Remark"))
                        Dim _BY As String = HttpContext.Current.User.Identity.Name
                        dt2.Rows.Add(_InvNo, _Remark, _BY, Now)
                    End If
                Next


                 Using con As New SqlConnection(constr)
                    Using cmd As New SqlCommand("Update_AccountingDNRemarks")
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = con
                        cmd.Parameters.AddWithValue("@AccountingDNRemarks", dt2)
                        con.Open()
                        cmd.ExecuteNonQuery()
                        con.Close()
                    End Using
                End Using

                e.CallbackData = "success"

            Catch ex As Exception
                e.CallbackData = ex.Message
            End Try



        End If
    End Sub

End Class


