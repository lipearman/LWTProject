
Imports Portal.Components
Imports LWT.Website
Imports System.IO
Imports System.IO.Compression

Partial Class _Default
    Inherits System.Web.UI.Page
    Public sitename As String
    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        'Session("SignInTime") = DateTime.Now()

        'FormsAuthentication.SetAuthCookie("jintarat", RememberMe.Checked)
        'Response.Redirect("~/DesktopDefault.aspx", False)

        'Dim myuser As MbUser = MbUser.GetUserByUsername("lalana")
        'If myuser IsNot Nothing Then
        '    Session("u") = myuser
        'End If

        Response.Redirect("~/SignIn.aspx", False)


        'Response.Write(Zip("ดุสิต ประเสริฐศิลป์"))
        'Response.Write("<br>")
        ''Response.Write(Compress("Dusit Prasertsilp"))
        ''Response.Write(Decompress(Compress("ดุสิต ประเสริฐศิลป์")))
        'Response.End()

    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
        sitename = portalSettings.PortalName

    End Sub


    Protected Sub LoginButton_Click(sender As Object, e As EventArgs)


        Using dc As New DataClasses_PortalDataContextExt()

            Dim _data = (From c In dc.Portal_Users Where c.UserName.Equals(UserName.Text) Select c).FirstOrDefault()
            If _data Is Nothing Then
                Password.Text = ""
                FailureText.Text = "No this user in database"
                Return
            End If


            If Not _data.IsApproved Then
                FailureText.Text = ("User is not approved")
                Return
            End If

            If _data.IsLocked Then
                FailureText.Text = ("User is Locked")
                Return
            End If

            Dim Error_Message As String = ""
            If Portal.Components.Utils.AuthenticateUser(webconfig._DirectoryDomain, UserName.Text, Password.Text, webconfig._DirectoryPath, Error_Message) Then
                Session("SignInTime") = DateTime.Now()

                FormsAuthentication.SetAuthCookie(UserName.Text, RememberMe.Checked)

                'Dim myuser As MbUser = MbUser.GetUserByUsername(UserName.Text)
                'If myuser IsNot Nothing Then
                '    Session("u") = myuser
                '    'Session.Timeout = DateAdd(DateInterval.Hour, 3, DateTime.Now())
                'End If

                Response.Redirect("~/DesktopDefault.aspx", False)
            Else
                Password.Text = ""
                FailureText.Text = (Error_Message)
            End If

        End Using
    End Sub



    'Public Shared Function Compress(text As String) As String
    '    Dim buffer As Byte() = Encoding.UTF8.GetBytes(text)
    '    Dim ms As New MemoryStream()
    '    Using zip As New GZipStream(ms, CompressionMode.Compress, True)
    '        zip.Write(buffer, 0, buffer.Length)
    '    End Using

    '    ms.Position = 0
    '    Dim outStream As New MemoryStream()

    '    Dim compressed As Byte() = New Byte(ms.Length - 1) {}
    '    ms.Read(compressed, 0, compressed.Length)

    '    Dim gzBuffer As Byte() = New Byte(compressed.Length + 3) {}
    '    System.Buffer.BlockCopy(compressed, 0, gzBuffer, 4, compressed.Length)
    '    System.Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gzBuffer, 0, 4)
    '    Return Convert.ToBase64String(gzBuffer)
    'End Function

    'Public Shared Function Decompress(compressedText As String) As String
    '    Dim gzBuffer As Byte() = Convert.FromBase64String(compressedText)
    '    Using ms As New MemoryStream()
    '        Dim msgLength As Integer = BitConverter.ToInt32(gzBuffer, 0)
    '        ms.Write(gzBuffer, 4, gzBuffer.Length - 4)

    '        Dim buffer As Byte() = New Byte(msgLength - 1) {}

    '        ms.Position = 0
    '        Using zip As New GZipStream(ms, CompressionMode.Decompress)
    '            zip.Read(buffer, 0, buffer.Length)
    '        End Using

    '        Return Encoding.UTF8.GetString(buffer)
    '    End Using
    'End Function





    'Public Shared Function Zip(value As String) As String
    '    'Transform string into byte[]
    '    Dim byteArray As Byte() = New Byte(value.Length - 1) {}
    '    Dim indexBA As Integer = 0
    '    For Each item As Char In value.ToCharArray()
    '        byteArray(System.Math.Max(System.Threading.Interlocked.Increment(indexBA), indexBA - 1)) = CByte(AscW(item))
    '    Next

    '    'Prepare for compress
    '    Dim ms As New System.IO.MemoryStream()
    '    Dim sw As New System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionMode.Compress)

    '    'Compress
    '    sw.Write(byteArray, 0, byteArray.Length)
    '    'Close, DO NOT FLUSH cause bytes will go missing...
    '    sw.Close()

    '    'Transform byte[] zip data to string
    '    byteArray = ms.ToArray()
    '    Dim sB As New System.Text.StringBuilder(byteArray.Length)
    '    For Each item As Byte In byteArray
    '        sB.Append(CChar(ChrW(item)))
    '    Next
    '    ms.Close()
    '    sw.Dispose()
    '    ms.Dispose()
    '    Return sB.ToString()
    'End Function




End Class

 
