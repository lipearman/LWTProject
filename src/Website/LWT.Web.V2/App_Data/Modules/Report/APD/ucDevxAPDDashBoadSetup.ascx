<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxAPDDashBoadSetup.ascx.vb" Inherits="Modules_ucDevxAPDDashBoadSetup" %>


<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="DashBoard" runat="server" Width="800">
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="SqlDataSource_Data"
                KeyFieldName="DB_ID" AutoGenerateColumns="False" Width="100%">
                <Columns>
                    <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowEditButton="true" />

                     <dx:GridViewDataComboBoxColumn FieldName="VIEW_ID" CellStyle-Wrap="False"  >
                        <PropertiesComboBox DataSourceID="SqlDataSource_VIEWs" TextField="VIEW_TITLE" ValueField="VIEW_ID">
                           
                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>


                    <dx:GridViewDataTextColumn FieldName="DB_TITLE" CellStyle-Wrap="False">
                        <PropertiesTextEdit>
                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="DB_XML" CellStyle-Wrap="False">
                        <PropertiesTextEdit>
                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                     
                   


                </Columns>
 
                <SettingsEditing Mode="EditForm" EditFormColumnCount="3"></SettingsEditing>



            </dx:ASPxGridView>

        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<asp:SqlDataSource ID="SqlDataSource_Data" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"
    SelectCommand="select * from tblReport_DashBoard order by DB_TITLE"

    InsertCommand="insert into tblReport_DashBoard(DB_TITLE,DB_XML,VIEW_ID) values(@DB_TITLE,@DB_XML,@VIEW_ID)"
    UpdateCommand="update tblReport_DashBoard set DB_TITLE=@DB_TITLE,DB_XML=@DB_XML,VIEW_ID=@VIEW_ID Where DB_ID=@DB_ID">
    <UpdateParameters>
        <asp:Parameter Name="DB_TITLE" Type="String" />
        <asp:Parameter Name="DB_XML" Type="String" />
        <asp:Parameter Name="VIEW_ID" Type="Int32" />
        <asp:Parameter Name="DB_ID" Type="Int32" />
    </UpdateParameters>
    <InsertParameters>
         <asp:Parameter Name="DB_TITLE" Type="String" />
        <asp:Parameter Name="DB_XML" Type="String" />
        <asp:Parameter Name="VIEW_ID" Type="Int32" />
    </InsertParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_VIEWs" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"
    SelectCommand="select * from tblReport_VIEWs order by VIEW_TITLE "></asp:SqlDataSource>
