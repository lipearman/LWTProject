<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxMotorPremium.ascx.vb" Inherits="Modules_ucDevxMotorPremium" %>
<script>
    function OnFocusedCardChanged() {
        LoadingPanel.Show();
        cardView.GetCardValues(cardView.GetFocusedCardIndex(), 'CampaignID;CampaignName', OnGetCardValues);
    }
    function OnGetCardValues(values) {
        //alert(values);
        LoadingPanel.Show();
        //lbUserName.SetText(values)
        //cbOpenPopup.PerformCallback(values);
        //lbModelImage.SetImageUrl('images/carlogo/' + values[1] + '.gif');
        //lbBrandName.SetText(values[1])
        cbCampaign.PerformCallback(values[0]);
    }

</script>
<dx:ASPxCallback runat="server" ID="cbCampaign" ClientInstanceName="cbCampaign">
    <ClientSideEvents CallbackComplete="function(s,e){                                                                                     
                LoadingPanel.Hide(); 
                gridCarPremium.Refresh();
            }" />
</dx:ASPxCallback>


<script type="text/javascript">
    var textSeparator = ";";
    function OnListBoxSelectionChanged(listBox, args) {
        if (args.index == 0)
            args.isSelected ? listBox.SelectAll() : listBox.UnselectAll();
        UpdateSelectAllItemState();
        UpdateText();
    }
    function UpdateSelectAllItemState() {
        IsAllSelected() ? checkListBoxNewModel.SelectIndices([0]) : checkListBoxNewModel.UnselectIndices([0]);
    }
    function IsAllSelected() {
        var selectedDataItemCount = checkListBoxNewModel.GetItemCount() - (checkListBoxNewModel.GetItem(0).selected ? 0 : 1);
        return checkListBoxNewModel.GetSelectedItems().length == selectedDataItemCount;
    }
    function UpdateText() {
        var selectedItems = checkListBoxNewModel.GetSelectedItems();
        newModel.SetText(GetSelectedItemsText(selectedItems));
    }
    function SynchronizeListBoxValues(dropDown, args) {
        checkListBoxNewModel.UnselectAll();
        var texts = dropDown.GetText().split(textSeparator);
        var values = GetValuesByTexts(texts);
        checkListBoxNewModel.SelectValues(values);
        UpdateSelectAllItemState();
        UpdateText(); // for remove non-existing texts
    }
    function GetSelectedItemsText(items) {
        var texts = [];
        for (var i = 0; i < items.length; i++)
            if (items[i].index != 0)
                texts.push(items[i].text);
        return texts.join(textSeparator);
    }
    function GetValuesByTexts(texts) {
        var actualValues = [];
        var item;
        for (var i = 0; i < texts.length; i++) {
            item = checkListBoxNewModel.FindItemByText(texts[i]);
            if (item != null)
                actualValues.push(item.value);
        }
        return actualValues;
    }

    function OnGetSelectedFieldValues(selectedValues) {
        if (selectedValues.length == 0) return;


        cbDeletePremium.PerformCallback(selectedValues);

    }

</script>


<script type="text/javascript">
    function button1_Click(s, e) {
        if (gridCarPremium.IsCustomizationWindowVisible())
            gridCarPremium.HideCustomizationWindow();
        else
            gridCarPremium.ShowCustomizationWindow();
        UpdateButtonText();
    }
    function gridCarPremium_CustomizationWindowCloseUp(s, e) {
        UpdateButtonText();
    }
    function UpdateButtonText() {
        var text = gridCarPremium.IsCustomizationWindowVisible() ? "Hide" : "Show";
        text += " More Column";
        button1.SetText(text);
    }
</script>



<script type="text/javascript">
    var keyValueCoverage;

    function OnMoreInfoCoverage(element, key) {
        callbackPanel_MoreInfoCoverage.SetContentHtml("");
        popupMoreInfoCoverage.ShowAtElement(element);
        keyValueCoverage = key;
    }
    function popup_ShownMoreInfoCoverage(s, e) {
        callbackPanel_MoreInfoCoverage.PerformCallback(keyValueCoverage);
    }
</script>

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="Motor Campaign" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>


            <dx:ASPxCallbackPanel ID="callbackPanel"
                ClientInstanceName="callbackPanel" runat="server"
                RenderMode="Table">

                <PanelCollection>
                    <dx:PanelContent ID="PanelContent1" runat="server">

                        <dx:ASPxGridLookup ID="gridCampaign" ClientInstanceName="gridCampaign" runat="server" SelectionMode="Single"
                            DataSourceID="SqlDataSource_MotorCampaign" Width="300" Caption="Campaign"
                            KeyFieldName="CampaignID" TextFormatString="{1} - {2}">
                            <ClientSideEvents ValueChanged="function (s, e){
                                         callbackPanel.PerformCallback('select_campaign');
                                         }" />

                            <GridViewProperties>

                                <Settings ShowHeaderFilterButton="true" />
                                <SettingsSearchPanel Visible="true" />


                            </GridViewProperties>
                            <Columns>
                                <dx:GridViewDataColumn FieldName="CampaignID" Visible="false" />
                                <dx:GridViewDataColumn FieldName="CampaignCode" Width="50" CellStyle-Wrap="False" />
                                <dx:GridViewDataColumn FieldName="CampaignName" CellStyle-Wrap="False" />

                                <dx:GridViewDataComboBoxColumn FieldName="RenewalYear" CellStyle-Wrap="False">
                                    <PropertiesComboBox>
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

                                <dx:GridViewDataColumn FieldName="EffectiveDate" CellStyle-Wrap="False" />
                                <dx:GridViewDataColumn FieldName="ExpiryDate" CellStyle-Wrap="False" />
                                <dx:GridViewDataColumn FieldName="Insurer" CellStyle-Wrap="False" />
                            </Columns>

                        </dx:ASPxGridLookup>


                        <asp:SqlDataSource ID="SqlDataSource_MotorCampaign" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
                            SelectCommand="select * 
                            , (select count(*) from (select distinct Uwriter from dbo.[tblCampaign.CommIn] where tblCampaign.CampaignID = [tblCampaign.CommIn].campaignID) a ) as Insurer
                            from tblCampaign where RiskGroupID=@RiskGroupID and IsActive=1 ">
                            <SelectParameters>
                                <asp:Parameter Name="RiskGroupID" />
                            </SelectParameters>
                        </asp:SqlDataSource>


                        <dx:ASPxPanel runat="server" ID="pnCarPremium" ClientVisible="false">
                            <PanelCollection>
                                <dx:PanelContent>

                                    <dx:ASPxFormLayout ID="frmdata" ClientInstanceName="frmdata" Width="100px" runat="server" ColCount="1">
                                        <Items>
                                            <dx:LayoutGroup Caption="Premium">
                                                <Items>
                                                    <dx:LayoutItem Caption="" ShowCaption="False">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer
                                                                ID="LayoutItemNestedControlContainer9"
                                                                runat="server">





                                                                <dx:ASPxButton ID="btnAddNewPremium" runat="server" Text="Add Premium" Image-IconID="actions_add_16x16">
                                                                    <ClientSideEvents Click="function(s, e) {           
                                           gridCarPremium.AddNewRow();
                                           e.processOnServer = false; 
                                          }" />
                                                                </dx:ASPxButton>


                                                                <dx:ASPxButton ID="btnDeletePremium" runat="server" Text="Delete">
                                                                    <Image IconID="actions_remove_16x16"></Image>
                                                                    <ClientSideEvents Click="function(s, e) {                                                              
                                            if(confirm('Confirm Delete?'))  
                                                {
                                                   LoadingPanel.Show();  
                                                   gridCarPremium.GetSelectedFieldValues('PremiumID', OnGetSelectedFieldValues);
                                                }                          
                                                e.processOnServer = false;                       
                                        }" />
                                                                </dx:ASPxButton>
                                                                <dx:ASPxCallback runat="server" ID="cbDeletePremium" ClientInstanceName="cbDeletePremium">
                                                                    <ClientSideEvents CallbackComplete="function(s,e){  
                                                            LoadingPanel.Hide();                                                                                                                                                                                                           
                                                            gridCarPremium.Refresh();
                                                            e.processOnServer = false;
                                                        }" />
                                                                </dx:ASPxCallback>




                                                                <dx:ASPxButton ID="btnDownloadPremiumFormat" runat="server" Text="Download Premium Data Format " Image-IconID="export_exporttoxlsx_16x16" />
                                                                <dx:ASPxButton ID="btnAddPremium" runat="server" Text="Add Multiple Premium" CausesValidation="False">
                                                                    <Image IconID="actions_add_16x16"></Image>
                                                                    <ClientSideEvents Click="function(s, e) {    
                                             popUpNewPremium.Show();               
                                             callbackPanel_NewPremium.PerformCallback('');                             
                                            e.processOnServer = false;                       
                                            }" />
                                                                </dx:ASPxButton>

                                                                <dx:ASPxButton runat="server" ID="button1" ClientInstanceName="button1" Text="Show More Column"
                                                                    UseSubmitBehavior="false" AutoPostBack="false">
                                                                    <ClientSideEvents Click="button1_Click" />
                                                                </dx:ASPxButton>



                                                                <dx:ASPxGridView ID="gridCarPremium" ClientInstanceName="gridCarPremium" runat="server" DataSourceID="SqlDataSource_CarPremium"
                                                                    KeyFieldName="PremiumID" AutoGenerateColumns="False" Settings-ShowTitlePanel="false" SettingsBehavior-AutoExpandAllGroups="true"
                                                                    Width="800">

                                                                    <ClientSideEvents CustomizationWindowCloseUp="gridCarPremium_CustomizationWindowCloseUp" />


                                                                    <SettingsPager Mode="ShowAllRecords" />
                                                                    <SettingsBehavior EnableCustomizationWindow="true" ConfirmDelete="true" EnableRowHotTrack="true" />
                                                                    <SettingsText Title="Premium Setup" PopupEditFormCaption="Premium Setup" />

                                                                    <Settings ShowTitlePanel="true" ShowHeaderFilterButton="true" ShowGroupPanel="true" />

                                                                    <SettingsPopup>
                                                                        <EditForm Modal="true" />
                                                                        <CustomizationWindow VerticalAlign="TopSides" HorizontalAlign="LeftSides" Width="200" Height="400" />
                                                                    </SettingsPopup>


                                                                    <SettingsEditing EditFormColumnCount="1"
                                                                        Mode="PopupEditForm"
                                                                        PopupEditFormHorizontalAlign="WindowCenter"
                                                                        PopupEditFormVerticalAlign="WindowCenter"
                                                                        PopupEditFormModal="True" />

