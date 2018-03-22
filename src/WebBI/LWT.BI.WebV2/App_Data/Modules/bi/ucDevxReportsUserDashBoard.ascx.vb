Imports Portal.Components
Imports DevExpress.Web.ASPxTreeList
Imports DevExpress.Web
Imports System.Data.SqlClient
Imports System.Data
Imports DevExpress.Web.ASPxPivotGrid
'Imports Portal.Web.BI


Partial Class Modules_ucDevxReportsUserDashBoard
    Inherits PortalModuleControl

    Public Function PreviewDB(ByVal DB_ID As String) As String
        Dim sb As New StringBuilder
        If Not String.IsNullOrEmpty(DB_ID) Then

            sb.AppendFormat("<a class=""dxeHyperlink_Office2010Blue"" href=""javascript:ShowUserDBPopup('{0}');"" style=""font-weight: bold;"">View</a>", DB_ID)

        End If
        Return sb.ToString()
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If (Not IsPostBack) Then
            '    treeList.DataBind()
            '    Dim firstUnread As TreeListNode = treeList.FindNodeByFieldValue("IsNew", True)
            '    firstUnread.Focus()
            '    treeList.ExpandToLevel(2)
            '    'messageLiteral.Text = GetEntryText(firstUnread)

            Session("RID") = Nothing


            'pivotGrid.OptionsView.ShowColumnGrandTotals = True
            'pivotGrid.OptionsView.ShowColumnTotals = True
            'pivotGrid.OptionsView.ShowRowGrandTotals = True
            'pivotGrid.OptionsView.ShowRowTotals = True
            'pivotGrid.OptionsView.ShowGrandTotalsForSingleValues = True
            'pivotGrid.OptionsView.ShowTotalsForSingleValues = True

        End If



    End Sub

    Protected Sub cbPreview_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbPreview.Callback
        Dim DB_ID = CInt(e.Parameter)
        Session("DB_ID") = DB_ID
        e.Result = DB_ID
    End Sub

    Protected Sub SqlDataSource1_Selecting(ByVal sender As Object, ByVal e As SqlDataSourceSelectingEventArgs) Handles SqlDataSource_Data.Selecting
        'If gridCampaign.GridView.FocusedRowIndex > -1 Then

        '    Dim _CampaignID As String = gridCampaign.GridView.GetRowValues(gridCampaign.GridView.FocusedRowIndex, gridCampaign.KeyFieldName).ToString()

        '    e.Command.CommandText = String.Format("select * from V_Campaign_Commission where CampaignID={0} and IsActive=1", _CampaignID)


        'End If
        Dim _DB_ID As New List(Of Integer)

        Using dc As New DataClasses_PortalBIExt
            Dim _data = (From c In dc.tblDashBoard_Assignments Where c.UserName.Equals(HttpContext.Current.User.Identity.Name)).ToList()


            Dim _DashBoards = (From c In dc.tblDashBoards).ToList()


            For Each item In _data
                Dim _Child = (From c In _DashBoards Where c.DB_ID = item.DB_ID).FirstOrDefault()

                _DB_ID.Add(item.DB_ID)
                If _Child IsNot Nothing Then
                    If _Child.ParentID IsNot Nothing Then
                        Dim _Parent = (From c In _DashBoards Where c.DB_ID = _Child.ParentID).FirstOrDefault()

                        If _Parent IsNot Nothing Then
                            Recursive(_Parent.DB_ID, _DashBoards, _DB_ID)
                        End If
                    End If

                End If


            Next

            If _DB_ID.Count > 0 Then
                Dim sb As New StringBuilder()

                If _DB_ID.Count = 1 Then
                    sb.Append(_DB_ID(0).ToString)
                Else
                    For Each _ID In _DB_ID
                        If String.IsNullOrEmpty(sb.ToString()) Then
                            sb.Append(_ID.ToString)
                        Else
                            sb.Append("," & _ID.ToString)
                        End If

                    Next
                End If

                e.Command.CommandText = String.Format("select * from tblDashBoard where DB_ID in({0}) order by No", sb.ToString())

            Else
                e.Command.CommandText = "select * from tblDashBoard where DB_ID is null "

            End If


        End Using
    End Sub

    Sub Recursive(ByVal _ParentID As Integer, ByVal _DashBoards As List(Of tblDashBoard), ByRef _DB_ID As List(Of Integer))

        'If value >= 100 Then
        '    Return value
        'End If
        Dim _Parent = (From c In _DashBoards Where c.DB_ID = _ParentID).FirstOrDefault()
        If _Parent IsNot Nothing Then
            _DB_ID.Add(_ParentID)
            If _Parent.ParentID IsNot Nothing Then
                Recursive(_Parent.ParentID, _DashBoards, _DB_ID)
            End If
        End If
    End Sub
End Class
