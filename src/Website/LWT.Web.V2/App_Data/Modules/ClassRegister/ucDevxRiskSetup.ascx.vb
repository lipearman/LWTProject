Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports DevExpress.Web.ASPxTreeList

Partial Class Modules_ucDevxRiskSetup
    Inherits PortalModuleControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
   

        If (Not IsPostBack) Then


        End If
    End Sub
 
 
    Protected Sub cbProject_Callback(ByVal source As Object, ByVal e As CallbackEventArgs) Handles cbProject.Callback
        Dim _param = e.Parameter.ToString().Split("|")
        Session("RiskGroupID") = _param(0).ToString()
        Session("InsuranceLine") = _param(1).ToString()
    End Sub

    Protected Sub cbAddNewCOR_Callback(ByVal sender As Object, ByVal e As CallbackEventArgs) Handles cbAddNewCOR.Callback
        Dim _risk As New List(Of String)
        For Each key In gridCORLookup.GetCurrentPageRowValues("Risk")
            If gridCORLookup.Selection.IsRowSelectedByKey(key) Then 
                _risk.Add(key.ToString())
            End If
        Next key

        If _risk.Count > 0 Then

            Using dc As New DataClasses_CPSExt()

                Dim NewDataList As New List(Of tblClassOfRisk)

                Dim _data = (From c In dc.V_ClassOfRisk_NonPublishes Where _risk.Contains(c.Risk)).ToList()
                For Each item In _data
                    Dim NewData As New tblClassOfRisk
                    With NewData
                        .Risk = item.Risk
                        .RiskGroupID = Convert.ToInt32(Session("RiskGroupID"))
                        .IsActive = True
                    End With

                    NewDataList.Add(NewData)
                Next

                dc.tblClassOfRisks.InsertAllOnSubmit(NewDataList)

                dc.SubmitChanges()
            End Using


            e.Result = "success"
        Else
            e.Result = "Please select Class Of Risk"
        End If



    End Sub

    Protected Sub callbackPanel_MoreInfo_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase) Handles callbackPanel_MoreInfo.Callback


        Dim _Risk As String = e.Parameter.ToString()

        Using dc As New DataClasses_CPSExt()

            Dim _data_cor = (From c In dc.V_ClassOfRisks Where c.Risk.Equals(_Risk)).FirstOrDefault()
            lbRisk.Text = _data_cor.Risk
            With _data_cor
                lbDepartment.Text = .Department
                lbRiskShortDesc.Text = .RiskShortDesc
                lbDescription.Text = .Description
                lbRiskGroupI.Text = .RiskGroupI
                lbRiskGroupII.Text = .RiskGroupII
                lbRiskGovernment.Text = .RiskGovernment
                lbInsuranceLine.Text = .InsuranceLine
                lbStatus.ImageUrl = String.Format("~/images/{0}.gif", .status)
 
            End With
        End Using
        Session("Risk") = _Risk

        gridCommIn.DataBind()
        gridCommOut.DataBind()

    End Sub

 
End Class
