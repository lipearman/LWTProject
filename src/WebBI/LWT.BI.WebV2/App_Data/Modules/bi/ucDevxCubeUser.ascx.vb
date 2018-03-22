Imports Portal.Components
Imports System.Data.SqlClient
Imports System.Data
Imports DevExpress.Web

Partial Class Modules_ucDevxCubeUser
    Inherits PortalModuleControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(LWT.Website.webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If

        pnMain.HeaderText = portalSettings.ActiveTab.TabName
        pnMain.HeaderImage.IconID = portalSettings.ActiveTab.IconID
    End Sub

    Protected Sub grid_RowInserting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles grid.RowInserting
        Dim grid As ASPxGridView = TryCast(sender, ASPxGridView)

        Dim _USERNAME = e.NewValues("USERNAME").ToString()
        Dim _CUBE_ID = e.NewValues("CUBE_ID").ToString()
        Using dc As New DataClasses_PortalBIExt()
            Dim data = (From c In dc.tblCubes Where c.CUBE_ID.Equals(_CUBE_ID)).FirstOrDefault()


            dc.tblCube_Users.InsertOnSubmit(New tblCube_User With {.CUBE_ID = _CUBE_ID, .USERNAME = _USERNAME})

            dc.SubmitChanges()
        End Using

        e.Cancel = True
        grid.CancelEdit()
    End Sub


End Class
