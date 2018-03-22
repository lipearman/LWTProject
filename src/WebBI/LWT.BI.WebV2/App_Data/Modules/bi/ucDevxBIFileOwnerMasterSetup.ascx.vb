Imports Portal.Components
Imports DevExpress.Web.ASPxTreeList
Imports DevExpress.Web
Imports System.Data.SqlClient
Imports System.Data
Imports DevExpress.Web.Data

Partial Class Modules_ucDevxBIFileOwnerMasterSetup
    Inherits PortalModuleControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load


        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(LWT.Website.webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If
        pnMain.HeaderImage.IconID = portalSettings.ActiveTab.IconID
        pnMain.HeaderText = portalSettings.ActiveTab.TabName
        If IsPostBack = False Then
            'RefreshToolbarButtonVisibility()
        End If
        SqlDataSource_Data.SelectParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name
        SqlDataSource_Data.SelectParameters("PortalId").DefaultValue = portalSettings.PortalId

        SqlDataSource_DataSourceFile.SelectParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name
        SqlDataSource_DataSourceFile.SelectParameters("PortalId").DefaultValue = portalSettings.PortalId

        treeList.ExpandAll()

    End Sub
    Public Function GetURL(ByVal _BID As String) As String
        Dim sb As New StringBuilder
        Using dc As New DataClasses_PortalBIExt()
            Dim Data = (From c In dc.tblDataSourceBIs Where c.BID.Equals(_BID)).FirstOrDefault()
            If Data IsNot Nothing Then
                If Data.DS_ID IsNot Nothing Then
                    sb.AppendFormat("<a href=""javascript:void(0);"" onclick=""javascript:cbPreview.PerformCallback('{0}');"">{1}</a>", Data.GUID, Data.TITLE)
                Else
                    sb.Append("<b>" & Data.TITLE & "</b>")
                End If
            End If

        End Using
        Return sb.ToString()
    End Function

    Protected Sub cbPreview_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbPreview.Callback

        Dim _BID = e.Parameter.ToString()

        Using dc As New DataClasses_PortalBIExt()
            Dim Data = (From c In dc.tblDataSourceBIs Where c.BID.Equals(_BID)).FirstOrDefault()
            
            Session("GUID") = Data.GUID
            e.Result = Data.GUID
        End Using

        'Session("BID") = _BID
        'e.Result = _BID

        'Session("GUID") = e.Parameter.ToString()
        'e.Result = e.Parameter.ToString()
    End Sub

    Protected Sub treeList_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As TreeListHtmlDataCellEventArgs) Handles treeList.HtmlDataCellPrepared
        If e.Column.FieldName = "BID" Or e.Column.FieldName = "DS_ID" Then
            e.Cell.HorizontalAlign = HorizontalAlign.Left
            e.Column.CellStyle.HorizontalAlign = HorizontalAlign.Left
        End If

        If e.Column.FieldName = "BID" Then
            Dim _BID = e.NodeKey
            Using dc As New DataClasses_PortalBIExt()
                Dim _rpt = dc.tblDataSourceBIs.Where(Function(x) x.BID.Equals(_BID)).FirstOrDefault()
                If _rpt.DS_ID Is Nothing Then
                    e.Cell.Text = _rpt.TITLE
                    e.Cell.Font.Bold = True

                End If
            End Using
        End If

    End Sub

    'Protected Sub treeList_NodeUpdating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles treeList.NodeUpdating
    '    Dim _CUBE_ID As String = e.NewValues("CUBE_ID")
    '    Using dc As New DataClasses_PortalBIExt
    '        If Not String.IsNullOrEmpty(_CUBE_ID) Then
    '            Dim _cb = (From c In dc.tblCubes Where c.CUBE_ID.Equals(_CUBE_ID.ToString())).FirstOrDefault()
    '            e.NewValues("DATABASE") = _cb.DATABASE
    '        Else
    '            e.NewValues("DATABASE") = Nothing
    '        End If
    '    End Using
    'End Sub

    Protected Sub treeList_CellEditorInitialize(ByVal sender As Object, ByVal e As TreeListColumnEditorEventArgs) Handles treeList.CellEditorInitialize

        If e.Column.FieldName.Equals("DS_ID") And Not treeList.IsNewNodeEditing Then
            e.Editor.ClientEnabled = False
        End If
        'If e.Column.FieldName.Equals("DS_ID") And treeList.NewNodeParentKey Is Nothing Then
        '    e.Editor.Visible = False
        'End If
    End Sub

    'Protected Sub treeList_NodeInserting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles treeList.NodeInserting
    '    Dim _No As Integer = e.NewValues("No")
    '    Dim _TITLE As String = e.NewValues("TITLE").ToString()
    '    Dim _DESCRIPTION As String = e.NewValues("DESCRIPTION").ToString()
    '    Dim _ParentID As Integer = e.NewValues("ParentID")
    '    Dim _DS_ID As String = e.NewValues("DS_ID")


    '    Using dc As New DataClasses_PortalBIExt

    '        Dim _BI As New tblDataSourceBI
    '        With _BI
    '            .No = _No

    '            .TITLE = _TITLE
    '            .DESCRIPTION = _DESCRIPTION
    '            .DEPARTMENT = _DESCRIPTION
    '            .ParentID = _ParentID


    '            .CREATEDATE = Now
    '            .CREATEBY = HttpContext.Current.User.Identity.Name

    '            If Not String.IsNullOrEmpty(_DS_ID) Then
    '                .DS_ID = _DS_ID
    '            End If


    '        End With



    '        dc.tblDataSourceBIs.InsertOnSubmit(_BI)

    '        dc.SubmitChanges()


    '    End Using


    '    e.Cancel = True
    '    treeList.CancelEdit()
    'End Sub



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



    'Protected Sub popupDetails_WindowCallback(ByVal source As Object, ByVal e As PopupWindowCallbackArgs) Handles popupDetails.WindowCallback
    '    Dim _DSGUID As String = e.Parameter.ToString()



    'End Sub

    Protected Sub cbSaveNewData_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbSaveNewData.Callback

        'Dim _GUID = Session("DSGUID")
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(LWT.Website.webconfig._PortalContextName), PortalSettings)

        Using dc As New DataClasses_PortalBIExt

            Dim newData As New tblDataSourceBI
            With newData
                .No = CInt(newNo.Value)
                .TITLE = newTITLE.Text
                .DESCRIPTION = newDESCRIPTION.Text
                .ParentID = 0
                .Owner = HttpContext.Current.User.Identity.Name
                .CREATEDATE = Now
                .CREATEBY = HttpContext.Current.User.Identity.Name

                .GUID = System.Guid.NewGuid().ToString()
                .PortalId = portalSettings.PortalId

                .ShowColumnGrandTotals = False
                .ShowColumnTotals = False
                .ShowRowGrandTotals = False
                .ShowRowTotals = False
                .ShowGrandTotalsForSingleValues = False
                .ShowTotalsForSingleValues = False
            End With

            dc.tblDataSourceBIs.InsertOnSubmit(newData)
            dc.SubmitChanges()

        End Using
        treeList.DataBind()

        e.Result = "success"
    End Sub



    'Protected Sub cbDeleteData_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbDeleteData.Callback

    '    Dim _BID = e.Parameter

    '    Using dc As New DataClasses_PortalBIExt

    '        Dim sb As New StringBuilder()

    '        sb.Append("delete tblDataSourceBI_Field_Filter where BID=" & _BID & ";")
    '        sb.Append("delete tblDataSourceBI_Field where BID=" & _BID & ";")
    '        sb.Append("delete tblDataSourceBI where BID=" & _BID & ";")

    '        dc.ExecuteCommand(sb.ToString())

    '    End Using
    '    treeList.DataBind()

    '    e.Result = "success"
    'End Sub





    Protected Sub cbSaveNewNode_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbSaveNewNode.Callback

        'Dim _GUID = Session("DSGUID")
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(LWT.Website.webconfig._PortalContextName), PortalSettings)

        Using dc As New DataClasses_PortalBIExt

            Dim _BI As New tblDataSourceBI
            With _BI
                .No = CInt(newNodeNo.Value)
                .TITLE = newNodeTitle.Text
                .DESCRIPTION = newNodeDescription.Text
                '.DEPARTMENT = _DESCRIPTION
                .ParentID = treeList.FocusedNode.Key
                .Owner = HttpContext.Current.User.Identity.Name
                .CREATEDATE = Now
                .CREATEBY = HttpContext.Current.User.Identity.Name

                .GUID = System.Guid.NewGuid().ToString()
                .PortalId = portalSettings.PortalId

                .ShowColumnGrandTotals = False
                .ShowColumnTotals = False
                .ShowRowGrandTotals = False
                .ShowRowTotals = False
                .ShowGrandTotalsForSingleValues = False
                .ShowTotalsForSingleValues = False





                If Not String.IsNullOrEmpty(newNodeDataSource.SelectedItem.Value) Then
                    .DS_ID = CInt(newNodeDataSource.SelectedItem.Value)
                End If


            End With

            dc.tblDataSourceBIs.InsertOnSubmit(_BI)

            dc.SubmitChanges()


        End Using

        e.Result = "success"
    End Sub







End Class
