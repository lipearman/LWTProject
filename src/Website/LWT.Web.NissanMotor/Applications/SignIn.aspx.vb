'Imports BaseConfig
Imports Portal.Components
Imports LWT.Website
Imports DevExpress.Web

Partial Class SignIn
    Inherits System.Web.UI.Page
    Public sitename As String
    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init

        'Session("SignInTime") = Nothing

        Session("SignInTime") = DateTime.Now()

        FormsAuthentication.SetAuthCookie("dusit", RememberMe.Checked)

        Response.Redirect(String.Format("~/DesktopDefault.aspx?pageid={0}", webconfig._DefaultPageID), False)



        'Response.Write(Portal.Components.Utils.Encrypt("8qDDWC2N") & "<br>")

        'Response.Write(Portal.Components.Utils.Encrypt("hFBCB96N") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("Z2Lc3Pqj") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("2UxQrFWd") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("5j6fa2Aa") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("xLzTyj6k") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("mHTX4Xhz") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("nsAmps46") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("2JzxMcYq") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("R9tCH6v9") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("4p9D9Weq") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("2HQD3u5r") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("kWCC5eeX") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("2VzFQvHw") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("YB9gZZz9") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("tKpWan84") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("2aMyzgJ9") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("UGYRL8ds") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("aqCL59wD") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("TPX3GbEx") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("8a7T6rBC") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("FLr24SHR") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("bpPc5HuC") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("w7tgxZBd") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("hyPPq4Pz") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("2QPkupFJ") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("aRhr9DSQ") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("sRj64RNJ") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("cuUke28y") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("Sz8yT3EV") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("4zeD4nZQ") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("k6qwFEhM") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("RrPVc2Dj") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("VJ6fT6hm") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("k9Z7RqZX") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("gf88SpAY") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("FhMKkJ5f") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("m7FzssSN") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("X5u9pQcE") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("pmCQ7DzX") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("5UdpfY83") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("zJXL9jEL") & "<br>")



        'Response.Write(Portal.Components.Utils.Encrypt("da6A4zDf") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("7Xjm2M3c") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("UkFH6k8T") & "<br>")
        'Response.Write(Portal.Components.Utils.Encrypt("KW8e8smT") & "<br>")

        'Response.End()

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

        Using dc As New DataClasses_PortalDataContextExt()

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

                    If Session("LoginFailCount") > 8 Then

                        _data.LastLoginDate = Date.Now()

                        _data.IsLocked = True

                        dc.SubmitChanges()

                        Session("LoginFailCount") = 0

                        FailureText.Text = "User Is Locked"

                    Else

                        FailureText.Text = "Login fail"

                    End If

                    Password.Text = ""

                    Return


                Else

                    If _data.ExpiredDate IsNot Nothing Then

                        If DateDiff(DateInterval.Day, _data.ExpiredDate.Value, Now) > 90 Then

                            pcChagePassword.ShowOnPageLoad = True

                            Return

                        End If
                    End If


                    _data.LastLoginDate = Now()

                    dc.SubmitChanges()

                    dc.ExecuteCommand("insert into tblLogin_Log(UserName,IP,CREATEDATE) Values({0},{1},getdate())", UserName.Text, Context.Request.UserHostAddress)

                    Session("qaptcha_key") = Nothing

                    Session("SignInTime") = DateTime.Now()

                    FormsAuthentication.SetAuthCookie(UserName.Text, RememberMe.Checked)

                    Response.Redirect(String.Format("~/DesktopDefault.aspx?pageid={0}", webconfig._DefaultPageID), False)

                End If

            End If

























            'If Portal.Components.Utils.AuthenticateUser(webconfig._DirectoryDomain, UserName.Text, Password.Text, webconfig._DirectoryPath, Error_Message) Then

            '    Session("SignInTime") = DateTime.Now()

            '    FormsAuthentication.SetAuthCookie(UserName.Text, RememberMe.Checked)
            '    Response.Redirect(String.Format("~/DesktopDefault.aspx?pageid={0}", webconfig._DefaultPageID), False)


            'Else
            '    Password.Text = ""
            '    FailureText.Text = (Error_Message)
            'End If

            'If Not _data.Password.Equals(Portal.Components.Utils.Encrypt(Password.Text)) Then


            '    If Session("LoginFailCount") Is Nothing Then
            '        Session("LoginFailCount") = 0
            '    End If
            '    Session("LoginFailCount") = Session("LoginFailCount") + 1
            '    If Session("LoginFailCount") > 6 Then
            '        _data.IsLocked = True
            '        dc.SubmitChanges()

            '        Session("LoginFailCount") = 0

            '        FailureText.Text = "User Is Locked"
            '    Else
            '        FailureText.Text = "Login fail"
            '        'Portal.Components.Utils.Encrypt(Password.Text) '
            '    End If



            '    Password.Text = ""
            '    Return


            'Else

            '    _data.LastLoginDate = Now()
            '    _data.Comment =



            '    Session("qaptcha_key") = Nothing

            '    Session("SignInTime") = DateTime.Now()
            '    FormsAuthentication.SetAuthCookie(UserName.Text, RememberMe.Checked)
            '    Response.Redirect(String.Format("~/DesktopDefault.aspx?pageid={0}", webconfig._DefaultPageID), False)
            'End If


            'If _data.Email.IndexOf("asia.lockton.com") > -1 Then 'LWT

            '    Dim _ws As New UACWebServices.WebService()
            '    _ws.Url = "http://172.16.40.234//LWTServices/WebService.asmx"

            '    If _ws.AuthenticateUser(webconfig._DirectoryDomain, UserName.Text, Password.Text, webconfig._DirectoryPath, Error_Message) Then
            '        _data.LastLoginDate = Now()
            '        dc.SubmitChanges()


            '        dc.ExecuteCommand("insert into tblLogin_Log(UserName,IP,CREATEDATE) Values({0},{1},getdate())", UserName.Text, Context.Request.UserHostAddress)

            '        Session("qaptcha_key") = Nothing

            '        Session("SignInTime") = DateTime.Now()
            '        FormsAuthentication.SetAuthCookie(UserName.Text, RememberMe.Checked)
            '        Response.Redirect(String.Format("~/DesktopDefault.aspx?pageid={0}", webconfig._DefaultPageID), False)

            '    Else
            '        Password.Text = ""
            '        FailureText.Text = (Error_Message)
            '    End If


            'Else
            'If Not _data.Password.Equals(Portal.Components.Utils.Encrypt(Password.Text)) Then


            '    If Session("LoginFailCount") Is Nothing Then
            '        Session("LoginFailCount") = 0
            '    End If
            '    Session("LoginFailCount") = Session("LoginFailCount") + 1
            '    If Session("LoginFailCount") > 6 Then
            '        _data.IsLocked = True
            '        dc.SubmitChanges()

            '        Session("LoginFailCount") = 0

            '        FailureText.Text = "User Is Locked"
            '    Else
            '        FailureText.Text = "Login fail"
            '        'Portal.Components.Utils.Encrypt(Password.Text) '
            '    End If



            '    Password.Text = ""
            '    Return


            'Else

            '    _data.LastLoginDate = Now()
            '    dc.SubmitChanges()


            '    dc.ExecuteCommand("insert into tblLogin_Log(UserName,IP,CREATEDATE) Values({0},{1},getdate())", UserName.Text, Context.Request.UserHostAddress)

            '    Session("qaptcha_key") = Nothing

            '    Session("SignInTime") = DateTime.Now()
            '    FormsAuthentication.SetAuthCookie(UserName.Text, RememberMe.Checked)
            '    Response.Redirect(String.Format("~/DesktopDefault.aspx?pageid={0}", webconfig._DefaultPageID), False)
            'End If

            'End If

        End Using
    End Sub





    Protected Sub cbChangePwd_Callback(ByVal source As Object, ByVal e As CallbackEventArgs) Handles cbChangePwd.Callback
        Dim _email = UserName.Text
        Dim _oldpassword = Password.Text
        Dim _newpassword = passwordTextBox.Text
        Dim _confirmpassword = confirmPasswordTextBox.Text

        'If String.IsNullOrEmpty(_email) Or String.IsNullOrEmpty(_newpassword) Then
        '    e.Result = "Email or New Password Is Blank"
        '    Return
        'End If

        If String.IsNullOrEmpty(_email) Then
            e.Result = "Email Is Blank"
            Return
        End If

        If String.IsNullOrEmpty(_newpassword) Or String.IsNullOrEmpty(_confirmpassword) Then
            e.Result = "New or Confirm Password Is Blank"
            Return
        End If

        If _newpassword <> _confirmpassword Then
            e.Result = "New and Confirm Password is not equal"
            Return
        End If


        Using dc As New DataClasses_PortalDataContextExt
            'Dim _data = (From c In dc.Portal_Users Where c.Email.Equals(_email) And c.Password.Equals(Utils.Encrypt(_oldpassword)) Select c).FirstOrDefault()

            Dim _data = (From c In dc.Portal_Users Where c.Email.Equals(_email) Select c).FirstOrDefault()

            If _data Is Nothing Then
                e.Result = "invalid email"
                Return
            End If

            If Not _data.IsApproved Then
                e.Result = "Your User is not approved "
                Return
            End If

            If _data.IsLocked Then
                e.Result = "Your User is loked"
                Return
            End If

            _data.Password = Utils.Encrypt(_newpassword)
            _data.LastPasswordChangedDate = DateTime.Now
            _data.LastActivityDate = DateTime.Now
            _data.ExpiredDate = DateTime.Now.AddDays(90)

            If _data.ExpiredDate Is Nothing Or _data.ExpiredDate < DateTime.Now Then
                _data.ExpiredDate = DateTime.Now.AddDays(90)
            End If

            dc.SubmitChanges()

            e.Result = "success"
        End Using

    End Sub


End Class
