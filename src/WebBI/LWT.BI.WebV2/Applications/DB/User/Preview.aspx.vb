Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Web
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Configuration
Imports DevExpress.DataAccess.ConnectionParameters
Imports DevExpress.DashboardWeb
Imports DevExpress.DashboardCommon
Imports DevExpress.Web

Partial Class applications_DB_Preview
    Inherits System.Web.UI.Page
    Private Shared dataBaseDashboardStorage As New DataBaseEditaleDashboardStorageUser(ConfigurationManager.ConnectionStrings("PortalBIConnection").ConnectionString, "", "")
    Private Shared _GUID As String


    Public Shared DashBoardName As String
    Public Shared DashBoardId As String
    Public Shared DashBoardList As String

    Protected Sub Page_Init(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Init
        'ASPxWebDashboard1.WorkingMode = WorkingMode.Viewer
        'dataBaseDashboardStorage = New DataBaseEditaleDashboardStorageUser(ConfigurationManager.ConnectionStrings("PortalBIConnection").ConnectionString, Session("GUID").ToString(), HttpContext.Current.User.Identity.Name)
        'ASPxWebDashboard1.SetDashboardStorage(dataBaseDashboardStorage)



        'ASPxWebDashboard1.WorkingMode = WorkingMode.Viewer
        'DashboardConfigurator.PassCredentials = True
        ''dataBaseDashboardStorage = New DataBaseEditaleDashboardStorage(ConfigurationManager.ConnectionStrings("PortalBIConnection").ConnectionString, Session("GUID").ToString())
        ''ASPxWebDashboard1.SetDashboardStorage(dataBaseDashboardStorage)


        'dataBaseDashboardStorage = New DataBaseEditaleDashboardStorageUser(ConfigurationManager.ConnectionStrings("PortalBIConnection").ConnectionString, Session("GUID").ToString(), HttpContext.Current.User.Identity.Name)

        'ASPxWebDashboard1.SetDashboardStorage(dataBaseDashboardStorage)
        'ASPxWebDashboard1.SetConnectionStringsProvider(New MyDataSourceWizardConnectionStringsProvider(LWT.Website.webconfig._PortalID))
        'ASPxWebDashboard1.UseDashboardConfigurator = False
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Page.IsPostBack = False Then
            If Session("GUID") Is Nothing Then
                Response.End()
            Else
                Using dc As New DataClasses_PortalBIExt()
                    Dim data = (From c In dc.V_Dashboard_Datas Where c.GUID.Equals(Session("GUID"))).FirstOrDefault()
                    DashBoardName = data.Title
                    DashBoardId = data.ID

                    Dim data_list = (From c In dc.V_Dashboard_Datas Where c.DB_GUID.Equals(data.DB_GUID)).ToList()

                    Dim sb As New StringBuilder()

                    For Each item In data_list
                        sb.AppendFormat("<li><a href=""javascript:void(0);"" onclick=""javascript:cbSelectView.PerformCallback('{0}');"">{1}</a></li>", item.GUID, item.Caption)
                    Next

                    DashBoardList = sb.ToString()
                End Using

            End If
        End If
        ASPxWebDashboard1.DashboardId = DashBoardId
        ASPxWebDashboard1.LoadDefaultDashboard = True

        dataBaseDashboardStorage = New DataBaseEditaleDashboardStorageUser(ConfigurationManager.ConnectionStrings("PortalBIConnection").ConnectionString, Session("GUID").ToString(), HttpContext.Current.User.Identity.Name)
        ASPxWebDashboard1.SetDashboardStorage(dataBaseDashboardStorage)
        ASPxWebDashboard1.SetConnectionStringsProvider(New MyDataSourceWizardConnectionStringsProvider(LWT.Website.webconfig._PortalID))
        ASPxWebDashboard1.UseDashboardConfigurator = False
        ASPxWebDashboard1.WorkingMode = WorkingMode.Viewer
        DashboardConfigurator.PassCredentials = True

    End Sub

    'Protected Sub ASPxDashboardViewer1_ConfigureDataConnection(sender As Object, e As DevExpress.DashboardWeb.ConfigureDataConnectionWebEventArgs) Handles ASPxWebDashboard1.ConfigureDataConnection
    '    Dim parameters As OlapConnectionParameters = DirectCast(e.ConnectionParameters, OlapConnectionParameters)
    '    If parameters IsNot Nothing Then
    '        If ASPxWebDashboard1.DashboardId IsNot Nothing Then
    '            Using dc As New DataClasses_PortalBIExt()
    '                Dim DashboardId = ASPxWebDashboard1.DashboardId

    '                Dim _data = (From c In dc.V_Dashboard_Datas Where c.ID = DashboardId).FirstOrDefault()

    '                Dim _conn = _data.CONNECTING

    '                DashboardConfigurator.PassCredentials = True

    '                Dim obj As New OlapConnectionParameters With {.ConnectionString = _conn}

    '                e.ConnectionParameters = obj

    '            End Using
    '        End If
    '    End If
    'End Sub



    Protected Sub cbSelectView_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbSelectView.Callback
        Dim _GUID = e.Parameter
        Session("GUID") = _GUID
        e.Result = _GUID

        ASPxWebControl.RedirectOnCallback("preview.aspx")

    End Sub
End Class
