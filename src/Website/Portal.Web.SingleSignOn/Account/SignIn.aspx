<%@ Page Title="" Language="vb" AutoEventWireup="false" CodeBehind="SignIn.aspx.vb" Inherits="Portal.Web.SingleSignOn.SignIn" %>



<% 
    'Response.Redirect("~/index.aspx")
%>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head>
    <title title="LWT Single Signon By UACS"></title>
 
    <link href="../Styles/Site.css" rel="stylesheet" type="text/css" />

    <%--    <style type="text/css">
        body {
            -webkit-filter: grayscale(1);
            filter: grayscale(1);
        }
    </style>--%>

    <link href="../Scripts/jquery.jgrowl.css" rel="stylesheet" type="text/css" />
    <script src="../Scripts/jquery-1.6.4.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.jgrowl.js" type="text/javascript"></script>
    <script src="../Scripts/jquery.signalR-0.5.0.js" type="text/javascript"></script>
    <script src="../signalr/hubs"></script>

    <script>
        $(function () {
            var messenger = $.connection.messenger // generate the client-side hub proxy { Initialized to Exposed Hub }


            function init() {
                messenger.addToGroup("sourcing");
                return messenger.getAllMessages().done(function (message) {
                    // Process Message Indivudally if Necessary            
                });
            }

            //messenger.begin = function () {
            //    $.jGrowl.defaults.animateClose = { width: 'hide' };
            //    $.jGrowl("Messenging System Started", { life: 1500 });
            //};

            messenger.add = function (message) {
                //$.jGrowl(message.Content, { header: message.Title, sticky: true });
                //alert(message.Content);


                cbRFID.PerformCallback(message.Content);
            };


            // Start the Connection
            $.connection.hub.start(function () {
                init().done(function () {
                    messenger.begin();
                });
            });



        });
    </script>
  

</head>
<body style="background: url('../Images/world-map.jpg') no-repeat; background-position: top right; background-color: #efefef;">

 
    <form id="Form1" runat="server" defaultfocus="UserName">
        <div class="page">
            <div class="header">
                <div class="title">
                    <table>
                        <tr>
                            <td>
                                <img src="../images/DispLogo.gif" alt="Lockton Logo" height="50">
                            </td>
                            <td>
                                <h1>Single Signon :<span id="individualSPAN"><%=System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName()).AddressList.GetValue(0).ToString()%></span>
                            </td>

                        </tr>

                    </table>

                </div>
                <div class="loginDisplay">
                    [ <a href="~/Account/signin.aspx" id="HeadLoginStatus" runat="server">Log In</a> ]
            <%--    <asp:LoginView ID="HeadLoginView" runat="server" EnableViewState="false">
                    <AnonymousTemplate>
                        [ <a href="~/Account/Login.aspx" ID="HeadLoginStatus" runat="server">Log In</a> ]
                    </AnonymousTemplate>
                    <LoggedInTemplate>
                        Welcome <span class="bold"><asp:LoginName ID="HeadLoginName" runat="server" /></span>!
                        [ <asp:LoginStatus ID="HeadLoginStatus" runat="server" LogoutAction="Redirect" LogoutText="Log Out" LogoutPageUrl="~/"/> ]
                    </LoggedInTemplate>
                </asp:LoginView>--%>
                </div>
                <div class="clear hideSkiplink">
                    <%-- <asp:Menu ID="NavigationMenu" runat="server" CssClass="menu" EnableViewState="false" IncludeStyleBlock="false" Orientation="Horizontal">
                    <Items>
                        <asp:MenuItem NavigateUrl="~/Default.aspx" Text="Home"/>
                        <asp:MenuItem NavigateUrl="~/About.aspx" Text="About"/>
                    </Items>
                </asp:Menu>--%>
                </div>
            </div>
            <div class="main">
                <h2>Log In
                </h2>
                <p>
                    Please enter your username and password.
        <asp:HyperLink ID="RegisterHyperLink" runat="server" EnableViewState="false">Register</asp:HyperLink>
                    if you don't have an account.
                </p>

                <span class="failureNotification">
                    <asp:Literal ID="FailureText" runat="server"></asp:Literal>
                </span>

                <asp:ValidationSummary ID="LoginUserValidationSummary" runat="server" CssClass="failureNotification"
                    ValidationGroup="LoginUserValidationGroup" />
                <div class="accountInfo">
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
        </div>
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Modal="True">
        </dx:ASPxLoadingPanel>

        <dx:ASPxCallback runat="server" ID="cbRFID" ClientInstanceName="cbRFID">
            <ClientSideEvents
                BeginCallback="function(s,e){
                    LoadingPanel.Show();
                }"
                CallbackComplete="function(s,e){
                    LoadingPanel.Hide();
                    if(e.result != '')
                    {
                        alert(e.result);
                    }
                }"
                EndCallback="function(s,e){
                    LoadingPanel.Hide();
                }" />
        </dx:ASPxCallback>
    </form>
</body>
</html>







