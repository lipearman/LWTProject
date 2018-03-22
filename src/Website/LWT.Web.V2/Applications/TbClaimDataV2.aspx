<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TbClaimDataV2.aspx.vb" Inherits="Applications_TbClaimDataV2" %>

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
        <%-- <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="DataContext_EBExt">
            <SelectParameters>
                <asp:QueryStringParameter Name="_GUID" QueryStringField="ID" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>--%>


<%--            <asp:SqlDataSource ID="ObjectDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsNLTConnectionString %>" 
                SelectCommand="select * from V_TbClaimDataV2" > 
    </asp:SqlDataSource>--%>

        <dx:LinqServerModeDataSource ID="ObjectDataSource1" runat="server" ContextTypeName="LWTReports_NLTDB_DatabaseContext" TableName="V_TbClaimDataV2s"  />
 
         <%--<ef:EntityDataSource runat="server" ID="ObjectDataSource1"  ContextTypeName="LWTReports_NLTDB_DatabaseContext" EntitySetName="V_TbClaimDataV2s" />--%>

        <input runat="server" id="ColumnIndex" type="hidden" enableviewstate="true" />
        <input runat="server" id="RowIndex" type="hidden" enableviewstate="true" />
        <dx:ASPxRoundPanel ID="pnEnquiry" ClientInstanceName="pnReport" EnableAnimation="true" ShowCollapseButton="false" AllowCollapsingByHeaderClick="false"
            HeaderText="Monthiy Claim 2012-2015 Paid Date" runat="server" Width="1200">
            <PanelCollection>
                <dx:PanelContent>
                    <table class="OptionsTable BottomMargin">
                        <tr>
                            <td>
                                <dx:ASPxCheckBox ID="cbShowColumnGrandTotals" runat="server" AutoPostBack="true" Text="ShowColumnGrandTotals" />
                            </td>
                            <td>
                                <dx:ASPxCheckBox ID="cbShowRowGrandTotals" runat="server" AutoPostBack="true" Text="ShowRowGrandTotals" />
                            </td>
                            <td>
                                <dx:ASPxCheckBox ID="cbShowGrandTotalsForSingleValues" runat="server" Checked="false" AutoPostBack="true" Text="ShowGrandTotalsForSingleValues" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <dx:ASPxCheckBox ID="cbShowColumnTotals" runat="server" AutoPostBack="true" Checked="false" Text="ShowColumnTotals" />
                            </td>
                            <td>
                                <dx:ASPxCheckBox ID="cbShowRowTotals" runat="server" AutoPostBack="true" Checked="false" Text="ShowRowTotals" />
                            </td>
                            <td>
                                <dx:ASPxCheckBox ID="cbShowTotalsForSingleValues" runat="server" AutoPostBack="true" Checked="false" Text="ShowTotalsForSingleValues" />
                            </td>
                        </tr>
                    </table>

                    <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" EnableClientSideAPI="True" Width="240px"
                        Text="Show Customization Window" ClientInstanceName="button">
                        <ClientSideEvents Click="function(s, e) { pivotGrid.ChangeCustomizationFieldsVisibility(); }" />
                    </dx:ASPxButton>

                    <dx:ASPxButton ID="btnExportData" runat="server" RenderMode="Button"
                        Width="90px" Text="Export" AutoPostBack="false" CausesValidation="false">
                        <Image IconID="export_exporttoxls_16x16"></Image>
                    </dx:ASPxButton>

                    <dx:ASPxPivotGrid ID="pivotGrid" runat="server" ClientInstanceName="pivotGrid"
                        EnableCallBacks="false" Styles-CellStyle-Wrap="False"
                        DataSourceID="ObjectDataSource1">
                        <ClientSideEvents CustomizationFieldsVisibleChanged="function(s, e) {
                        if(button != null &amp;&amp; pivotGrid != null) {
                            button.SetText((pivotGrid.GetCustomizationFieldsVisibility() ? &quot;Hide&quot; : &quot;Show&quot;) + &quot; Customization Window&quot;);
                                }
                            }" />
                        <OptionsPager RowsPerPage="1000000"></OptionsPager>
                        <OptionsBehavior BestFitMode="Cell" />
                        <Styles GrandTotalCellStyle-Font-Bold="true" TotalCellStyle-Font-Bold="true">
                            <TotalCellStyle Font-Bold="True"></TotalCellStyle>
                            <GrandTotalCellStyle Font-Bold="True"></GrandTotalCellStyle>
                        </Styles>
                        <OptionsView HorizontalScrollBarMode="Auto" />
                        <OptionsFilter NativeCheckBoxes="False" />
                        <OptionsChartDataSource DataProvideMode="UseCustomSettings" />

                        <OptionsView ShowColumnTotals="False" ShowRowTotals="False" HorizontalScrollBarMode="Auto" />
                        <Fields>
                            <%-- RowArea--%>


                            <%--ColumnArea--%>


                            <%--DataArea--%>
                            <dx:PivotGridField FieldName="ClaimCost"
                                Area="DataArea"
                                ID="PivotGridField13"
                                Caption="ClaimCost"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n2}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n2}"
                                SummaryType="Sum" />

                            <dx:PivotGridField FieldName="No_of_Claim"
                                Area="DataArea"
                                ID="PivotGridField12"
                                Caption="No# of Claim"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                SummaryType="Sum" />
                            <%-- <dx:PivotGridField FieldName="PresentedAmount" Area="DataArea" ID="PivotGridField38"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                SummaryType="Sum" Caption="PresentedAmount(count)" />
                            <dx:PivotGridField FieldName="ReimbursedAmount" Area="DataArea" ID="PivotGridField39"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                SummaryType="Sum" Caption="ReimbursedAmount(sum)" />

                            <%--FilterArea
                            <%--<dx:PivotGridField FieldName="TreateDateFrom" Area="FilterArea" ID="PivotGridField7" GroupInterval="DateYear" Caption="Year" />
                            <dx:PivotGridField FieldName="TreateDateFrom" Area="FilterArea" ID="LotDateDateYear" GroupInterval="DateMonth" Caption="Month" />
                            --%>
                              <dx:PivotGridField FieldName="AccidentDate"
                                Area="FilterArea"
                                SummaryType="Count" CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                ID="PivotGridField7"
                                GroupInterval="DateYear"
                                Caption="Accident Year" />
                            <dx:PivotGridField FieldName="GarageRegionTH" SummaryType="Count" CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                Area="FilterArea"
                                ID="PivotGridField23" Caption="GarageRegionTH" />
                            <dx:PivotGridField FieldName="GarageProvinceTH" SummaryType="Count" CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                Area="FilterArea"
                                ID="PivotGridField5"
                                Caption="GarageProvinceTH" />

                            <dx:PivotGridField FieldName="CustType"
                                SummaryType="Count"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                Area="FilterArea"
                                ID="PivotGridField21"
                                Caption="CustType" />


                            <dx:PivotGridField FieldName="InsurerName"
                                SummaryType="Count"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                Area="FilterArea"
                                ID="PivotGridField19"
                                Caption="InsurerName" />

                            <dx:PivotGridField FieldName="GarageType2" SummaryType="Count" CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}" Area="FilterArea" ID="PivotGridField18" Caption="GarageType2" />

                                <dx:PivotGridField FieldName="ClaimPaid" 
                                Caption="ClaimPaid" 
                                Area="FilterArea"
                                                            
                                GroupInterval="DateMonthYear"
                               />

                            <%--Hidden Field--%>
                            <%--Accident Month	
    IndemnityGroup	
    Starting Month	
    Starting Year	
    App_Beneficiary Group	
    App_CarGroup F	
    Claim Paid Month	
    Claim Paid Year	
    App_Showroomname	
    Accdent Province
                            --%>

                            <dx:PivotGridField FieldName="FullCustomerName" SummaryType="Count" CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}" Visible="False" ID="PivotGridField1" Caption="FullCustomerName" />
                            <dx:PivotGridField FieldName="ChassisNo" SummaryType="Count" CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}" Visible="False" ID="PivotGridField2" Caption="ChassisNo" />
                            <dx:PivotGridField FieldName="PolicyNo" SummaryType="Count" CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}" Visible="False" ID="PivotGridField3" Caption="PolicyNo" />
                            <dx:PivotGridField FieldName="Insurance_ClaimNo" SummaryType="Count" CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}" Visible="False" ID="PivotGridField4" Caption="Insurance_ClaimNo" />

                            <dx:PivotGridField FieldName="GarageProvinceEN" SummaryType="Count" CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}" Visible="False" ID="PivotGridField6" Caption="GarageProvinceEN" />
                            <dx:PivotGridField FieldName="AccidentDate" SummaryType="Count" CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}" Visible="False" ID="PivotGridField8" Caption="AccidentDate" />
                            <dx:PivotGridField FieldName="ClaimGroup" SummaryType="Count" CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}" Visible="False" ID="PivotGridField10" Caption="ClaimGroup" />
                            <dx:PivotGridField FieldName="IndemnityType" SummaryType="Count" CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}" Visible="False" ID="PivotGridField14" Caption="IndemnityType" />
                            <dx:PivotGridField FieldName="IndemnityType_E" SummaryType="Count" CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}" Visible="False" ID="PivotGridField15" Caption="IndemnityType E" />
                            <dx:PivotGridField FieldName="GarageName" SummaryType="Count" CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}" Visible="False" ID="PivotGridField16" Caption="GarageName" />
                            <dx:PivotGridField FieldName="GarageType1" SummaryType="Count" CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}" Visible="False" ID="PivotGridField17" Caption="GarageType1" />

                            <dx:PivotGridField FieldName="InsurerCode" SummaryType="Count" CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}" Visible="False" ID="PivotGridField20" Caption="InsurerCode" />
                            <dx:PivotGridField FieldName="App_StartingDate" SummaryType="Count" CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}" Visible="False" ID="PivotGridField24" Caption="App_StartingDate" />
                            <dx:PivotGridField FieldName="App_EndDate" SummaryType="Count" CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}" Visible="False" ID="PivotGridField27" Caption="App_EndDate" />
                            <dx:PivotGridField FieldName="App_Beneficiary" SummaryType="Count" CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}" Visible="False" ID="PivotGridField29" Caption="App_Beneficiary" />
                            <dx:PivotGridField FieldName="App_CarGroup" SummaryType="Count" CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}" Visible="False" ID="PivotGridField31" Caption="App_CarGroup" />
                            <dx:PivotGridField FieldName="App_CarRegistryYear" SummaryType="Count" CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}" Visible="False" ID="PivotGridField33" Caption="App_CarRegistryYear" />

                          

                            <dx:PivotGridField FieldName="AccidentDate" SummaryType="Count" CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                Visible="False"
                                ID="PivotGridField9"
                                GroupInterval="DateMonth"
                                Caption="Accident Month" />

                            <dx:PivotGridField FieldName="IndemnityGroup"
                                SummaryType="Sum"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                Visible="False"
                                ID="PivotGridField11"
                                Caption="IndemnityGroup" />


                            <dx:PivotGridField FieldName="App_StartingDate"
                                SummaryType="Count"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="DateTime"
                                Visible="False"
                                ID="PivotGridField26"
                                GroupInterval="DateYear"
                                Caption="Starting Year" />

                            <dx:PivotGridField FieldName="App_StartingDate"
                                SummaryType="Count"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="DateTime"
                                Visible="False"
                                ID="PivotGridField25"
                                GroupInterval="DateMonth"
                                Caption="Starting Month" />


                            <dx:PivotGridField FieldName="App_Beneficiary_Group"
                                SummaryType="Count"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                Visible="False"
                                ID="PivotGridField30"
                                Caption="App_Beneficiary Group" />
                            <dx:PivotGridField FieldName="App_CarGroup_F"
                                SummaryType="Count"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                Visible="False"
                                ID="PivotGridField32"
                                Caption="App_CarGroup F" />
                            <dx:PivotGridField FieldName="Claim_Paid_Month"
                                SummaryType="Count"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                Visible="False"
                                ID="PivotGridField34"
                                Caption="Claim Paid Month" />
                          <%--  <dx:PivotGridField FieldName="Claim_Paid_Year"
                                SummaryType="Count"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                Visible="False"
                                ID="PivotGridField35"
                                Caption="Claim Paid Year" />
                            <dx:PivotGridField FieldName="App_ShowroomName"
                                SummaryType="Count"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                Visible="False"
                                ID="PivotGridField28"
                                Caption="App_ShowroomName" />--%>
                         


                            <dx:PivotGridField FieldName="Accdent_Province"
                                SummaryType="Count"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                Visible="False"
                                ID="PivotGridField36"
                                Caption="Accdent Province" />

                            <dx:PivotGridField FieldName="CarAge"
                                SummaryType="Count"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                Visible="False"
                                ID="PivotGridField22"
                                Caption="CarAge" />

                        </Fields>

                    </dx:ASPxPivotGrid>

                    <dx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server"></dx:ASPxPivotGridExporter>



                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxRoundPanel>
        <dx:ASPxPopupControl ID="ASPxPopupControl1" Modal="true" runat="server" Height="1px"
            AutoUpdatePosition="true"
            EnableAnimation="true"
            EnableCallbackAnimation="true"
            AllowDragging="True" ClientInstanceName="DrillDownWindow" Left="200" Top="200"
            CloseAction="OuterMouseClick" Width="153px" HeaderText="Drill Down Window">
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    <dx:ASPxButton ID="ASPxButton2" runat="server" RenderMode="Button"
                        Width="90px" Text="Export" AutoPostBack="false" CausesValidation="false">
                        <Image IconID="export_exporttoxlsx_16x16"></Image>
                    </dx:ASPxButton>


                    <dx:ASPxGridView ID="ASPxGridView1" runat="server"
                        AutoGenerateColumns="false" Styles-Cell-Wrap="False"
                        ClientInstanceName="GridView">
                        <SettingsLoadingPanel Mode="ShowAsPopup" />
                        <ClientSideEvents EndCallback="function(s, e) { 
							if( s.cpShowDrillDownWindow )
								GridView.SetVisible(true);
						}" />
                         <Columns> 


