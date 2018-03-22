Imports Portal.Components

Public Class SignIn
    Inherits System.Web.UI.Page
    Public sitename As String

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Session("SignInTime") = Nothing

        FormsAuthentication.SetAuthCookie("dusit", RememberMe.Checked)
        Response.Redirect(String.Format("~/DesktopDefault.aspx?pageid={0}", webconfig._DefaultPageID), False)


    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
        sitename = portalSettings.PortalName

    End Sub


    Protected Sub LoginButton_Click(sender As Object, e As EventArgs) Handles LoginButton.Click
        'Dim _ws As New UACServices.WebService()
        '_ws.Url = "http://172.16.40.128/uac/ws/WebService.asmx"
        '_ws.AuthenticateUser(webconfig._DirectoryDomain, UserName.Text, Password.Text, webconfig._DirectoryPath, Error_Message)

        Using dc As New DataClasses_PortalDataContext(webconfig._PortalConn)

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

            ''If Portal.Components.Utils.AuthenticateUser(webconfig._DirectoryDomain, UserName.Text, Password.Text, webconfig._DirectoryPath, Error_Message) Then
            'Dim _ws As New LWTServices.WebService
            '_ws.Url = "http://lockthbnk-db07/LWTServices/WebService.asmx"

            'If _ws.AuthenticateUser(webconfig._DirectoryDomain, UserName.Text, Password.Text, webconfig._DirectoryPath, Error_Message) Then

            '    Session("SignInTime") = DateTime.Now()

            '    FormsAuthentication.SetAuthCookie(UserName.Text, RememberMe.Checked)

            '    'Dim myuser As MbUser = MbUser.GetUserByUsername(UserName.Text)
            '    'If myuser IsNot Nothing Then
            '    '    Session("u") = myuser
            '    '    'Session.Timeout = DateAdd(DateInterval.Hour, 3, DateTime.Now())
            '    'End If

            '    Response.Redirect("~/DesktopDefault.aspx", False)
            'Else
            '    Password.Text = ""
            '    FailureText.Text = (Error_Message)
            'End If

            If Utils.AuthenticateUser(webconfig._DirectoryDomain, UserName.Text, Password.Text, webconfig._DirectoryPath, Error_Message) Then
                Session("SignInTime") = DateTime.Now()
                FormsAuthentication.SetAuthCookie(UserName.Text, RememberMe.Checked)
                'PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference())

                Response.Redirect(String.Format("~/DesktopDefault.aspx?pageid={0}", webconfig._DefaultPageID), False)

            Else
                Password.Text = ""
                FailureText.Text = (Error_Message)
            End If
        End Using
    End Sub

End Class