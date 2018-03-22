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
                        txtLock: 'Locked : Please slide.',
                        txtUnlock: '',
                        autoSubmit: false,
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
    <style type="text/css">
        .formLayoutContainer {
            width: 760px;
            margin: auto;
        }

        .layoutGroupBoxCaption {
            font-size: 16px;
        }

    .btnchangepwd {
      padding: 0 18px;
      height: 29px;
      font-size: 12px;
      font-weight: bold;
      color: #527881;
      text-shadow: 0 1px #e3f1f1;
      background: #cde5ef;
      border: 1px solid;
      border-color: #b4ccce #b3c0c8 #9eb9c2;
      border-radius: 16px;
      outline: 0;
      -webkit-box-sizing: content-box;
      -moz-box-sizing: content-box;
      box-sizing: content-box;
      background-image: -webkit-linear-gradient(top, #edf5f8, #cde5ef);
      background-image: -moz-linear-gradient(top, #edf5f8, #cde5ef);
      background-image: -o-linear-gradient(top, #edf5f8, #cde5ef);
      background-image: linear-gradient(to bottom, #edf5f8, #cde5ef);
      -webkit-box-shadow: inset 0 1px white, 0 1px 2px rgba(0, 0, 0, 0.15);
      box-shadow: inset 0 1px white, 0 1px 2px rgba(0, 0, 0, 0.15);
    }

.txtpassword {
  margin: 5px;
  padding: 0 10px;
  width: 200px;
  height: 34px;
  color: #404040;
  background: white;
  border: 1px solid;
  border-color: #c4c4c4 #d1d1d1 #d4d4d4;
  border-radius: 2px;
  outline: 5px solid #eff4f7;
  -moz-outline-radius: 3px;
  -webkit-box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.12);
  box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.12);
}
    </style>
</head>
<body>
    <form id="Form1" runat="server">
        
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Modal="True">
        </dx:ASPxLoadingPanel>

        <br />
        <br />
        <section class="container">
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



        <dx:ASPxPopupControl ID="pcChagePassword"
            ClientInstanceName="pcChagePassword"
            HeaderText="Change Password"
            ShowFooter="false"
            ShowCloseButton="false"
            ShowCollapseButton="false"
            ShowMaximizeButton="false"
            ShowHeader="false"
            ShowShadow="false"
            ShowPinButton="false"
            ShowRefreshButton="false"
            ShowSizeGrip="False"
            runat="server"
            PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter"
            AllowDragging="false"
            Modal="True"
            EnableViewState="False">
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">

                    <dx:ASPxFormLayout ID="ASPxFormLayout1" 
                        runat="server"
                        RequiredMarkDisplayMode="None"
                        Styles-LayoutGroupBox-Caption-CssClass="layoutGroupBoxCaption" 
                        Width="100%">
                        <Items>

                            <dx:LayoutGroup Caption="Change Password" GroupBoxDecoration="HeadingLine" SettingsItemCaptions-HorizontalAlign="Right" ColCount="2">
                                <Items>
                                    <dx:LayoutItem ShowCaption="False">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxTextBox ID="passwordTextBox"  CssClass="txtpassword" MaxLength="12"
                                                    NullText="New Password" runat="server" ToolTip="New Password"
                                                    ClientInstanceName="passwordTextBox" Password="true" 
                                                    Width="170">
                                                    <ValidationSettings ErrorTextPosition="Bottom" 
                                                        ErrorDisplayMode="Text"  
                                                        RegularExpression-ValidationExpression="((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[@#$%]).{8,20})$"
                                                        Display="Dynamic" 
                                                        SetFocusOnError="true">
                                                        <RequiredField IsRequired="True" 
                                                            ErrorText="The value is required" />
                                                    </ValidationSettings>

                                                </dx:ASPxTextBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem>

                                    <dx:LayoutItem ShowCaption="False" Caption="Confirm password" ColSpan="2">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                                <dx:ASPxTextBox ID="confirmPasswordTextBox"  MaxLength="12"
                                                    NullText="Confirm New Password" 
                                                    runat="server"  CssClass="txtpassword" ToolTip="Confirm New Password"
                                                    ClientInstanceName="confirmPasswordTextBox" 
                                                    Password="true" 
                                                    Width="170">
                                                    <ValidationSettings ErrorTextPosition="Bottom" 
                                                        ErrorDisplayMode="Text" 
                                                        Display="Dynamic" 
                                                        SetFocusOnError="true">
                                                        <RequiredField IsRequired="True" ErrorText="The value is required" />
                                                    </ValidationSettings>
                                                     <ClientSideEvents Validation="function(s,e){
                                                             if (passwordTextBox.GetText() !== confirmPasswordTextBox.GetText())
                                                             {
                                                                e.isValid = false;
                                                                e.errorText = 'The password you entered do not match';
                                                             }
                               
                                                         }" />
                                                </dx:ASPxTextBox>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem> 


                                      <dx:LayoutItem   ShowCaption="False" ColSpan="2">
                                        <LayoutItemNestedControlCollection>
                                            <dx:LayoutItemNestedControlContainer>
                                               <dx:ASPxButton runat="server" ID="btnChangePassword" Text="Submit" Width="100px" CssClass="btnchangepwd" AutoPostBack="false" >  
                                                   
                                                   <ClientSideEvents Click="function(s,e){
                                                           if(ASPxClientEdit.AreEditorsValid()) 
                                                           {
                                                            LoadingPanel.Show(); 
                                                            cbChangePwd.PerformCallback('');    
                                                           }
                                                                                                          
                                                       }" />
                                                                                                    
                                               </dx:ASPxButton>
                                            </dx:LayoutItemNestedControlContainer>
                                        </LayoutItemNestedControlCollection>
                                    </dx:LayoutItem> 

                                </Items>
                            </dx:LayoutGroup>

                        </Items>
                    </dx:ASPxFormLayout>


                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>


        
<dx:ASPxCallback runat="server" ID="cbChangePwd" ClientInstanceName="cbChangePwd">
    <ClientSideEvents
        CallbackError="function(s,e){LoadingPanel.Hide(); }"
        CallbackComplete="function(s,e){ 
                LoadingPanel.Hide();   
                if (e.result != 'success') {
                    alert(e.result);                                                
                }   
                else
                {

                    pcChagePassword.Hide();

                    alert('Your password has been changed successfully.');
                    
                }                                                                  
                e.processOnServer = false;
        }" />
</dx:ASPxCallback>

    </form>
</body>
</html>
