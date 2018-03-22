Imports System.Data
Imports System.Web.Security
Imports Portal.Components

Imports DataBind
Imports DevExpress.Web
Imports System.Drawing

Partial Class Modules_ucDevxUnderwriterSetup
    Inherits PortalModuleControl


    Protected Sub cbSaveAddAE_Callback(ByVal source As Object, ByVal e As CallbackEventArgs) Handles cbSaveAddUW.Callback

        Dim _dc As New DataClasses_CPSExt()
        Dim _data_UW = (From c In _dc.Register_Unwriters Where c.Underwriter.Equals(GridUWLookup.Value)).FirstOrDefault()
        If _data_UW Is Nothing Then
            Dim _dc_SIBIS As New DataClasses_SIBISExt()
            Dim _data_SIBIS = (From c In _dc_SIBIS.Underwriters Where c.Underwriter.Equals(GridUWLookup.Value)).FirstOrDefault()
            Dim _newData As New Register_Unwriter
            With _newData
                .Underwriter = _data_SIBIS.Underwriter
                .CrossReference = _data_SIBIS.CrossReference
                .Name = _data_SIBIS.Name
                .Address1 = _data_SIBIS.Address1
                .Address2 = _data_SIBIS.Address2
                .Address3 = _data_SIBIS.Address3
                .PostCode = _data_SIBIS.PostCode
                .City = _data_SIBIS.City
                .PhoneBusiness = _data_SIBIS.PhoneBusiness
                .PhoneHome = _data_SIBIS.PhoneHome
                .Facsimile = _data_SIBIS.Facsimile
                .AccountContact = _data_SIBIS.AccountContact
                .Addressee = _data_SIBIS.Addressee
                .Salutation = _data_SIBIS.Salutation
                .DaysCredit = _data_SIBIS.DaysCredit
                .TrueUnderwriter = _data_SIBIS.TrueUnderwriter
                .EntryBy = _data_SIBIS.EntryBy
                .EntryDate = _data_SIBIS.EntryDate
                .FinanceContact = _data_SIBIS.FinanceContact
                .GeneralClaimContact = _data_SIBIS.GeneralClaimContact
                .Type = _data_SIBIS.Type
                .InsuranceLine = _data_SIBIS.InsuranceLine
                .VATPayType = _data_SIBIS.VATPayType
                .PhoneFinance = _data_SIBIS.PhoneFinance
                .PhoneClaims = _data_SIBIS.PhoneClaims
                .FaxFinance = _data_SIBIS.FaxFinance
                .FaxClaims = _data_SIBIS.FaxClaims
                .CreationDate = DateTime.Now
                .ApproveDate = _data_SIBIS.EntryDate

                .CreationBy = HttpContext.Current.User.Identity.Name
                .RequestDate = _data_SIBIS.EntryDate
                .RequestBy = _data_SIBIS.EntryBy
                .ApproveBy = _data_SIBIS.EntryBy

                .IsActive = True
                '.UserName = _data_SIBIS.UserName
                '.Password = _data_SIBIS.Password
                '.Email = _data_SIBIS.Email
                '.ShortName = ShortName.Text
                '.InsurerCode = InsurerCode.Text

            End With
            _dc.Register_Unwriters.InsertOnSubmit(_newData)
            _dc.SubmitChanges()

            e.Result = "success"
        Else
            e.Result = String.Format("Code {0} has already in system.", GridUWLookup.Value)
        End If


        '        
        '


    End Sub


    'Protected Sub btnExpandAll_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnExpandAll.Click
    '    gridUW.DetailRows.ExpandAllRows()
    'End Sub
    'Protected Sub btnCollapseAll_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCollapseAll.Click
    '    gridUW.DetailRows.CollapseAllRows()
    'End Sub
    Protected Sub gridUW_HtmlRowCreated(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewTableRowEventArgs) Handles gridUW.HtmlRowPrepared

        If e.RowType = GridViewRowType.Group Then
            'e.Row.BackColor = backColor
            e.Row.Font.Bold = True
        End If
    End Sub

  
End Class
