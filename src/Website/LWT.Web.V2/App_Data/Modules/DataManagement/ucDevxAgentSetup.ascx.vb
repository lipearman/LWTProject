Imports System.Data
Imports System.Web.Security
Imports Portal.Components

Imports DataBind
Imports DevExpress.Web

Partial Class Modules_ucDevxAgentSetup
    Inherits PortalModuleControl
    Protected Sub btnExpandAll_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExpandAll.Click
        gridAgent.DetailRows.ExpandAllRows()
    End Sub
    Protected Sub btnCollapseAll_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCollapseAll.Click
        gridAgent.DetailRows.CollapseAllRows()
    End Sub


    Protected Sub cbSaveAddAgent_Callback(ByVal source As Object, ByVal e As CallbackEventArgs) Handles cbSaveAddAgent.Callback

        Dim _dc As New DataClasses_CPSExt()
        Dim _data_Ag = (From c In _dc.Register_Agents Where c.Agent.Equals(GridAgentLookup.Value)).FirstOrDefault()
        If _data_Ag Is Nothing Then
            Dim _dc_SIBIS As New DataClasses_SIBISExt()
            Dim _data_SIBIS = (From c In _dc_SIBIS.Agents Where c.Agent.Equals(GridAgentLookup.Value)).FirstOrDefault()
            Dim _newData As New Register_Agent
            With _newData

                .Agent = _data_SIBIS.Agent
                .Name = _data_SIBIS.Name
                .Address1 = _data_SIBIS.Address1
                .Address2 = _data_SIBIS.Address2
                .Address3 = _data_SIBIS.Address3
                .PostCode = _data_SIBIS.PostCode
                .City = _data_SIBIS.City
                .PhoneBusiness = _data_SIBIS.PhoneBusiness
                .PhoneHome = _data_SIBIS.PhoneHome
                .ContactPerson = _data_SIBIS.ContactPerson
                .Addressee = _data_SIBIS.Addressee
                .Salutation = _data_SIBIS.Salutation
                .Occupation = _data_SIBIS.Occupation
                .EntryBy = _data_SIBIS.EntryBy
                .EntryDate = _data_SIBIS.EntryDate
                .IsSubAgent = _data_SIBIS.IsSubAgent
                .InternetAddress = _data_SIBIS.InternetAddress
                .CreationDate = DateTime.Now
                .CreationBy = HttpContext.Current.User.Identity.Name
                .RequestDate = _data_SIBIS.EntryDate
                .RequestBy = _data_SIBIS.EntryBy
                .ApproveDate = _data_SIBIS.EntryDate
                .ApproveBy = _data_SIBIS.EntryBy
                '.UserName = _data_SIBIS.UserName
                '.Password = _data_SIBIS.Password
                '.Email = _data_SIBIS.Email

                .IsActive = True


            End With
            _dc.Register_Agents.InsertOnSubmit(_newData)
            _dc.SubmitChanges()

            e.Result = "success"
        Else
            e.Result = String.Format("Agent {0} has already in system.", GridAgentLookup.Value)
        End If


        '        
        '


    End Sub



    ''btnAddUnderwriter
    'Protected Sub btnImportAgent_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnImportAgent.Click

    '    'ShortName.Text = ""
    '    'InsurerCode.Text = ""
    '    'GridUnderwriterLookup.Value = ""

    '    popUpAddAgent.ShowOnPageLoad = True
    '    GridAgentsLookup.DataBind()

    'End Sub


    ''btnSaveUnderwriter
    'Protected Sub btnSaveUnderwriter_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSaveAgent.Click

    '    Dim _dc As New DataClasses_CPSExt()
    '    Dim _dc_SIBIS As New DataClasses_SIBISExt()


    '    Dim _data_SIBIS = (From c In _dc_SIBIS.Agents Where c.Agent.Equals(GridAgentsLookup.Value)).FirstOrDefault()


    '    Dim _newData As New Register_Agent()

    '    With _newData
    '       .Agent =  _data_SIBIS.Agent
    '        .Name = _data_SIBIS.Name
    '        .Address1 = _data_SIBIS.Address1
    '        .Address2 = _data_SIBIS.Address2
    '        .Address3 = _data_SIBIS.Address3
    '        .PostCode = _data_SIBIS.PostCode
    '        .City = _data_SIBIS.City
    '        .PhoneBusiness = _data_SIBIS.PhoneBusiness
    '        .PhoneHome = _data_SIBIS.PhoneHome
    '        .ContactPerson = _data_SIBIS.ContactPerson
    '        .Addressee = _data_SIBIS.Addressee
    '        .Salutation = _data_SIBIS.Salutation
    '        .Occupation = _data_SIBIS.Occupation
    '        .EntryBy = _data_SIBIS.EntryBy
    '        .EntryDate = _data_SIBIS.EntryDate
    '        .IsSubAgent = _data_SIBIS.IsSubAgent
    '        .InternetAddress = _data_SIBIS.InternetAddress
    '        .CreationDate = DateTime.Now
    '        .CreationBy = HttpContext.Current.User.Identity.Name
    '        .RequestDate = _data_SIBIS.EntryDate
    '        .RequestBy = _data_SIBIS.EntryBy
    '        .ApproveDate = _data_SIBIS.EntryDate
    '        .ApproveBy = _data_SIBIS.EntryBy
    '        '.UserName = _data_SIBIS.UserName
    '        '.Password = _data_SIBIS.Password
    '        '.Email = _data_SIBIS.Email

    '    End With


    '    _dc.Register_Agents.InsertOnSubmit(_newData)

    '    _dc.SubmitChanges()

    '    grid.DataBind()

    'End Sub
End Class
