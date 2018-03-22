<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxDashBoardDataSource.ascx.vb" Inherits="Modules_ucDevxDashBoardDataSource" %>

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="" runat="server" EnableAdaptivity="true" Width="100%" HeaderImage-IconID="businessobjects_bonote_32x32">
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxGridView ID="grid"
                ClientInstanceName="grid"
                runat="server"
                DataSourceID="SqlDataSource_Data"
                AutoGenerateColumns="False"
                Width="100%" KeyFieldName="DS_ID"
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
                    <dx:GridViewDataTextColumn FieldName="TITLE" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true">
                    </dx:GridViewDataTextColumn>

 

                    <dx:GridViewDataComboBoxColumn FieldName="CONN_TYPE" PropertiesComboBox-ValidationSettings-RequiredField-IsRequired="true"
                       
                        CellStyle-Wrap="False">
                        <PropertiesComboBox>
                            <Items>
                                <dx:ListEditItem Text="msSqlConnection" Value="msSqlConnection" />
                                <dx:ListEditItem Text="olapConnection" Value="olapConnection" />
                            </Items>
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>

                   <dx:GridViewDataMemoColumn FieldName="CONN" PropertiesMemoEdit-Height="100" PropertiesMemoEdit-ValidationSettings-RequiredField-IsRequired="true"
                        Settings-AllowGroup="False" CellStyle-Wrap="False">
                    </dx:GridViewDataMemoColumn>
                </Columns>
            </dx:ASPxGridView>

        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<asp:SqlDataSource ID="SqlDataSource_Data" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="select DS_ID,TITLE,CONN,CONN_TYPE from tblDashBoard_DataSource where PortalId=@PortalId"
    InsertCommand="INSERT INTO  tblDashBoard_DataSource(TITLE,CONN,CONN_TYPE,PortalId) VALUES (@TITLE,@CONN,@CONN_TYPE,@PortalId)"
    UpdateCommand="UPDATE tblDashBoard_DataSource SET TITLE = @TITLE, CONN = @CONN, CONN_TYPE = @CONN_TYPE WHERE [DS_ID] = @DS_ID">
    <UpdateParameters>
        <asp:Parameter Name="TITLE" />
        <asp:Parameter Name="CONN" />
        <asp:Parameter Name="CONN_TYPE" />
        <asp:Parameter Name="DS_ID" />
    </UpdateParameters>


    <InsertParameters>
        <asp:Parameter Name="TITLE" />
        <asp:Parameter Name="CONN" />
        <asp:Parameter Name="CONN_TYPE" />
        <asp:Parameter Name="PortalId" />
    </InsertParameters>

    <SelectParameters>
          <asp:Parameter Name="PortalId" />
    </SelectParameters>
</asp:SqlDataSource>


