<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxCMIPremiumSetup.ascx.vb" Inherits="Modules_ucDevxCMIPremiumSetup" %>
<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="CMIPremium" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>



<dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="SqlDataSource_tblCMIPremium"
    KeyFieldName="CMI_PremiumCode" AutoGenerateColumns="False" Width="100%"
    OnHeaderFilterFillItems="grid_HeaderFilterFillItems"
    >
    <Columns>
         
      <dx:GridViewDataColumn FieldName="CarUse" > <Settings AllowHeaderFilter="True" HeaderFilterMode="List" /></dx:GridViewDataColumn>

        <dx:GridViewDataTextColumn FieldName="CMI_PremiumCode" > <Settings AllowHeaderFilter="True" HeaderFilterMode="List" /></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="CMI_Description" ></dx:GridViewDataTextColumn>

        <dx:GridViewDataTextColumn FieldName="CMINetPremium"  PropertiesTextEdit-DisplayFormatString="{0:n0}">
           
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="CMIVat" PropertiesTextEdit-DisplayFormatString="{0:n2}"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="CMIStamp" PropertiesTextEdit-DisplayFormatString="{0:n2}"></dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="CMIGrossPremium" PropertiesTextEdit-DisplayFormatString="{0:n2}">
             <Settings AllowHeaderFilter="True" HeaderFilterMode="List" />
        </dx:GridViewDataTextColumn>





    </Columns>
    <SettingsPager Mode="ShowAllRecords" />
 
</dx:ASPxGridView>

     </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>



<asp:SqlDataSource ID="SqlDataSource_tblCMIPremium" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select * from tblCMIPremium order by InOrder "
    >
    
</asp:SqlDataSource>