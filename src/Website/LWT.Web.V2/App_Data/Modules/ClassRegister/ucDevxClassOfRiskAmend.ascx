<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxClassOfRiskAmend.ascx.vb" Inherits="Modules_ucDevxClassOfRiskAmend" %>


<script type="text/javascript">
    var keyValue;

    function OnEditAmend(element, key) {

        callbackPanel_EditAmend.SetContentHtml("");

        popupEditAmend.ShowAtElement(element);

        keyValue = key;
    }
    function popup_ShownEditAmend(s, e) {
        callbackPanel_EditAmend.PerformCallback(keyValue);
    }


    function OnMoreInfo(element, key) {
        callbackPanel_MoreInfo.SetContentHtml("");
        popupMoreInfo.ShowAtElement(element);
        keyValue = key;
    }
    function popup_ShownCommOut(s, e) {
        callbackPanel_MoreInfo.PerformCallback(keyValue);
    }
</script>


<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="Amend Class Of Risk" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxButton ID="cmdAddNewAmendRisk" ClientInstanceName="cmdAddNewAmendRisk" runat="server" Text="Add New Amend" Image-IconID="actions_add_16x16">
                <ClientSideEvents Click="function(s, e) {           
                                                    popupAddNewAmendRisk.Show();                                                     
                                                    callbackPanel_AddNewAmendRisk.PerformCallback('initdata');
                                                    e.processOnServer = false; 
                                                    }" />
            </dx:ASPxButton>

            <dx:ASPxGridView ID="grid_ClassOfRisk_Amend" ClientInstanceName="grid_ClassOfRisk_Amend" runat="server" DataSourceID="SqlDataSource_ClassOfRisk_Amend"
                KeyFieldName="AmendRiskID" AutoGenerateColumns="False" Width="100%" SettingsBehavior-AutoExpandAllGroups="true">
                <SettingsPager Mode="ShowPager">
                    <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                </SettingsPager>
                <SettingsBehavior EnableRowHotTrack="true" ConfirmDelete="true" />
                <SettingsSearchPanel Visible="true" />
                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
              <%--  <SettingsCommandButton>
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


                    <dx:GridViewDataTextColumn Caption="#" VisibleIndex="0" Settings-AllowHeaderFilter="False" Width="50">
                        <DataItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <dx:ASPxButton runat="server" AutoPostBack="false"
                                            RenderMode="Button"
                                            ID="RowEditable"
                                            CommandArgument='<%# Eval("AmendRiskID")%>'
                                            Text="แก้ไข">
                                            <ClientSideEvents Click="function(s){ 
                                                 OnEditAmend(s.this, s.cpAmendRiskID )                        
                                              }" />
                                        </dx:ASPxButton>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>
                                        <dx:ASPxButton runat="server" AutoPostBack="false"
                                            RenderMode="Button"
                                            ID="RowDeleteable"
                                            CommandArgument='<%# Eval("AmendRiskID")%>'
                                            Text="ลบ">
                                            <ClientSideEvents Click="function(s) 
                                                {
                                                    if(confirm('Do you want to delete?'))
                                                    {  
                                                        grid_ClassOfRisk_Amend.DeleteRow(s.cpVisibleIndex);
                                                    }
                                                    e.processOnServer = false;
                                                }
                                            " />
                                        </dx:ASPxButton>

                                    </td>
                                </tr>

                            </table>


                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                    
                    <dx:GridViewDataColumn FieldName="status" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                        <EditFormSettings Visible="False" />
                        <DataItemTemplate>
                            <img src='images/<%# Eval("status").ToString().Trim()%>.gif' />
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn ReadOnly="true" FieldName="Risk" CellStyle-Wrap="False" CellStyle-Font-Bold="true">
                        <DataItemTemplate>
                            <a href="javascript:void(0);" onclick="OnMoreInfo(this, '<%# Container.KeyValue %>')"><%# Eval("Risk").ToString().Trim()%></a>
                        </DataItemTemplate>
                        <CellStyle Wrap="False"></CellStyle>

                    </dx:GridViewDataColumn>


                    <%--                    <dx:GridViewDataColumn ReadOnly="true" FieldName="Risk" CellStyle-Wrap="False" CellStyle-Font-Bold="true">
                        <DataItemTemplate>
                            <a href="javascript:void(0);" onclick="OnEditAmend(this, '<%# Container.KeyValue %>')"><%# Eval("Risk").ToString().Trim()%></a>
                        </DataItemTemplate>
                        <CellStyle Wrap="False"></CellStyle>
                    </dx:GridViewDataColumn>--%>







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
                    <dx:GridViewDataColumn FieldName="CommOut" Caption="CommOut" CellStyle-Wrap="False">
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

                </Columns>

            </dx:ASPxGridView>

        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<dx:ASPxPopupControl ID="popupAddNewAmendRisk"
    ClientInstanceName="popupAddNewAmendRisk"
    runat="server" AllowDragging="true"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    ShowPageScrollbarWhenModal="true"
    Width="980"
    Modal="true"
    CloseAction="CloseButton" ShowMaximizeButton="true" ScrollBars="Auto"
    HeaderText="Class Of Risk">
    <ClientSideEvents AfterResizing="function(s){s.AdjustControl()}" />
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            <dx:ASPxCallbackPanel ID="callbackPanel_AddNewAmendRisk" ClientInstanceName="callbackPanel_AddNewAmendRisk" runat="server"
                RenderMode="Table">
                <ClientSideEvents CallbackError="function(s,e){
                        LoadingPanel.Hide(); 
                    }" />
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent1" runat="server">


                        <dx:ASPxFormLayout runat="server" ID="ASPxFormLayout1" ClientInstanceName="settingsFormLayout"
                            AlignItemCaptionsInAllGroups="True" Width="100%">
                            <Items>

                                <dx:LayoutGroup Caption="Class Of Risk" GroupBoxDecoration="HeadingLine" ColCount="2">
                                    <Items>
                                        <dx:LayoutItem Caption="Class Of Risk" ColSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>



                                                    <dx:ASPxGridLookup ID="newCOR" ClientInstanceName="newCOR" runat="server" SelectionMode="Single"
                                                        DataSourceID="SqlDataSource_newCOR" Width="300"
                                                        KeyFieldName="Risk" TextFormatString="{0}">
                                                        <ClientSideEvents ValueChanged="function (s, e){
                                                         callbackPanel_AddNewAmendRisk.PerformCallback('select_cor');
                                                         }" />

                                                        <GridViewProperties>

                                                            <Settings ShowHeaderFilterButton="true" />
                                                            <SettingsSearchPanel Visible="true" />
                                                            <SettingsBehavior EnableRowHotTrack="true" />

                                                        </GridViewProperties>
                                                        <Columns>
                                                            <dx:GridViewDataColumn FieldName="Risk" />
                                                            <dx:GridViewDataColumn FieldName="Description" Width="50" CellStyle-Wrap="False" />
                                                            <dx:GridViewDataColumn FieldName="RiskShortDesc" CellStyle-Wrap="False" />
                                                            <dx:GridViewDataColumn FieldName="InsuranceLine" CellStyle-Wrap="False" />
                                                            <dx:GridViewDataColumn FieldName="Department" CellStyle-Wrap="False" />
                                                            <dx:GridViewDataColumn FieldName="RiskGovernment" CellStyle-Wrap="False" />
                                                        </Columns>
                                                        <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" "></ValidationSettings>
                                                    </dx:ASPxGridLookup>


                                                    <asp:SqlDataSource ID="SqlDataSource_newCOR" runat="server"
                                                        ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
                                                        SelectCommand="SELECT * from V_ClassOfRisk where status='approved' and IsActive=1  "></asp:SqlDataSource>

                                                    <br />
                                                    <dx:ASPxButton ID="btnAddNewAmendRisk" runat="server" ClientInstanceName="btnAddNewAmendRisk" ClientVisible="false" AutoPostBack="false" Text="Add Amend Risk" Image-IconID="actions_add_16x16">
                                                        <ClientSideEvents Click="function(s, e) {   
                                                   if(ASPxClientEdit.AreEditorsValid()) {
                                                        LoadingPanel.Show();
                                                        cbAddNewAmendRisk.PerformCallback('');                                                       
                                                    }
                                                    e.processOnServer = false; 
                                                }" />
                                                    </dx:ASPxButton>

                                                    <dx:ASPxCallback runat="server" ID="cbAddNewAmendRisk" ClientInstanceName="cbAddNewAmendRisk">
                                                        <ClientSideEvents
                                                            CallbackError="function(s,e){LoadingPanel.Hide();}"
                                                            CallbackComplete="function(s,e){                                                                                     
                                LoadingPanel.Hide(); 

                                if(e.result == 'success')
                                {
                                    popupAddNewAmendRisk.Hide();
                                    grid_ClassOfRisk_Amend.ApplySearchPanelFilter(newCOR.GetValue());
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

                                <dx:LayoutGroup Caption="Underwriter" GroupBoxDecoration="HeadingLine" ColCount="2">
                                    <Items>


                                        <dx:LayoutItem ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxGridView ID="newgridCommIn" ClientInstanceName="newgridCommIn" DataSourceID="SqlDataSource_newCommIn"
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
                                                            <dx:GridViewDataColumn FieldName="ORCommissionPercent_display" Caption="SF" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="AdminFeeIn1_display" Caption="Admin1" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="AdminFeeIn2_display" Caption="Admin2" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="Remark" CellStyle-Wrap="False"></dx:GridViewDataColumn>

                                                        </Columns>



                                                    </dx:ASPxGridView>



                                                    <asp:SqlDataSource ID="SqlDataSource_newCommIn" runat="server"
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

                                                    <dx:ASPxGridView ID="newgridCommOut" ClientInstanceName="newgridCommOut" DataSourceID="SqlDataSource_NewCommOut"
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
                                                        </Columns>
                                                    </dx:ASPxGridView>
                                                    <asp:SqlDataSource ID="SqlDataSource_NewCommOut" runat="server"
                                                        ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
                                                        SelectCommand="SELECT * from V_AgentRiskComm where Risk=@Risk">
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

<dx:ASPxPopupControl ID="popupEditAmend"
    ClientInstanceName="popupEditAmend"
    runat="server" AllowDragging="true"
    PopupHorizontalAlign="LeftSides"
    PopupVerticalAlign="WindowCenter"
    ShowPageScrollbarWhenModal="true"
    Modal="true"
    CloseAction="CloseButton" ShowMaximizeButton="true" ScrollBars="Auto"
    HeaderText="Comm Out">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
            <dx:ASPxCallbackPanel ID="callbackPanel_EditAmend" ClientInstanceName="callbackPanel_EditAmend" runat="server"
                RenderMode="Table">
                <ClientSideEvents CallbackError="function(s,e){LoadingPanel.Hide();}" />
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent2" runat="server">


                        <dx:ASPxFormLayout runat="server" ID="settingsFormLayout" AlignItemCaptionsInAllGroups="True" Width="100%" SettingsItemCaptions-HorizontalAlign="Right">
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
                                                    <dx:ASPxLabel runat="server" ID="lbRiskShortDesc"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="Risk I">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxLabel runat="server" ID="lbRiskGroupI"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="Risk II">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxLabel runat="server" ID="lbRiskGroupII"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="Risk Government">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxLabel runat="server" ID="lbRiskGovernment"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>





                                        <dx:LayoutItem Caption="Create By">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxLabel runat="server" ID="lbCreateBy"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Create Date">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxLabel runat="server" ID="lbCreateDate"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Request By">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxLabel runat="server" ID="lbRequestBy"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Request Date">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxLabel runat="server" ID="lbRequestdate"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Approve By">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxLabel runat="server" ID="lbApproveBy"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Approve Date">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxLabel runat="server" ID="lbApproveDate"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>


                                                    <dx:ASPxButton ID="btnRequestAmend" ClientInstanceName="btnRequestAmend" runat="server" Text="Send Request Amend">
                                                        <ClientSideEvents Click="function(s, e) { 
                                                                        if(confirm('Confirm to Send Request Amend?'))
                                                                        {                                                       
                                                                          LoadingPanel.Show();
                                                                          cbRequestAmend.PerformCallback('');
                                                                        }
                                                                         e.processOnServer = false;

                                                                    }
                                                                    " />
                                                    </dx:ASPxButton>

                                                    <dx:ASPxCallback runat="server" ID="cbRequestAmend" ClientInstanceName="cbRequestAmend">
                                                        <ClientSideEvents CallbackError="function(s,e){LoadingPanel.Hide();}"
                                                            CallbackComplete="function(s,e){
                                                                    LoadingPanel.Hide(); 
                                                                    if(e.result == 'success')
                                                                    {
                                                                         popupEditAmend.Hide();
                                                                         grid_ClassOfRisk_Amend.Refresh();
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

                                                    <dx:ASPxGridView ID="gridCommInRequest" ClientInstanceName="gridCommInRequest" runat="server" DataSourceID="SqlDataSource_CommInRequest"
                                                        KeyFieldName="ID" AutoGenerateColumns="False" Width="100%" SettingsBehavior-AutoExpandAllGroups="true">
                                                        <SettingsPager Mode="ShowPager">
                                                            <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                                                        </SettingsPager>
                                                        <SettingsBehavior EnableRowHotTrack="true" ConfirmDelete="true" />
                                                        <SettingsSearchPanel Visible="true" />
                                                        <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
                                                      <%--  <SettingsCommandButton>
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

                                                        <SettingsDetail ShowDetailRow="true" />

                                                        <Columns>
                                                            <dx:GridViewCommandColumn Width="50" CellStyle-Wrap="False" ShowEditButton="true" />

                                                            <dx:GridViewDataColumn FieldName="Underwriter" Caption="UCode" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="AccountContact" Caption="Name" CellStyle-Wrap="False"></dx:GridViewDataColumn>

                                                            <dx:GridViewDataColumn FieldName="Brokerage_display" Caption="Brokerage" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="ORCommissionPercent_display" Caption="OR" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="AdminFeeIn1_display" Caption="Admin1" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="AdminFeeIn2_display" Caption="Admin2" CellStyle-Wrap="False"></dx:GridViewDataColumn>

                                                            <dx:GridViewDataColumn FieldName="OffsetORFlag" Caption="UpFront" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="Remark" CellStyle-Wrap="False"></dx:GridViewDataColumn>




                                                            <%--  <dx:GridViewDataColumn FieldName="CreationDate" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="CreationBy" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="ApproveDate" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="ApproveBy" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataColumn>--%>

                                                            <%--                                                            <dx:GridViewDataTextColumn FieldName="RequestDate" CellStyle-Wrap="False">
                                                                <EditFormSettings Visible="False" />
                                                            </dx:GridViewDataTextColumn>

                                                            <dx:GridViewDataColumn FieldName="RequestBy" CellStyle-Wrap="False">
                                                                <EditFormSettings Visible="False" />
                                                            </dx:GridViewDataColumn>--%>

                                                            <dx:GridViewDataColumn FieldName="IsActive" CellStyle-Wrap="False"></dx:GridViewDataColumn>


                                                            <dx:GridViewDataColumn FieldName="status" Visible="false" Caption="Status" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                                                                <EditFormSettings Visible="False" />
                                                                <DataItemTemplate>
                                                                    <img src='images/<%# Eval("status").ToString().Trim()%>.gif' />
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataColumn>

                                                        </Columns>




                                                        <Templates>

                                                            <DetailRow>
                                                                Original:

                                                                <dx:ASPxGridView ID="detailGrid_CommIn" runat="server" DataSourceID="SqlDataSource_CommIn" KeyFieldName="Underwriter"
                                                                    Width="300" OnBeforePerformDataSelect="detailGrid_CommIn_DataSelect">
                                                                    <Columns>

                                                                        <%--                                                                        <dx:GridViewDataColumn FieldName="Underwriter" Caption="UCode" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                                        <dx:GridViewDataColumn FieldName="AccountContact" Caption="Name" CellStyle-Wrap="False"></dx:GridViewDataColumn>--%>
                                                                        <dx:GridViewDataColumn FieldName="Brokerage_display" Caption="Brokerage" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                                        <dx:GridViewDataColumn FieldName="ORCommissionPercent_display" Caption="SF" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                                        <dx:GridViewDataColumn FieldName="AdminFeeIn1_display" Caption="Admin1" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                                        <dx:GridViewDataColumn FieldName="AdminFeeIn2_display" Caption="Admin2" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                                        <dx:GridViewDataColumn FieldName="Remark" CellStyle-Wrap="False"></dx:GridViewDataColumn>

                                                                    </Columns>
                                                                </dx:ASPxGridView>
                                                            </DetailRow>





                                                            <EditForm>

                                                                <dx:ASPxFormLayout ID="frmEditUnderwriter" ClientInstanceName="frmEditUnderwriter" runat="server" ColCount="2" RequiredMarkDisplayMode="None">
                                                                    <Items>

                                                                        <dx:LayoutItem Caption="Underwriter">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer
                                                                                    ID="LayoutItemNestedControlContainer23"
                                                                                    runat="server"
                                                                                    SupportsDisabledAttribute="True">

                                                                                    <dx:ASPxLabel runat="server" ID="Underwriter" Value='<%# Eval("Underwriter")%>'></dx:ASPxLabel>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>

                                                                        <dx:LayoutItem Caption="Risk">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer
                                                                                    ID="LayoutItemNestedControlContainer9"
                                                                                    runat="server"
                                                                                    SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxLabel runat="server" ID="Risk" Value='<%# Eval("Risk")%>'></dx:ASPxLabel>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>

                                                                        <dx:LayoutItem Caption="Brokerage" CaptionSettings-VerticalAlign="Middle">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">

                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dx:ASPxSpinEdit ID="Brokerage" runat="server" MaxLength="10" Width="100" ValidationSettings-ValidationGroup="frmEditUnderwriter"
                                                                                                    MinValue="0" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" Value='<%# Eval("Brokerage")%>' NumberType="Integer">
                                                                                                    <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic">
                                                                                                        <RequiredField IsRequired="true" />
                                                                                                    </ValidationSettings>
                                                                                                </dx:ASPxSpinEdit>
                                                                                            </td>
                                                                                            <td>

                                                                                                <dx:ASPxRadioButtonList ID="BrokerageCal" runat="server" RepeatDirection="Horizontal" Value='<%# Eval("BrokerageCal")%>'>
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


                                                                        <dx:LayoutItem Caption="SF" CaptionSettings-VerticalAlign="Middle">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" SupportsDisabledAttribute="True">


                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dx:ASPxSpinEdit ID="OR" runat="server" MaxLength="10" Width="100" Value='<%# Eval("ORCommissionPercent")%>' ValidationSettings-ValidationGroup="frmEditUnderwriter"
                                                                                                    MinValue="0" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                                                    <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic">
                                                                                                        <RequiredField IsRequired="true" />
                                                                                                    </ValidationSettings>
                                                                                                </dx:ASPxSpinEdit>
                                                                                            </td>
                                                                                            <td>

                                                                                                <dx:ASPxRadioButtonList ID="ORCal" runat="server" RepeatDirection="Horizontal" Value='<%# Eval("ORInCal")%>'>
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
                                                                                                <dx:ASPxSpinEdit ID="Admin1" runat="server" MaxLength="10" Width="100" Value='<%# Eval("AdminFeeIn1")%>' ValidationSettings-ValidationGroup="frmEditUnderwriter"
                                                                                                    MinValue="0" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                                                    <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic">
                                                                                                        <RequiredField IsRequired="true" />
                                                                                                    </ValidationSettings>
                                                                                                </dx:ASPxSpinEdit>
                                                                                            </td>
                                                                                            <td>

                                                                                                <dx:ASPxRadioButtonList ID="Admin1Cal" runat="server" RepeatDirection="Horizontal" Value='<%# Eval("AdminFeeIn1Cal")%>'>
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
                                                                                                <dx:ASPxSpinEdit ID="Admin2" runat="server" MaxLength="10" Width="100" Value='<%# Eval("AdminFeeIn2")%>' ValidationSettings-ValidationGroup="frmEditUnderwriter"
                                                                                                    MinValue="0" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                                                    <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic">
                                                                                                        <RequiredField IsRequired="true" />
                                                                                                    </ValidationSettings>
                                                                                                </dx:ASPxSpinEdit>
                                                                                            </td>
                                                                                            <td>

                                                                                                <dx:ASPxRadioButtonList ID="Admin2Cal" runat="server" RepeatDirection="Horizontal" Value='<%# Eval("AdminFeeIn2Cal")%>'>
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


                                                                        <dx:LayoutItem Caption="Upfont">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" SupportsDisabledAttribute="True">


                                                                                    <dx:ASPxCheckBox runat="server" ID="OffsetORFlag" Value='<%# Eval("OffsetORFlag")%>'></dx:ASPxCheckBox>


                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>


                                                                        <dx:LayoutItem Caption="CalFrom">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer21" runat="server" SupportsDisabledAttribute="True">

                                                                                    <dx:ASPxComboBox ID="ORCalFrom" runat="server" Width="100" Value='<%# Eval("ORCalFrom")%>'>
                                                                                        <Items>
                                                                                            <dx:ListEditItem Text="PREMIUM" Value="P" />
                                                                                            <dx:ListEditItem Text="BROKERAGE" Value="B" />
                                                                                        </Items>
                                                                                    </dx:ASPxComboBox>


                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>





                                                                        <dx:LayoutItem Caption="Remark">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxMemo runat="server" ID="Remark" MaxLength="255" Value='<%# Eval("Remark")%>'></dx:ASPxMemo>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>

                                                                        <dx:LayoutItem Caption="IsActive">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server" SupportsDisabledAttribute="True">

                                                                                    <dx:ASPxCheckBox ID="IsActive" runat="server" Value='<%# Eval("IsActive")%>'>
                                                                                    </dx:ASPxCheckBox>

                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>


                                                                        <dx:LayoutItem ShowCaption="False">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server">

                                                                                    <table style="float: left">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dx:ASPxButton ID="b1" runat="server" UseSubmitBehavior="false" CausesValidation="true" AutoPostBack="false"
                                                                                                    Text="Save">
                                                                                                    <ClientSideEvents Click="function(s){
                                                                                if(ASPxClientEdit.ValidateGroup('frmEditUnderwriter')) 
                                                                                {
                                                                                        gridCommInRequest.UpdateEdit();              
                                                                                }          
                                                                            }                                                                    
                                                                        " />
                                                                                                </dx:ASPxButton>
                                                                                            </td>
                                                                                            <td>&nbsp;</td>
                                                                                            <td>
                                                                                                <dx:ASPxButton ID="b2" runat="server" UseSubmitBehavior="false" AutoPostBack="false"
                                                                                                    Text="Cancel">
                                                                                                    <ClientSideEvents Click="function(s){
                                                                        gridCommInRequest.CancelEdit();
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

                                <dx:LayoutGroup Caption="Commission Out" GroupBoxDecoration="HeadingLine" ColCount="2">
                                    <Items>
                                        <dx:LayoutItem ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxGridView ID="gridCommOutRequest" ClientInstanceName="gridCommOutRequest" DataSourceID="SqlDataSource_CommOutRequest"
                                                        runat="server"
                                                        KeyFieldName="ID">

                                                        <SettingsPager Mode="ShowPager">
                                                            <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                                                        </SettingsPager>
                                                        <SettingsBehavior EnableRowHotTrack="true" ConfirmDelete="true" />
                                                        <SettingsSearchPanel Visible="true" />
                                                        <Settings ShowHeaderFilterButton="true" />
                                                       <%-- <SettingsCommandButton>
                                                            <UpdateButton ButtonType="Button" Text="บันทึก"></UpdateButton>
                                                            <EditButton ButtonType="Button" Text="แก้ไข"></EditButton>
                                                            <DeleteButton ButtonType="Button" Text="ลบ"></DeleteButton>
                                                            <CancelButton ButtonType="Button" Text="ยกเลิก"></CancelButton>
                                                        </SettingsCommandButton>--%>
                                                        <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />

                                                        <SettingsPopup>
                                                            <EditForm Modal="true" MinWidth="100" Width="400" HorizontalAlign="WindowCenter" VerticalAlign="Middle" />
                                                            <HeaderFilter Height="200" />
                                                        </SettingsPopup>

                                                        <SettingsDetail ShowDetailRow="true" />
                                                        <Columns>
                                                            <dx:GridViewCommandColumn Width="100" CellStyle-Wrap="False" ShowEditButton="true" />

                                                            <%-- <dx:GridViewDataTextColumn FieldName="Risk" Caption="Risk" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />--%>
                                                            <dx:GridViewDataTextColumn FieldName="Agent" Caption="Code" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="Name" Caption="Name" CellStyle-Wrap="False" />
                                                            <dx:GridViewDataTextColumn FieldName="CommissionOut_Display" Caption="CommOut" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="OROut_Display" Caption="SF" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="AdminOut1_Display" Caption="Admin1" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="AdminOut2_Display" Caption="Admin2" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <%--            <dx:GridViewDataTextColumn FieldName="CreationDate"  CellStyle-Wrap="False"/>
            <dx:GridViewDataTextColumn FieldName="CreationBy"  CellStyle-Wrap="False"/>
                                                            --%>
                                                            <%--<dx:GridViewDataTextColumn FieldName="OROutCalFrom" Caption="OROutCalFrom"  CellStyle-Wrap="False"/>--%>





                                                            <dx:GridViewDataTextColumn FieldName="AgentGroup" Caption="Group" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />

                                                            <dx:GridViewDataTextColumn FieldName="Remark" Caption="Remark" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />



                                                            <%-- <dx:GridViewDataColumn FieldName="HasOverComm" Caption="OverComm" CellStyle-Wrap="False"></dx:GridViewDataColumn>--%>

                                                            <dx:GridViewDataColumn FieldName="HasOverComm" Caption="OverComm" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                                                                <EditFormSettings Visible="False" />
                                                                <DataItemTemplate>
                                                                    <img src='images/alert_<%# Eval("HasOverComm").ToString().Trim()%>.gif' style="height: 15px;" />
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="IsActive" CellStyle-Wrap="False" />

                                                            <dx:GridViewDataColumn FieldName="status" Visible="false" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                                                                <EditFormSettings Visible="False" />
                                                                <DataItemTemplate>
                                                                    <img src='images/<%# Eval("status").ToString().Trim()%>.gif' />
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataColumn>
                                                        </Columns>



                                                        <Templates>
                                                            <DetailRow>
                                                                Original:
                                                                <dx:ASPxGridView ID="detailGrid_CommOut" runat="server" DataSourceID="SqlDataSource_CommOut" KeyFieldName="Agent"
                                                                    Width="300" OnBeforePerformDataSelect="detailGrid_CommOut_DataSelect">
                                                                    <Columns>
                                                                        <dx:GridViewDataTextColumn FieldName="CommissionOut_Display" Caption="CommOut" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                                        <dx:GridViewDataTextColumn FieldName="OROut_Display" Caption="SF" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                                        <dx:GridViewDataTextColumn FieldName="AdminOut1_Display" Caption="Admin1" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                                        <dx:GridViewDataTextColumn FieldName="AdminOut2_Display" Caption="Admin2" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />

                                                                    </Columns>
                                                                </dx:ASPxGridView>

                                                            </DetailRow>


                                                            <EditForm>

                                                                <dx:ASPxFormLayout ID="frmEditAgentRisk" ClientInstanceName="frmEditAgentRisk" runat="server" ColCount="2" RequiredMarkDisplayMode="None">
                                                                    <Items>

                                                                        <dx:LayoutItem Caption="Agent">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer
                                                                                    ID="LayoutItemNestedControlContainer23"
                                                                                    runat="server"
                                                                                    SupportsDisabledAttribute="True">

                                                                                    <dx:ASPxLabel runat="server" ID="Agent" Value='<%# Eval("Agent")%>'></dx:ASPxLabel>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>

                                                                        <dx:LayoutItem Caption="Risk">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer
                                                                                    ID="LayoutItemNestedControlContainer9"
                                                                                    runat="server"
                                                                                    SupportsDisabledAttribute="True">
                                                                                    <dx:ASPxLabel runat="server" ID="Risk" Value='<%# Eval("Risk")%>'></dx:ASPxLabel>
                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>





                                                                        <dx:LayoutItem Caption="CommissionOut" CaptionSettings-VerticalAlign="Middle">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">

                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dx:ASPxSpinEdit ID="CommissionOut" runat="server" MaxLength="10" Width="100" ValidationSettings-ValidationGroup="frmEditUnderwriter"
                                                                                                    MinValue="0" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" Value='<%# Eval("CommissionOut")%>' NumberType="Integer">
                                                                                                    <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic">
                                                                                                        <RequiredField IsRequired="true" />
                                                                                                    </ValidationSettings>
                                                                                                </dx:ASPxSpinEdit>
                                                                                            </td>
                                                                                            <td>

                                                                                                <dx:ASPxRadioButtonList ID="CommOutCal" runat="server" RepeatDirection="Horizontal" Value='<%# Eval("CommOutCal")%>'>
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


                                                                        <dx:LayoutItem Caption="SF" CaptionSettings-VerticalAlign="Middle">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" SupportsDisabledAttribute="True">


                                                                                    <table>
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dx:ASPxSpinEdit ID="OROut" runat="server" MaxLength="10" Width="100" Value='<%# Eval("OROut")%>' ValidationSettings-ValidationGroup="frmEditUnderwriter"
                                                                                                    MinValue="0" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                                                    <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic">
                                                                                                        <RequiredField IsRequired="true" />
                                                                                                    </ValidationSettings>
                                                                                                </dx:ASPxSpinEdit>
                                                                                            </td>
                                                                                            <td>

                                                                                                <dx:ASPxRadioButtonList ID="OROutCal" runat="server" RepeatDirection="Horizontal" Value='<%# Eval("OROutCal")%>'>
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
                                                                                                <dx:ASPxSpinEdit ID="AdminOut1" runat="server" MaxLength="10" Width="100" Value='<%# Eval("AdminOut1")%>' ValidationSettings-ValidationGroup="frmEditUnderwriter"
                                                                                                    MinValue="0" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                                                    <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic">
                                                                                                        <RequiredField IsRequired="true" />
                                                                                                    </ValidationSettings>
                                                                                                </dx:ASPxSpinEdit>
                                                                                            </td>
                                                                                            <td>

                                                                                                <dx:ASPxRadioButtonList ID="AdminOut1Cal" runat="server" RepeatDirection="Horizontal" Value='<%# Eval("AdminOut1Cal")%>'>
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
                                                                                                <dx:ASPxSpinEdit ID="AdminOut2" runat="server" MaxLength="10" Width="100" Value='<%# Eval("AdminOut2")%>' ValidationSettings-ValidationGroup="frmEditUnderwriter"
                                                                                                    MinValue="0" AllowMouseWheel="false" SpinButtons-ShowIncrementButtons="false" NumberType="Integer">
                                                                                                    <ValidationSettings ErrorDisplayMode="Text" Display="Dynamic">
                                                                                                        <RequiredField IsRequired="true" />
                                                                                                    </ValidationSettings>
                                                                                                </dx:ASPxSpinEdit>
                                                                                            </td>
                                                                                            <td>

                                                                                                <dx:ASPxRadioButtonList ID="AdminOut2Cal" runat="server" RepeatDirection="Horizontal" Value='<%# Eval("AdminOut2Cal")%>'>
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


                                                                        <dx:LayoutItem Caption="CalFrom">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer21" runat="server" SupportsDisabledAttribute="True">

                                                                                    <dx:ASPxComboBox ID="OROutCalFrom" runat="server" Width="100" Value='<%# Eval("OROutCalFrom")%>'>
                                                                                        <Items>
                                                                                            <dx:ListEditItem Text="PREMIUM" Value="P" />
                                                                                            <dx:ListEditItem Text="BROKERAGE" Value="B" />
                                                                                        </Items>
                                                                                    </dx:ASPxComboBox>


                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>



                                                                        <dx:LayoutItem Caption="IsActive">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server" SupportsDisabledAttribute="True">

                                                                                    <dx:ASPxCheckBox ID="IsActive" runat="server" Value='<%# Eval("IsActive")%>'>
                                                                                    </dx:ASPxCheckBox>

                                                                                </dx:LayoutItemNestedControlContainer>
                                                                            </LayoutItemNestedControlCollection>
                                                                        </dx:LayoutItem>


                                                                        <dx:LayoutItem ShowCaption="False">
                                                                            <LayoutItemNestedControlCollection>
                                                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server">

                                                                                    <table style="float: left">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <dx:ASPxButton ID="b1" runat="server" UseSubmitBehavior="false" CausesValidation="true" AutoPostBack="false"
                                                                                                    Text="Save">
                                                                                                    <ClientSideEvents Click="function(s){
                                                                                if(ASPxClientEdit.ValidateGroup('frmEditAgentRisk')) 
                                                                                {
                                                                                        gridCommOutRequest.UpdateEdit();              
                                                                                }          
                                                                            }                                                                    
                                                                        " />
                                                                                                </dx:ASPxButton>
                                                                                            </td>
                                                                                            <td>&nbsp;</td>
                                                                                            <td>
                                                                                                <dx:ASPxButton ID="b2" runat="server" UseSubmitBehavior="false" AutoPostBack="false"
                                                                                                    Text="Cancel">
                                                                                                    <ClientSideEvents Click="function(s){
                                                                        gridCommOutRequest.CancelEdit();
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
                        </dx:ASPxFormLayout>



                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <ClientSideEvents Shown="popup_ShownEditAmend" />
