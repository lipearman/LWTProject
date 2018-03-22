<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxPolicyTypeSetup.ascx.vb" Inherits="Modules_ucDevxPolicyTypeSetup" %>

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="Policy Type" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>
            <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="SqlDataSource_tblPolicyType"
                KeyFieldName="InsureType" AutoGenerateColumns="False" Width="500">
                <Columns>
                    <dx:GridViewCommandColumn ShowNewButtonInHeader="false" ShowEditButton="true" Width="50" />
                    <dx:GridViewDataColumn FieldName="InsureType" Width="50">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Description">
                        <EditFormSettings />
                    </dx:GridViewDataColumn>
                <%--     <dx:GridViewDataColumn UnboundType="Integer" FieldName="ShowID">
                        <EditFormSettings />
                    </dx:GridViewDataColumn>--%>

                    <dx:GridViewDataSpinEditColumn FieldName="ShowID" PropertiesSpinEdit-NumberType="Integer"></dx:GridViewDataSpinEditColumn>

                </Columns>
                  <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"></SettingsEditing>
                <SettingsPager Mode="ShowAllRecords" />
             <%--   <SettingsCommandButton>
                    <UpdateButton ButtonType="Button" Text="บันทึก"></UpdateButton>
                    <EditButton ButtonType="Button" Text="แก้ไข"></EditButton>
                    <DeleteButton ButtonType="Button" Text="ลบ"></DeleteButton>
                    <CancelButton ButtonType="Button" Text="ยกเลิก"></CancelButton>
                    <NewButton  ButtonType="Button" Text="เพิ่ม"></NewButton>
                </SettingsCommandButton>--%>
                <SettingsPopup>
                    <EditForm Modal="true" Width="300" HorizontalAlign="Center" VerticalAlign="Middle" />
                </SettingsPopup>
            </dx:ASPxGridView>

        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<asp:SqlDataSource ID="SqlDataSource_tblPolicyType" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select * from tblPolicyType order by ShowID "
    UpdateCommand="update tblPolicyType set Description=@Description,ShowID=@ShowID Where InsureType=@InsureType">
    <UpdateParameters>
        <asp:Parameter Name="InsureType" />
        <asp:Parameter Name="Description" />
         <asp:Parameter Name="ShowID" />
    </UpdateParameters>
</asp:SqlDataSource>
