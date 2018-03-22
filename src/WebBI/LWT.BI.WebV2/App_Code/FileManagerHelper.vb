Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.Web
Imports System.Web.SessionState

Public NotInheritable Class FileManagerHelper
    Private Const Ident As String = "ASPxTreeListFileManagerDemo"
    Private Const KeyMapID As String = Ident & "KM"
    Private Const CleanerTimestampID As String = Ident & "GC"
    Private Const UploadedNameID As String = Ident & "uname"
    Private Const UploadedTmpNameID As String = Ident & "utmp"

    Public Const NameFieldName As String = "Name"
    Public Const FullPathName As String = "_path"

    Private Shared ReadOnly CleanerInterval As Long = TimeSpan.FromMinutes(2).Ticks
    Private Shared ReadOnly UserDataTTL As Long = TimeSpan.FromMinutes(10).Ticks

    Private Sub New()
    End Sub
    Private Shared ReadOnly Property Session() As HttpSessionState
        Get
            Return HttpContext.Current.Session
        End Get
    End Property
    Private Shared ReadOnly Property PathKeyMap() As Dictionary(Of String, Guid)
        Get
            Dim map As Dictionary(Of String, Guid) = TryCast(Session(KeyMapID), Dictionary(Of String, Guid))
            If map Is Nothing Then
                map = New Dictionary(Of String, Guid)()
                Session(KeyMapID) = map
            End If
            Return map
        End Get
    End Property
    Private Shared Property CleanerLastRunTime() As Long
        Get
            If TypeOf Session(CleanerTimestampID) Is Long Then
                Return CLng(Fix(Session(CleanerTimestampID)))
            End If
            Return 0
        End Get
        Set(ByVal value As Long)
            Session(CleanerTimestampID) = value
        End Set
    End Property

    Public Shared ReadOnly Property RootFolder() As String
        Get
            Dim name As String = StorageFolder + Path.DirectorySeparatorChar + Session.SessionID
            If (Not Directory.Exists(name)) Then
                Directory.CreateDirectory(name)
                Directory.CreateDirectory(name & "/My Pictures")
                Directory.CreateDirectory(name & "/My Music")
                Directory.CreateDirectory(name & "/Sandbox")
            End If
            Directory.SetCreationTime(name, DateTime.Now)
            PerformCleanup()
            Return name
        End Get
    End Property
    Private Shared ReadOnly Property StorageFolder() As String
        Get
            Return Path.GetTempPath() + Path.DirectorySeparatorChar & Ident
        End Get
    End Property

    Public Shared Function GetPathKey(ByVal name As String) As Guid
        name = Path.GetFullPath(name)
        If PathKeyMap.ContainsKey(name) Then
            Return PathKeyMap(name)
        End If
        Dim guid As Guid = Guid.NewGuid()
        PathKeyMap.Add(name, guid)
        Return guid
    End Function
    Public Shared Sub MovePath(ByVal oldName As String, ByVal newName As String)
        oldName = Path.GetFullPath(oldName)
        newName = Path.GetFullPath(newName)
        If oldName = newName Then
            Return
        End If
        If Directory.Exists(oldName) Then
            Directory.Move(oldName, newName)
        ElseIf File.Exists(oldName) Then
            File.Move(oldName, newName)
        Else
            Return
        End If
        ' Rename session keys
        Dim itemsToRename As New Dictionary(Of String, Guid)()
        For Each name As String In PathKeyMap.Keys
            If name.StartsWith(oldName) Then
                itemsToRename.Add(name, PathKeyMap(name))
            End If
        Next name
        For Each name As String In itemsToRename.Keys
            PathKeyMap.Remove(name)
        Next name
        For Each name As String In itemsToRename.Keys
            PathKeyMap.Add(newName & name.Substring(oldName.Length), itemsToRename(name))
        Next name
    End Sub
    Public Shared Sub BeginUploadFile(ByVal name As String, ByVal stream As Stream)
        Dim tmpName As String = Path.GetTempFileName()
        Session(UploadedNameID) = Path.GetFileName(name)
        Session(UploadedTmpNameID) = tmpName
        Using myFile As Stream = File.Create(tmpName)
            ' save contents
        End Using
    End Sub
    Public Shared Function EndUploadFile(ByVal destination As String) As String
        Dim fullName As String = Nothing
        If Session(UploadedNameID) IsNot Nothing AndAlso Session(UploadedTmpNameID) IsNot Nothing Then
            fullName = destination & Path.DirectorySeparatorChar + Session(UploadedNameID).ToString()
            Dim tmpName As String = Session(UploadedTmpNameID).ToString()
            Try
                File.Move(tmpName, fullName)
            Catch
                File.Delete(tmpName)
                Throw
            End Try
        End If
        Session(UploadedNameID) = Nothing
        Session(UploadedTmpNameID) = Nothing
        Return fullName
    End Function

    Private Shared Sub PerformCleanup()
        Dim now As Long = DateTime.Now.Ticks
        If now - CleanerLastRunTime < CleanerInterval Then
            Return
        End If
        PerformCleanupCore()
        CleanerLastRunTime = now
    End Sub
    Private Shared Sub PerformCleanupCore()
        Dim now As Long = DateTime.Now.Ticks
        Dim names() As String = Directory.GetDirectories(StorageFolder)
        For Each name As String In names
            If now - Directory.GetCreationTime(name).Ticks < UserDataTTL Then
                Continue For
            End If
            Try
                Directory.Delete(name, True)
            Catch
            End Try
        Next name
    End Sub
End Class
