<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default2.aspx.vb" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src='http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js'></script>
    <script src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.8.2/jquery-ui.min.js"></script>
    <link href="jquery/slidetl.css" rel="stylesheet" type="text/css" />
    
</head>
<body>
    <form id="form1" runat="server">
        Username :
        <input type="text" value="admin" name="user" /><br />
        Password : 
        <input type="password" value="admin" name="user" />
        <br />
        <br />
        <div id="slidetl">
            <div id="slider"></div>
        </div>
        <script type="text/javascript" src="jquery/slidetl.js"></script>
    </form>
</body>
</html>
