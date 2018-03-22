<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Report_SummitBI_App03.ascx.vb" Inherits="Reports_Report_SummitBI_App03" %>

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
            <dx:ASPxGridView ID="gridData"
                ClientInstanceName="gridData"
                runat="server" Styles-Cell-Wrap="False"
                DataSourceID="SqlDataSource_gridData"
                Width="100%"  Settings-HorizontalScrollBarMode="Visible"
                SettingsBehavior-AutoExpandAllGroups="true">
                <SettingsPager Mode="ShowPager">
                    <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                </SettingsPager>
                <SettingsBehavior EnableRowHotTrack="true" AllowEllipsisInText="true" EnableCustomizationWindow="true"   ColumnResizeMode="Control" />
                <SettingsSearchPanel Visible="true" />
                <Settings ShowHeaderFilterButton="true" />

                <SettingsSearchPanel CustomEditorID="tbToolbarSearch" />
                <SettingsPopup
                    CustomizationWindow-VerticalAlign="TopSides"
                    CustomizationWindow-HorizontalAlign="LeftSides">
                </SettingsPopup>

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
                    <dx:GridViewDataColumn Caption="เลขที่สัญญา" FieldName="ContractNo" ></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="เลขกรมธรรม์" FieldName="PolicyNo"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="ผู้เช่าซื้อ" FieldName="CustomerName"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="เลขตัวถัง" FieldName="MChassisNo"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="ยี่ห้อ" FieldName="BrandCode"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="รุ่น" FieldName="ModelCode"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="ทะเบียน" FieldName="MLicense"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="วันคุ้มครอง" FieldName="DateBegin"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="วันสิ้นสุด" FieldName="DateEnd"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="วันที่ขาย / วันที่แจ้งยกเลิก" FieldName="CancelDate"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="วันคุ้มครองคงเหลือ" FieldName="DateCount"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="บริษัทประกันภัย" FieldName="UWName"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="Offset/No Offset" FieldName="BillingStatus"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="เบี้ยสุทธิ" FieldName="NetPremium"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="ภาษี" FieldName="Vat"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="อากร" FieldName="Stamp"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="เบี้ยรวม" FieldName="Premium"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="เบี้ยสุทธิ (คืน)" FieldName="cn_RePremiumTotal"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="ภาษี(คืน)" FieldName="cn_ReVat"></dx:GridViewDataColumn>
                    <dx:GridViewDataColumn Caption="เบี้ยรวม(คืน)" FieldName="cn_RePremiumGrandTotal"></dx:GridViewDataColumn>
               
                    
<dx:GridViewDataColumn Caption="PolicyStatus1" FieldName="PolicyStatus1"></dx:GridViewDataColumn>
<dx:GridViewDataColumn Caption="PolicyStatus2" FieldName="PolicyStatus2"></dx:GridViewDataColumn>

<dx:GridViewDataColumn Caption="StatementClose" FieldName="StatementClose"></dx:GridViewDataColumn>
                    
                     </Columns>

            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter runat="server" ID="GridExporter" GridViewID="gridData" />
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<%--<dx:LinqServerModeDataSource ID="SqlDataSource_gridData" runat="server" ContextTypeName="DataClasses_LWTReportsExt" TableName="RawData_SummitBI_App03s" />--%>

<asp:SqlDataSource ID="SqlDataSource_gridData" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"
    SelectCommand="select * from RawData_SummitBI_App03 where  DateMakeContract is null "></asp:SqlDataSource>
