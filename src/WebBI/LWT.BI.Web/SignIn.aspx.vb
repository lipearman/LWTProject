Imports Portal.Components
Imports LWT.Website
Imports DevExpress.Web

Partial Class SignIn
    Inherits System.Web.UI.Page
    Public sitename As String
    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        ''Session("PageID") = Nothing
        ''Session("SignInTime") = DateTime.Now()

        ' ''FormsAuthentication.SetAuthCookie("Jira", True)
        'FormsAuthentication.SetAuthCookie("nuntiya", True)
        '' ''FormsAuthentication.SetAuthCookie("jlok", True)
        '' ''FormsAuthentication.SetAuthCookie("Wikran", True)
        FormsAuthentication.SetAuthCookie("dusit", True)
        'Response.Redirect(String.Format("~/DesktopDefault.aspx?pageid={0}", webconfig._DefaultPageID), False)

        Using dc As New DataClasses_PortalDataContextExt()

            Dim _data = (From c In dc.Portal_Users Where c.UserName.Equals("dusit") Select c).FirstOrDefault()


            Dim _defaultPage = (From c In dc.Portal_Users_DefaultPages Where c.PortalId.Equals(webconfig._PortalID) And c.UserId = _data.UserID).FirstOrDefault()
            If _defaultPage IsNot Nothing Then

                Dim defaulttab = (From c In dc.v_DesktopTabs Where c.TabId = _defaultPage.TabId And c.PortalId.Equals(webconfig._PortalID)).FirstOrDefault()


                Response.Redirect(String.Format("~/DesktopDefault.aspx?pageid={0}", defaulttab.PageId), False)

            Else
                Response.Redirect(String.Format("~/DesktopDefault.aspx?pageid={0}", webconfig._DefaultPageID), False)

            End If
        End Using



        'Response.Write(Portal.Components.Utils.Encrypt("RJ405T2P") & "<br>")

        'Response.End()


        'Dim dm As New Datamanage
        'Dim conn1 = ConfigurationManager.ConnectionStrings("PortalBIRawDataConnection").ConnectionString

        'dm.importToServer(Server.MapPath("~\App_Data\Modules\eGlobal\RawData\Daily_Claim nissan 27-30.07.60.xlsx"), _
        '                  conn1, "30B07F8B-75BE-486A-91A4-3FC60AAE1E64")

        'dm.importToServer(Server.MapPath("~\App_Data\Modules\eGlobal\RawData\THLOCAL20170831.xls"), _
        '                  conn1, "AC4C6E30-C5B2-4831-823B-959EBFBB6C52")

        'dm.importToServer(Server.MapPath("~\App_Data\Modules\eGlobal\RawData\Data001.xlsx"), _
        '                  conn1, "DAC31442-3E0F-4724-8D32-D5C5412C64BD")

        'dm.importToServer(Server.MapPath("~\App_Data\Modules\eGlobal\RawData\NTHLOCAL20170831.xls"), _
        '                  conn1, "00D8BDD9-B7A0-47CC-BA73-C2F955EA8389")

        'dm.importToServer(Server.MapPath("~\App_Data\Modules\eGlobal\RawData\THLOCAL201708311.xls"), _
        '                  conn1, "FA6BEB2E-06DD-4376-8DF6-55DF7D12C77C")

        '--~\App_Data\Modules\eGlobal\RawData\Daily_Claim nissan 27-30.07.60.xlsx	30B07F8B-75BE-486A-91A4-3FC60AAE1E64
        '--~\App_Data\Modules\eGlobal\RawData\THLOCAL20170831.xls	AC4C6E30-C5B2-4831-823B-959EBFBB6C52
        '--~\App_Data\Modules\eGlobal\RawData\Data001.xlsx	DAC31442-3E0F-4724-8D32-D5C5412C64BD
        '--~\App_Data\Modules\eGlobal\RawData\NTHLOCAL20170831.xls	00D8BDD9-B7A0-47CC-BA73-C2F955EA8389
        '~\App_Data\Modules\eGlobal\RawData\THLOCAL201708311.xls	FA6BEB2E-06DD-4376-8DF6-55DF7D12C77C



    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)

        sitename = portalSettings.PortalName

        LoginButton.Focus()


    End Sub


    Protected Sub cmdEnglish_Click(sender As Object, e As EventArgs) Handles cmdEnglish.Click
        Response.Redirect("SignIn-Eng.aspx")
    End Sub

    Protected Sub LoginButton_Click(sender As Object, e As EventArgs) Handles LoginButton.Click


        Using dc As New DataClasses_PortalDataContextExt()

            Dim _data = (From c In dc.Portal_Users Where c.UserName.Equals(UserName.Text) Select c).FirstOrDefault()
            If _data Is Nothing Then
                Password.Text = ""
                FailureText.Text = "No this user in System"
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

            If _data.Email.IndexOf("asia.lockton.com") > -1 Then 'LWT

                Dim _ws As New UACWebServices.WebService()

                _ws.Url = "http://172.16.40.234//LWTServices/WebService.asmx"

                If _ws.AuthenticateUser(webconfig._DirectoryDomain, UserName.Text, Password.Text, webconfig._DirectoryPath, Error_Message) Then

                    _data.LastLoginDate = Now()

                    dc.SubmitChanges()

                    dc.ExecuteCommand("insert into tblLogin_Log(UserName,IP,CREATEDATE) Values({0},{1},getdate())", UserName.Text, Context.Request.UserHostAddress)

                    Session("PageID") = ConfigurationSettings.AppSettings("DefaultLWTPageID")
                    Session("SignInTime") = DateTime.Now()

                    FormsAuthentication.SetAuthCookie(UserName.Text, True)

                    'Response.Redirect(String.Format("~/DesktopDefault.aspx?pageid={0}", webconfig._DefaultPageID), False)
                    'Response.Redirect("~/DesktopDefault.aspx", False)


                    Dim _defaultPage = (From c In dc.Portal_Users_DefaultPages Where c.PortalId.Equals(webconfig._PortalID) And c.UserId = _data.UserID).FirstOrDefault()
                    If _defaultPage IsNot Nothing Then

                        Dim defaulttab = (From c In dc.v_DesktopTabs Where c.TabId = _defaultPage.TabId And c.PortalId.Equals(webconfig._PortalID)).FirstOrDefault()

                         
                        Response.Redirect(String.Format("~/DesktopDefault.aspx?pageid={0}", defaulttab.PageId), False)

                    Else
                        Response.Redirect(String.Format("~/DesktopDefault.aspx?pageid={0}", webconfig._DefaultPageID), False)

                    End If


 

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

                    Session("SignInTime") = DateTime.Now()
                    Session("PageID") = webconfig._DefaultPageID

                    FormsAuthentication.SetAuthCookie(UserName.Text, True)

                    Response.Redirect("~/DesktopDefault.aspx", False)

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

