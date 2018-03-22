Imports System.Data
Imports System.Web.Security
Imports Portal.Components

Imports DataBind
Imports DevExpress.Web

Partial Class Modules_ucDevxCarBrandModelSetup
    Inherits PortalModuleControl

     
    'Protected Sub detailGrid_DataSelect(ByVal sender As Object, ByVal e As EventArgs)

    'End Sub

    'Protected Sub ASPxCardView1_CardInserting(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInsertingEventArgs) Handles detailGrid.RowInserting


    '    e.NewValues("ParentID") = cardView.GetSelectedFieldValues("ID")


    'End Sub
    'Protected Sub ASPxCardView1_DataBinding(ByVal sender As Object, ByVal e As EventArgs) Handles detailGrid.DataBinding
    '    If cardView.GetSelectedFieldValues("ID").Count > 0 Then
    '        Session("ParentID") = ParentID.Text
    '    End If
    'End Sub

    Protected Sub cbModel_Callback(ByVal source As Object, ByVal e As CallbackEventArgs) Handles cbModel.Callback
        Session("ParentID") = e.Parameter
    End Sub

End Class
