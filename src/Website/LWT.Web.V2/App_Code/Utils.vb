Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Linq
Imports System.Linq.Expressions
Imports System.Threading
Imports System.Web
Imports System.Web.UI
Imports System.Xml.Linq
Imports DevExpress.Web.Internal
Imports DevExpress.Web

Public NotInheritable Class Utils
	Public Const ThomasEmail As String = "thomas.hardy@example.com"
	Private Shared _isSiteMode? As Boolean
	Private Shared _navigationItems As List(Of NavigationItem)
	Private Shared lockObject As Object = New Object()
	Private Shared backgroundThread As Thread

	Private Sub New()
	End Sub
	Private Shared ReadOnly Property Context() As HttpContext
		Get
			Return HttpContext.Current
		End Get
	End Property
	Private Shared ReadOnly Property UploadImagesFolder() As String
		Get
			Return Context.Server.MapPath("~/Content/Photo/UploadImages/")
		End Get
	End Property

	Public Shared ReadOnly Property IsIE7() As Boolean
		Get
			Return RenderUtils.Browser.IsIE AndAlso RenderUtils.Browser.Version < 8
		End Get
	End Property
	Public Shared ReadOnly Property IsSiteMode() As Boolean
		Get
			If (Not _isSiteMode.HasValue) Then
				_isSiteMode = ConfigurationManager.AppSettings("SiteMode").Equals("true", StringComparison.InvariantCultureIgnoreCase)
			End If
			Return _isSiteMode.Value
		End Get
	End Property

	Public Shared Sub ApplyTheme(ByVal page As Page)
		Dim themeName = CurrentTheme
		If String.IsNullOrEmpty(themeName) Then
			themeName = "Default"
		End If
		page.Theme = themeName
	End Sub

	Public Shared ReadOnly Property CurrentTheme() As String
		Get
			Dim themeCookie = Context.Request.Cookies("MailDemoCurrentTheme")
			Return If(themeCookie Is Nothing, "Moderno", HttpUtility.UrlDecode(themeCookie.Value))
		End Get
	End Property

	Public Shared ReadOnly Property IsDarkTheme() As Boolean
		Get
			Dim theme = CurrentTheme
			Return theme Is "Office2010Black" OrElse theme Is "PlasticBlue" OrElse theme Is "RedWine" OrElse theme Is "BlackGlass"
		End Get
	End Property

	Public Shared ReadOnly Property CurrentPageName() As String
		Get
			Dim key = "CE1167E3-A068-4E7C-8BFD-4A7D308BEF43"
			If Context.Items(key) Is Nothing Then
				Context.Items(key) = GetCurrentPageName()
			End If
			Return Context.Items(key).ToString()
		End Get
	End Property

	Public Shared ReadOnly Property NavigationItems() As List(Of NavigationItem)
		Get
			If _navigationItems Is Nothing Then
				_navigationItems = New List(Of NavigationItem)()
				PopuplateNavigationItems(_navigationItems)
			End If
			Return _navigationItems
		End Get
	End Property

	Public Shared Function GetSearchText(ByVal page As Page) As String
		Dim key = "D672659E-FF11-40FF-A63B-FAFB0BFE760B"
		If Context.Items(key) Is Nothing Then
			Dim value As String = Nothing
			If (Not TryGetClientStateValue(Of String)(page, "SearchText", value)) Then
				value = String.Empty
			End If
			Context.Items(key) = value
		End If
		Return Context.Items(key).ToString()
	End Function

	Public Shared Function TryGetClientStateValue(Of T)(ByVal page As Page, ByVal key As String, <System.Runtime.InteropServices.Out()> ByRef result As T) As Boolean
		Dim hiddenField = TryCast(page.Master.Master.FindControl("HiddenField"), ASPxHiddenField)
		If hiddenField Is Nothing OrElse (Not hiddenField.Contains(key)) Then
			result = Nothing
			Return False
		End If
		result = CType(hiddenField(key), T)
		Return True
	End Function

    'Public Shared Function MakeContactsOrderBy(ByVal query As IQueryable(Of Contact), ByVal name As String, ByVal isDesc As Boolean) As IQueryable(Of Contact)
    '	Dim type = GetType(Contact)
    '	Dim [property] = type.GetProperty(name)
    '	Dim parameter = Expression.Parameter(type, "p")
    '	Dim propertyAccess = Expression.MakeMemberAccess(parameter, [property])
    '	Dim orderByExp = Expression.Lambda(propertyAccess, parameter)
    '	Dim resultExp As MethodCallExpression = Expression.Call(GetType(Queryable),If(isDesc, "OrderByDescending", "OrderBy"), New Type() { type, [property].PropertyType }, query.Expression, Expression.Quote(orderByExp))
    '	Return query.Provider.CreateQuery(Of Contact)(resultExp)
    'End Function

    'Public Shared Function GetAddressString(ByVal contact As Contact) As String
    '	Dim list = New List(Of String)()
    '	For Each item In New String() { contact.Address, contact.City, contact.Country }
    '		If (Not String.IsNullOrEmpty(item)) Then
    '			list.Add(item)
    '		End If
    '	Next item
    '	If list.Count = 0 Then
    '		Return String.Empty
    '	End If
    '	Return String.Join(", ", list)
    'End Function
	Public Shared Function GetContactPhotoContentBytes(ByVal savedPath As String) As Byte()
		Dim path = Context.Server.MapPath(String.Format("~/Content/Photo/{0}", savedPath))
		If (Not File.Exists(path)) Then
			Return Nothing
		End If

		Dim byteArray() As Byte = Nothing
		Using stream As New System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read)
			byteArray = New Byte(stream.Length - 1){}
			stream.Read(byteArray, 0, CInt(Fix(stream.Length)))
		End Using
		Return byteArray
	End Function
	Public Shared Function GetContactPhotoUrl(ByVal relativePath As String) As String
		If String.IsNullOrEmpty(relativePath) Then
			Return "Content/Photo/User.png"
		End If
		Return "Content/Photo/" & relativePath
	End Function

	Public Shared Function GetUploadedPhotoUrl(ByVal imageKeyString As String) As String
		Dim imageKey As Guid
		If String.IsNullOrEmpty(imageKeyString) OrElse (Not Guid.TryParse(imageKeyString, imageKey)) Then
			Return ""
		End If
		Return String.Format("UploadImages/{0}.jpg", imageKey)
	End Function

	Public Shared Function SaveContactPhoto(ByVal newPhoto() As Byte) As String
		If newPhoto Is Nothing Then
			Return ""
		End If
		Dim imageKey = Guid.NewGuid()
		Dim filePath = Path.Combine(UploadImagesFolder, imageKey.ToString() & ".jpg")

        'Using stream = New MemoryStream(newPhoto)
        '	Using original = CType(Image.FromStream(stream), Bitmap)
        '	Using thumbnail = CType(ImageUtils.CreateThumbnailImage(original, ImageSizeMode.FillAndCrop, New Size(200, 240)), Bitmap)
        '		ImageUtils.SaveToJpeg(thumbnail, filePath)
        '	End Using
        '	End Using
        'End Using

		Return String.Format("UploadImages/{0}.jpg", imageKey)
	End Function

	Public Shared Sub StartClearExpiredFilesBackgroundThread()
		SyncLock lockObject
			If backgroundThread Is Nothing Then
				backgroundThread = New Thread(AddressOf RemoveTempFilesWorker)
			End If
			If (Not backgroundThread.IsAlive) Then
				backgroundThread.Start(UploadImagesFolder)
			End If
		End SyncLock
	End Sub

	Private Shared Sub RemoveTempFilesWorker(ByVal startParam As Object)
		If startParam Is Nothing Then
			Return
		End If
		Dim directory = startParam.ToString()
		Do
			Thread.Sleep(60000)
			RemoveExpiredTempFiles(directory)
		Loop
	End Sub

	Private Shared Sub RemoveExpiredTempFiles(ByVal directory As String)
		Dim expirationTime = DateTime.UtcNow - New TimeSpan(0, 15, 0)
		Try
			For Each file In New DirectoryInfo(directory).GetFiles("*")
				If file.CreationTimeUtc < expirationTime Then
					Try
						file.Delete()
					Catch
					End Try
				End If
			Next file
		Catch
		End Try
	End Sub

	Private Shared Sub SmoothGraphics(ByVal g As Graphics)
		g.SmoothingMode = SmoothingMode.AntiAlias
		g.InterpolationMode = InterpolationMode.HighQualityBicubic
		g.PixelOffsetMode = PixelOffsetMode.HighQuality
	End Sub

	Private Shared Function GetCurrentPageName() As String
		Dim fileName = Path.GetFileName(Context.Request.Path)
        Dim result = fileName.Substring(0, fileName.Length - 5)

        If result.ToLower() = "mainpage" Then
            result = "mail"
        End If

        If result.ToLower() = "desktopdefault" Then
            result = "mail"
        End If

		If result.ToLower() = "default" Then
			result = "mail"
        End If

		If result.ToLower().Contains("print") Then
			result = "print"
		End If
		Return result.ToLower()
	End Function

	Private Shared Sub PopuplateNavigationItems(ByVal list As List(Of NavigationItem))
		Dim path = Utils.Context.Server.MapPath("~/App_Data/Navigation.xml")
		list.AddRange(XDocument.Load(path).Descendants("Item").Select(Function(n) New NavigationItem() With {.Text = n.Attribute("Text").Value, .NavigationUrl = n.Attribute("NavigateUrl").Value, .SpriteClassName = n.Attribute("SpriteClassName").Value}))
	End Sub
End Class

Public Class NavigationItem
	Public Property Text() As String
	Public Property NavigationUrl() As String
	Public Property SpriteClassName() As String
End Class