<%--                                                                    <SettingsCommandButton>
                                                                        <NewButton ButtonType="Button" Text="เพิ่ม"></NewButton>
                                                                        <UpdateButton ButtonType="Button" Text="บันทึก"></UpdateButton>

                                                                        <EditButton ButtonType="Button" Text="แก้ไข"></EditButton>
                                                                        <DeleteButton ButtonType="Button" Text="ลบ"></DeleteButton>

                                                                        <CancelButton ButtonType="Button" Text="ยกเลิก"></CancelButton>
                                                                    </SettingsCommandButton>
                                                                     --%>
                                                                    <Columns>

                                                                        <dx:GridViewCommandColumn ShowSelectCheckbox="True"
                                                                            ShowClearFilterButton="true"
                                                                            VisibleIndex="0"
                                                                            SelectAllCheckboxMode="Page" />
                                                                        <dx:GridViewCommandColumn
                                                                            ShowEditButton="true"
                                                                            CellStyle-Wrap="False" />

                                                                        <%--<dx:GridViewDataColumn FieldName="Underwriter" Caption="ประกันภัย" Settings-AllowGroup="False" CellStyle-Wrap="False"></dx:GridViewDataColumn>--%>
                                                                        <dx:GridViewDataComboBoxColumn FieldName="Underwriter" Caption="ประกันภัย" CellStyle-Wrap="False">
                                                                            <PropertiesComboBox DataSourceID="SqlDataSource_Underwriter"
                                                                                TextField="AccountContact"
                                                                                ValueField="Underwriter">
                                                                            </PropertiesComboBox>
                                                                        </dx:GridViewDataComboBoxColumn>

                                                                        <dx:GridViewDataColumn FieldName="coverage1" Caption="รหัส" EditFormSettings-Visible="False" Width="50" CellStyle-HorizontalAlign="Center" EditCellStyle-VerticalAlign="Bottom">
                                                                            <DataItemTemplate>

                                                                                <a href="javascript:void(0);" onclick="OnMoreInfoCoverage(this, '<%# Eval("PremiumID").ToString().Trim()%>')"><b><%# Eval("coverage1").ToString().Trim()%></b></a>

                                                                            </DataItemTemplate>
                                                                        </dx:GridViewDataColumn>


                                                                        <%--<dx:GridViewDataColumn FieldName="coverage1" Caption="coverage1" Settings-AllowGroup="False" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="coverage2" Caption="coverage2" Settings-AllowGroup="False" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="coverage3" Caption="coverage3" Settings-AllowGroup="False" CellStyle-Wrap="False"></dx:GridViewDataColumn>--%>

                                                                        <dx:GridViewDataColumn FieldName="Brand" Caption="ยี่ห้อ"   CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                                        <dx:GridViewDataColumn FieldName="Model" Caption="รุ่น" Settings-AllowGroup="False" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                                        <dx:GridViewDataColumn FieldName="Name" Caption="ชื่อ/แบบ" Settings-AllowGroup="False" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                                        <dx:GridViewDataColumn FieldName="ModelCode" Caption="Chassis" Visible="false" Settings-AllowGroup="False" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="CarPrice" Caption="ราคารถ" PropertiesTextEdit-DisplayFormatString="{0:n0}" Settings-AllowGroup="False" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataColumn FieldName="CarAge" Caption="อายุรถ" Settings-AllowGroup="False" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="SumInsuredFrom" Caption="ทุนจาก" PropertiesTextEdit-DisplayFormatString="{0:n0}" Settings-AllowGroup="False" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="SumInsuredTo" Caption="ถึงทุน" PropertiesTextEdit-DisplayFormatString="{0:n0}" Settings-AllowGroup="False" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="NetPremium" Caption="เบี้ยก่อนภาษี" PropertiesTextEdit-DisplayFormatString="{0:n2}" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="Stamp" Caption="แสตมป์" PropertiesTextEdit-DisplayFormatString="{0:n2}" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="Vat" Caption="ภาษี" PropertiesTextEdit-DisplayFormatString="{0:n2}" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="GrossPremium" Caption="เบี้ยรวม" PropertiesTextEdit-DisplayFormatString="{0:n2}" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="Discount" Caption="ส่วนลด" PropertiesTextEdit-DisplayFormatString="{0:n2}" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="TotalPremium" Caption="เบี้ยทั้งหมด" PropertiesTextEdit-DisplayFormatString="{0:n2}" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>

                                                                        <dx:GridViewDataColumn FieldName="WHT_Rate1" Caption="Rate1" Visible="false" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                                        <dx:GridViewDataColumn FieldName="WHT_Rate2" Caption="Rate2" Visible="false" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                                        <dx:GridViewDataColumn FieldName="WHT_Rate3" Caption="Rate3" Visible="false" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                                        <dx:GridViewDataTextColumn FieldName="SinglePrice" Caption="เบี้ยประกัน" PropertiesTextEdit-DisplayFormatString="{0:n0}" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>

                                                                        <dx:GridViewDataColumn FieldName="IsActive" Caption="IsActive" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False" CellStyle-Wrap="False"></dx:GridViewDataColumn>

                                                                        <%--
