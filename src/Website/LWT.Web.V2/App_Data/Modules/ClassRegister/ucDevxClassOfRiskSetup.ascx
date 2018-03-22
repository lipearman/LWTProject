<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxClassOfRiskSetup.ascx.vb" Inherits="Modules_ucDevxClassOfRiskSetup" %>

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

            <dx:ASPxButton ID="btnAddNewRisk" runat="server" Text="เพิ่ม Class" Image-IconID="actions_add_16x16">
                <ClientSideEvents Click="function(s, e) {           
                       popUpAddCOR.Show();    
                       ASPxClientEdit.ClearEditorsInContainerById('frmNewRisk');    
                       e.processOnServer = false; 
                      }" />
            </dx:ASPxButton>

            <dx:ASPxButton ID="btnImportRisk" runat="server" Text="นำเข้าจาก SIBIS" Image-IconID="actions_add_16x16">
                <ClientSideEvents Click="function(s, e) {           
                       popUpImportCOR.Show();        
                       e.processOnServer = false; 
                      }" />
            </dx:ASPxButton>

            <%--           <dx:ASPxButton ID="btnClassOfRiskFormat" runat="server" Text="ไฟล์รูปแบบ" Image-IconID="export_exporttoxlsx_16x16" />

            <dx:ASPxButton ID="btnImportClassOfRisk" runat="server" Text="Import New Class" UseSubmitBehavior="false" AutoPostBack="false" Image-IconID="export_exporttoxlsx_16x16">
                <ClientSideEvents Click="function(s)
                    {
                        popUpImportNewClass.Show();
                        tbxdata.SetText(''); 
                        e.processOnServer = false;
                    }
                    " />

            </dx:ASPxButton>--%>
            <br />

            <dx:ASPxGridView ID="gridCOR" ClientInstanceName="gridCOR" runat="server" DataSourceID="SqlDataSource_COR"
                KeyFieldName="Risk" AutoGenerateColumns="False" Width="100%" SettingsBehavior-AutoExpandAllGroups="true">
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
                <Settings ShowHeaderFilterButton="true" ShowGroupPanel="true" />
                <Columns>

                    <%-- <dx:GridViewCommandColumn Width="100" CellStyle-Wrap="False" ShowNewButtonInHeader="false" ShowEditButton="false" />--%>
                    <dx:GridViewDataColumn Caption="" EditFormSettings-Visible="False" Width="50" CellStyle-HorizontalAlign="Center" EditCellStyle-VerticalAlign="Bottom">
                        <DataItemTemplate>


                            <dx:ASPxButton ID="RowEditable" runat="server" Text="แก้ไข" AutoPostBack="false">
                                <ClientSideEvents Click="function (s, e) {  
                                    gridCOR.StartEditRow(s.cpVisibleIndex);
                                 }" />
                            </dx:ASPxButton>


                        </DataItemTemplate>
                    </dx:GridViewDataColumn>

                    <%--<dx:GridViewDataColumn FieldName="Risk" CellStyle-Wrap="False" ReadOnly="true">


                    </dx:GridViewDataColumn>--%>

