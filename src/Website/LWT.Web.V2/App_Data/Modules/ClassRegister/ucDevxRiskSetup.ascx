<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxRiskSetup.ascx.vb" Inherits="Modules_ucDevxRiskSetup" %>

<script>
    function OnGridFocusedRowChanged() {
        grid.GetRowValues(grid.GetFocusedRowIndex(), 'RiskGroupID;InsuranceLine', OnGetRowValues);
    }
    function OnGetRowValues(values) {
        LoadingPanel.Show();
        cbProject.PerformCallback(values[0] + '|' + values[1]);

    }

</script>
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
<dx:ASPxCallback runat="server" ID="cbProject" ClientInstanceName="cbProject">
    <ClientSideEvents
        CallbackError="function(s,e){LoadingPanel.Hide(); }"
        CallbackComplete="function(s,e){  
                
                gridCOR.Refresh();

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
                                                <dx:GridViewDataTextColumn FieldName="RiskGroupName" CellStyle-Wrap="False"  />
                                                <dx:GridViewDataTextColumn FieldName="InsuranceLine" CellStyle-Wrap="False" Visible="false"  />
                                            </Columns>


                                        </dx:ASPxGridView>

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>

                    <dx:LayoutGroup Caption="Class Of Risk" GroupBoxDecoration="HeadingLine" ColCount="2">
                        <Items>

                            <dx:LayoutItem ShowCaption="False">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer>


                                        <dx:ASPxButton ID="btnAddNewClass" runat="server" Text="Add New Class Of Risk" Image-IconID="actions_add_16x16">
                                            <ClientSideEvents Click="function(s, e) {           
                                                           popUpAddNewClass.Show(); 
                                                           gridCORLookup.Refresh();       
                                                           e.processOnServer = false; 
                                                          }" />
                                        </dx:ASPxButton>



                                        <dx:ASPxGridView ID="gridCOR" ClientInstanceName="gridCOR" runat="server" DataSourceID="SqlDataSource_COR"
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
                                                <EditForm Modal="true" Width="400" ShowHeader="true" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </SettingsPopup>
                                            <Settings ShowHeaderFilterButton="true" />
                                            <Columns>
                                                  <dx:GridViewCommandColumn Width="100" CellStyle-Wrap="False" ShowNewButtonInHeader="false" ShowEditButton="true" ShowDeleteButton="true" />

                                               
                                                <dx:GridViewDataColumn ReadOnly="true" FieldName="Risk" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center" CellStyle-Font-Bold="true">
                                                    <DataItemTemplate>
                                                        <a href="javascript:void(0);" onclick="OnMoreInfo(this, '<%# Eval("Risk").ToString().Trim()%>')"><%# Eval("Risk").ToString().Trim()%></a>
                                                    </DataItemTemplate>
                                                    <CellStyle Wrap="False"></CellStyle>
                                                </dx:GridViewDataColumn>
                                           


      <%--                                             <dx:GridViewDataColumn Caption="Logo" FieldName="Uwriter" EditFormSettings-Visible="False" Width="100" CellStyle-HorizontalAlign="Center" EditCellStyle-VerticalAlign="Bottom">
                                                        <DataItemTemplate>
                                                            <img src='images/InsurerLogo/<%# Eval("Uwriter").ToString().Trim()%>.jpg' width="30" />
                                                        </DataItemTemplate>
                                                    </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="UWName" CellStyle-Wrap="False">
                                                    <EditFormSettings Visible="False" />
                                                </dx:GridViewDataColumn>--%>

                                             <%--   <dx:GridViewDataColumn FieldName="CommIn_Display" CellStyle-Wrap="False">
                                                    <EditFormSettings Visible="False" />
                                                </dx:GridViewDataColumn>--%>

                                           <%--     <dx:GridViewDataColumn FieldName="CommOut" CellStyle-Wrap="False">
                                                    <EditFormSettings Visible="False" />
                                                </dx:GridViewDataColumn>--%>

                                                <dx:GridViewDataColumn FieldName="Description" CellStyle-Wrap="False">
                                                    <EditFormSettings Visible="False" />
                                                </dx:GridViewDataColumn>

                                               

                                                <dx:GridViewDataColumn FieldName="Department" CellStyle-Wrap="False">
                                                    <EditFormSettings Visible="False" />
                                                </dx:GridViewDataColumn>

                                                <dx:GridViewDataColumn FieldName="InsuranceLine" CellStyle-Wrap="False">
                                                    <EditFormSettings Visible="False" />
                                                </dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="RiskShortDesc" CellStyle-Wrap="False">
                                                    <EditFormSettings Visible="False" />
                                                </dx:GridViewDataColumn>
                                                 <dx:GridViewDataColumn FieldName="RiskGovernment" CellStyle-Wrap="False">
                                                    <EditFormSettings Visible="False" />
                                                </dx:GridViewDataColumn>

                                                <dx:GridViewDataColumn FieldName="IsActive" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                            <%--    <dx:GridViewDataColumn FieldName="EffectiveDate" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                <dx:GridViewDataColumn FieldName="ExpireDate" CellStyle-Wrap="False"></dx:GridViewDataColumn>
