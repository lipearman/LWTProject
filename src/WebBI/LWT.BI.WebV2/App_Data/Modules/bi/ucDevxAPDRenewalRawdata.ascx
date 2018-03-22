<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxAPDRenewalRawdata.ascx.vb" Inherits="Modules_ucDevxAPDRenewalRawdata" %>
<script>

    function OnToolbarItemClick(s, e) {
        if (IsExportToolbarCommand(e.item.name)) {
            e.processOnServer = true;
            e.usePostBack = true;
        }
    }
    function IsExportToolbarCommand(command) {
        return command == "ExportToXLSX" || command == "ExportToXLS";
    }
</script>
<style>
.dxeListBox_Office365 .dxeListBoxEllipsis td.dxeC, .dxeListBox_Office365 .dxeListBoxEllipsis td.dxeCR {
    width: 32px;
}
</style>
<asp:SqlDataSource ID="SqlDataSource_RawData" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIRawDataConnection %>"
    SelectCommand="select * from V_APDRenewal_RAWDATA order by LOTNO,NO "></asp:SqlDataSource>

<%--<dx:LinqServerModeDataSource ID="SqlDataSource_RawData" ContextTypeName="DataClasses_PortalBIRawdataExt" TableName="V_APDRenewal_RAWDATA" runat="server" />--%>


<dx:ASPxGridViewExporter runat="server" ID="GridExporter" GridViewID="gridRawData" />

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true"
    HeaderText="" Font-Bold="true" 
    runat="server" EnableAdaptivity="true" Width="100%" HeaderImage-IconID="businessobjects_bonote_32x32">
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxGridView ID="gridRawData" Width="100%"
                ClientInstanceName="gridRawData" KeyFieldName="ID"
                DataSourceID="SqlDataSource_RawData" 
                SettingsBehavior-ColumnResizeMode="Control"
                runat="server" AutoGenerateColumns="false">
                <Styles>
                    <Cell Wrap="False"></Cell>
                </Styles>
                <Settings HorizontalScrollBarMode="Visible" ShowFooter="true" ShowFilterRowMenu="true" UseFixedTableLayout="true"></Settings>
                <SettingsSearchPanel CustomEditorID="tbToolbarSearch" />
                <SettingsBehavior ColumnResizeMode="Control"></SettingsBehavior>
                <SettingsCustomizationDialog Enabled="true" />

       <SettingsPager Mode="ShowPager" PageSize="10" >
                    <PageSizeItemSettings Visible="true" Items="10, 20, 30, 40 ,50" ShowAllItem="true" />
                </SettingsPager>

                <ClientSideEvents ToolbarItemClick="OnToolbarItemClick" />




                <Toolbars>
                    <dx:GridViewToolbar Position="Top" ItemAlign="Left">
                        <Items>
                             <dx:GridViewToolbarItem Text="Export to" Image-IconID="actions_download_16x16office2013" BeginGroup="true">
                                <Items>
                                    <dx:GridViewToolbarItem Name="ExportToXLSX" Text="XLSX" Image-IconID="export_exporttoxlsx_16x16office2013" />
                                    <dx:GridViewToolbarItem Name="ExportToXLS" Text="XLS" Image-IconID="export_exporttoxls_16x16office2013" />
                                </Items>

                            </dx:GridViewToolbarItem>

                            <dx:GridViewToolbarItem Command="ShowCustomizationDialog" BeginGroup="true" />
                            <dx:GridViewToolbarItem Command="Refresh" BeginGroup="true" />

                        </Items>
                    </dx:GridViewToolbar>
                </Toolbars>


                <Columns>

