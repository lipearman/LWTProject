<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Applications/AppMasterPage.master" CodeFile="Preview.aspx.vb" Inherits="Applications_XtraReports_Master_Preview" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input runat="server" id="hdReportID" type="hidden" enableviewstate="true" />

         <dx:ASPxReportDesigner ID="reportDesigner" runat="server"  ClientInstanceName="reportDesigner">
        
     
     </dx:ASPxReportDesigner>

    <%--     <dx:ASPxWebDocumentViewer ID="reportViewer" runat="server">
        </dx:ASPxWebDocumentViewer>--%>
</asp:Content>
