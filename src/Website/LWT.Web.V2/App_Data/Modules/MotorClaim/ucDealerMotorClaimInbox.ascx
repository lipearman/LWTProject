<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDealerMotorClaimInbox.ascx.vb" Inherits="Modules_ucDealerMotorClaimInbox" %>

<link type="text/css" rel="Stylesheet" href="Content/styles.css" />
<link type="text/css" rel="Stylesheet" href="Content/sprite.css" />

<script type="text/javascript" src="Scripts/jquery-1.4.4.js"></script>
<script type="text/javascript">
    var keyValue;
    function OnMoreInfoClick(element, key) {
        callbackPanel_resultdetails.SetContentHtml("");
        popupResultDetails.ShowAtElement(element);
        keyValue = key;
    }
    function popup_Shown(s, e) {
        callbackPanel_resultdetails.PerformCallback(keyValue);
    }
</script>


<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" ShowHeader="false" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxGridView runat="server"
                ID="MailGrid"
                ClientInstanceName="ClientMailGrid"
                Width="100%"
                KeyFieldName="TRID" Settings-ShowHeaderFilterButton="false"
                DataSourceID="SqlDataSource_gridData"
                Settings-HorizontalScrollBarMode="Visible">

                <Columns>
                    <dx:GridViewDataTextColumn FieldName="TRNo" Visible="false" CellStyle-Wrap="False"
                        Settings-AllowFilterBySearchPanel="True"
                        SettingsHeaderFilter-Mode="CheckedList" />

                    <dx:GridViewDataComboBoxColumn FieldName="Unwriter" Caption="Insurer" CellStyle-Wrap="False"
                        Settings-AllowFilterBySearchPanel="False" Width="250">
                        <PropertiesComboBox DataSourceID="SqlDataSource_uw"
                            TextField="UWName"
                            ValueField="Unwriter">
                        </PropertiesComboBox>
                        <Settings AllowHeaderFilter="True" AllowAutoFilter="False" SortMode="DisplayText" />
                        <SettingsHeaderFilter Mode="CheckedList" />
                    </dx:GridViewDataComboBoxColumn>


                    <dx:GridViewDataTextColumn FieldName="ClaimNo" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="RefNo" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />



                    <dx:GridViewDataComboBoxColumn FieldName="ClaimStatus" CellStyle-Wrap="False"
                        Settings-AllowFilterBySearchPanel="False"
                        SettingsHeaderFilter-Mode="CheckedList">
                        <PropertiesComboBox>
                            <Items>
                                <dx:ListEditItem Value="00" Text="00 - Open" />
                                <dx:ListEditItem Value="01" Text="01 - Reserve" />
                                <dx:ListEditItem Value="02" Text="02 - Payment" />
                                <dx:ListEditItem Value="99" Text="99 - Close" />
                                <dx:ListEditItem Value="98" Text="98 - ReOpen" />
                            </Items>
                        </PropertiesComboBox>

                    </dx:GridViewDataComboBoxColumn>


                    <dx:GridViewDataTextColumn FieldName="AccidentDate"
                        CellStyle-Wrap="False"
                        Settings-AllowHeaderFilter="False"
                        Settings-ShowInFilterControl="False"
                        Settings-AllowFilterBySearchPanel="False"
                        Settings-AllowAutoFilter="False"
                        >

                    </dx:GridViewDataTextColumn>



                    <dx:GridViewDataTextColumn FieldName="AccidentTime"
                        CellStyle-Wrap="False"
                        Settings-AllowFilterBySearchPanel="False"
                        Settings-AllowHeaderFilter="False" />

                    <dx:GridViewDataTextColumn FieldName="AccidentPlace" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />

                    <dx:GridViewDataTextColumn FieldName="AccidentProvince" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />



                    <dx:GridViewDataTextColumn FieldName="TempPolicy" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="PolicyNo" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />


                    <dx:GridViewDataTextColumn FieldName="EffectiveDate"
                        CellStyle-Wrap="False"
                        Settings-AllowFilterBySearchPanel="False"
                        Settings-AllowHeaderFilter="False"
                        SettingsHeaderFilter-Mode="CheckedList" />





                    <dx:GridViewDataTextColumn FieldName="IsPost" Visible="false" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="False"
                        SettingsHeaderFilter-Mode="CheckedList" />
                    <%--<dx:GridViewDataTextColumn FieldName="ResultMessage" CellStyle-Wrap="False" Settings-AllowEllipsisInText="True" />--%>



                    <dx:GridViewDataTextColumn FieldName="InsuredName" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="CarLicense" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="ChassisNo" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />

                    <dx:GridViewDataTextColumn FieldName="DealerCode" Visible="false" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="False"
                        SettingsHeaderFilter-Mode="CheckedList" />

                    <dx:GridViewDataTextColumn FieldName="ShowRoomName" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="False"
                        SettingsHeaderFilter-Mode="CheckedList" />

                    <dx:GridViewDataTextColumn FieldName="ShowRoomCode" Visible="false" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="False"
                        SettingsHeaderFilter-Mode="CheckedList" />

                    <dx:GridViewDataTextColumn FieldName="DriverName" Visible="false" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />



                    <dx:GridViewDataTextColumn FieldName="NoticeName" Visible="false" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="NoticeTel" Visible="false" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />

                    <dx:GridViewDataTextColumn FieldName="ResultMessage"
                        Caption="Result" Visible="false"
                        CellStyle-Wrap="False"
                        Settings-AllowFilterBySearchPanel="False" Width="250"
                        SettingsHeaderFilter-Mode="CheckedList" />
                    <dx:GridViewDataTextColumn FieldName="Status" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="False"
                        SettingsHeaderFilter-Mode="CheckedList" />



                    <dx:GridViewDataDateColumn FieldName="TransactionDate"
                        Caption="Trans.Date" Width="120"
                        CellStyle-Wrap="False"
                        Settings-AllowFilterBySearchPanel="False"
                        Settings-AllowHeaderFilter="False"
                        SettingsHeaderFilter-Mode="CheckedList">
                        <PropertiesDateEdit DisplayFormatString="g" />
                    </dx:GridViewDataDateColumn>




                    <dx:GridViewDataTextColumn FieldName="AccidentDate" Caption="Accident From"
                        CellStyle-Wrap="False" Visible="false"
                        Settings-AllowFilterBySearchPanel="False">
                        <Settings AutoFilterCondition="GreaterOrEqual" />
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="AccidentDate" Caption="Accident To"
                        CellStyle-Wrap="False" Visible="false"
                        Settings-AllowFilterBySearchPanel="False">
                        <Settings AutoFilterCondition="LessOrEqual" />
                    </dx:GridViewDataTextColumn>


                </Columns>

                <SettingsCustomizationDialog Enabled="true" ShowSortingPage="false" ShowGroupingPage="false" />
                <Toolbars>
                    <dx:GridViewToolbar Position="Top" ItemAlign="Left">
                        <Items>
                            <dx:GridViewToolbarItem BeginGroup="true">
                                <Template>
                                    <dx:ASPxButton ID="ASPxButton1" Border-BorderWidth="0"
                                        ClientInstanceName="bnUploadXLS"
                                        AutoPostBack="false"
                                        runat="server"
                                        Image-IconID="mail_newmail_16x16"
                                        ToolTip="นำเข้า"
                                        Text="นำเข้า">
                                        <ClientSideEvents Click="function(s,e){ 
                                  
                                  popupImportClaim.Show();
                                  }" />
                                    </dx:ASPxButton>
                                </Template>
                            </dx:GridViewToolbarItem>

                            <dx:GridViewToolbarItem BeginGroup="true">
                                <Template>
                                    <dx:ASPxButton ID="bnExportXLS"
                                        ClientInstanceName="bnExportXLS" OnClick="bnExportXLS_Click"
                                        runat="server" Border-BorderWidth="0"
                                        Image-IconID="actions_download_16x16office2013"
                                        ToolTip="Export to Excel"
                                        Text="Export">
                                    </dx:ASPxButton>

                                </Template>
                            </dx:GridViewToolbarItem>


                            <dx:GridViewToolbarItem Command="Refresh" BeginGroup="true" />


                            <dx:GridViewToolbarItem Command="ShowCustomizationDialog" BeginGroup="true" />



                        </Items>
                    </dx:GridViewToolbar>




                </Toolbars>
                <Styles>
                    <Cell Wrap="False" />
                </Styles>


                <SettingsSearchPanel Visible="true" />
                <SettingsBehavior AllowFocusedRow="true" />
                <SettingsBehavior AllowDragDrop="True"
                    AutoExpandAllGroups="True"
                    EnableRowHotTrack="True"
                    ColumnResizeMode="Control" />
            </dx:ASPxGridView>


            <dx:ASPxGridViewExporter runat="server" ID="GridExporter" GridViewID="MailGrid">
                <Styles>
                    <Default Font-Names="Tahoma" Font-Size="Medium" Wrap="False"></Default>
                </Styles>
            </dx:ASPxGridViewExporter>





        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>


