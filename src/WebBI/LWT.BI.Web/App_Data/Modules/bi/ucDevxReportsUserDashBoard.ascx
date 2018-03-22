<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxReportsUserDashBoard.ascx.vb" Inherits="Modules_ucDevxReportsUserDashBoard" %>

<script type="text/javascript">
    function ShowDrillDown() {
        var mainElement = pivotGrid.GetMainElement();
        DrillDownWindow.ShowAtPos(ASPxClientUtils.GetAbsoluteX(mainElement), ASPxClientUtils.GetAbsoluteY(mainElement));
    }


    function ShowUserDBPopup(DB_ID) {
 
        //LoadingPanel.Show();
        //clientViewDB.SetContentUrl('applications/DB/Preview.aspx?RID=' + RID);
        //clientViewDB.Show();
        cbPreview.PerformCallback(DB_ID);
    }
</script>
<dx:ASPxCallback ID="cbPreview" runat="server" ClientInstanceName="cbPreview">
    <ClientSideEvents CallbackError="function(s, e) { 
            //LoadingPanel.Hide(); 
        }"
        CallbackComplete="function(s, e) { 
            //LoadingPanel.Hide(); 
            window.top.clientView.SetContentUrl('applications/DB/User/Preview.aspx');
            window.top.clientView.Show();            
        }" />
</dx:ASPxCallback>


<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="Report" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>


    <dx:ASPxTreeList ID="treeList" runat="server" AutoGenerateColumns="false" Width="900"
                                    DataSourceID="SqlDataSource_Data"
                                    SettingsBehavior-AutoExpandAllNodes="true"
                                    KeyFieldName="DB_ID"
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
                                                <%# PreviewDB(Eval("DB_ID").ToString())%>
                                            </DataCellTemplate>
                                        </dx:TreeListTextColumn>

                                    </Columns>

                                </dx:ASPxTreeList>




        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<asp:SqlDataSource ID="SqlDataSource_Data" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="select * from tblDashBoard  order by No "></asp:SqlDataSource>




<dx:ASPxPopupControl ID="clientViewDB" runat="server" ClientInstanceName="clientViewDB"
    Modal="True"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="Business Intelligence"
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