<dx:GridViewDataTextColumn FieldName="LOTNO" CellStyle-Wrap="False" Settings-AllowHeaderFilter="True" SettingsHeaderFilter-Mode="CheckedList"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="NO" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataDateColumn FieldName="ExpiredDate" CellStyle-Wrap="False" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy"></dx:GridViewDataDateColumn>
<dx:GridViewDataTextColumn FieldName="Officer" CellStyle-Wrap="False" Settings-AllowHeaderFilter="True" SettingsHeaderFilter-Mode="CheckedList"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="OfficerForSort" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="AE Name" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="AE Group Running" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="AE Group Name II" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="AES Group Running" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="AES Group Name" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
<%--<dx:GridViewDataTextColumn FieldName="Business Group" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>--%>
<dx:GridViewDataTextColumn FieldName="Business GroupF" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="Class Group" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="Division Code" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="Type" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="Group" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="Code" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="Insured" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="Insurer" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="PolicyNo" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataSpinEditColumn FieldName="SumInsured" CellStyle-Wrap="False" PropertiesSpinEdit-DisplayFormatString="{0:N0}"></dx:GridViewDataSpinEditColumn>
<dx:GridViewDataSpinEditColumn FieldName="ExpiringPremium" CellStyle-Wrap="False" PropertiesSpinEdit-DisplayFormatString="{0:N2}"></dx:GridViewDataSpinEditColumn>
<dx:GridViewDataTextColumn FieldName="ReminderDate" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="ResponseDate" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataSpinEditColumn FieldName="RenewalPremium" CellStyle-Wrap="False"  PropertiesSpinEdit-DisplayFormatString="{0:N2}"></dx:GridViewDataSpinEditColumn>
<dx:GridViewDataTextColumn FieldName="RemarkFromPolicy" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="RemarkFromAE" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="IsMatch" CellStyle-Wrap="False" Settings-AllowHeaderFilter="True"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="RenewFlag" CellStyle-Wrap="False" Settings-AllowHeaderFilter="True"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="TransType" CellStyle-Wrap="False" Settings-AllowHeaderFilter="True"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="RiskGroupI" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="Class of Risk (Sibis)" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="New PolicyNo" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataDateColumn FieldName="PeriodFrom" CellStyle-Wrap="False" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy"></dx:GridViewDataDateColumn>
<dx:GridViewDataDateColumn FieldName="PeriodTo" CellStyle-Wrap="False" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy"></dx:GridViewDataDateColumn>
<dx:GridViewDataDateColumn FieldName="DNDate" CellStyle-Wrap="False" PropertiesDateEdit-DisplayFormatString="dd/MM/yyyy"></dx:GridViewDataDateColumn>
<dx:GridViewDataTextColumn FieldName="InvoiceNo" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="Movement Status" CellStyle-Wrap="False" Settings-AllowHeaderFilter="True"></dx:GridViewDataTextColumn>
<dx:GridViewDataSpinEditColumn FieldName="Premium" CellStyle-Wrap="False" PropertiesSpinEdit-DisplayFormatString="{0:N2}"></dx:GridViewDataSpinEditColumn>
<dx:GridViewDataSpinEditColumn FieldName="StampDuty" CellStyle-Wrap="False" PropertiesSpinEdit-DisplayFormatString="{0:N2}"></dx:GridViewDataSpinEditColumn>
<dx:GridViewDataSpinEditColumn FieldName="VAT" CellStyle-Wrap="False" PropertiesSpinEdit-DisplayFormatString="{0:N2}"></dx:GridViewDataSpinEditColumn>
<dx:GridViewDataSpinEditColumn FieldName="TotalPremiumTHB" CellStyle-Wrap="False" PropertiesSpinEdit-DisplayFormatString="{0:N2}"></dx:GridViewDataSpinEditColumn>
<dx:GridViewDataSpinEditColumn FieldName="TotalIncome" CellStyle-Wrap="False" PropertiesSpinEdit-DisplayFormatString="{0:N2}"></dx:GridViewDataSpinEditColumn>
<dx:GridViewDataSpinEditColumn FieldName="NetIncome" CellStyle-Wrap="False" PropertiesSpinEdit-DisplayFormatString="{0:N2}"></dx:GridViewDataSpinEditColumn>
<dx:GridViewDataTextColumn FieldName="FiscalYear" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="Status" CellStyle-Wrap="False" Settings-AllowHeaderFilter="True" SettingsHeaderFilter-Mode="CheckedList"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="ClientCount" CellStyle-Wrap="False" Settings-AllowHeaderFilter="True" SettingsHeaderFilter-Mode="CheckedList"></dx:GridViewDataTextColumn>











                    <%--<dx:GridViewDataTextColumn FieldName="Inv No." CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn FieldName="DN Date" CellStyle-Wrap="False" PropertiesDateEdit-DisplayFormatString="{0:dd/MM/yyyy}"></dx:GridViewDataDateColumn>
                    <dx:GridViewDataDateColumn FieldName="Eff. Date" CellStyle-Wrap="False" PropertiesDateEdit-DisplayFormatString="{0:dd/MM/yyyy}"></dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="Client Code" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Client Name" Width="200" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Client Group" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="AA-Group Name" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="AExec" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="UW" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="PolicyNo" Width="200" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Risk" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Brief Description" Width="200" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="BriefII" Width="200" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="BriefIII" Width="200" CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Current" CellStyle-Wrap="False" PropertiesTextEdit-DisplayFormatString="{0:N2}"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="31 - 60 Days" CellStyle-Wrap="False" PropertiesTextEdit-DisplayFormatString="{0:N2}"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="61 - 90 Days" CellStyle-Wrap="False" PropertiesTextEdit-DisplayFormatString="{0:N2}"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="91-180 Days" CellStyle-Wrap="False" PropertiesTextEdit-DisplayFormatString="{0:N2}"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="181-365 Days" CellStyle-Wrap="False" PropertiesTextEdit-DisplayFormatString="{0:N2}"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Over 1 Year" CellStyle-Wrap="False" PropertiesTextEdit-DisplayFormatString="{0:N2}"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="AMOUNT BAL" CellStyle-Wrap="False" PropertiesTextEdit-DisplayFormatString="{0:N2}"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Sum >91 days" CellStyle-Wrap="False" PropertiesTextEdit-DisplayFormatString="{0:N2}"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn FieldName="CreateDate" CellStyle-Wrap="False" Visible="false" PropertiesDateEdit-DisplayFormatString="{0:dd/MM/yyyy}"></dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="CreateBy" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Remark" Width="200"  CellStyle-Wrap="False"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="BY" CellStyle-Wrap="False" Visible="false"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn FieldName="LastDate" CellStyle-Wrap="False" Visible="false" PropertiesDateEdit-DisplayFormatString="{0:dd/MM/yyyy}"></dx:GridViewDataDateColumn>
