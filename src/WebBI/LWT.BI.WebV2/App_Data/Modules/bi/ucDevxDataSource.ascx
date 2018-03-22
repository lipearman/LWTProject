<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxDataSource.ascx.vb" Inherits="Modules_ucDevxDataSource" %>

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="" runat="server" EnableAdaptivity="true" Width="100%" HeaderImage-IconID="businessobjects_bonote_32x32" >
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxGridView ID="grid"
                ClientInstanceName="grid"
                runat="server"
                DataSourceID="SqlDataSource_Data"
                AutoGenerateColumns="False"
                Width="100%" KeyFieldName="DATABASE"
                SettingsPager-Mode="ShowAllRecords">
                <ClientSideEvents
                    RowDblClick="function(s, e) {
                                                   s.StartEditRow(e.visibleIndex);
                      
                                                }     
                                                 " />

                <SettingsBehavior AllowEllipsisInText="True" AllowFocusedRow="true" />
                <SettingsResizing ColumnResizeMode="NextColumn" />
                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1">
                </SettingsEditing>
                <SettingsPopup>
                    <EditForm Modal="true" HorizontalAlign="Center" VerticalAlign="Middle" />
                </SettingsPopup>
                <Toolbars>
                    <dx:GridViewToolbar ItemAlign="Left">
                        <Items>
                            <dx:GridViewToolbarItem Command="New" />
                            <%--<dx:GridViewToolbarItem Command="Edit" />--%>
                            <dx:GridViewToolbarItem Command="Refresh" BeginGroup="true" />

                        </Items>
                    </dx:GridViewToolbar>
                </Toolbars>
                <SettingsCommandButton>
                   <EditButton Image-IconID="edit_edit_16x16office2013" />
                    <UpdateButton Text="Save" Image-IconID="actions_save_16x16devav"></UpdateButton>
                    <CancelButton Image-IconID="actions_cancel_16x16office2013"></CancelButton>
                </SettingsCommandButton>

                <Columns>

                    <%--<dx:GridViewCommandColumn ShowEditButton="true" ShowNewButtonInHeader="true" Width="50" />--%>

                    <dx:GridViewDataComboBoxColumn FieldName="DATABASE" PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" Width="100"
                        Settings-AllowHeaderFilter="True"
                        CellStyle-Wrap="False">
                        <PropertiesComboBox TextField="DATABASE" ValueField="DATABASE" DataSourceID="SqlDataSource_Database" DropDownStyle="DropDown">
                        </PropertiesComboBox>

                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewDataMemoColumn FieldName="CONNECTING" PropertiesMemoEdit-Height="100" PropertiesMemoEdit-ValidationSettings-RequiredField-IsRequired="true"
                        Settings-AllowGroup="False" CellStyle-Wrap="False">
                    </dx:GridViewDataMemoColumn>
                </Columns>
            </dx:ASPxGridView>

        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<asp:SqlDataSource ID="SqlDataSource_Data" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="select [DATABASE],[CONNECTING] from tblDataSource"
    InsertCommand="INSERT INTO  tblDataSource([DATABASE],CONNECTING) VALUES (@DATABASE,@CONNECTING)"
    UpdateCommand="UPDATE tblDataSource SET CONNECTING = @CONNECTING WHERE [DATABASE] = @DATABASE">
    <UpdateParameters>
        <asp:Parameter Name="DATABASE" />
        <asp:Parameter Name="CONNECTING" />
    </UpdateParameters>


    <InsertParameters>
        <asp:Parameter Name="DATABASE" />
        <asp:Parameter Name="CONNECTING" />
    </InsertParameters>

</asp:SqlDataSource>


<asp:SqlDataSource ID="SqlDataSource_Database" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="select convert(varchar(255),[CATALOG_NAME]) as [DATABASE] from V_DMX_Cubes  group by convert(varchar(255),[CATALOG_NAME]) order by convert(varchar(255),[CATALOG_NAME])"></asp:SqlDataSource>
