Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports DevExpress.Web.Data
Imports DevExpress.Web.ASPxHtmlEditor
Imports DevExpress.Web
Imports LWT.Website

Partial Class Modules_ucMailNotifications
    Inherits PortalModuleControl
    '*******************************************************
    '
    ' The Page_Load server event handler on this user control is used
    ' to populate the current site settings from the config system
    '
    '*******************************************************
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If
        Session("PortalId") = webconfig._PortalID

        '' Verify that the current user has access to access this page
        'If PortalSecurity.IsInRoles("Admins") = False Then
        '    Response.Redirect("~/Admin/EditAccessDenied.aspx")
        '    'Response.End()
        'End If

        ' If this is the first visit to the page, populate the site data
        If Page.IsPostBack = False Then

            ' Obtain PortalSettings from Current Context
            'Dim portalSettings As PortalSettings = CType(Context.Items(ConfigurationSettings.AppSettings("PortalContextName")), PortalSettings)


            'siteName.Text = portalSettings.PortalName
            'showEdit.Checked = portalSettings.AlwaysShowEditButton

            'BindData()

        End If
    End Sub




    Protected Sub grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles grid.RowUpdating

        Dim _ID = e.Keys("ID")

        Using dc = New DataClasses_PortalDataContextExt

            Dim _Data = (From c In dc.MailNotifications Where c.ID.Equals(_ID) Select c).FirstOrDefault()

            With _Data
                .Code = e.NewValues("Code")
                .Name = e.NewValues("Name")

                .IsActive = e.NewValues("IsActive")
                .CreationDate = DateTime.Now

                .MailFrom = e.NewValues("MailFrom")
                .MailTo = e.NewValues("MailTo")
                .MailCC = e.NewValues("MailCC")
                .MailBcc = e.NewValues("MailBcc")
                .MailSubject = e.NewValues("MailSubject")

                Dim _grid As ASPxGridView = TryCast(sender, ASPxGridView)
                Dim MailBody As ASPxHtmlEditor = TryCast(_grid.FindEditFormTemplateControl("MailBody"), ASPxHtmlEditor)

                .MailBody = Server.HtmlEncode(MailBody.Html)
            End With
            dc.SubmitChanges()

        End Using

        grid.CancelEdit()
        e.Cancel = True
    End Sub
    Protected Sub grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles grid.RowInserting

        Using dc = New DataClasses_PortalDataContextExt

            Dim newData As New Portal.Components.MailNotification ' MailNotification

            With newData
                .Code = e.NewValues("Code")
                .Name = e.NewValues("Name")

                .IsActive = e.NewValues("IsActive")
                .CreationDate = DateTime.Now

                .MailFrom = e.NewValues("MailFrom")
                .MailTo = e.NewValues("MailTo")
                .MailCC = e.NewValues("MailCC")
                .MailBcc = e.NewValues("MailBcc")
                .MailSubject = e.NewValues("MailSubject")

                Dim _grid As ASPxGridView = TryCast(sender, ASPxGridView)
                Dim MailBody As ASPxHtmlEditor = TryCast(_grid.FindEditFormTemplateControl("MailBody"), ASPxHtmlEditor)

                .MailBody = Server.HtmlEncode(MailBody.Html)
            End With
            dc.MailNotifications.InsertOnSubmit(newData)
            dc.SubmitChanges()
        End Using

        grid.CancelEdit()
        e.Cancel = True

    End Sub


End Class
