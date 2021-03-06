﻿<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxMotorCampaignClassOfRisk.ascx.vb" Inherits="Modules_ucDevxMotorCampaignClassOfRisk" %>
<%@ Register Assembly="DevExpress.Web.ASPxTreeList.v17.2, Version=17.2.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web.ASPxTreeList" TagPrefix="dx" %>
<style type="text/css">
    /*.table {
        padding: 0;
    }

    .dxcvControl_Office2010Blue .dxcvSeparator_Office2010Blue, .dxcvControl_Office2010Blue .dxcvSeparator_Office2010Blue div {
        width: 1px;
        height: 20px;
        overflow: hidden;
    }*/

    .dxtlAltNode_Office2010Blue:hover {
        background-color: #d4e0fc;
    }

    .dxtlNode_Office2010Blue:hover {
        background-color: #d4e0fc;
    }
</style>

<style type="text/css">
    .highlight {
        background-color: #ffd83a;
    }
</style>

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
    var keyValueLog;

    function OnMoreInfoLog(element, key) {
        callbackPanel_MoreInfoLog.SetContentHtml("");
        popupMoreInfoLog.ShowAtElement(element);
        keyValueLog = key;
    }
    function popup_ShownCommOutLog(s, e) {
        callbackPanel_MoreInfoLog.PerformCallback(keyValueLog);
    }
</script>


