<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucUserTabs.ascx.vb" Inherits="ucUserTabs" %>
 <script type="text/javascript">
     function ClearSelection() {
         TreeList2.SetFocusedNodeKey("");
         UpdateControls(null, "");
     }
     function UpdateSelection() {
         var defaultPage = "";
         var focusedNodeKey = TreeList2.GetFocusedNodeKey();
         if (focusedNodeKey != "")
             defaultPage = TreeList2.cpDefaultPage[focusedNodeKey];
         UpdateControls(focusedNodeKey, defaultPage);
     }
     function UpdateControls(key, text) {
         DropDownEdit.SetText(text);
         DropDownEdit.SetKeyValue(key);
         DropDownEdit.HideDropDown();
         UpdateButtons();
     }
     function UpdateButtons() {
         clearButton.SetEnabled(DropDownEdit.GetText() != "");
         selectButton.SetEnabled(TreeList2.GetFocusedNodeKey() != "");
     }
     function OnDropDown() {
         TreeList2.SetFocusedNodeKey(DropDownEdit.GetKeyValue());
         TreeList2.MakeNodeVisible(TreeList2.GetFocusedNodeKey());
     }
    </script>




<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="" runat="server" EnableAdaptivity="true" Width="100%" HeaderImage-IconID="support_feature_16x16office2013">
    <PanelCollection>
        <dx:PanelContent>






            <table>
                <tr>
                    <td>

                        <dx:ASPxGridView ID="gridUser" ClientInstanceName="gridUser" runat="server"
                            DataSourceID="SqlDataSource_Users"
                            KeyFieldName="UserName" EnableAdaptivity="true"
                            AutoGenerateColumns="False" SettingsPager-Mode="EndlessPaging"
                            EnableRowsCache="false" Width="300">
                            <Columns>
                                <%--<dx:GridViewDataColumn FieldName="sAMAccountName" Caption="User" CellStyle-Wrap ="False" />--%>

                                <dx:GridViewDataColumn FieldName="UserName" CellStyle-Wrap="False" Width="60" />
                                <dx:GridViewDataColumn FieldName="Email" />
                            </Columns>
                            <Settings ShowColumnHeaders="false" />
                            <SettingsSearchPanel Visible="true" />
                            <SettingsBehavior AllowFocusedRow="True" />
                            <ClientSideEvents RowDblClick="function(s, e) { 
                                                s.GetRowValues(s.GetFocusedRowIndex(), 'UserName;Email;Comment', function(values){
                                                LoadingPanel.Show();
                                                DetailImage.SetImageUrl('./images/user.png' ); 


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
                                                Email :<dx:ASPxLabel runat="server" ID="displaytitle" ClientInstanceName="displaytitle"></dx:ASPxLabel>
                                                <br />
                                                Note :<dx:ASPxLabel runat="server" ID="displaydepartment" ClientInstanceName="displaydepartment"></dx:ASPxLabel>
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
                                                            var defaultPage = '';
                                                            var focusedNodeKey = TreeList2.GetFocusedNodeKey();

                                                     

                                                            if (focusedNodeKey != '')
                                                                defaultPage = TreeList2.cpDefaultPage[focusedNodeKey];
                                                           
                                                        
                                                         cbSave.PerformCallback(focusedNodeKey);                                                                                  

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


                    </td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>

                    <td style="vertical-align: top">


               


                        <dx:ASPxCallbackPanel runat="server" ID="callbackPanel_view"
                            ClientInstanceName="callbackPanel_view" ClientVisible="false"
                            EnableAdaptivity="true" Width="100%">
                            <ClientSideEvents
                                CallbackError="function(s,e){LoadingPanel.Hide();}"
                                
                                EndCallback="function(s,e){
                                            LoadingPanel.Hide();
                                        

                                            callbackPanel_view.SetVisible(true);

                                            treeList.PerformCallback('');
                                     
                                            TreeList2.PerformCallback('');

                                            if(s.cpDefaultTabId !='')
                                            {
                                                UpdateControls(s.cpDefaultTabId, s.cpDefaultTabName);
                                            }
                                            else
                                            {
                                                ClearSelection();
                                            }
                                        
                                        }" />
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1">




         <dx:ASPxDropDownEdit ID="DropDownEdit" runat="server" ClientInstanceName="DropDownEdit"  
                            Width="170px" AllowUserInput="False" AnimationType="None">
                            
                            <ClientSideEvents Init="UpdateSelection" DropDown="OnDropDown" />


                            <DropDownWindowTemplate>
                                <div>
                                    <dx:ASPxTreeList ID="TreeList2" ClientInstanceName="TreeList2" runat="server"
                                        Width="500px" SettingsBehavior-AutoExpandAllNodes="true"
                                        DataSourceID="SqlDataSource_UserTabs" DataCacheMode="Disabled"  
                                        OnCustomJSProperties="TreeList2_CustomJSProperties"
                                        KeyFieldName="TabId" 
                                        ParentFieldName="ParentID">
                                        <Settings VerticalScrollBarMode="Auto" ScrollableHeight="150" />
                                        <ClientSideEvents FocusedNodeChanged="function(s,e){ selectButton.SetEnabled(true); }" />
                                        <BorderBottom BorderStyle="Solid" />
                                        <SettingsBehavior AllowFocusedNode="true" AutoExpandAllNodes="true" FocusNodeOnLoad="false" />
                                        <SettingsPager Mode="ShowAllNodes">
                                        </SettingsPager>
                                        <Styles>
                                            <Node Cursor="pointer">
                                            </Node>
                                            <Indent Cursor="default">
                                            </Indent>
                                        </Styles>
                                        <Columns>
                                            <dx:TreeListTextColumn FieldName="TabName" VisibleIndex="1">
                                            </dx:TreeListTextColumn>
                                          
                                        </Columns>
                                    </dx:ASPxTreeList>
                                </div>
                                <table style="background-color: White; width: 100%;">
                                    <tr>
                                        <td style="padding: 10px;">
                                            <dx:ASPxButton ID="clearButton" ClientEnabled="false" ClientInstanceName="clearButton"
                                                runat="server" AutoPostBack="false" Text="Clear">
                                                <ClientSideEvents Click="ClearSelection" />
                                            </dx:ASPxButton>
                                        </td>
                                        <td style="text-align: right; padding: 10px;">
                                            <dx:ASPxButton ID="selectButton" ClientEnabled="false" ClientInstanceName="selectButton"
                                                runat="server" AutoPostBack="false" Text="Select">
                                                <ClientSideEvents Click="UpdateSelection" />
                                            </dx:ASPxButton>
                                            <dx:ASPxButton ID="closeButton" runat="server" AutoPostBack="false" Text="Close">
                                                <ClientSideEvents Click="function(s,e) { DropDownEdit.HideDropDown(); }" />
                                            </dx:ASPxButton>
                                        </td>
                                    </tr>
                                </table>
                            </DropDownWindowTemplate>
                        </dx:ASPxDropDownEdit>





                                    <dx:ASPxTreeList ID="treeList" runat="server" ClientInstanceName="treeList"
                                        AutoGenerateColumns="False" DataSourceID="SqlDataSource_UserTabs"
                                        EnableAdaptivity="true" Width="100%"
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


                    </td>
                </tr>
            </table>















        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>


<asp:SqlDataSource ID="SqlDataSource_Users" runat="server" ConnectionString="<%$ ConnectionStrings:PortalConnectionString %>"
    SelectCommand="select * from Portal_Users order by UserName "></asp:SqlDataSource>


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