--%>






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
    SelectCommand="select RiskGroupID,RiskGroupCode,RiskGroupName 
    ,case when InsuranceLine = 1 then 'Life'
	when  InsuranceLine = 2 then 'Non Life'
	else ''
    end as InsuranceLine    
    from tblRiskGroup where RiskGroupID not in(1,2)  order by RiskGroupID  "></asp:SqlDataSource>




<asp:SqlDataSource ID="SqlDataSource_COR" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select * from V_ClassOfRisk_Publish where RiskGroupID=@RiskGroupID and InsuranceLine=@InsuranceLine "
    UpdateCommand="update tblClassOfRisk 
                     set IsActive=@IsActive
                    ,EffectiveDate=@EffectiveDate
                    ,ExpireDate=@ExpireDate
                     where ID=@ID"
    DeleteCommand="delete from tblClassOfRisk where ID=@ID"

                    >
    <SelectParameters>
        <asp:SessionParameter Name="RiskGroupID" SessionField="RiskGroupID" />
        <asp:SessionParameter Name="InsuranceLine" SessionField="InsuranceLine" />
    </SelectParameters>
    <UpdateParameters>
        <asp:Parameter Name="EffectiveDate" />
        <asp:Parameter Name="ExpireDate" />
        <asp:Parameter Name="IsActive" />
        <asp:Parameter Name="ID" /> 
    </UpdateParameters>
    <DeleteParameters>
         <asp:Parameter Name="ID" /> 
    </DeleteParameters>

</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_CORLookup" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select * from V_ClassOfRisk_NonPublish where InsuranceLine=@InsuranceLine ">
 
     <SelectParameters> 
        <asp:SessionParameter Name="InsuranceLine" SessionField="InsuranceLine" />
    </SelectParameters>
</asp:SqlDataSource>

<dx:ASPxPopupControl ID="popUpAddNewClass"
    ClientInstanceName="popUpAddNewClass"
    runat="server" Modal="True"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="Class Of Risk"
    AllowDragging="true"
    EnableAnimation="true" 
     ShowPageScrollbarWhenModal="true" ShowMaximizeButton="true" ScrollBars="Auto"
    CloseAction="CloseButton"
    EnableCallbackAnimation="true"
    EnableViewState="true" Width="800px">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
            
              <dx:ASPxButton ID="btnAddNewCOR" runat="server" Text="Add New Class Of Risk" Image-IconID="actions_add_16x16">
                    <ClientSideEvents Click="function(s, e) {
                                LoadingPanel.Show();
                                cbAddNewCOR.PerformCallback('');
                                e.processOnServer = false;
                        }" />
                </dx:ASPxButton>

              <dx:ASPxCallback runat="server" ID="cbAddNewCOR" ClientInstanceName="cbAddNewCOR">
                                                <ClientSideEvents CallbackComplete="function(s,e){                                                                                     
                                                    LoadingPanel.Hide(); 

                                                    if(e.result == 'success')
                                                    {
                                                        popUpAddNewClass.Hide();
                                                        gridCOR.Refresh();
                                                    }                                                     
                                                    else
                                                    {
                                                        alert(e.result);
                                                    }
                                           
                                                }" />

                                            </dx:ASPxCallback>
 
