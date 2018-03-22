<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxAPDReportsMasterTemplate.ascx.vb" Inherits="Modules_ucDevxAPDReportsMasterTemplate" %>


<script type="text/javascript">
    function ShowDrillDown() {
        var mainElement = pivotGrid.GetMainElement();
        DrillDownWindow.ShowAtPos(ASPxClientUtils.GetAbsoluteX(mainElement), ASPxClientUtils.GetAbsoluteY(mainElement));
    }


    function ShowMasterBIPopup(RID) {
        window.top.LoadingPanel.Show();
        window.top.clientView.SetContentUrl('applications/BI/Master/Preview.aspx?RID=' + RID);
        window.top.clientView.Show();
    }
</script>


<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="Report" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>


    <dx:ASPxTreeList ID="treeList" runat="server" AutoGenerateColumns="false" Width="900"
                                    DataSourceID="SqlDataSource_Data"
                                    SettingsBehavior-AutoExpandAllNodes="true"
                                    KeyFieldName="RID"
                                    ParentFieldName="ParentID">

                                    
                                    <Border BorderStyle="Solid" />
                                    <Settings ShowTreeLines="False" SuppressOuterGridLines="true" />
                                    <SettingsBehavior AllowFocusedNode="True" ExpandCollapseAction="NodeDblClick" />
                                    <Columns>

<%--                                        <dx:TreeListSpinEditColumn FieldName="No" Width="20" CellStyle-HorizontalAlign="Left">
                                            <EditFormSettings Visible="False" />
                                        </dx:TreeListSpinEditColumn>--%>

                                        <dx:TreeListTextColumn FieldName="TITLE" Caption="Report" CellStyle-Wrap="False">
                                            <EditFormSettings Visible="False" />
                                        </dx:TreeListTextColumn>

                                        <dx:TreeListMemoColumn FieldName="DESCRIPTION" Caption="DESCRIPTION" Width="500" CellStyle-Wrap="True" >                                             
                                        </dx:TreeListMemoColumn>

                                        <dx:TreeListTextColumn>
                                            <DataCellTemplate>
                                                <%# PreviewBI(Eval("RID").ToString(), Eval("VIEW_ID").ToString())%>
                                            </DataCellTemplate>
                                        </dx:TreeListTextColumn>

                                    </Columns>

                                </dx:ASPxTreeList>




        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<asp:SqlDataSource ID="SqlDataSource_Data" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"
    SelectCommand="select * from tblReport Where REPORT_TYPE='BI' order by No "></asp:SqlDataSource>




<dx:ASPxPopupControl ID="clientViewBI" runat="server" ClientInstanceName="clientViewBI"
    Modal="True"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="Business Intelligent"
    AllowDragging="true"
    AllowResize="True"
    EnableAnimation="true"
    CloseAction="CloseButton"
    EnableCallbackAnimation="true"
    EnableViewState="true"
    ShowPageScrollbarWhenModal="true"
    ShowMaximizeButton="true"
    Width="900"
    Height="790"
    FooterText=""
    ShowFooter="true">
 <ClientSideEvents Shown="function(s,e){ window.setTimeout(function() {LoadingPanel.Hide();},2000);}" />

    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl5" runat="server">
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>

