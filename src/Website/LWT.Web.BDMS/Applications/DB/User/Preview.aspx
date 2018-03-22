<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Applications/AppMasterPage.master" CodeFile="Preview.aspx.vb" Inherits="applications_DB_Preview" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <!-- Bootstrap CSS CDN -->
    <link href="../../../Content/bootstrap.min.css" rel="stylesheet" />
    <!-- Our Custom CSS -->
    <link href="../../../Content/sidebar/bootstrap-sidebar/style3.css" rel="stylesheet" />
    <!-- Scrollbar Custom CSS -->
<link href="../../../Content/sidebar/jquery.mCustomScrollbar.min.css" rel="stylesheet" />
    <div class="wrapper">
        <!-- Sidebar Holder -->
        <nav id="sidebar">
            <div id="dismiss">
                <i class="glyphicon glyphicon-arrow-left"></i>
            </div>
            <ul class="list-unstyled components">
                 <p><%=DashBoardName%></p>
                <%=DashBoardList %>
            </ul>
        </nav>



        <!-- Page Content Holder -->
        <div id="content">
            <button type="button" id="sidebarCollapse" class="btn btn-info navbar-btn">
                <i class="glyphicon glyphicon-menu-hamburger"></i>
            </button>


            <dx:ASPxDashboard ID="ASPxWebDashboard1" runat="server" Height="700"
                WorkingMode="Viewer" UseDashboardConfigurator="false"
                AllowExportDashboardItems="True" EnableCustomSql="true"
                IncludeDashboardIdToUrl="false"
                IncludeDashboardStateToUrl="false">

                <ClientSideEvents BeginCallback="function(s,e){window.parent.LoadingPanel.Show();}"
                    CustomDataCallback="function(s,e){window.parent.LoadingPanel.Show();}"
                    CallbackError="function(s,e){window.parent.LoadingPanel.Hide();}"
                    EndCallback="function(s,e){window.parent.LoadingPanel.Hide();}" />
            </dx:ASPxDashboard>


        </div>
    </div>


    <dx:ASPxCallback ID="cbSelectView" runat="server" ClientInstanceName="cbSelectView">
</dx:ASPxCallback>






    <div class="overlay"></div>
    <!-- Bootstrap Js CDN -->
    <script src="../../../Scripts/bootstrap.min.js"></script>

    <!-- jQuery Custom Scroller CDN -->
    <script src="../../../Content/sidebar/jquery.mCustomScrollbar.concat.min.js"></script>


    <script type="text/javascript">
        $(document).ready(function () {
            $("#sidebar").mCustomScrollbar({
                theme: "minimal"
            });

            $('#dismiss, .overlay').on('click', function () {
                $('#sidebar').removeClass('active');
                $('.overlay').fadeOut();
            });

            $('#sidebarCollapse').on('click', function () {
                $('#sidebar').addClass('active');
                $('.overlay').fadeIn();
                $('.collapse.in').toggleClass('in');
                $('a[aria-expanded=true]').attr('aria-expanded', 'false');
            });
        });
        </script>






</asp:Content>
