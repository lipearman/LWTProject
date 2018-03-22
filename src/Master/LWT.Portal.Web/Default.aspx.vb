Public Class _Default
    Inherits System.Web.UI.Page
    Public sitename As String
    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
         
        Response.Redirect("~/SignIn.aspx", False)

    End Sub

End Class