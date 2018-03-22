<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxBIFileLWTAdminMasterSetup.ascx.vb" Inherits="Modules_ucDevxBIFileLWTAdminMasterSetup" %>

<script type="text/javascript">
    function ShowMasterBIPopup(BID) {
        //alert(GUID);
        window.parent.LoadingPanel.Show();
        cbPreview.PerformCallback(BID);
    }
</script>

<dx:ASPxCallback ID="cbPreview" runat="server" ClientInstanceName="cbPreview">
    <ClientSideEvents CallbackError="function(s, e) { 
            //LoadingPanel.Hide(); 
        }"
        CallbackComplete="function(s, e) { 
          
            window.top.clientView.SetContentUrl('applications/BI/Master/PreviewExcel.aspx');
            window.top.clientView.Show();            
        }" />
</dx:ASPxCallback>


<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="" runat="server" EnableAdaptivity="true" Width="100%" HeaderImage-IconID="businessobjects_bonote_32x32">
    <PanelCollection>
        <dx:PanelContent>


            <dx:ASPxTreeList ID="treeList"
                ClientInstanceName="treeList"
                runat="server" DataCacheMode="Disabled"
                AutoGenerateColumns="false"
                DataSourceID="SqlDataSource_Data"
                Settings-ShowRoot="true" Width="100%"
                SettingsBehavior-AutoExpandAllNodes="true"
                SettingsEditing-EditFormColumnCount="1"
                SettingsEditing-ConfirmDelete="true"
                SettingsEditing-AllowRecursiveDelete="true" EnableTheming="true"
                KeyFieldName="BID" Settings-ShowTreeLines="true"
                SettingsText-ConfirmDelete="Confirm Delete?"
                ParentFieldName="ParentID">



                <SettingsPopupEditForm Modal="true" HorizontalAlign="Center" VerticalAlign="Middle" />

                <SettingsEditing Mode="PopupEditForm"
                    AllowRecursiveDelete="true"
                    ConfirmDelete="true" />
                <SettingsBehavior ColumnResizeMode="NextColumn" AllowFocusedNode="true" AllowEllipsisInText="True" />
                <Styles>
                    <AlternatingNode Enabled="true" />

                </Styles>


                <Toolbars>

                    <dx:TreeListToolbar Enabled="true" Position="Top" ItemAlign="Left">

                        <Items>
                            <dx:TreeListToolbarItem BeginGroup="false">
                                <Template>

                                    <dx:ASPxButton ID="ASPxButton1" 
                                        ClientInstanceName="bnNewTitle"
                                        Border-BorderStyle="Groove" Border-BorderWidth="1" ForeColor="Black" BackColor="White"
                                        AutoPostBack="false"
                                        runat="server"
                                        Image-IconID="dashboards_dashboardtitle_16x16"
                                        ToolTip="New Title"
                                        Text="New Title">
                                        <ClientSideEvents Click="function(s,e){ 
                                  
                                            popupNewTitle.Show();
                                            ASPxClientEdit.ClearEditorsInContainerById('frmNewTitle');
                                      
                                        }" />
                                    </dx:ASPxButton>
                                </Template>


                            </dx:TreeListToolbarItem>


                            <%--<dx:TreeListToolbarItem Command="NewRoot" Text="New Title" Image-IconID="dashboards_dashboardtitle_16x16" />--%>
                            <%--<dx:TreeListToolbarItem Command="New" Text="New Report" BeginGroup="true" Image-IconID="reports_addgroupheader_16x16office2013" />--%>
                            <%--<dx:TreeListToolbarItem Command="Update" BeginGroup="true" Image-IconID="actions_save_16x16devav" Visible="false" />
                            <dx:TreeListToolbarItem Command="Cancel" BeginGroup="true" Image-IconID="actions_cancel_16x16office2013" Visible="false" />--%>
                            <%--<dx:TreeListToolbarItem Command="Delete" BeginGroup="true" Image-IconID="edit_delete_16x16office2013" />--%>
                            <%--<dx:TreeListToolbarItem Command="Refresh" BeginGroup="true" Image-IconID="actions_refresh_16x16office2013" />--%>


                            <dx:TreeListToolbarItem BeginGroup="true">
                                <Template>
                                    <dx:ASPxButton ID="ASPxButton1" runat="server"
                                        AutoPostBack="false"
                                        Border-BorderStyle="Groove" Border-BorderWidth="1" ForeColor="Black" BackColor="White"
                                        
                                        Image-IconID="reports_addgroupheader_16x16office2013"
                                        Text="New Report">

                                        <ClientSideEvents Click="function(s,e){
                                             

                                            var focusedNode = treeList.GetFocusedNodeKey()
                                           
                                            if(focusedNode){
                                                var nodeState = treeList.GetNodeState(focusedNode);
                                                popupNewNode.Show();
                                                ASPxClientEdit.ClearEditorsInContainerById('frmNewNode');
                                            }
                                            else
                                            {
                                                 alert('Please select Title');
                                            }
                                        }" />
                                    </dx:ASPxButton>
                                </Template>
                            </dx:TreeListToolbarItem>



