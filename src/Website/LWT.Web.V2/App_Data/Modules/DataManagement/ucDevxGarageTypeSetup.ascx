<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxGarageTypeSetup.ascx.vb" Inherits="Modules_ucDevxGarageTypeSetup" %>


<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="GarageType" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="SqlDataSource_GarageType"
                KeyFieldName="GarageID" AutoGenerateColumns="False" Width="500">
                <Columns>
                    <dx:GridViewCommandColumn ShowNewButtonInHeader="false" ShowEditButton="true" Width="50" />
                    <dx:GridViewDataColumn FieldName="GarageID"  Width="50">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataColumn FieldName="GarageName">
                        <EditFormSettings />
                    </dx:GridViewDataColumn>

                </Columns>
                <SettingsPager Mode="ShowAllRecords" />
                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"></SettingsEditing>
               <%-- <SettingsCommandButton>
                    <UpdateButton ButtonType="Button" Text="บันทึก"></UpdateButton>

                    <EditButton ButtonType="Button" Text="แก้ไข"></EditButton>
                    <DeleteButton ButtonType="Button" Text="ลบ"></DeleteButton>

                    <CancelButton ButtonType="Button" Text="ยกเลิก"></CancelButton>
                </SettingsCommandButton>--%>

                
                <SettingsPopup>
                    <EditForm Modal="true" Width="300" HorizontalAlign="Center" VerticalAlign="Middle" />
                </SettingsPopup>
            </dx:ASPxGridView>

        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<asp:SqlDataSource ID="SqlDataSource_GarageType" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select * from tblGarageType order by ShowID "
    UpdateCommand="update tblGarageType set GarageName=@GarageName Where GarageID=@GarageID">
    <UpdateParameters>
        <asp:Parameter Name="GarageName" />
        <asp:Parameter Name="GarageID" />
    </UpdateParameters>
</asp:SqlDataSource>
