<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxOutStandingAPDEnquiry.ascx.vb" Inherits="Modules_ucDevxOutStandingAPDEnquiry" %>
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

<asp:SqlDataSource ID="LinqServerModeDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsSIBISDBConnectionString %>" SelectCommand="select * from V_OutstandingPremium_APD order by AExec"></asp:SqlDataSource>


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
    HeaderText="Outstanding Insurance Premium (Error Include) - Compare days with Eff.Date and Statement Closing Date" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>
            <div style="text-align: left">
                <dx:ASPxButton ID="btnResetFilter" runat="server" RenderMode="Button"
                    Width="90px" Text="Reset Filter" AutoPostBack="false" CausesValidation="false">
                    <Image IconID="actions_refresh_32x32"></Image>
                    <ClientSideEvents Click="function(s, e) { 

                                    GridData.ClearFilter();                                             
                      }" />
                </dx:ASPxButton>
                <dx:ASPxButton ID="btnExportData" runat="server" RenderMode="Button"
                    Width="90px" Text="Export" AutoPostBack="false" CausesValidation="false">
                    <Image IconID="export_exporttoxlsx_32x32"></Image>
                </dx:ASPxButton>

                 
                <br />
                <br />
                <dx:ASPxGridView ID="GridData" ClientInstanceName="GridData" runat="server" 
                    AutoGenerateColumns="False"
                    KeyFieldName="ID" Width="100%" Settings-HorizontalScrollBarMode="Visible"  
                    EnableCallBacks="true" SettingsBehavior-AllowDragDrop="false"
                    EnableCallbackAnimation="true"
                    EnableCallbackCompression="true"
                    EnableRowsCache="true"
                    DataSourceID="LinqServerModeDataSource1">

                    <Settings ShowHeaderFilterButton="true" ShowGroupPanel="false" ShowFooter="True" ShowGroupFooter="VisibleAlways" />
                    <SettingsResizing ColumnResizeMode="Control" />
                    <SettingsPager PageSize="20">
                        <PageSizeItemSettings Visible="true" Items="20,50,100,250" />
                    </SettingsPager>

                    <SettingsPopup>
                        <HeaderFilter Height="200" />
                    </SettingsPopup>

                    <SettingsBehavior EnableRowHotTrack="true" AllowFixedGroups="true"  />
                    <SettingsSearchPanel Visible="false" AllowTextInputTimer="true" Delay="2000" />
                    <Settings ShowGroupPanel="True" ShowGroupButtons="false" />


                    <GroupSummary>
                        <dx:ASPxSummaryItem FieldName="Premium" SummaryType="Sum" ShowInGroupFooterColumn="Premium" />
                        <dx:ASPxSummaryItem FieldName="GrossPremium" SummaryType="Sum" ShowInGroupFooterColumn="GrossPremium" />
                        <dx:ASPxSummaryItem FieldName="Aging0_30days" SummaryType="Sum" ShowInGroupFooterColumn="Aging0_30days" />
                        <dx:ASPxSummaryItem FieldName="Aging31_60days" SummaryType="Sum" ShowInGroupFooterColumn="Aging31_60days" />
                        <dx:ASPxSummaryItem FieldName="Aging61_90days" SummaryType="Sum" ShowInGroupFooterColumn="Aging61_90days" />
                        <dx:ASPxSummaryItem FieldName="Aging91_365days" SummaryType="Sum" ShowInGroupFooterColumn="Aging91_365days" />
                        <dx:ASPxSummaryItem FieldName="AgingOver1Year" SummaryType="Sum" ShowInGroupFooterColumn="AgingOver1Year" />
                        <dx:ASPxSummaryItem FieldName="Outstanding" SummaryType="Sum" ShowInGroupFooterColumn="Outstanding" />
                        <dx:ASPxSummaryItem FieldName="BalanceAmount" SummaryType="Sum" />
                    </GroupSummary>
                    <TotalSummary>
                        <dx:ASPxSummaryItem FieldName="Premium" SummaryType="Sum" />
                        <dx:ASPxSummaryItem FieldName="GrossPremium" SummaryType="Sum" />
                        <dx:ASPxSummaryItem FieldName="Aging0_30days" SummaryType="Sum" />
                        <dx:ASPxSummaryItem FieldName="Aging31_60days" SummaryType="Sum" />
                        <dx:ASPxSummaryItem FieldName="Aging61_90days" SummaryType="Sum" />
                        <dx:ASPxSummaryItem FieldName="Aging91_365days" SummaryType="Sum" />
                        <dx:ASPxSummaryItem FieldName="AgingOver1Year" SummaryType="Sum" />
                        <dx:ASPxSummaryItem FieldName="Outstanding" SummaryType="Sum" />
                        <dx:ASPxSummaryItem FieldName="BalanceAmount" SummaryType="Sum" />
                    </TotalSummary>

                    <Columns>


                        <%--