<%--                            <dx:TreeListToolbarItem BeginGroup="true">
                                <Template>
                                    <dx:ASPxButton ID="ASPxButton1" runat="server"
                                        AutoPostBack="false"
                                        Image-IconID="edit_delete_16x16office2013"
                                        Text="Delete">

                                        <ClientSideEvents Click="function(s,e){
                                            if(confirm('Confirm to Delete'))
                                            {

                                                                    var focusedNode = treeList.GetFocusedNodeKey()
                                                                    var nodeState = treeList.GetNodeState(focusedNode);
                                                                    if (nodeState != 'Child') {
                                                                        alert('Cannot delete, because under this subject has some report.');
                                                                        return false ;
                                                                    }
                                                                    else
                                                                    {
                                                                         cbDeleteData.PerformCallback(focusedNode);
                                                                    }

                                                                     
                                            }
                                                                        }" />
                                    </dx:ASPxButton>
                                </Template>
                            </dx:TreeListToolbarItem>--%>



                            <dx:TreeListToolbarItem BeginGroup="true">
                                <Template>
                                    <dx:ASPxButton ID="ASPxButton1" runat="server"
                                        Border-BorderStyle="Groove" Border-BorderWidth="1" ForeColor="Black" BackColor="White"
                                        
                                         AutoPostBack="false" Image-IconID="actions_refresh_16x16office2013" Text="Refresh">

                                        <ClientSideEvents Click="function(s,e){
                                                                            treeList.PerformCallback();
                                                                        }" />
                                    </dx:ASPxButton>
                                </Template>
                            </dx:TreeListToolbarItem>









                        </Items>
                    </dx:TreeListToolbar>
                </Toolbars>


                <Columns>


                    <dx:TreeListHyperLinkColumn FieldName="BID" Caption="TITLE">
                        <PropertiesHyperLink TextField="TITLE" NavigateUrlFormatString="javascript:cbPreview.PerformCallback('{0}');">
                        </PropertiesHyperLink>
                        <EditFormSettings Visible="False" />
                    </dx:TreeListHyperLinkColumn>

 <%--                    <dx:TreeListHyperLinkColumn FieldName="BID" Caption="TITLE">
                        <DataCellTemplate>
                            <%# GetURL(Eval("BID"))%>
                        </DataCellTemplate>
                          <EditFormSettings Visible="False" VisibleIndex="1" />
                    </dx:TreeListHyperLinkColumn>--%>


                    <dx:TreeListSpinEditColumn FieldName="No" Visible="false" Caption="No" 
                        CellStyle-HorizontalAlign="Center">
                        <EditFormSettings Visible="True" VisibleIndex="0" />

                        <PropertiesSpinEdit Width="100" Height="10" MinValue="1" MaxValue="99" Increment="1" LargeIncrement="1">
                            <ValidationSettings RequiredField-IsRequired="true"></ValidationSettings>
                            <SpinButtons ShowIncrementButtons="False" ShowLargeIncrementButtons="True" />
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

                    <dx:TreeListComboBoxColumn FieldName="DS_ID"  Caption="DataSource" Width="150" CellStyle-Wrap="False">
                        <EditFormSettings Visible="False" VisibleIndex="3" />
                        <PropertiesComboBox Width="400" DataSourceID="SqlDataSource_DataSourceFile" TextField="Title" ValueField="DS_ID" ValueType="System.String">
                        </PropertiesComboBox>
                    </dx:TreeListComboBoxColumn>

                      <dx:TreeListTextColumn FieldName="Owner" Caption="Owner" CellStyle-Wrap="False">
                         <EditFormSettings Visible="False" VisibleIndex="3" />
                    </dx:TreeListTextColumn>


                    <%--             <dx:TreeListCheckColumn FieldName="IsActive" Caption="IsActive" CellStyle-Wrap="False">
                        <EditFormSettings Visible="True"  VisibleIndex="4"   />
                    </dx:TreeListCheckColumn>--%>

                    <dx:TreeListCommandColumn >
                        <EditButton Visible="true" Image-IconID="edit_edit_16x16office2013" />
                        <DeleteButton Visible="true" Image-IconID="edit_delete_16x16office2013"></DeleteButton>
                        <UpdateButton Visible="true" Image-IconID="actions_save_16x16devav" Text="Save">
                        </UpdateButton>
                        <CancelButton Visible="true" Image-IconID="actions_cancel_16x16office2013" Text="Cancel">
                        </CancelButton>
                    </dx:TreeListCommandColumn>


                    <%--            <dx:TreeListCommandColumn ButtonType="Button"   UpdateButton-Visible="true" CancelButton-Visible="true">

                    </dx:TreeListCommandColumn>--%>
                </Columns>

            </dx:ASPxTreeList>


        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<asp:SqlDataSource ID="SqlDataSource_Data" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="select [BID]
    ,tblDataSourceBI.[No]
    ,tblDataSourceBI.[TITLE]
    ,tblDataSourceBI.[DESCRIPTION]
    ,tblDataSourceBI.[ParentID]
    ,tblDataSourceBI.[DS_ID]
    ,tblDataSourceBI.[CREATEDATE]
    ,tblDataSourceBI.[CREATEBY]
    ,tblDataSourceBI.[MODIFYDATE]
    ,tblDataSourceBI.[MODIFYBY] 
    ,tblDataSourceFile.[GUID]
    ,tblDataSourceBI.Owner
    from tblDataSourceBI 
    left join tblDataSourceFile on tblDataSourceFile.ID=tblDataSourceBI.DS_ID
    where tblDataSourceBI.PortalId=@PortalId
    Order By tblDataSourceBI.[No] "

    DeleteCommand="
    delete tblDataSourceBI_Assignment where BID=@BID;
    delete tblDataSourceBI_Field_Filter where BID=@BID;
    delete tblDataSourceBI_Field where BID=@BID;
    delete tblDataSourceBI where BID=@BID;
    "
    
 

    UpdateCommand="
    update tblDataSourceBI 
    set [No]=@No
    ,[TITLE]=@TITLE
    ,[DESCRIPTION]=@DESCRIPTION
    ,[ParentID]=@ParentID
    ,[DS_ID]=@DS_ID
    ,[MODIFYDATE]=getdate()
    ,[MODIFYBY]=@MODIFYBY
    Where BID=@BID;">


     <SelectParameters>
        <asp:Parameter Name="PortalId" />
    </SelectParameters>



        <DeleteParameters>
        <asp:Parameter Name="BID" DbType="Int32" />
    </DeleteParameters>


    <UpdateParameters>
        <asp:Parameter Name="BID" DbType="Int32" />
        <asp:Parameter Name="No" DbType="Int32" />
        <asp:Parameter Name="TITLE" DbType="String" />
        <asp:Parameter Name="DESCRIPTION" DbType="String" />
        <asp:Parameter Name="ParentID" DbType="Int32" />
        <asp:Parameter Name="DS_ID" DbType="String" />
        <asp:Parameter Name="ACTIVE" DbType="Boolean" />
        <asp:Parameter Name="MODIFYBY" DbType="String" />
    </UpdateParameters>


