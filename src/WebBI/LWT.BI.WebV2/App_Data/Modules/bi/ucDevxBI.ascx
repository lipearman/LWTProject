<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxBI.ascx.vb" Inherits="Modules_ucDevxBI" %>
<script type="text/javascript">
    function ShowMasterBIPopup(BID) {
        //LoadingPanel.Show();
        cbPreview.PerformCallback(BID);
    }
</script>

<dx:ASPxCallback ID="cbPreview" runat="server" ClientInstanceName="cbPreview">
    <ClientSideEvents CallbackError="function(s, e) { 
            //LoadingPanel.Hide(); 
        }"
        CallbackComplete="function(s, e) { 
            //LoadingPanel.Hide(); 
            window.top.clientView.SetContentUrl('applications/BI/Master/Preview.aspx');
            window.top.clientView.Show();            
        }" />
</dx:ASPxCallback>

<dx:ASPxCallback ID="cbCloneReport" runat="server" ClientInstanceName="cbCloneReport">
    <ClientSideEvents CallbackError="function(s, e) { 
            LoadingPanel.Hide(); 
        }"
        CallbackComplete="function(s, e) { 
            LoadingPanel.Hide(); 
            if(e.result=='success')
            {
                 treeList.PerformCallback();
            }
            else
            {
                alert(e.result);
            }
          
        }" />



</dx:ASPxCallback>

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="" runat="server" EnableAdaptivity="true" Width="100%" HeaderImage-IconID="businessobjects_bonote_32x32">
    <PanelCollection>
        <dx:PanelContent>


            <dx:ASPxTreeList ID="treeList"
                ClientInstanceName="treeList"
                runat="server"
                AutoGenerateColumns="false"
                DataSourceID="SqlDataSource_Data"
                Settings-ShowRoot="true" Width="100%"
                SettingsBehavior-AutoExpandAllNodes="true"
                SettingsEditing-EditFormColumnCount="1"
                SettingsEditing-ConfirmDelete="true"
                SettingsEditing-AllowRecursiveDelete="true"
                KeyFieldName="BID" Settings-ShowTreeLines="true"
                SettingsText-ConfirmDelete="Confirm Delete?"
                ParentFieldName="ParentID">

                <SettingsEditing Mode="PopupEditForm"
                    AllowRecursiveDelete="true"
                    ConfirmDelete="true" />
                <SettingsPopupEditForm Width="100"
                    Modal="true"
                    HorizontalAlign="WindowCenter"
                    VerticalAlign="WindowCenter" />


                <SettingsBehavior ColumnResizeMode="NextColumn" AllowFocusedNode="true" AllowEllipsisInText="True" />
                <Styles>
                    <AlternatingNode Enabled="true" />
                </Styles>

                <Toolbars>
                    <dx:TreeListToolbar Enabled="true" Position="Top" ItemAlign="Left">
                        <Items>

                            <dx:TreeListToolbarItem Command="NewRoot" BeginGroup="true" Text="New Title" Image-IconID="mail_newmail_16x16office2013" />
                            <dx:TreeListToolbarItem Command="New" BeginGroup="true" Text="New Report" Image-IconID="mail_send_16x16office2013" />
                            <dx:TreeListToolbarItem Command="Update" BeginGroup="true" Image-IconID="actions_save_16x16devav" Visible="false" />
                            <dx:TreeListToolbarItem Command="Cancel" BeginGroup="true" Image-IconID="actions_cancel_16x16office2013" Visible="false" />







                            <%--<dx:TreeListToolbarItem Command="Delete" BeginGroup="true" Image-IconID="edit_delete_16x16office2013" />--%>
                            <dx:TreeListToolbarItem Command="Refresh" BeginGroup="true" Image-IconID="actions_refresh_16x16office2013" />
                       
                            
                         <%--   
                            <dx:TreeListToolbarItem >
                                <Template>
                                    <dx:ASPxButton ID="ASPxButton1" BeginGroup="true" runat="server" AutoPostBack="false" Image-IconID="edit_copy_16x16" Text="Copy Report">
                                        <ClientSideEvents Click="function(s,e){
                                             var key = treeList.GetFocusedNodeKey();

                                             alert(key);
                                            }" />

                                    </dx:ASPxButton>
                                </Template>
                            </dx:TreeListToolbarItem>--%>
                            
                             </Items>
                    </dx:TreeListToolbar>
                </Toolbars>



                <Columns>