<%--                    <dx:GridViewDataColumn FieldName="status" Caption="Status" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>--%>
                    <dx:GridViewDataColumn FieldName="status" Caption="Status" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                        <EditFormSettings Visible="False" />
                        <DataItemTemplate>
                            <img src='images/<%# Eval("status").ToString().Trim()%>.gif' />
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>
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

                    <%--                    <dx:GridViewDataColumn FieldName="RiskGroupI" Caption="RiskI" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="RiskGroupII" Caption="RiskII" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataColumn FieldName="RiskGovernment" Caption="RiskGrov" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>--%>




                    <%--                    <dx:GridViewDataColumn FieldName="CommIn" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>
                    --%>



                    <%--  <dx:GridViewDataColumn Caption="Underwriter" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                        <EditFormSettings Visible="False" />
                        <DataItemTemplate>
                            <a href="javascript:void(0);" onclick="OnMoreInfoCommIn(this, '<%# Container.KeyValue %>')">(<%# Eval("Uwriter").ToString().Trim()%>)</a>
                        </DataItemTemplate>
                        <CellStyle Wrap="False"></CellStyle>
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataColumn Caption="Agents" CellStyle-Wrap="False" CellStyle-HorizontalAlign="Center">
                        <EditFormSettings Visible="False" />
                        <DataItemTemplate>
                            <a href="javascript:void(0);" onclick="OnMoreInfoCommOut(this, '<%# Container.KeyValue %>')">(<%# Eval("CommOut").ToString().Trim()%>)</a>
                        </DataItemTemplate>
                        <CellStyle Wrap="False"></CellStyle>
                    </dx:GridViewDataColumn>--%>

                    <%--     <dx:GridViewDataColumn FieldName="Uwriter" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="UWName" Caption="UWName" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>--%>

                    <%--    <dx:GridViewDataColumn FieldName="CommOut" Caption="Agent" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>--%>






                    <dx:GridViewDataColumn FieldName="CommIn" Caption="CommIn" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="CommIn_Request" Caption="CommIn.Req" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="CommOut" Caption="CommOut" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="CommOut_Request" Caption="CommOut.Req" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>


                    <dx:GridViewDataColumn FieldName="CreationDate" Visible="false" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataColumn FieldName="CreationBy" Visible="false" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>


                    <dx:GridViewDataDateColumn FieldName="RequestDate" CellStyle-Wrap="False" PropertiesDateEdit-DisplayFormatString="{0:dd/MM/yyyy HH:mm:ss}">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataDateColumn>

                    <dx:GridViewDataColumn FieldName="RequestBy" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>


                    <dx:GridViewDataColumn FieldName="ApproveDate" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>

                    <dx:GridViewDataColumn FieldName="ApproveBy" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>




                    <dx:GridViewDataColumn FieldName="IsActive" Visible="false" CellStyle-Wrap="False"></dx:GridViewDataColumn>

                    <dx:GridViewDataColumn FieldName="IsGeneralCode" Visible="false" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>



                    <%--[Risk]
,[Description]
,[RiskGroupI]
,[RiskGroupII]
,[RiskShortDesc]
,[RiskGovernment]
,[Department]
,[RiskGroupID]
,[RiskYear]
,[EffectiveDate]
,[ExpiryDate]
,[status]
,[CreationDate]
,[CreationBy]
,[RequestDate]
,[RequestBy]
,[ApproveDate]
,[ApproveBy]
,[IsActive]
,[ModifyBy]
,[ModifyDate]
,[Remark]
,[IsGeneralCode]--%>
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
        <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
            <dx:ASPxCallbackPanel ID="callbackPanel_MoreInfo" ClientInstanceName="callbackPanel_MoreInfo" runat="server"
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


                                        <dx:LayoutItem ShowCaption="False">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>


                                                    <dx:ASPxButton ID="btnRequestCOR" ClientInstanceName="btnRequestCOR" runat="server" Text="Request - Class Of Risk">
                                                        <ClientSideEvents Click="function(s, e) {                                                                 
                                                                    LoadingPanel.Show();
                                                                    cbRequestCOR.PerformCallback(lbRisk.GetText());
                                                                    e.processOnServer = false;

                                                                    }
                                                                    " />
                                                    </dx:ASPxButton>

                                                    <dx:ASPxCallback runat="server" ID="cbRequestCOR" ClientInstanceName="cbRequestCOR">
                                                        <ClientSideEvents CallbackError="function(s,e){LoadingPanel.Hide();}"
                                                            CallbackComplete="function(s,e){
                                                                    LoadingPanel.Hide(); 
                                                                    if(e.result == 'success')
                                                                    {
                                                                         //callbackPanel_MoreInfo.PerformCallback(lbRisk.GetText());
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
                                                            <dx:GridViewDataColumn FieldName="ORCommissionPercent_display" Caption="SF" CellStyle-Wrap="False"></dx:GridViewDataColumn>
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






























                                                    <%--                                                    <dx:ASPxButton ID="btnRequestCommIn" runat="server" Text="Request - Commission In">
                                                        <ClientSideEvents Click="function(s, e) {                                                                 
                                                                    LoadingPanel.Show();
                                                                    cbRequestCommIn.PerformCallback(lbRisk.GetText());
                                                                    e.processOnServer = false;

                                                                    }
                                                                    " />

                                                    </dx:ASPxButton>



                                                    <dx:ASPxCallback runat="server" ID="cbRequestCommIn" ClientInstanceName="cbRequestCommIn">
                                                        <ClientSideEvents CallbackError="function(s,e){LoadingPanel.Hide();}"
                                                            CallbackComplete="function(s,e){
                                                                    LoadingPanel.Hide(); 
                                                                    if(e.result == 'success')
                                                                    {
                                                                         callbackPanel_MoreInfo.PerformCallback(lbRisk.GetText());
                                                                         gridCOR.Refresh();
                                                                    }                                                     
                                                                    else
                                                                    {
                                                                        alert(e.result);
                                                                    }
                                                            }" />
                                                    </dx:ASPxCallback>--%>
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

                                                    <br />
                                                    <%--<dx:ASPxButton ID="btnRequestCommOut" runat="server" Text="Request - Commission Out">
                                                        <ClientSideEvents Click="function(s, e) {                                                                 
                                                                    LoadingPanel.Show();
                                                                    cbRequestCommOut.PerformCallback(lbRisk.GetText());
                                                                    e.processOnServer = false;

                                                                    }
                                                                    " />

                                                    </dx:ASPxButton>

                                                    <dx:ASPxCallback runat="server" ID="cbRequestCommOut" ClientInstanceName="cbRequestCommOut">
                                                        <ClientSideEvents CallbackError="function(s,e){LoadingPanel.Hide();}"
                                                            CallbackComplete="function(s,e){
                                                                    LoadingPanel.Hide(); 
                                                                    if(e.result == 'success')
                                                                    {
                                                                         callbackPanel_MoreInfo.PerformCallback(lbRisk.GetText());
                                                                         gridCOR.Refresh();
                                                                    }                                                     
                                                                    else
                                                                    {
                                                                        alert(e.result);
                                                                    }
                                                            }" />
                                                    </dx:ASPxCallback>--%>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>




                                    </Items>
                                </dx:LayoutGroup>

                            </Items>
                        </dx:ASPxFormLayout>


                        <asp:SqlDataSource ID="SqlDataSource_CommIn" runat="server"
                            ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
                            SelectCommand="SELECT * from V_RiskUwriter where Risk=@Risk">
                            <SelectParameters>
                                <asp:SessionParameter Name="Risk" SessionField="Risk" />
                            </SelectParameters>
                        </asp:SqlDataSource>

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