<dx:GridViewDataTextColumn FieldName="FullCustomerName" CellStyle-Wrap="False" Caption="FullCustomerName" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="ChassisNo" CellStyle-Wrap="False" Caption="ChassisNo" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>

<%--
<dx:GridViewDataTextColumn FieldName="PolicyNo" CellStyle-Wrap="False" Caption="PolicyNo" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="Insurance_ClaimNo" CellStyle-Wrap="False" Caption="Insurance_ClaimNo" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="GarageProvinceTH" CellStyle-Wrap="False" Caption="GarageProvinceTH" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="GarageProvinceEN" CellStyle-Wrap="False" Caption="GarageProvinceEN" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="AccidentDate" CellStyle-Wrap="False" Caption="AccidentDate" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="Accident_Month" CellStyle-Wrap="False" Caption="Accident_Month" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="ClaimGroup" CellStyle-Wrap="False" Caption="ClaimGroup" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="IndemnityGroup" CellStyle-Wrap="False" Caption="IndemnityGroup" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="No_of_Claim" CellStyle-Wrap="False" Caption="No_of_Claim" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>--%>

<%--<dx:GridViewDataTextColumn FieldName="ClaimCost" CellStyle-Wrap="False" Caption="ClaimCost" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="IndemnityType" CellStyle-Wrap="False" Caption="IndemnityType" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="IndemnityType_E" CellStyle-Wrap="False" Caption="IndemnityType_E" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="GarageName" CellStyle-Wrap="False" Caption="GarageName" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="GarageType1" CellStyle-Wrap="False" Caption="GarageType1" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="GarageType2" CellStyle-Wrap="False" Caption="GarageType2" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="InsurerName" CellStyle-Wrap="False" Caption="InsurerName" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="InsurerCode" CellStyle-Wrap="False" Caption="InsurerCode" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="CustType" CellStyle-Wrap="False" Caption="CustType" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="App_StartingDate" CellStyle-Wrap="False" Caption="App_StartingDate" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="Starting_Month" CellStyle-Wrap="False" Caption="Starting_Month" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="Starting_Year" CellStyle-Wrap="False" Caption="Starting_Year" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="App_EndDate" CellStyle-Wrap="False" Caption="App_EndDate" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="App_ShowroomName" CellStyle-Wrap="False" Caption="App_ShowroomName" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="App_Beneficiary" CellStyle-Wrap="False" Caption="App_Beneficiary" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="App_Beneficiary_Group" CellStyle-Wrap="False" Caption="App_Beneficiary_Group" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="App_CarGroup" CellStyle-Wrap="False" Caption="App_CarGroup" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="App_CarGroup_F" CellStyle-Wrap="False" Caption="App_CarGroup_F" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="App_CarRegistryYear" CellStyle-Wrap="False" Caption="App_CarRegistryYear" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>
<dx:GridViewDataTextColumn FieldName="Claim_Paid_Month" CellStyle-Wrap="False" Caption="Claim_Paid_Month" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn> 
                        
     --%>                        
                             <%--<dx:GridViewDataTextColumn FieldName="Claim_Paid_Year" CellStyle-Wrap="False" Caption="Claim_Paid_Year" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>--%>
                        <%--<dx:GridViewDataTextColumn FieldName="Accdent_Province" CellStyle-Wrap="False" Caption="Accdent_Province" Settings-AllowHeaderFilter="False"></dx:GridViewDataTextColumn>--%>



                         </Columns> 

                        <Styles>
                            <Header ImageSpacing="5px" SortingImageSpacing="5px" />
                        </Styles>
                    </dx:ASPxGridView>
                    <dx:ASPxGridViewExporter runat="server" ID="GridExporter" GridViewID="ASPxGridView1" />
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
    </form>
</body>
</html>
