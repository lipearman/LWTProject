<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucLWTMotorClaimInbox01.ascx.vb" Inherits="Modules_ucLWTMotorClaimInbox01" %>

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
<script type="text/javascript">
    function button1_Click(s, e) {
        if (ClientMailGrid.IsCustomizationWindowVisible())
            ClientMailGrid.HideCustomizationWindow();
        else
            ClientMailGrid.ShowCustomizationWindow();
        UpdateButtonText();
    }
    function grid_CustomizationWindowCloseUp(s, e) {
        UpdateButtonText();
    }
    function UpdateButtonText() {
        var text = ClientMailGrid.IsCustomizationWindowVisible() ? "Hide" : "Show";
        text += " Column";
        button1.SetText(text);
    }
</script>

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>


            <dx:ASPxGridView runat="server"
                ID="MailGrid" Settings-ShowPreview="true"
                PreviewFieldName="ResultMessage"
                SettingsBehavior-EnableRowHotTrack="true"
                Styles-PreviewRow-ForeColor="Red"
                SettingsBehavior-AllowEllipsisInText="true"
                SettingsLoadingPanel-Mode="ShowAsPopup"
                ClientInstanceName="ClientMailGrid"
                Width="98%"
                KeyFieldName="TRID"
                DataSourceID="SqlDataSource_gridData"
                Settings-HorizontalScrollBarMode="Visible">
                <Toolbars>
                    <dx:GridViewToolbar ItemAlign="Left">
                        <Items>

                            <dx:GridViewToolbarItem BeginGroup="true">
                                <Template>
                                    <dx:ASPxButton runat="server" ID="button1"
                                        ClientInstanceName="button1"
                                        Text="Show Column"
                                        UseSubmitBehavior="false"
                                        Image-IconID="richedit_showallfieldresults_16x16" Border-BorderWidth="0"
                                        AutoPostBack="false">
                                        <ClientSideEvents Click="button1_Click" />
                                    </dx:ASPxButton>
                                </Template>
                            </dx:GridViewToolbarItem>


                            <dx:GridViewToolbarItem BeginGroup="true">
                                <Template>
                                    <dx:ASPxButton ID="ASPxButton1"
                                        ClientInstanceName="bnUploadXLS"
                                        Border-BorderWidth="0"
                                        AutoPostBack="false"
                                        runat="server"
                                        Image-IconID="mail_newmail_16x16"
                                        ToolTip="นำเข้า" Text="นำเข้า">
                                        <ClientSideEvents Click="function(s,e){ 
                                  
                                  popupImportClaim.Show();
                                
                                      
                                }" />
                                    </dx:ASPxButton>
                                </Template>
                            </dx:GridViewToolbarItem>
                            <dx:GridViewToolbarItem BeginGroup="true">
                                <Template>
                                    <dx:ASPxButton ID="btnDownloadFormat" OnClick="bnExportXLS_Click"
                                        runat="server" Border-BorderWidth="0"
                                        Image-IconID="actions_download_16x16office2013"
                                        ToolTip="Excel for Import"
                                        Text="Export">
                                    </dx:ASPxButton>
                                </Template>
                            </dx:GridViewToolbarItem>



                            <dx:GridViewToolbarItem Command="Refresh" BeginGroup="true" />
                            <dx:GridViewToolbarItem BeginGroup="true">
                                <Template>
                                    <dx:ASPxButtonEdit ID="tbToolbarSearch" runat="server" NullText="Search..." Height="100%">
                                        <Buttons>
                                            <dx:SpinButtonExtended Image-IconID="find_find_16x16gray" />
                                        </Buttons>
                                    </dx:ASPxButtonEdit>
                                </Template>
                            </dx:GridViewToolbarItem>
                        </Items>
                    </dx:GridViewToolbar>
                </Toolbars>
                <Columns>

                    <dx:GridViewDataTextColumn FieldName="SubmitDate" Width="150" SortOrder="Descending" CellStyle-Wrap="False" Settings-AllowEllipsisInText="True" />


                    <dx:GridViewDataTextColumn FieldName="TRNo" CellStyle-Wrap="False"
                        Settings-AllowFilterBySearchPanel="True"
                        Settings-AllowHeaderFilter="False"
                        SettingsHeaderFilter-Mode="CheckedList" />

                    <%--      <dx:GridViewDataTextColumn FieldName="TRID" Visible="false" Settings-AllowSort="True" SortOrder="Descending" />
                    --%>

                    <dx:GridViewDataComboBoxColumn FieldName="Unwriter" Caption="Insurer" CellStyle-Wrap="False"
                        Settings-AllowFilterBySearchPanel="False" Width="250"
                        Settings-AllowHeaderFilter="True"
                        SettingsHeaderFilter-Mode="CheckedList">
                        <PropertiesComboBox DataSourceID="SqlDataSource_uw"
                            TextField="UWName"
                            ValueField="Unwriter">
                        </PropertiesComboBox>

                    </dx:GridViewDataComboBoxColumn>



                    <dx:GridViewDataComboBoxColumn FieldName="ClaimStatus" CellStyle-Wrap="False"
                        Settings-AllowFilterBySearchPanel="False"
                        Settings-AllowHeaderFilter="True"
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


                    <dx:GridViewDataTextColumn FieldName="TempPolicy" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="RefNo" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="PolicyNo" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />


                    <dx:GridViewDataTextColumn FieldName="EffectiveDate" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="False"
                        Settings-AllowHeaderFilter="True"
                        SettingsHeaderFilter-Mode="CheckedList" />

                    <dx:GridViewDataTextColumn FieldName="ClaimNo" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />



                    <dx:GridViewDataDateColumn FieldName="TransactionDate" Caption="Trans.Date" Width="120" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="False"
                        Settings-AllowHeaderFilter="True"
                        SettingsHeaderFilter-Mode="CheckedList">
                        <PropertiesDateEdit DisplayFormatString="g" />
                    </dx:GridViewDataDateColumn>


                    <dx:GridViewDataTextColumn FieldName="Status" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="False"
                        Settings-AllowHeaderFilter="True"
                        SettingsHeaderFilter-Mode="CheckedList" />

                    <dx:GridViewDataTextColumn FieldName="IsPost" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="False"
                        Settings-AllowHeaderFilter="True"
                        SettingsHeaderFilter-Mode="CheckedList" />
                    <%--<dx:GridViewDataTextColumn FieldName="ResultMessage" CellStyle-Wrap="False" Settings-AllowEllipsisInText="True" />--%>



                    <dx:GridViewDataTextColumn FieldName="InsuredName" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="CarLicense" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="ChassisNo" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />

                    <dx:GridViewDataTextColumn FieldName="DealerCode" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="False"
                        Settings-AllowHeaderFilter="True"
                        SettingsHeaderFilter-Mode="CheckedList" />

                    <dx:GridViewDataTextColumn FieldName="ShowRoomName" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="False"
                        Settings-AllowHeaderFilter="True"
                        SettingsHeaderFilter-Mode="CheckedList" />

                    <dx:GridViewDataTextColumn FieldName="ShowRoomCode" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="False"
                        Settings-AllowHeaderFilter="True"
                        SettingsHeaderFilter-Mode="CheckedList" />

                    <dx:GridViewDataTextColumn FieldName="DriverName" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="AccidentDate" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="False"
                        Settings-AllowHeaderFilter="True"
                        SettingsHeaderFilter-Mode="CheckedList" />
                    <dx:GridViewDataTextColumn FieldName="AccidentTime" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />


                    <dx:GridViewDataTextColumn FieldName="AccidentPlace" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />

                    <dx:GridViewDataTextColumn FieldName="AccidentProvince" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="NoticeName" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
                    <dx:GridViewDataTextColumn FieldName="NoticeTel" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />

                    <dx:GridViewDataTextColumn FieldName="Remark" CellStyle-Wrap="False"
                        Settings-AllowFilterBySearchPanel="False" Width="250"
                        Settings-AllowHeaderFilter="True"
                        SettingsHeaderFilter-Mode="CheckedList" />


                    <dx:GridViewDataTextColumn FieldName="GarageType" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