</dx:ASPxPopupControl>

<dx:ASPxPopupControl ID="popupMoreInfo"
    ClientInstanceName="popupMoreInfo"
    runat="server" AllowDragging="true"
    PopupHorizontalAlign="LeftSides"
    PopupVerticalAlign="WindowCenter"
    ShowPageScrollbarWhenModal="true"
    Modal="true"
    CloseAction="CloseButton" ShowMaximizeButton="true" ScrollBars="Auto"
    HeaderText="Amend Class Of Risk">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
            <dx:ASPxCallbackPanel ID="callbackPanel_MoreInfo" ClientInstanceName="callbackPanel_MoreInfo" runat="server"
                RenderMode="Table">
                <ClientSideEvents CallbackError="function(s,e){LoadingPanel.Hide();}" />
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent3" runat="server">


                        <dx:ASPxFormLayout runat="server" ID="ASPxFormLayout2" AlignItemCaptionsInAllGroups="True" Width="100%" SettingsItemCaptions-HorizontalAlign="Right">
                            <Items>

                                <dx:LayoutGroup Caption="Class Of Risk" GroupBoxDecoration="HeadingLine" ColCount="2">
                                    <Items>
                                        <dx:LayoutItem Caption="Risk">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxLabel runat="server" ID="ASPxLabel1"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Description">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxLabel runat="server" ID="ASPxLabel2"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="Department">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxLabel runat="server" ID="ASPxLabel3"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="InsuranceLine">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxLabel runat="server" ID="ASPxLabel4"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="RiskShortDesc">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxLabel runat="server" ID="ASPxLabel5"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="Risk I">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxLabel runat="server" ID="ASPxLabel6"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="Risk II">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxLabel runat="server" ID="ASPxLabel7"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="Risk Government">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxLabel runat="server" ID="ASPxLabel8"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="Create By">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxLabel runat="server" ID="ASPxLabel9"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Create Date">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxLabel runat="server" ID="ASPxLabel10"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Request By">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxLabel runat="server" ID="ASPxLabel11"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Request Date">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxLabel runat="server" ID="ASPxLabel12"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="Approve By">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxLabel runat="server" ID="ASPxLabel13"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Approve Date">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxLabel runat="server" ID="ASPxLabel14"></dx:ASPxLabel>
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

                                                    <dx:ASPxGridView ID="ASPxGridView1" runat="server" DataSourceID="SqlDataSource_CommInRequest"
                                                        KeyFieldName="ID" AutoGenerateColumns="False" Width="100%" SettingsBehavior-AutoExpandAllGroups="true">
                                                        <SettingsPager Mode="ShowPager">
                                                            <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                                                        </SettingsPager>
                                                        <SettingsBehavior EnableRowHotTrack="true" ConfirmDelete="true" />
                                                        <SettingsSearchPanel Visible="true" />
                                                        <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
                                                       <%-- <SettingsCommandButton>
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

                                                        <SettingsDetail ShowDetailRow="true" />
                                                        <Columns>

                                                            <dx:GridViewDataColumn FieldName="Underwriter" Caption="UCode" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="AccountContact" Caption="Name" CellStyle-Wrap="False"></dx:GridViewDataColumn>

                                                            <dx:GridViewDataColumn FieldName="Brokerage_display" Caption="Brokerage" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="ORCommissionPercent_display" Caption="SF" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="AdminFeeIn1_display" Caption="Admin1" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="AdminFeeIn2_display" Caption="Admin2" CellStyle-Wrap="False"></dx:GridViewDataColumn>

                                                            <dx:GridViewDataColumn FieldName="OffsetORFlag" Caption="UpFront" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="Remark" CellStyle-Wrap="False"></dx:GridViewDataColumn>




                                                            <%--  <dx:GridViewDataColumn FieldName="CreationDate" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="CreationBy" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="ApproveDate" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="ApproveBy" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataColumn>--%>
                                                            <dx:GridViewDataColumn FieldName="IsActive" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="status" Visible="false" Caption="Status" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                                                                <EditFormSettings Visible="False" />
                                                                <DataItemTemplate>
                                                                    <img src='images/<%# Eval("status").ToString().Trim()%>.gif' />
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataColumn>

                                                            <%--                                                            <dx:GridViewDataTextColumn FieldName="RequestDate" CellStyle-Wrap="False">
                                                                <EditFormSettings Visible="False" />
                                                            </dx:GridViewDataTextColumn>

                                                            <dx:GridViewDataColumn FieldName="RequestBy" CellStyle-Wrap="False">
                                                                <EditFormSettings Visible="False" />
                                                            </dx:GridViewDataColumn>--%>
                                                        </Columns>

                                                        <Templates>
                                                            <DetailRow>
                                                                Original:
                                                                <dx:ASPxGridView ID="detailGrid_CommIn" runat="server" DataSourceID="SqlDataSource_CommIn" KeyFieldName="Underwriter"
                                                                    Width="300" OnBeforePerformDataSelect="detailGrid_CommIn_DataSelect">
                                                                    <Columns>

                                                                        <%--                                                                        <dx:GridViewDataColumn FieldName="Underwriter" Caption="UCode" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                                        <dx:GridViewDataColumn FieldName="AccountContact" Caption="Name" CellStyle-Wrap="False"></dx:GridViewDataColumn>--%>
                                                                        <dx:GridViewDataColumn FieldName="Brokerage_display" Caption="Brokerage" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                                        <dx:GridViewDataColumn FieldName="ORCommissionPercent_display" Caption="SF" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                                        <dx:GridViewDataColumn FieldName="AdminFeeIn1_display" Caption="Admin1" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                                        <dx:GridViewDataColumn FieldName="AdminFeeIn2_display" Caption="Admin2" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                                        <dx:GridViewDataColumn FieldName="Remark" CellStyle-Wrap="False"></dx:GridViewDataColumn>

                                                                    </Columns>
                                                                </dx:ASPxGridView>
                                                            </DetailRow>

                                                        </Templates>


                                                    </dx:ASPxGridView>


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

                                                    <dx:ASPxGridView ID="ASPxGridView2" DataSourceID="SqlDataSource_CommOutRequest"
                                                        runat="server"
                                                        KeyFieldName="ID">

                                                        <SettingsPager Mode="ShowPager">
                                                            <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                                                        </SettingsPager>
                                                        <SettingsBehavior EnableRowHotTrack="true" ConfirmDelete="true" />
                                                        <SettingsSearchPanel Visible="true" />
                                                        <Settings ShowHeaderFilterButton="true" />
                                                        <%--<SettingsCommandButton>
                                                            <UpdateButton ButtonType="Button" Text="บันทึก"></UpdateButton>
                                                            <EditButton ButtonType="Button" Text="แก้ไข"></EditButton>
                                                            <DeleteButton ButtonType="Button" Text="ลบ"></DeleteButton>
                                                            <CancelButton ButtonType="Button" Text="ยกเลิก"></CancelButton>
                                                        </SettingsCommandButton>--%>
                                                        <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />

                                                        <SettingsPopup>
                                                            <EditForm Modal="true" MinWidth="100" Width="400" HorizontalAlign="WindowCenter" VerticalAlign="Middle" />
                                                            <HeaderFilter Height="200" />
                                                        </SettingsPopup>
                                                        <SettingsDetail ShowDetailRow="true" />
                                                        <Columns>
                                                            <%-- <dx:GridViewDataTextColumn FieldName="Risk" Caption="Risk" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />--%>
                                                            <dx:GridViewDataTextColumn FieldName="Agent" Caption="Code" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="Name" Caption="Name" CellStyle-Wrap="False" />
                                                            <dx:GridViewDataTextColumn FieldName="CommissionOut_Display" Caption="CommOut" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="OROut_Display" Caption="SF" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="AdminOut1_Display" Caption="Admin1" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="AdminOut2_Display" Caption="Admin2" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <%--            <dx:GridViewDataTextColumn FieldName="CreationDate"  CellStyle-Wrap="False"/>
            <dx:GridViewDataTextColumn FieldName="CreationBy"  CellStyle-Wrap="False"/>
                                                            --%>
                                                            <%--<dx:GridViewDataTextColumn FieldName="OROutCalFrom" Caption="OROutCalFrom"  CellStyle-Wrap="False"/>--%>





                                                            <dx:GridViewDataTextColumn FieldName="AgentGroup" Caption="Group" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />

                                                            <dx:GridViewDataTextColumn FieldName="Remark" Caption="Remark" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />



                                                            <%-- <dx:GridViewDataColumn FieldName="HasOverComm" Caption="OverComm" CellStyle-Wrap="False"></dx:GridViewDataColumn>--%>

                                                            <dx:GridViewDataColumn FieldName="HasOverComm" Caption="OverComm" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                                                                <EditFormSettings Visible="False" />
                                                                <DataItemTemplate>
                                                                    <img src='images/alert_<%# Eval("HasOverComm").ToString().Trim()%>.gif' style="height: 15px;" />
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="IsActive" CellStyle-Wrap="False" />
                                                            <dx:GridViewDataColumn FieldName="status" Visible="false" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                                                                <EditFormSettings Visible="False" />
                                                                <DataItemTemplate>
                                                                    <img src='images/<%# Eval("status").ToString().Trim()%>.gif' />
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataColumn>

                                                        </Columns>

                                                        <Templates>
                                                            <DetailRow>
                                                                Original:
                                                                <dx:ASPxGridView ID="detailGrid_CommOut" runat="server" DataSourceID="SqlDataSource_CommOut" KeyFieldName="Agent"
                                                                    Width="300" OnBeforePerformDataSelect="detailGrid_CommOut_DataSelect">
                                                                    <Columns>
                                                                        <dx:GridViewDataTextColumn FieldName="CommissionOut_Display" Caption="CommOut" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                                        <dx:GridViewDataTextColumn FieldName="OROut_Display" Caption="SF" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                                        <dx:GridViewDataTextColumn FieldName="AdminOut1_Display" Caption="Admin1" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                                        <dx:GridViewDataTextColumn FieldName="AdminOut2_Display" Caption="Admin2" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />

                                                                    </Columns>
                                                                </dx:ASPxGridView>

                                                            </DetailRow>
                                                        </Templates>

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
            </dx:ASPxCallbackPanel>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <ClientSideEvents Shown="popup_ShownCommOut" />
