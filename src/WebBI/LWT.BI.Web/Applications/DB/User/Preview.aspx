<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Applications/AppMasterPage.master" CodeFile="Preview.aspx.vb" Inherits="applications_DB_Preview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div>
        <dx:ASPxDashboardViewer ID="ASPxDashboardViewer1" runat="server"
            AllowExportDashboardItems="True"
            FullscreenMode="True"
            ColorScheme="dark">
        </dx:ASPxDashboardViewer>
    </div>
</asp:Content>