</asp:SqlDataSource>

<asp:SqlDataSource ID="SqlDataSource_DataSourceFile"
    runat="server"
    ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="
    
         select 
         null DS_ID
        ,null Title 

        union all 

        select [ID] as [DS_ID] 
        ,[Title]
        from tblDataSourceFile
        where PortalId=@PortalId
    order by [Title] 

    ">
     <SelectParameters>
        <asp:Parameter Name="PortalId" />
    </SelectParameters>

</asp:SqlDataSource>

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




<dx:ASPxPopupControl ID="popupNewTitle" ClientInstanceName="popupNewTitle"
    runat="server"
    Modal="True"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="New Title"
    AllowDragging="true"
    AllowResize="True"
    DragElement="Window"
    EnableAnimation="true"
    CloseAction="CloseButton"
    EnableCallbackAnimation="true"
    EnableViewState="true"
    ShowPageScrollbarWhenModal="true"
    ScrollBars="Auto"
    EnableAdaptivity="true">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">

            <dx:ASPxCallbackPanel ID="FormNewTitle" runat="server" RenderMode="Div" Height="100%" ClientInstanceName="FormNewTitle">
                <PanelCollection>
                    <dx:PanelContent>


                        <dx:ASPxFormLayout ID="frmNewTitle" ClientInstanceName="frmNewTitle" runat="server">

                            <Items>
                                <dx:LayoutGroup ShowCaption="False" AlignItemCaptions="true" ColCount="1">
                                    <Items>

                                        <dx:LayoutItem Caption="No">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>


                                                    <dx:ASPxSpinEdit ID="newNo" runat="server" Number="1" NumberType="Integer" ValidationSettings-RequiredField-IsRequired="true"
                                                        MinValue="1" MaxValue="99" Increment="1" LargeIncrement="1">
                                                        <SpinButtons ShowIncrementButtons="False" ShowLargeIncrementButtons="True" />

                                                    </dx:ASPxSpinEdit>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="TITLE">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox runat="server" ID="newTITLE" ValidationSettings-RequiredField-IsRequired="true"></dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="DESCRIPTION">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>


                                                    <dx:ASPxTextBox runat="server" ID="newDESCRIPTION" ValidationSettings-RequiredField-IsRequired="true"></dx:ASPxTextBox>


                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem Caption="">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>


                                                    <dx:ASPxButton runat="server" ID="btnSaveData" Image-IconID="actions_save_16x16devav"
                                                        Text="Save" AutoPostBack="false">
                                                        <ClientSideEvents Click="function(s,e)
                                                                                  {
                                                 
                                                                                  if(ASPxClientEdit.AreEditorsValid()) 
                                                                                    {                 
                                                                                          LoadingPanel.Show();                              
                                                                                          cbSaveNewData.PerformCallback();                                                    
                                                                                    }
                                                                                    e.processOnServer = false;
                                                                           }
                                                                           " />

                                                        <Image IconID="actions_save_16x16devav"></Image>
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




        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>


