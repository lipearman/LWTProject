Imports Microsoft.VisualBasic


Public Class CustomSiteMapProvider
    Inherits StaticSiteMapProvider
#Region "Members"

    Private ReadOnly _siteMapLock As New Object()
    Private _siteMapRoot As SiteMapNode
    'Private _PageId As String
#End Region

#Region "Methods"

    Public Overrides Function BuildSiteMap() As SiteMapNode
        ' Use a lock to provide thread safety
        SyncLock _siteMapLock
            If _siteMapRoot IsNot Nothing Then
                Return _siteMapRoot
            End If

            MyBase.Clear()

            CreateSiteMapRoot()
            CreateSiteMapNodes()

            Return _siteMapRoot
        End SyncLock
    End Function

    Protected Overrides Function GetRootNodeCore() As SiteMapNode
        Return BuildSiteMap()
    End Function

    Private Sub CreateSiteMapRoot()
        _siteMapRoot = New SiteMapNode(Me, "Root", "~/Default.aspx", "Root")
        AddNode(_siteMapRoot)
    End Sub

    Private Sub CreateSiteMapNodes()
        Dim node As SiteMapNode = Nothing
        For i As Integer = 1 To 3
            node = New SiteMapNode(Me, String.Format("Child{0}", i), String.Format("~/WebForm{0}.aspx", i), String.Format("Child{0}", i))

            AddNode(node, _siteMapRoot)
        Next
    End Sub

#End Region
End Class