<asp:SqlDataSource ID="SqlDataSource_ClaimTransaction_Data" runat="server"
    ConnectionString="<%$ ConnectionStrings:MotorClaimConnectionString %>"
    SelectCommand="SELECT * FROM tblClaimTransaction_Data  "></asp:SqlDataSource>


<dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="ClientLoadingPanel" Modal="true" />
<dx:ASPxHiddenField ID="HiddenField" runat="server" ClientInstanceName="ClientHiddenField" />



<asp:SqlDataSource ID="SqlDataSource_uw" runat="server" ConnectionString="<%$ ConnectionStrings:MotorClaimConnectionString %>"
    SelectCommand="select rtrim(UWCode) as Unwriter, rtrim(UWCode) + ' - ' + rtrim(UWName) as UWName from Running"></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_Showroom" runat="server" ConnectionString="<%$ ConnectionStrings:MotorClaimConnectionString %>"
    SelectCommand="SELECT ShowroomID
      ,ShowRoomCode + ' - ' + ShowRoomName + ' สาขา ' + BranchName as ShowRoomName
  FROM tblShowRoom 
  where ShowroomID is not null
  order by ShowRoomName, BranchName"></asp:SqlDataSource>



<dx:ASPxCallback runat="server" ID="cbSave" ClientInstanceName="cbSave">
    <ClientSideEvents
        CallbackError="function(s,e){LoadingPanel.Hide(); }"
        CallbackComplete="function(s,e){ 
                LoadingPanel.Hide();   
                if (e.result.indexOf('success') == -1 ) {
                    alert(e.result);                                                
                }   
                else
                {
                    ClientMailGrid.SetVisible(true)
                    DemoState = 'MailList';
                    ChangeDemoState('MailList', 'new', '');

                    ClientMailGrid.Refresh();
                     alert(e.result);
                }                                                                  
                e.processOnServer = false;
         
        }" />