<dx:ASPxPopupControl ID="popUpImportCOR"
    ClientInstanceName="popUpImportCOR"
    runat="server" Modal="True"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="ClassOfRisk"
    AllowDragging="true"
    EnableAnimation="true"
    CloseAction="CloseButton"
    EnableCallbackAnimation="true"
    EnableViewState="true" Width="100px">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">

            <dx:ASPxFormLayout ID="frmNewCOR" runat="server"
                AlignItemCaptionsInAllGroups="True" ColCount="1"
                RequiredMarkDisplayMode="None" ShowItemCaptionColon="False">
                <Items>

                    <dx:LayoutItem Caption="Class Of Risk">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer
                                ID="LayoutItemNestedControlContainer9"
                                runat="server"
                                SupportsDisabledAttribute="True">
                                <dx:ASPxGridLookup ID="GridCORLookup" runat="server"
                                    SelectionMode="Single"
                                    DataSourceID="SqlDataSource_SIBISCOR"
                                    ClientInstanceName="GridCORLookup"
                                    KeyFieldName="Risk"
                                    TextFormatString="{0}">
                                    <GridViewProperties>
                                        <SettingsBehavior EnableRowHotTrack="true" />
                                        <SettingsSearchPanel Visible="true" />
                                        <Settings ShowTitlePanel="true" ShowHeaderFilterButton="true" />
                                    </GridViewProperties>
                                    <ClearButton Visibility="True"></ClearButton>
                                    <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                    <CaptionSettings RequiredMarkDisplayMode="Hidden" />

                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="Risk" Width="50" Settings-AutoFilterCondition="BeginsWith" />
                                        <dx:GridViewDataColumn FieldName="Description" CellStyle-Wrap="False" Settings-AutoFilterCondition="Contains" />
                                        <dx:GridViewDataColumn FieldName="RiskGroupI" CellStyle-Wrap="False" Settings-AutoFilterCondition="Contains" />
                                        <dx:GridViewDataColumn FieldName="RiskGroupII" CellStyle-Wrap="False" Settings-AutoFilterCondition="Contains" />
                                        <dx:GridViewDataColumn FieldName="RiskShortDesc" CellStyle-Wrap="False" Settings-AutoFilterCondition="Contains" />
                                        <dx:GridViewDataColumn FieldName="RiskGovernment" CellStyle-Wrap="False" Settings-AutoFilterCondition="Contains" />
                                        <dx:GridViewDataColumn FieldName="Department" CellStyle-Wrap="False" Settings-AutoFilterCondition="Contains" />
                                    </Columns>
                                </dx:ASPxGridLookup>


                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="InsuranceLine">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer
                                runat="server"
                                SupportsDisabledAttribute="True">


                                <dx:ASPxRadioButtonList runat="server" ID="InsuranceLine" RepeatDirection="Horizontal" Border-BorderWidth="0">
                                    <Items>
                                        <dx:ListEditItem Text="Non Life" Value="2" />
                                        <dx:ListEditItem Text="Life" Value="1" />
                                    </Items>
                                    <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                    <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                </dx:ASPxRadioButtonList>

                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxButton ID="btnImportCOR" runat="server" ValidationContainerID="frmNewCOR"
                                    Text="บันทึก" Width="100">
                                    <ClientSideEvents Click="function(s, e) {
                                        if(ASPxClientEdit.AreEditorsValid()) 
                                        {                                              
                                            LoadingPanel.Show();
                                            cbImportCOR.PerformCallback('');
                                        }                                       
                                        e.processOnServer = false;
                                    }" />
                                </dx:ASPxButton>
                                <dx:ASPxCallback runat="server" ID="cbImportCOR" ClientInstanceName="cbImportCOR">
                                    <ClientSideEvents CallbackError="function(s,e){LoadingPanel.Hide();}" CallbackComplete="function(s,e){ 
                                             LoadingPanel.Hide();   
                                             if (e.result != 'success') {
                                                alert(e.result);                                                
                                             } 
                                             else
                                             {
                                                //GridCORLookup.GetValue();
                                                //gridCOR.Refresh();
                                                gridCOR.ApplySearchPanelFilter(GridCORLookup.GetValue());
                                                popUpImportCOR.Hide();
                                             }
                                         e.processOnServer = false;
                                        }" />
                                </dx:ASPxCallback>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                </Items>
            </dx:ASPxFormLayout>

        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>

