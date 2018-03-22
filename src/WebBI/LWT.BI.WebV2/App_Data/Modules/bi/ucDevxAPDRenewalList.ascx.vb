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

Partial Class Modules_ucDevxAPDRenewalList
    Inherits PortalModuleControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(LWT.Website.webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If

        pnMain.HeaderText = portalSettings.ActiveTab.TabName
        pnMain.HeaderImage.IconID = portalSettings.ActiveTab.IconID



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

            Dim InsertedTableName = "tblAPDRenewalList"
            Dim dm As New Datamanage
            Dim dt1 = dm.ExcelToDataTable(FileName)



            Try
                Dim dt2 As New DataTable()
                dt2.Columns.AddRange(New DataColumn(21) {New DataColumn("LotNo", GetType(Integer)), _
                                                        New DataColumn("No", GetType(Integer)), _
                                                        New DataColumn("ExpDate", GetType(String)), _
                                                        New DataColumn("Officer", GetType(String)), _
                                                        New DataColumn("OfficerForSort", GetType(String)), _
                                                        New DataColumn("Type", GetType(String)), _
                                                        New DataColumn("Group", GetType(String)), _
                                                        New DataColumn("Code", GetType(String)), _
                                                        New DataColumn("Insured", GetType(String)), _
                                                        New DataColumn("Insurer", GetType(String)), _
                                                        New DataColumn("PolicyNo", GetType(String)), _
                                                        New DataColumn("SumInsured", GetType(Double)), _
                                                        New DataColumn("ExpiringPremium", GetType(Double)), _
                                                        New DataColumn("ReminderDate", GetType(String)), _
                                                        New DataColumn("ResponseDate", GetType(String)), _
                                                        New DataColumn("Status", GetType(String)), _
                                                        New DataColumn("RenewalPremium", GetType(String)), _
                                                        New DataColumn("RemarkFromPolicy", GetType(String)), _
                                                        New DataColumn("RemarkFromAE", GetType(String)), _
                                                        New DataColumn("FiscalYear", GetType(Integer)), _
                                                        New DataColumn("ImportBy", GetType(String)), _
                                                        New DataColumn("ImportDate", GetType(DateTime)) _
                                                       })


                Dim _ImportBy As String = HttpContext.Current.User.Identity.Name
                Dim _ImportDate As DateTime = Now
                Dim _LotNo As Integer = Session("LotNo")
                Dim _FiscalYear = SqlHelper.ExecuteScalar(constr, CommandType.Text, "select FiscalYear from V_APDRenewalList where LotNo='" & _LotNo & "'").ToString()


                For Each row As DataRow In dt1.Rows
                    Dim _No As Integer = row("No")
                    Dim _ExpDate As String = IIf(DBNull.Value.Equals(row("ExpDate")), "", row("ExpDate"))
                    Dim _Officer As String = IIf(DBNull.Value.Equals(row("Officer")), "", row("Officer"))
                    Dim _Officer_ForSort As String = IIf(DBNull.Value.Equals(row("OfficerForSort")), "", row("OfficerForSort"))
                    Dim _Type As String = IIf(DBNull.Value.Equals(row("Type")), "", row("Type"))
                    Dim _Group As String = IIf(DBNull.Value.Equals(row("Group")), "", row("Group"))
                    Dim _Code As String = IIf(DBNull.Value.Equals(row("Code")), "", row("Code"))
                    Dim _Insured As String = IIf(DBNull.Value.Equals(row("Insured")), "", row("Insured"))
                    Dim _Insurer As String = IIf(DBNull.Value.Equals(row("Insurer")), "", row("Insurer"))
                    Dim _PolicyNo As String = IIf(DBNull.Value.Equals(row("PolicyNo")), "", row("PolicyNo"))
                    Dim _SumInsured As Double = row("SumInsured")
                    Dim _Expiring_Premium As Double = row("ExpiringPremium")
                    Dim _Reminde_Date As String = IIf(DBNull.Value.Equals(row("ReminderDate")), "", row("ReminderDate"))
                    Dim _Response_Date As String = IIf(DBNull.Value.Equals(row("ResponseDate")), "", row("ResponseDate"))
                    Dim _Status_RLEC As String = IIf(DBNull.Value.Equals(row("Status")), "", row("Status"))
                    Dim _Renewal_Premium As String = IIf(DBNull.Value.Equals(row("RenewalPremium")), "", row("RenewalPremium"))
                    Dim _RemarkFromPolicy As String = IIf(DBNull.Value.Equals(row("RemarkFromPolicy")), "", row("RemarkFromPolicy"))
                    Dim _RemarkFromAE As String = IIf(DBNull.Value.Equals(row("RemarkFromAE")), "", row("RemarkFromAE"))

                    dt2.Rows.Add(_LotNo, _No, _ExpDate, _Officer, _Officer_ForSort _
                                 , _Type, _Group, _Code, _Insured, _Insurer, _PolicyNo, _SumInsured, _
                                 _Expiring_Premium, _Reminde_Date, _Response_Date, _Status_RLEC, _
                                 _Renewal_Premium, _RemarkFromPolicy, _RemarkFromAE, _FiscalYear, _ImportBy, _ImportDate)

                    'If Not DBNull.Value.Equals(row("Remark")) Then
                    '    Dim _InvNo As Double = Double.Parse(row("Inv No."))
                    '    Dim _Remark As String = row("Remark") 'IIf(DBNull.Value.Equals(row("Remark")), "", row("Remark"))
                    '    Dim _BY As String = HttpContext.Current.User.Identity.Name
                    '    dt2.Rows.Add(_InvNo, _Remark, _BY, Now)
                    'End If
                Next


                Using con As New SqlConnection(constr)
                    Using cmd As New SqlCommand("Update_APDRenewalList")
                        cmd.CommandType = CommandType.StoredProcedure
                        cmd.Connection = con
                        cmd.Parameters.AddWithValue("@APDRenewalList", dt2)
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


    Protected Sub popupDetails_WindowCallback(ByVal source As Object, ByVal e As PopupWindowCallbackArgs) Handles popupDetails.WindowCallback
        Dim _LotNo As String = e.Parameter.ToString()
        Session("LotNo") = _LotNo
    End Sub


    Protected Sub cbUpload_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbUpload.Callback

        'Dim _LotNo = e.Parameter.ToString()
     
        Session("LotNo") = cbLotNo.Value

        e.Result = "success"


    End Sub
    Protected Sub btnExportData_Click(sender As Object, e As EventArgs)
        'GridExporter.WriteXlsxToResponse(Guid.NewGuid().ToString(), False)
        GridExporter.WriteXlsxToResponse(Guid.NewGuid().ToString(), False, New DevExpress.XtraPrinting.XlsxExportOptionsEx With {.ExportType = DevExpress.Export.ExportType.WYSIWYG})
    End Sub
End Class