</dx:ASPxCallback>



<asp:SqlDataSource ID="SqlDataSource_Notice" runat="server" ConnectionString="<%$ ConnectionStrings:MotorClaimConnectionString %>"
    SelectCommand="select * from V_ClaimTransactionData_Notice "></asp:SqlDataSource>

<dx:ASPxGridViewExporter runat="server" ID="ASPxGridViewExporterNotice" GridViewID="ASPxGridViewNotice">
    <Styles>
        <Default Font-Names="Tahoma" Font-Size="Medium" Wrap="False"></Default>
    </Styles>
</dx:ASPxGridViewExporter>



<dx:ASPxCallback runat="server" ID="cbSendNotice" ClientInstanceName="cbSendNotice">
    <ClientSideEvents
        CallbackError="function(s,e){LoadingPanel.Hide(); }"
        CallbackComplete="function(s,e){ 
                LoadingPanel.Hide();   
                if (e.result.indexOf('success') == -1 {
                   
                    alert(e.result);                                                
                }   
                else
                {
                    popupNotice.Hide();
                    ASPxGridViewNotice.Refresh();
                    ClientMailGrid.Refresh();    
                    alert(e.result);
                }                                                                  
                e.processOnServer = false;
        }" />
</dx:ASPxCallback>





<dx:ASPxPopupControl ID="popupResult" runat="server" ClientInstanceName="popupResult"
    Modal="True"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="ผลการ upload"
    ScrollBars="Both"
    AllowDragging="true"
    EnableAnimation="true"
    CloseAction="CloseButton"
    EnableCallbackAnimation="true"
    EnableViewState="true"
    ShowPageScrollbarWhenModal="true"
    Width="900"
    Height="550">

    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">

            <dx:ASPxGridView runat="server" ID="ASPxGridViewResult"
                ClientInstanceName="ASPxGridViewResult"
                SettingsLoadingPanel-Mode="ShowAsPopup"
                SettingsBehavior-AutoExpandAllGroups="true"
                DataSourceID="ObjectDataSource1"
                Width="100%"
                KeyFieldName="RunNo"
                Settings-HorizontalScrollBarMode="Visible">

                <Columns>
                    <dx:GridViewDataTextColumn FieldName="Status" GroupIndex="0" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />

                    <dx:GridViewDataTextColumn FieldName="RunNo" Width="50" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />

                    <dx:GridViewDataTextColumn FieldName="RefNo" Width="150" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataColumn Caption="Result">
                        <DataItemTemplate>
                            <a href="javascript:void(0);" onclick="OnMoreInfoClick(this, '<%# Container.KeyValue %>')">More Info...</a>
                        </DataItemTemplate>
                    </dx:GridViewDataColumn>


                    <dx:GridViewDataComboBoxColumn FieldName="ClaimStatus" CellStyle-Wrap="False">
                        <PropertiesComboBox>
                            <Items>
                                <dx:ListEditItem Value="00" Text="00 - Open" />
                                <dx:ListEditItem Value="01" Text="01 - Reserve" />
                                <dx:ListEditItem Value="02" Text="02 - Payment" />
                                <dx:ListEditItem Value="99" Text="99 - Close" />
                                <dx:ListEditItem Value="98" Text="98 - ReOpen" />
                            </Items>
                        </PropertiesComboBox>
                    </dx:GridViewDataComboBoxColumn>

                    <dx:GridViewDataTextColumn FieldName="TempPolicy" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />

                    <dx:GridViewDataTextColumn FieldName="Version" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="PolicyNo" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="PolicyYear" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="ClaimNo" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="TransactionDate" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="Unwriter" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="InsuredName" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="EffectiveDate" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="ExpiryDate" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="Beneficiary" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="CarBrand" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="CarModel" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="CarLicense" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="CarYear" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="ChassisNo" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="ShowRoomName" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="ShowRoomCode" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="ClaimNoticeDate" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="ClaimNoticeTime" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="ClaimDetails" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="ClaimType" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="ClaimResult" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="ClaimDamageDetails" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="CallCenter" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="AccidentDate" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="AccidentTime" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="AccidentPlace" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="AccidentTumbon" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="AccidentAmphur" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="AccidentProvince" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="AccidentZipcode" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="DriverName" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="DriverTel" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="NoticeName" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="NoticeTel" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="GarageType" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="GarageCode" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="GarageName" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="GaragePlace" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="GarageTumbon" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="GarageAmphur" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="GarageProvince" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="GarageZipcode" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="CarRepairDate" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="CarReceiveDate" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="ConsentFormNo" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="PartsDealerName" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="PaymentDetails" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="Amount1" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="Amount2" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="Amount3" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="Remark" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />

                </Columns>
                <GroupSummary>
                    <dx:ASPxSummaryItem FieldName="Status" SummaryType="Count" />
                </GroupSummary>


                <SettingsSearchPanel Visible="true" />

                <SettingsBehavior AllowDragDrop="True"
                    AutoExpandAllGroups="True" AllowFocusedRow="True"
                    EnableRowHotTrack="True"
                    ColumnResizeMode="Control" />



                <SettingsPager Mode="ShowPager" PageSize="15">
                    <PageSizeItemSettings Visible="false" Items="15, 30, 45" ShowAllItem="false" />
                </SettingsPager>


                <Settings ShowGroupPanel="True" />
                <SettingsLoadingPanel Mode="ShowOnStatusBar" />
                <SettingsBehavior EnableCustomizationWindow="true" />
                <ClientSideEvents CustomizationWindowCloseUp="grid_CustomizationWindowCloseUp" />


            </dx:ASPxGridView>

        </dx:PopupControlContentControl>
    </ContentCollection>