,PrefixClientCode
,coverage4
,coverage5
,coverage6
,coverage7
,coverage8
,coverage9
,coverage10
,coverage11
,coverage12
,coverage13
,coverage14
,coverage15    
                                                                        --%>
                                                                    </Columns>





                                                                    <Templates>


                                                                        <EditForm>



                                                                            <dx:ASPxFormLayout ID="formPremium" ClientInstanceName="formPremium" runat="server"
                                                                                Paddings-Padding="0" RequiredMarkDisplayMode="None"
                                                                                AlignItemCaptionsInAllGroups="True"
                                                                                ShowItemCaptionColon="true" ColCount="2"
                                                                                CellStyle-Paddings-Padding="0"
                                                                                GroupBoxDecoration="HeadingLine">

                                                                                <Items>
                                                                                    <dx:LayoutGroup Caption="ความคุ้มครอง" ColCount="2" ColSpan="2">
                                                                                        <Items>
                                                                                            <dx:LayoutItem Caption="บริษัทประกัน" FieldName="Underwriter">
                                                                                                <LayoutItemNestedControlCollection>
                                                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">
                                                                                                        <dx:ASPxComboBox ID="Underwriter" ClientInstanceName="Underwriter"
                                                                                                            runat="server"
                                                                                                            DropDownStyle="DropDownList" Value='<%# Eval("Underwriter")%>'
                                                                                                            DataSourceID="SqlDataSource_Underwriter"
                                                                                                            TextField="AccountContact" ValidationSettings-ValidationGroup="formPremium"
                                                                                                            ValueField="Underwriter">
                                                                                                            <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                                                                            <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                                                                                        </dx:ASPxComboBox>
                                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                                </LayoutItemNestedControlCollection>
                                                                                            </dx:LayoutItem>

                                                                                            <dx:LayoutItem Caption="ความคุ้มครอง" FieldName="CoverageID">
                                                                                                <LayoutItemNestedControlCollection>
                                                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">




                                                                                                        <dx:ASPxGridLookup ID="CoverageID"
                                                                                                            runat="server"
                                                                                                            SelectionMode="Single"
                                                                                                            DataSourceID="SqlDataSource_CoverageLookup"
                                                                                                            ClientInstanceName="CoverageID"
                                                                                                            KeyFieldName="CoverageID"
                                                                                                            TextFormatString="{0}"
                                                                                                            Value='<%# Eval("CoverageID")%>'>
                                                                                                            <ClearButton Visibility="True"></ClearButton>
                                                                                                            <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                                                                            <CaptionSettings RequiredMarkDisplayMode="Hidden" />

                                                                                                            <Columns>
                                                                                                                <dx:GridViewDataComboBoxColumn FieldName="Coverage1" Settings-AllowGroup="False" Settings-AllowHeaderFilter="True" Caption="ลักษณะการใช้" CellStyle-Wrap="False">
                                                                                                                    <PropertiesComboBox DataSourceID="SqlDataSource_CarUse"
                                                                                                                        TextField="txtCarUse"
                                                                                                                        ValueField="CarUse">
                                                                                                                    </PropertiesComboBox>
                                                                                                                </dx:GridViewDataComboBoxColumn>

                                                                                                                <dx:GridViewDataComboBoxColumn FieldName="Coverage2" Settings-AllowGroup="False" Settings-AllowHeaderFilter="True" CellStyle-Wrap="False" Caption="ประเภท">
                                                                                                                    <PropertiesComboBox DataSourceID="SqlDataSource_PolicyType"
                                                                                                                        TextField="Description"
                                                                                                                        ValueField="InsureType">

                                                                                                                        <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                                                                                                                    </PropertiesComboBox>

                                                                                                                </dx:GridViewDataComboBoxColumn>

                                                                                                                <dx:GridViewDataComboBoxColumn FieldName="Coverage3" Settings-AllowGroup="False" Settings-AllowHeaderFilter="True" Caption="ประเภทอู่" CellStyle-Wrap="False">
                                                                                                                    <PropertiesComboBox DataSourceID="SqlDataSource_Garage"
                                                                                                                        TextField="GarageName"
                                                                                                                        ValueField="GarageID">
                                                                                                                    </PropertiesComboBox>
                                                                                                                </dx:GridViewDataComboBoxColumn>


                                                                                                                <dx:GridViewDataTextColumn FieldName="Coverage4" Caption="ความรับผิด/คน"
                                                                                                                    Settings-AllowGroup="False"
                                                                                                                    Settings-AllowHeaderFilter="True"
                                                                                                                    PropertiesTextEdit-DisplayFormatString="{0:n0}">
                                                                                                                </dx:GridViewDataTextColumn>
                                                                                                                <dx:GridViewDataTextColumn FieldName="Coverage5" Settings-AllowGroup="False" Caption="ความรับผิด/ครั้ง" Settings-AllowHeaderFilter="True"></dx:GridViewDataTextColumn>
                                                                                                                <dx:GridViewDataTextColumn FieldName="Coverage6" Settings-AllowGroup="False" Caption="ทรัพย์สิน" Settings-AllowHeaderFilter="True"></dx:GridViewDataTextColumn>

                                                                                                                <dx:GridViewDataTextColumn FieldName="Coverage7" Settings-AllowGroup="False" Caption="ต่อตัวรถ" Settings-AllowHeaderFilter="False" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                                                                                                                <dx:GridViewDataTextColumn FieldName="Coverage8" Settings-AllowGroup="False" Caption="สูญหาย/ไฟไหม้" Settings-AllowHeaderFilter="False" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                                                                                                                <dx:GridViewDataTextColumn FieldName="Coverage9" Settings-AllowGroup="False" Caption="น้ำท่วม" Settings-AllowHeaderFilter="False" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>



                                                                                                                <dx:GridViewDataTextColumn FieldName="Coverage10" Settings-AllowGroup="False" Caption="PA ผู้ขับขี่" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
                                                                                                                <%--<dx:GridViewDataTextColumn FieldName="Coverage11" Settings-AllowGroup="False" Caption="ผู้โดยสาร" Settings-AllowHeaderFilter="False"  ></dx:GridViewDataTextColumn>--%>
                                                                                                                <dx:GridViewDataTextColumn FieldName="Coverage12" Settings-AllowGroup="False" Caption="PA ผู้โดยสาร" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>

                                                                                                                <dx:GridViewDataTextColumn FieldName="Coverage13" Settings-AllowGroup="False" Caption="ค่ารักษาพยาบาล" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
                                                                                                                <dx:GridViewDataTextColumn FieldName="Coverage14" Settings-AllowGroup="False" Caption="การประกันตัว" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
                                                                                                                <dx:GridViewDataTextColumn FieldName="Coverage15" Settings-AllowGroup="False" Caption="Deduct" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>



                                                                                                            </Columns>


                                                                                                        </dx:ASPxGridLookup>
                                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                                </LayoutItemNestedControlCollection>
                                                                                            </dx:LayoutItem>

                                                                                            <dx:LayoutItem Caption="ยี่ห้อ" FieldName="Brand">
                                                                                                <LayoutItemNestedControlCollection>
                                                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server">

                                                                                                        <dx:ASPxComboBox runat="server"
                                                                                                            ID="Brand" ClientInstanceName="Brand"
                                                                                                            DropDownStyle="DropDownList"
                                                                                                            IncrementalFilteringMode="StartsWith"
                                                                                                            DataSourceID="SqlDataSource_CarBrand"
                                                                                                            EnableSynchronization="true"
                                                                                                            TextField="Name"
                                                                                                            ValueField="Name"
                                                                                                            Value='<%# Eval("Brand")%>'
                                                                                                            Enablecallback="true">

                                                                                                            <ClientSideEvents
                                                                                                                Init="function(s, e) { 
                                                                                                                    var modelname = Model.GetText();
                                                                                                                    Model.PerformCallback(s.GetValue().toString()); 
                                                                                                                    Model.SetValue(modelname);
                                                                                                                }"
                                                                                                                SelectedIndexChanged="function(s, e) {   Model.PerformCallback(s.GetValue()); }" />
                                                                                                            <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                                                                            <CaptionSettings RequiredMarkDisplayMode="Auto" />
                                                                                                        </dx:ASPxComboBox>

                                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                                </LayoutItemNestedControlCollection>
                                                                                            </dx:LayoutItem>

                                                                                            <dx:LayoutItem Caption="รุ่น" FieldName="Model">
                                                                                                <LayoutItemNestedControlCollection>
                                                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server">

                                                                                                        <dx:ASPxComboBox runat="server"
                                                                                                            ID="Model" ClientInstanceName="Model"
                                                                                                            TextField="Name"
                                                                                                            ValueField="Name"
                                                                                                            IncrementalFilteringMode="StartsWith" OnCallback="Model_Callback"
                                                                                                            Value='<%# Eval("Model")%>'
                                                                                                            DropDownStyle="DropDownList">
                                                                                                            <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                                                                            <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                                                                                        </dx:ASPxComboBox>

                                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                                </LayoutItemNestedControlCollection>
                                                                                            </dx:LayoutItem>

                                                                                            <dx:LayoutItem Caption="ชื่อ/แบบ" FieldName="Name">
                                                                                                <LayoutItemNestedControlCollection>
                                                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">

                                                                                                        <dx:ASPxTextBox runat="server" ID="Name" Value='<%# Eval("Name")%>'>
                                                                                                            <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                                                                            <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                                                                                        </dx:ASPxTextBox>

                                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                                </LayoutItemNestedControlCollection>
                                                                                            </dx:LayoutItem>

                                                                                            <dx:LayoutItem Caption="Chassis" FieldName="ModelCode">
                                                                                                <LayoutItemNestedControlCollection>
                                                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server">

                                                                                                        <dx:ASPxTextBox runat="server" ID="ModelCode" Value='<%# Eval("ModelCode")%>'>
                                                                                                            <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                                                                            <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                                                                                        </dx:ASPxTextBox>

                                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                                </LayoutItemNestedControlCollection>
                                                                                            </dx:LayoutItem>

                                                                                            <dx:LayoutItem Caption="ราคารถ" FieldName="CarPrice">
                                                                                                <LayoutItemNestedControlCollection>
                                                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server">

                                                                                                        <dx:ASPxSpinEdit ID="CarPrice" ValidationSettings-ValidationGroup="formPremium"
                                                                                                            Value='<%# Eval("CarPrice")%>'
                                                                                                            DisplayFormatString="#,#0"
                                                                                                            NumberType="Integer" MinValue="0" DecimalPlaces="0"
                                                                                                            AllowMouseWheel="false"
                                                                                                            SpinButtons-ShowIncrementButtons="false" runat="server">
                                                                                                            <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                                                                            <CaptionSettings RequiredMarkDisplayMode="Hidden" />

                                                                                                        </dx:ASPxSpinEdit>

                                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                                </LayoutItemNestedControlCollection>
                                                                                            </dx:LayoutItem>

                                                                                            <dx:LayoutItem Caption="อายุรถ" FieldName="CarAge">
                                                                                                <LayoutItemNestedControlCollection>
                                                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server">

                                                                                                        <dx:ASPxSpinEdit ID="CarAge" ValidationSettings-ValidationGroup="formPremium"
                                                                                                            Value='<%# Eval("CarAge")%>'
                                                                                                            DisplayFormatString="#,#0"
                                                                                                            NumberType="Integer" MinValue="0"
                                                                                                            AllowMouseWheel="false"
                                                                                                            SpinButtons-ShowIncrementButtons="false" runat="server">
                                                                                                            <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                                                                            <CaptionSettings RequiredMarkDisplayMode="Hidden" />

                                                                                                        </dx:ASPxSpinEdit>

                                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                                </LayoutItemNestedControlCollection>
                                                                                            </dx:LayoutItem>

                                                                                            <dx:LayoutItem Caption="ช่วงทุน" FieldName="SumInsuredFrom">
                                                                                                <LayoutItemNestedControlCollection>
                                                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server">

                                                                                                        <dx:ASPxSpinEdit ID="SumInsuredFrom" ValidationSettings-ValidationGroup="formPremium"
                                                                                                            Value='<%# Eval("SumInsuredFrom")%>'
                                                                                                            DisplayFormatString="#,#0"
                                                                                                            NumberType="Integer" MinValue="0" DecimalPlaces="0"
                                                                                                            AllowMouseWheel="false"
                                                                                                            SpinButtons-ShowIncrementButtons="false" runat="server">
                                                                                                            <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                                                                            <CaptionSettings RequiredMarkDisplayMode="Hidden" />

                                                                                                        </dx:ASPxSpinEdit>

                                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                                </LayoutItemNestedControlCollection>
                                                                                            </dx:LayoutItem>

                                                                                            <dx:LayoutItem Caption="ถึง" FieldName="SumInsuredTo">
                                                                                                <LayoutItemNestedControlCollection>
                                                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server">

                                                                                                        <dx:ASPxSpinEdit ID="SumInsuredTo" ValidationSettings-ValidationGroup="formPremium"
                                                                                                            Value='<%# Eval("SumInsuredTo")%>'
                                                                                                            DisplayFormatString="#,#0"
                                                                                                            NumberType="Integer" MinValue="0" DecimalPlaces="0"
                                                                                                            AllowMouseWheel="false"
                                                                                                            SpinButtons-ShowIncrementButtons="false" runat="server">
                                                                                                            <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                                                                            <CaptionSettings RequiredMarkDisplayMode="Hidden" />

                                                                                                        </dx:ASPxSpinEdit>

                                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                                </LayoutItemNestedControlCollection>
                                                                                            </dx:LayoutItem>

                                                                                            <dx:LayoutItem Caption="IsActive" FieldName="IsActive">
                                                                                                <LayoutItemNestedControlCollection>
                                                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer25" runat="server">

                                                                                                        <dx:ASPxCheckBox runat="server" ID="IsActive" Value='<%# Eval("IsActive")%>'>
                                                                                                        </dx:ASPxCheckBox>

                                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                                </LayoutItemNestedControlCollection>
                                                                                            </dx:LayoutItem>

                                                                                        </Items>
                                                                                    </dx:LayoutGroup>



                                                                                    <dx:LayoutGroup Caption="เบี้ยประกัน" ColCount="1">
                                                                                        <Items>

                                                                                            <dx:LayoutItem Caption="Net Prem." FieldName="NetPremium">
                                                                                                <LayoutItemNestedControlCollection>
                                                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server">

                                                                                                        <dx:ASPxSpinEdit ID="NetPremium" ValidationSettings-ValidationGroup="formPremium"
                                                                                                            Value='<%# Eval("NetPremium")%>'
                                                                                                            DisplayFormatString="#,#0"
                                                                                                            NumberType="Integer" MinValue="0"
                                                                                                            AllowMouseWheel="false"
                                                                                                            SpinButtons-ShowIncrementButtons="false" runat="server">
                                                                                                            <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                                                                            <CaptionSettings RequiredMarkDisplayMode="Hidden" />

                                                                                                        </dx:ASPxSpinEdit>

                                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                                </LayoutItemNestedControlCollection>
                                                                                            </dx:LayoutItem>

                                                                                            <dx:LayoutItem Caption="Stamp" FieldName="Stamp">
                                                                                                <LayoutItemNestedControlCollection>
                                                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server">

                                                                                                        <dx:ASPxSpinEdit ID="Stamp" ValidationSettings-ValidationGroup="formPremium"
                                                                                                            Value='<%# Eval("Stamp")%>'
                                                                                                            DisplayFormatString="#,#0"
                                                                                                            NumberType="Integer" MinValue="0"
                                                                                                            AllowMouseWheel="false"
                                                                                                            SpinButtons-ShowIncrementButtons="false" runat="server">
                                                                                                            <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                                                                            <CaptionSettings RequiredMarkDisplayMode="Hidden" />

                                                                                                        </dx:ASPxSpinEdit>

                                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                                </LayoutItemNestedControlCollection>
                                                                                            </dx:LayoutItem>

                                                                                            <dx:LayoutItem Caption="Vat" FieldName="Vat">
                                                                                                <LayoutItemNestedControlCollection>
                                                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server">

                                                                                                        <dx:ASPxSpinEdit ID="Vat" ValidationSettings-ValidationGroup="formPremium"
                                                                                                            Value='<%# Eval("Vat")%>'
                                                                                                            DisplayFormatString="#,#0"
                                                                                                            NumberType="Integer" MinValue="0"
                                                                                                            AllowMouseWheel="false"
                                                                                                            SpinButtons-ShowIncrementButtons="false" runat="server">
                                                                                                            <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                                                                            <CaptionSettings RequiredMarkDisplayMode="Hidden" />

                                                                                                        </dx:ASPxSpinEdit>

                                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                                </LayoutItemNestedControlCollection>
                                                                                            </dx:LayoutItem>

                                                                                            <dx:LayoutItem Caption="Gross Prem." FieldName="GrossPremium">
                                                                                                <LayoutItemNestedControlCollection>
                                                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer17" runat="server">

                                                                                                        <dx:ASPxSpinEdit ID="GrossPremium" ValidationSettings-ValidationGroup="formPremium"
                                                                                                            Value='<%# Eval("GrossPremium")%>'
                                                                                                            DisplayFormatString="#,#0"
                                                                                                            NumberType="Integer" MinValue="0"
                                                                                                            AllowMouseWheel="false"
                                                                                                            SpinButtons-ShowIncrementButtons="false" runat="server">
                                                                                                            <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                                                                            <CaptionSettings RequiredMarkDisplayMode="Hidden" />

                                                                                                        </dx:ASPxSpinEdit>

                                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                                </LayoutItemNestedControlCollection>
                                                                                            </dx:LayoutItem>

                                                                                            <dx:LayoutItem Caption="Discount" FieldName="Discount">
                                                                                                <LayoutItemNestedControlCollection>
                                                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer18" runat="server">

                                                                                                        <dx:ASPxSpinEdit ID="Discount" ValidationSettings-ValidationGroup="formPremium"
                                                                                                            Value='<%# Eval("Discount")%>'
                                                                                                            DisplayFormatString="#,#0"
                                                                                                            NumberType="Integer" MinValue="0"
                                                                                                            AllowMouseWheel="false"
                                                                                                            SpinButtons-ShowIncrementButtons="false" runat="server">
                                                                                                            <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                                                                            <CaptionSettings RequiredMarkDisplayMode="Hidden" />

                                                                                                        </dx:ASPxSpinEdit>

                                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                                </LayoutItemNestedControlCollection>
                                                                                            </dx:LayoutItem>

                                                                                            <dx:LayoutItem Caption="Total Prem." FieldName="Total Prem">
                                                                                                <LayoutItemNestedControlCollection>
                                                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer19" runat="server">

                                                                                                        <dx:ASPxSpinEdit ID="TotalPremium" ValidationSettings-ValidationGroup="formPremium"
                                                                                                            Value='<%# Eval("TotalPremium")%>'
                                                                                                            DisplayFormatString="#,#0"
                                                                                                            NumberType="Integer" MinValue="0"
                                                                                                            AllowMouseWheel="false"
                                                                                                            SpinButtons-ShowIncrementButtons="false" runat="server">
                                                                                                            <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                                                                            <CaptionSettings RequiredMarkDisplayMode="Hidden" />

                                                                                                        </dx:ASPxSpinEdit>

                                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                                </LayoutItemNestedControlCollection>
                                                                                            </dx:LayoutItem>

                                                                                            <dx:LayoutItem Caption="SinglePrice" FieldName="SinglePrice">
                                                                                                <LayoutItemNestedControlCollection>
                                                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer20" runat="server">

                                                                                                        <dx:ASPxSpinEdit ID="SinglePrice" ValidationSettings-ValidationGroup="formPremium"
                                                                                                            Value='<%# Eval("SinglePrice")%>'
                                                                                                            DisplayFormatString="#,#0"
                                                                                                            NumberType="Integer" MinValue="0"
                                                                                                            AllowMouseWheel="false"
                                                                                                            SpinButtons-ShowIncrementButtons="false" runat="server">
                                                                                                            <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                                                                                            <CaptionSettings RequiredMarkDisplayMode="Hidden" />

                                                                                                        </dx:ASPxSpinEdit>

                                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                                </LayoutItemNestedControlCollection>
                                                                                            </dx:LayoutItem>



                                                                                        </Items>
                                                                                    </dx:LayoutGroup>
                                                                                    <dx:LayoutGroup Caption="WHT Discount" ColCount="1">
                                                                                        <Items>
                                                                                            <dx:LayoutItem Caption="WHT_Rate1" FieldName="WHT_Rate1">
                                                                                                <LayoutItemNestedControlCollection>
                                                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer21" runat="server">

                                                                                                        <dx:ASPxTextBox runat="server" ID="WHT_Rate1" Value='<%# Eval("WHT_Rate1")%>'></dx:ASPxTextBox>

                                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                                </LayoutItemNestedControlCollection>
                                                                                            </dx:LayoutItem>
                                                                                            <dx:LayoutItem Caption="WHT_Rate2" FieldName="WHT_Rate2">
                                                                                                <LayoutItemNestedControlCollection>
                                                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer22" runat="server">

                                                                                                        <dx:ASPxTextBox runat="server" ID="WHT_Rate2" Value='<%# Eval("WHT_Rate2")%>'></dx:ASPxTextBox>

                                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                                </LayoutItemNestedControlCollection>
                                                                                            </dx:LayoutItem>

                                                                                            <dx:LayoutItem Caption="WHT_Rate3" FieldName="WHT_Rate3">
                                                                                                <LayoutItemNestedControlCollection>
                                                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer23" runat="server">

                                                                                                        <dx:ASPxTextBox runat="server" ID="WHT_Rate3" Value='<%# Eval("WHT_Rate3")%>'></dx:ASPxTextBox>

                                                                                                    </dx:LayoutItemNestedControlContainer>
                                                                                                </LayoutItemNestedControlCollection>
                                                                                            </dx:LayoutItem>

                                                                                        </Items>
                                                                                    </dx:LayoutGroup>





                                                                                    <dx:LayoutItem ShowCaption="False">
                                                                                        <LayoutItemNestedControlCollection>
                                                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer24" runat="server">

                                                                                                <%--                                        <dx:ASPxButton ID="btnSaveCoverage" runat="server" Text="Save" ValidationContainerID="formCoverage" CausesValidation="true">
                                            <Image IconID="save_save_16x16"></Image>
                                        </dx:ASPxButton>--%>

                                                                                                <table style="float: left">
                                                                                                    <tr>
                                                                                                        <td>
                                                                                                            <dx:ASPxButton ID="b1" runat="server" UseSubmitBehavior="false" CausesValidation="true" AutoPostBack="false"
                                                                                                                Text="Save">
                                                                                                                <ClientSideEvents Click="function(s){
                                                                                if(ASPxClientEdit.ValidateGroup('formPremium')) gridCarPremium.UpdateEdit();
                                                                                                     
                                                                            }
                                                                    
                                                                        " />
                                                                                                            </dx:ASPxButton>
                                                                                                        </td>
                                                                                                        <td>&nbsp;</td>
                                                                                                        <td>
                                                                                                            <dx:ASPxButton ID="b2" runat="server" UseSubmitBehavior="false" AutoPostBack="false"
                                                                                                                Text="Cancel">
                                                                                                                <ClientSideEvents Click="function(s){
                                                                        gridCarPremium.CancelEdit();
                                                                     }
                                                                        " />
                                                                                                            </dx:ASPxButton>
                                                                                                        </td>
                                                                                                    </tr>
                                                                                                </table>
                                                                                            </dx:LayoutItemNestedControlContainer>
                                                                                        </LayoutItemNestedControlCollection>
                                                                                    </dx:LayoutItem>





                                                                                </Items>
                                                                            </dx:ASPxFormLayout>

                                                                        </EditForm>


                                                                    </Templates>
                                                                </dx:ASPxGridView>



                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>

                                                </Items>

                                            </dx:LayoutGroup>

                                        </Items>
                                        <Styles LayoutItem-Caption-Font-Bold="true" />
                                    </dx:ASPxFormLayout>

                                    <asp:SqlDataSource ID="SqlDataSource_CarPremium" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
                                        SelectCommand="select * from V_MotorPremium where CampaignID = @CampaignID"
                                        InsertCommand="
                                            INSERT INTO tblCarPremium
                                           (
                                            CampaignID
                                           ,Underwriter
                                           ,Name
                                           ,Brand
                                           ,Model
                                           ,ModelCode
                                           ,CarPrice
                                           ,SumInsuredFrom
                                           ,SumInsuredTo
                                           ,NetPremium
                                           ,Stamp
                                           ,Vat
                                           ,GrossPremium
                                           ,Discount
                                           ,TotalPremium
                                           ,WHT_Rate1
                                           ,WHT_Rate2
                                           ,WHT_Rate3
                                           ,SinglePrice
                                           ,CarAge
                                           ,CoverageID
                                           ,IsActive
                                           ,CreateDate
                                           ,CreateBy 
                                           )
                                     VALUES
                                           (
			                                @CampaignID
                                           ,@Underwriter
                                           ,@Name
                                           ,@Brand
                                           ,@Model
                                           ,@ModelCode
                                           ,@CarPrice
                                           ,@SumInsuredFrom
                                           ,@SumInsuredTo
                                           ,@NetPremium
                                           ,@Stamp
                                           ,@Vat
                                           ,@GrossPremium
                                           ,@Discount
                                           ,@TotalPremium
                                           ,@WHT_Rate1
                                           ,@WHT_Rate2
                                           ,@WHT_Rate3
                                           ,@SinglePrice
                                           ,@CarAge
                                           ,@CoverageID
                                           ,@IsActive
                                            ,GETDATE()
                                            ,@UserName 
                                           ) "
                                        UpdateCommand="
                                            UPDATE tblCarPremium
                                               SET CampaignID = @CampaignID
                                                  ,Underwriter = @Underwriter
                                                  ,Name = @Name
                                                  ,Brand = @Brand
                                                  ,Model = @Model
                                                  ,ModelCode = @ModelCode
                                                  ,CarPrice = @CarPrice
                                                  ,SumInsuredFrom = @SumInsuredFrom
                                                  ,SumInsuredTo = @SumInsuredTo
                                                  ,NetPremium = @NetPremium
                                                  ,Stamp = @Stamp
                                                  ,Vat = @Vat
                                                  ,GrossPremium = @GrossPremium
                                                  ,Discount = @Discount
                                                  ,TotalPremium = @TotalPremium
                                                  ,WHT_Rate1 = @WHT_Rate1
                                                  ,WHT_Rate2 = @WHT_Rate2
                                                  ,WHT_Rate3 = @WHT_Rate3
                                                  ,SinglePrice = @SinglePrice
                                                  ,CarAge = @CarAge
                                                  ,CoverageID = @CoverageID
                                                  ,IsActive = @IsActive
                                                ,ModifyDate = getdate()
                                                ,ModifyBy=@UserName 
                                             WHERE PremiumID=@PremiumID
                
                                        "
                                        DeleteCommand="delete tblCarPremium where PremiumID=@PremiumID ">
                                        <SelectParameters>
                                            <asp:SessionParameter Name="CampaignID" SessionField="CampaignID" />
                                        </SelectParameters>
                                        <InsertParameters>
                                            <asp:Parameter Name="CampaignID" />
                                            <asp:Parameter Name="Underwriter" />
                                            <asp:Parameter Name="Name" />
                                            <asp:Parameter Name="Brand" />
                                            <asp:Parameter Name="Model" />
                                            <asp:Parameter Name="ModelCode" />
                                            <asp:Parameter Name="CarPrice" />
                                            <asp:Parameter Name="SumInsuredFrom" />
                                            <asp:Parameter Name="SumInsuredTo" />
                                            <asp:Parameter Name="NetPremium" />
                                            <asp:Parameter Name="Stamp" />
                                            <asp:Parameter Name="Vat" />
                                            <asp:Parameter Name="GrossPremium" />
                                            <asp:Parameter Name="Discount" />
                                            <asp:Parameter Name="TotalPremium" />
                                            <asp:Parameter Name="WHT_Rate1" />
                                            <asp:Parameter Name="WHT_Rate2" />
                                            <asp:Parameter Name="WHT_Rate3" />
                                            <asp:Parameter Name="SinglePrice" />
                                            <asp:Parameter Name="CarAge" />
                                            <asp:Parameter Name="CoverageID" />
                                            <asp:Parameter Name="IsActive" />
                                            <asp:Parameter Name="UserName" />
                                        </InsertParameters>

                                        <UpdateParameters>
                                            <asp:Parameter Name="PremiumID" />
                                            <asp:Parameter Name="CampaignID" />
                                            <asp:Parameter Name="Underwriter" />
                                            <asp:Parameter Name="Name" />
                                            <asp:Parameter Name="Brand" />
                                            <asp:Parameter Name="Model" />
                                            <asp:Parameter Name="ModelCode" />
                                            <asp:Parameter Name="CarPrice" />
                                            <asp:Parameter Name="SumInsuredFrom" />
                                            <asp:Parameter Name="SumInsuredTo" />
                                            <asp:Parameter Name="NetPremium" />
                                            <asp:Parameter Name="Stamp" />
                                            <asp:Parameter Name="Vat" />
                                            <asp:Parameter Name="GrossPremium" />
                                            <asp:Parameter Name="Discount" />
                                            <asp:Parameter Name="TotalPremium" />
                                            <asp:Parameter Name="WHT_Rate1" />
                                            <asp:Parameter Name="WHT_Rate2" />
                                            <asp:Parameter Name="WHT_Rate3" />
                                            <asp:Parameter Name="SinglePrice" />
                                            <asp:Parameter Name="CarAge" />
                                            <asp:Parameter Name="CoverageID" />
                                            <asp:Parameter Name="IsActive" />
                                            <asp:Parameter Name="UserName" />


                                        </UpdateParameters>


                                        <DeleteParameters>
                                            <asp:Parameter Name="PremiumID" />
                                        </DeleteParameters>
                                    </asp:SqlDataSource>


                                </dx:PanelContent>


                            </PanelCollection>

                        </dx:ASPxPanel>



                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>


        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>


