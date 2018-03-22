Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports BaseConfig 
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports DevExpress.Web.ASPxTreeList
Imports System.IO

Partial Class Modules_ucDevxMotorCampaignAssignment
    Inherits PortalModuleControl
    Private RiskGroupID As Integer = 3

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        SqlDataSource_Project.SelectParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name
        SqlDataSource_MotorCampaign.SelectParameters("RiskGroupID").DefaultValue = RiskGroupID

        If (Not IsPostBack) Then

        End If
    End Sub

    Protected Sub cbCampaign_Callback(ByVal source As Object, ByVal e As CallbackEventArgs) Handles cbCampaign.Callback
        'Dim _campaignid = e.Parameter.ToString()
        Dim _campaignid As String = gridCampaign.GridView.GetRowValues(gridCampaign.GridView.FocusedRowIndex, gridCampaign.KeyFieldName).ToString()

        Session("CampaignID") = _campaignid

        Dim _dc As New DataClasses_CPSExt()
        Dim _projects = (From c In _dc.tblProjectCampaigns Where c.CampaignID.Equals(_campaignid)).ToList()
        If _projects.Count > 0 Then
            Dim sb As New StringBuilder()
            For Each _item In _projects
                sb.Append(_item.ProjectID.ToString().Trim() & ";")
            Next
            e.Result = sb.ToString()
        Else
            e.Result = ""
        End If

    End Sub

    Protected Sub cbSaveAddProject_Callback(ByVal source As Object, ByVal e As CallbackEventArgs) Handles cbSaveAddProject.Callback
        'Dim _campaignid = Session("CampaignID")
        Dim _campaignid As String = gridCampaign.GridView.GetRowValues(gridCampaign.GridView.FocusedRowIndex, gridCampaign.KeyFieldName).ToString()

        Dim _param = e.Parameter.ToString().Split(",")
        Dim _username = HttpContext.Current.User.Identity.Name
        Dim _projects As New List(Of Integer)

        For Each idx In _param
            If Not String.IsNullOrEmpty(idx) Then
                _projects.Add(ProjectList.Items(idx).Value)
            End If
        Next


        Using _dc As New DataClasses_CPSExt()
            Dim _userproject = (From c In _dc.V_UserProjects Where c.UserName.Equals(_username) Select c.ProjectID).ToList()

            Dim _data = (From c In _dc.tblProjectCampaigns Where _userproject.Contains(c.ProjectID) And c.CampaignID.Equals(_campaignid)).ToList()
            _dc.tblProjectCampaigns.DeleteAllOnSubmit(_data)

            _dc.SubmitChanges()

            If _projects.Count > 0 Then
                Dim _newdata As New List(Of tblProjectCampaign)
                For Each pid In _projects
                    _newdata.Add(New tblProjectCampaign With {.IsActive = True, .ProjectID = pid, .CampaignID = _campaignid})
                Next
                _dc.tblProjectCampaigns.InsertAllOnSubmit(_newdata)

                _dc.SubmitChanges()
            End If

            e.Result = "success"
        End Using
      




    End Sub





    Protected Sub callbackPanel_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase) Handles callbackPanel.Callback

        'Dim _cmd = e.Parameter.ToString()

        'If Not String.IsNullOrEmpty(_cmd) Then

        '    Dim _params = _cmd.Split("|")
        '    Select Case _params(0)

        '        Case "select_campaign"
        '            Dim _CampaignID As String = gridCampaign.GridView.GetRowValues(gridCampaign.GridView.FocusedRowIndex, gridCampaign.KeyFieldName).ToString()
        '            Session("CampaignID") = _CampaignID

        '            Dim _dc As New DataClasses_CPSExt()
        '            Dim _projects = (From c In _dc.tblProjectCampaigns Where c.CampaignID.Equals(_CampaignID)).ToList()
        '            If _projects.Count > 0 Then
        '                Dim sb As New StringBuilder()
        '                For Each _item In _projects
        '                    sb.Append(_item.ProjectID.ToString().Trim() & ";")
        '                Next

        '            End If

        '    End Select

        'End If


    End Sub


End Class