</dx:ASPxPopupControl>


<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="MotorClaimUploadResult" TypeName="MotorClaim.DataClasses_MotorClaimDataExt">
    <SelectParameters>
        <asp:SessionParameter Name="MCGUID" SessionField="MCGUID" />
        <asp:SessionParameter Name="MCTYPE" SessionField="MCTYPE" />
    </SelectParameters>
</asp:ObjectDataSource>

<asp:SqlDataSource ID="SqlDataSource_gridData" runat="server" ConnectionString="<%$ ConnectionStrings:MotorClaimConnectionString %>"
    SelectCommand="select * from V_ClaimTransactionData where RankNo=1"></asp:SqlDataSource>


<%--<dx:LinqServerModeDataSource runat="server" ID="SqlDataSource_gridData"   ContextTypeName="MotorClaim.DataClasses_MotorClaimDataExt" TableName="V_ClaimTransactionDatas"  />--%>

<dx:ASPxPopupControl ID="popupResultDetails" ClientInstanceName="popupResultDetails" runat="server" AllowDragging="true"
    PopupHorizontalAlign="OutsideRight" HeaderText="Result">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
            <dx:ASPxCallbackPanel ID="callbackPanel_resultdetails"
                ClientInstanceName="callbackPanel_resultdetails" runat="server"
                Width="320px" Height="100px"
                RenderMode="Table">
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent1" runat="server">
                        <table class="InfoTable">
                            <tr>
                                <td></td>
                                <td>
                                    <asp:Literal ID="litText" runat="server" Text=""></asp:Literal>
                                </td>
                            </tr>
                        </table>
                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxCallbackPanel>
        </dx:PopupControlContentControl>
    </ContentCollection>
    <ClientSideEvents Shown="popup_Shown" />
