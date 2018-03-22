<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DesktopDefault.aspx.vb" Inherits="DesktopDefault" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, user-scalable=no, maximum-scale=1.0, minimum-scale=1.0" />


    <link href="favicon.ico" rel="shortcut icon">
    <title><%=sitename %></title>

    <link rel="stylesheet" href="css/lockton/styles.css" type="text/css" media="all" />
    <link rel="stylesheet" href="css/lockton/skeleton.css" type="text/css" media="screen" />
    <link rel="stylesheet" href="css/lockton/print.css" type="text/css" media="print" />
    <script src="css/lockton/modernizr.js" type="text/javascript"></script>
    <script src="css/lockton/jquery.min.js" type="text/javascript"></script>
    <script src="css/lockton/scripts.js" type="text/javascript"></script>
    <script type="text/javascript" src="css/lockton/jquery.easing.1.3.js"></script>
    <script src="css/lockton/jquery.dlmenu.js"></script>
    <script>
        $(function () {
            $('#dl-menu').dlmenu({
                animationClasses: { classin: 'dl-animate-in-1', classout: 'dl-animate-out-1' }
            });
        });
    </script>



</head>
<body class="connect">


    <form id="form1" runat="server">
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Modal="True"></dx:ASPxLoadingPanel>
        <%--HeaderStyle-BackColor="#009688"--%>
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
            HeaderImage-IconID="businessobjects_boreport_32x32"
            HeaderStyle-BackColor="WindowFrame"
            Width="800"
            Height="680"
            FooterText=""
            ShowFooter="false">
            <ClientSideEvents  Shown="function(s,e){ 
                //window.setTimeout(function() {LoadingPanel.Hide();},2000);

                LoadingPanel.Show();
                }" />

            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl5" runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
        <%-- <asp:SqlDataSource ID="SqlDataSource_Tabs" runat="server" ConnectionString="<%$ ConnectionStrings:PortalConnectionString %>"
            SelectCommand="
  SELECT  
       [UserID]
      ,[UserName]
      ,[TabId]
      ,[TabName]
      ,[TabOrder]
      ,[ParentId]
      ,[Permission]
      ,[PortalId]
      ,[PortalCode]
      ,[PortalName]
      ,[Sortpath]
            ,IconID
      ,(select count(*) from PortalCfg_Modules where PortalCfg_Modules.TabId = v_UserTabs.TabId ) as Modules

  FROM [v_UserTabs]
  where PortalId=@PortalId and UserName=@UserName
  order by Sortpath,TabOrder

                ">
            <SelectParameters>
                <asp:Parameter Name="PortalId" />
                <asp:Parameter Name="UserName" />
            </SelectParameters>



        </asp:SqlDataSource>--%>


        <!----------- Header--------------->
        <div class="header">
            <div class="container">
                <div class="logo">
                    <a href="#">
                        <img src="css/images/interface/logo.png" alt="Lockton Logo" title="Lockton" />
                    </a>
                </div>
                <div class="navigation">
                    <div class="search">
                        <ul class="search-nav">
                            <li>Welcome, <%=UserName %> (<%=Now %>) 
 
                  <dx:ASPxButton ID="btnLogout" runat="server" RenderMode="Button"
                      Text="SignOut"
                      AutoPostBack="false"
                      CausesValidation="false">
                      <Image IconID="navigation_forward_16x16gray"></Image>


                      <ClientSideEvents Click="function(s, e) {
                              e.processOnServer = confirm('Confirm to SignOut?');
                        }" />
                  </dx:ASPxButton>

                            </li>
                        </ul>

                        <div style="float: left; margin-top: 15px">

                            <%-- <h3 style="color: gray; font-weight: bold"><%=sitename %></h3>--%>



                            <dx:ASPxButton ID="txtSiteName" runat="server" RenderMode="Button"
                                Text="SignOut" Width="400" 
                                AutoPostBack="false" Image-IconID="format_pictureshapeoutlinecolor_16x16"
                                CausesValidation="false">
                            </dx:ASPxButton>
                        </div>
                        <div class="clear"></div>
                    </div>
                    <ul class='nav'>
                        <%=topMenu %>
                    </ul>
                </div>
                <div class="clear"></div>
            </div>
            <%=subMenu %>
        </div>
        <!----------- dl-menu--------------->
        <div id="dl-menu" class="dl-menuwrapper">
            <button class="dl-trigger">Open Menu</button>
            <ul class="dl-menu">
                <%=dlMenu %>
            </ul>
        </div>




        <div class="holder">

            <!-------------Page Header------------->
            <div class="page_header">
                <div class="container">
                    <div class="header_content">
                        <h1><%=PageName %></h1>
                    </div>
                </div>
            </div>
            <!-------------Page Content------------->
            <div class="page_content">

                <div runat="server" id="container" class="container"></div>


                <div class="clear"></div>
            </div>
        </div>


        <!----------Footer---------------->
        <div class="footer">
            <div class="copyright">
                Copyright &copy; <%=Now.Year %> Lockton Wattana Insurance Brokers (Thailand) Ltd. All rights reserved.
            </div>
        </div>
    </form>




</body>
</html>
