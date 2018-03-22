Imports Portal.Components
Imports DevExpress.Web.ASPxTreeList
Imports DevExpress.Web
Imports System.Data.SqlClient
Imports System.Data
Imports DevExpress.Web.Data
Imports System.IO

Partial Class Modules_ucDevxDashBoard
    Inherits PortalModuleControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load


        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(LWT.Website.webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If
        'If IsPostBack = False Then
        '    RefreshToolbarButtonVisibility()
        'End If
        SqlDataSource_Data.SelectParameters("PortalId").DefaultValue = portalSettings.PortalId


        pnMain.HeaderText = portalSettings.ActiveTab.TabName
        pnMain.HeaderImage.IconID = portalSettings.ActiveTab.IconID
    End Sub

    'Protected Sub TreeList_BeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles treeList.BeforeGetCallbackResult
    '    RefreshToolbarButtonVisibility()
    'End Sub

    'Private Sub RefreshToolbarButtonVisibility()
    '    Dim displayUpdateCancelButtons As Boolean = treeList.EditingNodeKey IsNot Nothing OrElse treeList.IsNewNodeEditing
    '    For Each item As TreeListToolbarItem In treeList.Toolbars(0).Items
    '        If item.Template IsNot Nothing Then
    '            Continue For
    '        End If
    '        item.Visible = Not (displayUpdateCancelButtons Xor (item.Command = TreeListToolbarCommand.Update OrElse item.Command = TreeListToolbarCommand.Cancel))
    '    Next item
    'End Sub

    Public Function GetURL(ByVal DB_ID As String) As String
        Dim sb As New StringBuilder
        Using dc As New DataClasses_PortalBIExt()
            Dim Data = (From c In dc.tblDashBoards Where c.DB_ID.Equals(DB_ID)).FirstOrDefault()
            If Data IsNot Nothing Then
                  sb.AppendFormat("<a href=""javascript:void(0);"" onclick=""javascript:cbPreview.PerformCallback('{0}');"">{1}</a>", Data.GUID, Data.TITLE)

            End If

        End Using
        Return sb.ToString()
    End Function

    Protected Sub cbPreview_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbPreview.Callback

        'Dim _DB_ID = CInt(e.Parameter)

        'Using dc As New DataClasses_PortalBIExt()
        '    Dim _data = (From c In dc.tblDashBoards Where c.DB_ID = _DB_ID).FirstOrDefault()
        '    Session("GUID") = _data.GUID
        '    e.Result = _data.GUID

        '    'Dim _data = (From c In dc.V_Dashboard_Datas Where c.ParentID = _DB_ID).ToList()

        '    'If _data.Count > 0 Then
        '    '    'Dim _DB = (From c In dc.tblDashBoard_Datas Where c.DB_GUID = _data.GUID).Count
        '    '    Session("GUID") = _data(0).DB_GUID
        '    '    e.Result = _data(0).DB_GUID

        '    'Else
        '    '    e.Result = ""
        '    'End If

        'End Using


        Session("GUID") = e.Parameter
        e.Result = e.Parameter
     

    End Sub

    Protected Sub treeList_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As TreeListHtmlDataCellEventArgs) Handles treeList.HtmlDataCellPrepared

        If e.Level > 1 Then

            If e.Column.FieldName = "DB_ID" Then
                Dim _DB_ID = e.NodeKey
                Using dc As New DataClasses_PortalBIExt()
                    Dim _rpt = dc.tblDashBoards.Where(Function(x) x.DB_ID.Equals(_DB_ID)).FirstOrDefault()
                    e.Cell.Text = _rpt.TITLE
                    e.Cell.Font.Bold = True
                    e.Cell.HorizontalAlign = HorizontalAlign.Left
                    e.Column.CellStyle.HorizontalAlign = HorizontalAlign.Left
                End Using
            End If


        Else
            If e.Column.FieldName = "DB_ID" Then
                e.Cell.Font.Bold = True
                e.Cell.HorizontalAlign = HorizontalAlign.Left
                e.Column.CellStyle.HorizontalAlign = HorizontalAlign.Left
            End If


        End If



        If e.Column.FieldName = "CUBE_ID" Then
            e.Cell.HorizontalAlign = HorizontalAlign.Left
            e.Column.CellStyle.HorizontalAlign = HorizontalAlign.Left
        End If


    End Sub

    Protected Sub treeList_CommandColumnButtonInitialize(sender As Object, e As TreeListCommandColumnButtonEventArgs) Handles treeList.CommandColumnButtonInitialize
        If e.NodeKey Is Nothing Then
            Return
        End If


        Dim tl As ASPxTreeList = TryCast(sender, ASPxTreeList)
        Dim node As TreeListNode = tl.FindNodeByKeyValue(e.NodeKey)
        If node.Level = 1 Then
            If (e.ButtonType = TreeListCommandColumnButtonType.Edit) Then
                e.Visible = DevExpress.Utils.DefaultBoolean.True
            End If
        ElseIf e.ButtonType = TreeListCommandColumnButtonType.[New] Then
            e.Visible = DevExpress.Utils.DefaultBoolean.False
        End If
    End Sub



    'Protected Sub treeList_CellEditorInitialize(ByVal sender As Object, ByVal e As TreeListColumnEditorEventArgs) Handles treeList.CellEditorInitialize

    '    If e.Column.FieldName.Equals("CUBE_ID") And Not treeList.IsNewNodeEditing Then

    '        e.Editor.Enabled = False


    '    End If

    'End Sub

    Protected Sub treeList_NodeInserting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles treeList.NodeInserting
        Dim _No As Integer = e.NewValues("No")
        Dim _TITLE As String = e.NewValues("TITLE").ToString()
        Dim _DESCRIPTION As String = e.NewValues("DESCRIPTION").ToString()
        Dim _ParentID As Integer = e.NewValues("ParentID")
        Dim _CUBE_ID As String = e.NewValues("CUBE_ID")


        Using dc As New DataClasses_PortalBIExt

            Dim _DB As New tblDashBoard()
            With _DB
                .No = _No

                .TITLE = _TITLE
                .DESCRIPTION = _DESCRIPTION
                .DEPARTMENT = _DESCRIPTION
                .ParentID = _ParentID

                .PortalId = LWT.Website.webconfig._PortalID

                .GUID = Guid.NewGuid().ToString()

                .CREATEDATE = Now
                .CREATEBY = HttpContext.Current.User.Identity.Name

                'Dim cube_id = (From c In dc.tblDashBoards Where c.DB_ID = _ParentID).FirstOrDefault()

                '.CUBE_ID = cube_id.CUBE_ID

                .CUBE_ID = CInt(_CUBE_ID)
            End With



            dc.tblDashBoards.InsertOnSubmit(_DB)

            dc.SubmitChanges()





            'Dim _DB_Data As New tblDashBoard_Data()
            'With _DB_Data

            '    .Caption = _DB.TITLE
            '    .DB_ID = _DB.DB_ID

            '    Dim _parent = (From c In dc.tblDashBoards Where c.DB_ID = _DB.ParentID).FirstOrDefault()
            '    .DB_GUID = _parent.GUID

            '    Dim _cube = (From c In dc.V_Cubes Where c.CUBE_ID = _DB.CUBE_ID).FirstOrDefault()

            '    'Dim OLAPProvider As String = "http://172.16.40.234/OLAP/msmdpump.dll"

            '    Dim stream As New MemoryStream()
            '    Dim tr As New StringReader("<?xml version=""1.0"" encoding=""utf-8""?> <Dashboard><Title Text=""" & _DB.TITLE & """ /><DataSources><OLAPDataSource ComponentName=""dashboardOlapDataSource1"" Name=""" & _cube.DATABASE & """ ConnectionName=""local__"" ConnectionString=""provider=msolap;" & _cube.CONNECTING & """ /></DataSources></Dashboard>")


            '    'Dim tr As New StringReader("<?xml version=""1.0"" encoding=""utf-8""?><Dashboard><Title Text=""New Dashboard"" /><DataSources><OLAPDataSource ComponentName=""DataSource1"" ConnectionName=""ProductionReportConnectionString"" /></DataSources></Dashboard>")

            '    Dim doc = XDocument.Load(tr)
            '    doc.Save(stream)
            '    stream.Position = 0
            '    .Dashboard = stream.ToArray()
            'End With

            'dc.tblDashBoard_Datas.InsertOnSubmit(_DB_Data)

            'dc.SubmitChanges()


        End Using


        e.Cancel = True
        treeList.CancelEdit()
    End Sub





    Protected Sub cbSaveNewData_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbSaveNewData.Callback

        Using dc As New DataClasses_PortalBIExt

            Dim newData As New tblDashBoard
            With newData
                .No = CInt(newNo.Value)
                .TITLE = newTITLE.Text
                .DESCRIPTION = newDESCRIPTION.Text
                .GUID = Guid.NewGuid().ToString()
                .PortalId = LWT.Website.webconfig._PortalID
                .ParentID = 0
                .CREATEDATE = Now
                .CREATEBY = HttpContext.Current.User.Identity.Name

                '.CUBE_ID = CInt(newDataSource.SelectedItem.Value)
            End With

            dc.tblDashBoards.InsertOnSubmit(newData)
            dc.SubmitChanges()

        End Using
        'treeList.DataBind()

        e.Result = "success"
    End Sub







    'Protected Sub cbSaveNewNode_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbSaveNewNode.Callback

    '    Using dc As New DataClasses_PortalBIExt

    '        Dim _DB As New tblDashBoard
    '        With _DB
    '            .No = CInt(newNodeNo.Value)
    '            .TITLE = newNodeTitle.Text
    '            .DESCRIPTION = newNodeDescription.Text
    '            '.DEPARTMENT = _DESCRIPTION
    '            .ParentID = treeList.FocusedNode.Key
    '            .CREATEDATE = Now
    '            .CREATEBY = HttpContext.Current.User.Identity.Name

    '            .GUID = Guid.NewGuid().ToString()

    '            If Not String.IsNullOrEmpty(newNodeDataSource.SelectedItem.Value) Then
    '                .CUBE_ID = CInt(newNodeDataSource.SelectedItem.Value)
    '            End If

    '        End With

    '        dc.tblDashBoards.InsertOnSubmit(_DB)

    '        dc.SubmitChanges()


    '    End Using

    '    e.Result = "success"
    'End Sub




End Class