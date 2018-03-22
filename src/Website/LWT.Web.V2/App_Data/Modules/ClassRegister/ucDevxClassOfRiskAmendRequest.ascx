<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxClassOfRiskAmendRequest.ascx.vb" Inherits="Modules_ucDevxClassOfRiskAmendRequest" %>


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


<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="Amend Class Of Risk" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>



            <dx:ASPxGridView ID="grid_ClassOfRisk_Amend" ClientInstanceName="grid_ClassOfRisk_Amend" runat="server" DataSourceID="SqlDataSource_ClassOfRisk_Amend"
                KeyFieldName="AmendRiskID" AutoGenerateColumns="False" Width="100%" SettingsBehavior-AutoExpandAllGroups="true">
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
                            <a href="javascript:void(0);" onclick="OnMoreInfo(this, '<%# Container.KeyValue %>')"><%# Eval("Risk").ToString().Trim()%></a>
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




                    <dx:GridViewDataColumn FieldName="status" Visible="false"  CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                        <EditFormSettings Visible="False" />
                        <DataItemTemplate>
                            <img src='images/<%# Eval("status").ToString().Trim()%>.gif' />
                        </DataItemTemplate>
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




<dx:ASPxPopupControl ID="popupMoreInfo"
    ClientInstanceName="popupMoreInfo"
    runat="server" AllowDragging="true"
    PopupHorizontalAlign="LeftSides"
    PopupVerticalAlign="WindowCenter"
    ShowPageScrollbarWhenModal="true"
    Modal="true"
    CloseAction="CloseButton" ShowMaximizeButton="true" ScrollBars="Auto"
    HeaderText="Class Of Risk">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
            <dx:ASPxCallbackPanel ID="callbackPanel_MoreInfo" ClientInstanceName="callbackPanel_MoreInfo" runat="server"
                RenderMode="Table">
                <ClientSideEvents CallbackError="function(s,e){LoadingPanel.Hide();}" />
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent3" runat="server">


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


                                        <dx:LayoutItem ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>


                                                    <dx:ASPxButton ID="btnApproveAmend" runat="server" Text="Approve">
                                                        <ClientSideEvents Click="function(s, e) {
                                                                         
                                                                        if(confirm('Confirm Approved?'))
                                                                            {                                                                                                                              
                                                                                LoadingPanel.Show();
                                                                                cbApproveAmend.PerformCallback('');
                                                                            }
                                                                         
                                                                        e.processOnServer = false;
                                                                    }
                                                                    " />
                                                    </dx:ASPxButton>
                                                    <dx:ASPxCallback runat="server" ID="cbApproveAmend" ClientInstanceName="cbApproveAmend" CausesValidation="false">
                                                        <ClientSideEvents CallbackError="function(s,e){LoadingPanel.Hide();}"
                                                            CallbackComplete="function(s,e){
                                                                    LoadingPanel.Hide(); 
                                                                    if(e.result == 'success')
                                                                    {
                                                                         popupMoreInfo.Hide();
                                                                         grid_ClassOfRisk_Amend.Refresh();
                                                                    }                                                     
                                                                    else
                                                                    {
                                                                        alert(e.result);
                                                                    }
                                                            }" />
                                                    </dx:ASPxCallback>


                                                    <dx:ASPxButton ID="btnRejectAmend" runat="server" Text="Reject">
                                                        <ClientSideEvents Click="function(s, e) {                                                                   
                                                                    if(confirm('Confirm Reject?'))
                                                                     {
                                                                                                                              
                                                                            LoadingPanel.Show();
                                                                            cbRejectAmend.PerformCallback('');
                                                                     }
                                                                    e.processOnServer = false;
                                                                    }
                                                                    " />
                                                    </dx:ASPxButton>
                                                    <dx:ASPxCallback runat="server" ID="cbRejectAmend" ClientInstanceName="cbRejectAmend" CausesValidation="false">
                                                        <ClientSideEvents CallbackError="function(s,e){LoadingPanel.Hide();}"
                                                            CallbackComplete="function(s,e){
                                                                    LoadingPanel.Hide(); 
                                                                    if(e.result == 'success')
                                                                    {
                                                                         popupMoreInfo.Hide();
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
                                                     <%--   <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1" />
                                                        <SettingsCommandButton>
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
                                                            <dx:GridViewDataColumn FieldName="ORCommissionPercent_display" Caption="OR" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="AdminFeeIn1_display" Caption="Admin1" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="AdminFeeIn2_display" Caption="Admin2" CellStyle-Wrap="False"></dx:GridViewDataColumn>

                                                            <dx:GridViewDataColumn FieldName="OffsetORFlag" Caption="UpFront" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="Remark" CellStyle-Wrap="False"></dx:GridViewDataColumn>

                                                            <dx:GridViewDataColumn FieldName="status"  Visible="false" Caption="Status" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                                                                <EditFormSettings Visible="False" />
                                                                <DataItemTemplate>
                                                                    <img src='images/<%# Eval("status").ToString().Trim()%>.gif' />
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataColumn>

                                                            <dx:GridViewDataColumn FieldName="IsActive" CellStyle-Wrap="False"></dx:GridViewDataColumn>

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
                                                                        <dx:GridViewDataColumn FieldName="ORCommissionPercent_display" Caption="OR" CellStyle-Wrap="False"></dx:GridViewDataColumn>
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
                                                            <UpdateButton ButtonType="Button"  Text="บันทึก">
                                                                
                                                            </UpdateButton>
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
                                                            <dx:GridViewDataTextColumn FieldName="Agent" Caption="Code" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="Name" Caption="Name" CellStyle-Wrap="False" />
                                                            <dx:GridViewDataTextColumn FieldName="CommissionOut_Display" Caption="CommOut" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="OROut_Display" Caption="OR" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="AdminOut1_Display" Caption="Admin1" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="AdminOut2_Display" Caption="Admin2" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />

                                                            <dx:GridViewDataTextColumn FieldName="AgentGroup" Caption="Group" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />

                                                            <dx:GridViewDataTextColumn FieldName="Remark" Caption="Remark" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />

                                                            <dx:GridViewDataColumn FieldName="status"  Visible="false" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                                                                <EditFormSettings Visible="False" />
                                                                <DataItemTemplate>
                                                                    <img src='images/<%# Eval("status").ToString().Trim()%>.gif' />
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="IsActive" CellStyle-Wrap="False" />

                                                            <dx:GridViewDataColumn FieldName="HasOverComm" Caption="OverComm" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                                                                <EditFormSettings Visible="False" />
                                                                <DataItemTemplate>
                                                                    <img src='images/alert_<%# Eval("HasOverComm").ToString().Trim()%>.gif' style="height: 15px;" />
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataColumn>

                                                        </Columns>

                                                        <Templates>
                                                              <DetailRow>Original:
                                                                <dx:ASPxGridView ID="detailGrid_CommOut" runat="server" DataSourceID="SqlDataSource_CommOut" KeyFieldName="Agent"
                                                                    Width="300" OnBeforePerformDataSelect="detailGrid_CommOut_DataSelect">
                                                                    <Columns>
                                                            <dx:GridViewDataTextColumn FieldName="CommissionOut_Display" Caption="CommOut" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="OROut_Display" Caption="OR" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
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
    SelectCommand="select * from [V_ClassOfRisk_Amend] where status='req' "
    DeleteCommand="delete from [Register.RiskUwriter.Amend] where AmendRiskID=@AmendRiskID;
    delete from [Register.AgentRiskComm.Amend] where AmendRiskID=@AmendRiskID;
    delete from [Register.ClassOfRisk.Amend] where AmendRiskID=@AmendRiskID;">
    <SelectParameters>
        <asp:Parameter Name="UserName" />
    </SelectParameters>
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

