Imports System.Data
Imports System.Web.Security
Imports Portal.Components


Imports DevExpress.Web

Partial Class Modules_ucDevxAESetup
    Inherits PortalModuleControl
 
    Protected Sub cbSaveAddAE_Callback(ByVal source As Object, ByVal e As CallbackEventArgs) Handles cbSaveAddAE.Callback
 
        Dim _dc As New DataClasses_CPSExt()
        Dim _data_AccountExecutives = (From c In _dc.Register_AccountExecutives Where c.AccountExecutive.Equals(GridAELookup.Value)).FirstOrDefault()
        If _data_AccountExecutives Is Nothing Then
            Dim _dc_SIBIS As New DataClasses_SIBISExt()
            Dim _data_SIBIS = (From c In _dc_SIBIS.AccountExecutives Where c.AccountExecutive.Equals(GridAELookup.Value)).FirstOrDefault()
            Dim _newData As New Register_AccountExecutive()
            With _newData
                .AccountExecutive = _data_SIBIS.AccountExecutive
                .Name = _data_SIBIS.Name
                .Address1 = _data_SIBIS.Address1
                .Address2 = _data_SIBIS.Address2
                .Address3 = _data_SIBIS.Address3
                .PostCode = _data_SIBIS.PostCode
                .City = _data_SIBIS.City
                .PhoneBusiness = _data_SIBIS.PhoneBusiness
                .PhoneHome = _data_SIBIS.PhoneHome
                .EntryBy = _data_SIBIS.EntryBy
                .EntryDate = _data_SIBIS.EntryDate
                .Unit = _data_SIBIS.Unit
                .Title = _data_SIBIS.Title
                .Internet_Address = _data_SIBIS.Internet_Address
            End With
            _dc.Register_AccountExecutives.InsertOnSubmit(_newData)
            _dc.SubmitChanges()
            'gridAE.DataBind()
            e.Result = "success"

        Else
            e.Result = String.Format("Code {0} has already in system.", GridAELookup.Value)
        End If

    End Sub


    'Protected Sub gridAE_CellEditorInitialize(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewEditorEventArgs) Handles gridAE.CellEditorInitialize
    '    Dim ASPxGridView1 = DirectCast(sender, ASPxGridView)
    '    If (ASPxGridView1.IsNewRowEditing) Then Return
    '    If (e.Column.FieldName = "AccountExecutive") Then
    '        e.Editor.Enabled = False
    '    End If

    'End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        'If (Not IsPostBack) Then
        'gridAE.DataSource = SqlDataSource_AE
        'gridAE.DataBind()
        'End If
    End Sub
End Class
