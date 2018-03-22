<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucUserTabs.ascx.vb" Inherits="Modules_ucUserTabs" %>
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
                                    <ClientSideEvents RowClick="function(s, e) { 
                                                s.GetRowValues(s.GetFocusedRowIndex(), 'sAMAccountName;title;department', function(values){
                                               //LoadingPanel.Show();
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
                                                AutoGenerateColumns="False" DataSourceID="SqlDataSource_UserTabs"
                                                Width="100%"
                                                KeyFieldName="TabId"
                                                ParentFieldName="ParentID">
                                                <Columns>
                                                    <dx:TreeListTextColumn FieldName="TabName" Caption="Name" Width="200" CellStyle-Wrap="False">
                                                    </dx:TreeListTextColumn>



                                                </Columns>
                                                <SettingsBehavior AutoExpandAllNodes="true"
                                                    ExpandCollapseAction="NodeDblClick"
                                                    ProcessSelectionChangedOnServer="True" />
                                                <SettingsSelection
                                                    AllowSelectAll="true"
                                                    Enabled="True"
                                                    Recursive="true" />
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


<asp:SqlDataSource ID="SqlDataSource_UserTabs" runat="server" ConnectionString="<%$ ConnectionStrings:PortalConnectionString %>"
    SelectCommand=" 
    select *
     from
     ( 
  
     
       select distinct
       Portal_TabRoles.TabId    
      ,PortalCfg_Tabs.TabName
      ,PortalCfg_Tabs.TabOrder as OrderBy
      ,case when ParentId = 1 then PortalId*-1 else ParentId end ParentId
      ,PortalCfg_Tabs.PortalId 
      from Portal_TabRoles  
      inner join dbo.PortalCfg_Tabs on PortalCfg_Tabs.TabId = Portal_TabRoles.TabId
      inner join Portal_UserRoles on Portal_TabRoles.RoleId=[Portal_UserRoles].RoleID 
      inner join Portal_Users on [Portal_UserRoles].UserID = Portal_Users.UserID  
      where PortalCfg_Tabs.PortalId=@PortalId and Portal_Users.UserName=@UserName
  
    ) a  
      order by PortalId, OrderBy
    ">
    <SelectParameters>
        <asp:SessionParameter Name="UserName" SessionField="UserName" />
        <asp:SessionParameter Name="PortalId" SessionField="PortalId" />
    </SelectParameters>
</asp:SqlDataSource>
