Imports Portal.Components
Imports System.Data.SqlClient
Imports System.Data
Imports DevExpress.Web
Imports DevExpress.Web.Data

Partial Class Modules_ucDevxCube
    Inherits PortalModuleControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(LWT.Website.webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If

        pnMain.HeaderText = portalSettings.ActiveTab.TabName
        pnMain.HeaderImage.IconID = portalSettings.ActiveTab.IconID
    End Sub

    Protected Sub grid_CellEditorInitialize(ByVal sender As Object, ByVal e As ASPxGridViewEditorEventArgs) Handles grid.CellEditorInitialize
        If e.Column.FieldName.Equals("DATABASE") And Not grid.IsNewRowEditing Then
            e.Editor.ClientEnabled = False
        End If

        If e.Column.FieldName.Equals("CUBE") And Not grid.IsNewRowEditing Then
            e.Editor.ClientEnabled = False
        End If
    End Sub


    Protected Sub grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles grid.RowInserting

        Using dc = New DataClasses_PortalBIExt
            Dim _CUBE = e.NewValues("CUBE").ToString().Split("/")


            Dim ds = (From c In dc.tblDataSources Where c.DATABASE.Equals(_CUBE(0))).ToList()
            If ds.Count = 0 Then
                Throw New Exception(String.Format("No Data Source {0} in BI System!!! ", _CUBE(0)))
            End If

            Dim data = (From c In dc.tblCubes Where c.DATABASE.Equals(_CUBE(0)) And c.CUBE.Equals(_CUBE(1))).ToList

            If data.Count > 0 Then
                Throw New Exception(String.Format("Cube name :{0} has already in BI System!!! ", _CUBE(1)))
            Else
                Dim newData As New tblCube

                With newData

                    .DATABASE = _CUBE(0)
                    .CUBE = _CUBE(1)
                    .BASE_CUBE_NAME = e.NewValues("BASE_CUBE_NAME")

                End With
                dc.tblCubes.InsertOnSubmit(newData)
                dc.SubmitChanges()

            End If
           
        End Using

        grid.CancelEdit()
        e.Cancel = True

    End Sub




    Protected Sub grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles grid.RowUpdating

        Dim CUBE_ID = e.Keys("CUBE_ID")

        Using dc = New DataClasses_PortalBIExt
             

            Dim _Data = (From c In dc.tblCubes Where c.CUBE_ID.Equals(CUBE_ID) Select c).FirstOrDefault()

            With _Data

                .BASE_CUBE_NAME = e.NewValues("BASE_CUBE_NAME")

            End With
            dc.SubmitChanges()

        End Using

        grid.CancelEdit()
        e.Cancel = True
    End Sub


End Class
