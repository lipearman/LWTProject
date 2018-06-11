
Imports Portal.Components
Imports LWT.Website
Imports iTextSharp.text.pdf

Partial Class _Default
    Inherits System.Web.UI.Page
    Public sitename As String
    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Session("SignInTime") = DateTime.Now()

        'FormsAuthentication.SetAuthCookie("jintarat", RememberMe.Checked)
        'Response.Redirect("~/DesktopDefault.aspx", False)

        'Dim myuser As MbUser = MbUser.GetUserByUsername("lalana")
        'If myuser IsNot Nothing Then
        '    Session("u") = myuser
        'End If

        'Response.Redirect("~/SignIn.aspx", False)

        'Dim myString = "http://www.MySampleCode.com"


        'Response.Write(Today.Date.AddDays(-2).ToString("dddd"))
        'Response.End()





        'Response.Write(Portal.Components.Utils.Encrypt("07am6d0n") & "<br>")

        'Response.End()
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
        sitename = portalSettings.PortalName

    End Sub


    Protected Sub LoginButton_Click(sender As Object, e As EventArgs)


        Using dc As New DataClasses_PortalDataContextExt()

            Dim _data = (From c In dc.Portal_Users Where c.UserName.Equals(UserName.Text) Select c).FirstOrDefault()
            If _data Is Nothing Then
                Password.Text = ""
                FailureText.Text = "No this user in database"
                Return
            End If


            If Not _data.IsApproved Then
                FailureText.Text = ("User is not approved")
                Return
            End If

            If _data.IsLocked Then
                FailureText.Text = ("User is Locked")
                Return
            End If

            Dim Error_Message As String = ""
            If Portal.Components.Utils.AuthenticateUser(webconfig._DirectoryDomain, UserName.Text, Password.Text, webconfig._DirectoryPath, Error_Message) Then
                Session("SignInTime") = DateTime.Now()

                FormsAuthentication.SetAuthCookie(UserName.Text, RememberMe.Checked)

                'Dim myuser As MbUser = MbUser.GetUserByUsername(UserName.Text)
                'If myuser IsNot Nothing Then
                '    Session("u") = myuser
                '    'Session.Timeout = DateAdd(DateInterval.Hour, 3, DateTime.Now())
                'End If

                Response.Redirect("~/DesktopDefault.aspx", False)
            Else
                Password.Text = ""
                FailureText.Text = (Error_Message)
            End If

        End Using
    End Sub
End Class
