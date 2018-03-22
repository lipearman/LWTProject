<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucTabs.ascx.vb" Inherits="Modules_ucTabs" %>
<script type="text/javascript">
    function AddSelectedItems() {
        MoveSelectedItems(lbAvailable, lbChoosen);
        UpdateButtonState();
    }
    function AddAllItems() {
        MoveAllItems(lbAvailable, lbChoosen);
        UpdateButtonState();
    }
    function RemoveSelectedItems() {
        MoveSelectedItems(lbChoosen, lbAvailable);
        UpdateButtonState();
    }
    function RemoveAllItems() {
        MoveAllItems(lbChoosen, lbAvailable);
        UpdateButtonState();
    }
    function MoveSelectedItems(srcListBox, dstListBox) {
        srcListBox.BeginUpdate();
        dstListBox.BeginUpdate();
        var items = srcListBox.GetSelectedItems();
        for (var i = items.length - 1; i >= 0; i = i - 1) {
            dstListBox.AddItem(items[i].text, items[i].value);
            srcListBox.RemoveItem(items[i].index);
        }
        srcListBox.EndUpdate();
        dstListBox.EndUpdate();
    }
    function MoveAllItems(srcListBox, dstListBox) {
        srcListBox.BeginUpdate();
        var count = srcListBox.GetItemCount();
        for (var i = 0; i < count; i++) {
            var item = srcListBox.GetItem(i);
            dstListBox.AddItem(item.text, item.value);
        }
        srcListBox.EndUpdate();
        srcListBox.ClearItems();
    }
    function UpdateButtonState() {

        btnMoveAllItemsToRight.SetEnabled(lbAvailable.GetItemCount() > 0);
        btnMoveAllItemsToLeft.SetEnabled(lbChoosen.GetItemCount() > 0);
        btnMoveSelectedItemsToRight.SetEnabled(lbAvailable.GetSelectedItems().length > 0);
        btnMoveSelectedItemsToLeft.SetEnabled(lbChoosen.GetSelectedItems().length > 0);

    }


    function GetAllItems(srcListBox) {
        var items = ""
        var count = srcListBox.GetItemCount();
        for (var i = 0; i < count; i++) {
            var item = srcListBox.GetItem(i);
            if (i == 0) {
                items = '' + item.value
            }
            else {
                items = items + ',' + item.value
            }
        }


        return items
    }
</script>

<dx:ASPxGlobalEvents ID="GlobalEvents" runat="server">
    <ClientSideEvents ControlsInitialized="function(s, e) { UpdateButtonState(); }" />
</dx:ASPxGlobalEvents>
<br />






<asp:SqlDataSource ID="SqlDataSource_Site" runat="server" ConnectionString="<%$ ConnectionStrings:PortalConnectionString %>"
    SelectCommand="select * from PortalCfg_Globals order by PortalId "></asp:SqlDataSource>

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="" runat="server" EnableAdaptivity="true" Width="100%" HeaderImage-IconID="businessobjects_bonote_32x32" >
    <PanelCollection>
        <dx:PanelContent>