<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="Motor Campaign" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>

            <%--            <dx:ASPxCardView ID="cardView" ClientInstanceName="cardView" runat="server" DataSourceID="SqlDataSource_MotorCampaign"
                KeyFieldName="CampaignID" Width="100%">
                <ClientSideEvents FocusedCardChanged="OnFocusedCardChanged" />
                <Settings ShowHeaderFilterButton="true" />
                <SettingsBehavior AllowFocusedCard="true" />
                <SettingsPager>
                    <SettingsTableLayout ColumnCount="4" RowsPerPage="1" />
                </SettingsPager>
                <SettingsSearchPanel Visible="true" />
                <Settings ShowCustomizationPanel="true" />
                <SettingsText CommandCustomizationButton="เรียง/ค้นหา" CustomizationWindowCaption="เรียง/ค้นหา" />


                <Columns>

                    <dx:CardViewColumn FieldName="CampaignID" />
                    <dx:CardViewColumn FieldName="CampaignName" />
                    <dx:CardViewColumn FieldName="CampaignCode" />

                     <dx:CardViewComboBoxColumn FieldName="CampaignType">
                        <PropertiesComboBox ValidationSettings-RequiredField-IsRequired="true">
                            <Items>
                                <dx:ListEditItem Text="ภาคสมัครใจ + พรบ." Value="1" />
                                <dx:ListEditItem Text="ภาคสมัครใจ" Value="2" />
                            </Items>
                        </PropertiesComboBox>
                    </dx:CardViewComboBoxColumn>
                    <dx:CardViewComboBoxColumn FieldName="HasPremium">
                        <PropertiesComboBox ValidationSettings-RequiredField-IsRequired="true">
                            <Items>
                                <dx:ListEditItem Text="มีเบี้ย" Value="True" />
                                <dx:ListEditItem Text="ไม่มีเบี้ย" Value="False" />
                            </Items>

                        </PropertiesComboBox>
                    </dx:CardViewComboBoxColumn>


                    <dx:CardViewComboBoxColumn FieldName="RenewalYear">
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
                    </dx:CardViewComboBoxColumn>


                    <dx:CardViewColumn FieldName="EffectiveDate" />
                    <dx:CardViewColumn FieldName="ExpiryDate" />
                    <dx:CardViewColumn FieldName="CampaignDescription" Caption="Description" />
                </Columns>
                <CardLayoutProperties>
                    <Items>
                        <dx:CardViewColumnLayoutItem ColumnName="CampaignName" ShowCaption="True" />
                        <dx:CardViewColumnLayoutItem ColumnName="CampaignCode" ShowCaption="True" />
                         <dx:CardViewColumnLayoutItem ColumnName="CampaignType" />
                        <dx:CardViewColumnLayoutItem ColumnName="HasPremium" /> 
                        <dx:CardViewColumnLayoutItem ColumnName="RenewalYear" ShowCaption="True" />
                        <dx:CardViewColumnLayoutItem ColumnName="EffectiveDate" ShowCaption="True" />
                        <dx:CardViewColumnLayoutItem ColumnName="ExpiryDate" ShowCaption="True" />
                        <dx:CardViewColumnLayoutItem ColumnName="CampaignDescription" ShowCaption="True" />
                    </Items>
                </CardLayoutProperties>
            </dx:ASPxCardView>--%>



            <dx:ASPxCallbackPanel ID="callbackPanel"
                ClientInstanceName="callbackPanel" runat="server"
                RenderMode="Table">

                <PanelCollection>
                    <dx:PanelContent ID="PanelContent1" runat="server">





                        <table>
                            <tr>
                                <td> 
                                    <dx:ASPxGridLookup ID="gridCampaign" ClientInstanceName="gridCampaign" runat="server" SelectionMode="Single"
                                        DataSourceID="SqlDataSource_MotorCampaign" Width="300" Caption="Campaign" 
                                        KeyFieldName="CampaignID" TextFormatString="{1} - {2}">
                                        <ClientSideEvents ValueChanged="function (s, e){
                                         callbackPanel.PerformCallback('select_campaign');
                                         }" />

                                        <GridViewProperties>

                                            <Settings ShowHeaderFilterButton="true" />
                                            <SettingsSearchPanel Visible="true" />
                                                <SettingsBehavior EnableRowHotTrack="true" />

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



                                </td>

                                <td>&nbsp;
                                </td>
                                <td>
                                    <dx:ASPxTextBox ID="searchTxt" runat="server" Width="170px" ClientVisible="false">
                                    </dx:ASPxTextBox>
                                </td>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <dx:ASPxButton ID="searchBtn" runat="server" AutoPostBack="false" Text="Search" ClientVisible="false">
                                        <ClientSideEvents Click="function (s, e){
                                            callbackPanel.PerformCallback('search_commission');
                                         }" />
                                    </dx:ASPxButton>
                                </td>

                            </tr>

                        </table>


                        <br />




                        <dx:ASPxButton ID="cmdAddNewCommission" ClientInstanceName="cmdAddNewCommission" ClientVisible="false" runat="server" Text="Add Commission" Image-IconID="actions_add_16x16">
                            <ClientSideEvents Click="function(s, e) {           
                                                    popupAddNewCommission.Show(); 
                                                    //ASPxClientEdit.ClearEditorsInContainerById('settingsFormLayout'); 
                                                    callbackPanel_addnewCommission.PerformCallback('initdata');
                                                    e.processOnServer = false; 
                                                    }" />
                        </dx:ASPxButton>



                        <dx:ASPxButton ID="cmdAddNewCommissionAgent"
                            runat="server" ClientVisible="false"
                            Text="Add Agent"
                            Image-IconID="actions_add_16x16">


                            <ClientSideEvents Click="function(s, e) {           
                                                    popupAddNewCommissionAgent.Show(); 
                                                    //ASPxClientEdit.ClearEditorsInContainerById('settingsFormLayout2'); 
                                                    callbackPanel_addnewCommissionAgent.PerformCallback('initdata');
                                                    e.processOnServer = false; 
                                                    }" />


                        </dx:ASPxButton>

                         <dx:ASPxButton ID="btnExportToXlsx" runat="server" Text="Export to XLSX" Image-IconID="export_exporttoxlsx_16x16"  ClientVisible="false" />
                        <br /> 
                        <dx:ASPxTreeListExporter ID="treeListExporter" runat="server" TreeListID="treeList_Commission" />
                        <dx:ASPxTreeList ID="treeList_Commission" runat="server"
                            AutoGenerateColumns="False"
                            DataSourceID="SqlDataSource_Commission" Width="100%"
                            KeyFieldName="ID" ParentFieldName="ParentID">
                            <SettingsBehavior AutoExpandAllNodes="true" />
                            <ClientSideEvents CustomButtonClick="function(s, e) {
                                                if(e.buttonID == 'CustomEditButton')
                                                {              
                                                    LoadingPanel.Show();                                                   
                                                   callbackPanel_popupEditCommission.PerformCallback(e.nodeKey);
                                                }
                                                else if(e.buttonID == 'CustomDeleteButton')
                                                {                                                                 
                                                    if(confirm('Confirm to Delete?')) callbackPanel.PerformCallback('delete_commission|' + e.nodeKey);
                                                } 
                                                e.processOnServer = false; 
                                            }"
                                CallbackError="function(s, e) { LoadingPanel.Hide(); }" />

                            <Columns>
                               <%-- <dx:TreeListDataColumn FieldName="Risk" EditFormSettings-Visible="False" Width="50" CellStyle-HorizontalAlign="Center" EditCellStyle-VerticalAlign="Bottom">
                                    <DataCellTemplate>
                                        <a href="javascript:void(0);" onclick="OnEditCommission(this, '<%# Eval("ID").ToString().Trim()%>')"><%# Eval("Risk").ToString().Trim()%></a>
                                    </DataCellTemplate>
                                </dx:TreeListDataColumn>--%>

                                <dx:TreeListTextColumn FieldName="Risk">
                                    <DataCellTemplate>
                                        <%#GetCellText(Container)%>
                                    </DataCellTemplate>
                                </dx:TreeListTextColumn>


                                <%-- <dx:TreeListDataColumn FieldName="Code" />--%>
                                <%--  <dx:TreeListDataColumn FieldName="Name" />--%>
                                <dx:TreeListTextColumn FieldName="Code">
                                    <DataCellTemplate>
                                        <%#GetCellText(Container)%>
                                    </DataCellTemplate>
                                </dx:TreeListTextColumn>
                                <dx:TreeListTextColumn FieldName="Name">
                                    <DataCellTemplate>
                                        <%#GetCellText(Container)%>
                                    </DataCellTemplate>
                                </dx:TreeListTextColumn>

                                <dx:TreeListDataColumn FieldName="EffectiveDate" DisplayFormat="{0:dd/MM/yyyy}" />
                                <dx:TreeListDataColumn FieldName="ExpiryDate" DisplayFormat="{0:dd/MM/yyyy}" />


 <dx:TreeListTextColumn>

                                    <DataCellTemplate>

        <dx:ASPxTrackBar runat="server" 
            ID="trackBar1" 
            MinValue="-100" 
            MaxValue="100" 
            ShowChangeButtons="false" ShowDragHandles="false" CaptionSettings-ShowColon="false"
            Height="10"
            Width="100" 
            PositionStart="0" 
            PositionEnd='<%#Eval("ID") %>'  
            Enabled="false"
            ScalePosition="None" 
            ClientInstanceName="tickTrackBar">                       
        </dx:ASPxTrackBar>

                                    </DataCellTemplate>
                                </dx:TreeListTextColumn>


                                <dx:TreeListDataColumn FieldName="Commission_display" Caption="Comm" />
                                <dx:TreeListDataColumn FieldName="ORCommission_display" Caption="OR" />
                                <dx:TreeListDataColumn FieldName="Admin1_display" Caption="Admin1" />
                                <dx:TreeListDataColumn FieldName="Admin2_display" Caption="Admin2" />



                                <dx:TreeListDataColumn FieldName="IsActive" />
                                <dx:TreeListDataColumn FieldName="Remark" />


                                <dx:TreeListDataColumn FieldName="ID" Visible="False">
                                    <CellStyle BackColor="#ffebb1" />
                                </dx:TreeListDataColumn>
                                <dx:TreeListDataColumn FieldName="ParentID" Visible="False">
                                    <CellStyle BackColor="#ffebb1" />
                                </dx:TreeListDataColumn>

                              

                                <dx:TreeListDataColumn FieldName="HasOverComm" Caption="OverComm" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                                    <EditFormSettings Visible="False" />
                                    <DataCellTemplate>
                                        <img src='images/alert_<%# Eval("HasOverComm").ToString().Trim()%>.gif' style="height: 15px;" />
                                    </DataCellTemplate>
                                </dx:TreeListDataColumn>

 <dx:TreeListDataColumn CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center" Caption="Log" HeaderStyle-HorizontalAlign="Center" >
                                    <EditFormSettings Visible="False" />
                                    <DataCellTemplate>
                                         <a href="javascript:void(0);" onclick="OnMoreInfoLog(this, '<%# Eval("ID").ToString().Trim()%>')">...</a>
                                    </DataCellTemplate>
                                </dx:TreeListDataColumn>

                                <dx:TreeListCommandColumn ButtonType="Button" Caption="#" Width="10px">
                                    <CustomButtons>
                                        <dx:TreeListCommandColumnCustomButton ID="CustomEditButton" Text="แก้ไข"  Visibility="BrowsableNode" />
                                        <dx:TreeListCommandColumnCustomButton ID="CustomDeleteButton" Text="ลบ"  Visibility="BrowsableNode" />
                                    </CustomButtons>
                                </dx:TreeListCommandColumn>

                               



                                 

                            </Columns>

                        </dx:ASPxTreeList>


                        <asp:SqlDataSource ID="SqlDataSource_Commission" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
                            SelectCommand="select * from V_Campaign_Commission where CampaignID=@CampaignID  ">


                            <SelectParameters>
                                <asp:SessionParameter Name="CampaignID" SessionField="CampaignID" />
                            </SelectParameters>

                        </asp:SqlDataSource>






                        <%--                        <dx:ASPxFormLayout ID="frmdata" runat="server" ColCount="1" Width="100%">
                            <Items>
                                <dx:LayoutGroup Caption="Commission - In">
                                    <Items>
                                        <dx:LayoutItem Caption="" ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer
                                                    ID="LayoutItemNestedControlContainer9"
                                                    runat="server">






                                                    <dx:ASPxGridView ID="Grid_Campaign_CommIn"
                                                        ClientInstanceName="Grid_Campaign_CommIn" runat="server"
                                                        DataSourceID="SqlDataSource_Campaign_CommIn"
                                                        KeyFieldName="CommInID" AutoGenerateColumns="False">

                                                        <SettingsPager Mode="ShowPager">
                                                            <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                                                        </SettingsPager>
                                                        <SettingsBehavior EnableRowHotTrack="true" ConfirmDelete="true" />
                                                        <SettingsSearchPanel Visible="true" />
                                                        <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
                                                        <SettingsCommandButton>
                                                            <NewButton ButtonType="Button" Text="เพิ่ม"></NewButton>
                                                            <UpdateButton ButtonType="Button" Text="บันทึก"></UpdateButton>
                                                            <EditButton ButtonType="Button" Text="แก้ไข"></EditButton>
                                                            <DeleteButton ButtonType="Button" Text="ลบ"></DeleteButton>
                                                            <CancelButton ButtonType="Button" Text="ยกเลิก"></CancelButton>
                                                        </SettingsCommandButton>


                                                        <SettingsText PopupEditFormCaption="แก้ไขรายการ" ConfirmDelete="ยืนยันการลบ" />
                                                        <SettingsPopup>
                                                            <EditForm Modal="true" MinWidth="100" Width="400" HorizontalAlign="WindowCenter" VerticalAlign="Middle" />
                                                            <HeaderFilter Height="200" />
                                                        </SettingsPopup>
                                                        <Settings ShowHeaderFilterButton="true" ShowGroupPanel="true" />

                                                        <Columns>


                                                            <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowEditButton="true" ShowDeleteButton="true" />


                                                            <dx:GridViewDataTextColumn FieldName="Risk" Caption="Risk" CellStyle-Wrap="False" />
                                                            <dx:GridViewDataTextColumn FieldName="AccountContact" Caption="Name" CellStyle-Wrap="False" />
                                                            <dx:GridViewDataTextColumn FieldName="CommIn_Display" Caption="CommIn" CellStyle-Wrap="False" />
                                                            <dx:GridViewDataDateColumn FieldName="EffectiveDate" Caption="Effective" CellStyle-Wrap="False" />
                                                            <dx:GridViewDataDateColumn FieldName="ExpiryDate" Caption="Expiry" CellStyle-Wrap="False" />
                                                            <dx:GridViewDataColumn FieldName="IsActive" Caption="Active" CellStyle-Wrap="False" />
                                                        </Columns>


                                                    </dx:ASPxGridView>


                                                    <asp:SqlDataSource ID="SqlDataSource_Campaign_CommIn"
                                                        runat="server"
                                                        ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
                                                        SelectCommand="select * from V_Campaign_CommIn "></asp:SqlDataSource>



                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>


                                        </dx:LayoutItem>

                                    </Items>

                                </dx:LayoutGroup>
                    

                            </Items>
                            <Styles LayoutItem-Caption-Font-Bold="true" />
                        </dx:ASPxFormLayout>--%>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>

        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>



