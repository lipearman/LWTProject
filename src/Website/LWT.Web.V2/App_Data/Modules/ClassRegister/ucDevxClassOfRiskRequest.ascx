<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxClassOfRiskRequest.ascx.vb" Inherits="Modules_ucDevxClassOfRiskRequest" %>


<script type="text/javascript">
    var keyValue;

    function OnMoreInfo(element, key) {
        callbackPanel_MoreInfo.SetContentHtml("");
        popupMoreInfo.ShowAtElement(element);
        keyValue = key;
    }
    function popup_ShownCommOut(s, e) {
        callbackPanel_MoreInfo.PerformCallback(keyValue);
    }
</script>


<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="Underwriter" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxGridView ID="gridCOR" ClientInstanceName="gridCOR" runat="server" DataSourceID="SqlDataSource_COR"
                KeyFieldName="Risk" AutoGenerateColumns="False" Width="100%" SettingsBehavior-AutoExpandAllGroups="true">
                <SettingsPager Mode="ShowPager">
                    <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                </SettingsPager>
                <SettingsBehavior EnableRowHotTrack="true" ConfirmDelete="true" />
                <SettingsSearchPanel Visible="true" />
                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
             <%--   <SettingsCommandButton>
                    <UpdateButton ButtonType="Button" Text="บันทึก"></UpdateButton>
                    <EditButton ButtonType="Button" Text="แก้ไข"></EditButton>
                    <DeleteButton ButtonType="Button" Text="ลบ"></DeleteButton>
                    <CancelButton ButtonType="Button" Text="ยกเลิก"></CancelButton>
                </SettingsCommandButton>--%>


                <SettingsText PopupEditFormCaption="แก้ไขรายการ" ConfirmDelete="ยืนยันการลบ" />
                <SettingsPopup>
                    <EditForm Modal="true" Width="400" ShowHeader="true" HorizontalAlign="Center" VerticalAlign="Middle" />
                </SettingsPopup>
                <Settings ShowHeaderFilterButton="true" ShowGroupPanel="true" />
                <Columns>


                     <dx:GridViewDataColumn ReadOnly="true" FieldName="Risk" CellStyle-Wrap="False" CellStyle-Font-Bold="true">
                        <DataItemTemplate>
                            <a href="javascript:void(0);" onclick="OnMoreInfo(this, '<%# Container.KeyValue %>')"><%# Container.KeyValue %></a>
                        </DataItemTemplate>
                        <CellStyle Wrap="False"></CellStyle>

                    </dx:GridViewDataColumn>

                    <dx:GridViewDataTextColumn FieldName="Description" CellStyle-Wrap="False">

                        <PropertiesTextEdit>
                            <ValidationSettings Display="Dynamic" RequiredField-IsRequired="true" />
                        </PropertiesTextEdit>

                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="Department" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                        <PropertiesTextEdit>
                            <ValidationSettings Display="Dynamic" RequiredField-IsRequired="true" />
                        </PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataColumn FieldName="InsuranceLine" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="RiskShortDesc" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>

               





                    <dx:GridViewDataColumn FieldName="CommIn" Caption="CommIn" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="CommIn_Request" Caption="CommIn(Request)" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="CommOut" Caption="CommOut" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="CommOut_Request" Caption="CommOut(Request)" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>


                    <dx:GridViewDataColumn FieldName="CreationDate" Visible="false" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataColumn FieldName="CreationBy" Visible="false" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>


                    <dx:GridViewDataTextColumn FieldName="RequestDate" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataColumn FieldName="RequestBy" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>


                    <dx:GridViewDataColumn FieldName="ApproveDate" Visible="false" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataColumn FieldName="ApproveBy" Visible="false" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataColumn FieldName="IsActive" Visible="false" CellStyle-Wrap="False"></dx:GridViewDataColumn>

                    <dx:GridViewDataColumn FieldName="IsGeneralCode" Visible="false" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>

                    
                    <dx:GridViewDataColumn FieldName="status" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                        <EditFormSettings Visible="False" />
                        <DataItemTemplate>
                            <img src='images/<%# Eval("status").ToString().Trim()%>.gif' />
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>

                </Columns>

            </dx:ASPxGridView>

        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>



