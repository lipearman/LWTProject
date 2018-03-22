Imports Portal.Components
Imports System.Data.SqlClient
Imports System.Data
Imports DevExpress.Web
Imports DevExpress.XtraPivotGrid
Imports DevExpress.Web.Data

Partial Class Modules_ucDevxAttribute
    Inherits PortalModuleControl
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(LWT.Website.webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")

        End If

        If IsPostBack = False Then
            Session("CUBE_ID") = Nothing
        End If
        pnMain.HeaderText = portalSettings.ActiveTab.TabName
        pnMain.HeaderImage.IconID = portalSettings.ActiveTab.IconID
    End Sub
    Protected Sub ASPxCallback1_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbCube.Callback
        Dim CUBE_ID = e.Parameter.ToString()
        Session("CUBE_ID") = CUBE_ID
        e.Result = "success"
    End Sub
    Protected Sub cbRetrieve_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbRetrieve.Callback
        If Session("CUBE_ID") IsNot Nothing Then
            Dim CUBE_ID = Session("CUBE_ID").ToString()
            If Not String.IsNullOrEmpty(CUBE_ID) Then
                Using dc As New DataClasses_PortalBIExt()
                    pivotGrid.Caption = ""
                    pivotGrid.Fields.Clear()

                    Dim _OLAP = (From c In dc.V_Cubes Where c.CUBE_ID.Equals(CUBE_ID)).FirstOrDefault()
                    pivotGrid.OLAPConnectionString = _OLAP.CONNECTING
                    pivotGrid.OLAPDataProvider = OLAPDataProvider.OleDb
                    pivotGrid.BeginUpdate()
                    pivotGrid.RetrieveFields(PivotArea.FilterArea, False)

                    Dim _Attributes As New List(Of tblAttribute)

                    For Each _field As DevExpress.Web.ASPxPivotGrid.PivotGridField In pivotGrid.Fields
                        Dim _data = (From c In dc.tblAttributes Where c.FIELD.Equals(_field.FieldName) And c.CUBE_ID.Equals(CUBE_ID)).ToList()
                        If _data.Count = 0 Then
                            Dim _Attribute As New tblAttribute
                            With _Attribute
                                .FIELD = _field.FieldName
                                .FOLDER = _field.DisplayFolder
                                .CUBE_ID = CUBE_ID
                                .ATTRIBUTE = _field.Caption
                                .VISIBLE = "TRUE"

                                If _field.FieldName.StartsWith("[Measures]") Then
                                    .TYPE = "Measure"
                                    .FormatType = "{0:N2}"
                                Else
                                    .TYPE = "Attribute"
                                    .FormatType = "{0:N0}"
                                End If
                            End With

                            _Attributes.Add(_Attribute)
                        End If
                    Next


                    If _Attributes.Count > 0 Then
                        dc.tblAttributes.InsertAllOnSubmit(_Attributes)
                        dc.SubmitChanges()
                    End If
                End Using



                e.Result = "success"
            Else
                e.Result = "No Cube is selected."
            End If
        Else
            e.Result = "No Cube is selected."
        End If
    End Sub
    'Protected Sub grid_CellEditorInitialize(ByVal sender As Object, ByVal e As ASPxGridViewEditorEventArgs) Handles grid.CellEditorInitialize
    'If e.Column.FieldName.Equals("FIELD") And Not grid.IsNewRowEditing Then
    '    e.Editor.ClientEnabled = False
    'End If

    'If e.Column.FieldName.Equals("CUBE") And Not grid.IsNewRowEditing Then
    '    e.Editor.ClientEnabled = False
    'End If
    'End Sub




    Protected Sub grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles grid.RowUpdating

        Dim _ID = e.Keys("ID")

        Using dc = New DataClasses_PortalBIExt


            Dim _Data = (From c In dc.tblAttributes Where c.ID.Equals(_ID) Select c).FirstOrDefault()

            With _Data
                .FOLDER = e.NewValues("FOLDER")
                .ATTRIBUTE = e.NewValues("ATTRIBUTE")
                .FormatType = e.NewValues("FormatType")
                .Description = e.NewValues("Description")
            End With
            dc.SubmitChanges()

        End Using

        grid.CancelEdit()
        e.Cancel = True
    End Sub
End Class