<dx:ASPxPopupControl ID="popupAddNewCommission"
    ClientInstanceName="popupAddNewCommission"
    runat="server" AllowDragging="true"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    ShowPageScrollbarWhenModal="true"
    Width="980"
    Modal="true"
    CloseAction="CloseButton" ShowMaximizeButton="true" ScrollBars="Auto"
    HeaderText="Commission">
    <ClientSideEvents AfterResizing="function(s){s.AdjustControl()}" />
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
            <dx:ASPxCallbackPanel ID="callbackPanel_addnewCommission" ClientInstanceName="callbackPanel_addnewCommission" runat="server"
                RenderMode="Table">
                <ClientSideEvents CallbackError="function(s,e){
                        LoadingPanel.Hide(); 
                    }" />
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent2" runat="server">
                        <dx:ASPxButton ID="btnAddNewCommission" runat="server" ValidationContainerID="settingsFormLayout" AutoPostBack="false" Text="Add Commission" Image-IconID="actions_add_16x16">
                            <ClientSideEvents Click="function(s, e) {   
                                                   if(ASPxClientEdit.AreEditorsValid()) {
                                                        LoadingPanel.Show();
                                                        cbAddNewCommission.PerformCallback('');                                                       
                                                    }
                                                    e.processOnServer = false; 
                                                }" />
                        </dx:ASPxButton>

                        <dx:ASPxCallback runat="server" ID="cbAddNewCommission" ClientInstanceName="cbAddNewCommission">
                            <ClientSideEvents
                                CallbackError="function(s,e){LoadingPanel.Hide();}"
                                CallbackComplete="function(s,e){                                                                                     
                                LoadingPanel.Hide(); 

                                if(e.result == 'success')
                                {
                                    popupAddNewCommission.Hide(); 
                                    callbackPanel.PerformCallback('REFRESH');
                                }                                                     
                                else
                                {
                                    alert(e.result);
                                }
                                           
                            }" />

                        </dx:ASPxCallback>



                        <dx:ASPxFormLayout runat="server" ID="settingsFormLayout" ClientInstanceName="settingsFormLayout"
                            AlignItemCaptionsInAllGroups="True" Width="100%">
                            <Items>

                                <dx:LayoutGroup Caption="Class Of Risk" GroupBoxDecoration="HeadingLine" ColCount="2">
                                    <Items>
                                        <dx:LayoutItem Caption="Class Of Risk" ColSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxComboBox ID="newCOR" runat="server" DataSourceID="SqlDataSource_COR" Width="200"
                                                        TextField="Name" ValueField="Risk" ValueType="System.String"
                                                        ShowImageInEditBox="True">
                                                        <ItemImage Height="24px" Width="23px" />
                                                        <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" "></ValidationSettings>
                                                        <ClientSideEvents ValueChanged="function (s, e){
                                                            callbackPanel_addnewCommission.PerformCallback('');
                                                        }" />
                                                    </dx:ASPxComboBox>


                                                    <asp:SqlDataSource ID="SqlDataSource_COR" runat="server"
                                                        ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
                                                        SelectCommand="SELECT Risk, Risk + ' - ' + Description as Name  from V_ClassOfRisk_Publish "></asp:SqlDataSource>


                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem Caption="EffectiveDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>


                                                    <dx:ASPxDateEdit runat="server" ID="newEffectiveDate" ClientInstanceName="newEffectiveDate">
                                                        <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" "></ValidationSettings>
                                                        <ClientSideEvents ValueChanged="function(s,e){newExpiryDate.SetDate(newEffectiveDate.GetDate());  }" />
                                                    </dx:ASPxDateEdit>


                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="ExpiryDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxDateEdit runat="server" ID="newExpiryDate" ClientInstanceName="newExpiryDate">
                                                        <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" "></ValidationSettings>
                                                    </dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                    </Items>
                                </dx:LayoutGroup>

                                <dx:LayoutGroup Caption="Underwriter" GroupBoxDecoration="HeadingLine" ColCount="2">
                                    <Items>


                                        <dx:LayoutItem ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxGridView ID="newgridCommIn" ClientInstanceName="newgridCommIn" DataSourceID="SqlDataSource_CommIn"
                                                        runat="server"
                                                        KeyFieldName="Underwriter">
                                                        <SettingsPager Mode="ShowAllRecords">
                                                        </SettingsPager>
                                                        <SettingsBehavior EnableRowHotTrack="true" ConfirmDelete="true" />
                                                        <SettingsSearchPanel Visible="true" />
                                                        <Settings ShowHeaderFilterButton="true" />
                                                        <Columns>
                                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Width="40" ShowClearFilterButton="true" SelectAllCheckboxMode="AllPages" />
                                                            <dx:GridViewDataTextColumn FieldName="Risk" Caption="Risk" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />

                                                            <dx:GridViewDataColumn FieldName="Underwriter" Caption="UCode" CellStyle-Wrap="False"></dx:GridViewDataColumn>

                                                            <dx:GridViewDataColumn Caption="" EditFormSettings-Visible="False" Width="100" CellStyle-HorizontalAlign="Center" EditCellStyle-VerticalAlign="Bottom">
                                                                <DataItemTemplate>
                                                                    <img src='images/InsurerLogo/<%# Eval("Underwriter").ToString().Trim() %>.jpg' width="30" />
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataColumn>


                                                            <dx:GridViewDataColumn FieldName="AccountContact" Caption="Name" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="Brokerage_display" Caption="Brokerage" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="ORCommissionPercent_display" Caption="OR" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="AdminFeeIn1_display" Caption="Admin1" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="AdminFeeIn2_display" Caption="Admin2" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="Remark" CellStyle-Wrap="False"></dx:GridViewDataColumn>

                                                        </Columns>
                                                    </dx:ASPxGridView>
                                                    <asp:SqlDataSource ID="SqlDataSource_CommIn" runat="server"
                                                        ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
                                                        SelectCommand="SELECT * from V_RiskUwriter where Risk=@Risk and IsActive=1 and status='approved' ">
                                                        <SelectParameters>
                                                            <asp:SessionParameter Name="Risk" SessionField="Risk" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>



                                    </Items>
                                </dx:LayoutGroup>

                                <dx:LayoutGroup Caption="Agent" GroupBoxDecoration="HeadingLine" ColCount="2">
                                    <Items>


                                        <dx:LayoutItem ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxGridView ID="newgridCommOut" ClientInstanceName="newgridCommOut" DataSourceID="SqlDataSource_CommOut"
                                                        runat="server"
                                                        KeyFieldName="Agent">
                                                        <SettingsPager Mode="ShowAllRecords">
                                                        </SettingsPager>
                                                        <SettingsBehavior EnableRowHotTrack="true" ConfirmDelete="true" />
                                                        <SettingsSearchPanel Visible="true" />
                                                        <Settings ShowHeaderFilterButton="true" />

                                                        <SettingsPager Mode="ShowPager">
                                                            <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                                                        </SettingsPager>


                                                        <Columns>
                                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Width="40" ShowClearFilterButton="true" SelectAllCheckboxMode="AllPages" />
                                                            <dx:GridViewDataTextColumn FieldName="Risk" Caption="Risk" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="Agent" Caption="Code" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="Name" Caption="Name" CellStyle-Wrap="False" />
                                                            <%--<dx:GridViewDataTextColumn FieldName="CommissionOut_Display" Caption="CommOut" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />--%>
                                                            <dx:GridViewDataTextColumn FieldName="OROut_Display" Caption="OR" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="AdminOut1_Display" Caption="Admin1" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="AdminOut2_Display" Caption="Admin2" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="Remark" Caption="Remark" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="AgentGroup" Caption="Group" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />

                                                            <dx:GridViewDataColumn FieldName="HasOverComm" Caption="OverComm" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                                                                <EditFormSettings Visible="False" />
                                                                <DataItemTemplate>
                                                                    <img src='images/alert_<%# Eval("HasOverComm").ToString().Trim()%>.gif' style="height: 15px;" />
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataColumn>
                                                        </Columns>
                                                    </dx:ASPxGridView>
                                                    <asp:SqlDataSource ID="SqlDataSource_CommOut" runat="server"
                                                        ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
                                                        SelectCommand="SELECT * from V_AgentRiskComm where Risk=@Risk and IsActive=1 and status='approved' and AgentGroup <> 'LAPSE'">
                                                        <SelectParameters>
                                                            <asp:SessionParameter Name="Risk" SessionField="Risk" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>
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
</dx:ASPxPopupControl>

