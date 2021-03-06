﻿<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Applications/AppMasterPage.master" CodeFile="Preview.aspx.vb" Inherits="applications_BI_Master_Preview" %>







<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 
    <input runat="server" id="hdBID" type="hidden" enableviewstate="true" />


    <dx:ASPxRoundPanel ID="pnEnquiry"  HeaderImage-IconID="businessobjects_boreport_32x32" ClientInstanceName="pnReport" EnableAnimation="true" ShowCollapseButton="false" AllowCollapsingByHeaderClick="false"
        runat="server" Width="100%">
        <PanelCollection>
            <dx:PanelContent>
    <table class="OptionsTable BottomMargin">
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="true" Text="Summary&nbsp;Column:"></dx:ASPxLabel>
                        </td>

                        <td>
                            <dx:ASPxCheckBox ID="cbShowColumnGrandTotals" runat="server" AutoPostBack="false" Text="ShowColumnGrandTotals" >
                                <ClientSideEvents CheckedChanged="function(s,e){
                                    pivotGrid.PerformCallback();
                                    }" />
                            </dx:ASPxCheckBox>
                        </td>
                        <td>
                            <dx:ASPxCheckBox ID="cbShowColumnTotals" runat="server" AutoPostBack="false" Text="ShowColumnTotals" >
                                <ClientSideEvents CheckedChanged="function(s,e){
                                    pivotGrid.PerformCallback();
                                    }" />
                            </dx:ASPxCheckBox>

                        </td>
                        <td>

                            <dx:ASPxCheckBox ID="cbShowTotalsForSingleValues" runat="server" AutoPostBack="false" Text="ShowTotalsForSingleValues" >
                                <ClientSideEvents CheckedChanged="function(s,e){
                                    pivotGrid.PerformCallback();
                                    }" />
                            </dx:ASPxCheckBox>

                        </td>

                    </tr>
                    <tr>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Bold="true" Text="Summary&nbsp;Row:"></dx:ASPxLabel>
                        </td>

                        <td>
                            <dx:ASPxCheckBox ID="cbShowRowGrandTotals" runat="server" AutoPostBack="false" Text="ShowRowGrandTotals" >
                                <ClientSideEvents CheckedChanged="function(s,e){
                                    pivotGrid.PerformCallback();
                                    }" />
                            </dx:ASPxCheckBox>

                        </td>
                        <td>
                            <dx:ASPxCheckBox ID="cbShowRowTotals" runat="server" AutoPostBack="false" Text="ShowRowTotals" >
                                <ClientSideEvents CheckedChanged="function(s,e){
                                    pivotGrid.PerformCallback();
                                    }" />
                            </dx:ASPxCheckBox>
                        </td>
                        <td>
                            <dx:ASPxCheckBox ID="cbShowGrandTotalsForSingleValues" runat="server" AutoPostBack="false" Text="ShowGrandTotalsForSingleValues" >
                                <ClientSideEvents CheckedChanged="function(s,e){
                                    pivotGrid.PerformCallback();
                                    }" />
                            </dx:ASPxCheckBox>

                        </td>

                    </tr>

                    <tr>
                        <td>
                            <dx:ASPxLabel ID="ASPxLabel3" runat="server" Font-Bold="true" Text="Chart&nbsp;Type:"></dx:ASPxLabel>
                        </td>

                        <td>
                            <dx:ASPxComboBox ID="ChartType"
                                runat="server" Font-Bold="true"
                                AutoPostBack="false"
                                ValueType="System.String" />






                        </td>
                        <td>&nbsp;
 <dx:ASPxButton ID="btnChart" runat="server" RenderMode="Button"
     Width="90px" Text="Chart" AutoPostBack="true" CausesValidation="false">
     <Image IconID="chart_chart_16x16office2013"></Image>
     <ClientSideEvents Click="function(s, e) {   window.parent.LoadingPanel.Show(); }" />
 </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
                <br />
                <dx:ASPxButton ID="btnSaveBI" Image-IconID="actions_save_32x32devav"
                    ClientInstanceName="btnSaveBI"
                    runat="server" Text="Save BI"
                    AutoPostBack="false">
                    <ClientSideEvents Click="function(s,e) {
                                                if(confirm('Are you sure you want to save this report?'))
                                                {
                                                    window.parent.LoadingPanel.Show();
                                                    cbSaveBI.PerformCallback('');
                                                }
                                            }" />

                    <Image IconID="actions_save_16x16devav"></Image>
                </dx:ASPxButton>

                <dx:ASPxCallback ID="cbSaveBI" runat="server" ClientInstanceName="cbSaveBI">
                    <ClientSideEvents EndCallback="function(s,e){  
                        window.parent.LoadingPanel.Hide();
                        
                         pivotGrid.PerformCallback();
                        }" />
                </dx:ASPxCallback>
                <dx:ASPxButton ID="btShowModal" runat="server"
                    Image-IconID="programming_operatingsystem_16x16office2013"
                    Text="Data Fields"
                    AutoPostBack="false"
                    UseSubmitBehavior="false">
                    <ClientSideEvents Click="function(s,e){
                        //pivotGrid.ChangeCustomizationFieldsVisibility();
                         popPivot.Show();
                        }" />
                </dx:ASPxButton>

                <%-- <dx:ASPxButton ID="ShowButton" runat="server" Text="Summary Display"
                                            AutoPostBack="False"
                                            Image-IconID="setup_properties_16x16office2013" />

                                        <dx:ASPxPopupControl ID="PopupControl"
                                            runat="server" Modal="true"
                                            CloseAction="OuterMouseClick"
                                            LoadContentViaCallback="OnFirstShow"
                                            PopupElementID="ShowButton"
                                            PopupVerticalAlign="Below"
                                            PopupHorizontalAlign="LeftSides"
                                            AllowDragging="True"
                                            ShowFooter="false"
                                            HeaderText="Summary Display"
                                            ClientInstanceName="ClientPopupControl">
                                            <ContentCollection>
                                                <dx:PopupControlContentControl ID="PopupControlContentControl" runat="server">

                                                    <table class="OptionsTable BottomMargin">
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxLabel ID="ASPxLabel1" runat="server" Text="Summary&nbsp;Column:"></dx:ASPxLabel>
                                                            </td>

                                                            <td>
                                                                <dx:ASPxCheckBox ID="cbShowColumnGrandTotals" runat="server" AutoPostBack="true" Text="ShowColumnGrandTotals" />
                                                            </td>
                                                            <td>
                                                                <dx:ASPxCheckBox ID="cbShowColumnTotals" runat="server" AutoPostBack="true" Text="ShowColumnTotals" />

                                                            </td>
                                                            <td>

                                                                <dx:ASPxCheckBox ID="cbShowTotalsForSingleValues" runat="server" AutoPostBack="true" Text="ShowTotalsForSingleValues" />

                                                            </td>

                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <dx:ASPxLabel ID="ASPxLabel2" runat="server" Text="Summary&nbsp;Row:"></dx:ASPxLabel>
                                                            </td>

                                                            <td>
                                                                <dx:ASPxCheckBox ID="cbShowRowGrandTotals" runat="server" AutoPostBack="true" Text="ShowRowGrandTotals" />

                                                            </td>
                                                            <td>
                                                                <dx:ASPxCheckBox ID="cbShowRowTotals" runat="server" AutoPostBack="true" Text="ShowRowTotals" />
                                                            </td>
                                                            <td>
                                                                <dx:ASPxCheckBox ID="cbShowGrandTotalsForSingleValues" runat="server" AutoPostBack="true" Text="ShowGrandTotalsForSingleValues" />

                                                            </td>

                                                        </tr>
                                                    </table>



                                                </dx:PopupControlContentControl>
                                            </ContentCollection>

                                        </dx:ASPxPopupControl>--%>


                <%--                <dx:ASPxButton ID="btnSaveBI" Image-IconID="actions_save_16x16devav"
                    ClientInstanceName="btnSaveBI"
                    runat="server" Text="Save BI"
                    AutoPostBack="false">
                    <ClientSideEvents Click="function(s,e) {
                                                if(confirm('Are you sure you want to save this report?'))
                                                {
                                                    LoadingPanel.Show();
                                                    cbSaveBI.PerformCallback('');
                                                }
                                            }" />

                    <Image IconID="actions_save_16x16devav"></Image>
                </dx:ASPxButton>--%>


                <dx:ASPxButton ID="btnExportData" runat="server" RenderMode="Button"
                    Width="90px" Text="Export" AutoPostBack="false" CausesValidation="false">
                    <Image IconID="export_exporttoxls_16x16"></Image>
                </dx:ASPxButton>

                 <dx:ASPxButton ID="btnExportWithChart" runat="server" RenderMode="Button"
                    Width="90px" Text="Export With Chart" AutoPostBack="false" CausesValidation="false">
                    <Image IconID="export_exporttoxls_16x16"></Image>
                </dx:ASPxButton>


                <br />
                <br />

                <dx:ASPxPivotGrid ID="pivotGrid" runat="server"
                    CustomizationFieldsLeft="600"
                    CustomizationFieldsTop="400"
                    OptionsView-DataHeadersDisplayMode="Popup"
                    ClientInstanceName="pivotGrid"
                    Width="100%"
                    ClientIDMode="AutoID"
                    OptionsData-AutoExpandGroups="False"
                    EnableTheming="True">

                    <ClientSideEvents EndCallback="function(s, e) { 
							                        if( s.cpShowDrillDownWindow )
								                        GridView.SetVisible(true);
						                        }" />
                    <OptionsView HorizontalScrollBarMode="Auto"
                        ShowColumnHeaders="True"
                        ShowDataHeaders="True"
                        ShowFilterHeaders="True"
                        ShowRowHeaders="True"
                        ShowGrandTotalsForSingleValues="True"
                        ShowColumnGrandTotals="True"
                        ShowRowGrandTotals="True"
                        ShowColumnTotals="False" />

                    <OptionsCustomization
                        CustomizationFormStyle="Excel2007"
                        DeferredUpdates="false"></OptionsCustomization>
                    <OptionsPager AlwaysShowPager="True" Position="Top" RowsPerPage="10" PageSizeItemSettings-Position="Left" PagerAlign="Left">
                        <Summary Visible="False" />
                        <PageSizeItemSettings Visible="True" Items="10, 20, 30, 40, 50" ShowAllItem="true">
                        </PageSizeItemSettings>
                    </OptionsPager>

                    <OptionsData AutoExpandGroups="True" DataProcessingEngine="LegacyOptimized" />
                    <OptionsBehavior BestFitMode="Cell" />
                    <OptionsFilter NativeCheckBoxes="False" />
                    <Styles>

                        <TotalCellStyle Font-Bold="True">
                        </TotalCellStyle>
                        <GrandTotalCellStyle Font-Bold="True">
                        </GrandTotalCellStyle>
                    </Styles>


                </dx:ASPxPivotGrid>








            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxRoundPanel>


    <%--
    <dx:ASPxRoundPanel ID="pnEnquiry" ClientInstanceName="pnReport" EnableAnimation="true" ShowCollapseButton="false" AllowCollapsingByHeaderClick="false"
        runat="server" Width="100%">
        <PanelCollection>
            <dx:PanelContent>
                <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="600" Width="100%">
                    <Panes>
                        <dx:SplitterPane MinSize="340px" MaxSize="600px" Size="300px" AutoWidth="true" AutoHeight="True" ShowCollapseBackwardButton="True">
                            <ContentCollection>
                                <dx:SplitterContentControl ID="SplitterContentControl1" runat="server">
                                </dx:SplitterContentControl>
                            </ContentCollection>
                        </dx:SplitterPane>
                        <dx:SplitterPane>
                            <ContentCollection>
                                <dx:SplitterContentControl ID="SplitterContentControl2" AutoWidth="true" AutoHeight="True" runat="server">
                                </dx:SplitterContentControl>
                            </ContentCollection>
                        </dx:SplitterPane>
                    </Panes>
                </dx:ASPxSplitter>


            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxRoundPanel>--%>

    <%--    <dx:ASPxPopupControl ID="popChart" ClientInstanceName="popChart" runat="server"
        CloseAction="CloseButton"
        Modal="True"
        PopupHorizontalAlign="WindowCenter"
        HeaderText="Chart"
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
    </dx:ASPxPopupControl>--%>
    <dx:ASPxPopupControl ID="popChart" ClientInstanceName="popChart" runat="server"
        CloseAction="CloseButton"
        Modal="True" AllowResize="true"
        ShowMaximizeButton="true" ScrollBars="Both"
        ContentStyle-HorizontalAlign="Center"
        PopupHorizontalAlign="WindowCenter"
        HeaderText="Chart"
        AllowDragging="True">
         <HeaderStyle BackColor="#4796CE" ForeColor="White"  />

        <HeaderImage IconID="chart_chart_16x16office2013"></HeaderImage>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">


                <%--               <dx:ASPxComboBox ID="ChartType"
                    runat="server"
                    Caption="ChartType"
                    AutoPostBack="True"
                    ValueType="System.String" />

                <dxcharts:WebChartControl ID="WebChart"
                    ClientInstanceName="WebChart" runat="server"
                    Width="830px" Height="300px">
                </dxcharts:WebChartControl>--%>



                <dx:ASPxRoundPanel ID="ASPxRoundPanel1" runat="server" Width="100%" HorizontalAlign="Center" ShowHeader="False">
                    <PanelCollection>
                        <dx:PanelContent ID="PanelContent1" runat="server">



                            <table>
                                <tr>
                                    <td>

                                        <dxcharts:WebChartControl ID="WebChart"
                                            ClientInstanceName="chart" runat="server"
                                            Height="400px"
                                            Width="700px">
                                            <%--  <DiagramSerializable>
                                                <dxcharts:XYDiagram>
                                                    <AxisY Title-Text="Amount Awarded" Title-Visibility="True" VisibleInPanesSerializable="-1"
                                                        Tickmarks-MinorVisible="False" GridLines-MinorVisible="False" Label-TextPattern="{V:F2}" />
                                                </dxcharts:XYDiagram>
                                            </DiagramSerializable>--%>

                                            <SeriesTemplate LabelsVisibility="False" CrosshairLabelPattern="{V:N2}">
                                                <ViewSerializable>
                                                    <dxcharts:SideBySideBarSeriesView></dxcharts:SideBySideBarSeriesView>
                                                </ViewSerializable>
                                                <LabelSerializable>
                                                    <dxcharts:SideBySideBarSeriesLabel TextPattern="{V:N2}" Position="Top" TextOrientation="Horizontal">
                                                    </dxcharts:SideBySideBarSeriesLabel>

                                                </LabelSerializable>
                                            </SeriesTemplate>


                                            <%--  <Legend AlignmentHorizontal="Center" AlignmentVertical="TopOutside" Direction="LeftToRight" Visibility="true" Border-Color="White" ></Legend>--%>

                                            <%-- <SeriesTemplate LabelsVisibility="True" ArgumentDataMember="ContractNumber" ValueDataMembersSerializable="Value" CrosshairLabelPattern="{V:C2}">
                                                <ViewSerializable>
                                                    <dxcharts:SideBySideBarSeriesView></dxcharts:SideBySideBarSeriesView>
                                                </ViewSerializable>
                                                <LabelSerializable>
                                                    <dxcharts:SideBySideBarSeriesLabel TextPattern="{V:N0}" Position="Top" TextOrientation="Horizontal">
                                                    </dxcharts:SideBySideBarSeriesLabel>
                                                </LabelSerializable>
                                            </SeriesTemplate>
                                            <DiagramSerializable>
                                                <dxcharts:XYDiagram>
                                                    <AxisX VisibleInPanesSerializable="-1">
                                                        <Label Staggered="True" TextColor="Blue"></Label>
                                                    </AxisX>
                                                    <AxisY Title-Text="$, USD" Title-Visibility="True" VisibleInPanesSerializable="-1">
                                                        <GridLines MinorVisible="True"></GridLines>
                                                        <AutoScaleBreaks Enabled="True" MaxCount="2"></AutoScaleBreaks>
                                                    </AxisY>
                                                </dxcharts:XYDiagram>
                                            </DiagramSerializable>
                                            <ClientSideEvents ObjectHotTracked="ObjTracked" ObjectSelected="ObjSelected" />
                                            <BorderOptions Visibility="False" />
                                            --%>
                                        </dxcharts:WebChartControl>








                                    </td>
                                </tr>


                            </table>
                        </dx:PanelContent>
                    </PanelCollection>
                </dx:ASPxRoundPanel>
                <br />

                <dx:ASPxButton ID="cmdExportChart" runat="server"
                    Image-IconID="chart_chart_16x16office2013"
                    Text="Export"
                    AutoPostBack="false"
                    UseSubmitBehavior="false">
                    <ClientSideEvents Click="function(s,e){
                         e.processOnServer = true;
                       
                        }" />
                </dx:ASPxButton>

































            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>




    <dx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" ASPxPivotGridID="pivotGrid" runat="server">
    </dx:ASPxPivotGridExporter>




    
    <dx:ASPxPopupControl ID="popPivot" ClientInstanceName="popPivot" runat="server"
        CloseAction="CloseButton" ResizingMode="Postponed"
        AllowResize="true" ShowMaximizeButton="true"
        PopupHorizontalAlign="WindowCenter"
        HeaderText="Pivot Fields"
        Width="600" Height="540"
        AllowDragging="True">

   <HeaderStyle BackColor="#4796CE" ForeColor="White"  />

        <HeaderImage IconID="programming_operatingsystem_16x16office2013"></HeaderImage>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">

                <dx:ASPxPivotCustomizationControl ID="ASPxPivotCustomizationControl2"
                    ClientInstanceName="MyPivotCustomization"
                    runat="server" Layout="StackedSideBySide"
                    AllowedLayouts="BottomPanelOnly1by4, BottomPanelOnly2by2, StackedDefault, StackedSideBySide,TopPanelOnly"
                    Height="100%" Width="100%"
                    AllowSort="true"
                    AllowFilter="true"
                    ASPxPivotGridID="pivotGrid">
                </dx:ASPxPivotCustomizationControl>


            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
</asp:Content>
