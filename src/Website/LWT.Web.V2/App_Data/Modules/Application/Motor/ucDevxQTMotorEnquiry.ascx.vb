Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports BaseConfig
Imports DevExpress.Web
Imports DevExpress.Data
Imports DevExpress.Data.Linq
Imports System.Threading
Imports DevExpress.Web.Data



Partial Class Modules_ucDevxQTMotorEnquiry
    Inherits PortalModuleControl
    Public Function GetQuotationUrl(ByVal quotationno As String, ByVal ProjectID As String) As String
        Return String.Format("~/applications/editmotorquotation.aspx?quotationno={0}&ProjectID={1}", quotationno, ProjectID)
    End Function
    Private Sub BindData_CarModel(ByVal CarBrandlID As String)
        SqlDataSource_CarModel.SelectCommand = "SELECT ID, Name FROM tblCarBrandModel where ParentID is not null and ParentID=@ParentID  order by Name "
        SqlDataSource_CarModel.SelectParameters.Clear()
        SqlDataSource_CarModel.SelectParameters.Add("ParentID", TypeCode.Int32, CarBrandlID)
        newCarModel.DataSource = SqlDataSource_CarModel
        newCarModel.DataBind()
    End Sub



    Protected Sub Page_Load(sender As Object, e As EventArgs) Handles Me.Load
        SqlDataSource_UserProject.SelectParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name
        If Page.IsPostBack = False Then
            Session("QuotationNo") = ""
            Session("DiscGroupId") = ""
            dgQuotations.DataBind()
            btnSaveNewQuotation.AutoPostBack = False
        End If

    End Sub
  
 
    Private Sub SaveQuotation()
        Dim _year = DateTime.Today().Year
        If _year > 25000 Then
            _year = _year - 543
        End If
        Dim _prefix_quote = String.Format("QT{0}", Right(_year.ToString(), 2))
        Dim _dc = New DataClasses_CPSExt().tblMotorQuotations
        Dim _newQuotationNo = ""

        Dim data = _dc.Where(Function(c) c.QuotationNo.StartsWith(_prefix_quote)).OrderByDescending(Function(c) c.QuotationNo).FirstOrDefault()
        If data Is Nothing Then
            _newQuotationNo = _prefix_quote & "00000001"
        Else
            Dim _maxno = Convert.ToInt32(Right(data.QuotationNo, 8)) + 1
            _newQuotationNo = _prefix_quote & Right("00000000" & _maxno.ToString(), 8)
        End If


        Dim newdata = New tblMotorQuotation
        With newdata
            .TypeOfClient = "NEW"
            .UserRec = HttpContext.Current.User.Identity.Name
            .DateRec = DateTime.Now
            .QuotationStatus = "NQ"
            .QuotationNo = _newQuotationNo

            .CustTitleName = newCustTitleName.Text
            .CustName = newCustName.Text
            If Not newCustIDCardsType.SelectedItem Is Nothing Then .CustIDCardType = Convert.ToInt32(newCustIDCardsType.SelectedItem.Value)
            .CustIDCards = newCustIDCards.Text
            .CustAddress = newCustAddress.Text

            .CustLocationName = newLOCATION_CODE.Text
            If Not newLOCATION_CODE.SelectedItem Is Nothing Then .CustLocationCode = newLOCATION_CODE.SelectedItem.Value

            .CustTelNo = newCustTelNo.Text
            .CustEmail = newCustEmail.Text
            .CustFaxNo = newCustFaxNo.Text

            .CarBrandName = newCarBrand.Text
            .CarModelName = newCarModel.Text

            If Not newCarBrand.SelectedItem Is Nothing Then
                .CarBrandlID = Convert.ToInt32(newCarBrand.SelectedItem.Value)
                If Not String.IsNullOrEmpty(.CarModelName) Then
                    BindData_CarModel(.CarBrandlID)
                    Dim cid = newCarModel.Items.FindByText(.CarModelName).Value
                    If Not cid Is Nothing Then
                        .CarModelID = Convert.ToInt32(cid)
                    End If
                End If
            End If

            .CarSubModelName = newCarSubModelName.Text

            '.CarBrandModelName = newCarBrandModelID.Text
            '.CarBrandModelID = newCarBrandModelID.SelectedItem.Value
            '.CarSeriesName = newCarSeriesName.Text

            If Not newCarSuminsured.Value Is Nothing Then .CarSuminsured = CInt(newCarSuminsured.Value)
            If Not newCarReg.Value Is Nothing Then .CarRegis = CInt(newCarReg.Value)
            .CarId = newCarId.Text
            .CarEngineNo = newCarEngineNo.Text
            .CarChassisNo = newCarChassisNo.Text
            .CarSize = NewCarSize.Text




        End With

        _dc.InsertOnSubmit(newdata)
        _dc.Context.SubmitChanges()

        ASPxWebControl.RedirectOnCallback(GetQuotationUrl(_newQuotationNo, UserProject.Value))
    End Sub

   
    Protected Sub cbSaveNewQuotation_Callback(ByVal source As Object, ByVal e As CallbackEventArgsBase) Handles cbSaveNewQuotation.Callback
        If Page.IsCallback = True Then
            SaveQuotation()
        End If
    End Sub
 
 
    Protected Sub newCarModel_Callback(ByVal source As Object, ByVal e As CallbackEventArgsBase) Handles newCarModel.Callback
        If Page.IsCallback = True Then
            SqlDataSource_CarModel.SelectCommand = "SELECT ID, Name FROM tblCarBrandModel where ParentID is not null and ParentID=@ParentID  order by Name "
            SqlDataSource_CarModel.SelectParameters.Clear()
            SqlDataSource_CarModel.SelectParameters.Add("ParentID", TypeCode.Int32, e.Parameter)
            newCarModel.DataSource = SqlDataSource_CarModel
            newCarModel.DataBind()
        End If
    End Sub

    Protected Sub callbackPanel_Callback(ByVal source As Object, ByVal e As DevExpress.Web.CallbackEventArgsBase) Handles callbackPanel.Callback

        moreReferenceNo.Text = ""
        moreCarBrandName.Text = ""
        moreCarModelName.Text = ""


        Dim _QuotationNo As String = e.Parameter.ToString()

        Dim _dc = New DataClasses_CPSExt().V_MotorQuotations
        Dim data = _dc.Where(Function(c) c.QuotationNo.Equals(_QuotationNo)).FirstOrDefault()

        If Not data Is Nothing Then
            moreReferenceNo.Text = data.ReferenceNo
            moreCarBrandName.Text = data.CarBrandName
            moreCarModelName.Text = data.CarModelName

        End If


    End Sub












End Class
