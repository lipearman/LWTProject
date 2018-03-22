<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxRawDataAPDProduction.ascx.vb" Inherits="ucDevxRawDataAPDProduction" %>
<script type="text/javascript">
    function button1_Click(s, e) {
        if (gridData.IsCustomizationWindowVisible())
            gridData.HideCustomizationWindow();
        else
            gridData.ShowCustomizationWindow();
        UpdateButtonText();
    }
    function grid_CustomizationWindowCloseUp(s, e) {
        UpdateButtonText();
    }
    function UpdateButtonText() {
        var text = gridData.IsCustomizationWindowVisible() ? "Hide" : "Show";
        text += " Column";
        button1.SetText(text);
    }
</script>

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="" runat="server" EnableAdaptivity="true" Width="100%" HeaderImage-IconID="support_feature_16x16office2013" >
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxFormLayout ID="frmSearch" ClientInstanceName="frmSearch" runat="server" AlignItemCaptionsInAllGroups="True" ColCount="1" RequiredMarkDisplayMode="None" ShowItemCaptionColon="False">
                <Items>
                    <dx:LayoutGroup Caption="Search" ShowCaption="False" GroupBoxDecoration="None" ColCount="4" Width="330px">
                        <Items>
                            <dx:LayoutItem Caption="เลือก">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer
                                        ID="LayoutItemNestedControlContainer4"
                                        runat="server"
                                        SupportsDisabledAttribute="True">

                                        <dx:ASPxComboBox ID="FilterDateType"
                                            runat="server"
                                            ValueType="System.String">

                                            <Items>

                                                <dx:ListEditItem Text="วันที่ Billing" Value="BillingDate" Selected="true" />
                                                <dx:ListEditItem Text="วันเริ่มคุ้มครอง" Value="PeriodFrom" />
                                                <dx:ListEditItem Text="วันหมดอายุ" Value="PeriodTo" />

                                            </Items>

                                        </dx:ASPxComboBox>


                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption="จากวันที่">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer
                                        ID="LayoutItemNestedControlContainer23"
                                        runat="server"
                                        SupportsDisabledAttribute="True">
                                        <dx:ASPxDateEdit ID="DateFrom" ClientInstanceName="DateFrom" DisplayFormatString="{0:dd/MM/yyyy}" runat="server">
                                            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>

                                        </dx:ASPxDateEdit>

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption=" &nbsp; &nbsp;ถึงวันที่">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer3" runat="server" SupportsDisabledAttribute="True">
                                        <dx:ASPxDateEdit ID="DateTo" ClientInstanceName="DateTo" DisplayFormatString="{0:dd/MM/yyyy}" runat="server">
                                            <ValidationSettings ErrorDisplayMode="None" Display="Dynamic">
                                                <RequiredField IsRequired="True" />
                                            </ValidationSettings>

                                        </dx:ASPxDateEdit>
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>

                            <dx:LayoutItem Caption=" &nbsp; &nbsp;">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server" SupportsDisabledAttribute="True">

                                        <dx:ASPxButton ID="btnSearch" runat="server" RenderMode="Button"
                                            Width="90px" Text="Search" AutoPostBack="false" CausesValidation="true" ValidationContainerID="frmSearch">
                                            <Image IconID="filter_filter_16x16"></Image>
                                            <ClientSideEvents Click="function(s, e) { 
                                                            if(ASPxClientEdit.AreEditorsValid()) 
                                                            { 
                                                               LoadingPanel.Show();
                                                               e.processOnServer = true;
                                                            }
                                                            else
                                                            {
                                                                alert('กรุณากรอกข้อมูลให้ครบ');
                                                            }
                                                        }" />
                                        </dx:ASPxButton>

                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                            <dx:LayoutItem Caption="">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" SupportsDisabledAttribute="True">
                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
                        </Items>
                    </dx:LayoutGroup>
                </Items>
            </dx:ASPxFormLayout>
            <%-- ============ todo0 =======================--%>
            <%--KeyFieldName="Client" --%>
            <dx:ASPxGridView ID="gridData" KeyFieldName="Invoiceno"
                ClientInstanceName="gridData"
                DataSourceID="SqlDataSource_gridData"
                runat="server" Styles-Cell-Wrap="False"
                Width="100%" Settings-HorizontalScrollBarMode="Visible"
                SettingsBehavior-AutoExpandAllGroups="true">
                <SettingsPager Mode="ShowPager">
                    <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                </SettingsPager>
                <SettingsSearchPanel Visible="true" />
                <Settings ShowHeaderFilterButton="true" />
                <SettingsBehavior EnableRowHotTrack="true" AllowEllipsisInText="true" EnableCustomizationWindow="true" ColumnResizeMode="Control" />
                <SettingsPopup
                    CustomizationWindow-VerticalAlign="TopSides"
                    CustomizationWindow-HorizontalAlign="LeftSides">
                </SettingsPopup>

                <SettingsSearchPanel CustomEditorID="tbToolbarSearch" />

                <Toolbars>
                    <dx:GridViewToolbar ItemAlign="Left">
                        <Items>

                            <dx:GridViewToolbarItem BeginGroup="true">
                                <Template>
                                    <dx:ASPxButton runat="server" ID="button1"
                                        ClientInstanceName="button1"
                                        Text="Show Column"
                                        UseSubmitBehavior="false"
                                        Image-IconID="richedit_showallfieldresults_16x16" 
                                        AutoPostBack="false">
                                        <ClientSideEvents Click="button1_Click" />
                                    </dx:ASPxButton>
                                </Template>
                            </dx:GridViewToolbarItem>




                            <dx:GridViewToolbarItem BeginGroup="true">
                                <Template>
                                    <dx:ASPxButton ID="btnExport" OnClick="btnExportData_Click"
                                        runat="server" 
                                        Image-IconID="export_exporttoxlsx_16x16"
                                        ToolTip="Export"
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
                    <dx:GridViewDataColumn FieldName="Client"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Class"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Folio"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Year"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Version"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Underwriter"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="AExec"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="TransType"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Invoiceno"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Effective"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="PeriodFrom"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="PeriodTo"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="PolicyNo"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="BillingDate"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Currency"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ExchangeRate"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="SumInsuredTHB"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="PremiumTHB"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="StampTHB"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="VATTHB"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="BrokerageTHB"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ORInTHB"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="AdminFeeIn1THB"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="AdminFeeIn2THB"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="DiscountTHB"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="CommissionOutTHB"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="OROutTHB"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="AdminFeeOut1THB"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="AdminFeeOut2THB"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="BriefDescription"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="BriefII"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="BriefIII"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Location"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ReinsuranceInfo"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Remarks"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="PPWFromClient"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="PPWToUW"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="CoInsuranceNo"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="RenewFlag"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="AEProduction"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ErrorFlag"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="UpfrontFlag"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="EntryBy"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="EntryDate"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="AEName"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="AEGroupRunning"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="AEGroupName"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="AESGroupRunning"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="AESGroupName"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Owner"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="BusinessGroup"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ClassGroup"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="RiskGroupI"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="RiskGroupII"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="RiskGovernment"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="RiskShortDesc"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="BillingKey"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="AgentStaff"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="AgentName"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Department"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ClientName"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="SourceCode"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="SourceName"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="GroupName"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ClientType"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="IndustryEng"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="IndustryThai"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="TotalPremiumTHB"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="TotalIncome"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="NetIncome"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="UWName"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="UWNameEng"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="UWCrossRef"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Division"></dx:GridViewDataColumn>


                    <dx:GridViewDataColumn FieldName="ActualAmount" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="BudgetAmount" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="BudgetDate" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="BusinessGroup" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="CalendarPeriod" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="CalendarYear" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="DateBudget" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="EngMonthName" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="FiscalEngMonth" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="FiscalPeriod" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="FiscalThaiMonth" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="FiscalYear" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ForcastAmount" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="FullDateAlternateKey" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ThaiDayNameofWeek" Visible="false"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ThaiMonthName" Visible="false"></dx:GridViewDataColumn>

                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter runat="server" ID="GridExporter" GridViewID="gridData" />
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<dx:LinqServerModeDataSource ID="SqlDataSource_gridData" runat="server" ContextTypeName="DataClasses_LWTReportsExt" TableName="RawDataProductions" />

<%--<asp:SqlDataSource ID="SqlDataSource_gridData" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"
    SelectCommand="select * from RawDataProduction"
></asp:SqlDataSource>--%>