<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxCubeUser.ascx.vb" Inherits="Modules_ucDevxCubeUser" %>

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="" runat="server" EnableAdaptivity="true" Width="100%" HeaderImage-IconID="support_feature_16x16office2013" >
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxGridView ID="grid"
                ClientInstanceName="grid"
                runat="server"
                DataSourceID="SqlDataSource_Data"
                AutoGenerateColumns="False"
                Width="100%" KeyFieldName="ID" SettingsBehavior-ConfirmDelete="true"
                SettingsPager-Mode="ShowAllRecords">


                <SettingsBehavior AllowEllipsisInText="True" AllowFocusedRow="true" />
                <SettingsResizing ColumnResizeMode="NextColumn" />
                <SettingsEditing Mode="PopupEditForm"  EditFormColumnCount="1">
                </SettingsEditing>
                <SettingsPopup>
                    <EditForm Modal="true" Width="400" HorizontalAlign="Center" VerticalAlign="Middle" />
                </SettingsPopup>
                <SettingsCommandButton>
                    <DeleteButton RenderMode="Button" Image-IconID="actions_cancel_16x16office2013"></DeleteButton>
                    <CancelButton RenderMode="Button" Image-IconID="actions_cancel_16x16office2013"></CancelButton>
                    <UpdateButton Text="Save" RenderMode="Button" Image-IconID="actions_save_16x16devav"></UpdateButton>
                </SettingsCommandButton>

                <%--        <SettingsCommandButton>
                    <UpdateButton  Text="Save"></UpdateButton> 
                </SettingsCommandButton>--%>
                <SettingsBehavior AllowEllipsisInText="True" AllowFocusedRow="true" />
                <SettingsResizing ColumnResizeMode="NextColumn" />
                <Toolbars>
                    <dx:GridViewToolbar ItemAlign="Left">
                        <Items>
                            <dx:GridViewToolbarItem Command="New" />
                            <dx:GridViewToolbarItem Command="Delete" />
                            <dx:GridViewToolbarItem Command="Refresh" BeginGroup="true" />

                        </Items>
                    </dx:GridViewToolbar>
                </Toolbars>

                <Columns>
                    <%--         <dx:GridViewCommandColumn Width="80"
                        CellStyle-Wrap="False"
                        ShowNewButtonInHeader="true"
                        ShowDeleteButton="true" />--%>

                    <dx:GridViewDataComboBoxColumn FieldName="USERNAME" PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" Width="200">
                        <PropertiesComboBox DataSourceID="SqlDataSource_User" TextField="USERNAME" ValueField="USERNAME">
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewDataComboBoxColumn FieldName="CUBE_ID" PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true" Caption="CUBE">
                        <PropertiesComboBox DataSourceID="SqlDataSource_Cubes" TextField="BASE_CUBE_NAME" ValueField="CUBE_ID">
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>

                </Columns>
            </dx:ASPxGridView>

        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<asp:SqlDataSource ID="SqlDataSource_Data" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="select [ID],[CUBE_ID],[USERNAME] from tblCube_User  "
    DeleteCommand="delete tblCube_User where ID=@ID">
    <DeleteParameters>
        <asp:Parameter Name="DATABASE" />
        <asp:Parameter Name="USERNAME" />
    </DeleteParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_Cubes"
    runat="server"
    ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="select [CUBE_ID],[DATABASE] + ' - ' + [CUBE] as [BASE_CUBE_NAME] from tblCube order by [BASE_CUBE_NAME] "></asp:SqlDataSource>


<asp:SqlDataSource ID="SqlDataSource_User"
    runat="server"
    ConnectionString="<%$ ConnectionStrings:PortalConnectionString %>"
    SelectCommand="select UserName from [Portal_Users] where email like '%@asia.lockton.com' Order By [UserName] "></asp:SqlDataSource>