--%>

                </Columns>
                <TotalSummary>
                     
                    <dx:ASPxSummaryItem FieldName="PolicyNo" SummaryType="Count" DisplayFormat="{0:N0}" />
                    <dx:ASPxSummaryItem FieldName="Movement Status" SummaryType="Sum" DisplayFormat="{0:N0}" />

                    <dx:ASPxSummaryItem FieldName="Premium" SummaryType="Sum" DisplayFormat="{0:N2}" />
                    <dx:ASPxSummaryItem FieldName="StampDuty" SummaryType="Sum" DisplayFormat="{0:N2}" />
                    <dx:ASPxSummaryItem FieldName="VAT" SummaryType="Sum" DisplayFormat="{0:N2}" />
                    <dx:ASPxSummaryItem FieldName="TotalPremiumTHB" SummaryType="Sum" DisplayFormat="{0:N2}" />
                    <dx:ASPxSummaryItem FieldName="TotalIncome" SummaryType="Sum" DisplayFormat="{0:N2}" />
                    <dx:ASPxSummaryItem FieldName="NetIncome" SummaryType="Sum" DisplayFormat="{0:N2}" />

                    <dx:ASPxSummaryItem FieldName="ClientCount" SummaryType="Count" DisplayFormat="{0:N0}" />
                </TotalSummary>
            </dx:ASPxGridView>



        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>