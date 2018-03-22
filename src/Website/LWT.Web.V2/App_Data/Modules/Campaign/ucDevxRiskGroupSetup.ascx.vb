Imports System.Data
Imports System.Web.Security
Imports Portal.Components

'Imports DataBind
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports DevExpress.Web.ASPxTreeList
Imports System.IO

Partial Class Modules_ucDevxRiskGroupSetup
    Inherits PortalModuleControl
    'Private RiskGroupID As Integer = 40

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        SqlDataSource_RiskGroup.InsertParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name
        SqlDataSource_RiskGroup.UpdateParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name

        If (Not IsPostBack) Then


        End If
    End Sub

     

End Class
