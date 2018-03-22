<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/Applications/AppMasterPage.master" CodeFile="Preview.aspx.vb" Inherits="applications_BI_Preview_Preview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <input runat="server" id="hdRID" type="hidden" enableviewstate="true" />
    <input runat="server" id="hdContextTypeName" type="hidden" enableviewstate="true" />
    <input runat="server" id="hdTableName" type="hidden" enableviewstate="true" />
    <dx:ASPxLoadingPanel ID="LoadingPanel" runat="server" ClientInstanceName="LoadingPanel"
        Modal="True">
    </dx:ASPxLoadingPanel>
    <dx:LinqServerModeDataSource ID="ObjectDataSource1" runat="server" />

    <dx:ASPxRoundPanel ID="pnEnquiry" ClientInstanceName="pnReport" Visible="false" EnableAnimation="true" ShowCollapseButton="false" AllowCollapsingByHeaderClick="false"
        runat="server" Width="100%">
        <PanelCollection>
            <dx:PanelContent>

                <dx:ASPxButton ID="btnSaveBI" Image-IconID="actions_save_16x16devav"
                    ClientInstanceName="btnSaveBI"
                    runat="server" Text="Save BI"
                    AutoPostBack="false">
                    <ClientSideEvents Click="function(s,e) {

                                                    LoadingPanel.Show();
                                                    cbSaveBI.PerformCallback('');
                                            }" />
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
                    <%--<ClientSideEvents Click="function(s, e) { popChart.Show(); }" />--%>
                    <ClientSideEvents Click="function(s, e) { LoadingPanel.Show(); }" />
                </dx:ASPxButton>

            <%--    <dx:ASPxButton ID="btnTop" runat="server" RenderMode="Button"
                    Width="90px" Text="Top" AutoPostBack="true" CausesValidation="false">
                    <Image IconID="priorities_highpriority_16x16devav"></Image>
                    <ClientSideEvents Click="function(s, e) { LoadingPanel.Show(); }" />
                </dx:ASPxButton>--%>
                <br />
                   <br />
                <table class="OptionsTable OptionsBottomMargin">
                    <tr>
                         <td>
                            <dx:ASPxComboBox ID="ddlField" runat="server"  Width="120" SelectedIndex="0" Caption="Values for the Field">
                               
                            </dx:ASPxComboBox>
                        </td>
                        <td style="padding-left: 20px">
                              </td>
                        <td>
                            <dx:ASPxComboBox ID="ddlTopCount" runat="server" AutoPostBack="true" Width="50px" Caption="Show Top">
                                <ClientSideEvents SelectedIndexChanged="function(s, e) { LoadingPanel.Show(); }" />
                                <Items>
                                    <dx:ListEditItem Text="All" Value="0" />
                                    <dx:ListEditItem Text="1" Value="1" />
                                    <dx:ListEditItem Text="2" Value="2" />
                                    <dx:ListEditItem Text="3" Value="3" />
                                    <dx:ListEditItem Text="4" Value="4" />
                                    <dx:ListEditItem Text="5" Value="5" />
                                    <dx:ListEditItem Text="10" Value="10" />
                                    <dx:ListEditItem Text="20" Value="20" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                       
                    </tr>
                </table>

                <br />



                <dx:ASPxButton ID="btShowModal" runat="server"
                    Image-IconID="grid_pivot_16x16"
                    Text="Show Pivot Data"
                    AutoPostBack="true"
                    UseSubmitBehavior="false" Width="100%">
                    <%--<ClientSideEvents Click="function(s, e) { popPivot.Show(); }" />--%>
                    <ClientSideEvents Click="function(s, e) { LoadingPanel.Show(); }" />
                </dx:ASPxButton>

                <dx:ASPxPivotGrid ID="pivotGrid" runat="server" ClientInstanceName="pivotGrid"
                    EnableCallBacks="false" Styles-CellStyle-Wrap="False" Width="100%"
                    DataSourceID="ObjectDataSource1">
                    <%--    <ClientSideEvents BeginCallback="function(s, e) { LoadingPanel.Show(); }" 
                        EndCallback="function(s, e) { LoadingPanel.Hide(); }"
                        />--%>


                    <%--  <OptionsPager AlwaysShowPager="True" Position="Bottom" >
                        <Summary Visible="False" />
                        <PageSizeItemSettings Visible="True" Items="10, 20, 50" ShowAllItem="true" >
                        </PageSizeItemSettings>
                    </OptionsPager>--%>

                    <OptionsPager RowsPerPage="1000000"></OptionsPager>



                    <OptionsBehavior BestFitMode="Cell" />
                    <Styles GrandTotalCellStyle-Font-Bold="true" TotalCellStyle-Font-Bold="true">
                        <TotalCellStyle Font-Bold="True"></TotalCellStyle>
                        <GrandTotalCellStyle Font-Bold="True"></GrandTotalCellStyle>
                    </Styles>

                      <OptionsView
                        ShowColumnHeaders="True"
                        ShowDataHeaders="True"
                        ShowFilterHeaders="True"
                        ShowRowHeaders="True" />
                  

                </dx:ASPxPivotGrid>
                <dx:ASPxPivotGridExporter ID="ASPxPivotGridExporter1" runat="server">
                </dx:ASPxPivotGridExporter>


            </dx:PanelContent>
        </PanelCollection>
    </dx:ASPxRoundPanel>



    <dx:ASPxPopupControl ID="popPivot" ClientInstanceName="popPivot" runat="server"