<dx:ASPxPopupControl ID="popUpNewPremium"
    ClientInstanceName="popUpNewPremium"
    runat="server" Modal="True"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="Premium"
    AllowDragging="true"
    EnableAnimation="true" CloseAction="CloseButton"
    EnableCallbackAnimation="true"
    EnableViewState="true" Width="138px">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">

             <dx:ASPxCallbackPanel ID="callbackPanel_NewPremium" ClientInstanceName="callbackPanel_NewPremium" runat="server"
                RenderMode="Table">
                <ClientSideEvents CallbackError="function(s,e){
                        LoadingPanel.Hide(); 
                    }" />
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent2" runat="server">



            <dx:ASPxFormLayout ID="formNewPremium" ClientInstanceName="formPremium" runat="server"
                Paddings-Padding="0" RequiredMarkDisplayMode="None"
                AlignItemCaptionsInAllGroups="True"
                ShowItemCaptionColon="true"
                CellStyle-Paddings-Padding="0"
                GroupBoxDecoration="HeadingLine">

                <Items>



                    <dx:LayoutGroup Caption="ความคุ้มครอง" ColCount="2">
                        <Items>
                            <dx:LayoutItem Caption="บริษัทประกัน">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">
                                        <dx:ASPxComboBox ID="newUnderwriter"
                                            runat="server" Width="210px"
                                            DropDownStyle="DropDownList"
                                            DataSourceID="SqlDataSource_Underwriter"
                                            TextField="AccountContact" ValidationSettings-ValidationGroup="formNewPremium"
                                            ValueField="Underwriter">
                                            <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                            <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="ความคุ้มครอง">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer26" runat="server">
                                        <dx:ASPxGridLookup ID="newCoverageID"
                                            runat="server" Width="210px"
                                            SelectionMode="Single"
                                            DataSourceID="SqlDataSource_CoverageLookup"
                                            KeyFieldName="CoverageID" ValidationSettings-ValidationGroup="formNewPremium"
                                            TextFormatString="{0}">
                                            <ClearButton Visibility="True"></ClearButton>
                                            <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                            <CaptionSettings RequiredMarkDisplayMode="Hidden" />

                                            <Columns>
                                                <dx:GridViewDataComboBoxColumn FieldName="Coverage1" Settings-AllowGroup="False" Settings-AllowHeaderFilter="True" Caption="ลักษณะการใช้" CellStyle-Wrap="False">
                                                    <PropertiesComboBox DataSourceID="SqlDataSource_CarUse"
                                                        TextField="txtCarUse"
                                                        ValueField="CarUse">
                                                    </PropertiesComboBox>
                                                </dx:GridViewDataComboBoxColumn>

                                                <dx:GridViewDataComboBoxColumn FieldName="Coverage2" Settings-AllowGroup="False" Settings-AllowHeaderFilter="True" CellStyle-Wrap="False" Caption="ประเภท">
                                                    <PropertiesComboBox DataSourceID="SqlDataSource_PolicyType"
                                                        TextField="Description"
                                                        ValueField="InsureType">

                                                        <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                                                    </PropertiesComboBox>

                                                </dx:GridViewDataComboBoxColumn>

                                                <dx:GridViewDataComboBoxColumn FieldName="Coverage3" Settings-AllowGroup="False" Settings-AllowHeaderFilter="True" Caption="ประเภทอู่" CellStyle-Wrap="False">
                                                    <PropertiesComboBox DataSourceID="SqlDataSource_Garage"
                                                        TextField="GarageName"
                                                        ValueField="GarageID">
                                                    </PropertiesComboBox>
                                                </dx:GridViewDataComboBoxColumn>


                                                <dx:GridViewDataTextColumn FieldName="Coverage4" Caption="ความรับผิด/คน"
                                                    Settings-AllowGroup="False"
                                                    Settings-AllowHeaderFilter="True"
                                                    PropertiesTextEdit-DisplayFormatString="{0:n0}">
                                                </dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Coverage5" Settings-AllowGroup="False" Caption="ความรับผิด/ครั้ง" Settings-AllowHeaderFilter="True"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Coverage6" Settings-AllowGroup="False" Caption="ทรัพย์สิน" Settings-AllowHeaderFilter="True"></dx:GridViewDataTextColumn>

                                                <dx:GridViewDataTextColumn FieldName="Coverage7" Settings-AllowGroup="False" Caption="ต่อตัวรถ" Settings-AllowHeaderFilter="False" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Coverage8" Settings-AllowGroup="False" Caption="สูญหาย/ไฟไหม้" Settings-AllowHeaderFilter="False" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Coverage9" Settings-AllowGroup="False" Caption="น้ำท่วม" Settings-AllowHeaderFilter="False" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>



                                                <dx:GridViewDataTextColumn FieldName="Coverage10" Settings-AllowGroup="False" Caption="PA ผู้ขับขี่" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
                                                <%--<dx:GridViewDataTextColumn FieldName="Coverage11" Settings-AllowGroup="False" Caption="ผู้โดยสาร" Settings-AllowHeaderFilter="False"  ></dx:GridViewDataTextColumn>--%>
                                                <dx:GridViewDataTextColumn FieldName="Coverage12" Settings-AllowGroup="False" Caption="PA ผู้โดยสาร" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>

                                                <dx:GridViewDataTextColumn FieldName="Coverage13" Settings-AllowGroup="False" Caption="ค่ารักษาพยาบาล" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Coverage14" Settings-AllowGroup="False" Caption="การประกันตัว" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
                                                <dx:GridViewDataTextColumn FieldName="Coverage15" Settings-AllowGroup="False" Caption="Deduct" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>



                                            </Columns>


                                        </dx:ASPxGridLookup>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="ยี่ห้อ">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server">

                                        <dx:ASPxComboBox runat="server"
                                            ID="newBrand" Width="210px"
                                            DropDownStyle="DropDownList"
                                            IncrementalFilteringMode="StartsWith"
                                            DataSourceID="SqlDataSource_CarBrand"
                                            EnableSynchronization="true"
                                            TextField="Name" ValidationSettings-ValidationGroup="formNewPremium"
                                            Enablecallback="true">

                                            <ClientSideEvents
                                                SelectedIndexChanged="function(s, e) { checkListBoxNewModel.PerformCallback(s.GetValue().toString());}" />
                                            <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                            <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                        </dx:ASPxComboBox>

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="รุ่น">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer27" runat="server">

                                        <%--   <dx:ASPxComboBox runat="server"
                                            ID="newModel" ClientInstanceName="newModel"
                                            TextField="Name"
                                            ValueField="Name" 
                                            IncrementalFilteringMode="StartsWith"
                                            EnableViewState="true"  ValidationSettings-ValidationGroup="formNewPremium"
                                            EnableSynchronization="true"
                                            DropDownStyle="DropDownList">
                                            <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                            <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                        </dx:ASPxComboBox>--%>


                                        <dx:ASPxDropDownEdit ClientInstanceName="newModel" ReadOnly="true"
                                            AutoResizeWithContainer="true" ID="newModel" Width="210px" runat="server" AnimationType="None">
                                            <DropDownWindowStyle BackColor="#EDEDED" />
                                            <DropDownWindowTemplate>
                                                <dx:ASPxListBox Width="100%" Height="300" ID="listBoxNewModel"
                                                    ClientInstanceName="checkListBoxNewModel" SelectionMode="CheckColumn"
                                                    TextField="Name" OnCallback="newModel_Callback"
                                                    ValueField="Name"
                                                    runat="server">
                                                    <Border BorderStyle="None" />
                                                    <BorderBottom BorderStyle="Solid" BorderWidth="1px" BorderColor="#DCDCDC" />

                                                    <ClientSideEvents SelectedIndexChanged="OnListBoxSelectionChanged" />
                                                </dx:ASPxListBox>
                                                <table style="width: 100%">
                                                    <tr>
                                                        <td style="padding: 4px">
                                                            <dx:ASPxButton ID="ASPxButton1" AutoPostBack="False" runat="server" Text="Close" Style="float: right">
                                                                <ClientSideEvents Click="function(s, e){ newModel.HideDropDown(); }" />
                                                            </dx:ASPxButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </DropDownWindowTemplate>
                                            <ClientSideEvents TextChanged="SynchronizeListBoxValues" DropDown="SynchronizeListBoxValues" />
                                            <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " ValidationGroup="formNewPremium" />
                                            <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                        </dx:ASPxDropDownEdit>

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>




                        </Items>
                    </dx:LayoutGroup>

                    <dx:LayoutGroup Caption="เบี้ยประกัน">
                        <Items>
                            <dx:LayoutItem Caption="Premium Data">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxMemo runat="server" ID="tbxdata" Width="600" Height="300">
                                            <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " ValidationGroup="formNewPremium" />
                                            <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                        </dx:ASPxMemo>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>


                    <dx:LayoutItem ShowCaption="False">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer24" runat="server">
                                <dx:ASPxButton ID="btnSavePremium" runat="server" AutoPostBack="false" ValidationContainerID="formNewPremium"
                                    Text="Import" Width="100">
                                    <ClientSideEvents Click="function(s, e) {
                                                if(ASPxClientEdit.ValidateGroup('formNewPremium'))
                                                {
                                                   
                                                    LoadingPanel.Show();   
                                                    cbNewPremium.PerformCallback('');                                       
                                                   
                                                }
                                                 e.processOnServer = false;               
                                            }" />
                                </dx:ASPxButton>


                                <dx:ASPxCallback runat="server" ID="cbNewPremium" ClientInstanceName="cbNewPremium">
                                    <ClientSideEvents CallbackError="function(s,e){ LoadingPanel.Hide();}"  CallbackComplete="function(s,e){                                                                                     
                                                LoadingPanel.Hide(); 
                                                popUpNewPremium.Hide();
                                                gridCarPremium.Refresh();
                                                e.processOnServer = false;
                                            }" />
                                </dx:ASPxCallback>

                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>


                </Items>
            </dx:ASPxFormLayout>



                          </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>


        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>




