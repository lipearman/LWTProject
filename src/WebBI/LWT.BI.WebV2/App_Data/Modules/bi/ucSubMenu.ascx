<%@ Control Language="VB" AutoEventWireup="false" CodeFile="ucSubMenu.ascx.vb" Inherits="Modules_ucSubMenu" %>
<%=submenu %>

<dx:ASPxSiteMapControl EnableTheming="False" ID="sm"
    runat="server" Categorized="True" 
    RepeatDirection="Horizontal"
    CssFilePath="~/SiteMap/Resources/MultiColumnCategorized/styles.css" 
    CssPostfix="MultiColumn"
    Width="100%">
    <Columns>
        <dx:SiteMapColumn />
        <dx:SiteMapColumn />
        <dx:SiteMapColumn />
        <dx:SiteMapColumn />
    </Columns>
    <LevelProperties>
        <dx:LevelProperties>
            <ChildNodesPaddings PaddingBottom="13px" PaddingTop="0px" />
        </dx:LevelProperties>

        <dx:LevelProperties BulletStyle="None">
            <ChildNodesPaddings PaddingLeft="0px" PaddingTop="9px" />
        </dx:LevelProperties>

        <dx:LevelProperties ImageSpacing="4px" NodeSpacing="4px" VerticalAlign="Middle">
            <Image Url="~/SiteMap/Resources/MultiColumnCategorized/csmBullet.gif" Width="3px" />
        </dx:LevelProperties>

    </LevelProperties>
    <Paddings Padding="21px" PaddingBottom="8px" />

    <ColumnSeparatorStyle Width="10px">
        <Paddings Padding="0px" />
    </ColumnSeparatorStyle>

</dx:ASPxSiteMapControl>



<%--<dx:ASPxSiteMapControl ID="sm" runat="server"></dx:ASPxSiteMapControl>--%>
<asp:SqlDataSource ID="sds" runat="server" SelectCommand="

        select * from [v_UserTabs]
        where 
    (
        TabId =
	        (
		        SELECT top 1 ParentId
		        FROM [v_UserTabs] 
		        where UserName=@UserName
		        and PortalId=@PortalId
		        and PageId=@PageId
	        )

        and UserName=@UserName and PortalId=@PortalId
    )
        or
    (
        SortPath like 
	        (
                SELECT top 1 SortPath
		        FROM [v_UserTabs] 
		        where UserName=@UserName
		        and PortalId=@PortalId
		        and PageId=@PageId
	        )	+ '%'

        and UserName=@UserName and PortalId=@PortalId
    )
        order by SortPath



    "
	ConnectionString="<%$ ConnectionStrings:PortalConnectionString %>">
    <SelectParameters>
        <asp:Parameter Name="UserName" />
        <asp:Parameter Name="PortalId" />
        <asp:Parameter Name="PageId" />
    </SelectParameters>
</asp:SqlDataSource>