</dx:ASPxPopupControl>



<dx:ASPxPopupControl ID="popupDetails" ClientInstanceName="popupDetails"
    runat="server"
    Modal="True"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="Details"
    AllowDragging="true"
    AllowResize="True"
    DragElement="Window"
    EnableAnimation="true"
    CloseAction="CloseButton"
    EnableCallbackAnimation="true"
    EnableViewState="true"
    ShowPageScrollbarWhenModal="true"
    ScrollBars="Auto"
    ShowMaximizeButton="true"
    Width="800"
    Height="680"
    FooterText=""
    ShowFooter="true">

    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">


            <dx:ASPxCallbackPanel ID="MailPreviewPanel" runat="server" RenderMode="Div" CssClass="MailPreviewPanel" ClientInstanceName="ClientMailPreviewPanel">
                <PanelCollection>

                    <dx:PanelContent>
                        <dx:ASPxFormLayout ID="formPreview" runat="server" Width="100%" AlignItemCaptionsInAllGroups="True">
                            <Styles>
                                <Disabled ForeColor="Black"></Disabled>

                            </Styles>
                            <Items>
                                <dx:LayoutGroup Caption="Result" ColCount="2">

                                    <Items>
                                        <dx:LayoutItem Caption="TRID" FieldName="TRID">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer51" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel runat="server" ID="TRID" ClientInstanceName="TRID">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxLabel>


                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="TRNo" FieldName="TRNo">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer65" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel runat="server" ID="TRNo">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxLabel>


                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem Caption="SubmitDate" FieldName="SubmitDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer66" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel runat="server" ID="SubmitDate">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem Caption="Status" FieldName="Status">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer67" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxLabel runat="server" ID="Status">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:EmptyLayoutItem></dx:EmptyLayoutItem>
                                        <dx:LayoutItem ShowCaption="False" ColSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer64" runat="server" SupportsDisabledAttribute="True">



                                                    <dx:ASPxGridView runat="server" ID="ASPxGridPreview_Result" SettingsBehavior-AllowSort="false" Width="100%" KeyFieldName="ID"
                                                        Border-BorderWidth="0">
                                                        <Columns>
                                                            <dx:GridViewDataTextColumn FieldName="ResultNo" Caption="Reference No" Width="15%" />
                                                            <dx:GridViewDataTextColumn FieldName="ResultCode" Caption="Error Field" Width="15%" />
                                                            <dx:GridViewDataTextColumn FieldName="ResultMessage" Caption="ข้อความ" Width="80%" />
                                                        </Columns>
                                                        <SettingsPager Mode="ShowAllRecords" />

                                                        <SettingsBehavior AllowDragDrop="false"
                                                            AutoExpandAllGroups="true"
                                                            EnableRowHotTrack="True"
                                                            ColumnResizeMode="NextColumn" />




                                                        <Styles>
                                                            <Row Cursor="pointer" />
                                                            <Disabled ForeColor="Black"></Disabled>
                                                        </Styles>

                                                    </dx:ASPxGridView>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>



















                                    </Items>
                                </dx:LayoutGroup>
                                <%--<dx:EmptyLayoutItem />--%>
                                <dx:LayoutGroup Caption="Data" ColCount="2">
                                    <Items>


                                        <dx:LayoutItem Caption="ClaimStatus" FieldName="ClaimStatus">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" SupportsDisabledAttribute="True">
                                                    <%--<dx:ASPxLabel runat="server" ID="ASPxLabel1"></dx:ASPxLabel>--%>

                                                    <dx:ASPxComboBox runat="server" DropDownStyle="DropDown" ID="ClaimStatus" ValidationSettings-RequiredField-IsRequired="true">
                                                        <Items>
                                                            <dx:ListEditItem Value="00" Text="00 - Open" />
                                                            <dx:ListEditItem Value="01" Text="01 - Reserve" />
                                                            <dx:ListEditItem Value="02" Text="02 - Payment" />
                                                            <dx:ListEditItem Value="99" Text="99 - Close" />
                                                            <dx:ListEditItem Value="98" Text="98 - ReOpen" />
                                                        </Items>
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxComboBox>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="TempPolicy" FieldName="TempPolicy">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="TempPolicy" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>



                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="RefNo" FieldName="RefNo">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="RefNo" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Version" FieldName="Version">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxSpinEdit runat="server" ID="Version" NumberType="Integer" Number="0" MinValue="0" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxSpinEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="PolicyNo" FieldName="PolicyNo">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="PolicyNo" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="PolicyYear" FieldName="PolicyYear">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxSpinEdit runat="server" ID="PolicyYear" NumberType="Integer" Number="0" MinValue="0" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxSpinEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="ClaimNo" FieldName="ClaimNo">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="ClaimNo" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="TransactionDate" FieldName="TransactionDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="TransactionDate" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Unwriter" FieldName="Unwriter">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxComboBox runat="server" DropDownStyle="DropDown" ID="Unwriter" TextField="UWName" Width="100%"
                                                        ValueField="Unwriter" DataSourceID="SqlDataSource_uw" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxComboBox>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="InsuredName" FieldName="InsuredName">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="InsuredName" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="EffectiveDate" FieldName="EffectiveDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="EffectiveDate" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="ExpiryDate" FieldName="ExpiryDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="ExpiryDate" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Beneficiary" FieldName="Beneficiary">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="Beneficiary" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="CarBrand" FieldName="CarBrand">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="CarBrand" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="CarModel" FieldName="CarModel">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="CarModel" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="CarLicense" FieldName="CarLicense">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer17" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="CarLicense" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="CarYear" FieldName="CarYear">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer18" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="CarYear" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="ChassisNo" FieldName="ChassisNo">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer19" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="ChassisNo" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="ShowRoomName" FieldName="ShowRoomName">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer20" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="ShowRoomName" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="ShowRoomCode" FieldName="ShowRoomCode">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer21" runat="server" SupportsDisabledAttribute="True">


                                                    <dx:ASPxComboBox runat="server" DropDownStyle="DropDown" ID="ShowRoomCode" Width="100%"
                                                        TextField="ShowroomName"
                                                        ValueField="ShowroomID"
                                                        DataSourceID="SqlDataSource_Showroom" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="ClaimNoticeDate" FieldName="ClaimNoticeDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer22" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="ClaimNoticeDate" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="ClaimNoticeTime" FieldName="ClaimNoticeTime">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="ClaimNoticeTime" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="ClaimDetails" FieldName="ClaimDetails">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer23" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="ClaimDetails" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="ClaimType" FieldName="ClaimType">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer24" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxComboBox runat="server" DropDownStyle="DropDown" ID="ClaimType" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                        <Items>
                                                            <dx:ListEditItem Value="0" Text="ไม่ระบุ" />
                                                            <dx:ListEditItem Value="1" Text="1 - เคลมสด" />
                                                            <dx:ListEditItem Value="2" Text="2 - แห้ง" />
                                                        </Items>
                                                    </dx:ASPxComboBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="ClaimResult" FieldName="ClaimResult">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer25" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxComboBox runat="server" DropDownStyle="DropDown" ID="ClaimResult" ValidationSettings-RequiredField-IsRequired="true">
                                                        <Items>
                                                            <dx:ListEditItem Value="0" Text="ไม่ระบุ" />
                                                            <dx:ListEditItem Value="1" Text="1 - ถูก" />
                                                            <dx:ListEditItem Value="2" Text="2 - ผิด" />
                                                            <dx:ListEditItem Value="3" Text="3 - ประมาทร่วม" />
                                                            <dx:ListEditItem Value="4" Text="4 - รอผลคดี" />

                                                        </Items>
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxComboBox>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="ClaimDamageDetails" FieldName="ClaimDamageDetails">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer26" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="ClaimDamageDetails" Width="100%" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="CallCenter" FieldName="CallCenter">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer29" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="CallCenter" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="AccidentDate" FieldName="AccidentDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer30" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="AccidentDate" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="AccidentTime" FieldName="AccidentTime">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer27" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="AccidentTime" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="AccidentPlace" FieldName="AccidentPlace">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer31" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="AccidentPlace" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="AccidentTumbon" FieldName="AccidentTumbon">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer32" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="AccidentTumbon" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="AccidentAmphur" FieldName="AccidentAmphur">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer33" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="AccidentAmphur" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="AccidentProvince" FieldName="AccidentProvince">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer34" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="AccidentProvince" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="AccidentZipcode" FieldName="AccidentZipcode">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer35" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="AccidentZipcode" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="DriverName" FieldName="DriverName">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer36" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="DriverName" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="DriverTel" FieldName="DriverTel">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer37" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="DriverTel" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="NoticeName" FieldName="NoticeName">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer38" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="NoticeName" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="NoticeTel" FieldName="NoticeTel">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer39" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="NoticeTel" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="GarageType" FieldName="GarageType">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer40" runat="server" SupportsDisabledAttribute="True">


                                                    <dx:ASPxComboBox runat="server" DropDownStyle="DropDown" ID="GarageType" ValidationSettings-RequiredField-IsRequired="true">
                                                        <Items>
                                                            <dx:ListEditItem Value="0" Text="ไม่ระบุ" />
                                                            <dx:ListEditItem Value="1" Text="1 - BP Shop" />
                                                            <dx:ListEditItem Value="2" Text="2 - Authorized" />
                                                            <dx:ListEditItem Value="3" Text="3 - Insurance" />
                                                            <dx:ListEditItem Value="4" Text="4 - Other" />
                                                        </Items>
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxComboBox>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="GarageCode" FieldName="GarageCode">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer41" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="GarageCode" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="GarageName" FieldName="GarageName">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer42" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="GarageName" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="GaragePlace" FieldName="GaragePlace">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer43" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="GaragePlace" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="GarageTumbon" FieldName="GarageTumbon">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer44" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="GarageTumbon" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="GarageAmphur" FieldName="GarageAmphur">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer45" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="GarageAmphur" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="GarageProvince" FieldName="GarageProvince">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer46" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="GarageProvince" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="GarageZipcode" FieldName="GarageZipcode">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer47" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="GarageZipcode" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="ConsentFormNo" FieldName="ConsentFormNo">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer50" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="ConsentFormNo" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="CarRepairDate" FieldName="CarRepairDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer48" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="CarRepairDate" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="CarReceiveDate" FieldName="CarReceiveDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer49" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="CarReceiveDate" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="PartsDealerName" FieldName="PartsDealerName">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer54" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="PartsDealerName" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="PaymentDetails" FieldName="PaymentDetails">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer56" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="PaymentDetails" Width="100%" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="Amount1" FieldName="Amount1">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer58" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxSpinEdit runat="server" ID="Amount1" NumberType="Float" Number="0" MinValue="0" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxSpinEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Amount2" FieldName="Amount2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer59" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxSpinEdit runat="server" ID="Amount2" NumberType="Float" Number="0" MinValue="0" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxSpinEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Amount3" FieldName="Amount3">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer60" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxSpinEdit runat="server" ID="Amount3" NumberType="Float" Number="0" MinValue="0" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxSpinEdit>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="Remark" FieldName="Remark">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer57" runat="server" SupportsDisabledAttribute="True">
                                                    <dx:ASPxTextBox runat="server" ID="Remark" ValidationSettings-RequiredField-IsRequired="true">
                                                        <DisabledStyle ForeColor="Black"></DisabledStyle>
                                                    </dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem Caption=" ">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer28" runat="server" SupportsDisabledAttribute="True">

                                                    <dx:ASPxButton ID="btnSave" AutoPostBack="false" ClientInstanceName="btnSave" runat="server" Image-IconID="export_export_16x16office2013" Text="Save">
                                                        <ClientSideEvents Click="function(s, e) {
                                                                                if(ASPxClientEdit.AreEditorsValid()) 
                                                                                {                                              
                                                                                    LoadingPanel.Show();
                                                                                    cbSave.PerformCallback(TRID.GetText());
                                                                                }
                                                                                else
                                                                                {
                                                                                    alert('กรุณากรอกข้อมูลให้ครบ');
                                                                                }
                                                                                e.processOnServer = false;
                                                                            }" />

                                                    </dx:ASPxButton>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                    </Items>
                                </dx:LayoutGroup>

                            </Items>
                        </dx:ASPxFormLayout>




                    </dx:PanelContent>
                </PanelCollection>
                <Styles>
                    <Disabled ForeColor="Black"></Disabled>
                </Styles>
            </dx:ASPxCallbackPanel>




        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
