<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxRiskGroupSetup.ascx.vb" Inherits="Modules_ucDevxRiskGroupSetup" %>

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="Risk Group" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxGridView ID="grid" ClientInstanceName="grid" runat="server" DataSourceID="SqlDataSource_RiskGroup"
                KeyFieldName="RiskGroupID" AutoGenerateColumns="False">

                <SettingsPager Mode="ShowAllRecords" />
                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"></SettingsEditing>
               <%-- <SettingsCommandButton>
                    <UpdateButton ButtonType="Button" Text="บันทึก"></UpdateButton>
                    <EditButton ButtonType="Button" Text="แก้ไข"></EditButton>
                    <DeleteButton ButtonType="Button" Text="ลบ"></DeleteButton>
                    <CancelButton ButtonType="Button" Text="ยกเลิก"></CancelButton>
                    <NewButton ButtonType="Button" Text="เพิ่ม"></NewButton>
                </SettingsCommandButton>--%>
                <SettingsPopup>
                    <EditForm Modal="true" Width="500" HorizontalAlign="Center" VerticalAlign="Middle" />
                </SettingsPopup>
                 <SettingsBehavior EnableRowHotTrack="true" />


                <Columns>
                    <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowEditButton="true" />

                    <dx:GridViewDataColumn EditFormSettings-Visible="False" CellStyle-Wrap="False" >
                        <DataItemTemplate>
                            <img src='images/risklogo/<%# Eval("RiskGroupCode")%>.jpg' />
                        </DataItemTemplate>
                    </dx:GridViewDataColumn> 
                    <dx:GridViewDataTextColumn FieldName="RiskGroupCode" CellStyle-Wrap="False" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true"  />
                    <dx:GridViewDataTextColumn FieldName="RiskGroupName" CellStyle-Wrap="False" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" />
                    <dx:GridViewDataTextColumn FieldName="Description" CellStyle-Wrap="False"  PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" />
                    <dx:GridViewDataComboBoxColumn FieldName="InsuranceLine">
                        <PropertiesComboBox>
                            <Items>
                                <dx:ListEditItem Text="Life" Value="1" />
                                <dx:ListEditItem Text="Non Life" Value="2" />
                            </Items>
                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                        </PropertiesComboBox>
                         
                    </dx:GridViewDataComboBoxColumn>


                </Columns>


            </dx:ASPxGridView>



            <%--            <dx:ASPxButton ID="btnAddNew" runat="server" Text="เพิ่ม Risk Group" Image-IconID="actions_add_16x16">
                <ClientSideEvents Click="function(s, e) {           
                       CardView.AddNewCard();        
                       e.processOnServer = false; 
                      }" />
            </dx:ASPxButton>

            <dx:ASPxCardView ID="CardView" ClientInstanceName="CardView" runat="server" DataSourceID="SqlDataSource_RiskGroup"
                KeyFieldName="RiskGroupID" Width="100%">
                <SettingsEditing Mode="EditForm" />
                <SettingsSearchPanel Visible="true" />
                <Settings ShowHeaderFilterButton="true"  />
                <SettingsPopup >
                    <HeaderFilter Height="300" Width="300"  />
                </SettingsPopup>
                <SettingsBehavior AllowFocusedCard="true" ConfirmDelete="true"  />
                <SettingsCommandButton>
                    <UpdateButton ButtonType="Button" Text="บันทึก"></UpdateButton>
                    <EditButton ButtonType="Button" Text="แก้ไข"></EditButton>
                    <DeleteButton ButtonType="Button" Text="ลบ"></DeleteButton>
                    <CancelButton ButtonType="Button" Text="ยกเลิก"></CancelButton>
                </SettingsCommandButton>
                <SettingsPager Mode="ShowPager">
                    <PageSizeItemSettings Visible="true" Items="6, 12, 18" ShowAllItem="true" />
                </SettingsPager>

                <Columns>
                   
                    <dx:CardViewTextColumn FieldName="RiskGroupCode">
                        <PropertiesTextEdit>                            
                            <ValidationSettings>
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>                            
                        </PropertiesTextEdit>                        
                    </dx:CardViewTextColumn>

                    <dx:CardViewTextColumn FieldName="RiskGroupName">
                        <PropertiesTextEdit>
                            <ValidationSettings>
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:CardViewTextColumn>

                       <dx:CardViewTextColumn FieldName="Description">
                        <PropertiesTextEdit>
                            <ValidationSettings>
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:CardViewTextColumn>

                    <dx:CardViewComboBoxColumn FieldName="InsuranceLine">
                        <PropertiesComboBox ValidationSettings-RequiredField-IsRequired="true">
                            <Items>
                                <dx:ListEditItem Text="Life" Value="1" />
                                <dx:ListEditItem Text="Non Life" Value="2" />
                            </Items>
                        </PropertiesComboBox>
                    </dx:CardViewComboBoxColumn>


                </Columns>
                <CardLayoutProperties >
                    <Items> 
                        <dx:CardViewColumnLayoutItem ShowCaption="False"  >
                            <Template>
                                 <img src='images/riskgrouplogo/<%# Eval("RiskGroupCode")%>.jpg' width="150" height="100" />
                            </Template>
                        </dx:CardViewColumnLayoutItem>
                        
                        <dx:CardViewColumnLayoutItem ColumnName="RiskGroupCode" />
                        <dx:CardViewColumnLayoutItem ColumnName="RiskGroupName" />
                        <dx:CardViewColumnLayoutItem ColumnName="Description" />
                        <dx:CardViewColumnLayoutItem ColumnName="InsuranceLine" />
 

                        <dx:EditModeCommandLayoutItem HorizontalAlign="Right" />
                        <dx:CardViewCommandLayoutItem ShowEditButton="true" ShowDeleteButton="true" HorizontalAlign="Left" />

                    </Items>
                </CardLayoutProperties>
            </dx:ASPxCardView>


            --%>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>


