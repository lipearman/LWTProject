<%@ WebHandler Language="VB" Class="Qaptcha" %>

Imports System
Imports System.Web
Imports System.Web.SessionState
Public Class Qaptcha : Implements IHttpHandler, IReadOnlySessionState
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        context.Response.ContentType = "text/plain"
        Try
         
            Dim qaptcha_key = context.Request.Form("qaptcha_key")
            
            'context.Session("qaptcha_key") = qaptcha_key
            
            context.Session("qaptcha_key") = qaptcha_key
            
            context.Response.Write("{""error"":0}")
            
            
        Catch ex As Exception
            context.Response.Write("{""error"":1}")
        End Try
       
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class