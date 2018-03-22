<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="index.aspx.vb" Inherits="Portal.Web.SingleSignOn.index" %>

<%@ Import Namespace="System.Security.Principal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-US" class="no-js">
<head id="Head1" runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" media="screen" href="res/css/style.css" />
    <script type="text/javascript" src="res/js/jquery-1.7.1.min.js"></script>
    <script type="text/javascript" src="res/js/jquery-ui-1.8.17.custom.min.js"></script>
    <script type="text/javascript" src="res/js/modernizr.js"></script>
    <script type="text/javascript" src="res/js/fix-and-clock.js"></script>



    <style>
        input[type=text]::-moz-selection {
            background: rgba(124,196,255,0.7);
        }

        input[type=text]::selection {
            background: rgba(124,196,255,0.7);
        }

        input[type=text] {
            width: 154px;
            height: 24px;
            box-shadow: 0 0 2px 3px #4189c3;
            border-radius: 3px;
            border: 1px solid rgba(0,0,0,0);
            padding: 2px 26px 2px 5px;
            font-family: "Lucida Grande", "Lucida Sans Unicode", sans-serif;
        }

            input[type=text].valid {
                box-shadow: none;
                color: #6d6d6d;
                border-top: 1px solid #343434;
                border-left: 1px solid #343434;
                border-right: 1px solid #515151;
                border-bottom: 1px solid #515151;
            }

            input[type=text]::-webkit-input-placeholder {
                font-size: 12px;
                color: #6d6d6d;
                letter-spacing: 0;
            }

            input[type=text]:-moz-placeholder {
                font-size: 12px;
                color: #6d6d6d;
                letter-spacing: 0;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <!-- FAIL -->
        <div id="fail">
            <div class="fail-browser-logo">
                <img src="res/img/apple-logo-login.png" alt="Mac OS X" />
            </div>
            <div class="fail-browser">
                <p><strong>I'm sorry to inform you that your browser don't support CSS3 Animations!</strong></p>
                <p>This site uses features that <em>require</em> a modern browser - why not try <a href="https://www.google.com/intl/en/chrome/browser/desktop/" target="_blank" title="Download Firefox">Google Chrome</a> ?</p>
            </div>
        </div>


        <!-- BOOT -->
        <div id="pageLoading">
            <div class="loading">
                <div class="apple-logo"></div>
                <div class="spinner">
                </div>

            </div>
            <div class="footer">
                <center>© 2012-<%=Year(DateTime.Now())%> Lockton Wattana Insurance Brokers (Thailand) Ltd. All Rights Reserved.</center>
            </div>
        </div>



        <!-- LOGIN -->
        <div id="pageLogin">
            <div class="footer">
                <center>© 2012-<%=Year(DateTime.Now())%> Lockton Wattana Insurance Brokers (Thailand) Ltd. All Rights Reserved.</center>
            </div>
            <header id="headlogin">
                <nav id="menu-dx-login">
                    <ul>
                        <li class="wireless"></li>
                        <li class="time">
                            <ul>
                                <li class="hours"></li>
                                <li class="point">:</li>
                                <li class="min"></li>
                            </ul>
                        </li>
                    </ul>
                </nav>
            </header>
            <div class="new-apple-logo"></div>

            <div class="user-avatar">
                <div id="avatar">
<%--                    <a href="#hide" class="hide" id="hide"></a>
                    <a href="#show" class="show" id="show"></a>--%>
                    <div id="cover"></div>
                    <div class="ava-css">
                        <img src="res/img/user.png" />
                    </div>
                    <div class="logName">
                        <%-- <p><%=WindowsIdentity.GetCurrent().Name.ToString() %></p>--%>
                        <div class="validate">
                            <input type="text" id="user" placeholder="User" runat="server" />
                        </div>
                    </div>
                    <div id="switch">
                        <div class="validate">

                            <input type="password" id="password" placeholder="Password" runat="server" />
                            <%--<input type="submit" class="submit" />--%>



                            <dx:ASPxButton ID="cmdSubmit" AutoPostBack="false" CssClass="submit" runat="server" Text="">
                                <ClientSideEvents Click="function(s,e){                                         
                                        cbSubmit.PerformCallback( $('#user').val() + '|' + $('#password').val());
                                    }" />
                            </dx:ASPxButton>
                            <dx:ASPxCallback ID="cbSubmit" runat="server" ClientInstanceName="cbSubmit">
                                <ClientSideEvents CallbackComplete="function(s,e){                                         	                                     
	                                    if (e.result == 'success') 
                                        {
                                            $('.submit').removeClass('submit').addClass('charge');
                                        }
                                        else
                                        { 
    	                                    alert(e.result);
    	                                }
                                    }" />
                            </dx:ASPxCallback>


                            <div class="tooltip-pass">
                                <p>Passwords are required to be a minimum of 8 to 20 characters string with at least one digit, one upper case letter, one lower case letter and one special symbol (@#$%)</p>

                            </div>

                        </div>
                    </div>
                </div>
            </div>
        </div>


    </form>
</body>
</html>
