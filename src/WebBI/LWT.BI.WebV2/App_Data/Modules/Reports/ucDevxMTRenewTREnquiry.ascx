<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxMTRenewTREnquiry.ascx.vb" Inherits="Modules_ucDevxMTRenewTREnquiry" %>

<%-- <script type="text/javascript">
     function ApplyFilter(dateFrom, dateTo) {
         var d1 = dateFrom.GetText();
         var d2 = dateTo.GetText();
         if (d1 == "" || d2 == "")
             return;
         GridData.AutoFilterByColumn("AccidentDate", d1 + "|" + d2);
     }
    </script>--%>
<%--<dx:LinqServerModeDataSource ID="LinqServerModeDataSource1" runat="server" DefaultSorting="BalanceAmount desc" ContextTypeName="DataClasses_LWTReport_SIBISExt" TableName="V_MT_RENEW_TRs" />--%>

<asp:SqlDataSource ID="LinqServerModeDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsSIBISDBConnectionString %>"></asp:SqlDataSource>


<style>
    .dxflGroup_MetropolisBlue {
        padding: 0px 0px;
        width: 100%;
    }

    .dxflGroupCell_MetropolisBlue {
        padding: 0 0px;
    }
</style>

<dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
    Modal="True">
</dx:ASPxLoadingPanel>

