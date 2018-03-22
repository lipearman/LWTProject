Imports Portal.Components
Imports DevExpress.Web.ASPxTreeList
Imports DevExpress.Web
Imports System.Data.SqlClient
Imports System.Data
Imports DevExpress.Web.Data

Partial Class Modules_ucDevxBIUser
    Inherits PortalModuleControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load

        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(LWT.Website.webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If


        SqlDataSource_Data.SelectParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name
        SqlDataSource_Data.SelectParameters("PortalId").DefaultValue = portalSettings.PortalId

        pnMain.HeaderText = portalSettings.ActiveTab.TabName
        pnMain.HeaderImage.IconID = portalSettings.ActiveTab.IconID
    End Sub

    Protected Sub cbPreview_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbPreview.Callback
        'Dim _BID = CInt(e.Parameter)
        Session("GUID") = e.Parameter
        e.Result = e.Parameter
    End Sub
    Protected Sub cbPreviewExcel_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbPreviewExcel.Callback
        'Dim _BID = CInt(e.Parameter)
        Session("GUID") = e.Parameter
        e.Result = e.Parameter
    End Sub
    Public Function GetURL(ByVal _GUID As String) As String
        Dim sb As New StringBuilder
        Using dc As New DataClasses_PortalBIExt()
            Dim Data = (From c In dc.V_DataSources Where c.GUID.Equals(_GUID)).FirstOrDefault()
            If Data IsNot Nothing Then

                Select Case Data.CONNTYPE
                    Case "olapConnection"
                        sb.AppendFormat("<a href=""javascript:void(0);"" onclick=""javascript:cbPreview.PerformCallback('{0}');"">{1}</a>", _GUID, Data.TITLE)


                    Case "msSqlConnection"
                        sb.AppendFormat("<a href=""javascript:void(0);"" onclick=""javascript:cbPreviewExcel.PerformCallback('{0}');"">{1}</a>", _GUID, Data.TITLE)

                    Case Else
                        sb.Append("<b>" & Data.TITLE & "</b>")
                End Select
               

            Else

            End If

        End Using



       
        Return sb.ToString()
    End Function

    Protected Sub treeList_HtmlDataCellPrepared(ByVal sender As Object, ByVal e As TreeListHtmlDataCellEventArgs) Handles treeList.HtmlDataCellPrepared
        'If e.Column.FieldName = "GUID" Or e.Column.FieldName = "CUBE_ID" Then
        '    e.Cell.HorizontalAlign = HorizontalAlign.Left
        '    e.Column.CellStyle.HorizontalAlign = HorizontalAlign.Left
        'End If
        'If e.Column.FieldName = "GUID" Then
        '    Dim _GUID = e.NodeKey
        '    Using dc As New DataClasses_PortalBIExt()
        '        'Dim _rpt = dc.tblBIs.Where(Function(x) x.BID.Equals(_BID)).FirstOrDefault()
        '        'If _rpt.CUBE_ID Is Nothing Then
        '        '    e.Cell.Text = _rpt.TITLE
        '        '    e.Cell.Font.Bold = True
        '        'End If
        '        Dim _rpt = dc.V_DataSources.Where(Function(x) x.GUID.Equals(_GUID)).FirstOrDefault()
        '        If _rpt Is Nothing Then
        '            'e.Cell.Text = _rpt.TITLE
        '            e.Cell.Font.Bold = True
        '        End If
        '    End Using
        'End If

    End Sub




    Protected Sub SqlDataSource1_Selecting(ByVal sender As Object, ByVal e As SqlDataSourceSelectingEventArgs) Handles SqlDataSource_Data.Selecting
   



        Using dc As New DataClasses_PortalBIExt()
            Dim _BID1 As New List(Of Integer)
            Dim _data = (From c In dc.tblBIAssignments Where c.USERNAME.Equals(HttpContext.Current.User.Identity.Name)).ToList()
            Dim _Reports = (From c In dc.tblBIs).ToList()
            For Each item In _data
                Dim _Child = (From c In _Reports Where c.BID = item.BID).FirstOrDefault()
                _BID1.Add(item.BID)
                If _Child IsNot Nothing Then
                    If _Child.ParentID IsNot Nothing Then
                        Dim _Parent = (From c In _Reports Where c.BID = _Child.ParentID).FirstOrDefault()

                        If _Parent IsNot Nothing Then
                            Recursive(_Parent.BID, _Reports, _BID1)
                        End If
                    End If

                End If
            Next

            '==========================================
            Dim _BID2 As New List(Of Integer)
            Dim _data2 = (From c In dc.tblDataSourceBI_Assignments Where c.USERNAME.Equals(HttpContext.Current.User.Identity.Name)).ToList()

            Dim _Report2 = (From c In dc.tblDataSourceBIs).ToList()
            For Each item In _data2
                Dim _Child2 = (From c In _Report2 Where c.BID = item.BID).FirstOrDefault()
                _BID2.Add(item.BID)
                If _Child2 IsNot Nothing Then
                    If _Child2.ParentID IsNot Nothing Then
                        Dim _Parent = (From c In _Report2 Where c.BID = _Child2.ParentID).FirstOrDefault()

                        If _Parent IsNot Nothing Then
                            Recursive2(_Parent.BID, _Report2, _BID2)
                        End If
                    End If

                End If
            Next


            Dim MySQL As New StringBuilder()

            Dim sb1 As New StringBuilder()
            If _BID1.Count > 0 Then
                If _BID1.Count = 1 Then
                    sb1.Append(_BID1(0).ToString)
                Else
                    For Each _ID In _BID1
                        If String.IsNullOrEmpty(sb1.ToString()) Then
                            sb1.Append(_ID.ToString)
                        Else
                            sb1.Append("," & _ID.ToString)
                        End If

                    Next
                End If
            Else
                sb1.Append("NULL")
            End If


            Dim sb2 As New StringBuilder()
            If _BID2.Count > 0 Then
                If _BID2.Count = 1 Then
                    sb2.Append(_BID2(0).ToString)
                Else
                    For Each _ID In _BID2
                        If String.IsNullOrEmpty(sb2.ToString()) Then
                            sb2.Append(_ID.ToString)
                        Else
                            sb2.Append("," & _ID.ToString)
                        End If

                    Next
                End If
            Else
                sb2.Append("NULL")
            End If



            Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(LWT.Website.webconfig._PortalContextName), PortalSettings)

            'e.Command.CommandText = String.Format("select RANK() OVER  (PARTITION BY case when tblBI.[ParentID] = 0 then tblBI.[BID] else tblBI.[ParentID] end ORDER BY  tblBI.[BID],tblBI.[No] ) AS MyRank ,tblBI.*,ParentBI.GUID as ParentGUID from tblBI left join tblBI as ParentBI on tblBI.[ParentID] = ParentBI.BID where tblBI.BID in({0}) and tblBI.PortalId ='{1}' ", sb.ToString(), portalSettings.PortalId)
            'e.Command.CommandText = String.Format("select tblBI.* from tblBI where tblBI.BID in({0}) order by tblBI.No", sb.ToString())

            MySQL.AppendLine(" select RANK() OVER  (PARTITION BY case when tblBI.[ParentID] = 0 then tblBI.[BID] else tblBI.[ParentID] end ORDER BY  tblBI.[BID],tblBI.[No] ) AS MyRank ")
            MySQL.AppendLine(" ,convert(varchar(255), ParentBI.GUID) COLLATE THAI_CI_AS as ParentGUID ")
            MySQL.AppendLine(" ,convert(varchar(255),tblBI.[GUID]) COLLATE THAI_CI_AS as [GUID] ")
            MySQL.AppendLine(" ,tblBI.TITLE ")
            MySQL.AppendLine(" ,tblBI.DESCRIPTION ")
            MySQL.AppendLine(" from tblBI left join tblBI as ParentBI ")
            MySQL.AppendLine(" on tblBI.[ParentID] = ParentBI.BID ")
            MySQL.AppendFormat(" where tblBI.BID in({0}) and tblBI.PortalId ='{1}' ", sb1.ToString(), portalSettings.PortalId)
            MySQL.AppendLine(" union all ")
            MySQL.AppendLine(" select RANK() OVER  (PARTITION BY case when tblDataSourceBI.[ParentID] = 0 then tblDataSourceBI.[BID] else tblDataSourceBI.[ParentID] end ORDER BY  tblDataSourceBI.[BID],tblDataSourceBI.[No] ) AS MyRank ")
            MySQL.AppendLine(" , convert(varchar(255), ParentBI.GUID) COLLATE THAI_CI_AS as ParentGUID ")
            MySQL.AppendLine(" ,convert(varchar(255),tblDataSourceBI.GUID) COLLATE THAI_CI_AS as [GUID] ")
            MySQL.AppendLine(" ,tblDataSourceBI.TITLE ")
            MySQL.AppendLine(" ,tblDataSourceBI.DESCRIPTION ")
            MySQL.AppendLine(" from tblDataSourceBI left join tblDataSourceBI as ParentBI ")
            MySQL.AppendLine(" on tblDataSourceBI.[ParentID] = ParentBI.BID ")
            MySQL.AppendFormat(" where tblDataSourceBI.BID in({0}) and tblDataSourceBI.PortalId ='{1}' ", sb2.ToString(), portalSettings.PortalId)



            e.Command.CommandText = MySQL.ToString()


            If _BID1.Count = 0 And _BID2.Count = 0 Then
                e.Command.CommandText = "select * from tblBI where BID is null"
            End If

        End Using
    End Sub
    Sub Recursive(ByVal _ParentID As Integer, ByVal _Reports As List(Of tblBI), ByRef _BID As List(Of Integer))
        Dim _Parent = (From c In _Reports Where c.BID = _ParentID).FirstOrDefault()
        If _Parent IsNot Nothing Then
            _BID.Add(_ParentID)
            If _Parent.ParentID IsNot Nothing Then
                Recursive(_Parent.ParentID, _Reports, _BID)
            End If
        End If
    End Sub
    Sub Recursive2(ByVal _ParentID As Integer, ByVal _Reports As List(Of tblDataSourceBI), ByRef _BID As List(Of Integer))
        Dim _Parent = (From c In _Reports Where c.BID = _ParentID).FirstOrDefault()
        If _Parent IsNot Nothing Then
            _BID.Add(_ParentID)
            If _Parent.ParentID IsNot Nothing Then
                Recursive2(_Parent.ParentID, _Reports, _BID)
            End If
        End If
    End Sub
End Class