<dx:ASPxPopupControl ID="popupAddNewCommissionAgent"
    ClientInstanceName="popupAddNewCommissionAgent"
    runat="server" AllowDragging="true"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    ShowPageScrollbarWhenModal="true"
    Width="800"
    Modal="true"
    CloseAction="CloseButton" ShowMaximizeButton="true" ScrollBars="Auto"
    HeaderText="Commission">
    <ClientSideEvents AfterResizing="function(s){s.AdjustControl()}" />
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
            <dx:ASPxCallbackPanel ID="callbackPanel_addnewCommissionAgent" ClientInstanceName="callbackPanel_addnewCommissionAgent" runat="server"
                RenderMode="Table">
                <ClientSideEvents CallbackError="function(s,e){
                        LoadingPanel.Hide(); 
                    }" />
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent4" runat="server">




                        <dx:ASPxButton ID="ASPxButton1" runat="server" ValidationContainerID="settingsFormLayout2" AutoPostBack="false" Text="Add Agent" Image-IconID="actions_add_16x16">
                            <ClientSideEvents Click="function(s, e) {   
                                                   if(ASPxClientEdit.AreEditorsValid()) {
                                                        LoadingPanel.Show();
                                                        cbAddNewCommissionAgent.PerformCallback('');                                                       
                                                    }
                                                    e.processOnServer = false; 
                                                }" />
                        </dx:ASPxButton>

                        <dx:ASPxCallback runat="server" ID="cbAddNewCommissionAgent" ClientInstanceName="cbAddNewCommissionAgent">
                            <ClientSideEvents
                                CallbackError="function(s,e){LoadingPanel.Hide();}"
                                CallbackComplete="function(s,e){                                                                                     
                                LoadingPanel.Hide(); 

                                if(e.result == 'success')
                                {
                                    popupAddNewCommissionAgent.Hide(); 
                                    callbackPanel.PerformCallback('REFRESH');
                                }                                                     
                                else
                                {
                                    alert(e.result);
                                }
                                           
                            }" />

                        </dx:ASPxCallback>






                        <dx:ASPxFormLayout runat="server" ID="settingsFormLayout2" ClientInstanceName="settingsFormLayout2"
                            AlignItemCaptionsInAllGroups="True">
                            <Items>

                                <dx:LayoutGroup Caption="Class Of Risk" GroupBoxDecoration="HeadingLine" ColCount="2" Width="680">
                                    <Items>
                                        <dx:LayoutItem Caption="Class Of Risk" ColSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxGridLookup ID="gridCommIn" ClientInstanceName="gridCommIn" runat="server" SelectionMode="Single"
                                                        DataSourceID="SqlDataSource_MotorCampaignCommIn" Width="500"
                                                        KeyFieldName="CommInID" TextFormatString="{0} - {1} ({2:dd/MM/yyyy} - {3:dd/MM/yyyy})">
                                                        <ClientSideEvents ValueChanged="function (s, e){
                                                    callbackPanel_addnewCommissionAgent.PerformCallback('select_commin');
                                                     
                                                }" />
                                                        <GridViewProperties>
                                                            <Settings ShowHeaderFilterButton="true" />
                                                            <SettingsSearchPanel Visible="true" />

                                                        </GridViewProperties>
                                                        <ValidationSettings>
                                                            <RequiredField IsRequired="true" ErrorText=" " />
                                                        </ValidationSettings>
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn FieldName="Risk" />
                                                            <dx:GridViewDataTextColumn FieldName="AccountContact" Width="50" CellStyle-Wrap="False" />
                                                            <dx:GridViewDataTextColumn FieldName="EffectiveDate" CellStyle-Wrap="False" PropertiesTextEdit-DisplayFormatString="{0:dd/MM/yyyy}" />
                                                            <dx:GridViewDataTextColumn FieldName="ExpiryDate" CellStyle-Wrap="False" PropertiesTextEdit-DisplayFormatString="{0:dd/MM/yyyy}" />
                                                        </Columns>
                                                    </dx:ASPxGridLookup>


                                                    <asp:SqlDataSource ID="SqlDataSource_MotorCampaignCommIn" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
                                                        SelectCommand="select * from V_Campaign_CommIn where CampaignID=@CampaignID and IsActive=1 ">
                                                        <SelectParameters>
                                                            <asp:SessionParameter Name="CampaignID" SessionField="CampaignID" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>















                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem Caption="EffectiveDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>


                                                    <dx:ASPxDateEdit runat="server" ID="newAgentEffectiveDate" ClientInstanceName="newAgentEffectiveDate">
                                                        <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" "></ValidationSettings>
                                                        <ClientSideEvents ValueChanged="function(s,e){newAgentExpiryDate.SetDate(newAgentEffectiveDate.GetDate());  }" />
                                                    </dx:ASPxDateEdit>


                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="ExpiryDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxDateEdit runat="server" ID="newAgentExpiryDate" ClientInstanceName="newAgentExpiryDate">
                                                        <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" "></ValidationSettings>
                                                    </dx:ASPxDateEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                    </Items>
                                </dx:LayoutGroup>


                                <dx:LayoutGroup Caption="Agent" GroupBoxDecoration="HeadingLine" ColCount="2" Width="680">
                                    <Items>


                                        <dx:LayoutItem ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxGridView ID="newgridCommOut2" ClientInstanceName="newgridCommOut2" DataSourceID="SqlDataSource3"
                                                        runat="server"
                                                        KeyFieldName="Agent">
                                                        <SettingsPager Mode="ShowAllRecords">
                                                        </SettingsPager>
                                                        <SettingsBehavior EnableRowHotTrack="true" ConfirmDelete="true" />
                                                        <SettingsSearchPanel Visible="true" />
                                                        <Settings ShowHeaderFilterButton="true" />

                                                        <SettingsPager Mode="ShowPager">
                                                            <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                                                        </SettingsPager>


                                                        <Columns>
                                                            <dx:GridViewCommandColumn ShowSelectCheckbox="True" Width="40" ShowClearFilterButton="true" SelectAllCheckboxMode="AllPages" />
                                                            <dx:GridViewDataTextColumn FieldName="Risk" Caption="Risk" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="Agent" Caption="Code" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="Name" Caption="Name" CellStyle-Wrap="False" />
                                                            <%--<dx:GridViewDataTextColumn FieldName="CommissionOut_Display" Caption="CommOut" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />--%>
                                                            <dx:GridViewDataTextColumn FieldName="OROut_Display" Caption="OR" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="AdminOut1_Display" Caption="Admin1" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="AdminOut2_Display" Caption="Admin2" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="Remark" Caption="Remark" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="AgentGroup" Caption="Group" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />

                                                            <dx:GridViewDataColumn FieldName="HasOverComm" Caption="OverComm" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                                                                <EditFormSettings Visible="False" />
                                                                <DataItemTemplate>
                                                                    <img src='images/alert_<%# Eval("HasOverComm").ToString().Trim()%>.gif' style="height: 15px;" />
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataColumn>
                                                        </Columns>
                                                    </dx:ASPxGridView>


                                                    <asp:SqlDataSource ID="SqlDataSource3" runat="server"
                                                        ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
                                                        SelectCommand="SELECT * from V_AgentRiskComm where Risk='' and IsActive=1 and status='approved' and AgentGroup <> 'LAPSE'"></asp:SqlDataSource>
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
</dx:ASPxPopupControl>

