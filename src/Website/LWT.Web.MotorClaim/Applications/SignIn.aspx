<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SignIn.aspx.vb" Inherits="SignIn" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
     <link href="favicon.ico" rel="shortcut icon">
    <title><%=sitename %></title>
    <%--<link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />--%>
    <link href="~/css/login.css" rel="stylesheet" />


    <!-- include CSS & JS files -->
    <!-- CSS file -->
    <link rel="stylesheet" type="text/css" href="jquery/QapTcha.jquery.css" media="screen" />

    <!-- jQuery files -->
    <script type="text/javascript" src="jquery/jquery.js"></script>
    <script type="text/javascript" src="jquery/jquery-ui.js"></script>
    <script type="text/javascript" src="jquery/jquery.ui.touch.js"></script>
    <script type="text/javascript" src="jquery/QapTcha.jquery.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('.QapTcha').QapTcha(
                    {
                        txtLock : 'Locked : Please slide.',
                        txtUnlock: '',
                        autoSubmit : false,
                        disabledSubmit: true,
                        autoRevert: true
                     
                    }
                );
        });

        //txtLock : 'Locked : form can\'t be submited',
        //txtUnlock : 'Unlocked : form can be submited',
        //disabledSubmit : true,
        //autoRevert : true,
        //PHPfile: './Qaptcha.ashx',
        //autoSubmit : false

    </script>

</head>
<body>
    <form id="Form1" runat="server">

 
<br /><br />
        <section class="container"  >
    <div class="login">
      <h1>Login to <%=sitename %></h1>
      
        <p > 
              <asp:TextBox ID="UserName" runat="server" name="login"   placeholder="UserName"   ></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UserNameRequired" ForeColor="Red" runat="server" Display="Dynamic" ControlToValidate="UserName"
                                CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required."
                                ValidationGroup="LoginUserValidationGroup"></asp:RequiredFieldValidator>

        </p>
        <p>
             <asp:TextBox ID="Password" runat="server" name="password" TextMode="Password" placeholder="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordRequired" ForeColor="Red" runat="server" ControlToValidate="Password" Display="Dynamic"
                                CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                                ValidationGroup="LoginUserValidationGroup"></asp:RequiredFieldValidator>

      

         <div class="QapTcha"></div>
       </p>
        <p class="remember_me">
          
             
        </p>
        <p class="submit"> 
          <Table>
              <tr>
                  <td><label> <asp:CheckBox ID="RememberMe" runat="server" Text="Remember me on this computer" /></label></td>
                  <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                  <td> <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Login" ValidationGroup="LoginUserValidationGroup" OnClick="LoginButton_Click" />
                  </td>
              </tr>
          </Table>
           
        </p>

          <p style="color:red">
              <asp:Literal ID="FailureText" runat="server" ></asp:Literal>
          </p>
      
    </div>

    <div class="login-help">
      <p> © 2012-<%=Year(DateTime.Now())%> Lockton Wattana Insurance Brokers (Thailand) Ltd. All Rights Reserved.</p>
    </div>
  </section>


        <%--
        <div class="page">
            <div class="header">
                <div class="title">
                    <table>
                        <tr>
                            <td>
                                <img src="images/DispLogo.gif" alt="Lockton Logo" height="50">
                            </td>
                            <td>
                                <h1> </h1>
                            </td>

                        </tr>

                    </table>

                </div>
                <div class="loginDisplay">
                </div>
                <div class="clear hideSkiplink">
                </div>
            </div>
            <div class="main">
                <h2>Log In
                </h2>
                <p>
                    Please enter your username and password.
        <a id="RegisterHyperLink" href="Register.aspx">Register</a>
                    if you don't have an account.
                </p>
                <div class="accountInfo">
                    <span class="failureNotification">
                        <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                    </span>

                    <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
                        ValidationGroup="LoginUserValidationGroup" />


                    <fieldset class="login">
                        <legend>Account Information</legend>
                        <p>
                            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">Username:</asp:Label>

                            <asp:TextBox ID="UserName" runat="server" CssClass="textEntry"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                                CssClass="failureNotification" ErrorMessage="User Name is required." ToolTip="User Name is required."
                                ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>

                        </p>
                        <p>
                            <asp:Label ID="PasswordLabel" runat="server" AssociatedControlID="Password">Password:</asp:Label>
                            <asp:TextBox ID="Password" runat="server" CssClass="passwordEntry" TextMode="Password"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="PasswordRequired" runat="server" ControlToValidate="Password"
                                CssClass="failureNotification" ErrorMessage="Password is required." ToolTip="Password is required."
                                ValidationGroup="LoginUserValidationGroup">*</asp:RequiredFieldValidator>
                        </p>
                        <p>
                            <asp:CheckBox ID="RememberMe" runat="server" />
                            <asp:Label ID="RememberMeLabel" runat="server" AssociatedControlID="RememberMe" CssClass="inline">Keep me logged in</asp:Label>

                        </p>
                    </fieldset>
                    <p class="submitButton">
                        <asp:Button ID="LoginButton" runat="server" CommandName="Login" Text="Log In" ValidationGroup="LoginUserValidationGroup" OnClick="LoginButton_Click" />
                    </p>

                </div>
            </div>
            <div class="clear">
            </div>
        </div>
        <div class="footer">
        </div>--%>
    </form>
</body>
</html>
