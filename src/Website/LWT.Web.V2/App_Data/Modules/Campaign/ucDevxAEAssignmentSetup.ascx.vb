Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports DevExpress.Web

Partial Class Modules_ucDevxAEAssignmentSetup
    Inherits PortalModuleControl 
  
 
    Protected Sub cbOpenPopup_Callback(ByVal source As Object, ByVal e As CallbackEventArgs) Handles cbOpenPopup.Callback
        Dim _username = e.Parameter

        Dim _dc As New DataClasses_CPSExt()
        Dim _data_user = (From c In _dc.tblProjectUserAssignments Where c.UserName.Equals(_username)).ToList()
        If _data_user.Count > 0 Then
            Dim sb As New StringBuilder()
            For Each _item In _data_user
                sb.Append(_item.AECode.Trim() & ";")
            Next
            e.Result = sb.ToString()
        Else
            e.Result = ""
        End If


    End Sub
    Protected Sub cbSaveAddAE_Callback(ByVal source As Object, ByVal e As CallbackEventArgs) Handles cbSaveAddAE.Callback

        Dim _param = e.Parameter.Split(":")

        Dim _dc_portal As New DataClasses_PortalDataContextExt()
        Dim _User = (From c In _dc_portal.V_LWT_USERs Where c.sAMAccountName.Equals(_param(0))).FirstOrDefault()

        Dim _dc As New DataClasses_CPSExt()

       
        _dc.ExecuteCommand("delete from tblProjectUserAssignment where UserName='" & _User.sAMAccountName & "'")

        Dim _selecedAE = _param(1).Split(",")
        For Each idx In _selecedAE
            If Not String.IsNullOrEmpty(idx) Then
                _dc.ExecuteCommand("insert into tblProjectUserAssignment(UserName,AECode,Name,Department) values('" & _User.sAMAccountName & "','" & AECodeList.Items(idx).Value & "','" & _User.displayName & "','" & _User.department & "')")
            End If
        Next


        e.Result = "success"
    End Sub

     
End Class