<%--
<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="Report" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxSplitter runat="server" ID="ASPxSplitter1" Width="1400" ResizingMode="Live">
                <Panes>
                    <dx:SplitterPane AutoHeight="True" Size="30%" MinSize="110px">
                        <ContentCollection>
                            <dx:SplitterContentControl ID="SplitterContentControl1" runat="server"
                                SupportsDisabledAttribute="True">

                                <dx:ASPxTreeList ID="treeList" runat="server" AutoGenerateColumns="false" Width="100%"
                                    DataSourceID="SqlDataSource_Data"
                                    SettingsBehavior-AutoExpandAllNodes="true"
                                    KeyFieldName="RID"
                                    ParentFieldName="ParentID">

                                    <ClientSideEvents
                                        Init="function(s, e){
                                            var key = s.GetFocusedNodeKey();
                                            callbackPanel_view.PerformCallback(key);                                       
                                        }"
                                        FocusedNodeChanged="function(s, e){
                                            var key = s.GetFocusedNodeKey();
                                            callbackPanel_view.PerformCallback(key);                                       
                                        }" />
                                    <Border BorderStyle="Solid" />
                                    <Settings ShowTreeLines="False" SuppressOuterGridLines="true" />
                                    <SettingsBehavior AllowFocusedNode="True" ExpandCollapseAction="NodeDblClick" />
                                    <Columns>


                                        <dx:TreeListTextColumn FieldName="TITLE" Caption="Report" Width="200" CellStyle-Wrap="False">
                                            <EditFormSettings Visible="False" />
                                        </dx:TreeListTextColumn>

                                        <dx:TreeListComboBoxColumn FieldName="VIEW_ID" Caption="DATA" CellStyle-Wrap="False">

                                            <PropertiesComboBox DataSourceID="SqlDataSource_VIEWs" TextField="VIEW_TITLE" ValueField="VIEW_ID" ValueType="System.String">

                                                <ValidationSettings Display="Dynamic">
                                                    <RequiredField IsRequired="true" />
                                                </ValidationSettings>
                                            </PropertiesComboBox>

                                        </dx:TreeListComboBoxColumn>

                                        <dx:TreeListTextColumn FieldName="VIEW_QUERY" Width="200" Visible="false" CellStyle-Wrap="False">
                                            <PropertiesTextEdit>
                                                <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                                            </PropertiesTextEdit>
                                            <EditFormSettings Visible="True" ColumnSpan="2" />
                                        </dx:TreeListTextColumn>
                                        <dx:TreeListTextColumn FieldName="VIEW_CONNECTION" Width="200" Visible="false" CellStyle-Wrap="False">
                                            <PropertiesTextEdit>
                                                <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                                            </PropertiesTextEdit>
                                            <EditFormSettings Visible="True" ColumnSpan="2" />
                                        </dx:TreeListTextColumn>
                                    </Columns>

                                </dx:ASPxTreeList>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                    <dx:SplitterPane AutoHeight="True" Size="70%" MinSize="280px">
                        <ContentCollection>
                            <dx:SplitterContentControl ID="SplitterContentControl2" runat="server"
                                SupportsDisabledAttribute="True">
                                <dx:ASPxCallbackPanel runat="server" ID="callbackPanel_view"
                                    ClientInstanceName="callbackPanel_view"
                                    Height="100%" Width="100%">

                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent1">


                                            <dx:ASPxFormLayout ID="formLayout" runat="server" Width="100%" RequiredMarkDisplayMode="RequiredOnly" EnableViewState="false" EncodeHtml="false">
                                                <Items>
                                                    <dx:LayoutGroup Caption="Fields" GroupBoxDecoration="None" ColCount="1">
                                                        <Items>



                                                            <dx:LayoutItem ShowCaption="False">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer1" runat="server">


                                                                        <input runat="server" id="ColumnIndex" type="hidden" enableviewstate="true" />
                                                                        <input runat="server" id="RowIndex" type="hidden" enableviewstate="true" />
                                                                        <dx:ASPxRoundPanel ID="pnEnquiry" ClientInstanceName="pnReport" EnableAnimation="true" ShowCollapseButton="false" AllowCollapsingByHeaderClick="false"
                                                                            runat="server" Width="100%">
                                                                            <PanelCollection>
                                                                                <dx:PanelContent>


                                                                                    <dx:ASPxButton ID="ASPxButton1" runat="server" AutoPostBack="False" EnableClientSideAPI="True" Width="240px"
                                                                                        Text="Show Customization Window" ClientInstanceName="button">
                                                                                        <ClientSideEvents Click="function(s, e) { pivotGrid.ChangeCustomizationFieldsVisibility(); }" />
                                                                                    </dx:ASPxButton>



                                                                                    <dx:ASPxPivotGrid ID="pivotGrid" runat="server" ClientInstanceName="pivotGrid"
                                                                                        EnableCallBacks="true" Styles-CellStyle-Wrap="False" Width="100%">
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

                                                                                    </dx:ASPxPivotGrid>



                                                                                </dx:PanelContent>
                                                                            </PanelCollection>
                                                                        </dx:ASPxRoundPanel>



                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>

                                                            <dx:LayoutItem ShowCaption="False">
                                                                <LayoutItemNestedControlCollection>
                                                                    <dx:LayoutItemNestedControlContainer ID="LayoutItemNestedControlContainer2" runat="server">
                                                                        <dx:ASPxButton ID="btnSaveBI"
                                                                            ClientInstanceName="btnSaveBI"
                                                                            runat="server" Text="Save BI"
                                                                            AutoPostBack="false">
                                                                            <ClientSideEvents Click="function(s,e) {
                                                                                 if(ASPxClientEdit.AreEditorsValid()) {
                                                                                   LoadingPanel.Show();
                                                                                   cbSaveBI.PerformCallback('');                                                                                  
                                                                                 }
                                                                        }" />
                                                                        </dx:ASPxButton>

                                                                        <dx:ASPxCallback ID="cbSaveBI" runat="server" ClientInstanceName="cbSaveBI">
                                                                            <ClientSideEvents
                                                                                CallbackComplete="function(s, e) {                                                                                    
                                                                                             LoadingPanel.Hide();   
                                                                                                                                                                                                                                                                                                                                                                                                                
                                                                                }" />
                                                                        </dx:ASPxCallback>



                                                                        <dx:ASPxButton ID="btnPreview"
                                                                            ClientInstanceName="btnPreview"
                                                                            runat="server" Text="Preview"
                                                                            AutoPostBack="false">
                                                                            <ClientSideEvents Click="function(s,e) {
                                                                                 LoadingPanel.Show();
                                                                                 clientViewBI.SetContentUrl('applications/BI/Preview/Preview.aspx');
                                                                                 clientViewBI.Show();
                                                                        }" />
                                                                        </dx:ASPxButton>

                                                                    </dx:LayoutItemNestedControlContainer>
                                                                </LayoutItemNestedControlCollection>
                                                            </dx:LayoutItem>




                                                        </Items>
                                                    </dx:LayoutGroup>
                                                </Items>
                                            </dx:ASPxFormLayout>

                                        </dx:PanelContent>
                                    </PanelCollection>
                                </dx:ASPxCallbackPanel>
                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                </Panes>
            </dx:ASPxSplitter>






        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<asp:SqlDataSource ID="SqlDataSource_Data" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"
    SelectCommand="select * from tblReport Where REPORT_TYPE='BI' order by No "
    UpdateCommand="update tblReport set VIEW_QUERY=@VIEW_QUERY, VIEW_CONNECTION=@VIEW_CONNECTION Where RID=@RID">

    <UpdateParameters>
        <asp:Parameter Name="VIEW_QUERY" DbType="String" />
        <asp:Parameter Name="VIEW_CONNECTION" DbType="String" />
        <asp:Parameter Name="RID" DbType="Int32" />
    </UpdateParameters>

