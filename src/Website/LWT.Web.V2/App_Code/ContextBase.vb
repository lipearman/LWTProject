Imports Microsoft.VisualBasic
Imports System.Configuration
Imports System.Data.Entity
Imports System.Data.Entity.Infrastructure
Imports DevExpress.Internal

Partial Public Class ContextBase
    Inherits DbContext

    Public Sub New(ByVal connectionString As String)
        MyBase.New(GetPathedConnectionString(connectionString))
    End Sub

    Private Shared Function GetPathedConnectionString(ByVal connectionString As String) As String
        Dim sqlExpressString As String = ConfigurationManager.ConnectionStrings(connectionString).ConnectionString
        Return sqlExpressString 'DbEngineDetector.PatchConnectionString(sqlExpressString)
    End Function
End Class