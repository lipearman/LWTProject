<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DesktopDefault.aspx.vb" Inherits="DesktopDefault" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" xml:lang="en">
<head id="Head1" runat="server">
    <meta http-equiv="content-type" content="text/html; charset=utf-8" />
    <link href="favicon.ico" rel="shortcut icon">
    <title><%=sitename %></title>
    <script src="./res/js/jquery.min.js" type="text/javascript"></script>
    <link type="text/css" rel="stylesheet" href="~/res/css/default.css" />
    <link type="text/css" rel="stylesheet" href="./res/main.css" />

    <style>
        .member
        {
            position: absolute;
            color: #077FBC;
            background: url(./res/icon/user.png) no-repeat;
            padding-left: 20px;
            position: absolute;
            left: 12px;
            bottom: 5px;

        }
    </style>
</head>
<body>
    <form id="form1" runat="server">


        <f:PageManager ID="PageManager1" AutoSizePanelID="RegionPanel1" EnableAjax="false" runat="server"></f:PageManager>
        <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Modal="True"></dx:ASPxLoadingPanel>


        <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server">
            <Regions>
                <f:Region ID="Region1" ShowBorder="false" Height="102px" ShowHeader="false"
                    Position="Top" Layout="Region" runat="server">
                    <Items>
                        <f:ContentPanel RegionPosition="Top" ShowBorder="false" CssClass="jumbotron" ShowHeader="false" runat="server">
                            <div class="wrap">
                                <div class="logos">
                                   <img src="images/Lockton-Logo_250.png" height="51" alt="Logo"><br />
                                   <span style="font-size: 15px;font-weight: bold;" > <%=SiteName%> </span>
                                </div>
                                <div class="menu">
                                    <ul>
                                        <asp:Repeater ID="gridTopMenu" runat="server">
                                            <ItemTemplate>
                                                <li class="<%# Eval("PageId")%>">
                                                    <a href="#" onclick="cbTopMenu.PerformCallback('<%# Eval("PageId")%>')"><span><%# Eval("TabName")%></span> </a>
                                                </li>
                                            </ItemTemplate>
                                        </asp:Repeater>
                                    </ul>


                                </div>

                                <div class="member">
                                    Hello <%=UserName & " (" & Session("SignInTime") & ")"%>！
                                </div>

                                <div class="exit">
                                    <a href="LogOff.aspx" onclick="LoadingPanel.Show();">Logout</a>
                                </div>
                            </div>
                        </f:ContentPanel>
                    </Items>
                </f:Region>


                <f:Region ID="Region2" RegionSplit="true" Width="200px"
                    ShowHeader="true" Title="Menu" Icon="Outline"
                    EnableCollapse="true" Layout="Fit" RegionPosition="Left" runat="server">
                    <Content>



                        <dx:ASPxTreeList ID="leftTree" runat="server" Theme="MetropolisBlue"
                            OnDataBound="leftTree_DataBound" EnableViewState="true"
                            OnCustomCallback="leftTree_CustomCallback"
                            OnCustomDataCallback="leftTree_CustomDataCallback"
                            OnHtmlDataCellPrepared="leftTree_HtmlDataCellPrepared"
                            OnHtmlRowPrepared="leftTree_HtmlRowPrepared" 
                            ClientInstanceName="leftTree" 
                            Settings-ShowColumnHeaders="false" 
                            Border-BorderStyle="None"
                            AutoGenerateColumns="False"
                            ParentFieldName="ParentId"
                            KeyFieldName="PageId"
                            DataSourceID="SqlDataSource_leftTree"
                            Width="100%">
                            <Styles>
                                <Cell Cursor="pointer"> 
                                    <Paddings PaddingLeft="20" />
                                    <BackgroundImage Repeat="NoRepeat"   HorizontalPosition="left"/>
                                    
                                </Cell>
                                
                            </Styles>
                            <Columns>


                                <dx:TreeListDataColumn FieldName="TabName" 
                                    Caption="Page Title"
                                    VisibleIndex="0" />


