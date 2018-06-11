Imports System.Data.SqlClient
Imports System.Data
Imports LWT.Website
Imports Portal.Components
Partial Class Passport
    Inherits System.Web.UI.Page


    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Init
        Dim _loginpage = "~/SignIn.aspx"

        If Not IsPostBack Then

            Using dc As New DataClasses_PortalDataContext(webconfig._PortalConn)

                Dim pid = Request("pid")

                If Not pid Is Nothing Then

                    Dim _data = (From c In dc.SingleSignOn_Passports Where c.Passport.Equals(pid)).FirstOrDefault()

                    If _data IsNot Nothing Then
                        Session("PageID") = ConfigurationSettings.AppSettings("DefaultLWTPageID")
                        Session("SignInTime") = DateTime.Now()
                        FormsAuthentication.SetAuthCookie(_data.UserName, False)
                        Response.Redirect("~/DesktopDefault.aspx", False)

                    End If

                Else
                    'go to login page
                    Response.Redirect(_loginpage)
                End If
            End Using



        Else
            'go to login page
            Response.Redirect(_loginpage)




        End If

    End Sub
End Class
