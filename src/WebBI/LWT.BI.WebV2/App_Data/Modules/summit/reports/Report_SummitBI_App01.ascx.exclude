<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Report_SummitBI_App01.ascx.vb" Inherits="Reports_Report_SummitBI_App01" %>

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

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" ShowHeader="false" runat="server" Width="100%">
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
                                                <dx:ListEditItem Text="วันที่ทำสัญญา" Value="DateMakeContract" Selected="true" />
                                                <dx:ListEditItem Text="วันที่แจ้งงาน" Value="DateMakePolicy" />
                                                <dx:ListEditItem Text="วันเริ่มคุ้มครอง" Value="DateBegin" />
                                                <dx:ListEditItem Text="วันหมดอายุ" Value="DateEnd" />
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
            <dx:ASPxGridView ID="gridData" ClientInstanceName="gridData" DataSourceID="SqlDataSource_gridData"
                runat="server" Styles-Cell-Wrap="False"
                KeyFieldName="ContractNo"
                Width="100%"  Settings-HorizontalScrollBarMode="Visible"
                SettingsBehavior-AutoExpandAllGroups="true">
                <SettingsPager Mode="ShowPager">
                    <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                </SettingsPager>
                <SettingsBehavior EnableRowHotTrack="true" AllowEllipsisInText="true" EnableCustomizationWindow="true"   ColumnResizeMode="Control"/>
                <SettingsPopup
                    CustomizationWindow-VerticalAlign="TopSides"
                    CustomizationWindow-HorizontalAlign="LeftSides">
                </SettingsPopup>
                <Settings ShowHeaderFilterButton="true" />
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
                                        Image-IconID="richedit_showallfieldresults_16x16" Border-BorderWidth="0"
                                        AutoPostBack="false">
                                        <ClientSideEvents Click="button1_Click" />
                                    </dx:ASPxButton>
                                </Template>
                            </dx:GridViewToolbarItem>




                            <dx:GridViewToolbarItem BeginGroup="true">
                                <Template>
                                    <dx:ASPxButton ID="btnExport" OnClick="btnExportData_Click"
                                        runat="server" Border-BorderWidth="0"
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

                    <%--<dx:GridViewDataColumn FieldName="BranchCode" SettingsHeaderFilter-Mode="CheckedList" ></dx:GridViewDataColumn>--%>
                    <dx:GridViewDataColumn FieldName="CarType" SettingsHeaderFilter-Mode="CheckedList"></dx:GridViewDataColumn>
                    <%--<dx:GridViewDataColumn FieldName="PlanCode"  SettingsHeaderFilter-Mode="CheckedList" ></dx:GridViewDataColumn>--%>
                    <dx:GridViewDataColumn FieldName="Title"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="FirstName"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="LastName"></dx:GridViewDataColumn>
                    <%--<dx:GridViewDataColumn FieldName="IDCard" ></dx:GridViewDataColumn>--%>
                    <%--<dx:GridViewDataColumn FieldName="Address" ></dx:GridViewDataColumn>--%>
                    <%--<dx:GridViewDataColumn FieldName="Road" ></dx:GridViewDataColumn>--%>
                    <dx:GridViewDataColumn FieldName="District"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Amphur"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Province"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Postal"></dx:GridViewDataColumn>
                    <%--<dx:GridViewDataColumn FieldName="Phone" ></dx:GridViewDataColumn>--%>
                    <dx:GridViewDataColumn FieldName="Name2"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Brand"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Model"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="SystemModelCode"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ColorName"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="CC"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ChassisNo"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="EngineNo"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="mRegisterYear"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="mLicense"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="mLicenseProvince"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="mUseType"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="mBuyType"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ContractDate"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="ContractNo"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Period"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="EffectiveDate"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="SumInsure1"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Premium"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Benefit"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="Relation"></dx:GridViewDataColumn>
                    <%--<dx:GridViewDataColumn FieldName="BenefitAddress" ></dx:GridViewDataColumn>--%>
                    <dx:GridViewDataColumn FieldName="BillingType"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="BillingName1"></dx:GridViewDataColumn>
                    <%--<dx:GridViewDataColumn FieldName="BillingAddress1" ></dx:GridViewDataColumn>--%>
                    <dx:GridViewDataColumn FieldName="BillingPremiumTotal1"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="BillingName2"></dx:GridViewDataColumn>
                    <%--<dx:GridViewDataColumn FieldName="BillingAddress2" ></dx:GridViewDataColumn>--%>
                    <dx:GridViewDataColumn FieldName="BillingPremiumTotal2"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="StatementClose"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="CancelledDate"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="จำนวนวันยกเลิกนับจากวันที่คุ้มครอง" FieldName="CancelDateCount"></dx:GridViewDataColumn>


                    <dx:GridViewDataColumn FieldName="Status"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="PolicyStatus1"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn FieldName="PolicyStatus2"></dx:GridViewDataColumn>


                </Columns>
            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter runat="server" ID="GridExporter" GridViewID="gridData" />
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>


<%--
    
<dx:LinqServerModeDataSource ID="SqlDataSource_gridData"  runat="server" ContextTypeName="DataClasses_LWTReportsExt"  TableName="RawData_SummitBI_App01s"  />

    
--%>


<asp:SqlDataSource ID="SqlDataSource_gridData" runat="server"
    ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"
    SelectCommand="select * from RawData_SummitBI_App01 where  DateMakeContract is null "></asp:SqlDataSource>