<dx:ASPxPopupControl ID="popUpAddCOR"
    ClientInstanceName="popUpAddCOR"
    runat="server" Modal="True"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="New ClassOfRisk"
    AllowDragging="true"
    EnableAnimation="true" CloseAction="CloseButton"
    EnableCallbackAnimation="true"
    EnableViewState="true" Width="100px">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">

            <dx:ASPxFormLayout ID="frmNewRisk" runat="server"
                AlignItemCaptionsInAllGroups="True" ColCount="1"
                RequiredMarkDisplayMode="None" ShowItemCaptionColon="False">
                <Items>
                    <dx:LayoutItem Caption="Prefix">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer
                                ID="LayoutItemNestedControlContainer4"
                                runat="server"
                                SupportsDisabledAttribute="True">
                                <%--                               <dx:ASPxComboBox ID="newRiskGroup"
                                    runat="server"
                                    DropDownStyle="DropDownList"
                                    DataSourceID="SqlDataSource_RiskGroup"
                                    TextField="RiskShortDesc"
                                    ValueField="RiskGroupID">
                                    <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                    <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                </dx:ASPxComboBox>--%>

                                <dx:ASPxTextBox runat="server" ID="newPrefix" MaxLength="4" ClientInstanceName="newPrefix">
                                    <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                    <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                </dx:ASPxTextBox>


                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption="IsGenCode">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer
                                ID="LayoutItemNestedControlContainer12"
                                runat="server"
                                SupportsDisabledAttribute="True">
                                <dx:ASPxCheckBox runat="server" ID="newIsGeneralCode">
                                    <ClientSideEvents CheckedChanged="function(s, e) {
                                                if(s.GetChecked())
                                                {
                                                    newRiskCode.SetText('Gen Risk Code...');
                                                    newRiskCode.SetEnabled(false);
                                                }
                                                else
                                                {
                                                    newRiskCode.SetText('');
                                                    newRiskCode.SetEnabled(true);
                                                }

                                         }" />
                                </dx:ASPxCheckBox>

                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption="ClassOfRisk">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer
                                ID="LayoutItemNestedControlContainer3"
                                runat="server"
                                SupportsDisabledAttribute="True">
                                <dx:ASPxTextBox runat="server" ID="newRiskCode" MaxLength="8" ClientInstanceName="newRiskCode">
                                    <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                    <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>



                    <dx:LayoutItem Caption="Description">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer
                                ID="LayoutItemNestedControlContainer8"
                                runat="server"
                                SupportsDisabledAttribute="True">
                                <dx:ASPxTextBox runat="server" ID="newDescription" MaxLength="40" ClientInstanceName="newDescription">
                                    <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                    <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                    <dx:LayoutItem Caption="Department">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer
                                ID="LayoutItemNestedControlContainer7"
                                runat="server"
                                SupportsDisabledAttribute="True">
                                <dx:ASPxTextBox runat="server" ID="newDepartment" MaxLength="8" ClientInstanceName="newDepartment">
                                    <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                    <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                </dx:ASPxTextBox>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="InsuranceLine">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer
                                ID="LayoutItemNestedControlContainer2"
                                runat="server"
                                SupportsDisabledAttribute="True">

                                <dx:ASPxRadioButtonList runat="server" ID="newInsuranceLine" RepeatDirection="Horizontal" Border-BorderWidth="0">
                                    <Items>
                                        <dx:ListEditItem Text="Non Life" Value="2" />
                                        <dx:ListEditItem Text="Life" Value="1" />
                                    </Items>
                                    <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                    <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                </dx:ASPxRadioButtonList>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>
                    <dx:LayoutItem Caption="">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" SupportsDisabledAttribute="True">
                                <dx:ASPxButton ID="btnSaveNewRisk" runat="server" ValidationContainerID="frmNewRisk"
                                    Text="บันทึก" Width="100">
                                    <ClientSideEvents Click="function(s, e) {
                                        if(ASPxClientEdit.AreEditorsValid()) 
                                        {                                              
                                            LoadingPanel.Show();
                                            cbAddNewRisk.PerformCallback('');
                                        }                                       
                                        e.processOnServer = false;
                                    }" />
                                </dx:ASPxButton>
                                <dx:ASPxCallback runat="server" ID="cbAddNewRisk" ClientInstanceName="cbAddNewRisk">
                                    <ClientSideEvents
                                        CallbackError="function(s,e){LoadingPanel.Hide();}"
                                        CallbackComplete="function(s,e){ 
                                             LoadingPanel.Hide();   
                                             if (e.result != 'success') {
                                                alert(e.result);                                                
                                             } 
                                             else
                                             {
                                                gridCOR.ApplySearchPanelFilter(newDescription.GetText());
                                                popUpAddCOR.Hide();
                                             }
                                         e.processOnServer = false;
                                        }" />
                                </dx:ASPxCallback>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>

                </Items>
            </dx:ASPxFormLayout>

        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>

