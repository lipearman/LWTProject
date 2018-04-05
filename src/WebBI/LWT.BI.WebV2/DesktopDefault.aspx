<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DesktopDefault.aspx.vb" Inherits="DesktopDefault" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />

    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title><%=sitename%></title>
    <link href="favicon.ico" rel="Lockton Wattana Insurance Brokers Thailand">
    <script src="Scripts/bootstrap.min.js"></script>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <%--     <%: Styles.Render("~/bundles/styles-main") %>
        <%: Scripts.Render("~/bundles/scripts-main") %>--%>


        <script src="Scripts/jquery-3.1.1.js"></script>
        <script src="Scripts/jquery.mCustomScrollbar.concat.min.js"></script>
        <script src="Scripts/jquery.parallax.min.js"></script>
        <script src="Scripts/bootstrap.min.js"></script>
        <script src="Scripts/common.js"></script>
        <%--<script src="Scripts/search.js"></script>--%>
        <script src="Scripts/landing.js"></script>


        <link href="Content/demo-icons.css" rel="stylesheet" />
        <link href="Content/jquery.mCustomScrollbar.min.css" rel="stylesheet" />
        <link href="Content/bootstrap.min.css" rel="stylesheet" />
        <link href="Content/common.css" rel="stylesheet" />
        <link href="Content/landing.css" rel="stylesheet" />
        <link href="Content/font-awesome.min.css" rel="stylesheet" />


    </asp:PlaceHolder>