<dx:ASPxCallback ID="cbSaveNewData" runat="server" ClientInstanceName="cbSaveNewData">
    <ClientSideEvents EndCallback="function(s, e) { 
            LoadingPanel.Hide(); 
            treeList.PerformCallback();
        }"
        CallbackError="function(s, e) { 
            LoadingPanel.Hide(); 
        }"
        CallbackComplete="function(s, e) {
            LoadingPanel.Hide();
            popupNewTitle.Hide();
            treeList.PerformCallback();
            
                  
        }" />
</dx:ASPxCallback>
<dx:ASPxCallback ID="cbDeleteData" runat="server" ClientInstanceName="cbDeleteData">
    <ClientSideEvents EndCallback="function(s, e) { 
            LoadingPanel.Hide(); 
            treeList.PerformCallback();
        }"
        CallbackError="function(s, e) { 
            LoadingPanel.Hide(); 
        }"
        CallbackComplete="function(s, e) {
            LoadingPanel.Hide();
            treeList.PerformCallback();  
        }" />
</dx:ASPxCallback>


<dx:ASPxPopupControl ID="popupNewNode" ClientInstanceName="popupNewNode"
    runat="server"
    Modal="True"
    PopupHorizontalAlign="WindowCenter"
    PopupVerticalAlign="WindowCenter"
    HeaderText="New Report"
    AllowDragging="true"
    AllowResize="True"
    DragElement="Window"
    EnableAnimation="true"
    CloseAction="CloseButton"
    EnableCallbackAnimation="true"
    EnableViewState="true"
    ShowPageScrollbarWhenModal="true"
    ScrollBars="Auto"
    EnableAdaptivity="true">
    <ContentCollection>
        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">

            <dx:ASPxCallbackPanel ID="ASPxCallbackPanel1" runat="server" RenderMode="Div" Height="100%" ClientInstanceName="FormNewNode">
                <PanelCollection>
                    <dx:PanelContent>


                        <dx:ASPxFormLayout ID="ASPxFormLayout1" ClientInstanceName="frmNewNode" runat="server">

                            <Items>
                                <dx:LayoutGroup ShowCaption="False" AlignItemCaptions="true" ColCount="1">
                                    <Items>

                                        <dx:LayoutItem Caption="No">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>


                                                    <dx:ASPxSpinEdit ID="newNodeNo" runat="server" Number="1" NumberType="Integer" ValidationSettings-RequiredField-IsRequired="true"
                                                        MinValue="1" MaxValue="99" Increment="1" LargeIncrement="1">
                                                        <SpinButtons ShowIncrementButtons="False" ShowLargeIncrementButtons="True" />

                                                    </dx:ASPxSpinEdit>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="TITLE">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox runat="server" ID="newNodeTitle" ValidationSettings-RequiredField-IsRequired="true"></dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="DESCRIPTION">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>


                                                    <dx:ASPxTextBox runat="server" ID="newNodeDescription" ValidationSettings-RequiredField-IsRequired="true"></dx:ASPxTextBox>


                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>

                                        <dx:LayoutItem Caption="DataSource">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>

                                                    <dx:ASPxComboBox ID="newNodeDataSource" runat="server" DataSourceID="SqlDataSource_DataSourceFile" TextField="Title" ValueField="DS_ID">
                                                    </dx:ASPxComboBox>


                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>


                                        <dx:LayoutItem Caption="">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>


                                                    <dx:ASPxButton runat="server" ID="ASPxButton2" Image-IconID="actions_save_16x16devav"
                                                        Text="Save" AutoPostBack="false">
                                                        <ClientSideEvents Click="function(s,e)
                                                                                  {
                                                 
                                                                                  if(ASPxClientEdit.AreEditorsValid()) 
                                                                                    {                 
                                                                                          LoadingPanel.Show();                              
                                                                                          cbSaveNewNode.PerformCallback();                                                    
                                                                                    }
                                                                                    e.processOnServer = false;
                                                                           }
                                                                           " />

                                                        <Image IconID="actions_save_16x16devav"></Image>
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




        </dx:PopupControlContentControl>
    </ContentCollection>
</dx:ASPxPopupControl>




<dx:ASPxCallback ID="cbSaveNewNode" runat="server" ClientInstanceName="cbSaveNewNode">
    <ClientSideEvents EndCallback="function(s, e) { 
            LoadingPanel.Hide(); 
            treeList.PerformCallback();
        }"
        CallbackError="function(s, e) { 
            LoadingPanel.Hide(); 
        }"
        CallbackComplete="function(s, e) {
            LoadingPanel.Hide();
            popupNewNode.Hide();
            treeList.PerformCallback();
            
                  
        }" />
</dx:ASPxCallback>

