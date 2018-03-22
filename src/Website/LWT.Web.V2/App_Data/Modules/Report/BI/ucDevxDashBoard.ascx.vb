Imports Portal.Components
Imports DevExpress.Web.ASPxTreeList
Imports DevExpress.Web

Partial Class Modules_ucDevxDashBoard
    Inherits PortalModuleControl
    Public Function PreviewDB(ByVal RID As String, ByVal DB_ID As String) As String
        Dim sb As New StringBuilder
        If Not String.IsNullOrEmpty(RID) And Not String.IsNullOrEmpty(DB_ID) Then

            sb.AppendFormat("<a class=""dxeHyperlink_Office2010Blue"" href=""javascript:ShowDetailPopup('{0}');"" style=""font-weight: bold;"">View</a>", RID)

        End If
        Return sb.ToString()
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs)
        'If (Not IsPostBack) Then
        '    Dim values As Array = System.Enum.GetValues(GetType(TreeListEditMode))

        '    treeList.DataBind()
        '    treeList.ExpandToLevel(2)
        'End If
        'treeList.SettingsEditing.AllowNodeDragDrop = True
    End Sub

    Protected Sub treeList_HtmlDataCellPrepared(sender As Object, e As TreeListHtmlDataCellEventArgs) Handles treeList.HtmlDataCellPrepared
        If e.Column.Name = "View" Then

            'e.Cell.Attributes.Add("onclick", String.Format("ShowDetailPopup({0})", e.NodeKey))

            'Dim _RID = e.NodeKey
            'Using dc As New DataClasses_LWTReportsExt()
            '    Dim _data = (From c In dc.tblReports Where c.RID.Equals(_RID)).FirstOrDefault()
            '    If _data IsNot Nothing Then
            '        Dim b As New TreeListHyperLinkColumn()
            '        b.FieldName = "RID"
            '        b.PropertiesHyperLink.ImageUrl = "../../../../res/icon/report.png"
            '        b.PropertiesHyperLink.NavigateUrlFormatString = "javascript:ShowMasterBIPopup({0});"

            '        '<%--<dx:TreeListHyperLinkColumn FieldName="RID" Caption="Template" HeaderStyle-HorizontalAlign="Center">
            '        '    <PropertiesHyperLink ImageUrl="../../../../res/icon/report.png" NavigateUrlFormatString='javascript:<%# PreviewDB(Eval("RID").ToString(), Eval("DB_ID").ToString())%>;'>
            '        '    </PropertiesHyperLink>
            '        '    <EditFormSettings Visible="False" />
            '        '</dx:TreeListHyperLinkColumn>--%>
            '    End If
            'End Using



        End If
    End Sub


    Protected Sub cbPreview_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbPreview.Callback
        Dim RID = CInt(e.Parameter)
        Session("RID") = RID
        e.Result = RID
    End Sub



    'Protected Sub treeList_CellEditorInitialize(ByVal sender As Object, ByVal e As TreeListColumnEditorEventArgs) Handles treeList.CellEditorInitialize
    '    Session("VIEW_ID") = Nothing

    '    If (Not treeList.IsEditing) OrElse e.Column.FieldName <> "VIEW_ID" Then
    '        Return
    '    End If
    '    If e.Value Is DBNull.Value OrElse e.Value Is Nothing Then
    '        Return
    '    End If
    '    Dim val As Object = e.Value 'treeList.se GetRowValuesByKeyValue(e.NodeKey, "Country")
    '    If val Is DBNull.Value Then
    '        Return
    '    End If
    '    Dim VIEW_ID As String = CStr(val)

    '    'Dim combo As ASPxComboBox = TryCast(e.Editor, ASPxComboBox)
    '    'FillDBCombo(combo, VIEW_ID)

    '    'AddHandler combo.Callback, AddressOf cmbDB_OnCallback

    '    Session("VIEW_ID") = VIEW_ID
    'End Sub

    ''Protected Sub FillDBCombo(ByVal cmb As ASPxComboBox, ByVal _VIEW_ID As String)
    ''    If String.IsNullOrEmpty(_VIEW_ID) Then
    ''        Return
    ''    End If

    ''    Using dc As New DataClasses_LWTReportsExt()
    ''        Dim DBs = (From c In dc.tblReport_DashBoards Where c.VIEW_ID.Equals(_VIEW_ID)).ToList()


    ''        cmb.Items.Clear()
    ''        For Each item In DBs
    ''            cmb.Items.Add(item.DB_TITLE, item.DB_ID)
    ''        Next
    ''    End Using


    ''End Sub

    ''Private Sub cmbDB_OnCallback(ByVal source As Object, ByVal e As CallbackEventArgsBase)
    ''    FillDBCombo(TryCast(source, ASPxComboBox), e.Parameter)
    ''End Sub
End Class
