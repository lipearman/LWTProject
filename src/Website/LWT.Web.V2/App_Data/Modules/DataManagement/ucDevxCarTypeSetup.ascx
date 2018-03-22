<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxCarTypeSetup.ascx.vb" Inherits="Modules_ucDevxCarTypeSetup" %>


<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="CarType" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>


            <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="SqlDataSource_tblCarType"
                KeyFieldName="CarType" AutoGenerateColumns="False" Width="500">
                <Columns>
                    <dx:GridViewCommandColumn ShowNewButtonInHeader="false"  Width="50" ShowEditButton="true" />
                    <dx:GridViewDataColumn FieldName="CarType" Width="50">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataColumn FieldName="CarTypeName">
                        <EditFormSettings />
                    </dx:GridViewDataColumn>

                </Columns>
                  <SettingsPopup>
                    <EditForm Modal="true" Width="400" HorizontalAlign="Center" VerticalAlign="Middle" />
                </SettingsPopup>
                <SettingsPager Mode="ShowAllRecords" /> 
                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"></SettingsEditing>
                  <%-- <SettingsCommandButton>
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

<asp:SqlDataSource ID="SqlDataSource_tblCarType" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select * from tblCarType "
    UpdateCommand="update tblCarType set CarTypeName=@CarTypeName Where CarType=@CarType">
    <UpdateParameters>
        <asp:Parameter Name="CarTypeName" />
        <asp:Parameter Name="CarType" />
    </UpdateParameters>
</asp:SqlDataSource>