</dx:ASPxPopupControl>



<asp:SqlDataSource ID="SqlDataSource_ClassOfRisk_Amend" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select * from [V_ClassOfRisk_Amend]  "
    DeleteCommand="delete from [Register.RiskUwriter.Amend] where AmendRiskID=@AmendRiskID;
    delete from [Register.AgentRiskComm.Amend] where AmendRiskID=@AmendRiskID;
    delete from [Register.ClassOfRisk.Amend] where AmendRiskID=@AmendRiskID;">
  
    <DeleteParameters>
        <asp:Parameter Name="AmendRiskID" />
    </DeleteParameters>

</asp:SqlDataSource>


<asp:SqlDataSource ID="SqlDataSource_CommInRequest" runat="server"
    ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="SELECT * from V_RiskUwriter_Amend where AmendRiskID=@AmendRiskID"
    UpdateCommand="update [Register.RiskUwriter.Amend] 
                                set   Brokerage=@Brokerage
                                ,BrokerageCal=@BrokerageCal
                                ,ORCommissionPercent=@ORCommissionPercent
                                ,ORInCal=@ORInCal
                                ,AdminFeeIn1=@AdminFeeIn1
                                ,AdminFeeIn1Cal=@AdminFeeIn1Cal
                                ,AdminFeeIn2=@AdminFeeIn2
                                ,AdminFeeIn2Cal=@AdminFeeIn2Cal
                                ,ORCalFrom=@ORCalFrom
                                ,OffsetORFlag=@OffsetORFlag
                                ,OffsetAdm1Flag=@OffsetAdm1Flag
                                ,OffsetAdm2Flag=@OffsetAdm2Flag
                                ,Remark=@Remark
                                ,IsActive=@IsActive
                                ,ModifyBy=@UserName
                                ,ModifyDate=getdate()
                                where ID=@ID">


    <SelectParameters>
        <asp:SessionParameter Name="AmendRiskID" SessionField="AmendRiskID" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="ID" />
        <asp:Parameter Name="Brokerage" />
        <asp:Parameter Name="BrokerageCal" />
        <asp:Parameter Name="ORCommissionPercent" />
        <asp:Parameter Name="ORInCal" />
        <asp:Parameter Name="AdminFeeIn1" />
        <asp:Parameter Name="AdminFeeIn1Cal" />
        <asp:Parameter Name="AdminFeeIn2" />
        <asp:Parameter Name="AdminFeeIn2Cal" />
        <asp:Parameter Name="ORCalFrom" />
        <asp:Parameter Name="OffsetORFlag" />
        <asp:Parameter Name="OffsetAdm1Flag" />
        <asp:Parameter Name="OffsetAdm2Flag" />
        <asp:Parameter Name="Remark" />
        <asp:Parameter Name="IsActive" />
        <asp:Parameter Name="UserName" />
    </UpdateParameters>

