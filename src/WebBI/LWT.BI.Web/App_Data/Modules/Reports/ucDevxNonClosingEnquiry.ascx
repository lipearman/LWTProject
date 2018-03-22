<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxNonClosingEnquiry.ascx.vb" Inherits="Modules_ucDevxNonClosingEnquiry" %>

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

<asp:SqlDataSource ID="LinqServerModeDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsSIBISDBConnectionString %>" SelectCommand="select * from V_NonClosing "></asp:SqlDataSource>


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
    HeaderText="Non Closing Date" runat="server" Width="100%">
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
                <dx:ASPxGridView ID="GridData" ClientInstanceName="GridData" runat="server" AutoGenerateColumns="False"
                    KeyFieldName="BrefII" Width="100%"
                    EnableCallBacks="true" SettingsBehavior-AllowDragDrop="false"
                    EnableCallbackAnimation="true"
                    EnableCallbackCompression="true"
                    EnableRowsCache="true" Settings-HorizontalScrollBarMode="Visible"
                    DataSourceID="LinqServerModeDataSource1">

                    <Settings ShowHeaderFilterButton="true" ShowGroupPanel="false" ShowFooter="True" ShowGroupFooter="VisibleAlways" />

                    <SettingsPager PageSize="20">
                        <PageSizeItemSettings Visible="true" Items="20,50,100,250" />
                    </SettingsPager>
                     <SettingsResizing ColumnResizeMode="Control" />
                    <SettingsPopup>
                        <HeaderFilter Height="200" />
                    </SettingsPopup>

                    <SettingsBehavior EnableRowHotTrack="true" AllowFixedGroups="true" />
                    <SettingsSearchPanel Visible="false" AllowTextInputTimer="true" Delay="2000" />
                    <Settings ShowGroupPanel="True" ShowGroupButtons="false" />


                    <GroupSummary>
                        <dx:ASPxSummaryItem FieldName="NetPremium" SummaryType="Sum" ShowInGroupFooterColumn="NetPremium" DisplayFormat="{0:n2}" />
                    </GroupSummary>
                    <TotalSummary>
                        <dx:ASPxSummaryItem FieldName="NetPremium" SummaryType="Sum" DisplayFormat="{0:n2}" />
                    </TotalSummary>
                    
                    <Columns>
                        <dx:GridViewDataTextColumn FieldName="System" CellStyle-Wrap="False" Caption="System" Settings-AllowGroup="False" GroupIndex="0">
                        <Settings HeaderFilterMode="CheckedList" />
                        </dx:GridViewDataTextColumn>

                        <dx:GridViewDataTextColumn FieldName="ClosingDate" CellStyle-Wrap="False" Caption="ClosingDate" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False" />
                        <dx:GridViewDataTextColumn FieldName="days" CellStyle-Wrap="False" Caption="days" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>
                        <dx:GridViewDataTextColumn FieldName="Aging" CellStyle-Wrap="False" Caption="Aging" Settings-AllowGroup="False">
                        <Settings HeaderFilterMode="CheckedList" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="TypeOfClient" CellStyle-Wrap="False" Caption="TypeOfClient" Settings-AllowGroup="False">
                        <Settings HeaderFilterMode="CheckedList" />
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="ClientCode" CellStyle-Wrap="False" Caption="ClientCode" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>
                        <dx:GridViewDataTextColumn FieldName="BriefII" CellStyle-Wrap="False" Caption="BriefII" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>
                        <dx:GridViewDataTextColumn FieldName="ClientName" CellStyle-Wrap="False" Caption="ClientName" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>
                        <dx:GridViewDataTextColumn FieldName="CHASSIS" CellStyle-Wrap="False" Caption="CHASSIS" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>
                        <dx:GridViewDataTextColumn FieldName="EngineNo" CellStyle-Wrap="False" Caption="EngineNo" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>

                        <dx:GridViewDataTextColumn FieldName="PeriodFrom" CellStyle-Wrap="False" Caption="PeriodFrom" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>
                        <dx:GridViewDataTextColumn FieldName="PeriodTo" CellStyle-Wrap="False" Caption="PeriodTo" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False"/>
                        <dx:GridViewDataTextColumn FieldName="NetPremium" PropertiesTextEdit-DisplayFormatString="{0:n2}" CellStyle-Wrap="False" Caption="NetPremium"  Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False">

    
                        </dx:GridViewDataTextColumn>
                          <dx:GridViewDataTextColumn FieldName="Status" CellStyle-Wrap="False" Caption="Status" Settings-AllowFilterBySearchPanel="False" Settings-AllowHeaderFilter="False" Settings-AllowGroup="False">
                               <Settings HeaderFilterMode="CheckedList" />
                        </dx:GridViewDataTextColumn>

                    </Columns>

                </dx:ASPxGridView>


                <dx:ASPxGridViewExporter runat="server" ID="GridExporter" GridViewID="GridData" />
            </div>



        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>