<dx:ASPxPopupControl ID="popupEditCommission"
    ClientInstanceName="popupEditCommission"
    runat="server" AllowDragging="true"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    ShowPageScrollbarWhenModal="true"
    Modal="true"
    CloseAction="CloseButton"
    HeaderText="Edit Commission">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            <dx:ASPxCallbackPanel ID="callbackPanel_popupEditCommission"
                ClientInstanceName="callbackPanel_popupEditCommission" runat="server"
                RenderMode="Table">
                <ClientSideEvents EndCallback="function(s,e){                                                                                                    
                                LoadingPanel.Hide(); 
                                popupEditCommission.Show();
              
                            }" />
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent3" runat="server">

                        <dx:ASPxFormLayout ID="frmEditCommission" ClientInstanceName="frmEditCommission" runat="server"
                            ColCount="2"
                            RequiredMarkDisplayMode="None">
                            <Items>


                                <dx:LayoutItem Caption="Risk">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer
                                            ID="LayoutItemNestedControlContainer9"
                                            runat="server"
                                            SupportsDisabledAttribute="True">
                                            <dx:ASPxLabel runat="server" ID="lbRisk"></dx:ASPxLabel>

                                            <dx:ASPxHiddenField runat="server" ID="lbID" ClientInstanceName="lbID"></dx:ASPxHiddenField>


                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>


                                <dx:LayoutItem Caption="Name">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer
                                            ID="LayoutItemNestedControlContainer23"
                                            runat="server"
                                            SupportsDisabledAttribute="True">

                                            <dx:ASPxLabel runat="server" ID="lbName"></dx:ASPxLabel>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>

                                <dx:LayoutItem Caption="EffectiveDate">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer
                                            ID="LayoutItemNestedControlContainer5"
                                            runat="server"
                                            SupportsDisabledAttribute="True">

                                            <dx:ASPxDateEdit runat="server" ID="EffectiveDate" ClientInstanceName="EffectiveDate">
                                                <ClientSideEvents ValueChanged="function(s,e){ExpiryDate.SetDate(EffectiveDate.GetDate());  }" />
                                                <ValidationSettings RequiredField-IsRequired="true" RegularExpression-ErrorText=" " ValidationGroup="frmEditCommission"></ValidationSettings>
                                            </dx:ASPxDateEdit>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>

                                <dx:LayoutItem Caption="ExpiryDate">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer
                                            ID="LayoutItemNestedControlContainer6"
                                            runat="server"
                                            SupportsDisabledAttribute="True">

                                            <dx:ASPxDateEdit runat="server" ID="ExpiryDate" ClientInstanceName="ExpiryDate">
                                                <ValidationSettings RequiredField-IsRequired="true" RegularExpression-ErrorText=" " ValidationGroup="frmEditCommission"></ValidationSettings>
                                            </dx:ASPxDateEdit>
                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>





                                <dx:LayoutItem Caption="Commission" CaptionSettings-VerticalAlign="Middle">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">

                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxSpinEdit ID="Commission" runat="server" MaxLength="10" Width="100" ValidationSettings-ValidationGroup="frmEditUnderwriter"
                                                            MinValue="0" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" ValidationGroup="frmEditCommission">
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td>

                                                        <dx:ASPxRadioButtonList ID="CommCal" runat="server" RepeatDirection="Horizontal">
                                                            <Items>
                                                                <dx:ListEditItem Text="%" Value="x" />
                                                                <dx:ListEditItem Text="บาท" Value="+" />
                                                            </Items>
                                                            <Border BorderStyle="None" />
                                                        </dx:ASPxRadioButtonList>
                                                    </td>

                                                </tr>
                                            </table>

                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>


                                <dx:LayoutItem Caption="OR" CaptionSettings-VerticalAlign="Middle">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" SupportsDisabledAttribute="True">


                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxSpinEdit ID="ORComm" runat="server" MaxLength="10" Width="100" ValidationSettings-ValidationGroup="frmEditUnderwriter"
                                                            MinValue="0" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" ValidationGroup="frmEditCommission">
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td>

                                                        <dx:ASPxRadioButtonList ID="ORCommCal" runat="server" RepeatDirection="Horizontal">
                                                            <Items>
                                                                <dx:ListEditItem Text="%" Value="x" />
                                                                <dx:ListEditItem Text="บาท" Value="+" />
                                                            </Items>
                                                            <Border BorderStyle="None" />
                                                        </dx:ASPxRadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>


                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>

                                <dx:LayoutItem Caption="Admin1" CaptionSettings-VerticalAlign="Middle">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" SupportsDisabledAttribute="True">


                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxSpinEdit ID="Admin1" runat="server" MaxLength="10" Width="100" ValidationSettings-ValidationGroup="frmEditUnderwriter"
                                                            MinValue="0" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic" ValidationGroup="frmEditCommission">
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td>

                                                        <dx:ASPxRadioButtonList ID="Admin1Cal" runat="server" RepeatDirection="Horizontal">
                                                            <Items>
                                                                <dx:ListEditItem Text="%" Value="x" />
                                                                <dx:ListEditItem Text="บาท" Value="+" />
                                                            </Items>
                                                            <Border BorderStyle="None" />
                                                        </dx:ASPxRadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>


                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>


                                <dx:LayoutItem Caption="Admin2" CaptionSettings-VerticalAlign="Middle">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" SupportsDisabledAttribute="True">


                                            <table>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxSpinEdit ID="Admin2" runat="server" MaxLength="10" Width="100" ValidationSettings-ValidationGroup="frmEditUnderwriter"
                                                            MinValue="0" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                            <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic">
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>
                                                        </dx:ASPxSpinEdit>
                                                    </td>
                                                    <td>

                                                        <dx:ASPxRadioButtonList ID="Admin2Cal" runat="server" RepeatDirection="Horizontal">
                                                            <Items>
                                                                <dx:ListEditItem Text="%" Value="x" />
                                                                <dx:ListEditItem Text="บาท" Value="+" />
                                                            </Items>
                                                            <Border BorderStyle="None" />
                                                        </dx:ASPxRadioButtonList>
                                                    </td>
                                                </tr>
                                            </table>

                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>


                                <dx:LayoutItem Caption="IsActive">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server" SupportsDisabledAttribute="True">

                                            <dx:ASPxCheckBox ID="IsActive" runat="server">
                                            </dx:ASPxCheckBox>

                                        </dx:LayoutItemNestedControlContainer>
                                    </LayoutItemNestedControlCollection>
                                </dx:LayoutItem>


                                <dx:LayoutItem ShowCaption="False">
                                    <LayoutItemNestedControlCollection>
                                        <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server">


                                            <dx:ASPxButton ID="cmdSaveCommission" runat="server" ValidationContainerID="frmEditCommission" UseSubmitBehavior="false" CausesValidation="true" AutoPostBack="false"
                                                Text="Save">
                                                <ClientSideEvents Click="function(s){                                                                                                                                                       
                                                                            if(ASPxClientEdit.ValidateGroup('frmEditCommission')) 
                                                                            {
                                                                                LoadingPanel.Show(); 
                                                                                cbSaveCommission.PerformCallback('');             
                                                                            }                                                  
                                                                            e.processOnServer = false;       
                                                                        }                                                                    
                                                                    " />
                                            </dx:ASPxButton>

                                            <dx:ASPxCallback runat="server" ID="cbSaveCommission" ClientInstanceName="cbSaveCommission">
                                                <ClientSideEvents
                                                    CallbackError="function(s,e){LoadingPanel.Hide();}"
                                                    CallbackComplete="function(s,e){                                                                                     
                                                        LoadingPanel.Hide(); 

                                                        if(e.result == 'success')
                                                        {
                                                             popupEditCommission.Hide();
                                                             callbackPanel.PerformCallback('REFRESH'); 
                                                        }                                                     
                                                        else
                                                        {
                                                            alert(e.result);
                                                        }
                                           
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



