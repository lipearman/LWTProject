Public Class Logoff
    Inherits BasePage
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        FormsAuthentication.SignOut()
        Session("CustomPrincipal") = Nothing
        HttpContext.Current.Response.Clear()
        Response.Redirect("~/SignIn.aspx")
        'Response.Redirect("~/Login.aspx")
    End Sub

End Class