<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Demo.aspx.vb" Inherits="Demo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        /*.dx-dashboard-settings-form {
        visibility:hidden
    }*/
        /*.dx-dashboard-main-menu-item:nth-child(2)
    ,.dx-dashboard-main-menu-item:nth-child(4)
    ,.dx-dashboard-main-menu-item:nth-child(5) {
       display:none
    }*/
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div>
            <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel" Modal="True"></dx:ASPxLoadingPanel>

            <%--<dx:ASPxDashboard ID="ASPxWebDashboard1" runat="server" OnConfigureDataConnection="ASPxDashboard1_ConfigureDataConnection"></dx:ASPxDashboard>--%>
        

<%--            <dx:ASPxDashboardViewer ID="ASPxDashboardViewer1" runat="server">
            </dx:ASPxDashboardViewer>--%>

                  <dx:ASPxDashboard ID="ASPxWebDashboard1" runat="server"  Height="700"
                    WorkingMode = "Viewer" UseDashboardConfigurator="true"
					AllowExportDashboardItems ="True"
					IncludeDashboardIdToUrl = "false"
					IncludeDashboardStateToUrl = "false"
          >

       <ClientSideEvents BeginCallback="function(s,e){LoadingPanel.Show();}"
            CustomDataCallback="function(s,e){LoadingPanel.Show();}"
            CallbackError="function(s,e){LoadingPanel.Hide();}"
            EndCallback="function(s,e){LoadingPanel.Hide();}"

           BeforeRender="function onBeforeRender(sender, eventArgs) {
                if (sender) {
                    
                    var dashboardControl = sender.getDashboardControl();
                    
                    dashboardControl.registerExtension(new DevExpress.Dashboard.DashboardPanelExtension(dashboardControl));
                }
            }"


            />
        </dx:ASPxDashboard>
        </div>
    </form>



</body>







</html>
