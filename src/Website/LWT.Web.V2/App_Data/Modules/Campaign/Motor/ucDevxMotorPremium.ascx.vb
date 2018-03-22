Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports BaseConfig 
Imports DevExpress.Web
Imports DevExpress.Web.Data
Imports DevExpress.Web.ASPxTreeList
Imports System.IO

Partial Class Modules_ucDevxMotorPremium
    Inherits PortalModuleControl
    Private RiskGroupID As Integer = 3

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        'SqlDataSource_MotorCampaign.SelectParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name
        SqlDataSource_MotorCampaign.SelectParameters("RiskGroupID").DefaultValue = RiskGroupID

        'SqlDataSource_MotorCampaign.InsertParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name
        'SqlDataSource_MotorCampaign.InsertParameters("RiskGroupID").DefaultValue = RiskGroupID

        'SqlDataSource_MotorCampaign.UpdateParameters("RiskGroupID").DefaultValue = RiskGroupID
        'SqlDataSource_MotorCampaign.UpdateParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name

        SqlDataSource_MotorCoverage.SelectParameters("RiskGroupID").DefaultValue = RiskGroupID

        If (Not IsPostBack) Then


        End If
    End Sub

    Protected Sub cbCampaign_Callback(ByVal source As Object, ByVal e As CallbackEventArgs) Handles cbCampaign.Callback
        Session("CampaignID") = e.Parameter
    End Sub

    Protected Sub Model_Callback(ByVal source As Object, ByVal e As CallbackEventArgsBase)

        SqlDataSource_CarModel.SelectCommand = "SELECT ModelName as Name FROM V_CarBrandModel where  BrandName=@BrandName  order by ModelName "
        SqlDataSource_CarModel.SelectParameters.Clear()
        SqlDataSource_CarModel.SelectParameters.Add("BrandName", TypeCode.String, e.Parameter)


        Dim Model = DirectCast(source, ASPxComboBox)
        Model.DataSource = SqlDataSource_CarModel
        Model.DataBind()
         
    End Sub

    Protected Sub newModel_Callback(ByVal source As Object, ByVal e As CallbackEventArgsBase)
        If (String.IsNullOrEmpty(e.Parameter)) Then Return

        newBrand.DataBind()

        SqlDataSource_CarModel.SelectCommand = "SELECT ModelName as Name FROM V_CarBrandModel where  BrandName=@BrandName  order by ModelName "
        SqlDataSource_CarModel.SelectParameters.Clear()
        SqlDataSource_CarModel.SelectParameters.Add("BrandName", TypeCode.String, e.Parameter)


        Dim listBoxNewModel = DirectCast(source, ASPxListBox)
        listBoxNewModel.DataSource = SqlDataSource_CarModel
        listBoxNewModel.DataBind()

        listBoxNewModel.Items.Insert(0, New ListEditItem("(Select all)"))


    End Sub

    Protected Sub gridCarPremium_InitNewRow(ByVal sender As Object, ByVal e As DevExpress.Web.Data.ASPxDataInitNewRowEventArgs) Handles gridCarPremium.InitNewRow
        e.NewValues("IsActive") = True


        e.NewValues("Brand") = "All"


    End Sub


    'Protected Sub gridCarPremium_StartRowEditing(ByVal sender As Object, ByVal e As ASPxStartRowEditingEventArgs) Handles gridCarPremium.StartRowEditing

    '    Dim _gridCarPremium = DirectCast(sender, ASPxGridView)
    '    Dim _formPremium As ASPxFormLayout = TryCast(_gridCarPremium.FindEditFormTemplateControl("formPremium"), ASPxFormLayout)

    '    Dim Brand As ASPxComboBox = TryCast(_formPremium.FindControl("Brand"), ASPxComboBox)
    '    Dim Model As ASPxComboBox = TryCast(_formPremium.FindControl("Model"), ASPxComboBox)

    '    SqlDataSource_CarModel.SelectCommand = "SELECT ModelName as Name FROM V_CarBrandModel where  BrandName=@BrandName  order by ModelName "
    '    SqlDataSource_CarModel.SelectParameters.Clear()
    '    SqlDataSource_CarModel.SelectParameters.Add("BrandName", TypeCode.String, Brand.Value)


    '    Model.DataSource = SqlDataSource_CarModel
    '    Model.DataBind()
    'End Sub
   


    Protected Sub gridCarPremium_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles gridCarPremium.RowInserting
        Dim _gridCarPremium = DirectCast(sender, ASPxGridView)
        Dim _formPremium As ASPxFormLayout = TryCast(_gridCarPremium.FindEditFormTemplateControl("formPremium"), ASPxFormLayout)

        Dim Underwriter As ASPxComboBox = TryCast(_formPremium.FindControl("Underwriter"), ASPxComboBox)
        Dim CoverageID As ASPxGridLookup = TryCast(_formPremium.FindControl("CoverageID"), ASPxGridLookup)
        Dim Brand As ASPxComboBox = TryCast(_formPremium.FindControl("Brand"), ASPxComboBox)
        Dim Model As ASPxComboBox = TryCast(_formPremium.FindControl("Model"), ASPxComboBox)
        Dim Name As ASPxTextBox = TryCast(_formPremium.FindControl("Name"), ASPxTextBox)
        Dim ModelCode As ASPxTextBox = TryCast(_formPremium.FindControl("ModelCode"), ASPxTextBox)
        Dim CarPrice As ASPxSpinEdit = TryCast(_formPremium.FindControl("CarPrice"), ASPxSpinEdit)
        Dim CarAge As ASPxSpinEdit = TryCast(_formPremium.FindControl("CarAge"), ASPxSpinEdit)
        Dim SumInsuredFrom As ASPxSpinEdit = TryCast(_formPremium.FindControl("SumInsuredFrom"), ASPxSpinEdit)
        Dim SumInsuredTo As ASPxSpinEdit = TryCast(_formPremium.FindControl("SumInsuredTo"), ASPxSpinEdit)
        Dim NetPremium As ASPxSpinEdit = TryCast(_formPremium.FindControl("NetPremium"), ASPxSpinEdit)
        Dim Stamp As ASPxSpinEdit = TryCast(_formPremium.FindControl("Stamp"), ASPxSpinEdit)
        Dim Vat As ASPxSpinEdit = TryCast(_formPremium.FindControl("Vat"), ASPxSpinEdit)
        Dim GrossPremium As ASPxSpinEdit = TryCast(_formPremium.FindControl("GrossPremium"), ASPxSpinEdit)
        Dim Discount As ASPxSpinEdit = TryCast(_formPremium.FindControl("Discount"), ASPxSpinEdit)
        Dim TotalPremium As ASPxSpinEdit = TryCast(_formPremium.FindControl("TotalPremium"), ASPxSpinEdit)
        Dim SinglePrice As ASPxSpinEdit = TryCast(_formPremium.FindControl("SinglePrice"), ASPxSpinEdit)
        Dim WHT_Rate1 As ASPxTextBox = TryCast(_formPremium.FindControl("WHT_Rate1"), ASPxTextBox)
        Dim WHT_Rate2 As ASPxTextBox = TryCast(_formPremium.FindControl("WHT_Rate2"), ASPxTextBox)
        Dim WHT_Rate3 As ASPxTextBox = TryCast(_formPremium.FindControl("WHT_Rate3"), ASPxTextBox)
        Dim IsActive As ASPxCheckBox = TryCast(_formPremium.FindControl("IsActive"), ASPxCheckBox)




        e.NewValues("Underwriter") = Underwriter.Value
        e.NewValues("CoverageID") = CoverageID.Value
        e.NewValues("Brand") = Brand.Value
        e.NewValues("Model") = Model.Value
        e.NewValues("Name") = Name.Value
        e.NewValues("ModelCode") = ModelCode.Value
        e.NewValues("CarPrice") = CarPrice.Value
        e.NewValues("CarAge") = CarAge.Value
        e.NewValues("SumInsuredFrom") = SumInsuredFrom.Value
        e.NewValues("SumInsuredTo") = SumInsuredTo.Value
        e.NewValues("NetPremium") = NetPremium.Value
        e.NewValues("Stamp") = Stamp.Value
        e.NewValues("Vat") = Vat.Value
        e.NewValues("GrossPremium") = GrossPremium.Value
        e.NewValues("Discount") = Discount.Value
        e.NewValues("TotalPremium") = TotalPremium.Value
        e.NewValues("SinglePrice") = SinglePrice.Value
        e.NewValues("WHT_Rate1") = WHT_Rate1.Value
        e.NewValues("WHT_Rate2") = WHT_Rate2.Value
        e.NewValues("WHT_Rate3") = WHT_Rate3.Value
        e.NewValues("CampaignID") = Session("CampaignID")
        e.NewValues("CreateBy") = HttpContext.Current.User.Identity.Name
        e.NewValues("IsActive") = IsActive.Checked
    End Sub


    Protected Sub gridCarPremium_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles gridCarPremium.RowUpdating
        Dim _gridCarPremium = DirectCast(sender, ASPxGridView)
        Dim _formPremium As ASPxFormLayout = TryCast(_gridCarPremium.FindEditFormTemplateControl("formPremium"), ASPxFormLayout)

        Dim Underwriter As ASPxComboBox = TryCast(_formPremium.FindControl("Underwriter"), ASPxComboBox)
        Dim CoverageID As ASPxGridLookup = TryCast(_formPremium.FindControl("CoverageID"), ASPxGridLookup)
        Dim Brand As ASPxComboBox = TryCast(_formPremium.FindControl("Brand"), ASPxComboBox)
        Dim Model As ASPxComboBox = TryCast(_formPremium.FindControl("Model"), ASPxComboBox)
        Dim Name As ASPxTextBox = TryCast(_formPremium.FindControl("Name"), ASPxTextBox)
        Dim ModelCode As ASPxTextBox = TryCast(_formPremium.FindControl("ModelCode"), ASPxTextBox)
        Dim CarPrice As ASPxSpinEdit = TryCast(_formPremium.FindControl("CarPrice"), ASPxSpinEdit)
        Dim CarAge As ASPxSpinEdit = TryCast(_formPremium.FindControl("CarAge"), ASPxSpinEdit)
        Dim SumInsuredFrom As ASPxSpinEdit = TryCast(_formPremium.FindControl("SumInsuredFrom"), ASPxSpinEdit)
        Dim SumInsuredTo As ASPxSpinEdit = TryCast(_formPremium.FindControl("SumInsuredTo"), ASPxSpinEdit)
        Dim NetPremium As ASPxSpinEdit = TryCast(_formPremium.FindControl("NetPremium"), ASPxSpinEdit)
        Dim Stamp As ASPxSpinEdit = TryCast(_formPremium.FindControl("Stamp"), ASPxSpinEdit)
        Dim Vat As ASPxSpinEdit = TryCast(_formPremium.FindControl("Vat"), ASPxSpinEdit)
        Dim GrossPremium As ASPxSpinEdit = TryCast(_formPremium.FindControl("GrossPremium"), ASPxSpinEdit)
        Dim Discount As ASPxSpinEdit = TryCast(_formPremium.FindControl("Discount"), ASPxSpinEdit)
        Dim TotalPremium As ASPxSpinEdit = TryCast(_formPremium.FindControl("TotalPremium"), ASPxSpinEdit)
        Dim SinglePrice As ASPxSpinEdit = TryCast(_formPremium.FindControl("SinglePrice"), ASPxSpinEdit)
        Dim WHT_Rate1 As ASPxTextBox = TryCast(_formPremium.FindControl("WHT_Rate1"), ASPxTextBox)
        Dim WHT_Rate2 As ASPxTextBox = TryCast(_formPremium.FindControl("WHT_Rate2"), ASPxTextBox)
        Dim WHT_Rate3 As ASPxTextBox = TryCast(_formPremium.FindControl("WHT_Rate3"), ASPxTextBox)
        Dim IsActive As ASPxCheckBox = TryCast(_formPremium.FindControl("IsActive"), ASPxCheckBox)


        e.NewValues("Underwriter") = Underwriter.Value
        e.NewValues("CoverageID") = CoverageID.Value
        e.NewValues("Brand") = Brand.Value
        e.NewValues("Model") = Model.Value
        e.NewValues("Name") = Name.Value
        e.NewValues("ModelCode") = ModelCode.Value
        e.NewValues("CarPrice") = CarPrice.Value
        e.NewValues("CarAge") = CarAge.Value
        e.NewValues("SumInsuredFrom") = SumInsuredFrom.Value
        e.NewValues("SumInsuredTo") = SumInsuredTo.Value
        e.NewValues("NetPremium") = NetPremium.Value
        e.NewValues("Stamp") = Stamp.Value
        e.NewValues("Vat") = Vat.Value
        e.NewValues("GrossPremium") = GrossPremium.Value
        e.NewValues("Discount") = Discount.Value
        e.NewValues("TotalPremium") = TotalPremium.Value
        e.NewValues("SinglePrice") = SinglePrice.Value
        e.NewValues("WHT_Rate1") = WHT_Rate1.Value
        e.NewValues("WHT_Rate2") = WHT_Rate2.Value
        e.NewValues("WHT_Rate3") = WHT_Rate3.Value
        e.NewValues("CampaignID") = Session("CampaignID")
        e.NewValues("ModifyBy") = HttpContext.Current.User.Identity.Name

        e.NewValues("IsActive") = IsActive.Checked

    End Sub


    Protected Sub btnCoveragePremiumFormat_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDownloadPremiumFormat.Click
        Dim fileName = String.Format("{0}.xls", System.Guid.NewGuid.ToString())
        Dim filePath = String.Format("{0}\{1}", Server.MapPath("~/Template"), "MotorCoveragePremium.xlsx")

        Response.ClearContent()
        Response.AddHeader("content-disposition", "attachment; filename=" & fileName)
        Response.ContentType = "application/octet-stream"
        Response.WriteFile(filePath)
        Response.End()

    End Sub


    'Protected Sub btnSavePremium_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSavePremium.Click
    '    importdata()
    '    popUpNewPremium.ShowOnPageLoad = False
    '    gridCarPremium.DataBind()
    'End Sub
    'cbDeletePremium
    Protected Sub cbDeletePremium_Callback(ByVal source As Object, ByVal e As CallbackEventArgsBase) Handles cbDeletePremium.Callback
        Dim _premiumid = e.Parameter.ToString().Split(",")

        Using dc As New DataClasses_CPSExt()

            Dim _data = (From c In dc.tblCarPremiums Where _premiumid.Contains(c.PremiumID)).ToList()


            dc.tblCarPremiums.DeleteAllOnSubmit(_data)

            dc.SubmitChanges()
        End Using



    End Sub



    Protected Sub cbNewPremium_Callback(ByVal source As Object, ByVal e As CallbackEventArgsBase) Handles cbNewPremium.Callback
        importdata()
    End Sub

    Private Sub importdata()

        Dim _CampaignID As String = gridCampaign.GridView.GetRowValues(gridCampaign.GridView.FocusedRowIndex, gridCampaign.KeyFieldName).ToString()



        Dim dc = New DataClasses_CPSExt()


        Dim _dataList As New List(Of tblCarPremium)

        Dim i As Integer = 0
        Dim reader = New StringReader(tbxdata.Text)
        Dim line As String
        line = reader.ReadLine()
        While line IsNot Nothing
            Dim _row() As String = line.Split(vbTab)
            'Name,ModelCode,Price,Age, SI_From,SI_To,Net Prem,VAT,Stamp,Disc to NMT,Total Premium,Single Price,WHT,LWT+NLT,DLR




            '.GrossPremium = Convert.ToDouble(_GrossPremium)
            '.Stamp = Math.Ceiling((Convert.ToDouble(_GrossPremium) * 0.004))
            '.vat = (Math.Ceiling((Convert.ToDouble(_GrossPremium) * 0.004)) + Convert.ToDouble(_GrossPremium)) * 0.07

            Dim ModelList = newModel.Value.ToString().Split(";")

            For Each ModelName As String In ModelList



                Dim _newData As New tblCarPremium()
                With _newData

                    .Model = ModelName.Trim()

                    .Underwriter = newUnderwriter.Value
                    .CoverageID = Convert.ToInt32(newCoverageID.Value)
                    .CampaignID = Convert.ToInt32(_CampaignID)
                    .Brand = newBrand.Value



                    .Name = _row(0)

                    If String.IsNullOrEmpty(.Name) Then
                        .Name = "-" '.Model
                    End If

                    .ModelCode = _row(1)
                    .CarPrice = Convert.ToDouble(_row(2))
                    .CarAge = Convert.ToInt32(_row(3))
                    .SumInsuredFrom = Convert.ToInt32(_row(4).ToString().Replace(",", ""))
                    .SumInsuredTo = Convert.ToDouble(_row(5).ToString().Replace(",", ""))
                    .NetPremium = Convert.ToDouble(_row(6).ToString().Replace(",", ""))
                    .Vat = Convert.ToDouble(_row(7).ToString().Replace(",", ""))
                    .Stamp = Convert.ToDouble(_row(8).ToString().Replace(",", ""))
                    .GrossPremium = Convert.ToDouble(_row(9).ToString().Replace(",", ""))
                    .Discount = Convert.ToDouble(_row(10).ToString().Replace(",", ""))
                    .TotalPremium = Convert.ToDouble(_row(11).ToString().Replace(",", ""))
                    .SinglePrice = Convert.ToDouble(_row(12).ToString().Replace(",", ""))
                    .WHT_Rate1 = _row(13)
                    .WHT_Rate2 = _row(14)
                    .WHT_Rate3 = _row(15)

                    .CreateDate = DateTime.Now()
                    .CreateBy = HttpContext.Current.User.Identity.Name

                    .IsActive = True
                End With

                _dataList.Add(_newData)

            Next

            line = reader.ReadLine()
        End While

        dc.tblCarPremiums.InsertAllOnSubmit(_dataList)

        dc.SubmitChanges()

        tbxdata.Text = ""

    End Sub








    Protected Sub callbackPanel_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase) Handles callbackPanel.Callback

        Dim _cmd = e.Parameter.ToString()

        If Not String.IsNullOrEmpty(_cmd) Then

            Dim _params = _cmd.Split("|")
            Select Case _params(0)

                Case "select_campaign"
                    Dim _CampaignID As String = gridCampaign.GridView.GetRowValues(gridCampaign.GridView.FocusedRowIndex, gridCampaign.KeyFieldName).ToString()
                    Session("CampaignID") = _CampaignID

                Case "search_commission"
                    'Dim iterator As New TreeListNodeIterator(treeList_Commission.RootNode)
                    'Do While iterator.Current IsNot Nothing
                    '    CheckNode(iterator.Current)
                    '    iterator.GetNext()
                    'Loop

                Case "delete_commission"
                    'Dim _ID As Integer = Convert.ToInt32(_params(1).ToString())
                    'If _ID > 0 Then
                    '    Using dc As New DataClasses_CPSExt()
                    '        Dim _data = (From c In dc.tblCampaign_CommOuts Where c.CommOutID.Equals(_ID)).FirstOrDefault()
                    '        dc.tblCampaign_CommOuts.DeleteOnSubmit(_data)
                    '        dc.SubmitChanges()
                    '    End Using
                    'Else
                    '    Using dc As New DataClasses_CPSExt()
                    '        Dim _data = (From c In dc.tblCampaign_CommIns Where c.CommInID.Equals(_ID * -1)).FirstOrDefault()

                    '        dc.tblCampaign_CommOuts.DeleteAllOnSubmit(_data.tblCampaign_CommOuts)

                    '        dc.tblCampaign_CommIns.DeleteOnSubmit(_data)

                    '        dc.SubmitChanges()
                    '    End Using
                    'End If

                Case "REFRESH"

            End Select


            gridCarPremium.DataBind()
            pnCarPremium.ClientVisible = True
            'cmdAddNewCommissionAgent.ClientVisible = True
            'searchTxt.ClientVisible = True
            'searchBtn.ClientVisible = True
        End If


    End Sub




    Protected Sub SqlDataSource_CarPremium_Selecting(ByVal sender As Object, ByVal e As SqlDataSourceSelectingEventArgs) Handles SqlDataSource_CarPremium.Selecting
        If gridCampaign.GridView.FocusedRowIndex > -1 Then
            Dim _CampaignID As String = gridCampaign.GridView.GetRowValues(gridCampaign.GridView.FocusedRowIndex, gridCampaign.KeyFieldName).ToString()
            e.Command.CommandText = String.Format("select * from V_MotorPremium where CampaignID = {0} ", _CampaignID)
        End If

    End Sub
    Protected Sub SqlDataSource_Underwriter_Selecting(ByVal sender As Object, ByVal e As SqlDataSourceSelectingEventArgs) Handles SqlDataSource_Underwriter.Selecting
        If gridCampaign.GridView.FocusedRowIndex > -1 Then
            Dim _CampaignID As String = gridCampaign.GridView.GetRowValues(gridCampaign.GridView.FocusedRowIndex, gridCampaign.KeyFieldName).ToString()
            e.Command.CommandText = String.Format("SELECT distinct [Underwriter],[AccountContact],[CampaignID] FROM [V_Campaign_CommIn] where isactive = 1 and [CampaignID] = {0} ", _CampaignID)
        End If

    End Sub



    Protected Sub callbackPanel_MoreInfoCoverage_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase) Handles callbackPanel_MoreInfoCoverage.Callback
        Dim _PremiumID As Integer = Convert.ToInt32(e.Parameter.ToString())
        Using dc As New DataClasses_CPSExt()
            Dim _data = (From c In dc.V_MotorPremiums Where c.PremiumID.Equals(_PremiumID)).FirstOrDefault()

            With _data
                Coverage1.Value = .coverage1
                Coverage2.Value = .coverage2
                Coverage3.Value = .coverage3
                Coverage4.Text = .coverage4
                Coverage5.Text = .coverage5
                Coverage6.Text = .coverage6
                Coverage7.Text = .coverage7
                Coverage8.Text = .coverage8
                Coverage9.Text = .coverage9
                Coverage10.Text = .coverage10
                Coverage11.Text = .coverage11
                Coverage12.Text = .coverage12
                Coverage13.Text = .coverage13
                Coverage14.Text = .coverage14
                Coverage15.Text = .coverage15
            End With

        End Using


    End Sub




    Protected Sub callbackPanel_NewPremium_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase) Handles callbackPanel_NewPremium.Callback
        newUnderwriter.Value = ""
        newCoverageID.Value = ""
        newBrand.Value = ""
        newModel.Value = ""
        tbxdata.Text = ""

       
        newUnderwriter.DataBind()
        newCoverageID.DataBind()
        newBrand.DataBind()
        newModel.DataBind()



    End Sub

End Class
