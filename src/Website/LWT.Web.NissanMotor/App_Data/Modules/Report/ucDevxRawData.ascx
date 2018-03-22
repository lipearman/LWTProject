<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxRawData.ascx.vb" Inherits="Modules_ucDevxRawData" %>

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
                                                <dx:ListEditItem Text="วันที่แจ้งงาน" Value="DateClosing" Selected="true" />
                                                <dx:ListEditItem Text="วันเริ่มคุ้มครอง" Value="DateEffective" /> 
                                            </Items>

                                        </dx:ASPxComboBox>


                                    </dx:LayoutItemNestedControlContainer>
                                </LayoutItemNestedControlCollection>
                            </dx:LayoutItem>
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
                KeyFieldName="TempID" AutoGenerateColumns="False" Width="100%" 
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
                    <dx:GridViewDataColumn FieldName="RowNo" CellStyle-Wrap="False" Caption="ลำดับ" ExportCellStyle-Wrap="False" />
                    <dx:GridViewDataColumn FieldName="TempID" CellStyle-Wrap="False" Caption="เลขรับแจ้ง" ExportCellStyle-Wrap="False" />
                    <dx:GridViewDataColumn FieldName="ClientCode" CellStyle-Wrap="False" Caption="Client Code" ExportCellStyle-Wrap="False" />
                                        
<dx:GridViewDataColumn FieldName="CusTitle" CellStyle-Wrap="False" Caption="คำนำหน้า" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="CusName" CellStyle-Wrap="False" Caption="ชื่อ" ExportCellStyle-Wrap="False" />
<dx:GridViewDataColumn FieldName="CusSurname" CellStyle-Wrap="False" Caption="สกุล" ExportCellStyle-Wrap="False" />
                        




                    
                    <dx:GridViewDataColumn FieldName="InsuredName" CellStyle-Wrap="False" Caption="ชื่อและนามสกุล ผู้เอาประกันภัย" ExportCellStyle-Wrap="False" />
                    
                    
                    <dx:GridViewDataColumn FieldName="InsurerNameTh" CellStyle-Wrap="False" Caption="บริษัทประกันภัย" ExportCellStyle-Wrap="False" />
                   <dx:GridViewDataColumn FieldName="CarChassisNo" CellStyle-Wrap="False" Caption="เลขตัวถัง" ExportCellStyle-Wrap="False" />
                 <dx:GridViewDataColumn FieldName="CarEngineNo" CellStyle-Wrap="False" Caption="เลขเครื่อง" ExportCellStyle-Wrap="False" />
                    
                    
                    <dx:GridViewDataColumn FieldName="DealerName" CellStyle-Wrap="False" Caption="ดีลเลอร์" ExportCellStyle-Wrap="False" />
                     <dx:GridViewDataColumn FieldName="ShowroomName" CellStyle-Wrap="False" Caption="โชว์รูม" ExportCellStyle-Wrap="False" />
                    <dx:GridViewDataColumn FieldName="NLTDealerCode" CellStyle-Wrap="False" Caption="DealerCode" ExportCellStyle-Wrap="False" />
                    <dx:GridViewDataColumn FieldName="NLTArea" CellStyle-Wrap="False" Caption="Area" ExportCellStyle-Wrap="False" />

                    <dx:GridViewDataColumn FieldName="AppBeneficiary" CellStyle-Wrap="False" Caption="ผู้รับผลประโยชน์" ExportCellStyle-Wrap="False" />
                    <dx:GridViewDataDateColumn FieldName="DateEffective" CellStyle-Wrap="False" Caption="วันเริ่มคุ้มครอง" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" ExportCellStyle-Wrap="False" />
                    <dx:GridViewDataDateColumn FieldName="DateExpired" CellStyle-Wrap="False" Caption="วันสิ้นสุด" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy" ExportCellStyle-Wrap="False" />
                    <dx:GridViewDataColumn FieldName="CarTypeNameFull" CellStyle-Wrap="False" Caption="รุ่นรถ" ExportCellStyle-Wrap="False" />
                     <%--  <dx:GridViewDataTextColumn FieldName="VmiNetPremium" PropertiesTextEdit-DisplayFormatString="{0:N2}" CellStyle-Wrap="False" Caption="เบี้ยสุทธิ" ExportCellStyle-Wrap="False" />
                    <dx:GridViewDataTextColumn FieldName="VmiTotalPremium" PropertiesTextEdit-DisplayFormatString="{0:N2}" CellStyle-Wrap="False" Caption="เบี้ยรวม" ExportCellStyle-Wrap="False" />
               --%>

                     <dx:GridViewDataColumn FieldName="CusAddress1" CellStyle-Wrap="False" Caption="ที่อยู่ผู้เอาประกัน" ExportCellStyle-Wrap="False" />
                     <dx:GridViewDataColumn FieldName="CusMobile" CellStyle-Wrap="False" Caption="เบอร์โทรศัพท์" ExportCellStyle-Wrap="False" />
                     
                <dx:GridViewDataColumn FieldName="Status" CellStyle-Wrap="False" Visible="false" Settings-AllowHeaderFilter="True" SettingsHeaderFilter-Mode="CheckedList" ExportCellStyle-Wrap="False" />
                    <dx:GridViewDataColumn FieldName="PolicyNo" CellStyle-Wrap="False"   ExportCellStyle-Wrap="False" />
                        
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
