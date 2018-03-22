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

Partial Class Modules_ucDevxFileDataSourceManager3
    Inherits PortalModuleControl
    Private _DSID As String = "38"
    Private _GUID As String = "tblFinancialRawData"

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Init

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(LWT.Website.webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If

        pnMain.HeaderText = portalSettings.ActiveTab.TabName
        pnMain.HeaderImage.IconID = portalSettings.ActiveTab.IconID




        If (Not IsPostBack) Then
            SqlDataSource_gridData.SelectParameters("DSID").DefaultValue = _DSID
            SqlDataSource_RawData.SelectParameters("tableName").DefaultValue = _GUID
            SqlDataSource_Fields.SelectParameters("DSID").DefaultValue = _DSID
            SqlDataSource_COLUMN_NAME.SelectParameters("DSGUID").DefaultValue = _GUID

            BindData()
        End If
    End Sub

    Private Sub BindData()
        'gridRawData.Columns.Clear()
        'gridRawData.AutoGenerateColumns = True
        'gridRawData.DataBind()
        'gridFields.DataBind()
        Using dc As New DataClasses_PortalBIExt
            Dim _data = (From c In dc.tblDataSourceFiles Where c.GUID.Equals(_GUID)).FirstOrDefault()
            editTitle.Text = _data.Title
            editDescription.Text = _data.DESCRIPTION
            'editASAP.Value = _data.ASATDATE
        End Using
    End Sub

    'Protected Sub gridRawData_Init(sender As Object, e As EventArgs) Handles gridRawData.Init
    '    gridRawData.DataBind()
    'End Sub

    Protected Sub cbUpdateData_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbUpdateData.Callback

        Using dc As New DataClasses_PortalBIExt
            Dim _data = (From c In dc.tblDataSourceFiles Where c.GUID.Equals(_GUID)).FirstOrDefault()


            With _data

                .Title = editTitle.Text

                .DESCRIPTION = editDescription.Text
                .ASATDATE = Now 'editASAP.Value

                .MODIFYBY = HttpContext.Current.User.Identity.Name
                .MODIFYDATE = Now

            End With


            dc.SubmitChanges()
        End Using


        e.Result = "saved"
    End Sub

    Protected Sub cbRetrieve_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbRetrieveFields.Callback


        Dim dm As New Datamanage
        Dim conn1 = ConfigurationManager.ConnectionStrings("PortalBIRawDataConnection").ConnectionString
        Try
            dm.RetrieveFields(conn1, _GUID)

            e.Result = "success"

        Catch ex As Exception
            e.Result = ex.Message
        End Try



    End Sub

    Protected Sub frmImport_UpdateFile_FileUploadComplete(ByVal sender As Object, ByVal e As DevExpress.Web.FileUploadCompleteEventArgs) Handles frmImport_UpdateFile.FileUploadComplete

        If frmImport_UpdateFile.Page IsNot Nothing Then

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

            Dim InsertedTableName = _GUID
            Dim dm As New Datamanage

            Try


                dm.ValidateDataToServer(FileName, conn1, InsertedTableName)

                HostingEnvironment.Cache.Remove(_GUID)

                dm.ReplaceDataToServer(FileName, conn1, InsertedTableName)


                Using dc As New DataClasses_PortalBIExt
                    Dim _data = (From c In dc.tblDataSourceFiles Where c.GUID.Equals(_GUID)).FirstOrDefault()
                    With _data
                        .MODIFYBY = HttpContext.Current.User.Identity.Name
                        .MODIFYDATE = Now
                    End With
                    dc.SubmitChanges()
                End Using


                e.CallbackData = "success"

            Catch ex As Exception
                e.CallbackData = ex.Message
            End Try



        End If
    End Sub

    Protected Sub frmImport_InsertFile_FileUploadComplete(ByVal sender As Object, ByVal e As DevExpress.Web.FileUploadCompleteEventArgs) Handles frmImport_InsertFile.FileUploadComplete

        If frmImport_InsertFile.Page IsNot Nothing Then

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

            Dim InsertedTableName = _GUID
            Dim dm As New Datamanage

            Try
                dm.ValidateDataToServer(FileName, conn1, InsertedTableName)

                dm.AppendDataToServer(FileName, conn1, InsertedTableName)

                Using dc As New DataClasses_PortalBIExt
                    Dim _data = (From c In dc.tblDataSourceFiles Where c.GUID.Equals(_GUID)).FirstOrDefault()
                    With _data
                        .MODIFYBY = HttpContext.Current.User.Identity.Name
                        .MODIFYDATE = Now
                    End With
                    dc.SubmitChanges()
                End Using


                e.CallbackData = "success"

            Catch ex As Exception
                e.CallbackData = ex.Message
            End Try

        End If
    End Sub

    Protected Sub cbAddColumns_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbAddColumns.Callback

        Dim _columns = e.Parameter.ToString()
        'Dim _GUID As String = Session("DSGUID")

        If Not String.IsNullOrEmpty(_columns) Then

            Dim connRawdata As String = ConfigurationManager.ConnectionStrings("PortalBIRawDataConnection").ConnectionString

            Using dc As New DataClasses_PortalBIExt()
                Dim _data = (From c In dc.tblDataSourceFiles Where c.GUID.Equals(_GUID)).FirstOrDefault()
                If _data IsNot Nothing Then


                    Dim _newfieldList As New List(Of tblDataSourceFile_Field)

                    For Each _field In _columns.Split(",")

                        Dim _tblAttributes = (From c In dc.tblDataSourceFile_Fields Where c.DS_ID.Equals(_data.ID) And c.FIELD_NAME.Equals(_field)).FirstOrDefault()
                        If _tblAttributes Is Nothing Then

                            Dim _newfield As New tblDataSourceFile_Field
                            With _newfield

                                .FIELD_CAPTION = _field
                                .FIELD_NAME = _field
                                .DS_ID = _data.ID




                                Dim data_type = SqlHelper.ExecuteScalar(connRawdata, CommandType.Text, "SELECT data_type FROM information_schema.columns WHERE table_name = '" & _GUID & "' and column_name='" & _field & "'")

                                If data_type IsNot Nothing Then

                                    Dim _Type = data_type.ToString().ToLower()
                                    Select Case _Type
                                        Case "int"
                                            .SummaryType = 0
                                            .CellFormat_FormatType = 1
                                            .CellFormat_FormatString = "{0:N0}"
                                            .Dimension = "FA"
                                        Case "float"
                                            .SummaryType = 1
                                            .CellFormat_FormatType = 1
                                            .CellFormat_FormatString = "{0:N2}"
                                            .Dimension = "ME"

                                        Case Else
                                            .SummaryType = 0
                                            .CellFormat_FormatType = 3
                                            .CellFormat_FormatString = "{0:N0}"
                                            .Dimension = "FA"
                                    End Select



                                Else
                                    .SummaryType = 0
                                    .CellFormat_FormatType = 3
                                    .CellFormat_FormatString = "{0:N0}"
                                    .Dimension = ""
                                End If






                                .UnboundColumnType = 0
                                .UnboundExpressionMode = 0
                                .UnboundExpression = "-"
                                .PivotSummaryDisplayType = 0
                                .GroupInterval = 0


                                .AREA = -1
                                .AREAINDEX = -1
                                .ORDERBY = 0

                            End With

                            _newfieldList.Add(_newfield)

                        End If
                    Next


                    If _newfieldList.Count > 0 Then
                        dc.tblDataSourceFile_Fields.InsertAllOnSubmit(_newfieldList)
                        dc.SubmitChanges()


                        Dim sqlQuery As String = "SELECT * FROM [dbo].[" & _GUID & "]"
                        HostingEnvironment.Cache.Remove(sqlQuery)
                        WebCacheWithSqlDependency.DbManager.GetBITable(_GUID)


                        e.Result = "success"

                    Else
                        e.Result = "No Fields Data."
                    End If
                End If

            End Using













        Else
            e.Result = "No Fields Data."
        End If





    End Sub


    Protected Sub gridFields_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles gridFields.InitNewRow


        'e.NewValues("BirthDate") = New DateTime(1970, 1, 10)
        'e.NewValues("Title") = "Sales Representative"


        'e.NewValues("FIELD_CAPTION") = _field
        'e.NewValues("FIELD_NAME") = _field
        'e.NewValues("DS_ID") = _data.ID

        e.NewValues("SummaryType") = 9
        e.NewValues("CellFormat_FormatType") = 3
        e.NewValues("CellFormat_FormatString") = "{0:N2}"

        e.NewValues("UnboundColumnType") = 2
        e.NewValues("UnboundExpressionMode") = 3
        'e.NewValues("UnboundExpression") = "-"
        e.NewValues("PivotSummaryDisplayType") = 0
        e.NewValues("GroupInterval") = 0


        'e.NewValues("Dimension") = "ME"

        e.NewValues("AREA") = -1
        e.NewValues("AREAINDEX") = -1
        e.NewValues("ORDERBY") = 0



    End Sub

    Protected Sub gridFields_CellEditorInitialize(ByVal sender As Object, ByVal e As ASPxGridViewEditorEventArgs) Handles gridFields.CellEditorInitialize
        If gridFields.IsNewRowEditing Then
            'If (e.Column.FieldName = "SummaryType") Then
            '    e.Editor.Enabled = False
            'End If
            'If (e.Column.FieldName = "PivotSummaryDisplayType") Then
            '    e.Editor.Enabled = False
            'End If
            'If (e.Column.FieldName = "UnboundColumnType") Then
            '    e.Editor.Enabled = False
            'End If
            'If (e.Column.FieldName = "CellFormat_FormatType") Then
            '    e.Editor.Enabled = False
            'End If
            'If (e.Column.FieldName = "GroupInterval") Then
            '    e.Editor.Enabled = False
            'End If
            'If (e.Column.FieldName = "UnboundExpressionMode") Then
            '    e.Editor.Enabled = False
            'End If
        End If
    End Sub

    Protected Sub gridFields_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles gridFields.RowInserting
        'Dim _GUID As String = Session("DSGUID")

        If String.IsNullOrEmpty(e.NewValues("UnboundExpression")) Then
            Throw New Exception("No UnboundExpression.")
        End If

        Using dc = New DataClasses_PortalBIExt




            Dim _data = (From c In dc.tblDataSourceFiles Where c.GUID.Equals(_GUID)).FirstOrDefault()



            If _data IsNot Nothing Then
                Dim _FIELD_CAPTION = e.NewValues("FIELD_CAPTION").ToString()

                Dim _chkField = (From c In dc.tblDataSourceFile_Fields Where c.FIELD_NAME = _FIELD_CAPTION And c.DS_ID = _data.ID).FirstOrDefault()

                If _chkField IsNot Nothing Then

                    Throw New Exception("Exists field : " & _FIELD_CAPTION)

                Else
                    Dim _newfield As New tblDataSourceFile_Field
                    With _newfield

                        .FIELD_CAPTION = _FIELD_CAPTION
                        .FIELD_NAME = _FIELD_CAPTION
                        .DS_ID = _data.ID

                        .SummaryType = 9
                        .CellFormat_FormatType = 3
                        .CellFormat_FormatString = e.NewValues("CellFormat_FormatString")

                        .UnboundColumnType = 2
                        .UnboundExpressionMode = 3
                        .UnboundExpression = e.NewValues("UnboundExpression")
                        .PivotSummaryDisplayType = 0
                        .GroupInterval = 0


                        .Dimension = e.NewValues("Dimension")

                        .AREA = -1
                        .AREAINDEX = -1
                        .ORDERBY = 0

                    End With

                    dc.tblDataSourceFile_Fields.InsertOnSubmit(_newfield)
                    dc.SubmitChanges()

                End If





            Else

                Throw New Exception("No DataSource selected.")
            End If





        End Using



        gridFields.CancelEdit()
        e.Cancel = True
    End Sub


    Protected Sub cbDeleteFields_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbDeleteFields.Callback

        Dim _FIELD_ID = e.Parameter.ToString()

        Using dc = New DataClasses_PortalBIExt
            dc.ExecuteCommand("delete from tblDataSourceFile_Field where DS_ID='" & _DSID & "' and FIELD_ID in(" & _FIELD_ID & ")")
        End Using

        e.Result = "success"


    End Sub

    Protected Sub cbClearCache_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbClearCache.Callback

        Dim sqlQuery As String = "SELECT * FROM [dbo].[" & _GUID & "]"
        HostingEnvironment.Cache.Remove(sqlQuery)
        WebCacheWithSqlDependency.DbManager.GetBITable(_GUID)

        e.Result = "success"


    End Sub

    'cbDeleteColumns

    'Protected Sub ridFields_RowDeleting(ByVal sender As Object, ByVal e As ASPxDataDeletingEventArgs) Handles gridFields.RowDeleting



    '    Dim _FIELD_ID = e.Keys("FIELD_ID").ToString


    '    gridFields.CancelEdit()
    '    e.Cancel = True

    'End Sub

End Class


