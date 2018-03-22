<%@ Page Language="vb" AutoEventWireup="false" Inherits="LWT.Website.AccessDenied" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <link href="favicon.ico" rel="shortcut icon">
    <title><%=sitename %></title>
    <%--<link href="~/Styles/Site.css" rel="stylesheet" type="text/css" />--%>
    <link href="~/css/login.css" rel="stylesheet" />
</head>
<body>
    <form id="Form1" runat="server">


        <br />
        <br />
        <section class="container">
    <div class="login">
      <h1>Access Denied</h1>
      
        <p > 
            Either you are not currently logged in, or you do not have access to modify the
            current portal module content. Please contact the portal administrator to obtain
            edit access for this module.  
        </p>
         
      
    </div>

    <div class="login-help">
      <p> © 2012-<%=Year(DateTime.Now())%> Lockton Wattana Insurance Brokers (Thailand) Ltd. All Rights Reserved.</p>
    </div>
  </section>

    </form>
</body>
</html>