<dx:ASPxGridView ID="gridCORLookup" ClientInstanceName="gridCORLookup" runat="server" DataSourceID="SqlDataSource_CORLookup"
                KeyFieldName="Risk" AutoGenerateColumns="False" Width="100%" SettingsBehavior-AutoExpandAllGroups="true">
                <SettingsPager Mode="ShowPager">
                    <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                </SettingsPager>
                <SettingsBehavior EnableRowHotTrack="true" ConfirmDelete="true" />
                <SettingsSearchPanel Visible="true" /> 
                 
                <SettingsPopup>
                    <EditForm Modal="true" Width="400" ShowHeader="true" HorizontalAlign="Center" VerticalAlign="Middle" />
                </SettingsPopup>
                <Settings ShowHeaderFilterButton="true" />
                <Columns>
                     <dx:GridViewCommandColumn ShowSelectCheckbox="True" ShowClearFilterButton="true"  SelectAllCheckboxMode="Page" />

                            <dx:GridViewDataColumn ReadOnly="true" FieldName="Risk" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center" CellStyle-Font-Bold="true">
                                <DataItemTemplate>
                                    <a href="javascript:void(0);" onclick="OnMoreInfo(this, '<%# Eval("Risk").ToString().Trim()%>')"><%# Eval("Risk").ToString().Trim()%></a>
                                </DataItemTemplate>
                                <CellStyle Wrap="False"></CellStyle>
                            </dx:GridViewDataColumn>
                    
<%--                    <dx:GridViewDataColumn Caption="Logo" FieldName="Uwriter" EditFormSettings-Visible="False" Width="100" CellStyle-HorizontalAlign="Center" EditCellStyle-VerticalAlign="Bottom">
                        <DataItemTemplate>
                            <img src='images/InsurerLogo/<%# Eval("Uwriter").ToString().Trim()%>.jpg' width="30" />
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>--%>
<%--                            <dx:GridViewDataColumn FieldName="Uwriter" CellStyle-Wrap="False">
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataColumn>--%>
                    <%--        <dx:GridViewDataColumn FieldName="UWName" Caption="Insurer" CellStyle-Wrap="False">
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataColumn>--%>

<%--                            <dx:GridViewDataColumn FieldName="CommIn_Display" Caption="Comm-In" CellStyle-Wrap="False">
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataColumn>--%>

                         <%--   <dx:GridViewDataColumn FieldName="CommOut" Caption="Agents" CellStyle-Wrap="False">
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataColumn>--%>

                            <dx:GridViewDataColumn FieldName="Description" CellStyle-Wrap="False">
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataColumn>
                            <dx:GridViewDataColumn FieldName="Department" CellStyle-Wrap="False">
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataColumn>

                            <dx:GridViewDataColumn FieldName="InsuranceLine" CellStyle-Wrap="False">
                                <EditFormSettings Visible="False" />
                            </dx:GridViewDataColumn> 

                    <dx:GridViewDataColumn FieldName="RiskShortDesc" CellStyle-Wrap="False">
                                                    <EditFormSettings Visible="False" />
                                                </dx:GridViewDataColumn>
                                                 <dx:GridViewDataColumn FieldName="RiskGovernment" CellStyle-Wrap="False">
                                                    <EditFormSettings Visible="False" />
                                                </dx:GridViewDataColumn>
                </Columns>

            </dx:ASPxGridView>



        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>






 
 
 
