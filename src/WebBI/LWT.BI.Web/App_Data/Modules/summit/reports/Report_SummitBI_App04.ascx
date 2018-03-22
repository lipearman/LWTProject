<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Report_SummitBI_App04.ascx.vb" Inherits="Reports_Report_SummitBI_App04" %>



<dx:ASPxRoundPanel ID="pnEnquiry" ShowHeader="false" ClientInstanceName="pnReport" HeaderText="4. ยกเลิก" EnableAnimation="true" ShowCollapseButton="false" AllowCollapsingByHeaderClick="false"
    runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>
            <dx:ASPxSplitter ID="ASPxSplitter1" runat="server" Height="600" Width="100%">
                <Styles>
                    <Separator BackColor="#0072C6"></Separator>
                </Styles>
                <Panes>


                    <dx:SplitterPane MinSize="340px" MaxSize="600px"
                        Size="300px" AutoWidth="true" AutoHeight="True" ShowCollapseBackwardButton="True">
                        <ContentCollection>
                            <dx:SplitterContentControl ID="SplitterContentControl1" runat="server">


                                <dx:ASPxButton ID="ASPxButton1" runat="server" Text="Data Fields"
                                    AutoPostBack="False"
                                    Image-IconID="programming_operatingsystem_32x32office2013">
                                    <ClientSideEvents Click="function(s,e){
                                         pivotGrid.ChangeCustomizationFieldsVisibility();
                                          }" />
                                </dx:ASPxButton>



                                <dx:ASPxButton ID="ShowButton" runat="server" Text="Summary Display"
                                    AutoPostBack="False"
                                    Image-IconID="setup_properties_32x32office2013" />

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
                                                        <dx:ASPxLabel ID="ASPxLabel1" runat="server" Font-Bold="true" Text="Summary&nbsp;Column:"></dx:ASPxLabel>
                                                    </td>

                                                    <td>
                                                        <dx:ASPxCheckBox ID="cbShowColumnGrandTotals" runat="server" AutoPostBack="false" Text="ShowColumnGrandTotals" />
                                                    </td>
                                                    <td>
                                                        <dx:ASPxCheckBox ID="cbShowColumnTotals" runat="server" AutoPostBack="false" Text="ShowColumnTotals" />

                                                    </td>
                                                    <td>

                                                        <dx:ASPxCheckBox ID="cbShowTotalsForSingleValues" runat="server" AutoPostBack="false" Text="ShowTotalsForSingleValues" />

                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxLabel ID="ASPxLabel2" runat="server" Font-Bold="false" Text="Summary&nbsp;Row:"></dx:ASPxLabel>
                                                    </td>

                                                    <td>
                                                        <dx:ASPxCheckBox ID="cbShowRowGrandTotals" runat="server" AutoPostBack="false" Text="ShowRowGrandTotals" />

                                                    </td>
                                                    <td>
                                                        <dx:ASPxCheckBox ID="cbShowRowTotals" runat="server" AutoPostBack="false" Text="ShowRowTotals" />
                                                    </td>
                                                    <td>
                                                        <dx:ASPxCheckBox ID="cbShowGrandTotalsForSingleValues" runat="server" AutoPostBack="false" Text="ShowGrandTotalsForSingleValues" />

                                                    </td>

                                                </tr>
                                            </table>

                                            <dx:ASPxButton ID="ASPxButton2" runat="server"
                                                 Text="Apply"> 

                                            </dx:ASPxButton>

                                        </dx:PopupControlContentControl>
                                    </ContentCollection>

                                </dx:ASPxPopupControl>



                                <dx:ASPxButton ID="btnChart" runat="server"
                                    Width="90px" Text="Chart" AutoPostBack="false">
                                    <Image IconID="chart_chart_32x32office2013"></Image>

                                </dx:ASPxButton>




                                <dx:ASPxPopupControl ID="popChart"
                                    ClientInstanceName="popChart" runat="server" Modal="true"
                                    CloseAction="OuterMouseClick"
                                    LoadContentViaCallback="OnFirstShow"
                                    PopupElementID="btnChart"
                                    PopupVerticalAlign="Below"
                                    PopupHorizontalAlign="LeftSides"
                                    AllowDragging="True"
                                    ShowFooter="false"
                                    HeaderText="Summary Display">
                                    <ContentCollection>
                                        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">


                                            <dx:ASPxComboBox ID="ChartType"
                                                runat="server"
                                                Caption="ChartType"
                                                AutoPostBack="false"
                                                ValueType="System.String">
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) {
                                                         
                                                        WebChart.PerformCallback(&quot;&quot; + s.GetText() + &quot;&quot;); 

                                                      
                                                    }" />
                                            </dx:ASPxComboBox>

                                            <dxcharts:WebChartControl ID="WebChart"
                                                ClientInstanceName="WebChart" runat="server"
                                                Width="830px" Height="400px">
                                            </dxcharts:WebChartControl>



                                        </dx:PopupControlContentControl>
                                    </ContentCollection>
                                </dx:ASPxPopupControl>



                                <dx:ASPxButton ID="btnExportData" runat="server" RenderMode="Button"
                                    Width="90px" Text="Export" AutoPostBack="false" CausesValidation="false">
                                    <Image IconID="export_exporttoxlsx_32x32office2013"></Image>
                                </dx:ASPxButton>




                                <br />
                                <br />

                                <dx:ASPxPivotGrid ID="pivotGrid" runat="server"
                                    CustomizationFieldsLeft="600" OptionsView-DataHeadersDisplayMode="Popup"
                                    CustomizationFieldsTop="400" DataSourceID="SqlDataSource_gridData"
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
                                        ShowRowHeaders="True" />

                                    <OptionsCustomization CustomizationFormStyle="Excel2007" DeferredUpdates="true"></OptionsCustomization>
                                    <OptionsPager AlwaysShowPager="True" Position="Top" RowsPerPage="10" PageSizeItemSettings-Position="Left" PagerAlign="Left">
                                        <Summary Visible="False" />
                                        <PageSizeItemSettings Visible="True" Items="10, 20, 30, 40, 50" ShowAllItem="true">
                                        </PageSizeItemSettings>
                                    </OptionsPager>

                                    <OptionsData AutoExpandGroups="True"></OptionsData>
                                    <OptionsBehavior BestFitMode="Cell" />
                                    <OptionsFilter NativeCheckBoxes="False" />
                                    <Styles GrandTotalCellStyle-Font-Bold="true" TotalCellStyle-Font-Bold="true">
                                        <TotalCellStyle Font-Bold="True"></TotalCellStyle>
                                        <GrandTotalCellStyle Font-Bold="True"></GrandTotalCellStyle>
                                    </Styles>



                                    <Fields>

                                        <dx:PivotGridField ID="field1" Area="DataArea" Caption="Contract" FieldName="AppID" DisplayFolder="ตัวชี้วัด" AllowedAreas="DataArea" SummaryType="Count"
                                            CellFormat-FormatString="{0:N0}" EmptyCellText="0"
                                            GrandTotalCellFormat-FormatString="{0:N0}"
                                            ValueFormat-FormatString="{0:N0}"
                                            TotalCellFormat-FormatString="{0:N0}"
                                            TotalValueFormat-FormatString="{0:N0}"
                                            CellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatType="Numeric" ValueFormat-FormatType="Numeric">
                                        </dx:PivotGridField>

                                        <dx:PivotGridField ID="field2" Visible="False" Area="DataArea" Caption="Suminsure1" FieldName="Suminsure1" DisplayFolder="ตัวชี้วัด" AllowedAreas="DataArea" SummaryType="Sum"
                                            CellFormat-FormatString="{0:N2}" EmptyCellText="0.00"
                                            GrandTotalCellFormat-FormatString="{0:N2}"
                                            ValueFormat-FormatString="{0:N2}"
                                            TotalCellFormat-FormatString="{0:N2}"
                                            TotalValueFormat-FormatString="{0:N2}"
                                            CellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatType="Numeric" ValueFormat-FormatType="Numeric">
                                        </dx:PivotGridField>

                                        <dx:PivotGridField ID="field3" Visible="False" Area="DataArea" Caption="Suminsure2" FieldName="Suminsure2" DisplayFolder="ตัวชี้วัด" AllowedAreas="DataArea" SummaryType="Sum"
                                            CellFormat-FormatString="{0:N2}" EmptyCellText="0.00"
                                            GrandTotalCellFormat-FormatString="{0:N2}"
                                            ValueFormat-FormatString="{0:N2}"
                                            TotalCellFormat-FormatString="{0:N2}"
                                            TotalValueFormat-FormatString="{0:N2}"
                                            CellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatType="Numeric" ValueFormat-FormatType="Numeric">
                                        </dx:PivotGridField>

                                        <dx:PivotGridField ID="field4" Visible="False" Area="DataArea" Caption="Suminsure3" FieldName="Suminsure3" DisplayFolder="ตัวชี้วัด" AllowedAreas="DataArea" SummaryType="Sum"
                                            CellFormat-FormatString="{0:N2}" EmptyCellText="0.00"
                                            GrandTotalCellFormat-FormatString="{0:N2}"
                                            ValueFormat-FormatString="{0:N2}"
                                            TotalCellFormat-FormatString="{0:N2}"
                                            TotalValueFormat-FormatString="{0:N2}"
                                            CellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatType="Numeric" ValueFormat-FormatType="Numeric">
                                        </dx:PivotGridField>

                                        <dx:PivotGridField ID="field5" Visible="False" Caption="Premium" FieldName="Premium" DisplayFolder="ตัวชี้วัด" AllowedAreas="DataArea" SummaryType="Sum"
                                            CellFormat-FormatString="{0:N2}" EmptyCellText="0.00"
                                            GrandTotalCellFormat-FormatString="{0:N2}"
                                            ValueFormat-FormatString="{0:N2}"
                                            TotalCellFormat-FormatString="{0:N2}"
                                            TotalValueFormat-FormatString="{0:N2}"
                                            CellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatType="Numeric" ValueFormat-FormatType="Numeric">
                                        </dx:PivotGridField>


                                        <dx:PivotGridField ID="PivotGridField6" Area="DataArea" Caption="Refund Premium" FieldName="cn_RePremiumTotal" DisplayFolder="ตัวชี้วัด" AllowedAreas="DataArea" SummaryType="Sum"
                                            CellFormat-FormatString="{0:N2}" EmptyCellText="0.00"
                                            GrandTotalCellFormat-FormatString="{0:N2}"
                                            ValueFormat-FormatString="{0:N2}"
                                            TotalCellFormat-FormatString="{0:N2}"
                                            TotalValueFormat-FormatString="{0:N2}"
                                            CellFormat-FormatType="Numeric" GrandTotalCellFormat-FormatType="Numeric" TotalCellFormat-FormatType="Numeric" TotalValueFormat-FormatType="Numeric" ValueFormat-FormatType="Numeric">
                                        </dx:PivotGridField>


