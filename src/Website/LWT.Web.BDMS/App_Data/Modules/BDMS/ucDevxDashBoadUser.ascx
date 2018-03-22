<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxDashBoadUser.ascx.vb" Inherits="Modules_ucDevxDashBoadUser" %>

<script type="text/javascript">
    function ShowMasterBIPopup(GUID) {
        //LoadingPanel.Show();
        cbPreview.PerformCallback(GUID);
    }
</script>

<dx:ASPxCallback ID="cbPreview" runat="server" ClientInstanceName="cbPreview">
    <ClientSideEvents CallbackError="function(s, e) { 
            //LoadingPanel.Hide(); 
        }"
        CallbackComplete="function(s, e) { 
            //LoadingPanel.Hide(); 
        

            window.top.clientView2.SetContentUrl('applications/DB/User/Preview.aspx');
            window.top.clientView2.Show();            
        }" />
</dx:ASPxCallback>

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="" runat="server" EnableAdaptivity="true" Width="100%" HeaderImage-IconID="businessobjects_bonote_32x32" >
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
                KeyFieldName="DB_ID" Settings-ShowTreeLines="true"
                SettingsText-ConfirmDelete="Confirm Delete?"
                ParentFieldName="ParentID">

                <SettingsEditing Mode="PopupEditForm"
                    AllowRecursiveDelete="true"
                    ConfirmDelete="true" />
                <SettingsPopupEditForm Width="100"
                    Modal="true"
                    HorizontalAlign="WindowCenter"
                    VerticalAlign="WindowCenter" />


                <SettingsBehavior ColumnResizeMode="NextColumn" AllowEllipsisInText="True" />
                <Styles>
                    <AlternatingNode Enabled="true" />
                </Styles>
                <Columns>


<%--                    <dx:TreeListHyperLinkColumn FieldName="GUID" Caption="TITLE">
                        <PropertiesHyperLink TextField="TITLE" NavigateUrlFormatString="javascript:ShowMasterBIPopup('{0}');">
                        </PropertiesHyperLink>

                        <EditFormSettings Visible="False" />
                    </dx:TreeListHyperLinkColumn>--%>
                       <dx:TreeListHyperLinkColumn FieldName="DB_ID" Caption="TITLE" >
                        <DataCellTemplate>
                            <%# GetURL(Eval("DB_ID"))%>
                        </DataCellTemplate>
                          <EditFormSettings Visible="False" VisibleIndex="1" />
                    </dx:TreeListHyperLinkColumn>

                    <dx:TreeListSpinEditColumn FieldName="No" Visible="false" Caption="No" Width="80" CellStyle-HorizontalAlign="Center">
                        <EditFormSettings Visible="True" VisibleIndex="0" />
                        <PropertiesSpinEdit Width="100" Increment="1" LargeIncrement="1">
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



                </Columns>

            </dx:ASPxTreeList>


        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxRoundPanel>

<asp:SqlDataSource ID="SqlDataSource_Data" runat="server" ConnectionString="<%$ ConnectionStrings:PortalBIConnection %>"
    SelectCommand="
        SELECT distinct
			 [DB_ID] 
            ,[No]
            ,[TITLE]
            ,[DESCRIPTION]
            ,[PortalId]
            ,[UserName]
            ,0 as ParentId
         FROM V_Dashboard_Assignment
         where UserName=@UserName
         and PortalId=@PortalId
         order by [No]
    ">
    <SelectParameters>
         <asp:Parameter Name="PortalId" />
        <asp:Parameter Name="UserName" />
    </SelectParameters>
</asp:SqlDataSource>

 