<dx:ASPxPopupControl ID="popupImportClaim" ClientInstanceName="popupImportClaim"
    runat="server"
    Modal="True"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="นำเข้า"
    AllowDragging="true"
    AllowResize="True"
    DragElement="Window"
    EnableAnimation="true"
    CloseAction="CloseButton"
    EnableCallbackAnimation="true"
    EnableViewState="true"
    ShowPageScrollbarWhenModal="true"
    ScrollBars="Auto"
    ShowMaximizeButton="true"
    Width="600"
    Height="200"
    FooterText=""
    ShowFooter="true">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">




            <dx:ASPxCallbackPanel ID="MailFormPanel" runat="server" RenderMode="Div" Height="100%" ClientInstanceName="ClientMailFormPanel">
                <PanelCollection>
                    <dx:PanelContent>


                        <dx:ASPxFormLayout ID="frmImportConsentForm" ClientInstanceName="frmImportConsentForm" runat="server">

                            <Items>
                                <dx:LayoutGroup Caption="Upload Claim Form" AlignItemCaptions="true" ColCount="2">
                                    <Items>



                                        <dx:LayoutItem Caption="กรุณาเลือกไฟล์ข้อมูล" ColSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxUploadControl ID="frmImport_UploadFile"
                                                        ClientInstanceName="frmImport_UploadFile"
                                                        runat="server"
                                                        ShowUploadButton="false"
                                                        ShowProgressPanel="true"
                                                        Width="400">
                                                        <ValidationSettings AllowedFileExtensions=".xlsx,.csv"></ValidationSettings>
                                                        <ClientSideEvents FileUploadComplete=" function(s, e) {    
                                                                                 
                                                                                if (e.callbackData.indexOf('success') == -1 ){    
                                                                                   popupResult.Show();
                                                                                    ASPxGridViewResult.Refresh();
                                                                                    alert(e.callbackData);
                                                                                }
                                                                                else
                                                                                {
                                                                                   
                                                                                  alert(e.callbackData);
                                                                                    popupImportClaim.Hide();
                                                                                    ClientMailGrid.Refresh();
                                                                                }
                                                                        }" />
                                                    </dx:ASPxUploadControl>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="" ColSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxButton ID="btnDownloadFormat" runat="server" Text="Import Format" Image-IconID="export_exporttoxlsx_16x16" CausesValidation="false" AutoPostBack="false">
                                                        <ClientSideEvents Click="function(s,e){  
                                                                                    e.processOnServer = true;
                                                                                }" />

                                                    </dx:ASPxButton>

                                                    <dx:ASPxButton runat="server" ID="frmImport_btnUPload" Image-IconID="navigation_up_16x16"
                                                        Text="Upload" AutoPostBack="false">
                                                        <ClientSideEvents Click="function(s,e)
                                                                                  {
                                                 
                                                                                  if(ASPxClientEdit.AreEditorsValid()) 
                                                                                    {                                              
                                                                                        var txt = frmImport_UploadFile.GetText(); 
                                                           
                                                                                        if (txt == '') 
                                                                                        {                                                               
                                                                                           alert('Invalid File!!!');
                                                                                        }
                                                                                        else
                                                                                        {                                            
                                                                                           frmImport_UploadFile.Upload();
                                                                                        }                                                           
                                                                                    }
                                                                                    e.processOnServer = false;
                                                                           }
                                                                           " />
                                                    </dx:ASPxButton>

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
