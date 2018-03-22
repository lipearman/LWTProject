<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucDevxBIUser.ascx.vb" Inherits="Modules_ucDevxBIUser" %>
 
<dx:ASPxCallback ID="cbPreview" runat="server" ClientInstanceName="cbPreview">
    <ClientSideEvents CallbackError="function(s, e) { 
            //LoadingPanel.Hide(); 
        }"
        CallbackComplete="function(s, e) { 
            //LoadingPanel.Hide(); 
            window.top.clientView.SetContentUrl('applications/BI/User/Preview.aspx');
            window.top.clientView.Show();            
        }" />
</dx:ASPxCallback>
 
<dx:ASPxCallback ID="cbPreviewExcel" runat="server" ClientInstanceName="cbPreviewExcel">
    <ClientSideEvents CallbackError="function(s, e) { 
            //LoadingPanel.Hide(); 
        }"
        CallbackComplete="function(s, e) { 
            //LoadingPanel.Hide(); 
            window.top.clientView.SetContentUrl('applications/BI/User/PreviewExcel.aspx');
            window.top.clientView.Show();            
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
                KeyFieldName="GUID" Settings-ShowTreeLines="true"
                SettingsText-ConfirmDelete="Confirm Delete?"
                ParentFieldName="ParentGUID">

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


                    <dx:TreeListHyperLinkColumn FieldName="GUID" Caption="TITLE">
                        <DataCellTemplate>
                            <%# GetURL(Eval("GUID")) %>
                        </DataCellTemplate>
                    </dx:TreeListHyperLinkColumn>

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

     

 select RANK() OVER  
 (
	PARTITION BY case when tblBI.[ParentID] = 0 then tblBI.[BID] else tblBI.[ParentID] end ORDER BY  tblBI.[BID],tblBI.[No] 
 ) AS MyRank 
    
,convert(varchar(255),tblBI.GUID) COLLATE THAI_CI_AS as [GUID]
,convert(varchar(255),ParentBI.GUID)  COLLATE THAI_CI_AS as ParentGUID
,tblBI.[BID]
,tblBI.[No]
,tblBI.[TITLE]
,tblBI.[DESCRIPTION]
,tblBI.[ParentID]
,tblCube.BASE_CUBE_NAME as [SourceName]
from tblBI 
inner join tblBI ParentBI on tblBI.[ParentID] = ParentBI.BID
inner join tblCube on tblBI.CUBE_ID = tblCube.CUBE_ID
inner JOIN dbo.tblBIAssignment ON tblBI.BID = tblBIAssignment.BID
and tblBIAssignment.UserName=@UserName 
where    tblBI.PortalId=@PortalId 


 union all     
         
select 
RANK() OVER  
(
PARTITION BY case when tblDataSourceBI.[ParentID] = 0 then tblDataSourceBI.[BID] else tblDataSourceBI.[ParentID] end ORDER BY  tblDataSourceBI.[BID],tblDataSourceBI.[No] 
) AS MyRank 
,convert(varchar(255),tblDataSourceBI.GUID) COLLATE THAI_CI_AS as [GUID]
,convert(varchar(255),ParentDataSourceBI.GUID ) COLLATE THAI_CI_AS as ParentGUID

,tblDataSourceBI.[BID]
,tblDataSourceBI.[No]
,tblDataSourceBI.[TITLE]
,tblDataSourceBI.[DESCRIPTION]
,tblDataSourceBI.[ParentID]
 
,tblDataSourceFile.Title as [SourceName]

from tblDataSourceBI 
inner join tblDataSourceBI as  ParentDataSourceBI on tblDataSourceBI.[ParentID] =  ParentDataSourceBI.[BID]
inner join tblDataSourceFile on tblDataSourceBI.DS_ID = dbo.tblDataSourceFile.ID
inner JOIN tblDataSourceBI_Assignment 
ON tblDataSourceBI_Assignment.BID = tblDataSourceBI.BID
and tblDataSourceBI_Assignment.UserName=@UserName 

where tblDataSourceBI_Assignment.USERNAME is not null
and  tblDataSourceBI.PortalId=@PortalId 





 
    ">
    <SelectParameters>
        <asp:Parameter Name="UserName" />
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
