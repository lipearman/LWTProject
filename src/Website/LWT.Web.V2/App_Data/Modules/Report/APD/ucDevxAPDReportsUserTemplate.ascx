<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxAPDReportsUserTemplate.ascx.vb" Inherits="Modules_ucDevxAPDReportsUserTemplate" %>



<script type="text/javascript">
    function ShowDrillDown() {
        var mainElement = pivotGrid.GetMainElement();
        DrillDownWindow.ShowAtPos(ASPxClientUtils.GetAbsoluteX(mainElement), ASPxClientUtils.GetAbsoluteY(mainElement));
    }


    function ShowUserBIPopup(RID) {
        window.top.LoadingPanel.Show();
        cbPreview.PerformCallback(RID);
    }
</script>
<dx:ASPxCallback ID="cbPreview" runat="server" ClientInstanceName="cbPreview">
    <ClientSideEvents CallbackError="function(s, e) { 
            window.top.LoadingPanel.Hide(); 
        }"
        CallbackComplete="function(s, e) { 
            window.top.LoadingPanel.Hide(); 
            window.top.clientView.SetContentUrl('applications/BI/User/Preview.aspx');
            window.top.clientView.Show();            
        }" />
</dx:ASPxCallback>

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
                                      <%--  <dx:TreeListTextColumn FieldName="RID"   CellStyle-Wrap="False">
                                            
                                        </dx:TreeListTextColumn>--%>
                                       
                                    </Columns>

                                </dx:ASPxTreeList>




        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>


<%-- <dx:LinqServerModeDataSource ID="ObjectDataSource1" ContextTypeName="DataClasses_LWTReportsExt" TableName="tblReports" runat="server" />--%>

<asp:SqlDataSource ID="SqlDataSource_Data" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"
    
     SelectCommand="select * from tblReport where REPORT_TYPE='BI' order by No "
    >


</asp:SqlDataSource>

<%--<asp:SqlDataSource ID="SqlDataSource_Data" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"
    SelectCommand="
     SELECT dbo.tblReport.RID
        , dbo.tblReport.No
        , dbo.tblReport.TITLE
        , dbo.tblReport.DESCRIPTION
        , dbo.tblReport.DEPARTMENT
        , dbo.tblReport.ParentID
        , dbo.tblReport.VIEW_ID
        , dbo.tblReport.DB_ID
        , dbo.tblReport.REPORT_TYPE
        , dbo.tblReport_Assignment.UserName
        , case when tblReport_Assignment.UserName IS not null then tblReport.RID end SelectRID
        FROM  dbo.tblReport 
        left JOIN dbo.tblReport_Assignment ON dbo.tblReport.RID = dbo.tblReport_Assignment.RID    
        and UserName=@UserName
        order by dbo.tblReport.No
    ">

     <SelectParameters>
         <asp:Parameter Name="UserName" /> 
    </SelectParameters>

</asp:SqlDataSource>--%>




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
