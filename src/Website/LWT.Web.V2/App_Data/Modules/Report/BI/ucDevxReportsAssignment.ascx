﻿<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxReportsAssignment.ascx.vb" Inherits="Modules_ucDevxReportsAssignment" %>

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="Report" runat="server" Width="100%">
    <PanelCollection>
        <dx:PanelContent>

            <dx:ASPxSplitter runat="server" ID="ASPxSplitter1" Width="1400" ResizingMode="Live">
                <Panes>
                    <dx:SplitterPane AutoHeight="True" Size="320" MinSize="110px">
                        <ContentCollection>
                            <dx:SplitterContentControl ID="SplitterContentControl1" runat="server"
                                SupportsDisabledAttribute="True">

                                <dx:ASPxGridView ID="gridUser" ClientInstanceName="gridUser" runat="server"
                                    DataSourceID="SqlDataSource_Users"
                                    KeyFieldName="sAMAccountName"
                                    AutoGenerateColumns="False" SettingsPager-Mode="EndlessPaging"
                                    EnableRowsCache="false" Width="300">
                                    <Columns>
                                        <%--<dx:GridViewDataColumn FieldName="sAMAccountName" Caption="User" CellStyle-Wrap ="False" />--%>

                                        <dx:GridViewDataColumn FieldName="displayName" CellStyle-Wrap="False" />
                                        <dx:GridViewDataColumn FieldName="title" Visible="false" />
                                        <dx:GridViewDataColumn FieldName="department" Visible="false" />
                                    </Columns>
                                    <Settings ShowColumnHeaders="false" />
                                    <SettingsSearchPanel Visible="true" />
                                    <SettingsBehavior AllowFocusedRow="True" />
                                    <ClientSideEvents  RowDblClick="function(s, e) { 
                                            s.GetRowValues(s.GetFocusedRowIndex(), 'sAMAccountName;title;department', function(values){
                                                LoadingPanel.Show();
                                                 DetailImage.SetImageUrl('http://lockthbnkedc01/sites/HRDweb/LWT%20Member%20Photos/' + values[0] +  '.jpg' ); 
                                                displayuser.SetVisible(true);   
                                                displaytitle.SetText(values[1]);
                                                displaydepartment.SetText(values[2]);

                                                
                                               

                                                callbackPanel_view.PerformCallback(values[0]);
                                            });
                                         }" />
                                </dx:ASPxGridView>
                                <br />

                                <dx:ASPxPanel runat="server" ID="displayuser" ClientInstanceName="displayuser" ClientVisible="false">
                                    <PanelCollection>

                                        <dx:PanelContent>
                                            <table style="width: 100%; height: 140px" class="OptionsTable TopMargin">
                                                <tr>
                                                    <td>
                                                        <dx:ASPxImage runat="server" ID="DetailImage" ClientInstanceName="DetailImage" Width="100px" />
                                                        <br />
                                                        ตำแหน่ง :<dx:ASPxLabel runat="server" ID="displaytitle" ClientInstanceName="displaytitle"></dx:ASPxLabel>
                                                        <br />
                                                        แผนก :<dx:ASPxLabel runat="server" ID="displaydepartment" ClientInstanceName="displaydepartment"></dx:ASPxLabel>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <dx:ASPxButton ID="btnSave"
                                                            ClientInstanceName="btnSave"
                                                            runat="server" Text="Save"
                                                            AutoPostBack="false">
                                                            <ClientSideEvents Click="function(s,e) {
                                                     
                                                    LoadingPanel.Show();
                                                    cbSave.PerformCallback('');                                                                                  

                                            }" />
                                                        </dx:ASPxButton>

                                                        <dx:ASPxCallback ID="cbSave" runat="server" ClientInstanceName="cbSave">
                                                            <ClientSideEvents
                                                                CallbackComplete="function(s, e) { 
                                                                LoadingPanel.Hide();  
                                                                
                                                                }" />
                                                        </dx:ASPxCallback>

                                                    </td>
                                                </tr>
                                            </table>

                                        </dx:PanelContent>
                                    </PanelCollection>


                                </dx:ASPxPanel>



                            </dx:SplitterContentControl>
                        </ContentCollection>
                    </dx:SplitterPane>
                    <dx:SplitterPane AutoHeight="True" Size="70%" MinSize="280px">
                        <ContentCollection>
                            <dx:SplitterContentControl ID="SplitterContentControl2" runat="server"
                                SupportsDisabledAttribute="True">
                                <dx:ASPxCallbackPanel runat="server" ID="callbackPanel_view"
                                    ClientInstanceName="callbackPanel_view" ClientVisible="false"
                                    Height="100%" Width="100%">
                                    <ClientSideEvents
                                        CallbackError="function(s,e){LoadingPanel.Hide();}"
                                        EndCallback="function(s,e){
                                        LoadingPanel.Hide();
                                        callbackPanel_view.SetVisible(true);
                                        treeList.PerformCallback('');
                                        }" />
                                    <PanelCollection>
                                        <dx:PanelContent ID="PanelContent1">




                                            <dx:ASPxTreeList ID="treeList" runat="server" ClientInstanceName="treeList"
                                                AutoGenerateColumns="False" DataSourceID="SqlDataSource_Report"
                                                Width="100%"
                                                KeyFieldName="RID"
                                                ParentFieldName="ParentID">
                                                <Columns>
                                                 


                                                    <%--  <dx:TreeListSpinEditColumn FieldName="No" Width="50" CellStyle-HorizontalAlign="Left">
                                                        <PropertiesSpinEdit>
                                                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                                                        </PropertiesSpinEdit>
                                                    </dx:TreeListSpinEditColumn>--%>

                                                    <dx:TreeListTextColumn FieldName="TITLE" Caption="Report" Width="200" CellStyle-Wrap="False">
                                                        <PropertiesTextEdit>
                                                            <ValidationSettings Display="Dynamic">
                                                                <RequiredField IsRequired="true" />
                                                            </ValidationSettings>
                                                        </PropertiesTextEdit>

                                                    </dx:TreeListTextColumn>

                                                    <dx:TreeListMemoColumn FieldName="DESCRIPTION" Width="500" CellStyle-Wrap="True">
                                                    </dx:TreeListMemoColumn>

                                                </Columns>
                                                <SettingsBehavior AutoExpandAllNodes="true" ExpandCollapseAction="NodeDblClick" ProcessSelectionChangedOnServer="True" />
                                                <SettingsSelection AllowSelectAll="true" Enabled="True" Recursive="True" />
                                                <%--<ClientSideEvents SelectionChanged="treeList_SelectionChanged" CustomDataCallback="treeList_CustomDataCallback" />--%>
                                            </dx:ASPxTreeList>


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


<asp:SqlDataSource ID="SqlDataSource_Users" runat="server" ConnectionString="<%$ ConnectionStrings:PortalConnectionString %>"
    SelectCommand="select * from V_LWT_USER order by sAMAccountName "></asp:SqlDataSource>


<asp:SqlDataSource ID="SqlDataSource_Report" runat="server" ConnectionString="<%$ ConnectionStrings:LWTReportsConnectionString %>"
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
        where REPORT_TYPE='BI'
        order by dbo.tblReport.No
    ">
    <SelectParameters>
        <asp:SessionParameter Name="UserName" SessionField="assignusername" DefaultValue="" DbType="String" />
    </SelectParameters>
</asp:SqlDataSource>