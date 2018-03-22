<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxCampaignSetup.ascx.vb" Inherits="Modules_ucDevxCampaignSetup" %>

    <script>
        function OnGridFocusedRowChanged() {
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'RiskGroupID;RiskGroupName', OnGetRowValues);
        }
        function OnGetRowValues(values) {
            LoadingPanel.Show();
            cbCampaign.PerformCallback(values[0]);
 
        }

      </script>

<dx:ASPxCallback runat="server" ID="cbCampaign" ClientInstanceName="cbCampaign">
    <ClientSideEvents 
        CallbackError="function(s,e){LoadingPanel.Hide(); }"
        CallbackComplete="function(s,e){  
                
                GridViewCampaign.Refresh();

         LoadingPanel.Hide();
            }" />
</dx:ASPxCallback>

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="Project Setup" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>


            <dx:ASPxFormLayout runat="server" ColCount="2" ID="settingsFormLayout" AlignItemCaptionsInAllGroups="True" Width="100%" SettingsItemCaptions-HorizontalAlign="Right">
                <Items>


                    <dx:LayoutGroup Caption="สินค้าประกันภัย" GroupBoxDecoration="HeadingLine" ColCount="2">
                        <Items>

                            <dx:LayoutItem ShowCaption="False">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>


                                        <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="SqlDataSource_RiskGroup"
                                            KeyFieldName="RiskGroupID" AutoGenerateColumns="False"
                                            Settings-ShowColumnHeaders="false"
                                            SettingsPager-Mode="ShowAllRecords"
                                            Settings-GridLines="None"
                                            SettingsBehavior-EnableRowHotTrack="true"
                                            SettingsBehavior-AllowSelectByRowClick="true">
                                            <SettingsBehavior AllowFocusedRow="True"></SettingsBehavior>
                                              <ClientSideEvents FocusedRowChanged="OnGridFocusedRowChanged" />
                                            <Columns>
                                                <dx:GridViewDataColumn FieldName="RiskGroupID" Visible="false"></dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn EditFormSettings-Visible="False" CellStyle-Wrap="False">
                                                    <DataItemTemplate>
                                                        <img src='images/risklogo/<%# Eval("RiskGroupCode")%>.jpg' />
                                                    </DataItemTemplate>
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataTextColumn FieldName="RiskGroupName" CellStyle-Wrap="False" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" />

                                            </Columns>


                                        </dx:ASPxGridView>

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>

                    <dx:LayoutGroup Caption="Campaign" GroupBoxDecoration="HeadingLine" ColCount="2">
                        <Items>

                            <dx:LayoutItem ShowCaption="False">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>



                                        <dx:ASPxGridView ID="GridViewCampaign" 
                                            ClientInstanceName="GridViewCampaign" runat="server" 
                                            DataSourceID="SqlDataSource_Campaign"
                                            KeyFieldName="CampaignID" AutoGenerateColumns="False"  >

