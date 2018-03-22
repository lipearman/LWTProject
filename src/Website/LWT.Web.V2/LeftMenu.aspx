<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LeftMenu.aspx.vb" Inherits="LeftMenu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="leftTree" runat="server"></f:PageManager>
        <f:Tree runat="server" ShowBorder="false" ShowHeader="false" ID="leftTree">
        </f:Tree>
    </form>
</body>
</html>
