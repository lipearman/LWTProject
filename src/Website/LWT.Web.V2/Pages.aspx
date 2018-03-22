﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Pages.aspx.vb" Inherits="Pages" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
        .linknavigate
        {
            position: absolute;
            color: #077FBC;
            background: url(./res/icon/page.png) no-repeat;
            padding-left: 20px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">

     

        <f:PageManager ID="PageManager1" AutoSizePanelID="RegionPanel1" runat="server"></f:PageManager>

        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Modal="True">
        </dx:ASPxLoadingPanel>

        <div class="linknavigate">
            <%=parenttabname %>
        </div>
        <br />
        <div runat="server" id="ContentPanel1"></div>
    </form>
</body>
</html>
