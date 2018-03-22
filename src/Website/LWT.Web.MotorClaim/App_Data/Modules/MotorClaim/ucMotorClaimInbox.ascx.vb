Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text.RegularExpressions
Imports System.Web
Imports System.Web.UI
Imports DevExpress.Data.Filtering
Imports DevExpress.Web
Imports System.Xml
Imports Portal.Components

'Imports DevExpress.Spreadsheet
Imports System.IO



Imports NPOI.XSSF.UserModel
Imports Excel
Imports System.Data
Imports Context
Imports LWT.Website
Imports MotorClaimWebService


Partial Class Modules_ucMotorClaimInbox
    Inherits PortalModuleControl
    Private Const PreviewMessageFormat As String = "<div class='MailPreview'>" & "<div class='Subject'>{0}</div>" & "<div class='Info'>" & "<div>From: {1}</div>" & "<div>To: {2}</div>" & "<div>Date: {3:g}</div>" & "</div>" & "<div class='Separator'></div>" & "<div class='Body'>{4}</div>" & "</div>", ReplyMessageFormat As String = "Hi,<br/><br/><br/><br/>Thanks,<br/>Thomas Hardy<br/><br/><br/>----- Original Message -----<br/>Subject: {0}<br/>From: {1}<br/>To: {2}<br/>Date: {3:g}<br/>{4}", NotFoundMessageFormat As String = "<h1>Can't find message with the key={0}</h1>"

    Protected ReadOnly Property SearchText() As String
        Get
            Return Utils.GetSearchText(Page)
        End Get
    End Property

    'Protected Sub Page_PreInit(ByVal sender As Object, ByVal e As EventArgs) Handles Me.PreInit
    '    Utils.ApplyTheme(Page)
    'End Sub

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Init





        If (Not IsPostBack) Then


            'Dim filePath = Server.MapPath("~/Template/ImportFormat.xlsx")
            'Using Spreadsheet As New ASPxSpreadsheet.ASPxSpreadsheet

            '    Spreadsheet.Open(filePath)


            '    Dim worksheet As Worksheet = Spreadsheet.Document.Worksheets(0)


            '    Dim dr = worksheet.Cells.Count

            'End Using

            Session("MCGUID") = Nothing
            Session("MCTYPE") = Nothing

        End If


        If (Not Page.IsPostBack) Then
            'MailTree.SelectedNode = MailTree.Nodes.FindByText("00 - Open")
            MailTree.SelectedNode = MailTree.Nodes.FindByText("Claim Inbox")
        End If
    End Sub
    'Protected Function ShowToColumn() As Boolean
    '    Return MailTree.SelectedNode.Name = "Sent Items" OrElse MailTree.SelectedNode.Name = "Drafts"
    'End Function

    Private Function ShouldBindGrid() As Boolean
        Return (Not Page.IsCallback) OrElse MailGrid.IsCallback
    End Function
    Protected Sub Page_Load(ByVal s As Object, ByVal e As EventArgs) Handles Me.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
        If Not PortalSecurity.IsInRoles(portalSettings.ActiveTab.AuthorizedRoles, portalSettings.ActiveTab.TabId) = True Then
            Response.Redirect("~/Admin/AccessDenied.aspx")
        End If

        Dim _Portal_User = MotorClaimModel.DataProvider.Portal_User_Data.AsQueryable()
        Dim _UWCode = _Portal_User.Where(Function(m) m.UserName.Equals(HttpContext.Current.User.Identity.Name)).FirstOrDefault()


        If Not System.IO.Directory.Exists(Server.MapPath("~/App_Data/ConsentForms/" & _UWCode.PasswordQuestion & "/ConsentForms")) Then
            System.IO.Directory.CreateDirectory(Server.MapPath("~/App_Data/ConsentForms/" & _UWCode.PasswordQuestion & "/ConsentForms"))
        End If

        With ASPxFileManager1
            .Settings.RootFolder = "~/App_Data/ConsentForms/" & _UWCode.PasswordQuestion & "/ConsentForms"
            .SettingsUpload.AdvancedModeSettings.TemporaryFolder = .Settings.RootFolder
            '.SettingsFolders.Visible = False
            '.SettingsToolbar.ShowPath = False
            '.SettingsToolbar.ShowRefreshButton = False
            '.SettingsToolbar.ShowDownloadButton = False


            .SettingsEditing.AllowCreate = True
            .SettingsEditing.AllowRename = True
            .SettingsEditing.AllowMove = True

            '.SettingsUpload.UseAdvancedUploadMode = False
            '.SettingsUpload.AdvancedModeSettings.PacketSize.MinValue = 10
            .SettingsUpload.AdvancedModeSettings.EnableMultiSelect = True

        End With




        ActionMenuDataSource.XPath = String.Format("Pages/{0}/Item", Utils.CurrentPageName)
        ActionMenu.ShowPopOutImages = DevExpress.Utils.DefaultBoolean.True

        If ShouldBindGrid() Then
            BindGrid()
        End If

        'If MailFormPanel.IsCallback OrElse IsPostBack AndAlso (Not IsCallback) Then
        '    AddressesList.DataSource = DemoModel.DataProvider.Contacts.Select(Function(c) New With {Key .Text = c.Name, Key .Value = c.Email, Key .ImageUrl = Utils.GetContactPhotoUrl(c.PhotoUrl)})
        '    AddressesList.DataBind()
        'End If

        'MailGrid.DataColumns("To").Visible = ShowToColumn()
        'MailGrid.DataColumns("From").Visible = Not ShowToColumn()

    End Sub
    Protected Sub ActionMenu_ItemDataBound(ByVal sender As Object, ByVal e As MenuItemEventArgs)
        Dim itemHierarchyData As IHierarchyData = CType(e.Item.DataItem, IHierarchyData)
        Dim element = CType(itemHierarchyData.Item, XmlElement)

        Dim classAttr = element.Attributes("SpriteClassName")
        'If classAttr IsNot Nothing Then
        e.Item.Image.SpriteProperties.CssClass = classAttr.Value
        'End If

        'If e.Item.Parent Is e.Item.Menu.RootItem Then
        '    e.Item.ClientVisible = False
        'End If
    End Sub
    Private Sub BindGrid()
        MailGrid.DataSource = SelectMessages()
        MailGrid.DataBind()
        MailGrid.ExpandAll()
    End Sub
    Private Function SelectMessages() As List(Of V_ClaimTransactionData)

        Dim _Portal_User = MotorClaimModel.DataProvider.Portal_User_Data.AsQueryable()
        Dim _UWCode = _Portal_User.Where(Function(m) m.UserName.Equals(HttpContext.Current.User.Identity.Name)).FirstOrDefault()


        Dim result = MotorClaimModel.DataProvider.V_ClaimTransactionData.AsQueryable()

        Select Case MailTree.SelectedNode.Text
            Case "00 - Open"
                result = result.Where(Function(m) m.ClaimStatus.Equals("00") And m.Unwriter.Equals(_UWCode.PasswordQuestion) And m.IsPost.Equals(0)).OrderByDescending(Function(m) m.TRID)
            Case "01 - Reserve"
                result = result.Where(Function(m) m.ClaimStatus.Equals("01") And m.Unwriter.Equals(_UWCode.PasswordQuestion) And m.IsPost.Equals(0)).OrderByDescending(Function(m) m.TRID)
            Case "02 - Payment"
                result = result.Where(Function(m) m.ClaimStatus.Equals("02") And m.Unwriter.Equals(_UWCode.PasswordQuestion) And m.IsPost.Equals(0)).OrderByDescending(Function(m) m.TRID)
            Case "99 - Close"
                result = result.Where(Function(m) m.ClaimStatus.Equals("99") And m.Unwriter.Equals(_UWCode.PasswordQuestion) And m.IsPost.Equals(0)).OrderByDescending(Function(m) m.TRID)
            Case "98 - ReOpen"
                result = result.Where(Function(m) m.ClaimStatus.Equals("98") And m.Unwriter.Equals(_UWCode.PasswordQuestion) And m.IsPost.Equals(0)).OrderByDescending(Function(m) m.TRID)
                'Case "Consent Form"
                '    result = result.Where(Function(m) m.ClaimStatus.Equals("CF"))
            Case Else
                result = result.Where(Function(m) Not m.ClaimStatus.Equals("CF") And m.Unwriter.Equals(_UWCode.PasswordQuestion) And m.IsPost.Equals(0)).OrderByDescending(Function(m) m.TRID)
        End Select

        Return result.ToList()

    End Function




    '<dx:ASPxCallbackPanel ID="MailPreviewPanel" runat="server" RenderMode="Div" Height="100%" CssClass="MailPreviewPanel" ClientInstanceName="ClientMailPreviewPanel" ClientVisible="false"
    '          OnCallback="MailPreviewPanel_Callback" />
    Protected Sub MailPreviewPanel_Callback(ByVal sender As Object, ByVal e As CallbackEventArgsBase) Handles MailPreviewPanel.Callback
        Dim id As Integer



        Dim sb As New StringBuilder()


        Dim text = String.Format(NotFoundMessageFormat, e.Parameter)

        If Integer.TryParse(e.Parameter, id) Then




            Dim data = MotorClaimModel.DataProvider.tblClaimTransaction_Data.AsQueryable()



            Dim MyData = data.Where(Function(m) m.TRID.Equals(id)).FirstOrDefault()
            'Dim message = result.Where(Function(m) m.TRID.Equals(id)).FirstOrDefault()
            'If message IsNot Nothing Then                 
            '    Select Case message.ClaimStatus
            '        Case "00" ' - Open

            '        Case "01" ' - Reserve

            '        Case "02" ' - Payment

            '        Case "99" ' - Close

            '        Case "98" ' - ReOpen

            '        Case "CF" 'Consent Form

            '    End Select
            'End If
            formPreview.DataSource = MyData
            formPreview.DataBind()

            If MyData.ClaimStatus.Equals("CF") Then


                formPreview.Items(formPreview.Items.Count - 1).Visible = False
            Else

                formPreview.Items(formPreview.Items.Count - 1).Visible = True


            End If


            If MyData.Status = False Then
                Dim result = MotorClaimModel.DataProvider.tblClaimTransaction_Result.AsQueryable()
                ASPxGridPreview_Result.DataSource = result.Where(Function(m) m.GUID.Equals(MyData.GUID)).ToList()
                ASPxGridPreview_Result.DataBind()

                ASPxGridPreview_Result.Visible = True
            Else
                ASPxGridPreview_Result.Visible = False
            End If



            'ASPxGridPreview_Result.ExpandAll()

            'SqlDataSource_PreviewResult.SelectParameters("").DefaultValue = ""


            'SqlDataSource_PreviewResult.DataSourceMode = SqlDataSourceMode.DataReader
            ''============ todo2=======================
            'SqlDataSource_PreviewResult.SelectCommand = String.Format("SELECT * FROM tblClaimTransaction_Result where GUID='{0}' ", MyData(0).GUID.ToString())
            'SqlDataSource_PreviewResult.DataBind()

            'ASPxGridPreview_Result.DataSourceID = "SqlDataSource_PreviewResult"
            'ASPxGridPreview_Result.DataBind()

        End If


        'MailPreviewPanel.Controls.Add(New LiteralControl(sb.ToString()))


    End Sub







    'Private Function FormatMessageCore(ByVal message As Message, ByVal format As String) As String
    '    Dim subject = message.Subject
    '    If message.IsReply Then
    '        subject = "Re: " & subject
    '    End If
    '    Return String.Format(format, subject, message.From, message.To, message.Date, message.Text)
    'End Function

    'Protected Function TryParseKeyValues(ByVal stringKeys As IEnumerable(Of String), <System.Runtime.InteropServices.Out()> ByRef resultKeys() As Integer) As Boolean
    '    resultKeys = Nothing
    '    Dim list = New List(Of Integer)()
    '    For Each sKey In stringKeys
    '        Dim key As Integer
    '        If (Not Integer.TryParse(sKey, key)) Then
    '            Return False
    '        End If
    '        list.Add(key)
    '    Next sKey
    '    resultKeys = list.ToArray()
    '    Return True
    'End Function

    'Private Function GetMessagesKeyMap(ByVal messages As IEnumerable(Of Message)) As Dictionary(Of String, List(Of String))
    '    Dim dict = New Dictionary(Of String, List(Of String))()
    '    Dim query = messages.GroupBy(Function(m) m.Folder).Where(Function(g) g.Count() > 0)
    '    For Each item In messages.GroupBy(Function(m) m.Folder).Where(Function(g) g.Count() > 0)
    '        dict.Add(item.Key, item.Select(Function(m) m.ID.ToString()).ToList())
    '    Next item
    '    Return dict
    'End Function


    '==================================== MailTree ==============================================
    '  <dx:ASPxTreeView runat="server" ID="MailTree" ClientInstanceName="ClientMailTree" AllowSelectNode="True" CssClass="MailTree"
    'OnCustomJSProperties="MailTree_CustomJSProperties">
    'Protected Sub MailTree_CustomJSProperties(ByVal sender As Object, ByVal e As CustomJSPropertiesEventArgs) Handles MailTree.CustomJSProperties
    '    e.Properties("cpUnreadMessagesHash") = GetMessagesKeyMap(DemoModel.DataProvider.UnreadMessages)
    'End Sub

    '==================================== MailGrid ==============================================
    '<dx:ASPxGridView runat="server" ID="MailGrid"
    'ClientInstanceName="ClientMailGrid" Width="100%" KeyFieldName="ID" EnableRowsCache="false" 
    'OnCustomCallback="MailGrid_CustomCallback"
    'OnCustomDataCallback="MailGrid_CustomDataCallback"
    'OnCustomColumnDisplayText="MailGrid_CustomColumnDisplayText" 
    'OnCustomGroupDisplayText="MailGrid_CustomGroupDisplayText"
    'OnCustomJSProperties="MailGrid_CustomJSProperties"
    'Border-BorderWidth="0">

    Protected Sub MailGrid_CustomCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomCallbackEventArgs) Handles MailGrid.CustomCallback
        If String.IsNullOrEmpty(e.Parameters) Then
            Return
        End If
        Dim args = e.Parameters.Split("|"c)
        If args(0) = "FolderChanged" OrElse args(0) = "Search" Then
            Dim filter As String = If(args(0) = "Search", SearchText, String.Empty)
            MailGrid.SearchPanelFilter = filter
            BindGrid()

        End If
        'If args(0) = "SendMail" OrElse args(0) = "SaveMail" Then
        '    Dim subject = SubjectEditor.Text
        '    Dim [to] = ToEditor.Text
        '    Dim messageText As String = If(MailEditor.Html.Length <= 10000, MailEditor.Html, MailEditor.Html.Substring(0, 10000))
        '    Dim folder As String = If(args(0) = "SendMail", "Sent Items", "Drafts")
        '    Dim id As Integer
        '    If args.Length = 2 AndAlso Integer.TryParse(args(1), id) Then
        '        DemoModel.DataProvider.UpdateMessage(id, subject, [to], messageText, folder)
        '    Else
        '        DemoModel.DataProvider.AddMessage(subject, [to], messageText, folder)
        '    End If
        '    BindGrid()
        'End If
        'If args(0) = "Delete" AndAlso args.Length > 1 Then
        '    Dim keys() As Integer = Nothing
        '    If (Not TryParseKeyValues(args.Skip(1), keys)) Then
        '        Return
        '    End If
        '    DemoModel.DataProvider.DeleteMessages(keys)
        '    BindGrid()
        'End If
    End Sub

    'Protected Sub MailGrid_CustomDataCallback(ByVal sender As Object, ByVal e As ASPxGridViewCustomDataCallbackEventArgs) Handles MailGrid.CustomDataCallback
    '    Dim args = e.Parameters.Split("|"c)
    '    If args(0) = "MailForm" AndAlso args(1) = "Reply" AndAlso args.Length = 3 Then
    '        Dim id As Integer
    '        If (Not Integer.TryParse(args(2), id)) Then
    '            Return
    '        End If
    '        Dim message = DemoModel.DataProvider.Messages.FirstOrDefault(Function(m) m.ID = id)
    '        If message Is Nothing Then
    '            Return
    '        End If
    '        Dim result = New Dictionary(Of String, String)()
    '        result("To") = message.To

    '        Dim subject = message.Subject
    '        If (Not subject.StartsWith("Re: ")) Then
    '            subject = "Re: " & subject
    '        End If
    '        result("Subject") = subject

    '        result("Text") = FormatMessageCore(message, ReplyMessageFormat)
    '        e.Result = result
    '    End If
    '    If args(0) = "MailForm" AndAlso args.Length = 3 AndAlso args(1) = "EditDraft" Then
    '        Dim id As Integer
    '        If (Not Integer.TryParse(args(2), id)) Then
    '            Return
    '        End If
    '        Dim message = DemoModel.DataProvider.Messages.FirstOrDefault(Function(m) m.ID = id)
    '        If message Is Nothing Then
    '            Return
    '        End If
    '        Dim result = New Dictionary(Of String, String)()
    '        result("To") = message.To
    '        result("Subject") = message.Subject
    '        result("Text") = message.Text
    '        e.Result = result
    '    End If
    '    If args(0) = "MarkAs" AndAlso args.Length > 2 Then
    '        Dim read = args(1) = "Read"
    '        Dim keys() As Integer = Nothing
    '        If (Not TryParseKeyValues(args.Skip(2), keys)) Then
    '            Return
    '        End If
    '        DemoModel.DataProvider.MarkMessagesAs(read, keys)
    '    End If
    'End Sub

    'Protected Sub MailGrid_CustomColumnDisplayText(ByVal sender As Object, ByVal e As ASPxGridViewColumnDisplayTextEventArgs) Handles MailGrid.CustomColumnDisplayText
    '    If e.Column.FieldName = "Subject" AndAlso CBool(e.GetFieldValue("IsReply")) Then
    '        e.DisplayText = String.Format("Re: {0}", e.Value)
    '    End If
    '    If e.Column.FieldName = "To" Then
    '        Dim list = New List(Of String)()
    '        For Each item In e.Value.ToString().Split(","c)
    '            Dim email = item.Trim()
    '            Dim contact = DemoModel.DataProvider.Contacts.FirstOrDefault(Function(c) c.Email = email)
    '            list.Add(If(contact IsNot Nothing, contact.Name, email))
    '        Next item
    '        e.DisplayText = String.Join(", ", list)
    '    End If
    '    If e.Column.FieldName = "From" Then
    '        Dim From = e.Value.ToString()
    '        Dim contact = DemoModel.DataProvider.Contacts.FirstOrDefault(Function(c) c.Email = From)
    '        e.DisplayText = If(contact IsNot Nothing, contact.Name, From)
    '    End If
    'End Sub

    'Protected Sub MailGrid_CustomGroupDisplayText(ByVal sender As Object, ByVal e As ASPxGridViewColumnDisplayTextEventArgs) Handles MailGrid.CustomGroupDisplayText
    '    If e.Column.FieldName = "Subject" Then
    '        e.DisplayText = HttpUtility.HtmlEncode(e.Value)
    '    End If
    'End Sub
    'Protected Sub MailGrid_CustomJSProperties(ByVal sender As Object, ByVal e As ASPxGridViewClientJSPropertiesEventArgs) Handles MailGrid.CustomJSProperties
    '    If MailTree.SelectedNode.Name = "Inbox" Then
    '        Dim list = New List(Of Message)()
    '        For i = 0 To MailGrid.VisibleRowCount - 1
    '            If MailGrid.IsGroupRow(i) Then
    '                Continue For
    '            End If
    '            Dim message = TryCast(MailGrid.GetRow(i), Message)
    '            If message IsNot Nothing Then
    '                list.Add(message)
    '            End If
    '        Next i
    '        e.Properties("cpVisibleMailKeysHash") = GetMessagesKeyMap(list)
    '    End If
    'End Sub



    Protected Sub btnDownloadFormat_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDownloadFormat.Click
        Dim fileName = String.Format("{0}.xlsx", System.Guid.NewGuid.ToString())
        Dim filePath = String.Format("{0}\{1}", Server.MapPath("~/Template"), "ImportFormat.xlsx")

        Response.ClearContent()
        Response.AddHeader("content-disposition", "attachment; filename=" & fileName)
        Response.ContentType = "application/octet-stream"
        Response.WriteFile(filePath)
        Response.End()

    End Sub


    Protected Sub frmImport_Upload_FileUploadComplete(ByVal sender As Object, ByVal e As DevExpress.Web.FileUploadCompleteEventArgs) Handles frmImport_UploadFile.FileUploadComplete

        If btnDownloadFormat.Page Is Nothing AndAlso frmImport_UploadFile.Page IsNot Nothing Then


            Dim _GUID As String = System.Guid.NewGuid().ToString()
            Session("MCGUID") = _GUID

            'Dim FilePath = Page.MapPath("~/UploadFiles/")
            'Dim FileName = String.Format(FilePath & "/{0}.xlsx", _GUID)

            'If Not System.IO.Directory.Exists(FilePath) Then
            '    System.IO.Directory.CreateDirectory(FilePath)
            'End If

            'Dim Extension As String = Path.GetExtension(e.UploadedFile.FileName)
            'e.UploadedFile.SaveAs(FileName)

            '_GUID = GetExcelSheetsXlsX(FileName)

            'e.CallbackData = "success"





            Dim FilePath = Page.MapPath("~/UploadFiles/")
           
            If Not System.IO.Directory.Exists(FilePath) Then
                System.IO.Directory.CreateDirectory(FilePath)
            End If

         

            Using conn As New DataClasses_MotorClaimDataExt()
                Dim _data As New List(Of ClaimUpload_DataObject)

                If e.UploadedFile.FileName.ToLower().IndexOf(".xlsx") > -1 Then

                    Dim FileName = String.Format(FilePath & "/{0}.xlsx", _GUID)
                    Dim Extension As String = Path.GetExtension(e.UploadedFile.FileName)
                    e.UploadedFile.SaveAs(FileName)

                    _data = conn.MotorClaimUploadResult(_GUID, "xlsx")
                    Session("MCTYPE") = "xlsx"




                ElseIf e.UploadedFile.FileName.ToLower().IndexOf(".csv") > -1 Then
                    Dim FileName = String.Format(FilePath & "/{0}.csv", _GUID)
                    Dim Extension As String = Path.GetExtension(e.UploadedFile.FileName)
                    e.UploadedFile.SaveAs(FileName)


                    _data = conn.MotorClaimUploadResult(_GUID, "csv")
                    Session("MCTYPE") = "csv"


                End If

                Dim _result1 = (From c In _data).ToList()
                Dim _result2 = (From c In _data Where c.Status.Equals("Complete")).ToList()
                Dim _result3 = (From c In _data Where c.Status.Equals("Incomplete")).ToList()
                If _result3.Count > 0 Then

                    'All           10 รายการ
                    'Complete      9 รายการ
                    'Incomplete    1 รายการ
                    Dim sb As New StringBuilder

                    sb.AppendLine("Result::")
                    sb.AppendLine(String.Format("- All {0} รายการ", _result1.Count.ToString()))
                    sb.AppendLine(String.Format("- Complete {0} รายการ", _result2.Count.ToString()))
                    sb.AppendLine(String.Format("- Incomplete {0} รายการ", _result3.Count.ToString()))


                    e.CallbackData = sb.ToString()

                Else

                    Dim _Portal_User = MotorClaimModel.DataProvider.Portal_User_Data.AsQueryable()
                    Dim _UWCode = _Portal_User.Where(Function(m) m.UserName.Equals(HttpContext.Current.User.Identity.Name)).FirstOrDefault()


                    Dim ws As New MotorClaimWebService
                    Dim _newdata As New List(Of ClaimTransaction_DataObject)
                    For Each _item In _data
                        '=========================== add new ========================
                        Dim _itemdata As New ClaimTransaction_DataObject
                        With _itemdata
                            .ClaimStatus = ws.ValidateData(_item.ClaimStatus, objType._String)
                            .TempPolicy = ws.ValidateData(_item.TempPolicy, objType._String)
                            .RefNo = ws.ValidateData(_item.RefNo, objType._String)
                            .Version = ws.ValidateData(_item.Version, objType._Integer) 'int
                            .PolicyNo = ws.ValidateData(_item.PolicyNo, objType._String)
                            .PolicyYear = ws.ValidateData(_item.PolicyYear, objType._Integer) 'int
                            .ClaimNo = ws.ValidateData(_item.ClaimNo, objType._String)
                            .TransactionDate = ws.ValidateData(_item.TransactionDate, objType._String)
                            .Unwriter = _UWCode.PasswordQuestion
                            .InsuredName = ws.ValidateData(_item.InsuredName, objType._String)
                            .EffectiveDate = ws.ValidateData(_item.EffectiveDate, objType._String)
                            .ExpiryDate = ws.ValidateData(_item.ExpiryDate, objType._String)
                            .Beneficiary = ws.ValidateData(_item.Beneficiary, objType._String)
                            .CarBrand = ws.ValidateData(_item.CarBrand, objType._String)
                            .CarModel = ws.ValidateData(_item.CarModel, objType._String)
                            .CarLicense = ws.ValidateData(_item.CarLicense, objType._String)
                            .CarYear = ws.ValidateData(_item.CarYear, objType._String)
                            .ChassisNo = ws.ValidateData(_item.ChassisNo, objType._String)
                            .ShowRoomName = ws.ValidateData(_item.ShowRoomName, objType._String)
                            .ShowRoomCode = ws.ValidateData(_item.ShowRoomCode, objType._String)
                            .ClaimNoticeDate = ws.ValidateData(_item.ClaimNoticeDate, objType._String)
                            .ClaimNoticeTime = ws.ValidateData(_item.ClaimNoticeTime, objType._String)
                            .ClaimDetails = ws.ValidateData(_item.ClaimDetails, objType._String)
                            .ClaimType = ws.ValidateData(_item.ClaimType, objType._Integer) 'int
                            .ClaimResult = ws.ValidateData(_item.ClaimResult, objType._Integer) 'int
                            .ClaimDamageDetails = ws.ValidateData(_item.ClaimDamageDetails, objType._String)
                            .CallCenter = ws.ValidateData(_item.CallCenter, objType._String)
                            .AccidentDate = ws.ValidateData(_item.AccidentDate, objType._String)
                            .AccidentTime = ws.ValidateData(_item.AccidentTime, objType._String)
                            .AccidentPlace = ws.ValidateData(_item.AccidentPlace, objType._String)
                            .AccidentTumbon = ws.ValidateData(_item.AccidentTumbon, objType._String)
                            .AccidentAmphur = ws.ValidateData(_item.AccidentAmphur, objType._String)
                            .AccidentProvince = ws.ValidateData(_item.AccidentProvince, objType._String)
                            .AccidentZipcode = ws.ValidateData(_item.AccidentZipcode, objType._String)
                            .DriverName = ws.ValidateData(_item.DriverName, objType._String)
                            .DriverTel = ws.ValidateData(_item.DriverTel, objType._String)
                            .NoticeName = ws.ValidateData(_item.NoticeName, objType._String)
                            .NoticeTel = ws.ValidateData(_item.NoticeTel, objType._String)
                            .GarageType = ws.ValidateData(_item.GarageType, objType._Integer) 'int
                            .GarageCode = ws.ValidateData(_item.GarageCode, objType._String)
                            .GarageName = ws.ValidateData(_item.GarageName, objType._String)
                            .GaragePlace = ws.ValidateData(_item.GaragePlace, objType._String)
                            .GarageTumbon = ws.ValidateData(_item.GarageTumbon, objType._String)
                            .GarageAmphur = ws.ValidateData(_item.GarageAmphur, objType._String)
                            .GarageProvince = ws.ValidateData(_item.GarageProvince, objType._String)
                            .GarageZipcode = ws.ValidateData(_item.GarageZipcode, objType._String)
                            .CarRepairDate = ws.ValidateData(_item.CarRepairDate, objType._String)
                            .CarReceiveDate = ws.ValidateData(_item.CarReceiveDate, objType._String)
                            .ConsentFormNo = ws.ValidateData(_item.ConsentFormNo, objType._String)
                            .PartsDealerName = ws.ValidateData(_item.PartsDealerName, objType._String)
                            .PaymentDetails = ws.ValidateData(_item.PaymentDetails, objType._String)
                            .Amount1 = ws.ValidateData(_item.Amount1, objType._Double) 'float
                            .Amount2 = ws.ValidateData(_item.Amount2, objType._Double) 'float
                            .Amount3 = ws.ValidateData(_item.Amount3, objType._Double) 'float
                            .Remark = ws.ValidateData(_item.Remark, objType._String)
                        End With
                        _newdata.Add(_itemdata)
                    Next

                    Dim _result = ws.GetMotorClaim(_newdata)

                    e.CallbackData = String.Format("success {0} รายการ", _newdata.Count)
                End If
            End Using

















            'ASPxWebControl.RedirectOnCallback(HttpContext.Current.Request.Url.AbsoluteUri)
            'Response.Redirect(HttpContext.Current.Request.Url.AbsoluteUri)

            ''If Extension.ToLower().Equals(".xls") Then
            ''    Dim _GUID = GetExcelSheetsXls(FilePath)
            ''    Session("GUID") = _GUID
            ''Else
            'If Extension.ToLower().Equals(".xlsx") Then
            '    Dim _GUID = GetExcelSheetsXlsX(FilePath)
            '    Session("frmImport_GUID") = _GUID
            'Else
            '    e.CallbackData = "invalid file"
            'End If
            ''End If

            'If Session("frmImport_hasError") = True Then
            '    e.CallbackData = "error"
            'End If




        End If
    End Sub


    'Private Function GetExcelSheetsXlsX(ByVal FilePath As String) As String
    '    Dim _GUID As String = System.Guid.NewGuid().ToString()
    '    Dim _data As New List(Of ClaimTransaction_DataObject)
    '    Dim _ClaimTransaction_Data As New List(Of tblClaimTransaction_Data)
    '    Dim _ClaimResultMessage As New List(Of ClaimResultMessage)


    '    Using ws As New MotorClaimWebService
    '        Using File = New FileStream(FilePath, FileMode.Open, FileAccess.Read)

    '            ''1. Reading from a binary Excel file ('97-2003 format; *.xls)
    '            'Dim excelReader As IExcelDataReader = ExcelReaderFactory.CreateBinaryReader(Stream)
    '            '...
    '            '2. Reading from a OpenXml Excel file (2007 format; *.xlsx)
    '            Dim excelReader As IExcelDataReader = ExcelReaderFactory.CreateOpenXmlReader(File)
    '            '...
    '            '3. DataSet - The result of each spreadsheet will be created in the result.Tables
    '            'Dim result As DataSet = excelReader.AsDataSet()
    '            '...
    '            '4. DataSet - Create column names from first row
    '            excelReader.IsFirstRowAsColumnNames = True
    '            Dim result As DataSet = excelReader.AsDataSet()

    '            Dim _Portal_User = MotorClaimModel.DataProvider.Portal_User_Data.AsQueryable()
    '            Dim _UWCode = _Portal_User.Where(Function(m) m.UserName.Equals(HttpContext.Current.User.Identity.Name)).FirstOrDefault()


    '            For Each irow As DataRow In result.Tables(0).Rows
    '                Dim _ClaimStatus = irow(0)
    '                Dim _TempPolicy = irow(1)
    '                Dim _RefNo = irow(2)
    '                Dim _Version = irow(3)
    '                Dim _PolicyNo = irow(4)
    '                Dim _PolicyYear = irow(5)
    '                Dim _ClaimNo = irow(6)
    '                Dim _TransactionDate = irow(7)
    '                Dim _Unwriter = _UWCode.PasswordQuestion 'HttpContext.Current.User.Identity.Name 'irow(8)
    '                Dim _InsuredName = irow(9)
    '                Dim _EffectiveDate = irow(10)
    '                Dim _ExpiryDate = irow(11)
    '                Dim _Beneficiary = irow(12)
    '                Dim _CarBrand = irow(13)
    '                Dim _CarModel = irow(14)
    '                Dim _CarLicense = irow(15)
    '                Dim _CarYear = irow(16)
    '                Dim _ChassisNo = irow(17)
    '                Dim _ShowRoomName = irow(18)
    '                Dim _ShowRoomCode = irow(19)
    '                Dim _ClaimNoticeDate = irow(20)
    '                Dim _ClaimNoticeTime = irow(21)
    '                Dim _ClaimDetails = irow(22)
    '                Dim _ClaimType = irow(23)
    '                Dim _ClaimResult = irow(24)
    '                Dim _ClaimDamageDetails = irow(25)
    '                Dim _CallCenter = irow(26)
    '                Dim _AccidentDate = irow(27)
    '                Dim _AccidentTime = irow(28)
    '                Dim _AccidentPlace = irow(29)
    '                Dim _AccidentTumbon = irow(30)
    '                Dim _AccidentAmphur = irow(31)
    '                Dim _AccidentProvince = irow(32)
    '                Dim _AccidentZipcode = irow(33)
    '                Dim _DriverName = irow(34)
    '                Dim _DriverTel = irow(35)
    '                Dim _NoticeName = irow(36)
    '                Dim _NoticeTel = irow(37)
    '                Dim _GarageType = irow(38)
    '                Dim _GarageCode = irow(39)
    '                Dim _GarageName = irow(40)
    '                Dim _GaragePlace = irow(41)
    '                Dim _GarageTumbon = irow(42)
    '                Dim _GarageAmphur = irow(43)
    '                Dim _GarageProvince = irow(44)
    '                Dim _GarageZipcode = irow(45)
    '                Dim _CarRepairDate = irow(46)
    '                Dim _CarReceiveDate = irow(47)
    '                Dim _ConsentFormNo = irow(48)
    '                Dim _PartsDealerName = irow(49)
    '                Dim _PaymentDetails = irow(50)
    '                Dim _Amount1 = irow(51)
    '                Dim _Amount2 = irow(52)
    '                Dim _Amount3 = irow(53)
    '                Dim _Remark = irow(54)

    '                '======================= Validate ========================================
    '                Dim _tblClaimData As New tblClaimTransaction_Data()
    '                With _tblClaimData
    '                    .TRNo = _RefNo.ToString()

    '                    '.ClaimStatus = _ClaimStatus.ToString()
    '                    '.TempPolicy = _TempPolicy.ToString()
    '                    '.RefNo = _RefNo.ToString()
    '                    '.Version = _Version.ToString()
    '                    '.PolicyNo = _PolicyNo.ToString()
    '                    '.PolicyYear = _PolicyYear.ToString()
    '                    '.ClaimNo = _ClaimNo.ToString()
    '                    '.TransactionDate = _TransactionDate.ToString()
    '                    '.Unwriter = _Unwriter.ToString()
    '                    '.InsuredName = _InsuredName.ToString()
    '                    '.EffectiveDate = _EffectiveDate.ToString()
    '                    '.ExpiryDate = _ExpiryDate.ToString()
    '                    '.Beneficiary = _Beneficiary.ToString()
    '                    '.CarBrand = _CarBrand.ToString()
    '                    '.CarModel = _CarModel.ToString()
    '                    '.CarLicense = _CarLicense.ToString()
    '                    '.CarYear = _CarYear.ToString()
    '                    '.ChassisNo = _ChassisNo.ToString()
    '                    '.ShowRoomName = _ShowRoomName.ToString()
    '                    '.ShowRoomCode = _ShowRoomCode.ToString()
    '                    '.ClaimNoticeDate = _ClaimNoticeDate.ToString()
    '                    '.ClaimNoticeTime = _ClaimNoticeTime.ToString()
    '                    '.ClaimDetails = _ClaimDetails.ToString()
    '                    '.ClaimType = _ClaimType.ToString()
    '                    '.ClaimResult = _ClaimResult.ToString()
    '                    '.ClaimDamageDetails = _ClaimDamageDetails.ToString()
    '                    '.CallCenter = _CallCenter.ToString()
    '                    '.AccidentDate = _AccidentDate.ToString()
    '                    '.AccidentTime = _AccidentTime.ToString()
    '                    '.AccidentPlace = _AccidentPlace.ToString()
    '                    '.AccidentTumbon = _AccidentTumbon.ToString()
    '                    '.AccidentAmphur = _AccidentAmphur.ToString()
    '                    '.AccidentProvince = _AccidentProvince.ToString()
    '                    '.AccidentZipcode = _AccidentZipcode.ToString()
    '                    '.DriverName = _DriverName.ToString()
    '                    '.DriverTel = _DriverTel.ToString()
    '                    '.NoticeName = _NoticeName.ToString()
    '                    '.NoticeTel = _NoticeTel.ToString()
    '                    '.GarageType = _GarageType.ToString()
    '                    '.GarageCode = _GarageCode.ToString()
    '                    '.GarageName = _GarageName.ToString()
    '                    '.GaragePlace = _GaragePlace.ToString()
    '                    '.GarageTumbon = _GarageTumbon.ToString()
    '                    '.GarageAmphur = _GarageAmphur.ToString()
    '                    '.GarageProvince = _GarageProvince.ToString()
    '                    '.GarageZipcode = _GarageZipcode.ToString()
    '                    '.CarRepairDate = _CarRepairDate.ToString()
    '                    '.CarReceiveDate = _CarReceiveDate.ToString()
    '                    '.ConsentFormNo = _ConsentFormNo.ToString()
    '                    '.PartsDealerName = _PartsDealerName.ToString()
    '                    '.PaymentDetails = _PaymentDetails.ToString()
    '                    '.Amount1 = _Amount1.ToString()
    '                    '.Amount2 = _Amount2.ToString()
    '                    '.Amount3 = _Amount3.ToString()
    '                    '.Remark = _Remark.ToString()


    '                    .ClaimStatus = ws.ValidateData(_ClaimStatus, objType._String)
    '                    .TempPolicy = ws.ValidateData(_TempPolicy, objType._String)
    '                    .RefNo = ws.ValidateData(_RefNo, objType._String)
    '                    .Version = ws.ValidateData(_Version, objType._Integer) 'int
    '                    .PolicyNo = ws.ValidateData(_PolicyNo, objType._String)
    '                    .PolicyYear = ws.ValidateData(_PolicyYear, objType._Integer) 'int
    '                    .ClaimNo = ws.ValidateData(_ClaimNo, objType._String)
    '                    .TransactionDate = ws.ValidateData(_TransactionDate, objType._String)
    '                    .Unwriter = ws.ValidateData(_Unwriter, objType._String)
    '                    .InsuredName = ws.ValidateData(_InsuredName, objType._String)
    '                    .EffectiveDate = ws.ValidateData(_EffectiveDate, objType._String)
    '                    .ExpiryDate = ws.ValidateData(_ExpiryDate, objType._String)
    '                    .Beneficiary = ws.ValidateData(_Beneficiary, objType._String)
    '                    .CarBrand = ws.ValidateData(_CarBrand, objType._String)
    '                    .CarModel = ws.ValidateData(_CarModel, objType._String)
    '                    .CarLicense = ws.ValidateData(_CarLicense, objType._String)
    '                    .CarYear = ws.ValidateData(_CarYear, objType._String)
    '                    .ChassisNo = ws.ValidateData(_ChassisNo, objType._String)
    '                    .ShowRoomName = ws.ValidateData(_ShowRoomName, objType._String)
    '                    .ShowRoomCode = ws.ValidateData(_ShowRoomCode, objType._String)
    '                    .ClaimNoticeDate = ws.ValidateData(_ClaimNoticeDate, objType._String)
    '                    .ClaimNoticeTime = ws.ValidateData(_ClaimNoticeTime, objType._String)
    '                    .ClaimDetails = ws.ValidateData(_ClaimDetails, objType._String)
    '                    .ClaimType = ws.ValidateData(_ClaimType, objType._Integer) 'int
    '                    .ClaimResult = ws.ValidateData(_ClaimResult, objType._Integer) 'int
    '                    .ClaimDamageDetails = ws.ValidateData(_ClaimDamageDetails, objType._String)
    '                    .CallCenter = ws.ValidateData(_CallCenter, objType._String)
    '                    .AccidentDate = ws.ValidateData(_AccidentDate, objType._String)
    '                    .AccidentTime = ws.ValidateData(_AccidentTime, objType._String)
    '                    .AccidentPlace = ws.ValidateData(_AccidentPlace, objType._String)
    '                    .AccidentTumbon = ws.ValidateData(_AccidentTumbon, objType._String)
    '                    .AccidentAmphur = ws.ValidateData(_AccidentAmphur, objType._String)
    '                    .AccidentProvince = ws.ValidateData(_AccidentProvince, objType._String)
    '                    .AccidentZipcode = ws.ValidateData(_AccidentZipcode, objType._String)
    '                    .DriverName = ws.ValidateData(_DriverName, objType._String)
    '                    .DriverTel = ws.ValidateData(_DriverTel, objType._String)
    '                    .NoticeName = ws.ValidateData(_NoticeName, objType._String)
    '                    .NoticeTel = ws.ValidateData(_NoticeTel, objType._String)
    '                    .GarageType = ws.ValidateData(_GarageType, objType._Integer) 'int
    '                    .GarageCode = ws.ValidateData(_GarageCode, objType._String)
    '                    .GarageName = ws.ValidateData(_GarageName, objType._String)
    '                    .GaragePlace = ws.ValidateData(_GaragePlace, objType._String)
    '                    .GarageTumbon = ws.ValidateData(_GarageTumbon, objType._String)
    '                    .GarageAmphur = ws.ValidateData(_GarageAmphur, objType._String)
    '                    .GarageProvince = ws.ValidateData(_GarageProvince, objType._String)
    '                    .GarageZipcode = ws.ValidateData(_GarageZipcode, objType._String)
    '                    .CarRepairDate = ws.ValidateData(_CarRepairDate, objType._String)
    '                    .CarReceiveDate = ws.ValidateData(_CarReceiveDate, objType._String)
    '                    .ConsentFormNo = ws.ValidateData(_ConsentFormNo, objType._String)
    '                    .PartsDealerName = ws.ValidateData(_PartsDealerName, objType._String)
    '                    .PaymentDetails = ws.ValidateData(_PaymentDetails, objType._String)
    '                    .Amount1 = ws.ValidateData(_Amount1, objType._Double) 'float
    '                    .Amount2 = ws.ValidateData(_Amount2, objType._Double) 'float
    '                    .Amount3 = ws.ValidateData(_Amount3, objType._Double) 'float
    '                    .Remark = ws.ValidateData(_Remark, objType._String)

    '                End With

    '                Dim _item_result As New List(Of ClaimResultMessage)
    '                Select Case _tblClaimData.ClaimStatus
    '                    Case "00"
    '                        _item_result = ws.ValidateClaimData00(_tblClaimData)
    '                    Case "01"
    '                        _item_result = ws.ValidateClaimData01(_tblClaimData)
    '                    Case "02"
    '                        _item_result = ws.ValidateClaimData02(_tblClaimData)
    '                    Case "99"
    '                        _item_result = ws.ValidateClaimData99(_tblClaimData)
    '                    Case "98"
    '                        _item_result = ws.ValidateClaimData98(_tblClaimData)
    '                    Case Else 
    '                        _item_result.Add(New ClaimResultMessage With {.ResultCode = "ClaimStatus", .ResultMessage = "Invalid ClaimStatus"})
    '                End Select

    '                If _item_result.Count > 0 Then
    '                     For Each i_rusult In _item_result
    '                        _ClaimResultMessage.Add(New ClaimResultMessage With {.ResultCode = String.Format("RefNo.{0} ({1}) , {2}", _tblClaimData.RefNo, _tblClaimData.ClaimStatus, i_rusult.ResultCode), .ResultMessage = i_rusult.ResultMessage})
    '                    Next
    '                Else
    '                    '=========================== add new ========================
    '                    Dim _item As New ClaimTransaction_DataObject
    '                    With _item
    '                        .ClaimStatus = _ClaimStatus.ToString()
    '                        .TempPolicy = _TempPolicy.ToString()
    '                        .RefNo = _RefNo.ToString()
    '                        .Version = _Version.ToString()
    '                        .PolicyNo = _PolicyNo.ToString()
    '                        .PolicyYear = _PolicyYear.ToString()
    '                        .ClaimNo = _ClaimNo.ToString()
    '                        .TransactionDate = _TransactionDate.ToString()
    '                        .Unwriter = _Unwriter.ToString()
    '                        .InsuredName = _InsuredName.ToString()
    '                        .EffectiveDate = _EffectiveDate.ToString()
    '                        .ExpiryDate = _ExpiryDate.ToString()
    '                        .Beneficiary = _Beneficiary.ToString()
    '                        .CarBrand = _CarBrand.ToString()
    '                        .CarModel = _CarModel.ToString()
    '                        .CarLicense = _CarLicense.ToString()
    '                        .CarYear = _CarYear.ToString()
    '                        .ChassisNo = _ChassisNo.ToString()
    '                        .ShowRoomName = _ShowRoomName.ToString()
    '                        .ShowRoomCode = _ShowRoomCode.ToString()
    '                        .ClaimNoticeDate = _ClaimNoticeDate.ToString()
    '                        .ClaimNoticeTime = _ClaimNoticeTime.ToString()
    '                        .ClaimDetails = _ClaimDetails.ToString()
    '                        .ClaimType = _ClaimType.ToString()
    '                        .ClaimResult = _ClaimResult.ToString()
    '                        .ClaimDamageDetails = _ClaimDamageDetails.ToString()
    '                        .CallCenter = _CallCenter.ToString()
    '                        .AccidentDate = _AccidentDate.ToString()
    '                        .AccidentTime = _AccidentTime.ToString()
    '                        .AccidentPlace = _AccidentPlace.ToString()
    '                        .AccidentTumbon = _AccidentTumbon.ToString()
    '                        .AccidentAmphur = _AccidentAmphur.ToString()
    '                        .AccidentProvince = _AccidentProvince.ToString()
    '                        .AccidentZipcode = _AccidentZipcode.ToString()
    '                        .DriverName = _DriverName.ToString()
    '                        .DriverTel = _DriverTel.ToString()
    '                        .NoticeName = _NoticeName.ToString()
    '                        .NoticeTel = _NoticeTel.ToString()
    '                        .GarageType = _GarageType.ToString()
    '                        .GarageCode = _GarageCode.ToString()
    '                        .GarageName = _GarageName.ToString()
    '                        .GaragePlace = _GaragePlace.ToString()
    '                        .GarageTumbon = _GarageTumbon.ToString()
    '                        .GarageAmphur = _GarageAmphur.ToString()
    '                        .GarageProvince = _GarageProvince.ToString()
    '                        .GarageZipcode = _GarageZipcode.ToString()
    '                        .CarRepairDate = _CarRepairDate.ToString()
    '                        .CarReceiveDate = _CarReceiveDate.ToString()
    '                        .ConsentFormNo = _ConsentFormNo.ToString()
    '                        .PartsDealerName = _PartsDealerName.ToString()
    '                        .PaymentDetails = _PaymentDetails.ToString()
    '                        .Amount1 = _Amount1.ToString()
    '                        .Amount2 = _Amount2.ToString()
    '                        .Amount3 = _Amount3.ToString()
    '                        .Remark = _Remark.ToString()
    '                    End With

    '                    _data.Add(_item)


    '                End If






    '            Next


    '            If _ClaimResultMessage.Count > 0 Then
    '                Dim sb As New StringBuilder
    '                For Each _result In _ClaimResultMessage
    '                    sb.AppendFormat(String.Format("{0} : {1}<br>", _result.ResultCode, _result.ResultMessage))
    '                Next
    '                Throw New Exception(sb.ToString())
    '            Else
    '                If _data.Count > 0 Then
    '                    Dim _result = ws.GetMotorClaim(_data)
    '                Else
    '                    _GUID = ""
    '                End If
    '            End If



    '        End Using

    '    End Using

    '    Return _GUID

    'End Function



    Protected Sub bnExportXLS_Click(sender As Object, e As EventArgs) Handles bnExportXLS.Click
        GridExporter.WriteXlsxToResponse(Guid.NewGuid().ToString(), False, New DevExpress.XtraPrinting.XlsxExportOptionsEx With {.ExportType = DevExpress.Export.ExportType.WYSIWYG})


        'Dim Stream As New System.IO.MemoryStream
        'GridExporter.WriteXlsx(Stream)
        'WriteToResponse(Guid.NewGuid().ToString(), True, "xlsx", Stream)
    End Sub


    'Protected Sub WriteToResponse(ByVal fileName As String, ByVal saveAsFile As Boolean, ByVal fileFormat As String, ByVal stream As MemoryStream)
    '    If Page Is Nothing OrElse Page.Response Is Nothing Then
    '        Return
    '    End If
    '    Dim disposition As String
    '    If saveAsFile Then
    '        disposition = "attachment"
    '    Else
    '        disposition = "inline"
    '    End If
    '    Page.Response.Clear()
    '    Page.Response.Buffer = False
    '    Page.Response.AppendHeader("Content-Type", String.Format("application/{0}", fileFormat))
    '    Page.Response.AppendHeader("Content-Transfer-Encoding", "binary")
    '    Page.Response.AppendHeader("Content-Disposition", String.Format("{0}; filename={1}.{2}", disposition, HttpUtility.UrlEncode(fileName).Replace("+", "%20"), fileFormat))
    '    Page.Response.BinaryWrite(stream.ToArray())
    '    Page.Response.End()
    'End Sub



    Protected Sub frmImport_gvApp_HtmlDataCellPrepared(sender As Object, e As DevExpress.Web.ASPxGridViewTableDataCellEventArgs) Handles ASPxGridViewResult.HtmlDataCellPrepared

        If e.GetValue("Message") IsNot Nothing Then
            If e.DataColumn.FieldName <> "Status" _
           And e.DataColumn.FieldName <> "Message" _
           And e.GetValue("Message").ToString.IndexOf(String.Format("#{0}#", e.DataColumn.FieldName.ToString())) > -1 Then

                e.Cell.BackColor = System.Drawing.Color.Red
                e.Cell.ForeColor = System.Drawing.Color.White

            End If
        End If


    End Sub
    Protected Sub callbackPanel_resultdetails_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase) Handles callbackPanel_resultdetails.Callback
        Using conn As New DataClasses_MotorClaimDataExt()
            Dim _RunNo = e.Parameter

            Dim _data As New List(Of ClaimUpload_DataObject)

            Dim FilePath = Page.MapPath("~/UploadFiles/")
            Dim excel_FileName = String.Format(FilePath & "/{0}.xlsx", Session("MCGUID"))
            Dim csv_FileName = String.Format(FilePath & "/{0}.csv", Session("MCGUID"))
           
            If File.Exists(excel_FileName) Then
                _data = conn.MotorClaimUploadResult(Session("MCGUID"), "xlsx")

            ElseIf File.Exists(csv_FileName) Then
                _data = conn.MotorClaimUploadResult(Session("MCGUID"), "csv")
            End If

            Dim _result = (From c In _data Where c.RunNo.Equals(_RunNo)).FirstOrDefault()

            Dim sb As New StringBuilder

            If Not String.IsNullOrEmpty(_result.Message) Then
                Dim _a = _result.Message.ToString().Split("|")
                For Each _message In _a
                    If Not String.IsNullOrEmpty(_message.Trim) Then
                        Dim _Fields = _message.Replace("[", "").Replace("]", "").Split(",")
                        sb.Append(String.Format("- {0} : {1}<br>", _Fields(0), _Fields(1)))
                    End If
                Next

                litText.Text = "Incomplete <br>" & sb.ToString()

            Else
                litText.Text = "Complete"
            End If



        End Using
    End Sub
End Class