<dx:GridViewDataTextColumn FieldName="GarageCode" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
<dx:GridViewDataTextColumn FieldName="GarageName" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
<dx:GridViewDataTextColumn FieldName="GaragePlace" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
<dx:GridViewDataTextColumn FieldName="GarageTumbon" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
<dx:GridViewDataTextColumn FieldName="GarageAmphur" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
<dx:GridViewDataTextColumn FieldName="GarageProvince" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" />
<dx:GridViewDataTextColumn FieldName="GarageZipcode" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" /> 

<dx:GridViewDataTextColumn FieldName="ConsentFormNo" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" /> 


<dx:GridViewDataTextColumn FieldName="Amount1" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" /> 
<dx:GridViewDataTextColumn FieldName="Amount2" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" /> 
<dx:GridViewDataTextColumn FieldName="Amount3" CellStyle-Wrap="False" Settings-AllowFilterBySearchPanel="True" /> 

 

                </Columns>
                <FormatConditions>
                    <dx:GridViewFormatConditionHighlight FieldName="TransactionDate" Expression="[TransactionDate] <= LocalDateTimeToday()" Format="Custom" />
                    <dx:GridViewFormatConditionHighlight FieldName="TransactionDate" Expression="[TransactionDate] >= LocalDateTimeToday()" Format="Custom" />
                </FormatConditions>

                <ClientSideEvents CustomizationWindowCloseUp="grid_CustomizationWindowCloseUp"
                    RowClick="function(s, e) {
                        var key = s.GetRowKey(e.visibleIndex);  
                        ClientMailPreviewPanel.PerformCallback(key);
                        popupDetails.Show();       
                           
                       
                    }     
                     " />
                <SettingsPopup
                    CustomizationWindow-VerticalAlign="TopSides"
                    CustomizationWindow-HorizontalAlign="LeftSides">
                </SettingsPopup>
                <Settings ShowHeaderFilterButton="true" />

                <SettingsSearchPanel CustomEditorID="tbToolbarSearch" />
                <SettingsBehavior AllowDragDrop="True"
                    AllowFocusedRow="True" EnableCustomizationWindow="true"
                    EnableRowHotTrack="True"
                    ColumnResizeMode="Control" />


                <SettingsPager Mode="ShowPager" PageSize="10">
                    <PageSizeItemSettings Visible="true" Items="10, 15, 30, 45" ShowAllItem="false" />
                </SettingsPager>
                <Styles>
                    <Header Font-Bold="true" Font-Underline="true" HorizontalAlign="Center">
                    </Header>
                </Styles>

            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter runat="server" ID="GridExporter" GridViewID="MailGrid">
                <Styles>
                    <Default Font-Names="Tahoma" Font-Size="Medium" Wrap="False"></Default>
                </Styles>
            </dx:ASPxGridViewExporter>





        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>