<dx:ASPxPopupControl ID="popupMoreInfo"
    ClientInstanceName="popupMoreInfo"
    runat="server" AllowDragging="true"
    PopupHorizontalAlign="LeftSides"
    PopupVerticalAlign="TopSides"
    ShowPageScrollbarWhenModal="true"
    Modal="true"
     CloseAction="CloseButton" ShowMaximizeButton="true" ScrollBars="Auto"
    HeaderText="Class Of Risk">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
            <dx:ASPxCallbackPanel ID="callbackPanel_MoreInfo" ClientInstanceName="callbackPanel_MoreInfo" runat="server"
                RenderMode="Table">
                <ClientSideEvents CallbackError="function(s,e){LoadingPanel.Hide();}" />
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent2" runat="server">


                        <dx:ASPxFormLayout runat="server" ID="settingsFormLayout" ClientInstanceName="settingsFormLayout" AlignItemCaptionsInAllGroups="True" Width="100%" SettingsItemCaptions-HorizontalAlign="Right">
                            <Items>

                                <dx:LayoutGroup Caption="Class Of Risk" GroupBoxDecoration="HeadingLine" ColCount="2">
                                    <Items>
                                        <dx:LayoutItem Caption="Risk">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxLabel runat="server" ID="lbRisk" ClientInstanceName="lbRisk"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Description">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxLabel runat="server" ID="lbDescription"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="Department">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxLabel runat="server" ID="lbDepartment"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem Caption="InsuranceLine">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxLabel runat="server" ID="lbInsuranceLine"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="RiskShortDesc">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox runat="server" ID="RiskShortDesc">
                                                        <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " ValidationGroup="settingsFormLayout" />
                                                        <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>



                                        <dx:LayoutItem Caption="Risk I (Lockton Group)">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxComboBox ID="RiskGroup1"
                                                        runat="server" Width="170"
                                                        DropDownStyle="DropDownList"
                                                        DataSourceID="SqlDataSource_RiskGroup1"
                                                        TextField="RiskGroup1"
                                                        ValueField="RiskGroup1">
                                                        <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " ValidationGroup="settingsFormLayout" />
                                                        <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                                    </dx:ASPxComboBox>


                                                    <asp:SqlDataSource ID="SqlDataSource_RiskGroup1" runat="server" ConnectionString="<%$ ConnectionStrings:SIBISDBConnectionString %>"
                                                        SelectCommand="SELECT rtrim(RiskGroup1) as RiskGroup1 FROM RiskGroup1  ORDER BY RiskGroup1"></asp:SqlDataSource>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="Risk II (LWT)">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxComboBox ID="RiskGroup2"
                                                        runat="server" Width="170"
                                                        DropDownStyle="DropDownList"
                                                        DataSourceID="SqlDataSource_RiskGroup2"
                                                        TextField="RiskGroup2"
                                                        ValueField="RiskGroup2">
                                                        <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " ValidationGroup="settingsFormLayout" />
                                                        <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                                    </dx:ASPxComboBox>


                                                    <asp:SqlDataSource ID="SqlDataSource_RiskGroup2" runat="server" ConnectionString="<%$ ConnectionStrings:SIBISDBConnectionString %>"
                                                        SelectCommand="SELECT rtrim(RiskGroup2) as RiskGroup2 FROM RiskGroup2  ORDER BY RiskGroup2"></asp:SqlDataSource>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="Risk Government">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxComboBox ID="RiskGovernment"
                                                        runat="server" Width="170"
                                                        DropDownStyle="DropDownList"
                                                        DataSourceID="SqlDataSource_RiskGovernment"
                                                        TextField="RiskGovernment"
                                                        ValueField="RiskGovernment">
                                                        <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " ValidationGroup="settingsFormLayout" />
                                                        <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                                    </dx:ASPxComboBox>


                                                    <asp:SqlDataSource ID="SqlDataSource_RiskGovernment" runat="server" ConnectionString="<%$ ConnectionStrings:SIBISDBConnectionString %>"
                                                        SelectCommand="SELECT rtrim(RiskGovernment) as RiskGovernment FROM RiskGovernment  ORDER BY RiskGovernment"></asp:SqlDataSource>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="Status" ColSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxImage ID="lbStatus" Height="15" runat="server"></dx:ASPxImage>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>