Asat_Month
,DivisionCode
,AExec
,ClientCode
,ReferenceType
,Asat
,Name
,Underwriter
,ClientGroup
,BriefDescription
,PolicyNo
,ReferenceNo
,Attachment
,Aging0_30days
,Aging31_60days
,Aging61_90days
,Aging91_365days
,AgingOver1Year
,Outstanding
,Class
,Premium
,StampDuty
,VAT
,InvoiceNo
,AExecName
,RiskGroupII
,CrossReference
                                                
                                                
                        --%>
                        <dx:GridViewDataTextColumn FieldName="DivisionCode" CellStyle-Wrap="False" Caption="DivisionCode" Settings-AllowGroup="False" GroupIndex="0">
                            <Settings HeaderFilterMode="CheckedList" />
                        </dx:GridViewDataTextColumn>

                        <dx:GridViewDataTextColumn FieldName="ClientCode" CellStyle-Wrap="False" Caption="ClientCode" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False" />
                        <dx:GridViewDataTextColumn FieldName="Name" CellStyle-Wrap="False" Caption="Name" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>
                        <dx:GridViewDataTextColumn FieldName="DivisionCode" CellStyle-Wrap="False" Caption="DIV" Settings-AllowGroup="False">
                            <Settings HeaderFilterMode="CheckedList" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="AExec" CellStyle-Wrap="False" Caption="AE" Settings-AllowGroup="False">
                            <Settings HeaderFilterMode="CheckedList" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ClientGroup" CellStyle-Wrap="False" Caption="Group" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>

                        <dx:GridViewDataTextColumn FieldName="Underwriter" CellStyle-Wrap="False" Caption="Underwriter" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>
                        <dx:GridViewDataTextColumn FieldName="Class" CellStyle-Wrap="False" Caption="Class" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>
                        <dx:GridViewDataTextColumn FieldName="RiskGroupII" CellStyle-Wrap="False" Caption="RiskGroupII" Settings-AllowFilterBySearchPanel="False" Settings-AllowGroup="False">
                            <Settings HeaderFilterMode="CheckedList" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="PolicyNo" CellStyle-Wrap="False" Caption="Policy" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>
                        <dx:GridViewDataTextColumn FieldName="BriefDescription" CellStyle-Wrap="False" Caption="Brief" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>
                        <dx:GridViewDataTextColumn FieldName="Effective" CellStyle-Wrap="False" Caption="Effective" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>
                        <dx:GridViewDataTextColumn FieldName="InvoiceNo" CellStyle-Wrap="False" Caption="DN" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>
                        <dx:GridViewDataTextColumn FieldName="Premium" PropertiesTextEdit-DisplayFormatString="{0:n2}" CellStyle-Wrap="False" Caption="NetPremium" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>
                        <dx:GridViewDataTextColumn FieldName="GrossPremium" PropertiesTextEdit-DisplayFormatString="{0:n2}" CellStyle-Wrap="False" Caption="TotalPremium" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>
                        <dx:GridViewDataTextColumn FieldName="Days" CellStyle-Wrap="False" Caption="Days" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>
                        <dx:GridViewDataTextColumn FieldName="Aging0_30days" PropertiesTextEdit-DisplayFormatString="{0:n0}" CellStyle-Wrap="False" Caption="Aging0_30days" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>
                        <dx:GridViewDataTextColumn FieldName="Aging31_60days" PropertiesTextEdit-DisplayFormatString="{0:n2}" CellStyle-Wrap="False" Caption="Aging31_60days" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>
                        <dx:GridViewDataTextColumn FieldName="Aging61_90days" PropertiesTextEdit-DisplayFormatString="{0:n2}" CellStyle-Wrap="False" Caption="Aging61_90days" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>
                        <dx:GridViewDataTextColumn FieldName="Aging91_365days" PropertiesTextEdit-DisplayFormatString="{0:n2}" CellStyle-Wrap="False" Caption="Aging91_365days" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>
                        <dx:GridViewDataTextColumn FieldName="AgingOver1Year" PropertiesTextEdit-DisplayFormatString="{0:n2}" CellStyle-Wrap="False" Caption="AgingOver1Year" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>
                        <dx:GridViewDataTextColumn FieldName="BalanceAmount" PropertiesTextEdit-DisplayFormatString="{0:n2}" CellStyle-Wrap="False" Caption="BalanceAmount" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>
                        <dx:GridViewDataTextColumn FieldName="Outstanding" PropertiesTextEdit-DisplayFormatString="{0:n2}" CellStyle-Wrap="False" Caption="Amount" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>
                        <dx:GridViewDataTextColumn FieldName="CrossReference" CellStyle-Wrap="False" Caption="CrossReference" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>
                         <dx:GridViewDataTextColumn FieldName="Department" CellStyle-Wrap="False" Caption="Department" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>
                        <dx:GridViewDataTextColumn FieldName="Aging" CellStyle-Wrap="False" Caption="Aging" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>


                        <dx:GridViewDataTextColumn FieldName="BriefIII" CellStyle-Wrap="False" Caption="Chassis" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>

                        <dx:GridViewDataTextColumn FieldName="TRNO" CellStyle-Wrap="False" Caption="TRNo" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>

                        <dx:GridViewDataTextColumn FieldName="STATUS" CellStyle-Wrap="False" Caption="STATUS" Settings-AllowFilterBySearchPanel="False" Settings-AllowGroup="False" >
                             <Settings HeaderFilterMode="CheckedList" />
                        </dx:GridViewDataTextColumn>

                        <dx:GridViewDataTextColumn FieldName="Result" CellStyle-Wrap="False" Caption="Result" Settings-AllowFilterBySearchPanel="False" Settings-AllowGroup="False" >
                             <Settings HeaderFilterMode="CheckedList" />
                        </dx:GridViewDataTextColumn>
                        
                          <dx:GridViewDataTextColumn FieldName="DealerName" CellStyle-Wrap="False" Caption="DealerName" Settings-AllowFilterBySearchPanel="False" Settings-AllowGroup="False" >
                             <Settings HeaderFilterMode="CheckedList" />
                        </dx:GridViewDataTextColumn>
