
Partial Class Logoff
    Inherits System.Web.UI.Page
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

       
        FormsAuthentication.SignOut()
        Session("CustomPrincipal") = Nothing
        HttpContext.Current.Response.Clear()
        Response.Redirect("~/Default.aspx")



    End Sub
End Class
