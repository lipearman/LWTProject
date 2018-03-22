'Imports BaseConfig
Imports Portal.Components
Imports LWT.Website

Partial Class SignIn
    Inherits System.Web.UI.Page
    Public sitename As String
    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        ' ''Session("SignInTime") = Nothing
        Session("SignInTime") = DateTime.Now()
        'FormsAuthentication.SetAuthCookie("jutamas@ms-ins.co.th", RememberMe.Checked)
        FormsAuthentication.SetAuthCookie("dusit", RememberMe.Checked)
        'FormsAuthentication.SetAuthCookie("mongkon_k@navakij.co.th", RememberMe.Checked)
        Response.Redirect(String.Format("~/DesktopDefault.aspx?pageid={0}", webconfig._DefaultPageID), False)




    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
        sitename = portalSettings.PortalName

    End Sub


    Protected Sub LoginButton_Click(sender As Object, e As EventArgs)



        If HttpContext.Current.Session("qaptcha_key") Is Nothing Then
            Password.Text = ""
            'FailureText.Text = "Locked : form can't be submited"
            Return
        End If


        Using dc As New DataClasses_PortalDataContext(webconfig._PortalConn)

            Dim _data = (From c In dc.Portal_Users Where c.UserName.Equals(UserName.Text) Select c).FirstOrDefault()
            If _data Is Nothing Then
                Password.Text = ""
                FailureText.Text = "No this user in System"
                Return
            End If

            Session("qaptcha_key") = Nothing

            If Not _data.IsApproved Then
                FailureText.Text = ("User is not approved")
                Return
            End If

            If _data.IsLocked Then
                FailureText.Text = ("User is Locked")
                Return
            End If

            Dim Error_Message As String = ""

            'If Portal.Components.Utils.AuthenticateUser(webconfig._DirectoryDomain, UserName.Text, Password.Text, webconfig._DirectoryPath, Error_Message) Then

            '    Session("SignInTime") = DateTime.Now()

            '    FormsAuthentication.SetAuthCookie(UserName.Text, RememberMe.Checked)
            '    Response.Redirect(String.Format("~/DesktopDefault.aspx?pageid={0}", webconfig._DefaultPageID), False)


            'Else
            '    Password.Text = ""
            '    FailureText.Text = (Error_Message)
            'End If



            If _data.Email.IndexOf("asia.lockton.com") > -1 Then 'LWT

                Dim _ws As New UACWebServices.WebService()
                _ws.Url = "http://172.16.40.234//LWTServices/WebService.asmx"

                If _ws.AuthenticateUser(webconfig._DirectoryDomain, UserName.Text, Password.Text, webconfig._DirectoryPath, Error_Message) Then
                    _data.LastLoginDate = Now()
                    dc.SubmitChanges()


                    dc.ExecuteCommand("insert into tblLogin_Log(UserName,IP,CREATEDATE) Values({0},{1},getdate())", UserName.Text, Context.Request.UserHostAddress)

                    Session("qaptcha_key") = Nothing

                    Session("SignInTime") = DateTime.Now()
                    FormsAuthentication.SetAuthCookie(UserName.Text, RememberMe.Checked)
                    Response.Redirect(String.Format("~/DesktopDefault.aspx?pageid={0}", webconfig._DefaultPageID), False)

                Else
                    Password.Text = ""
                    FailureText.Text = (Error_Message)
                End If


            Else
                If Not _data.Password.Equals(Portal.Components.Utils.Encrypt(Password.Text)) Then


                    If Session("LoginFailCount") Is Nothing Then
                        Session("LoginFailCount") = 0
                    End If
                    Session("LoginFailCount") = Session("LoginFailCount") + 1
                    If Session("LoginFailCount") > 6 Then
                        _data.IsLocked = True
                        dc.SubmitChanges()

                        Session("LoginFailCount") = 0

                        FailureText.Text = "User Is Locked"
                    Else
                        FailureText.Text = "Login fail"
                        'Portal.Components.Utils.Encrypt(Password.Text) '
                    End If



                    Password.Text = ""
                    Return


                Else

                    _data.LastLoginDate = Now()
                    dc.SubmitChanges()


                    dc.ExecuteCommand("insert into tblLogin_Log(UserName,IP,CREATEDATE) Values({0},{1},getdate())", UserName.Text, Context.Request.UserHostAddress)

                    Session("qaptcha_key") = Nothing

                    Session("SignInTime") = DateTime.Now()
                    FormsAuthentication.SetAuthCookie(UserName.Text, RememberMe.Checked)
                    Response.Redirect(String.Format("~/DesktopDefault.aspx?pageid={0}", webconfig._DefaultPageID), False)
                End If

            End If





        End Using
    End Sub
End Class