</head>
<body>
    <form id="Form1" runat="server">

        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server"
            ClientInstanceName="LoadingPanel"
            Modal="True">
        </dx:ASPxLoadingPanel>
        <dx:ASPxPopupControl ID="clientView" runat="server" ClientInstanceName="clientView"
            Modal="True" Maximized="true"
            PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter"
            HeaderText="Business Intelligence"
            AllowDragging="true"
            AllowResize="True"
            DragElement="Window"
            EnableAnimation="true"
            CloseAction="CloseButton"
            EnableCallbackAnimation="true"
            EnableViewState="true"
            ShowPageScrollbarWhenModal="true"
            ScrollBars="Auto"
            ShowMaximizeButton="true"
            HeaderImage-IconID="businessobjects_botask_32x32"
            HeaderStyle-BackColor="WindowFrame"
            Width="800"
            Height="680"
            FooterText=""
            ShowFooter="false">
            <HeaderStyle BackColor="#4796CE" ForeColor="White" />
            <ContentStyle>
        <Paddings Padding="0px" />
    </ContentStyle>

            <ClientSideEvents Shown="function(s,e){ 
                //window.setTimeout(function() {LoadingPanel.Hide();},2000);

                LoadingPanel.Show();
                }" />

            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl5" runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>


        <dx:ASPxPopupControl ID="clientView2" runat="server" ClientInstanceName="clientView2"
            Modal="True" Maximized="true"
            PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter"
            HeaderText="Dashboard"
            AllowDragging="true"
            AllowResize="True"
            DragElement="Window"
            EnableAnimation="true"
            CloseAction="CloseButton"
            EnableCallbackAnimation="true"
            EnableViewState="true"
            ShowPageScrollbarWhenModal="true"
            ScrollBars="Auto"
            ShowMaximizeButton="true"
            HeaderImage-IconID="businessobjects_botask_32x32"
            HeaderStyle-BackColor="WindowFrame"
            Width="800"
            Height="680"
            FooterText=""
            ShowFooter="false">

            <HeaderStyle BackColor="#4796CE" ForeColor="White" />

            <ContentStyle>
        <Paddings Padding="0px" />
    </ContentStyle>

            <ClientSideEvents Shown="function(s,e){ 
                //window.setTimeout(function() {LoadingPanel.Hide();},2000);

                LoadingPanel.Show();
                }" />

            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

           <dx:ASPxPopupControl ID="clientView3" runat="server" ClientInstanceName="clientView3"
            Modal="True" Maximized="true"
            PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter"
            HeaderText="XtraReports"
            AllowDragging="true"
            AllowResize="True"
            DragElement="Window"
            EnableAnimation="true"
            CloseAction="CloseButton"
            EnableCallbackAnimation="true"
            EnableViewState="true"
            ShowPageScrollbarWhenModal="true"
            ScrollBars="Auto"
            ShowMaximizeButton="true"
            HeaderImage-IconID="businessobjects_botask_32x32"
            HeaderStyle-BackColor="WindowFrame"
            Width="800"
            Height="680"
            FooterText=""
            ShowFooter="false">

            <HeaderStyle BackColor="#4796CE" ForeColor="White" />

            <ContentStyle>
        <Paddings Padding="0px" />
    </ContentStyle>

            <ClientSideEvents Shown="function(s,e){ 
                //window.setTimeout(function() {LoadingPanel.Hide();},2000);

                LoadingPanel.Show();
                }" />

            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>



        <nav class="navbar navbar-fixed-top" id="navbar">
            <div class="container-fluid">

                <button type="button" class="nav-button" id="collapse-button">
                    <span class="sr-only">Toggle navigation
                    </span>
                    <%-- <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>--%>
                    <img src="images/Lockton-Logo_250.png" width="64" />
                </button>

                <div class="logo">

                    <table style="margin-top: 10px;">
                        <tr>

                            <td style="vertical-align: central; text-align: left">
                                <%-- <img src="Content/landing-control-charts.png" width="100" />--%>
                                <dx:ASPxImage runat="server" EmptyImage-IconID="businessobjects_bopivotchart_32x32"></dx:ASPxImage>
                            </td>

                            <td>
                                <h3><%=sitename%></h3>
                                <h4>Powered By A&S</h4>

                            </td>
                        </tr>
                    </table>









                </div>

                <ul class="nav nav-pills pull-right nav-header">
                    <li role="presentation" class="hidden-xs">
                        
                        <a href="javascript:;" style="pointer-events: none; cursor: default;">
                            <%--<span class="image fa fa-user" aria-hidden="true"></span>--%>
                             <span>
                                 
                                 <dx:ASPxImage ID="ASPxImage1" runat="server" EmptyImage-IconID="businessobjects_bocustomer_32x32"></dx:ASPxImage>
                           
                                &nbsp;Hi,<%=HttpContext.Current.User.Identity.Name%></span></a></li>

                    <li role="presentation">

                        <dx:ASPxButton ID="btnLogout" runat="server" Image-IconID="arrows_next_16x16office2013" AutoPostBack="false" RenderMode="Link" Text="Logout&nbsp;">
                            <ClientSideEvents Click="function(s,e){
                                 LoadingPanel.Show();
                                 cbLoginOut.PerformCallback();
                                 e.processOnServer = false;
                                }" />
                        </dx:ASPxButton>
                        &nbsp;
                    </li>
                    <%--<li role="presentation" class="hidden-xs"><a href="#">Buy</a></li>--%>
                    <%--  <li role="presentation"><a href="javascript:void(0);" id="settingsButton"><span class="icon icon-settings"></span></a></li>--%>
                </ul>
            </div>
        </nav>


        <section id="sidebar">
            <div class="sidebar-body">
                <%--<demo:Search runat="server" ID="Search"></demo:Search>--%>
                <dx:BootstrapTreeView runat="server" ID="navTreeView" ClientInstanceName="navTreeView"
                    ShowExpandButtons="false" ClientIDMode="Static" Target="_top">
                    <ClientSideEvents ExpandedChanging="dxbsDemo.preventExpandedChanging" NodeClick="dxbsDemo.onSideBarNodeClick" />
                </dx:BootstrapTreeView>
            </div>
            <div class="sidebar-footer">
                <ul class="nav nav-pills nav-stacked hidden-lg hidden-md hidden-sm" id="bottomNav">
                    <li role="presentation"><a href="javascript:LoadingPanel.Show(); cbLoginOut.PerformCallback();"><span class="image icon icon-download"></span><span>Logout</span></a></li>
                    <%--<li role="presentation"><a href="javascript:void(0);"><span class="image icon icon-buy"></span><span>Buy</span></a></li>--%>
                </ul>
            </div>
        </section>




        <section class="screen screen-2">
            <div class="container">
                <br />
                <br />
                <br />
                <br />
                 <dx:ASPxImage ID="ASPxImage2" runat="server" EmptyImage-IconID="businessobjects_bocountry_32x32"></dx:ASPxImage><b><%=NavigateBar %></b>
                <br />
              <%--  <br />--%>
                <div runat="server" id="container"></div>
            </div>

        </section>
        <section class="screen screen-3">

            <div class="container">
                <footer>
                    Copyright &copy; <%=Now.Year %> Lockton Wattana Insurance Brokers (Thailand) Ltd. All rights reserved.
                    <br />
                </footer>

            </div>
        </section>



        <dx:ASPxCallback ID="cbLoginOut" runat="server" ClientInstanceName="cbLoginOut">
        </dx:ASPxCallback>


    </form>
</body>
</html>
