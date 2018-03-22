<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxDashBoadSetup.ascx.vb" Inherits="Modules_ucDevxDashBoadSetup" %>


<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="DashBoard" runat="server" Width="800">
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="SqlDataSource_Data"   
                KeyFieldName="DB_ID" AutoGenerateColumns="False" Width="100%">
                <Columns>
                    <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowEditButton="true" />

                 

                    <dx:GridViewDataTextColumn FieldName="DB_TITLE" CellStyle-Wrap="False" VisibleIndex="0" >
                        <PropertiesTextEdit>
                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="DB_XML" CellStyle-Wrap="False" VisibleIndex="1">
                        <PropertiesTextEdit>
                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>
                     
                    <dx:GridViewDataTextColumn FieldName="DB_CONN" Visible="false" CellStyle-Wrap="False" VisibleIndex="2">
                        <PropertiesTextEdit>
                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                        </PropertiesTextEdit>
                        <EditFormSettings Visible="True" />
                    </dx:GridViewDataTextColumn>


                </Columns>
 
                <SettingsEditing Mode="EditForm" EditFormColumnCount="1"></SettingsEditing>



            </dx:ASPxGridView>

        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<asp:SqlDataSource ID="SqlDataSource_Data" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="select * from tblDashBoard_DataSource order by DB_TITLE"

    InsertCommand="insert into tblDashBoard_DataSource(DB_TITLE,DB_XML,DB_CONN) values(@DB_TITLE,@DB_XML,@DB_CONN)"
    UpdateCommand="update tblDashBoard_DataSource set DB_TITLE=@DB_TITLE,DB_XML=@DB_XML,DB_CONN=@DB_CONN Where DB_ID=@DB_ID">
    <UpdateParameters>
        <asp:Parameter Name="DB_TITLE" Type="String" />
        <asp:Parameter Name="DB_XML" Type="String" />
        <asp:Parameter Name="DB_CONN" Type="String" />
        <asp:Parameter Name="DB_ID" Type="Int32" />
    </UpdateParameters>
    <InsertParameters>
         <asp:Parameter Name="DB_TITLE" Type="String" />
        <asp:Parameter Name="DB_XML" Type="String" />
          <asp:Parameter Name="DB_CONN" Type="String" />
    </InsertParameters>
</asp:SqlDataSource>