<table>
    <tr>
        <td style="vertical-align:top">
            <dx:ASPxTreeList runat="server" Width="300" ID="tree"
                ClientInstanceName="tree" EnableSynchronization="True"
                DataSourceID="SqlDataSource_tabs" SettingsLoadingPanel-Enabled="false"
                KeyFieldName="TabId"
                ParentFieldName="ParentId">
                <ClientSideEvents CustomButtonClick="function(s, e) {

                            LoadingPanel.Show();
                            tree.PerformCallback(e.buttonID);
                            callbackPanel.SetVisible(true);
                            callbackPanel.PerformCallback('');

                           
                            
                        }" />
                <Columns>
                    <dx:TreeListCommandColumn>
                        <NewButton Visible="true" Image-IconID="reports_addheader_16x16office2013" Text=" " Image-ToolTip="New Page" />
                        <EditButton Visible="true" Image-IconID="actions_editname_16x16" Text=" " />
                    </dx:TreeListCommandColumn>


                    <dx:TreeListTextColumn FieldName="Name" CellStyle-Wrap="False">
                        <PropertiesTextEdit>
                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                        </PropertiesTextEdit>
                    </dx:TreeListTextColumn>

                    <dx:TreeListCommandColumn ButtonType="Image">
                        <CustomButtons>
                            <dx:TreeListCommandColumnCustomButton ID="cmdEdit">
                                <Image IconID="edit_customization_16x16"></Image>
                            </dx:TreeListCommandColumnCustomButton>
                            <dx:TreeListCommandColumnCustomButton ID="cmdMoveUp">
                                <Image IconID="arrows_moveup_16x16office2013"></Image>
                            </dx:TreeListCommandColumnCustomButton>
                            <dx:TreeListCommandColumnCustomButton ID="cmdMoveDown">
                                <Image IconID="arrows_movedown_16x16office2013"></Image>
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

            <dx:ASPxCallbackPanel runat="server" ID="callbackPanel" SettingsLoadingPanel-Enabled="false"
                ClientInstanceName="callbackPanel" ClientVisible="false"
                Width="100%">
                <ClientSideEvents
                    CallbackError="function(s,e){LoadingPanel.Hide();}"
                    EndCallback="function(s,e){ LoadingPanel.Hide(); }" />
                <PanelCollection>
                    <dx:PanelContent ID="PanelContent1"> 
                        <dx:ASPxRoundPanel ID="pnTabName" EnableAnimation="true" ShowCollapseButton="true"
                            HeaderText="Data Source" Font-Bold="true"
                            EnableAdaptivity="true"
                            runat="server" Width="120%">
                            <PanelCollection>
                                <dx:PanelContent>



                                    <table style="width: 100%">

                                        <tr>
                                            <td style="width: 35%">
                                                <dx:ASPxListBox ID="lbAvailable" runat="server" EnableViewState="false"
                                                    ClientInstanceName="lbAvailable" DataSourceID="SqlDataSource_ModuleDefinitions"
                                                    ValueField="ModuleDefId" TextField="ModuleDefName" EnableSynchronization="True"
                                                    Width="100%" Height="240px" SelectionMode="Single" Caption="Available">
                                                    <CaptionSettings Position="Top" />

                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) { UpdateButtonState(); }" />
                                                </dx:ASPxListBox>
                                            </td>
                                            <td style="padding: 10px 60px;">
                                                <div>
                                                    <dx:ASPxButton ID="btnMoveSelectedItemsToRight" runat="server" ClientInstanceName="btnMoveSelectedItemsToRight"
                                                        AutoPostBack="False" Text="Add >" Width="130px" ClientEnabled="False"
                                                        ToolTip="Add selected items">
                                                        <ClientSideEvents Click="function(s, e) { AddSelectedItems(); }" />
                                                    </dx:ASPxButton>
                                                </div>
                                                <div class="TopPadding">
                                                    <dx:ASPxButton ID="btnMoveAllItemsToRight" runat="server" ClientInstanceName="btnMoveAllItemsToRight"
                                                        AutoPostBack="False" Text="Add All >>" Width="130px" ToolTip="Add all items">
                                                        <ClientSideEvents Click="function(s, e) { AddAllItems(); }" />
                                                    </dx:ASPxButton>
                                                </div>
                                                <div style="height: 32px">
                                                </div>
                                                <div>
                                                    <dx:ASPxButton ID="btnMoveSelectedItemsToLeft" runat="server" ClientInstanceName="btnMoveSelectedItemsToLeft"
                                                        AutoPostBack="False" Text="< Remove" Width="130px" ClientEnabled="False"
                                                        ToolTip="Remove selected items">
                                                        <ClientSideEvents Click="function(s, e) { RemoveSelectedItems(); }" />
                                                    </dx:ASPxButton>
                                                </div>
                                                <div class="TopPadding">
                                                    <dx:ASPxButton ID="btnMoveAllItemsToLeft" runat="server" ClientInstanceName="btnMoveAllItemsToLeft"
                                                        AutoPostBack="False" Text="<< Remove All" Width="130px" ClientEnabled="False"
                                                        ToolTip="Remove all items">
                                                        <ClientSideEvents Click="function(s, e) { RemoveAllItems(); }" />
                                                    </dx:ASPxButton>
                                                </div>
                                            </td>
                                            <td style="width: 35%">
                                                <dx:ASPxListBox ID="lbChoosen" runat="server" EnableViewState="false" ClientInstanceName="lbChoosen" EnableSynchronization="True"
                                                    DataSourceID="SqlDataSource_Modules" ValueField="ModuleId" TextField="ModuleTitle" Width="100%"
                                                    Height="240px" SelectionMode="Single" Caption="Chosen">
                                                    <CaptionSettings Position="Top" />
                                                    <ClientSideEvents SelectedIndexChanged="function(s, e) { UpdateButtonState(); }"></ClientSideEvents>
                                                </dx:ASPxListBox>
                                            </td>
                                        </tr>
                                    </table>


                                </dx:PanelContent>
                            </PanelCollection>
                        </dx:ASPxRoundPanel>


                        <br />
                        <dx:ASPxButton ID="btnSaveModule" Image-IconID="actions_save_16x16devav"
                            ClientInstanceName="btnSaveModule"
                            runat="server" Text="Save"
                            AutoPostBack="false">
                            <ClientSideEvents Click="function(s,e) {
                                                    LoadingPanel.Show();
                                                    cbSaveModule.PerformCallback(GetAllItems(lbChoosen));
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

