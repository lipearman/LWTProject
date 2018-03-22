Imports System.Data
Imports System.Web.Security
Imports Portal.Components

Imports DataBind

Partial Class Modules_ucDevxLeasingSetup
    Inherits PortalModuleControl

    Protected Sub grid_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles grid.InitNewRow
        e.NewValues("IsActive") = True
    End Sub
End Class
