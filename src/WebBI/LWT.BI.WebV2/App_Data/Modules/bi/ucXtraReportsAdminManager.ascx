<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucXtraReportsAdminManager.ascx.vb" Inherits="Modules_ucXtraReportsAdminManager" %>

<%--<script type="text/javascript">
    function ShowReportPopup(ReportID) {
        alert(ReportID);
        //window.parent.LoadingPanel.Show();
        //cbPreview.PerformCallback(BID);
    }
</script>--%>
<dx:ASPxCallback ID="cbPreview" runat="server" ClientInstanceName="cbPreview">
    <ClientSideEvents
        CallbackComplete="function(s, e) { 
            clientViewXtraReports.SetContentUrl('applications/XtraReports/Master/Preview.aspx');
            clientViewXtraReports.Show();
        }" />
</dx:ASPxCallback>
<dx:ASPxPopupControl ID="clientViewXtraReports" runat="server" ClientInstanceName="clientViewXtraReports"
    Modal="True" Maximized="true"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="XtraReports"
    AllowDragging="true"
    AllowResize="True"
    DragElement="Window"
    EnableAnimation="true"
    CloseAction="CloseButton"
    EnableCallbackAnimation="true"
    EnableViewState="true"
    ShowPageScrollbarWhenModal="true"
    ScrollBars="Auto"
    ShowMaximizeButton="true"
    HeaderImage-IconID="businessobjects_botask_32x32"
    HeaderStyle-BackColor="WindowFrame"
    Width="800"
    Height="680"
    FooterText=""
    ShowFooter="false">

    <HeaderStyle BackColor="#4796CE" ForeColor="White" />

    <ContentStyle>
        <Paddings Padding="0px" />
    </ContentStyle>

    <ClientSideEvents Shown="function(s,e){ 
                    LoadingPanel.Show();
                }"
        CloseButtonClick="function(s,e){
            grid.Refresh();
        }" />

    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="" runat="server" EnableAdaptivity="true" Width="100%" HeaderImage-IconID="businessobjects_bonote_32x32">
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxGridView ID="grid"
                ClientInstanceName="grid"
                runat="server"
                DataSourceID="SqlDataSource_Data"
                AutoGenerateColumns="False"
                Width="100%" KeyFieldName="GUID"
                SettingsPager-Mode="ShowAllRecords">
                <ClientSideEvents RowDblClick="function(s, e) {
                                                    var key = s.GetRowKey(e.visibleIndex);  
                                                    cbPreview.PerformCallback(key);
                                                }     
                                                 " />

                <SettingsBehavior AllowEllipsisInText="True" AllowFocusedRow="true" />
                <SettingsResizing ColumnResizeMode="NextColumn" />
                <SettingsEditing Mode="PopupEditForm" EditFormColumnCount="1">
                </SettingsEditing>
                <SettingsPopup>
                    <EditForm Modal="true" HorizontalAlign="Center" VerticalAlign="Middle" />
                </SettingsPopup>
                <Toolbars>
                    <dx:GridViewToolbar ItemAlign="Left">
                        <Items>
                            <dx:BootstrapGridViewToolbarItem>
                                <Template>

                                    <dx:ASPxButton ID="ASPxButton1"
                                        ClientInstanceName="bnNewReport"
                                        Border-BorderStyle="Groove" Border-BorderWidth="1" ForeColor="Black" BackColor="White"
                                        AutoPostBack="false"
                                        runat="server"
                                        Image-IconID="dashboards_dashboardtitle_16x16"
                                        ToolTip="New Title"
                                        Text="New Title">
                                        <ClientSideEvents Click="function(s,e){ 
                                  
                                            cbPreview.PerformCallback();
                                      
                                        }" />
                                    </dx:ASPxButton>
                                </Template>
                            </dx:BootstrapGridViewToolbarItem>
                            <%--<dx:GridViewToolbarItem Command="New" />--%>
                            <dx:GridViewToolbarItem Command="Edit" BeginGroup="true"></dx:GridViewToolbarItem>
                            <dx:GridViewToolbarItem Command="Refresh" BeginGroup="true" />

                        </Items>
                    </dx:GridViewToolbar>
                </Toolbars>
                <SettingsCommandButton>
                    <EditButton Image-IconID="edit_edit_16x16office2013" />
                    <UpdateButton Text="Save" Image-IconID="actions_save_16x16devav"></UpdateButton>
                    <CancelButton Image-IconID="actions_cancel_16x16office2013"></CancelButton>
                </SettingsCommandButton>

                <Columns>



                    <dx:GridViewDataColumn FieldName="ReportID" Width="100">
                        <EditFormSettings Visible="False" />
                    </dx:GridViewDataColumn>
                    <dx:GridViewDataTextColumn FieldName="DisplayName" Settings-AllowGroup="False" CellStyle-Wrap="False">
                        <PropertiesTextEdit ValidationSettings-RequiredField-IsRequired="true"></PropertiesTextEdit>
                    </dx:GridViewDataTextColumn>


                </Columns>
            </dx:ASPxGridView>

        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<asp:SqlDataSource ID="SqlDataSource_Data" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="select * from tblXtraReports"
    InsertCommand="INSERT INTO  tblXtraReports(DisplayName) VALUES (@DisplayName)"
    UpdateCommand="UPDATE tblXtraReports SET DisplayName = @DisplayName WHERE GUID = @GUID">
    <UpdateParameters>
        <asp:Parameter Name="DisplayName" />
        <asp:Parameter Name="GUID" />
    </UpdateParameters>
    <InsertParameters>
        <asp:Parameter Name="DisplayName" />
    </InsertParameters>

</asp:SqlDataSource>