<%----
    
วันที่ยกเลิก (มีผลยกเลิก) CancelDate, cn_EffectedDateCancel
    


cn_ReNetPremium
cn_ReStamp
cn_ReVat
cn_RePremiumTotal


    --%>




 <dx:PivotGridField ID="field21" Area="FilterArea" Caption="StatementClose" FieldName="StatementClose" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>

                                        <dx:PivotGridField ID="field32" Area="ColumnArea" Caption="pm_Group" FieldName="pm_Group" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                        <dx:PivotGridField ID="field6" Area="ColumnArea" Caption="PolPeriod" FieldName="PolPeriod" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                        <dx:PivotGridField ID="field7" Area="FilterArea" Caption="PolYear" FieldName="PolYear" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                        <dx:PivotGridField ID="field8" Area="FilterArea" Caption="PolMonth" FieldName="PolMonth" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>

 <dx:PivotGridField ID="PivotGridField4" Area="RowArea" Caption="CancelMonth" FieldName="CancelMonth" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
 <dx:PivotGridField ID="field36" Area="RowArea" Caption="PolStatus1" FieldName="PolStatus1" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>

                                        <dx:PivotGridField ID="PivotGridField1" Area="FilterArea" Caption="MakeYear" FieldName="MakeYear" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                        <dx:PivotGridField ID="PivotGridField2" Area="FilterArea" Caption="MakeMonth" FieldName="MakeMonth" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                  
                                        
                                             


                                        <dx:PivotGridField ID="field9" Visible="False" Caption="BrandCode" FieldName="BrandCode" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                        <dx:PivotGridField ID="field10" Visible="False" Caption="ModelCode" FieldName="ModelCode" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                        <dx:PivotGridField ID="field11" Visible="False" Caption="MColor" FieldName="MColor" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                        <dx:PivotGridField ID="field12" Visible="False" Caption="MCC" FieldName="MCC" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                        <dx:PivotGridField ID="field13" Visible="False" Caption="MYearRegister" FieldName="MYearRegister" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                        <dx:PivotGridField ID="field14" Visible="False" Caption="MProvince" FieldName="MProvince" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                        <dx:PivotGridField ID="field15" Visible="False" Caption="CarUseTypeID" FieldName="CarUseTypeID" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                        <dx:PivotGridField ID="field16" Visible="False" Caption="MPurchaseType" FieldName="MPurchaseType" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                        <dx:PivotGridField ID="field17" Visible="False" Caption="Beneficiary" FieldName="Beneficiary" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                        <dx:PivotGridField ID="field18" Visible="False" Caption="BeneficiaryAddress" FieldName="BeneficiaryAddress" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                        <dx:PivotGridField ID="field19" Visible="False" Caption="BeneficiaryRelationship" FieldName="BeneficiaryRelationship" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                        <dx:PivotGridField ID="field20" Visible="False" Caption="Status1" FieldName="Status1" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>

                                        <dx:PivotGridField ID="field22" Visible="False" Caption="UWName" FieldName="UWName" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                        <dx:PivotGridField ID="field23" Visible="False" Caption="BranchName" FieldName="BranchName" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                        <dx:PivotGridField ID="field24" Visible="False" Caption="DealerCode" FieldName="DealerCode" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                        <dx:PivotGridField ID="field25" Visible="False" Caption="DealerName" FieldName="DealerName" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                        <dx:PivotGridField ID="field26" Visible="False" Caption="ProvinceName" FieldName="ProvinceName" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                        <dx:PivotGridField ID="field27" Visible="False" Caption="ProvinceNameEng" FieldName="ProvinceNameEng" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                        <dx:PivotGridField ID="field28" Visible="False" Caption="PlanName" FieldName="PlanName" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                        <dx:PivotGridField ID="field29" Visible="False" Caption="PromotionName" FieldName="PromotionName" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                        <dx:PivotGridField ID="field30" Visible="False" Caption="pm_MType" FieldName="pm_MType" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                        <dx:PivotGridField ID="field31" Visible="False" Caption="pm_UWCode" FieldName="pm_UWCode" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                        <dx:PivotGridField ID="field33" Visible="False" Caption="ProjectCode" FieldName="ProjectCode" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                        <dx:PivotGridField ID="field34" Visible="False" Caption="ProjectName" FieldName="ProjectName" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                        <dx:PivotGridField ID="field35" Area="FilterArea" Caption="Status" FieldName="Status" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                        <dx:PivotGridField ID="field37" Area="FilterArea" Caption="PolStatus2" FieldName="PolStatus2" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>


                                        <dx:PivotGridField ID="field38" Area="FilterArea" Caption="BillingStatus" FieldName="BillingStatus" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                        <dx:PivotGridField ID="PivotGridField5" Area="FilterArea" Caption="DateMakeContract" FieldName="DateMakeContract" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"
                                            ValueFormat-FormatString="yyyy-MM-dd"
                                            ValueFormat-FormatType="DateTime"
                                            CellFormat-FormatString="yyyy-MM-dd"
                                            CellFormat-FormatType="DateTime">
                                        </dx:PivotGridField>





<dx:PivotGridField ID="PivotGridField7" Visible="False" Caption="ContractMonth" FieldName="ContractMonth" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
<dx:PivotGridField ID="PivotGridField3" Visible="False" Caption="ContractYear" FieldName="ContractYear" DisplayFolder="มุมมอง" AllowedAreas="FilterArea,ColumnArea,RowArea"></dx:PivotGridField>
                                            






                                    </Fields>



                                </dx:ASPxPivotGrid>


                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>




                </Panes>
            </dx:ASPxSplitter>


        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>




<dx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" ASPxPivotGridID="pivotGrid" runat="server">
</dx:ASPxPivotGridExporter>


<asp:SqlDataSource ID="SqlDataSource_gridData" runat="server"
    ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"
    SelectCommand="select * from RawData_SummitBI_App where PolStatus1 like 'Cancel%'  "></asp:SqlDataSource>

