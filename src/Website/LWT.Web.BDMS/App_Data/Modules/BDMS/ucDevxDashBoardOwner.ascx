<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxDashBoardOwner.ascx.vb" Inherits="Modules_ucDevxDashBoardOwner" %>

<script type="text/javascript">
    function ShowMasterBIPopup(DB_ID) {
        //LoadingPanel.Show();
        cbPreview.PerformCallback(DB_ID);
    }
</script>
 
<dx:ASPxCallback ID="cbPreview" runat="server" ClientInstanceName="cbPreview">
    <ClientSideEvents CallbackError="function(s, e) { 
            //LoadingPanel.Hide(); 
        }"
        CallbackComplete="function(s, e) { 
            //LoadingPanel.Hide(); 
            if(e.result=='' || e.result==null)
            {
                alert('Dashboard Details');
            }
            else
            {

                window.top.clientView2.SetContentUrl('applications/DB/Owner/Preview.aspx');
                window.top.clientView2.Show();  
        
            }
                  
        }" />
</dx:ASPxCallback>

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="" runat="server" EnableAdaptivity="true" Width="100%" HeaderImage-IconID="businessobjects_bonote_32x32">
    <PanelCollection>
        <dx:PanelContent>


            <dx:ASPxTreeList ID="treeList"
                ClientInstanceName="treeList"
                runat="server"  DataCacheMode="Disabled"
                AutoGenerateColumns="false"
                DataSourceID="SqlDataSource_Data"
                Settings-ShowRoot="true" Width="100%"
                SettingsBehavior-AutoExpandAllNodes="true"
                SettingsEditing-EditFormColumnCount="1"
                SettingsEditing-ConfirmDelete="true"
                SettingsEditing-AllowRecursiveDelete="true"
                KeyFieldName="DB_ID" Settings-ShowTreeLines="true"
                SettingsText-ConfirmDelete="Confirm Delete?"
                ParentFieldName="ParentID">

                <SettingsEditing Mode="PopupEditForm"
                    AllowRecursiveDelete="true"
                    ConfirmDelete="true" />
                <SettingsPopupEditForm Width="300"
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

                        <dx:TreeListToolbarItem BeginGroup="true">
                                <Template>

                                    <dx:ASPxButton ID="ASPxButton1"
                                        ClientInstanceName="bnNewTitle"
                                        Border-BorderWidth="0" 
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

                       <%-- <dx:TreeListToolbarItem BeginGroup="true">
                            <Template>
                                <dx:ASPxButton ID="ASPxButton1" runat="server"
                                    AutoPostBack="false"
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
                        </dx:TreeListToolbarItem>--%>



                            
                            
                            
                             <dx:TreeListToolbarItem Command="Delete" BeginGroup="true" Image-IconID="edit_delete_16x16office2013" />
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


 <%--                   <dx:TreeListHyperLinkColumn FieldName="DB_ID" Caption="TITLE">
                        <PropertiesHyperLink TextField="TITLE" NavigateUrlFormatString="javascript:ShowMasterBIPopup({0});">
                        </PropertiesHyperLink>

                        <EditFormSettings Visible="False" />
                    </dx:TreeListHyperLinkColumn>--%>
                         <dx:TreeListHyperLinkColumn FieldName="DB_ID" Caption="TITLE">
                        <DataCellTemplate>
                            <%# GetURL(Eval("DB_ID"))%>
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



                    <dx:TreeListTextColumn FieldName="TITLE" Visible="false"   Caption="TITLE" CellStyle-Wrap="False">
                        <PropertiesTextEdit  Width="250">
                            <ValidationSettings >
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                        <EditFormSettings Visible="True" VisibleIndex="1" />
                    </dx:TreeListTextColumn>

                    <dx:TreeListTextColumn FieldName="DESCRIPTION" Caption="DESCRIPTION"  CellStyle-Wrap="False">
                        <PropertiesTextEdit Width="250">
                            <ValidationSettings >
                                <RequiredField IsRequired="true" />
                            </ValidationSettings>
                        </PropertiesTextEdit>
                        <EditFormSettings Visible="True" VisibleIndex="2" />
                    </dx:TreeListTextColumn>

 


                    <dx:TreeListCommandColumn >
                    
                        <NewButton Visible="false" Text="Create" Image-IconID="edit_edit_16x16office2013" />
                        <EditButton Visible="true" Image-IconID="edit_edit_16x16office2013" />
                        <UpdateButton Visible="true" Text="Save" Image-IconID="actions_save_16x16devav"></UpdateButton>
                        <CancelButton Visible="true" Image-IconID="actions_cancel_16x16office2013"></CancelButton>
                    </dx:TreeListCommandColumn>


                </Columns>

            </dx:ASPxTreeList>


        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<asp:SqlDataSource ID="SqlDataSource_Data" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="select
       [DB_ID]
      ,[No]
      ,[TITLE]
      ,[DESCRIPTION]
      ,[DEPARTMENT]
      ,[CUBE_ID]
      ,[CREATEDATE]
      ,[CREATEBY]
      ,[MODIFYDATE]
      ,[MODIFYBY]
      ,[GUID]
      ,[ParentID]
    from tblDashBoard 
    where PortalId=@PortalId and CREATEBY=@CREATEBY
    Order By [No] "
    UpdateCommand="

    update tblDashBoard 
    set [No]=@No
    ,[TITLE]=@TITLE
    ,[DESCRIPTION]=@DESCRIPTION
    ,[ParentID]=@ParentID
    ,[MODIFYDATE]=getdate()
    ,[MODIFYBY]=@MODIFYBY
    Where DB_ID=@DB_ID;

    update tblDashBoard_Data 
    set [Caption]=@TITLE
    Where DB_ID=@DB_ID;
    
    "

    DeleteCommand="
    delete tblDashBoard_Data where DB_GUID collate Thai_CI_As in(select GUID from tblDashBoard where DB_ID =@DB_ID);
    delete tblDashBoard_Assignment where DB_ID=@DB_ID;
    delete tblDashBoard where DB_ID=@DB_ID;
    ">

    <SelectParameters>
        <asp:Parameter Name="PortalId" />
        <asp:Parameter Name="CREATEBY" />
    </SelectParameters>

    <DeleteParameters>
        <asp:Parameter Name="DB_ID" DbType="Int32" />
    </DeleteParameters>

    <UpdateParameters>
        <asp:Parameter Name="DB_ID" DbType="Int32" />
        <asp:Parameter Name="No" DbType="Int32" />
        <asp:Parameter Name="TITLE" DbType="String" />
        <asp:Parameter Name="DESCRIPTION" DbType="String" />
        <asp:Parameter Name="ParentID" DbType="Int32" />
        <asp:Parameter Name="ACTIVE" DbType="Boolean" />
        <asp:Parameter Name="MODIFYBY" DbType="String" />
    </UpdateParameters>


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

            <dx:ASPxCallbackPanel ID="FormNewTitle" runat="server" RenderMode="Table" ClientInstanceName="FormNewTitle">
                <PanelCollection>
                    <dx:PanelContent>


                        <dx:ASPxFormLayout ID="frmNewTitle" ClientInstanceName="frmNewTitle" runat="server">

                            <Items>
                                <dx:LayoutGroup ShowCaption="False" AlignItemCaptions="true" ColCount="1">
                                    <Items>

                                        <dx:LayoutItem Caption="No">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>


                                                    <dx:ASPxSpinEdit ID="newNo" Width="200" runat="server" Number="1" NumberType="Integer"  ValidationSettings-Display="Dynamic" ValidationSettings-RequiredField-IsRequired="true"
                                                        MinValue="1" MaxValue="99" Increment="1" LargeIncrement="1">
                                                        <SpinButtons ShowIncrementButtons="False" ShowLargeIncrementButtons="True" />

                                                    </dx:ASPxSpinEdit>

                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="TITLE">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>
                                                    <dx:ASPxTextBox runat="server"  Width="200" ID="newTITLE" ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-Display="Dynamic"></dx:ASPxTextBox>
                                                </dx:LayoutItemNestedControlContainer>
                                            </LayoutItemNestedControlCollection>
                                        </dx:LayoutItem>
                                        <dx:LayoutItem Caption="DESCRIPTION">
                                            <LayoutItemNestedControlCollection>
                                                <dx:LayoutItemNestedControlContainer>


                                                    <dx:ASPxTextBox runat="server"  Width="200" ID="newDESCRIPTION" ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-Display="Dynamic"></dx:ASPxTextBox>


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