<%--<asp:SqlDataSource ID="SqlDataSource_ClaimTransaction_Data" runat="server"
    ConnectionString="<%$ ConnectionStrings:MotorClaimConnectionString %>"
    SelectCommand="SELECT * FROM tblClaimTransaction_Data  "></asp:SqlDataSource>--%>


<dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="ClientLoadingPanel" Modal="true" />
<dx:ASPxHiddenField ID="HiddenField" runat="server" ClientInstanceName="ClientHiddenField" />



<asp:SqlDataSource ID="SqlDataSource_uw" runat="server" ConnectionString="<%$ ConnectionStrings:MotorClaimConnectionString %>"
    SelectCommand="select rtrim(UWCode) as Unwriter, rtrim(UWCode) + ' - ' + rtrim(UWName) as UWName from Running"></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_Showroom"
    runat="server"
    ConnectionString="<%$ ConnectionStrings:MotorClaimConnectionString %>"
    SelectCommand="SELECT ShowroomCode
      ,ShowRoomName + ' สาขา ' + BranchName + ' (' + convert(varchar,isnull(ShowRoomID,'')) + ')' as ShowRoomName
  FROM tblShowRoom 
  order by ShowRoomName, BranchName"></asp:SqlDataSource>





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

            <dx:ASPxGridView runat="server" ID="ASPxGridViewResult" ClientInstanceName="ASPxGridViewResult"
                SettingsLoadingPanel-Mode="ShowAsPopup" SettingsBehavior-AutoExpandAllGroups="true"
                DataSourceID="ObjectDataSource1" Width="100%" KeyFieldName="RunNo"
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

                <SettingsPager Mode="ShowPager" />

                <SettingsSearchPanel Visible="true" />

                <SettingsBehavior AllowDragDrop="True"
                    AutoExpandAllGroups="True" AllowFocusedRow="True"
                    EnableRowHotTrack="True"
                    ColumnResizeMode="Control" />


                <SettingsPager Mode="ShowPager" PageSize="15">
                    <PageSizeItemSettings Visible="false" Items="15, 30, 45" ShowAllItem="false" />
                </SettingsPager>

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


