Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports FineUI 
Imports System.IO
Imports System.Web.Mail
Imports System.Net.Mail

Imports NPOI.HSSF.UserModel
Imports NPOI.HPSF
Imports NPOI.SS.UserModel
Imports NPOI.SS.Util
Imports NPOI.HSSF.Util
Imports System.Drawing
Imports iTextSharp.text.pdf

Partial Class Modules_ucDevxNoticeHeaderSetup_B0016
    Inherits PortalModuleControl
    Dim _MailNotifications_Amend As String = "B0015"
    'Dim _MailNotifications_Endorse As String = "B0016"

    Dim _NoticeCode As String = "B0016"

    '*******************************************************
    '
    ' The Page_Load server event handler on this user control is used
    ' to populate the current site settings from the config system
    '
    '*******************************************************
    Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        If Page.IsPostBack = False Then
            'BindEmailNotification()
            BindData()
        End If
    End Sub

    Private Sub BindData()
        Dim column As FineUI.GridColumn = Grid1.FindColumn(Grid1.SortField)
        BindGridWithSort(column.SortField, Grid1.SortDirection)
    End Sub

      Private Sub BindGridWithSort(ByVal sortField As String, ByVal sortDirection As String)
        Using dc As New DataClasses_CPSExt()
            Dim _data = dc.ExecuteQuery(Of MyContext.NoticeHeader)("select top 1000 * from tblNoticeHeader where NoticeCode='" & _NoticeCode & "' ").ToList()
            Dim table As DataTable = MyUtils.EQToDataTable(_data)
            Dim view1 As DataView = table.DefaultView
            If _data.Count > 0 Then
                view1.Sort = String.Format("{0} {1}", sortField, sortDirection)
            End If
            Grid1.DataSource = view1
            Grid1.DataBind()
        End Using
    End Sub

    Protected Sub Grid1_Sort(ByVal sender As Object, ByVal e As FineUI.GridSortEventArgs) Handles Grid1.Sort
        BindGridWithSort(e.SortField, e.SortDirection)
    End Sub

    Protected Sub Grid1_PageIndexChange(ByVal sender As Object, ByVal e As FineUI.GridPageEventArgs) Handles Grid1.PageIndexChange
        Grid1.PageIndex = e.NewPageIndex
        BindData()
    End Sub

    Protected Sub Grid1_RowCommand(ByVal sender As Object, ByVal e As FineUI.GridCommandEventArgs) Handles Grid1.RowCommand
        Dim _NotificationID As String = Grid1.DataKeys(e.RowIndex)(0)
        hdNotificationID.Text = _NotificationID

        Select Case e.CommandName
            Case "btnUpload"

                WindowUploaddata.Title = "Data"
                WindowUploaddata.Hidden = False

            Case "btnExport"

                Using dc As New DataClasses_CPSDataContext

                    Dim _data = (From c In dc.tblNoticeDetails Where c.NoticeID.Equals(_NotificationID) And (c.f14.Equals("1") Or c.f14.Equals("2"))).ToList()

                    If _data.Count > 0 Then
                        ExportData(_NotificationID)
                    Else
                        Alert.Show("No Data")
                    End If

                End Using



            Case "btnSendMail"

                Using dc As New DataClasses_CPSDataContext

                    Dim _data = (From c In dc.tblNoticeDetails Where c.NoticeID.Equals(_NotificationID) And (c.f14.Equals("1") Or c.f14.Equals("2"))).ToList()

                    If _data.Count > 0 Then

                        Dim _Insurer = (From c In dc.tblNoticeDetails _
                                 Where c.NoticeID.Equals(_NotificationID) _
                                 And (c.f14.Equals("1") Or c.f14.Equals("2")) _
                                 Group By UWCode = c.f09, UWName = c.f10 _
                                 Into g = Group _
                                 Select UWCode, UWName, Clients = g.Count() _
                           ).ToList()


                        For Each iInsurer In _Insurer
                            WH_genxls(_NotificationID, iInsurer.UWCode)
                        Next

                        Dim _itemdata = (From c In dc.tblNoticeHeaders Where c.NoticeID.Equals(_NotificationID)).FirstOrDefault()
                        If _itemdata IsNot Nothing Then

                            If _itemdata.SendDate Is Nothing Then
                                _itemdata.SendDate = DateTime.Now()
                                _itemdata.SendBy = HttpContext.Current.User.Identity.Name.ToString()
                            Else
                                _itemdata.ReSendDate = DateTime.Now()
                                _itemdata.ReSendBy = HttpContext.Current.User.Identity.Name.ToString()
                            End If


                            dc.SubmitChanges()
                        End If
                        BindData()

                        Alert.Show("done")


                    Else
                        Alert.Show("No Data")
                    End If

                End Using


            Case "btnExport_Amend"

                Using dc As New DataClasses_CPSDataContext

                    Dim _data = (From c In dc.tblNoticeDetails Where c.NoticeID.Equals(_NotificationID) _
                   And (c.f14.Equals("2") Or c.f14.Equals("3"))).ToList()

                    If _data.Count > 0 Then
                        ExportData_Amend(_NotificationID)
                    Else
                        Alert.Show("No Data")
                    End If

                End Using


            Case "btnSendMail_Amend"

                Using dc As New DataClasses_CPSDataContext

                    Dim _data = (From c In dc.tblNoticeDetails Where c.NoticeID.Equals(_NotificationID) And (c.f14.Equals("2") Or c.f14.Equals("3"))).ToList()

                    If _data.Count > 0 Then

                        WH_genxls_Amend(_NotificationID)

                        Dim _itemdata = (From c In dc.tblNoticeHeaders Where c.NoticeID.Equals(_NotificationID)).FirstOrDefault()
                        If _itemdata IsNot Nothing Then

                            If _itemdata.SendDate Is Nothing Then
                                _itemdata.SendDate = DateTime.Now()
                                _itemdata.SendBy = HttpContext.Current.User.Identity.Name.ToString()
                            Else
                                _itemdata.ReSendDate = DateTime.Now()
                                _itemdata.ReSendBy = HttpContext.Current.User.Identity.Name.ToString()
                            End If


                            dc.SubmitChanges()
                        End If
                        BindData()

                        Alert.Show("done")


                    Else
                        Alert.Show("No Data")
                    End If

                End Using
        End Select

    End Sub

    Protected Sub btnRefresh_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        PageContext.Refresh()
    End Sub

    Protected Sub btnAdd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        WindowDataNew.Hidden = False
    End Sub

    Protected Sub btnSaveNew_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSaveNew.Click

        Using dc As New DataClasses_CPSDataContext
            '                                                       , .DataFields = "No,Client Code,ผู้เอาประกันภัย,New,Policy No. หรือ เลขรับแจ้ง,หมายเลขตัวถัง/ทะเบียนรถ, SumInsure , VMI Premium ,Beneficiary,Insurer,ShowRoom,Type,Amendment Details,รายการข้อมูลเดิม,รายการที่แก้ไขใหม่" _
            Dim _newdata As New tblNoticeHeader With {.NoticeTitle = "แจ้งรายการสลักหลังแก้ไขกรมธรรม์" _
                                                       , .NoticeCode = _NoticeCode _
                                                       , .CreationDate = DateTime.Now _
                                                       , .CreationBy = HttpContext.Current.User.Identity.Name _
                                                       , .NoticeDate = dpNewNoticeDate.SelectedDate _
                                                    }
            dc.tblNoticeHeaders.InsertOnSubmit(_newdata)
            dc.SubmitChanges()

        End Using
        BindData()
        WindowDataNew.Hidden = True
    End Sub

    Private Sub importdata()
        Dim sb As New StringBuilder()

        Dim _BillingNotificationData As New List(Of tblNoticeDetail)

        Dim i As Integer = 0
        Dim reader = New StringReader(tbxdata.Text.Replace("\n", " "))
        Dim line As String
        line = reader.ReadLine()

        Dim dc_CPS As New DataClasses_CPSDataContext
        Dim dc_TIDMotor As New DataClasses_TIDMasterExt
        Dim dc_M2 As New DataClasses_NLTDBExt
        Dim dc_SIBIS As New DataClasses_SIBISExt

        'Using dc_TIDMotor As New DataClasses_TIDMotorDataContext(webconfig._TidmasterConn)
        '    Using dc_SIBIS As New DataClasses_SIBISDBDataContext(webconfig._SIBISDBConn)
        '        Using dc_M2 As New DataClasses_NLTDBDataContext(webconfig._NLTDBConn)

        
        While line IsNot Nothing

            Dim sb_Error As New StringBuilder()

            Dim _row() As String = line.Split(vbTab)


            Dim _REFNO = _row(0).ToString().Trim()
            Dim _clientcode = _row(1).ToString().Trim()
            Dim _AmentCode = _row(2).ToString().Trim()
            Dim _NewAmendDeatils_Old = _row(3).ToString().Trim()
            Dim _NewAmendDeatils_New = _row(4).ToString().Trim()


            'Dim _data = dc_TIDMotor.SP_CLIENT_SEARCH(_clientcode).FirstOrDefault()
            Dim _data = (From c In dc_TIDMotor.V_MotorEndorsements Where c.REFNO.Equals(_REFNO)).FirstOrDefault()

            'Dim _data = (From c In dc_SIBIS.Clients Where c.Client.Equals(_clientcode)).FirstOrDefault()

            If _data Is Nothing Then
                sb_Error.AppendFormat("Client: '{0}' ;ไม่พบข้อมูล<br>", _clientcode)
            End If
            If _AmentCode.Split(":").Count <> 2 Then
                sb_Error.AppendFormat("Client: '{0}' ; Endorsement Details '{1}' ไม่ถูกต้อง<br>", _clientcode, _AmentCode)
            End If

            If Not String.IsNullOrEmpty(sb_Error.ToString()) Then
                sb.Append(sb_Error.ToString())
            Else
                Dim _itemdata As New tblNoticeDetail()
                With _itemdata

                    .NoticeID = hdNotificationID.Text
                    .Code = _data.ClientCode 'Code Client Code
                    .f01 = _data.ClientName 'f01 ผู้เอาประกันภัย
                    .f02 = _data.RNType 'New
                    .f03 = _data.PolicyNo 'Policy No. หรือ เลขรับแจ้ง
                    .f04 = _data.CHASSIS   'หมายเลขตัวถัง
                    .f05 = _data.CarPlate 'ทะเบียนรถ

                    .f06 = _REFNO.ToString() 'Refno

                    Dim _Ament = _AmentCode.Split(":")
                    .f11 = _Ament(0).ToString().Trim() 'f11 Amendment Details
                    .f15 = _Ament(1).ToString().Trim()


                    Select Case _data.Motor
                        Case "M1"
                            Dim _CNShort1 = (From c In dc_TIDMotor.CompanyInsures Where c.CompanyCode.Equals(_data.InsurerCode)).FirstOrDefault()

                            Dim _UW = (From c In dc_SIBIS.Underwriters Where c.AccountContact.Equals(_CNShort1.CompanyName) Order By c.EntryDate).FirstOrDefault()
                            If Not _UW Is Nothing Then
                                .f09 = _UW.Underwriter.Trim() 'UWCode
                                .f10 = _UW.AccountContact.Trim() 'InsurerName
                            Else
                                Dim _UW2 = (From c In dc_SIBIS.Underwriters Where c.CrossReference.Equals(_CNShort1.CNShort1) Order By c.EntryDate).FirstOrDefault()
                                If Not _UW2 Is Nothing Then
                                    .f09 = _UW2.Underwriter.Trim() 'UWCode
                                    .f10 = _UW2.AccountContact.Trim() 'InsurerName
                                Else
                                    Dim _UW3 = (From c In dc_CPS.tblNoticeMailContacts Where c.M1Code.Equals(_data.InsurerCode) And c.NoticeCode.Equals(_NoticeCode)).FirstOrDefault()
                                    .f09 = _UW3.Code.Trim() 'UWCode
                                    .f10 = _UW3.Name.Trim() 'InsurerName
                                End If
                            End If



                            'Dim _Policy = dc_SIBIS.sp_getPolicyByRefNo(_data.ClientCode, _data.REFNO).FirstOrDefault()
                            '.f09 = _Policy.Underwriter.Trim() 'UWCode
                            ''.f03 = _Policy.PolicyNo.Trim() 'Policy No.
                            'Dim _uw = (From c In dc_SIBIS.Underwriters Where c.Underwriter = .f09).FirstOrDefault()
                            '.f10 = _uw.AccountContact.Trim() 'InsurerName

                        Case "M2"
                            .f09 = _data.InsurerCode 'UWCode
                            Dim _UW = (From c In dc_M2.InsurerUniques Where c.InsurerCode.Equals(.f09)).FirstOrDefault()
                            .f10 = _UW.InsurerNameThai.Trim() 'InsurerName

                            'Dim _UW2 = (From c In dc_CPS.tblNoticeMailContacts Where c.M1Code.Equals(_data.InsurerCode)).FirstOrDefault()
                            '.f09 = _UW2.Code.Trim() 'UWCode
                            '.f10 = _UW2.Name.Trim() 'InsurerName
                    End Select



                    '========================= 1.Endorse Only ================
                    Select Case .f15.Trim()
                        Case "1" 'แก้ไขวันคุ้มครองพรบ.
                            If _data.CMI_StartDateOld IsNot Nothing Then
                                '.f12 = String.Format("{0} - {1}", _data.CMI_StartDateOld.Value.ToString("dd/MM/yyyy"), _data.CMI_EndDateOld.Value.ToString("dd/MM/yyyy"))
                                .f12 = _data.CMI_StartDateOld.Value.ToString("dd/MM/yyyy")

                                'Dim _CMI_StartDateOld = (From c In dc_SIBIS.Policies Where c.Client.Equals(_data.ClientCode) _
                                '                           And c.Class.StartsWith("CI") _
                                '                           And c.BriefIII.StartsWith(_data.CHASSIS) _
                                '                           And c.BriefII.Equals(_data.REFNO) _
                                '                           Order By c.PeriodFrom Descending
                                '                           ).FirstOrDefault()
                                '.f12 = _CMI_StartDateOld.PeriodFrom.ToString("dd/MM/yyyy")
                                .f13 = _data.CMI_StartDate.Value.ToString("dd/MM/yyyy")
                                .f14 = "1"
                            End If
                        Case "2" 'แก้ไขวันคุ้มครอง 
                            .f12 = String.Format("{0} - {1}", _data.StartDateOld.Value.ToString("dd/MM/yyyy"), _data.EndDateOld.Value.ToString("dd/MM/yyyy"))
                            '.f12 = _data.StartDateOld.Value.ToString("dd/MM/yyyy")                                       
                            '.f12 = _VMI.PeriodFrom.ToString("dd/MM/yyyy")
                            .f13 = String.Format("{0} - {1}", _data.StartDate.Value.ToString("dd/MM/yyyy"), _data.EndDate.Value.ToString("dd/MM/yyyy")) '_data.StartDate.Value.ToString("dd/MM/yyyy")
                            .f14 = "1"
                        Case "3" 'แก้ไขรุ่นรถ
                            .f12 = _data.ModelOld
                            '.f12 = _VMI.BriefDescription

                            .f13 = _data.Model
                            .f14 = "1"
                        Case "4" 'แก้ไขทุนประกัน
                            .f12 = _data.SuminsuredOld
                            '.f12 = _VMI.SumInsured
                            .f13 = _data.Suminsured
                            .f14 = "1"
                        Case "5" 'แก้ไขประเภทประกันภัย
                            .f12 = _data.PolicyTypeOld
                            .f13 = _data.PolicyType
                            .f14 = "1"
                        Case "6" 'แก้ไขทะเบียนรถ
                            .f12 = _data.CarPlateOld
                            .f13 = _data.CarPlate
                            .f14 = "1"
                        Case "7" 'แก้ไขดีลเลอร์
                            .f12 = _data.DealerOld
                            .f13 = _data.Dealer
                            .f14 = "1"
                        Case "8" 'แก้ไขเลขถัง
                            .f12 = _data.CHASSISOld
                            '.f12 = _VMI.BriefIII
                            .f13 = _data.CHASSIS
                            .f14 = "1"
                        Case "9" 'แก้ไขเลขเครื่อง
                            .f12 = _data.CarEngineOld
                            .f13 = _data.CarEngine
                            .f14 = "1"
                        Case "10" 'แก้ไขเลขบัตรประชาชน
                            .f12 = _data.IDCARDOld
                            .f13 = _data.IDCARD
                            .f14 = "1"
                        Case "14" 'แก้ไขผู้รับผลประโยชน์
                            .f12 = _data.BenefitialyOld
                            .f13 = _data.Benefitialy
                            .f14 = "1"
                        Case "15" 'แก้ไขโครงการ
                            .f12 = _data.ProjectOld
                            .f13 = _data.Project
                            .f14 = "1"
                        Case "16" 'เพิ่มข้อมูลพรบ. add ################### (OK)
                            .f12 = _NewAmendDeatils_Old
                            .f13 = _NewAmendDeatils_New '_data.CMI_PolicyNo 'f13 รายการที่แก้ไขใหม่
                            .f14 = "1"
                        Case "17" 'เพิ่มเติมอุปกรณ์ add ###################
                            .f12 = _NewAmendDeatils_Old
                            .f13 = _NewAmendDeatils_New '_data.CarAccessories 'f13 รายการที่แก้ไขใหม่
                            .f14 = "1"
                        Case "18"     'เพิ่มผู้ขับขี่ที่ 1:18 add ################### (OK)
                            .f12 = ""
                            .f13 = String.Format("{0} (อายุ {1} ปี)", _data.DriverName1, _data.DriverOld1)  'f13 รายการที่แก้ไขใหม่
                            .f14 = "1"
                        Case "19"     'เพิ่มผู้ขับขี่ที่ 2:19 add ################### (OK)
                            .f12 = ""
                            .f13 = String.Format("{0} (อายุ {1} ปี)", _data.DriverName2, _data.DriverOld2) 'f13 รายการที่แก้ไขใหม่
                            .f14 = "1"
                        Case "21"     'แก้ไขผู้รับผลประโยชน์ในกรมธรรม์และพรบ.:21
                            .f12 = _data.BenefitialyOld
                            '.f12 = _client.Name.Trim()
                            .f13 = _data.Benefitialy
                            .f14 = "1"
                        Case "22"     'แก้ไขที่อยู่ในกรมธรรม์และพรบ.:22
                            .f12 = _data.Address1Old
                            '.f12 = String.Format("{0} {1} {2} {4}", _client.Address1.Trim(), _client.Address2.Trim(), _client.Address3.Trim(), _client.PostCode.Trim()).Trim()
                            .f13 = _data.Address1
                            'Case "23"     'ขยายอาณาเขต:23 add ###################
                            '    .f12 = ""
                            '    .f13 = _NewAmendDeatils 'f13 รายการที่แก้ไขใหม่
                            .f14 = "1"

                        Case "23"
                            .f12 = _NewAmendDeatils_Old
                            .f13 = _NewAmendDeatils_New
                            .f14 = "1"
                        Case "24"     'ขยายวันคุ้มครอง:24
                            ''.f12 = String.Format("{0} - {1}", _data.StartDate.Value.ToString("dd/MM/yyyy"), _data.EndDate.Value.ToString("dd/MM/yyyy"))
                            '.f12 = _data.StartDateOld.Value.ToString("dd/MM/yyyy")
                            ''.f12 = _VMI.PeriodFrom.ToString("dd/MM/yyyy")
                            '.f13 = _data.StartDate.Value.ToString("dd/MM/yyyy")
                            .f12 = _NewAmendDeatils_Old
                            .f13 = _NewAmendDeatils_New
                            .f14 = "1"
                        Case "25"     'แก้ไขวันคุ้มครองในกรมธรรม์และพรบ.:25
                            '.f12 = String.Format("{0} - {1}", _data.StartDate.Value.ToString("dd/MM/yyyy"), _data.EndDate.Value.ToString("dd/MM/yyyy"))
                            '.f12 = String.Format("{0} - {1}", _data.StartDateOld.Value.ToString("dd/MM/yyyy"), _data.EndDateOld.Value.ToString("dd/MM/yyyy"))
                            If _data.StartDateOld IsNot Nothing Then .f12 = _data.StartDateOld.Value.ToString("dd/MM/yyyy")
                            '.f12 = _VMI.PeriodFrom.ToString("dd/MM/yyyy")
                            .f13 = _data.StartDate.Value.ToString("dd/MM/yyyy")
                            .f14 = "1"
                        Case "26"     'แก้ไขทะเบียนรถในกรมธรรม์และพรบ.:26
                            .f12 = _data.CarPlateOld
                            .f13 = _data.CarPlate
                            .f14 = "1"
                        Case "27"   'แก้ไขใบเสร็จพรบ. add ###################
                            .f12 = _NewAmendDeatils_Old '_data.CMI_PaymentByOld
                            .f13 = _NewAmendDeatils_New '_data.CMI_PaymentBy 'f13 รายการที่แก้ไขใหม่
                            .f14 = "1"
                        Case "28"   'แก้ไขราคา + ทุนประกัน
                            .f12 = String.Format("ทุน {0}", _data.SuminsuredOld)
                            .f13 = String.Format("ทุน {0}", _data.Suminsured)
                            .f14 = "1"
                        Case "29"   'แก้ไขรุ่นรถ + ทุนประกัน
                            .f12 = String.Format("รุ่น {0}, ทุน {1}", _data.ModelOld, _data.SuminsuredOld)
                            .f13 = String.Format("รุ่น {0}, ทุน {1}", _data.Model, _data.Suminsured)
                            .f14 = "1"
                        Case "30"   'แก้ไขเลขพรบ.
                            .f12 = _NewAmendDeatils_Old ' _data.CMI_PolicyNoOld
                            .f13 = _NewAmendDeatils_New '_data.CMI_PolicyNo
                            '5.แก้ไขวันคุ้มครองในกรมธรรม์
                            .f14 = "1"


                    End Select

                    '========================= 2.Amend and Endorse ================
                    Select Case .f15.Trim()
                        Case "11", "20" 'แก้ไขชื่อสกุลผู้เอาประกันและผู้ติดต่อ (amend)., "20" 'แก้ไขชื่อสกุลผู้เอาประกัน:20  (amend)
                            Dim _client = (From c In dc_SIBIS.Clients Where c.Client.Equals(_clientcode)).FirstOrDefault()
                            .f12 = _client.Name.Trim()
                            '.f12 = _data.ClientNameOld

                            .f13 = _data.ClientName
                            .f14 = "2"


                        Case "12", "13" 'แก้ไขที่อยู่ผู้เอาประกันและผู้ติดต่อ (amend)."13" 'แก้ไขที่อยู่ผู้เอาประกัน (amend)
                            '.f12 = _data.Address1Old
                            Dim _client = (From c In dc_SIBIS.Clients Where c.Client.Equals(_clientcode)).FirstOrDefault()
                            .f12 = String.Format("{0} {1} {2} {3}", _client.Address1.Trim(), _client.Address2.Trim(), _client.Address3.Trim(), _client.PostCode.Trim())

                            .f13 = _data.Address1
                            .f14 = "2"



                            'Case "13" 'แก้ไขที่อยู่ผู้เอาประกัน (amend)
                            '    .f12 = _data.Address1Old
                            '    .f13 = _data.Address1
                            '    .f14 = "2"
                            'Case "20" 'แก้ไขชื่อสกุลผู้เอาประกัน:20  (amend)
                            '    .f12 = _data.ClientNameOld
                            '    .f13 = _data.ClientName
                            '    .f14 = "2"
                    End Select

                    '========================= 3.Amend Only ================
                    Select Case _data.Motor
                        Case "M1"
                            Select Case .f15.Trim()
                                Case "31"  'แก้ไขชื่อสกุลผู้ติดต่อ : 31  (amend)
                                    '.f12 = _data.ConTractNameOld
                                    Dim _contract = (From c In dc_SIBIS.ContactPersons Where c.ClientCode.Equals(_clientcode) Order By c.EntryDate Descending).FirstOrDefault()
                                    .f12 = _contract.ContractPerson.Trim()
                                    .f13 = _data.ConTractName
                                    .f14 = "3"
                                Case "32"  'แก้ไขที่อยู่ผู้ติดต่อ : 32  (amend)
                                    Dim _contract = (From c In dc_SIBIS.ContactPersons Where c.ClientCode.Equals(_clientcode) Order By c.EntryDate Descending).FirstOrDefault()
                                    .f12 = String.Format("{0} {1} {2} {3} {4}", _contract.AddressName.Trim(), _contract.Address1.Trim(), _contract.Address2.Trim(), _contract.Address3.Trim(), _contract.Zipcode.Trim()) ' _data.Address2Old
                                    Dim _app = (From c In dc_TIDMotor.MotorDatas Where c.ID.Equals(_data.ID)).FirstOrDefault()
                                    .f13 = String.Format("{0} {1} {2}", _app.AddressConTract1.Trim(), _app.AddressConTract2.Trim(), _app.AddressConTract3.Trim())
                                    .f14 = "3"
                                Case "33", "35"  'แก้ไขเบอร์โทรผู้เอาประกัน : 33  (amend)
                                    Dim _client = (From c In dc_SIBIS.Clients Where c.Client.Equals(_clientcode)).FirstOrDefault()
                                    .f12 = String.Format("มือถือ:{0}, เบอร์บ้าน:{1}, office:{2}", _client.MobileNo.Trim(), _client.PhoneHome.Trim(), _client.PhoneBusiness.Trim()) '_data.PhoneNumber1Old
                                    Dim _app = (From c In dc_TIDMotor.MotorDatas Where c.ID.Equals(_data.ID)).FirstOrDefault()
                                    .f13 = String.Format("มือถือ:{0}, เบอร์บ้าน:{1}, office:{2}", _app.TelMobileClient.Trim(), _app.TelHomeClient.Trim(), _app.TelWorkClient.Trim())
                                    .f14 = "3"
                                Case "34"  'แก้ไขเบอร์โทรผู้ติดต่อ : 34  (amend) 
                                    Dim _contract = (From c In dc_SIBIS.ContactPersons Where c.ClientCode.Equals(_clientcode) Order By c.EntryDate Descending).FirstOrDefault()
                                    .f12 = String.Format("มือถือ:{0}, เบอร์บ้าน:{1}, office:{2}", _contract.MobilePhone.Trim(), _contract.HomeTelephone.Trim(), _contract.BusinessTelephone.Trim()) '_contract.MobilePhone '_data.PhoneNumber2Old
                                    Dim _app = (From c In dc_TIDMotor.MotorDatas Where c.ID.Equals(_data.ID)).FirstOrDefault()
                                    .f13 = String.Format("มือถือ:{0}, เบอร์บ้าน:{1}, office:{2}", _app.TelMobileConTract.Trim(), _app.TelHomeConTract.Trim(), _app.TelWorkConTract.Trim())
                                    .f14 = "3"
                                    'Case "35"  'แก้ไขเบอร์โทรผู้เอาประกันและผู้ติดต่อ : 35 (amend)
                                    '    Dim _client = (From c In dc_SIBIS.Clients Where c.Client.Equals(_clientcode)).FirstOrDefault()
                                    '    .f12 = String.Format("มือถือ:{0}, เบอร์บ้าน:{1}, office:{2}", _client.MobileNo.Trim(), _client.PhoneHome.Trim(), _client.PhoneBusiness.Trim()) '_data.PhoneNumber1Old
                                    '    .f14 = "3"
                            End Select

                        Case "M2"
                            Select Case .f15.Trim()
                                Case "31"  'แก้ไขชื่อสกุลผู้ติดต่อ : 31  (amend)
                                    '.f12 = _data.ConTractNameOld
                                    Dim _contract = (From c In dc_SIBIS.ContactPersons Where c.ClientCode.Equals(_clientcode) Order By c.EntryDate Descending).FirstOrDefault()
                                    .f12 = _contract.ContractPerson.Trim()
                                    .f13 = _data.ConTractName
                                    .f14 = "3"
                                Case "32"  'แก้ไขที่อยู่ผู้ติดต่อ : 32  (amend)
                                    Dim _contract = (From c In dc_SIBIS.ContactPersons Where c.ClientCode.Equals(_clientcode) Order By c.EntryDate Descending).FirstOrDefault()
                                    .f12 = String.Format("{0} {1} {2} {3} {4}", _contract.AddressName.Trim(), _contract.Address1.Trim(), _contract.Address2.Trim(), _contract.Address3.Trim(), _contract.Zipcode.Trim()) ' _data.Address2Old
                                    Dim _app = (From c In dc_M2.Applications Where c.AID.Equals(_data.ID)).FirstOrDefault()
                                    .f13 = _app.Address2
                                    .f14 = "3"
                                Case "33", "35"  'แก้ไขเบอร์โทรผู้เอาประกัน : 33  (amend)
                                    Dim _client = (From c In dc_SIBIS.Clients Where c.Client.Equals(_clientcode)).FirstOrDefault()
                                    .f12 = String.Format("มือถือ:{0}, เบอร์บ้าน:{1}, office:{2}", _client.MobileNo.Trim(), _client.PhoneHome.Trim(), _client.PhoneBusiness.Trim()) '_data.PhoneNumber1Old
                                    Dim _app = (From c In dc_M2.Applications Where c.AID.Equals(_data.ID)).FirstOrDefault()
                                    .f13 = String.Format("มือถือ:{0}, เบอร์บ้าน:{1}, office:{2}", _app.Mobile.Trim(), _app.HomePhone.Trim(), _app.OfficePhone.Trim()) '_data.PhoneNumber1Old
                                    .f14 = "3"
                                Case "34"  'แก้ไขเบอร์โทรผู้ติดต่อ : 34  (amend) 
                                    Dim _contract = (From c In dc_SIBIS.ContactPersons Where c.ClientCode.Equals(_clientcode) Order By c.EntryDate Descending).FirstOrDefault()
                                    .f12 = String.Format("มือถือ:{0}, เบอร์บ้าน:{1}, office:{2}", _contract.MobilePhone.Trim(), _contract.HomeTelephone.Trim(), _contract.BusinessTelephone.Trim()) '_contract.MobilePhone '_data.PhoneNumber2Old
                                    Dim _app = (From c In dc_M2.Applications Where c.AID.Equals(_data.ID)).FirstOrDefault()
                                    .f13 = String.Format("มือถือ:{0}, เบอร์บ้าน:{1}, office:{2}", _app.Mobile.Trim(), _app.HomePhone.Trim(), _app.OfficePhone.Trim()) '_data.PhoneNumber1Old
                                    .f14 = "3"
                                    'Case "35"  'แก้ไขเบอร์โทรผู้เอาประกันและผู้ติดต่อ : 35 (amend)
                                    '    Dim _client = (From c In dc_SIBIS.Clients Where c.Client.Equals(_clientcode)).FirstOrDefault()
                                    '    .f12 = String.Format("มือถือ:{0}, เบอร์บ้าน:{1}, office:{2}", _client.MobileNo.Trim(), _client.PhoneHome.Trim(), _client.PhoneBusiness.Trim()) '_data.PhoneNumber1Old
                                    '    .f14 = "3"
                            End Select
                    End Select



                End With
                _BillingNotificationData.Add(_itemdata)
            End If


            line = reader.ReadLine()
        End While
        '        End Using
        '    End Using
        'End Using


        If Not String.IsNullOrEmpty(sb.ToString()) Then
            Alert.Show(sb.ToString())
        Else
            Using dc As New DataClasses_CPSDataContext

                dc.ExecuteCommand("delete from tblNoticeDetail where NoticeID={0}", hdNotificationID.Text)

                dc.tblNoticeDetails.InsertAllOnSubmit(_BillingNotificationData)

                dc.SubmitChanges()

                Alert.Show(_BillingNotificationData.Count)
            End Using

            tbxdata.Text = ""

            WindowUploaddata.Hidden = True
        End If

    End Sub

    Protected Sub btnUpload_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnUpload.Click
        Select Case WindowUploaddata.Title
            Case "Data"
                importdata()

        End Select

    End Sub

    Protected Sub btnExcelFormat_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExcelFormat.Click
        Dim fileName = String.Format("{0}.xls", System.Guid.NewGuid.ToString())
        Dim filePath = String.Format("{0}\{1}", Server.MapPath("~/Template"), "Template for Endorsement send to Insurer.xls")



        Response.ClearContent()
        Response.AddHeader("content-disposition", "attachment; filename=" & fileName)
        Response.ContentType = "application/octet-stream"
        Response.WriteFile(filePath)
        Response.End()

    End Sub

    Protected Sub btnDeleteAll_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnDeleteAll.Click
        If Grid1.SelectedRowIndexArray.Length > 0 Then
            Using dc As New DataClasses_CPSDataContext
                For Each _item In Grid1.SelectedRowIndexArray
                    Dim NotificationID = Grid1.DataKeys(_item)(0)
                    '[Billing.NotificationData]
                    dc.ExecuteCommand("delete from tblNoticeDetail where NoticeID=" & NotificationID)
                    '[Billing.Notification]
                    dc.ExecuteCommand("delete from tblNoticeHeader where NoticeID=" & NotificationID)
                Next
            End Using
            BindData()
            Alert.Show("done")

            Grid1.Hidden = False
        Else
            Alert.Show("No select data")
        End If
    End Sub

    Private Sub ExportData(ByVal NotificationID As String)


        Dim fileName = String.Format("{0}.xls", System.Guid.NewGuid.ToString())
        Dim filePath = String.Format("{0}\{1}", Server.MapPath("~/upload"), fileName)

        Dim hssfworkbook As New HSSFWorkbook()
        '=====================cell style ===================
        Dim Header_Font = hssfworkbook.CreateFont()
        Header_Font.FontName = "Tahoma"
        Header_Font.FontHeightInPoints = 10
        Header_Font.Boldweight = FontBoldWeight.Bold

        Dim Header_Style As ICellStyle = hssfworkbook.CreateCellStyle()
        Header_Style.SetFont(Header_Font)
        Header_Style.Alignment = HorizontalAlignment.Center
        Header_Style.FillPattern = FillPattern.SparseDots
        Header_Style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Lime.Index
        Header_Style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Lime.Index
        Header_Style.BorderBottom = BorderStyle.Thin
        Header_Style.BorderLeft = BorderStyle.Thin
        Header_Style.BorderRight = BorderStyle.Thin
        Header_Style.BorderTop = BorderStyle.Thin
        Header_Style.BottomBorderColor = HSSFColor.Black.Index
        Header_Style.LeftBorderColor = HSSFColor.Black.Index
        Header_Style.RightBorderColor = HSSFColor.Black.Index
        Header_Style.TopBorderColor = HSSFColor.Black.Index

        Dim Header_Style2 As ICellStyle = hssfworkbook.CreateCellStyle()
        Header_Style2.SetFont(Header_Font)
        Header_Style2.Alignment = HorizontalAlignment.Center
        Header_Style2.BorderBottom = BorderStyle.Thin
        Header_Style2.BorderLeft = BorderStyle.Thin
        Header_Style2.BorderRight = BorderStyle.Thin
        Header_Style2.BorderTop = BorderStyle.Thin
        Header_Style2.BottomBorderColor = HSSFColor.Black.Index
        Header_Style2.LeftBorderColor = HSSFColor.Black.Index
        Header_Style2.RightBorderColor = HSSFColor.Black.Index
        Header_Style2.TopBorderColor = HSSFColor.Black.Index

        Dim Header_Style3 As ICellStyle = hssfworkbook.CreateCellStyle()
        Header_Style3.SetFont(Header_Font)
        Header_Style3.Alignment = HorizontalAlignment.Center
        Header_Style3.FillPattern = FillPattern.SparseDots
        Header_Style3.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightYellow.Index
        Header_Style3.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.LightYellow.Index
        Header_Style3.BorderBottom = BorderStyle.Thin
        Header_Style3.BorderLeft = BorderStyle.Thin
        Header_Style3.BorderRight = BorderStyle.Thin
        Header_Style3.BorderTop = BorderStyle.Thin
        Header_Style3.BottomBorderColor = HSSFColor.Black.Index
        Header_Style3.LeftBorderColor = HSSFColor.Black.Index
        Header_Style3.RightBorderColor = HSSFColor.Black.Index
        Header_Style3.TopBorderColor = HSSFColor.Black.Index

        Dim Row_Font = hssfworkbook.CreateFont()
        Row_Font.FontName = "Tahoma"
        Row_Font.FontHeightInPoints = 8

        Dim Row_Style_Left As ICellStyle = hssfworkbook.CreateCellStyle()
        Row_Style_Left.SetFont(Row_Font)
        Row_Style_Left.BorderBottom = BorderStyle.Thin
        Row_Style_Left.BorderLeft = BorderStyle.Thin
        Row_Style_Left.BorderRight = BorderStyle.Thin
        Row_Style_Left.BorderTop = BorderStyle.Thin
        Row_Style_Left.BottomBorderColor = HSSFColor.Black.Index
        Row_Style_Left.LeftBorderColor = HSSFColor.Black.Index
        Row_Style_Left.RightBorderColor = HSSFColor.Black.Index
        Row_Style_Left.TopBorderColor = HSSFColor.Black.Index
        Row_Style_Left.VerticalAlignment = VerticalAlignment.Center

        Dim Row_Style_Left_bg As ICellStyle = hssfworkbook.CreateCellStyle()
        Row_Style_Left_bg.SetFont(Row_Font)
        Row_Style_Left_bg.FillPattern = FillPattern.SparseDots
        Row_Style_Left_bg.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index
        Row_Style_Left_bg.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index
        Row_Style_Left_bg.BorderBottom = BorderStyle.Thin
        Row_Style_Left_bg.BorderLeft = BorderStyle.Thin
        Row_Style_Left_bg.BorderRight = BorderStyle.Thin
        Row_Style_Left_bg.BorderTop = BorderStyle.Thin
        Row_Style_Left_bg.BottomBorderColor = HSSFColor.Black.Index
        Row_Style_Left_bg.LeftBorderColor = HSSFColor.Black.Index
        Row_Style_Left_bg.RightBorderColor = HSSFColor.Black.Index
        Row_Style_Left_bg.TopBorderColor = HSSFColor.Black.Index
        Row_Style_Left_bg.VerticalAlignment = VerticalAlignment.Center

        Dim Row_Font_BOLD = hssfworkbook.CreateFont()
        Row_Font_BOLD.FontName = "Tahoma"
        Row_Font_BOLD.FontHeightInPoints = 10
        Row_Font_BOLD.Boldweight = FontBoldWeight.Bold

        Dim Row_Style_BOLD As ICellStyle = hssfworkbook.CreateCellStyle()
        Row_Style_BOLD.SetFont(Row_Font_BOLD)

        Using dc As New DataClasses_CPSExt
            Dim _Insurer = (From c In dc.tblNoticeDetails _
                         Where c.NoticeID.Equals(NotificationID) _
                         And (c.f14.Equals("1") Or c.f14.Equals("2")) _
                         Group By UWCode = c.f09, UWName = c.f10 _
                         Into g = Group _
                         Select UWCode, UWName, Clients = g.Count() _
                   ).ToList()

            For Each iInsurer In _Insurer

                Dim _ClientData = (From c In dc.tblNoticeDetails _
                         Where c.NoticeID.Equals(NotificationID) And c.f09.Equals(iInsurer.UWCode) _
                         And (c.f14.Equals("1") Or c.f14.Equals("2"))).ToList()

                '====================== data new sheet ====================
                Dim sheet1 As ISheet = hssfworkbook.CreateSheet(iInsurer.UWName)

                '================== create row ===================
                Dim j As Integer = 0

                '================== create column ===================
                Dim _fields = "No,Client Code,ผู้เอาประกันภัย,New,Policy No. หรือ เลขรับแจ้ง,หมายเลขตัวถัง,ทะเบียนรถ,รายการแก้ไข,รายการข้อมูลเดิม,รายการที่แก้ไขใหม่".Split(",")
                Dim frow As IRow = sheet1.CreateRow(j)
                For i = 0 To _fields.Count - 1
                    Dim cell = frow.CreateCell(i)
                    cell.SetCellValue(_fields(i))
                    cell.CellStyle = Header_Style

                    sheet1.SetColumnWidth(i, _fields(i).Length * 400)
                Next

                Dim itemNo As Integer = 0
                For Each _item In _ClientData
                    itemNo += 1
                    j += 1
                    Dim row As IRow = sheet1.CreateRow(j)
                    For i = 0 To _fields.Count - 1
                        row.CreateCell(i).CellStyle = Row_Style_Left
                    Next

                    '========================== Data =============================
                    row.GetCell(0).SetCellValue(itemNo) 'No
                    row.GetCell(1).SetCellValue(_item.Code) 'Client Code
                    row.GetCell(2).SetCellValue(_item.f01) 'ผู้เอาประกันภัย
                    row.GetCell(3).SetCellValue(_item.f02) 'New
                    row.GetCell(4).SetCellValue(_item.f03) 'Policy No. หรือ เลขรับแจ้ง
                    row.GetCell(5).SetCellValue(_item.f04) 'หมายเลขตัวถัง
                    row.GetCell(6).SetCellValue(_item.f05) 'ทะเบียนรถ
                    row.GetCell(7).SetCellValue(_item.f11) 'Amendment Details
                    row.GetCell(8).SetCellValue(_item.f12) 'รายการข้อมูลเดิม
                    row.GetCell(9).SetCellValue(_item.f13) 'รายการที่แก้ไขใหม่

                Next

            Next
        End Using



        Dim fs As New FileStream(filePath, FileMode.Create)
        hssfworkbook.Write(fs)
        fs.Close()
 


        Response.ClearContent()
        Response.AddHeader("content-disposition", "attachment; filename=" & fileName)
        Response.ContentType = "application/octet-stream"
        Response.WriteFile(filePath)
        Response.End()


    End Sub

    Private Sub WH_genxls(ByVal NotificationID As String, ByVal UWCode As String)

        Using dc As New DataClasses_CPSExt


            Dim hssfworkbook As New HSSFWorkbook()

            '=====================cell style ===================
            Dim Header_Font = hssfworkbook.CreateFont()
            Header_Font.FontName = "Tahoma"
            Header_Font.FontHeightInPoints = 10
            Header_Font.Boldweight = FontBoldWeight.Bold

            Dim Header_Style As ICellStyle = hssfworkbook.CreateCellStyle()
            Header_Style.SetFont(Header_Font)
            Header_Style.Alignment = HorizontalAlignment.Center
            Header_Style.FillPattern = FillPattern.SparseDots
            Header_Style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Lime.Index
            Header_Style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Lime.Index
            Header_Style.BorderBottom = BorderStyle.Thin
            Header_Style.BorderLeft = BorderStyle.Thin
            Header_Style.BorderRight = BorderStyle.Thin
            Header_Style.BorderTop = BorderStyle.Thin
            Header_Style.BottomBorderColor = HSSFColor.Black.Index
            Header_Style.LeftBorderColor = HSSFColor.Black.Index
            Header_Style.RightBorderColor = HSSFColor.Black.Index
            Header_Style.TopBorderColor = HSSFColor.Black.Index

            Dim Header_Style2 As ICellStyle = hssfworkbook.CreateCellStyle()
            Header_Style2.SetFont(Header_Font)
            Header_Style2.Alignment = HorizontalAlignment.Center
            Header_Style2.BorderBottom = BorderStyle.Thin
            Header_Style2.BorderLeft = BorderStyle.Thin
            Header_Style2.BorderRight = BorderStyle.Thin
            Header_Style2.BorderTop = BorderStyle.Thin
            Header_Style2.BottomBorderColor = HSSFColor.Black.Index
            Header_Style2.LeftBorderColor = HSSFColor.Black.Index
            Header_Style2.RightBorderColor = HSSFColor.Black.Index
            Header_Style2.TopBorderColor = HSSFColor.Black.Index

            Dim Header_Style3 As ICellStyle = hssfworkbook.CreateCellStyle()
            Header_Style3.SetFont(Header_Font)
            Header_Style3.Alignment = HorizontalAlignment.Center
            Header_Style3.FillPattern = FillPattern.SparseDots
            Header_Style3.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightYellow.Index
            Header_Style3.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.LightYellow.Index
            Header_Style3.BorderBottom = BorderStyle.Thin
            Header_Style3.BorderLeft = BorderStyle.Thin
            Header_Style3.BorderRight = BorderStyle.Thin
            Header_Style3.BorderTop = BorderStyle.Thin
            Header_Style3.BottomBorderColor = HSSFColor.Black.Index
            Header_Style3.LeftBorderColor = HSSFColor.Black.Index
            Header_Style3.RightBorderColor = HSSFColor.Black.Index
            Header_Style3.TopBorderColor = HSSFColor.Black.Index


            Dim Row_Font = hssfworkbook.CreateFont()
            Row_Font.FontName = "Tahoma"
            Row_Font.FontHeightInPoints = 8

            Dim Row_Style_Left As ICellStyle = hssfworkbook.CreateCellStyle()
            Row_Style_Left.SetFont(Row_Font)
            Row_Style_Left.BorderBottom = BorderStyle.Thin
            Row_Style_Left.BorderLeft = BorderStyle.Thin
            Row_Style_Left.BorderRight = BorderStyle.Thin
            Row_Style_Left.BorderTop = BorderStyle.Thin
            Row_Style_Left.BottomBorderColor = HSSFColor.Black.Index
            Row_Style_Left.LeftBorderColor = HSSFColor.Black.Index
            Row_Style_Left.RightBorderColor = HSSFColor.Black.Index
            Row_Style_Left.TopBorderColor = HSSFColor.Black.Index
            Row_Style_Left.VerticalAlignment = VerticalAlignment.Center

            Dim Row_Style_Left_bg As ICellStyle = hssfworkbook.CreateCellStyle()
            Row_Style_Left_bg.SetFont(Row_Font)
            Row_Style_Left_bg.FillPattern = FillPattern.SparseDots
            Row_Style_Left_bg.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index
            Row_Style_Left_bg.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index
            Row_Style_Left_bg.BorderBottom = BorderStyle.Thin
            Row_Style_Left_bg.BorderLeft = BorderStyle.Thin
            Row_Style_Left_bg.BorderRight = BorderStyle.Thin
            Row_Style_Left_bg.BorderTop = BorderStyle.Thin
            Row_Style_Left_bg.BottomBorderColor = HSSFColor.Black.Index
            Row_Style_Left_bg.LeftBorderColor = HSSFColor.Black.Index
            Row_Style_Left_bg.RightBorderColor = HSSFColor.Black.Index
            Row_Style_Left_bg.TopBorderColor = HSSFColor.Black.Index
            Row_Style_Left_bg.VerticalAlignment = VerticalAlignment.Center

            'Dim Row_Style_Center As CellStyle = hssfworkbook.CreateCellStyle()
            'Row_Style_Center.SetFont(Row_Font)
            'Row_Style_Center.Alignment = HorizontalAlignment.CENTER

            'Dim Row_Style_Right As CellStyle = hssfworkbook.CreateCellStyle()
            'Row_Style_Right.SetFont(Row_Font)
            'Row_Style_Right.Alignment = HorizontalAlignment.RIGHT


            Dim Row_Font_BOLD = hssfworkbook.CreateFont()
            Row_Font_BOLD.FontName = "Tahoma"
            Row_Font_BOLD.FontHeightInPoints = 10
            Row_Font_BOLD.Boldweight = FontBoldWeight.Bold

            Dim Row_Style_BOLD As ICellStyle = hssfworkbook.CreateCellStyle()
            Row_Style_BOLD.SetFont(Row_Font_BOLD)
            'Row_Style_Right_BOLD.Alignment = HorizontalAlignment.RIGHT


            'Dim Row_Style_Right_Float As HSSFCellStyle = hssfworkbook.CreateCellStyle
            'Row_Style_Right_Float.SetFont(Row_Font)
            'Row_Style_Right_Float.Alignment = HorizontalAlignment.RIGHT
            'Row_Style_Right_Float.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,#0.0")

            'Dim Row_Style_Right_Float_Bold As HSSFCellStyle = hssfworkbook.CreateCellStyle
            'Row_Style_Right_Float_Bold.SetFont(Row_Font_BOLD)
            'Row_Style_Right_Float_Bold.Alignment = HorizontalAlignment.RIGHT
            'Row_Style_Right_Float_Bold.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,#0.0")
            Dim _ClientData = (From c In dc.tblNoticeDetails _
                         Where c.NoticeID.Equals(NotificationID) And c.f09.Equals(UWCode) _
                         And (c.f14.Equals("1") Or c.f14.Equals("2")) _
                         ).ToList()
            Dim _InsurerName As String = _ClientData(0).f10
            '====================== data new sheet ====================
            Dim sheet1 As ISheet = hssfworkbook.CreateSheet(_InsurerName)

            '================== create row ===================
            Dim j As Integer = 0
            'No
            'Client Code
            'ผู้เอาประกันภัย
            'New
            'Policy No. หรือ เลขรับแจ้ง
            'หมายเลขตัวถัง/ทะเบียนรถ
            'SumInsure
            'Beneficiary
            'Insurer
            'ShowRoom
            'Amendment Details
            'รายการข้อมูลเดิม
            'รายการที่แก้ไขใหม่

            '================== create column ===================
            Dim _fields = "No,Client Code,ผู้เอาประกันภัย,New,Policy No. หรือ เลขรับแจ้ง,หมายเลขตัวถัง,ทะเบียนรถ,รายการแก้ไข,รายการข้อมูลเดิม,รายการที่แก้ไขใหม่".Split(",")
            Dim frow As IRow = sheet1.CreateRow(j)
            For i = 0 To _fields.Count - 1
                Dim cell = frow.CreateCell(i)
                cell.SetCellValue(_fields(i))
                cell.CellStyle = Header_Style

                sheet1.SetColumnWidth(i, _fields(i).Length * 400)
            Next





            Dim itemNo As Integer = 0
            For Each _item In _ClientData
                itemNo += 1
                j += 1
                Dim row As IRow = sheet1.CreateRow(j)
                For i = 0 To _fields.Count - 1
                    row.CreateCell(i).CellStyle = Row_Style_Left
                Next

                '========================== Data =============================
                row.GetCell(0).SetCellValue(itemNo) 'No
                row.GetCell(1).SetCellValue(_item.Code) 'Client Code
                row.GetCell(2).SetCellValue(_item.f01) 'ผู้เอาประกันภัย
                row.GetCell(3).SetCellValue(_item.f02) 'New
                row.GetCell(4).SetCellValue(_item.f03) 'Policy No. หรือ เลขรับแจ้ง
                row.GetCell(5).SetCellValue(_item.f04) 'หมายเลขตัวถัง
                row.GetCell(6).SetCellValue(_item.f05) 'ทะเบียนรถ
                'row.GetCell(7).SetCellValue(_item.f07) 'Beneficiary
                'row.GetCell(6).SetCellValue(_item.f10) 'Insurer
                'row.GetCell(9).SetCellValue(_item.f09) 'ShowRoom
                row.GetCell(7).SetCellValue(_item.f11) 'Amendment Details
                row.GetCell(8).SetCellValue(_item.f12) 'รายการข้อมูลเดิม
                row.GetCell(9).SetCellValue(_item.f13) 'รายการที่แก้ไขใหม่

                'sheet1.AddMergedRegion(New CellRangeAddress(j - tmp.Count, j - 1, 0, 0))
                'sheet1.AddMergedRegion(New CellRangeAddress(j - tmp.Count, j - 1, 1, 1))
                'sheet1.AddMergedRegion(New CellRangeAddress(j - tmp.Count, j - 1, 2, 2))
            Next




            '================== init  sheet property =================
            Dim _data = (From c In dc.tblNoticeHeaders Where c.NoticeID.Equals(NotificationID)).FirstOrDefault()

            Dim _filename = String.Format("{0}.xls", System.Guid.NewGuid())
            Dim _fileDataPath = String.Format("{0}\{1}", Server.MapPath("~/upload"), _filename)
            Dim fs As New FileStream(_fileDataPath, FileMode.Create)
            hssfworkbook.Write(fs)
            fs.Close()


            'Try

            Dim _displayName As String = ""
            Dim _title As String = ""
            Dim _department As String = ""
            Dim _company As String = ""
            Dim _streetAddress As String = ""
            Dim _telephoneNumber As String = ""
            Dim _facsimileTelephoneNumber As String = ""
            Dim _mail As String = ""

            Dim _agent = (From c In dc.tblNoticeMailContacts Where c.Code.Equals(UWCode) And c.NoticeCode.Equals(_NoticeCode)).FirstOrDefault()
            Dim strMailFrom As String = "" '"nissan2@asia.lockton.com"
            Dim strMailCC As String = ""

            Dim strMailTo As String = ""
            '"siriwimol@asia.lockton.com,dusit@asia.lockton.com,parichat@asia.lockton.com" '_agent.Mailto '  Replace("dusit@asia.lockton.com,parichat@asia.lockton.com", ";", ",") '

            Dim strSubject As String = ""
            Dim strMessage As New StringBuilder()


            Using dc_portal = New DataClasses_PortalDataContextExt
                Dim _user = (From c In dc_portal.v_ads_actives Where c.sAMAccountName.Equals(HttpContext.Current.User.Identity.Name)).FirstOrDefault()

                With _user
                    
                    _displayName = .displayName
                    _title = .title
                    _department = .department
                    _company = .company
                    _streetAddress = .streetaddress
                    _telephoneNumber = .telephoneNumber
                    _facsimileTelephoneNumber = .facsimileTelephoneNumber
                    _mail = .mail
                End With
              
                Dim _mailNotification = (From c In dc_portal.MailNotifications Where c.Code.Equals(_NoticeCode)).FirstOrDefault()

                'รายการแก้ไข Amendment ประจำวันที่ {date}  จำนวน {items} รายการ
                strSubject = _mailNotification.MailSubject.Replace("{insurer}", _InsurerName)

                'strMessage.AppendLine("<!DOCTYPE html><html><body>")
                'strMessage.AppendLine(Server.HtmlDecode(_mailNotification.MailBody).Replace("{insurer}", _InsurerName))
                'strMessage.AppendLine("</body></html>")

                Dim _MailBody As String = Server.HtmlDecode(_mailNotification.MailBody).Replace("{insurer}", _InsurerName)

                _MailBody = _MailBody.Replace("{displayName}", _displayName)
                _MailBody = _MailBody.Replace("{title}", _title)
                _MailBody = _MailBody.Replace("{department}", _department)
                _MailBody = _MailBody.Replace("{company}", _company)
                _MailBody = _MailBody.Replace("{streetAddress}", _streetAddress)
                _MailBody = _MailBody.Replace("{telephoneNumber}", _telephoneNumber)
                _MailBody = _MailBody.Replace("{facsimileTelephoneNumber}", _facsimileTelephoneNumber)
                _MailBody = _MailBody.Replace("{mail}", _mail)

                strMessage.AppendLine(_MailBody)



                strMailFrom = _mail
                strMailCC = _mailNotification.MailCC & ";" & _mail
                strMailTo = _agent.MailTo


                'strMailTo = "dusit@asia.lockton.com;laddawan@asia.lockton.com;ratika@asia.lockton.com;anat@asia.lockton.com" '_agent.Mailto
                'strMailTo = "dusit@asia.lockton.com;ratika@asia.lockton.com"
            End Using


            Dim MySmtpClient As New System.Net.Mail.SmtpClient(ConfigurationSettings.AppSettings("smtp"))
            'MySmtpClient.Credentials = New System.Net.NetworkCredential("Your-username", "Your-password")


            'Dim msg As New System.Net.Mail.MailMessage(strMailFrom, strMailTo, strSubject, strMessage.ToString())
            Dim msg As New System.Net.Mail.MailMessage()


            '===========================================================
            Dim bodyHTML As String = "<html><body>" & strMessage.ToString() & "<span style='font-size:16.0pt;font-family:Angsana New,serif;color:green'><img src='cid:LOGO_IMAGE2' alt='Logo' /><i>Please consider the environment before printing this e-mail.</i></span></body></html>"
            Dim alternateView As AlternateView = alternateView.CreateAlternateViewFromString(bodyHTML, Nothing, "text/html")

            'Dim path_to_the_image_file1 As String = String.Format("{0}\{1}", Server.MapPath("~/images"), "maillockton30.jpg")
            Dim path_to_the_image_file2 As String = String.Format("{0}\{1}", Server.MapPath("~/images"), "mailsmallpicture.jpg")

            'Create the LinkedResource here
            'Dim logo1 As New LinkedResource(path_to_the_image_file1, "image/jpeg")  'Content Type is set as image/jpeg
            'logo1.ContentId = "LOGO_IMAGE1"
            'logo1.TransferEncoding = Net.Mime.TransferEncoding.Base64
            'alternateView.LinkedResources.Add(logo1)

            Dim logo2 As New LinkedResource(path_to_the_image_file2, "image/jpeg")  'Content Type is set as image/jpeg
            logo2.ContentId = "LOGO_IMAGE2"
            logo2.TransferEncoding = Net.Mime.TransferEncoding.Base64
            alternateView.LinkedResources.Add(logo2)
            msg.AlternateViews.Add(alternateView)
            '===========================================================

            msg.Subject = strSubject 'Subject
            msg.Body = Nothing

            msg.From = New MailAddress(strMailFrom) 'Mail From

            Dim _MailTo = strMailTo.Split(";") 'Mail To
            For Each item In _MailTo
                If Not String.IsNullOrEmpty(item.Trim()) Then msg.To.Add(item)
            Next

            Dim _MailCC = strMailCC.Split(";") 'Mail CC
            For Each item In _MailCC
                If Not String.IsNullOrEmpty(item.Trim()) Then msg.CC.Add(item)
            Next

            msg.BodyEncoding = Encoding.UTF8
            msg.IsBodyHtml = True
            msg.Priority = Net.Mail.MailPriority.High

            Dim att_data = New Attachment(_fileDataPath)
            att_data.Name = String.Format("Endorsement_{0}.xls", DateTime.Now.ToString("dd.MM.yyyy"))
            msg.Attachments.Add(att_data)

            MySmtpClient.Send(msg)


            Alert.Show("Send")
            'Catch ex As Exception
            '    Throw ex
            'End Try

        End Using


 


    End Sub

    Private Sub ExportData_Amend(ByVal _NotificationID As String)
        Using dc As New DataClasses_CPSExt

            Dim _OwnerData = (From c In dc.tblNoticeHeaders Where c.NoticeID.Equals(_NotificationID)).FirstOrDefault()
            Dim _OwnerName As String = ""
             

            Dim hssfworkbook As New HSSFWorkbook()

            '=====================cell style ===================
            Dim Header_Font = hssfworkbook.CreateFont()
            Header_Font.FontName = "Tahoma"
            Header_Font.FontHeightInPoints = 10
            Header_Font.Boldweight = FontBoldWeight.Bold

            Dim Header_Style As ICellStyle = hssfworkbook.CreateCellStyle()
            Header_Style.SetFont(Header_Font)
            Header_Style.Alignment = HorizontalAlignment.Center
            Header_Style.FillPattern = FillPattern.SparseDots
            Header_Style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightGreen.Index
            Header_Style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.LightGreen.Index
            Header_Style.BorderBottom = BorderStyle.Thin
            Header_Style.BorderLeft = BorderStyle.Thin
            Header_Style.BorderRight = BorderStyle.Thin
            Header_Style.BorderTop = BorderStyle.Thin
            Header_Style.BottomBorderColor = HSSFColor.Black.Index
            Header_Style.LeftBorderColor = HSSFColor.Black.Index
            Header_Style.RightBorderColor = HSSFColor.Black.Index
            Header_Style.TopBorderColor = HSSFColor.Black.Index

            Dim Header_Style2 As ICellStyle = hssfworkbook.CreateCellStyle()
            Header_Style2.SetFont(Header_Font)
            Header_Style2.Alignment = HorizontalAlignment.Center
            Header_Style2.BorderBottom = BorderStyle.Thin
            Header_Style2.BorderLeft = BorderStyle.Thin
            Header_Style2.BorderRight = BorderStyle.Thin
            Header_Style2.BorderTop = BorderStyle.Thin
            Header_Style2.BottomBorderColor = HSSFColor.Black.Index
            Header_Style2.LeftBorderColor = HSSFColor.Black.Index
            Header_Style2.RightBorderColor = HSSFColor.Black.Index
            Header_Style2.TopBorderColor = HSSFColor.Black.Index

            Dim Header_Style3 As ICellStyle = hssfworkbook.CreateCellStyle()
            Header_Style3.SetFont(Header_Font)
            Header_Style3.Alignment = HorizontalAlignment.Center
            Header_Style3.FillPattern = FillPattern.SparseDots
            Header_Style3.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightYellow.Index
            Header_Style3.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.LightYellow.Index
            Header_Style3.BorderBottom = BorderStyle.Thin
            Header_Style3.BorderLeft = BorderStyle.Thin
            Header_Style3.BorderRight = BorderStyle.Thin
            Header_Style3.BorderTop = BorderStyle.Thin
            Header_Style3.BottomBorderColor = HSSFColor.Black.Index
            Header_Style3.LeftBorderColor = HSSFColor.Black.Index
            Header_Style3.RightBorderColor = HSSFColor.Black.Index
            Header_Style3.TopBorderColor = HSSFColor.Black.Index


            Dim Row_Font = hssfworkbook.CreateFont()
            Row_Font.FontName = "Tahoma"
            Row_Font.FontHeightInPoints = 8

            Dim Row_Style_Left As ICellStyle = hssfworkbook.CreateCellStyle()
            Row_Style_Left.SetFont(Row_Font)
            Row_Style_Left.BorderBottom = BorderStyle.Thin
            Row_Style_Left.BorderLeft = BorderStyle.Thin
            Row_Style_Left.BorderRight = BorderStyle.Thin
            Row_Style_Left.BorderTop = BorderStyle.Thin
            Row_Style_Left.BottomBorderColor = HSSFColor.Black.Index
            Row_Style_Left.LeftBorderColor = HSSFColor.Black.Index
            Row_Style_Left.RightBorderColor = HSSFColor.Black.Index
            Row_Style_Left.TopBorderColor = HSSFColor.Black.Index
            Row_Style_Left.VerticalAlignment = VerticalAlignment.Center

            Dim Row_Style_Left_bg As ICellStyle = hssfworkbook.CreateCellStyle()
            Row_Style_Left_bg.SetFont(Row_Font)
            Row_Style_Left_bg.FillPattern = FillPattern.SparseDots
            Row_Style_Left_bg.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index
            Row_Style_Left_bg.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index
            Row_Style_Left_bg.BorderBottom = BorderStyle.Thin
            Row_Style_Left_bg.BorderLeft = BorderStyle.Thin
            Row_Style_Left_bg.BorderRight = BorderStyle.Thin
            Row_Style_Left_bg.BorderTop = BorderStyle.Thin
            Row_Style_Left_bg.BottomBorderColor = HSSFColor.Black.Index
            Row_Style_Left_bg.LeftBorderColor = HSSFColor.Black.Index
            Row_Style_Left_bg.RightBorderColor = HSSFColor.Black.Index
            Row_Style_Left_bg.TopBorderColor = HSSFColor.Black.Index
            Row_Style_Left_bg.VerticalAlignment = VerticalAlignment.Center

            'Dim Row_Style_Center As CellStyle = hssfworkbook.CreateCellStyle()
            'Row_Style_Center.SetFont(Row_Font)
            'Row_Style_Center.Alignment = HorizontalAlignment.CENTER

            'Dim Row_Style_Right As CellStyle = hssfworkbook.CreateCellStyle()
            'Row_Style_Right.SetFont(Row_Font)
            'Row_Style_Right.Alignment = HorizontalAlignment.RIGHT


            Dim Row_Font_BOLD = hssfworkbook.CreateFont()
            Row_Font_BOLD.FontName = "Tahoma"
            Row_Font_BOLD.FontHeightInPoints = 10
            Row_Font_BOLD.Boldweight = FontBoldWeight.Bold

            Dim Row_Style_BOLD As ICellStyle = hssfworkbook.CreateCellStyle()
            Row_Style_BOLD.SetFont(Row_Font_BOLD)
            'Row_Style_Right_BOLD.Alignment = HorizontalAlignment.RIGHT


            'Dim Row_Style_Right_Float As HSSFCellStyle = hssfworkbook.CreateCellStyle
            'Row_Style_Right_Float.SetFont(Row_Font)
            'Row_Style_Right_Float.Alignment = HorizontalAlignment.RIGHT
            'Row_Style_Right_Float.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,#0.0")

            'Dim Row_Style_Right_Float_Bold As HSSFCellStyle = hssfworkbook.CreateCellStyle
            'Row_Style_Right_Float_Bold.SetFont(Row_Font_BOLD)
            'Row_Style_Right_Float_Bold.Alignment = HorizontalAlignment.RIGHT
            'Row_Style_Right_Float_Bold.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,#0.0")


            '====================== data new sheet ====================
            Dim sheet1 As ISheet = hssfworkbook.CreateSheet(MainRegionPanel.Title)

            '================== create row ===================
            Dim j As Integer = 0
            Dim row1 As IRow = sheet1.CreateRow(j)
            row1.CreateCell(0).CellStyle = Row_Style_BOLD
            row1.GetCell(0).SetCellValue("TO.")
            row1.CreateCell(1).CellStyle = Row_Style_BOLD
            row1.GetCell(1).SetCellValue("คุณนันทิยา (บิ๋ม)   ชั้น 35")
            '================== create row ===================
            j += 1
            Dim row2 As IRow = sheet1.CreateRow(j)
            row2.CreateCell(0).CellStyle = Row_Style_BOLD
            row2.GetCell(0).SetCellValue("FR.")
            row2.CreateCell(1).CellStyle = Row_Style_BOLD
            row2.GetCell(1).SetCellValue(_OwnerName & "  APD-Customer Service ชั้น 4")
            '================== create row ===================
            j += 1
            Dim row3 As IRow = sheet1.CreateRow(j)
            '================== create row ===================
            j += 1
            Dim row4 As IRow = sheet1.CreateRow(j)
            row4.CreateCell(0).CellStyle = Row_Style_BOLD
            row4.GetCell(0).SetCellValue("CLIENT AMENDMENT LIST FORM")
            '================== create row ===================
            j += 1
            Dim row5 As IRow = sheet1.CreateRow(j)
            row5.CreateCell(0).CellStyle = Header_Style
            row5.GetCell(0).SetCellValue("Div")
            row5.CreateCell(1).CellStyle = Header_Style
            row5.GetCell(1).SetCellValue("")
            row5.CreateCell(2).CellStyle = Header_Style
            row5.GetCell(2).SetCellValue("Request By")
            row5.CreateCell(3).CellStyle = Header_Style
            row5.GetCell(3).SetCellValue("Approved By")
            row5.CreateCell(4).CellStyle = Header_Style
            row5.GetCell(4).SetCellValue("Review By")
            row5.CreateCell(5).CellStyle = Header_Style
            row5.GetCell(5).SetCellValue("Completed By")

            '================== create row ===================
            j += 1
            Dim row6 As IRow = sheet1.CreateRow(j)
            row6.CreateCell(0).CellStyle = Header_Style
            row6.GetCell(0).SetCellValue("APD")
            row6.CreateCell(1).CellStyle = Header_Style2
            row6.GetCell(1).SetCellValue("")
            row6.CreateCell(2).CellStyle = Header_Style3
            row6.GetCell(2).SetCellValue(_OwnerName)
            row6.CreateCell(3).CellStyle = Header_Style3
            row6.GetCell(3).SetCellValue("Laddawan")
            row6.CreateCell(4).CellStyle = Header_Style2
            row6.GetCell(4).SetCellValue("")
            row6.CreateCell(5).CellStyle = Header_Style2
            row6.GetCell(5).SetCellValue("")
            '================== create row ===================
            j += 1
            Dim row7 As IRow = sheet1.CreateRow(j)
            row7.CreateCell(0).CellStyle = Header_Style
            row7.GetCell(0).SetCellValue("Date")
            row7.CreateCell(1).CellStyle = Header_Style2
            row7.GetCell(1).SetCellValue("")
            row7.CreateCell(2).CellStyle = Header_Style3
            row7.GetCell(2).SetCellValue(DateTime.Now.ToString("dd/MM/yyyy"))
            row7.CreateCell(3).CellStyle = Header_Style3
            row7.GetCell(3).SetCellValue(DateTime.Now.ToString("dd/MM/yyyy"))
            row7.CreateCell(4).CellStyle = Header_Style2
            row7.GetCell(4).SetCellValue("")
            row7.CreateCell(5).CellStyle = Header_Style2
            row7.GetCell(5).SetCellValue("")
            '================== create row ===================
            j += 1
            '================== create column ===================
            Dim _fields = "Item,Client Code,Client Name (ชื่อลูกค้า),Ament Subjects  (เรื่องที่ต้องการแก้ไข),Original Details (ข้อมูลเดิม),Amendment Details (แก้ไขเป็น)".Split(",")
            Dim frow As IRow = sheet1.CreateRow(j)
            For i = 0 To _fields.Count - 1
                Dim cell = frow.CreateCell(i)
                cell.SetCellValue(_fields(i))
                cell.CellStyle = Header_Style
            Next

            '================== init Column Length =====================
            Dim _item_f01_Length = frow.GetCell(0).StringCellValue.Length
            Dim _item_f02_Length = frow.GetCell(1).StringCellValue.Length
            Dim _item_f03_Length = frow.GetCell(2).StringCellValue.Length
            Dim _item_f04_Length = frow.GetCell(3).StringCellValue.Length
            Dim _item_f05_Length = frow.GetCell(4).StringCellValue.Length
            Dim _item_f06_Length = frow.GetCell(5).StringCellValue.Length

            sheet1.SetColumnWidth(0, _item_f01_Length * 400)
            sheet1.SetColumnWidth(1, _item_f02_Length * 400)
            sheet1.SetColumnWidth(2, _item_f03_Length * 400)
            sheet1.SetColumnWidth(3, _item_f04_Length * 400)
            sheet1.SetColumnWidth(4, _item_f05_Length * 400)
            sheet1.SetColumnWidth(5, _item_f06_Length * 400)


            '================== create row ===================
            j += 1
            Dim _ClientData = (From c In dc.tblNoticeDetails _
                          Where c.NoticeID.Equals(_NotificationID) _
                          And (c.f14.Equals("3") Or c.f14.Equals("2"))).ToList()

            Dim _ClientGrp = (From c In _ClientData _
                    Group By clientcode = c.Code Into MyGroup = Group _
                    Select clientcode).ToList()

            Dim itemNo As Integer = 0
            For Each _ClientCode As String In _ClientGrp
                itemNo += 1
                Dim tmp = (From c In _ClientData Where c.Code.Equals(_ClientCode)).ToList()
                Dim _groupnumber As Integer = 0

                For Each _item In tmp

                    Dim row As IRow = sheet1.CreateRow(j)
                    '========================== Data =============================
                    If itemNo Mod 2 > 0 Then
                        row.CreateCell(0).CellStyle = Row_Style_Left
                        row.CreateCell(1).CellStyle = Row_Style_Left
                        row.CreateCell(2).CellStyle = Row_Style_Left
                        row.CreateCell(3).CellStyle = Row_Style_Left
                        row.CreateCell(4).CellStyle = Row_Style_Left
                        row.CreateCell(5).CellStyle = Row_Style_Left
                    Else
                        row.CreateCell(0).CellStyle = Row_Style_Left_bg
                        row.CreateCell(1).CellStyle = Row_Style_Left_bg
                        row.CreateCell(2).CellStyle = Row_Style_Left_bg
                        row.CreateCell(3).CellStyle = Row_Style_Left_bg
                        row.CreateCell(4).CellStyle = Row_Style_Left_bg
                        row.CreateCell(5).CellStyle = Row_Style_Left_bg
                    End If

                    '"Item,Client Code,Client Name (ชื่อลูกค้า),Ament Subjects  (เรื่องที่ต้องการแก้ไข),Original Details (ข้อมูลเดิม),Amendment Details (แก้ไขเป็น)"
                    If _groupnumber = 0 Then
                        row.GetCell(0).SetCellValue(itemNo)
                        row.GetCell(1).SetCellValue(_item.Code.Trim()) 'Client Code
                        row.GetCell(2).SetCellValue(_item.f01.Trim()) 'Client Name (ชื่อลูกค้า)
                    End If

                    row.GetCell(3).SetCellValue(_item.f11.Trim()) 'Ament Subjects  (เรื่องที่ต้องการแก้ไข)
                    If Not _item.f12 Is Nothing Then row.GetCell(4).SetCellValue(_item.f12.Trim()) 'Original Details (ข้อมูลเดิม)
                    row.GetCell(5).SetCellValue(_item.f13.Trim()) 'Amendment Details (แก้ไขเป็น)

                    _groupnumber += 1
                    j += 1
                Next

                'Dim column1 = New CellRangeAddress(j - tmp.Count, j - 1, 1, 1)
                'Dim column2 = New CellRangeAddress(j - tmp.Count, j - 1, 1, 1)
                'Dim column3 = New CellRangeAddress(j - tmp.Count, j - 1, 1, 1)
                sheet1.AddMergedRegion(New CellRangeAddress(j - tmp.Count, j - 1, 0, 0))
                sheet1.AddMergedRegion(New CellRangeAddress(j - tmp.Count, j - 1, 1, 1))
                sheet1.AddMergedRegion(New CellRangeAddress(j - tmp.Count, j - 1, 2, 2))
            Next




            '================== init  sheet property =================
            Dim _data = (From c In dc.tblNoticeHeaders Where c.NoticeID.Equals(_NotificationID)).FirstOrDefault()

            Dim _filename = String.Format("{0}.xls", System.Guid.NewGuid().ToString())
            Dim _fileDataPath = String.Format("{0}\{1}", Server.MapPath("~/upload"), _filename)
            Dim fs As New FileStream(_fileDataPath, FileMode.Create)
            hssfworkbook.Write(fs)
            fs.Close()




            Response.ClearContent()
            Response.AddHeader("content-disposition", "attachment; filename=" & _filename)
            Response.ContentType = "application/octet-stream"
            Response.WriteFile(_fileDataPath)
            Response.End()

        End Using


 

    End Sub

    Private Sub WH_genxls_Amend(ByVal NotificationID As String)

        Using dc As New DataClasses_CPSExt
            Dim _displayName As String = ""
            Dim _title As String = ""
            Dim _department As String = ""
            Dim _company As String = ""
            Dim _streetAddress As String = ""
            Dim _telephoneNumber As String = ""
            Dim _facsimileTelephoneNumber As String = ""
            Dim _mail As String = ""


            Using dc_portal = New DataClasses_PortalDataContextExt
                Dim _user = (From c In dc_portal.v_ads_actives Where c.sAMAccountName.Equals(HttpContext.Current.User.Identity.Name)).FirstOrDefault()

                With _user
                    _displayName = .displayName
                    _title = .title
                    _department = .department
                    _company = .company
                    _streetAddress = .streetaddress
                    _telephoneNumber = .telephoneNumber
                    _facsimileTelephoneNumber = .facsimileTelephoneNumber
                    _mail = .mail
                End With
            End Using

            Dim hssfworkbook As New HSSFWorkbook()

            '=====================cell style ===================
            Dim Header_Font = hssfworkbook.CreateFont()
            Header_Font.FontName = "Tahoma"
            Header_Font.FontHeightInPoints = 10
            Header_Font.Boldweight = FontBoldWeight.Bold

            Dim Header_Style As ICellStyle = hssfworkbook.CreateCellStyle()
            Header_Style.SetFont(Header_Font)
            Header_Style.Alignment = HorizontalAlignment.Center
            Header_Style.FillPattern = FillPattern.SparseDots
            Header_Style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightGreen.Index
            Header_Style.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.LightGreen.Index
            Header_Style.BorderBottom = BorderStyle.Thin
            Header_Style.BorderLeft = BorderStyle.Thin
            Header_Style.BorderRight = BorderStyle.Thin
            Header_Style.BorderTop = BorderStyle.Thin
            Header_Style.BottomBorderColor = HSSFColor.Black.Index
            Header_Style.LeftBorderColor = HSSFColor.Black.Index
            Header_Style.RightBorderColor = HSSFColor.Black.Index
            Header_Style.TopBorderColor = HSSFColor.Black.Index

            Dim Header_Style2 As ICellStyle = hssfworkbook.CreateCellStyle()
            Header_Style2.SetFont(Header_Font)
            Header_Style2.Alignment = HorizontalAlignment.Center
            Header_Style2.BorderBottom = BorderStyle.Thin
            Header_Style2.BorderLeft = BorderStyle.Thin
            Header_Style2.BorderRight = BorderStyle.Thin
            Header_Style2.BorderTop = BorderStyle.Thin
            Header_Style2.BottomBorderColor = HSSFColor.Black.Index
            Header_Style2.LeftBorderColor = HSSFColor.Black.Index
            Header_Style2.RightBorderColor = HSSFColor.Black.Index
            Header_Style2.TopBorderColor = HSSFColor.Black.Index

            Dim Header_Style3 As ICellStyle = hssfworkbook.CreateCellStyle()
            Header_Style3.SetFont(Header_Font)
            Header_Style3.Alignment = HorizontalAlignment.Center
            Header_Style3.FillPattern = FillPattern.SparseDots
            Header_Style3.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.LightYellow.Index
            Header_Style3.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.LightYellow.Index
            Header_Style3.BorderBottom = BorderStyle.Thin
            Header_Style3.BorderLeft = BorderStyle.Thin
            Header_Style3.BorderRight = BorderStyle.Thin
            Header_Style3.BorderTop = BorderStyle.Thin
            Header_Style3.BottomBorderColor = HSSFColor.Black.Index
            Header_Style3.LeftBorderColor = HSSFColor.Black.Index
            Header_Style3.RightBorderColor = HSSFColor.Black.Index
            Header_Style3.TopBorderColor = HSSFColor.Black.Index


            Dim Row_Font = hssfworkbook.CreateFont()
            Row_Font.FontName = "Tahoma"
            Row_Font.FontHeightInPoints = 8

            Dim Row_Style_Left As ICellStyle = hssfworkbook.CreateCellStyle()
            Row_Style_Left.SetFont(Row_Font)
            Row_Style_Left.BorderBottom = BorderStyle.Thin
            Row_Style_Left.BorderLeft = BorderStyle.Thin
            Row_Style_Left.BorderRight = BorderStyle.Thin
            Row_Style_Left.BorderTop = BorderStyle.Thin
            Row_Style_Left.BottomBorderColor = HSSFColor.Black.Index
            Row_Style_Left.LeftBorderColor = HSSFColor.Black.Index
            Row_Style_Left.RightBorderColor = HSSFColor.Black.Index
            Row_Style_Left.TopBorderColor = HSSFColor.Black.Index
            Row_Style_Left.VerticalAlignment = VerticalAlignment.Center

            Dim Row_Style_Left_bg As ICellStyle = hssfworkbook.CreateCellStyle()
            Row_Style_Left_bg.SetFont(Row_Font)
            Row_Style_Left_bg.FillPattern = FillPattern.SparseDots
            Row_Style_Left_bg.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index
            Row_Style_Left_bg.FillBackgroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index
            Row_Style_Left_bg.BorderBottom = BorderStyle.Thin
            Row_Style_Left_bg.BorderLeft = BorderStyle.Thin
            Row_Style_Left_bg.BorderRight = BorderStyle.Thin
            Row_Style_Left_bg.BorderTop = BorderStyle.Thin
            Row_Style_Left_bg.BottomBorderColor = HSSFColor.Black.Index
            Row_Style_Left_bg.LeftBorderColor = HSSFColor.Black.Index
            Row_Style_Left_bg.RightBorderColor = HSSFColor.Black.Index
            Row_Style_Left_bg.TopBorderColor = HSSFColor.Black.Index
            Row_Style_Left_bg.VerticalAlignment = VerticalAlignment.Center

            'Dim Row_Style_Center As CellStyle = hssfworkbook.CreateCellStyle()
            'Row_Style_Center.SetFont(Row_Font)
            'Row_Style_Center.Alignment = HorizontalAlignment.CENTER

            'Dim Row_Style_Right As CellStyle = hssfworkbook.CreateCellStyle()
            'Row_Style_Right.SetFont(Row_Font)
            'Row_Style_Right.Alignment = HorizontalAlignment.RIGHT


            Dim Row_Font_BOLD = hssfworkbook.CreateFont()
            Row_Font_BOLD.FontName = "Tahoma"
            Row_Font_BOLD.FontHeightInPoints = 10
            Row_Font_BOLD.Boldweight = FontBoldWeight.Bold

            Dim Row_Style_BOLD As ICellStyle = hssfworkbook.CreateCellStyle()
            Row_Style_BOLD.SetFont(Row_Font_BOLD)
            'Row_Style_Right_BOLD.Alignment = HorizontalAlignment.RIGHT


            'Dim Row_Style_Right_Float As HSSFCellStyle = hssfworkbook.CreateCellStyle
            'Row_Style_Right_Float.SetFont(Row_Font)
            'Row_Style_Right_Float.Alignment = HorizontalAlignment.RIGHT
            'Row_Style_Right_Float.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,#0.0")

            'Dim Row_Style_Right_Float_Bold As HSSFCellStyle = hssfworkbook.CreateCellStyle
            'Row_Style_Right_Float_Bold.SetFont(Row_Font_BOLD)
            'Row_Style_Right_Float_Bold.Alignment = HorizontalAlignment.RIGHT
            'Row_Style_Right_Float_Bold.DataFormat = HSSFDataFormat.GetBuiltinFormat("#,#0.0")


            '====================== data new sheet ====================
            Dim sheet1 As ISheet = hssfworkbook.CreateSheet(MainRegionPanel.Title)

            '================== create row ===================
            Dim j As Integer = 0
            Dim row1 As IRow = sheet1.CreateRow(j)
            row1.CreateCell(0).CellStyle = Row_Style_BOLD
            row1.GetCell(0).SetCellValue("TO.")
            row1.CreateCell(1).CellStyle = Row_Style_BOLD
            row1.GetCell(1).SetCellValue("คุณนันทิยา (บิ๋ม)   ชั้น 35")
            '================== create row ===================
            j += 1
            Dim row2 As IRow = sheet1.CreateRow(j)
            row2.CreateCell(0).CellStyle = Row_Style_BOLD
            row2.GetCell(0).SetCellValue("FR.")
            row2.CreateCell(1).CellStyle = Row_Style_BOLD
            row2.GetCell(1).SetCellValue(_displayName & "  APD-Customer Service ชั้น 4")
            '================== create row ===================
            j += 1
            Dim row3 As IRow = sheet1.CreateRow(j)
            '================== create row ===================
            j += 1
            Dim row4 As IRow = sheet1.CreateRow(j)
            row4.CreateCell(0).CellStyle = Row_Style_BOLD
            row4.GetCell(0).SetCellValue("CLIENT AMENDMENT LIST FORM")
            '================== create row ===================
            j += 1
            Dim row5 As IRow = sheet1.CreateRow(j)
            row5.CreateCell(0).CellStyle = Header_Style
            row5.GetCell(0).SetCellValue("Div")
            row5.CreateCell(1).CellStyle = Header_Style
            row5.GetCell(1).SetCellValue("")
            row5.CreateCell(2).CellStyle = Header_Style
            row5.GetCell(2).SetCellValue("Request By")
            row5.CreateCell(3).CellStyle = Header_Style
            row5.GetCell(3).SetCellValue("Approved By")
            row5.CreateCell(4).CellStyle = Header_Style
            row5.GetCell(4).SetCellValue("Review By")
            row5.CreateCell(5).CellStyle = Header_Style
            row5.GetCell(5).SetCellValue("Completed By")

            '================== create row ===================
            j += 1
            Dim row6 As IRow = sheet1.CreateRow(j)
            row6.CreateCell(0).CellStyle = Header_Style
            row6.GetCell(0).SetCellValue("APD")
            row6.CreateCell(1).CellStyle = Header_Style2
            row6.GetCell(1).SetCellValue("")
            row6.CreateCell(2).CellStyle = Header_Style3
            row6.GetCell(2).SetCellValue(_displayName)
            row6.CreateCell(3).CellStyle = Header_Style3
            row6.GetCell(3).SetCellValue("Laddawan")
            row6.CreateCell(4).CellStyle = Header_Style2
            row6.GetCell(4).SetCellValue("")
            row6.CreateCell(5).CellStyle = Header_Style2
            row6.GetCell(5).SetCellValue("")
            '================== create row ===================
            j += 1
            Dim row7 As IRow = sheet1.CreateRow(j)
            row7.CreateCell(0).CellStyle = Header_Style
            row7.GetCell(0).SetCellValue("Date")
            row7.CreateCell(1).CellStyle = Header_Style2
            row7.GetCell(1).SetCellValue("")
            row7.CreateCell(2).CellStyle = Header_Style3
            row7.GetCell(2).SetCellValue(DateTime.Now.ToString("dd/MM/yyyy"))
            row7.CreateCell(3).CellStyle = Header_Style3
            row7.GetCell(3).SetCellValue(DateTime.Now.ToString("dd/MM/yyyy"))
            row7.CreateCell(4).CellStyle = Header_Style2
            row7.GetCell(4).SetCellValue("")
            row7.CreateCell(5).CellStyle = Header_Style2
            row7.GetCell(5).SetCellValue("")
            '================== create row ===================
            j += 1
            '================== create column ===================
            Dim _fields = "Item,Client Code,Client Name (ชื่อลูกค้า),Ament Subjects  (เรื่องที่ต้องการแก้ไข),Original Details (ข้อมูลเดิม),Amendment Details (แก้ไขเป็น)".Split(",")
            Dim frow As IRow = sheet1.CreateRow(j)
            For i = 0 To _fields.Count - 1
                Dim cell = frow.CreateCell(i)
                cell.SetCellValue(_fields(i))
                cell.CellStyle = Header_Style
            Next

            '================== init Column Length =====================
            Dim _item_f01_Length = frow.GetCell(0).StringCellValue.Length
            Dim _item_f02_Length = frow.GetCell(1).StringCellValue.Length
            Dim _item_f03_Length = frow.GetCell(2).StringCellValue.Length
            Dim _item_f04_Length = frow.GetCell(3).StringCellValue.Length
            Dim _item_f05_Length = frow.GetCell(4).StringCellValue.Length
            Dim _item_f06_Length = frow.GetCell(5).StringCellValue.Length

            sheet1.SetColumnWidth(0, _item_f01_Length * 400)
            sheet1.SetColumnWidth(1, _item_f02_Length * 400)
            sheet1.SetColumnWidth(2, _item_f03_Length * 400)
            sheet1.SetColumnWidth(3, _item_f04_Length * 400)
            sheet1.SetColumnWidth(4, _item_f05_Length * 400)
            sheet1.SetColumnWidth(5, _item_f06_Length * 400)


            '================== create row ===================
            j += 1
            Dim _ClientData = (From c In dc.tblNoticeDetails _
                          Where c.NoticeID.Equals(NotificationID) _
                          And (c.f14.Equals("3") Or c.f14.Equals("2"))).ToList()

            Dim _ClientGrp = (From c In _ClientData _
                    Group By clientcode = c.Code Into MyGroup = Group _
                    Select clientcode).ToList()

            Dim itemNo As Integer = 0
            For Each _ClientCode As String In _ClientGrp
                itemNo += 1
                Dim tmp = (From c In _ClientData Where c.Code.Equals(_ClientCode)).ToList()
                Dim _groupnumber As Integer = 0

                For Each _item In tmp

                    Dim row As IRow = sheet1.CreateRow(j)
                    '========================== Data =============================
                    If itemNo Mod 2 > 0 Then
                        row.CreateCell(0).CellStyle = Row_Style_Left
                        row.CreateCell(1).CellStyle = Row_Style_Left
                        row.CreateCell(2).CellStyle = Row_Style_Left
                        row.CreateCell(3).CellStyle = Row_Style_Left
                        row.CreateCell(4).CellStyle = Row_Style_Left
                        row.CreateCell(5).CellStyle = Row_Style_Left
                    Else
                        row.CreateCell(0).CellStyle = Row_Style_Left_bg
                        row.CreateCell(1).CellStyle = Row_Style_Left_bg
                        row.CreateCell(2).CellStyle = Row_Style_Left_bg
                        row.CreateCell(3).CellStyle = Row_Style_Left_bg
                        row.CreateCell(4).CellStyle = Row_Style_Left_bg
                        row.CreateCell(5).CellStyle = Row_Style_Left_bg
                    End If


                    If _groupnumber = 0 Then
                        row.GetCell(0).SetCellValue(itemNo)
                        row.GetCell(1).SetCellValue(_item.Code.Trim())
                        row.GetCell(2).SetCellValue(_item.f01.Trim())
                    End If

                    row.GetCell(3).SetCellValue(_item.f11.Trim())
                    If Not _item.f12 Is Nothing Then row.GetCell(4).SetCellValue(_item.f12.Trim())
                    row.GetCell(5).SetCellValue(_item.f13.Trim())

                    _groupnumber += 1
                    j += 1
                Next

                'Dim column1 = New CellRangeAddress(j - tmp.Count, j - 1, 1, 1)
                'Dim column2 = New CellRangeAddress(j - tmp.Count, j - 1, 1, 1)
                'Dim column3 = New CellRangeAddress(j - tmp.Count, j - 1, 1, 1)
                sheet1.AddMergedRegion(New CellRangeAddress(j - tmp.Count, j - 1, 0, 0))
                sheet1.AddMergedRegion(New CellRangeAddress(j - tmp.Count, j - 1, 1, 1))
                sheet1.AddMergedRegion(New CellRangeAddress(j - tmp.Count, j - 1, 2, 2))
            Next




            '================== init  sheet property =================
            Dim _data = (From c In dc.tblNoticeHeaders Where c.NoticeID.Equals(NotificationID)).FirstOrDefault()

            Dim _filename = String.Format("{0}.xls", System.Guid.NewGuid().ToString())
            Dim _fileDataPath = String.Format("{0}\{1}", Server.MapPath("~/upload"), _filename)
            Dim fs As New FileStream(_fileDataPath, FileMode.Create)
            hssfworkbook.Write(fs)
            fs.Close()


            Try

                Dim strMailFrom As String = "" '"nissan2@asia.lockton.com"
                Dim strMailCC As String = ""

                Dim strMailTo As String = "" ' _agent.Mailto
                '"siriwimol@asia.lockton.com,dusit@asia.lockton.com,parichat@asia.lockton.com" '_agent.Mailto '  Replace("dusit@asia.lockton.com,parichat@asia.lockton.com", ";", ",") '

                Dim strSubject As String = ""
                Dim strMessage As New StringBuilder()


                Using dc_portal = New DataClasses_PortalDataContextExt


                    Dim _mailNotification = (From c In dc_portal.MailNotifications Where c.Code.Equals(_MailNotifications_Amend)).FirstOrDefault()
                    'รายการแก้ไข Amendment ประจำวันที่ {date}  จำนวน {items} รายการ
                    strSubject = _mailNotification.MailSubject.Replace("{items}", itemNo.ToString())

                    'strMessage.AppendLine("<!DOCTYPE html><html><body>")
                    'strMessage.AppendLine(Server.HtmlDecode(_mailNotification.MailBody.Replace("{items}", itemNo.ToString())))
                    'strMessage.AppendLine("</body></html>")


                    Dim _MailBody As String = Server.HtmlDecode(_mailNotification.MailBody.Replace("{items}", itemNo.ToString()))

                    _MailBody = _MailBody.Replace("{displayName}", _displayName)
                    _MailBody = _MailBody.Replace("{title}", _title)
                    _MailBody = _MailBody.Replace("{department}", _department)
                    _MailBody = _MailBody.Replace("{company}", _company)
                    _MailBody = _MailBody.Replace("{streetAddress}", _streetAddress)
                    _MailBody = _MailBody.Replace("{telephoneNumber}", _telephoneNumber)
                    _MailBody = _MailBody.Replace("{facsimileTelephoneNumber}", _facsimileTelephoneNumber)
                    _MailBody = _MailBody.Replace("{mail}", _mail)

                    strMessage.AppendLine(_MailBody)

                    strMailFrom = _mail
                    strMailCC = _mailNotification.MailCC & ";" & _mail
                    strMailTo = _mailNotification.MailTo

                    'strMailTo = "dusit@asia.lockton.com;ratika@asia.lockton.com" '_agent.Mailto
                End Using


                Dim MySmtpClient As New System.Net.Mail.SmtpClient(ConfigurationSettings.AppSettings("smtp"))
                'MySmtpClient.Credentials = New System.Net.NetworkCredential("Your-username", "Your-password")

                'Dim msg As New System.Net.Mail.MailMessage(strMailFrom, strMailTo, strSubject, strMessage.ToString())
                Dim msg As New System.Net.Mail.MailMessage()

                '===========================================================
                Dim bodyHTML As String = "<html><body>" & strMessage.ToString().Replace("{items}", _ClientData.Count.ToString()) & "<span style='font-size:16.0pt;font-family:Angsana New,serif;color:green'><img src='cid:LOGO_IMAGE2' alt='Logo' /><i>Please consider the environment before printing this e-mail.</i></span></body></html>"
                Dim alternateView As AlternateView = alternateView.CreateAlternateViewFromString(bodyHTML, Nothing, "text/html")

                'Dim path_to_the_image_file1 As String = String.Format("{0}\{1}", Server.MapPath("~/images"), "maillockton30.jpg")
                Dim path_to_the_image_file2 As String = String.Format("{0}\{1}", Server.MapPath("~/images"), "mailsmallpicture.jpg")

                'Create the LinkedResource here
                'Dim logo1 As New LinkedResource(path_to_the_image_file1, "image/jpeg")  'Content Type is set as image/jpeg
                'logo1.ContentId = "LOGO_IMAGE1"
                'logo1.TransferEncoding = Net.Mime.TransferEncoding.Base64
                'alternateView.LinkedResources.Add(logo1)

                Dim logo2 As New LinkedResource(path_to_the_image_file2, "image/jpeg")  'Content Type is set as image/jpeg
                logo2.ContentId = "LOGO_IMAGE2"
                logo2.TransferEncoding = Net.Mime.TransferEncoding.Base64
                alternateView.LinkedResources.Add(logo2)
                msg.AlternateViews.Add(alternateView)
                '===========================================================

                msg.Subject = strSubject 'Subject
                msg.Body = Nothing 'Body

                msg.From = New MailAddress(strMailFrom) 'Mail From

                Dim _MailTo = strMailTo.Split(";") 'Mail To
                For Each item In _MailTo
                    If Not String.IsNullOrEmpty(item.Trim()) Then msg.To.Add(item)
                Next

                Dim _MailCC = strMailCC.Split(";") 'Mail CC
                For Each item In _MailCC
                    If Not String.IsNullOrEmpty(item.Trim()) Then msg.CC.Add(item)
                Next

                msg.BodyEncoding = Encoding.UTF8
                msg.IsBodyHtml = True
                msg.Priority = Net.Mail.MailPriority.High

                Dim att_data = New Attachment(_fileDataPath)
                att_data.Name = String.Format("แก้ไข Amend จำนวน {0} รายการ.xls", itemNo.ToString())
                msg.Attachments.Add(att_data)

                MySmtpClient.Send(msg)


                Alert.Show("Send")
            Catch ex As Exception
                Throw ex
            End Try

        End Using






    End Sub



End Class
