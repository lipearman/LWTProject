<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />

    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title><%=sitename%></title>
    <link href="favicon.ico" rel="Lockton Wattana Insurance Brokers Thailand">

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
                                <dx:ASPxImage ID="ASPxImage1" runat="server" EmptyImage-IconID="businessobjects_bopivotchart_32x32"></dx:ASPxImage>
                            </td>

                            <td>
                                <h3><%=sitename%></h3>
                                <h4>Powered By A&S</h4>

                            </td>
                        </tr>
                    </table>

                </div>
                <div id="scrollNav">
                    <dx:BootstrapMenu runat="server" ID="scrollNavMenu" ClientIDMode="Static">
                        <Items>
                            <dx:BootstrapMenuItem Text="Overview" Name="Overviews"></dx:BootstrapMenuItem>
<dx:BootstrapMenuItem Text="Data Visualization" Name="Products"></dx:BootstrapMenuItem>
                            <dx:BootstrapMenuItem Text="Technologies" Name="Features"></dx:BootstrapMenuItem>

                            
                        </Items>
                        <ItemTemplate>
                            <a href="#<%# Eval("Name") %>"><%# Eval("Text") %></a>
                        </ItemTemplate>
                    </dx:BootstrapMenu>
                </div>
                <ul class="nav nav-pills pull-right nav-header">
                    <li role="presentation" class="hidden-xs">
                        <%--<dx:BootstrapButton ID="BootstrapButton1" runat="server" AutoPostBack="false" Text="Login">
                           <ClientSideEvents Click="funcion(s,e){
                              // BootstrapPopupControl1.PopUp();
                                alert('xxx');
                                }" />
                        </dx:BootstrapButton>--%>

                        <dx:ASPxButton ID="ASPxButton1" runat="server" Image-IconID="arrows_next_16x16office2013" AutoPostBack="false" RenderMode="Link" Text="Login">
                            <ClientSideEvents Click="function(s,e){
                               BootstrapPopupControl1.Show();
                               
                                }" />
                        </dx:ASPxButton>

                        <%--   <dx:BootstrapHyperLink ID="BootstrapHyperLink1" Text="Login"  runat="server">
                             <ClientSideEvents Click="function(s,e){
                                //BootstrapPopupControl1.Show();
                                alert(s);
                                }" />
                        </dx:BootstrapHyperLink>--%>
                    </li>
                    <%--<li role="presentation" class="hidden-xs"><a href="#">Buy</a></li>--%>
                    <%--  <li role="presentation"><a href="javascript:void(0);" id="settingsButton"><span class="icon icon-settings"></span></a></li>--%>
                </ul>
            </div>
        </nav>
        <section class="screen screen-1">
            <a id="Overviews"></a>
            <div class="letter-b"></div>
            <ul id="scene" class="clearfix">
                <li class="layer tablet" data-depth="0.10"></li>
                <li class="layer shapes" data-depth="0.60"></li>
            </ul>
            <div class="text-block">
                <h1><%=sitename%></h1>




                <span>The power and simplicity of Data Visualization WebForms
                    with the responsiveness and render clarity 
                    , bringing you the best of both worlds.</span><br />
                <a class="btn btn-animated" href="javascript:BootstrapPopupControl1.Show();" role="button">LOGIN</a>


                <%--                  <dx:ASPxButton ID="ASPxButton2" runat="server" CssClass="btn btn-animated" AutoPostBack="false" RenderMode="Link" Text="Login">
                            <ClientSideEvents Click="function(s,e){
                               BootstrapPopupControl1.Show();
                               
                                }" />
                        </dx:ASPxButton>--%>
            </div>
            <div class="arrow-container">
                <div class="scroll-arrow">
                    Scroll Down
                <div class="line"></div>
                </div>
            </div>
        </section>
       
        <section class="screen screen-3">
            <a id="Products"></a>
            <h2>What’s in the Data Visualization</h2>
            <h3>From data entry forms to data shaping and visualization – a comprehensive control suite for your next business app.
            </h3>
            <div class="container products" data-trigger="product">
                <div class="col-lg-8 col-md-12">
                    <a class="product large invisible" data-animation="left-to-right" href="javascript:;">
                        <h4>Business Intelligence</h4>
                        <div class="text">
                            <span>A powerful control with unlimited master-detail levels and unmatched data shaping capabilities.</span>
                            <%--<span class="btn btn-animated">LOGIN</span>--%>
                        </div>
                        <div class="pic pic-grid"></div>
                    </a>
                </div>
                <div class="col-lg-4 col-md-6">
                    <a class="product regular invisible" href="javascript:void(0);">
                        <h4>Dashboard</h4>
                        <div class="text">
                            Fast and lightweight chart control with 20+ data presentation types. 
                        </div>
                        <div class="pic pic-charts"></div>
                    </a>
                </div>
                <div class="col-lg-4 col-md-6">
                    <a class="product regular invisible" href="javascript:void(0);">
                        <h4>Navigation</h4>
                        <div class="text">
                            From automatically-arranged data entry layouts to web navigation UI.
                        </div>
                        <div class="pic pic-nav"></div>
                    </a>
                </div>
                <div class="col-lg-12 col-md-12">
                    <a class="product horizontal invisible" href="javascript:void(0);">
                        <h4>Data Services</h4>
                        <div class="text">An extensive collection of data editors to be used standalone or within container controls.</div>
                        <div class="pic pic-editors"></div>
                    </a>
                </div>
            </div>
            <div class="container">
                <footer>
                    Copyright &copy; <%=Now.Year %> Lockton Wattana Insurance Brokers (Thailand) Ltd. All rights reserved.
                    <br />


                    <%--    Copyright © 2017 Developer Express Inc.<br />
                    All trademarks or registered trademarks are property of their respective owners.--%>
                </footer>

            </div>
        </section>
         <section class="screen screen-2">
            <a id="Features"></a>
            <h2>A Unique Blend of Technologies</h2>
            <h3>The carefully crafted architecture based on a combination of well-established frameworks is at the heart of this library. Code once and deliver visually consistent, ultra-responsive and truly adaptive applications.</h3>
            <div class="container features" data-trigger="feature">
                <div class="col-lg-4 col-md-6">
                    <div class="feature invisible">
                        <span class="icon icon-1ic"></span>
                        <h4>Bootstrap-Enabled</h4>
                        <div class="desc">
                            All controls natively integrate into Bootstrap-driven layouts thus contributing to the application’s adaptivity and consistent look across browsers and devices.
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-6">
                    <div class="feature invisible">
                        <span class="icon icon-2ic"></span>
                        <h4>Adaptive by Design</h4>
                        <div class="desc">
                            Our rendered code uses Bootstrap CSS components exclusively, meaning that all controls will adjust to any screen resolution.
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-6">
                    <div class="feature invisible">
                        <span class="icon icon-3ic"></span>
                        <h4>Mobile-Friendly</h4>
                        <div class="desc">
                            Extend your site’s audience reach and boost search rankings by leveraging lightweight render, responsive layout, and performance approaching native controls.
                        </div>
                    </div>
                </div>
                <div class="clearfix visible-lg-block"></div>
                <div class="col-lg-4 col-md-6">
                    <div class="feature invisible">
                        <span class="icon icon-4ic"></span>
                        <h4>Visually Consistent</h4>
                        <div class="desc">
                            Leave it to Bootstrap’s myriad of available visual themes to guarantee consistent look and feel throughout your web application – from early prototype to production.
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-6">
                    <div class="feature invisible">
                        <span class="icon icon-5ic"></span>
                        <h4>An Extensive Control Suite</h4>
                        <div class="desc">
                            The DevExpress Bootstrap Control Suite includes numerous controls for data editing, navigation and layout management.
                        </div>
                    </div>
                </div>
                <div class="col-lg-4 col-md-6">
                    <div class="feature invisible">
                        <span class="icon icon-6ic"></span>
                        <h4>Powered by ASP.NET Controls</h4>
                        <div class="desc">
                            Revamped rendering, no-compromise feature set. Expect the same level of server-side and client-side API as in DevExpress ASP.NET WebForms Controls.
                        </div>
                    </div>
                </div>
            </div>
        </section>

        <dx:BootstrapPopupControl ID="BootstrapPopupControl1"
            ClientInstanceName="BootstrapPopupControl1"
            runat="server"
            PopupHorizontalAlign="WindowCenter"
            PopupVerticalAlign="WindowCenter"
            Width="300px"
            CloseAction="OuterMouseClick"
            Modal="true"
            HeaderText="LOGIN">

            <HeaderTemplate>

                <h4>
                    <a href="javascript:;" style="pointer-events: none; cursor: default;">
                        <%--<span class="image fa fa-user" aria-hidden="true"></span>--%><span>
                            <dx:ASPxImage runat="server" EmptyImage-IconID="businessobjects_bouser_32x32"></dx:ASPxImage>
                            &nbsp;Login</span></a>
                </h4>

            </HeaderTemplate>

            <ClientSideEvents Shown="function(s,e){
                    ASPxClientEdit.ClearGroup('Validation');
                    txtMessage.SetText(e.result);
                }" />


            <%--BeginCollapse--%>
            <ContentCollection>
                <dx:ContentControl>

                    <dx:BootstrapFormLayout ID="BootstrapFormLayout1" runat="server">
                        <Items>
                            <dx:BootstrapLayoutItem Caption="User Name" ShowCaption="False" ColSpanSm="12">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <dx:BootstrapTextBox ID="txtUserName" runat="server" NullText="User Name">
                                            <ValidationSettings ValidationGroup="Validation">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:BootstrapTextBox>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:BootstrapLayoutItem>
                            <dx:BootstrapLayoutItem Caption="Password" ShowCaption="False" ColSpanSm="12">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <dx:BootstrapTextBox ID="txtPassword" Password="true" runat="server" NullText="Password">
                                            <ValidationSettings ValidationGroup="Validation">
                                                <RequiredField IsRequired="true" />
                                            </ValidationSettings>
                                        </dx:BootstrapTextBox>
                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:BootstrapLayoutItem>

                            <dx:BootstrapLayoutItem ShowCaption="False" HorizontalAlign="Left" ColSpanSm="12">
                                <ContentCollection>
                                    <dx:ContentControl>
                                        <dx:BootstrapButton ID="BootstrapButton1" runat="server" Text="Submit"
                                            SettingsBootstrap-RenderOption="Primary" AutoPostBack="false">
                                            <ClientSideEvents Click="function(s, e) { 
                                                     if(ASPxClientEdit.ValidateGroup('Validation'))
                                                     {
                                                        LoadingPanel.Show(); 
                                                 
                                                        cbLogin.PerformCallback();
                                                   
                                                        e.processOnServer = false;
                                                     } 
                                                 }" />
                                        </dx:BootstrapButton>


                                        <dx:BootstrapButton ID="BootstrapButton2" runat="server" Text="Close"
                                            SettingsBootstrap-RenderOption="Default"
                                            AutoPostBack="false">

                                            <ClientSideEvents Click="function(s, e) { 
                                                  //ASPxClientEdit.ClearGroup('Validation'); 
                                                  BootstrapPopupControl1.Hide();
                                                  }" />

                                        </dx:BootstrapButton>



                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:BootstrapLayoutItem>



                            <dx:BootstrapLayoutItem Caption="Password" ShowCaption="False" ColSpanSm="12">
                                <ContentCollection>
                                    <dx:ContentControl>

                                        <dx:ASPxLabel ID="ASPxLabel1" ClientInstanceName="txtMessage" runat="server" Text="" ForeColor="Red"></dx:ASPxLabel>


                                    </dx:ContentControl>
                                </ContentCollection>
                            </dx:BootstrapLayoutItem>

                        </Items>
                    </dx:BootstrapFormLayout>

                </dx:ContentControl>
            </ContentCollection>
            <%--EndCollapse--%>
        </dx:BootstrapPopupControl>






        <dx:ASPxCallback ID="cbLogin" runat="server" ClientInstanceName="cbLogin">
            <ClientSideEvents CallbackComplete="function(s, e) {
                    txtMessage.SetText(e.result);
                  
                    LoadingPanel.Hide();                                                  
             }" />
        </dx:ASPxCallback>

    </form>
</body>
</html>
