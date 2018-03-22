<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucRoles.ascx.vb" Inherits="Modules_ucRoles" %>

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="" runat="server" EnableAdaptivity="true" Width="100%" HeaderImage-IconID="support_feature_16x16office2013" >
    <PanelCollection>
        <dx:PanelContent>

            <table>
                <tr>
                    <td>
                        <dx:ASPxTreeList runat="server" EnableAdaptivity="true" Width="300" ID="tree" SettingsLoadingPanel-Enabled="false"
                            ClientInstanceName="tree" EnableSynchronization="True"
                            DataSourceID="SqlDataSource_roles"
                            KeyFieldName="RoleID"
                            ParentFieldName="ParentId">
                            <ClientSideEvents CustomButtonClick="function(s, e) {
                                        LoadingPanel.Show();
                                        tree.PerformCallback(e.buttonID);
                                        callbackPanel_tabs.SetVisible(true);
                                        callbackPanel_tabs.PerformCallback('');
                                                                    
                                    }" />
                            <Columns>
                                <dx:TreeListCommandColumn ShowNewButtonInHeader="false">

                                    <NewButton Visible="true" Image-IconID="reports_addheader_16x16office2013" Text=" " Image-ToolTip="New Role" />
                                    <UpdateButton Text="Save"></UpdateButton>
                                    <EditButton Visible="true" Image-IconID="actions_editname_16x16" Text=" " />
                                </dx:TreeListCommandColumn>

                                <dx:TreeListTextColumn FieldName="RoleName" CellStyle-Wrap="False">
                                    <PropertiesTextEdit>
                                        <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                                    </PropertiesTextEdit>
                                </dx:TreeListTextColumn>

                                <dx:TreeListCommandColumn ButtonType="Image">
                                    <CustomButtons>
                                        <dx:TreeListCommandColumnCustomButton ID="cmdEdit">
                                            <Image IconID="edit_customization_16x16"></Image>
                                        </dx:TreeListCommandColumnCustomButton>
                                    </CustomButtons>

                                </dx:TreeListCommandColumn>

                            </Columns>

                            <SettingsBehavior AllowFocusedNode="true" />
                            <SettingsEditing AllowNodeDragDrop="true" />
                        </dx:ASPxTreeList>


                    </td>
                    <td>&nbsp;&nbsp;&nbsp;&nbsp;</td>

                    <td style="vertical-align: top">




                        <dx:ASPxCallbackPanel runat="server" ID="callbackPanel_tabs" Height="500" SettingsLoadingPanel-Enabled="false"
                            ClientInstanceName="callbackPanel_tabs" Width="100%"
                            ClientVisible="false">
                            <ClientSideEvents
                                CallbackError="function(s,e){LoadingPanel.Hide();}"
                                EndCallback="function(s,e){
                                        LoadingPanel.Hide();
                                        //callbackPanel_view.SetVisible(true);
                                        treeList.PerformCallback('');
                                        }" />
                            <PanelCollection>
                                <dx:PanelContent ID="PanelContent1">

                                    <dx:ASPxLabel runat="server" ID="displayRole" ClientInstanceName="displayRole"></dx:ASPxLabel>

                                    <br />
                                    <dx:ASPxTreeList ID="treeList" runat="server" ClientInstanceName="treeList" SettingsLoadingPanel-Enabled="false"
                                        AutoGenerateColumns="False" DataSourceID="SqlDataSource_Tabs" EnableAdaptivity="true" Width="100%"
  SettingsBehavior-AutoExpandAllNodes="true"
                                        KeyFieldName="TabId"
                                        ParentFieldName="ParentId">
                                        <Columns>
                                            <%--   <dx:TreeListDataColumn FieldName="TabId" Width="500" CellStyle-Wrap="True">
                                                    </dx:TreeListDataColumn>--%>
                                            <dx:TreeListDataColumn FieldName="TabName"  CellStyle-Wrap="True">
                                            </dx:TreeListDataColumn>





                                        </Columns>
                                        <Settings ScrollableHeight=" 400" VerticalScrollBarMode="Visible" />
                                        <SettingsBehavior AutoExpandAllNodes="true" ExpandCollapseAction="NodeDblClick" ProcessSelectionChangedOnServer="True" />
                                        <SettingsSelection AllowSelectAll="true" Enabled="True" Recursive="True" />

                                    </dx:ASPxTreeList>
                                    <dx:ASPxButton ID="btnSaveTabRoles" Image-IconID="actions_save_16x16devav"
                                        ClientInstanceName="btnSaveTabRoles"
                                        runat="server" Text="Save"
                                        AutoPostBack="false">
                                        <ClientSideEvents Click="function(s,e) {
                                                    LoadingPanel.Show();
                                                    cbSaveTabRoles.PerformCallback('');
                                            }" />
                                    </dx:ASPxButton>

                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxCallbackPanel>

                    </td>
                </tr>
            </table>






 
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<dx:ASPxCallback ID="cbSaveTabRoles" runat="server" ClientInstanceName="cbSaveTabRoles">
    <ClientSideEvents CallbackError="function(s, e) { LoadingPanel.Hide(); }"
        CallbackComplete="function(s, e) { LoadingPanel.Hide(); }" />