<dx:LayoutItem Caption="CommIn(Request)">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxLabel runat="server" ID="lbCommIn_Request" ClientInstanceName="lbCommIn_Request"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="CommOut(Request)">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxLabel runat="server" ID="lbCommOut_Request"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>


                                                    <dx:ASPxButton ID="btnSaveCOR" runat="server" ValidationContainerID="settingsFormLayout" Text="Save">
                                                        <ClientSideEvents Click="function(s, e) {  
                                                                        if(ASPxClientEdit.ValidateGroup('settingsFormLayout')) 
                                                                        {                                                               
                                                                            LoadingPanel.Show();
                                                                            cbSaveCOR.PerformCallback(lbRisk.GetText());
                                                                        }
                                                                        e.processOnServer = false;
                                                                    }
                                                                    " />
                                                    </dx:ASPxButton>
                                                    <dx:ASPxCallback runat="server" ID="cbSaveCOR" ClientInstanceName="cbSaveCOR">
                                                        <ClientSideEvents CallbackError="function(s,e){LoadingPanel.Hide();}"
                                                            CallbackComplete="function(s,e){
                                                                    LoadingPanel.Hide(); 
                                                                    if(e.result == 'success')
                                                                    {
                                                                         gridCOR.Refresh();
                                                                    }                                                     
                                                                    else
                                                                    {
                                                                        alert(e.result);
                                                                    }
                                                            }" />
                                                    </dx:ASPxCallback>



                                                    <dx:ASPxButton ID="btneApproveCOR" runat="server" ValidationContainerID="settingsFormLayout" Text="Approve">
                                                        <ClientSideEvents Click="function(s, e) {
                                                                        if(ASPxClientEdit.ValidateGroup('settingsFormLayout')) 
                                                                        {

                                                                                if(confirm('Confirm Approved?'))
                                                                                 {                                                                                                                              
                                                                                        LoadingPanel.Show();
                                                                                        cbApproveCOR.PerformCallback(lbRisk.GetText());
                                                                                 }
                                                                        }
                                                                        e.processOnServer = false;
                                                                    }
                                                                    " />
                                                    </dx:ASPxButton>
                                                    <dx:ASPxCallback runat="server" ID="cbApproveCOR" ClientInstanceName="cbApproveCOR" CausesValidation="false">
                                                        <ClientSideEvents CallbackError="function(s,e){LoadingPanel.Hide();}"
                                                            CallbackComplete="function(s,e){
                                                                    LoadingPanel.Hide(); 
                                                                    if(e.result == 'success')
                                                                    {
                                                                         popupMoreInfo.Hide();
                                                                         gridCOR.Refresh();
                                                                    }                                                     
                                                                    else
                                                                    {
                                                                        alert(e.result);
                                                                    }
                                                            }" />
                                                    </dx:ASPxCallback>
                                                    <dx:ASPxButton ID="btnRejectCOR" runat="server" Text="Reject">
                                                        <ClientSideEvents Click="function(s, e) {
                                                                    

                                                                    if(confirm('Confirm Reject?'))
                                                                     {
                                                                                                                              
                                                                            LoadingPanel.Show();
                                                                            cbRejectCOR.PerformCallback(lbRisk.GetText());
                                                                     }
                                                                    e.processOnServer = false;
                                                                    }
                                                                    " />
                                                    </dx:ASPxButton>
                                                    <dx:ASPxCallback runat="server" ID="cbRejectCOR" ClientInstanceName="cbRejectCOR" CausesValidation="false">
                                                        <ClientSideEvents CallbackError="function(s,e){LoadingPanel.Hide();}"
                                                            CallbackComplete="function(s,e){
                                                                    LoadingPanel.Hide(); 
                                                                    if(e.result == 'success')
                                                                    {
                                                                         popupMoreInfo.Hide();
                                                                         gridCOR.Refresh();
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
                                </dx:LayoutGroup>



                                <dx:LayoutGroup Caption="Commission In" GroupBoxDecoration="HeadingLine" ColCount="3">
                                    <Items>


                                        <dx:LayoutItem ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>


                                                    <dx:ASPxGridView ID="gridCommIn" ClientInstanceName="gridCommIn" runat="server" DataSourceID="SqlDataSource_CommIn"
                                                        KeyFieldName="ID" AutoGenerateColumns="False" Width="100%" SettingsBehavior-AutoExpandAllGroups="true">
                                                        <SettingsPager Mode="ShowPager">
                                                            <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                                                        </SettingsPager>
                                                        <SettingsBehavior EnableRowHotTrack="true" ConfirmDelete="true" />
                                                        <SettingsSearchPanel Visible="true" />
                                                        <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
                                                     <%--   <SettingsCommandButton>
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

                                                            <dx:GridViewDataColumn FieldName="Underwriter" Caption="UCode" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="AccountContact" Caption="Name" CellStyle-Wrap="False"></dx:GridViewDataColumn>

                                                            <dx:GridViewDataColumn FieldName="Brokerage_display" Caption="Brokerage" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="ORCommissionPercent_display" Caption="SF" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="AdminFeeIn1_display" Caption="Admin1" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="AdminFeeIn2_display" Caption="Admin2" CellStyle-Wrap="False"></dx:GridViewDataColumn>

                                                            <dx:GridViewDataColumn FieldName="OffsetORFlag" Caption="UpFront" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="Remark" CellStyle-Wrap="False"></dx:GridViewDataColumn>

                                                           


                                                            <dx:GridViewDataColumn FieldName="CreationDate" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="CreationBy" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="ApproveDate" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="ApproveBy" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="IsActive" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            

                                                            <dx:GridViewDataTextColumn FieldName="RequestDate" CellStyle-Wrap="False">
                                                                <EditFormSettings Visible="False" />
                                                            </dx:GridViewDataTextColumn>

                                                            <dx:GridViewDataColumn FieldName="RequestBy" CellStyle-Wrap="False">
                                                                <EditFormSettings Visible="False" />
                                                            </dx:GridViewDataColumn>

<dx:GridViewDataColumn FieldName="status" Caption="Status" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                                                                <EditFormSettings Visible="False" />
                                                                <DataItemTemplate>
                                                                    <img src='images/<%# Eval("status").ToString().Trim()%>.gif' />
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataColumn>


                                                        </Columns>

                                                    </dx:ASPxGridView>


                                                    <asp:SqlDataSource ID="SqlDataSource_CommIn" runat="server"
                                                        ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
                                                        SelectCommand="SELECT * from V_RiskUwriter where Risk=@Risk order by status desc">
                                                        <SelectParameters>
                                                            <asp:SessionParameter Name="Risk" SessionField="Risk" />
                                                        </SelectParameters>
                                                    </asp:SqlDataSource>



                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                    </Items>
                                </dx:LayoutGroup>

                                <dx:LayoutGroup Caption="Commission Out" GroupBoxDecoration="HeadingLine" ColCount="2">
                                    <Items>
                                        <dx:LayoutItem ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxGridView ID="gridCommOut" ClientInstanceName="gridCommOut" DataSourceID="SqlDataSource_CommOut"
                                                        runat="server"
                                                        KeyFieldName="ID">

                                                        <SettingsPager Mode="ShowPager">
                                                            <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                                                        </SettingsPager>
                                                        <SettingsBehavior EnableRowHotTrack="true" ConfirmDelete="true" />
                                                        <SettingsSearchPanel Visible="true" />
                                                        <Settings ShowHeaderFilterButton="true" />


                                                        <Columns>
                                                            <%--<dx:GridViewDataTextColumn FieldName="Risk" Caption="Risk" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />--%>
                                                            <dx:GridViewDataTextColumn FieldName="Agent" Caption="Code" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="Name" Caption="Name" CellStyle-Wrap="False" />
                                                            <dx:GridViewDataTextColumn FieldName="CommissionOut_Display" Caption="CommOut" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="OROut_Display" Caption="SF" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
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
                                                            
                                                             <dx:GridViewDataColumn FieldName="IsActive" CellStyle-Wrap="False" />





                                                            


                                                              <dx:GridViewDataTextColumn FieldName="RequestDate" CellStyle-Wrap="False">
                                                                <EditFormSettings Visible="False" />
                                                            </dx:GridViewDataTextColumn>

                                                            <dx:GridViewDataColumn FieldName="RequestBy" CellStyle-Wrap="False">
                                                                <EditFormSettings Visible="False" />
                                                            </dx:GridViewDataColumn>


                                                            <dx:GridViewDataColumn FieldName="status" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                                                                <EditFormSettings Visible="False" />
                                                                <DataItemTemplate>
                                                                    <img src='images/<%# Eval("status").ToString().Trim()%>.gif' />
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataColumn>


                                                        </Columns>
                                                    </dx:ASPxGridView>

                                                     <asp:SqlDataSource ID="SqlDataSource_CommOut" runat="server"
                            ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
                            SelectCommand="SELECT * from V_AgentRiskComm where Risk=@Risk order by status desc">
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
    <ClientSideEvents Shown="popup_ShownCommOut" />
</dx:ASPxPopupControl>



<asp:SqlDataSource ID="SqlDataSource_COR" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select * from [V_ClassOfRisk] where  (status in('req') or CommOut_request > 0 or CommIn_request > 0) order by status desc ">
    <SelectParameters>
        <asp:Parameter Name="UserName" />
    </SelectParameters>


</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_SIBISCOR" runat="server" ConnectionString="<%$ ConnectionStrings:SIBISDBConnectionString %>"
    SelectCommand="SELECT * from ClassOfRisk"></asp:SqlDataSource>