<%--<dx:LinqServerModeDataSource runat="server" ID="SqlDataSource_gridData"   ContextTypeName="MotorClaim.DataClasses_MotorClaimDataExt" TableName="V_ClaimTransactionDatas"  />--%>

<dx:ASPxPopupControl ID="popupResultDetails" ClientInstanceName="popupResultDetails" runat="server"
    AllowDragging="true"
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




<dx:ASPxPopupControl ID="popupDetails" ClientInstanceName="popupDetails"
    runat="server"
    Modal="True" Maximized="true"
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
    FooterText="" HeaderImage-IconID="programming_tag_32x32"
    ShowFooter="true">

    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">


            <dx:ASPxCallbackPanel ID="MailPreviewPanel" runat="server" RenderMode="Div" CssClass="MailPreviewPanel" ClientInstanceName="ClientMailPreviewPanel">
                <PanelCollection>

                    <dx:PanelContent>
                        <dx:ASPxFormLayout ID="formPreview" Styles-LayoutGroupBox-Caption-ForeColor="#0000ff" SettingsItems-VerticalAlign="Top" DataSourceID="SqlDataSource_Details" runat="server" Width="100%" AlignItemCaptionsInAllGroups="True">
                            <Styles>
                                <LayoutItem Caption-Font-Bold="true"></LayoutItem>
                            </Styles>
                            <Items>

                                <dx:LayoutGroup GroupBoxDecoration="Box" Caption="รายละเอียดการแจ้งงาน" ColCount="3">

                                    <Items>





                                        <dx:LayoutItem Caption="รหัสสาขา" FieldName="ShowRoomCode">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">

                                                    <dx:ASPxLabel ID="ASPxLabel1" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="โชว์รูม" FieldName="ShowRoomName">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel2" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="สาขา" FieldName="BranchName">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel6" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem Caption="ประกันภัย" FieldName="InsurerName">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer5" runat="server">
                                                    <dx:ASPxLabel AllowEllipsisInText="true" ID="ASPxLabel4" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="เลขรับแจ้ง" FieldName="RefNo">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer6" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel5" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="เลขเคลม" FieldName="ClaimNo">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer4" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel3" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="ผู้แจ้งเหตุ" FieldName="NoticeName">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer7" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel7" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="เบอร์โทร" FieldName="NoticeTel">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel8" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem Caption="วันที่แจ้งเหตุ" FieldName="ClaimNoticeDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer9" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel9" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>





                                        <dx:LayoutItem Caption="สถานะ" FieldName="ClaimStatus">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer40" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel40" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem Caption="แจ้งงาน" FieldName="IsPost">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer41" runat="server">
                                                    <dx:ASPxCheckBox ID="ASPxLabel41" Wrap="False" ReadOnly="true" AllowEllipsisInText="true" runat="server"></dx:ASPxCheckBox>


                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="วันที่แจ้งงาน" FieldName="PostDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer42" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel42" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem Caption="" ShowCaption="False" ColSpan="3" FieldName="ResultMessage">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer43" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel43" ForeColor="Red" Font-Italic="true" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                    </Items>
                                </dx:LayoutGroup>



                                <dx:LayoutGroup Caption="รายละเอียดความคุ้มครอง" ColCount="3">
                                    <Items>
                                        <dx:LayoutItem Caption="เลขที่ กธ." FieldName="PolicyNo">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer10" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel10" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="คุ้มครอง" FieldName="EffectiveDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer11" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel11" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="สิ้นสุด" FieldName="ExpiryDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer12" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel12" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="ผู้เอาประกัน" FieldName="FullCustomerName">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer13" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel13" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="ผู้รับผลประโยชน์" FieldName="Beneficiary">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer14" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel14" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <%--  <dx:LayoutItem Caption="วันที่แจ้ง" FieldName="ClaimNoticeDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel15" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>--%>
                                    </Items>
                                </dx:LayoutGroup>


                                <dx:LayoutGroup Caption="รายละเอียดรถยนต์" ColCount="3">
                                    <Items>
                                        <dx:LayoutItem Caption="หมายเลขทะเบียน" FieldName="CarLicense">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer155" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel155" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="เลขถัง" FieldName="ChassisNo">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer16" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel16" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="ทะเบียน" FieldName="CarLicense">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer17" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel17" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="ยี่ห้อ" FieldName="CarBrand">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer18" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel18" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="รุ่น" FieldName="CarModel">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer19" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel19" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="ปี" FieldName="CarYear">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer15" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel15" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>



                                        <dx:LayoutItem Caption="ผู้ขับขี่" FieldName="DriverName">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer20" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel20" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="เบอร์โทร" FieldName="DriverTel">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer21" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel21" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                    </Items>
                                </dx:LayoutGroup>




                                <dx:LayoutGroup Caption="รายละเอียดอุบัติเหตุ" ColCount="3">
                                    <Items>
                                        <dx:LayoutItem Caption="วันที่เกิดเหตุ" FieldName="AccidentDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer22" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel22" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="เวลา" FieldName="AccidentTime">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer23" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel23" Wrap="False" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="สถานที่เกิดเหตุ" FieldName="AccidentPlace">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer25" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel25" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="ลักษณะการเกิดเหตุ" FieldName="ClaimDetails">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer24" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel24" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="ประเภทเคลม" FieldName="ClaimType">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer26" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel26" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="ผลวิเคราะห์เคลม" FieldName="ClaimResult">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer27" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel27" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem Caption="ความเสียหาย" FieldName="ClaimDamageDetails">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer28" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel28" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                    </Items>
                                </dx:LayoutGroup>






                                <dx:LayoutGroup Caption="ประเภทการซ่อม" ColCount="3">
                                    <Items>

                                        <dx:LayoutItem Caption="ประเภทอู่" FieldName="GarageType">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer32" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel32" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="ชื่ออู่" FieldName="GarageName">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer33" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel33" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="ที่อยู่" FieldName="GaragePlace">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer34" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel34" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="วันที่นำรถเข้าซ่อม" FieldName="CarRepairDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer35" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel35" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="วันที่รับรถ" FieldName="CarReceiveDate">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer36" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel36" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="ConsentFormNo" FieldName="ConsentFormNo">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer37" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel37" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="ชื่อดีลเลอร์ที่สั่งอะไหล่" FieldName="PartsDealerName">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer38" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel38" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="รายการซ่อม" FieldName="PaymentDetails" ColSpan="2">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer39" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel39" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>




                                        <dx:LayoutItem Caption="ค่าแรง" FieldName="LaborAmt">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer29" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel29" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="ค่าอะไหล่" FieldName="PartsAmt">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer30" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel30" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="ค่ารายการอื่นๆ" FieldName="OtherAmt">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer31" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel31" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>



                                        <dx:LayoutItem Caption="Remark" FieldName="Remark" ColSpan="3">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer44" runat="server">
                                                    <dx:ASPxLabel ID="ASPxLabel44" AllowEllipsisInText="true" runat="server"></dx:ASPxLabel>
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

<asp:SqlDataSource ID="SqlDataSource_gridData" runat="server" ConnectionString="<%$ ConnectionStrings:MotorClaimConnectionString %>"
    SelectCommand="select * from V_ClaimTransactionData where RankNo=1 and ClaimStatus='01'"></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_Details" runat="server" ConnectionString="<%$ ConnectionStrings:MotorClaimConnectionString %>"
    SelectCommand="select * from V_ClaimTransactionData_Details where TRID=@TRID">

    <SelectParameters>
        <asp:SessionParameter Name="TRID" SessionField="TRID" />
    </SelectParameters>

</asp:SqlDataSource>
