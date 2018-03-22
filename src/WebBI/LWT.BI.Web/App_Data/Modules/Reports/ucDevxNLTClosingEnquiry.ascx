<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxNLTClosingEnquiry.ascx.vb" Inherits="Modules_ucDevxNLTClosingEnquiry" %>

<%-- <script type="text/javascript">
     function ApplyFilter(dateFrom, dateTo) {
         var d1 = dateFrom.GetText();
         var d2 = dateTo.GetText();
         if (d1 == "" || d2 == "")
             return;
         GridData.AutoFilterByColumn("AccidentDate", d1 + "|" + d2);
     }
    </script>--%>
<dx:LinqServerModeDataSource ID="LinqServerModeDataSource1" runat="server" DefaultSorting="AID desc" ContextTypeName="DataClasses_NLTDB_LWTReportsExt" TableName="V_NLT_Closings" />

<%--<asp:SqlDataSource ID="LinqServerModeDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsSIBISConnectionString %>"></asp:SqlDataSource>--%>


<style>
    .dxflGroup_MetropolisBlue
    {
        padding: 0px 0px;
        width: 100%;
    }

    .dxflGroupCell_MetropolisBlue
    {
        padding: 0 0px;
    }
</style>

<dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
    Modal="True">
</dx:ASPxLoadingPanel>

<dx:ASPxRoundPanel ID="pnEnquiry" ClientInstanceName="pnEnquiry" EnableAnimation="true" ShowCollapseButton="false" AllowCollapsingByHeaderClick="false"
    HeaderText="รายงาน Closing NLT " runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>




            <dx:ASPxFormLayout ID="frmSearch" ClientInstanceName="frmSearch" runat="server" AlignItemCaptionsInAllGroups="True" ColCount="1" RequiredMarkDisplayMode="None" ShowItemCaptionColon="False">
                <Items>
                    <dx:LayoutGroup Caption="Search" ShowCaption="False" GroupBoxDecoration="None" ColCount="3" Width="330px">
                        <Items>
                            <%--                                                    <dx:LayoutItem ShowCaption="False" Caption="บริษัทประกัน">
                                                        <LayoutItemNestedControlCollection>
                                                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer18" runat="server" SupportsDisabledAttribute="True">

                                                                <dx:ASPxComboBox ID="CompanyCode" Width="250" ClientInstanceName="CompanyCode" runat="server" ValueType="System.String">
                                                                    
                                                                </dx:ASPxComboBox>


                                                            </dx:LayoutItemNestedControlContainer>
                                                        </LayoutItemNestedControlCollection>
                                                    </dx:LayoutItem>--%>
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
                            <dx:LayoutItem Caption=" &nbsp;">
                                <LayoutItemNestedControlCollection>
                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server" SupportsDisabledAttribute="True">
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
                        </Items>
                    </dx:LayoutGroup>
                    <dx:LayoutItem ShowCaption="False" HorizontalAlign="Center" Width="100%">
                        <LayoutItemNestedControlCollection>
                            <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer8" runat="server"
                                SupportsDisabledAttribute="True"
                                AutoPostBack="False">
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
                                        <Image IconID="export_exporttoxls_32x32"></Image>
                                    </dx:ASPxButton>


                                </div>
                            </dx:LayoutItemNestedControlContainer>
                        </LayoutItemNestedControlCollection>
                    </dx:LayoutItem>



                </Items>
            </dx:ASPxFormLayout>

            <br />
            <dx:ASPxGridView ID="GridData" ClientInstanceName="GridData" runat="server" AutoGenerateColumns="False"
                KeyFieldName="AID" Width="100%"
                EnableCallBacks="true" Settings-HorizontalScrollBarMode="Visible"
                EnableCallbackAnimation="true"
                EnableCallbackCompression="true"
                EnableRowsCache="true"
                DataSourceID="LinqServerModeDataSource1">

                <SettingsPager PageSize="10">

                    <PageSizeItemSettings Visible="true" Items="10, 20, 30, 50" />
                </SettingsPager>
                <Settings ShowFooter="True" ShowHeaderFilterButton="true" />
                <SettingsPopup>
                    <HeaderFilter Height="200" />
                </SettingsPopup>
                <SettingsResizing ColumnResizeMode="Control" />
                <SettingsBehavior EnableRowHotTrack="true" />
                <SettingsSearchPanel Visible="false" AllowTextInputTimer="true" Delay="2000" />



                <Columns>


                    <dx:GridViewDataTextColumn FieldName="TempID" CellStyle-Wrap="False" Caption="TempID" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="ClosingDate" CellStyle-Wrap="False" Caption="ClosingDate" Settings-AllowHeaderFilter="False" />

                    <dx:GridViewDataTextColumn FieldName="CarGroupName" CellStyle-Wrap="False" Caption="CarGroupName">
                        <Settings HeaderFilterMode="CheckedList" />
                    </dx:GridViewDataTextColumn>

                    <dx:GridViewDataTextColumn FieldName="GroupName" CellStyle-Wrap="False" Caption="GroupName" Settings-AllowHeaderFilter="False" />

                    <dx:GridViewDataTextColumn FieldName="Code1" CellStyle-Wrap="False" Caption="InsurerCode" Settings-AllowFilterBySearchPanel="False">
                        <Settings HeaderFilterMode="CheckedList" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="DealerCode" CellStyle-Wrap="False" Caption="DealerCode" Settings-AllowFilterBySearchPanel="False">
                        <Settings HeaderFilterMode="CheckedList" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="DealerName" CellStyle-Wrap="False" Caption="DealerName" Settings-AllowFilterBySearchPanel="False">
                        <Settings HeaderFilterMode="CheckedList" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="NLTDealerCode" CellStyle-Wrap="False" Caption="NLTDealerCode" Settings-AllowFilterBySearchPanel="False">
                        <Settings HeaderFilterMode="CheckedList" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="NLTArea" CellStyle-Wrap="False" Caption="NLTArea" Settings-AllowFilterBySearchPanel="False">
                        <Settings HeaderFilterMode="CheckedList" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="RegionName" CellStyle-Wrap="False" Caption="RegionName" Settings-AllowFilterBySearchPanel="False">
                        <Settings HeaderFilterMode="CheckedList" />
                    </dx:GridViewDataTextColumn>




                    <%--<dx:GridViewDataTextColumn FieldName="IDKEY" CellStyle-Wrap="False" Caption="IDKEY" Settings-AllowHeaderFilter="False" />--%>
                </Columns>

            </dx:ASPxGridView>


            <dx:ASPxGridViewExporter runat="server" ID="GridExporter" GridViewID="GridData" />


        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>
