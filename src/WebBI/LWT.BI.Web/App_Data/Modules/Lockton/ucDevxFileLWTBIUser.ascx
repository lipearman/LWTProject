<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxFileLWTBIUser.ascx.vb" Inherits="Modules_ucDevxFileLWTBIUser" %>

<script type="text/javascript">
    function ShowMasterBIPopup(BID) {
        window.parent.LoadingPanel.Show();
        cbPreview.PerformCallback(BID);
    }
</script>

<dx:ASPxCallback ID="cbPreview" runat="server" ClientInstanceName="cbPreview">
    <ClientSideEvents CallbackError="function(s, e) { 
            //LoadingPanel.Hide(); 
        }"
        CallbackComplete="function(s, e) { 
            //LoadingPanel.Hide(); 
            window.top.clientView.SetContentUrl('applications/BI/User/PreviewExcel.aspx');
            window.top.clientView.Show();            
        }" />
</dx:ASPxCallback>

 

<dx:ASPxRoundPanel ID="pnMain" EnableAnimation="true" ShowCollapseButton="true" HeaderText="" runat="server" EnableAdaptivity="true" Width="100%" HeaderImage-IconID="support_feature_16x16office2013">
    <PanelCollection>
        <dx:PanelContent>


            <dx:ASPxTreeList ID="treeList" 
                ClientInstanceName="treeList"
                runat="server"
                AutoGenerateColumns="false"
                DataSourceID="SqlDataSource_Data"
                Settings-ShowRoot="true" Width="900"
                SettingsBehavior-AutoExpandAllNodes="true"
                SettingsEditing-EditFormColumnCount="1"
                SettingsEditing-ConfirmDelete="true"   
                SettingsEditing-AllowRecursiveDelete="true"
                KeyFieldName="BID" Settings-ShowTreeLines="true" 
                ParentFieldName="ParentID">

                <SettingsEditing Mode="PopupEditForm"
                    AllowRecursiveDelete="true"
                    ConfirmDelete="true" />
                <SettingsPopupEditForm Width="100"
                    Modal="true"
                    HorizontalAlign="WindowCenter"
                    VerticalAlign="WindowCenter" />


                <SettingsBehavior ColumnResizeMode="NextColumn" AllowSort="false" AllowEllipsisInText="True" />
                <Styles>
                    <AlternatingNode Enabled="true" />
                </Styles>
                <Columns>
                     <dx:TreeListHyperLinkColumn FieldName="BID" Visible="false" SortOrder="Ascending">
                      
                    </dx:TreeListHyperLinkColumn>


                    <dx:TreeListHyperLinkColumn FieldName="BID" Caption="TITLE">
                        <PropertiesHyperLink TextField="TITLE" NavigateUrlFormatString="javascript:ShowMasterBIPopup('{0}');">
                        </PropertiesHyperLink>

                        <EditFormSettings Visible="False" />
                    </dx:TreeListHyperLinkColumn>


                    <dx:TreeListSpinEditColumn FieldName="No" Visible="false" Caption="No" Width="80" CellStyle-HorizontalAlign="Center">
                        <EditFormSettings Visible="True" VisibleIndex="0" />
                        <PropertiesSpinEdit Width="100">
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
    select tblDataSourceBI.[BID]
,tblDataSourceBI.[No]
,tblDataSourceBI.[TITLE]
,tblDataSourceBI.[DESCRIPTION]
,tblDataSourceBI.[ParentID]
,tblDataSourceBI.[CREATEDATE]
,tblDataSourceBI.[CREATEBY]
,tblDataSourceBI.[MODIFYDATE]
,tblDataSourceBI.[MODIFYBY] 
,tblDataSourceFile.Title as [SourceName]
,tblDataSourceFile.GUID
from tblDataSourceBI 
left join tblDataSourceFile on tblDataSourceBI.DS_ID = dbo.tblDataSourceFile.ID
left JOIN tblDataSourceBI_Assignment 
ON tblDataSourceBI_Assignment.BID = tblDataSourceBI.BID
and tblDataSourceBI_Assignment.UserName=@UserName
where tblDataSourceBI_Assignment.USERNAME is not null
    
        
Order By tblDataSourceBI.BID , tblDataSourceBI.[No]    


    ">
    <SelectParameters>
        <asp:Parameter Name="UserName" />
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
