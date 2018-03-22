<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="LWT.Master.CSharp.Default" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta charset="utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>DevExpress Bootstrap Controls for ASP.NET Demos</title>
    <asp:PlaceHolder ID="PlaceHolder1" runat="server">
        <%: Styles.Render("~/bundles/styles-main") %>
        <%: Scripts.Render("~/bundles/scripts-main") %>
    </asp:PlaceHolder>
</head>
<body>
    <form id="Form1" runat="server">
       
        <nav class="navbar navbar-fixed-top" id="navbar">
            <div class="container-fluid">
                
                <button type="button" class="nav-button" id="collapse-button">
                    <span class="sr-only">Toggle navigation
                    </span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                <div class="logo">
                    <h3> 
                  <%--      <dx:BootstrapButton ID="BootstrapButton1"   runat="server" AutoPostBack="false" Text="Button">
                            <SettingsBootstrap  RenderOption="Danger"  />

                        </dx:BootstrapButton>
                        --%>
                        <%--<dx:ASPxButton ID="ASPxButton1" runat="server" Text="ASPxButton"></dx:ASPxButton>--%>
                        <a href="<%= ResolveUrl("~/") %>" target="_top">DevExpress</a></h3>
                    <h4><a href="<%= ResolveUrl("~/") %>" target="_top">Bootstrap Controls</a></h4>
                </div>
                <div id="scrollNav">
                    <dx:BootstrapMenu runat="server" ID="scrollNavMenu" ClientIDMode="Static">
                        <Items>
                            <dx:BootstrapMenuItem Text="Overview" Name="DXControls"></dx:BootstrapMenuItem>
                            <dx:BootstrapMenuItem Text="Technologies" Name="Features"></dx:BootstrapMenuItem>
                            <dx:BootstrapMenuItem Text="Controls" Name="Products"></dx:BootstrapMenuItem>
                        </Items>
                        <ItemTemplate>
                            <a href="#<%# Eval("Name") %>"><%# Eval("Text") %></a>
                        </ItemTemplate>
                    </dx:BootstrapMenu>
                </div>
                <ul class="nav nav-pills pull-right nav-header">
                    <li role="presentation"><a href="#">Free Trial</a></li>
                    <li role="presentation"><a href="#">Buy</a></li>
                     
                </ul>
            </div>
        </nav>
        <%--<demo:SideBar runat="server"></demo:SideBar>--%>
        <section class="screen screen-1">
            <a id="DXControls"></a>
            <div class="letter-b"></div>
            <ul id="scene" class="clearfix">
                <li class="layer tablet" data-depth="0.10"></li>
                <li class="layer shapes" data-depth="0.60"></li>
            </ul>
            <div class="text-block">
                <h1>DevExpress<br />
                    Bootstrap Controls</h1>
                <span>The power and simplicity of server-side ASP.NET WebForms Controls 
                    merges with the client-side responsiveness and render code clarity 
                    of Bootstrap framework, bringing you the best of both worlds.</span><br />
                <a class="btn btn-animated" href="#" role="button">TRY DEMOS</a>
            </div>
            <div class="arrow-container">
                <div class="scroll-arrow">
                    Scroll Down
                <div class="line"></div>
                </div>
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
        <section class="screen screen-3">
            <a id="Products"></a>
            <h2>What’s in the Toolbox</h2>
            <h3>From data entry forms to data shaping and visualization – a comprehensive control suite for your next business app.
            </h3>
            <div class="container products" data-trigger="product">
                <div class="col-lg-8 col-md-12">
                    <a class="product large invisible" data-animation="left-to-right" href="#">
                        <h4>Grid View</h4>
                        <div class="text">
                            <span>A powerful grid control with unlimited master-detail levels and unmatched data shaping capabilities.</span>
                            <span class="btn btn-animated">SEE DEMOS</span>
                        </div>
                        <div class="pic pic-grid"></div>
                    </a>
                </div>
                <div class="col-lg-4 col-md-6">
                    <a class="product regular invisible" href="#">
                        <h4>Charts</h4>
                        <div class="text">
                            Fast and lightweight chart control with 20+ data presentation types. 
                        </div>
                        <div class="pic pic-charts"></div>
                    </a>
                </div>
                <div class="col-lg-4 col-md-6">
                    <a class="product regular invisible" href="#">
                        <h4>Navigation</h4>
                        <div class="text">
                            From automatically-arranged data entry layouts to web navigation UI.
                        </div>
                        <div class="pic pic-nav"></div>
                    </a>
                </div>
                <div class="col-lg-12 col-md-12">
                    <a class="product horizontal invisible" href="#">
                        <h4>Data Editors</h4>
                        <div class="text">An extensive collection of data editors to be used standalone or within container controls.</div>
                        <div class="pic pic-editors"></div>
                    </a>
                </div>
            </div>
            <div class="container">
                <footer>
                    Copyright © 2017 Developer Express Inc.<br />
                    All trademarks or registered trademarks are property of their respective owners.
                </footer>
            </div>
        </section>
    </form>
</body>
</html>