<dx:ASPxPopupControl ID="popupMoreInfoLog"
    ClientInstanceName="popupMoreInfoLog"
    runat="server" AllowDragging="true"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    ShowPageScrollbarWhenModal="true"
    Modal="true"
    CloseAction="CloseButton" ShowMaximizeButton="true" ScrollBars="Auto"
    HeaderText="Commission">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
            <dx:ASPxCallbackPanel ID="callbackPanel_MoreInfoLog" ClientInstanceName="callbackPanel_MoreInfoLog" runat="server"
                RenderMode="Table">
                <ClientSideEvents CallbackError="function(s,e){LoadingPanel.Hide();}" />
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent5" runat="server">







      <dx:ASPxGridView ID="grid_CommissionLog" ClientInstanceName="grid_CommissionLog" runat="server" DataSourceID="SqlDataSource_CommissionLog"
                 AutoGenerateColumns="False" Width="500">
                <Columns> 

<%-- 
 
Code
Commission_display
ORCommission_display
Admin1_display
Admin2_display
EffectiveDate
ExpiryDate
CreationDate
CreationBy
IsActive
    --%>

<dx:GridViewDataColumn FieldName="Code" > </dx:GridViewDataColumn>
<dx:GridViewDataColumn FieldName="Commission_display">  </dx:GridViewDataColumn>                    
<dx:GridViewDataColumn FieldName="ORCommission_display" > </dx:GridViewDataColumn>
<dx:GridViewDataColumn FieldName="Admin1_display">  </dx:GridViewDataColumn>                    
<dx:GridViewDataColumn FieldName="Admin2_display" > </dx:GridViewDataColumn>
<dx:GridViewDataColumn FieldName="EffectiveDate">  </dx:GridViewDataColumn>                    
<dx:GridViewDataColumn FieldName="ExpiryDate" > </dx:GridViewDataColumn>
<dx:GridViewDataColumn FieldName="CreationDate">  </dx:GridViewDataColumn>                    
<dx:GridViewDataColumn FieldName="CreationBy" > </dx:GridViewDataColumn>
<dx:GridViewDataColumn FieldName="IsActive">  </dx:GridViewDataColumn>
                </Columns>
               
                <SettingsPager Mode="ShowAllRecords" /> 
            
            </dx:ASPxGridView>






<asp:SqlDataSource ID="SqlDataSource_CommissionLog" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select * from V_Campaign_CommissionLog where ID=@ID " >
    <SelectParameters>
        <asp:Parameter Name="ID" />
    </SelectParameters>
</asp:SqlDataSource>

































 
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <ClientSideEvents Shown="popup_ShownCommOutLog" />
</dx:ASPxPopupControl>


