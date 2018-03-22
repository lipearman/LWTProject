<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TblClaimDaily.aspx.vb" Inherits="Applications_TblClaimDaily" %>

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

        <dx:LinqServerModeDataSource ID="ObjectDataSource1" runat="server" ContextTypeName="DataClasses_LWTReport_NLTExt" TableName="TblClaimDailies" />

        <input runat="server" id="ColumnIndex" type="hidden" enableviewstate="true" />
        <input runat="server" id="RowIndex" type="hidden" enableviewstate="true" />
        <dx:ASPxRoundPanel ID="pnEnquiry" ClientInstanceName="pnReport" EnableAnimation="true" ShowCollapseButton="false" AllowCollapsingByHeaderClick="false"
            HeaderText="Daily Claim 2012-2015" runat="server" Width="1200">
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

                            <%--DataArea--%>
                              <dx:PivotGridField FieldName="VIN"
                                Area="DataArea"
                                ID="PivotGridField21"
                                Caption="Chassis"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                SummaryType="Count" />


                            <dx:PivotGridField FieldName="No_of_Claim"
                                Area="DataArea"
                                ID="PivotGridField13"
                                Caption="No_of_Claim"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                SummaryType="Sum" />

                            <dx:PivotGridField FieldName="ClaimTimes"
                                Area="DataArea"
                                ID="PivotGridField19"
                                Caption="ClaimTimes"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                SummaryType="Sum" />
                             
                           
                           

                           

 
                            <%-- RowArea--%>
                            
                                     <dx:PivotGridField FieldName="ClosingDate"
                                Area="RowArea"
                                SummaryType="Count" 
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                ID="PivotGridField22"
                                GroupInterval="DateYear"
                                Caption="ClosingDate (Year)" />
                             <dx:PivotGridField FieldName="InsurerName"
                                Area="RowArea"
                                ID="PivotGridField20"
                                Caption="InsurerName"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                SummaryType="Count" />
                            

                            <%--ColumnArea--%>
                            

                            <%--FilterArea--%>
                             <dx:PivotGridField FieldName="ClosingDate"
                                Area="FilterArea"
                                SummaryType="Count" CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                ID="PivotGridField7"
                                GroupInterval="DateMonthYear"
                                Caption="ClosingDate" />


                             <dx:PivotGridField FieldName="StartingDate"
                                Area="FilterArea"
                                SummaryType="Count" CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                ID="PivotGridField1"
                                GroupInterval="DateMonthYear"
                                Caption="StartingDate" />

                           

                   

                             <dx:PivotGridField FieldName="StartingDate"
                                Area="FilterArea"
                                SummaryType="Count" CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                ID="PivotGridField23"
                                GroupInterval="DateYear"
                                Caption="StartingDate (Year)" />

                           


                            <%--Hidden Field--%> 
                            
                             <dx:PivotGridField FieldName="YearOfCar"
                                Visible="False"
                                ID="PivotGridField16"
                                Caption="YearOfCar"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                SummaryType="Count" />


                           <dx:PivotGridField FieldName="SumInsurance"
                                Visible="False"
                                ID="PivotGridField17"
                                Caption="SumInsurance"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                SummaryType="Sum" /> 
                              <dx:PivotGridField FieldName="Voluntary_NetPremium"
                                Visible="False"
                                ID="PivotGridField18"
                                Caption="Voluntary_NetPremium"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n2}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n2}"
                                SummaryType="Sum" /> 
                             


                              <dx:PivotGridField FieldName="Authorize_Amt"
                                Visible="False"
                                ID="PivotGridField9"
                                Caption="Authorize_Amt"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n2}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n2}"
                                SummaryType="Sum" />
                              <dx:PivotGridField FieldName="Insurance_Amt"
                                Visible="False"
                                ID="PivotGridField10"
                                Caption="Insurance_Amt"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n2}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n2}"
                                SummaryType="Sum" />
                              <dx:PivotGridField FieldName="Parts_ordered_from_shop_Amt"
                                Visible="False"
                                ID="PivotGridField11"
                                Caption="Parts_ordered_from_shop_Amt"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n2}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n2}"
                                SummaryType="Sum" />
                              <dx:PivotGridField FieldName="BP_Shop_Amt"
                                Visible="False"
                                ID="PivotGridField12"
                                Caption="BP_Shop_Amt"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n2}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n2}"
                                SummaryType="Sum" />
                              <dx:PivotGridField FieldName="Consent_Form_Amt"
                                Visible="False"
                                ID="PivotGridField14"
                                Caption="Consent_Form_Amt"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n2}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n2}"
                                SummaryType="Sum" />
                              <dx:PivotGridField FieldName="Other_Amt"
                                Visible="False"
                                ID="PivotGridField15"
                                Caption="Other_Amt"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n2}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n2}"
                                SummaryType="Sum" />




                              <dx:PivotGridField FieldName="Authorize"
                                 Visible="False"
                                ID="PivotGridField2"
                                Caption="Authorize"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                SummaryType="Sum" />
                            <dx:PivotGridField FieldName="Insurance"
                                 Visible="False"
                                ID="PivotGridField3"
                                Caption="Insurance"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                SummaryType="Sum" />
                            <dx:PivotGridField FieldName="Parts_ordered_from_shop"
                                Visible="False"
                                ID="PivotGridField4"
                                Caption="Parts"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                SummaryType="Sum" />
                            <dx:PivotGridField FieldName="BP_Shop"
                                 Visible="False"
                                ID="PivotGridField5"
                                Caption="BP_Shop"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                SummaryType="Sum" />
                            <dx:PivotGridField FieldName="Consent_Form"
                                 Visible="False"
                                ID="PivotGridField6"
                                Caption="Consent_Form"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                SummaryType="Sum" />
                             <dx:PivotGridField FieldName="Other"
                                 Visible="False"
                                ID="PivotGridField8"
                                Caption="Other"
                                CellFormat-FormatType="Numeric"
                                CellFormat-FormatString="{0:n0}"
                                GrandTotalCellFormat-FormatType="Numeric"
                                GrandTotalCellFormat-FormatString="{0:n0}"
                                SummaryType="Sum" />
                        </Fields>

                    </dx:ASPxPivotGrid>

                    <dx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server"></dx:ASPxPivotGridExporter>



                </dx:PanelContent>
            </PanelCollection>
        </dx:ASPxRoundPanel> 
    </form>
</body>
</html>