Top="10" Left="10" AllowDragging="True" Modal="True"  ShowFooter="false"
        DragElement="Window" AllowResize="True"  ShowHeader="false"
        PopupVerticalAlign="NotSet" PopupHorizontalAlign="NotSet" 
        AutoUpdatePosition="True" HeaderText="" FooterText="" 
        
        >
        <%--<ClientSideEvents Shown="function(s,e){ pivotGrid.PerformCallback(); }" />--%>
 
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">

                <dx:ASPxPivotCustomizationControl ID="ASPxPivotCustomizationControl1"
                    ClientInstanceName="ASPxPivotCustomizationControl1"
                    runat="server" Layout="StackedDefault"
                    AllowedLayouts="BottomPanelOnly1by4, BottomPanelOnly2by2, StackedDefault, StackedSideBySide"
                    Height="500px" Width="300px"
                    AllowSort="true"
                    AllowFilter="true"
                    ASPxPivotGridID="pivotGrid">
                </dx:ASPxPivotCustomizationControl>



            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>
 

    <dx:ASPxPopupControl ID="popChart" ClientInstanceName="popChart" runat="server"
        CloseAction="CloseButton"
        Modal="True"
        PopupHorizontalAlign="WindowCenter"
        HeaderText="Pivot Fields"
        AllowDragging="True">
        <%--<ClientSideEvents Shown="function(s,e){ pivotGrid.PerformCallback(); }" />--%>

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



    <%-- <dx:ASPxPopupControl ID="popTop" ClientInstanceName="popTop" runat="server"
        CloseAction="CloseButton"
        Modal="True"
        PopupHorizontalAlign="WindowCenter"
        HeaderText="Top"
        AllowDragging="True">
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">



                <table class="OptionsTable OptionsBottomMargin">
                    <tr>
                        <td>
                            <dx:ASPxComboBox ID="ddlTopCount" runat="server" Width="50px" AutoPostBack="true" Caption="Show Top">
                                <Items>
                                    <dx:ListEditItem Text="All" Value="0" />
                                    <dx:ListEditItem Text="5" Value="5" />
                                    <dx:ListEditItem Text="10" Value="10" />
                                    <dx:ListEditItem Text="20" Value="20" />
                                </Items>
                            </dx:ASPxComboBox>
                        </td>
                        <td style="padding-left: 20px"></td>
                        <td>
                            <dx:ASPxRadioButtonList ID="rdoField" runat="server" RepeatColumns="4" Visible="false"
                                Caption="Field" Border-BorderStyle="None">
                            </dx:ASPxRadioButtonList>

                        </td>
                        <td style="padding-left: 20px"></td>
                        <td>&nbsp; 
                        </td>
                        <td>

                            <dx:ASPxRadioButtonList ID="rdoData" runat="server" RepeatColumns="4" Visible="false"
                                Caption="By" Border-BorderStyle="None">
                            </dx:ASPxRadioButtonList>

                        </td>
                    </tr>
                </table>




            </dx:PopupControlContentControl>
        </ContentCollection>
    </dx:ASPxPopupControl>--%>
</asp:Content>
