Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports LWT.Website
Imports DevExpress.Web

Partial Class Modules_ucSubMenu
    Inherits PortalModuleControl
    Public submenu As String

    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If

        sds.SelectParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name
        sds.SelectParameters("PortalId").DefaultValue = webconfig._PortalID
        sds.SelectParameters("PageId").DefaultValue = portalSettings.ActiveTab.PageID

        Dim dataSource As SiteMapDataSource = GenerateSiteMapHierarchy(sds)
        dataSource.ShowStartingNode = False

        sm.DataSource = dataSource
        sm.DataBind()


    End Sub



    Protected Function GenerateSiteMapHierarchy(ByVal dataSource As SqlDataSource) As SiteMapDataSource

        'Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)

     
        Dim _ParentID As String = "1"
      

        Dim ret As New SiteMapDataSource()

        Dim arg As New DataSourceSelectArguments("ParentID")
        Dim dataView As DataView = TryCast(dataSource.Select(arg), DataView)
        Dim table As DataTable = dataView.Table




        If table.Select("ParentID = " & _ParentID).Length = 0 Then
            Using dc As New DataClasses_PortalDataContextExt()
                Dim portalSettings As PortalSettings = CType(Context.Items(ConfigurationSettings.AppSettings("PortalContextName")), PortalSettings)
                Dim _tabs = (From c In dc.v_UserTabs Where c.PortalId = portalSettings.PortalId And c.PageId = portalSettings.PageID And c.UserName.ToLower() = HttpContext.Current.User.Identity.Name.ToLower()).FirstOrDefault()

                If _tabs.ParentId <> 1 Then


                    Dim _tabs2 = (From c In dc.v_UserTabs Where c.PortalId = portalSettings.PortalId And c.Sortpath = Left(_tabs.Sortpath, 9) And c.ParentId = 1 And c.UserName.ToLower() = HttpContext.Current.User.Identity.Name.ToLower()).FirstOrDefault()

                    _ParentID = _tabs2.TabId



                End If
                '_ParentID = _tabs.ParentId




            End Using

        End If

        Dim rowRootNode As DataRow = table.Select("ParentID = " & _ParentID)(0)

        Dim provider As New UnboundSiteMapProvider(rowRootNode("PageId").ToString(), rowRootNode("TabName").ToString())
        AddNodeToProviderRecursive(provider.RootNode, rowRootNode("ParentID").ToString(), table, provider)
        ret.Provider = provider
        Return ret
    End Function

    Private Sub AddNodeToProviderRecursive(ByVal parentNode As SiteMapNode, ByVal parentID As String, ByVal table As DataTable, ByVal provider As UnboundSiteMapProvider)

        Dim childRows() As DataRow = table.Select("ParentID = " & parentID)
        For Each row As DataRow In childRows
            Try
                Dim childNode As SiteMapNode = CreateSiteMapNode(row, provider)
                provider.AddSiteMapNode(childNode, parentNode)
                AddNodeToProviderRecursive(childNode, row("TabId").ToString(), table, provider)
            Catch ex As Exception

            End Try

        Next row
    End Sub

    Private Function CreateSiteMapNode(ByVal dataRow As DataRow, ByVal provider As UnboundSiteMapProvider) As SiteMapNode
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)



        'If dataRow("ParentID").ToString() = "1" Then
        '    Return provider.CreateNode("", dataRow("TabName").ToString(), "")
        'Else
        'Return provider.CreateNode("~/DesktopDefault.aspx?PageId=" & dataRow("PageId").ToString(), dataRow("TabName").ToString(), "")
        'End If
        Return provider.CreateNode("~/" & dataRow("TabName").ToString() & ".html", dataRow("TabName").ToString(), dataRow("TabName").ToString())

    End Function


End Class