<dx:GridViewDataTimeEditColumn FieldName="Effective" PropertiesTimeEdit-DisplayFormatString="{0:dd/MM/yyyy}" CellStyle-Wrap="False" Caption="Effective2" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>
                        <%--    <dx:GridViewDataTextColumn FieldName="AExecName" CellStyle-Wrap="False" Caption="AExecName" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" />
                        <dx:GridViewDataTextColumn FieldName="Asat" CellStyle-Wrap="False" Caption="Asat" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ReferenceType" CellStyle-Wrap="False" Caption="ReferenceType" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" />
                        <dx:GridViewDataTextColumn FieldName="Asat" CellStyle-Wrap="False" Caption="Asat" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" />
                        <dx:GridViewDataTextColumn FieldName="Attachment" CellStyle-Wrap="False" Caption="Attachment" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" />
                        <dx:GridViewDataTextColumn FieldName="StampDuty" CellStyle-Wrap="False" Caption="StampDuty" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" />
                        <dx:GridViewDataTextColumn FieldName="VAT" CellStyle-Wrap="False" Caption="VAT" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" />

                        <dx:GridViewDataTextColumn FieldName="InvoiceNo" CellStyle-Wrap="False" Caption="InvoiceNo" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" />
                        <dx:GridViewDataTextColumn FieldName="CrossReference" CellStyle-Wrap="False" Caption="CrossReference" Settings-AllowFilterBySearchPanel="False">
                            <Settings HeaderFilterMode="CheckedList" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="IDKEY" CellStyle-Wrap="False" Caption="IDKEY" Settings-AllowHeaderFilter="False" />
                        --%>
                    </Columns>

                </dx:ASPxGridView>


                <dx:ASPxGridViewExporter runat="server" ID="GridExporter" GridViewID="GridData" />
            </div>



        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>
