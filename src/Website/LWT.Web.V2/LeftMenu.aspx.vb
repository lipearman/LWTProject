Imports Portal.Components
Imports System.Security
Imports System.Data
Imports DevExpress.Web
Imports LWT.Website
Imports FineUI
Partial Class LeftMenu
    Inherits BasePage
    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
        'If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles) = True Then
        '    Response.Redirect("~/Admin/AccessDenied.aspx")
        'End If

        'If Not Request.Params("pageid") Is Nothing Then PageID = Request.Params("pageid")
        'GenTopMenu()
        'GenDynamicMenuV2()
        'generateDynamicControl()
    End Sub


    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'UserName = IIf(String.IsNullOrEmpty(HttpContext.Current.User.Identity.Name), "Guest", HttpContext.Current.User.Identity.Name)
        LoadData()
    End Sub



    Private Sub LoadData()
        Dim table = CreateDataTable()
        Dim ds = New DataSet()
        ds.Tables.Add(table)
        ds.Relations.Add("TreeRelation", ds.Tables(0).Columns("Id"), ds.Tables(0).Columns("ParentId"), False)
        For Each row As DataRow In ds.Tables(0).Rows
            Dim node = New FineUI.TreeNode()
            node.Text = row("Text").ToString()
            'node.Expanded = True
            'node.Target = "mainframe"
            leftTree.Nodes.Add(node)
            ResolveSubTree(row, node)
        Next
    End Sub


    Private Sub ResolveSubTree(ByVal dr As DataRow, ByVal treeNode As FineUI.TreeNode)
        For Each row As DataRow In dr.GetChildRows("TreeRelation")

            Dim node = New FineUI.TreeNode()
            node.Text = row("Text").ToString()
            node.EnableCheckBox = False
            node.NavigateUrl = row("URL").ToString()
            node.Target = "mainframe"
            node.NodeID = row("Id").ToString()
            treeNode.Nodes.Add(node)
            ResolveSubTree(row, node)


        Next
    End Sub



    Private Function CreateDataTable() As DataTable
        Dim table = New DataTable()
        table.Columns.Add(New DataColumn("Id", GetType(String)))
        table.Columns.Add(New DataColumn("Text", GetType(String)))
        table.Columns.Add(New DataColumn("URL", GetType(String)))
        table.Columns.Add(New DataColumn("IsLeaf", GetType(Integer)))
        table.Columns.Add(New DataColumn("ParentId", GetType(String)))
        table.Columns.Add(New DataColumn("LinksID", GetType(Integer)))
        table.Columns.Add(New DataColumn("Level", GetType(Integer)))

        Dim _TreeMenuRoles As New List(Of MyContext.TreeMenuRoles)


        '==========================================
        Dim portalSettings As PortalSettings = CType(Context.Items(ConfigurationSettings.AppSettings("PortalContextName")), PortalSettings)

        Dim config As Configuration = New Configuration
        Dim portalTabs = New ArrayList

        Dim _PageID = Session("PageID")
        Dim _tab = (From c In portalSettings.DesktopTabs Where c.PageID = _PageID).FirstOrDefault()
        Dim tabs = (From c In portalSettings.DesktopTabs Where c.Sortpath.StartsWith(_tab.Sortpath) And c.PortalId = portalSettings.PortalId Order By c.Sortpath).ToList()

        For Each tab As DesktopTab In tabs
            If PortalSecurity.IsInRoles(tab.AuthorizedRoles, tab.TabId) Then
                Dim row2 = table.NewRow()
                row2("Id") = tab.TabId.ToString()
                row2("Text") = tab.TabName.ToString()
                row2("IsLeaf") = False
                row2("ParentId") = IIf(tab.ParentId Is Nothing, DBNull.Value, tab.ParentId.ToString())
                row2("URL") = String.Format("pages.aspx?pageid={0}", tab.PortalId.ToString())

                row2("Level") = tab.Sortpath.ToString().Split(";").Count
                table.Rows.Add(row2)

            End If
        Next tab

        Return table
    End Function

End Class
