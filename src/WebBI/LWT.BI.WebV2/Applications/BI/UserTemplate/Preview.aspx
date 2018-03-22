<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Applications/AppMasterPage.master" CodeFile="Preview.aspx.vb" Inherits="applications_BI_UserTemplate_Preview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <input runat="server" id="hdBID" type="hidden" enableviewstate="true" />
    <dx:ASPxRoundPanel ID="pnEnquiry" ClientInstanceName="pnReport" EnableAnimation="true" ShowCollapseButton="false" AllowCollapsingByHeaderClick="false"
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

           <dx:ASPxButton ID="btnSaveBI" Image-IconID="actions_save_16x16devav"
                    ClientInstanceName="btnSaveBI"
                    runat="server" Text="Save BI"
                    AutoPostBack="false">
                    <ClientSideEvents Click="function(s,e) {
                                                    window.parent.LoadingPanel.Show();
                                                    cbSaveBI.PerformCallback('');
                                            }" />

                    <Image IconID="actions_save_16x16devav"></Image>
                </dx:ASPxButton>

                <dx:ASPxCallback ID="cbSaveBI" runat="server" ClientInstanceName="cbSaveBI">
                    <ClientSideEvents
                        CallbackComplete="function(s, e) { window.parent.LoadingPanel.Hide(); pivotGrid.PerformCallback(); }" />
                </dx:ASPxCallback>



                <dx:ASPxButton ID="btShowModal" runat="server"
                    Image-IconID="programming_operatingsystem_16x16office2013"
                    Text="Data Fields"
                    AutoPostBack="false"
                    UseSubmitBehavior="false">
                    <ClientSideEvents Click="function(s,e){
                        pivotGrid.ChangeCustomizationFieldsVisibility();
                        }" />
                </dx:ASPxButton>


     

                <dx:ASPxButton ID="btnExportData" runat="server" RenderMode="Button"
                    Width="90px" Text="Export" AutoPostBack="false" CausesValidation="false">
                    <Image IconID="export_exporttoxls_16x16"></Image>
                </dx:ASPxButton>

                <%--    <dx:ASPxButton ID="btnChart" runat="server" RenderMode="Button"
                    Width="90px" Text="Chart" AutoPostBack="true" CausesValidation="false">
                    <Image IconID="chart_chart_16x16office2013"></Image>
                    <ClientSideEvents Click="function(s, e) { LoadingPanel.Show(); }" />
                </dx:ASPxButton>--%>
                
                <dx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" ASPxPivotGridID="pivotGrid" runat="server">
                </dx:ASPxPivotGridExporter>

                <br /> <br />

                <dx:ASPxPivotGrid ID="pivotGrid" runat="server"
                    CustomizationFieldsLeft="600"
                    CustomizationFieldsTop="400"
                    ClientInstanceName="pivotGrid"
                    Width="100%" Prefilter-Enabled="True"
                    OptionsCustomization-CustomizationFormStyle="Excel2007"
                    ClientIDMode="AutoID"
                    OptionsCustomization-AllowSort="true"
                    OptionsCustomization-AllowSortBySummary="true"
                    OptionsCustomization-AllowSortInCustomizationForm="true"
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

                    <OptionsCustomization CustomizationFormStyle="Excel2007"></OptionsCustomization>
                    <OptionsPager AlwaysShowPager="True" Position="Top" RowsPerPage="10" PageSizeItemSettings-Position="Left" PagerAlign="Left">
                        <Summary Visible="False" />
                        <PageSizeItemSettings Visible="True" Items="10, 20, 30, 40, 50" ShowAllItem="true">
                        </PageSizeItemSettings>
                    </OptionsPager>

                    <OptionsData AutoExpandGroups="True"></OptionsData>
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

</asp:Content>
