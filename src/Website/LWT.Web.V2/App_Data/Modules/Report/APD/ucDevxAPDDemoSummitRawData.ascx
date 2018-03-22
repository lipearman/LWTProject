<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxAPDDemoSummitRawData.ascx.vb" Inherits="Modules_ucDevxAPDDemoSummitRawData" %>

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
        text += " Customization Window";
        button1.SetText(text);
    }
</script>



<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="ข้อมูล RAWData" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxFormLayout ID="frmSearch" ClientInstanceName="frmSearch" runat="server" AlignItemCaptionsInAllGroups="True" ColCount="1" RequiredMarkDisplayMode="None" ShowItemCaptionColon="False">
                <Items>
                    <dx:LayoutGroup Caption="Search" ShowCaption="False" GroupBoxDecoration="None" ColCount="4" Width="330px">
                        <Items>

                            <dx:LayoutItem Caption="วันที่ ">
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


                                        <table>
                                            <tr>
                                                <td>
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
                                                </td>
                                                <td>&nbsp; &nbsp;</td>
                                                <td></td>
                                            </tr>

                                        </table>

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
            <dx:ASPxButton runat="server" ID="button1" ClientInstanceName="button1" Text="Show Customization Window"
                UseSubmitBehavior="false" AutoPostBack="false">
                <ClientSideEvents Click="button1_Click" />
            </dx:ASPxButton>
            <%--KeyFieldName="Client" --%>
            <dx:ASPxGridView ID="gridData" ClientInstanceName="gridData" runat="server" DataSourceID="SqlDataSource_gridData"
                KeyFieldName="SummitID" AutoGenerateColumns="False" Width="100%" 
                SettingsPopup-CustomizationWindow-Width="300"
                SettingsPopup-CustomizationWindow-Height="300"
                SettingsBehavior-AutoExpandAllGroups="true">
                <SettingsPager Mode="ShowPager">
                    <PageSizeItemSettings Visible="true" Items="10, 20, 50" ShowAllItem="true" />
                </SettingsPager>
                <SettingsBehavior EnableRowHotTrack="true" ConfirmDelete="true" EnableCustomizationWindow="true" />
                <SettingsSearchPanel Visible="true" />
                <Settings ShowGroupPanel="true" />

                <SettingsLoadingPanel Mode="ShowOnStatusBar" />

                <ClientSideEvents CustomizationWindowCloseUp="grid_CustomizationWindowCloseUp" />

                <Columns>

<dx:GridViewDataColumn FieldName="RowNo" Settings-AllowHeaderFilter="False" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="AgentCode" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="InsureTypeCode" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="InsureTypeName" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="CarBrand" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="ClientCode" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="Title" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="InsuredName" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="IDCard" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="Address" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="Telephone" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="DealerName" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="Insuerer" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="CarModel" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="CarBody" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="Chassis" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="CarPlate" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="CarCode" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="CarRegYear" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="Period" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="EffectiveDate" Settings-AllowHeaderFilter="False"  CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="ExpiryDate" Settings-AllowHeaderFilter="False"  CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="CarPrice" Settings-AllowHeaderFilter="False"  CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="Suminsured1Y" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="Suminsured2Y" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="Suminsured3Y" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="GrossPremium" Settings-AllowHeaderFilter="False"  CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="Stamp" Settings-AllowHeaderFilter="False"  CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="VAT" Settings-AllowHeaderFilter="False"  CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="NetPremium" Settings-AllowHeaderFilter="False"  CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="ClassOfRisk" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="BuyType" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="ReceiveCode" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="CertificateNo" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="PolicyNo" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="CreateDate" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="Benefitialy" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="Coverage" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="Status" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="Status1" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="Insuerer2" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="Benefitialy2" CellStyle-Wrap="False" ExportCellStyle-Wrap="False" />

                     </Columns>

            </dx:ASPxGridView>
            <dx:ASPxGridViewExporter runat="server" ID="GridExporter" GridViewID="gridData" />

            <br />
            <dx:ASPxButton ID="btnExportData" runat="server" RenderMode="Button"
                Width="90px" Text="Export" AutoPostBack="false" CausesValidation="false">
                <Image IconID="export_exporttoxlsx_16x16"></Image>
            </dx:ASPxButton>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>


<asp:SqlDataSource ID="SqlDataSource_gridData" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"></asp:SqlDataSource>