<dx:ASPxPopupControl ID="popupMoreInfoCoverage"
    ClientInstanceName="popupMoreInfoCoverage"
    runat="server" AllowDragging="true"
    PopupHorizontalAlign="OutsideRight"
    ShowPageScrollbarWhenModal="true"
    Modal="true"
    CloseAction="CloseButton" ShowMaximizeButton="true" ScrollBars="Auto"
    HeaderText="Coverage">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
            <dx:ASPxCallbackPanel ID="callbackPanel_MoreInfoCoverage" ClientInstanceName="callbackPanel_MoreInfoCoverage" runat="server"
                RenderMode="Table">
                <ClientSideEvents CallbackError="function(s,e){LoadingPanel.Hide();}" />
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent5" runat="server">


                        <dx:ASPxFormLayout ID="formCoverage" ClientInstanceName="formCoverage" runat="server"
                            Paddings-Padding="0" RequiredMarkDisplayMode="None"
                            AlignItemCaptionsInAllGroups="True"
                            ShowItemCaptionColon="true" Width="400"
                            CellStyle-Paddings-Padding="0"
                            GroupBoxDecoration="HeadingLine">
                            <Items>
                                <dx:LayoutGroup ShowCaption="False" ColCount="1">
                                    <Items>

                                        <dx:LayoutItem Caption="ข้อเสนอเบี้ยประกันภัยรถยนต์" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer22" runat="server">
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem ShowCaption="False" FieldName="Coverage1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">


                                                    <dx:ASPxComboBox ID="Coverage1" AllowMouseWheel="false" DropDownButton-ClientVisible="false"
                                                        CaptionCellStyle-Width="180" Caption=" - ลักษณะการใช้" runat="server" Width="170"
                                                        DropDownStyle="DropDown" ReadOnly="true"
                                                        DataSourceID="SqlDataSource_CarUse"
                                                        TextField="txtCarUse" ValidationSettings-ValidationGroup="formCoverage"
                                                        ValueField="CarUse">
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem ShowCaption="False" FieldName="Coverage2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                                                    <dx:ASPxComboBox ID="Coverage2" CaptionCellStyle-Width="180" Caption=" - ประเภทกรมธรรม์" runat="server" Width="170"
                                                        DropDownStyle="DropDown" ReadOnly="true" DropDownButton-ClientVisible="false"
                                                        DataSourceID="SqlDataSource_PolicyType" ClientInstanceName="Coverage2"
                                                        TextField="Description" ValidationSettings-ValidationGroup="formCoverage"
                                                        ValueField="InsureType">
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem ShowCaption="False" FieldName="Coverage3">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server">
                                                    <dx:ASPxComboBox ID="Coverage3" Caption=" - ลักษณะอู่" CaptionCellStyle-Width="180" DisplayFormatString="#,#0" runat="server" Width="170"
                                                        DropDownStyle="DropDown" ReadOnly="true"
                                                        DataSourceID="SqlDataSource_Garage" DropDownButton-ClientVisible="false"
                                                        TextField="GarageName" ValidationSettings-ValidationGroup="formCoverage"
                                                        ValueField="GarageID">
                                                    </dx:ASPxComboBox>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="ความรับผิดต่อบุคคลภายนอก" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer21" runat="server">
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>



                                        <dx:LayoutItem ShowCaption="False" FieldName="Coverage4">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server">


                                                    <dx:ASPxTextBox runat="server"
                                                        ID="Coverage4"
                                                        CaptionCellStyle-Width="180"
                                                        Caption="- บาดเจ็บหรือเสียชีวิต/คน(บาท)"
                                                        ReadOnly="true">
                                                    </dx:ASPxTextBox>


                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem ShowCaption="False" FieldName="Coverage5">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server">

                                                    <dx:ASPxTextBox runat="server"
                                                        ID="Coverage5"
                                                        CaptionCellStyle-Width="180"
                                                        Caption="- บาดเจ็บหรือเสียชัวิต/ครั้ง(บาท)"
                                                        ReadOnly="true">
                                                    </dx:ASPxTextBox>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem ShowCaption="False" FieldName="Coverage6">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server">



                                                    <dx:ASPxTextBox runat="server"
                                                        ID="Coverage6"
                                                        CaptionCellStyle-Width="180"
                                                        Caption="- ทรัพย์สิน(บาท)"
                                                        ReadOnly="true">
                                                    </dx:ASPxTextBox>



                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem Caption="ความเสียหายต่อรถยนต์" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer20" runat="server">
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem ShowCaption="False" FieldName="Coverage7" HorizontalAlign="Left">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">

                                                    <dx:ASPxTextBox runat="server"
                                                        ID="Coverage7"
                                                        CaptionCellStyle-Width="180"
                                                        Caption=" - ความเสียหายต่อรถยนต์"
                                                        ReadOnly="true">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem ShowCaption="False" FieldName="Coverage8" HorizontalAlign="Left">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">

                                                    <dx:ASPxTextBox runat="server"
                                                        ID="Coverage8"
                                                        CaptionCellStyle-Width="180"
                                                        Caption=" - รถยนต์สูญหาย/ไฟไหม้"
                                                        ReadOnly="true">
                                                    </dx:ASPxTextBox>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem ShowCaption="False" FieldName="Coverage9" HorizontalAlign="Left">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer25" runat="server">



                                                    <dx:ASPxTextBox runat="server"
                                                        ID="Coverage9"
                                                        CaptionCellStyle-Width="180"
                                                        Caption=" - คุ้มครองน้ำท่วม"
                                                        ReadOnly="true">
                                                    </dx:ASPxTextBox>


                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="ความคุ้มครองตาม ร.ย. 01 การประกันภัยอุบัติเหตุส่วนบุคคล" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer19" runat="server">
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem ShowCaption="False" FieldName="Coverage10">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server">

                                                    <dx:ASPxTextBox runat="server"
                                                        ID="Coverage10"
                                                        CaptionCellStyle-Width="180"
                                                        Caption=" - ประกันภัยอุบัติเหตุผู้ขับขี่(บาท)"
                                                        ReadOnly="true">
                                                    </dx:ASPxTextBox>



                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem ShowCaption="False" FieldName="Coverage11">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer28" runat="server">


                                                    <dx:ASPxTextBox runat="server"
                                                        ID="Coverage11"
                                                        CaptionCellStyle-Width="180"
                                                        Caption=" - จำนวนผู้โดยสาร(คน)"
                                                        ReadOnly="true">
                                                    </dx:ASPxTextBox>



                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem ShowCaption="False" FieldName="Coverage12">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer29" runat="server">
                                                    <dx:ASPxTextBox runat="server"
                                                        ID="Coverage12"
                                                        CaptionCellStyle-Width="180"
                                                        Caption=" - ประกันภัยอุบัติเหตุผู้โดยสาร(บาท)"
                                                        ReadOnly="true">
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>



                                        <dx:LayoutItem Caption="ความคุ้มครองตาม ร.ย. 02 การประกันภัยค่ารักษาพยาบาล" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer18" runat="server">
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>



                                        <dx:LayoutItem ShowCaption="False" FieldName="Coverage13">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server">
                                                    <dx:ASPxTextBox runat="server" ID="Coverage13" CaptionCellStyle-Width="180" Caption=" - ค่ารักษาพยาบาล(บาท)" ReadOnly="true"></dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem Caption="ความคุ้มครองตาม ร.ย. 03 การประกันตัวผู้ขับขี่" CaptionStyle-Font-Bold="true">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer17" runat="server">
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server">


                                                    <dx:ASPxTextBox runat="server" ID="Coverage14" CaptionCellStyle-Width="180" Caption=" - การประกันตัวผู้ขับขี่(บาท)" ReadOnly="true"></dx:ASPxTextBox>


                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="ความเสียหายส่วนแรก (Deductible)" CaptionStyle-Font-Bold="true">

                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server">
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>

                                        </dx:LayoutItem>

                                        <dx:LayoutItem ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer30" runat="server">

                                                    <dx:ASPxTextBox runat="server" ID="Coverage15" CaptionCellStyle-Width="180" Caption=" - ความเสียหายส่วนแรก(บาท) " ReadOnly="true"></dx:ASPxTextBox>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>
                            </Items>
                        </dx:ASPxFormLayout>




                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <ClientSideEvents Shown="popup_ShownMoreInfoCoverage" />