<%--                    <dx:TreeListHyperLinkColumn FieldName="BID" Caption="TITLE">
                        <PropertiesHyperLink TextField="TITLE" NavigateUrlFormatString="javascript:ShowMasterBIPopup({0});">
                        </PropertiesHyperLink>

                        <EditFormSettings Visible="False" />
                    </dx:TreeListHyperLinkColumn>--%>
                     <dx:TreeListHyperLinkColumn FieldName="BID" Caption="TITLE">
                        <DataCellTemplate>
                            <%# GetURL(Eval("BID"))%>
                        </DataCellTemplate>
                          <EditFormSettings Visible="False" VisibleIndex="1" />
                    </dx:TreeListHyperLinkColumn>

                    <dx:TreeListSpinEditColumn FieldName="No" Visible="false" Caption="No" Width="100" CellStyle-HorizontalAlign="Center">
                        <EditFormSettings Visible="True" VisibleIndex="0" />
                        <PropertiesSpinEdit Width="120" NumberType="Integer" MinValue="1" MaxValue="999" Increment="1" LargeIncrement="1">
                            <SpinButtons ShowIncrementButtons="false" ShowLargeIncrementButtons="true" />
                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                        </PropertiesSpinEdit>
                    </dx:TreeListSpinEditColumn>



                    <dx:TreeListTextColumn FieldName="TITLE" Visible="false" Caption="TITLE" CellStyle-Wrap="False">
                        <PropertiesTextEdit Width="400">
                            <ValidationSettings Display="Dynamic">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                        <EditFormSettings Visible="True" VisibleIndex="1" />
                    </dx:TreeListTextColumn>

                    <dx:TreeListTextColumn FieldName="DESCRIPTION" Caption="DESCRIPTION" CellStyle-Wrap="False">
                        <PropertiesTextEdit Width="400">
                            <ValidationSettings Display="Dynamic">
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                        <EditFormSettings Visible="True" VisibleIndex="2" />
                    </dx:TreeListTextColumn>

                    <dx:TreeListComboBoxColumn FieldName="CUBE_ID" Visible="false" Caption="CUBE" Width="150" CellStyle-Wrap="False">
                        <EditFormSettings Visible="True" VisibleIndex="3" />
                        <PropertiesComboBox DataSourceID="SqlDataSource_Cubes" TextField="BASE_CUBE_NAME" ValueField="CUBE_ID" ValueType="System.String">
                        </PropertiesComboBox>
                    </dx:TreeListComboBoxColumn>




                    <%--             
                        <dx:TreeListCheckColumn FieldName="IsActive" Caption="IsActive" CellStyle-Wrap="False">
                        <EditFormSettings Visible="True"  VisibleIndex="4"   />
                    </dx:TreeListCheckColumn>--%>

                    <%--        <dx:TreeListCommandColumn ShowNewButtonInHeader="true" Width="100">
                        <NewButton Visible="true" Text=" " Image-ToolTip="New" Image-IconID="edit_new_16x16gray" />
                        <EditButton Visible="true" Text=" " Image-ToolTip="Edit" Image-IconID="actions_edit_16x16devav" />
                        <DeleteButton Visible="true" Text=" " Image-ToolTip="Delete" Image-IconID="edit_delete_16x16office2013" />
                        <UpdateButton Text=" " Image-ToolTip="Save" Image-IconID="actions_save_16x16devav"></UpdateButton>
                        <CancelButton Text=" " Image-ToolTip="Cancel" Image-IconID="actions_cancel_16x16office2013"></CancelButton>

                    </dx:TreeListCommandColumn>--%>

                    <dx:TreeListCommandColumn >
                        <EditButton Visible="true" Image-IconID="edit_edit_16x16office2013"  />
                        <DeleteButton Visible="true" Image-IconID="edit_delete_16x16office2013"></DeleteButton>
                        <UpdateButton Visible="true" Text="Save" Image-IconID="actions_save_16x16devav"></UpdateButton>
                        <CancelButton Visible="true" Image-IconID="actions_cancel_16x16office2013"></CancelButton>
                        <CustomButtons>
                        <dx:TreeListCommandColumnCustomButton ID="Clone" Text="Clone" >
                            <Image ToolTip="Clone Record"  IconID="edit_copy_16x16office2013" />
                          
                        </dx:TreeListCommandColumnCustomButton>
                        </CustomButtons>
                    </dx:TreeListCommandColumn>


                </Columns>
                <ClientSideEvents CustomButtonClick="function(s,e){
                        if (e.buttonID == 'Clone' && confirm('Confirm to Clone this Report?')) {
                            //alert(e.nodeKey);
                            LoadingPanel.Show();
                            cbCloneReport.PerformCallback(e.nodeKey);
                        }
                    }" />
            </dx:ASPxTreeList>


        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<asp:SqlDataSource ID="SqlDataSource_Data" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="select [BID]
    ,[No]
    ,[TITLE]
    ,[DESCRIPTION]
    ,[ParentID]
    ,[CUBE_ID]
    ,[CREATEDATE]
    ,[CREATEBY]
    ,[MODIFYDATE]
    ,[MODIFYBY] 
    from tblBI Order By [No] "
    UpdateCommand="

    update tblBI 
    set [No]=@No
    ,[TITLE]=@TITLE
    ,[DESCRIPTION]=@DESCRIPTION
    ,[ParentID]=@ParentID
    ,[CUBE_ID]=@CUBE_ID
    ,[MODIFYDATE]=getdate()
    ,[MODIFYBY]=@MODIFYBY
    Where BID=@BID;"


    DeleteCommand="
    delete tblBIAssignment where BID=@BID;
    delete tblBIFilter where BID=@BID;
    delete tblBIAttribute where BID=@BID;
    delete tblBI where BID=@BID;
    ">

     <%--DeleteCommand="delete tblBI where BID=@BID;--%>

    <DeleteParameters>
        <asp:Parameter Name="BID" DbType="Int32" />
    </DeleteParameters>

    <UpdateParameters>
        <asp:Parameter Name="BID" DbType="Int32" />
        <asp:Parameter Name="No" DbType="Int32" />
        <asp:Parameter Name="TITLE" DbType="String" />
        <asp:Parameter Name="DESCRIPTION" DbType="String" />
        <asp:Parameter Name="ParentID" DbType="Int32" />
        <asp:Parameter Name="CUBE_ID" DbType="String" />
        <asp:Parameter Name="ACTIVE" DbType="Boolean" />
        <asp:Parameter Name="MODIFYBY" DbType="String" />
    </UpdateParameters>


</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_Cubes"
    runat="server"
    ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="select 
     null CUBE_ID
    ,null BASE_CUBE_NAME 

    union all 
    select [CUBE_ID] 
    ,[CUBE] as [BASE_CUBE_NAME] 
    from tblCube order by BASE_CUBE_NAME 

    "></asp:SqlDataSource>

<dx:ASPxPopupControl ID="clientViewBI" runat="server" ClientInstanceName="clientViewBI"
    Modal="True" Maximized="true"
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

    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl5" runat="server">
        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>
