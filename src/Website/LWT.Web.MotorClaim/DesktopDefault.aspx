<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DesktopDefault.aspx.vb" Inherits="DesktopDefault" %>

<%@ Import Namespace="System.Security.Principal" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <link href="favicon.ico" rel="shortcut icon">
    <title><%=sitename %></title>

    <!-- BEGIN: load jquery -->
    <script src="js/jquery-1.6.4.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui/jquery.ui.core.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui/jquery.ui.widget.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui/jquery.ui.accordion.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui/jquery.effects.core.min.js" type="text/javascript"></script>
    <script src="js/jquery-ui/jquery.effects.slide.min.js" type="text/javascript"></script>
    <!-- END: load jquery -->




    <%--    <link rel="stylesheet" type="text/css" href="css/reset.css" media="screen" />
    <link rel="stylesheet" type="text/css" href="css/text.css" media="screen" />--%>
    <link rel="stylesheet" type="text/css" href="css/grid.css" media="screen" />
    <link rel="stylesheet" type="text/css" href="css/layout.css" media="screen" />
    <%--    <link rel="stylesheet" type="text/css" href="css/nav.css" media="screen" />--%>

    <!--[if IE 6]><link rel="stylesheet" type="text/css" href="css/ie6.css" media="screen" /><![endif]-->
    <!--[if IE 7]><link rel="stylesheet" type="text/css" href="css/ie.css" media="screen" /><![endif]-->

    <%--    <!-- BEGIN: load jqplot -->
    <link rel="stylesheet" type="text/css" href="css/jquery.jqplot.min.css" />
    <!--[if lt IE 9]><script language="javascript" type="text/javascript" src="js/jqPlot/excanvas.min.js"></script><![endif]-->
    <script language="javascript" type="text/javascript" src="js/jqPlot/jquery.jqplot.min.js"></script>
    <script language="javascript" type="text/javascript" src="js/jqPlot/plugins/jqplot.barRenderer.min.js"></script>
    <script language="javascript" type="text/javascript" src="js/jqPlot/plugins/jqplot.pieRenderer.min.js"></script>
    <script language="javascript" type="text/javascript" src="js/jqPlot/plugins/jqplot.categoryAxisRenderer.min.js"></script>
    <script language="javascript" type="text/javascript" src="js/jqPlot/plugins/jqplot.highlighter.min.js"></script>
    <script language="javascript" type="text/javascript" src="js/jqPlot/plugins/jqplot.pointLabels.min.js"></script>
    <!-- END: load jqplot -->--%>



    <%--<script src="js/setup.js" type="text/javascript"></script>--%>
    <%--    <script type="text/javascript">

        $(document).ready(function () {
            setupDashboardChart('chart1');
            setupLeftMenu();
            setSidebarHeight();


        });
    </script>--%>

    <%--    <link href="js/guidely/guidely.css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="js/guidely/guidely.min.js"></script>--%>


    <%--================================================================================--%>
    <link href="css/dcmegamenu.css" rel="stylesheet" type="text/css" />
    <script type='text/javascript' src='js/jquery.hoverIntent.minified.js'></script>
    <script type='text/javascript' src='js/jquery.dcmegamenu.1.3.3.js'></script>
    <script type="text/javascript">
        $(document).ready(function ($) {
            $('#mega-menu-4').dcMegaMenu({
                rowItems: '3',
                speed: 'fast',
                effect: 'slide'
            });
        });
    </script>
    <link href="css/skins/blue.css" rel="stylesheet" type="text/css" />
    <%--<link href="css/skins/black.css" rel="stylesheet" type="text/css" />
<link href="css/skins/grey.css" rel="stylesheet" type="text/css" />

<link href="css/skins/green.css" rel="stylesheet" type="text/css" />
<link href="css/skins/light_blue.css" rel="stylesheet" type="text/css" />
<link href="css/skins/orange.css" rel="stylesheet" type="text/css" />
<link href="css/skins/red.css" rel="stylesheet" type="text/css" />
<link href="css/skins/white.css" rel="stylesheet" type="text/css" />--%>


 

    <%--================================================================================--%>
</head>
<body style="background-color: #092564;">
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="ContentPanel1"></f:PageManager>

        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
            Modal="True">
        </dx:ASPxLoadingPanel>
        <div class="container_12" style="width: initial">
            <div class="grid_12 header-repeat" style="position: fixed; right: 0; left: 0; margin-bottom: 0; z-index: 1000;">
                <div id="branding">
                    <div class="floatleft">
                        <img src="images/Lockton-Logo_250.png" height="51" alt="Logo" />
                    </div>
                    <div class="floatleft" style="margin-top: 10px">
                        <h2 style="color: white;">&nbsp;&nbsp;&nbsp;&nbsp;<%=sitename %></h2>
                    </div>
                    <div class="floatright" style="margin-top: 10px">
                        <div class="floatleft">
                            <img src="img/img-profile.jpg" alt="Profile Pic" />
                        </div>
                        <div class="floatleft marginleft10">
                            <ul class="inline-ul floatleft">
                                <li>Hello <%=UserName %></li>
                                <li><a href="LogOff.aspx" onclick="LoadingPanel.Show();">Logout</a></li>
                            </ul>
                            <br />
                            <span class="small grey">Last Login: <%=Session("SignInTime") %></span>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>

            <%--<div class="grid_12">  <ul class="nav main"> </ul></div>--%>
            <div class="blue" style="position: fixed; right: 0; left: 0; margin-top: 55px; margin-bottom: 0; z-index: 1000;">

                <ul id="mega-menu-4" class="mega-menu">
                    <%=mainMenu%>
                </ul>
 
            </div>

            <%--<br />--%>

            <div class="clear">
            </div>
            <%--            <div class="grid_2">
                <div class="box sidemenu">
                    <div class="block ui-accordion ui-widget ui-helper-reset ui-accordion-icons" id="section-menu" role="tablist">
                      
                    </div>
                </div>
            </div>--%>
            <div class="grid_10" style="width: auto; right: 0; left: 0; margin-top: 88px; margin-bottom: 0;">
                <div class="box round first grid">
                    <div style="font-size: 1em; font-weight: bold; color: #1B548D; border-bottom: 1px solid #B3CBD6;">
                        <%=parenttabname%>
                    </div>
                    <div runat="server" id="ContentPanel1"></div>
                    <f:Panel ID="MainRegionPanel" runat="server" ShowBorder="false" ShowHeader="false">
                        <Items>
                            <f:ContentPanel ID="ContentPanel2" runat="server" ShowBorder="false" ShowHeader="false">
                            </f:ContentPanel>
                        </Items>
                    </f:Panel>

                </div>
            </div>
            <div class="clear">
            </div>
        </div>
        <div class="clear">
        </div>
        <div id="site_info" style="width: inherit">
            <p>
                © 2012-<%=Year(DateTime.Now())%> Lockton Wattana Insurance Brokers (Thailand) Ltd. All Rights Reserved.
            </p>
        </div>



    </form>
</body>
</html>