<asp:SqlDataSource ID="SqlDataSource_RiskGroup" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select * from tblRiskGroup where RiskGroupID not in(1,2) order by RiskGroupID  "
    InsertCommand="
        INSERT INTO tblRiskGroup
                   (
                   RiskGroupCode
                  ,RiskGroupName
                  ,Description
                  ,InsuranceLine
                  ,CreationDate
                  ,CreationBy
                  )
             VALUES
             (     
                    @RiskGroupCode
                  ,@RiskGroupName
                  ,@Description
                  ,@InsuranceLine
                  ,getdate()
                  ,@UserName    
             )    
    "
    UpdateCommand="update tblRiskGroup
    set RiskGroupName=@RiskGroupName
        ,Description=@Description
        ,InsuranceLine=@InsuranceLine
        ,ModifyDate = getdate()
        ,ModifyBy=@UserName 
    where RiskGroupID=@RiskGroupID"
    DeleteCommand="delete tblRiskGroup where RiskGroupID=@RiskGroupID ">
    <InsertParameters>
        <asp:Parameter Name="RiskGroupCode" />
        <asp:Parameter Name="RiskGroupName" />
        <asp:Parameter Name="Description" />
        <asp:Parameter Name="InsuranceLine" />
        <asp:Parameter Name="UserName" />
    </InsertParameters>

    <UpdateParameters>
        <asp:Parameter Name="RiskGroupCode" />
        <asp:Parameter Name="RiskGroupName" />
        <asp:Parameter Name="Description" />
        <asp:Parameter Name="InsuranceLine" />
        <asp:Parameter Name="UserName" />
        <asp:Parameter Name="RiskGroupID" />
    </UpdateParameters>


    <DeleteParameters>
        <asp:Parameter Name="RiskGroupID" />
    </DeleteParameters>


</asp:SqlDataSource>