<dx:ASPxRoundPanel ID="pnEnquiry" ClientInstanceName="pnEnquiry" EnableAnimation="true" ShowCollapseButton="false" AllowCollapsingByHeaderClick="false"
    HeaderText="รายงานติดตามการชำระเงินของลูกค้า (งานต่ออายุ) " runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxPageControl runat="server" ID="ASPxPageControl1" Paddings-Padding="0" Width="100%">
                <TabPages>

                    <dx:TabPage Text="รายการ Closing" Visible="true">
                        <ContentCollection>

                            <dx:ContentControl ID="ContentControl2" runat="server">

                                <div style="text-align: left">

                                    <dx:ASPxFormLayout ID="frmSearch" ClientInstanceName="frmSearch" runat="server" AlignItemCaptionsInAllGroups="True" ColCount="1" RequiredMarkDisplayMode="None" ShowItemCaptionColon="False">
                                        <Items>
                                            <dx:LayoutGroup Caption="Search" ShowCaption="False" GroupBoxDecoration="None" ColCount="3" Width="330px">
                                                <Items>
                                                    <dx:LayoutItem ShowCaption="False" Caption="บริษัทประกัน">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer18" runat="server" SupportsDisabledAttribute="True">

                                                                <dx:ASPxComboBox ID="CompanyCode" Width="220" ClientInstanceName="CompanyCode" runat="server" ValueType="System.String">
                                                                    <ValidationSettings ErrorDisplayMode="None" Display="Dynamic">
                                                                        <RequiredField IsRequired="True" />
                                                                    </ValidationSettings>

                                                                </dx:ASPxComboBox>


                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                    <dx:LayoutItem Caption="&nbsp;&nbsp;&nbsp;&nbsp;วันที่ Closing">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer
                                                                ID="LayoutItemNestedControlContainer23"
                                                                runat="server"
                                                                SupportsDisabledAttribute="True">
                                                                <dx:ASPxDateEdit ID="ClosingFrom" ClientInstanceName="ClosingFrom" runat="server">
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
                                                                <dx:ASPxDateEdit ID="ClosingTo" ClientInstanceName="ClosingTo" runat="server">
                                                                    <ValidationSettings ErrorDisplayMode="None" Display="Dynamic">
                                                                        <RequiredField IsRequired="True" />
                                                                    </ValidationSettings>

                                                                </dx:ASPxDateEdit>
                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>
                                                </Items>
                                            </dx:LayoutGroup>
                                            <dx:LayoutItem ShowCaption="False" HorizontalAlign="Center" Width="100%">
                                                <LayoutItemNestedControlCollection>
                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server"
                                                        SupportsDisabledAttribute="True"
                                                        AutoPostBack="False">

                                                        <dx:ASPxButton ID="btnSearch" runat="server" RenderMode="Button"
                                                            Width="90px" Text="Search" AutoPostBack="false" CausesValidation="true" ValidationContainerID="frmSearch">
                                                            <Image IconID="filter_filter_32x32"></Image>
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

                                                        <%--                                         <dx:ASPxButton ID="btnResetFilter" runat="server" RenderMode="Button"
                                                        Width="90px" Text="Reset Filter" AutoPostBack="false" CausesValidation="false">
                                                        <Image IconID="actions_refresh_32x32"></Image>
                                                        <ClientSideEvents Click="function(s, e) { 
                                            ASPxClientEdit.ClearEditorsInContainerById('frmSearch'); 
                                            GridData.ClearFilter();  
                                    //GridData.ApplySearchPanelFilter('');                                            
                                    }" />
                                                    </dx:ASPxButton>--%>

                                                    &nbsp;

                                                    <dx:ASPxButton ID="btnExportData" runat="server" RenderMode="Button"
                                                        Width="90px" Text="Export" AutoPostBack="false" CausesValidation="false">
                                                        <Image IconID="export_exporttoxls_32x32"></Image>
                                                    </dx:ASPxButton>

                                                        <%--  <dx:ASPxButton ID="btnViewReport" runat="server" RenderMode="Button"
                                                        Width="90px" Text="View Report" AutoPostBack="false" CausesValidation="false">
                                                        <Image IconID="programming_showtestreport_32x32"></Image>
                                                        <ClientSideEvents Click="function(s, e) {  
                        LoadingPanel.Show(); 
                        cbViewReport.PerformCallback('');
                        e.processOnServer = false;                   
                      }" />

                                                    </dx:ASPxButton>
                                                    <dx:ASPxCallback runat="server" ID="cbViewReport" ClientInstanceName="cbViewReport">
                                                        <ClientSideEvents CallbackComplete="function(s,e){
                    LoadingPanel.Hide();
                    if (e.result != '' && e.result != null) {
                       window.location.assign('LivePivot/wpt2/wpt2.aspx?id=' + e.result);                
                    } 
                }" />
                                                    </dx:ASPxCallback>--%>
                                                    </dx:LayoutItemNestedControlContainer>
                                                </LayoutItemNestedControlCollection>
                                            </dx:LayoutItem>



                                        </Items>
                                    </dx:ASPxFormLayout>

                                    <br />
                                    <dx:ASPxGridView ID="GridData" ClientInstanceName="GridData" runat="server" AutoGenerateColumns="False"
                                        KeyFieldName="IDKEY" Width="100%"
                                        EnableCallBacks="true" Settings-HorizontalScrollBarMode="Visible"
                                        EnableCallbackAnimation="true"
                                        EnableCallbackCompression="true"
                                        EnableRowsCache="true">
                                         <SettingsResizing ColumnResizeMode="Control" />
                                        <SettingsPager PageSize="50">
                                            
                                            <PageSizeItemSettings Visible="true" Items="50, 100, 150, 200" />
                                        </SettingsPager>
                                        <Settings ShowFooter="True" ShowHeaderFilterButton="true" />
                                        <SettingsPopup>
                                            <HeaderFilter Height="200" />
                                        </SettingsPopup>

                                        <SettingsBehavior EnableRowHotTrack="true" />
                                        <SettingsSearchPanel Visible="true" AllowTextInputTimer="true" Delay="2000" />



                                        <Columns>


                                            <%--
RiskGovernment
ClosingMonth
PreDateStart
BodyNo
ChkStatus
CarBrand
CarModel
CompanyCode
Underwriter
PeriodTo

