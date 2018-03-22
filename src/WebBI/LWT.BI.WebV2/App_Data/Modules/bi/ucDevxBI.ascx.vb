Imports Portal.Components
Imports DevExpress.Web.ASPxTreeList
Imports DevExpress.Web
Imports System.Data.SqlClient
Imports System.Data
Imports DevExpress.Web.Data

Partial Class Modules_ucDevxBI
    Inherits PortalModuleControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load


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

        'Dim _BID = CInt(e.Parameter)
        'Session("BID") = _BID
        'e.Result = _BID

         
        Session("GUID") = e.Parameter
        e.Result = e.Parameter
    End Sub


    Public Function GetURL(ByVal _BID As String) As String
        Dim sb As New StringBuilder
        Using dc As New DataClasses_PortalBIExt()
            Dim Data = (From c In dc.tblBIs Where c.BID.Equals(_BID)).FirstOrDefault()
            If Data IsNot Nothing Then
                If Data.CUBE_ID IsNot Nothing Then
                    sb.AppendFormat("<a href=""javascript:void(0);"" onclick=""javascript:cbPreview.PerformCallback('{0}');"">{1}</a>", Data.GUID, Data.TITLE)
                Else
                    sb.Append("<b>" & Data.TITLE & "</b>")
                End If
            End If

        End Using
        Return sb.ToString()
    End Function


    Protected Sub treeList_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As TreeListHtmlDataCellEventArgs) Handles treeList.HtmlDataCellPrepared
        If e.Column.FieldName = "BID" Or e.Column.FieldName = "CUBE_ID" Then
            e.Cell.HorizontalAlign = HorizontalAlign.Left
            e.Column.CellStyle.HorizontalAlign = HorizontalAlign.Left
        End If

        If e.Column.FieldName = "BID" Then
            Dim _BID = e.NodeKey
            Using dc As New DataClasses_PortalBIExt()
                Dim _rpt = dc.tblBIs.Where(Function(x) x.BID.Equals(_BID)).FirstOrDefault()
                If _rpt.CUBE_ID Is Nothing Then
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

        If e.Column.FieldName.Equals("CUBE_ID") And Not treeList.IsNewNodeEditing Then

            e.Editor.ClientEnabled = False
 
        End If

    End Sub


    Protected Sub treeList_CommandColumnButtonInitialize(ByVal sender As Object, ByVal e As TreeListCommandColumnButtonEventArgs) Handles treeList.CommandColumnButtonInitialize

        If e.ButtonType = TreeListCommandColumnButtonType.Custom Then

            Dim dataitem = treeList.FindNodeByKeyValue(e.NodeKey).DataItem

            Dim CUBEID = DirectCast(dataitem, System.Data.DataRowView).Row("CUBE_ID")

            If CUBEID Is DBNull.Value Then
                e.Visible = DevExpress.Utils.DefaultBoolean.False
            End If

        End If

       

    End Sub

    Protected Sub treeList_NodeInserting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles treeList.NodeInserting
        Dim _No As Integer = e.NewValues("No")
        Dim _TITLE As String = e.NewValues("TITLE").ToString()
        Dim _DESCRIPTION As String = e.NewValues("DESCRIPTION").ToString()
        Dim _ParentID As Integer = e.NewValues("ParentID")
        Dim _CUBE_ID As String = e.NewValues("CUBE_ID")

        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(LWT.Website.webconfig._PortalContextName), PortalSettings)

        Using dc As New DataClasses_PortalBIExt

            Dim _BI As New tblBI()
            With _BI
                .No = _No

                .TITLE = _TITLE
                .DESCRIPTION = _DESCRIPTION
                .DEPARTMENT = _DESCRIPTION
                .ParentID = _ParentID

                .GUID = System.Guid.NewGuid().ToString()
                .PortalId = portalSettings.PortalId

                .CREATEDATE = Now
                .CREATEBY = HttpContext.Current.User.Identity.Name

                If Not String.IsNullOrEmpty(_CUBE_ID) Then
                    .CUBE_ID = _CUBE_ID
                End If

                .ShowColumnGrandTotals = False
                .ShowColumnTotals = False
                .ShowRowGrandTotals = False
                .ShowRowTotals = False
                .ShowGrandTotalsForSingleValues = False
                .ShowTotalsForSingleValues = False

                'If Not String.IsNullOrEmpty(_CUBE) Then
                '    Dim _cb = (From c In dc.tblCubes Where c.CUBE.Equals(_CUBE)).FirstOrDefault()
                '    .DATABASE = _cb.DATABASE
                'Else
                '    .DATABASE = Nothing
                'End If



                'If Not String.IsNullOrEmpty(_CUBE) Then

                '    Dim _cb = (From c In dc.tblCubes Where c.CUBE.Equals(_CUBE)).FirstOrDefault()
                '    If _cb IsNot Nothing Then
                '        .DATABASE = _cb.DATABASE

                '        Dim _attbs = (From c In dc.V_Attributes Where c.DATABASE.Equals(_cb.DATABASE) And c.CUBE.Equals(_cb.CUBE)).ToList()
                '        If _attbs.Count > 0 Then
                '            Dim i As Integer = 0
                '            For Each item In _attbs
                '                i += 1
                '                .tblBIAttributes.Add(New tblBIAttribute With {.FIELD = item.FIELD, .AREA = -1, .ORDERBY = i})
                '            Next
                '        End If
                '    End If
                'End If

            End With



            dc.tblBIs.InsertOnSubmit(_BI)

            dc.SubmitChanges()


        End Using


        e.Cancel = True
        treeList.CancelEdit()
    End Sub


    Protected Sub cbCloneReport_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbCloneReport.Callback

        Dim _BID = CInt(e.Parameter)
 
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(LWT.Website.webconfig._PortalContextName), PortalSettings)

        Using dc As New DataClasses_PortalBIExt

            Dim Data = (From c In dc.tblBIs Where c.BID.Equals(_BID)).FirstOrDefault()


            '1. tblBI
            Dim _BI As New tblBI()
            With _BI
                .No = Data.No

                .TITLE = Data.TITLE & " (Copy)"

                .DESCRIPTION = Data.DESCRIPTION
                .DEPARTMENT = Data.DEPARTMENT
                .ParentID = Data.ParentID

                .GUID = System.Guid.NewGuid().ToString()
                .PortalId = portalSettings.PortalId

                .CREATEDATE = Now
                .CREATEBY = HttpContext.Current.User.Identity.Name

                .CUBE_ID = Data.CUBE_ID

                .ShowColumnGrandTotals = Data.ShowColumnGrandTotals
                .ShowColumnTotals = Data.ShowColumnTotals
                .ShowRowGrandTotals = Data.ShowRowGrandTotals
                .ShowRowTotals = Data.ShowRowTotals
                .ShowGrandTotalsForSingleValues = Data.ShowGrandTotalsForSingleValues
                .ShowTotalsForSingleValues = Data.ShowTotalsForSingleValues
            End With
            dc.tblBIs.InsertOnSubmit(_BI)
            dc.SubmitChanges()


            Dim sb As New StringBuilder()

            '2. tblBIAttribute
            sb.AppendLine("INSERT INTO tblBIAttribute(FIELD,BID,AREA,ORDERBY,FIELDTYPE,AREAINDEX,SortBySummaryInfo) ")
            sb.AppendLine("select FIELD," & _BI.BID.ToString() & ",AREA,ORDERBY,FIELDTYPE,AREAINDEX,SortBySummaryInfo from  tblBIAttribute where BID=" & _BID.ToString() & ";")

            '3. tblBIFilter
            sb.AppendLine("INSERT INTO tblBIFilter(FIELD,BID,FILTER_VALUE,FILTER_TYPE) ")
            sb.AppendLine("select FIELD," & _BI.BID.ToString() & ",FILTER_VALUE,FILTER_TYPE from  tblBIFilter where BID=" & _BID.ToString() & ";")


            '4. tblBIAssignment
            sb.AppendLine("INSERT INTO tblBIAssignment(BID,USERNAME) VALUES(" & _BI.BID.ToString() & ",'" & _BI.CREATEBY & "'); ")



            dc.ExecuteCommand(sb.ToString())


        End Using


        e.Result = "success"

    End Sub


End Class
