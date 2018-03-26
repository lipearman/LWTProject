<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxLWTRenewalRawdata.ascx.vb" Inherits="Modules_ucDevxLWTRenewalRawdata" %>
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
<asp:SqlDataSource ID="SqlDataSource_RawData" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsSIBISDBConnectionString %>"
    SelectCommand="select top 100 * from tblLWTRenewalList order by FiscalYear,Lotno,ItemID "
    UpdateCommand="
    update tblLWTRenewalList
    set RemarkFromAE=@RemarkFromAE

    where ItemID=@ItemID

    "
    >
 <UpdateParameters>
      <asp:Parameter Name="RemarkFromAE" />
      <asp:Parameter Name="ItemID" />
 </UpdateParameters>

</asp:SqlDataSource>

<%--<dx:LinqServerModeDataSource ID="SqlDataSource_RawData" ContextTypeName="DataClasses_PortalBIRawdataExt" TableName="V_APDRenewal_RAWDATA" runat="server" />--%>


<dx:ASPxGridViewExporter runat="server" ID="GridExporter" GridViewID="gridRawData" />

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true"
    HeaderText="" Font-Bold="true"
    runat="server" EnableAdaptivity="true" Width="100%" HeaderImage-IconID="businessobjects_bonote_32x32">
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxGridView ID="gridRawData" Width="100%"
                ClientInstanceName="gridRawData" KeyFieldName="ItemID"
                DataSourceID="SqlDataSource_RawData" SettingsPopup-EditForm-Modal="true"
                SettingsBehavior-ColumnResizeMode="Control"
                runat="server" AutoGenerateColumns="false">

                <Styles>
                    <Cell Wrap="False"></Cell>
                </Styles>
                <Settings HorizontalScrollBarMode="Visible" ShowFooter="true" ShowFilterRowMenu="true" UseFixedTableLayout="true"></Settings>
                <SettingsSearchPanel Visible="true" />
                <SettingsBehavior ColumnResizeMode="Control"></SettingsBehavior>
                <SettingsCustomizationDialog Enabled="true" />

                <SettingsPager Mode="ShowPager" PageSize="10">
                    <PageSizeItemSettings Visible="true" Items="10, 20, 30, 40 ,50" ShowAllItem="true" />
                </SettingsPager>

                <ClientSideEvents ToolbarItemClick="OnToolbarItemClick" />

                <SettingsDataSecurity AllowEdit="true" />
                <SettingsEditing Mode="PopupEditForm"></SettingsEditing>
                <SettingsPopup EditForm-HorizontalAlign="WindowCenter" EditForm-VerticalAlign="WindowCenter"></SettingsPopup>
               
                
                 <Settings ShowGroupPanel="true" />

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
                    <dx:BootstrapGridViewCommandColumn ShowEditButton="true" />
                    
                    <dx:GridViewDataTextColumn FieldName="ItemID" CellStyle-Wrap="False" ReadOnly="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="FiscalYear"  CellStyle-Wrap="False" ReadOnly="true">
                        <Settings AllowHeaderFilter="True" />
                        <SettingsHeaderFilter Mode="CheckedList" >
                            
                        </SettingsHeaderFilter>
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Lotno" CellStyle-Wrap="False" ReadOnly="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn FieldName="ExpiryDate" CellStyle-Wrap="False" ReadOnly="true" PropertiesDateEdit-DisplayFormatString="{0:dd/MM/yyyy}" >
                         <PropertiesDateEdit >
                          <DropDownButton Visible="false"></DropDownButton>
                      </PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="AccountExecutive" CellStyle-Wrap="False" ReadOnly="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="TypeofRisk" CellStyle-Wrap="False" ReadOnly="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Client" CellStyle-Wrap="False" ReadOnly="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ClientName" CellStyle-Wrap="False" ReadOnly="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="UWShort" CellStyle-Wrap="False" ReadOnly="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="PolicyNo" CellStyle-Wrap="False" ReadOnly="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="EndorsementNo" CellStyle-Wrap="False" ReadOnly="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="SumInsured" CellStyle-Wrap="False" ReadOnly="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Premium" CellStyle-Wrap="False" ReadOnly="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn FieldName="Datereceived" CellStyle-Wrap="False" ReadOnly="true"  PropertiesDateEdit-DisplayFormatString="{0:dd/MM/yyyy}">
                      <PropertiesDateEdit >
                          <DropDownButton Visible="false"></DropDownButton>
                      </PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataDateColumn FieldName="EffectiveDate" CellStyle-Wrap="False" ReadOnly="true"  PropertiesDateEdit-DisplayFormatString="{0:dd/MM/yyyy}">
                         <PropertiesDateEdit >
                          <DropDownButton Visible="false"></DropDownButton>
                      </PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="Remark" CellStyle-Wrap="False" ReadOnly="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="ClientGroup" CellStyle-Wrap="False" ReadOnly="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="GroupName" CellStyle-Wrap="False" ReadOnly="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Underwriter" CellStyle-Wrap="False" ReadOnly="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="InvRefNo" CellStyle-Wrap="False" ReadOnly="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Complusary" CellStyle-Wrap="False" ReadOnly="true"></dx:GridViewDataTextColumn>
                    <dx:GridViewDataDateColumn FieldName="LastUpdate" CellStyle-Wrap="False" PropertiesDateEdit-DisplayFormatString="{0:dd/MM/yyyy}">
                         <PropertiesDateEdit >
                          <DropDownButton Visible="false"></DropDownButton>
                      </PropertiesDateEdit>
                    </dx:GridViewDataDateColumn>
                    <dx:GridViewDataTextColumn FieldName="RemarkFromAE" CellStyle-Wrap="False" >
                        <PropertiesTextEdit>
                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                        </PropertiesTextEdit>
                        <EditFormSettings ColumnSpan="2" />
                    </dx:GridViewDataTextColumn>

                </Columns>
            </dx:ASPxGridView>



        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>