</dx:ASPxPopupControl>




<asp:SqlDataSource ID="SqlDataSource_Garage" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand=" SELECT  * from tblGarageType order by ShowID "></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource_PolicyType" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand=" SELECT  * from tblPolicyType "></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource_CarUse" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>" SelectCommand="select CarUse, CarUse + ' - ' + txtCarUse as  txtCarUse from tblCarUse "></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_TPBIP" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>" SelectCommand="select 0 as Coverage union  SELECT Coverage FROM tblCoverListMaster where CoverType='TPBIP' order by Coverage "></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource_TPBIT" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>" SelectCommand="select 0 as Coverage union SELECT Coverage FROM tblCoverListMaster where CoverType='TPBIT' order by Coverage "></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource_TPPD" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>" SelectCommand="select 0 as Coverage union SELECT Coverage FROM tblCoverListMaster where CoverType='TPPD' order by Coverage "></asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource_RY02" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>" SelectCommand="select 0 as Coverage union SELECT Coverage FROM tblCoverListMaster where CoverType='RY02' order by Coverage "></asp:SqlDataSource>


<asp:SqlDataSource ID="SqlDataSource_MotorCoverage" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="SELECT * FROM V_MotorCoverage where RiskGroupID = @RiskGroupID">
    <SelectParameters>
        <asp:Parameter Name="RiskGroupID" />
    </SelectParameters>
</asp:SqlDataSource>


<asp:SqlDataSource ID="SqlDataSource_Underwriter" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="SELECT distinct [Underwriter],[AccountContact],[CampaignID] FROM [V_Campaign_CommIn] where isactive = 1 and [CampaignID] = @CampaignID">
    <SelectParameters>
        <asp:SessionParameter Name="CampaignID" SessionField="CampaignID" />
    </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_CarBrand" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="SELECT ID, Name FROM tblCarBrandModel where ParentID is null  order by Name "></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_CarModel" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="SELECT ModelName as Name FROM V_CarBrandModel where  BrandName=?  order by Name ">
    <SelectParameters>
        <asp:Parameter Name="?" />
    </SelectParameters>
</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_CoverageLookup" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select * from V_MotorCoverage "></asp:SqlDataSource>
