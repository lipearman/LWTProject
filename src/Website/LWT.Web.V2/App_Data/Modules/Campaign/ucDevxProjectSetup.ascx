<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxProjectSetup.ascx.vb" Inherits="Modules_ucDevxProjectSetup" %>

    <script>
        function OnGridFocusedRowChanged() {
            grid.GetRowValues(grid.GetFocusedRowIndex(), 'RiskGroupID;RiskGroupName', OnGetRowValues);
        }
        function OnGetRowValues(values) {
            LoadingPanel.Show();
            cbProject.PerformCallback(values[0]);
 
        }

      </script>

<dx:ASPxCallback runat="server" ID="cbProject" ClientInstanceName="cbProject">
    <ClientSideEvents 
        CallbackError="function(s,e){LoadingPanel.Hide(); }"
        CallbackComplete="function(s,e){  
                
                GridViewProject.Refresh();

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

                    <dx:LayoutGroup Caption="โครงการ" GroupBoxDecoration="HeadingLine" ColCount="2">
                        <Items>

                            <dx:LayoutItem ShowCaption="False">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>



                                        <dx:ASPxGridView ID="GridViewProject" ClientInstanceName="GridViewProject" runat="server" DataSourceID="SqlDataSource_Project"
                                            KeyFieldName="ProjectID" AutoGenerateColumns="False"  >

                                            <SettingsPager Mode="ShowAllRecords" />
                                            <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1"></SettingsEditing>
                                         <%--   <SettingsCommandButton>
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
                                             <Settings ShowHeaderFilterButton="true"   />

                                            <Columns>
                                                <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowEditButton="true" />
                                                <dx:GridViewDataTextColumn FieldName="ProjectCode" CellStyle-Wrap="False" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" />
                                                <dx:GridViewDataTextColumn FieldName="ProjectName" CellStyle-Wrap="False" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" />
                                                <dx:GridViewDataTextColumn FieldName="CLIENTCODE" CellStyle-Wrap="False" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" />
                                                <dx:GridViewDataComboBoxColumn FieldName="AECode" CellStyle-Wrap="False">
                                                    <PropertiesComboBox TextField="Name" ValueField="Code" IncrementalFilteringMode="None" DataSourceID="SqlDataSource_AE">
                                                        <ValidationSettings>
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                    </PropertiesComboBox>

                                                </dx:GridViewDataComboBoxColumn>

                                                <dx:GridViewDataColumn FieldName="IsActive" />


                                            </Columns>


                                        </dx:ASPxGridView>




                                        <%--                                        <dx:ASPxButton ID="btnAddProject" runat="server" Text="เพิ่มโครงการ" Image-IconID="actions_add_16x16">
                                            <ClientSideEvents Click="function(s, e) {           
            CardView.AddNewCard();        
            e.processOnServer = false; 
            }" />
                                        </dx:ASPxButton>
    <dx:ASPxCardView ID="CardView" ClientInstanceName="CardView" runat="server" DataSourceID="SqlDataSource_Project" KeyFieldName="ProjectID" Width="100%">
                                            <SettingsEditing Mode="EditForm" />
                                            <SettingsSearchPanel Visible="true" />
                                            <Settings ShowHeaderFilterButton="true" />
                                            <SettingsPopup>
                                                <HeaderFilter Height="300" Width="300" />
                                            </SettingsPopup>
                                            <SettingsBehavior AllowFocusedCard="true" ConfirmDelete="true" />
                                            <SettingsCommandButton>
                                                <UpdateButton ButtonType="Button" Text="บันทึก"></UpdateButton>
                                                <EditButton ButtonType="Button" Text="แก้ไข"></EditButton>
                                                <DeleteButton ButtonType="Button" Text="ลบ"></DeleteButton>
                                                <CancelButton ButtonType="Button" Text="ยกเลิก"></CancelButton>
                                            </SettingsCommandButton>
                                            <SettingsPager Mode="ShowPager">
                                                <PageSizeItemSettings Visible="true" Items="6, 9, 12" ShowAllItem="true" />
                                            </SettingsPager>

                                            <Columns>
                                                <dx:CardViewTextColumn FieldName="ProjectCode">
                                                    <PropertiesTextEdit>
                                                        <ValidationSettings>
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                    </PropertiesTextEdit>
                                                </dx:CardViewTextColumn>
                                                <dx:CardViewTextColumn FieldName="ProjectName">
                                                    <PropertiesTextEdit>
                                                        <ValidationSettings>
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                    </PropertiesTextEdit>
                                                </dx:CardViewTextColumn>
                                                <dx:CardViewTextColumn FieldName="CLIENTCODE">
                                                    <PropertiesTextEdit MaxLength="2">
                                                        <ValidationSettings>
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                    </PropertiesTextEdit>
                                                </dx:CardViewTextColumn>
                                                <dx:CardViewComboBoxColumn FieldName="AECode">
                                                    <PropertiesComboBox TextField="Name" ValueField="Code" IncrementalFilteringMode="None" DataSourceID="SqlDataSource_AE">
                                                        <ValidationSettings>
                                                            <RequiredField IsRequired="true" />
                                                        </ValidationSettings>
                                                    </PropertiesComboBox>
                                                </dx:CardViewComboBoxColumn>
                                                <dx:CardViewColumn FieldName="IsActive" />
                                            </Columns>
                                            <CardLayoutProperties>
                                                <Items>

                                                    <dx:CardViewColumnLayoutItem ColumnName="ProjectCode" />
                                                    <dx:CardViewColumnLayoutItem ColumnName="ProjectName" />
                                                    <dx:CardViewColumnLayoutItem ColumnName="CLIENTCODE" />
                                                    <dx:CardViewColumnLayoutItem ColumnName="AECode" />
                                                    <dx:CardViewColumnLayoutItem ColumnName="IsActive" />
                                                    <dx:EditModeCommandLayoutItem HorizontalAlign="Right" />
                                                    <dx:CardViewCommandLayoutItem ShowEditButton="true" ShowDeleteButton="true" HorizontalAlign="Left" />

                                                </Items>
                                            </CardLayoutProperties>
                                        </dx:ASPxCardView>--%>
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