</asp:SqlDataSource>


<asp:SqlDataSource ID="SqlDataSource_Fields" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"
    SelectCommand="select * from tblReport_Field Where RID=@RID order by FIELD_NAME"
    UpdateCommand="update tblReport_Field 
    set      FIELD_CAPTION=@FIELD_CAPTION
            ,SummaryType=@SummaryType
            ,CellFormat_FormatType=@CellFormat_FormatType
            ,CellFormat_FormatString=@CellFormat_FormatString
            ,GrandTotalCellFormat_FormatType=@GrandTotalCellFormat_FormatType
            ,GrandTotalCellFormat_FormatString=@GrandTotalCellFormat_FormatString
            ,GroupInterval=@GroupInterval    
    Where FIELD_ID=@FIELD_ID"
    DeleteCommand="delete from tblReport_Field where FIELD_ID=@FIELD_ID">

    <UpdateParameters>
        <asp:Parameter Name="FIELD_CAPTION" Type="String" />
        <asp:Parameter Name="SummaryType" Type="String" />
        <asp:Parameter Name="CellFormat_FormatType" Type="String" />
        <asp:Parameter Name="CellFormat_FormatString" Type="String" />
        <asp:Parameter Name="GrandTotalCellFormat_FormatType" Type="String" />
        <asp:Parameter Name="GrandTotalCellFormat_FormatString" Type="String" />
        <asp:Parameter Name="GroupInterval" Type="String" />
        <asp:Parameter Name="FIELD_ID" Type="Int32" />
    </UpdateParameters>
    <SelectParameters>
        <asp:SessionParameter Name="RID" SessionField="RID" />
    </SelectParameters>
    <DeleteParameters>
        <asp:Parameter Name="FIELD_ID" Type="Int32" />
    </DeleteParameters>

</asp:SqlDataSource>



<asp:SqlDataSource ID="SqlDataSource_VIEWs" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"
    SelectCommand="select * from tblReport_VIEWs order by VIEW_TITLE "></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_PivotSummaryType" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"
    SelectCommand="select Value SummaryType,* from tblReport_MasterData where GroupName = 'PivotSummaryType' order by Name "></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_FilterArea" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"
    SelectCommand="select * from tblReport_MasterData where GroupName = 'FilterArea' order by Name "></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_FormatType" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"
    SelectCommand="select * from tblReport_MasterData where GroupName = 'FormatType' order by Name "></asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_PivotGroupInterval" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"
    SelectCommand="select * from tblReport_MasterData where GroupName = 'PivotGroupInterval' order by Name "></asp:SqlDataSource>


<dx:ASPxPopupControl ID="clientViewBI" runat="server" ClientInstanceName="clientViewBI"
    Modal="True"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="Business Intelligent"
    AllowDragging="true"
    AllowResize="True"
    EnableAnimation="true"
    CloseAction="CloseButton"
    EnableCallbackAnimation="true"
    EnableViewState="true"
    ShowPageScrollbarWhenModal="true"
    ShowMaximizeButton="true"
    Width="900"
    Height="790"
    FooterText=""
    ShowFooter="true">
    <ClientSideEvents Shown="function(s,e){ window.setTimeout(function() {LoadingPanel.Hide();},1000);}" />

    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl5" runat="server">
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>--%>