</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_CommOutRequest" runat="server"
    ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="SELECT * from V_AgentRiskComm_Amend where  AmendRiskID=@AmendRiskID"
    UpdateCommand="update [Register.AgentRiskComm.Amend] 
                set   CommissionOut=@CommissionOut
                ,CommOutCal=@CommOutCal
                ,OROut=@OROut
                ,OROutCal=@OROutCal
                ,AdminOut1=@AdminOut1
                ,AdminOut1Cal=@AdminOut1Cal
                ,AdminOut2=@AdminOut2
                ,AdminOut2Cal=@AdminOut2Cal
                ,OROutCalFrom=@OROutCalFrom 
                ,IsActive=@IsActive
                ,ModifyBy=@UserName
                ,ModifyDate=getdate()
                where ID=@ID">
    <SelectParameters>
        <asp:SessionParameter Name="AmendRiskID" SessionField="AmendRiskID" />
    </SelectParameters>

    <UpdateParameters>
        <asp:Parameter Name="ID" />
        <asp:Parameter Name="CommissionOut" />
        <asp:Parameter Name="CommOutCal" />
        <asp:Parameter Name="OROut" />
        <asp:Parameter Name="OROutCal" />
        <asp:Parameter Name="AdminOut1" />
        <asp:Parameter Name="AdminOut1Cal" />
        <asp:Parameter Name="AdminOut2" />
        <asp:Parameter Name="AdminOut2Cal" />
        <asp:Parameter Name="OROutCalFrom" />
        <asp:Parameter Name="IsActive" />
        <asp:Parameter Name="UserName" />
    </UpdateParameters>

