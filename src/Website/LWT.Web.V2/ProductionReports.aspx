<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProductionReports.aspx.vb" Inherits="ProductionReports" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">
        function ShowDrillDown() {
            var mainElement = pivotGrid.GetMainElement();
            DrillDownWindow.ShowAtPos(ASPxClientUtils.GetAbsoluteX(mainElement), ASPxClientUtils.GetAbsoluteY(mainElement));
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
                Modal="True">
            </dx:ASPxLoadingPanel>

            <input runat="server" id="ColumnIndex" type="hidden" enableviewstate="true" />
            <input runat="server" id="RowIndex" type="hidden" enableviewstate="true" />



            <dx:ASPxRoundPanel ID="pnEnquiry" ClientInstanceName="pnReport" EnableAnimation="true" ShowCollapseButton="false" AllowCollapsingByHeaderClick="false"
                runat="server" Width="100%">

                <PanelCollection>
                    <dx:PanelContent>


                        <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="600" Width="100%">
                            <Panes>
                                <dx:SplitterPane MinSize="340px" MaxSize="600px" Size="300px" AutoWidth="true" AutoHeight="True" ShowCollapseBackwardButton="True">
                                    <ContentCollection>
                                        <dx:SplitterContentControl ID="SplitterContentControl1" runat="server">
                                            <dx:ASPxPivotCustomizationControl ID="ASPxPivotCustomizationControl1"
                                                ClientInstanceName="ASPxPivotCustomizationControl1"
                                                runat="server"
                                                Layout="StackedDefault"
                                                AllowedLayouts="TopPanelOnly"
                                                Height="600px" Width="100%"
                                                AllowSort="true"
                                                AllowFilter="true"
                                                DeferredUpdates="false"
                                                ASPxPivotGridID="pivotGrid">
                                            </dx:ASPxPivotCustomizationControl>

                                            <br />

                                            <dx:ASPxButton ID="btnSaveBI" Image-IconID="actions_save_16x16devav"
                                                ClientInstanceName="btnSaveBI"
                                                runat="server" Text="Save BI"
                                                AutoPostBack="false">
                                                <ClientSideEvents Click="function(s,e) {

                                                    LoadingPanel.Show();
                                                    cbSaveBI.PerformCallback('');
                                            }" />

                                                <Image IconID="actions_save_16x16devav"></Image>
                                            </dx:ASPxButton>


                                            <dx:ASPxCallback ID="cbSaveBI" runat="server" ClientInstanceName="cbSaveBI">
                                                <ClientSideEvents
                                                    CallbackComplete="function(s, e) { LoadingPanel.Hide(); }" />
                                            </dx:ASPxCallback>

                                            <dx:ASPxButton ID="btnExportData" runat="server" RenderMode="Button"
                                                Width="90px" Text="Export" AutoPostBack="false" CausesValidation="false">
                                                <Image IconID="export_exporttoxls_16x16"></Image>
                                            </dx:ASPxButton>

                                            <dx:ASPxButton ID="btnChart" runat="server" RenderMode="Button"
                                                Width="90px" Text="Chart" AutoPostBack="true" CausesValidation="false">
                                                <Image IconID="chart_chart_16x16office2013"></Image>
                                                <ClientSideEvents Click="function(s, e) { LoadingPanel.Show(); }" />
                                            </dx:ASPxButton>

                                        </dx:SplitterContentControl>
                                    </ContentCollection>
                                </dx:SplitterPane>
                                <dx:SplitterPane>
                                    <ContentCollection>
                                        <dx:SplitterContentControl ID="SplitterContentControl2" AutoWidth="true" AutoHeight="True" runat="server">
                                             
                                            <dx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" ASPxPivotGridID="pivotGrid" runat="server">
                                            </dx:ASPxPivotGridExporter>

                                            <dx:ASPxPivotGrid ID="pivotGrid" runat="server"
                                                CustomizationFieldsLeft="600"
                                                CustomizationFieldsTop="400"
                                                ClientInstanceName="pivotGrid"
                                                Width="100%" Prefilter-Enabled="True"
                                                OptionsCustomization-CustomizationFormStyle="Excel2007"
                                                ClientIDMode="AutoID" OptionsData-AutoExpandGroups="True"
                                                EnableTheming="True"
                                                OLAPConnectionString="<%$ ConnectionStrings:ProductionCubeConnection %>"
                                                Theme="Office2010Blue">

                                                <Fields>

                                                    <dx:PivotGridField ID="TotalClaim" Area="DataArea" DisplayFolder="Rawdata" Caption="Total Claim" FieldName="[Measures].[Net Claim]">
                                                    </dx:PivotGridField>

                                                    <dx:PivotGridField ID="LotNo" Area="RowArea" Caption="[Lot No]" FieldName="[Dim BDMS].[Lot No].[Lot No]" Name="[Lot No]">
                                                    </dx:PivotGridField>

                                                </Fields>


                                                <ClientSideEvents EndCallback="function(s, e) { 
							                        if( s.cpShowDrillDownWindow )
								                        GridView.SetVisible(true);
						                        }" />
                                                <OptionsView HorizontalScrollBarMode="Auto"
                                                    ShowColumnHeaders="True"
                                                    ShowDataHeaders="True"
                                                    ShowFilterHeaders="False"
                                                    ShowRowHeaders="True"
                                                    ShowGrandTotalsForSingleValues="True"
                                                    ShowColumnGrandTotals="True"
                                                    ShowRowGrandTotals="True"
                                                    ShowColumnTotals="False" />


                                                <OptionsCustomization CustomizationFormStyle="Excel2007" DeferredUpdates="True"></OptionsCustomization>


                                                <OptionsPager AlwaysShowPager="True" Position="Top" RowsPerPage="20" PageSizeItemSettings-Position="Left" PagerAlign="Left">
                                                    <Summary Visible="False" />
                                                    <PageSizeItemSettings Visible="True" Items="20, 30, 40, 50" ShowAllItem="true">
                                                    </PageSizeItemSettings>
                                                </OptionsPager>

                                                <OptionsData AutoExpandGroups="True"></OptionsData>
                                                <OptionsBehavior BestFitMode="Cell" />
                                                <OptionsFilter NativeCheckBoxes="False" />
                                                <Styles GrandTotalCellStyle-Font-Bold="true" TotalCellStyle-Font-Bold="true">
                                                    <TotalCellStyle Font-Bold="True"></TotalCellStyle>
                                                    <GrandTotalCellStyle Font-Bold="True"></GrandTotalCellStyle>
                                                </Styles>


                                              
                                            </dx:ASPxPivotGrid>




                                        </dx:SplitterContentControl>
                                    </ContentCollection>
                                </dx:SplitterPane>
                            </Panes>
                        </dx:ASPxSplitter>


                    </dx:PanelContent>
                </PanelCollection>
            </dx:ASPxRoundPanel>

            <dx:ASPxPopupControl ID="popChart" ClientInstanceName="popChart" runat="server"
                CloseAction="CloseButton"
                Modal="True"
                PopupHorizontalAlign="WindowCenter"
                HeaderText="Pivot Fields"
                AllowDragging="True">
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">


                        <dx:ASPxComboBox ID="ChartType"
                            runat="server"
                            Caption="ChartType"
                            AutoPostBack="True"
                            ValueType="System.String" />

                        <dxcharts:WebChartControl ID="WebChart"
                            ClientInstanceName="WebChart" runat="server"
                            Width="830px" Height="300px">
                        </dxcharts:WebChartControl>



                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>






            <%--            <dx:ASPxPopupControl ID="ASPxPopupControl1" Modal="true" runat="server" Height="1px"
                AutoUpdatePosition="true"
                EnableAnimation="true"
                EnableCallbackAnimation="true"
                AllowDragging="True" ClientInstanceName="DrillDownWindow" Left="200" Top="200"
                CloseAction="OuterMouseClick" Width="153px" HeaderText="Drill Down Window">
                <ContentCollection>
                    <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">

                        <dx:ASPxGridView ID="ASPxGridView1" runat="server"
                            AutoGenerateColumns="true" Styles-Cell-Wrap="False"
                            ClientInstanceName="GridView">
                            <SettingsLoadingPanel Mode="ShowAsPopup" />
                            <ClientSideEvents EndCallback="function(s, e) { 
							if( s.cpShowDrillDownWindow )
								GridView.SetVisible(true);
						}" />
                             
                           

                            <Styles>
                                <Header ImageSpacing="5px" SortingImageSpacing="5px" />
                            </Styles>
                        </dx:ASPxGridView>

                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>--%>
        </div>
    </form>
</body>
</html>