AgentName
netpremium
Stamp
Vat
grosspremium--%>


                                            <dx:GridViewDataTextColumn FieldName="InvoiceNo" CellStyle-Wrap="False" Caption="เลข DN" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="ClientCode" CellStyle-Wrap="False" Caption="ClientCode" Settings-AllowHeaderFilter="False" />
                                            <dx:GridViewDataTextColumn FieldName="NameClient" CellStyle-Wrap="False" Caption="ชื่อผู้เอาประกัน" Settings-AllowHeaderFilter="False" />
                                            <dx:GridViewDataTextColumn FieldName="TRNO" CellStyle-Wrap="False" Caption="TRNO" Settings-AllowHeaderFilter="False" />
                                            <dx:GridViewDataTextColumn FieldName="TYPE" CellStyle-Wrap="False" Caption="TYPE" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" />

                                            <dx:GridViewDataTextColumn FieldName="PAIDAMOUNT" CellStyle-Wrap="False" Caption="ยอดที่ชำระ" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" />
                                            <dx:GridViewDataTextColumn FieldName="BalanceAmount" CellStyle-Wrap="False" Caption="คงค้าง" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" />
                                            <dx:GridViewDataTextColumn FieldName="OriginalAmount" CellStyle-Wrap="False" Caption="เบี้ยประกัน" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" />
                                            <dx:GridViewDataTextColumn FieldName="IDCar" CellStyle-Wrap="False" Caption="ทะเบียน" Settings-AllowHeaderFilter="False" />

                                            <dx:GridViewDataTextColumn FieldName="PolicyNo" CellStyle-Wrap="False" Caption="กรมธรรม์" Settings-AllowHeaderFilter="False" />
                                            <dx:GridViewDataTextColumn FieldName="BriefDescription" CellStyle-Wrap="False" Caption="BriefDescription" Settings-AllowHeaderFilter="False" />
                                            <dx:GridViewDataTextColumn FieldName="PeriodFrom" CellStyle-Wrap="False" Caption="PeriodFrom" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" />
                                            <dx:GridViewDataTextColumn FieldName="PeriodTo" CellStyle-Wrap="False" Caption="PeriodTo" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" />


                                            <dx:GridViewDataTextColumn FieldName="AgentCode" CellStyle-Wrap="False" Caption="AgentCode" Settings-AllowHeaderFilter="False" />
                                            <dx:GridViewDataTextColumn FieldName="CompanyName" CellStyle-Wrap="False" Caption="บริษัทประกัน" Settings-AllowHeaderFilter="False" />
                                            <dx:GridViewDataTextColumn FieldName="Class" CellStyle-Wrap="False" Caption="Class" Settings-AllowHeaderFilter="False" />
                                            <dx:GridViewDataTextColumn FieldName="ClosingDate" CellStyle-Wrap="False" Caption="วันที่ Closing" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" />
                                            <dx:GridViewDataTextColumn FieldName="IDCodeCRV" CellStyle-Wrap="False" Caption="เลขที่ MT" Settings-AllowHeaderFilter="False" />

                                            <dx:GridViewDataTextColumn FieldName="TRDATE" CellStyle-Wrap="False" Caption="TRDATE" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" />
                                            <dx:GridViewDataTextColumn FieldName="RVPDATE" CellStyle-Wrap="False" Caption="RVPDATE" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" />
                                            <dx:GridViewDataTextColumn FieldName="PURGEDATE" CellStyle-Wrap="False" Caption="PURGEDATE" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" />



                                            <%--<dx:GridViewDataTextColumn FieldName="IDKEY" CellStyle-Wrap="False" Caption="IDKEY" Settings-AllowHeaderFilter="False" />--%>
                                        </Columns>

                                    </dx:ASPxGridView>


                                    <dx:ASPxGridViewExporter runat="server" ID="GridExporter" GridViewID="GridData" />
                                </div>

                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>


                    <dx:TabPage Text="รายงาน Closing" Enabled="false" >
                        <ContentCollection>
                            <dx:ContentControl ID="ContentControl3" runat="server">
                            </dx:ContentControl>
                        </ContentCollection>
                    </dx:TabPage>

                </TabPages>

            </dx:ASPxPageControl>


        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>
