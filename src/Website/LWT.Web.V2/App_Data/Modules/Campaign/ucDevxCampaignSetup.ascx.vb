Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports BaseConfig 
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports DevExpress.Web.ASPxTreeList

Partial Class Modules_ucDevxCampaignSetup
    Inherits PortalModuleControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        SqlDataSource_Campaign.SelectParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name
        SqlDataSource_Campaign.InsertParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name
        SqlDataSource_Campaign.UpdateParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name
    End Sub
    Protected Sub grid_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles GridViewCampaign.InitNewRow
        e.NewValues("IsActive") = True
        e.NewValues("RenewalYear") = 0
    End Sub
    Protected Sub cbCampaign_Callback(ByVal source As Object, ByVal e As CallbackEventArgs) Handles cbCampaign.Callback
        Dim _RiskGroupID = e.Parameter.ToString()
        Session("RiskGroupID") = _RiskGroupID
    End Sub
End Class
