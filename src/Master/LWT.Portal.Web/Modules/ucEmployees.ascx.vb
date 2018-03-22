Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports FineUI
Public Class ucEmployees
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

            ' Obtain PortalSettings from Current Context
            Dim portalSettings As PortalSettings = CType(Context.Items("PortalSettings"), PortalSettings)


            BindTitleOfCourtesy()
            BindProjectRole()
            'BindProject()
            BindEmpGroup()
            BindData()
            BindRegion()

        End If
    End Sub

    'Private Sub BindProject()
    '    Using dc As New DataClasses_PortalDataContext(PortalConn)

    '        Dim _data_Project = (From c In dc.PortalCfg_Globals Order By c.PortalCode Select c.PortalId, c.PortalName).ToList()

    '        ddlProject.DataTextField = "PortalName"
    '        ddlProject.DataValueField = "PortalId"
    '        ddlProject.DataSource = _data_Project
    '        ddlProject.DataBind()
    '    End Using
    'End Sub
    'Private Sub BindRole()
    '    Using dc As New DataClasses_PortalDataContext(PortalConn)

    '        Dim _data_role = (From c In dc.Portal_Roles Where c.PortalID.Equals(ddlProject.SelectedValue) Order By c.RoleCode Select c.RoleID, c.RoleName).ToList()

    '        rblUserRole.DataTextField = "RoleName"
    '        rblUserRole.DataValueField = "RoleID"
    '        rblUserRole.DataSource = _data_role
    '        rblUserRole.DataBind()


    '    End Using
    'End Sub
    Private Sub BindUserRole()

        If Not String.IsNullOrEmpty(hdUserId.Text) Then



            'rblUserRole_SelectedIndexChanged()
            'RemoveHandler rblUserRole.SelectedIndexChanged, AddressOf rblUserRole_SelectedIndexChanged

            Using dc = New DataClasses_PortalDataContext(PortalConn)
                Dim _Portal_UserRoles = (From c In dc.Portal_UserRoles Where c.UserID.Equals(hdUserId.Text) Select c).ToList()

                For Each item In trUserRoles.Nodes
                    For Each item_userrole In item.Nodes
                        If _Portal_UserRoles.Count > 0 Then
                            For Each temp In _Portal_UserRoles
                                If item_userrole.NodeID.ToString().Equals(temp.RoleID.ToString()) Then
                                    item_userrole.Checked = True
                                    Exit For
                                Else
                                    item_userrole.Checked = False
                                End If
                            Next
                        Else
                            item_userrole.Checked = False
                        End If



                    Next
                Next

            End Using

            'AddHandler rblUserRole.SelectedIndexChanged, AddressOf rblUserRole_SelectedIndexChanged


        End If
    End Sub

    Private Sub BindTitleOfCourtesy()
        Using dc As New DataClasses_PortalDataContext(PortalConn)
            Dim _data = (From c In dc.Portal_Tables Where c.TABLE_NUMBER.Equals("T0002") Order By c.ITEM_DESC_TH Select c.ITEM_CODE, c.ITEM_DESC_TH).ToList()
            ddlTitleOfCourtesy.DataTextField = "ITEM_DESC_TH"
            ddlTitleOfCourtesy.DataValueField = "ITEM_DESC_TH"
            ddlTitleOfCourtesy.DataSource = _data
            ddlTitleOfCourtesy.DataBind()
            ddlTitleOfCourtesy.Items.Insert(0, New ListItem("", ""))
        End Using

    End Sub
    Private Sub BindRegion()
        Using dc As New DataClasses_PortalDataContext(PortalConn)
            Dim _data = (From c In dc.Portal_Tables Where c.TABLE_NUMBER.Equals("T0001") And c.ITEM_CODE.StartsWith("G") Order By c.ITEM_DESC_TH Select c.ITEM_CODE, c.ITEM_DESC_TH).ToList()
            ddlRegion.DataTextField = "ITEM_DESC_TH"
            ddlRegion.DataValueField = "ITEM_CODE"
            ddlRegion.DataSource = _data
            ddlRegion.DataBind()
            ddlRegion.Items.Insert(0, New ListItem("", ""))
        End Using

        ddlProvince.Items.Clear()
        ddlAmphur.Items.Clear()
        ddlLocation.Items.Clear()
        'ddlZipcode.Items.Clear()
        tbxZipcode.Text = ""

    End Sub
    Private Sub BindProvince()
        Using dc As New DataClasses_PortalDataContext(PortalConn)
            Dim _data = (From c In dc.Portal_Tables Where c.TABLE_NUMBER.Equals("T0001") And c.ITEM_CODE.StartsWith("P") And c.ITEM_PARENT.Equals(ddlRegion.SelectedValue) Order By c.ITEM_DESC_TH Select c.ITEM_CODE, c.ITEM_DESC_TH).ToList()
            ddlProvince.DataTextField = "ITEM_DESC_TH"
            ddlProvince.DataValueField = "ITEM_CODE"
            ddlProvince.DataSource = _data
            ddlProvince.DataBind()
            ddlProvince.Items.Insert(0, New ListItem("", ""))
        End Using
        'ddlProvince.Items.Clear()
        ddlAmphur.Items.Clear()
        ddlLocation.Items.Clear()
        'ddlZipcode.Items.Clear()
        tbxZipcode.Text = ""
    End Sub
    Private Sub BindAmphur()
        Using dc As New DataClasses_PortalDataContext(PortalConn)
            Dim _data = (From c In dc.master_locations Where c.PROVINCE.Equals(ddlProvince.SelectedText) Group c By c.AMPHUR Into g = Group Select New With {Key AMPHUR}).OrderBy(Function(tkey) tkey.AMPHUR).ToList()



            'Dim _data = (From c In dc.Portal_Tables Where c.TABLE_NUMBER.Equals("T0001") And c.ITEM_CODE.StartsWith("A") And c.ITEM_PARENT.Equals(ddlProvince.SelectedValue) Order By c.ITEM_DESC_TH Select c.ITEM_CODE, c.ITEM_DESC_TH).ToList()
            ddlAmphur.DataTextField = "AMPHUR"
            ddlAmphur.DataValueField = "AMPHUR"
            ddlAmphur.DataSource = _data
            ddlAmphur.DataBind()
            ddlAmphur.Items.Insert(0, New ListItem("", ""))
        End Using
        'ddlProvince.Items.Clear()
        'ddlAmphur.Items.Clear()
        ddlLocation.Items.Clear()
        'ddlZipcode.Items.Clear()
        tbxZipcode.Text = ""
    End Sub
    Private Sub BindDistrict()
        Using dc As New DataClasses_PortalDataContext(PortalConn)
            Dim _data = (From c In dc.master_locations Where c.PROVINCE.Equals(ddlProvince.SelectedText) And c.AMPHUR.Equals(ddlAmphur.SelectedText) Order By c.DISTRICT Select c.LOCATION_CODE, c.DISTRICT).ToList()
            ddlLocation.DataTextField = "DISTRICT"
            ddlLocation.DataValueField = "LOCATION_CODE"
            ddlLocation.DataSource = _data
            ddlLocation.DataBind()
            ddlLocation.Items.Insert(0, New ListItem("", ""))
        End Using
        'ddlProvince.Items.Clear()
        'ddlAmphur.Items.Clear()
        'ddlDistrict.Items.Clear()
        'ddlZipcode.Items.Clear()
        tbxZipcode.Text = ""
    End Sub
    Private Sub BindZipcode()
        Using dc As New DataClasses_PortalDataContext(PortalConn)
            Dim _data = (From c In dc.master_locations Where c.LOCATION_CODE.Equals(ddlLocation.SelectedValue) Order By c.LOCATION_CODE Select c).FirstOrDefault()
            'ddlZipcode.DataTextField = "ITEM_DESC_TH"
            'ddlZipcode.DataValueField = "ITEM_CODE"
            'ddlZipcode.DataSource = _data
            'ddlZipcode.DataBind()
            If Not _data Is Nothing Then
                tbxZipcode.Text = _data.ZIPCODE
            Else
                tbxZipcode.Text = ""
            End If

        End Using

    End Sub

    Protected Sub ddlRegion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlRegion.SelectedIndexChanged
        BindProvince()
    End Sub
    Protected Sub ddlProvince_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProvince.SelectedIndexChanged
        BindAmphur()
    End Sub
    Protected Sub ddlAmphur_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlAmphur.SelectedIndexChanged
        BindDistrict()
    End Sub
    Protected Sub ddlLocation_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlLocation.SelectedIndexChanged
        BindZipcode()
    End Sub


    Private Sub BindEmpGroup()
        Using dc As New DataClasses_PortalDataContext(PortalConn)

            Dim _data_Project = (From c In dc.EmpGroups Select c.EmpGroupID, c.EmpGroupName).ToList()

            ddlEmpGroup.DataTextField = "EmpGroupName"
            ddlEmpGroup.DataValueField = "EmpGroupID"
            ddlEmpGroup.DataSource = _data_Project
            ddlEmpGroup.DataBind()


            ddlEmpGroup.Items.Insert(0, New ListItem("==== All =====", ""))


            ddlEmpGroupID.DataTextField = "EmpGroupName"
            ddlEmpGroupID.DataValueField = "EmpGroupID"
            ddlEmpGroupID.DataSource = _data_Project
            ddlEmpGroupID.DataBind()


            ddlEmpGroupID.Items.Insert(0, New ListItem("", ""))

        End Using
    End Sub


    Protected Sub filePhoto_FileSelected(sender As Object, e As EventArgs) Handles filePhoto.FileSelected
        If filePhoto.HasFile Then
            Dim fileName As String = filePhoto.ShortFileName

            If Not Utils.ValidateFileType(fileName) Then
                Alert.Show("Invalid Picture Type！")
                Return
            End If


            fileName = fileName.Replace(":", "_").Replace(" ", "_").Replace("\", "_").Replace("/", "_")
            fileName = DateTime.Now.Ticks.ToString() & "_" & fileName
            filePhoto.SaveAs(Server.MapPath("~/upload/image/employee/" & fileName))
            imgPhoto.ImageUrl = "~/upload/image/employee/" & fileName

            filePhoto.Reset()
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
        Grid1.Hidden = False
        SimpleForm1.Hidden = Not (Grid1.Hidden)
        BindData()

    End Sub
    Protected Sub btnBack_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Grid1.Hidden = SimpleForm1.Hidden
        SimpleForm1.Hidden = Not (Grid1.Hidden)
    End Sub
    Private Sub BindData()
        Dim column As FineUI.GridColumn = Grid1.FindColumn(Grid1.SortField)
        BindGridWithSort(column.SortField, Grid1.SortDirection)
    End Sub

    Private Sub BindGridWithSort(ByVal sortField As String, ByVal sortDirection As String)
        'If ViewState("View") Is Nothing Then
        Using dc As New DataClasses_PortalDataContext(PortalConn)
            Dim sb_where As New StringBuilder()
            If Not String.IsNullOrEmpty(tbxKeyword.Text) Then
                sb_where.AppendFormat(" and FirstName like '{0}%' ", tbxKeyword.Text)
            End If

            If Not String.IsNullOrEmpty(ddlEmpGroup.SelectedValue) Then
                sb_where.AppendFormat(" and EmpGroupID = {0} ", ddlEmpGroup.SelectedValue)
            End If

            ViewState("View") = dc.ExecuteQuery(Of Context.Employee)("select * from Employees where UserID is not null " & sb_where.ToString()).ToList()
        End Using
        'End If

        Dim _data As List(Of Context.Employee) = ViewState("View")
        Dim table As DataTable = Utils.EQToDataTable(_data)
        Dim view1 As DataView = table.DefaultView
        If _data.Count > 0 Then
            view1.Sort = String.Format("{0} {1}", sortField, sortDirection)
        End If
        Grid1.DataSource = view1
        Grid1.DataBind()

    End Sub

    Protected Sub Grid1_RowDoubleClick(sender As Object, e As FineUI.GridRowClickEventArgs) Handles Grid1.RowDoubleClick
        Grid1.Hidden = SimpleForm1.Hidden
        SimpleForm1.Hidden = Not (Grid1.Hidden)

        Dim _EmployeeID As String = Grid1.DataKeys(e.RowIndex + (Grid1.PageIndex * Grid1.PageSize))(0)
        SimpleForm1.Enabled = True
        BindRegion()



        Using dc = New DataClasses_PortalDataContext(PortalConn)
            hdEmployeeID.Text = _EmployeeID




            Dim _Employees = (From c In dc.Employees Where c.EmployeeID.Equals(hdEmployeeID.Text) Select c).FirstOrDefault()

            SimpleForm1.Title = String.Format("Employee : {0} {1}", _Employees.FirstName, _Employees.LastName)

            If Not String.IsNullOrEmpty(_Employees.PhotoPath) Then
                imgPhoto.ImageUrl = _Employees.PhotoPath

            Else
                imgPhoto.ImageUrl = "~/res/images/blank.png"
            End If
            'imgPhoto.Height = 100

            hdUserId.Text = _Employees.UserID
            If String.IsNullOrEmpty(hdUserId.Text) Then
                btnUserProfile.Hidden = True
            Else
                btnUserProfile.Hidden = False
            End If

            ddlEmpGroupID.SelectedValue = IIf(Not _Employees.EmpGroupID Is Nothing, _Employees.EmpGroupID, "")

            tbxEmail.Text = _Employees.Email

            ddlTitleOfCourtesy.SelectedValue = IIf(Not _Employees.TitleOfCourtesy Is Nothing, _Employees.TitleOfCourtesy, "")

            tbxFirstName.Text = _Employees.FirstName
            tbxLastName.Text = _Employees.LastName
            tbxTitle.Text = _Employees.Title
            dpBirthDate.SelectedDate = _Employees.BirthDate
            dpHireDate.SelectedDate = _Employees.HireDate
            tbxAddress.Text = _Employees.Address


            ddlRegion.SelectedValue = IIf(Not _Employees.Region Is Nothing, _Employees.Region, "")

            If Not String.IsNullOrEmpty(_Employees.LOCATION_CODE) Then

                Dim _location = (From c In dc.master_locations Where c.LOCATION_CODE.Equals(_Employees.LOCATION_CODE)).FirstOrDefault()
                If Not _location Is Nothing Then

                    Dim _Province = (From c In dc.Portal_Tables Where c.TABLE_NUMBER.Equals("T0001") And c.ITEM_CODE.StartsWith("P") And c.ITEM_DESC_TH.Equals(_location.PROVINCE) Select c.ITEM_CODE).Single()

                    BindProvince()
                    ddlProvince.SelectedValue = IIf(Not _Province Is Nothing, _Province.ToString(), "")

                    BindAmphur()
                    ddlAmphur.SelectedValue = IIf(Not _location.AMPHUR Is Nothing, _location.AMPHUR.ToString(), "")

                    BindDistrict()
                    ddlLocation.SelectedValue = _location.LOCATION_CODE

                    tbxZipcode.Text = _location.ZIPCODE
                Else
                    tbxZipcode.Text = _Employees.PostalCode
                End If
            Else
                tbxZipcode.Text = _Employees.PostalCode
            End If


            tbxHomePhone.Text = _Employees.HomePhone
            tbxExtension.Text = _Employees.Extension
            tbxNotes.Text = _Employees.Notes


        End Using
    End Sub

    Protected Sub btnSaveEmployee_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveEmployee.Click

        If String.IsNullOrEmpty(hdEmployeeID.Text) Then Return


        Using dc = New DataClasses_PortalDataContext(PortalConn)
            Dim _Employees = (From c In dc.Employees Where c.EmployeeID.Equals(hdEmployeeID.Text) Select c).FirstOrDefault()

            With _Employees
                .EmpGroupID = ddlEmpGroupID.SelectedValue
                .Email = tbxEmail.Text
                .TitleOfCourtesy = ddlTitleOfCourtesy.SelectedValue

                .FirstName = tbxFirstName.Text
                .LastName = tbxLastName.Text
                .Title = tbxTitle.Text

                .BirthDate = dpBirthDate.SelectedDate
                .HireDate = dpHireDate.SelectedDate
                .Address = tbxAddress.Text

                .Region = ddlRegion.SelectedValue
                .LOCATION_CODE = ddlLocation.SelectedValue
                .PostalCode = tbxZipcode.Text
                .Province = ddlProvince.SelectedText
                .Amphur = ddlAmphur.SelectedText
                .District = ddlLocation.SelectedText

                .HomePhone = tbxHomePhone.Text
                .Extension = tbxExtension.Text
                .Notes = tbxNotes.Text
                .PhotoPath = imgPhoto.ImageUrl
            End With



            dc.SubmitChanges()
        End Using
        ViewState("View") = Nothing
        BindData()
    End Sub

    'Protected Sub ddlProject_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlProject.SelectedIndexChanged
    '    BindRole()
    '    BindUserRole()
    'End Sub
    Protected Sub btnUserProfile_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUserProfile.Click
        WindowUserProfile.Hidden = False

        Using dc = New DataClasses_PortalDataContext(PortalConn)

            Dim _Portal_Users = (From c In dc.Portal_Users Where c.UserID.Equals(hdUserId.Text) Select c).FirstOrDefault()

            If ddlEmpGroupID.SelectedText.Equals("LWT") Then
                Image1.ImageUrl = String.Format("http://lockthbnkedc01/sites/HRDweb/LWT%20Member%20Photos/{0}.jpg", _Portal_Users.UserName)
            Else
                Image1.ImageUrl = imgPhoto.ImageUrl
            End If


            SimpleForm1.Title = String.Format("User : {0}", _Portal_Users.UserName)

            lbUserName.Text = _Portal_Users.UserName
            cbIsaApproved.Checked = _Portal_Users.IsApproved
            tbxComment.Text = _Portal_Users.Comment

            lbEmail.Text = _Portal_Users.Email
            lbPasswordQuestion.Text = _Portal_Users.PasswordQuestion
            lbPasswordAnswer.Text = _Portal_Users.PasswordAnswer
            cbIsLocked.Checked = _Portal_Users.IsLocked

            dpExpiredDate.SelectedDate = _Portal_Users.ExpiredDate

            lbCreationDate.Text = IIf(Not _Portal_Users.CreationDate Is Nothing, _Portal_Users.CreationDate, "")
            lbLastLoginDate.Text = IIf(Not _Portal_Users.LastLoginDate Is Nothing, _Portal_Users.LastLoginDate, "")
            lbLastActivityDate.Text = IIf(Not _Portal_Users.LastActivityDate Is Nothing, _Portal_Users.LastActivityDate, "")
            lbLastPasswordChangedDate.Text = IIf(Not _Portal_Users.LastPasswordChangedDate Is Nothing, _Portal_Users.LastPasswordChangedDate, "")


            lbLastLockOutDate.Text = IIf(Not _Portal_Users.LastLockOutDate Is Nothing, _Portal_Users.LastLockOutDate, "")
            lbFailedPasswordAttemptCount.Text = IIf(Not _Portal_Users.FailedPasswordAttemptCount Is Nothing, _Portal_Users.FailedPasswordAttemptCount, "")
            lbFailedPasswordAttemptWindowStart.Text = IIf(Not _Portal_Users.FailedPasswordAttemptWindowStart Is Nothing, _Portal_Users.FailedPasswordAttemptWindowStart, "")
            lbFailedPasswordAnswerAttemptCount.Text = IIf(Not _Portal_Users.FailedPasswordAnswerAttemptCount Is Nothing, _Portal_Users.FailedPasswordAnswerAttemptCount, "")
            lbFailedPasswordAnswerAttemptWindowStart.Text = IIf(Not _Portal_Users.FailedPasswordAnswerAttemptWindowStart Is Nothing, _Portal_Users.FailedPasswordAnswerAttemptWindowStart, "")

            'BindRole()
            BindUserRole()
        End Using
    End Sub
    'Protected Sub rblUserRole_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles rblUserRole.SelectedIndexChanged
    '    If String.IsNullOrEmpty(hdUserId.Text) Then Return


    '    Using dc = New DataClasses_PortalDataContext(PortalConn)
    '        'clear all role
    '        For Each item In rblUserRole.Items
    '            dc.ExecuteCommand("delete from Portal_UserRoles where UserID={0} and RoleID={1} ", hdUserId.Text, item.Value)
    '        Next

    '        'insert new role
    '        For Each item In rblUserRole.SelectedValueArray
    '            dc.ExecuteCommand("insert into Portal_UserRoles(UserID,RoleID) values({0},{1}) ", hdUserId.Text, item.ToString())
    '        Next
    '    End Using



    'End Sub
    Protected Sub btnApplyUser_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnApplyUser.Click
        If String.IsNullOrEmpty(hdUserId.Text) Then Return


        Using dc = New DataClasses_PortalDataContext(PortalConn)
            Dim _Portal_Users = (From c In dc.Portal_Users Where c.UserID.Equals(hdUserId.Text) Select c).FirstOrDefault()

            _Portal_Users.IsApproved = cbIsaApproved.Checked
            _Portal_Users.Comment = tbxComment.Text
            _Portal_Users.IsLocked = cbIsLocked.Checked

            _Portal_Users.ExpiredDate = dpExpiredDate.SelectedDate

            dc.SubmitChanges()



            For Each item In trUserRoles.Nodes
                For Each item_userrole In item.Nodes
                    Dim _RoleId As String = item_userrole.NodeID
                    dc.ExecuteCommand("delete from Portal_UserRoles where UserID={0} and RoleID={1} ", hdUserId.Text, _RoleId)
                    If item_userrole.Checked Then
                        dc.ExecuteCommand("insert into Portal_UserRoles(UserID,RoleID) values({0},{1}) ", hdUserId.Text, _RoleId)
                    End If
                Next
            Next
        End Using

    End Sub





    Private Sub BindProjectRole()

        Using dc As New DataClasses_PortalDataContext(PortalConn)
            'Dim _TreeMenuRoles As New List(Of TreeMenuRoles)

            Dim _Projects = (From c In dc.PortalCfg_Globals Order By c.PortalCode).ToList()

            For Each item In _Projects
                Dim node = New FineUI.TreeNode()
                'node.NodeID = item.PortalId
                node.Text = item.PortalName
                node.Expanded = True
                node.EnableCheckBox = False


                Dim _ProjectRoles = (From c In dc.ProjectRoles Where c.PortalID.Equals(item.PortalId) Order By c.PortalID).ToList()
                For Each item_role In _ProjectRoles

                    Dim subnode = New FineUI.TreeNode()
                    subnode.NodeID = item_role.RoleID
                    subnode.Text = item_role.RoleName
                    subnode.Expanded = True
                    subnode.EnableCheckBox = True

                    node.Nodes.Add(subnode)
                Next



                trUserRoles.Nodes.Add(node)
            Next


        End Using

    End Sub


End Class