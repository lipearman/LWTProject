<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxCube.ascx.vb" Inherits="Modules_ucDevxCube" %>

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="" runat="server" EnableAdaptivity="true" Width="100%" HeaderImage-IconID="support_feature_16x16office2013" >
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxGridView ID="grid"
                ClientInstanceName="grid"
                runat="server"
                DataSourceID="SqlDataSource_Data"
                AutoGenerateColumns="False"
                Width="100%"  
                KeyFieldName="CUBE_ID"
                SettingsPager-Mode="ShowAllRecords">
                  <ClientSideEvents
                                                    RowDblClick="function(s, e) {
                                                   s.StartEditRow(e.visibleIndex);
                      
                                                }     
                                                 " />
                <SettingsBehavior AllowEllipsisInText="True" AllowFocusedRow="true" />
                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"></SettingsEditing>
                <SettingsResizing ColumnResizeMode="NextColumn" />
                 <SettingsPopup>
                    <EditForm Modal="true" HorizontalAlign="Center" VerticalAlign="Middle" />
                </SettingsPopup>
                <SettingsCommandButton>
                  <EditButton Image-IconID="edit_edit_16x16office2013" />
                    <UpdateButton Text="Save" Image-IconID="actions_save_16x16devav"></UpdateButton>
                    <CancelButton Image-IconID="actions_cancel_16x16office2013"></CancelButton>
                </SettingsCommandButton>
                <Toolbars>
                    <dx:GridViewToolbar ItemAlign="Left">
                        <Items>
                            <dx:GridViewToolbarItem Command="New" />
                            <%--<dx:GridViewToolbarItem Command="Edit" />--%>
                            <dx:GridViewToolbarItem Command="Refresh" BeginGroup="true" />

                        </Items>
                    </dx:GridViewToolbar>
                </Toolbars>
                <Columns>
                    <%--<dx:GridViewCommandColumn ShowEditButton="true" ShowNewButtonInHeader="true" Width="50" />--%>

                    <dx:GridViewDataTextColumn FieldName="CUBE_ID" Width="70" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>



                    <dx:GridViewDataComboBoxColumn FieldName="CUBE" Caption="DATABASE/CUBE" PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" Width="300"
                        Settings-AllowHeaderFilter="True"
                        CellStyle-Wrap="False">
                        <PropertiesComboBox TextField="CUBE" ValueField="CUBE" DataSourceID="SqlDataSource_Cube" DropDownStyle="DropDown">
                        </PropertiesComboBox>

                    </dx:GridViewDataComboBoxColumn>
                    <dx:GridViewDataTextColumn FieldName="BASE_CUBE_NAME" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" Settings-AllowGroup="False" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                </Columns>
            </dx:ASPxGridView>

        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<asp:SqlDataSource ID="SqlDataSource_Data" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="select tblCube.CUBE_ID
    , tblCube.[DATABASE] + '/' + tblCube.[CUBE] as CUBE
    , tblCube.[BASE_CUBE_NAME] 
    from tblCube    
    "></asp:SqlDataSource>




<asp:SqlDataSource ID="SqlDataSource_Cube" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="select convert(varchar,CATALOG_NAME) + '/' + convert(varchar,CUBE_CAPTION) as CUBE from V_DMX_Cubes  "></asp:SqlDataSource>