<%--<dx:ASPxPopupControl ID="popUpImportNewClass"
    ClientInstanceName="popUpImportNewClass"
    runat="server" Modal="True"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="Import New Class Of Risk" Border-BorderWidth="0"
    AllowDragging="true" Width="300"
    EnableAnimation="true" CloseAction="CloseButton"
    EnableCallbackAnimation="true"
    EnableViewState="false"
    ShowPageScrollbarWhenModal="true">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl5" runat="server">

            <dx:ASPxFormLayout ID="ASPxFormLayout1" runat="server" Border-BorderWidth="0"
                Paddings-Padding="0" AlignItemCaptionsInAllGroups="True"
                RequiredMarkDisplayMode="None" ShowItemCaptionColon="true" Width="400"
                CellStyle-Paddings-Padding="0" GroupBoxDecoration="HeadingLine">
                <Items>
                    <dx:LayoutGroup Caption="Class Of Risk">
                        <Items>
                             <dx:LayoutItem Caption="Ris kGroup">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server">

                                         <dx:ASPxComboBox ID="newImportRiskGroup"
                                            ClientInstanceName="newImportRiskGroup"
                                            Width="300"
                                            runat="server"
                                            DropDownStyle="DropDownList"
                                            DataSourceID="SqlDataSource_RiskGroup"
                                            TextField="RiskShortDesc"
                                            ValueField="RiskGroupID">
                                            <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText=" " />
                                            <CaptionSettings RequiredMarkDisplayMode="Hidden" />
                                        </dx:ASPxComboBox>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem> 

                            <dx:LayoutItem ShowCaption="False">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server">

                                        <dx:ASPxMemo runat="server" ID="tbxdata" Width="400" Height="300"
                                            ClientInstanceName="tbxdata">
                                            <ValidationSettings>
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>
                                        </dx:ASPxMemo>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem ShowCaption="False">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server">

                                        <dx:ASPxButton ID="btnImportCoverageData" runat="server"
                                            ValidationContainerID="popUpImportNewClass"
                                            Image-IconID="DiskUpload"
                                            Text="Import" Width="100">
                                            <Image IconID="export_exporttoxlsx_16x16"></Image>

                                            <ClientSideEvents Click="function(s, e) {
                                                    if(ASPxClientEdit.AreEditorsValid()) {
                                                        LoadingPanel.Show();
                                                        cbImportCoverageData.PerformCallback('');
                                                        e.processOnServer = true; 
                                                    }
                                                    else
                                                    {
                                            
                                                    }

                                                    e.processOnServer = false;
                                                }" />

                                        </dx:ASPxButton>

                                        <dx:ASPxCallback runat="server" ID="cbImportCoverageData" ClientInstanceName="cbImportCoverageData">
                                            <ClientSideEvents CallbackComplete="function(s,e){                                                                                     
                                                    LoadingPanel.Hide();  
                                                    if(e.result == 'success')
                                                    {
                                                        popUpImportNewClass.Hide();
                                                        gridCOR.ApplySearchPanelFilter(newImportRiskGroup.GetText() + ' new');
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
                </Items>
            </dx:ASPxFormLayout>





        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
--%>


<asp:SqlDataSource ID="SqlDataSource_COR" runat="server" ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select * from [V_ClassOfRisk]  "
    UpdateCommand="update [Register.ClassOfRisk] 
    set Description=@Description
    ,Department=@Department
    ,IsActive=@IsActive
    ,ModifyBy=@UserName
    ,ModifyDate=getdate() 
    where Risk=@Risk">

    <UpdateParameters>
        <asp:Parameter Name="Description" />
        <asp:Parameter Name="Department" />
        <asp:Parameter Name="IsActive" />
        <asp:Parameter Name="Risk" />
        <asp:Parameter Name="UserName" />
    </UpdateParameters>

</asp:SqlDataSource>

<%--<asp:SqlDataSource ID="SqlDataSource_RiskGroup" runat="server"
    ConnectionString="<%$ ConnectionStrings:CPSConnectionString %>"
    SelectCommand="select RiskGroupID,RiskGroupI,RiskGroupII,RiskGovernment,Prefix, RiskShortDesc + '(' + RiskGroupII + ')' as RiskShortDesc  from tblRiskGroup order by RiskShortDesc"></asp:SqlDataSource>--%>




<asp:SqlDataSource ID="SqlDataSource_SIBISCOR" runat="server" ConnectionString="<%$ ConnectionStrings:SIBISDBConnectionString %>"
    SelectCommand="SELECT * from ClassOfRisk"></asp:SqlDataSource>
