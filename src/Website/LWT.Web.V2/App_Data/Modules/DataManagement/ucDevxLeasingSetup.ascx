<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxLeasingSetup.ascx.vb" Inherits="Modules_ucDevxLeasingSetup" %>
<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="Leasing" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>


            <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="SqlDataSource_Leasing"
                KeyFieldName="ID" AutoGenerateColumns="False" Width="600">
                <Columns>
                    <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowEditButton="true" />
                    <dx:GridViewDataColumn FieldName="ID" Visible="false">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataTextColumn FieldName="CompanyName" CellStyle-Wrap="False" >
                        <PropertiesTextEdit>
                            <ValidationSettings Display="Dynamic">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                        <EditFormSettings VisibleIndex="0" />
                    </dx:GridViewDataTextColumn>


                    <dx:GridViewDataColumn FieldName="Address" CellStyle-Wrap="False">
                        <EditFormSettings VisibleIndex="1" />
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataColumn FieldName="Phone" CellStyle-Wrap="False">
                        <EditFormSettings VisibleIndex="2" />
                    </dx:GridViewDataColumn>


                    <dx:GridViewDataColumn FieldName="Email" CellStyle-Wrap="False">
                        <EditFormSettings VisibleIndex="3" />
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataColumn FieldName="BillingName" CellStyle-Wrap="False">
                        <EditFormSettings VisibleIndex="4" />
                    </dx:GridViewDataColumn>


                    <dx:GridViewDataTextColumn FieldName="BillingAddress" Visible="false" PropertiesTextEdit-MaxLength="255" >
                        <EditFormSettings Visible="True" VisibleIndex="5" />
                    </dx:GridViewDataTextColumn>


                    <dx:GridViewDataCheckColumn FieldName="IsActive" CellStyle-Wrap="False" >
                        <EditFormSettings VisibleIndex="6" />
                    </dx:GridViewDataCheckColumn>


                </Columns>
                <SettingsPager Mode="ShowAllRecords" /> 
                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"></SettingsEditing>
                 <%--<SettingsCommandButton>
                    <UpdateButton ButtonType="Button" Text="บันทึก"></UpdateButton>
                    <EditButton ButtonType="Button" Text="แก้ไข"></EditButton>
                    <DeleteButton ButtonType="Button" Text="ลบ"></DeleteButton>
                    <CancelButton ButtonType="Button" Text="ยกเลิก"></CancelButton>
                      <NewButton  ButtonType="Button" Text="เพิ่ม"></NewButton>
                </SettingsCommandButton>--%>
                <SettingsPopup>
                    <EditForm Modal="true" Width="500" HorizontalAlign="Center" VerticalAlign="Middle" />
                </SettingsPopup>

            </dx:ASPxGridView>


        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<asp:SqlDataSource ID="SqlDataSource_Leasing" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select * from tblLeasing order by CompanyName "
    UpdateCommand="update tblLeasing set 
    CompanyName=@CompanyName,Address=@Address,Phone=@Phone,Email=@Email,IsActive=@IsActive,BillingName=@BillingName,BillingAddress=@BillingAddress
    Where ID=@ID"
    InsertCommand="Insert into tblLeasing(CompanyName,Address,Phone,Email,IsActive,BillingName,BillingAddress)
    values(@CompanyName,@Address,@Phone,@Email,@IsActive,@BillingName,@BillingAddress)
    ">
    <UpdateParameters>
        <asp:Parameter Name="CompanyName" DbType="String" />
        <asp:Parameter Name="Address" DbType="String" />
        <asp:Parameter Name="Phone" DbType="String" />
        <asp:Parameter Name="Email" DbType="String" />
        <asp:Parameter Name="IsActive" DbType="Boolean" />
        <asp:Parameter Name="BillingName" DbType="String" />
        <asp:Parameter Name="BillingAddress" DbType="String" />
        <asp:Parameter Name="ID" DbType="Int32" />
    </UpdateParameters>

    <InsertParameters>
        <asp:Parameter Name="CompanyName" DbType="String" />
        <asp:Parameter Name="Address" DbType="String" />
        <asp:Parameter Name="Phone" DbType="String" />
        <asp:Parameter Name="Email" DbType="String" />
        <asp:Parameter Name="IsActive" DbType="Boolean" />
        <asp:Parameter Name="BillingName" DbType="String" />
        <asp:Parameter Name="BillingAddress" DbType="String" />
    </InsertParameters>
</asp:SqlDataSource>
