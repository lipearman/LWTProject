<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Applications/AppMasterPage.master" CodeFile="PreviewExcel.aspx.vb" Inherits="applications_BI_User_PreviewExcel" %>







<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function number_format(number, decimals, dec_point, thousands_sep) {
            // http://kevin.vanzonneveld.net
            // +   original by: Jonas Raoni Soares Silva (http://www.jsfromhell.com)
            // +   improved by: Kevin van Zonneveld (http://kevin.vanzonneveld.net)
            // +     bugfix by: Michael White (http://getsprink.com)
            // +     bugfix by: Benjamin Lupton
            // +     bugfix by: Allan Jensen (http://www.winternet.no)
            // +    revised by: Jonas Raoni Soares Silva (http://www.jsfromhell.com)
            // +     bugfix by: Howard Yeend
            // +    revised by: Luke Smith (http://lucassmith.name)
            // +     bugfix by: Diogo Resende
            // +     bugfix by: Rival
            // +      input by: Kheang Hok Chin (http://www.distantia.ca/)
            // +   improved by: davook
            // +   improved by: Brett Zamir (http://brett-zamir.me)
            // +      input by: Jay Klehr
            // +   improved by: Brett Zamir (http://brett-zamir.me)
            // +      input by: Amir Habibi (http://www.residence-mixte.com/)
            // +     bugfix by: Brett Zamir (http://brett-zamir.me)
            // +   improved by: Theriault
            // +   improved by: Drew Noakes
            // *     example 1: number_format(1234.56);
            // *     returns 1: '1,235'
            // *     example 2: number_format(1234.56, 2, ',', ' ');
            // *     returns 2: '1 234,56'
            // *     example 3: number_format(1234.5678, 2, '.', '');
            // *     returns 3: '1234.57'
            // *     example 4: number_format(67, 2, ',', '.');
            // *     returns 4: '67,00'
            // *     example 5: number_format(1000);
            // *     returns 5: '1,000'
            // *     example 6: number_format(67.311, 2);
            // *     returns 6: '67.31'
            // *     example 7: number_format(1000.55, 1);
            // *     returns 7: '1,000.6'
            // *     example 8: number_format(67000, 5, ',', '.');
            // *     returns 8: '67.000,00000'
            // *     example 9: number_format(0.9, 0);
            // *     returns 9: '1'
            // *    example 10: number_format('1.20', 2);
            // *    returns 10: '1.20'
            // *    example 11: number_format('1.20', 4);
            // *    returns 11: '1.2000'
            // *    example 12: number_format('1.2000', 3);
            // *    returns 12: '1.200'
            var n = !isFinite(+number) ? 0 : +number,
                prec = !isFinite(+decimals) ? 0 : Math.abs(decimals),
                sep = (typeof thousands_sep === 'undefined') ? ',' : thousands_sep,
                dec = (typeof dec_point === 'undefined') ? '.' : dec_point,
                toFixedFix = function (n, prec) {
                    // Fix for IE parseFloat(0.55).toFixed(0) = 0;
                    var k = Math.pow(10, prec);
                    return Math.round(n * k) / k;
                },
                s = (prec ? toFixedFix(n, prec) : Math.round(n)).toString().split('.');
            if (s[0].length > 3) {
                s[0] = s[0].replace(/\B(?=(?:\d{3})+(?!\d))/g, sep);
            }
            if ((s[1] || '').length < prec) {
                s[1] = s[1] || '';
                s[1] += new Array(prec - s[1].length + 1).join('0');
            }
            return s.join(dec);
        }



        function customcrosshair(s, e) {
            var summary = 0;
            for (var i = 0; i < e.crosshairElements.length; i++) {
                summary += e.crosshairElements[i].Point.point.values[0];

            }

            e.crosshairElements[e.crosshairElements.length - 1].LabelElement.footerText = 'Total :' + number_format(summary, 2);

            for (var i = 0; i < e.crosshairGroupHeaderElements.length; i++) {
                e.crosshairGroupHeaderElements[i].text = '<%=legenttitle%>';
            }
        }

    </script>



    <input runat="server" id="hdBID" type="hidden" enableviewstate="true" />


    <dx:ASPxRoundPanel ID="pnEnquiry" HeaderImage-IconID="businessobjects_boreport_32x32" ClientInstanceName="pnReport" EnableAnimation="true" ShowCollapseButton="false" AllowCollapsingByHeaderClick="false"
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
                                    <ClientSideEvents Click="function(s, e) { window.top.LoadingPanel.Show(); }" />
                                </dx:ASPxButton>
                        </td>
                    </tr>
                </table>
                <br />

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





                <dx:ASPxButton ID="btnExportData" runat="server" RenderMode="Button"
                    Width="90px" Text="Export" AutoPostBack="false" CausesValidation="false">
                    <Image IconID="export_exporttoxls_16x16"></Image>
                </dx:ASPxButton>

                <dx:ASPxButton ID="btnExportWithChart" runat="server" RenderMode="Button"
                    Width="90px" Text="Export With Chart" AutoPostBack="false" CausesValidation="false">
                    <Image IconID="export_exporttoxls_16x16"></Image>
                </dx:ASPxButton>

                <dx:ASPxButton ID="btnClose" runat="server" RenderMode="Button"
                    Width="90px" Text="Close" AutoPostBack="true" CausesValidation="false">
                    <Image IconID="actions_close_16x16office2013"></Image>
                    <ClientSideEvents Click="function(s, e) {window.top.clientView.Hide();}" />
                </dx:ASPxButton>

                <br />
                <br />

                <dx:ASPxPivotGrid ID="pivotGrid" runat="server"
                    CustomizationFieldsLeft="600"
                    CustomizationFieldsTop="400"
                    OptionsView-DataHeadersDisplayMode="Popup"
                    ClientInstanceName="pivotGrid"
                    Width="100%" DataSourceID="GetExcelPivotData"
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
                        CustomizationFormLayout="StackedSideBySide"
                        CustomizationFormAllowedLayouts="BottomPanelOnly1by4,BottomPanelOnly2by2,StackedDefault,StackedSideBySide,TopPanelOnly"
                        CustomizationWindowWidth="400"
                        CustomizationWindowHeight="600"
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
                                            <ClientSideEvents CustomDrawCrosshair="customcrosshair" />
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

                                            <%--                                            <SeriesTemplate LabelsVisibility="True" ArgumentDataMember="ContractNumber" ValueDataMembersSerializable="Value" CrosshairLabelPattern="{V:C2}">
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









    <%--    <asp:ObjectDataSource ID="GetExcelPivotData" runat="server" SelectMethod="GetExcelPivotData" TypeName="DataClasses_PortalBIExt">
        <SelectParameters>
            <asp:SessionParameter Name="BID" SessionField="BID" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>--%>
    <%--    
    <asp:SqlDataSource ID="GetExcelPivotData" runat="server"
        ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
        SelectCommand="select * from RawData">



    </asp:SqlDataSource>
    --%>

    <asp:ObjectDataSource ID="GetExcelPivotData" runat="server"
        SelectMethod="GetBITable"
        TypeName="WebCacheWithSqlDependency.DbManager">
        <SelectParameters>
            <asp:SessionParameter Name="tableName" SessionField="DSGUID" Type="String" DefaultValue="_Blank" />
        </SelectParameters>
    </asp:ObjectDataSource>


</asp:Content>
