<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxCarUseSetup.ascx.vb" Inherits="Modules_ucDevxCarUseSetup" %>


<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="CarUse" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>



            <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="SqlDataSource_tblCarUse"
                KeyFieldName="CarUse" AutoGenerateColumns="False" Width="500">
                <Columns>
                    <dx:GridViewCommandColumn ShowNewButtonInHeader="false" ShowEditButton="true" Width="50" />
                    <dx:GridViewDataColumn FieldName="CarUse" Width="50">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="txtCarUse">
                        <EditFormSettings />
                    </dx:GridViewDataColumn>

                </Columns>
                <SettingsPager Mode="ShowAllRecords" />
                
                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"></SettingsEditing>
             
                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"></SettingsEditing>
                <%--<SettingsCommandButton>
                    <UpdateButton ButtonType="Button" Text="บันทึก"></UpdateButton>

                    <EditButton ButtonType="Button" Text="แก้ไข"></EditButton>
                    <DeleteButton ButtonType="Button" Text="ลบ"></DeleteButton>

                    <CancelButton ButtonType="Button" Text="ยกเลิก"></CancelButton>
                </SettingsCommandButton>--%>
                <SettingsPopup>
                    <EditForm Modal="true" Width="400" HorizontalAlign="Center" VerticalAlign="Middle" />
                </SettingsPopup>
            </dx:ASPxGridView>

        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<asp:SqlDataSource ID="SqlDataSource_tblCarUse" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select * from tblCarUse order by CarUse"
    UpdateCommand="update tblCarUse set txtCarUse=@txtCarUse Where CarUse=@CarUse">
    <UpdateParameters>
        <asp:Parameter Name="txtCarUse" />
        <asp:Parameter Name="CarUse" />
    </UpdateParameters>
</asp:SqlDataSource>