</asp:SqlDataSource>


<asp:SqlDataSource ID="SqlDataSource_CommIn" runat="server"
    ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="SELECT * from V_RiskUwriter where Risk=@Risk and Underwriter=@Underwriter ">
    <SelectParameters>
        <asp:SessionParameter Name="Risk" SessionField="Risk" />
        <asp:SessionParameter Name="Underwriter" SessionField="Underwriter" />
    </SelectParameters>
</asp:SqlDataSource>
<asp:SqlDataSource ID="SqlDataSource_CommOut" runat="server"
    ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="SELECT * from V_AgentRiskComm where Risk=@Risk and Agent=@Agent ">
    <SelectParameters>
        <asp:SessionParameter Name="Risk" SessionField="Risk" />
        <asp:SessionParameter Name="Agent" SessionField="Agent" />
    </SelectParameters>
</asp:SqlDataSource>


<%--<asp:SqlDataSource ID="SqlDataSource_CommInLog" runat="server"
    ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="SELECT * from V_RiskUwriter_Log where Risk=@Risk and Underwriter=@Underwriter ">
    <SelectParameters> 
        <asp:SessionParameter Name="Risk" SessionField="Risk" />
        <asp:SessionParameter Name="Underwriter" SessionField="Underwriter" />
    </SelectParameters>
</asp:SqlDataSource>--%>