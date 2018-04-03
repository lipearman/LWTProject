
Imports Portal.Components
Imports LWT.Website
Imports System.IO
Imports System.IO.Compression
Imports DevExpress.Web
Imports System.Data.SqlClient
Imports Microsoft.AnalysisServices.AdomdClient

Partial Class _Default
    Inherits System.Web.UI.Page
    Public sitename As String


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim portalSettings As PortalSettings = CType(HttpContext.Current.Items(webconfig._PortalContextName), PortalSettings)
        sitename = portalSettings.PortalName


        'FormsAuthentication.SetAuthCookie("apichart", True)
        'Response.Redirect(String.Format("~/DesktopDefault.aspx?pageid={0}", webconfig._DefaultPageID), False)

        'Using objConnection As New AdomdConnection("data source=http://172.16.40.234/OLAP/msmdpump.dll;initial catalog=LWTProduction;User Id=lockthbnk-db07\biuser;Password=Password123")
        '    Using objCommand As New AdomdCommand()
        '        'Dim objDatatable As New DataTable
        '        Dim strCommand As New StringBuilder()
        '        strCommand.AppendLine("CREATE GLOBAL CUBE [APDProductionBudgetCube]")
        '        strCommand.AppendLine("Storage 'c:\temp\APDProductionBudgetCube.cub'")
        '        strCommand.AppendLine("FROM [APDProductionBudgetCube]")
        '        strCommand.AppendLine("(")
        '        strCommand.AppendLine(" MEASURE [APDProductionBudgetCube].[Actual Amount]")
        '        strCommand.AppendLine(",MEASURE [APDProductionBudgetCube].[Admin Fee In1THB]")
        '        strCommand.AppendLine(",MEASURE [APDProductionBudgetCube].[Admin Fee In2THB]")
        '        strCommand.AppendLine(",MEASURE [APDProductionBudgetCube].[Admin Fee Out1THB]")
        '        strCommand.AppendLine(",MEASURE [APDProductionBudgetCube].[Admin Fee Out2THB]")
        '        strCommand.AppendLine(",MEASURE [APDProductionBudgetCube].[Brokerage THB]")
        '        strCommand.AppendLine(",MEASURE [APDProductionBudgetCube].[Budget Amount]")
        '        strCommand.AppendLine(",MEASURE [APDProductionBudgetCube].[CN Count]")
        '        strCommand.AppendLine(",MEASURE [APDProductionBudgetCube].[Commission Out THB]")
        '        strCommand.AppendLine(",MEASURE [APDProductionBudgetCube].[Discount THB]")
        '        strCommand.AppendLine(",MEASURE [APDProductionBudgetCube].[DN Count]")
        '        strCommand.AppendLine(",MEASURE [APDProductionBudgetCube].[Extra Amount]")
        '        strCommand.AppendLine(",MEASURE [APDProductionBudgetCube].[Net Income]")
        '        strCommand.AppendLine(",MEASURE [APDProductionBudgetCube].[OR In THB]")
        '        strCommand.AppendLine(",MEASURE [APDProductionBudgetCube].[OR Out THB]")
        '        strCommand.AppendLine(",MEASURE [APDProductionBudgetCube].[Premium THB]")
        '        strCommand.AppendLine(",MEASURE [APDProductionBudgetCube].[Production Count]")
        '        strCommand.AppendLine(",MEASURE [APDProductionBudgetCube].[Stamp THB]")
        '        strCommand.AppendLine(",MEASURE [APDProductionBudgetCube].[Sum Insured THB]")
        '        strCommand.AppendLine(",MEASURE [APDProductionBudgetCube].[Total Income]")
        '        strCommand.AppendLine(",MEASURE [APDProductionBudgetCube].[Total Premium THB]")
        '        strCommand.AppendLine(",MEASURE [APDProductionBudgetCube].[VATTHB]")
        '        strCommand.AppendLine(",DIMENSION [Dim APD Budget].[AE Group Name]")
        '        strCommand.AppendLine(",DIMENSION [Dim APD Budget].[AE Group Running]")
        '        strCommand.AppendLine(",DIMENSION [Dim APD Budget].[AE Name]")
        '        strCommand.AppendLine(",DIMENSION [Dim APD Budget].[AE Production]")
        '        strCommand.AppendLine(",DIMENSION [Dim APD Budget].[AES Group Name]")
        '        strCommand.AppendLine(",DIMENSION [Dim APD Budget].[AES Group Running] ")
        '        strCommand.AppendLine(",DIMENSION [Dim APD Budget].[Budget Key] ")
        '        strCommand.AppendLine(",DIMENSION [Dim APD Budget].[Business Group] ")
        '        strCommand.AppendLine(",DIMENSION [Dim APD Budget].[Class Group] ")
        '        strCommand.AppendLine(",DIMENSION [Dim APD Budget].[Division]")
        '        strCommand.AppendLine(",DIMENSION [Dim APD Budget].[Fiscal Eng Month] ")
        '        strCommand.AppendLine(",DIMENSION [Dim APD Budget].[Fiscal Period]")
        '        strCommand.AppendLine(",DIMENSION [Dim APD Budget].[Fiscal Thai Month] ")
        '        strCommand.AppendLine(",DIMENSION [Dim APD Budget].[Fiscal Year] ")
        '        strCommand.AppendLine(",DIMENSION [Dim APD Budget].[Owner]")
        '        strCommand.AppendLine(");")


        'strCommand.AppendLine("CREATE GLOBAL CUBE [LWTProductionCube]  ")
        'strCommand.AppendLine("Storage 'c:\temp\LWTProductionCube.cub'  ")
        'strCommand.AppendLine("FROM [LWTProductionCube]  ")
        'strCommand.AppendLine("(  ")
        'strCommand.AppendLine("MEASURE [LWTProductionCube].[Premium THB]")
        'strCommand.AppendLine(",MEASURE [LWTProductionCube].[Stamp THB]")
        'strCommand.AppendLine(",MEASURE [LWTProductionCube].[VATTHB]")
        'strCommand.AppendLine(",MEASURE [LWTProductionCube].[Total Premium THB]")
        'strCommand.AppendLine(",MEASURE [LWTProductionCube].[Total Income]")
        'strCommand.AppendLine(",MEASURE [LWTProductionCube].[Net Income]")
        'strCommand.AppendLine(",MEASURE [LWTProductionCube].[Brokerage THB]")
        'strCommand.AppendLine(",MEASURE [LWTProductionCube].[OR In THB]")
        'strCommand.AppendLine(",MEASURE [LWTProductionCube].[Admin Fee In1THB]")
        'strCommand.AppendLine(",MEASURE [LWTProductionCube].[Admin Fee In2THB]")
        'strCommand.AppendLine(",MEASURE [LWTProductionCube].[Discount THB]")
        'strCommand.AppendLine(",MEASURE [LWTProductionCube].[Commission Out THB]")
        'strCommand.AppendLine(",MEASURE [LWTProductionCube].[OR Out THB]")
        'strCommand.AppendLine(",MEASURE [LWTProductionCube].[Admin Fee Out1THB]")
        'strCommand.AppendLine(",MEASURE [LWTProductionCube].[Admin Fee Out2THB]")
        'strCommand.AppendLine(",MEASURE [LWTProductionCube].[Extra Amount]")
        'strCommand.AppendLine(",MEASURE [LWTProductionCube].[Production Fact Count]")
        'strCommand.AppendLine(",MEASURE [LWTProductionCube].[Client Distinct Count]")
        'strCommand.AppendLine(",DIMENSION [Dim AE].[AE Group Name]")
        'strCommand.AppendLine(",DIMENSION [Dim AE].[AE Group Running]")
        'strCommand.AppendLine(",DIMENSION [Dim AE].[AE Name]")
        'strCommand.AppendLine(",DIMENSION [Dim AE].[AES Group Name]")
        'strCommand.AppendLine(",DIMENSION [Dim AE].[AES Group Running]")
        'strCommand.AppendLine(",DIMENSION [Dim AE].[Business Group]")
        'strCommand.AppendLine(",DIMENSION [Dim AE].[Class Group]")
        'strCommand.AppendLine(",DIMENSION [Dim AE].[Owner]")
        'strCommand.AppendLine(",DIMENSION [Dim Agent Staff].[Staff Name]")
        'strCommand.AppendLine(",DIMENSION [Dim Class Of Risk].[Department]")
        'strCommand.AppendLine(",DIMENSION [Dim Class Of Risk].[Risk Description]")
        'strCommand.AppendLine(",DIMENSION [Dim Class Of Risk].[Risk Government]")
        'strCommand.AppendLine(",DIMENSION [Dim Class Of Risk].[Risk Group I]")
        'strCommand.AppendLine(",DIMENSION [Dim Class Of Risk].[Risk Group II]")
        'strCommand.AppendLine(",DIMENSION [Dim Class Of Risk].[Risk Short Desc]")
        'strCommand.AppendLine(",DIMENSION [Dim Clients].[Client Group]")
        'strCommand.AppendLine(",DIMENSION [Dim Clients].[Client Name]")
        'strCommand.AppendLine(",DIMENSION [Dim Clients].[Client Type]")
        'strCommand.AppendLine(",DIMENSION [Dim Clients].[Industry Eng]")
        'strCommand.AppendLine(",DIMENSION [Dim Clients].[Industry Thai]")
        'strCommand.AppendLine(",DIMENSION [Dim Clients].[Source Code]")
        'strCommand.AppendLine(",DIMENSION [Dim Clients].[Source Name]")
        'strCommand.AppendLine(",DIMENSION [Dim Date].[Calendar Period]")
        'strCommand.AppendLine(",DIMENSION [Dim Date].[Calendar Year]")
        'strCommand.AppendLine(",DIMENSION [Dim Date].[Eng Month Name]")
        'strCommand.AppendLine(",DIMENSION [Dim Date].[Fiscal Eng Month]")
        'strCommand.AppendLine(",DIMENSION [Dim Date].[Fiscal Period]")
        'strCommand.AppendLine(",DIMENSION [Dim Date].[Fiscal Thai Month]")
        'strCommand.AppendLine(",DIMENSION [Dim Date].[Fiscal Year]")
        'strCommand.AppendLine(",DIMENSION [Dim Date].[Full Date Alternate Key]")
        'strCommand.AppendLine(",DIMENSION [Dim Date].[Thai Day Nameof Week]")
        'strCommand.AppendLine(",DIMENSION [Dim Date].[Thai Month Name]")
        'strCommand.AppendLine(",DIMENSION [Dim Underwriter].[UW Cross Ref]")
        'strCommand.AppendLine(",DIMENSION [Dim Underwriter].[UW Name]")
        'strCommand.AppendLine(",DIMENSION [Dim Underwriter].[UW Name Eng]")
        'strCommand.AppendLine(",DIMENSION [Production Fact].[Agent Staff]")
        'strCommand.AppendLine(",DIMENSION [Production Fact].[Billing Day]")
        'strCommand.AppendLine(",DIMENSION [Production Fact].[Effective Day]")
        'strCommand.AppendLine(",DIMENSION [Production Fact].[Effective Month]")
        'strCommand.AppendLine(",DIMENSION [Production Fact].[Effective Year]")
        'strCommand.AppendLine(",DIMENSION [Production Fact].[Error Flag]")
        'strCommand.AppendLine(",DIMENSION [Production Fact].[Expired Month]")
        'strCommand.AppendLine(",DIMENSION [Production Fact].[Expired Year]")
        'strCommand.AppendLine(",DIMENSION [Production Fact].[Renew Flag]")
        'strCommand.AppendLine(",DIMENSION [Production Fact].[Trans Type]")
        'strCommand.AppendLine(",DIMENSION [Production Fact].[Upfront Flag]         ")
        'strCommand.AppendLine(") ")





        '        objConnection.Open()

        '        objCommand.Connection = objConnection
        '        objCommand.CommandText = strCommand.ToString()
        '        objCommand.Execute()

        '        objConnection.Close()
        '    End Using
        'End Using

    End Sub


    Protected Sub cbLogin_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbLogin.Callback
        Dim UserName = txtUserName.Text
        Dim Password = txtPassword.Text



        Using dc As New DataClasses_PortalDataContextExt()

            Dim _data = (From c In dc.Portal_Users Where c.UserName.Equals(UserName) Select c).FirstOrDefault()
            If _data Is Nothing Then
                e.Result = "No this user in System"
                Return
            End If


            If Not _data.IsApproved Then
                e.Result = ("User is not approved")
                Return
            End If

            If _data.IsLocked Then
                e.Result = ("User is Locked")
                Return
            End If

            Dim Error_Message As String = ""


            Dim _ws As New UACWebServices.WebService()

            _ws.Url = "http://172.16.40.234//LWTServices/WebService.asmx"

            If _ws.AuthenticateUser(webconfig._DirectoryDomain, UserName, Password, webconfig._DirectoryPath, Error_Message) Then

                _data.LastLoginDate = Now()

                dc.SubmitChanges()

                dc.ExecuteCommand("insert into tblLogin_Log(UserName,IP,CREATEDATE) Values({0},{1},getdate())", UserName, Context.Request.UserHostAddress)

                Session("PageID") = ConfigurationSettings.AppSettings("DefaultLWTPageID")
                Session("SignInTime") = DateTime.Now()

                FormsAuthentication.SetAuthCookie(UserName, True)

                Dim _defaultPage = (From c In dc.Portal_Users_DefaultPages Where c.PortalId.Equals(webconfig._PortalID) And c.UserId = _data.UserID).FirstOrDefault()
                If _defaultPage IsNot Nothing Then
                    Dim defaulttab = (From c In dc.v_DesktopTabs Where c.TabId = _defaultPage.TabId And c.PortalId.Equals(webconfig._PortalID)).FirstOrDefault()
                    ASPxWebControl.RedirectOnCallback(String.Format("~/DesktopDefault.aspx?pageid={0}", defaulttab.PageId))
                Else
                    ASPxWebControl.RedirectOnCallback(String.Format("~/DesktopDefault.aspx?pageid={0}", webconfig._DefaultPageID))
                End If
            Else
                e.Result = Error_Message
            End If


        End Using



    End Sub

End Class


