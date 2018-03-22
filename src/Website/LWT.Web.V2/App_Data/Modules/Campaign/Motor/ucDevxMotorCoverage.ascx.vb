Imports System.Data
Imports System.Web.Security
Imports Portal.Components
Imports BaseConfig
Imports DataBind
Imports System.IO
Imports DevExpress.Web
Imports DevExpress.Web.Data

Partial Class Modules_ucDevxMotorCoverage
    Inherits PortalModuleControl

    Private RiskGroupID As Integer = 3

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        SqlDataSource_MotorCoverage.SelectParameters("RiskGroupID").DefaultValue = RiskGroupID

        SqlDataSource_MotorCoverage.InsertParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name
        SqlDataSource_MotorCoverage.InsertParameters("RiskGroupID").DefaultValue = RiskGroupID

        SqlDataSource_MotorCoverage.UpdateParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name


        If (Not IsPostBack) Then
            'SqlDataSource_Coverage.SelectParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name
            'SqlDataSource_ProjectRisk.SelectParameters("UserName").DefaultValue = HttpContext.Current.User.Identity.Name







        End If
    End Sub
    'btnCoverageFormat
    Protected Sub btnCoverageFormat_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCoverageFormat.Click
        Dim fileName = String.Format("{0}.xls", System.Guid.NewGuid.ToString())
        Dim filePath = String.Format("{0}\{1}", Server.MapPath("~/Template"), "MotorCoverage.xlsx")



        Response.ClearContent()
        Response.AddHeader("content-disposition", "attachment; filename=" & fileName)
        Response.ContentType = "application/octet-stream"
        Response.WriteFile(filePath)
        Response.End()

    End Sub

    Protected Sub btnImportCoverage_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnImportCoverage.Click
        popUpAddCoverage.ShowOnPageLoad = True
    End Sub
    Protected Sub btnImportCoverageData_click(ByVal sender As Object, ByVal e As EventArgs) Handles btnImportCoverageData.Click
        importdata()
        popUpAddCoverage.ShowOnPageLoad = False
        gridCoverage.DataBind()
    End Sub
    Private Sub importdata()

        Dim dc = New DataClasses_CPSExt()


        Dim _dataList As New List(Of tblCoverage)

        Dim i As Integer = 0
        Dim reader = New StringReader(tbxdata.Text)
        Dim line As String
        line = reader.ReadLine()
        While line IsNot Nothing
            Dim _row() As String = line.Split(vbTab)

            '--ข้อเสนอเบี้ยประกันภัยรถยนต์:	 
            '--- ลักษณะการใช้:	coverage1
            '--- ประเภทกรมธรรม์:	coverage2
            '--- ลักษณะอู่:	coverage3

            '--ความรับผิดต่อบุคคลภายนอก:	 
            '--- บาดเจ็บหรือเสียชีวิต/คน(บาท):	coverage4
            '--- บาดเจ็บหรือเสียชัวิต/ครั้ง(บาท):	coverage5	
            '--- ทรัพย์สิน(บาท):			coverage6

            '--ความเสียหายต่อรถยนต์:	 
            '--- ความเสียหายต่อรถยนต์:	coverage7
            '--- รถยนต์สูญหาย/ไฟไหม้:	coverage8
            '--- คุ้มครองน้ำท่วม:		coverage9

            '--ความคุ้มครองตาม ร.ย. 01 การประกันภัยอุบัติเหตุส่วนบุคคล:	 
            '--- ประกันภัยอุบัติเหตุผู้ขับขี่(บาท):	coverage10
            '--- จำนวนผู้โดยสาร(คน):		coverage11
            '--- ประกันภัยอุบัติเหตุผู้โดยสาร(บาท):	coverage12

            '--ความคุ้มครองตาม ร.ย. 02 การประกันภัยค่ารักษาพยาบาล:	 
            '--- ค่ารักษาพยาบาล(บาท):		coverage13

            '--ความคุ้มครองตาม ร.ย. 03 การประกันตัวผู้ขับขี่:	 
            '--- การประกันตัวผู้ขับขี่(บาท):		coverage14

            '--ความเสียหายส่วนแรก (Deductible):	 
            '--- ความเสียหายส่วนแรก(บาท) :		coverage15

            Dim _Coverage1 = _row(0).ToString()
            Dim _Coverage2 = _row(1).ToString()
            Dim _Coverage3 = _row(2).ToString()
            Dim _Coverage4 = _row(3).ToString()
            Dim _Coverage5 = _row(4).ToString()
            Dim _Coverage6 = _row(5).ToString()
            Dim _Coverage7 = _row(6).ToString()
            Dim _Coverage8 = _row(7).ToString()
            Dim _Coverage9 = _row(8).ToString()
            Dim _Coverage10 = _row(9).ToString()
            Dim _Coverage11 = _row(10).ToString()
            Dim _Coverage12 = _row(11).ToString()
            Dim _Coverage13 = _row(12).ToString()
            Dim _Coverage14 = _row(13).ToString()
            Dim _Coverage15 = _row(14).ToString()



            Select Case _Coverage2
                Case "2" 'ประเภท 2
                    _Coverage7 = "-"
                    _Coverage9 = "-"
                Case "3" 'ประเภท 3
                    _Coverage7 = "-"
                    _Coverage8 = "-"
                    _Coverage9 = "-"
                Case "4" 'ประเภท 3 พิเศษ
                    _Coverage8 = "-"
                    _Coverage9 = "-"
                Case "5" 'ประเภท 2 พิเศษ
                    _Coverage9 = "-"
            End Select

            _dataList.Add(New tblCoverage With {.RiskGroupID = RiskGroupID _
                                                                            , .Coverage1 = _Coverage1 _
                                                                            , .Coverage2 = _Coverage2 _
                                                                            , .Coverage3 = _Coverage3 _
                                                                            , .Coverage4 = _Coverage4 _
                                                                            , .Coverage5 = _Coverage5 _
                                                                            , .Coverage6 = _Coverage6 _
                                                                            , .Coverage7 = _Coverage7 _
                                                                            , .Coverage8 = _Coverage8 _
                                                                            , .Coverage9 = _Coverage9 _
                                                                            , .Coverage10 = _Coverage10 _
                                                                            , .Coverage11 = _Coverage11 _
                                                                            , .Coverage12 = _Coverage12 _
                                                                            , .Coverage13 = _Coverage13 _
                                                                            , .Coverage14 = _Coverage14 _
                                                                            , .Coverage15 = _Coverage15 _
                                                                           })


            line = reader.ReadLine()
        End While


        dc.tblCoverages.InsertAllOnSubmit(_dataList)

        dc.SubmitChanges()

        tbxdata.Text = ""

    End Sub

    'Protected Sub gridView_HtmlEditFormCreated(sender As Object, e As DevExpress.Web.ASPxGridViewEditFormEventArgs) Handles gridCoverage.HtmlEditFormCreated
    '    TryCast(e.EditForm.NamingContainer, DevExpress.Web.Rendering.GridViewHtmlEditFormPopup).ShowPageScrollbarWhenModal = True
    'End Sub
    Protected Sub ASPxGridViewIntance_HtmlEditFormCreated(ByVal sender As Object, ByVal e As ASPxGridViewEditFormEventArgs) Handles gridCoverage.HtmlEditFormCreated
        Dim popup As DevExpress.Web.Rendering.GridViewEditFormPopup = TryCast(e.EditForm.NamingContainer, DevExpress.Web.Rendering.GridViewEditFormPopup)
        popup.ShowPageScrollbarWhenModal = True        
    End Sub

    Protected Sub gridProject_HtmlRowCreated(ByVal sender As Object, ByVal e As DevExpress.Web.ASPxGridViewTableRowEventArgs) Handles gridCoverage.HtmlRowPrepared
        'If e.RowType <> DevExpress.Web.GridViewRowType.Data Then
        '    Return
        'End If
        'Dim _CoverageID = e.KeyValue().ToString()
        'Dim _Editable = e.GetValue("Editable").ToString()
        'Dim RowEditable As ASPxButton = gridCoverage.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "RowEditable")
        'Dim RowDeleteable As ASPxButton = gridCoverage.FindRowCellTemplateControl(e.VisibleIndex, Nothing, "RowDeleteable")
        'If _Editable.Equals("1") Then
        '    RowEditable.Visible = True
        '    RowDeleteable.Visible = True
        '    If RowEditable.JSProperties.Keys.Count = 0 Then
        '        RowEditable.JSProperties.Add("cpVisibleIndex", e.VisibleIndex)
        '    End If
        '    If RowDeleteable.JSProperties.Keys.Count = 0 Then
        '        RowDeleteable.JSProperties.Add("cpVisibleIndex", e.VisibleIndex)
        '    End If
        'Else
        '    RowEditable.Visible = False
        '    RowDeleteable.Visible = False           
        'End If



    End Sub



    Protected Sub grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs) Handles gridCoverage.RowUpdating
        Dim _gridCoverage = DirectCast(sender, ASPxGridView)
        Dim _formCoverage As ASPxFormLayout = TryCast(_gridCoverage.FindEditFormTemplateControl("formCoverage"), ASPxFormLayout)

        Dim Coverage1 As ASPxComboBox = TryCast(_formCoverage.FindControl("Coverage1"), ASPxComboBox)
        Dim Coverage2 As ASPxComboBox = TryCast(_formCoverage.FindControl("Coverage2"), ASPxComboBox)
        Dim Coverage3 As ASPxComboBox = TryCast(_formCoverage.FindControl("Coverage3"), ASPxComboBox)
        Dim Coverage4 As ASPxComboBox = TryCast(_formCoverage.FindControl("Coverage4"), ASPxComboBox)
        Dim Coverage5 As ASPxComboBox = TryCast(_formCoverage.FindControl("Coverage5"), ASPxComboBox)
        Dim Coverage6 As ASPxComboBox = TryCast(_formCoverage.FindControl("Coverage6"), ASPxComboBox)

        Dim Coverage7 As ASPxComboBox = TryCast(_formCoverage.FindControl("Coverage7"), ASPxComboBox)
        Dim Coverage8 As ASPxComboBox = TryCast(_formCoverage.FindControl("Coverage8"), ASPxComboBox)
        Dim Coverage9 As ASPxComboBox = TryCast(_formCoverage.FindControl("Coverage9"), ASPxComboBox)

        Dim Coverage10 As ASPxSpinEdit = TryCast(_formCoverage.FindControl("Coverage10"), ASPxSpinEdit)
        Dim Coverage11 As ASPxSpinEdit = TryCast(_formCoverage.FindControl("Coverage11"), ASPxSpinEdit)
        Dim Coverage12 As ASPxSpinEdit = TryCast(_formCoverage.FindControl("Coverage12"), ASPxSpinEdit)
        Dim Coverage13 As ASPxSpinEdit = TryCast(_formCoverage.FindControl("Coverage13"), ASPxSpinEdit)
        Dim Coverage14 As ASPxSpinEdit = TryCast(_formCoverage.FindControl("Coverage14"), ASPxSpinEdit)
        Dim Coverage15 As ASPxSpinEdit = TryCast(_formCoverage.FindControl("Coverage15"), ASPxSpinEdit)

        e.NewValues("Coverage1") = Coverage1.Value
        e.NewValues("Coverage2") = Coverage2.Value
        e.NewValues("Coverage3") = Coverage3.Value
        e.NewValues("Coverage4") = Coverage4.Value
        e.NewValues("Coverage5") = Coverage5.Value
        e.NewValues("Coverage6") = Coverage6.Value

        e.NewValues("Coverage7") = Coverage7.Value
        e.NewValues("Coverage8") = Coverage8.Value
        e.NewValues("Coverage9") = Coverage9.Value

        e.NewValues("Coverage10") = Coverage10.Value
        e.NewValues("Coverage11") = Coverage11.Value
        e.NewValues("Coverage12") = Coverage12.Value

        e.NewValues("Coverage13") = Coverage13.Value
        e.NewValues("Coverage14") = Coverage14.Value
        e.NewValues("Coverage15") = Coverage15.Value

    End Sub
    Protected Sub grid_RowInserting(ByVal sender As Object, ByVal e As ASPxDataInsertingEventArgs) Handles gridCoverage.RowInserting
        Dim _gridCoverage = DirectCast(sender, ASPxGridView)
        Dim _formCoverage As ASPxFormLayout = TryCast(_gridCoverage.FindEditFormTemplateControl("formCoverage"), ASPxFormLayout)

        Dim Coverage1 As ASPxComboBox = TryCast(_formCoverage.FindControl("Coverage1"), ASPxComboBox)
        Dim Coverage2 As ASPxComboBox = TryCast(_formCoverage.FindControl("Coverage2"), ASPxComboBox)
        Dim Coverage3 As ASPxComboBox = TryCast(_formCoverage.FindControl("Coverage3"), ASPxComboBox)
        Dim Coverage4 As ASPxComboBox = TryCast(_formCoverage.FindControl("Coverage4"), ASPxComboBox)
        Dim Coverage5 As ASPxComboBox = TryCast(_formCoverage.FindControl("Coverage5"), ASPxComboBox)
        Dim Coverage6 As ASPxComboBox = TryCast(_formCoverage.FindControl("Coverage6"), ASPxComboBox)

        Dim Coverage7 As ASPxComboBox = TryCast(_formCoverage.FindControl("Coverage7"), ASPxComboBox)
        Dim Coverage8 As ASPxComboBox = TryCast(_formCoverage.FindControl("Coverage8"), ASPxComboBox)
        Dim Coverage9 As ASPxComboBox = TryCast(_formCoverage.FindControl("Coverage9"), ASPxComboBox)

        Dim Coverage10 As ASPxSpinEdit = TryCast(_formCoverage.FindControl("Coverage10"), ASPxSpinEdit)
        Dim Coverage11 As ASPxSpinEdit = TryCast(_formCoverage.FindControl("Coverage11"), ASPxSpinEdit)
        Dim Coverage12 As ASPxSpinEdit = TryCast(_formCoverage.FindControl("Coverage12"), ASPxSpinEdit)
        Dim Coverage13 As ASPxComboBox = TryCast(_formCoverage.FindControl("Coverage13"), ASPxComboBox)
        Dim Coverage14 As ASPxSpinEdit = TryCast(_formCoverage.FindControl("Coverage14"), ASPxSpinEdit)
        Dim Coverage15 As ASPxSpinEdit = TryCast(_formCoverage.FindControl("Coverage15"), ASPxSpinEdit)

        e.NewValues("Coverage1") = Coverage1.Value
        e.NewValues("Coverage2") = Coverage2.Value
        e.NewValues("Coverage3") = Coverage3.Value
        e.NewValues("Coverage4") = Coverage4.Value
        e.NewValues("Coverage5") = Coverage5.Value
        e.NewValues("Coverage6") = Coverage6.Value

        e.NewValues("Coverage7") = Coverage7.Value
        e.NewValues("Coverage8") = Coverage8.Value
        e.NewValues("Coverage9") = Coverage9.Value

        e.NewValues("Coverage10") = Coverage10.Value
        e.NewValues("Coverage11") = Coverage11.Value
        e.NewValues("Coverage12") = Coverage12.Value

        e.NewValues("Coverage13") = Coverage13.Value
        e.NewValues("Coverage14") = Coverage14.Value
        e.NewValues("Coverage15") = Coverage15.Value
    End Sub


End Class