</dx:ASPxCallback>
<asp:SqlDataSource ID="SqlDataSource_roles" runat="server" ConnectionString="<%$ ConnectionStrings:PortalConnectionString %>"
    SelectCommand="
      select *
      from 
            (
                select PortalId*-1 as RoleID
                , 'Project: ' + PortalName as RoleName
                , null as  ParentId
                , PortalId
                FROM PortalCfg_Globals 
                Where PortalId=@PortalId

                union all

                SELECT RoleID as RoleID
                    ,RoleName as RoleName
                    ,PortalId*-1 as ParentId
                    ,PortalId
                FROM Portal_Roles 
                Where PortalId=@PortalId
            ) a
      order by RoleID,ParentId,RoleName    
    "
    UpdateCommand="update Portal_Roles 
    set RoleName=@RoleName
    ,ModifyDate = getdate()
    Where RoleID=@RoleID"
    InsertCommand="Insert into Portal_Roles(
                                            PortalID
                                            ,RoleCode
                                            ,RoleName
                                            ,RoleDescription,CreateDate )
    values(
       @PortalId
     , @RoleCode
     , @RoleName
     , @RoleDescription
    ,getdate()
    )



    ">
    <InsertParameters>
        <asp:SessionParameter SessionField="PortalId" Name="PortalId" />
        <asp:Parameter Name="RoleCode" />
        <asp:Parameter Name="RoleName" />
        <asp:Parameter Name="RoleDescription" />
    </InsertParameters>


    <UpdateParameters>
        <asp:Parameter Name="RoleName" />
        <asp:Parameter Name="RoleID" DbType="Int32" />
    </UpdateParameters>



    <SelectParameters>
        <asp:SessionParameter Name="PortalId" SessionField="PortalId" />
    </SelectParameters>


</asp:SqlDataSource>




<asp:SqlDataSource ID="SqlDataSource_Tabs" runat="server" ConnectionString="<%$ ConnectionStrings:PortalConnectionString %>"
    SelectCommand="

        SELECT
         PortalCfg_Tabs.TabId
        ,PortalCfg_Tabs.TabName
        ,PortalCfg_Tabs.TabOrder
        ,PortalCfg_Tabs.AccessRoles
        ,PortalCfg_Tabs.ShowMobile
        ,PortalCfg_Tabs.MobileTabName
        ,PortalCfg_Tabs.PortalId
        ,PortalCfg_Tabs.ParentId
        ,PortalCfg_Tabs.CreateDate
        ,PortalCfg_Tabs.ModifyDate
        ,PortalCfg_Tabs.PageID

        ,case when Portal_TabRoles.TabId IS not null then Portal_TabRoles.TabId end SelectTabId

        FROM  dbo.PortalCfg_Tabs 
        left JOIN dbo.Portal_TabRoles ON dbo.Portal_TabRoles.TabID = dbo.PortalCfg_Tabs.TabID
        and Portal_TabRoles.RoleID=@RoleID
        where PortalId=@PortalId  
        order by ParentId, TabOrder  


    ">
    <SelectParameters>
        <asp:SessionParameter SessionField="PortalId" Name="PortalId" />
        <asp:SessionParameter SessionField="RoleID" Name="RoleID" />
    </SelectParameters>
</asp:SqlDataSource>