<SettingsPager Mode="ShowPager">
                    <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                </SettingsPager>
                <SettingsBehavior EnableRowHotTrack="true" ConfirmDelete="true" />
                <SettingsSearchPanel Visible="true" />
                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
              <%--  <SettingsCommandButton> 
                     <NewButton ButtonType="Button" Text="เพิ่ม"></NewButton>
                    <UpdateButton ButtonType="Button" Text="บันทึก"></UpdateButton>
                    <EditButton ButtonType="Button" Text="แก้ไข"></EditButton>
                    <DeleteButton ButtonType="Button" Text="ลบ"></DeleteButton>
                    <CancelButton ButtonType="Button" Text="ยกเลิก"></CancelButton>
                </SettingsCommandButton>--%>


                <SettingsText PopupEditFormCaption="แก้ไขรายการ" ConfirmDelete="ยืนยันการลบ" />
                <SettingsPopup>
                    <EditForm Modal="true" MinWidth="100" Width="400" HorizontalAlign="WindowCenter" VerticalAlign="Middle" />
                    <HeaderFilter Height="200" />
                </SettingsPopup>
                <Settings ShowHeaderFilterButton="true" ShowGroupPanel="true" />



                                            <Columns>


                                                <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowEditButton="true" />


                                                <dx:GridViewDataTextColumn FieldName="CampaignCode" Caption="Code"  CellStyle-Wrap="False" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" />
                                                <dx:GridViewDataTextColumn FieldName="CampaignName" Caption="Name"  CellStyle-Wrap="False" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" />
                                                <dx:GridViewDataTextColumn FieldName="CampaignDescription" Caption="Description" CellStyle-Wrap="False" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" />


                                                <dx:GridViewDataComboBoxColumn FieldName="RenewalYear">
                                                   <PropertiesComboBox ValidationSettings-RequiredField-IsRequired="true">
                                                       <Items>
                                                            <dx:ListEditItem Text="ไม่ระบุ" Value="0" />
                                                            <dx:ListEditItem Text="ปีที่ 1" Value="1" />
                                                            <dx:ListEditItem Text="ปีที่ 2" Value="2" />
                                                            <dx:ListEditItem Text="ปีที่ 3" Value="3" />
                                                            <dx:ListEditItem Text="ปีที่ 4" Value="4" />
                                                            <dx:ListEditItem Text="ปีที่ 5" Value="5" />
                                                            <dx:ListEditItem Text="ปีที่ 6" Value="6" />
                                                            <dx:ListEditItem Text="ปีที่ 7" Value="7" />
                                                            <dx:ListEditItem Text="ปีที่ 8" Value="8" />
                                                            <dx:ListEditItem Text="ปีที่ 9" Value="9" />
                                                            <dx:ListEditItem Text="ปีที่ 10" Value="10" />
                                                       </Items>
                                                   </PropertiesComboBox>
                                                </dx:GridViewDataComboBoxColumn>


                                                <dx:GridViewDataDateColumn FieldName="EffectiveDate" CellStyle-Wrap="False" PropertiesDateEdit-ValidationSettings-RequiredField-IsRequired="true"/>
                                                <dx:GridViewDataDateColumn FieldName="ExpiryDate" CellStyle-Wrap="False" PropertiesDateEdit-ValidationSettings-RequiredField-IsRequired="true" />

                                                <dx:GridViewDataColumn FieldName="IsActive" />

                                                 <dx:GridViewDataColumn FieldName="CreateDate" EditFormSettings-Visible="False" />
                                                 <dx:GridViewDataColumn FieldName="CreateBy"  EditFormSettings-Visible="False" />
                                            </Columns>


                                        </dx:ASPxGridView>

                                         
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>

                </Items>
            </dx:ASPxFormLayout>

        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>



<asp:SqlDataSource ID="SqlDataSource_RiskGroup" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select * from tblRiskGroup where RiskGroupID not in(1,2)  order by RiskGroupID  "></asp:SqlDataSource>


<asp:SqlDataSource ID="SqlDataSource_Campaign" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    

    SelectCommand="SELECT * FROM tblCampaign where CreateBy=@UserName and RiskGroupID=@RiskGroupID "

    UpdateCommand="UPDATE tblCampaign 
    SET CampaignName=@CampaignName
    , CampaignCode = @CampaignCode
    , CampaignDescription = @CampaignDescription
    , RenewalYear=@RenewalYear
    , EffectiveDate = @EffectiveDate 
    , ExpiryDate = @ExpiryDate    
    , IsActive = @IsActive        
    , ModifyDate = getdate()
    , ModifyBy = @UserName 
    WHERE CampaignID=@CampaignID"

    InsertCommand="INSERT INTO tblCampaign (RenewalYear,RiskGroupID,CampaignCode,CampaignName,CampaignDescription,EffectiveDate,ExpiryDate,IsActive,CreateDate,CreateBy) values(@RenewalYear,@RiskGroupID,@CampaignCode,@CampaignName,@CampaignDescription,@EffectiveDate,@ExpiryDate,@IsActive,getdate(),@UserName) "
      
    >

    <SelectParameters>
        <asp:Parameter DefaultValue="" Name="UserName" />
         <asp:SessionParameter Name="RiskGroupID" SessionField="RiskGroupID" />
    </SelectParameters>

    <InsertParameters>
        <asp:Parameter Name="CampaignName" />
        <asp:Parameter Name="CampaignCode" />
        <asp:Parameter Name="CampaignDescription" />
        <asp:Parameter Name="RenewalYear" />
        <asp:Parameter Name="EffectiveDate" />
        <asp:Parameter Name="ExpiryDate" />
        <asp:Parameter Name="IsActive"/>
        <asp:Parameter Name="UserName" />   
        <asp:SessionParameter Name="RiskGroupID" SessionField="RiskGroupID" />
    </InsertParameters>

    <UpdateParameters>
        <asp:Parameter Name="CampaignID"   />
        <asp:Parameter Name="CampaignName" />
        <asp:Parameter Name="CampaignCode" />
        <asp:Parameter Name="CampaignDescription" />
        <asp:Parameter Name="RenewalYear" />
        <asp:Parameter Name="EffectiveDate" />
        <asp:Parameter Name="ExpiryDate" />
        <asp:Parameter Name="IsActive"/>
        <asp:Parameter Name="UserName" />        
    </UpdateParameters>
</asp:SqlDataSource>

 










