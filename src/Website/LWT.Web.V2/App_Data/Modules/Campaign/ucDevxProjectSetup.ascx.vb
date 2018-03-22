Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports BaseConfig 
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports DevExpress.Web.ASPxTreeList

Partial Class Modules_ucDevxProjectSetup
    Inherits PortalModuleControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        'If (Not IsPostBack) Then

        SqlDataSource_Project.SelectParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name
        SqlDataSource_Project.InsertParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name
        SqlDataSource_Project.UpdateParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name

        SqlDataSource_AE.SelectParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name

        'End If



        'Dim editMode = CType(System.Enum.Parse(GetType(CardViewEditingMode), "EditForm"), CardViewEditingMode)
        'CardView.SettingsEditing.Mode = editMode
    End Sub
    Protected Sub grid_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles GridViewProject.InitNewRow
        e.NewValues("IsActive") = True
    End Sub

    ''CardView
    'Protected Sub CardView_InitNewCard(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles CardView.InitNewCard
    '    e.NewValues("IsActive") = True
    'End Sub
    Protected Sub cbProject_Callback(ByVal source As Object, ByVal e As CallbackEventArgs) Handles cbProject.Callback
        Dim _RiskGroupID = e.Parameter.ToString()

        Session("RiskGroupID") = _RiskGroupID
 

    End Sub
End Class