<dx:ASPxPopupControl ID="popupMoreInfo"
    ClientInstanceName="popupMoreInfo"
    runat="server" AllowDragging="true"
    PopupHorizontalAlign="LeftSides"
    PopupVerticalAlign="WindowCenter"
    ShowPageScrollbarWhenModal="true"
    Modal="true"
    CloseAction="CloseButton" ShowMaximizeButton="true" ScrollBars="Auto"
    HeaderText="Comm Out">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
            <dx:ASPxCallbackPanel ID="callbackPanel_MoreInfo" ClientInstanceName="callbackPanel_MoreInfo" runat="server"
                RenderMode="Table">
                <ClientSideEvents CallbackError="function(s,e){LoadingPanel.Hide();}" />
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent2" runat="server">


                        <dx:ASPxFormLayout runat="server" ID="ASPxFormLayout1" AlignItemCaptionsInAllGroups="True" Width="100%" SettingsItemCaptions-HorizontalAlign="Right">
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

                                        <dx:LayoutItem Caption="Status">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <%--<dx:ASPxLabel runat="server" ID="lbStatus"></dx:ASPxLabel>--%>
                                                    <dx:ASPxImage ID="lbStatus" Height="15" runat="server"></dx:ASPxImage>

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

                                        <dx:LayoutItem Caption="Risk Government" ColSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxLabel runat="server" ID="lbRiskGovernment"></dx:ASPxLabel>
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



                                                        <Columns>

                                                            <dx:GridViewDataColumn FieldName="Underwriter" Caption="UCode" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="AccountContact" Caption="Name" CellStyle-Wrap="False"></dx:GridViewDataColumn>

                                                            <dx:GridViewDataColumn FieldName="Brokerage_display" Caption="Brokerage" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="ORCommissionPercent_display" Caption="OR" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="AdminFeeIn1_display" Caption="Admin1" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="AdminFeeIn2_display" Caption="Admin2" CellStyle-Wrap="False"></dx:GridViewDataColumn>

                                                            <dx:GridViewDataColumn FieldName="OffsetORFlag" Caption="UpFront" CellStyle-Wrap="False"></dx:GridViewDataColumn>
                                                            <dx:GridViewDataColumn FieldName="Remark" CellStyle-Wrap="False"></dx:GridViewDataColumn>

                                                            <dx:GridViewDataColumn FieldName="status" Caption="Status" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                                                                <EditFormSettings Visible="False" />
                                                                <DataItemTemplate>
                                                                    <img src='images/<%# Eval("status").ToString().Trim()%>.gif' />
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataColumn>


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




                                                        </Columns>

                                                    </dx:ASPxGridView>


                                                       <asp:SqlDataSource ID="SqlDataSource_CommIn" runat="server"
                            ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
                            SelectCommand="SELECT * from V_RiskUwriter where Risk=@Risk">
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
                                                            <dx:GridViewDataTextColumn FieldName="Risk" Caption="Risk" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="Agent" Caption="Code" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="Name" Caption="Name" CellStyle-Wrap="False" />
                                                            <dx:GridViewDataTextColumn FieldName="CommissionOut_Display" Caption="CommOut" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="OROut_Display" Caption="OR" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="AdminOut1_Display" Caption="Admin1" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataTextColumn FieldName="AdminOut2_Display" Caption="Admin2" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <%--            <dx:GridViewDataTextColumn FieldName="CreationDate"  CellStyle-Wrap="False"/>
            <dx:GridViewDataTextColumn FieldName="CreationBy"  CellStyle-Wrap="False"/>
                                                            --%>
                                                            <%--<dx:GridViewDataTextColumn FieldName="OROutCalFrom" Caption="OROutCalFrom"  CellStyle-Wrap="False"/>--%>


                                                            <dx:GridViewDataTextColumn FieldName="Remark" Caption="Remark" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />



                                                            <dx:GridViewDataTextColumn FieldName="AgentGroup" Caption="Group" CellStyle-Wrap="False" Settings-HeaderFilterMode="CheckedList" />
                                                            <dx:GridViewDataColumn FieldName="IsActive" CellStyle-Wrap="False" />



                                                            <dx:GridViewDataColumn FieldName="status" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                                                                <EditFormSettings Visible="False" />
                                                                <DataItemTemplate>
                                                                    <img src='images/<%# Eval("status").ToString().Trim()%>.gif' />
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataColumn>

                                                            <%-- <dx:GridViewDataColumn FieldName="HasOverComm" Caption="OverComm" CellStyle-Wrap="False"></dx:GridViewDataColumn>--%>

                                                            <dx:GridViewDataColumn FieldName="HasOverComm" Caption="OverComm" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                                                                <EditFormSettings Visible="False" />
                                                                <DataItemTemplate>
                                                                    <img src='images/alert_<%# Eval("HasOverComm").ToString().Trim()%>.gif' style="height: 15px;" />
                                                                </DataItemTemplate>
                                                            </dx:GridViewDataColumn>

                                                        </Columns>
                                                    </dx:ASPxGridView>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>




                                    </Items>
                                </dx:LayoutGroup>

                            </Items>
                        </dx:ASPxFormLayout>


                     

                        <asp:SqlDataSource ID="SqlDataSource_CommOut" runat="server"
                            ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
                            SelectCommand="SELECT * from V_AgentRiskComm where Risk=@Risk">
                            <SelectParameters>
                                <asp:SessionParameter Name="Risk" SessionField="Risk" />
                            </SelectParameters>
                        </asp:SqlDataSource>

                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <ClientSideEvents Shown="popup_ShownCommOut" />
</dx:ASPxPopupControl>