<%--                                <dx:TreeListTextColumn FieldName="Status" Name="colStatus" VisibleIndex="0">
                                    <DataCellTemplate>
                                        <dx:ASPxImage ID="ASPxImage1" runat="server">
                                        </dx:ASPxImage>
                                    </DataCellTemplate>
                                </dx:TreeListTextColumn>--%>



                            </Columns> 
                            <Settings ShowTreeLines="False"  />
                            <SettingsBehavior ExpandCollapseAction="NodeDblClick" AllowFocusedNode="true" />
                            <ClientSideEvents NodeClick="function(s, e) { 

                                 //s.GetInputElement().style.fontWeight = 'bold';

                                 var key = e.nodeKey;

                                //alert(key);

                                 //leftTree.PerformCustomDataCallback(key); 

                                  window.frames['mainframe'].location.href = 'pages.aspx?pageid=' + key;
                                }"
                                CustomDataCallback="function(s, e) { 
                                    if(e.result != '')
                                    {
                                        window.frames['mainframe'].location.href = 'pages.aspx?pageid=' + e.result;
                                    } 
                                }
                                "
                       
                                />


                        </dx:ASPxTreeList>
                    </Content>
                    <%--  FocusedNodeChanged="function(s, e) { 



                                var key = leftTree.GetFocusedNodeKey();
                                leftTree.PerformCustomDataCallback(key); 
                                }" --%>
                </f:Region>

                <f:Region ID="mainRegion" ShowHeader="false" Position="Center" BodyPadding="10"
                    EnableIFrame="true" IFrameName="mainframe" IFrameUrl="about:blank" runat="server">
                </f:Region>

                <f:Region ID="bottomPanel" RegionPosition="Bottom" ShowBorder="false" ShowHeader="false" EnableCollapse="false" runat="server" Layout="Fit">
                    <Items>
                        <f:ContentPanel ID="ContentPanel3" runat="server" ShowBorder="false" ShowHeader="false">
                            <table class="bottomtable">
                                <tr>
                                    <td style="width: 300px;"></td>
                                    <td style="text-align: center;">© 2012-2017 Lockton Wattana Insurance Brokers (Thailand) Ltd. All Rights Reserved.</td>
                                    <td style="width: 300px; text-align: right;"></td>
                                </tr>
                            </table>
                        </f:ContentPanel>
                    </Items>
                </f:Region>

            </Regions>
        </f:RegionPanel>




        <dx:ASPxCallback runat="server" ClientInstanceName="cbTopMenu" ID="cbTopMenu">
            <ClientSideEvents
                CallbackComplete="function(s,e){  
                    $('.menu ul li').removeClass('selected');
                    $('.menu ul li.' + e.result).addClass('selected');
 
                    leftTree.PerformCallback('refresh');
  
                    window.frames['mainframe'].location.href = 'pages.aspx?pageid=' + e.result;
               }" />

        </dx:ASPxCallback>


        <asp:SqlDataSource ID="SqlDataSource_leftTree" runat="server" ConnectionString="<%$ ConnectionStrings:PortalConnectionString %>"
            SelectCommand="
                select [v_DesktopTabs].[TabId]
                      ,[v_DesktopTabs].[PageId]
                      ,[v_DesktopTabs].[TabName]
                      ,[v_DesktopTabs].[TabOrder]
                      ,[v_DesktopTabs].[ShowMobile]
                      ,[v_DesktopTabs].[MobileTabName]
                      ,[v_DesktopTabs].[PortalId]
                      ,case when [v_DesktopTabs].[ParentId]=1 then null else [PortalCfg_Tabs].[PageId] end as [ParentId]
                      ,[v_DesktopTabs].[Sortpath]
                      ,[v_DesktopTabs].[CreateDate]
                      ,[v_DesktopTabs].[ModifyDate]
                      ,[v_DesktopTabs].[TreeLevel]
                from 
                [v_DesktopTabs]
                left join [PortalCfg_Tabs]
                on [v_DesktopTabs].[ParentId] = [PortalCfg_Tabs].[TabId]
                where [Sortpath] like (

	                SELECT top 1 [Sortpath] + '%'
	                FROM [v_DesktopTabs]
	                where [PortalId] = @PortalId and PageId=@PageID

                )
                and [v_DesktopTabs].PageId not in(@PageID)


                and [v_DesktopTabs].TabId in(
					  SELECT Portal_UserTabs.TABID
					  FROM Portal_UserTabs
					  inner join Portal_Users on Portal_UserTabs.USERID = Portal_Users.USERID
					  where Portal_Users.UserName = @UserName and Portal_UserTabs.PortalId = @PortalId
				)



                order by [v_DesktopTabs].[Sortpath]
                ,[v_DesktopTabs].TabOrder


                ">
            <SelectParameters>
                <asp:Parameter Name="PortalId" />
                <asp:Parameter Name="UserName" />
                <asp:SessionParameter SessionField="PageID" Name="PageID" Type="String" ConvertEmptyStringToNull="true" />
            </SelectParameters>

        </asp:SqlDataSource>



<dx:ASPxPopupControl ID="clientView" runat="server" ClientInstanceName="clientView"
    Modal="True"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="Business Intelligent"
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
    Width="800"
    Height="680"
    FooterText=""
    ShowFooter="true">
    <ClientSideEvents Shown="function(s,e){ window.setTimeout(function() {LoadingPanel.Hide();},2000);}" />

    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl5" runat="server">
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>



















    </form>














    <script type="text/jscript">
        function selectMenu(menuClassName) {
            $('.menu ul li').removeClass('selected');
            $('.menu ul li.' + menuClassName).addClass('selected');

            //var tree = F(leftTreeID);
            //var treeFirstChild = tree.getRootNode().firstChild;
            //treeFirstChild.expand();
            //var treeFirstLink = treeFirstChild.firstChild;
            //tree.getSelectionModel().select(treeFirstLink);
            //window.frames['mainframe'].location.href = treeFirstLink.data.href;

            window.frames['mainframe'].location.href = 'pages.aspx?pageid=' + menuClassName;



        }
        F.ready(function () {
            selectMenu('<%=Session("PageID")%>');
        });
    </script>




</body>
</html>