<dx:ASPxCallback ID="cbSaveModule" runat="server" ClientInstanceName="cbSaveModule">
    <ClientSideEvents CallbackError="function(s, e) { LoadingPanel.Hide(); }"
        CallbackComplete="function(s, e) { LoadingPanel.Hide(); }" />
</dx:ASPxCallback>



<asp:SqlDataSource ID="SqlDataSource_tabs" runat="server"
    ConnectionString="<%$ ConnectionStrings:PortalConnectionString %>"
    SelectCommand="  

        select *
            from 
        (
            select PortalId*-1 as TabId
            ,'Project: ' + PortalName as Name
            , 0 as OrderBy
            , null as  ParentId
            , PortalId
            FROM PortalCfg_Globals
            where PortalId=@PortalId

            union all

            SELECT TabId as TabId
                ,TabName as Name
                ,TabOrder as OrderBy
                ,case when ParentId = 1 then PortalId*-1 else ParentId end ParentId
                ,PortalId
            FROM PortalCfg_Tabs
            where ParentId  is not null and PortalId=@PortalId
        ) a
         
        order by PortalId, OrderBy

    "
    InsertCommand="insert into PortalCfg_Tabs(
    TabName
    ,ParentId
    ,TabOrder
    ,CreateDate
    ,PageID
    ,PortalId) 

    values(@Name
    ,@ParentId
    ,0
    ,getdate()
    ,newid()
    ,@PortalId)"
    UpdateCommand="update PortalCfg_Tabs set TabName=@Name,ModifyDate=getdate() where TabId=@TabId">


    <UpdateParameters>
        <asp:Parameter Name="Name" DbType="String" />
        <asp:Parameter Name="TabId" DbType="Int32" />
    </UpdateParameters>

    <InsertParameters>
        <asp:Parameter Name="Name" DbType="String" />
        <asp:Parameter Name="ParentId" DbType="Int32" />
        <asp:SessionParameter Name="PortalId" SessionField="PortalId" />
    </InsertParameters>


    <SelectParameters>
        <asp:SessionParameter Name="PortalId" SessionField="PortalId" />
    </SelectParameters>
</asp:SqlDataSource>






<asp:SqlDataSource ID="SqlDataSource_ModuleDefinitions" runat="server" ConnectionString="<%$ ConnectionStrings:PortalConnectionString %>"
    SelectCommand="select * from PortalCfg_ModuleDefinitions where PortalId=@PortalId  
    and ModuleDefId not in(select ModuleDefId from PortalCfg_Modules where TabId=@TabId)
    order by ModuleDefName ">
    <SelectParameters>
        <asp:SessionParameter Name="PortalId" SessionField="PortalId" />
        <asp:SessionParameter Name="TabId" SessionField="TabId" />
    </SelectParameters>
</asp:SqlDataSource>



<asp:SqlDataSource ID="SqlDataSource_Modules" runat="server" ConnectionString="<%$ ConnectionStrings:PortalConnectionString %>"
    SelectCommand="select * from PortalCfg_Modules where TabId=@TabId order by ModuleOrder">
    <SelectParameters>
        <asp:SessionParameter Name="TabId" SessionField="TabId" />
    </SelectParameters>
</asp:SqlDataSource>


