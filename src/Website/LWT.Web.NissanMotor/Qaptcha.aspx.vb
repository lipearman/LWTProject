
Partial Class Qaptcha
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Response.ContentType = "text/plain"
        Try

            Dim qaptcha_key = Request.Form("qaptcha_key")
            Session("qaptcha_key") = qaptcha_key
            Response.Write("{""error"":0}")
        Catch ex As Exception
            Response.Write("{""error"":1}")
        End Try

        Response.End()
    End Sub
End Class
