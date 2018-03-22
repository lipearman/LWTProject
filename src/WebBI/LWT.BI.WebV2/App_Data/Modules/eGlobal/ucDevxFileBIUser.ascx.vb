Imports Portal.Components
Imports DevExpress.Web.ASPxTreeList
Imports DevExpress.Web
Imports System.Data.SqlClient
Imports System.Data
Imports DevExpress.Web.Data

Partial Class Modules_ucDevxFileBIUser
    Inherits PortalModuleControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        SqlDataSource_Data.SelectParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(LWT.Website.webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If

        pnMain.HeaderText = portalSettings.ActiveTab.TabName
        pnMain.HeaderImage.IconID = portalSettings.ActiveTab.IconID
    End Sub

    Protected Sub cbPreview_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbPreview.Callback
        Dim _BID = e.Parameter
        Session("BID") = _BID
        e.Result = _BID
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


                Else
                    'Dim _ds = (From c In dc.tblDataSourceFiles Where c.ID = _rpt.DS_ID).FirstOrDefault()
                    'e.Cell.Text = "<a class=""dxeHyperlink_Office2010Blue"" href=""javascript:ShowMasterBIPopup('" & _ds.BID & "');"">" & _rpt.TITLE & "</a>"
                End If
            End Using
        End If

    End Sub


    Protected Sub SqlDataSource1_Selecting(ByVal sender As Object, ByVal e As SqlDataSourceSelectingEventArgs) Handles SqlDataSource_Data.Selecting
        'If gridCampaign.GridView.FocusedRowIndex > -1 Then

        '    Dim _CampaignID As String = gridCampaign.GridView.GetRowValues(gridCampaign.GridView.FocusedRowIndex, gridCampaign.KeyFieldName).ToString()

        '    e.Command.CommandText = String.Format("select * from V_Campaign_Commission where CampaignID={0} and IsActive=1", _CampaignID)


        'End If
        Dim _BID As New List(Of Integer)

        Using dc As New DataClasses_PortalBIExt()
            Dim _data = (From c In dc.tblDataSourceBI_Assignments Where c.USERNAME.Equals(HttpContext.Current.User.Identity.Name) Order By c.BID).ToList()


            Dim _Reports = (From c In dc.tblDataSourceBIs Order By c.BID, c.No).ToList()


            For Each item In _data
                Dim _Child = (From c In _Reports Where c.BID = item.BID).FirstOrDefault()

                _BID.Add(item.BID)
                If _Child IsNot Nothing Then
                    If _Child.ParentID IsNot Nothing Then
                        Dim _Parent = (From c In _Reports Where c.BID = _Child.ParentID Order By c.BID, c.No).FirstOrDefault()

                        If _Parent IsNot Nothing Then
                            Recursive(_Parent.BID, _Reports, _BID)
                        End If
                    End If

                End If


            Next

            If _BID.Count > 0 Then
                Dim sb As New StringBuilder()

                If _BID.Count = 1 Then
                    sb.Append(_BID(0).ToString)
                Else
                    For Each _ID In _BID
                        If String.IsNullOrEmpty(sb.ToString()) Then
                            sb.Append(_ID.ToString)
                        Else
                            sb.Append("," & _ID.ToString)
                        End If

                    Next
                End If

                e.Command.CommandText = String.Format("select * from tblDataSourceBI where BID in({0}) order by BID, No", sb.ToString())

            Else
                e.Command.CommandText = "select * from tblDataSourceBI where BID is null "

            End If


        End Using
    End Sub



    Sub Recursive(ByVal _ParentID As Integer, ByVal _Reports As List(Of tblDataSourceBI), ByRef _BID As List(Of Integer))

        'If value >= 100 Then
        '    Return value
        'End If
        Dim _Parent = (From c In _Reports Where c.BID = _ParentID).FirstOrDefault()
        If _Parent IsNot Nothing Then
            _BID.Add(_ParentID)
            If _Parent.ParentID IsNot Nothing Then
                Recursive(_Parent.ParentID, _Reports, _BID)
            End If
        End If
    End Sub
End Class
