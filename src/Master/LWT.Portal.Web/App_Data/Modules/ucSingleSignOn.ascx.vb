Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports FineUI
Imports BaseConfig
Imports System.Data.Linq
Public Class ucSingleSignOn
    Inherits PortalModuleControl

    '*******************************************************
    '
    ' The Page_Load server event handler on this user control is used
    ' to populate the current site settings from the config system
    '
    '*******************************************************
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '' Verify that the current user has access to access this page
        'If PortalSecurity.IsInRoles("Admins") = False Then
        '    Response.Redirect("~/Admin/EditAccessDenied.aspx")
        '    'Response.End()
        'End If

        ' If this is the first visit to the page, populate the site data
        If Page.IsPostBack = False Then
            BindData()
        End If
    End Sub


    Protected Sub grid1_Sort(ByVal sender As Object, ByVal e As FineUI.GridSortEventArgs) Handles Grid1.Sort
        BindGridWithSort(e.SortField, e.SortDirection)
    End Sub
    Protected Sub Grid1_PageIndexChange(ByVal sender As Object, ByVal e As FineUI.GridPageEventArgs) Handles Grid1.PageIndexChange
        Grid1.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Protected Sub tbxKeyword_TriggerClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbxKeyword.TriggerClick
        ViewState("View") = Nothing
        Grid1.PageIndex = 0
        BindData()

    End Sub

    Private Sub BindData()
        Dim column As FineUI.GridColumn = Grid1.FindColumn(Grid1.SortField)
        BindGridWithSort(column.SortField, Grid1.SortDirection)
    End Sub

    Private Sub BindGridWithSort(ByVal sortField As String, ByVal sortDirection As String)
        'If ViewState("View") Is Nothing Then
        Using dc As New DataClasses_PortalDataContextExt()
            Dim sb_where As New StringBuilder()
            If Not String.IsNullOrEmpty(tbxKeyword.Text) Then
                sb_where.AppendFormat(" and FirstName like '{0}%' ", tbxKeyword.Text)
            End If


            ViewState("View") = dc.ExecuteQuery(Of Context.Employee)("select * from Employees where UserID is not null and EmpGroupID=1 " & sb_where.ToString()).ToList()
        End Using
        'End If

        Dim _data As List(Of Context.Employee) = ViewState("View")
        Dim table As DataTable = MyUtils.EQToDataTable(_data)
        Dim view1 As DataView = table.DefaultView
        If _data.Count > 0 Then
            view1.Sort = String.Format("{0} {1}", sortField, sortDirection)
        End If
        Grid1.DataSource = view1
        Grid1.DataBind()
        SimpleForm1.Enabled = False
    End Sub

    Protected Sub Grid1_RowDoubleClick(sender As Object, e As FineUI.GridRowClickEventArgs) Handles Grid1.RowDoubleClick

        Dim _EmployeeID As String = Grid1.DataKeys(e.RowIndex + (Grid1.PageIndex * Grid1.PageSize))(0)
        Dim _UserID As String = Grid1.DataKeys(e.RowIndex + (Grid1.PageIndex * Grid1.PageSize))(1)

        hdEmployeeID.Text = _EmployeeID
        hdUserId.Text = _UserID


        Using dc = New DataClasses_PortalDataContextExt()

            Dim _Employees = (From c In dc.Employees Where c.EmployeeID.Equals(hdEmployeeID.Text) Select c).FirstOrDefault()
            SimpleForm1.Title = String.Format("Employee : {0} {1}", _Employees.FirstName, _Employees.LastName)


            Dim _ProjectSingleSignOn_Mapping = (From c In dc.ProjectSingleSignOn_Mappings Where c.UserID.Equals(_UserID) Order By c.PortalCode, c.RoleCode Select c).ToList()




        End Using

        BindUserSingleSignOn()

        SimpleForm1.Enabled = True

        BindProjectRole()



    End Sub

    Protected Sub btnAddUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAddUser.Click

        If String.IsNullOrEmpty(hdEmployeeID.Text) Then Return

        If String.IsNullOrEmpty(ddlProjectRole.SelectedValue) Then Return

        'Alert.Show(ddlProjectRole.SelectedValue)
        WindowUser.Hidden = False

        Using dc As New DataClasses_PortalDataContext(PortalConn)

            Dim _selected = ddlProjectRole.SelectedValue.Split(";")
            Dim _projectCode = _selected(0)
            Dim _roleCode = _selected(1)

            Dim _Projects = (From c In dc.PortalCfg_Globals _
                             Where c.PortalCode.Equals(_projectCode) _
                             ).FirstOrDefault()


            WindowUser.Title = "User : " & _Projects.PortalName

            Using dc_user As New DataContext(_Projects.cfg_conn)

                Dim _users = dc_user.ExecuteQuery(Of Context.SingleSignOn_Users)(_Projects.cfg_query_user).ToList()

                Grid3.DataSource = _users.OrderBy(Function(obj) obj.UserName)
                Grid3.DataBind()
            End Using



        End Using


        'Grid3


    End Sub

    Private Sub BindProjectRole()

        Dim myList As New List(Of Context.JQueryFeature)()


        Using dc As New DataClasses_PortalDataContext(PortalConn)

            'Dim _Projects = (From c In dc.ProjectRoleUsers Where c.UserID.Equals(hdUserId.Text) And Not c.PortalId.Equals(webconfig._PortalID) _
            'Group By key = New With {c.PortalCode, c.PortalId, c.PortalName} _
            'Into Group Select key).ToList()


            myList.Add(New Context.JQueryFeature("" _
                                                         , "Select Role" _
                                                         , 0 _
                                                         , False _
                                                         ))

            'Dim _Projects = (From c In dc.PortalCfg_Globals Where Not c.cfg_query_user.Equals("") Order By c.PortalName).ToList()
            Dim _Projects = (From c In dc.PortalCfg_Globals Where c.PortalId <> 1 Order By c.PortalName).ToList()
            For Each item In _Projects
                myList.Add(New Context.JQueryFeature("" _
                                                         , item.PortalName _
                                                         , 1 _
                                                         , False _
                                                         ))


                'Dim _ProjectRoles = (From c In dc.ProjectRoleUsers Where c.PortalId.Equals(item.PortalId) And c.UserID.Equals(hdUserId.Text) Order By c.RoleCode).ToList()
                'For Each item_role In _ProjectRoles
                '    myList.Add(New Context.JQueryFeature(item.PortalCode & ";" & item_role.RoleCode _
                '                                         , item_role.RoleName _
                '                                         , 2 _
                '                                         , True _
                '                                         ))
                'Next


                Dim _ProjectRoles = (From c In dc.ProjectRoles Where c.PortalID.Equals(item.PortalId) Order By c.PortalID).ToList()
                For Each item_role In _ProjectRoles

                    myList.Add(New Context.JQueryFeature(item.PortalCode & ";" & item_role.RoleCode _
                                                                       , item_role.RoleName _
                                                                       , 2 _
                                                                       , True _
                                                                       ))
                Next


            Next

        End Using




        'Using dc As New DataClasses_PortalDataContext(PortalConn)
        '    'Dim _TreeMenuRoles As New List(Of TreeMenuRoles)

        '    Dim _Projects = (From c In dc.PortalCfg_Globals Order By c.PortalCode).ToList()

        '    For Each item In _Projects
        '        Dim node = New FineUI.TreeNode()
        '        'node.NodeID = item.PortalId
        '        node.Text = item.PortalName
        '        node.Expanded = True
        '        node.EnableCheckBox = False


        '        Dim _ProjectRoles = (From c In dc.ProjectRoles Where c.PortalID.Equals(item.PortalId) Order By c.PortalID).ToList()
        '        For Each item_role In _ProjectRoles

        '            Dim subnode = New FineUI.TreeNode()
        '            subnode.NodeID = item_role.RoleID
        '            subnode.Text = item_role.RoleName
        '            subnode.Expanded = True
        '            subnode.EnableCheckBox = True

        '            node.Nodes.Add(subnode)
        '        Next



        '        trUserRoles.Nodes.Add(node)
        '    Next


        'End Using


        ddlProjectRole.DataTextField = "Name"
        ddlProjectRole.DataValueField = "Id"
        ddlProjectRole.DataSimulateTreeLevelField = "Level"
        ddlProjectRole.DataEnableSelectField = "EnableSelect"
        ddlProjectRole.DataSource = myList
        ddlProjectRole.DataBind()

    End Sub



    'Private Sub BindProjectRole()



    'End Sub

    Private Sub BindUserSingleSignOn()
        trUserRoles.Hidden = True
        trUserRoles.Nodes.Clear()

        Using dc As New DataClasses_PortalDataContext(PortalConn)

            Dim _Projects = (From c In dc.ProjectSingleSignOn_Mappings Where c.UserID.Equals(hdUserId.Text) And Not c.PortalId.Equals(webconfig._PortalID) _
            Group By key = New With {c.PortalCode, c.PortalId, c.PortalName} _
            Into Group Select key Order By key.PortalName).ToList()




            For Each item In _Projects
                Dim node = New FineUI.TreeNode()
                'node.NodeID = item.PortalId
                node.Text = item.PortalName
                node.Expanded = True
                node.EnableCheckBox = False


                Dim _ProjectRoles = (From c In dc.ProjectSingleSignOn_Mappings Where c.PortalCode.Equals(item.PortalCode) And c.UserID.Equals(hdUserId.Text) Order By c.RoleCode).ToList()
                For Each item_role In _ProjectRoles

                    Dim subnode = New FineUI.TreeNode()
                    subnode.NodeID = item_role.ID
                    subnode.Text = item_role.RoleName & ":" & item_role.UserMapping

                    subnode.Expanded = True
                    subnode.EnableCheckBox = True

                    node.Nodes.Add(subnode)



                    trUserRoles.Hidden = False
                Next

                trUserRoles.Nodes.Add(node)

            Next



        End Using



    End Sub



    Protected Sub Grid3_RowDoubleClick(sender As Object, e As FineUI.GridRowClickEventArgs) Handles Grid3.RowDoubleClick

        If String.IsNullOrEmpty(ddlProjectRole.SelectedValue) Then Return


        Dim _UserName As String = Grid3.DataKeys(e.RowIndex)(0)

        Dim _selected = ddlProjectRole.SelectedValue.Split(";")
        Dim _projectCode = _selected(0)
        Dim _roleCode = _selected(1)


        Using dc = New DataClasses_PortalDataContext(PortalConn)

            Dim _Portal_Roles = (From c In dc.Portal_Roles Where c.RoleCode.Equals(_roleCode)).FirstOrDefault()

            Dim _Portal_UserRoles = (From c In dc.Portal_UserRoles Where c.UserID.Equals(hdUserId.Text) And c.RoleID.Equals(_Portal_Roles.RoleID)).FirstOrDefault()

            If _Portal_UserRoles Is Nothing Then 'new
                dc.ExecuteCommand("insert into Portal_UserRoles(UserID,RoleID) values(" & hdUserId.Text & "," & _Portal_Roles.RoleID.ToString() & ")")
            End If


            Dim _data = (From c In dc.SingleSignOn_Mappings Where c.UserId.Equals(hdUserId.Text) And c.ProjectCode.Equals(_projectCode) And c.RoleCode.Equals(_roleCode) Select c).FirstOrDefault()
            If _data Is Nothing Then

                dc.SingleSignOn_Mappings.InsertOnSubmit(New SingleSignOn_Mapping With {.ProjectCode = _projectCode _
                                                                                      , .RoleCode = _roleCode _
                                                                                      , .UserMapping = _UserName _
                                                                                      , .UserId = hdUserId.Text} _
                                                                                  )
            Else

                _data.UserMapping = _UserName

            End If

            dc.SubmitChanges()


        End Using

        BindUserSingleSignOn()


        WindowUser.Hidden = True
    End Sub


    Protected Sub btnDeleteUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteUser.Click
        Using dc As New DataClasses_PortalDataContext(PortalConn)
            For Each item_userrole In trUserRoles.GetCheckedNodeIDs
                'dc.ExecuteCommand("delete SingleSignOn_Mapping where id={0}", item_userrole)
                Dim _SingleSignOn_Mappings = (From c In dc.SingleSignOn_Mappings Where c.ID.Equals(item_userrole)).FirstOrDefault()

                Dim _UserId = _SingleSignOn_Mappings.UserId
                Dim _RoleCode = _SingleSignOn_Mappings.RoleCode
                Dim _Portal_Roles = (From c In dc.Portal_Roles Where c.RoleCode.Equals(_RoleCode)).FirstOrDefault()
                Dim _RoleID = _Portal_Roles.RoleID

                dc.SingleSignOn_Mappings.DeleteOnSubmit(_SingleSignOn_Mappings)
                dc.SubmitChanges()

                dc.ExecuteCommand("delete from Portal_UserRoles where userid=" & _UserId & " and roleid=" & _RoleID)

            Next
        End Using
        BindUserSingleSignOn()
    End Sub


End Class