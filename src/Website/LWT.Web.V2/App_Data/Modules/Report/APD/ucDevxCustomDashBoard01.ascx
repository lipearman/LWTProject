<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxCustomDashBoard01.ascx.vb" Inherits="Modules_ucDevxCustomDashBoard01" %>
<%--<%@ register assembly="GMaps" namespace="Subgurim.Controles" tagprefix="cc1" %>
<style type="text/css">
    v\:* {
        behavior: url(#default#VML);
    }
</style>--%>
<link rel="stylesheet" media="all" href="js/jvectormap/jquery-jvectormap.css" />

<script src="js/jvectormap/assets/jquery-1.8.2.js"></script>
<script src="js/jvectormap/jquery-jvectormap.js"></script>
<script src="js/jvectormap/lib/jquery-mousewheel.js"></script>

<script src="js/jvectormap/src/jvectormap.js"></script>

<script src="js/jvectormap/src/abstract-element.js"></script>
<script src="js/jvectormap/src/abstract-canvas-element.js"></script>
<script src="js/jvectormap/src/abstract-shape-element.js"></script>

<script src="js/jvectormap/src/svg-element.js"></script>
<script src="js/jvectormap/src/svg-group-element.js"></script>
<script src="js/jvectormap/src/svg-canvas-element.js"></script>
<script src="js/jvectormap/src/svg-shape-element.js"></script>
<script src="js/jvectormap/src/svg-path-element.js"></script>
<script src="js/jvectormap/src/svg-circle-element.js"></script>
<script src="js/jvectormap/src/svg-image-element.js"></script>
<script src="js/jvectormap/src/svg-text-element.js"></script>

<script src="js/jvectormap/src/vml-element.js"></script>
<script src="js/jvectormap/src/vml-group-element.js"></script>
<script src="js/jvectormap/src/vml-canvas-element.js"></script>
<script src="js/jvectormap/src/vml-shape-element.js"></script>
<script src="js/jvectormap/src/vml-path-element.js"></script>
<script src="js/jvectormap/src/vml-circle-element.js"></script>
<script src="js/jvectormap/src/vml-image-element.js"></script>

<script src="js/jvectormap/src/map-object.js"></script>
<script src="js/jvectormap/src/region.js"></script>
<script src="js/jvectormap/src/marker.js"></script>

<script src="js/jvectormap/src/vector-canvas.js"></script>
<script src="js/jvectormap/src/simple-scale.js"></script>
<script src="js/jvectormap/src/ordinal-scale.js"></script>
<script src="js/jvectormap/src/numeric-scale.js"></script>
<script src="js/jvectormap/src/color-scale.js"></script>
<script src="js/jvectormap/src/legend.js"></script>
<script src="js/jvectormap/src/data-series.js"></script>
<script src="js/jvectormap/src/proj.js"></script>
<script src="js/jvectormap/src/map.js"></script>

 

<script src="js/jvectormap/jquery-jvectormap-th-mill.js"></script>
<script>
    function findIndexInData(data, property, value) {
        var result = -1;
        data.some(function (item, i) {
            if (item[property] === value) {
                result = i;
                return true;
            }
        });
        return result;
    }


    $(function () {
        var provinces = [
            <%=_province%>
                //{ coords: [16.01253756, 103.1619644], name: '1.Mahasarakham', status: 'nonbp', code: 'TH-44' },
                //{ coords: [17.4843426, 101.7224121], name: '2.Loei', status: 'nonbp', code: 'TH-42' },
                //{ coords: [14.02273869, 99.53201294], name: '3.Kanchanaburi', status: 'nonbp', code: 'TH-71' },
                //{ coords: [14.97933174, 102.0973206], name: '4.Nakornratchasima', status: 'nonbp', code: 'TH-30' },
                //{ coords: [14.99198613, 103.1039429], name: '5.Buriram', status: 'nonbp', code: 'TH-31' },
                //{ coords: [15.80689229, 102.0314026], name: '6.Chaiyaphum', status: 'nonbp', code: 'TH-36' },
                //{ coords: [14.87986361, 103.4953308], name: '7.Surin', status: 'nonbp', code: 'TH-32' },
                //{ coords: [13.82379719, 102.0647049], name: '8.Srakaew', status: 'nonbp', code: 'TH-27' },
                //{ coords: [16.47964989, 99.52308655], name: '9.Kamphaengphet', status: 'nonbp', code: 'TH-62' },
                //{ coords: [18.36049268, 103.6450195], name: '10.Buangkan', status: 'nonbp', code: 'TH-38' },

        ];


        $('#thailand-map').vectorMap({
            map: 'th_mill',
            markers: provinces.map(function (h) { return { name: h.name, latLng: h.coords } }),
            markerLabelStyle: {
                initial: {
                    fill: 'blue'
                }
            },

            labels: {
                markers: {
                    render: function (index) {
                        return provinces[index].name;
                    },
                    offsets: function (index) {
                        var offset = provinces[index]['offsets'] || [0, 0];

                        return [offset[0] - 7, offset[1] + 3];
                    }
                } 
            },

            series: {
                markers: [{
                    attribute: 'image',
                    scale: {
                        'nonbp': 'icons/pushpin-yellow.png'
                    },
                    //normalizeFunction: 'polynomial',
                    values: provinces.reduce(function (p, c, i) { p[i] = c.status; return p }, {}),
                    //legend: {
                    //    horizontal: true,
                    //    title: 'BP',
                    //    labelRender: function (v) {
                    //        return {
                    //            nonbp: 'nonbp',
                    //        }[v];
                    //    }
                    //}

                }],

                regions: [{
                    scale: {
                        red: '#ff0000',
                        green: '#00ff00'
                    },
                    attribute: 'fill',
                    values: {

                        "TH-36": 'red',
                        "TH-71": 'red',
                        "TH-38": 'red',
                        "TH-31": 'red',
                        "TH-44": 'red',
                        "TH-42": 'red',
                        "TH-30": 'red',
                        "TH-32": 'red',
                        "TH-27": 'red',
                        "TH-62": 'red'

                    },
                    //legend: {
                    //    horizontal: true,
                    //    title: 'Color'
                    //}
                }]

            },

            onMarkerTipShow: function (event, label, index) {
                label.html(
                    '<b>' + label.html() + '</b></br>' +
                    '<b>- Non_BP_C: </b>' + provinces[index].nonbpc + '</br>' +
                    '<b>- Non_BP_A: </b>' + provinces[index].nonbpa + '</br>' +
                    '<b>- Claims Incured NotPaid: </b>' + provinces[index].claimnotpaid + '</br>' +
                    '<b>- Aftersales Opportunity: </b>' + provinces[index].aftersales + '</br>'
                );
            },

            //onRegionTipShow: function (event, label, code) {
            //    label.html(
            //        '<b>' + label.html() + '</b></br>' +
            //        '<b>Province: </b>' + provinces[findIndexInData(provinces, 'code', code)].name
            //    );
            //}

            onRegionClick: function (event, code) {
                alert(code);
            },

        });
    });




</script>



<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="Strategic Planning (Dashboard - Application)" runat="server" Width="800">
    <PanelCollection>
        <dx:PanelContent>
            <table>
                <tr>
                    <td>
                        <%--<img src="img/Thailand.jpg" width="480" />--%>
                        <%--<cc1:GMap ID="GMap1" Width="480" Height="600" runat="server" Key="AIzaSyAg0AH-6k964XD3YfrvJP7D8z30cATIXp4" />--%>

                        <div id="thailand-map" style="width: 480px; height: 600px"></div>




                    </td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;

                    </td>
                    <td valign="top">

                        <dx:ASPxComboBox runat="server" ClientInstanceName="CmbType_BP_Shop"
                            ID="CmbType_BP_Shop"
                            DropDownStyle="DropDownList"
                            Caption="Type_BP_Shop:">
                            <Items>
                                <dx:ListEditItem Selected="true" Text="All" Value="All" />
                                <dx:ListEditItem Text="BP Shop" Value="BP Shop" />
                                <dx:ListEditItem Text="Non BP Shop" Value="Non BP" />
                            </Items>
                            <ClientSideEvents
                                ValueChanged="function(s,e){
                LoadingPanel.Show();
                cbType_BP_Shop.PerformCallback(CmbType_BP_Shop.GetValue().toString());
                e.processOnServer = false;
                
        }" />
                        </dx:ASPxComboBox>
                        <dx:ASPxCallback ID="cbType_BP_Shop" runat="server" ClientInstanceName="cbType_BP_Shop">
                            <ClientSideEvents
                                CallbackComplete="function(s, e) {                                                                                    
            gridData.Refresh();   
            LoadingPanel.Hide();                                                                                                                                                                                        
    }" />
                        </dx:ASPxCallback>


                        <dx:ASPxGridView ID="gridData" ClientInstanceName="gridData" runat="server"
                            DataSourceID="SqlDataSource_gridData" SettingsPager-Mode="ShowAllRecords"
                            AutoGenerateColumns="False" Width="100%">
                            <Styles>
                                <Header Font-Bold="true" HorizontalAlign="Center">
                                </Header>
                                <Footer Font-Bold="true" HorizontalAlign="Center"></Footer>
                            </Styles>

                            <Columns>
                                <dx:GridViewBandColumn Caption="AccidentPlace  (A)">
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="AccidentPlace_F" Caption="AccidentPlace (F)"
                                            Settings-AllowHeaderFilter="True" PropertiesTextEdit-DisplayFormatString="{0:N0}"
                                            SettingsHeaderFilter-Mode="CheckedList"
                                            Settings-AllowSort="True" />
                                    </Columns>
                                </dx:GridViewBandColumn>

                                <dx:GridViewBandColumn Caption="No. of Non BP (B)">
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="Non_BP_C" SortOrder="Descending" PropertiesTextEdit-DisplayFormatString="{0:N0}" Caption="Values<br>Count of Non BP (C)" Settings-AllowSort="True" CellStyle-HorizontalAlign="Center" />
                                    </Columns>
                                </dx:GridViewBandColumn>

                                <dx:GridViewBandColumn Caption="Claims Paid  (C)">
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="Non_BP_A" Caption="Sum of Non BP (A)" PropertiesTextEdit-DisplayFormatString="{0:N0}" Settings-AllowSort="True" CellStyle-HorizontalAlign="Center" />
                                    </Columns>
                                </dx:GridViewBandColumn>


                                <dx:GridViewBandColumn Caption="Claims Incured <br>but not paid <br>(Claims Incurred - Claims Paid)<br>(D)">
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="Claims_Incured_NotPaid" Caption="Sum of But not paid" PropertiesTextEdit-DisplayFormatString="{0:N0}" Settings-AllowSort="True" CellStyle-HorizontalAlign="Center" />
                                    </Columns>
                                </dx:GridViewBandColumn>


                                <dx:GridViewBandColumn Caption="Total Aftersales Opportunity<br>(E) = (C+D)">
                                    <Columns>
                                        <dx:GridViewDataTextColumn FieldName="Aftersales_Opportunity" Caption="Sum of Aftersales Opportunity" PropertiesTextEdit-DisplayFormatString="{0:N0}" Settings-AllowSort="True" CellStyle-HorizontalAlign="Center" />
                                    </Columns>
                                </dx:GridViewBandColumn>




                            </Columns>
                            <Settings ShowFooter="True" />
                            <TotalSummary>
                                <dx:ASPxSummaryItem FieldName="Non_BP_C" SummaryType="Sum" DisplayFormat="{0:N0}" />
                                <dx:ASPxSummaryItem FieldName="Non_BP_A" SummaryType="Sum" DisplayFormat="{0:N0}" />
                                <dx:ASPxSummaryItem FieldName="Claims_Incured_NotPaid" SummaryType="Sum" DisplayFormat="{0:N0}" />
                                <dx:ASPxSummaryItem FieldName="Aftersales_Opportunity" SummaryType="Sum" DisplayFormat="{0:N0}" />


                            </TotalSummary>
                        </dx:ASPxGridView>








                    </td>
                </tr>



            </table>







        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<%--<dx:LinqServerModeDataSource ID="SqlDataSource_gridData" ContextTypeName="DataClasses_LWTReportsExt" TableName="V_CustomDashBoard01s" runat="server" />--%>

<asp:SqlDataSource ID="SqlDataSource_gridData" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"
    SelectCommand="
SELECT [tblMazdaRawData].AccidentPlace_F,tblProvince.Latitude,tblProvince.Longitude
,sum([tblMazdaRawData].Non_BP_C)  as Non_BP_C
,sum([tblMazdaRawData].Non_BP_A) as Non_BP_A
,SUM([tblMazdaRawData].Claims_Incured_NotPaid) as Claims_Incured_NotPaid
,SUM([tblMazdaRawData].Aftersales_Opportunity) as Aftersales_Opportunity
FROM [tblMazdaRawData]  
left join tblProvince  on [tblMazdaRawData].AccidentPlace_F = tblProvince.Name 
where Type_BP_Shop like case when @Type_BP_Shop='All' then Type_BP_Shop else @Type_BP_Shop end        

group by [tblMazdaRawData].AccidentPlace_F,tblProvince.Latitude,tblProvince.Longitude
">
    <SelectParameters>
        <asp:SessionParameter Name="Type_BP_Shop" SessionField="Type_BP_Shop" />
    </SelectParameters>
</asp:SqlDataSource>

