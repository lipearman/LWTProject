<%@ Application Language="VB" %>
<%@ Import Namespace="LWT.Portal.Web" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Web.SessionState" %>
<%@ Import Namespace="System.Security.Principal" %>
<%@ Import Namespace="Portal.Components" %>
<%@ Import Namespace="System.Diagnostics" %>
<%@ Import Namespace="System.Web.Security" %>
<%@ Import Namespace="System.Threading" %>
<%@ Import Namespace="System.Globalization" %>
<%@ Import Namespace="System.Web.Mail" %>
<%@ Import Namespace="System.Text" %>
<script RunAt="server">
    Protected Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
 
        Dim _PageID As String = ""
        If Not (Request.Params("pageid") Is Nothing) Then
            _PageID = Request.Params("pageid")
        End If
        
        ' Build and add the PortalSettings object to the current Context 
        Context.Items.Add(webconfig._PortalContextName, New Portal.Components.PortalSettings(_PageID))
         
    End Sub
    
    Private Sub Application_AcquireRequestState(ByVal sender As Object, ByVal e As EventArgs) '(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.AcquireRequestState
 
    End Sub
    
    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application startup
    End Sub
    
    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs on application shutdown
    End Sub
        
    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when an unhandled error occurs
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a new session is started
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Code that runs when a session ends. 
        ' Note: The Session_End event is raised only when the sessionstate mode
        ' is set to InProc in the Web.config file. If session mode is set to StateServer 
        ' or SQLServer, the event is not raised.
    End Sub
    Public Shared Function GetApplicationPath(ByVal request As HttpRequest) As String
        Dim path As String = String.Empty
        Try
            If request.ApplicationPath <> "/" Then
                path = request.ApplicationPath
            End If
        Catch e As Exception
            Throw e
        End Try

        Return path
    End Function
</script>
