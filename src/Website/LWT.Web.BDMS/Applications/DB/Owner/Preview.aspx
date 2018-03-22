<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Applications/AppMasterPage.master" CodeFile="Preview.aspx.vb" Inherits="applications_DB_Preview_Owner" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script src="../../../Scripts/DeleteExtension.js"></script>


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
        WorkingMode="Viewer"  UseDashboardConfigurator="false"
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

                   //sender.dashboardDesigner.unregisterExtension('dxdde-data-source-wizard');

                    dashboardControl.unregisterExtension('data-source-wizard');
                }
            }"
            
            
             />
    </dx:ASPxDashboard>

</asp:Content>
