﻿Imports Portal.Components
Imports DevExpress.Web.ASPxTreeList
Imports DevExpress.Web
Imports System.Data.SqlClient
Imports System.Data
Imports DevExpress.Web.Data

Partial Class Modules_ucDevxBIUserTemplate
    Inherits PortalModuleControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        SqlDataSource_Data.SelectParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name
        SqlDataSource_Cubes.SelectParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name

        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(LWT.Website.webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If
        If IsPostBack = False Then
            RefreshToolbarButtonVisibility()
        End If

        pnMain.HeaderText = portalSettings.ActiveTab.TabName
        pnMain.HeaderImage.IconID = portalSettings.ActiveTab.IconID
    End Sub

    Protected Sub TreeList_BeforeGetCallbackResult(ByVal sender As Object, ByVal e As EventArgs) Handles treeList.BeforeGetCallbackResult
        RefreshToolbarButtonVisibility()
    End Sub

    Private Sub RefreshToolbarButtonVisibility()
        Dim displayUpdateCancelButtons As Boolean = treeList.EditingNodeKey IsNot Nothing OrElse treeList.IsNewNodeEditing
        For Each item As TreeListToolbarItem In treeList.Toolbars(0).Items
            If item.Template IsNot Nothing Then
                Continue For
            End If
            item.Visible = Not (displayUpdateCancelButtons Xor (item.Command = TreeListToolbarCommand.Update OrElse item.Command = TreeListToolbarCommand.Cancel))
        Next item
    End Sub
    Protected Sub cbPreview_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbPreview.Callback

        Dim _BUID = CInt(e.Parameter)
        Session("BUID") = _BUID
        e.Result = _BUID

    End Sub
     
   
    Protected Sub treeList_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As TreeListHtmlDataCellEventArgs) Handles treeList.HtmlDataCellPrepared
        If e.Column.FieldName = "BUID" Or e.Column.FieldName = "CUBE_ID" Then
            e.Cell.HorizontalAlign = HorizontalAlign.Left
            e.Column.CellStyle.HorizontalAlign = HorizontalAlign.Left
        End If

        If e.Column.FieldName = "BUID" Then
            Dim _BUID = e.NodeKey
            Using dc As New DataClasses_PortalBIExt()
                Dim _rpt = dc.tblBI_Users.Where(Function(x) x.BUID.Equals(_BUID)).FirstOrDefault()
                If _rpt.CUBE_ID Is Nothing Then
                    e.Cell.Text = _rpt.TITLE
                    e.Cell.Font.Bold = True

                End If
            End Using
        End If

    End Sub

    'Protected Sub treeList_NodeUpdating(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataUpdatingEventArgs) Handles treeList.NodeUpdating
    '    Dim _CUBE As String = e.NewValues("CUBE_ID")
    '    Using dc As New DataClasses_PortalBIExt
    '        If Not String.IsNullOrEmpty(_CUBE) Then
    '            Dim _cb = (From c In dc.tblCube_Users Where c.CUBE_ID.Equals(_CUBE.ToString())).FirstOrDefault()
    '            e.NewValues("DATABASE") = _cb.DATABASE



    '        Else
    '            e.NewValues("DATABASE") = Nothing
    '        End If
    '    End Using
    'End Sub
    Protected Sub treeList_CellEditorInitialize(ByVal sender As Object, ByVal e As TreeListColumnEditorEventArgs) Handles treeList.CellEditorInitialize

        If e.Column.FieldName.Equals("CUBE_ID") And Not treeList.IsNewNodeEditing Then

            e.Editor.ClientEnabled = False

        End If

    End Sub
    Protected Sub treeList_NodeInserting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles treeList.NodeInserting
        Dim _No As Integer = e.NewValues("No")
        Dim _TITLE As String = e.NewValues("TITLE").ToString()
        Dim _DESCRIPTION As String = e.NewValues("DESCRIPTION").ToString()
        Dim _ParentID As Integer = e.NewValues("ParentID")
        'Dim _DATABASE As String = e.NewValues("DATABASE")
        Dim _CUBE_ID As String = e.NewValues("CUBE_ID")


        Using dc As New DataClasses_PortalBIExt

            Dim _BIUser As New tblBI_User()
            With _BIUser
                .No = _No

                .TITLE = _TITLE
                .DESCRIPTION = _DESCRIPTION
                .DEPARTMENT = _DESCRIPTION
                .ParentID = _ParentID

                .CREATEDATE = Now
                .CREATEBY = HttpContext.Current.User.Identity.Name
                .USERNAME = HttpContext.Current.User.Identity.Name

                .CUBE_ID = _CUBE_ID
                'If Not String.IsNullOrEmpty(_CUBE) Then
                '    Dim _cb = (From c In dc.tblCube_Users Where c.CUBE.Equals(_CUBE.ToString())).FirstOrDefault()
                '    .DATABASE = _cb.DATABASE
                '    .CUBE = _CUBE
                'End If

                .ShowColumnGrandTotals = False
                .ShowColumnTotals = False
                .ShowRowGrandTotals = False
                .ShowRowTotals = False
                .ShowGrandTotalsForSingleValues = False
                .ShowTotalsForSingleValues = False



            End With



            dc.tblBI_Users.InsertOnSubmit(_BIUser)

            dc.SubmitChanges()


        End Using


        e.Cancel = True
        treeList.CancelEdit()
    End Sub
End Class
