<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Applications/AppMasterPage.master" CodeFile="Preview.aspx.vb" Inherits="applications_DB_Preview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../../Scripts/DeleteExtension.js"></script>
    <%--<script src="../../../Scripts/PreviewExtension.js"></script>--%>
 
    <style type="text/css">
        /*.dx-dashboard-settings-form {
        visibility:hidden
    }*/
        /*.dx-dashboard-main-menu-item:nth-child(2)
        , .dx-dashboard-main-menu-item:nth-child(4)
        , .dx-dashboard-main-menu-item:nth-child(5)
        {
            display: none;
        }*/
    </style>
    <dx:ASPxDashboard ID="ASPxWebDashboard1" runat="server" Height="700"
        UseDashboardConfigurator="false"
        AllowExportDashboardItems="True" EnableCustomSql="true"
        IncludeDashboardIdToUrl="false"
        IncludeDashboardStateToUrl="false">

        <ClientSideEvents BeginCallback="function(s,e){window.parent.LoadingPanel.Show();}"
            CustomDataCallback="function(s,e){window.parent.LoadingPanel.Show();}"
            CallbackError="function(s,e){window.parent.LoadingPanel.Hide();}"
            EndCallback="function(s,e){window.parent.LoadingPanel.Hide();}"
           
            BeforeRender="function(sender, eventArgs) {
                if (sender) {
                    
             
                    var dashboardControl = sender.getDashboardControl();


                    var extension = new DevExpress.Dashboard.DashboardPanelExtension(dashboardControl);
                    //extension.allowSwitchToDesigner(false);
                    dashboardControl.registerExtension(extension);

                    dashboardControl.registerExtension(new DeleteDashboardExtension(sender));
                    //dashboardControl.registerExtension(new PreviewDashboardExtension(sender));
                    //dashboardControl.unregisterExtension('data-source-wizard');
          
                }
            }"
            
            
             />
    </dx:ASPxDashboard>





    <dx:ASPxCallback ID="cbPreview" runat="server" ClientInstanceName="cbPreview">
    <ClientSideEvents
        CallbackComplete="function(s, e) { 
            if(e.result=='' || e.result==null)
            {
                alert('Dashboard Details');
            }
            else
            {
                clientView2.SetContentUrl('../User/Preview.aspx');
                clientView2.Show();  
            }
                  
        }" />
</dx:ASPxCallback>


    
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



            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>



</asp:Content>
