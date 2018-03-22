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

Partial Class Modules_ucDevxFileLWTDataSourceManager
    Inherits PortalModuleControl

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
            Session("DS_ID") = Nothing
            Session("DSGUID") = Nothing
        End If

        'If gridRawData.IsCallback Then
        'gridRawData.Columns.Clear()
        'gridRawData.AutoGenerateColumns = True
        'gridRawData.DataBind()
        'End If

        'gridRawData.DataSourceID = Nothing
        'gridRawData.DataSource = WebCacheWithSqlDependency.DbManager.GetBITable(Session("DSGUID"))

        'Dim keys = New List(Of String)
        'Dim enumerator = HostingEnvironment.Cache.GetEnumerator()
        'While (enumerator.MoveNext())
        '    keys.Add(enumerator.Key.ToString())
        'End While
        'For Each item In keys
        '    HostingEnvironment.Cache.Remove(item)
        'Next


        SqlDataSource_gridData.SelectParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name

    End Sub

    Protected Sub frmImport_Upload_FileUploadComplete(ByVal sender As Object, ByVal e As DevExpress.Web.FileUploadCompleteEventArgs) Handles frmImport_UploadFile.FileUploadComplete

        If frmImport_UploadFile.Page IsNot Nothing Then

            Dim _GUID As String = System.Guid.NewGuid().ToString()
            Session("DSGUID") = _GUID

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
                dm.ImportDataToServer(FileName, conn1, InsertedTableName)

                'dm.RetrieveFields(conn1, _GUID)

                'Using dc As New DataClasses_PortalBIExt
                '    Dim _data = (From c In dc.tblDataSourceFiles Where c.GUID.Equals(_GUID)).FirstOrDefault()
                '    With _data
                '        .MODIFYBY = HttpContext.Current.User.Identity.Name
                '        .MODIFYDATE = Now
                '    End With
                '    dc.SubmitChanges()
                'End Using

                e.CallbackData = "success"

            Catch ex As Exception
                e.CallbackData = ex.Message
            End Try



        End If
    End Sub

    Protected Sub cbSaveNewData_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbSaveNewData.Callback

        Dim _GUID = Session("DSGUID")


        Using dc As New DataClasses_PortalBIExt

            Dim newData As New tblDataSourceFile
            With newData
                .Title = newTitle.Text
                .DESCRIPTION = newDescription.Text
                .ASATDATE = newASAT.Value
                .GUID = _GUID

                .Owner = HttpContext.Current.User.Identity.Name
                .CREATEBY = HttpContext.Current.User.Identity.Name
                .CREATEDATE = Now
            End With

            dc.tblDataSourceFiles.InsertOnSubmit(newData)
            dc.SubmitChanges()



            Dim InsertedTableName = _GUID
            Dim dm As New Datamanage
            Dim conn1 = ConfigurationManager.ConnectionStrings("PortalBIRawDataConnection").ConnectionString
            dm.RetrieveFields(conn1, _GUID)

        End Using


        e.Result = "success"
    End Sub

    Protected Sub popupDetails_WindowCallback(ByVal source As Object, ByVal e As PopupWindowCallbackArgs) Handles popupDetails.WindowCallback
        Dim _DSGUID As String = e.Parameter.ToString()
        Session("DSGUID") = _DSGUID

        gridRawData.Columns.Clear()
        gridRawData.AutoGenerateColumns = True
        gridRawData.DataBind()
        gridFields.DataBind()


        Using dc As New DataClasses_PortalBIExt
            Dim _data = (From c In dc.tblDataSourceFiles Where c.GUID.Equals(_DSGUID)).FirstOrDefault()

            Session("DS_ID") = _data.ID

            editTitle.Text = _data.Title
            editDescription.Text = _data.DESCRIPTION
            'editASAP.Value = _data.ASATDATE





        End Using



    End Sub
    Protected Sub gridRawData_Init(sender As Object, e As EventArgs) Handles gridRawData.Init


        'gridRawData.Columns.Clear()
        'gridRawData.AutoGenerateColumns = True
        gridRawData.DataBind()

    End Sub

    Protected Sub cbUpdateData_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbUpdateData.Callback

        Dim _GUID = Session("DSGUID")


        Using dc As New DataClasses_PortalBIExt
            Dim _data = (From c In dc.tblDataSourceFiles Where c.GUID.Equals(_GUID)).FirstOrDefault()


            With _data

                .Title = editTitle.Text

                .DESCRIPTION = editDescription.Text
                '.ASATDATE = editASAP.Value



                .MODIFYBY = HttpContext.Current.User.Identity.Name
                .MODIFYDATE = Now
            End With


            dc.SubmitChanges()
        End Using


        e.Result = "saved"
    End Sub


    Protected Sub cbRetrieve_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbRetrieveFields.Callback

        Dim _GUID = Session("DSGUID")
        Dim _DS_ID = Session("DS_ID")

        Dim InsertedTableName = _GUID
        Dim dm As New Datamanage
        Dim conn1 = ConfigurationManager.ConnectionStrings("PortalBIRawDataConnection").ConnectionString
        Try
            dm.RetrieveFields(conn1, _GUID)





            e.Result = "success"

        Catch ex As Exception
            e.Result = ex.Message
        End Try



    End Sub




    Protected Sub cbDeleteData_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbDeleteData.Callback

        Dim _GUID = Session("DSGUID")
        Dim _DS_ID = Session("DS_ID")

        Dim InsertedTableName = _GUID
        Dim dm As New Datamanage
        Dim conn1 = ConfigurationManager.ConnectionStrings("PortalBIConnection").ConnectionString
        Try
            dm.DeleteData(conn1, _DS_ID, _GUID)

            HostingEnvironment.Cache.Remove(_GUID)

            e.Result = "success"

            Session("DSGUID") = Nothing
            Session("DS_ID") = Nothing



        Catch ex As Exception
            e.Result = ex.Message
        End Try



    End Sub




    'Protected Sub Event_HtmlRowCreated(sender As Object, e As ASPxGridViewTableRowEventArgs) Handles gridRawData.HtmlRowCreated

    '    If e.RowType = GridViewRowType.Header Then
    '        'Dim xRH As Integer = (px_TableAreaH - 60) / grCRMxGrid.SettingsPager.PageSize
    '        e.Row.Width = Unit.Pixel(300)
    '    End If


    'End Sub





    Protected Sub frmImport_UpdateFile_FileUploadComplete(ByVal sender As Object, ByVal e As DevExpress.Web.FileUploadCompleteEventArgs) Handles frmImport_UpdateFile.FileUploadComplete

        If frmImport_UpdateFile.Page IsNot Nothing Then

            Dim _GUID As String = Session("DSGUID")


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

            Dim _GUID As String = Session("DSGUID")


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




    'Protected Sub SqlDataSource_RawData_Selecting(sender As Object, e As SqlDataSourceSelectingEventArgs) Handles SqlDataSource_RawData.Selecting
    '    If Session("DSGUID") IsNot Nothing Then
    '        e.Command.CommandText = "select * from [" & Session("DSGUID") & "]"
    '    Else
    '        e.Command.CommandText = "select * from [_Blank]"
    '    End If
    'End Sub

    'Protected Sub ObjectDataSource1_Selecting(sender As Object, e As ObjectDataSourceSelectingEventArgs) Handles SqlDataSource_RawData.Selecting
    '    gridRawData.Columns.Clear()


    '    If Session("DSGUID") IsNot Nothing Then
    '        e.InputParameters("tableName") = Session("DSGUID")
    '    Else
    '        e.InputParameters("tableName") = "_Blank"
    '    End If
    'End Sub

    Protected Sub cbAddColumns_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbAddColumns.Callback

        Dim _columns = e.Parameter.ToString()
        Dim _GUID As String = Session("DSGUID")

        If Not String.IsNullOrEmpty(_columns) Then



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

                                .SummaryType = 0
                                .CellFormat_FormatType = 3
                                .CellFormat_FormatString = "{0:N0}"

                                .UnboundColumnType = 2
                                .UnboundExpressionMode = 0
                                .UnboundExpression = ""
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
        'e.NewValues("UnboundExpression") = ""
        e.NewValues("PivotSummaryDisplayType") = 0
        e.NewValues("GroupInterval") = 0

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
        Dim _GUID As String = Session("DSGUID")

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
        Dim _DS_ID = Session("DS_ID")
        Using dc = New DataClasses_PortalBIExt
            dc.ExecuteCommand("delete from tblDataSourceFile_Field where DS_ID='" & _DS_ID & "' and FIELD_ID in(" & _FIELD_ID & ")")
        End Using

        e.Result = "success"


    End Sub
End Class