<asp:SqlDataSource ID="SqlDataSource_Project" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    DeleteCommand="delete tblProject WHERE ProjectID=@ProjectID "
    InsertCommand="INSERT INTO [tblProject] (RiskGroupID,CLIENTCODE,[AECode],[ProjectCode], [ProjectName], [CreationDate], [CreationBy], [IsActive]) values(@RiskGroupID, @CLIENTCODE,@AECode, @ProjectCode, @ProjectName, getdate(), @UserName, @IsActive) "
    SelectCommand="SELECT * FROM [tblProject] where AECode in(select AECode from tblProjectUserAssignment where UserName=@UserName) and RiskGroupID=@RiskGroupID "
    UpdateCommand="UPDATE [tblProject] SET CLIENTCODE=@CLIENTCODE, [AECode] = @AECode,[ProjectCode] = @ProjectCode, [ProjectName] = @ProjectName, [ModifyDate] = getdate(), [ModifyBy] = @UserName, [IsActive] = @IsActive WHERE ProjectID=@ProjectID">
    <DeleteParameters>
        <asp:Parameter Name="ProjectID" Type="Int32" />
    </DeleteParameters>

    <SelectParameters>
        <asp:Parameter DefaultValue="" Name="UserName" />
         <asp:SessionParameter Name="RiskGroupID" SessionField="RiskGroupID" />
    </SelectParameters>

    <InsertParameters>
        <asp:Parameter Name="AECode" Type="String" />
        <asp:Parameter Name="ProjectCode" Type="String" />
        <asp:Parameter Name="ProjectName" Type="String" />
        <asp:Parameter Name="CLIENTCODE" Type="String" />
        <asp:Parameter Name="IsActive" Type="Boolean" />
        <asp:Parameter Name="UserName" />
         <asp:SessionParameter Name="RiskGroupID" SessionField="RiskGroupID" />
    </InsertParameters>
    <UpdateParameters>
        <asp:Parameter Name="ProjectID" Type="Int32" />
        <asp:Parameter Name="AECode" Type="String" />
        <asp:Parameter Name="ProjectCode" Type="String" />
        <asp:Parameter Name="ProjectName" Type="String" />
        <asp:Parameter Name="CLIENTCODE" Type="String" />
        <asp:Parameter Name="UserName" />
        <asp:Parameter Name="IsActive" Type="Boolean" />
    </UpdateParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_AE" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="SELECT rtrim(AccountExecutive) as Code, rtrim(AccountExecutive) + ' - ' + rtrim(Name) as Name 
    FROM [Register.AccountExecutive] where AccountExecutive in(select AECode from tblProjectUserAssignment where UserName=@UserName) 
    order by AccountExecutive">
    <SelectParameters>
        <asp:Parameter DefaultValue="" Name="UserName" />
    </SelectParameters>

</asp:SqlDataSource>










